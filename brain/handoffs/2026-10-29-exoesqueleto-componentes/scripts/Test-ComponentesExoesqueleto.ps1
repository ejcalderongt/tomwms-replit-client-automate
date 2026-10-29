param([Parameter(Mandatory=$true)][string]$BuildRoot,
      [Parameter(Mandatory=$true)][string]$OutputRoot,
      [string]$Diagnostico)
# #EJC20261029_EXO_COMPONENTES: topología y render sin SQL ni pantalla operativa.
$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
foreach($file in @('Entity.dll','DAL.dll','WMS_DEV.exe')) {
    [void][Reflection.Assembly]::LoadFrom((Join-Path $BuildRoot $file))
}
$g=New-Object TOMWMS.clsExoesqueletoBodega
foreach($id in 1..6){$n=New-Object TOMWMS.clsNodoExoesqueletoBodega;$n.Id=$id;$n.X=$id*2;$n.Y=2;$g.Nodos.Add($n)}
function Edge($a,$b,$state){
    $e=New-Object TOMWMS.clsEnlaceExoesqueletoBodega
    $e.Origen=$g.Nodos[$a-1];$e.Destino=$g.Nodos[$b-1];$e.Estado=$state;$g.Enlaces.Add($e)
}
Edge 1 2 'CALCULADO';Edge 3 4 'CALCULADO';Edge 2 3 'BLOQUEADO';Edge 4 5 'CANDIDATO'
$m=[TOMWMS.clsMapaComponentesExoesqueleto]::Calcular($g)
if($m.Componentes.Count -ne 2 -or -not $m.SoloCandidatos.Contains(5) -or -not $m.Aislados.Contains(6)){throw 'Clasificación incorrecta'}
$g.Enlaces[2].Estado='CALCULADO'
$m=[TOMWMS.clsMapaComponentesExoesqueleto]::Calcular($g)
if($m.Componentes.Count -ne 1){throw 'Edición no actualiza componentes'}
if($Diagnostico){
    $d=Get-Content -LiteralPath $Diagnostico -Raw -Encoding UTF8|ConvertFrom-Json
    $g=New-Object TOMWMS.clsExoesqueletoBodega
    $nodes=@{}
    foreach($row in $d.nodos){$n=New-Object TOMWMS.clsNodoExoesqueletoBodega;$n.Id=$row.id;$n.X=$row.x;$n.Y=$row.y;$n.Estado=$row.estado;$g.Nodos.Add($n);$nodes[$row.id]=$n}
    foreach($row in $d.enlaces){$e=New-Object TOMWMS.clsEnlaceExoesqueletoBodega;$e.Origen=$nodes[$row.origen];$e.Destino=$nodes[$row.destino];$e.Estado=$row.estado;$e.Tipo=$row.tipo;$e.Grupo=$row.grupo;$g.Enlaces.Add($e)}
    $m=[TOMWMS.clsMapaComponentesExoesqueleto]::Calcular($g)
    if($m.Componentes.Count -ne 30 -or $m.Aislados.Count -ne 0 -or $m.SoloCandidatos.Count -ne 4){throw 'BOD1: clasificación de componentes incorrecta'}
    if(($m.Componentes|Measure-Object SegmentosAcceso -Sum).Sum -ne 21){throw 'BOD1: accesos inconsistentes'}
}
[void][IO.Directory]::CreateDirectory($OutputRoot)
$env:WMS_EXO_TRACE_DIR=$OutputRoot
[TOMWMS.clsDiagnosticoExoesqueleto]::Registrar($g,$null,'PRUEBA_COMPONENTES')
if($g.ErrorDiagnostico){throw $g.ErrorDiagnostico}
$report=Get-Content -LiteralPath $g.UltimoDiagnostico -Raw -Encoding UTF8|ConvertFrom-Json
if($report.componentes_transitables.Count -ne $m.Componentes.Count){throw 'JSON inconsistente'}
foreach($c in $report.componentes_transitables){if($null -ne $c.ubicaciones_atendidas){throw 'Cobertura inventada'}}

$control=New-Object TOMWMS.ctlExoesqueletoBodega
$bitmap=New-Object Drawing.Bitmap(1060,900)
try{
    $control.Size=New-Object Drawing.Size(1060,900);$control.Grafo=$g;$control.ZoomBodega=25;$control.MostrarComponentes=$true
    $control.CreateControl();$control.DrawToBitmap($bitmap,(New-Object Drawing.Rectangle(0,0,1060,900)))
    $bitmap.Save((Join-Path $OutputRoot 'componentes.png'))
}finally{$bitmap.Dispose();$control.Dispose()}
[pscustomobject]@{synthetic='passed';components=$m.Componentes.Count;isolated=$m.Aislados.Count;candidate_only=$m.SoloCandidatos.Count;render='passed';database_calls=0}|ConvertTo-Json
