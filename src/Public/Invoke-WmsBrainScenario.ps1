function Invoke-WmsBrainScenario {
<#
.SYNOPSIS
    Corre las queries de deteccion de un scenario (READ-ONLY por contrato del cliente).

.DESCRIPTION
    Importante: en este cliente los scenarios son READ-ONLY. Si el directorio
    incluye setup.sql con writes, NO se ejecuta y se reporta WARN. Solo se
    ejecutan las queries listadas en queries/ o las que detection.sql /
    expectations.sql contengan, todas validadas via Invoke-WmsBrainQuery
    (refusa DML).

.PARAMETER Name
    Nombre del directorio bajo scenarios/.

.PARAMETER Profile
    Default: $env:WMS_BRAIN_DEFAULT_PROFILE.

.PARAMETER BrainRepo
    Default: $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN.

.PARAMETER OutputDir
    Default: $env:TEMP\WmsBrainClient\scenarios\<Name>\

.EXAMPLE
    Invoke-WmsBrainScenario -Name doble-despacho -Profile BB-PRD
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
    param(
        [Parameter(Mandatory)] [string] $Name,
        [ValidateSet('K7-PRD', 'BB-PRD', 'C9-QAS', 'LOCAL_DEV')]
        [string] $Profile,
        [string] $BrainRepo,
        [string] $OutputDir
    )
    if (-not $BrainRepo) { $BrainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN }
    if (-not $BrainRepo) {
        throw "[3] -BrainRepo no provisto y `$env:WMS_BRAIN_EXCHANGE_REPO_BRAIN no esta seteado."
    }
    $scnDir = Join-Path (Join-Path $BrainRepo 'scenarios') $Name
    if (-not (Test-Path -LiteralPath $scnDir)) {
        throw "[2] Scenario no existe: $scnDir"
    }
    if (-not $OutputDir) {
        $tempBase = [System.IO.Path]::GetTempPath()
        $OutputDir = Join-Path (Join-Path (Join-Path $tempBase 'WmsBrainClient') 'scenarios') $Name
    }
    if (-not (Test-Path -LiteralPath $OutputDir)) {
        New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null
    }

    # Si existe setup.sql -> WARN, no ejecutar
    $setup = Join-Path $scnDir 'setup.sql'
    if (Test-Path -LiteralPath $setup) {
        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainScenario' -Level 'WARN' `
            -Message "setup.sql detectado en $scnDir; este cliente es READ-ONLY, NO se ejecuta."
    }

    $candidates = @()
    foreach ($name in @('detection.sql', 'expectations.sql')) {
        $p = Join-Path $scnDir $name
        if (Test-Path -LiteralPath $p) { $candidates += (Get-Item -LiteralPath $p) }
    }
    $queriesDir = Join-Path $scnDir 'queries'
    if (Test-Path -LiteralPath $queriesDir) {
        $candidates += @(Get-ChildItem -Path $queriesDir -Filter '*.sql' -File | Sort-Object Name)
    }
    if (-not $candidates) {
        throw "[2] Scenario $Name no tiene detection.sql / expectations.sql / queries/*.sql"
    }

    $rows = New-Object System.Collections.Generic.List[object]
    foreach ($q in $candidates) {
        $qid = $q.BaseName
        $csvPath = Join-Path $OutputDir ("$qid.csv")
        if (-not $PSCmdlet.ShouldProcess("$qid en $Profile", 'Invoke-WmsBrainQuery')) { continue }
        $sw = [System.Diagnostics.Stopwatch]::StartNew()
        $err = $null; $count = 0
        try {
            $sql = Get-Content -LiteralPath $q.FullName -Raw
            $r = Invoke-WmsBrainQuery -Profile $Profile -Query $sql -AsCsv $csvPath
            $count = if ($r -is [System.Array]) { $r.Count } elseif ($null -ne $r) { 1 } else { 0 }
        } catch {
            $err = $_.Exception.Message
        }
        $sw.Stop()
        $rows.Add([PSCustomObject]@{
            Query     = $qid
            Status    = if ($err) { 'error' } else { 'ok' }
            RowCount  = $count
            ElapsedMs = [int]$sw.Elapsed.TotalMilliseconds
            CsvPath   = if ($err) { $null } else { $csvPath }
            Error     = $err
        }) | Out-Null
    }

    $summaryPath = Join-Path $OutputDir 'summary.md'
    $sb = New-Object System.Text.StringBuilder
    [void]$sb.AppendLine("# Scenario $Name (READ-ONLY)")
    [void]$sb.AppendLine("- Profile: $Profile")
    [void]$sb.AppendLine("- Queries: $($rows.Count)")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("| Query | Status | Rows | ElapsedMs |")
    [void]$sb.AppendLine("|-------|--------|------|-----------|")
    foreach ($r2 in $rows) {
        [void]$sb.AppendLine(("| {0} | {1} | {2} | {3} |" -f $r2.Query, $r2.Status, $r2.RowCount, $r2.ElapsedMs))
    }
    $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
    [System.IO.File]::WriteAllText($summaryPath, $sb.ToString(), $utf8NoBom)

    [PSCustomObject]@{
        Scenario    = $Name
        Profile     = $Profile
        OutputDir   = $OutputDir
        SummaryPath = $summaryPath
        Queries     = $rows
        SetupSkipped = (Test-Path -LiteralPath $setup)
    }
}
