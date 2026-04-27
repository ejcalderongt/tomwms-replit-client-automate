function ConvertFrom-WmsBrainBridgeListOutput {
<#
.SYNOPSIS
    Parsea la salida texto de `node scripts/brain_bridge.mjs list ...`.

.DESCRIPTION
    Tolera tres formatos:
      A) JSON puro (array u objeto), por ejemplo si el bridge soporta `--json`.
      B) Lineas key=value, formato real del bridge actual:
           20260427-1845-EJC  type=apply_succeeded  status=pending  source=apply_bundle  message="..."
         Acepta valores con o sin comillas, separados por dos o mas espacios o tabs.
      C) Formato tabular legacy `id status type summary` (4 columnas separadas
         por espacios), que algunos forks viejos del bridge emiten.

    Filtra automaticamente headers, separadores tipo `-----` y lineas vacias.

.PARAMETER StdOut
    Cadena con el stdout completo del subprocess.

.OUTPUTS
    Array de PSCustomObject con los campos parseados (id, type, status, ...).
#>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [AllowEmptyString()] [string] $StdOut
    )

    $stdout = $StdOut.Trim()
    if ([string]::IsNullOrWhiteSpace($stdout)) { return @() }

    # Formato A: JSON
    if ($stdout.StartsWith('[') -or $stdout.StartsWith('{')) {
        try {
            $parsed = $stdout | ConvertFrom-Json -ErrorAction Stop
            if ($parsed -is [System.Array]) { return @($parsed) }
            return @($parsed)
        } catch {
            # caer a parser de texto
        }
    }

    $events = @()
    foreach ($rawLine in ($stdout -split "`r?`n")) {
        $line = $rawLine.Trim()
        if (-not $line) { continue }
        # Saltar headers (palabras separadas por 2+ espacios sin '=' y sin id YYYYMMDD-HHMM-INIT)
        if ($line -match '^-{3,}$') { continue }
        if ($line -notmatch '^\s*\d{8}-\d{4}-[A-Z0-9]+\b') {
            # No empieza con id valido -> probablemente header/banner
            continue
        }

        # Capturar id al inicio
        if ($line -notmatch '^(?<id>\d{8}-\d{4}-[A-Z0-9]+)\s+(?<rest>.+)$') { continue }
        $id   = $Matches['id']
        $rest = $Matches['rest']

        $obj = [ordered]@{ id = $id }

        # Formato B: tokens key=value (real). Captura key=value con value posiblemente entre "..."
        $kvMatches = [regex]::Matches($rest, '([A-Za-z_][\w\-]*)\s*=\s*(?:"([^"]*)"|(\S+))')
        if ($kvMatches.Count -gt 0) {
            foreach ($m in $kvMatches) {
                $key = $m.Groups[1].Value
                $val = if ($m.Groups[2].Success) { $m.Groups[2].Value } else { $m.Groups[3].Value }
                $obj[$key] = $val
            }
        }
        else {
            # Formato C: tabular legacy `status  type  summary`
            if ($rest -match '^\s*(?<status>\S+)\s+(?<type>\S+)\s+(?<summary>.+)$') {
                $obj['status']  = $Matches['status']
                $obj['type']    = $Matches['type']
                $obj['summary'] = $Matches['summary']
            }
        }

        $events += [PSCustomObject]$obj
    }
    return $events
}

function ConvertFrom-WmsBrainHelloSyncOutput {
<#
.SYNOPSIS
    Parsea la salida texto de `node scripts/hello_sync.mjs`.

.DESCRIPTION
    Tolera dos formatos:
      A) Formato actual del .mjs:
           OK  rama=wms-brain  head=abcd1234  bundles=12  ultimo_producido=v23  ultimo_aplicado=v22  pendiente=v23
         (case-insensitive, key=value separados por espacios o tabs).
      B) Formato legacy verboso:
           Rama: wms-brain
           HEAD: abcd1234
           Bundles encontrados: 12
           Ultimo producido: v23
           Ultimo aplicado: v22
           Pendientes: v23

    Mapea claves a un PSCustomObject con campos canonicos:
      ExchangeBranch, ExchangeHead, BundlesEncontrados (int o ''),
      UltimoProducido, UltimoAplicado, Pendiente (array).

    Acepta sinonimos: rama|branch, head, bundles|bundles_encontrados,
    ultimo_producido|last_produced, ultimo_aplicado|last_applied,
    pendiente|pendientes|pending.
