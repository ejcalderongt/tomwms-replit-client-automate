# #EJC20261027_BRAIN_GRAPH_BUILD
param(
    [string]$BofRoot = "$env:USERPROFILE/source/repos/TOMWMS_BOF",
    [string]$HhRoot = "$env:USERPROFILE/source/repos/TOMHH2025",
    [string]$DbaRoot = "$env:USERPROFILE/source/repos/DBA",
    [string]$PythonPath = "$env:USERPROFILE/AppData/Local/PrograX24/brain-runtime/Scripts/python.exe",
    [switch]$AllSqlSources,
    [string[]]$SqlFiles = @('20260703_ml_get_ubicacion_sugerida_preload_tramo.sql', 'VW_Ubicaciones_Tramo_Disponibles', 'stock', 'stock_res', 'VW_Stock_Res')
)
$ErrorActionPreference = 'Stop'
$brainQualityRoot = $PSScriptRoot
$brainRepositoryRoot = Split-Path (Split-Path $brainQualityRoot -Parent) -Parent
$outputDirectory = Join-Path $brainRepositoryRoot 'analysis/brain-improvement'
foreach ($target in @($BofRoot, $HhRoot, $DbaRoot, $PythonPath)) {
    if (-not (Test-Path -LiteralPath $target)) { throw "Required local path unavailable: $target" }
}
$project = Join-Path $brainQualityRoot 'ast-exporter/AstExporter.csproj'
& dotnet build $project --nologo -v:q
if ($LASTEXITCODE -ne 0) { throw 'AST exporter build failed' }
$exporter = Join-Path $brainQualityRoot 'ast-exporter/bin/Debug/net10.0/AstExporter.dll'
$vbFacts = Join-Path $outputDirectory 'vb-syntax-facts.json'
$javaFacts = Join-Path $outputDirectory 'java-syntax-facts.json'
$sqlFacts = Join-Path $outputDirectory 'sql-syntax-facts.json'
& dotnet $exporter $BofRoot $vbFacts 'WSHHRN/WSHHRN.vbproj' 'TOMIMSV4/DAL/DAL.vbproj'
if ($LASTEXITCODE -ne 0) { throw 'VB export failed; graph not rebuilt' }
& $PythonPath (Join-Path $brainQualityRoot 'java_facts.py') --root $HhRoot --output $javaFacts
if ($LASTEXITCODE -ne 0) { throw 'Java export failed; graph not rebuilt' }
if ($AllSqlSources) {
    $inventoryPath = Join-Path $outputDirectory 'sql-source-inventory.json'
    & $PythonPath (Join-Path $brainQualityRoot 'sql_inventory.py') --root $DbaRoot --output $inventoryPath
    if ($LASTEXITCODE -ne 0) { throw 'SQL source inventory failed' }
    & dotnet $exporter --sql $DbaRoot $sqlFacts "@$inventoryPath"
} else {
    & dotnet $exporter --sql $DbaRoot $sqlFacts @SqlFiles
}
if ($LASTEXITCODE -ne 0) { throw 'SQL export failed; graph not rebuilt' }
& $PythonPath (Join-Path $brainQualityRoot 'graph.py') build --vb $vbFacts --java $javaFacts --sql $sqlFacts --output (Join-Path $outputDirectory 'functional-graph.json')
if ($LASTEXITCODE -ne 0) { throw 'Graph generation failed' }
