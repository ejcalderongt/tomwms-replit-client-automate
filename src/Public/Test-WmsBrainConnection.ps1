function Test-WmsBrainConnection {
<#
.SYNOPSIS
    Ping a un perfil SQL: reporta version, user, db y latencia.

.DESCRIPTION
    Ejecuta SELECT @@VERSION, USER_NAME(), DB_NAME() via Invoke-WmsBrainQuery
    y reporta latencia. Opcionalmente testea $env:BRAIN_BASE_URL/health.

.PARAMETER Profile
    Perfil. Default: $env:WMS_BRAIN_DEFAULT_PROFILE.

.PARAMETER IncludeBrainApi
    Tambien hace GET a $env:BRAIN_BASE_URL/health.

.EXAMPLE
    Test-WmsBrainConnection -Profile K7-PRD -IncludeBrainApi
#>
    [CmdletBinding()]
    param(
        [ValidateSet('K7-PRD', 'BB-PRD', 'C9-QAS', 'LOCAL_DEV')]
        [string] $Profile,
        [switch] $IncludeBrainApi
    )

    $p = Get-WmsBrainKnownProfile -Name $Profile
    Write-WmsBrainLog -Cmdlet 'Test-WmsBrainConnection' -Level 'INFO' `
        -Message "ping perfil=$($p.Name)"

    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    $r = $null
    $err = $null
    try {
        $r = Invoke-WmsBrainQuery -Profile $p.Name `
            -Query "SELECT @@VERSION AS Version, USER_NAME() AS UserName, DB_NAME() AS DbName" `
            -MaxRows 1 -QueryTimeout 15
    } catch {
        $err = $_.Exception.Message
    }
    $sw.Stop()

    $sqlOk = ($null -ne $r -and -not $err)
    $apiOk = $null
    $apiLatencyMs = $null
    if ($IncludeBrainApi) {
        $base = $env:BRAIN_BASE_URL
        if (-not $base) {
            $apiOk = $false
            Write-WmsBrainLog -Cmdlet 'Test-WmsBrainConnection' -Level 'WARN' `
                -Message '$env:BRAIN_BASE_URL no seteado'
        } else {
            $url = ($base.TrimEnd('/')) + '/health'
            $sw2 = [System.Diagnostics.Stopwatch]::StartNew()
            try {
                $h = @{}
                if ($env:BRAIN_IMPORT_TOKEN) { $h['Authorization'] = "Bearer $($env:BRAIN_IMPORT_TOKEN)" }
                Invoke-RestMethod -Uri $url -Headers $h -TimeoutSec 10 | Out-Null
                $apiOk = $true
            } catch {
                $apiOk = $false
                Write-WmsBrainLog -Cmdlet 'Test-WmsBrainConnection' -Level 'WARN' `
                    -Message "API health fallo: $($_.Exception.Message)"
            }
            $sw2.Stop()
            $apiLatencyMs = [int]$sw2.Elapsed.TotalMilliseconds
        }
    }

    $out = [PSCustomObject]@{
        Profile     = $p.Name
        Server      = $p.Server
        Database    = $p.Database
        SqlOk       = $sqlOk
        SqlLatencyMs = [int]$sw.Elapsed.TotalMilliseconds
        SqlVersion  = if ($r) { ($r | Select-Object -First 1).Version } else { $null }
        SqlUser     = if ($r) { ($r | Select-Object -First 1).UserName } else { $null }
        SqlDb       = if ($r) { ($r | Select-Object -First 1).DbName } else { $null }
        SqlError    = $err
        ApiOk       = $apiOk
        ApiLatencyMs = $apiLatencyMs
    }
    $level = if ($sqlOk) { 'OK' } else { 'ERROR' }
    Write-WmsBrainLog -Cmdlet 'Test-WmsBrainConnection' -Level $level `
        -Message "perfil=$($p.Name) sqlOk=$sqlOk latency=$($out.SqlLatencyMs)ms"
    return $out
}
