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
$dal=[TOMWMS.clsLnTrans_licencia_empaque]
$entity=[TOMWMS.clsBeTrans_licencia_empaque]
$flags=[Reflection.BindingFlags]'Static,NonPublic'
$generate=$dal.GetMethod('GenerarEnTransaccion',$flags)
$recycle=$dal.GetMethod('RegistrarTarimasCompletas',$flags)
$insert=$dal.GetMethod('Insertar')
function Query([string]$sql,$tx=$null){
    $cmd=$cn.CreateCommand();$cmd.Transaction=$tx;$cmd.CommandText=$sql;$cmd.CommandTimeout=30
    try { return $cmd.ExecuteScalar() } finally {$cmd.Dispose()}
}
function Execute([string]$sql,$tx){
    $cmd=$cn.CreateCommand();$cmd.Transaction=$tx;$cmd.CommandText=$sql;$cmd.CommandTimeout=30
    try{[void]$cmd.ExecuteNonQuery()}finally{$cmd.Dispose()}
}
function Requests {
    $list=New-Object 'System.Collections.Generic.List[TOMWMS.clsBeTrans_licencia_empaque]'
    $e=New-Object TOMWMS.clsBeTrans_licencia_empaque
    $e.IdBodega=7;$e.TipoOrigen='PEDIDO';$e.IdDocumento=[long]126;$e.User_agr='EJC_TEST_ROLLBACK'
    $list.Add($e);return ,$list
}
function Assert($condition,[string]$message){if(-not $condition){throw $message}}
function Scenario([string]$name,[scriptblock]$body){
    $tx=$cn.BeginTransaction([Data.IsolationLevel]::ReadCommitted)
    try{& $body $tx;Write-Output "PASS $name"}finally{$tx.Rollback();$tx.Dispose()}
}
try{
    $cn.Open()
    $baseline=[int](Query 'SELECT COUNT(*) FROM dbo.trans_licencia_empaque')
    $user=[int](Query 'SELECT TOP(1) IdUsuario FROM dbo.Resolucion_lp_usuario WHERE IdBodega=7 AND Activo=1 AND Correlativo_Final>Correlativo_Actual+2 ORDER BY IdResolucionLpUsuario')
    Assert ($user -gt 0) 'Falta resolución activa de prueba.'
    $correlativo=Query "SELECT TOP(1) Correlativo_Actual FROM dbo.Resolucion_lp_usuario WHERE IdBodega=7 AND IdUsuario=$user AND Activo=1 ORDER BY IdResolucionLpUsuario"
    Scenario '2 completas + 1 adicional, repetición idempotente, correlativo solo adicionales' {
        param($tx)
        $first=$generate.Invoke($null,@((Requests).PSObject.BaseObject,$user,'EJC_TEST_ROLLBACK',88,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))
        $reused=@($first|Where-Object EsRecicladaRecepcion)
        $new=@($first|Where-Object {-not $_.EsRecicladaRecepcion})
        Write-Output ('RESULT '+$first.Count+' RECICLADAS '+$reused.Count+' NUEVAS '+$new.Count)
        $first | Select-Object LicPlate,EsRecicladaRecepcion | Format-Table -AutoSize
        Assert ($reused.Count -eq 2 -and $new.Count -eq 1) 'Resultado esperado 2+1.'
        Assert ((($reused.LicPlate|Sort-Object)-join ',') -eq 'JM000141,JM000144') 'LP originales distintas.'
        $second=$generate.Invoke($null,@((Requests).PSObject.BaseObject,$user,'EJC_TEST_ROLLBACK',88,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))
        $reused2=@($second|Where-Object EsRecicladaRecepcion)
        Assert ((($reused.IdLicenciaEmpaque|Sort-Object)-join ',') -eq (($reused2.IdLicenciaEmpaque|Sort-Object)-join ',')) 'Recicladas duplicadas.'
        Assert ((Query "SELECT COUNT(*) FROM dbo.trans_licencia_empaque WHERE LicPlate IN ('JM000141','JM000144')" $tx) -eq 2) 'Duplicación de cabeceras.'
        Assert ((Query "SELECT TOP(1) Correlativo_Actual FROM dbo.Resolucion_lp_usuario WHERE IdBodega=7 AND IdUsuario=$user AND Activo=1 ORDER BY IdResolucionLpUsuario" $tx) -eq ($correlativo+2)) 'Consumo incorrecto de resolución.'
        Assert ((Query "SELECT SUM(CantidadImpresiones) FROM dbo.trans_licencia_empaque WHERE LicPlate IN ('JM000141','JM000144')" $tx) -eq 0) 'Impresiones ficticias.'
        # La validación pública HH/packing debe reconocer el código de recepción.
        $settings=[Configuration.ConfigurationManager]::AppSettings
        $field=[Collections.Specialized.NameObjectCollectionBase].GetField('_readOnly',[Reflection.BindingFlags]'Instance,NonPublic')
        $field.SetValue($settings,$false);$settings.Set('CST',$b.ConnectionString)
        # Leer desde otra conexión bloquearía las filas de la prueba; se valida tras
        # commit únicamente en uso real. GetSingleByLicPlate comparte la transacción.
        $get=$dal.GetMethod('GetSingleByLicPlate')
        $lp=$get.Invoke($null,@('JM000141',$true,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))
        Assert ($lp.EsRecicladaRecepcion -and $lp.TipoOrigen -eq 'PEDIDO' -and $lp.IdDocumento -eq 126 -and $lp.Estado -eq 'GENERADA') 'Contrato packing incorrecto.'
    }
    $mutations=@{
        'reserva parcial'='UPDATE dbo.stock_res SET cantidad=249 WHERE IdStockRes=733';
        'stock parcial'='UPDATE dbo.stock SET cantidad=249 WHERE IdStock=1419 AND IdBodega=7';
        'recepción original distinta'='UPDATE dbo.trans_re_det SET cantidad_recibida=9 WHERE IdRecepcionDet=801';
        'lote distinto aunque mismo total'="UPDATE dbo.stock_res SET lote='EJC_TEST_LOTE' WHERE IdStockRes=733";
        'picking parcial'='UPDATE dbo.trans_picking_ubic SET cantidad_solicitada=9 WHERE IdPickingUbic=480';
        'reserva en otro pedido'='UPDATE dbo.stock_res SET IdPedido=125 WHERE IdStockRes=733'
    }
    foreach($name in $mutations.Keys){
        $mutation=$mutations[$name]
        Scenario "descarta JM000144: $name" {
            param($tx)
            Execute $mutation $tx
            $rows=$recycle.Invoke($null,@((Requests).PSObject.BaseObject,88,'EJC_TEST_ROLLBACK',$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))
            Assert ($rows.Count -eq 1 -and $rows[0].LicPlate -eq 'JM000141') 'Se recicló una tarima parcial o inconsistente.'
        }
    }
    Scenario 'picking granel conserva generación sin reciclaje' {
        param($tx)
        Execute "UPDATE dbo.trans_picking_enc SET Tipo_Preparacion='Granel' WHERE IdPickingEnc=88" $tx
        $rows=$recycle.Invoke($null,@((Requests).PSObject.BaseObject,88,'EJC_TEST_ROLLBACK',$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))
        Assert ($rows.Count -eq 0) 'Granel no debe reciclar tarimas.'
    }
    Scenario 'LP existente de origen incompatible bloquea el lote' {
        param($tx)
        $item=New-Object TOMWMS.clsBeTrans_licencia_empaque
        $item.IdBodega=7;$item.LicPlate='JM000141';$item.TipoOrigen='MANUAL';$item.User_agr='EJC_TEST_ROLLBACK'
        $args=[object[]]@($item.PSObject.BaseObject,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject);[void]$insert.Invoke($null,$args)
        $failed=$false
        try{[void]$generate.Invoke($null,@((Requests).PSObject.BaseObject,$user,'EJC_TEST_ROLLBACK',88,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))}catch{$failed=$true}
        Assert $failed 'Se aceptó una licencia incompatible.'
    }
    Scenario 'licencia anulada no se reactiva' {
        param($tx)
        $rows=$recycle.Invoke($null,@((Requests).PSObject.BaseObject,88,'EJC_TEST_ROLLBACK',$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))
        Execute "UPDATE dbo.trans_licencia_empaque SET Estado='ANULADA',FechaAnulacion=GETDATE(),MotivoAnulacion='EJC_TEST_ROLLBACK' WHERE LicPlate='JM000141'" $tx
        $failed=$false
        try{[void]$recycle.Invoke($null,@((Requests).PSObject.BaseObject,88,'EJC_TEST_ROLLBACK',$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))}catch{$failed=$true}
        Assert $failed 'Se reactivó una licencia anulada.'
    }
    Scenario 'picking despachado bloquea reciclaje' {
        param($tx)
        Execute "UPDATE dbo.trans_picking_enc SET Estado='Despachado' WHERE IdPickingEnc=88" $tx
        $failed=$false
        try{[void]$recycle.Invoke($null,@((Requests).PSObject.BaseObject,88,'EJC_TEST_ROLLBACK',$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))}catch{$failed=$true}
        Assert $failed 'Se recicló un picking despachado.'
    }
    Scenario 'sin contexto mantiene generación tradicional' {
        param($tx)
        $rows=$generate.Invoke($null,@((Requests).PSObject.BaseObject,$user,'EJC_TEST_ROLLBACK',0,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))
        Assert ($rows.Count -eq 1 -and -not $rows[0].EsRecicladaRecepcion) 'Se alteró generación tradicional.'
    }
    Scenario 'pedido ajeno al picking bloquea el lote' {
        param($tx)
        $req=Requests;$req[0].IdDocumento=[long]125
        $failed=$false
        try{[void]$generate.Invoke($null,@($req.PSObject.BaseObject,$user,'EJC_TEST_ROLLBACK',88,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))}catch{$failed=$true}
        Assert $failed 'Se aceptó un pedido ajeno al picking.'
    }
    Assert ((Query 'SELECT COUNT(*) FROM dbo.trans_licencia_empaque') -eq $baseline) 'Se persistieron licencias de prueba.'
    Assert ((Query "SELECT TOP(1) Correlativo_Actual FROM dbo.Resolucion_lp_usuario WHERE IdBodega=7 AND IdUsuario=$user AND Activo=1 ORDER BY IdResolucionLpUsuario") -eq $correlativo) 'Se persistió consumo de resolución.'
    Write-Output 'PASS rollback final: sin licencias ni correlativos de prueba persistidos'
}finally{$cn.Dispose()}
