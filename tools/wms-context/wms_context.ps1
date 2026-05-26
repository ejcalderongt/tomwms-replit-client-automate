param(
    [Parameter(Position = 0, Mandatory = $true)]
    [string]$Task,

    [switch]$NoLlm,
    [switch]$Refresh,
    [string]$Model,
    [string]$LmStudioUrl
)

$ErrorActionPreference = "Stop"

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$pythonScript = Join-Path $scriptDir "wms_context.py"

if (-not (Test-Path $pythonScript)) {
    throw "No se encontro $pythonScript"
}

$taskText = $Task.Trim()
if ([string]::IsNullOrWhiteSpace($taskText)) {
    throw "Uso: .\tools\wms-context\wms_context.ps1 ""descripcion de la tarea"""
}

$argsList = @($pythonScript, $taskText)

if ($NoLlm) {
    $argsList += "--no-llm"
}
if ($Refresh) {
    $argsList += "--refresh"
}
if (-not [string]::IsNullOrWhiteSpace($Model)) {
    $argsList += @("--model", $Model)
}
if (-not [string]::IsNullOrWhiteSpace($LmStudioUrl)) {
    $argsList += @("--lm-studio-url", $LmStudioUrl)
}

python @argsList
