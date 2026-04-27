function Show-WmsBrainQuestion {
<#
.SYNOPSIS
    Muestra el detalle de una question card por id.

.PARAMETER Id
    Q-NNN.

.PARAMETER NoQueries
    No imprime las suggestedQueries.

.PARAMETER Markdown
    Imprime el body markdown completo (no solo el YAML).

.PARAMETER ClientRepo
    Default: $env:WMS_BRAIN_CLIENT_REPO.

.EXAMPLE
    Show-WmsBrainQuestion -Id Q-001
#>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $Id,
        [switch] $NoQueries,
        [switch] $Markdown,
        [string] $ClientRepo
    )
    $dir = Get-WmsBrainQuestionsDir -ClientRepo $ClientRepo
    $f = Get-ChildItem -Path $dir -Filter "$Id-*.md" -File -ErrorAction SilentlyContinue | Select-Object -First 1
    if (-not $f) {
        $f = Get-ChildItem -Path $dir -Filter "$Id.md" -File -ErrorAction SilentlyContinue | Select-Object -First 1
    }
    if (-not $f) {
        throw "[2] No encuentro card para id '$Id' en $dir"
    }
    $card = Read-WmsBrainQuestionCard -Path $f.FullName

    Write-WmsBrainBanner -Lines @(
        ("Id        : {0}" -f $card.id),
        ("Title     : {0}" -f $card.title),
        ("Status    : {0}" -f $card.status),
        ("Priority  : {0}" -f $card.priority),
        ("CreatedBy : {0}" -f $card.createdBy),
        ("CreatedAt : {0}" -f $card.createdAt),
        ("Tags      : {0}" -f ((@($card.tags) -join ', '))),
        ("Path      : {0}" -f $f.FullName),
        ('')
    )
    if ($card.PSObject.Properties['targets']) {
        Write-WmsBrainBanner -Lines @('Targets:')
        foreach ($t in @($card.targets)) {
            Write-WmsBrainBanner -Lines @("  - $($t.codename)/$($t.environment)  minRows=$($t.minRows)")
        }
        Write-WmsBrainBanner -Lines @('')
    }

    if (-not $NoQueries -and $card.PSObject.Properties['suggestedQueries']) {
        Write-WmsBrainBanner -Lines @('SuggestedQueries:')
        foreach ($q in @($card.suggestedQueries)) {
            Write-WmsBrainBanner -Lines @(
                ("  - {0}: {1}" -f $q.id, $q.description),
                '    -- SQL --',
                ($q.sql -split "`r?`n" | ForEach-Object { '    ' + $_ })
            )
        }
        Write-WmsBrainBanner -Lines @('')
    }

    if ($Markdown -and $card.Body) {
        Write-WmsBrainBanner -Lines @('--- Markdown body ---', $card.Body)
    }
    return $card
}
