param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")]
    [string]$Process,
    [string]$OutPath = ""
)

$ErrorActionPreference = "Stop"

$checklists = @{
    "picking" = @(
        "Procesar una linea y confirmar que permanece visible con estado esperado (no desaparece incorrectamente).",
        "Verificar que trans_movimientos registra ubicacion destino cuando aplica.",
        "Reintentar sobre la misma licencia y validar mensaje coherente sin error de red/estado fantasma."
    )
    "cambio_ubicacion" = @(
        "Procesar movimiento valido y confirmar persistencia de IdUbicacionDestino.",
        "Reintentar y validar que no muestra 'movimiento invalido' si el movimiento ya fue aplicado correctamente.",
        "Probar caso con datos nulos/vacios y verificar manejo controlado sin NullPointerException."
    )
    "verificacion" = @(
        "Si imagen/CDN falla, la lista de productos debe cargar igual.",
        "Confirmar que Guardar/Procesar no se bloquea por recursos visuales.",
        "Validar mensajes de error con causa operativa y no tecnica."
    )
    "recepcion" = @(
        "Finalizar recepcion sin NPE en campos string opcionales.",
        "Verificar que registros guardados reflejan cantidades esperadas.",
        "Reingresar a la licencia y validar estado consistente."
    )
    "existencias" = @(
        "Consulta termina y limpia estado 'consultando'.",
        "El foco vuelve al control esperado.",
        "Se cargan todos los registros conforme filtros activos."
    )
    "inventario" = @(
        "Aplicar inventario sin freeze ni timeout visible.",
        "Validar stock/movimientos resultantes.",
        "Reconsulta inmediata consistente."
    )
    "packing" = @(
        "Procesar empaque y confirmar movimiento persistido.",
        "Reintento controlado sin duplicados.",
        "Pantalla refresca estado correcto."
    )
}

$lines = New-Object System.Collections.Generic.List[string]
$lines.Add("process: $Process")
$lines.Add("tag: '#EJC20260527'")
$lines.Add("verification_checklist:")
foreach ($item in $checklists[$Process]) {
    $lines.Add("  - ""$item""")
}
$lines.Add("result: pending")

if ([string]::IsNullOrWhiteSpace($OutPath)) {
    $lines | Out-String
}
else {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) {
        New-Item -ItemType Directory -Path $dir | Out-Null
    }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
}
