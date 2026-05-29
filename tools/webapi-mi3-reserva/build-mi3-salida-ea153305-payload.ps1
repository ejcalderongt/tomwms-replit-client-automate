param(
    [string]$No = "EA-153305-WAPI01",
    [string]$OutFile = "$PSScriptRoot\mi3-salida-ea153305-webapi.json"
)

# #EJC20260520_RESERVA_BYB_FIX:
# Payload reproductible para probar la misma ruta MI3 desde WMSWebAPI:
# POST /api/sync/salidas/mi3/insertar.
# Bodega/cliente BYB QA: BA0002. Propietario BYB PT: codigo 01.

$fecha = "2026-05-20T18:43:00"

$descripciones = @{
    "00020502" = "CHILE JALAPENO 96/5.8 oz. PICANTE -"
    "00140702" = "MOSTAZA 12/200 GRAMOS DOY PACK -"
    "00174467" = "SALSA PICAMAS ROJA 24/553 g JUMBO"
    "00025001" = "CHILE JALAPENO PICANTE 12/320G BOLSA -"
    "00025004" = "CHILE JALAPENO 48/5.8 oz. SEMIPICANTE -"
    "00025005" = "CHILE JALAPENO 48/5.8 oz. PICANTE -"
    "00050608" = "DISPLAY DE CONCENTRADO DE TAMARINDO 6/12 UN -"
    "00053325" = "CONCENTRADO DE CARAMBOLA 12/678 ML -"
    "00080707" = "MERMELADA DE FRESA 24/200 g DOY PACK -"
    "00084423" = "MERMELADA DE FRESA 12/260 G VASO -"
    "00193250" = "SALSA DE TOMATE DULCE 16/ 950 g BOLSA -"
    "00194250" = "SALSA DE TOMATE DULCE 4/3500 g BOLSA -"
    "00194450" = "SALSA DE TOMATE 8/1800 g BOLSA -"
    "00194473" = "SALSA DE TOMATE 24/400 g PLASTICO -"
    "00194476" = "SALSA DE TOMATE DULCE 48/100 g BOLSA -"
    "00194482" = "SALSA DULCE BB PLASTICO 14/755 g -"
    "00200400" = "SALSA INGLESA 48/3.5 ONZ. PLASTICA -"
    "00200500" = "SALSA INGLESA 24/152 mL PLASTICA -"
    "00220400" = "SALSA SOYA 48/3.5 oz PLASTICA -"
    "00224100" = "SALSA SOYA 4/3850 mL PLASTICO -"
    "00230438" = "VINAGRE CLARO 48/100 mL -"
    "00230939" = "VINAGRE CLARO 24/200 mL -"
    "00231838" = "VINAGRE CLARO 24/500 mL -"
    "00232538" = "VINAGRE CLARO 12/750 mL BOT. LARGA -"
    "00234138" = "VINAGRE CLARO 4/3785 mL GALON PLASTICO -"
    "00281554" = "SALSA MILTOMATE SEMIPICANTE BB 24/360 g -"
    "00290542" = "SALSA PICANTE MAYA-IK 24/155 g COBANERO"
    "00441400" = "SALSA BARBACOA SQUEZZE 24/14 oz PLAST. -"
    "00441404" = "SALSA BARBACOA 24/95 GRAMOS DOY PACK -"
    "00444100" = "SALSA BARBACOA 4/4,400 g -"
    "00521008" = "SALSA PICANTE FRONTERA 8/ 1/2 GALON"
    "00521009" = "SALSA PICANTE FRONTERA 4/ 1 GALON"
    "00531008" = "SALSA KETCHUP FRONTERA 8/1500 g 1/2 GAL"
}

