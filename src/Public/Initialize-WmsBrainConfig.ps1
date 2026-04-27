function Initialize-WmsBrainConfig {
<#
.SYNOPSIS
    Wizard interactivo para crear/actualizar el config local del modulo
    (defaults para conectar a BD y exploracion del brain).

.DESCRIPTION
    Pregunta paso a paso por:
      1. Profile productivo por defecto (K7-PRD | BB-PRD | C9-QAS).
      2. Paths a los 3 repos orphan (auto-detecta C:\Tools\tomwms-replit-client-*).
      3. SQL Server: host, user y -opcionalmente- password
         (cifrado con DPAPI scope CurrentUser+Machine).
      4. Brain API base URL (opcional).
      5. Si persistir los valores como variables de entorno User scope.

    Escribe el config en $env:USERPROFILE\.wmsbrain\config.json.
    El password NUNCA viaja al repo: si se guarda, queda cifrado con
    DPAPI y solo es descifrable por el mismo usuario en la misma maquina.

.PARAMETER Path
    Ruta del archivo de config. Default:
    $env:USERPROFILE\.wmsbrain\config.json

.PARAMETER Force
    Sobreescribe el config existente sin pedir confirmacion al final.
    El wizard sigue usando los valores existentes como defaults.

.PARAMETER NonInteractive
    No abre prompts. Usa los defaults (existentes en config previo,
    o auto-detectados, o hardcoded). Util para bootstraping en CI.
    Combinar con -Force si ya existe el config.

.PARAMETER SetEnv
    Despues de escribir el config, setea las variables de entorno
    correspondientes (User scope) para que los otros cmdlets las lean.
    Equivalente a responder "s" al paso 5. En modo NonInteractive
    es la unica forma de activarlas.

.PARAMETER IncludePassword
    Pregunta y persiste el password de SQL en el config (cifrado con
    DPAPI). Por default NO se pregunta. Sin este switch, el config
    queda sin password y vos seguis usando $env:WMS_KILLIOS_DB_PASSWORD
    a mano (mas seguro si compartis el config).

.OUTPUTS
    PSCustomObject con: Path, Created (bool), Config (objeto escrito),
    AppliedEnvVars (array de nombres persistidos como env var).

.EXAMPLE
    Initialize-WmsBrainConfig

    Wizard completo con prompts interactivos. No persiste env vars
    salvo que respondas "s" en el paso 5.

.EXAMPLE
    Initialize-WmsBrainConfig -SetEnv

    Wizard + persiste todas las vars no-secretas en User scope al final.

.EXAMPLE
    Initialize-WmsBrainConfig -IncludePassword -SetEnv

    Wizard + pregunta el password de SQL (oculto), lo cifra con DPAPI
    y lo guarda en el config. Persiste env vars User scope; ademas
    hidrata $env:WMS_KILLIOS_DB_PASSWORD para esta sesion.

.EXAMPLE
    Initialize-WmsBrainConfig -NonInteractive -SetEnv -Force

    Sin prompts. Si existe config, lo respeta como defaults; si no,
    arranca con auto-detect. Persiste env vars User scope.

.LINK
    Show-WmsBrainQuickStart

.LINK
    Get-WmsBrainConnectionString

.NOTES
    El password se cifra con ConvertFrom-SecureString (DPAPI). Solo
    descifrable por el mismo usuario en la misma maquina. NUNCA
    commitees $env:USERPROFILE\.wmsbrain\config.json a un repo.
#>
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [string] $Path,
        [switch] $Force,
        [switch] $NonInteractive,
        [switch] $SetEnv,
        [switch] $IncludePassword
    )

    if (-not $Path) { $Path = Get-WmsBrainConfigPath }

    # Banner ASCII + tagline rotativos (mismos del QuickStart)
    $art = Get-WmsBrainAsciiArt
    $tag = Get-WmsBrainTagline
    Write-Host ' '
    foreach ($line in ($art -split "`r?`n")) { Write-Host $line }
    Write-Host ' '
    Write-Host ("  >> $tag")
    Write-Host ' '

    $modeLabel = 'Interactivo'
    if ($NonInteractive) { $modeLabel = 'NonInteractive' }
    Write-WmsBrainBanner -Lines @(
        '=========================================================',
        ' WmsBrainClient - Wizard de configuracion local',
        '=========================================================',
        (' Destino: {0}' -f $Path),
        (' Modo:    {0}' -f $modeLabel)
    )

    # Existing config (si lo hay) sirve de defaults
    $existing = $null
    if (Test-Path -LiteralPath $Path) {
        $existing = Get-WmsBrainLocalConfig -Path $Path
        if ($existing) {
            Write-WmsBrainBanner -Lines @(
                ' ',
                ' Ya existe un config en esa ruta. Voy a usar sus valores como defaults.',
                ' Cualquier campo que dejes en blanco mantiene el valor anterior.'
            )
        }
    }

    function _Default($value, $fallback) {
        if ($null -ne $value -and "$value" -ne '') { return $value } else { return $fallback }
    }

    # === 1. Profile default ===
    $profileChoices = @('K7-PRD','BB-PRD','C9-QAS')
    $defaultProfile = _Default ($existing.defaultProfile) 'K7-PRD'
    if (-not $NonInteractive) {
        Write-WmsBrainBanner -Lines @(' ', ' [1/5] Profile productivo por defecto')
        for ($i=0; $i -lt $profileChoices.Count; $i++) {
            $marker = if ($profileChoices[$i] -eq $defaultProfile) { '*' } else { ' ' }
            Write-Host ('   {0} [{1}] {2}' -f $marker, ($i+1), $profileChoices[$i])
        }
        $current = ($profileChoices.IndexOf($defaultProfile) + 1)
        $answer = Read-Host ("   Eleccion (1-3) [{0}]" -f $current)
        if ($answer -match '^[1-3]$') {
            $defaultProfile = $profileChoices[[int]$answer - 1]
        }
    }

    # === 2. Repos ===
    $autoMain   = 'C:\Tools\tomwms-replit-client-main'
    $autoBrain  = 'C:\Tools\tomwms-replit-client-brain'
    $autoClient = 'C:\Tools\tomwms-replit-client-automate'

    $repoMain   = _Default ($existing.repos.main)   $(if (Test-Path $autoMain)   { $autoMain }   else { '' })
    $repoBrain  = _Default ($existing.repos.brain)  $(if (Test-Path $autoBrain)  { $autoBrain }  else { '' })
    $repoClient = _Default ($existing.repos.client) $(if (Test-Path $autoClient) { $autoClient } else { '' })

    if (-not $NonInteractive) {
        Write-WmsBrainBanner -Lines @(' ', ' [2/5] Paths a los 3 repos orphan (auto-detecta en C:\Tools)')
        $a = Read-Host ("   MAIN   (rama main)             [{0}]" -f $repoMain);   if ($a) { $repoMain   = $a }
        $a = Read-Host ("   BRAIN  (rama wms-brain)        [{0}]" -f $repoBrain);  if ($a) { $repoBrain  = $a }
        $a = Read-Host ("   CLIENT (rama wms-brain-client) [{0}]" -f $repoClient); if ($a) { $repoClient = $a }
    }

    # === 3. SQL ===
    $dbHost = _Default ($existing.db.host) '52.41.114.122,1437'
    $dbUser = _Default ($existing.db.user) ''
    $dbPasswordEncrypted = if ($existing -and $existing.db -and $existing.db.passwordEncrypted) { $existing.db.passwordEncrypted } else { '' }

    if (-not $NonInteractive) {
        Write-WmsBrainBanner -Lines @(' ', ' [3/5] SQL Server (BD productiva, READ-ONLY)')
        $a = Read-Host ("   Host (host,port) [{0}]" -f $dbHost); if ($a) { $dbHost = $a }
        $a = Read-Host ("   User             [{0}]" -f $dbUser); if ($a) { $dbUser = $a }
        if ($IncludePassword) {
            Write-Host '   Password (Enter sin tipear si no querés cambiarlo)' -ForegroundColor Yellow
            $secure = Read-Host '   Password (oculto, se cifra con DPAPI)' -AsSecureString
            if ($secure -and $secure.Length -gt 0) {
                $dbPasswordEncrypted = ConvertTo-WmsBrainEncryptedString -SecureString $secure
            }
        } else {
            Write-Host '   (Password: omitido. Re-correr con -IncludePassword para guardarlo cifrado.)' -ForegroundColor DarkGray
        }
    }

    # === 4. Brain URL ===
    $brainUrl = _Default ($existing.brain.baseUrl) ''
    if (-not $NonInteractive) {
        Write-WmsBrainBanner -Lines @(' ', ' [4/5] Brain API base URL (opcional, dejalo vacio si no aplica)')
        $a = Read-Host ("   BRAIN_BASE_URL   [{0}]" -f $brainUrl); if ($a) { $brainUrl = $a }
    }

    # === 5. Apply env vars? ===
    $applyEnv = $SetEnv.IsPresent
    if (-not $NonInteractive -and -not $SetEnv) {
        Write-WmsBrainBanner -Lines @(' ', ' [5/5] Persistir variables de entorno (User scope) ahora?')
        $a = Read-Host '   Esto setea env vars en User scope para que los otros cmdlets las lean. (s/n) [s]'
        $applyEnv = ($a -eq '' -or $a -match '^[sySY]')
    }

    # Build config
    $config = [PSCustomObject]@{
        version        = 1
        createdAt      = (Get-Date).ToString('o')
        createdBy      = ('{0}@{1}' -f $env:USERNAME, $env:COMPUTERNAME)
        defaultProfile = $defaultProfile
        repos          = [PSCustomObject]@{
            main   = $repoMain
            brain  = $repoBrain
            client = $repoClient
        }
        db             = [PSCustomObject]@{
            host              = $dbHost
            user              = $dbUser
            passwordEncrypted = $dbPasswordEncrypted
        }
        brain          = [PSCustomObject]@{
            baseUrl = $brainUrl
        }
    }

    # Confirm overwrite si ya existia
    $created = -not [bool]$existing
    $proceed = $true
    if ($existing -and -not $Force -and -not $NonInteractive) {
        Write-WmsBrainBanner -Lines @(
            ' ',
            ' Ya existe un config en esa ruta. Lo voy a sobreescribir con los valores de arriba.'
        )
        $a = Read-Host '   Continuar? (s/n) [s]'
        $proceed = ($a -eq '' -or $a -match '^[sySY]')
    }

    if (-not $proceed) {
        Write-WmsBrainBanner -Lines @(
            ' ',
            '[--] Operacion cancelada por el usuario. Config no modificado.'
        )
        return [PSCustomObject]@{
            Path           = $Path
            Created        = $false
            Config         = $existing
            AppliedEnvVars = @()
            Cancelled      = $true
        }
    }

    if ($PSCmdlet.ShouldProcess($Path, 'Write WmsBrainClient config')) {
        Save-WmsBrainLocalConfig -Config $config -Path $Path -Force | Out-Null
        Write-WmsBrainBanner -Lines @(
            ' ',
            ('[OK] Config escrito en {0}' -f $Path)
        )
    }

    # Apply env vars
    $applied = @()
    if ($applyEnv) {
        if ($repoMain)   { [Environment]::SetEnvironmentVariable('WMS_BRAIN_EXCHANGE_REPO_MAIN',  $repoMain,   'User'); $env:WMS_BRAIN_EXCHANGE_REPO_MAIN  = $repoMain;   $applied += 'WMS_BRAIN_EXCHANGE_REPO_MAIN' }
        if ($repoBrain)  { [Environment]::SetEnvironmentVariable('WMS_BRAIN_EXCHANGE_REPO_BRAIN', $repoBrain,  'User'); $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN = $repoBrain;  $applied += 'WMS_BRAIN_EXCHANGE_REPO_BRAIN' }
        if ($repoClient) { [Environment]::SetEnvironmentVariable('WMS_BRAIN_CLIENT_REPO',         $repoClient, 'User'); $env:WMS_BRAIN_CLIENT_REPO         = $repoClient; $applied += 'WMS_BRAIN_CLIENT_REPO' }
        if ($defaultProfile) { [Environment]::SetEnvironmentVariable('WMS_BRAIN_DEFAULT_PROFILE', $defaultProfile, 'User'); $env:WMS_BRAIN_DEFAULT_PROFILE = $defaultProfile; $applied += 'WMS_BRAIN_DEFAULT_PROFILE' }
        if ($dbHost)     { [Environment]::SetEnvironmentVariable('WMS_KILLIOS_DB_HOST', $dbHost, 'User'); $env:WMS_KILLIOS_DB_HOST = $dbHost; $applied += 'WMS_KILLIOS_DB_HOST' }
        if ($dbUser)     { [Environment]::SetEnvironmentVariable('WMS_KILLIOS_DB_USER', $dbUser, 'User'); $env:WMS_KILLIOS_DB_USER = $dbUser; $applied += 'WMS_KILLIOS_DB_USER' }
        if ($brainUrl)   { [Environment]::SetEnvironmentVariable('BRAIN_BASE_URL', $brainUrl, 'User'); $env:BRAIN_BASE_URL = $brainUrl; $applied += 'BRAIN_BASE_URL' }

        # Hidratar $env:WMS_KILLIOS_DB_PASSWORD desde el config (solo en sesion, NO persistido como env var)
        if ($dbPasswordEncrypted) {
            try {
                $env:WMS_KILLIOS_DB_PASSWORD = ConvertFrom-WmsBrainEncryptedString -EncryptedString $dbPasswordEncrypted
                $applied += '$env:WMS_KILLIOS_DB_PASSWORD (sesion, desde config cifrado)'
            } catch {
                Write-WmsBrainLog -Level WARN -Message ("No pude desencriptar password del config: {0}" -f $_.Exception.Message)
            }
        }

        Write-WmsBrainBanner -Lines @(
            ' ',
            ('[OK] {0} variables persistidas (User scope):' -f $applied.Count)
        )
        foreach ($n in $applied) { Write-Host ('       {0}' -f $n) }

        if (-not $dbPasswordEncrypted) {
            Write-WmsBrainBanner -Lines @(
                ' ',
                '[--] Password de SQL NO esta en el config (no se uso -IncludePassword).',
                '     Setealo aparte para esta sesion:',
                '       $env:WMS_KILLIOS_DB_PASSWORD = "tu-password"',
                '     O re-correr el wizard incluyendolo cifrado:',
                '       Initialize-WmsBrainConfig -IncludePassword -Force'
            )
        }
    } else {
        Write-WmsBrainBanner -Lines @(
            ' ',
            '[--] No se persistieron variables de entorno (no se respondio "s" ni se uso -SetEnv).',
            '     Para activarlas mas tarde:  Initialize-WmsBrainConfig -SetEnv -Force'
        )
    }

    Write-WmsBrainBanner -Lines @(
        ' ',
        '=========================================================',
        ' Listo. Probalo:',
        '   Show-WmsBrainQuickStart',
        ' ',
        ' Para re-editar:',
        '   Initialize-WmsBrainConfig -Force',
        '========================================================='
    )

    return [PSCustomObject]@{
        Path           = $Path
        Created        = $created
        Config         = $config
        AppliedEnvVars = $applied
    }
}
