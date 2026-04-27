function New-WmsBrainLearningEvent {
<#
.SYNOPSIS
    Emite un brain_event para un learning propuesto. En schema v2 emite
    type=learning_proposed nativo; en v1 cae al workaround directive.

.DESCRIPTION
    Genera un brain_event.json para que OpenClaw / brain digiera una pieza
    nueva de conocimiento (regla, hallazgo, decisión arquitectónica) sin
    pasar por un bundle.

    El tipo emitido depende de la versión de schema soportada por el bridge:

      schema v1 (default, fallback): -Type directive con
        Tags @("learning", $LearningId, $Scope, $SourceQuestionId?, ...ExtraTags)
        FilesChanged @($LearningCardPath relativo si fue provisto)
        Modules derivado de $Scope si es codename (K7/BB/C9/ID/MH/MC)
        Message "<L-NNN>: <title>. Scope=<scope>. Origen=<sourceQ>?. Card=<path>?."

      schema v2 (cuando el bridge lo soporta): -Type learning_proposed con
        ref.learning_id, ref.learning_card_path?, ref.source_question_id?
        context.scope
        status='pending'. Ver EXTENSION-V2-PROPOSAL.md §3.3.

    La detección de versión delega en Get-WmsBrainEffectiveSchemaVersion:
      - Si pasas -LegacyDirective, fuerza v1.
      - Si $env:WMS_BRAIN_FORCE_V1 está seteado a truthy, fuerza v1.
      - Si $env:WMS_BRAIN_EXCHANGE_REPO_MAIN apunta a un repo con
        scripts/brain_bridge.mjs, lee la constante SCHEMA_VERSION del .mjs.
      - Default: v1 (no se asume v2 sin confirmación del bridge).

    Hay 3 modos de uso (independientes del schema):
      1. Learning libre: -Title obligatorio, opcional -SourceQuestionId / -LearningCardPath.
      2. Learning derivado de Q-NNN respondida: -SourceQuestionId Q-NNN -Title "...".
      3. Learning leyendo card existente (templates/learning-card.template.md):
         -LearningCardPath path/al/.md -> levanta title del front-matter.

.PARAMETER LearningId
    Identificador. Si no se pasa, se genera L-YYYYMMDD-HHMM.

.PARAMETER Title
    Titulo corto. Obligatorio si no se pasa -LearningCardPath.

.PARAMETER Scope
    Donde aplica el learning. Validos: K7, BB, C9, ID, MH, MC, ALL, OPS, BRIDGE.

.PARAMETER SourceQuestionId
    Q-NNN del que se deriva (opcional).

.PARAMETER LearningCardPath
    Path a un .md con el detalle del learning (opcional).

.PARAMETER Source
    Default: openclaw.

.PARAMETER Author
    Iniciales. Default: $env:WMS_BRAIN_AUTHOR_INIT o EJC.

.PARAMETER ExtraTags
    Tags adicionales.

.PARAMETER OutputDir
    Default: $env:TEMP\WmsBrainEvents\

.PARAMETER LegacyDirective
    Fuerza el comportamiento legacy (schema v1, type=directive con
    tags=["learning",...]) incluso si el bridge ya soporta v2. También se
    puede setear $env:WMS_BRAIN_FORCE_V1 a un valor truthy (1/true/yes/on)
    para el mismo efecto. Cuando el bridge está en v2 (y ningún override
    aplica), se emite type=learning_proposed.

.EXAMPLE
    New-WmsBrainLearningEvent -Title 'NavSync corre cada 2 min' -Scope BB -SourceQuestionId Q-001

.EXAMPLE
    # Forzar emisión legacy aun si el bridge ya está en v2
    New-WmsBrainLearningEvent -Title 'algo' -Scope OPS -LegacyDirective
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Low')]
    param(
        [string] $LearningId,
        [string] $Title,
        [Parameter(Mandatory)]
        [ValidateSet('K7', 'BB', 'C9', 'ID', 'MH', 'MC', 'ALL', 'OPS', 'BRIDGE')]
        [string] $Scope,
        [string] $SourceQuestionId,
        [string] $LearningCardPath,
        [string] $Source = 'openclaw',
        [string] $Author,
        [string[]] $ExtraTags = @(),
        [string]   $OutputDir,
        [switch]   $LegacyDirective
    )

    # Si paso card .md, leer titulo del front-matter
    $relPath = $null
    if ($LearningCardPath) {
        if (-not (Test-Path -LiteralPath $LearningCardPath)) {
            throw "[2] LearningCardPath no existe: $LearningCardPath"
        }
        try {
            $card = Read-WmsBrainQuestionCard -Path $LearningCardPath
            if (-not $Title -and $card.PSObject.Properties['title']) {
                $Title = [string]$card.title
            }
        } catch {
            Write-WmsBrainLog -Cmdlet 'New-WmsBrainLearningEvent' -Level 'WARN' `
                -Message "no pude parsear front-matter de $LearningCardPath ($($_.Exception.Message))"
        }
        $relPath = $LearningCardPath
    }

    if (-not $Title) {
        throw "[2] -Title es obligatorio si no se pasa -LearningCardPath con front-matter."
    }

    if (-not $LearningId) {
        $LearningId = 'L-' + (Get-Date -Format 'yyyyMMdd-HHmm')
    }
    if ($LearningId -notmatch '^L-[\w\-]+$') {
        throw "[2] LearningId invalido '$LearningId'. Formato esperado: L-<token>."
    }

    if ($SourceQuestionId -and $SourceQuestionId -notmatch '^Q-\d{3,}$') {
        throw "[2] SourceQuestionId invalido '$SourceQuestionId'. Formato esperado: Q-NNN."
    }

    $tags = @('learning', $LearningId, $Scope)
    if ($SourceQuestionId) { $tags += $SourceQuestionId }
    $tags += $ExtraTags
    $tags = @($tags | Where-Object { $_ } | Select-Object -Unique)

    $msgParts = @("${LearningId}: $Title", "Scope=$Scope")
    if ($SourceQuestionId) { $msgParts += "Origen=$SourceQuestionId" }
    if ($relPath)          { $msgParts += "Card=$relPath" }
    $message = ($msgParts -join '. ') + '.'

    $modules = @()
    if ($Scope -in @('K7', 'BB', 'C9', 'ID', 'MH', 'MC')) { $modules = @($Scope) }

    $effectiveSv = Get-WmsBrainEffectiveSchemaVersion -LegacyDirective:$LegacyDirective

    if ($effectiveSv -eq '2') {
        $emittedType = 'learning_proposed'

        $refExtra = [ordered]@{
            learning_id = $LearningId
        }
        if ($relPath)          { $refExtra['learning_card_path']  = $relPath }
        if ($SourceQuestionId) { $refExtra['source_question_id'] = $SourceQuestionId }

        $contextExtra = [ordered]@{
            scope = $Scope
        }

        Write-WmsBrainLog -Cmdlet 'New-WmsBrainLearningEvent' -Level 'INFO' `
            -Message "construyendo evento learning_proposed (schema v2) para $LearningId scope=$Scope"

        $params = @{
            Type          = 'learning_proposed'
            Source        = $Source
            Message       = $message
            Tags          = $tags
            Modules       = $modules
            SchemaVersion = '2'
            Status        = 'pending'
            RefExtra      = $refExtra
            ContextExtra  = $contextExtra
        }
    } else {
        $emittedType = 'directive'
        Write-WmsBrainLog -Cmdlet 'New-WmsBrainLearningEvent' -Level 'INFO' `
            -Message "construyendo evento directive (workaround v1) para learning $LearningId scope=$Scope"

        $params = @{
            Type    = 'directive'
            Source  = $Source
            Message = $message
            Tags    = $tags
            Modules = $modules
        }
    }

    if ($relPath)   { $params['FilesChanged'] = @($relPath) }
    if ($Author)    { $params['Author']       = $Author }
    if ($OutputDir) { $params['OutputDir']    = $OutputDir }

    if (-not $PSCmdlet.ShouldProcess($LearningId, "Emitir brain_event learning (type=$emittedType, schema=$effectiveSv, scope=$Scope)")) {
        return
    }

    $evt = New-WmsBrainEvent @params -Confirm:$false
    [PSCustomObject]@{
        LearningId       = $LearningId
        Scope            = $Scope
        SourceQuestionId = $SourceQuestionId
        EventId          = $evt.Id
        Path             = $evt.Path
        Event            = $evt.Event
        SchemaVersion    = $effectiveSv
        EmittedType      = $emittedType
    }
}