$lineasRaw = @(
    "10000|00020502|10",
    "20000|00140702|125",
    "30000|00174467|50",
    "40000|00025001|5.083333",
    "50000|00025004|1.291667",
    "60000|00025005|1.3125",
    "70000|00050608|10.666667",
    "80000|00053325|5.416667",
    "90000|00080707|5",
    "100000|00084423|2",
    "110000|00193250|7.5625",
    "120000|00194250|3.75",
    "130000|00194450|2.125",
    "140000|00194473|17",
    "150000|00194476|12",
    "160000|00194482|1.285714",
    "170000|00200400|15",
    "180000|00200500|11",
    "190000|00220400|1",
    "200000|00224100|8.75",
    "210000|00230438|5",
    "220000|00230939|2.958333",
    "230000|00231838|2.541667",
    "240000|00232538|5.166667",
    "250000|00234138|15.75",
    "260000|00281554|2.666667",
    "270000|00290542|2.708333",
    "280000|00441400|1.166667",
    "290000|00441404|1.208333",
    "300000|00444100|7.75",
    "310000|00521008|4",
    "320000|00521009|8.25",
    "330000|00531008|4.375",
    "340000|00025001|3",
    "350000|00025004|4",
    "360000|00025005|5",
    "370000|00050608|1",
    "380000|00053325|7",
    "390000|00080707|8",
    "400000|00084423|9",
    "410000|00193250|10",
    "420000|00194250|2.75",
    "430000|00194450|1.5",
    "440000|00194473|13",
    "450000|00194476|14",
    "460000|00194482|1.071429",
    "470000|00200400|16",
    "480000|00200500|17",
    "490000|00220400|18",
    "500000|00224100|4.75",
    "510000|00230438|20",
    "520000|00230939|21",
    "530000|00231838|22",
    "540000|00232538|1.916667",
    "550000|00234138|6",
    "560000|00281554|1.041667",
    "570000|00290542|1.083333",
    "580000|00441400|1.125",
    "590000|00441404|1.166667",
    "600000|00444100|7.25",
    "610000|00521008|3.75",
    "620000|00521009|7.75",
    "630000|00531008|4"
)

$lineas = foreach ($raw in $lineasRaw) {
    $parts = $raw.Split("|")
    $lineNo = [int]$parts[0]
    $codigo = $parts[1]
    $cantidad = [double]::Parse($parts[2], [System.Globalization.CultureInfo]::InvariantCulture)

    [ordered]@{
        noEnc = $No
        line_No = $lineNo
        variant_Code = "CJ"
        no = $codigo
        description = $descripciones[$codigo]
        item_No = $codigo
        qty_to_Receive = 0
        qty_to_Ship = 0
        quantity = $cantidad
        quantity_Shipped = 0
        transfer_to_CodeField = "BA0002"
        transfer_From_CodeField = "BA0002"
        shipment_Date = $fecha
        unit_of_Measure_Code = "UNI"
        status = 0
        process_Result = ""
        price = 0
        lote_No = ""
        expiration_Date = "1900-01-01T00:00:00"
        source_ID = "MI3-WEBAPI-TEST"
        idPedidoDet = 0
        quantity_In_UMBas = 0
        is_Partially_Processed = $false
        quantity_Reserved_WMS = 0
        scan_Type = ""
        color = ""
        size = ""
        lotes_Detalle = @()
    }
}

$payload = [ordered]@{
    beINavPedCompraEnc = [ordered]@{
        no = $No
        posting_Date = $fecha
        receipt_Date = $fecha
        shipment_Date = $fecha
        status = 0
        transfer_from_Code = "BA0002"
        transfer_from_Contact = ""
        transfer_from_Name = "PRODUCTO TERMINADO"
        transfer_to_Code = "BA0002"
        transfer_to_Contact = ""
        transfer_to_Name = "PRODUCTO TERMINADO"
        transfer_to_CodeField = "BA0002"
        product_Owner_Code = "01"
        is_Internal_Transfer = $true
        receipt_Document_Reference = "EA-153305"
        document_Type = 1
        external_Document_No = "EA-153305"
        roadCodigoRuta = "1060315"
        roadCodigoVendedor = ""
        manufacturing_Process = 0
        address = ""
        comments = "#EJC20260520_RESERVA_BYB_FIX WebAPI parity test"
        company_Code = ""
        isExport = $false
        lineas_Detalle = $lineas
    }
}

$json = $payload | ConvertTo-Json -Depth 12
[System.IO.File]::WriteAllText($OutFile, $json, [System.Text.UTF8Encoding]::new($false))
Write-Host "Payload generado: $OutFile"
