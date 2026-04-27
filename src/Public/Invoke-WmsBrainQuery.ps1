function Invoke-WmsBrainQuery {
<#
.SYNOPSIS
    Corre una query SELECT-only contra un perfil. Refusa DML/DDL.

.DESCRIPTION
    Wrappea Invoke-Sqlcmd con safety:
    1. Pre-ejecucion: regex anti-DML (INSERT/UPDATE/DELETE/MERGE/TRUNCATE/
       DROP/ALTER/CREATE/GRANT/REVOKE/EXEC/EXECUTE/sp_/xp_) fuera de
       comentarios y string literals. Si pega -> exit 7.
    2. Ejecuta con MaxRows enforced.
    3. Opcional: -AsCsv exporta a CSV UTF-8 sin BOM.

.PARAMETER Profile
    Perfil de conexion. Default: WMS_BRAIN_DEFAULT_PROFILE.

.PARAMETER Query
    SQL crudo (solo SELECT/CTE/etc).

.PARAMETER MaxRows
    Default: 10000.

.PARAMETER QueryTimeout
    Segundos. Default: 60.

.PARAMETER AsCsv
    Path de salida CSV.

.EXAMPLE
    Invoke-WmsBrainQuery -Profile K7-PRD -Query "SELECT TOP 10 * FROM dbo.ParametrosSAP"

.NOTES
    Las 3 BDs (K7-PRD/BB-PRD/C9-QAS) son productivas. Cualquier intento de
    DML/DDL aborta antes de mandarse al server.
#>
    [CmdletBinding()]
    param(
        [ValidateSet('K7-PRD', 'BB-PRD', 'C9-QAS', 'LOCAL_DEV')]
        [string] $Profile,

        [Parameter(Mandatory)]
        [string] $Query,

        [int] $MaxRows = 10000,
        [int] $QueryTimeout = 60,
        [string] $AsCsv
    )

    Assert-WmsBrainSqlIsReadOnly -Sql $Query -Cmdlet 'Invoke-WmsBrainQuery'

    $p = Get-WmsBrainKnownProfile -Name $Profile
    if (-not (Get-Command -Name 'Invoke-Sqlcmd' -ErrorAction SilentlyContinue)) {
        throw "[8] Modulo SqlServer no instalado. Install-Module SqlServer -Scope CurrentUser -Force"
    }

    $params = Resolve-WmsBrainSqlcmdParams -Profile $p -Query $Query -QueryTimeout $QueryTimeout -MaxRows $MaxRows

    Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainQuery' -Level 'INFO' `
        -Message "perfil=$($p.Name) timeout=${QueryTimeout}s maxRows=$MaxRows"

    try {
        $sw = [System.Diagnostics.Stopwatch]::StartNew()
        $rows = & 'Invoke-Sqlcmd' @params
        $sw.Stop()
        $count = if ($rows -is [System.Array]) { $rows.Count } elseif ($null -ne $rows) { 1 } else { 0 }
        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainQuery' -Level 'OK' `
            -Message "$count fila(s) en $($sw.Elapsed.TotalMilliseconds.ToString('0'))ms"

        if ($MaxRows -gt 0 -and $count -gt $MaxRows) {
            $rows = $rows | Select-Object -First $MaxRows
            Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainQuery' -Level 'WARN' `
                -Message "truncado a MaxRows=$MaxRows"
        }

        if ($AsCsv) {
            $dir = Split-Path -Parent $AsCsv
            if ($dir -and -not (Test-Path -LiteralPath $dir)) {
                New-Item -ItemType Directory -Path $dir -Force | Out-Null
            }
            $rows | Export-Csv -LiteralPath $AsCsv -NoTypeInformation -Encoding UTF8
            Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainQuery' -Level 'OK' `
                -Message "exportado a $AsCsv"
        }

        return $rows
    } catch {
        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainQuery' -Level 'ERROR' `
            -Message (ConvertTo-WmsBrainSafeString $_.Exception.Message)
        throw "[6] Error de SQL en perfil $($p.Name): $($_.Exception.Message)"
    }
}
