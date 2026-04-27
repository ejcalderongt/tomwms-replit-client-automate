function New-WmsBrainQuestionEvent {
<#
.SYNOPSIS
    Workaround para representar una pregunta como brain_event de tipo 'directive' (schema v1).

.DESCRIPTION
    Lee la card questions/Q-NNN-*.md, extrae title, tags y targets, y construye
    un brain_event.json via New-WmsBrainEvent con:
      -Type directive
      -Tags @("question", $QuestionId, ...tags-de-la-card...)
      -Modules derivado de targets[*].codename
      -Message "<title>. Ver wms-brain-client/questions/<file>.md. Correr Invoke-WmsBrainQuestion -Id <Id> -Profile <Codename-Env>."

    Cuando se acepte schema_version "2", este cmdlet pasara a emitir
    -Type question_request nativo. Ver EXTENSION-V2-PROPOSAL.md.

.PARAMETER QuestionId
    Q-NNN.

.PARAMETER Source
    Default: openclaw.

.PARAMETER Author
    Iniciales. Default: $env:WMS_BRAIN_AUTHOR_INIT o EJC.

.PARAMETER ClientRepo
    Default: $env:WMS_BRAIN_CLIENT_REPO.

.PARAMETER OutputDir
    Default: $env:TEMP\WmsBrainEvents\

.EXAMPLE
    New-WmsBrainQuestionEvent -QuestionId Q-003
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Low')]
    param(
        [Parameter(Mandatory)] [string] $QuestionId,
        [string] $Source = 'openclaw',
        [string] $Author,
        [string] $ClientRepo,
        [string] $OutputDir
    )
    $dir = Get-WmsBrainQuestionsDir -ClientRepo $ClientRepo
    $f = Get-ChildItem -Path $dir -Filter "$QuestionId-*.md" -File -ErrorAction SilentlyContinue | Select-Object -First 1
    if (-not $f) {
        $f = Get-ChildItem -Path $dir -Filter "$QuestionId.md" -File -ErrorAction SilentlyContinue | Select-Object -First 1
    }
    if (-not $f) { throw "[2] No encuentro card para id '$QuestionId' en $dir" }
    $card = Read-WmsBrainQuestionCard -Path $f.FullName

    $title = if ($card.PSObject.Properties['title']) { [string]$card.title } else { '' }
    $cardTags = @()
    if ($card.PSObject.Properties['tags']) { $cardTags = @($card.tags | ForEach-Object { [string]$_ }) }
    $codenames = @()
    if ($card.PSObject.Properties['targets']) {
        foreach ($t in @($card.targets)) {
            if ($t -and $t.PSObject.Properties['codename']) { $codenames += [string]$t.codename }
        }
    }
    $primary = if (@($card.targets).Count -gt 0) { @($card.targets)[0] } else { $null }
    $profileHint = if ($primary) { ('{0}-{1}' -f $primary.codename, $primary.environment) } else { 'K7-PRD' }
    $relPath = "wms-brain-client/questions/$($f.Name)"
    $message = "$($card.id): $title. Ver $relPath. Correr Invoke-WmsBrainQuestion -Id $($card.id) -Profile $profileHint."

    $allTags = @('question', $card.id, 'wms-brain-client') + $codenames + $cardTags
    $allTags = @($allTags | Select-Object -Unique)

    Write-WmsBrainLog -Cmdlet 'New-WmsBrainQuestionEvent' -Level 'INFO' `
        -Message "construyendo evento directive (workaround) para $($card.id)"

    $params = @{
        Type         = 'directive'
        Source       = $Source
        Message      = $message
        Modules      = $codenames
        Tags         = $allTags
        FilesChanged = @($relPath)
    }
    if ($Author)    { $params['Author']    = $Author }
    if ($OutputDir) { $params['OutputDir'] = $OutputDir }

    if (-not $PSCmdlet.ShouldProcess($card.id, "Emitir brain_event question (workaround directive)")) {
        return
    }

    $evt = New-WmsBrainEvent @params -Confirm:$false
    [PSCustomObject]@{
        QuestionId = $card.id
        EventId    = $evt.Id
        Path       = $evt.Path
        Event      = $evt.Event
    }
}
