function Get-WmsBrainConnectionString {
<#
.SYNOPSIS
    Devuelve el connection string de un perfil (K7-PRD/BB-PRD/C9-QAS/LOCAL_DEV).

.DESCRIPTION
    Resuelve el perfil al server, database, user y resuelve la password en
    runtime desde la var de entorno indicada (NUNCA cachea ni loggea el valor).

    Si -AsHashtable, devuelve un hash splat-eable para Invoke-Sqlcmd.

.PARAMETER Profile
    Uno de K7-PRD, BB-PRD, C9-QAS, LOCAL_DEV. Default: $env:WMS_BRAIN_DEFAULT_PROFILE
    o K7-PRD si no esta seteado.

.PARAMETER AsHashtable
    Devuelve hashtable splat-eable para Invoke-Sqlcmd (sin imprimir password).

.EXAMPLE
    Get-WmsBrainConnectionString -Profile BB-PRD

.NOTES
    El string devuelto contiene la password en claro. NO lo loggear.
#>
    [CmdletBinding()]
    param(
        [ValidateSet('K7-PRD', 'BB-PRD', 'C9-QAS', 'LOCAL_DEV')]
        [string] $Profile,

        [switch] $AsHashtable
    )

    $p = Get-WmsBrainKnownProfile -Name $Profile
    Write-WmsBrainLog -Cmdlet 'Get-WmsBrainConnectionString' -Level 'INFO' `
        -Message "perfil=$($p.Name) server=$($p.Server) db=$($p.Database)"

    if ($AsHashtable) {
        return Resolve-WmsBrainSqlcmdParams -Profile $p
    }

    if ($p.Trusted) {
        return "Server=$($p.Server);Database=$($p.Database);Integrated Security=true;TrustServerCertificate=true;"
    }
    $pwd = [Environment]::GetEnvironmentVariable($p.PasswordVar)
    if ([string]::IsNullOrEmpty($pwd)) {
        throw "[3] Variable '$($p.PasswordVar)' no seteada. Imprescindible para perfil $($p.Name)."
    }
    return "Server=$($p.Server);Database=$($p.Database);User Id=$($p.UserName);Password=$pwd;TrustServerCertificate=true;Encrypt=Optional;"
}
