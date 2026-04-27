function New-WmsBrainQuestionEvent {
<#
.SYNOPSIS
    Emite un brain_event para una question card Q-NNN.

.DESCRIPTION
    Lee la card questions/Q-NNN-*.md, extrae title, tags y targets, y emite
    el brain_event correspondiente. El tipo emitido depende de la version
    del schema soportada por el bridge:

      schema v1 (default, fallback): -Type directive con
        Tags @("question", $QuestionId, ...tags-de-la-card...)
        Modules derivado de targets[*].codename
        FilesChanged @("wms-brain-client/questions/<file>.md")
        Message "<title>. Ver wms-brain-client/questions/<file>.md. Correr Invoke-WmsBrainQuestion -Id <Id> -Profile <Codename-Env>."

      schema v2 (cuando el bridge lo soporta): -Type question_request con
        ref.question_id, ref.question_card_path, ref.rama_repo
        context.targets, context.expected_outputs
        status='pending' (terminal: 'answered' tras question_answer).

    La deteccion de version delega en Get-WmsBrainEffectiveSchemaVersion:
      - Si pasas -LegacyDirective, fuerza v1.
      - Si $env:WMS_BRAIN_FORCE_V1 esta seteado a truthy, fuerza v1.
      - Si $env:WMS_BRAIN_EXCHANGE_REPO_MAIN apunta a un repo con
        scripts/brain_bridge.mjs, lee la constante SCHEMA_VERSION del .mjs.
      - Default: v1 (no se asume v2 sin confirmacion del bridge).

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

.PARAMETER LegacyDirective
    Fuerza el comportamiento legacy (schema v1, type=directive) incluso si
    el bridge soporta v2. Util para volver al workaround sin tener que setear
    una variable de entorno global.

.OUTPUTS
    PSCustomObject con QuestionId, EventId, Path (al .json emitido),
    Event (la hashtable serializada), SchemaVersion (1 | 2) y
    EmittedType (directive | question_request).

.EXAMPLE
    New-WmsBrainQuestionEvent -QuestionId Q-003

    Caso default: lee la card Q-003 del repo wms-brain-client, y emite
    el evento con el tipo nativo segun el SCHEMA_VERSION del bridge.

.EXAMPLE
    # Forzar emision legacy aun si el bridge ya esta en v2
    New-WmsBrainQuestionEvent -QuestionId Q-003 -LegacyDirective

    Util durante una transicion de bridge o si el lado consumidor
    todavia no procesa question_request.

.EXAMPLE
    # Emitir y notificar inmediato
    $evt = New-WmsBrainQuestionEvent -QuestionId Q-003
    Invoke-WmsBrainNotify -FromEventFile $evt.Path

    Pipeline tipico: emitir el .json a $env:TEMP\WmsBrainEvents y
    encolar via brain_bridge.mjs notify.

.EXAMPLE
    # Setear el autor (sobreescribe $env:WMS_BRAIN_AUTHOR_INIT) y
    # mandar el .json a un dir custom
    New-WmsBrainQuestionEvent -QuestionId Q-007 -Author MNS `
        -OutputDir C:\tmp\eventos\Q-007

    Util cuando el evento lo dispara un compañero o lo queres archivar
    fuera del TEMP por defecto.

.EXAMPLE
    # Dry-run usando -WhatIf
    New-WmsBrainQuestionEvent -QuestionId Q-003 -WhatIf

    SupportsShouldProcess permite ver que se va a emitir sin tocar
    disco.

.LINK
    Submit-WmsBrainAnswer

.LINK
    Invoke-WmsBrainNotify

.LINK
    Show-WmsBrainQuickStart
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Low')]
    param(
        [Parameter(Mandatory)] [string] $QuestionId,
        [string] $Source = 'openclaw',
        [string] $Author,
        [string] $ClientRepo,
        [string] $OutputDir,
        [switch] $LegacyDirective
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
    $targetsList = @()
    if ($card.PSObject.Properties['targets']) {
        foreach ($t in @($card.targets)) {
            if ($t -and $t.PSObject.Properties['codename']) { $codenames += [string]$t.codename }
            if ($t) {
                $tEntry = [ordered]@{}
                if ($t.PSObject.Properties['codename'])    { $tEntry['codename']    = [string]$t.codename }
                if ($t.PSObject.Properties['environment']) { $tEntry['environment'] = [string]$t.environment }
                if ($t.PSObject.Properties['minRows'])     { $tEntry['minRows']     = $t.minRows }
                $targetsList += [PSCustomObject]$tEntry
            }
        }
    }
    $primary = if (@($card.targets).Count -gt 0) { @($card.targets)[0] } else { $null }
    $profileHint = if ($primary) { ('{0}-{1}' -f $primary.codename, $primary.environment) } else { 'K7-PRD' }
    $relPath = "wms-brain-client/questions/$($f.Name)"

    $effectiveSv = Get-WmsBrainEffectiveSchemaVersion -LegacyDirective:$LegacyDirective

    if ($effectiveSv -eq '2') {
        # Emision nativa schema v2: type=question_request.
        $message = if ($title) { "$($card.id): $title" } else { "$($card.id)" }

        $expectedOutputs = @()
        if ($card.PSObject.Properties['expectedOutputs']) {
            $expectedOutputs = @($card.expectedOutputs | ForEach-Object { [string]$_ })
        } elseif ($card.PSObject.Properties['expected_outputs']) {
            $expectedOutputs = @($card.expected_outputs | ForEach-Object { [string]$_ })
        }

        $allTags = @('question', $card.id, 'wms-brain-client') + $codenames + $cardTags
        $allTags = @($allTags | Where-Object { $_ } | Select-Object -Unique)

        $refExtra = [ordered]@{
            question_id        = [string]$card.id
            question_card_path = $relPath
            rama_repo          = 'wms-brain-client'
        }
        $contextExtra = [ordered]@{
            targets          = @($targetsList)
            expected_outputs = @($expectedOutputs)
        }

        Write-WmsBrainLog -Cmdlet 'New-WmsBrainQuestionEvent' -Level 'INFO' `
            -Message "construyendo evento question_request (schema v2) para $($card.id)"

        $params = @{
            Type          = 'question_request'
            Source        = $Source
            Message       = $message
            Modules       = $codenames
            Tags          = $allTags
            FilesChanged  = @($relPath)
            SchemaVersion = '2'
            Status        = 'pending'
            RefExtra      = $refExtra
            ContextExtra  = $contextExtra
        }
    } else {
        # Workaround legacy schema v1: type=directive con tags=["question",...].
        $message = "$($card.id): $title. Ver $relPath. Correr Invoke-WmsBrainQuestion -Id $($card.id) -Profile $profileHint."
        $allTags = @('question', $card.id, 'wms-brain-client') + $codenames + $cardTags
        $allTags = @($allTags | Where-Object { $_ } | Select-Object -Unique)

        Write-WmsBrainLog -Cmdlet 'New-WmsBrainQuestionEvent' -Level 'INFO' `
            -Message "construyendo evento directive (workaround v1) para $($card.id)"

        $params = @{
            Type         = 'directive'
            Source       = $Source
            Message      = $message
            Modules      = $codenames
            Tags         = $allTags
            FilesChanged = @($relPath)
        }
    }

    if ($Author)    { $params['Author']    = $Author }
    if ($OutputDir) { $params['OutputDir'] = $OutputDir }

    $emittedType = $params['Type']
    if (-not $PSCmdlet.ShouldProcess($card.id, "Emitir brain_event question (type=$emittedType, schema=$effectiveSv)")) {
        return
    }

    $evt = New-WmsBrainEvent @params -Confirm:$false
    [PSCustomObject]@{
        QuestionId    = $card.id
        EventId       = $evt.Id
        Path          = $evt.Path
        Event         = $evt.Event
        SchemaVersion = $effectiveSv
        EmittedType   = $emittedType
    }
}
