# _GitHelpers.ps1 — helpers de git encapsulados (sin dependencias externas).
#
# Compatibilidad: PowerShell 5.1 + 7. Usa ProcessStartInfo.Arguments (string)
# con quoting Win32 via ConvertTo-WmsBrainProcessArgString (definido en
# _NodeRunner.ps1). NO se usa ProcessStartInfo.ArgumentList porque esa API
# fue agregada en .NET Core 2.1 y no existe en .NET Framework 4.x (PS 5.1).

function Invoke-WmsBrainGit {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $RepoPath,
        [Parameter(Mandatory)] [string[]] $GitArgs,
        [switch] $AllowFail
    )
    if (-not (Test-Path -LiteralPath (Join-Path $RepoPath '.git'))) {
        throw "No es un repo git: $RepoPath"
    }
    $git = Get-Command -Name 'git' -ErrorAction SilentlyContinue
    if (-not $git) { throw "No se encontro 'git' en el PATH." }

    $psi = New-Object System.Diagnostics.ProcessStartInfo
    $psi.FileName = $git.Source
    $psi.WorkingDirectory = $RepoPath
    $psi.RedirectStandardOutput = $true
    $psi.RedirectStandardError = $true
    $psi.UseShellExecute = $false
    $psi.CreateNoWindow = $true
    $psi.Arguments = ConvertTo-WmsBrainProcessArgString -Arguments $GitArgs
    $proc = [System.Diagnostics.Process]::Start($psi)
    $stdout = $proc.StandardOutput.ReadToEnd()
    $stderr = $proc.StandardError.ReadToEnd()
    $proc.WaitForExit()
    $exit = $proc.ExitCode
    $result = [PSCustomObject]@{
        ExitCode = $exit
        StdOut   = $stdout.Trim()
        StdErr   = $stderr.Trim()
    }
    if ($exit -ne 0 -and -not $AllowFail) {
        throw "git $($GitArgs -join ' ') fallo (exit=$exit) en ${RepoPath}: $stderr"
    }
    return $result
}

function Get-WmsBrainGitBranch {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $RepoPath
    )
    $r = Invoke-WmsBrainGit -RepoPath $RepoPath -GitArgs @('rev-parse', '--abbrev-ref', 'HEAD') -AllowFail
    if ($r.ExitCode -ne 0) { return $null }
    return $r.StdOut
}

function Test-WmsBrainGitWorkingTreeClean {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $RepoPath
    )
    $r = Invoke-WmsBrainGit -RepoPath $RepoPath -GitArgs @('status', '--porcelain') -AllowFail
    if ($r.ExitCode -ne 0) { return $false }
    return ([string]::IsNullOrWhiteSpace($r.StdOut))
}

function Assert-WmsBrainGitOnBranch {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $RepoPath,
        [Parameter(Mandatory)] [string] $ExpectedBranch,
        [int] $ExitCode = 4
    )
    $b = Get-WmsBrainGitBranch -RepoPath $RepoPath
    if ($b -ne $ExpectedBranch) {
        throw "[$ExitCode] Repo '$RepoPath' esta en rama '$b'. Se esperaba '$ExpectedBranch'."
    }
}
