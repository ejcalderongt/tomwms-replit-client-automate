# #EJC20261105_PORTAL_PORTABLE: detener unicamente procesos registrados por esta instalacion.
param([string]$InstallRoot=(Join-Path $env:LOCALAPPDATA 'TOMWMSPortal'))
$ErrorActionPreference='Stop'
$stateFile=Join-Path $InstallRoot 'private/processes.json'
if(-not (Test-Path $stateFile)){Write-Output 'Sin procesos registrados.';return}
$state=Get-Content $stateFile -Raw | ConvertFrom-Json
foreach($entry in $state.PSObject.Properties){
 $record=$entry.Value;$process=Get-Process -Id $record.Id -ErrorAction SilentlyContinue
 if($process -and $process.StartTime.ToUniversalTime().Ticks -eq [long]$record.StartedTicks){Stop-Process -Id $process.Id -ErrorAction Stop}
}
Remove-Item -LiteralPath $stateFile
Write-Output 'Procesos de esta instalacion detenidos.'
