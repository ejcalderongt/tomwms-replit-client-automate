param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")]
    [string]$Process,
    [Parameter(Mandatory = $true)]
    [string[]]$Files,
    [string[]]$Methods = @(),
    [string[]]$Tables = @(),
    [string[]]$Flags = @(),
    [string[]]$States = @(),
    [string]$OutPath,
    [string]$Tag = "EJC20260527"
)

$ErrorActionPreference = "Stop"

$lines = New-Object System.Collections.Generic.List[string]
$id = "rg-footprint-" + (Get-Date -Format "yyyyMMdd-HHmmss")
$lines.Add("id: $id")
$lines.Add("tag: '#$Tag'")
$lines.Add("process: $Process")
$lines.Add("fix_scope:")
$lines.Add("  files:")
foreach ($f in $Files) { $lines.Add("    - '$f'") }
$lines.Add("  methods:")
foreach ($m in $Methods) { $lines.Add("    - '$m'") }
$lines.Add("  tables:")
foreach ($t in $Tables) { $lines.Add("    - '$t'") }
$lines.Add("  flags:")
foreach ($fl in $Flags) { $lines.Add("    - '$fl'") }
$lines.Add("  states:")
foreach ($s in $States) { $lines.Add("    - '$s'") }
$lines.Add("failure_class: ''")
$lines.Add("notes: ''")

if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
} else {
    $lines | Out-String
}
