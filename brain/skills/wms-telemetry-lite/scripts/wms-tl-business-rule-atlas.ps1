param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [string[]]$RulePatterns = @(
    "verifica_auto",
    "procesado_bof",
    "Verificar_con_imagen",
    "Fotografia_Verificacion",
    "IdBodega",
    "IdUbicacionDestino",
    "TipoPedido",
    "Estado"
  ),
  [string]$OutPath = ""
)
$ErrorActionPreference = "Stop"
Push-Location $RepoPath
try {
  $lines = New-Object System.Collections.Generic.List[string]
  $lines.Add("id: business-rule-atlas-$(Get-Date -Format 'yyyyMMdd-HHmmss')")
  $lines.Add("tag: '#EJC20260527'")
  $lines.Add("rules:")
  foreach ($r in $RulePatterns) {
    $hits = rg -n --no-ignore -g *.vb -g *.java -g *.sql $r .
    $count = if ($hits) { @($hits).Count } else { 0 }
    $scope = if ($count -gt 500) { "very_high" } elseif ($count -gt 100) { "high" } elseif ($count -gt 20) { "medium" } else { "low" }
    $lines.Add("  - name: '$r'")
    $lines.Add("    occurrences: $count")
    $lines.Add("    impact_amplitude: '$scope'")
  }
  $lines.Add("candidate_unused_rules: []")
  $lines.Add("candidate_overlap_rules: []")
  $lines.Add("next_actions:")
  $lines.Add("  - 'Validate high-amplitude rules by process and client.'")
  $lines.Add("  - 'Audit low-occurrence rules for deprecation or missing wiring.'")
  $lines.Add("  - 'Cross-check overlap candidates in BOF/HH/WS gate conditions.'")

  if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
  } else { $lines | Out-String }
}
finally { Pop-Location }

