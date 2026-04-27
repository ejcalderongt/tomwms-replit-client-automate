function Get-WmsBrainEventQueue {
<#
.SYNOPSIS
    Lista eventos del bridge (delegado a brain_bridge.mjs list).

.DESCRIPTION
    Wrappea: node scripts/brain_bridge.mjs list --exchange-repo X --status Y
    y postprocesa la salida.

.PARAMETER ExchangeRepo
    Default: $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN.

.PARAMETER Status
    pending|analyzed|proposed|applied|skipped|all. Default: pending.

.PARAMETER Type
    Filtro client-side por type.

.PARAMETER Tag
    Filtro client-side por tag.

.EXAMPLE
    Get-WmsBrainEventQueue -Status pending | Format-Table id,type,context.message
#>
    [CmdletBinding()]
    param(
        [string] $ExchangeRepo,
        [ValidateSet('pending', 'analyzed', 'proposed', 'applied', 'skipped', 'all')]
        [string] $Status = 'pending',
        [string] $Type,
        [string] $Tag
    )

    if (-not $ExchangeRepo) { $ExchangeRepo = $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN }
    if (-not $ExchangeRepo) {
        throw "[3] -ExchangeRepo no provisto y `$env:WMS_BRAIN_EXCHANGE_REPO_BRAIN no esta seteado."
    }
    $mainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
    if (-not $mainRepo) { throw "[3] `$env:WMS_BRAIN_EXCHANGE_REPO_MAIN no esta seteado." }
    $bridge = Join-Path $mainRepo 'scripts/brain_bridge.mjs'
    if (-not (Test-Path -LiteralPath $bridge)) {
        throw "[2] No encuentro brain_bridge.mjs en: $bridge"
    }

    $argList = @('list', '--exchange-repo', $ExchangeRepo, '--status', $Status)
    Write-WmsBrainLog -Cmdlet 'Get-WmsBrainEventQueue' -Level 'INFO' `
        -Message "list status=$Status exchange=$ExchangeRepo"

    $r = Invoke-WmsBrainNode -ScriptPath $bridge -Arguments $argList -PassThruRaw
    if ($r.ExitCode -ne 0) {
        throw "[8] brain_bridge list fallo (exit=$($r.ExitCode)): $($r.StdErr)"
    }

    # Delegar parseo (JSON, key=value real del bridge, o tabular legacy)
    $events = @(ConvertFrom-WmsBrainBridgeListOutput -StdOut $r.StdOut)
    if (-not $events -or $events.Count -eq 0) {
        Write-WmsBrainLog -Cmdlet 'Get-WmsBrainEventQueue' -Level 'WARN' `
            -Message "salida del bridge no produjo eventos parseables; devuelvo lista vacia"
    }

    if ($Type) {
        $events = @($events | Where-Object {
            $_.PSObject.Properties['type'] -and $_.type -eq $Type
        })
    }
    if ($Tag) {
        $events = @($events | Where-Object {
            $tags = @()
            if ($_.PSObject.Properties['context'] -and $_.context -and $_.context.PSObject.Properties['tags']) {
                $tags = @($_.context.tags)
            } elseif ($_.PSObject.Properties['tags']) {
                $tags = @($_.tags)
            }
            $tags -contains $Tag
        })
    }

    return $events
}
