# WmsBrainClient.psm1 — loader del modulo.
# Dot-sources Private/* primero (helpers internos) y despues Public/*
# (cmdlets exportados). Exporta funciones segun .psd1 (FunctionsToExport).

# Nota: NO se aplica Set-StrictMode 'Latest' a nivel modulo. Varias rutas
# acceden a propiedades opcionales de PSCustomObject (ej. ev.context.tags)
# que con StrictMode Latest tirarian runtime errors. Cada cmdlet hace su
# propia validacion (PSObject.Properties[...]) antes de tocar campos
# opcionales. Erik puede activar Set-StrictMode -Version 2.0 a su gusto si
# quiere validacion adicional.
$ErrorActionPreference = 'Stop'

$script:WmsBrainClientRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$script:WmsBrainClientVersion = '0.2.0'
$script:WmsBrainClientExpectedSchemaVersion = '1'

# Cargar helpers privados primero
$privateDir = Join-Path $script:WmsBrainClientRoot 'Private'
if (Test-Path $privateDir) {
    Get-ChildItem -Path $privateDir -Filter '_*.ps1' -File |
        Sort-Object Name |
        ForEach-Object {
            . $_.FullName
        }
}

# Luego cargar cmdlets publicos
$publicDir = Join-Path $script:WmsBrainClientRoot 'Public'
$publicFunctions = @()
if (Test-Path $publicDir) {
    Get-ChildItem -Path $publicDir -Filter '*.ps1' -File |
        Sort-Object Name |
        ForEach-Object {
            . $_.FullName
            $publicFunctions += [System.IO.Path]::GetFileNameWithoutExtension($_.Name)
        }
}

# Alias corto para uso interactivo
if (-not (Get-Alias -Name 'wmsbc' -ErrorAction SilentlyContinue)) {
    Set-Alias -Name 'wmsbc' -Value 'Show-WmsBrainStatus' -Scope Script
}

Export-ModuleMember -Function $publicFunctions -Alias 'wmsbc'
