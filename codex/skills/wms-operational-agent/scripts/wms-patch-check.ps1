param(
    [string]$RepoRoot = "C:/Users/yejc2/source/repos/TOMWMS",
    [string]$HhRoot = "C:/Users/yejc2/StudioProjects/TOMHH2025",
    [string]$BrainRoot = "C:/Users/yejc2/source/repos/wms-brain/wms-brain",
    [string]$Tag = ("#EJC" + (Get-Date -Format "yyyyMMdd"))
)

$ErrorActionPreference = "Stop"

function Get-ChangedFiles($root) {
    if (-not (Test-Path $root)) { return @() }
    @(git -C $root status --short | ForEach-Object {
        $line = $_
        if ($line.Length -ge 4) { $line.Substring(3).Trim() }
    } | Where-Object { $_ })
}

function Write-Check($ok, $message) {
    if ($ok) {
        Write-Output "[OK] $message"
    } else {
        Write-Output "[WARN] $message"
    }
}

$bofFiles = Get-ChangedFiles $RepoRoot
$hhFiles = Get-ChangedFiles $HhRoot
$brainFiles = Get-ChangedFiles $BrainRoot
$skillFiles = @($bofFiles | Where-Object { $_ -like "codex/*" -or $_ -eq "codex/" })
$bofCodeFiles = @($bofFiles | Where-Object { -not ($_ -like "codex/*" -or $_ -eq "codex/") })

Write-Output "== WMS Patch Check =="
Write-Output "Tag expected for durable comments/notes: $Tag"
Write-Output ""

$touchedHh = $hhFiles.Count -gt 0
$touchedBof = $bofCodeFiles.Count -gt 0
$touchedSkill = $skillFiles.Count -gt 0
$touchedBrain = $brainFiles.Count -gt 0

Write-Output "Changed BOF/TOMWMS files:"
$bofCodeFiles | ForEach-Object { Write-Output "  $_" }
Write-Output "Changed WMS skill/docs files:"
$skillFiles | ForEach-Object { Write-Output "  $_" }
Write-Output "Changed HH files:"
$hhFiles | ForEach-Object { Write-Output "  $_" }
Write-Output "Changed Brain files:"
$brainFiles | ForEach-Object { Write-Output "  $_" }
Write-Output ""

Write-Check (-not ($touchedHh -and $touchedBof)) "HH and BOF are not mixed in the same working patch"
Write-Check (-not ($bofFiles | Where-Object { $_ -match "Reference\.vb$" })) "Reference.vb not touched"
Write-Check ($touchedBrain) "Brain/context trace updated when process knowledge changed"

$tagHits = @()
if ($touchedBof -or $touchedSkill) {
    $tagHits += git -C $RepoRoot diff -- . | Select-String -SimpleMatch $Tag
}
if ($touchedHh) {
    $tagHits += git -C $HhRoot diff -- . | Select-String -SimpleMatch $Tag
}
if ($touchedBrain) {
    $tagHits += git -C $BrainRoot diff -- . | Select-String -SimpleMatch $Tag
}
Write-Check ($tagHits.Count -gt 0) "Current EJC tag appears in diffs when durable rule/comments were added"

Write-Output ""
Write-Output "Recommended verification:"
if ($touchedBof) {
    Write-Output '  BOF/WS build: & "C:/Program Files/Microsoft Visual Studio/18/Community/MSBuild/Current/Bin/MSBuild.exe" WSHHRN/WSHHRN.vbproj /t:Build /p:Configuration=Debug /p:Platform="AnyCPU" /v:minimal'
}
if ($touchedHh) {
    Write-Output '  HH build: cd C:/Users/yejc2/StudioProjects/TOMHH2025; ./gradlew.bat :app:compileDebugJavaWithJavac'
}
Write-Output "  Attempt Brain reindex if BRAIN_IMPORT_TOKEN is available."
