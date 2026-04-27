function Test-WmsBrainEnvironment {
<#
.SYNOPSIS
    Valida vars de entorno, paths de repos, scripts .mjs y modulo SqlServer.

.DESCRIPTION
    Imprime un reporte tabular tipo [OK]/[WARN]/[ERROR] con cada chequeo.
    Bajo -Strict, si hay algun ERROR sale con codigo 3.

    Chequeos:
    1. WMS_BRAIN_EXCHANGE_REPO_MAIN existe + branch=main
    2. WMS_BRAIN_EXCHANGE_REPO_BRAIN existe + branch=wms-brain
    3. WMS_BRAIN_CLIENT_REPO existe + branch=wms-brain-client
    4. brain_bridge.mjs / apply_bundle.mjs / hello_sync.mjs presentes
    5. node --version >= 20
    6. Get-Command Invoke-Sqlcmd
    7. Vars heredadas seteadas
    8. SCHEMA_VERSION del .mjs == esperado del cliente

.PARAMETER Strict
    Si hay algun chequeo ERROR, lanza terminating error (codigo logico 3).
    Se eligio throw en vez de exit para no matar la sesion del host del
    llamador desde adentro de un cmdlet de modulo.

.OUTPUTS
    Lista de PSCustomObject con Level (OK | WARN | ERROR), Section
    (nombre del chequeo) y Detail (mensaje libre). En modo `-Strict`,
    si algun chequeo es ERROR ademas lanza un terminating error con
    codigo logico 3.

.EXAMPLE
    Test-WmsBrainEnvironment

    Imprime el reporte y devuelve la lista de chequeos. No falla
    aunque haya ERRORs.

.EXAMPLE
    Test-WmsBrainEnvironment -Strict

    Igual, pero si hay algun chequeo en ERROR lanza terminating error
    (codigo 3). Para gating en pipelines / scripts.

.EXAMPLE
    Test-WmsBrainEnvironment | Where-Object Level -ne 'OK' | Format-Table -Auto

    Filtra solo lo que esta WARN o ERROR para foco rapido.

.EXAMPLE
    $report = Test-WmsBrainEnvironment
    $missing = @($report | Where-Object { $_.Level -eq 'ERROR' -and $_.Detail -like '*not set*' })
    Write-Host "Te faltan $($missing.Count) variables criticas."

    Programa logica condicional sobre los chequeos.

.LINK
    Show-WmsBrainQuickStart

.LINK
    Show-WmsBrainStatus

.NOTES
    Nunca imprime el contenido de WMS_KILLIOS_DB_PASSWORD ni de tokens.
    Para una guia interactiva con prompts para setear las vars
    faltantes, usar `Show-WmsBrainQuickStart -SetMissing`.
#>
    [CmdletBinding()]
    param(
        [switch] $Strict
    )

    $report = New-Object System.Collections.Generic.List[object]
    function _Add {
        param($Level, $Section, $Detail)
        $report.Add([PSCustomObject]@{ Level = $Level; Section = $Section; Detail = $Detail }) | Out-Null
    }

    # 1-3. Repos
    $repos = @(
        @{ Var = 'WMS_BRAIN_EXCHANGE_REPO_MAIN';  ExpectedBranch = 'main';             Label = 'ExchangeRepo MAIN' },
        @{ Var = 'WMS_BRAIN_EXCHANGE_REPO_BRAIN'; ExpectedBranch = 'wms-brain';        Label = 'ExchangeRepo BRAIN' },
        @{ Var = 'WMS_BRAIN_CLIENT_REPO';         ExpectedBranch = 'wms-brain-client'; Label = 'ClientRepo' }
    )
    foreach ($r in $repos) {
        $path = [Environment]::GetEnvironmentVariable($r.Var)
        if (-not $path) {
            _Add 'ERROR' $r.Label "var '$($r.Var)' no esta seteada"
            continue
        }
        if (-not (Test-Path -LiteralPath $path)) {
            _Add 'ERROR' $r.Label "path no existe: $path"
            continue
        }
        $b = Get-WmsBrainGitBranch -RepoPath $path
        if ($b -ne $r.ExpectedBranch) {
            # En -Strict, branch incorrecto = ERROR (gating operacional).
            # Sin -Strict queda como WARN para no bloquear inspeccion.
            $level = if ($Strict) { 'ERROR' } else { 'WARN' }
            _Add $level $r.Label "$path branch=$b (esperaba=$($r.ExpectedBranch))"
        } else {
            _Add 'OK'   $r.Label "$path branch=$b"
        }
    }

    # 4. .mjs
    $mainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
    if ($mainRepo -and (Test-Path -LiteralPath $mainRepo)) {
        $bridgeMjs = Join-Path $mainRepo 'scripts/brain_bridge.mjs'
        $applyMjs  = Join-Path $mainRepo 'scripts/apply_bundle.mjs'
        $helloMjs  = Join-Path $mainRepo 'scripts/hello_sync.mjs'
        if (Test-Path -LiteralPath $bridgeMjs) {
            try {
                $sv = Get-WmsBrainBridgeSchemaVersion -BrainBridgeMjsPath $bridgeMjs
                if ($sv -eq $script:WmsBrainClientExpectedSchemaVersion) {
                    _Add 'OK' 'brain_bridge.mjs' "SCHEMA_VERSION=$sv"
                } else {
                    _Add 'ERROR' 'brain_bridge.mjs' "SCHEMA_VERSION=$sv pero cliente espera $($script:WmsBrainClientExpectedSchemaVersion)"
                }
            } catch {
                _Add 'ERROR' 'brain_bridge.mjs' $_.Exception.Message
            }
        } else { _Add 'ERROR' 'brain_bridge.mjs' "no existe en $bridgeMjs" }

        if (Test-Path -LiteralPath $applyMjs)  { _Add 'OK' 'apply_bundle.mjs' '(con --brain-message)' }
        else                                   { _Add 'ERROR' 'apply_bundle.mjs' "no existe en $applyMjs" }

        if (Test-Path -LiteralPath $helloMjs)  { _Add 'OK' 'hello_sync.mjs' 'presente' }
        else                                   { _Add 'ERROR' 'hello_sync.mjs' "no existe en $helloMjs" }
    } else {
        _Add 'WARN' 'mjs scripts' 'no se pudo verificar (repo MAIN no resuelto)'
    }

    # 5. node version
    $nv = Get-WmsBrainNodeVersion
    if (-not $nv) {
        _Add 'ERROR' 'node' "no encontrado en PATH"
    } else {
        $major = [int](($nv -split '\.')[0])
        if ($major -lt 20) {
            _Add 'ERROR' 'node' "v$nv (se requiere >= 20)"
        } else {
            _Add 'OK' 'node' "v$nv"
        }
    }

    # 6. Invoke-Sqlcmd
    $sqlcmd = Get-Command -Name 'Invoke-Sqlcmd' -ErrorAction SilentlyContinue
    if (-not $sqlcmd) {
        _Add 'ERROR' 'Invoke-Sqlcmd' "modulo SqlServer no instalado. Install-Module SqlServer -Scope CurrentUser"
    } else {
        $modVer = $sqlcmd.Module.Version
        _Add 'OK' 'Invoke-Sqlcmd' "modulo $($sqlcmd.Module.Name) $modVer"
    }

    # 7. Vars heredadas
    $required = @(
        @{ Var = 'WMS_KILLIOS_DB_HOST';     Level = 'WARN' },
        @{ Var = 'WMS_KILLIOS_DB_USER';     Level = 'WARN' },
        @{ Var = 'WMS_KILLIOS_DB_PASSWORD'; Level = 'ERROR' },
        @{ Var = 'BRAIN_BASE_URL';          Level = 'WARN' },
        @{ Var = 'BRAIN_IMPORT_TOKEN';      Level = 'WARN' },
        @{ Var = 'AZURE_DEVOPS_PAT';        Level = 'WARN' }
    )
    foreach ($v in $required) {
        $val = [Environment]::GetEnvironmentVariable($v.Var)
        if ([string]::IsNullOrEmpty($val)) {
            _Add $v.Level "`$env:$($v.Var)" "(not set)"
        } else {
            _Add 'OK' "`$env:$($v.Var)" "(set, no-print)"
        }
    }

    # Imprimir reporte
    foreach ($row in $report) {
        $tag = "[{0,-5}]" -f $row.Level
        Write-WmsBrainBanner -Lines @(("{0} {1,-22} {2}" -f $tag, $row.Section, $row.Detail))
    }

    if ($Strict) {
        $errs = @($report | Where-Object { $_.Level -eq 'ERROR' })
        if ($errs.Count -gt 0) {
            $msg = "[3] $($errs.Count) chequeos en ERROR (modo Strict): " + (($errs | ForEach-Object { "$($_.Section)=$($_.Detail)" }) -join '; ')
            Write-WmsBrainLog -Cmdlet 'Test-WmsBrainEnvironment' -Level 'ERROR' -Message $msg
            $err = New-Object System.Management.Automation.ErrorRecord (
                (New-Object System.InvalidOperationException $msg),
                'WmsBrainClient.Environment.StrictFailed',
                [System.Management.Automation.ErrorCategory]::InvalidOperation,
                $errs
            )
            throw $err
        }
    }

    return $report
}