#>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [AllowEmptyString()] [string] $StdOut
    )

    $branch  = ''
    $head    = ''
    $bundles = ''
    $ultProd = ''
    $ultApl  = ''
    $pend    = @()

    $kvAll = @{}
    foreach ($rawLine in ($StdOut -split "`r?`n")) {
        $line = $rawLine.Trim()
        if (-not $line) { continue }

        # Formato A: tokens key=value en la misma linea
        $kvHits = [regex]::Matches($line, '([A-Za-z_][\w\-]*)\s*=\s*(?:"([^"]*)"|(\S+))')
        if ($kvHits.Count -gt 0) {
            foreach ($m in $kvHits) {
                $k = $m.Groups[1].Value.ToLowerInvariant()
                $v = if ($m.Groups[2].Success) { $m.Groups[2].Value } else { $m.Groups[3].Value }
                if (-not $kvAll.ContainsKey($k)) { $kvAll[$k] = $v }
            }
            continue
        }

        # Formato B: lineas con prefijo `Clave: valor` (case-insensitive)
        if ($line -match '^(?<k>[A-Za-z][\w \-]*?)\s*:\s+(?<v>.+)$') {
            $k = $Matches['k'].Trim().ToLowerInvariant()
            $v = $Matches['v'].Trim()
            # Normalizar nombres compuestos espaciados
            $k = $k -replace '\s+', '_'
            if (-not $kvAll.ContainsKey($k)) { $kvAll[$k] = $v }
        }
    }

    # Resolver sinonimos
    $synonyms = @{
        ExchangeBranch     = @('rama', 'branch')
        ExchangeHead       = @('head', 'commit')
        BundlesEncontrados = @('bundles', 'bundles_encontrados', 'bundles_found')
        UltimoProducido    = @('ultimo_producido', 'last_produced', 'last_produced_bundle')
        UltimoAplicado     = @('ultimo_aplicado', 'last_applied', 'last_applied_bundle')
        Pendiente          = @('pendiente', 'pendientes', 'pending')
    }
    function _Get-First {
        param($Map, [string[]] $Keys)
        foreach ($k in $Keys) { if ($Map.ContainsKey($k)) { return $Map[$k] } }
        return $null
    }
    $branch  = ([string](_Get-First $kvAll $synonyms.ExchangeBranch))
    $head    = ([string](_Get-First $kvAll $synonyms.ExchangeHead))
    $bundlesRaw = (_Get-First $kvAll $synonyms.BundlesEncontrados)
    if ($null -ne $bundlesRaw -and $bundlesRaw -match '^\d+$') {
        $bundles = [int]$bundlesRaw
    } elseif ($null -ne $bundlesRaw) {
        $bundles = $bundlesRaw
    }
    $ultProd = ([string](_Get-First $kvAll $synonyms.UltimoProducido))
    $ultApl  = ([string](_Get-First $kvAll $synonyms.UltimoAplicado))
    $pendRaw = (_Get-First $kvAll $synonyms.Pendiente)
    if ($pendRaw) { $pend = @($pendRaw -split '\s*,\s*' | Where-Object { $_ }) }

    return [PSCustomObject]@{
        ExchangeBranch     = $branch
        ExchangeHead       = $head
        BundlesEncontrados = $bundles
        UltimoProducido    = $ultProd
        UltimoAplicado     = $ultApl
        Pendiente          = $pend
        Raw                = $kvAll
    }
}
