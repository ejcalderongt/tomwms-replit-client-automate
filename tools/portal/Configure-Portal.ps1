# #EJC20261105_PORTAL_PORTABLE: configuracion privada por usuario Windows; SQL solo lectura.
[CmdletBinding()]
param(
 [string]$InstallRoot = (Join-Path $env:LOCALAPPDATA 'TOMWMSPortal'),
 [string]$Server, [string]$Database, [PSCredential]$SqlCredential,
 [switch]$WindowsAuthentication, [switch]$TrustServerCertificate,
 [ValidateRange(1024,65535)][int]$UiPort=5174,
 [ValidateRange(1024,65535)][int]$ApiPort=5197
)
$ErrorActionPreference='Stop'
if($UiPort -eq $ApiPort){throw 'Los puertos de React y API deben ser distintos.'}
if(-not $Server){$Server=Read-Host 'Servidor SQL (ejemplo SERVIDOR\SQLEXPRESS o servidor,1433)'}
if(-not $Database){$Database=Read-Host 'Nombre exacto de la base WMS existente'}
if(-not $Server.Trim() -or -not $Database.Trim()){throw 'Servidor y base son obligatorios.'}
if(-not $WindowsAuthentication -and -not $SqlCredential){
 $auth=Read-Host 'Autenticacion Windows o SQL [W/S]'
 if($auth -match '^[Ww]$'){$WindowsAuthentication=$true}
 elseif($auth -match '^[Ss]$'){$SqlCredential=Get-Credential -Message 'Credenciales SQL para la base WMS'}
 else {throw 'Elegir W o S.'}
}
Add-Type -AssemblyName System.Data
$builder=New-Object System.Data.SqlClient.SqlConnectionStringBuilder
$builder['Data Source']=$Server.Trim();$builder['Initial Catalog']=$Database.Trim()
$builder['Encrypt']=$true;$builder['TrustServerCertificate']=[bool]$TrustServerCertificate
$builder['Connect Timeout']=15;$builder['Application Name']='TOMWMSPortal'
if($WindowsAuthentication){$builder['Integrated Security']=$true}
else {
 if(-not $SqlCredential){throw 'Faltan credenciales SQL.'}
 $builder['User ID']=$SqlCredential.UserName
 $builder['Password']=$SqlCredential.GetNetworkCredential().Password
}
$connection=New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
try {
 try {$connection.Open()} catch {throw 'No se pudo conectar a SQL. Revisar servidor, base, autenticacion, red y certificado. No se guardo configuracion.'}
 $cmd=$connection.CreateCommand();$cmd.CommandTimeout=15
 $cmd.CommandText="SELECT CASE WHEN OBJECT_ID('dbo.portal_usuario') IS NOT NULL AND OBJECT_ID('dbo.portal_usuario_permiso') IS NOT NULL AND OBJECT_ID('dbo.portal_propietario_modulo') IS NOT NULL AND OBJECT_ID('dbo.propietarios') IS NOT NULL AND OBJECT_ID('dbo.VW_Stock_Res') IS NOT NULL THEN 1 ELSE 0 END"
 if([int]$cmd.ExecuteScalar() -ne 1){throw 'BD sin los objetos requeridos del portal. Revisar el runbook y solicitar provisionamiento DBA; no se aplico SQL.'}
 $cmd.CommandText='SELECT COUNT(*) FROM dbo.portal_usuario WHERE Activo=1'
 if([int]$cmd.ExecuteScalar() -eq 0){throw 'No hay usuarios activos del portal en esta BD. Requiere provisionamiento autorizado; no se creo ninguna cuenta.'}
} finally {$connection.Dispose()}
$private=Join-Path $InstallRoot 'private'
New-Item -ItemType Directory -Force $private | Out-Null
# ACL local: usuario actual y SYSTEM; no heredar acceso de otros usuarios del equipo.
$acl=Get-Acl -LiteralPath $private
$acl.SetAccessRuleProtection($true,$false)
$sid=[Security.Principal.WindowsIdentity]::GetCurrent().User
foreach($principal in @($sid,[Security.Principal.SecurityIdentifier]::new('S-1-5-18'))){
 $acl.AddAccessRule([Security.AccessControl.FileSystemAccessRule]::new($principal,'FullControl','ContainerInherit,ObjectInherit','None','Allow'))
}
Set-Acl -LiteralPath $private -AclObject $acl
$secure=ConvertTo-SecureString $builder.ConnectionString -AsPlainText -Force
$secure | ConvertFrom-SecureString | Set-Content -LiteralPath (Join-Path $private 'connection.dpapi') -Encoding UTF8
$builder.Clear();$secure=$null
$settingsFile=Join-Path $private 'appsettings.json'
if(-not (Test-Path -LiteralPath $settingsFile)){
 $bytes=New-Object byte[] 64;$rng=[Security.Cryptography.RandomNumberGenerator]::Create();$rng.GetBytes($bytes);$rng.Dispose()
 @{AllowedHosts='localhost;127.0.0.1';MostrarDetallesErrores=$false;Entorno='PortalLocal';
   JwtSettings=@{Key=[Convert]::ToBase64String($bytes);Issuer='WMSWebAPI';Audience='WMSWebAPIUsers';ExpireMinutes=60};
   InternalAuth=@{Username=[guid]::NewGuid().ToString();Password=[guid]::NewGuid().ToString()};
   Logging=@{LogLevel=@{Default='Warning';'Microsoft.AspNetCore'='Warning'}}} |
  ConvertTo-Json -Depth 6 | Set-Content -LiteralPath $settingsFile -Encoding UTF8
}
@{UiPort=$UiPort;ApiPort=$ApiPort} | ConvertTo-Json | Set-Content (Join-Path $private 'ports.json') -Encoding UTF8
Write-Output 'Conexion comprobada y configuracion guardada para este usuario Windows. No se modifico la BD.'
