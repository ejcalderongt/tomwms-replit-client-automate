# CP-002 — frmMovimiento_Reporte comment "(Por error en el cambio de ubicación fecha_vence = now -> JP)"

> Comment-pointer en el reporte fiscal que apunta a un bug histórico atribuido a "JP": al hacer un cambio de ubicación de mercadería, la fecha de vencimiento del producto se sobreescribía con `Now` (fecha actual) en lugar de mantenerse el vencimiento real. Esto genera divergencia entre el lote en BD y el lote en el reporte.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-002-frmmovreporte-fecha-vence-now-jp |
| Tipo | comment-pointer (no es hardcode ejecutable, es comentario de código) |
| Estado | documentado (síntoma) — pendiente confirmar bug raíz |
| Severidad estimada | alta |
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Fiscales/frmMovimiento_Reporte.vb` |
| Línea | 126 (comment), dentro del bloque de cascada de matching L113-148 |
| Persona referenciada | "JP" (iniciales — pendiente identificar quién) |
| Fecha estimada de aparición | desconocida (probablemente 2018-2020 cuando se construyó la cascada) |

## Cita textual

`frmMovimiento_Reporte.vb:122-131`:

```vb
If Idx1 = -1 Then 'No coincide la fecha de vencimiento para el mismo lote en el mismo movimiento
    '(Por error en el cambio de ubicación fecha_vence = now -> JP.)
    Debug.Print("Espera")
    'Magia por EJC para corregir cagada.
    If RepMovEnUnaFecha(Idx).Fecha_Vence.Date > BeStockEnFecha.Fecha_Vence.Date Then
        'BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date
        Debug.Print(BeStockEnFecha.Codigo)
    End If
End If
```

## Reconstrucción del bug histórico

1. **Operación involucrada**: cambio de ubicación de mercadería (probablemente desde la HH Android, función "cambiar ubicación" en la operatoria de UBIC).

2. **Bug que dejó "JP"**: al hacer el cambio de ubicación, la `Fecha_Vence` del registro destino quedaba seteada en `Now()` (fecha y hora del momento del cambio), en lugar de heredarse desde el registro origen.

3. **Consecuencia en `trans_movimientos`**: para el mismo `IdProducto + IdLote`, aparecen registros con `Fecha_Vence` distintas — la real y la `Now()` del momento del cambio. El cardex queda fragmentado en dos puntos del balance que no deberían existir como dos puntos.

4. **Detección en el reporte**: cuando el reporte fiscal arma `RepMovEnUnaFecha`, encuentra dos entradas para el mismo `Codigo + IdEstadoOrigen + Lote` pero con `Fecha_Vence` distintas. La cascada de matching del reporte intenta reconciliarlas — pero el bloque de fix (L128-130) está **comentado**. Solo escribe `Debug.Print` y sigue.

5. **Estado actual**: el reporte detecta la inconsistencia (Debug.Print en consola), pero no la corrige. La operación de cambio de ubicación, si el bug aún ocurre, sigue introduciendo el mismo problema.

## Por qué importa

Este case-pointer es el **único rastro documentado** del bug en el código fuente. Sin este comment, el bug sería invisible — los datos en `trans_movimientos` se ven coherentes individualmente; solo la lectura agregada del reporte fiscal lo expone.

Si el bug de "JP" sigue vigente:
- Cualquier reporte que agrupe por `Codigo + Lote + Fecha_Vence` (no solo el fiscal) ve los datos fragmentados.
- El control de póliza puede verse afectado porque la `Fecha_Vence` es uno de los ejes de identidad del lote para clientes con póliza.
- El balance teórico calculado por el reporte fiscal puede tener gaps falsos derivados de esta fragmentación.

## Acción forense

### Identificar la operación de cambio de ubicación

Buscar en el código backend:

```bash
rg -n "Cambio.*Ubicac|Cambiar.*Ubicac|UbicacionCambio" /tmp/repos/TOMWMS_BOF/TOMIMSV4/
```

Localizar la función que hace UPDATE/INSERT a `trans_movimientos` con TipoTarea relacionada a UBIC. Inspeccionar si setea `Fecha_Vence = Now()` o si la hereda correctamente.

### Detectar evidencia en BD productiva

Query propuesta (a agregar a `tools/case-seed/queries/data-discrepancy/`):

```sql
-- 09_fecha_vence_now_jp_bug.sql
-- Detectar lotes con Fecha_Vence sospechosamente == Fecha del movimiento
-- (heuristica: si Fecha_Vence == Fecha del movimiento al minuto, probable bug JP)

SELECT TOP 100
    M.IdProducto,
    P.Codigo,
    M.IdLote,
    M.Fecha,
    M.Fecha_Vence,
    DATEDIFF(SECOND, M.Fecha, M.Fecha_Vence) AS DeltaSegundos,
    M.TipoTarea,
    M.IdMovimiento
FROM trans_movimientos M
INNER JOIN producto P ON P.IdProducto = M.IdProducto
WHERE ABS(DATEDIFF(SECOND, M.Fecha, M.Fecha_Vence)) < 60   -- Fecha_Vence al mismo minuto que el movimiento
  AND M.TipoTarea IN ('UBIC', 'UBIC1', 'UBIC2')             -- ajustar segun tTipoTarea real
ORDER BY M.Fecha DESC
```

(Ajustar nombres de columnas y valores según schema del cliente y enum real de `tTipoTarea`.)

## Bitácora de debug

Ver `brain/debuged-cases/CP-002.md` para status + findings_log + resolución (cuando aparezca).

## Pendientes

- [ ] Identificar quién es "JP" (iniciales).
- [ ] Localizar la función backend de cambio de ubicación.
- [ ] Verificar si el bug aún ocurre (si sí, fix; si no, marcar como solved en bitácora).
- [ ] Decidir: si el bug sigue vigente, ¿des-comentar la línea de fix `BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date` o atacar el bug raíz?

## Cross-refs

- `00-INDEX.md` — inventario de case-pointers
- `03-frmmovreporte-magia-ejc.md` — CP-003, mismo bloque (la "Magia por EJC" es la respuesta intentada al bug de JP)
- `dataway-analysis/04-ecuacion-de-balance/granularidad-y-keys.md` — análisis de la cascada de matching del reporte fiscal
- `dataway-analysis/04-ecuacion-de-balance/divergencia-reportes-paralelos.md` — divergencia 1 (lógica de matching)
- `brain/debuged-cases/CP-002.md` — bitácora de status
