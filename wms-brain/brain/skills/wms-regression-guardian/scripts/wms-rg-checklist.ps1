param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("picking","cambio_ubicacion","verificacion","recepcion","existencias","inventario","packing")]
    [string]$Process,
    [string]$OutPath = ""
)

$ErrorActionPreference = "Stop"

$checks = @{
    "picking" = @(
        "Verificar que linea corregida no desaparece incorrectamente tras procesar.",
        "Confirmar habilitacion correcta de acciones segun estado (no bloquear en Procesado/Verificado, bloquear en Despachado).",
        "Validar que movimiento asociado guarda destino y no deja nulos."
    )
    "cambio_ubicacion" = @(
        "Confirmar persistencia de IdUbicacionDestino en trans_movimientos.",
        "Reintento no debe disparar error engañoso si el movimiento ya se aplico.",
        "Casos nulos/vacios deben ser controlados sin excepcion."
    )
    "verificacion" = @(
        "Fallo de imagen/CDN no debe impedir carga de datos.",
        "Reproceso de linea no verificada funciona sin bloquear flujo.",
        "Mensajeria operativa coherente y accionable."
    )
    "recepcion" = @(
        "Finalizacion sin NullPointer/NullReference en campos opcionales.",
        "Datos guardados consistentes con cantidades de entrada.",
        "Reconsulta inmediata refleja estado actualizado."
    )
    "existencias" = @(
        "Estado de consulta se limpia al finalizar.",
        "Foco vuelve al input esperado.",
        "Carga completa conforme filtros sin truncamiento inesperado."
    )
    "inventario" = @(
        "Aplicacion de inventario no pierde lineas ni estado visual.",
        "Stock/movimientos resultantes consistentes.",
        "Reintento controlado sin duplicados."
    )
    "packing" = @(
        "Empaque procesa sin perder asignacion de ubicacion destino.",
        "Lineas pendientes/confirmadas reflejan estado real.",
        "Sin errores de concurrencia al reintentar."
    )
}

$lines = New-Object System.Collections.Generic.List[string]
$lines.Add("process: $Process")
$lines.Add("tag: '#EJC20260527'")
$lines.Add("checklist:")
foreach ($c in $checks[$Process]) { $lines.Add("  - '$c'") }
$lines.Add("result: pending")

if ($OutPath) {
    $dir = Split-Path -Parent $OutPath
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    [System.IO.File]::WriteAllLines($OutPath, $lines, [System.Text.UTF8Encoding]::new($false))
    Write-Output "Wrote $OutPath"
} else {
    $lines | Out-String
}
