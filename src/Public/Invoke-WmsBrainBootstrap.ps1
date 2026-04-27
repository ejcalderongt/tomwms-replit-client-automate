function Invoke-WmsBrainBootstrap {
<#
.SYNOPSIS
    Bootstrap del entorno: corre brain-up.ps1 del repo de exchange.

.DESCRIPTION
    Wrappea el script PS existente brain-up.ps1 que instala/actualiza el modulo
    WmsBrainClient, valida las vars de entorno (WMS_KILLIOS_DB_PASSWORD entre
    otras) y configura aliases.

.PARAMETER ScriptPath
    Path explicito al brain-up.ps1. Default: $env:WMS_BRAIN_EXCHANGE_REPO_MAIN\scripts\brain-up.ps1.

.PARAMETER Force
    Si el script lo respeta, fuerza re-install.

.EXAMPLE
    Invoke-WmsBrainBootstrap

.NOTES
    Si el script no existe, devuelve exit 2 (faltante de path).
#>
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
    param(
        [string] $ScriptPath,
        [switch] $Force
    )

    if (-not $ScriptPath) {
        $base = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
        if (-not $base) {
            throw "[3] -ScriptPath no provisto y `$env:WMS_BRAIN_EXCHANGE_REPO_MAIN no esta seteado."
        }
        $ScriptPath = Join-Path $base 'scripts/brain-up.ps1'
    }
    if (-not (Test-Path -LiteralPath $ScriptPath)) {
        throw "[2] No encuentro brain-up.ps1 en: $ScriptPath"
    }

    if (-not $PSCmdlet.ShouldProcess($ScriptPath, 'Ejecutar bootstrap')) { return }

    Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainBootstrap' -Level 'INFO' `
        -Message "ejecutando $ScriptPath"

    $cmdArgs = @{}
    if ($Force) { $cmdArgs['Force'] = $true }
    & $ScriptPath @cmdArgs
    $code = $LASTEXITCODE
    if ($null -eq $code) { $code = 0 }
    [PSCustomObject]@{
        ScriptPath = $ScriptPath
        ExitCode   = $code
    }
}
