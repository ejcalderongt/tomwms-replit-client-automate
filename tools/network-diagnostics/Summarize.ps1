param([Parameter(Mandatory)][string]$Run)
$ErrorActionPreference='Stop'
$inv=[Globalization.CultureInfo]::InvariantCulture
function Percentile($values,[double]$p) {
    $a=@($values | Sort-Object)
    if (-not $a.Count) { return $null }
    $a[[math]::Max(0,[math]::Ceiling($a.Count*$p)-1)]
}
$summary=@()
foreach ($file in Get-ChildItem -LiteralPath $Run -Filter '*.csv') {
    if ($file.BaseName -notmatch '^(icmp|dns|tcp|http|sql)-' -or $file.Name -like '*.snapshot.csv') { continue }
    $rows=@(Import-Csv -LiteralPath $file.FullName)
    if (-not $rows.Count) { continue }
    $ok=@($rows | Where-Object success -eq 'True')
    $bad=$rows.Count-$ok.Count
    foreach ($metric in @('totalMs','dnsMs','tcpMs','tlsMs','waitFirstByteMs','ttfbMs','downloadMs','connectMs','queryMs')) {
        $numbers=@($ok | Where-Object { $_.$metric -ne '' } | ForEach-Object { [double]::Parse($_.$metric,$inv) })
        if (-not $numbers.Count -and $metric -ne 'totalMs') { continue }
        $summary+=[pscustomobject]@{kind=$rows[0].kind;target=$rows[0].target;metric=$metric;attempts=$rows.Count;successes=$ok.Count;
            failures=$bad;failurePercent=[math]::Round(100*$bad/$rows.Count,2);samples=$numbers.Count;
            p50=Percentile $numbers 0.50;p95=Percentile $numbers 0.95;p99=Percentile $numbers 0.99;
            max=if($numbers.Count){($numbers | Measure-Object -Maximum).Maximum}else{$null}}
    }
}
if ($summary.Count) { $summary | Export-Csv (Join-Path $Run 'summary.csv') -NoTypeInformation -Encoding UTF8 }
$workerFailures=@()
if (Test-Path (Join-Path $Run 'workers.csv')) { $workerFailures=@(Import-Csv (Join-Path $Run 'workers.csv') | Where-Object state -ne Completed) }
@(
    '# Resumen de mediciones'
    ''
    'Los percentiles incluyen solo intentos exitosos. Revisar fallos, timeouts, workers.csv y CSV originales por separado.'
    'Una sesión interrumpida o un worker fallido no demuestra ausencia de incidentes.'
    "Workers no completados: $($workerFailures.Count)."
    'DNS consulta A al resolver configurado: su timeout es el de Windows, no timeoutSeconds.'
    'TCP por nombre incluye resolución local. HTTP usa proceso/conexión nuevos por intento; puede usar proxy del entorno.'
    'waitFirstByteMs incluye envío de solicitud, transporte, colas y procesamiento; no es tiempo SQL ni CPU puro.'
    'SQL SELECT 1 mide conexión/login y roundtrip trivial; no representa la consulta real de WMS.'
    'p99 con pocas muestras no es estable. No comparar escenarios de duración o carga distintas sin indicarlo.'
    ''
    'No hay atribución automática de causa. Correlacionar incidents.csv, métricas y evidencia de AP/IIS/SQL.'
) | Set-Content (Join-Path $Run 'SUMMARY.md') -Encoding UTF8
# Hashes at summary time; rerun this script after adding notes/captures for a final manifest.
Get-ChildItem -LiteralPath $Run -File | Where-Object Name -ne 'SHA256.csv' | ForEach-Object {
    [pscustomobject]@{file=$_.Name;sha256=(Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash}
} | Export-Csv (Join-Path $Run 'SHA256.csv') -NoTypeInformation -Encoding UTF8
