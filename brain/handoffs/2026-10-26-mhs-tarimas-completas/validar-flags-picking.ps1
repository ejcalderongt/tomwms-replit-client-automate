param([string]$BofRoot='C:\Users\yejc2\source\repos\TOMWMS_BOF')
$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Data
Add-Type -AssemblyName System.Configuration
$ini=@{}
Get-Content (Join-Path $BofRoot 'TOMIMSV4\TOMIMSV4\Conn.ini') | ForEach-Object {
    if($_ -match '^\s*([^=]+?)\s*=\s*(.*?)\s*$'){$ini[$matches[1]]=$matches[2]}
}
$b=New-Object Data.SqlClient.SqlConnectionStringBuilder
$b['Data Source']='10.0.0.31';$b['Initial Catalog']='TOMWMS_MHS_DEV'
$b['User ID']='tom.wms';$b['Password']=$ini['CLAVE_BD_WMS']
$b['Connect Timeout']=5;$b['TrustServerCertificate']=$true
# Solo DEV; valores de conexión nunca se imprimen ni se guardan.
$cn=New-Object Data.SqlClient.SqlConnection($b.ConnectionString)
$bin=Join-Path $BofRoot 'TOMIMSV4\DAL\bin\Debug'
[void][Reflection.Assembly]::LoadFrom((Join-Path $bin 'Entity.dll'))
[void][Reflection.Assembly]::LoadFrom((Join-Path $bin 'DAL.dll'))

$dal=[TOMWMS.clsLnTrans_picking_enc]
$get=$dal.GetMethods() | Where-Object {$_.Name -eq 'GetSingle' -and $_.GetParameters().Count -eq 3} | Select-Object -First 1
$save=$dal.GetMethod('Actualizar')
function ReadPicking($tx){
 $e=New-Object TOMWMS.clsBeTrans_picking_enc;$e.IdPickingEnc=88
 $args=[object[]]@($e.PSObject.BaseObject,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject)
 if(-not $get.Invoke($null,$args)){throw 'Picking inexistente'}
 return $args[0]
}
function Check($e,$required,$type,$confirm,$photo){
 if($e.Requiere_Preparacion -ne $required -or $e.Tipo_Preparacion -ne $type -or $e.Confirmar_Packing_Entrega -ne $confirm -or $e.Fotografia_Verificacion -ne $photo){throw 'Flags persistidos no coinciden'}
}
try{
 $cn.Open();$tx=$cn.BeginTransaction()
 try{
  $original=ReadPicking $tx
  Write-Output ('BASE picking88: Requiere='+$original.Requiere_Preparacion+' Tipo='+$original.Tipo_Preparacion+' EstadoPrep='+$original.Estado_Preparacion+' ConfirmaEntrega='+$original.Confirmar_Packing_Entrega)
  foreach($case in @(@($false,'',$false,$false),@($true,'Granel',$true,$false),@($true,'Tarima',$false,$true),@($true,'Tarima',$true,$true))){
   $e=ReadPicking $tx
   $e.Requiere_Preparacion=$case[0];$e.Tipo_Preparacion=$case[1];$e.Estado_Preparacion=if($case[0]){'Nuevo'}else{'N/A'}
   $e.Confirmar_Packing_Entrega=$case[2];$e.Fotografia_Verificacion=$case[3]
   $args=[object[]]@($e.PSObject.BaseObject,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject)
   $rows=$save.Invoke($null,$args)
   if($rows -ne 1){throw 'UPDATE no afectó una fila'}
   $roundtrip=ReadPicking $tx
   Check $roundtrip $case[0] $case[1] $case[2] $case[3]
   Write-Output ('PASS UPDATE + GetSingle: Tipo='+$case[1]+' Requiere='+$case[0]+' Entrega='+$case[2]+' Fotografia='+$case[3])
  }
 }finally{$tx.Rollback();$tx.Dispose()}
 $tx=$cn.BeginTransaction()
 try{
  $after=ReadPicking $tx
  Check $after $original.Requiere_Preparacion $original.Tipo_Preparacion $original.Confirmar_Packing_Entrega $original.Fotografia_Verificacion
  Write-Output 'PASS rollback: flags originales conservados'
 }finally{$tx.Rollback();$tx.Dispose()}
}finally{$cn.Dispose()}
