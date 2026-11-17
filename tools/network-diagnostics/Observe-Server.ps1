# Optional: run locally ON the IIS/SQL Windows server, no remote admin required by this script.
param([Parameter(Mandatory)][string]$Output,[ValidateRange(10,3600)][int]$Seconds=300,
      [ValidateRange(5,60)][int]$Interval=10)
$ErrorActionPreference='Stop'
New-Item -ItemType Directory -Path $Output -Force | Out-Null
$end=[datetime]::UtcNow.AddSeconds($Seconds)
do {
    $now=[datetime]::UtcNow.ToString('o')
    try {
        Get-CimInstance Win32_PerfFormattedData_PerfOS_Processor -Filter "Name='_Total'" |
            Select-Object @{n='timestampUtc';e={$now}},PercentProcessorTime |
            Export-Csv (Join-Path $Output 'server-cpu.csv') -Append -NoTypeInformation -Encoding UTF8
        Get-CimInstance Win32_OperatingSystem | Select-Object @{n='timestampUtc';e={$now}},FreePhysicalMemory,TotalVisibleMemorySize |
            Export-Csv (Join-Path $Output 'server-memory.csv') -Append -NoTypeInformation -Encoding UTF8
        Get-CimInstance Win32_PerfFormattedData_PerfDisk_PhysicalDisk |
            Select-Object @{n='timestampUtc';e={$now}},Name,CurrentDiskQueueLength,DiskReadsPersec,DiskWritesPersec |
            Export-Csv (Join-Path $Output 'server-disk.csv') -Append -NoTypeInformation -Encoding UTF8
        Get-Process -Name w3wp,sqlservr -ErrorAction SilentlyContinue |
            Select-Object @{n='timestampUtc';e={$now}},Id,ProcessName,CPU,WorkingSet64,PrivateMemorySize64,HandleCount |
            Export-Csv (Join-Path $Output 'server-processes.csv') -Append -NoTypeInformation -Encoding UTF8
    } catch { "$now $($_.Exception.GetType().Name)" | Add-Content (Join-Path $Output 'server-errors.txt') }
    Start-Sleep -Seconds $Interval
} while ([datetime]::UtcNow -lt $end)
