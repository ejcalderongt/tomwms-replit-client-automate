# _Config.ps1 — helpers para leer/escribir el config local del modulo.
#
# Path por default:  $env:USERPROFILE\.wmsbrain\config.json
# Schema (v1):
#   {
#     "version": 1,
#     "createdAt": "...",
#     "createdBy": "user@host",
#     "defaultProfile": "K7-PRD" | "BB-PRD" | "C9-QAS",
#     "repos": { "main": "...", "brain": "...", "client": "..." },
#     "db":    { "host": "host,port", "user": "...", "passwordEncrypted": "<DPAPI base64>" },
#     "brain": { "baseUrl": "..." }
#   }
#
# Password se cifra con DPAPI scope=CurrentUser+Machine via ConvertFrom-SecureString.
# Solo descifrable por el mismo usuario en la misma maquina. NUNCA viaja al repo.

function Get-WmsBrainConfigPath {
    [CmdletBinding()]
    param()
    $base = if ($env:USERPROFILE) { $env:USERPROFILE } else { [Environment]::GetFolderPath('UserProfile') }
    if (-not $base) { $base = $HOME }
    return (Join-Path $base '.wmsbrain\config.json')
}

function Get-WmsBrainLocalConfig {
    [CmdletBinding()]
    param(
        [string] $Path
    )
    if (-not $Path) { $Path = Get-WmsBrainConfigPath }
    if (-not (Test-Path -LiteralPath $Path)) { return $null }
    try {
        $raw = Get-Content -LiteralPath $Path -Raw -Encoding UTF8
        if (-not $raw) { return $null }
        return ($raw | ConvertFrom-Json)
    } catch {
        Write-WmsBrainLog -Level WARN -Message "No pude leer config local en '$Path': $($_.Exception.Message)"
        return $null
    }
}

function Save-WmsBrainLocalConfig {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [PSCustomObject] $Config,
        [string] $Path,
        [switch] $Force
    )
    if (-not $Path) { $Path = Get-WmsBrainConfigPath }
    $dir = Split-Path -Parent $Path
    if ($dir -and -not (Test-Path -LiteralPath $dir)) {
        New-Item -ItemType Directory -Path $dir -Force | Out-Null
    }
    if ((Test-Path -LiteralPath $Path) -and -not $Force) {
        throw "Config ya existe en '$Path'. Usa -Force para sobreescribir."
    }
    $json = $Config | ConvertTo-Json -Depth 10
    Set-Content -LiteralPath $Path -Value $json -Encoding UTF8
    return $Path
}

function ConvertTo-WmsBrainEncryptedString {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [System.Security.SecureString] $SecureString
    )
    return ConvertFrom-SecureString -SecureString $SecureString
}

function ConvertFrom-WmsBrainEncryptedString {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $EncryptedString
    )
    $secure = ConvertTo-SecureString -String $EncryptedString
    $bstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure)
    try {
        return [Runtime.InteropServices.Marshal]::PtrToStringAuto($bstr)
    } finally {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($bstr)
    }
}

function Get-WmsBrainAsciiArt {
    [CmdletBinding()]
    param(
        [int] $Index = -1
    )
    $figures = @(
@'
       _---~~(~~-_.
     _{        )   )
   ,   ) -~~- ( ,-' )_
  (  `-,_..`., )-- '_,)
 ( ` _)  (  -~( -_ `,  }
 (_-  _  ~_-~~~~`,  ,' )
   `~ -^(    __;-,((()))
         ~~~~ {_ -_(())
                `\  }
                  { }
'@,
@'
        ___________________
       /                  /|
      /__________________/ |
     |  __  __  __  __  |  |
     | |__||__||__||__| |  |
     |  __  __  __  __  | /|
     | |__||__||__||__| | /|
     |__________________|/ |
     |  __  __  __  __  |  |
     | |__||__||__||__| | /
     |__________________|/
'@,
@'
        _.-=""""=-._
      .'  _      _  '.
     /   (_)    (_)   \
    |     ___    ___    |
    |    | OK |  | OK |  |
    |    |____|  |____|  |
     \    \________/    /
      `._            _.'
         `'--------'`
'@,
@'
       .-============-.
      /                \
     |   .----------.   |
     |   |  TOMWMS   |  |
     |   |    HH     |  |
     |   '----------'   |
     |   o    o    o    |
      \________________/
        ||          ||
        ''          ''
'@,
@'
            _____
          /       \
         |  o   o  |
         |    >    |
          \  ___  /
           \_____/
          /|     |\
         / |--+--| \
            |   |
           _|___|_
'@,
@'
     /\        _    _    _
    /  \      | |  | |  | |
   /    \     | |  | |  | |
  /  /\  \    | |  | |  | |
 /  /__\  \   | |  | |  | |
/__/    \__\  |_|  |_|  |_|
'@,
@'
   .---.---.---.---.---.
   | K | 7 | - | P | R |
   '---'---'---'---'---'
       |   PIPELINE   |
       v              v
   [main]<->[brain]<->[client]
'@
    )
    if ($Index -lt 0 -or $Index -ge $figures.Count) {
        $Index = ((Get-Date).DayOfYear - 1) % $figures.Count
    }
    return $figures[$Index]
}

function Get-WmsBrainTagline {
    [CmdletBinding()]
    param(
        [int] $Index = -1
    )
    $taglines = @(
        'TOMWMS Brain online. Pipeline READY.',
        'Conectando inteligencia operativa al flujo del galpon.',
        'El cerebro WMS escucha, aprende y propone.',
        'BD productiva en modo lectura: explorar antes de actuar.',
        'Cada Q-NNN es una hipotesis a confirmar.',
        'Bridge MAIN <-> BRAIN sincronizado.',
        'Vos tomas decisiones; el cerebro las recuerda.',
        'Pipeline de eventos abierto. SCHEMA_VERSION=1.',
        'Listo para emitir, responder y aplicar bundles.',
        'Velocidad sin destruir: cambios atomicos por bundle.',
        'PrograX24 + Killios: datos en tiempo real, decisiones a tiempo.',
        'Read-only por diseno. Mutaciones solo via bundles aprobados.'
    )
    if ($Index -lt 0 -or $Index -ge $taglines.Count) {
        $now = Get-Date
        $Index = ($now.Hour * 60 + $now.Minute) % $taglines.Count
    }
    return $taglines[$Index]
}
