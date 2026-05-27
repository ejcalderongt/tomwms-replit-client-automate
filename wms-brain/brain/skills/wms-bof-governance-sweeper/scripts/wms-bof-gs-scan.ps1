param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [string]$ScopePath = "TOMIMSV4",
  [Parameter(Mandatory = $true)][string]$OutPath
)
$ErrorActionPreference = "Stop"

$scanRoot = Join-Path $RepoPath $ScopePath
if (-not (Test-Path $scanRoot)) { throw "Scope path not found: $scanRoot" }

$vbFiles = Get-ChildItem -Path $scanRoot -Recurse -Filter *.vb -File

$unused = [System.Collections.Generic.List[string]]::new()
$dupSql = [System.Collections.Generic.Dictionary[string,int]]::new()
$heavy = [System.Collections.Generic.List[string]]::new()
$roundtrip = [System.Collections.Generic.List[string]]::new()
$ruleRisk = [System.Collections.Generic.List[string]]::new()

foreach ($f in $vbFiles) {
  $text = Get-Content -Path $f.FullName -Raw -ErrorAction SilentlyContinue
  if ($null -eq $text) { continue }
  $lines = $text -split "`r?`n"
  $isDesigner = $f.Name -like "*.Designer.vb"

  # 1) Unused private method candidates (same-file heuristic)
  if (-not $isDesigner) {
    for ($i = 0; $i -lt $lines.Count; $i++) {
      $line = $lines[$i]
      $m = [regex]::Match($line, '^\s*Private\s+(Sub|Function)\s+([A-Za-z0-9_]+)\b')
      if ($m.Success) {
        $method = $m.Groups[2].Value
        $occ = ([regex]::Matches($text, "\b$method\b")).Count
        if ($occ -le 1) {
          $unused.Add("$($f.FullName):$($i+1): private method candidate unused -> $method")
        }
      }
    }
  }

  # 2) Duplicated SQL fragment candidates
  for ($i = 0; $i -lt $lines.Count; $i++) {
    $line = $lines[$i].Trim()
    if ($line -match '(?i)(SELECT|FROM|WHERE|JOIN|GROUP BY|ORDER BY)') {
      $key = ($line -replace '\s+', ' ').ToUpperInvariant()
      if ($key.Length -ge 25) {
        if (-not $dupSql.ContainsKey($key)) { $dupSql[$key] = 0 }
        $dupSql[$key]++
      }
    }
  }

  # 3) Heavy query candidates
  for ($i = 0; $i -lt $lines.Count; $i++) {
    $line = $lines[$i]
    if ($line -match '(?i)SELECT\s+\*' -or
        $line -match '(?i)IN\s*\(\s*SELECT' -or
        $line -match "(?i)LIKE\s+'%.*%'" -or
        $line -match '(?i)ORDER\s+BY' -or
        $line -match '(?i)NOLOCK') {
      $heavy.Add("$($f.FullName):$($i+1): $($line.Trim())")
    }
  }

  # 4) Roundtrip hotspot candidates
  $rtCount = ([regex]::Matches($text, '(?i)(callmethod\(|ExecuteNonQuery|ExecuteScalar|filldt\(|wsExecute|execws\()')).Count
  if ($rtCount -ge 8) {
    $roundtrip.Add("$($f.FullName): roundtrip-call density = $rtCount")
  }

  # 5) Business-rule risk candidates
  if (-not $isDesigner) {
    for ($i = 0; $i -lt $lines.Count; $i++) {
      $line = $lines[$i]
      if ($line -match '(?i)\b(IdEstado|Estado|Parametro|Bodega|Permitir|Reemplazo|Despacho|Recepcion)\b' -and
          $line -match '(?i)(=|<>|>|<)\s*[0-9]+') {
        $ruleRisk.Add("$($f.FullName):$($i+1): $($line.Trim())")
      }
    }
  }
}

$dupTop = $dupSql.GetEnumerator() | Where-Object { $_.Value -ge 5 } | Sort-Object Value -Descending | Select-Object -First 50

$out = [System.Collections.Generic.List[string]]::new()
$out.Add("scope: $scanRoot")
$out.Add("vb_files_scanned: $($vbFiles.Count)")
$out.Add("")
$out.Add("=== UNUSED_PRIVATE_CANDIDATES ===")
if ($unused.Count -eq 0) { $out.Add("(none)") } else { $unused | Select-Object -First 200 | ForEach-Object { $out.Add($_) } }
$out.Add("")
$out.Add("=== DUPLICATE_SQL_FRAGMENT_CANDIDATES ===")
if (($dupTop | Measure-Object).Count -eq 0) { $out.Add("(none)") } else { $dupTop | ForEach-Object { $out.Add("count=$($_.Value) | fragment=$($_.Key)") } }
$out.Add("")
$out.Add("=== HEAVY_QUERY_CANDIDATES ===")
if ($heavy.Count -eq 0) { $out.Add("(none)") } else { $heavy | Select-Object -First 300 | ForEach-Object { $out.Add($_) } }
$out.Add("")
$out.Add("=== ROUNDTRIP_HOTSPOTS ===")
if ($roundtrip.Count -eq 0) { $out.Add("(none)") } else { $roundtrip | Select-Object -First 200 | ForEach-Object { $out.Add($_) } }
$out.Add("")
$out.Add("=== BUSINESS_RULE_RISK_CANDIDATES ===")
if ($ruleRisk.Count -eq 0) { $out.Add("(none)") } else { $ruleRisk | Select-Object -First 300 | ForEach-Object { $out.Add($_) } }

[System.IO.File]::WriteAllLines($OutPath, $out, [System.Text.UTF8Encoding]::new($false))
Write-Output "Wrote $OutPath"
