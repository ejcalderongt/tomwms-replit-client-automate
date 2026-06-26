param(
  [string]$Root = (Get-Location).Path
)

$files = @(
  'AGENTS.md',
  'SOUL.md',
  'USER.md',
  'TOOLS.md',
  'MEMORY.md',
  'HEARTBEAT.md',
  'wms-brain\README.md',
  'mpos-brain\README.md',
  'brain\atlas\implosion-mapa.yml',
  'brain\wms\recepcion\README.md',
  'brain\wms\recepcion\lp-flujo-analisis.yml',
  'brain\wms\recepcion\lp-endpoints.yml',
  'brain\wms\recepcion\db-probes.yml',
  'brain\wms\recepcion\implosion\implosion-flujo.yml',
  'brain\wms\recepcion\implosion\implosion-trazas.yml'
)

$result = [ordered]@{
  workspace_root = $Root
  anchors = @()
  brains = @()
  task_files = @()
}

foreach ($rel in $files) {
  $path = Join-Path $Root $rel
  if (-not (Test-Path $path)) { continue }
  $entry = [ordered]@{
    path = $rel
    size = (Get-Item $path).Length
  }
  if ($rel -match '^(AGENTS|SOUL|USER|TOOLS|MEMORY|HEARTBEAT)\.md$') {
    $result.anchors += $entry
  } elseif ($rel -match '^(wms-brain|mpos-brain)\\README\.md$') {
    $result.brains += $entry
  } else {
    $result.task_files += $entry
  }
}

$result | ConvertTo-Json -Depth 6
