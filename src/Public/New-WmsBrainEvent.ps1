function New-WmsBrainEvent {
<#
.SYNOPSIS
    Crea un brain_event.json valido (schema_version "1") en disco.

.DESCRIPTION
    Construye un PSCustomObject con la shape exacta del bridge actual y lo
    serializa a JSON UTF-8 sin BOM. El id sigue el formato YYYYMMDD-HHMM-INIT.
    Si colisiona, regenera con +1 minuto.

.PARAMETER Type
    Tipo de evento. Validos en v1: apply_succeeded, apply_failed, skill_update,
    directive, merge_completed, external_change.

.PARAMETER Source
    openclaw, replit, manual, apply_bundle.

.PARAMETER Message
    Texto del evento (context.message).

.PARAMETER Modules
    Modulos tocados (context.modules_touched).

.PARAMETER Tags
    Tags (context.tags).

.PARAMETER Bundle
    ref.bundle (ej. v23).

.PARAMETER CommitSha
    ref.commit_sha.

.PARAMETER RamaDestino
    ref.rama_destino.

.PARAMETER FilesChanged
    ref.files_changed.

.PARAMETER Marker
    ref.marker.

.PARAMETER Author
    Iniciales para el id y history.by. Default: $env:WMS_BRAIN_AUTHOR_INIT o EJC.

.PARAMETER OutputDir
    Directorio destino. Default: $env:TEMP\WmsBrainEvents\

.EXAMPLE
    New-WmsBrainEvent -Type apply_succeeded -Source apply_bundle `
        -Message "v23 aplicado limpio" -Bundle v23 -CommitSha abc1234

.NOTES
    Salida: PSCustomObject con Id, Path, Event.
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Low')]
    param(
        [Parameter(Mandatory)]
        [ValidateSet('apply_succeeded', 'apply_failed', 'skill_update', 'directive', 'merge_completed', 'external_change')]
        [string] $Type,

        [Parameter(Mandatory)]
        [ValidateSet('openclaw', 'replit', 'manual', 'apply_bundle', 'from-file')]
        [string] $Source,

        [Parameter(Mandatory)]
        [string] $Message,

        [string[]] $Modules = @(),
        [string[]] $Tags = @(),
        [string]   $Bundle,
        [string]   $CommitSha,
        [string]   $RamaDestino,
        [string[]] $FilesChanged = @(),
        [string]   $Marker,
        [string]   $Author,
        [string]   $OutputDir
    )

    if (-not $Author) {
        $Author = $env:WMS_BRAIN_AUTHOR_INIT
        if (-not $Author) { $Author = 'EJC' }
    }
    if (-not $OutputDir) {
        $tempBase = [System.IO.Path]::GetTempPath()
        $OutputDir = Join-Path $tempBase 'WmsBrainEvents'
    }
    if (-not (Test-Path -LiteralPath $OutputDir)) {
        New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null
    }

    $existing = @()
    Get-ChildItem -LiteralPath $OutputDir -Filter '*.json' -File -ErrorAction SilentlyContinue |
        ForEach-Object { $existing += [System.IO.Path]::GetFileNameWithoutExtension($_.Name) }

    $now = Get-Date
    $id = New-WmsBrainEventId -Initials $Author -ExistingIds $existing -At $now
    $createdAt = Get-WmsBrainIsoLocal -At $now

    # Shape exacta segun PROTOCOL §1.1
    $event = [ordered]@{
        id             = $id
        schema_version = '1'
        created_at     = $createdAt
        type           = $Type
        source         = $Source
        host           = [Environment]::MachineName
        ref            = [ordered]@{
            bundle        = if ($Bundle)      { $Bundle }      else { $null }
            commit_sha    = if ($CommitSha)   { $CommitSha }   else { $null }
            rama_destino  = if ($RamaDestino) { $RamaDestino } else { $null }
            files_changed = @($FilesChanged)
            marker        = if ($Marker)      { $Marker }      else { $null }
        }
        context        = [ordered]@{
            message         = $Message
            modules_touched = @($Modules)
            tags            = @($Tags)
        }
        analysis = $null
        proposal = $null
        status   = 'pending'
        decision = $null
        history  = @(
            [ordered]@{
                at     = $createdAt
                action = 'notify'
                by     = $Author
            }
        )
    }

    # Validar antes de escribir
    $eventObj = [PSCustomObject]$event
    $errors = Test-WmsBrainEventShape -Event $eventObj -SchemaVersion '1'
    if ($errors.Count -gt 0) {
        throw "[5] Evento generado no respeta schema v1: $($errors -join '; ')"
    }

    $path = Join-Path $OutputDir ("$id.json")
    if (-not $PSCmdlet.ShouldProcess($path, "Escribir brain_event.json (id=$id type=$Type)")) {
        return [PSCustomObject]@{ Id = $id; Path = $path; Event = $eventObj }
    }

    $json = $eventObj | ConvertTo-Json -Depth 10
    $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
    [System.IO.File]::WriteAllText($path, $json, $utf8NoBom)

    Write-WmsBrainLog -Cmdlet 'New-WmsBrainEvent' -Level 'OK' `
        -Message "id=$id type=$Type path=$path"

    [PSCustomObject]@{
        Id    = $id
        Path  = $path
        Event = $eventObj
    }
}
