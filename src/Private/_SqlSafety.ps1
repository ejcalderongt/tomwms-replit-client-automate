# _SqlSafety.ps1 — safety SELECT-only para queries contra K7-PRD/BB-PRD/C9-QAS.
#
# Las 3 BDs son productivas. Cualquier intento de DML/DDL desde este cliente
# debe abortar antes de mandarse al server. Exit code 7 (DML detectado).

$script:WmsBrainDmlKeywords = @(
    'INSERT', 'UPDATE', 'DELETE', 'MERGE', 'TRUNCATE',
    'DROP', 'ALTER', 'CREATE', 'GRANT', 'REVOKE',
    'EXEC', 'EXECUTE', 'sp_', 'xp_'
)

# Quita comentarios SQL ('--' a fin de linea, '/*..*/') y string literals
# para que la deteccion no pegue dentro de un comentario o un literal.
function ConvertTo-WmsBrainCleanSql {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory, ValueFromPipeline)]
        [AllowEmptyString()]
        [string] $Sql
    )
    process {
        $s = $Sql
        # block comments /* ... */
        $s = [regex]::Replace($s, '(?s)/\*.*?\*/', ' ')
        # line comments -- ...
        $s = [regex]::Replace($s, '(?m)--[^\r\n]*', ' ')
        # string literals 'foo' (incluyendo escapes '' que SQL Server usa)
        $s = [regex]::Replace($s, "'(?:''|[^'])*'", "'STR'")
        # bracketed identifiers [Foo]
        $s = [regex]::Replace($s, '\[[^\]]*\]', '[ID]')
        return $s
    }
}

# Devuelve $null si la query es SELECT-only, o un string con la palabra
# prohibida detectada.
function Test-WmsBrainSqlIsReadOnly {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $Sql
    )
    if (-not $Sql -or -not $Sql.Trim()) {
        return 'query vacia'
    }
    $clean = ConvertTo-WmsBrainCleanSql -Sql $Sql
    foreach ($kw in $script:WmsBrainDmlKeywords) {
        # word-boundary, salvo sp_/xp_ que son prefijo
        if ($kw -match '_$') {
            $pattern = '\b' + [regex]::Escape($kw)
        } else {
            $pattern = '\b' + [regex]::Escape($kw) + '\b'
        }
        if ([regex]::IsMatch($clean, $pattern, 'IgnoreCase')) {
            return $kw
        }
    }
    return $null
}

function Assert-WmsBrainSqlIsReadOnly {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $Sql,
        [string] $Cmdlet = 'Assert-WmsBrainSqlIsReadOnly'
    )
    $hit = Test-WmsBrainSqlIsReadOnly -Sql $Sql
    if ($hit) {
        $msg = "[7] DML/DDL detectado en query (palabra '$hit'). Las BDs PRD/QAS son READ-ONLY desde WmsBrainClient."
        Write-WmsBrainLog -Cmdlet $Cmdlet -Level 'ERROR' -Message $msg
        $err = New-Object System.Management.Automation.ErrorRecord (
            (New-Object System.InvalidOperationException $msg),
            'WmsBrainClient.Sql.DmlBlocked',
            [System.Management.Automation.ErrorCategory]::PermissionDenied,
            $Sql
        )
        $err.CategoryInfo.TargetName = $hit
        throw $err
    }
}
