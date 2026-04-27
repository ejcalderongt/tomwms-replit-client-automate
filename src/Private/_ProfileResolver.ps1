# _ProfileResolver.ps1 — resuelve perfiles K7-PRD / BB-PRD / C9-QAS / LOCAL_DEV
# a connection strings y a hashtables aptos para Invoke-Sqlcmd.
#
# IMPORTANTE: nunca cachea $env:WMS_KILLIOS_DB_PASSWORD en variable PS;
# se resuelve en runtime cada vez (la pass puede rotar).

$script:WmsBrainProfiles = @{
    'K7-PRD' = @{
        Server   = '52.41.114.122,1437'
        Database = 'TOMWMS_KILLIOS_PRD'
        UserName = 'wmsuser'
        PasswordVar = 'WMS_KILLIOS_DB_PASSWORD'
        Trusted  = $false
        Codename = 'K7'
        Environment = 'PRD'
    }
    'BB-PRD' = @{
        Server   = '52.41.114.122,1437'
        Database = 'IMS4MB_BYB_PRD'
        UserName = 'wmsuser'
        PasswordVar = 'WMS_KILLIOS_DB_PASSWORD'
        Trusted  = $false
        Codename = 'BB'
        Environment = 'PRD'
    }
    'C9-QAS' = @{
        Server   = '52.41.114.122,1437'
        Database = 'IMS4MB_CEALSA_QAS'
        UserName = 'wmsuser'
        PasswordVar = 'WMS_KILLIOS_DB_PASSWORD'
        Trusted  = $false
        Codename = 'C9'
        Environment = 'QAS'
    }
    'LOCAL_DEV' = @{
        Server   = 'localhost'
        Database = 'TOMWMS_DEV'
        UserName = $null
        PasswordVar = $null
        Trusted  = $true
        Codename = 'LD'
        Environment = 'DEV'
    }
}

function Get-WmsBrainKnownProfile {
    [CmdletBinding()]
    param(
        [string] $Name
    )
    if (-not $Name) {
        $envP = $env:WMS_BRAIN_DEFAULT_PROFILE
        if (-not $envP) { $envP = 'K7-PRD' }
        $Name = $envP
    }
    if (-not $script:WmsBrainProfiles.ContainsKey($Name)) {
        throw "Perfil desconocido: '$Name'. Validos: $($script:WmsBrainProfiles.Keys -join ', ')"
    }
    return [PSCustomObject]@{
        Name        = $Name
        Server      = $script:WmsBrainProfiles[$Name].Server
        Database    = $script:WmsBrainProfiles[$Name].Database
        UserName    = $script:WmsBrainProfiles[$Name].UserName
        PasswordVar = $script:WmsBrainProfiles[$Name].PasswordVar
        Trusted     = $script:WmsBrainProfiles[$Name].Trusted
        Codename    = $script:WmsBrainProfiles[$Name].Codename
        Environment = $script:WmsBrainProfiles[$Name].Environment
    }
}

function Get-WmsBrainProfileNames {
    [CmdletBinding()] param()
    return $script:WmsBrainProfiles.Keys | Sort-Object
}

# Devuelve hashtable apto para splat con Invoke-Sqlcmd. Resuelve la pass
# en runtime; refusa si la var no esta seteada (exit code 3).
function Resolve-WmsBrainSqlcmdParams {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [PSCustomObject] $Profile,
        [string] $Query,
        [int] $QueryTimeout = 60,
        [int] $MaxRows = 10000
    )
    $h = @{
        ServerInstance = $Profile.Server
        Database       = $Profile.Database
        QueryTimeout   = $QueryTimeout
        ConnectionTimeout = 15
        TrustServerCertificate = $true
        Encrypt        = 'Optional'
    }
    # MaxRows ya no muta MaxBinaryLength: Invoke-Sqlcmd no expone -MaxRows
    # nativo, asi que el limite de filas se enforce client-side en
    # Invoke-WmsBrainQuery (Select-Object -First). MaxBinaryLength se deja
    # en el default del modulo SqlServer (1024 bytes) para no truncar.
    if ($Profile.Trusted) {
        # nada — Invoke-Sqlcmd usa Windows Auth si no se pasan -Username/-Password
    } else {
        $pwd = [Environment]::GetEnvironmentVariable($Profile.PasswordVar)
        if ([string]::IsNullOrEmpty($pwd)) {
            throw "[3] Variable de entorno '$($Profile.PasswordVar)' no esta seteada. Imprescindible para perfil $($Profile.Name)."
        }
        $h['Username'] = $Profile.UserName
        $h['Password'] = $pwd
    }
    if ($Query) { $h['Query'] = $Query }
    return $h
}
