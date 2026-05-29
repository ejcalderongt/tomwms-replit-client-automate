param(
  [ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")][string]$Process = "picking",
  [string]$OutPath,
  [string]$Tag = "EJC20260527"
)
$ErrorActionPreference = "Stop"
$defaultStates = @("Nuevo","Pendiente","Procesado","Verificado","Despachado","Anulado")
$defaultTransitions = @(
  "Nuevo->Pendiente",
  "Pendiente->Procesado",
  "Procesado->Verificado",
  "Verificado->Despachado",
  "Nuevo->Anulado",
  "Pendiente->Anulado",
  "Procesado->Anulado",
  "Verificado->Anulado"
)
$lines = New-Object System.Collections.Generic.List[string]
$lines.Add("id: sma-$(Get-Date -Format 'yyyyMMdd-HHmmss')-$Process")
$lines.Add("tag: '#$Tag'")
$lines.Add("process: $Process")
$lines.Add("canonical_states:")
foreach ($s in $defaultStates) { $lines.Add("  - '$s'") }
$lines.Add("transitions:")
foreach ($t in $defaultTransitions) { $lines.Add("  - '$t'") }
$lines.Add("ui_gates: []")
$lines.Add("ws_gates: []")
$lines.Add("db_observed_states: []")
$lines.Add("residual_risk: medium")
if ($OutPath) {
  $dir = Split-Path -Parent $OutPath
  if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
  [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
  Write-Output "Wrote $OutPath"
} else { $lines | Out-String }

