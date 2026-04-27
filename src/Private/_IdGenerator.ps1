# _IdGenerator.ps1 — genera ids unicos formato YYYYMMDD-HHMM-INIT.
#
# Si dos eventos se crean en el mismo minuto, el segundo se pospone +1 minuto
# hasta encontrar un id no presente en la lista de ids vistos.

function New-WmsBrainEventId {
    [CmdletBinding()]
    param(
        [string] $Initials,
        [string[]] $ExistingIds = @(),
        [DateTime] $At = (Get-Date)
    )
    $init = ($Initials -replace '[^A-Za-z0-9]', '').ToUpper()
    if (-not $init) {
        $envInit = $env:WMS_BRAIN_AUTHOR_INIT
        if ($envInit) {
            $init = ($envInit -replace '[^A-Za-z0-9]', '').ToUpper()
        }
    }
    if (-not $init) { $init = 'EJC' }
    if ($init.Length -gt 4) { $init = $init.Substring(0, 4) }

    $cursor = $At
    $maxTries = 240
    for ($i = 0; $i -lt $maxTries; $i++) {
        $candidate = '{0}{1}{2}-{3}{4}-{5}' -f `
            $cursor.Year.ToString('0000'),
            $cursor.Month.ToString('00'),
            $cursor.Day.ToString('00'),
            $cursor.Hour.ToString('00'),
            $cursor.Minute.ToString('00'),
            $init
        if ($ExistingIds -notcontains $candidate) {
            return $candidate
        }
        $cursor = $cursor.AddMinutes(1)
    }
    throw "No pude generar id unico tras $maxTries intentos. Iniciales='$init'."
}

function Get-WmsBrainIsoLocal {
    [CmdletBinding()]
    param(
        [DateTime] $At = (Get-Date)
    )
    # Formato ISO-8601 con offset local (no Z salvo UTC real).
    $offset = [TimeZoneInfo]::Local.GetUtcOffset($At)
    $sign = if ($offset.TotalMinutes -ge 0) { '+' } else { '-' }
    $hh = ([int][math]::Abs($offset.Hours)).ToString('00')
    $mm = ([int][math]::Abs($offset.Minutes)).ToString('00')
    return ('{0:yyyy-MM-ddTHH:mm:ss}{1}{2}:{3}' -f $At, $sign, $hh, $mm)
}
