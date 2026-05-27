param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("recepcion", "picking", "packing", "verificacion", "reemplazo", "existencias", "inventario", "inventario_ciclico", "cambio_ubicacion")]
    [string]$Process,

    [string]$RepoRoot = "C:/Users/yejc2/source/repos/TOMWMS",
    [string]$HhRoot = "C:/Users/yejc2/StudioProjects/TOMHH2025",
    [string]$BrainRoot = "C:/Users/yejc2/source/repos/wms-brain/wms-brain"
)

$ErrorActionPreference = "Stop"

function Write-Section($title) {
    Write-Output ""
    Write-Output "== $title =="
}

$traceIndex = Join-Path $BrainRoot "brain/handoffs/2026-05-22-codex-performance-bof-hh/TRAZAS-FINAS-OPERATIVAS-INDEX-2026-05-26.yml"
$traceMap = @{
    recepcion = "brain/handoffs/2026-05-22-codex-performance-bof-hh/RECEPCION-HH-GRAFO-TRAZA-FINA-2026-05-26.yml"
    picking = "brain/handoffs/2026-05-22-codex-performance-bof-hh/PICKING-HH-GRAFO-TRAZA-FINA-2026-05-26.yml"
    packing = "brain/handoffs/2026-05-22-codex-performance-bof-hh/PACKING-HH-GRAFO-TRAZA-FINA-2026-05-26.yml"
    verificacion = "brain/handoffs/2026-05-22-codex-performance-bof-hh/VERIFICACION-HH-GRAFO-TRAZA-FINA-2026-05-26.yml"
    reemplazo = "brain/handoffs/2026-05-22-codex-performance-bof-hh/REEMPLAZO-HH-GRAFO-TRAZA-FINA-2026-05-26.yml"
    existencias = "brain/handoffs/2026-05-22-codex-performance-bof-hh/EXISTENCIAS-HH-BOF-GRAFO-TRAZA-FINA-2026-05-26.yml"
    inventario = "brain/handoffs/2026-05-22-codex-performance-bof-hh/INV-CIC-GRAFO-TRAZA-FINA-2026-05-26.yml"
    inventario_ciclico = "brain/handoffs/2026-05-22-codex-performance-bof-hh/INV-CIC-GRAFO-TRAZA-FINA-2026-05-26.yml"
}

$hotspots = @{
    picking = @(
        "TOMIMSV4/DAL/Transacciones/Picking",
        "TOMIMSV4/DAL/Transacciones/Movimiento",
        "WSHHRN/TOMHHWS.asmx.vb",
        "$HhRoot/app/src/main/java/com/dts/tom/Transacciones/Picking"
    )
    recepcion = @(
        "TOMIMSV4/DAL/Transacciones/Recepcion",
        "WSHHRN/TOMHHWS.asmx.vb",
        "$HhRoot/app/src/main/java/com/dts/tom/Transacciones/Recepcion"
    )
    existencias = @(
        "TOMIMSV4/DAL/Transacciones/Stock",
        "WSHHRN/TOMHHWS.asmx.vb",
        "$HhRoot/app/src/main/java/com/dts/tom/Transacciones/Stock"
    )
    cambio_ubicacion = @(
        "TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH",
        "WSHHRN/TOMHHWS.asmx.vb",
        "$HhRoot/app/src/main/java/com/dts/tom/Transacciones/CambioUbicacion"
    )
}

Write-Section "WMS Preflight"
Write-Output "Process: $Process"
Write-Output "RepoRoot: $RepoRoot"
Write-Output "HhRoot: $HhRoot"
Write-Output "BrainRoot: $BrainRoot"

Write-Section "Required Context"
$agents = Join-Path $RepoRoot "AGENTS.md"
Write-Output ("AGENTS.md: " + $(if (Test-Path $agents) { $agents } else { "MISSING" }))
Write-Output ("Trace index: " + $(if (Test-Path $traceIndex) { $traceIndex } else { "MISSING" }))

$traceRel = $traceMap[$Process]
if ($traceRel) {
    $tracePath = Join-Path $BrainRoot $traceRel
    Write-Output ("Process trace: " + $(if (Test-Path $tracePath) { $tracePath } else { "MISSING - create before patching" }))
} else {
    Write-Output "Process trace: not mapped - create focused trace before patching"
}

Write-Section "Git Status"
if (Test-Path $RepoRoot) {
    git -C $RepoRoot status --short
}
if (Test-Path $HhRoot) {
    Write-Output "-- HH --"
    git -C $HhRoot status --short
}
if (Test-Path $BrainRoot) {
    Write-Output "-- Brain --"
    git -C $BrainRoot status --short
}

Write-Section "Hotspots"
if ($hotspots.ContainsKey($Process)) {
    foreach ($path in $hotspots[$Process]) {
        Write-Output $path
    }
} else {
    Write-Output "No hotspot list yet for $Process. Use trace and rg to map."
}

Write-Section "Build Commands"
Write-Output 'BOF/WS: & "C:/Program Files/Microsoft Visual Studio/18/Community/MSBuild/Current/Bin/MSBuild.exe" WSHHRN/WSHHRN.vbproj /t:Build /p:Configuration=Debug /p:Platform="AnyCPU" /v:minimal'
Write-Output 'HH: cd C:/Users/yejc2/StudioProjects/TOMHH2025; ./gradlew.bat :app:compileDebugJavaWithJavac'

Write-Section "Next"
Write-Output "1. Read AGENTS.md and process trace before editing."
Write-Output "2. Map HH -> WS -> DAL -> SQL if cross-layer."
Write-Output "3. Keep HH/BOF/SQL patches separated unless Erik explicitly asks otherwise."
