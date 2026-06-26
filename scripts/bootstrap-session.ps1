param(
  [string]$Domain = "default",
  [string]$Root = (Get-Location).Path
)

$indexPath = Join-Path $Root "brain\federated-index.yml"
if (-not (Test-Path $indexPath)) {
  throw "Missing federated index: $indexPath"
}

$today = (Get-Date).ToString("yyyy-MM-dd")
$lines = Get-Content -Path $indexPath
$brainMap = @{}
$routes = @{}
$section = $null
$currentBrain = $null
$currentRoute = $null
$inStartup = $false

foreach ($line in $lines) {
  if ($line -match '^brains:\s*$') { $section = 'brains'; continue }
  if ($line -match '^routes:\s*$') { $section = 'routes'; continue }
  if ($line -match '^notes:\s*$') { $section = 'notes'; continue }

  if ($section -eq 'brains') {
    if ($line -match '^\s{2}-\s+id:\s*(.+)$') {
      $currentBrain = $Matches[1].Trim()
      $brainMap[$currentBrain] = [ordered]@{
        id = $currentBrain
        path = $null
        kind = $null
        purpose = $null
        startup = New-Object System.Collections.Generic.List[string]
      }
      $inStartup = $false
      continue
    }
    if ($null -ne $currentBrain -and $line -match '^\s{4}path:\s*(.+)$') { $brainMap[$currentBrain].path = $Matches[1].Trim(); continue }
    if ($null -ne $currentBrain -and $line -match '^\s{4}kind:\s*(.+)$') { $brainMap[$currentBrain].kind = $Matches[1].Trim(); continue }
    if ($null -ne $currentBrain -and $line -match '^\s{4}purpose:\s*(.+)$') { $brainMap[$currentBrain].purpose = $Matches[1].Trim(); continue }
    if ($null -ne $currentBrain -and $line -match '^\s{4}startup:\s*$') { $inStartup = $true; continue }
    if ($null -ne $currentBrain -and $inStartup -and $line -match '^\s{6}-\s+(.+)$') {
      [void]$brainMap[$currentBrain].startup.Add($Matches[1].Trim())
      continue
    }
  }

  if ($section -eq 'routes') {
    if ($line -match '^\s{2}([A-Za-z0-9_-]+):\s*$') {
      $currentRoute = $Matches[1].Trim()
      $routes[$currentRoute] = New-Object System.Collections.Generic.List[string]
      continue
    }
    if ($null -ne $currentRoute -and $line -match '^\s{4}-\s+(.+)$') {
      [void]$routes[$currentRoute].Add($Matches[1].Trim())
      continue
    }
  }
}

$requestedRoute = $Domain.ToLowerInvariant()
if (-not $routes.ContainsKey($requestedRoute)) {
  $requestedRoute = 'default'
}

$paths = New-Object System.Collections.Generic.List[string]
foreach ($base in @(
  "AGENTS.md",
  "SOUL.md",
  "USER.md",
  "TOOLS.md",
  "MEMORY.md",
  ("memory\{0}.md" -f $today),
  "brain\federated-index.yml"
)) {
  [void]$paths.Add($base)
}

foreach ($brainId in $routes[$requestedRoute]) {
  if (-not $brainMap.ContainsKey($brainId)) { continue }
  foreach ($rel in $brainMap[$brainId].startup) {
    [void]$paths.Add($rel)
  }
}

$pack = [ordered]@{
  workspace_root = $Root
  domain = $requestedRoute
  route_manifest = "brain/federated-index.yml"
  session_date = $today
  brains = @()
  loaded = @()
}

foreach ($brainId in $routes[$requestedRoute]) {
  if ($brainMap.ContainsKey($brainId)) {
    $pack.brains += [ordered]@{
      id = $brainMap[$brainId].id
      path = $brainMap[$brainId].path
      kind = $brainMap[$brainId].kind
    }
  }
}

foreach ($rel in ($paths | Select-Object -Unique)) {
  $full = Join-Path $Root $rel
  if (Test-Path $full) {
    $pack.loaded += [ordered]@{
      path = $rel
      size = (Get-Item $full).Length
    }
  }
}

$pack | ConvertTo-Json -Depth 6
