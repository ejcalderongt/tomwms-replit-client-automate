<#
.SYNOPSIS
    Wrapper PowerShell para apply_bundle.mjs.

.DESCRIPTION
    Aplica un bundle vNN al repo local TOMWMS VS.NET respetando el contrato
    descripto en entregables_ajuste/AGENTS.md.

    Solo invoca scripts/apply_bundle.mjs (Node.js); toda la logica vive ahi.
    Este wrapper existe para conveniencia en Windows: detecta paths, simplifica
    args, y permite invocar con switches PowerShell idiomaticos.

.PARAMETER Latest
    Tomar el ultimo vNN_bundle disponible (ordenado por fecha + version).

.PARAMETER Bundle
    Path explicito al directorio vNN_bundle.

.PARAMETER RepoPath
    Path al checkout local del repo VS (TOMIMSV4). Default: cwd.

.PARAMETER RamaDestino
    Rama esperada y destino final del merge. Default: la del MANIFEST.

.PARAMETER DryRun
    Correr todas las validaciones, NO aplicar.

.PARAMETER Yes
    Confirmar automaticamente (uso CI / scripts).

.PARAMETER BundlesRoot
    Path a entregables_ajuste/. Default: <dir del script>/../entregables_ajuste

.EXAMPLE
    .\Apply-Bundle.ps1 -Latest -RepoPath C:\proyectos\TOMIMSV4

.EXAMPLE
    .\Apply-Bundle.ps1 -Bundle ..\entregables_ajuste\2026-04-25\v23_bundle -RepoPath C:\proyectos\TOMIMSV4 -DryRun

.NOTES
    Requiere Node.js >= 16 en PATH. No tiene dependencias npm externas.
#>

[CmdletBinding(DefaultParameterSetName = "Latest")]
param(
    [Parameter(ParameterSetName = "Latest")]
    [switch]$Latest,

    [Parameter(ParameterSetName = "Bundle", Mandatory = $true)]
    [string]$Bundle,

    [Parameter(Mandatory = $true)]
    [string]$RepoPath,

    [string]$RamaDestino,

    [switch]$DryRun,

    [switch]$Yes,

    [string]$BundlesRoot
)

$ErrorActionPreference = "Stop"

# Detectar Node
$node = Get-Command node -ErrorAction SilentlyContinue
if (-not $node) {
    Write-Error "Node.js no encontrado en PATH. Instalar Node >= 16."
    exit 2
}

# Resolver path al script Node
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$applyScript = Join-Path $scriptDir "apply_bundle.mjs"
if (-not (Test-Path $applyScript)) {
    Write-Error "No se encontro apply_bundle.mjs en $scriptDir"
    exit 2
}

# Resolver RepoPath
$RepoPath = (Resolve-Path -LiteralPath $RepoPath).Path

# Construir args
$nodeArgs = @($applyScript)

if ($PSCmdlet.ParameterSetName -eq "Latest" -or $Latest) {
    $nodeArgs += "--latest"
} elseif ($PSCmdlet.ParameterSetName -eq "Bundle") {
    $Bundle = (Resolve-Path -LiteralPath $Bundle).Path
    $nodeArgs += "--bundle"
    $nodeArgs += $Bundle
} else {
    Write-Error "Especificar -Latest o -Bundle <path>"
    exit 2
}

$nodeArgs += "--repo"
$nodeArgs += $RepoPath

if ($RamaDestino) {
    $nodeArgs += "--rama-destino"
    $nodeArgs += $RamaDestino
}

if ($DryRun) { $nodeArgs += "--dry-run" }
if ($Yes)    { $nodeArgs += "--yes" }

if ($BundlesRoot) {
    $BundlesRoot = (Resolve-Path -LiteralPath $BundlesRoot).Path
    $nodeArgs += "--bundles-root"
    $nodeArgs += $BundlesRoot
}

# Pasar nombre de agente para el apply_log
$env:AGENT_NAME = "Apply-Bundle.ps1"

# Ejecutar
& node $nodeArgs
exit $LASTEXITCODE
