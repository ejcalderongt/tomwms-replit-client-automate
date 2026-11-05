# #EJC20261105_PORTAL_PORTABLE: servicios en loopback y secretos DPAPI fuera del webroot.
[CmdletBinding()]
param([string]$InstallRoot=(Join-Path $env:LOCALAPPDATA 'TOMWMSPortal'),[switch]$OpenBrowser)
$ErrorActionPreference='Stop'
$private=Join-Path $InstallRoot 'private';$app=Join-Path $InstallRoot 'app'
$ports=Get-Content (Join-Path $private 'ports.json') -Raw | ConvertFrom-Json
$node=(Get-Content (Join-Path $private 'runtime.json') -Raw | ConvertFrom-Json).NodePath
$config=Join-Path $private 'appsettings.json'
$encrypted=(Get-Content (Join-Path $private 'connection.dpapi') -Raw).Trim() | ConvertTo-SecureString
$ptr=[Runtime.InteropServices.Marshal]::SecureStringToBSTR($encrypted)
try {$connection=[Runtime.InteropServices.Marshal]::PtrToStringBSTR($ptr)} finally {[Runtime.InteropServices.Marshal]::ZeroFreeBSTR($ptr)}
$stateFile=Join-Path $private 'processes.json'
$state=@{}
if(Test-Path $stateFile){$saved=Get-Content $stateFile -Raw | ConvertFrom-Json;foreach($p in $saved.PSObject.Properties){$state[$p.Name]=$p.Value}}
$logs=Join-Path $InstallRoot 'logs';New-Item -ItemType Directory -Force $logs | Out-Null
function Assert-PortOwner($name,$port){
 $listener=@(Get-NetTCPConnection -State Listen -LocalPort $port -ErrorAction SilentlyContinue)
 if(-not $listener.Count){return $false}
 $record=$state[$name]
 if(-not $record){throw "Puerto $port ocupado por otro proceso. Configurar puertos libres; no se detuvo ningun proceso."}
 $process=Get-Process -Id $record.Id -ErrorAction SilentlyContinue
 if(-not $process -or $process.StartTime.ToUniversalTime().Ticks -ne [long]$record.StartedTicks -or @($listener | Where-Object OwningProcess -ne $record.Id).Count){throw "El puerto $port no pertenece a esta instalacion."}
 return $true
}
$previousConnection=$env:ConnectionStrings__CST;$previousPort=$env:PORTAL_UI_PORT;$previousApi=$env:PORTAL_API_PORT
try {
 $env:ConnectionStrings__CST=$connection;$env:PORTAL_UI_PORT=[string]$ports.UiPort;$env:PORTAL_API_PORT=[string]$ports.ApiPort
 if(-not (Assert-PortOwner 'Api' $ports.ApiPort)){
  Copy-Item -LiteralPath $config -Destination (Join-Path $app 'api/appsettings.json') -Force
  $process=Start-Process -FilePath (Join-Path $app 'api/WMSWebAPI.exe') -WorkingDirectory (Join-Path $app 'api') -ArgumentList @('--environment','PortalLocal','--urls',"http://127.0.0.1:$($ports.ApiPort)") -WindowStyle Hidden -PassThru -RedirectStandardOutput (Join-Path $logs 'api.log') -RedirectStandardError (Join-Path $logs 'api-error.log')
  $state['Api']=@{Id=$process.Id;StartedTicks=$process.StartTime.ToUniversalTime().Ticks}
 }
 # React no necesita recibir la conexion a SQL.
 $env:ConnectionStrings__CST=$previousConnection
 if(-not (Assert-PortOwner 'Ui' $ports.UiPort)){
  $process=Start-Process -FilePath $node -WorkingDirectory $app -ArgumentList 'portal-server.cjs' -WindowStyle Hidden -PassThru -RedirectStandardOutput (Join-Path $logs 'ui.log') -RedirectStandardError (Join-Path $logs 'ui-error.log')
  $state['Ui']=@{Id=$process.Id;StartedTicks=$process.StartTime.ToUniversalTime().Ticks}
 }
} finally {
 $env:ConnectionStrings__CST=$previousConnection;$connection=$null
 $env:PORTAL_UI_PORT=$previousPort;$env:PORTAL_API_PORT=$previousApi
 $state | ConvertTo-Json -Depth 4 | Set-Content $stateFile -Encoding UTF8
}
$base="http://127.0.0.1:$($ports.UiPort)";$deadline=(Get-Date).AddSeconds(60);$ready=$false
do {
 try {
  $ui=Invoke-WebRequest "$base/login" -UseBasicParsing -TimeoutSec 3
  $status=0
  try {$null=Invoke-WebRequest "$base/api/PortalAcceso/me" -UseBasicParsing -TimeoutSec 3} catch {if($_.Exception.Response){$status=[int]$_.Exception.Response.StatusCode}}
  $ready=$ui.StatusCode -eq 200 -and $ui.Content -match '/assets/' -and $status -eq 401
 } catch {$ready=$false}
 if(-not $ready){Start-Sleep -Seconds 1}
} until($ready -or (Get-Date) -ge $deadline)
if(-not $ready){throw "Portal no disponible. Revisar $logs"}
Write-Output "React y API responden: $base/login"
if($OpenBrowser){Start-Process "$base/login"}
