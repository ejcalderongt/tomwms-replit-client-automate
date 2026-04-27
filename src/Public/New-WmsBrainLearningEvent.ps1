function New-WmsBrainLearningEvent {
<#
.SYNOPSIS
    Workaround para representar un learning propuesto como brain_event de tipo 'directive' (schema v1).

.DESCRIPTION
    Genera un brain_event.json para que OpenClaw / brain digiera una pieza
    nueva de conocimiento (regla, hallazgo, decision arquitectonica) sin
    pasar por un bundle. La idea es que cuando se acepte SCHEMA_VERSION="2"
    este cmdlet emita -Type learning_proposed nativo. Hoy mapea a:

      -Type directive
      -Tags @("learning", $LearningId, $Scope, ...ExtraTags)
      -FilesChanged @($LearningCardPath relativo si fue provisto)
      -Message "<title>. Scope=<scope>. Source=<sourceQ>. Ver <relPath>."

    Hay 3 modos:
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

.EXAMPLE
    New-WmsBrainLearningEvent -Title 'NavSync corre cada 2 min' -Scope BB -SourceQuestionId Q-001
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
        [string]   $OutputDir
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

    $msgParts = @("$LearningId: $Title", "Scope=$Scope")
    if ($SourceQuestionId) { $msgParts += "Origen=$SourceQuestionId" }
    if ($relPath)          { $msgParts += "Card=$relPath" }
    $message = ($msgParts -join '. ') + '.'

    $modules = @()
    if ($Scope -in @('K7', 'BB', 'C9', 'ID', 'MH', 'MC')) { $modules = @($Scope) }

    Write-WmsBrainLog -Cmdlet 'New-WmsBrainLearningEvent' -Level 'INFO' `
        -Message "construyendo evento directive (workaround) para learning $LearningId scope=$Scope"

    $params = @{
        Type    = 'directive'
        Source  = $Source
        Message = $message
        Tags    = $tags
        Modules = $modules
    }
    if ($relPath)   { $params['FilesChanged'] = @($relPath) }
    if ($Author)    { $params['Author']       = $Author }
    if ($OutputDir) { $params['OutputDir']    = $OutputDir }

    if (-not $PSCmdlet.ShouldProcess($LearningId, "Emitir brain_event learning (scope=$Scope)")) {
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
    }
}
