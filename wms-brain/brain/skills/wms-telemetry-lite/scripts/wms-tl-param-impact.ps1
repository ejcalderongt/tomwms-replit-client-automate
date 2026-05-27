param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [string[]]$ParamPatterns = @("verifica_auto","Verificar_con_imagen","procesado_bof","IdBodega","IdUbicacionDestino"),
  [string]$OutPath = ""
)
$ErrorActionPreference = "Stop"
Push-Location $RepoPath
try {
  $lines = New-Object System.Collections.Generic.List[string]
  $lines.Add("id: telemetry-param-impact-$(Get-Date -Format 'yyyyMMdd-HHmmss')")
  $lines.Add("tag: '#EJC20260527'")
  $lines.Add("parameters:")
  foreach ($p in $ParamPatterns) {
    $lines.Add("  - name: '$p'")
    $hits = rg -n --no-ignore -g *.vb -g *.java -g *.sql $p .
    $count = if ($hits) { @($hits).Count } else { 0 }
    $lines.Add("    occurrences: $count")
  }
  $lines.Add("impact:")
  $lines.Add("  by_bodega: []")
  $lines.Add("  by_interface: []")
  $lines.Add("  blast_radius: []")
  $lines.Add("unused_candidates: []")
  $lines.Add("overlap_candidates: []")
  if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
  } else { $lines | Out-String }
}
finally { Pop-Location }

