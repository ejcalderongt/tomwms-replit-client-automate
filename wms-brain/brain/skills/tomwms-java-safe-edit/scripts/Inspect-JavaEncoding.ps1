param(
    [Parameter(Mandatory = $true)]
    [string]$Path
)

$ErrorActionPreference = "Stop"

$resolved = Resolve-Path -LiteralPath $Path
$bytes = [System.IO.File]::ReadAllBytes($resolved)

$bom = "none"
if ($bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF) {
    $bom = "utf-8-bom"
} elseif ($bytes.Length -ge 2 -and $bytes[0] -eq 0xFF -and $bytes[1] -eq 0xFE) {
    $bom = "utf-16-le"
} elseif ($bytes.Length -ge 2 -and $bytes[0] -eq 0xFE -and $bytes[1] -eq 0xFF) {
    $bom = "utf-16-be"
}

$text = [System.Text.Encoding]::UTF8.GetString($bytes)
$crlf = ([regex]::Matches($text, "`r`n")).Count
$lfOnly = ([regex]::Matches($text, "(?<!`r)`n")).Count
$crOnly = ([regex]::Matches($text, "`r(?!`n)")).Count
$mojibakeHits = ([regex]::Matches($text, "Ã.|Â.|ï¿½")).Count
$nonAscii = ([regex]::Matches($text, "[^\u0000-\u007F]")).Count

[pscustomobject]@{
    Path = $resolved.Path
    Bytes = $bytes.Length
    Bom = $bom
    CRLF = $crlf
    LFOnly = $lfOnly
    CROnly = $crOnly
    NonAsciiChars = $nonAscii
    MojibakeLikeHits = $mojibakeHits
} | Format-List

if ($bom -eq "utf-8-bom") {
    Write-Warning "UTF-8 BOM detected. Preserve unless the user asks to normalize."
}
if ($mojibakeHits -gt 0) {
    Write-Warning "Mojibake-like sequences detected. Prefer ASCII patch anchors and avoid broad rewrites."
}
