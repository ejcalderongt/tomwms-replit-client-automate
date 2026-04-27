# _QuestionCardParser.ps1 — parser de question cards Q-NNN con front-matter
# YAML. Usa el modulo 'powershell-yaml' (ConvertFrom-Yaml). Si no esta
# instalado, lanza error claro pidiendo instalarlo.

function Test-WmsBrainYamlModule {
    [CmdletBinding()] param()
    return [bool](Get-Command -Name 'ConvertFrom-Yaml' -ErrorAction SilentlyContinue)
}

function Assert-WmsBrainYamlModule {
    if (-not (Test-WmsBrainYamlModule)) {
        # Intentar import-on-demand
        try {
            Import-Module -Name 'powershell-yaml' -ErrorAction Stop
        } catch {
            throw "[8] Modulo 'powershell-yaml' no instalado. Install-Module powershell-yaml -Scope CurrentUser -Force"
        }
    }
}

function Read-WmsBrainYamlFrontMatter {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $Path
    )
    if (-not (Test-Path -LiteralPath $Path)) {
        throw "Card no existe: $Path"
    }
    $raw = Get-Content -LiteralPath $Path -Raw
    if ($raw -notmatch '^---\r?\n') {
        throw "Card sin front-matter YAML: $Path"
    }
    $rest = $raw -replace '^---\r?\n', ''
    $idx = $rest.IndexOf("`n---")
    if ($idx -lt 0) { $idx = $rest.IndexOf("`r`n---") }
    if ($idx -lt 0) {
        throw "Card con front-matter sin cierre '---': $Path"
    }
    $yaml = $rest.Substring(0, $idx)
    $body = $rest.Substring($idx) -replace '^\r?\n---\r?\n', ''
    return [PSCustomObject]@{
        Path = $Path
        Yaml = $yaml
        Body = $body
    }
}

function Read-WmsBrainQuestionCard {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $Path
    )
    Assert-WmsBrainYamlModule
    $fm = Read-WmsBrainYamlFrontMatter -Path $Path
    $parsed = ConvertFrom-Yaml -Yaml $fm.Yaml
    # Convertir hashtable a PSCustomObject de forma recursiva
    $obj = ConvertTo-WmsBrainCardObject -Obj $parsed
    Add-Member -InputObject $obj -NotePropertyName 'Path' -NotePropertyValue $Path -Force
    Add-Member -InputObject $obj -NotePropertyName 'Body' -NotePropertyValue $fm.Body -Force
    return $obj
}

function ConvertTo-WmsBrainCardObject {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [AllowNull()] [object] $Obj
    )
    if ($null -eq $Obj) { return $null }
    if ($Obj -is [System.Collections.IDictionary]) {
        $out = [ordered]@{}
        foreach ($k in $Obj.Keys) {
            $out[[string]$k] = ConvertTo-WmsBrainCardObject -Obj $Obj[$k]
        }
        return [PSCustomObject]$out
    }
    if ($Obj -is [System.Collections.IList] -and -not ($Obj -is [string])) {
        return @($Obj | ForEach-Object { ConvertTo-WmsBrainCardObject -Obj $_ })
    }
    return $Obj
}

# Resuelve el directorio de questions del repo wms-brain-client.
function Get-WmsBrainQuestionsDir {
    [CmdletBinding()]
    param(
        [string] $ClientRepo
    )
    if (-not $ClientRepo) { $ClientRepo = $env:WMS_BRAIN_CLIENT_REPO }
    if (-not $ClientRepo) {
        # Fallback: relativo al modulo
        $candidate = Join-Path (Split-Path -Parent $script:WmsBrainClientRoot) 'questions'
        if (Test-Path -LiteralPath $candidate) { return $candidate }
        throw "[3] `$env:WMS_BRAIN_CLIENT_REPO no esta seteado y no encuentro questions/ en el modulo."
    }
    $dir = Join-Path $ClientRepo 'questions'
    if (-not (Test-Path -LiteralPath $dir)) {
        throw "[2] No existe directorio de questions: $dir"
    }
    return $dir
}
