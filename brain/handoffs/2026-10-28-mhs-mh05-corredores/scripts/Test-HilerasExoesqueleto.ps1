param([Parameter(Mandatory=$true)][string]$BuildRoot,
      [Parameter(Mandatory=$true)][string]$OutputRoot)
# #EJC20261028_EXO_HILERAS: dos racks completos, orientaciones y rechazo por ancho.
$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Drawing
foreach($file in @('Entity.dll','DAL.dll','WMS_MHS.exe')) {
    [void][Reflection.Assembly]::LoadFrom((Join-Path $BuildRoot $file))
}
[TOMWMS.WmsTrace]::ENABLED=$false
$env:WMS_EXO_TRACE_DIR=$OutputRoot
foreach($horizontal in @($true,$false)) {
    foreach($separation in @(2.0,4.0)) {
        $b=New-Object TOMWMS.clsBeBodega
        $b.IdBodega=900;$b.Ancho=20;$b.Largo=20
        $s=New-Object TOMWMS.clsBeBodega_sector
        $s.IdBodega=900;$s.IdArea=1;$s.IdSector=1;$b.Sectores.Add($s)
        foreach($id in @(1,2)) {
            $t=New-Object TOMWMS.clsBeBodega_tramo
            $t.IdBodega=900;$t.IdArea=1;$t.IdSector=1;$t.IdTramo=$id
            $t.Ancho=1.22;$t.Largo=10;$t.Horizontal=$horizontal;$t.Es_Rack=$true;$t.Activo=$true
            $t.Margen_izquierdo=4;$t.Margen_superior=4
            if($horizontal){$t.Margen_superior+=($id-1)*$separation}
            else{$t.Margen_izquierdo+=($id-1)*$separation}
            $b.Tramos.Add($t)
        }
        $g=(New-Object TOMWMS.clsGeneradorExoesqueletoBodega).Generar($b,1.2,0.25)
        if($g.ErrorDiagnostico){throw $g.ErrorDiagnostico}
        if($separation -eq 2) {
            if($g.CantidadCorredoresCandidatos -ne 0 -or $g.CorredoresRechazados.Count -ne 1){throw 'Pasillo estrecho no rechazado'}
            if($g.CorredoresRechazados[0] -notmatch '0.78'){throw 'Ancho incorrecto'}
            foreach($id in @(3,4)) {
                $t=New-Object TOMWMS.clsBeBodega_tramo
                $t.IdBodega=900;$t.IdArea=1;$t.IdSector=1;$t.IdTramo=$id
                $t.Ancho=1.22;$t.Largo=10;$t.Horizontal=$horizontal;$t.Es_Rack=$true;$t.Activo=$true
                $t.Margen_izquierdo=4;$t.Margen_superior=4
                if($horizontal){$t.Margen_superior+=($id-1)*2}
                else{$t.Margen_izquierdo+=($id-1)*2}
                $b.Tramos.Add($t)
            }
            $g=(New-Object TOMWMS.clsGeneradorExoesqueletoBodega).Generar($b,1.2,0.25)
            if($g.CantidadCorredoresCandidatos -ne 0){throw 'Par no contiguo reintroduce pasillo estrecho'}
            if(-not ($g.SegmentosRechazados -match 'SIN_HOLGURA_OPERATIVA')){throw 'Falta diagnóstico de holgura'}
        } elseif($g.CantidadCorredoresCandidatos -lt 1){throw 'Hilera de un tramo no reconocida'}
        $r=Get-Content $g.UltimoDiagnostico -Raw|ConvertFrom-Json
        if($r.metricas.enlaces_con_colision -ne 0 -or $r.metricas.nodos_fuera -ne 0){throw 'Geometría inválida'}
        if(-not ($g.HallazgosCalidad -match 'sin ubicaciones marcadas')){throw 'Clasificación pendiente oculta'}
    }
}
[pscustomobject]@{cases=6;orientations=2;narrow_rejected=$true;single_rack_rows_detected=$true;collisions=0;database_calls=0}|ConvertTo-Json
