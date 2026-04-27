function Invoke-WmsBrainSuite {
<#
.SYNOPSIS
    Corre los .sql de una suite contra un perfil y escribe CSVs + summary.

.PARAMETER Name
    Nombre del directorio bajo suites/.

.PARAMETER Profile
    Perfil. Default: $env:WMS_BRAIN_DEFAULT_PROFILE.

.PARAMETER Step
    Solo corre ese step (ej. S3 -> S3.sql).

.PARAMETER BrainRepo
    Default: $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN.

.PARAMETER OutputDir
    Default: $env:TEMP\WmsBrainClient\suites\<Name>\

.EXAMPLE
    Invoke-WmsBrainSuite -Name outbox-health -Profile BB-PRD
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
    param(
        [Parameter(Mandatory)] [string] $Name,
        [ValidateSet('K7-PRD', 'BB-PRD', 'C9-QAS', 'LOCAL_DEV')]
        [string] $Profile,
        [string] $Step,
        [string] $BrainRepo,
        [string] $OutputDir
    )
    if (-not $BrainRepo) { $BrainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN }
    if (-not $BrainRepo) {
        throw "[3] -BrainRepo no provisto y `$env:WMS_BRAIN_EXCHANGE_REPO_BRAIN no esta seteado."
    }
    $suiteDir = Join-Path (Join-Path $BrainRepo 'suites') $Name
    if (-not (Test-Path -LiteralPath $suiteDir)) {
        throw "[2] Suite no existe: $suiteDir"
    }
    if (-not $OutputDir) {
        $tempBase = [System.IO.Path]::GetTempPath()
        $OutputDir = Join-Path (Join-Path (Join-Path $tempBase 'WmsBrainClient') 'suites') $Name
    }
    if (-not (Test-Path -LiteralPath $OutputDir)) {
        New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null
    }

    $sqls = Get-ChildItem -Path $suiteDir -Filter '*.sql' -File | Sort-Object Name
    if ($Step) {
        $sqls = $sqls | Where-Object { $_.BaseName -eq $Step }
        if (-not $sqls) { throw "[2] Step '$Step' no encontrado en $suiteDir" }
    }

    $rows = New-Object System.Collections.Generic.List[object]
    foreach ($s in $sqls) {
        $stepName = $s.BaseName
        $csvPath = Join-Path $OutputDir ("$stepName.csv")
        if (-not $PSCmdlet.ShouldProcess("$stepName en $Profile", "Invoke-WmsBrainQuery")) {
            continue
        }
        $sql = Get-Content -LiteralPath $s.FullName -Raw
        $sw = [System.Diagnostics.Stopwatch]::StartNew()
        $err = $null; $count = 0
        try {
            $r = Invoke-WmsBrainQuery -Profile $Profile -Query $sql -AsCsv $csvPath
            $count = if ($r -is [System.Array]) { $r.Count } elseif ($null -ne $r) { 1 } else { 0 }
        } catch {
            $err = $_.Exception.Message
            Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainSuite' -Level 'ERROR' `
                -Message (ConvertTo-WmsBrainSafeString $err)
        }
        $sw.Stop()
        $rows.Add([PSCustomObject]@{
            Step      = $stepName
            Status    = if ($err) { 'error' } else { 'ok' }
            RowCount  = $count
            ElapsedMs = [int]$sw.Elapsed.TotalMilliseconds
            CsvPath   = if ($err) { $null } else { $csvPath }
            Error     = $err
        }) | Out-Null
    }

    $summaryPath = Join-Path $OutputDir 'summary.md'
    $sb = New-Object System.Text.StringBuilder
    [void]$sb.AppendLine("# Suite $Name")
    [void]$sb.AppendLine("- Profile: $Profile")
    [void]$sb.AppendLine("- Steps: $($rows.Count)")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("| Step | Status | Rows | ElapsedMs |")
    [void]$sb.AppendLine("|------|--------|------|-----------|")
    foreach ($r2 in $rows) {
        [void]$sb.AppendLine(("| {0} | {1} | {2} | {3} |" -f $r2.Step, $r2.Status, $r2.RowCount, $r2.ElapsedMs))
    }
    $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
    [System.IO.File]::WriteAllText($summaryPath, $sb.ToString(), $utf8NoBom)

    Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainSuite' -Level 'OK' `
        -Message "summary en $summaryPath"

    [PSCustomObject]@{
        Suite       = $Name
        Profile     = $Profile
        OutputDir   = $OutputDir
        SummaryPath = $summaryPath
        Steps       = $rows
    }
}
