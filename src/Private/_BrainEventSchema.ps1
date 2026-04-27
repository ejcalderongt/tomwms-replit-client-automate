# _BrainEventSchema.ps1 — constantes del schema de eventos del bridge.
#
# Mantener en sincro con scripts/brain_bridge.mjs (rama main del repo de
# exchange). El cliente lee ese .mjs en runtime via Get-WmsBrainBridgeSchemaVersion
# para detectar mismatch.

$script:WmsBrainEventSchemaVersionV1 = '1'
$script:WmsBrainEventSchemaVersionV2 = '2'

$script:WmsBrainValidTypesV1 = @(
    'apply_succeeded',
    'apply_failed',
    'skill_update',
    'directive',
    'merge_completed',
    'external_change'
)

$script:WmsBrainValidTypesV2 = $script:WmsBrainValidTypesV1 + @(
    'question_request',
    'question_answer',
    'learning_proposed'
)

$script:WmsBrainValidStatusesV1 = @('pending', 'analyzed', 'proposed', 'applied', 'skipped')
$script:WmsBrainValidStatusesV2 = $script:WmsBrainValidStatusesV1 + @('answered')

$script:WmsBrainValidSources = @('openclaw', 'replit', 'manual', 'apply_bundle', 'from-file')

function Get-WmsBrainValidEventTypes {
    [CmdletBinding()]
    param(
        [ValidateSet('1', '2')] [string] $SchemaVersion = '1'
    )
    if ($SchemaVersion -eq '2') { return $script:WmsBrainValidTypesV2 }
    return $script:WmsBrainValidTypesV1
}

function Get-WmsBrainValidEventStatuses {
    [CmdletBinding()]
    param(
        [ValidateSet('1', '2')] [string] $SchemaVersion = '1'
    )
    if ($SchemaVersion -eq '2') { return $script:WmsBrainValidStatusesV2 }
    return $script:WmsBrainValidStatusesV1
}

# Lee SCHEMA_VERSION del .mjs real para validar match.
function Get-WmsBrainBridgeSchemaVersion {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $BrainBridgeMjsPath
    )
    if (-not (Test-Path -LiteralPath $BrainBridgeMjsPath)) {
        throw "brain_bridge.mjs no existe en: $BrainBridgeMjsPath"
    }
    $content = Get-Content -LiteralPath $BrainBridgeMjsPath -Raw
    $regex = [regex]'(?m)^const\s+SCHEMA_VERSION\s*=\s*["'']([^"'']+)["''];'
    $m = $regex.Match($content)
    if (-not $m.Success) {
        throw "No pude detectar SCHEMA_VERSION en $BrainBridgeMjsPath"
    }
    return $m.Groups[1].Value
}

# Validador minimo: chequea que un objeto evento tenga campos obligatorios y
# valores en enums conocidos. Devuelve listado de errores (vacio = OK).
#
# -AllowDraft afloja el contrato para draft events tal como los emite
# `apply_bundle.mjs --brain-message`: id puede estar vacio/ausente, status
# puede ser 'draft', y created_at/history pueden no estar (el bridge los
# hidrata al hacer notify).
function Test-WmsBrainEventShape {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [object] $Event,
        [ValidateSet('1', '2')] [string] $SchemaVersion = '1',
        [switch] $AllowDraft
    )
    $errors = New-Object System.Collections.Generic.List[string]
    if ($AllowDraft) {
        # Solo type+source+ref+context son indispensables; el resto lo hidrata el bridge.
        $required = @('type', 'source', 'ref', 'context')
    } else {
        $required = @('id', 'schema_version', 'created_at', 'type', 'source', 'ref', 'context', 'status', 'history')
    }
    foreach ($k in $required) {
        if (-not $Event.PSObject.Properties[$k]) {
            $errors.Add("falta campo obligatorio: $k")
        }
    }
    if ($Event.PSObject.Properties['schema_version'] -and $Event.schema_version) {
        if ([string]$Event.schema_version -ne [string]$SchemaVersion) {
            $errors.Add("schema_version='$($Event.schema_version)' no coincide con esperado '$SchemaVersion'")
        }
    }
    if ($Event.PSObject.Properties['type']) {
        $valid = Get-WmsBrainValidEventTypes -SchemaVersion $SchemaVersion
        if ($valid -notcontains $Event.type) {
            $errors.Add("type invalido '$($Event.type)'. Validos: $($valid -join ', ')")
        }
    }
    if ($Event.PSObject.Properties['status'] -and $Event.status) {
        $validS = Get-WmsBrainValidEventStatuses -SchemaVersion $SchemaVersion
        if ($AllowDraft) { $validS = $validS + @('draft') }
        if ($validS -notcontains $Event.status) {
            $errors.Add("status invalido '$($Event.status)'. Validos: $($validS -join ', ')")
        }
    }
    if ($Event.PSObject.Properties['source']) {
        if ($script:WmsBrainValidSources -notcontains $Event.source) {
            $errors.Add("source invalido '$($Event.source)'. Validos: $($script:WmsBrainValidSources -join ', ')")
        }
    }
    if ($Event.PSObject.Properties['id'] -and $Event.id) {
        if ($Event.id -notmatch '^\d{8}-\d{4}-[A-Z0-9]{1,8}$') {
            $errors.Add("id no respeta formato YYYYMMDD-HHMM-INIT: '$($Event.id)'")
        }
    }
    return $errors
}

# Detecta si un evento ya cargado del disco es un draft de apply_bundle.mjs.
# Criterios: no tiene id (o esta vacio) o status=='draft'.
function Test-WmsBrainEventIsDraft {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [object] $Event
    )
    $hasId = $Event.PSObject.Properties['id'] -and -not [string]::IsNullOrWhiteSpace([string]$Event.id)
    $isDraftStatus = $Event.PSObject.Properties['status'] -and ([string]$Event.status -eq 'draft')
    return ((-not $hasId) -or $isDraftStatus)
}
