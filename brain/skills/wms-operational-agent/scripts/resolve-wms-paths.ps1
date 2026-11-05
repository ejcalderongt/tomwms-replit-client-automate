# #EJC20261027_BRAIN_PATHS
param([string]$BrainRoot = '', [string]$RepoRoot = '', [string]$HhRoot = '', [switch]$AsJson)

function Resolve-WmsPaths {
    param([string]$BrainRoot = '', [string]$RepoRoot = '', [string]$HhRoot = '')
    $candidates = [System.Collections.Generic.List[string]]::new()
    if ($BrainRoot) { $candidates.Add($BrainRoot) }
    elseif ($env:WMS_BRAIN_ROOT) { $candidates.Add($env:WMS_BRAIN_ROOT) }
    else {
        $ancestor = $PSScriptRoot
        while ($ancestor) {
            $candidates.Add($ancestor)
            $next = Split-Path $ancestor -Parent
            if ($next -eq $ancestor) { break }
            $ancestor = $next
        }
        $locator = Join-Path $env:LOCALAPPDATA 'PrograX24/brain-location.json'
        if (Test-Path -LiteralPath $locator) {
            $saved = Get-Content -LiteralPath $locator -Raw | ConvertFrom-Json
            if ($saved.BrainRoot) { $candidates.Add([string]$saved.BrainRoot) }
        }
        $candidates.Add((Join-Path $env:USERPROFILE 'OneDrive/Documents/ChatGPT/TOMWMS/.federation/wms-brain'))
        $candidates.Add((Join-Path $env:USERPROFILE 'source/repos/wms-brain'))
    }
    $resolvedBrain = $null
    foreach ($candidate in $candidates) {
        if (Test-Path -LiteralPath (Join-Path $candidate 'tools/brain-quality/workspace.json')) {
            $resolvedBrain = (Resolve-Path -LiteralPath $candidate).Path
            break
        }
    }
    if (-not $resolvedBrain) { throw 'Current Brain manifest not found. Supply -BrainRoot or WMS_BRAIN_ROOT.' }
    $sourceMap = Get-Content -LiteralPath (Join-Path $resolvedBrain 'tools/brain-quality/source-roots.json') -Raw | ConvertFrom-Json
    if (-not $RepoRoot) { $RepoRoot = ([string]$sourceMap.roots.bof).Replace('${USERPROFILE}', $env:USERPROFILE) }
    if (-not $HhRoot) { $HhRoot = ([string]$sourceMap.roots.hh).Replace('${USERPROFILE}', $env:USERPROFILE) }
    foreach ($required in @($RepoRoot, $HhRoot)) {
        if (-not (Test-Path -LiteralPath $required -PathType Container)) { throw "Configured repository not found: $required" }
    }
    [pscustomobject]@{
        BrainRoot = $resolvedBrain
        RepoRoot = (Resolve-Path -LiteralPath $RepoRoot).Path
        HhRoot = (Resolve-Path -LiteralPath $HhRoot).Path
        Manifest = Join-Path $resolvedBrain 'tools/brain-quality/workspace.json'
    }
}

if ($MyInvocation.InvocationName -ne '.') {
    $result = Resolve-WmsPaths -BrainRoot $BrainRoot -RepoRoot $RepoRoot -HhRoot $HhRoot
    if ($AsJson) { $result | ConvertTo-Json } else { $result }
}
