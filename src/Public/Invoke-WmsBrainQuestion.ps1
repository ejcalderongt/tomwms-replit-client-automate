function Invoke-WmsBrainQuestion {
<#
.SYNOPSIS
    Ejecuta los suggestedQueries de una question card y prepara draft de answer.

.DESCRIPTION
    Para cada query: corre Invoke-WmsBrainQuery, guarda CSV en
    $OutputDir\<Id>\<query-id>.csv y un summary.md con conteos.
    Tambien genera un draft de answer card (answer.md) con la estructura del
    template (template/answer-card.template.md), listo para que Erik lo edite y
    luego ejecute Submit-WmsBrainAnswer.

.PARAMETER Id
    Q-NNN.

.PARAMETER Profile
    Override del perfil. Default: target principal de la card.

.PARAMETER OnlyQueries
    Subset de query ids (ej. q1,q3).

.PARAMETER MaxRows
    Default: 10000.

.PARAMETER DryRun
    No corre SQL; solo lista que correria.

.PARAMETER ClientRepo
    Default: $env:WMS_BRAIN_CLIENT_REPO.

.PARAMETER OutputDir
    Default: $env:TEMP\WmsBrainClient\drafts\

.EXAMPLE
    Invoke-WmsBrainQuestion -Id Q-001 -Profile BB-PRD
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
    param(
        [Parameter(Mandatory)] [string] $Id,
        [ValidateSet('K7-PRD', 'BB-PRD', 'C9-QAS', 'LOCAL_DEV')]
        [string] $Profile,
        [string[]] $OnlyQueries,
        [int]    $MaxRows = 10000,
        [switch] $DryRun,
        [string] $ClientRepo,
        [string] $OutputDir
    )

    $dir = Get-WmsBrainQuestionsDir -ClientRepo $ClientRepo
    $f = Get-ChildItem -Path $dir -Filter "$Id-*.md" -File -ErrorAction SilentlyContinue | Select-Object -First 1
    if (-not $f) {
        $f = Get-ChildItem -Path $dir -Filter "$Id.md" -File -ErrorAction SilentlyContinue | Select-Object -First 1
    }
    if (-not $f) { throw "[2] No encuentro card para id '$Id' en $dir" }
    $card = Read-WmsBrainQuestionCard -Path $f.FullName

    if (-not $card.PSObject.Properties['suggestedQueries'] -or -not (@($card.suggestedQueries).Count -gt 0)) {
        throw "[2] La card $Id no tiene suggestedQueries."
    }

    if (-not $Profile) {
        if ($card.PSObject.Properties['targets'] -and (@($card.targets).Count -gt 0)) {
            $primary = (@($card.targets))[0]
            $codename = $primary.codename
            $envName  = $primary.environment
            $Profile = "$codename-$envName"
            if ((Get-WmsBrainProfileNames) -notcontains $Profile) {
                throw "[2] Target '$Profile' no es perfil conocido. Especifique -Profile."
            }
        } else {
            throw "[2] La card no tiene targets. Especifique -Profile."
        }
    }

    if (-not $OutputDir) {
        $tempBase = [System.IO.Path]::GetTempPath()
        $OutputDir = Join-Path (Join-Path $tempBase 'WmsBrainClient') 'drafts'
    }
    $idDir = Join-Path $OutputDir $Id
    if (-not (Test-Path -LiteralPath $idDir)) {
        New-Item -ItemType Directory -Path $idDir -Force | Out-Null
    }

    $queries = @($card.suggestedQueries)
    if ($OnlyQueries) {
        $queries = @($queries | Where-Object { $OnlyQueries -contains $_.id })
        if ($queries.Count -eq 0) {
            throw "[2] -OnlyQueries no matcheo ningun query id de la card."
        }
    }

    $summary = New-Object System.Collections.Generic.List[object]
    $started = Get-Date

    foreach ($q in $queries) {
        $qid = $q.id
        $desc = $q.description
        $sql = $q.sql
        $csvPath = Join-Path $idDir ("$qid.csv")

        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainQuestion' -Level 'INFO' `
            -Message "ejecutando $qid contra $Profile"

        if ($DryRun) {
            $summary.Add([PSCustomObject]@{
                QueryId   = $qid
                Profile   = $Profile
                RowCount  = $null
                CsvPath   = $null
                Status    = 'dry-run'
                ElapsedMs = 0
                Error     = $null
            }) | Out-Null
            continue
        }

        if (-not $PSCmdlet.ShouldProcess("$qid en $Profile", "Invoke-WmsBrainQuery")) {
            continue
        }
        $sw = [System.Diagnostics.Stopwatch]::StartNew()
        $err = $null; $rows = @()
        try {
            $rows = Invoke-WmsBrainQuery -Profile $Profile -Query $sql -MaxRows $MaxRows -AsCsv $csvPath
        } catch {
            $err = $_.Exception.Message
            Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainQuestion' -Level 'ERROR' `
                -Message (ConvertTo-WmsBrainSafeString $err)
        }
        $sw.Stop()
        $count = if ($rows -is [System.Array]) { $rows.Count } elseif ($null -ne $rows) { 1 } else { 0 }
        $summary.Add([PSCustomObject]@{
            QueryId    = $qid
            Description = $desc
            Profile    = $Profile
            RowCount   = $count
            CsvPath    = if ($err) { $null } else { $csvPath }
            Status     = if ($err) { 'error' } else { 'ok' }
            ElapsedMs  = [int]$sw.Elapsed.TotalMilliseconds
            Error      = $err
        }) | Out-Null
    }

    # Escribir summary.md
    $summaryPath = Join-Path $idDir 'summary.md'
    $sb = New-Object System.Text.StringBuilder
    [void]$sb.AppendLine("# Summary $Id")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("- Profile: $Profile")
    [void]$sb.AppendLine("- Started: $($started.ToString('s'))")
    [void]$sb.AppendLine("- Queries: $($summary.Count)")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("| QueryId | Status | Rows | ElapsedMs | CSV |")
    [void]$sb.AppendLine("|---------|--------|------|-----------|-----|")
    foreach ($s in $summary) {
        [void]$sb.AppendLine(("| {0} | {1} | {2} | {3} | {4} |" -f $s.QueryId, $s.Status, $s.RowCount, $s.ElapsedMs, $s.CsvPath))
    }
    $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
    [System.IO.File]::WriteAllText($summaryPath, $sb.ToString(), $utf8NoBom)

    # Draft answer.md
    $answerPath = Join-Path $idDir 'answer.md'
    $primaryTarget = if ($card.PSObject.Properties['targets'] -and @($card.targets).Count -gt 0) { @($card.targets)[0] } else { $null }
    $tplCodename = if ($primaryTarget) { $primaryTarget.codename } else { '' }
    $tplEnv      = if ($primaryTarget) { $primaryTarget.environment } else { '' }
    $tagsLine = if ($card.PSObject.Properties['tags']) { '[' + ((@($card.tags) | ForEach-Object { $_ }) -join ', ') + ']' } else { '[]' }
    $draftAuthor = if ($env:WMS_BRAIN_AUTHOR_INIT) { $env:WMS_BRAIN_AUTHOR_INIT.ToLower() } else { 'ejc' }
    $iso = Get-WmsBrainIsoLocal
    $duration = ((Get-Date) - $started).TotalSeconds.ToString('0')
    $answerHeader = @"
---
protocolVersion: 1
id: A-XXX
answersQuestion: $($card.id)
title: $($card.title)
operator: $draftAuthor
operatorRole: developer
target:
  codename: $tplCodename
  environment: $tplEnv
executedAt: $iso
durationSeconds: $duration
verdict: <confirmed|partial|inconclusive|rejected>
confidence: <high|medium|low>
status: answered
tags: $tagsLine
---

## Resumen

<TODO: 2-4 lineas de conclusion ejecutiva.>

## Hallazgos

"@
    [void]$sb.Clear()
    [void]$sb.Append($answerHeader)
    foreach ($s in $summary) {
        [void]$sb.AppendLine("### $($s.QueryId)")
        [void]$sb.AppendLine("")
        [void]$sb.AppendLine($s.Description)
        [void]$sb.AppendLine("")
        [void]$sb.AppendLine('```')
        if ($s.CsvPath -and (Test-Path -LiteralPath $s.CsvPath)) {
            $sample = Get-Content -LiteralPath $s.CsvPath -TotalCount 11
            foreach ($l in $sample) { [void]$sb.AppendLine($l) }
        } else {
            [void]$sb.AppendLine("(sin output, ver summary.md: status=$($s.Status))")
        }
        [void]$sb.AppendLine('```')
        [void]$sb.AppendLine("")
        [void]$sb.AppendLine("**Interpretacion**: <TODO>")
        [void]$sb.AppendLine("")
    }
    [void]$sb.AppendLine("## Conclusion")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("<TODO>")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("## Anomalias detectadas")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("- <TODO o 'Ninguna detectada en esta corrida'>")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("## Sugerencia de follow-up")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("- <TODO o 'Ninguna por ahora'>")
    [System.IO.File]::WriteAllText($answerPath, $sb.ToString(), $utf8NoBom)

    Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainQuestion' -Level 'OK' `
        -Message "draft listo en $idDir"

    [PSCustomObject]@{
        QuestionId  = $Id
        Profile     = $Profile
        OutputDir   = $idDir
        SummaryPath = $summaryPath
        AnswerDraft = $answerPath
        Queries     = $summary
    }
}
