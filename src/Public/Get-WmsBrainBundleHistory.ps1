function Get-WmsBrainBundleHistory {
<#
.SYNOPSIS
    Recorre apply_log.json de los bundles aplicados y devuelve tabla.

.DESCRIPTION
    Lee entregables_ajuste/*/v*_bundle/apply_log.json y devuelve los ultimos N
    aplicados con status filtrable.

.PARAMETER Repo
    Default: $env:WMS_BRAIN_EXCHANGE_REPO_MAIN.

.PARAMETER Last
    Cantidad maxima a devolver. Default: 10.

.PARAMETER Status
    OK | FAIL | PENDING.

.EXAMPLE
    Get-WmsBrainBundleHistory -Last 5
#>
    [CmdletBinding()]
    param(
        [string] $Repo,
        [int]    $Last = 10,
        [ValidateSet('OK', 'FAIL', 'PENDING')]
        [string] $Status
    )
    if (-not $Repo) { $Repo = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN }
    if (-not $Repo) {
        throw "[3] -Repo no provisto y `$env:WMS_BRAIN_EXCHANGE_REPO_MAIN no esta seteado."
    }
    $base = Join-Path $Repo 'entregables_ajuste'
    if (-not (Test-Path -LiteralPath $base)) {
        Write-WmsBrainLog -Cmdlet 'Get-WmsBrainBundleHistory' -Level 'WARN' `
            -Message "no existe $base"
        return @()
    }
    $logs = Get-ChildItem -Path $base -Filter 'apply_log.json' -Recurse -File -ErrorAction SilentlyContinue
    $rows = New-Object System.Collections.Generic.List[object]
    foreach ($f in $logs) {
        try {
            $data = Get-Content -LiteralPath $f.FullName -Raw | ConvertFrom-Json
        } catch {
            Write-WmsBrainLog -Cmdlet 'Get-WmsBrainBundleHistory' -Level 'WARN' `
                -Message "no parsea $($f.FullName): $($_.Exception.Message)"
            continue
        }
        $bundleDir = Split-Path -Parent $f.FullName
        $bundleName = Split-Path -Leaf $bundleDir
        $row = [PSCustomObject]@{
            Bundle      = $bundleName
            Status      = if ($data.PSObject.Properties['status'])     { $data.status }     else { 'UNKNOWN' }
            AppliedAt   = if ($data.PSObject.Properties['applied_at']) { $data.applied_at } else { $null }
            CommitSha   = if ($data.PSObject.Properties['commit_sha']) { $data.commit_sha } else { $null }
            Rama        = if ($data.PSObject.Properties['rama_destino']) { $data.rama_destino } else { $null }
            ApplyLogPath = $f.FullName
        }
        $rows.Add($row) | Out-Null
    }
    $sorted = $rows | Sort-Object -Property AppliedAt -Descending
    if ($Status) { $sorted = $sorted | Where-Object { $_.Status -eq $Status } }
    return @($sorted | Select-Object -First $Last)
}
