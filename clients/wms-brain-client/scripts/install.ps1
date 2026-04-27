<#
.SYNOPSIS
    Instala el modulo WmsBrainClient en $HOME\Documents\PowerShell\Modules\WmsBrainClient

.DESCRIPTION
    Copia src\* a la ubicacion estandar de modulos PS para CurrentUser. Si el
    modulo ya esta cargado, lo descarga y reimporta. Tambien instala el modulo
    'powershell-yaml' (necesario para parsear question cards).

.PARAMETER Force
    Sobrescribe sin preguntar.

.PARAMETER InstallDependencies
    Instala powershell-yaml y SqlServer si no estan.

.EXAMPLE
    .\install.ps1 -Force -InstallDependencies
#>
[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
param(
    [switch] $Force,
    [switch] $InstallDependencies
)

$ErrorActionPreference = 'Stop'

$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$srcDir = Resolve-Path (Join-Path $here '..\src')
$psd1   = Join-Path $srcDir 'WmsBrainClient.psd1'
if (-not (Test-Path -LiteralPath $psd1)) {
    throw "No encuentro el manifest en: $psd1"
}

Write-Host "[install] Validando manifest..."
$manifest = Test-ModuleManifest -Path $psd1
$version  = $manifest.Version
Write-Host "[install] Manifest OK. Version=$version"

# Destino estandar para CurrentUser.
# Ojo: NO usar $pshome / $PSHOME aca: es una variable automatica de
# PowerShell (read-only/constant) y asignarla tira
# "No se puede sobrescribir la variable PSHOME porque es de solo
# lectura o constante." en PS 7+.
$modulesRoot = if ($env:OneDriveCommercial) {
    Join-Path $env:OneDriveCommercial 'Documents\PowerShell\Modules'
} elseif ($env:OneDrive) {
    Join-Path $env:OneDrive 'Documents\PowerShell\Modules'
} else {
    Join-Path $HOME 'Documents\PowerShell\Modules'
}
$destBase = Join-Path $modulesRoot 'WmsBrainClient'
$destVer  = Join-Path $destBase $version.ToString()

if (Test-Path -LiteralPath $destVer) {
    if ($Force -or $PSCmdlet.ShouldProcess($destVer, 'Borrar version existente')) {
        Remove-Item -LiteralPath $destVer -Recurse -Force
    } else {
        throw "Version $version ya instalada en $destVer. Usa -Force para reinstalar."
    }
}

Write-Host "[install] Copiando a $destVer ..."
New-Item -ItemType Directory -Path $destVer -Force | Out-Null
Copy-Item -Path (Join-Path $srcDir '*') -Destination $destVer -Recurse -Force

# Descargar si esta cargado
if (Get-Module -Name WmsBrainClient -ErrorAction SilentlyContinue) {
    Write-Host "[install] Descargando modulo previo..."
    Remove-Module WmsBrainClient -Force
}

# Instalar dependencias opcionales
#
# Versiones pinneadas a proposito (ver README seccion "Tests" / "Instalacion"):
# misma idea que el pin de Pester en CI -- queremos que dos personas que corran
# el instalador en dias distintos terminen con el MISMO build. Para bumpear:
#   1. Subir la version aca (PSGallery -> Find-Module <nombre>).
#   2. Actualizar la tabla en README.md ("Versiones pinneadas").
#   3. Correr install.ps1 -InstallDependencies en una maquina limpia y
#      .\scripts\Run-SmokeTest.ps1 para confirmar que sigue verde.
$PinnedDependencyVersions = [ordered]@{
    'powershell-yaml' = '0.4.7'
    'SqlServer'       = '22.3.0'
}

if ($InstallDependencies) {
    foreach ($dep in $PinnedDependencyVersions.Keys) {
        $required = $PinnedDependencyVersions[$dep]
        $installed = Get-Module -ListAvailable -Name $dep |
            Where-Object { $_.Version.ToString() -eq $required }
        if (-not $installed) {
            Write-Host "[install] Install-Module $dep -RequiredVersion $required -Scope CurrentUser ..."
            try {
                Install-Module -Name $dep -RequiredVersion $required -Scope CurrentUser -Force -AllowClobber -ErrorAction Stop
            } catch {
                Write-Warning "No pude instalar $dep $required automaticamente: $($_.Exception.Message). Instalalo manualmente con: Install-Module -Name $dep -RequiredVersion $required -Scope CurrentUser"
            }
        } else {
            Write-Host "[install] $dep $required ya disponible."
        }
    }
}

Write-Host "[install] Reimportando..."
Import-Module WmsBrainClient -Force
$count = (Get-Command -Module WmsBrainClient -CommandType Function).Count
Write-Host "[install] OK. $count cmdlets disponibles. Probar: Show-WmsBrainStatus"
