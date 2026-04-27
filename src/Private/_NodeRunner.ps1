# _NodeRunner.ps1 — wrapper para invocar scripts node (.mjs) y capturar
# stdout / stderr / exit code de forma uniforme.
#
# Compatibilidad: PowerShell 5.1 (Windows PowerShell sobre .NET Framework 4.x)
# y PowerShell 7+ (.NET Core / .NET 6+). Por eso NO se usa
# ProcessStartInfo.ArgumentList (introducido en .NET Core 2.1, no presente en
# .NET Framework 4.x). Se construye un string `Arguments` con quoting
# Win32-correcto via ConvertTo-WmsBrainProcessArgString.

# Quoting Win32/CRT (CommandLineToArgvW). Reglas:
#  - Si el arg no contiene espacio/tab/comillas y no esta vacio, va sin comillas.
#  - Sino se rodea con comillas dobles.
#  - Cada `"` interna se escapa como `\"`.
#  - Cada secuencia de N backslashes inmediatamente antes de un `"` (o del cierre
#    de comillas) se escribe como 2N backslashes + el `"` escapado, asi al
#    re-parsear queda como N backslashes + `"`.
function ConvertTo-WmsBrainProcessArgString {
    [CmdletBinding()]
    param([string[]] $Arguments = @())

    $sb = New-Object System.Text.StringBuilder
    for ($i = 0; $i -lt $Arguments.Count; $i++) {
        if ($i -gt 0) { [void]$sb.Append(' ') }
        $a = [string]$Arguments[$i]
        if ($a.Length -gt 0 -and $a.IndexOfAny([char[]]@(' ', "`t", '"')) -lt 0) {
            [void]$sb.Append($a)
            continue
        }
        [void]$sb.Append('"')
        $bsRun = 0
        for ($j = 0; $j -lt $a.Length; $j++) {
            $c = $a[$j]
            if ($c -eq '\') {
                $bsRun++
            } elseif ($c -eq '"') {
                [void]$sb.Append('\' * (2 * $bsRun + 1))
                [void]$sb.Append('"')
                $bsRun = 0
            } else {
                if ($bsRun -gt 0) { [void]$sb.Append('\' * $bsRun); $bsRun = 0 }
                [void]$sb.Append($c)
            }
        }
        if ($bsRun -gt 0) { [void]$sb.Append('\' * (2 * $bsRun)) }
        [void]$sb.Append('"')
    }
    return $sb.ToString()
}

function Invoke-WmsBrainNode {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $ScriptPath,
        [string[]] $Arguments = @(),
        [string] $WorkingDirectory,
        [switch] $PassThruRaw
    )

    if (-not (Test-Path -LiteralPath $ScriptPath)) {
        throw "Script node no existe: $ScriptPath"
    }
    $node = Get-Command -Name 'node' -ErrorAction SilentlyContinue
    if (-not $node) {
        throw "No se encontro 'node' en el PATH. Instala Node.js >= 20."
    }

    $cwd = if ($WorkingDirectory) { $WorkingDirectory } else { Split-Path -Parent $ScriptPath }

    $allArgs = @($ScriptPath) + @($Arguments)
    $argString = ConvertTo-WmsBrainProcessArgString -Arguments $allArgs

    $psi = New-Object System.Diagnostics.ProcessStartInfo
    $psi.FileName = $node.Source
    $psi.WorkingDirectory = $cwd
    $psi.RedirectStandardOutput = $true
    $psi.RedirectStandardError = $true
    $psi.UseShellExecute = $false
    $psi.CreateNoWindow = $true
    $psi.Arguments = $argString

    Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainNode' -Level 'DEBUG' `
        -Message ("ejecutando: node {0} {1}" -f $ScriptPath, ($Arguments -join ' '))

    $proc = [System.Diagnostics.Process]::Start($psi)
    $stdout = $proc.StandardOutput.ReadToEnd()
    $stderr = $proc.StandardError.ReadToEnd()
    $proc.WaitForExit()
    $exit = $proc.ExitCode

    $result = [PSCustomObject]@{
        ExitCode = $exit
        StdOut   = $stdout
        StdErr   = $stderr
        Command  = "node $ScriptPath $($Arguments -join ' ')"
    }
    if ($PassThruRaw) { return $result }

    if ($exit -ne 0) {
        $msg = "node script fallo (exit=$exit): $ScriptPath`n--stdout--`n$stdout`n--stderr--`n$stderr"
        Write-WmsBrainLog -Cmdlet 'Invoke-WmsBrainNode' -Level 'ERROR' -Message (ConvertTo-WmsBrainSafeString $msg)
        throw $msg
    }
    return $result
}

function Get-WmsBrainNodeVersion {
    [CmdletBinding()] param()
    $node = Get-Command -Name 'node' -ErrorAction SilentlyContinue
    if (-not $node) { return $null }
    $v = & $node.Source --version 2>$null
    if (-not $v) { return $null }
    return ($v -replace '^v', '').Trim()
}
