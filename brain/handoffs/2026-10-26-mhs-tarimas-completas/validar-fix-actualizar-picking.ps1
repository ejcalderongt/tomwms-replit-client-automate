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
$save=$dal.GetMethod('Guarda_Trans_picking_enc')
function ReadPicking($tx){
 $e=New-Object TOMWMS.clsBeTrans_picking_enc;$e.IdPickingEnc=88
 $args=[object[]]@($e.PSObject.BaseObject,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject)
 if(-not $get.Invoke($null,$args)){throw 'Picking inexistente'}
 return $args[0]
}
function Check($e,$required,$type,$confirm,$photo){
 if($e.Requiere_Preparacion -ne $required -or $e.Tipo_Preparacion -ne $type -or $e.Confirmar_Packing_Entrega -ne $confirm -or $e.Fotografia_Verificacion -ne $photo){throw 'Flags persistidos no coinciden'}
}

function Exec($sql,$tx){$q=$cn.CreateCommand();$q.Transaction=$tx;$q.CommandText=$sql;try{[void]$q.ExecuteNonQuery()}finally{$q.Dispose()}}
function SavePicking($e,$tx){$e.IsNew=$false;$args=[object[]]@($e.PSObject.BaseObject,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject);[void]$save.Invoke($null,$args)}
try{
 $cn.Open();$tx=$cn.BeginTransaction()
 try{
  $original=ReadPicking $tx
  Exec "UPDATE trans_picking_enc SET Estado_Preparacion='Finalizado',Fecha_Inicio_Preparacion='20261025 08:00',Fecha_Fin_Preparacion='20261025 09:00' WHERE IdPickingEnc=88 AND IdBodega=7" $tx
  $e=ReadPicking $tx
  $e.Estado_Preparacion='Nuevo';$e.Fecha_Inicio_Preparacion=[datetime]'1900-01-01';$e.Fecha_Fin_Preparacion=[datetime]'1900-01-01'
  $e.Confirmar_Packing_Entrega=-not $e.Confirmar_Packing_Entrega
  SavePicking $e $tx
  $after=ReadPicking $tx
  if($after.Estado_Preparacion -ne 'Finalizado' -or $after.Fecha_Inicio_Preparacion -ne [datetime]'2026-10-25 08:00' -or $after.Fecha_Fin_Preparacion -ne [datetime]'2026-10-25 09:00'){throw 'Perdió avance vigente'}
  Write-Output 'PASS conserva estado/fechas vigentes incluso con snapshot obsoleto; otros flags sí actualizan'
  $after.Tipo_Preparacion='Granel';$failed=$false
  try{SavePicking $after $tx}catch{$failed=$true}
  if(-not $failed){throw 'Permitió cambio con avance'}
  Write-Output 'PASS bloquea cambio Tarima -> Granel con preparación finalizada'
  Exec "UPDATE trans_picking_enc SET Estado_Preparacion='Nuevo',Fecha_Inicio_Preparacion='19000101',Fecha_Fin_Preparacion='19000101' WHERE IdPickingEnc=88 AND IdBodega=7" $tx
  foreach($type in @('','Granel','Tarima')){
   $e=ReadPicking $tx;$e.Tipo_Preparacion=$type;$e.Requiere_Preparacion=($type -ne '')
   SavePicking $e $tx;$after=ReadPicking $tx
   $expected=if($type -eq ''){'N/A'}else{'Nuevo'}
   if($after.Tipo_Preparacion -ne $type -or $after.Estado_Preparacion -ne $expected){throw 'Cambio sin avance incorrecto'}
   Write-Output "PASS cambio sin avance: tipo='$type', estado='$expected'"
  }
 }finally{$tx.Rollback();$tx.Dispose()}
 $tx=$cn.BeginTransaction()
 try{
  $after=ReadPicking $tx
  Check $after $original.Requiere_Preparacion $original.Tipo_Preparacion $original.Confirmar_Packing_Entrega $original.Fotografia_Verificacion
  if($after.Estado_Preparacion -ne $original.Estado_Preparacion -or $after.Fecha_Inicio_Preparacion -ne $original.Fecha_Inicio_Preparacion -or $after.Fecha_Fin_Preparacion -ne $original.Fecha_Fin_Preparacion){throw 'Rollback incompleto'}
  Write-Output 'PASS rollback: flags, estado y fechas originales conservados'
 }finally{$tx.Rollback();$tx.Dispose()}
}finally{$cn.Dispose()}
