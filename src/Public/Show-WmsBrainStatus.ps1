function Show-WmsBrainStatus {
<#
.SYNOPSIS
    Imprime un banner de estado del cliente: version, vars, pendientes.

.DESCRIPTION
    Resumen rapido de:
    - Version del modulo
    - SCHEMA_VERSION esperado por el cliente vs detectado en brain_bridge.mjs
    - Repos configurados (paths y branches)
    - Cantidad de eventos pendientes (segun Get-WmsBrainEventQueue, si reachable)
    - Cantidad de questions pendientes (segun Get-WmsBrainQuestion)

.EXAMPLE
    Show-WmsBrainStatus

.NOTES
    No falla si algun chequeo no es resoluble; solo deja '-' o 'n/a'.
#>
    [CmdletBinding()] param()

    $bridgeSv = '?'
    $mainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
    if ($mainRepo -and (Test-Path -LiteralPath $mainRepo)) {
        $bridgeMjs = Join-Path $mainRepo 'scripts/brain_bridge.mjs'
        if (Test-Path -LiteralPath $bridgeMjs) {
            try { $bridgeSv = Get-WmsBrainBridgeSchemaVersion -BrainBridgeMjsPath $bridgeMjs }
            catch { $bridgeSv = "ERROR: $($_.Exception.Message)" }
        }
    }

    $repos = @(
        @{ Var = 'WMS_BRAIN_EXCHANGE_REPO_MAIN';  Branch = 'main' },
        @{ Var = 'WMS_BRAIN_EXCHANGE_REPO_BRAIN'; Branch = 'wms-brain' },
        @{ Var = 'WMS_BRAIN_CLIENT_REPO';         Branch = 'wms-brain-client' }
    )
    $repoLines = @()
    foreach ($r in $repos) {
        $p = [Environment]::GetEnvironmentVariable($r.Var)
        if (-not $p) {
            $repoLines += ("  {0,-32} (no seteado)" -f $r.Var)
        } else {
            $b = '-'
            if (Test-Path -LiteralPath $p) {
                try { $b = Get-WmsBrainGitBranch -RepoPath $p } catch { $b = '?' }
            }
            $repoLines += ("  {0,-32} {1}  branch={2}" -f $r.Var, $p, $b)
        }
    }

    $pendQ = '-'
    try { $pendQ = (@(Get-WmsBrainEventQueue -Status pending -ErrorAction Stop)).Count }
    catch { }

    $pendCard = '-'
    try { $pendCard = (@(Get-WmsBrainQuestion -Status pending -ErrorAction Stop)).Count }
    catch { }

    $svMatch = if ($bridgeSv -eq $script:WmsBrainClientExpectedSchemaVersion) { 'OK' } else { 'MISMATCH' }

    Write-WmsBrainBanner -Lines @(
        '=========================================================',
        ' WmsBrainClient — Status',
        '=========================================================',
        (" Version del modulo : {0}" -f $script:WmsBrainClientVersion),
        (" Schema esperado    : {0}" -f $script:WmsBrainClientExpectedSchemaVersion),
        (" Schema detectado   : {0}  [{1}]" -f $bridgeSv, $svMatch),
        '',
        ' Repos:'
    )
    Write-WmsBrainBanner -Lines $repoLines
    Write-WmsBrainBanner -Lines @(
        '',
        (" Eventos pendientes : {0}" -f $pendQ),
        (" Questions pendientes: {0}" -f $pendCard),
        '========================================================='
    )
    [PSCustomObject]@{
        ModuleVersion          = $script:WmsBrainClientVersion
        ExpectedSchemaVersion  = $script:WmsBrainClientExpectedSchemaVersion
        DetectedSchemaVersion  = $bridgeSv
        SchemaMatch            = ($svMatch -eq 'OK')
        EventsPending          = $pendQ
        QuestionsPending       = $pendCard
    }
}
