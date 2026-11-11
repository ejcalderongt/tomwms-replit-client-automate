# Build a portable, credential-free baseline. Does not sync or push the Brain.
param([Parameter(Mandatory)][string]$Destination)
$ErrorActionPreference='Stop'
$brain=(Resolve-Path (Join-Path $PSScriptRoot '../..')).Path
if (Test-Path -LiteralPath $Destination) { throw 'Destination must not already exist (preserve existing packages)' }
New-Item -ItemType Directory -Path $Destination | Out-Null
$Destination=(Resolve-Path $Destination).Path
# Explicit allowlist: never distribute local configs, captures, auth headers or arbitrary analysis.
$files=@('README.md','RESTAURAR-OTRA-PC.md','PROMPT-AGENTE.md','REPORT-TEMPLATE.md','VALIDACION.md','config.example.json','Prepare-PC.ps1',
    'Run-Diagnostics.ps1','Probe.ps1','Snapshot.ps1','Mark-Incident.ps1','Summarize.ps1',
    'Start-Capture.ps1','Analyze-Capture.ps1','Collect-SqlSnapshot.ps1','Observe-Server.ps1')
foreach ($file in $files) { Copy-Item -LiteralPath (Join-Path $PSScriptRoot $file) -Destination $Destination }
$context=Join-Path $Destination 'brain-context'
New-Item -ItemType Directory -Path $context | Out-Null
$refs=[ordered]@{
    'governance.reference.md'='AGENTS.md'
    'workspace.reference.json'='tools/brain-quality/workspace.json'
    'integration-services.reference.yml'='brain/agents/domain-integration-services.yml'
}
$revision= & git -C $brain rev-parse HEAD
$provenance=@()
foreach ($name in $refs.Keys) {
    $source=Join-Path $brain $refs[$name]
    if (-not (Test-Path -LiteralPath $source)) { throw "Missing Brain source: $($refs[$name])" }
    Copy-Item -LiteralPath $source -Destination (Join-Path $context $name)
    $dirty=[bool](& git -C $brain status --porcelain -- $refs[$name])
    $provenance+=@{packaged=$name;source=$refs[$name];repositoryHead=[string]$revision;workingTreeModified=$dirty;sha256=(Get-FileHash $source).Hash;validation='context-only; deployment not verified'}
}
$provenance | ConvertTo-Json -Depth 5 | Set-Content (Join-Path $context 'provenance.json') -Encoding UTF8
Get-ChildItem -LiteralPath $Destination -Recurse -File | Where-Object Name -ne 'PACKAGE-SHA256.csv' | ForEach-Object {
    [pscustomobject]@{file=$_.FullName.Substring($Destination.Length+1);sha256=(Get-FileHash $_.FullName).Hash}
} | Export-Csv (Join-Path $Destination 'PACKAGE-SHA256.csv') -NoTypeInformation -Encoding UTF8
Compress-Archive -Path (Join-Path $Destination '*') -DestinationPath ($Destination+'.zip')
Get-FileHash -LiteralPath ($Destination+'.zip') -Algorithm SHA256
