param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")][string]$Process = "picking",
  [string]$OutPath = ""
)
$ErrorActionPreference = "Stop"
$patterns = @(
  'Estado\s*=\s*"',
  '\.Estado\s*<>',
  'Despachado',
  'Verificado',
  'Procesado',
  'Pendiente',
  'Anulado',
  'Enabled\s*='
)
Push-Location $RepoPath
try {
  $lines = New-Object System.Collections.Generic.List[string]
  foreach ($p in $patterns) {
    $lines.Add("=== PATTERN: $p ===")
    $res = rg -n --no-ignore -g *.vb -g *.java $p .
    if ([string]::IsNullOrWhiteSpace(($res | Out-String))) { $lines.Add("(sin hallazgos)") } else { foreach ($r in $res) { $lines.Add($r) } }
    $lines.Add("")
  }
  if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
  } else { $lines | Out-String }
}
finally { Pop-Location }

