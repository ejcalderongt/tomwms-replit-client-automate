param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")]
    [string]$Process,
    [string]$FootprintPath,
    [string]$SiblingScanPath,
    [string]$ChecklistPath,
    [ValidateSet("low","medium","high")]
    [string]$RiskLevel = "medium",
    [string]$Recommendation = "",
    [string]$OutPath
)

$ErrorActionPreference = "Stop"

$lines = New-Object System.Collections.Generic.List[string]
$id = "rg-risk-" + (Get-Date -Format "yyyyMMdd-HHmmss")
$lines.Add("id: $id")
$lines.Add("tag: '#EJC20260527'")
$lines.Add("process: $Process")
$lines.Add("risk_level: $RiskLevel")
$lines.Add("open_risks: []")
$lines.Add("evidence:")
$lines.Add("  footprint: '$FootprintPath'")
$lines.Add("  sibling_scan: '$SiblingScanPath'")
$lines.Add("  checklist: '$ChecklistPath'")
$lines.Add("recommendation: '$Recommendation'")

if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
} else {
    $lines | Out-String
}
