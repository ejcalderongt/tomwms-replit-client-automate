function New-WmsBrainAnswerEvent {
<#
.SYNOPSIS
    Workaround para representar la respuesta a una Q-NNN como brain_event de tipo 'directive' (schema v1).

.DESCRIPTION
    Construye un brain_event.json via New-WmsBrainEvent con:
      -Type directive
      -Tags @("answer", $AnswerId, $QuestionId, ...tags-extra)
      -FilesChanged @("wms-brain-client/answers/<A-NNN-slug>.md")
      -Message "<resumen>. Verdict=<v>, confidence=<c>. Card en answers/<file>."

    Cuando el bridge incorpore SCHEMA_VERSION="2", este cmdlet pasara a emitir
    -Type question_answer nativo con status=answered. Ver EXTENSION-V2-PROPOSAL.md.

    Es la contraparte minima de New-WmsBrainQuestionEvent. Util cuando ya
    tenes el .md de la respuesta promovido (Submit-WmsBrainAnswer ya lo hizo)
    y solo queres re-emitir el evento de notificacion (ej. notify fallo
    la primera vez).

.PARAMETER QuestionId
    Q-NNN al que responde.

.PARAMETER AnswerId
    A-NNN asignado.

.PARAMETER AnswerFile
    Nombre del .md en answers/ (ej. A-001-q-001.md).

.PARAMETER Verdict
    confirmed | partial | inconclusive | rejected | error.

.PARAMETER Confidence
    low | medium | high.

.PARAMETER Source
    Default: openclaw.

.PARAMETER Author
    Iniciales. Default: $env:WMS_BRAIN_AUTHOR_INIT o EJC.

.PARAMETER ExtraTags
    Tags adicionales (ej. codename del cliente).

.PARAMETER OutputDir
    Default: $env:TEMP\WmsBrainEvents\

.EXAMPLE
    New-WmsBrainAnswerEvent -QuestionId Q-001 -AnswerId A-001 `
        -AnswerFile 'A-001-q-001.md' -Verdict confirmed -Confidence high
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Low')]
    param(
        [Parameter(Mandatory)] [string] $QuestionId,
        [Parameter(Mandatory)] [string] $AnswerId,
        [Parameter(Mandatory)] [string] $AnswerFile,
        [Parameter(Mandatory)]
        [ValidateSet('confirmed', 'partial', 'inconclusive', 'rejected', 'error')]
        [string] $Verdict,
        [Parameter(Mandatory)]
        [ValidateSet('low', 'medium', 'high')]
        [string] $Confidence,
        [string]   $Source = 'openclaw',
        [string]   $Author,
        [string[]] $ExtraTags = @(),
        [string]   $OutputDir
    )

    if ($AnswerId -notmatch '^A-\d{3,}$') {
        throw "[2] AnswerId invalido '$AnswerId'. Formato esperado: A-NNN."
    }
    if ($QuestionId -notmatch '^Q-\d{3,}$') {
        throw "[2] QuestionId invalido '$QuestionId'. Formato esperado: Q-NNN."
    }

    $relPath = "wms-brain-client/answers/$AnswerFile"
    $message = "Respuesta a $QuestionId. Verdict=$Verdict, confidence=$Confidence. Card en $relPath."
    $tags = @('answer', $AnswerId, $QuestionId) + @($ExtraTags)
    $tags = @($tags | Where-Object { $_ } | Select-Object -Unique)

    Write-WmsBrainLog -Cmdlet 'New-WmsBrainAnswerEvent' -Level 'INFO' `
        -Message "construyendo evento directive (workaround) para $AnswerId -> $QuestionId"

    $params = @{
        Type         = 'directive'
        Source       = $Source
        Message      = $message
        Tags         = $tags
        FilesChanged = @($relPath)
    }
    if ($Author)    { $params['Author']    = $Author }
    if ($OutputDir) { $params['OutputDir'] = $OutputDir }

    if (-not $PSCmdlet.ShouldProcess($AnswerId, "Emitir brain_event answer ($Verdict/$Confidence)")) {
        return
    }

    $evt = New-WmsBrainEvent @params -Confirm:$false
    [PSCustomObject]@{
        AnswerId   = $AnswerId
        QuestionId = $QuestionId
        EventId    = $evt.Id
        Path       = $evt.Path
        Event      = $evt.Event
    }
}
