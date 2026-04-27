function Show-WmsBrainQuickStart {
<#
.SYNOPSIS
    Guia interactiva de inicio rapido. Muestra estado del modulo,
    variables de entorno relevantes y proximos pasos sugeridos.

.DESCRIPTION
    Imprime un dashboard one-shot con cinco secciones:

      1. HEADER         — version del modulo, schema esperado vs
                          detectado en brain_bridge.mjs.
      2. PRE-FLIGHT     — chequeos rapidos de pre-requisitos (Pester,
                          node, Invoke-Sqlcmd, deps pinneadas) usando
                          Test-WmsBrainEnvironment como motor.
      3. VARIABLES      — tabla con las variables de entorno relevantes
                          y su estado actual: OK / MISSING / SET (no-print
                          para secretos).
      4. PROXIMOS PASOS — lista ordenada de comandos a correr segun lo
                          que falte (setea variables -> Test-WmsBrainEnvironment
                          -> Show-WmsBrainStatus -> Test-WmsBrainConnection -> ...).
      5. EJEMPLOS       — 4 comandos copy-paste para empezar a usar el
                          modulo (Get-WmsBrainQuestion, New-WmsBrainQuestionEvent,
                          Submit-WmsBrainAnswer, Invoke-WmsBrainQuery).

    Por default es read-only y no modifica nada. Con -SetMissing pide
    via Read-Host las variables criticas que falten y las setea para la
    sesion actual. Con -Persist (combinado con -SetMissing) tambien las
    persiste en el scope User; las variables marcadas como secreto
    (DB password, tokens) NUNCA se persisten en disco aunque se pase
    -Persist, solo quedan en la sesion.

.PARAMETER SetMissing
    Pide via Read-Host las variables criticas faltantes y las setea
    para la sesion actual (Set-Item Env:VAR). Util para configurar el
    modulo la primera vez sin tener que setear vars a mano.

.PARAMETER Persist
    Junto con -SetMissing, ademas persiste cada var seteada en el
    scope User ([Environment]::SetEnvironmentVariable(name,value,'User')).
    Variables marcadas como secreto se ignoran para -Persist por
    seguridad y quedan solo para la sesion.

.PARAMETER Compact
    Imprime solo HEADER y PROXIMOS PASOS, omite PRE-FLIGHT, VARIABLES y
    EJEMPLOS. Util cuando ya conoces el modulo y solo queres un nudge.

.OUTPUTS
    PSCustomObject con ModuleVersion, ExpectedSchemaVersion,
    DetectedSchemaVersion, EnvSummary (hashtable nombre->estado) y
    NextSteps (array de strings sugeridos).

.EXAMPLE
    Show-WmsBrainQuickStart

    Imprime el dashboard completo en read-only.

.EXAMPLE
    Show-WmsBrainQuickStart -SetMissing

    Imprime el dashboard y, al final, abre prompts para cada variable
    critica faltante. Las setea solo para la sesion actual.

.EXAMPLE
    Show-WmsBrainQuickStart -SetMissing -Persist

    Igual que arriba, pero ademas persiste en el scope User las vars
    no-secretas (los secretos solo quedan en la sesion).

.EXAMPLE
    Show-WmsBrainQuickStart -Compact

    Solo header + proximos pasos sugeridos.

.LINK
    Test-WmsBrainEnvironment

.LINK
    Show-WmsBrainStatus

.NOTES
    Nunca imprime el contenido de variables marcadas como secreto.
    Solo reporta "set, no-print" o "MISSING".
#>
    [CmdletBinding()]
    param(
        [switch] $SetMissing,
        [switch] $Persist,
        [switch] $Compact
    )

    # ----- 0. Catalogo de variables relevantes -----
    # Critical = se prompteara con -SetMissing.
    # Secret   = nunca se imprime su valor ni se persiste con -Persist.
    $envCatalog = @(
        [PSCustomObject]@{ Name = 'WMS_BRAIN_EXCHANGE_REPO_MAIN';  Critical = $true;  Secret = $false; Hint = 'Path local al repo `main` (con scripts/brain_bridge.mjs).' }
        [PSCustomObject]@{ Name = 'WMS_BRAIN_EXCHANGE_REPO_BRAIN'; Critical = $true;  Secret = $false; Hint = 'Path local al repo `wms-brain` (cards y bundles).' }
        [PSCustomObject]@{ Name = 'WMS_BRAIN_CLIENT_REPO';         Critical = $true;  Secret = $false; Hint = 'Path local al repo `wms-brain-client` (questions/answers).' }
        [PSCustomObject]@{ Name = 'WMS_BRAIN_AUTHOR_INIT';         Critical = $false; Secret = $false; Hint = 'Iniciales del autor (e.g. EJC). Default: EJC.' }
        [PSCustomObject]@{ Name = 'WMS_BRAIN_DEFAULT_PROFILE';     Critical = $false; Secret = $false; Hint = 'Perfil de BD default (K7-PRD | BB-PRD | C9-QAS).' }
        [PSCustomObject]@{ Name = 'WMS_KILLIOS_DB_HOST';           Critical = $true;  Secret = $false; Hint = 'Host SQL Server. Por convencion 52.41.114.122,1437.' }
        [PSCustomObject]@{ Name = 'WMS_KILLIOS_DB_USER';           Critical = $true;  Secret = $false; Hint = 'Usuario read-only de las 3 BDs.' }
        [PSCustomObject]@{ Name = 'WMS_KILLIOS_DB_PASSWORD';       Critical = $true;  Secret = $true;  Hint = 'Password del usuario read-only. NO se persiste con -Persist.' }
        [PSCustomObject]@{ Name = 'BRAIN_BASE_URL';                Critical = $false; Secret = $false; Hint = 'URL del API del brain (si esta en uso).' }
        [PSCustomObject]@{ Name = 'BRAIN_IMPORT_TOKEN';            Critical = $false; Secret = $true;  Hint = 'Token bearer del API. NO se persiste con -Persist.' }
    )

    # ----- 1. HEADER -----
    $bridgeSv = '?'
    $mainRepo = $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
    if ($mainRepo -and (Test-Path -LiteralPath $mainRepo)) {
        $bridgeMjs = Join-Path $mainRepo 'scripts/brain_bridge.mjs'
        if (Test-Path -LiteralPath $bridgeMjs) {
            try { $bridgeSv = Get-WmsBrainBridgeSchemaVersion -BrainBridgeMjsPath $bridgeMjs }
            catch { $bridgeSv = "ERROR: $($_.Exception.Message)" }
        }
    }
    $svMatch = if ($bridgeSv -eq $script:WmsBrainClientExpectedSchemaVersion) { 'OK' } else { 'MISMATCH/UNKNOWN' }

    Write-WmsBrainBanner -Lines @(
        '=========================================================',
        ' WmsBrainClient — Quick Start',
        '=========================================================',
        (' Version del modulo  : {0}' -f $script:WmsBrainClientVersion),
        (' Schema esperado     : {0}' -f $script:WmsBrainClientExpectedSchemaVersion),
        (' Schema detectado    : {0}  [{1}]' -f $bridgeSv, $svMatch),
        '========================================================='
    )

    # ----- 2. PRE-FLIGHT (solo si no -Compact) -----
    $preflightHasErrors = $false
    if (-not $Compact) {
        Write-WmsBrainBanner -Lines @(' ', ' [1/4] PRE-FLIGHT')
        try {
            $env_report = Test-WmsBrainEnvironment -ErrorAction Stop
            $errCount  = @($env_report | Where-Object { $_.Level -eq 'ERROR' }).Count
            $warnCount = @($env_report | Where-Object { $_.Level -eq 'WARN' }).Count
            $okCount   = @($env_report | Where-Object { $_.Level -eq 'OK' }).Count
            $preflightHasErrors = ($errCount -gt 0)
            Write-WmsBrainBanner -Lines @(
                ('  Resumen: {0} OK / {1} WARN / {2} ERROR' -f $okCount, $warnCount, $errCount)
            )
        } catch {
            Write-WmsBrainBanner -Lines @("  Pre-flight fallo: $($_.Exception.Message)")
            $preflightHasErrors = $true
        }
    }

    # ----- 3. VARIABLES (solo si no -Compact) -----
    $envSummary = [ordered]@{}
    if (-not $Compact) {
        Write-WmsBrainBanner -Lines @(' ', ' [2/4] VARIABLES DE ENTORNO')
        $rows = @()
        foreach ($e in $envCatalog) {
            $val = [Environment]::GetEnvironmentVariable($e.Name)
            $hasVal = -not [string]::IsNullOrEmpty($val)
            $tag = if (-not $hasVal -and $e.Critical) {
                '[MISSING]'
            } elseif (-not $hasVal) {
                '[--     ]'
            } elseif ($e.Secret) {
                '[SET    ]'
            } else {
                '[OK     ]'
            }
            $shown = if (-not $hasVal) { '(not set)' }
                     elseif ($e.Secret) { '(set, no-print)' }
                     else { $val }
            $rows += ('  {0} {1,-32} {2}' -f $tag, $e.Name, $shown)
            $envSummary[$e.Name] = if (-not $hasVal) { 'MISSING' } elseif ($e.Secret) { 'SET' } else { 'OK' }
        }
        Write-WmsBrainBanner -Lines $rows
    } else {
        # En compact, igual computo el resumen para el output object
        foreach ($e in $envCatalog) {
            $val = [Environment]::GetEnvironmentVariable($e.Name)
            $hasVal = -not [string]::IsNullOrEmpty($val)
            $envSummary[$e.Name] = if (-not $hasVal) { 'MISSING' } elseif ($e.Secret) { 'SET' } else { 'OK' }
        }
    }

    # ----- 4. PROXIMOS PASOS (siempre) -----
    $nextSteps = @()
    $missingCritical = @($envCatalog | Where-Object { $_.Critical -and ($envSummary[$_.Name] -eq 'MISSING') })
    if ($missingCritical.Count -gt 0) {
        $nextSteps += ('Te faltan {0} variables criticas. Corre:' -f $missingCritical.Count)
        $nextSteps += '  Show-WmsBrainQuickStart -SetMissing            # interactivo (sesion)'
        $nextSteps += '  Show-WmsBrainQuickStart -SetMissing -Persist   # interactivo (persistente, no secretos)'
    } else {
        $nextSteps += 'Todas las variables criticas estan seteadas.'
    }
    if ($bridgeSv -eq '?' -or $bridgeSv -like 'ERROR:*') {
        $nextSteps += 'No pude leer SCHEMA_VERSION del brain_bridge.mjs. Verifica $env:WMS_BRAIN_EXCHANGE_REPO_MAIN.'
    } elseif ($svMatch -ne 'OK') {
        $nextSteps += ('Schema mismatch: cliente espera {0}, bridge tiene {1}. Si tu bridge es viejo, igual va a funcionar en modo compat.' -f $script:WmsBrainClientExpectedSchemaVersion, $bridgeSv)
    }
    if ($preflightHasErrors) {
        $nextSteps += 'Pre-flight tiene ERROR. Corre para ver detalle:'
        $nextSteps += '  Test-WmsBrainEnvironment | Format-Table -Auto'
    }
    $nextSteps += 'Con todo OK, prueba en este orden:'
    $nextSteps += '  Show-WmsBrainStatus                           # banner de estado'
    $nextSteps += '  Test-WmsBrainConnection -Profile K7-PRD       # ping a SQL'
    $nextSteps += '  Get-WmsBrainQuestion -Status pending          # cards Q-NNN pendientes'

    Write-WmsBrainBanner -Lines @(' ', ' [3/4] PROXIMOS PASOS')
    Write-WmsBrainBanner -Lines ($nextSteps | ForEach-Object { '  ' + $_ })

    # ----- 5. EJEMPLOS (solo si no -Compact) -----
    if (-not $Compact) {
        Write-WmsBrainBanner -Lines @(' ', ' [4/4] EJEMPLOS COPY-PASTE')
        Write-WmsBrainBanner -Lines @(
            '  # Listar las cards Q-NNN pendientes',
            '  Get-WmsBrainQuestion -Status pending',
            ' ',
            '  # Emitir un evento por una card concreta',
            '  New-WmsBrainQuestionEvent -QuestionId Q-003',
            ' ',
            '  # Responder una card (despues de Invoke-WmsBrainQuestion)',
            '  Submit-WmsBrainAnswer -QuestionId Q-003 -Verdict confirmed -Confidence high',
            ' ',
            '  # Query directo a una BD productiva (read-only)',
            '  Invoke-WmsBrainQuery -Profile K7-PRD -Query "SELECT TOP 5 * FROM dbo.Item"',
            ' ',
            '  # Ayuda completa de cualquier cmdlet',
            '  Get-Help New-WmsBrainQuestionEvent -Full',
            ' '
        )
    }

    # ----- 6. SetMissing (interactivo, solo si se pidio) -----
    if ($SetMissing -and $missingCritical.Count -gt 0) {
        Write-WmsBrainBanner -Lines @(' ', ' INTERACTIVO: setear variables faltantes')
        foreach ($e in $missingCritical) {
            $prompt = if ($e.Secret) {
                "  $($e.Name)  [SECRETO, no se persiste]  ($($e.Hint))"
            } else {
                "  $($e.Name)  ($($e.Hint))"
            }
            Write-WmsBrainBanner -Lines @($prompt)
            $val = if ($e.Secret) {
                $secure = Read-Host -Prompt "   valor" -AsSecureString
                if (-not $secure -or $secure.Length -eq 0) { '' }
                else {
                    $bstr = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure)
                    try { [System.Runtime.InteropServices.Marshal]::PtrToStringBSTR($bstr) }
                    finally { [System.Runtime.InteropServices.Marshal]::ZeroFreeBSTR($bstr) }
                }
            } else {
                Read-Host -Prompt "   valor"
            }
            if ([string]::IsNullOrEmpty($val)) {
                Write-WmsBrainBanner -Lines @("   (vacio, salteado)")
                continue
            }
            # Sesion actual
            Set-Item -Path "Env:$($e.Name)" -Value $val
            $envSummary[$e.Name] = if ($e.Secret) { 'SET' } else { 'OK' }
            # Persist (User scope), saltando secretos por seguridad
            if ($Persist) {
                if ($e.Secret) {
                    Write-WmsBrainBanner -Lines @("   seteado en sesion. NO persistido (secreto).")
                } else {
                    [Environment]::SetEnvironmentVariable($e.Name, $val, 'User')
                    Write-WmsBrainBanner -Lines @("   seteado en sesion + persistido en scope User.")
                }
            } else {
                Write-WmsBrainBanner -Lines @("   seteado en sesion (no persistido).")
            }
        }
        Write-WmsBrainBanner -Lines @(' ', ' Listo. Si persististe vars, abrilas en una ventana nueva para cargarlas.')
    }

    [PSCustomObject]@{
        ModuleVersion          = $script:WmsBrainClientVersion
        ExpectedSchemaVersion  = $script:WmsBrainClientExpectedSchemaVersion
        DetectedSchemaVersion  = $bridgeSv
        SchemaMatch            = ($svMatch -eq 'OK')
        EnvSummary             = $envSummary
        NextSteps              = $nextSteps
    }
}
