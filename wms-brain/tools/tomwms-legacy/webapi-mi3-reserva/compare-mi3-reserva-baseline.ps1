param(
    [Parameter(Mandatory = $true)]
    [string]$No,

    [Parameter(Mandatory = $true)]
    [string]$BaselinePath,

    [string]$AppSettingsPath = "..\..\WMSWebAPI\appsettings.Development.json",

    [string]$ConnectionStringName = "CST",

    [switch]$StrictReservations
)

$ErrorActionPreference = "Stop"

function Resolve-FullPath {
    param([string]$Path)

    if ([System.IO.Path]::IsPathRooted($Path)) {
        return $Path
    }

    return (Resolve-Path -Path (Join-Path (Get-Location) $Path)).Path
}

$baselineFile = Resolve-FullPath $BaselinePath
$settingsFile = Resolve-FullPath $AppSettingsPath

$baseline = Get-Content -Raw -Path $baselineFile | ConvertFrom-Json
$settings = Get-Content -Raw -Path $settingsFile | ConvertFrom-Json
$connectionString = $settings.ConnectionStrings.$ConnectionStringName

if ([string]::IsNullOrWhiteSpace($connectionString)) {
    throw "Connection string '$ConnectionStringName' not found in $settingsFile"
}

Add-Type -AssemblyName System.Data

$query = @"
DECLARE @Ref nvarchar(50) = @Documento;
DECLARE @IdPedidoEnc int = (
    SELECT TOP 1 IdPedidoEnc
    FROM dbo.trans_pe_enc WITH (NOLOCK)
    WHERE Referencia = @Ref
    ORDER BY IdPedidoEnc DESC
);

SELECT
    pd.No_linea,
    pd.Codigo_Producto,
    pd.Cantidad AS PedidoCantidad,
    sr.IdStock,
    ISNULL(s.lote, '') AS Lote,
    sr.Cantidad,
    sr.IdPresentacion,
    sr.IdUnidadMedida,
    sr.IdStockRes
FROM dbo.trans_pe_det pd WITH (NOLOCK)
LEFT JOIN dbo.stock_res sr WITH (NOLOCK)
    ON sr.IdPedidoDet = pd.IdPedidoDet
   AND sr.IdTransaccion = pd.IdPedidoEnc
LEFT JOIN dbo.stock s WITH (NOLOCK)
    ON s.IdStock = sr.IdStock
WHERE pd.IdPedidoEnc = @IdPedidoEnc
ORDER BY pd.No_linea, sr.IdStockRes;
"@

$conn = [System.Data.SqlClient.SqlConnection]::new($connectionString)
$conn.Open()
try {
    $cmd = $conn.CreateCommand()
    $cmd.CommandTimeout = 120
    $cmd.CommandText = $query
    [void]$cmd.Parameters.Add("@Documento", [System.Data.SqlDbType]::NVarChar, 50)
    $cmd.Parameters["@Documento"].Value = $No

    $dt = [System.Data.DataTable]::new()
    $adapter = [System.Data.SqlClient.SqlDataAdapter]::new($cmd)
    [void]$adapter.Fill($dt)
}
finally {
    $conn.Close()
}

$actualByLine = @{}
foreach ($row in $dt.Rows) {
    $lineNo = [int]$row.No_linea
    if (-not $actualByLine.ContainsKey($lineNo)) {
        $actualByLine[$lineNo] = @()
    }

    if ($row.IdStock -ne [DBNull]::Value) {
        $actualByLine[$lineNo] += [pscustomobject]@{
            IdStock          = [int]$row.IdStock
            Lote             = [string]$row.Lote
            Cantidad         = [double]$row.Cantidad
            IdPresentacion   = if ($row.IdPresentacion -eq [DBNull]::Value) { 0 } else { [int]$row.IdPresentacion }
            IdUnidadMedida   = if ($row.IdUnidadMedida -eq [DBNull]::Value) { 0 } else { [int]$row.IdUnidadMedida }
        }
    }
}

$diffs = New-Object System.Collections.Generic.List[object]

foreach ($line in $baseline.lineas) {
    $lineNo = [int]$line.line_No
    $legacyReservations = @($line.reservas)
    $actualReservations = @($actualByLine[$lineNo])

    $legacyCount = $legacyReservations.Count
    $actualCount = $actualReservations.Count
    $legacySum = [double](($legacyReservations | Measure-Object cantidad -Sum).Sum)
    $actualSum = [double](($actualReservations | Measure-Object Cantidad -Sum).Sum)

    if ($legacyCount -ne $actualCount -or [math]::Abs($legacySum - $actualSum) -gt 0.000001) {
        $diffs.Add([pscustomobject]@{
            Tipo = "LINE_SUMMARY"
            Linea = $lineNo
            Legacy = "rows=$legacyCount sum=$legacySum"
            Actual = "rows=$actualCount sum=$actualSum"
        })
        continue
    }

    if ($StrictReservations) {
        $legacyStrict = $legacyReservations |
            Sort-Object idStock,lote,cantidad |
            ForEach-Object { "$($_.idStock)|$($_.lote)|$([double]$_.cantidad)" }
        $actualStrict = $actualReservations |
            Sort-Object IdStock,Lote,Cantidad |
            ForEach-Object { "$($_.IdStock)|$($_.Lote)|$([double]$_.Cantidad)" }

        $legacyText = ($legacyStrict -join ";")
        $actualText = ($actualStrict -join ";")

        if ($legacyText -ne $actualText) {
            $diffs.Add([pscustomobject]@{
                Tipo = "STRICT_RESERVATIONS"
                Linea = $lineNo
                Legacy = $legacyText
                Actual = $actualText
            })
        }
    }
}

$summary = [pscustomobject]@{
    Documento = $No
    Baseline = $baseline.referencia
    LineasBaseline = [int]$baseline.cantidad_lineas
    LineasActual = ($actualByLine.Keys | Measure-Object).Count
    StockResRowsBaseline = [int]$baseline.total_stockRes_Filas
    StockResRowsActual = $dt.Rows.Count
    StockResSumBaseline = [double]$baseline.total_stockRes_Cantidad
    StockResSumActual = [double](($dt | Measure-Object Cantidad -Sum).Sum)
    StrictReservations = [bool]$StrictReservations
    Diferencias = $diffs.Count
}

$summary | Format-List

if ($diffs.Count -gt 0) {
    $diffs | Format-Table -AutoSize
    exit 1
}

"Comparacion OK."
