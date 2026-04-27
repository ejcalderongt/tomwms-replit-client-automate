function Get-WmsBrainSuiteList {
<#
.SYNOPSIS
    Lista suites y scenarios disponibles del repo en rama wms-brain.

.PARAMETER BrainRepo
    Default: $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN.

.EXAMPLE
    Get-WmsBrainSuiteList | Format-Table

.NOTES
    Recorre <repo>/suites/*/README.md y <repo>/scenarios/*/README.md y cuenta
    los .sql del directorio para reportar 'Steps'.
#>
    [CmdletBinding()]
    param(
        [string] $BrainRepo
    )
    if (-not $BrainRepo) { $BrainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN }
    if (-not $BrainRepo) {
        throw "[3] -BrainRepo no provisto y `$env:WMS_BRAIN_EXCHANGE_REPO_BRAIN no esta seteado."
    }
    $rows = New-Object System.Collections.Generic.List[object]
    foreach ($section in @('suites', 'scenarios')) {
        $base = Join-Path $BrainRepo $section
        if (-not (Test-Path -LiteralPath $base)) { continue }
        Get-ChildItem -Path $base -Directory -ErrorAction SilentlyContinue | ForEach-Object {
            $readme = Join-Path $_.FullName 'README.md'
            $desc = ''
            if (Test-Path -LiteralPath $readme) {
                $first = Get-Content -LiteralPath $readme -TotalCount 30
                # Heuristica: tomar la primera linea no-vacia que no sea '#'
                foreach ($l in $first) {
                    $ll = $l.Trim()
                    if ($ll -and -not $ll.StartsWith('#')) {
                        $desc = $ll
                        break
                    }
                }
            }
            $sqlCount = (Get-ChildItem -Path $_.FullName -Filter '*.sql' -File -ErrorAction SilentlyContinue).Count
            $rows.Add([PSCustomObject]@{
                Type        = $section.TrimEnd('s')   # suite / scenario
                Name        = $_.Name
                Steps       = if ($section -eq 'suites') { $sqlCount } else { '-' }
                Description = $desc
                Path        = $_.FullName
            }) | Out-Null
        }
    }
    return $rows
}
