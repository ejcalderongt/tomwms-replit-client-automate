# CP-006 — frmMovimiento_Reporte breakpoint preciso `Fecha_Vence + SIN REGISTRO + DESP`

> Hardcode con triple condición que dispara `Debug.Print("Wait a second!")` solo cuando se cumplen las **tres**: `Fecha_Vence = TheGoalDate (2019-08-30)` + `EstadoOrigen = "SIN REGISTRO"` + `TipoTarea = DESP`. Es el breakpoint preciso de la trinity y comparte la condición exacta con `CP-001` del reporte estándar — son **el mismo caso histórico** debugueado en los dos reportes.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-006-frmmovreporte-breakpoint-triple |
| Tipo | hardcode (If con triple AndAlso + Debug.Print) |
| Estado | documentado — pertenece a trinity y es espejo de CP-001 |
| Severidad estimada | alta (apunta al caso del cliente afectado real) |
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Fiscales/frmMovimiento_Reporte.vb` |
| Línea | 99-101 |
| Depende de | CP-004 (declara `TheGoalDate`) |
| Espejo idéntico | `frmStockEnUnaFecha.vb:143` (tercer If del CP-001) |

## Cita textual

`frmMovimiento_Reporte.vb:99-101`:

```vb
If ObjM.Fecha_Vence = TheGoalDate AndAlso ObjM.EstadoOrigen = "SIN REGISTRO" AndAlso ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
    Debug.Print("Wait a second!")
End If
```

## Lectura

Solo dispara cuando se cumplen **las tres** condiciones simultáneamente:

1. `Fecha_Vence = 2019-08-30` (el lote del caso original)
2. `EstadoOrigen = "SIN REGISTRO"` (mercadería sin clasificar)
3. `TipoTarea = DESP` (movimiento de despacho)

Esta combinación describe **exactamente el caso patológico** que en su momento un cliente vivió: estaban despachando mercadería desde estado "SIN REGISTRO" con esa fecha de vencimiento, generando gap en el balance.

## Por qué es el case-pointer más fuerte de los 7

CP-006 es **idéntico** al tercer `If` del CP-001 (en `frmStockEnUnaFecha`). Misma condición, mismo `Debug.Print("Wait a second!")`. Esto no es coincidencia: alguien pegó la misma trampa de debug en los dos reportes porque el caso afectaba a ambos.

Implicaciones:

1. **El cliente original probablemente corría los dos reportes** — o el equipo investigaba el caso desde los dos ángulos.
2. **Si era cliente con control de póliza** (Cealsa, Idealsa, Cumbre, etc.), CP-006 es el más cercano al caso original (el reporte fiscal era el que tenía el ojo puesto en ese cliente).
3. **El bug raíz no es del reporte** — los reportes solo lo detectan. El bug está en la operación que permitió `DESP desde SIN REGISTRO` o en el flujo que dejó al producto sin clasificar.

## Hipótesis del bug raíz

Las mismas tres del CP-001:

1. **Recepción incompleta**: mercadería con SIN REGISTRO temporal, despachada antes de clasificarse.
2. **Estado borrado**: bug en cambio de estado HH borró el estado quedando como SIN REGISTRO.
3. **Importación legacy**: datos importados sin control de estado.

Para clientes con póliza, hay una cuarta hipótesis posible:

4. **Póliza pendiente de asignación**: el cliente recibió mercadería antes de tener la póliza tramitada; se asignó EstadoOrigen=SIN REGISTRO temporal hasta que llegara la póliza. Si entre tanto se despachó (legítimamente, contra otro estado), el movimiento DESP queda con EstadoOrigen=SIN REGISTRO en `trans_movimientos` aunque no debería.

## Acción forense

### Query unificada (CP-001 + CP-006)

Reusa la query 07 del CP-001 (`07_movimientos_sin_registro_desp.sql`).

Para cliente con póliza específicamente, agregar query 10:

```sql
-- 10_caso_cp006_poliza_sin_registro_desp.sql
-- Cuantos DESP se hicieron desde EstadoOrigen=SIN REGISTRO en clientes con poliza?
-- (Asumir IdLicencia o IdEmpresa identifica el cliente con poliza)

SELECT
    L.NombreLicencia,
    COUNT(*) AS CantidadMovimientos,
    MIN(M.Fecha) AS Primero,
    MAX(M.Fecha) AS Ultimo,
    SUM(M.Cantidad) AS TotalCantidad
FROM trans_movimientos M
INNER JOIN producto P ON P.IdProducto = M.IdProducto
INNER JOIN licencia L ON L.IdLicencia = M.IdLicencia
LEFT JOIN producto_estado EO ON EO.IdEstado = M.IdEstadoOrigen
WHERE M.TipoTarea = 'DESP'
  AND EO.Descripcion = 'SIN REGISTRO'
GROUP BY L.NombreLicencia
ORDER BY CantidadMovimientos DESC
```

(Ajustar nombres según schema. El nombre `licencia.NombreLicencia` puede ser otro.)

## Bitácora de debug

Ver `brain/debuged-cases/CP-006.md`. Linkear con `CP-001.md` porque comparten el caso de fondo.

## Cross-refs

- `00-INDEX.md`
- `01-stockfecha-codigo-030772033524.md` — CP-001 (espejo idéntico en estándar)
- `04-frmmovreporte-thegoaldate-declaracion.md` — CP-004 (proveedor de la fecha)
- `05-frmmovreporte-breakpoint-fecha.md` — CP-005 (vista panorámica)
- `dataway-analysis/04-ecuacion-de-balance/divergencia-reportes-paralelos.md` — contexto del reporte fiscal con control de póliza
- `brain/debuged-cases/CP-006.md`
