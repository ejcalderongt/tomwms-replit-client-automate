param(
  [Parameter(Mandatory = $true)][string]$RepoPath,
  [Parameter(Mandatory = $true)][string[]]$Files,
  [string]$OutPath = "",
  [string]$SqlServer = "",
  [string]$SqlDatabase = "",
  [string]$SqlUser = "",
  [string]$SqlPassword = ""
)
$ErrorActionPreference = "Stop"

function Get-DbTokens {
  param([string]$filePath)
  $patterns = @(
    "\btrans_[A-Za-z0-9_]+\b",
    "\bstock(_[A-Za-z0-9_]+)?\b",
    "\bsp_[A-Za-z0-9_]+\b",
    "\bVW_[A-Za-z0-9_]+\b"
  )
  $tokens = New-Object System.Collections.Generic.HashSet[string]
  foreach ($p in $patterns) {
    $hits = rg --no-filename --no-line-number -o $p $filePath
    if ($hits) {
      foreach ($h in $hits) {
        $val = $h.Trim()
        if ($val -match '^\d+:(.+)$') {
          $val = $Matches[1]
        }
        if (-not [string]::IsNullOrWhiteSpace($val)) { [void]$tokens.Add($val) }
      }
    }
  }
  return $tokens
}

Push-Location $RepoPath
try {
  $allTokens = New-Object System.Collections.Generic.HashSet[string]
  foreach ($f in $Files) {
    if (Test-Path $f) {
      $tokens = Get-DbTokens -filePath $f
      foreach ($t in $tokens) { [void]$allTokens.Add($t) }
    }
  }

  $lines = New-Object System.Collections.Generic.List[string]
  $lines.Add("check: db_correlation")
  $lines.Add("tokens:")
  foreach ($t in ($allTokens | Sort-Object)) { $lines.Add("  - $t") }

  $canQueryDb = -not [string]::IsNullOrWhiteSpace($SqlServer) -and -not [string]::IsNullOrWhiteSpace($SqlDatabase) -and -not [string]::IsNullOrWhiteSpace($SqlUser) -and -not [string]::IsNullOrWhiteSpace($SqlPassword)
  if ($canQueryDb -and $allTokens.Count -gt 0) {
    $lines.Add("db_exists:")
    foreach ($t in ($allTokens | Sort-Object)) {
      $query = "SET NOCOUNT ON; SELECT CASE WHEN EXISTS (SELECT 1 FROM sys.objects WHERE name = '$t') THEN 1 ELSE 0 END"
      $res = sqlcmd -S $SqlServer -d $SqlDatabase -U $SqlUser -P $SqlPassword -h -1 -W -Q $query 2>$null
      $resText = ($res | Out-String)
      $exists = ($resText -match '(^|\s)1(\s|$)')
      $lines.Add("  - ${t}: $exists")
    }
  } else {
    $lines.Add("db_exists: []")
    $lines.Add("note: DB lookup skipped (missing credentials or no tokens).")
  }

  if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
  } else { $lines | Out-String }
}
finally { Pop-Location }
