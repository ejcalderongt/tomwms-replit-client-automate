function Invoke-WmsBrainNotify {
<#
.SYNOPSIS
    Encola un brain_event.json al exchange (rama wms-brain) via brain_bridge.mjs notify.

.DESCRIPTION
    Pre-flight:
    1. -ExchangeRepo debe estar en rama 'wms-brain'.
    2. Working tree del exchange debe estar limpio.
    3. El .json debe respetar schema v1 (eventos completos), o cumplir el
       contrato relajado de draft (eventos emitidos por
       `apply_bundle.mjs --brain-message`, sin id y con status='draft':
       el bridge los hidrata durante notify).

    Delega a: node scripts/brain_bridge.mjs notify --exchange-repo X --from-event-file Y

    Post: salvo -NoPush, hace git push origin wms-brain.

.PARAMETER ExchangeRepo
    Path al clon del exchange en rama wms-brain. Default:
    $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN.

.PARAMETER FromEventFile
    Path al .json del evento (output de New-WmsBrainEvent).

.PARAMETER NoPush
    No empuja a origin/wms-brain.

.EXAMPLE
    Invoke-WmsBrainNotify -FromEventFile $evt.Path

.NOTES
    Exit 4 si el repo no esta en wms-brain o working tree esta sucio.
    Exit 5 si el .json no respeta schema v1.
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High')]
    param(
        [string] $ExchangeRepo,
        [Parameter(Mandatory)] [string] $FromEventFile,
        [switch] $NoPush
    )

    if (-not $ExchangeRepo) { $ExchangeRepo = $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN }
    if (-not $ExchangeRepo) {
        throw "[3] -ExchangeRepo no provisto y `$env:WMS_BRAIN_EXCHANGE_REPO_BRAIN no esta seteado."
    }
    if (-not (Test-Path -LiteralPath $ExchangeRepo)) {
        throw "[2] ExchangeRepo no existe: $ExchangeRepo"
    }
    if (-not (Test-Path -LiteralPath $FromEventFile)) {
        throw "[2] FromEventFile no existe: $FromEventFile"
    }

    # Pre-flight
    Assert-WmsBrainGitOnBranch -RepoPath $ExchangeRepo -ExpectedBranch 'wms-brain' -ExitCode 4
    if (-not (Test-WmsBrainGitWorkingTreeClean -RepoPath $ExchangeRepo)) {
        throw "[4] Working tree de '$ExchangeRepo' tiene cambios sin commitear. Limpia antes de notify."
    }

    $raw = Get-Content -LiteralPath $FromEventFile -Raw
    $eventObj = $raw | ConvertFrom-Json
    $isDraft = Test-WmsBrainEventIsDraft -Event $eventObj
    if ($isDraft) {
        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainNotify' -Level 'INFO' `
            -Message "draft event detectado (id vacio o status=draft); validacion relajada, el bridge hidrata"
        $errs = Test-WmsBrainEventShape -Event $eventObj -SchemaVersion '1' -AllowDraft
    } else {
        $errs = Test-WmsBrainEventShape -Event $eventObj -SchemaVersion '1'
    }
    if ($errs.Count -gt 0) {
        $tag = if ($isDraft) { 'draft' } else { 'v1' }
        throw "[5] $FromEventFile no respeta schema ${tag}: $($errs -join '; ')"
    }

    # Resolver path al brain_bridge.mjs
    $mainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
    if (-not $mainRepo) { throw "[3] `$env:WMS_BRAIN_EXCHANGE_REPO_MAIN no esta seteado (necesario para brain_bridge.mjs)." }
    $bridge = Join-Path $mainRepo 'scripts/brain_bridge.mjs'
    if (-not (Test-Path -LiteralPath $bridge)) {
        throw "[2] No encuentro brain_bridge.mjs en: $bridge"
    }

    $argList = @('notify', '--exchange-repo', $ExchangeRepo, '--from-event-file', $FromEventFile)
    $idLabel = if ($eventObj.PSObject.Properties['id'] -and -not [string]::IsNullOrWhiteSpace([string]$eventObj.id)) { [string]$eventObj.id } else { '<draft>' }

    if (-not $PSCmdlet.ShouldProcess("$ExchangeRepo (id=$idLabel)", 'Encolar brain event')) { return }

    Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainNotify' -Level 'INFO' `
        -Message "notify id=$idLabel exchange=$ExchangeRepo draft=$isDraft"

    $r = Invoke-WmsBrainNode -ScriptPath $bridge -Arguments $argList -PassThruRaw
    if ($r.ExitCode -ne 0) {
        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainNotify' -Level 'ERROR' -Message $r.StdErr
        throw "[8] brain_bridge.mjs notify fallo (exit=$($r.ExitCode)). Stderr: $($r.StdErr)"
    }
    Write-WmsBrainBanner -Lines @($r.StdOut)

    if (-not $NoPush) {
        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainNotify' -Level 'INFO' `
            -Message "push origin wms-brain"
        $g = Invoke-WmsBrainGit -RepoPath $ExchangeRepo -GitArgs @('push', 'origin', 'wms-brain') -AllowFail
        if ($g.ExitCode -ne 0) {
            Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainNotify' -Level 'WARN' `
                -Message "git push fallo (exit=$($g.ExitCode)): $($g.StdErr)"
        } else {
            Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainNotify' -Level 'OK' -Message 'push OK'
        }
    }

    [PSCustomObject]@{
        Id            = $idLabel
        Draft         = [bool]$isDraft
        ExchangeRepo  = $ExchangeRepo
        Pushed        = (-not $NoPush)
        BridgeOutput  = $r.StdOut
        ExitCode      = $r.ExitCode
    }
}
