function Get-WmsBrainQuestion {
<#
.SYNOPSIS
    Lista question cards del repo wms-brain-client (filtrable por status, codename, prioridad, tag).

.DESCRIPTION
    Recorre questions/*.md y parsea YAML front-matter. Devuelve PSCustomObject
    pipeable.

.PARAMETER Status
    pending | in-progress | answered | closed | all. Default: pending.

.PARAMETER Codename
    Filtra por target codename (K7|BB|C9|...).

.PARAMETER Priority
    low | medium | high | critical.

.PARAMETER Tag
    Uno o varios tags; matchea si la card tiene TODOS los tags pasados.

.PARAMETER ClientRepo
    Path al clon de wms-brain-client. Default: $env:WMS_BRAIN_CLIENT_REPO.

.EXAMPLE
    Get-WmsBrainQuestion -Status pending -Codename BB
#>
    [CmdletBinding()]
    param(
        [ValidateSet('pending', 'in-progress', 'answered', 'closed', 'all')]
        [string] $Status = 'pending',
        [string] $Codename,
        [ValidateSet('low', 'medium', 'high', 'critical')]
        [string] $Priority,
        [string[]] $Tag,
        [string] $ClientRepo
    )
    $dir = Get-WmsBrainQuestionsDir -ClientRepo $ClientRepo
    $files = Get-ChildItem -Path $dir -Filter 'Q-*.md' -File -ErrorAction SilentlyContinue

    $rows = New-Object System.Collections.Generic.List[object]
    foreach ($f in $files) {
        try {
            $card = Read-WmsBrainQuestionCard -Path $f.FullName
        } catch {
            Write-WmsBrainLog -Cmdlet 'Get-WmsBrainQuestion' -Level 'WARN' `
                -Message "no parsea $($f.Name): $($_.Exception.Message)"
            continue
        }
        $rowStatus = if ($card.PSObject.Properties['status']) { [string]$card.status } else { 'pending' }
        if ($Status -ne 'all' -and $rowStatus -ne $Status) { continue }
        if ($Priority -and ($card.PSObject.Properties['priority']) -and ($card.priority -ne $Priority)) { continue }
        if ($Codename) {
            $codes = @()
            if ($card.PSObject.Properties['targets']) {
                foreach ($t in @($card.targets)) {
                    if ($t -and $t.PSObject.Properties['codename']) { $codes += [string]$t.codename }
                }
            }
            if ($codes -notcontains $Codename) { continue }
        }
        if ($Tag) {
            $cardTags = @()
            if ($card.PSObject.Properties['tags']) { $cardTags = @($card.tags) }
            $allMatch = $true
            foreach ($t in $Tag) { if ($cardTags -notcontains $t) { $allMatch = $false; break } }
            if (-not $allMatch) { continue }
        }
        $rows.Add([PSCustomObject]@{
            Id        = if ($card.PSObject.Properties['id']) { $card.id } else { $f.BaseName }
            Title     = if ($card.PSObject.Properties['title']) { $card.title } else { '' }
            Priority  = if ($card.PSObject.Properties['priority']) { $card.priority } else { '' }
            Status    = $rowStatus
            Codename  = (@($card.targets) | ForEach-Object {
                            if ($_ -and $_.PSObject.Properties['codename']) { [string]$_.codename }
                        }) -join ','
            Tags      = if ($card.PSObject.Properties['tags']) { (@($card.tags) -join ',') } else { '' }
            Path      = $f.FullName
            Card      = $card
        }) | Out-Null
    }
    return ($rows | Sort-Object -Property Id)
}
