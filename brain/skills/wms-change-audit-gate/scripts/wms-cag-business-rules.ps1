param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [Parameter(Mandatory = $true)][string[]]$Files,
  [string[]]$RulePatterns = @("verifica_auto","procesado_bof","Verificar_con_imagen","Fotografia_Verificacion","TipoPedido","IdBodega","Estado","IdUbicacionDestino"),
  [string]$OutPath = ""
)
$ErrorActionPreference = "Stop"
Push-Location $RepoPath
try {
  $lines = New-Object System.Collections.Generic.List[string]
  $lines.Add("check: business_rules")
  foreach ($r in $RulePatterns) {
    $count = 0
    foreach ($f in $Files) {
      $hits = rg -n $r $f
      if ($hits) { $count += @($hits).Count }
    }
    $lines.Add("$r=$count")
  }
  if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
  } else { $lines | Out-String }
}
finally { Pop-Location }

