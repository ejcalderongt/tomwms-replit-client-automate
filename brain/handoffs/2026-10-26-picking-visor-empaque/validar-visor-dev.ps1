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
$bin='C:\Users\yejc2\OneDrive\Documents\ChatGPT\TOMWMS\deployments\picking-empaque-20261026'
[void][Reflection.Assembly]::LoadFrom((Join-Path $bin 'Entity.dll'))
[void][Reflection.Assembly]::LoadFrom((Join-Path $bin 'DAL.dll'))

$dal=[TOMWMS.clsLnTrans_licencia_empaque]
$method=$dal.GetMethod('Get_Visor_Empaque_Picking')
function GetView($tx,$warehouse=7){
 $result=$method.Invoke($null,@(88,$warehouse,$cn.PSObject.BaseObject,$tx.PSObject.BaseObject))
 return ,$result
}
function Exec($sql,$tx){$cmd=$cn.CreateCommand();$cmd.Transaction=$tx;$cmd.CommandText=$sql;try{[void]$cmd.ExecuteNonQuery()}finally{$cmd.Dispose()}}
function Scalar($sql,$tx){$cmd=$cn.CreateCommand();$cmd.Transaction=$tx;$cmd.CommandText=$sql;try{return $cmd.ExecuteScalar()}finally{$cmd.Dispose()}}
function Assert($condition,$message){if(-not $condition){throw $message}}
try{
 $cn.Open();$tx=$cn.BeginTransaction()
 try{
  $baselineCount=Scalar 'SELECT COUNT(*) FROM dbo.trans_packing_enc' $tx
  $baseline=GetView $tx
  $parents=$baseline.Tables['Licencias'];$children=$baseline.Tables['Contenido']
  Write-Output ('BASE licencias='+$parents.Rows.Count+' lineas='+$children.Rows.Count)
  Assert ($parents.Rows.Count -ge 4 -and $children.Rows.Count -eq 0) 'Fixture 126/88 cambió: revisar caso real antes de probar.'
  Assert (@($parents.Select("OrigenEtiqueta='REUTILIZADA'")).Count -eq 2) 'Faltan recicladas.'
  Assert (@($parents.Select("EstadoContenido='VACIA'")).Count -eq $parents.Rows.Count) 'Vacías incorrectas.'
  Write-Output 'PASS licencias vacías, nuevas y recicladas sin inventar contenido'
  $fixture=@"
DECLARE @Base int=(SELECT ISNULL(MAX(IdPackingEnc),0) FROM dbo.trans_packing_enc);
INSERT dbo.trans_packing_enc
(IdPackingEnc,IdBodega,IdPickingEnc,IdDespachoEnc,IdProductoBodega,IdProductoEstado,IdPresentacion,IdUnidadMedida,
lote,fecha_vence,lic_plate,no_linea,cantidad_bultos_packing,cantidad_camas_packing,IdOperadorBodega,IdEmpresaServicio,
referencia,fecha_packing,IdPedidoEnc,Finalizado,usr_agr,fec_mod,usr_mod,IdProductoTallaColor,entregado)
SELECT @Base+v.n,p.IdBodega,p.IdPickingEnc,0,p.IdProductoBodega,p.IdProductoEstado,p.IdPresentacion,p.IdUnidadMedida,
p.lote,p.fecha_vence,p.lic_plate,v.LP,1,1,ISNULL((SELECT MIN(IdOperadorBodega) FROM operador_bodega WHERE IdBodega=7),0),
b.IdEmpresa,'EJC_TEST_ROLLBACK',DATEADD(minute,v.n,CAST('2026-10-26T12:00:00' AS datetime)),p.IdPedidoEnc,
v.Finalizado,'EJC_TEST_ROLLBACK',DATEADD(minute,v.n,CAST('2026-10-26T12:00:00' AS datetime)),
'EJC_TEST_ROLLBACK',ISNULL(p.IdProductoTallaColor,0),0
FROM trans_picking_ubic p JOIN bodega b ON b.IdBodega=p.IdBodega
CROSS JOIN(VALUES(1,N'JM000141',0),(2,N'JM000141',1),(3,N'EJC_LEGACY_ROLLBACK',0),(4,N'',0))v(n,LP,Finalizado)
WHERE p.IdPickingUbic=480 AND p.IdBodega=7;
"@
  Exec $fixture $tx
  $view=GetView $tx;$parents=$view.Tables['Licencias'];$children=$view.Tables['Contenido']
  Assert ($children.Rows.Count -eq 4) 'Se duplicaron/perdieron detalles por los joins.'
  $parent=$parents.Select("LicenciaEmpaque='JM000141'")[0]
  Assert ($parent.LineasEmpacadas -eq 2 -and $parent.LineasFinalizadas -eq 1 -and $parent.EstadoContenido -eq 'CON CONTENIDO') 'Resumen parcial incorrecto.'
  Assert ($parent.GetChildRows('ContenidoEmpacado').Count -eq 2) 'Relación de expansión incorrecta.'
  Assert ($parent.MinEntreRegistrosPacking -eq 1) 'Intervalo entre registros incorrecto.'
  Assert ($parents.Select("LicenciaEmpaque='JM000144'")[0].GetChildRows('ContenidoEmpacado').Count -eq 0) 'Licencia vacía tiene contenido ajeno.'
  Write-Output 'PASS maestro/detalle: una licencia con 2 líneas, expansión correcta, sin duplicar cantidades'
  $legacy=$parents.Select("LicenciaEmpaque='EJC_LEGACY_ROLLBACK'")[0]
  Assert ($legacy.ObservacionTrazabilidad -eq 'SIN CABECERA DE LICENCIA' -and $legacy.IsNull('FechaGeneracion')) 'Histórico sin cabecera ocultado o fechas inventadas.'
  Assert (@($parents.Select("LicenciaEmpaque LIKE '(SIN LICENCIA #%'")).Count -eq 1) 'Packing sin licencia no es trazable.'
  Write-Output 'PASS históricos sin cabecera y registro sin licencia visibles y trazables'
  Exec "UPDATE trans_packing_enc SET Finalizado=1 WHERE IdPickingEnc=88 AND IdBodega=7 AND no_linea='JM000141'" $tx
  $view=GetView $tx
  Assert ($view.Tables['Licencias'].Select("LicenciaEmpaque='JM000141'")[0].EstadoContenido -eq 'LINEAS FINALIZADAS') 'Indicador finalizado incorrecto.'
  Write-Output 'PASS estado visual finalizado a partir de todas las líneas, sin inventar fecha de cierre'
  Exec "UPDATE trans_licencia_empaque SET FechaPrimeraImpresion=DATEADD(minute,5,FechaGeneracion),FechaUltimaImpresion=DATEADD(minute,5,FechaGeneracion),UsuarioUltimaImpresion='EJC_TEST_ROLLBACK',CantidadImpresiones=2,FechaAsociacion=DATEADD(minute,10,FechaGeneracion) WHERE LicPlate='JM000141'" $tx
  $view=GetView $tx;$parent=$view.Tables['Licencias'].Select("LicenciaEmpaque='JM000141'")[0]
  Assert ($parent.MinGeneracionImpresion -eq 5 -and $parent.MinGeneracionAsociacion -eq 10) 'Métricas de hitos incorrectas.'
  Assert ($parent.UsuarioUltimaImpresion -eq 'EJC_TEST_ROLLBACK' -and $parent.CantidadImpresiones -eq 2) 'Auditoría impresión incorrecta.'
  Write-Output 'PASS generación/impresión/asociación: métricas 5/10 min y último impresor cruzables'
  Exec "UPDATE trans_licencia_empaque SET FechaPrimeraImpresion=DATEADD(minute,-5,FechaGeneracion) WHERE LicPlate='JM000141'" $tx
  $view=GetView $tx;$parent=$view.Tables['Licencias'].Select("LicenciaEmpaque='JM000141'")[0]
  Assert ($parent.IsNull('MinGeneracionImpresion') -and $parent.ObservacionTrazabilidad -eq 'REVISAR FECHAS') 'Tiempo negativo aceptado como válido.'
  Write-Output 'PASS fechas inconsistentes señaladas; no se reportan duraciones negativas'
  $other=GetView $tx 1
  Assert ($other.Tables['Licencias'].Rows.Count -eq 0 -and $other.Tables['Contenido'].Rows.Count -eq 0) 'Fuga entre bodegas.'
  Write-Output 'PASS aislamiento por picking/bodega'
  Exec "UPDATE trans_picking_enc SET Estado_Preparacion='Finalizado',Fecha_Inicio_Preparacion='20261026 12:00',Fecha_Fin_Preparacion='20261026 13:30' WHERE IdPickingEnc=88 AND IdBodega=7" $tx
  $view=GetView $tx
  Assert ($view.Tables['Proceso'].Rows.Count -eq 1 -and $view.Tables['Proceso'].Rows[0].MinPreparacionPicking -eq 90) 'Duración de picking incorrecta o duplicada por licencia.'
  Write-Output 'PASS métrica de preparación del picking: 90 min, una sola fila de proceso'
  Exec "UPDATE trans_picking_enc SET Fecha_Fin_Preparacion='20261026 11:00' WHERE IdPickingEnc=88 AND IdBodega=7" $tx
  $view=GetView $tx
  Assert ($view.Tables['Proceso'].Rows[0].IsNull('MinPreparacionPicking') -and $view.Tables['Proceso'].Rows[0].CalidadMedicion -eq 'REVISAR FECHAS') 'Duración negativa de proceso aceptada.'
  Write-Output 'PASS proceso con fechas invertidas no produce duración válida'
  Exec "UPDATE trans_picking_enc SET Fecha_Fin_Preparacion='20261026 13:30' WHERE IdPickingEnc=88 AND IdBodega=7; UPDATE trans_licencia_empaque SET FechaPrimeraImpresion=DATEADD(minute,5,FechaGeneracion) WHERE LicPlate='JM000141';" $tx
  # Exportar SOLO una representación de consulta para pruebas de UI, sin persistir operaciones.
  $view=GetView $tx
  $view.WriteXml((Join-Path $PSScriptRoot 'fixture-ui.xml'),[Data.XmlWriteMode]::WriteSchema)
 }finally{$tx.Rollback();$tx.Dispose()}
 $tx=$cn.BeginTransaction()
 try{
  Assert ((Scalar 'SELECT COUNT(*) FROM dbo.trans_packing_enc' $tx) -eq $baselineCount) 'Rollback incompleto.'
  Assert ((Scalar "SELECT COUNT(*) FROM dbo.trans_packing_enc WHERE usr_agr='EJC_TEST_ROLLBACK'" $tx) -eq 0) 'Persistió fixture.'
  Write-Output 'PASS rollback final: sin packing ni modificaciones de licencias de prueba'
 }finally{$tx.Rollback();$tx.Dispose()}
}finally{$cn.Dispose()}
