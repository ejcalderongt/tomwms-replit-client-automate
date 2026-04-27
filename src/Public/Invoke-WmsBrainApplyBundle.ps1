function Invoke-WmsBrainApplyBundle {
<#
.SYNOPSIS
    Aplica un bundle al checkout VS (delega a apply_bundle.mjs).

.DESCRIPTION
    Mapea 1:1 los flags del .mjs:
      --latest, --bundle, --repo, --rama-destino, --dry-run, --yes,
      --bundles-root, --brain-message, --brain-modules, --brain-tags

.PARAMETER Latest
    Tomar el ultimo vNN_bundle de -BundlesRoot.

.PARAMETER Bundle
    Path explicito al directorio vNN_bundle (mutuamente excluyente con -Latest).

.PARAMETER Repo
    Path al checkout local del repo VS (TOMIMSV4).

.PARAMETER RamaDestino
    Rama esperada y target del eventual merge.

.PARAMETER DryRun
    Validaciones precondicion sin aplicar.

.PARAMETER Yes
    Confirmar automaticamente (para CI).

.PARAMETER BundlesRoot
    Default: <repo MAIN>\entregables_ajuste

.PARAMETER BrainMessage
    Si se pasa, despues del exito escribe brain_event.json (apply_succeeded).

.PARAMETER BrainModules
    CSV de modulos tocados.

.PARAMETER BrainTags
    CSV de tags.

.EXAMPLE
    Invoke-WmsBrainApplyBundle -Latest -Repo C:\src\TOMIMSV4 -BrainMessage "v23 OK"

.NOTES
    Salida: PSCustomObject con Result, Branch, CommitSha, MarkerHits,
    ApplyLogPath, BrainEventPath. Si BrainMessage fue pasado, BrainEventPath
    queda listo para Invoke-WmsBrainNotify.
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High', DefaultParameterSetName = 'Latest')]
    param(
        [Parameter(ParameterSetName = 'Latest')] [switch] $Latest,
        [Parameter(ParameterSetName = 'Explicit', Mandatory)] [string] $Bundle,

        [Parameter(Mandatory)] [string] $Repo,
        [string]   $RamaDestino,
        [switch]   $DryRun,
        [switch]   $Yes,
        [string]   $BundlesRoot,
        [string]   $BrainMessage,
        [string]   $BrainModules,
        [string]   $BrainTags
    )

    if (-not (Test-Path -LiteralPath $Repo)) {
        throw "[2] -Repo no existe: $Repo"
    }
    $mainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
    if (-not $mainRepo) { throw "[3] `$env:WMS_BRAIN_EXCHANGE_REPO_MAIN no esta seteado." }
    $applyMjs = Join-Path $mainRepo 'scripts/apply_bundle.mjs'
    if (-not (Test-Path -LiteralPath $applyMjs)) {
        throw "[2] No encuentro apply_bundle.mjs en: $applyMjs"
    }

    $argList = @()
    if ($PSCmdlet.ParameterSetName -eq 'Latest') {
        if (-not $Latest) {
            throw "[2] Falta -Latest o -Bundle."
        }
        $argList += '--latest'
    } else {
        if (-not (Test-Path -LiteralPath $Bundle)) {
            throw "[2] -Bundle no existe: $Bundle"
        }
        $argList += @('--bundle', $Bundle)
    }
    $argList += @('--repo', $Repo)
    if ($RamaDestino)  { $argList += @('--rama-destino', $RamaDestino) }
    if ($DryRun)       { $argList += '--dry-run' }
    if ($Yes)          { $argList += '--yes' }
    if ($BundlesRoot)  { $argList += @('--bundles-root', $BundlesRoot) }
    if ($BrainMessage) { $argList += @('--brain-message', $BrainMessage) }
    if ($BrainModules) { $argList += @('--brain-modules', $BrainModules) }
    if ($BrainTags)    { $argList += @('--brain-tags',    $BrainTags) }

    $what = if ($DryRun) { 'dry-run' } else { 'aplicar bundle' }
    if (-not $PSCmdlet.ShouldProcess($Repo, $what)) { return }

    Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainApplyBundle' -Level 'INFO' `
        -Message "node apply_bundle.mjs $($argList -join ' ')"

    $r = Invoke-WmsBrainNode -ScriptPath $applyMjs -Arguments $argList -PassThruRaw
    Write-WmsBrainBanner -Lines @($r.StdOut)
    if ($r.ExitCode -ne 0) {
        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainApplyBundle' -Level 'ERROR' `
            -Message "apply_bundle.mjs exit=$($r.ExitCode)"
        throw "[8] apply_bundle.mjs fallo (exit=$($r.ExitCode)). Stderr: $($r.StdErr)"
    }

    # Parser oportunista de la salida
    $branch = $null; $commit = $null; $applyLog = $null; $brainEvt = $null; $markers = @()
    foreach ($line in ($r.StdOut -split "`r?`n")) {
        if ($line -match 'Rama actual:\s+(\S+)')                    { $branch = $Matches[1] }
        elseif ($line -match 'commit:\s+([0-9a-f]{7,40})')          { $commit = $Matches[1] }
        elseif ($line -match 'apply_log\.json.*?->\s*(.+)$')        { $applyLog = $Matches[1].Trim() }
        elseif ($line -match 'brain_event\.json.*?->\s*(.+)$')      { $brainEvt = $Matches[1].Trim() }
        elseif ($line -match '#FIX_v\d+_[A-Z0-9_]+')                { $markers += $Matches[0] }
    }
    if (-not $applyLog) {
        $tmp = Get-ChildItem -Path $Repo -Filter 'apply_log.json' -Recurse -ErrorAction SilentlyContinue |
               Sort-Object LastWriteTime -Descending | Select-Object -First 1
        if ($tmp) { $applyLog = $tmp.FullName }
    }

    [PSCustomObject]@{
        Result          = if ($DryRun) { 'dry-run-OK' } else { 'OK' }
        Branch          = $branch
        CommitSha       = $commit
        MarkerHits      = $markers
        ApplyLogPath    = $applyLog
        BrainEventPath  = $brainEvt
        ExitCode        = $r.ExitCode
        RawOutput       = $r.StdOut
    }
}
