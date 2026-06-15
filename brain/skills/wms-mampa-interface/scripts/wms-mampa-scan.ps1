param(
    [Parameter(Mandatory = $false)]
    [string]$RepoRoot = "C:\Users\carol\source\repos\TOMWMS_BOF",

    [Parameter(Mandatory = $false)]
    [string]$OutputPath = ""
)

$ErrorActionPreference = 'Stop'

$target = Join-Path $RepoRoot "SAPSYNCMAMPA\Clases Interface Sync\Transacciones_WMS\clsSyncTransacWMS.vb"
if (-not (Test-Path -LiteralPath $target)) {
    throw "No se encontro la interface MAMPA en: $target"
}

$patterns = @(
    'Public Shared Async Function Procesar_Ajustes_SAP',
    'Private Shared Function AjusteYaExisteEnWMS',
    'Private Shared Function Get_Ajustes_Tiendas',
    'Public Shared Function MapearAAjustes',
    'Private Shared Async Function Procesar_Documentos_Ajustes',
    'RegistrarTrazaTransacWms',
    'RegistrarFalloTransacWmsAsync',
    'ActualizarProgresoSeguro'
)

$results = foreach ($pattern in $patterns) {
    $match = Select-String -Path $target -SimpleMatch -Pattern $pattern | Select-Object -First 1
    if ($match) {
        [pscustomobject]@{
            pattern = $pattern
            line    = $match.LineNumber
            text    = $match.Line.Trim()
        }
    }
}

function Escape-YamlValue {
    param([string]$Value)
    if ($null -eq $Value) { return "''" }
    return "'" + ($Value -replace "'", "''") + "'"
}

$lines = New-Object System.Collections.Generic.List[string]
$lines.Add("repo_root: " + (Escape-YamlValue $RepoRoot))
$lines.Add("target_file: " + (Escape-YamlValue $target))
$lines.Add("scan_time: " + (Escape-YamlValue ((Get-Date).ToString("s"))))
$lines.Add("hotspots:")

foreach ($item in $results) {
    $lines.Add("  - pattern: " + (Escape-YamlValue $item.pattern))
    $lines.Add("    line: " + $item.line)
    $lines.Add("    text: " + (Escape-YamlValue ($item.text -replace '\s+', ' ')))
}

$yaml = $lines -join "`n"

if ($OutputPath) {
    $parent = Split-Path -Parent $OutputPath
    if ($parent -and -not (Test-Path -LiteralPath $parent)) {
        New-Item -ItemType Directory -Path $parent -Force | Out-Null
    }
    Set-Content -LiteralPath $OutputPath -Value $yaml -Encoding UTF8
}

$yaml
