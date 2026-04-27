# _Logger.ps1 — formato unificado de logs del modulo.
#
# Convencion: [WmsBrainClient] [<cmdlet>] [<lvl>] <msg>
# Niveles: INFO, WARN, ERROR, DEBUG, OK
#
# DEBUG sale solo si $VerbosePreference != 'SilentlyContinue'.
# Salidas a stderr cuando lvl >= WARN, salvo banners.

function Write-WmsBrainLog {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $Cmdlet,
        [Parameter(Mandatory)] [ValidateSet('INFO', 'WARN', 'ERROR', 'DEBUG', 'OK')] [string] $Level,
        [Parameter(Mandatory)] [string] $Message
    )
    $line = "[WmsBrainClient] [$Cmdlet] [$Level] $Message"
    switch ($Level) {
        'ERROR' { [Console]::Error.WriteLine($line) }
        'WARN'  { [Console]::Error.WriteLine($line) }
        'DEBUG' { Write-Verbose $line }
        default { Write-Information $line -InformationAction Continue }
    }
}

function Write-WmsBrainBanner {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string[]] $Lines
    )
    foreach ($l in $Lines) {
        Write-Host $l
    }
}

# Sanitiza valores sensibles antes de loggear. Reemplaza valores conocidos
# por placeholders. NUNCA imprime el contenido literal de las vars secret.
function ConvertTo-WmsBrainSafeString {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [AllowEmptyString()] [string] $InputString
    )
    $secretEnvVars = @(
        'WMS_KILLIOS_DB_PASSWORD',
        'BRAIN_IMPORT_TOKEN',
        'AZURE_DEVOPS_PAT'
    )
    $out = $InputString
    foreach ($v in $secretEnvVars) {
        $val = [Environment]::GetEnvironmentVariable($v)
        if ($val -and $val.Length -gt 4) {
            $out = $out.Replace($val, "<$v:redacted>")
        }
    }
    return $out
}
