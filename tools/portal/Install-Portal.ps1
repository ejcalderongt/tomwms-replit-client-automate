# #EJC20261105_PORTAL_PORTABLE: instalacion reproducible sin rutas de la PC de Erik.
[CmdletBinding()]
param([string]$InstallRoot=(Join-Path $env:LOCALAPPDATA 'TOMWMSPortal'),[switch]$SkipConfigure,[switch]$SkipPrerequisites)
$ErrorActionPreference='Stop'
function Run($command,[string[]]$Arguments){ & $command @Arguments | Out-Host; if($LASTEXITCODE -ne 0){throw "Fallo la herramienta $command (codigo $LASTEXITCODE)."} }
function Refresh-Path {$env:PATH=[Environment]::GetEnvironmentVariable('Path','Machine')+';'+[Environment]::GetEnvironmentVariable('Path','User')}
function Install-Requirement($command,$package){
 if(Get-Command $command -ErrorAction SilentlyContinue){return}
 if($SkipPrerequisites){throw "Falta $command. Ejecutar sin -SkipPrerequisites para instalar los requisitos."}
 if(-not (Get-Command winget.exe -ErrorAction SilentlyContinue)){throw 'Falta winget. Instalar App Installer de Microsoft y reintentar.'}
 Run 'winget.exe' @('install','--id',$package,'--exact','--silent','--accept-package-agreements','--accept-source-agreements','--disable-interactivity')
 Refresh-Path
 if(-not (Get-Command $command -ErrorAction SilentlyContinue)){throw "Reabrir PowerShell tras instalar $package y reintentar."}
}
Install-Requirement 'git.exe' 'Git.Git'
Install-Requirement 'npm.cmd' 'OpenJS.NodeJS.LTS'
Install-Requirement 'dotnet.exe' 'Microsoft.DotNet.SDK.8'
if(-not ((& dotnet.exe --list-sdks) -match '^([89]|[1-9][0-9])\.')){
 if($SkipPrerequisites){throw 'Se requiere .NET SDK 8 o superior.'}
 Run 'winget.exe' @('install','--id','Microsoft.DotNet.SDK.8','--exact','--silent','--accept-package-agreements','--accept-source-agreements','--disable-interactivity')
 Refresh-Path
 if(-not ((& dotnet.exe --list-sdks) -match '^([89]|[1-9][0-9])\.')){throw 'Reabrir PowerShell para usar el SDK instalado.'}
}
$node=(Get-Command node.exe -ErrorAction Stop).Source
$major=[int]((& $node -p 'process.versions.node.split(".")[0]') | Select-Object -Last 1)
if($major -lt 22){throw 'Se requiere Node 22 o superior. Actualizar Node LTS y reintentar.'}
$InstallRoot=[IO.Path]::GetFullPath($InstallRoot)
New-Item -ItemType Directory -Force $InstallRoot | Out-Null
$private=Join-Path $InstallRoot 'private'
if(Test-Path (Join-Path $private 'processes.json')){throw 'La instalacion ya tiene registro de procesos. Revisar si estan activos antes de reconstruir; usar Start-Portal para el arranque habitual.'}
$manifest=Get-Content (Join-Path $PSScriptRoot 'release.json') -Raw | ConvertFrom-Json
function Checkout($name,$release,$patterns){
 $target=Join-Path $InstallRoot "source/$name"
 if(Test-Path $target){
  if(-not (Test-Path (Join-Path $target '.git'))){throw "Carpeta no administrada: $target"}
  $actual=(& git.exe -C $target rev-parse HEAD).Trim()
  $origin=(& git.exe -C $target remote get-url origin).Trim()
  & git.exe -C $target diff --quiet HEAD
  if($LASTEXITCODE -ne 0 -or $actual -ne $release.commit -or $origin -ne $release.url){throw "Fuente distinta o modificada: $target. No se sobrescribio."}
  return $target
 }
 Run 'git.exe' @('clone','--filter=blob:none','--no-checkout',$release.url,$target)
 Push-Location $target
 try {
  Run 'git.exe' (@('sparse-checkout','set','--no-cone')+$patterns)
  Run 'git.exe' @('fetch','origin',$release.branch)
  Run 'git.exe' @('checkout','--detach',$release.commit)
  $head=(& git.exe rev-parse HEAD).Trim();if($head -ne $release.commit){throw 'Revision de fuente incorrecta.'}
 } finally {Pop-Location}
 return $target
}
# No imprimir stdout de Git como valor de retorno de Checkout.
$reactPatterns=@('/src/','/public/','/package.json','/package-lock.json','/index.html','/tsconfig*.json','/postcss.config.*','/tailwind.config.*','/vite.caremed.config.ts')
$reactOutput=Checkout 'react' $manifest.react $reactPatterns
$react=Join-Path $InstallRoot 'source/react'
$apiOutput=Checkout 'api' $manifest.api @('/WMSWebAPI/','/WMS.AppGlobalCore/','/WMS.DALCore/','/WMS.EntityCore/','/NuGet.Config','/nuget.config','/Directory.Build.*','/Directory.Packages.*','/global.json')
$api=Join-Path $InstallRoot 'source/api'
$app=Join-Path $InstallRoot 'app'
New-Item -ItemType Directory -Force (Join-Path $app 'api'),(Join-Path $app 'react'),(Join-Path $InstallRoot 'tools'),$private | Out-Null
Push-Location $react
try {Run 'npm.cmd' @('ci','--no-audit','--no-fund');Run 'npm.cmd' @('exec','--','vite','build','--config','vite.caremed.config.ts')} finally {Pop-Location}
Run 'dotnet.exe' @('publish',(Join-Path $api 'WMSWebAPI/WMSWebAPI.csproj'),'-c','Release','-r','win-x64','--self-contained','true','-o',(Join-Path $app 'api'))
# No desplegar configuraciones, conexiones ni secretos historicos del repositorio fuente.
Get-ChildItem (Join-Path $app 'api') -Filter 'appsettings*.json' | ForEach-Object {[IO.File]::WriteAllText($_.FullName,'{}')}
Copy-Item (Join-Path $react 'dist-caremed/*') (Join-Path $app 'react') -Recurse -Force
Copy-Item (Join-Path $PSScriptRoot 'portal-server.cjs') $app -Force
Copy-Item (Join-Path $PSScriptRoot '*.ps1') (Join-Path $InstallRoot 'tools') -Force
Copy-Item (Join-Path $PSScriptRoot 'release.json'),(Join-Path $PSScriptRoot 'portal-server.cjs') (Join-Path $InstallRoot 'tools') -Force
@{NodePath=$node;ReactCommit=$manifest.react.commit;ApiCommit=$manifest.api.commit} | ConvertTo-Json | Set-Content (Join-Path $private 'runtime.json') -Encoding UTF8
$shortcut=(New-Object -ComObject WScript.Shell).CreateShortcut((Join-Path ([Environment]::GetFolderPath('Desktop')) 'Iniciar Portal TOMWMS.lnk'))
$shortcut.TargetPath=Join-Path $env:SystemRoot 'System32/WindowsPowerShell/v1.0/powershell.exe'
$shortcut.Arguments='-NoProfile -File "'+(Join-Path $InstallRoot 'tools/Start-Portal.ps1')+'" -InstallRoot "'+$InstallRoot+'" -OpenBrowser'
$shortcut.WorkingDirectory=$InstallRoot;$shortcut.WindowStyle=7;$shortcut.Save()
if(-not $SkipConfigure){& (Join-Path $InstallRoot 'tools/Configure-Portal.ps1') -InstallRoot $InstallRoot; & (Join-Path $InstallRoot 'tools/Start-Portal.ps1') -InstallRoot $InstallRoot -OpenBrowser}
Write-Output 'Instalacion publicada. Acceso de escritorio: Iniciar Portal TOMWMS.'
