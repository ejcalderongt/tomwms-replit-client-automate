function Get-WmsBrainEvent {
<#
.SYNOPSIS
    Trae un evento individual por id (delega a brain_bridge.mjs show).

.PARAMETER ExchangeRepo
    Default: $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN.

.PARAMETER Id
    Id del evento (formato YYYYMMDD-HHMM-INIT).

.EXAMPLE
    Get-WmsBrainEvent -Id 20260427-1845-EJC
#>
    [CmdletBinding()]
    param(
        [string] $ExchangeRepo,
        [Parameter(Mandatory)] [string] $Id
    )
    if (-not $ExchangeRepo) { $ExchangeRepo = $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN }
    if (-not $ExchangeRepo) {
        throw "[3] -ExchangeRepo no provisto y `$env:WMS_BRAIN_EXCHANGE_REPO_BRAIN no esta seteado."
    }
    if ($Id -notmatch '^\d{8}-\d{4}-[A-Z0-9]+$') {
        throw "[2] -Id '$Id' no respeta formato YYYYMMDD-HHMM-INIT."
    }
    $mainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
    if (-not $mainRepo) { throw "[3] `$env:WMS_BRAIN_EXCHANGE_REPO_MAIN no esta seteado." }
    $bridge = Join-Path $mainRepo 'scripts/brain_bridge.mjs'

    $r = Invoke-WmsBrainNode -ScriptPath $bridge -Arguments @('show', '--exchange-repo', $ExchangeRepo, '--id', $Id) -PassThruRaw
    if ($r.ExitCode -ne 0) {
        throw "[8] brain_bridge show fallo (exit=$($r.ExitCode)): $($r.StdErr)"
    }

    $stdout = $r.StdOut.Trim()
    if ($stdout.StartsWith('{')) {
        try { return ($stdout | ConvertFrom-Json) }
        catch {
            Write-WmsBrainLog -Cmdlet 'Get-WmsBrainEvent' -Level 'WARN' -Message "no parsea JSON; devuelvo raw"
        }
    }
    return [PSCustomObject]@{ id = $Id; raw = $stdout }
}
