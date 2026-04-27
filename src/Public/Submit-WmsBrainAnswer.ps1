function Submit-WmsBrainAnswer {
<#
.SYNOPSIS
    Promueve un draft answer card a answers/A-NNN-<slug>.md y opcionalmente notifica.

.DESCRIPTION
    Flujo:
    1. Lee draft local (answer.md).
    2. Sanitiza (codenames OK, sin password / tokens).
    3. Asigna A-NNN siguiente y escribe answers/A-NNN-<slug>.md en el repo
       wms-brain-client.
    4. Salvo -NoNotify, crea brain_event.json (-Type directive,
       -Tags @("answer", "A-NNN", "Q-NNN")) y lo encola via Invoke-WmsBrainNotify.

.PARAMETER QuestionId
    Q-NNN al que responde.

.PARAMETER Verdict
    confirmed | partial | inconclusive | rejected | error.

.PARAMETER Confidence
    low | medium | high.

.PARAMETER DraftPath
    Default: $env:TEMP\WmsBrainClient\drafts\<Id>\answer.md

.PARAMETER EditNotes
    Abre el draft con notepad/code antes de promoverlo.

.PARAMETER NoNotify
    Solo escribe el .md, no encola brain_event.

.PARAMETER ClientRepo
    Default: $env:WMS_BRAIN_CLIENT_REPO.

.PARAMETER LegacyDirective
    Fuerza el comportamiento legacy (schema v1, type=directive con
    tags=["answer",...]) incluso si el bridge ya soporta v2. Tambien se
    puede setear $env:WMS_BRAIN_FORCE_V1 a un valor truthy (1/true/yes/on)
    para el mismo efecto. Cuando el bridge esta en v2 (y ningun override
    aplica), se emite type=question_answer con status='answered'.

.OUTPUTS
    PSCustomObject con AnswerId (A-NNN asignado), QuestionId,
    AnswerPath (path al .md promovido), EventId, EventPath,
    SchemaVersion (1 | 2) y EmittedType (directive | question_answer
    si se notifico, o $null si se uso -NoNotify).

.EXAMPLE
    Submit-WmsBrainAnswer -QuestionId Q-001 -Verdict confirmed -Confidence high

    Caso default: lee el draft local (en $env:TEMP\WmsBrainClient\drafts\Q-001\
    answer.md), lo sanitiza, lo promueve a answers/A-NNN-q-001.md y
    notifica al brain.

.EXAMPLE
    # Forzar emision legacy aun si el bridge ya esta en v2
    Submit-WmsBrainAnswer -QuestionId Q-001 -Verdict confirmed `
        -Confidence high -LegacyDirective

    Util durante una transicion de bridge o si el lado consumidor
    todavia no procesa question_answer.

.EXAMPLE
    # Editar el draft antes de promover y notificar
    Submit-WmsBrainAnswer -QuestionId Q-005 -Verdict partial `
        -Confidence medium -EditNotes

    Abre el draft con notepad/code (segun $env:VISUAL/$env:EDITOR)
    para retocar antes de promoverlo. Util para tunear redaccion.

.EXAMPLE
    # Solo escribir el .md, no notificar al brain
    Submit-WmsBrainAnswer -QuestionId Q-002 -Verdict inconclusive `
        -Confidence low -NoNotify

    Escribe answers/A-NNN-q-002.md pero NO genera ni encola
    brain_event. Util si todavia no queres avisar al brain o si vas
    a notificar a mano despues con New-WmsBrainAnswerEvent.

.EXAMPLE
    # Workflow completo desde cero
    Invoke-WmsBrainQuestion -Id Q-008 -Profile BB-PRD     # corre queries, deja draft
    # ...edita el draft a mano si hace falta...
    Submit-WmsBrainAnswer -QuestionId Q-008 -Verdict confirmed -Confidence high

    Pipeline tipico end-to-end: investigar -> redactar -> promover y
    notificar.

.EXAMPLE
    # Dry-run usando -WhatIf
    Submit-WmsBrainAnswer -QuestionId Q-001 -Verdict rejected `
        -Confidence high -WhatIf

    Muestra que .md se va a escribir sin tocar disco (ConfirmImpact=High,
    asi que igual va a pedir confirmacion sin -WhatIf).

.LINK
    New-WmsBrainQuestionEvent

.LINK
    Invoke-WmsBrainQuestion

.LINK
    Show-WmsBrainQuickStart
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High')]
    param(
        [Parameter(Mandatory)] [string] $QuestionId,
        [Parameter(Mandatory)]
        [ValidateSet('confirmed', 'partial', 'inconclusive', 'rejected', 'error')]
        [string] $Verdict,
        [Parameter(Mandatory)]
        [ValidateSet('low', 'medium', 'high')]
        [string] $Confidence,
        [string] $DraftPath,
        [switch] $EditNotes,
        [switch] $NoNotify,
        [string] $ClientRepo,
        [switch] $LegacyDirective
    )

    if (-not $ClientRepo) { $ClientRepo = $env:WMS_BRAIN_CLIENT_REPO }
    if (-not $ClientRepo) {
        throw "[3] -ClientRepo no provisto y `$env:WMS_BRAIN_CLIENT_REPO no esta seteado."
    }
    $answersDir = Join-Path $ClientRepo 'answers'
    if (-not (Test-Path -LiteralPath $answersDir)) {
        New-Item -ItemType Directory -Path $answersDir -Force | Out-Null
    }

    if (-not $DraftPath) {
        $tempBase = [System.IO.Path]::GetTempPath()
        $DraftPath = Join-Path (Join-Path (Join-Path $tempBase 'WmsBrainClient') 'drafts') (Join-Path $QuestionId 'answer.md')
    }
    if (-not (Test-Path -LiteralPath $DraftPath)) {
        throw "[2] Draft no existe: $DraftPath. Corre Invoke-WmsBrainQuestion -Id $QuestionId primero."
    }

    if ($EditNotes) {
        $editor = if ($env:VISUAL) { $env:VISUAL } elseif ($env:EDITOR) { $env:EDITOR } elseif ($IsWindows -or $env:OS -eq 'Windows_NT') { 'notepad' } else { 'nano' }
        Write-WmsBrainLog -Cmdlet 'Submit-WmsBrainAnswer' -Level 'INFO' `
            -Message "abriendo $DraftPath con $editor"
        & $editor $DraftPath
    }

    $draft = Get-Content -LiteralPath $DraftPath -Raw

    # Sanitizar: refusar si aparecen secretos en el draft
    $forbiddenVars = @('WMS_KILLIOS_DB_PASSWORD', 'BRAIN_IMPORT_TOKEN', 'AZURE_DEVOPS_PAT')
    foreach ($vName in $forbiddenVars) {
        $vVal = [Environment]::GetEnvironmentVariable($vName)
        if ($vVal -and $vVal.Length -gt 4 -and $draft.Contains($vVal)) {
            throw "[7] Draft contiene literal de `$env:$vName. Sanitiza antes de submit."
        }
    }

    # Asignar A-NNN secuencial
    $existingAns = Get-ChildItem -Path $answersDir -Filter 'A-*.md' -File -ErrorAction SilentlyContinue
    $maxN = 0
    foreach ($ea in $existingAns) {
        if ($ea.BaseName -match '^A-(\d{3,})') {
            $n = [int]$Matches[1]
            if ($n -gt $maxN) { $maxN = $n }
        }
    }
    $nextN = $maxN + 1
    $aId = ('A-{0:000}' -f $nextN)

    # Slug desde el id de la pregunta
    $slug = $QuestionId.ToLower()
    $finalName = "$aId-$slug.md"
    $finalPath = Join-Path $answersDir $finalName

    # Reemplazos en el draft: A-XXX, verdict, confidence
    $final = $draft `
        -replace '(?m)^id:\s*A-XXX\s*$', "id: $aId" `
        -replace '(?m)^verdict:\s*<[^>]+>\s*$', "verdict: $Verdict" `
        -replace '(?m)^confidence:\s*<[^>]+>\s*$', "confidence: $Confidence"

    if (-not $PSCmdlet.ShouldProcess($finalPath, "Promover answer card $aId")) { return }

    $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
    [System.IO.File]::WriteAllText($finalPath, $final, $utf8NoBom)

    Write-WmsBrainLog -Cmdlet 'Submit-WmsBrainAnswer' -Level 'OK' `
        -Message "answer card escrita: $finalPath"

    $eventId = $null
    $eventPath = $null
    $emittedType = $null
    $effectiveSv = Get-WmsBrainEffectiveSchemaVersion -LegacyDirective:$LegacyDirective
    if (-not $NoNotify) {
        $relAnswerPath = "wms-brain-client/answers/$finalName"
        $msg = "Respuesta a $QuestionId. Verdict=$Verdict, confidence=$Confidence. Card en $relAnswerPath."
        $tags = @('answer', $aId, $QuestionId)

        if ($effectiveSv -eq '2') {
            $emittedType = 'question_answer'
            $refExtra = [ordered]@{
                answers_question_id = $QuestionId
                answer_card_path    = $relAnswerPath
            }
            $contextExtra = [ordered]@{
                verdict    = $Verdict
                confidence = $Confidence
            }
            Write-WmsBrainLog -Cmdlet 'Submit-WmsBrainAnswer' -Level 'INFO' `
                -Message "emitiendo question_answer (schema v2, status=answered) para $aId -> $QuestionId"
            $evt = New-WmsBrainEvent -Type question_answer -Source openclaw -Message $msg -Tags $tags `
                -FilesChanged @($relAnswerPath) `
                -SchemaVersion '2' -Status 'answered' `
                -RefExtra $refExtra -ContextExtra $contextExtra `
                -Confirm:$false
        } else {
            $emittedType = 'directive'
            Write-WmsBrainLog -Cmdlet 'Submit-WmsBrainAnswer' -Level 'INFO' `
                -Message "emitiendo directive (workaround v1) para $aId -> $QuestionId"
            $evt = New-WmsBrainEvent -Type directive -Source openclaw -Message $msg -Tags $tags `
                -FilesChanged @($relAnswerPath) `
                -Confirm:$false
        }

        $eventId = $evt.Id
        $eventPath = $evt.Path
        try {
            Invoke-WmsBrainNotify -FromEventFile $eventPath -Confirm:$false | Out-Null
        } catch {
            Write-WmsBrainLog -Cmdlet 'Submit-WmsBrainAnswer' -Level 'WARN' `
                -Message "notify fallo: $($_.Exception.Message). Evento queda en $eventPath para reintentar."
        }
    }

    [PSCustomObject]@{
        AnswerId      = $aId
        QuestionId    = $QuestionId
        AnswerPath    = $finalPath
        EventId       = $eventId
        EventPath     = $eventPath
        SchemaVersion = $effectiveSv
        EmittedType   = $emittedType
    }
}
