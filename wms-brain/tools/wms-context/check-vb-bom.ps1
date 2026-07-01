param(
    [switch]$Fix,
    [switch]$All
)

$ErrorActionPreference = 'Stop'

function Test-Utf8Bom {
    param([string]$Path)

    $bytes = [System.IO.File]::ReadAllBytes($Path)
    return $bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF
}

function Write-Utf8BomFile {
    param([string]$Path)

    $text = [System.IO.File]::ReadAllText($Path)
    $utf8Bom = [System.Text.UTF8Encoding]::new($true)
    [System.IO.File]::WriteAllText($Path, $text, $utf8Bom)
}

$repoRoot = Split-Path -Parent $PSScriptRoot

if ($All) {
    $vbFiles = Get-ChildItem -Path $repoRoot -Recurse -File -Filter '*.vb' |
        Where-Object {
            $_.FullName -notmatch '\\(bin|obj|packages|\.vs)\\' -and
            $_.FullName -notmatch '\\TOMHH2025\\'
        }
} else {
    $changed = & git -C $repoRoot diff --name-only --diff-filter=ACMRT -- '*.vb'
    $untracked = & git -C $repoRoot ls-files --others --exclude-standard -- '*.vb'
    $vbFiles = @($changed + $untracked) | Where-Object { -not [string]::IsNullOrWhiteSpace($_) } | ForEach-Object {
        $fullPath = Join-Path $repoRoot $_
        if (Test-Path $fullPath) {
            $fullPath
        }
    }
}

$missingBom = New-Object System.Collections.Generic.List[string]

foreach ($file in $vbFiles) {
    if ([string]::IsNullOrWhiteSpace($file)) {
        continue
    }
    if (-not (Test-Utf8Bom -Path $file)) {
        $missingBom.Add($file)
        if ($Fix) {
            Write-Utf8BomFile -Path $file
        }
    }
}

if ($missingBom.Count -eq 0) {
    if ($All) {
        Write-Host "OK: todos los archivos VB revisados tienen UTF-8 con BOM."
    } else {
        Write-Host "OK: todos los archivos VB modificados/relevantes tienen UTF-8 con BOM."
    }
    exit 0
}

if ($Fix) {
    Write-Host "Corregidos $($missingBom.Count) archivos VB sin BOM."
    exit 0
}

Write-Host "Archivos VB sin BOM detectados:"
$missingBom | ForEach-Object { Write-Host $_ }
exit 1
