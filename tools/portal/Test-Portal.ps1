# #EJC20261105_PORTAL_PORTABLE: humo autenticado, sin escribir documentos ni guardar tokens.
[CmdletBinding()]
param([string]$InstallRoot=(Join-Path $env:LOCALAPPDATA 'TOMWMSPortal'),
 [PSCredential]$PortalCredential,
 [Parameter(Mandatory)][datetime]$From,[Parameter(Mandatory)][datetime]$To)
$ErrorActionPreference='Stop'
if($To -lt $From -or ($To-$From).TotalDays -gt 31){throw 'Seleccionar un rango de hasta 31 dias.'}
if(-not $PortalCredential){$PortalCredential=Get-Credential -Message 'Cuenta existente del portal de la BD elegida'}
if(-not $PortalCredential){throw 'Se requieren credenciales del portal.'}
$ports=Get-Content (Join-Path $InstallRoot 'private/ports.json') -Raw | ConvertFrom-Json
$base="http://127.0.0.1:$($ports.UiPort)"
$body=@{username=$PortalCredential.UserName;password=$PortalCredential.GetNetworkCredential().Password} | ConvertTo-Json
try {$login=Invoke-RestMethod "$base/api/PortalAcceso/login" -Method Post -ContentType 'application/json' -Body $body -TimeoutSec 120}
catch {throw 'Login rechazado o API no disponible. Revisar cuenta y permisos en la base elegida.'}
finally {$body=$null}
$ownerId=[int]$login.propietario.idPropietario
if(-not $login.token -or $ownerId -le 0){throw 'Contrato de login invalido.'}
$headers=@{Authorization='Bearer '+$login.token}
try {
 foreach($path in @('/api/PortalAcceso/me','/api/Bodegas/listar',"/api/Stock/listar?IdBodega=0&IdPropietario=$ownerId&page=1&pageSize=50",
   "/kpi/Kpi/picking?from=$($From.ToString('yyyy-MM-dd'))&to=$($To.ToString('yyyy-MM-dd'))",
   "/kpi/Kpi/recepcion?from=$($From.ToString('yyyy-MM-dd'))&to=$($To.ToString('yyyy-MM-dd'))")){
  try {$null=Invoke-RestMethod ($base+$path) -Headers $headers -TimeoutSec 120}
  catch {throw "Consulta rechazada o fallida: $path. Revisar permisos y logs locales; no se guardaron respuestas."}
  Write-Output "HTTP 200: $path"
 }
} finally {$headers=$null;$login=$null}
Write-Output 'Login, identidad, bodegas, inventario y KPIs picking/recepcion comprobados.'
