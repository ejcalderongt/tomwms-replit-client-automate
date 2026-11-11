param([Parameter(Mandatory)][string]$Run,[Parameter(Mandatory)][ValidateSet('start','end','note')][string]$Phase,
      [Parameter(Mandatory)][string]$Operation,[string]$Device='', [string]$Zone='', [string]$Note='')
if (-not (Test-Path -LiteralPath (Join-Path $Run 'session.json'))) { throw 'Run must be an existing diagnostics session' }
[pscustomobject]@{timestampUtc=[datetime]::UtcNow.ToString('o');phase=$Phase;operation=$Operation;device=$Device;zone=$Zone;note=$Note} |
    Export-Csv (Join-Path $Run 'incidents.csv') -Append -NoTypeInformation -Encoding UTF8
