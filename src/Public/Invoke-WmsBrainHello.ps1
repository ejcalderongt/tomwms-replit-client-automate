function Invoke-WmsBrainHello {
<#
.SYNOPSIS
    Handshake operativo: corre hello_sync.mjs y resume el estado del exchange.

.DESCRIPTION
    Wrappea node scripts/hello_sync.mjs del repo de exchange (rama main) y
    devuelve un objeto resumen del estado: rama actual del clon, head, bundles
    encontrados, ultimo producido, ultimo aplicado y pendiente.

    En rol consumidor exige -WmsRepo (path al checkout de TOMIMSV4 / TOMWMS_BOF).

.PARAMETER Rol
    consumidor (Erik aplica bundles que vienen de Replit) o productor
    (Replit empaqueta bundles).

.PARAMETER ExchangeRepo
    Path al clon del repo de exchange en rama main. Default:
    $env:WMS_BRAIN_EXCHANGE_REPO_MAIN.

.PARAMETER WmsRepo
    Path al checkout de TOMIMSV4 / TOMWMS_BOF. Obligatorio si Rol=consumidor.

.PARAMETER NoPull
    No hace fetch/pull antes del handshake.

.PARAMETER Quiet
    Reduce verbosidad del .mjs.

.EXAMPLE
    Invoke-WmsBrainHello -Rol consumidor -WmsRepo C:\src\TOMIMSV4

.NOTES
    Salida: PSCustomObject con Rol, ExchangeBranch, ExchangeHead,
    BundlesEncontrados, UltimoProducido, UltimoAplicado, Pendiente.
#>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)]
        [ValidateSet('consumidor', 'productor')]
        [string] $Rol,

        [string] $ExchangeRepo,
        [string] $WmsRepo,
        [switch] $NoPull,
        [switch] $Quiet
    )

    if (-not $ExchangeRepo) { $ExchangeRepo = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN }
    if (-not $ExchangeRepo) {
        throw "[3] -ExchangeRepo no provisto y `$env:WMS_BRAIN_EXCHANGE_REPO_MAIN no esta seteado."
    }
    if (-not (Test-Path -LiteralPath $ExchangeRepo)) {
        throw "[2] ExchangeRepo no existe: $ExchangeRepo"
    }
    if ($Rol -eq 'consumidor' -and -not $WmsRepo) {
        throw "[2] -WmsRepo es obligatorio en rol consumidor."
    }

    $script = Join-Path $ExchangeRepo 'scripts/hello_sync.mjs'
    if (-not (Test-Path -LiteralPath $script)) {
        throw "[2] No encuentro hello_sync.mjs en: $script"
    }

    $argList = @('--rol', $Rol, '--exchange-repo', $ExchangeRepo)
    if ($WmsRepo) { $argList += @('--wms-repo', $WmsRepo) }
    if ($NoPull)  { $argList += '--no-pull' }
    if ($Quiet)   { $argList += '--quiet' }

    Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainHello' -Level 'INFO' `
        -Message "ejecutando handshake (rol=$Rol, exchange=$ExchangeRepo)"

    $r = Invoke-WmsBrainNode -ScriptPath $script -Arguments $argList -PassThruRaw
    if (-not $Quiet) { Write-WmsBrainBanner -Lines @($r.StdOut) }
    if ($r.ExitCode -ne 0) {
        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainHello' -Level 'ERROR' `
            -Message "hello_sync.mjs salio con exit=$($r.ExitCode)"
        throw "[8] hello_sync.mjs fallo (exit=$($r.ExitCode)). Stderr: $($r.StdErr)"
    }

    # Delegar parseo (formato `OK rama=... head=...` o legacy `Rama: ...`)
    $parsed = ConvertFrom-WmsBrainHelloSyncOutput -StdOut $r.StdOut

    [PSCustomObject]@{
        Rol                = $Rol
        ExchangeBranch     = $parsed.ExchangeBranch
        ExchangeHead       = $parsed.ExchangeHead
        BundlesEncontrados = $parsed.BundlesEncontrados
        UltimoProducido    = $parsed.UltimoProducido
        UltimoAplicado     = $parsed.UltimoAplicado
        Pendiente          = $parsed.Pendiente
        ExitCode           = $r.ExitCode
        RawOutput          = $r.StdOut
    }
}
