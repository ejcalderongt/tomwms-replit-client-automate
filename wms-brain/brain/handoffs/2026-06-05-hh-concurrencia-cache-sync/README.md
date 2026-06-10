# HH Concurrencia + Cache Sync + Validación SQL Reutilizable (2026-06-05)

## Objetivo

Estandarizar diagnóstico y blindaje de desfasajes por concurrencia/estado en memoria en procesos HH, evitando repetir consultas SQL ad hoc y reglas manuales por cada incidente.

## Alcance

- Recepción HH (`frm_recepcion_datos`)
- Picking HH (`frm_picking_datos`)
- Verificación HH (`frm_verificacion_datos`)
- Patrones de validación BD para casos operativos (scan/lista/guardado)

## Fixes seguros aplicados (HH)

### Recepción

Archivo:
- `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java`

Tags:
- `#EJC20260605_TRACE_REC_CONCURRENCIA`
- `#EJC20260605_FIX_REC_CONC_REFRESH_UI`
- `#EJC20260605_FIX_REC_CACHE_SYNC`

Cambios:
- Trazas de concurrencia (`WMS.REC_CONC`) en carga cache/refresh WS, validación, guardado y post-guardado.
- Detección de error de concurrencia refrescable y mensaje operativo con recarga in-place.
- Recalculo defensivo de pendientes desde memoria actual (`gl.gpListDetalleOC`) antes de validar/guardar.
- Sincronización explícita de `gl.gselitem.Cantidad_recibida` tras guardado exitoso.

Riesgo que mitiga:
- Validar contra snapshot viejo y permitir intentos inválidos bajo doble HH.

### Recepción (carga UI por partes / re-render)

Archivo:
- `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java`

Tag:
- `#EJC20260605_FIX_REC_UI_CARGA_SUAVE`

Cambios:
- Se inicializa `RecyclerView` de caja master una sola vez en `onCreate`.
- Se reutiliza `adapterCm` con `refreshItems(...)` en refresh de detalle (CM, memoria y limpiar lista), evitando reinstanciar adapter/layoutManager.
- Se movió la configuración visual de modo `Control_Talla_Color` antes de disparar carga WS inicial para evitar parpadeo por ocultar/mostrar después.
- Se eliminó `progress.show/cancel` cosmético al final de `FinalizaCargaProductos` (validación de impresora) para evitar flicker.

Riesgo que mitiga:
- Sensación de “pantalla cargando por partes” por reconstrucción repetida de lista y toggles de visibilidad tardíos.

### Recepción (lista de tareas: SKU + licencia pallet en una sola resolución)

Archivos:
- `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_lista_tareas_recepcion.java`
- `TOMWMS/TOMIMSV4/DAL/Transacciones/OrdenCompra/OC_Detalle/clsLnTrans_oc_det_Partial.vb`

Tags:
- `#EJC20260605_FIX_REC_OC_UNIFICA_SKU_LP`
- `WMS.REC_MHS_SCAN`

Cambio:
- Se documentó y aplicó resolución de OC en una sola llamada para escaneo desde lista de tareas:
  - por SKU/código (`trans_oc_det` + `VW_ProductoSI`)
  - por licencia pallet (`i_nav_barras_pallet.Codigo_Barra`)
- Se implementó con `EXISTS` en DAL para evitar duplicados de un `JOIN` directo.
- Se mantuvo fallback HH temporal a `Get_All_Pallet_Ingreso_By_Barra` como red de seguridad operativa.

Riesgo que mitiga:
- Doble roundtrip por escaneo en MHS (latencia + variabilidad de resultados).

### Picking

Archivo:
- `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Picking/frm_picking_datos.java`

Tag:
- `#EJC20260605_FIX_PICK_CONC_REFRESH`

Cambios:
- Detección de errores de concurrencia de negocio (producto/línea ya actualizada por otra terminal).
- Mensaje operativo con ExDialog y salida controlada para forzar recarga de contexto.

Riesgo que mitiga:
- Continuar proceso con contexto obsoleto en pantalla de picking datos.

### Verificación

Archivo:
- `TOMHH2025/app/src/main/java/com/dts/tom/Transacciones/Verificacion/frm_verificacion_datos.java`

Tag:
- `#EJC20260605_FIX_VERIF_CONC_REFRESH`

Cambios:
- Detección de errores de concurrencia equivalentes a picking.
- Mensaje operativo y cierre limpio para recargar estado actualizado.

Riesgo que mitiga:
- Reproceso de líneas ya operadas en paralelo.

## Matriz de riesgo de desfasaje (estado en memoria vs BD)

| Proceso | Síntoma típico | Punto vulnerable | Mitigación mínima segura |
|---|---|---|---|
| Recepción | "cantidad excede" tardío o inconsistente | validación local con objeto viejo | refrescar + recalcular pendiente desde lista viva antes de guardar |
| Picking | línea parece pendiente pero ya fue operada | selección/guardado tras operación paralela | detectar conflicto y recargar flujo |
| Verificación | marca/cantidad no coincide con última operación | callback con estado antiguo | detectar conflicto y cerrar/recargar |

## Estructura reutilizable de validación SQL (no repetir queries)

Regla: usar bloques fijos por proceso y sólo parametrizar `IdRecepcionEnc/IdPickingEnc/IdPedidoEnc/IdProductoBodega`.

### Plantilla A - Estado de línea operativo (recepción)

```sql
-- A1) Resumen por línea OC
SELECT
  d.IdOrdenCompraDet,
  d.Cantidad AS cantidad_total,
  d.Cantidad_Recibida AS cantidad_recibida,
  (d.Cantidad - d.Cantidad_Recibida) AS cantidad_pendiente
FROM trans_oc_det d
WHERE d.IdOrdenCompraEnc = @IdOrdenCompraEnc;

-- A2) Detalle por lote/licencia (si aplica)
SELECT
  l.IdOrdenCompraDet,
  l.Lic_Plate,
  l.Codigo_Producto,
  l.Cantidad,
  l.Cantidad_Recibida,
  (l.Cantidad - l.Cantidad_Recibida) AS pendiente_lote
FROM trans_oc_det_lote l
WHERE l.IdOrdenCompraEnc = @IdOrdenCompraEnc
ORDER BY l.IdOrdenCompraDet, l.Lic_Plate;
```

### Plantilla B - Estado de línea operativo (picking/verificación)

```sql
SELECT
  p.IdPedidoEnc,
  p.IdPickingEnc,
  p.IdPedidoDet,
  p.IdStock,
  p.danado_picking,
  p.danado_verificacion,
  p.no_encontrado,
  p.cantidad_solicitada,
  p.cantidad_recibida,
  p.cantidad_verificada,
  p.fecha_packing,
  p.lic_plate,
  p.lote
FROM trans_picking_ubic p
WHERE p.IdPickingEnc = @IdPickingEnc
ORDER BY p.IdPedidoEnc, p.IdPedidoDet, p.IdStock;
```

### Plantilla C - Correlación de completitud por caja master/licencia

```sql
SELECT
  l.IdOrdenCompraDet,
  l.Lic_Plate,
  SUM(l.Cantidad) AS qty_total,
  SUM(l.Cantidad_Recibida) AS qty_recibida,
  SUM(l.Cantidad - l.Cantidad_Recibida) AS qty_pendiente
FROM trans_oc_det_lote l
WHERE l.IdOrdenCompraEnc = @IdOrdenCompraEnc
GROUP BY l.IdOrdenCompraDet, l.Lic_Plate
ORDER BY l.IdOrdenCompraDet, l.Lic_Plate;
```

## Criterio operativo para no repetir análisis

1. Ejecutar Plantilla A/B/C según proceso.
2. Correlacionar con traza logcat del tag del proceso (`WMS.REC_CONC`, `WMS.PICK_CONC`, `WMS.VERIF_CONC`).
3. Si BD y UI difieren, la acción segura es recargar contexto y no continuar operación local.
4. Sólo si BD está inconsistente, levantar caso de saneamiento de datos (sin fix HH adicional).

## Checklist corto para nuevos incidentes de concurrencia

1. ¿Hubo doble HH sobre misma línea/licencia?
2. ¿La pantalla validó con snapshot viejo?
3. ¿Se disparó mensaje de recarga (ExDialog) o cayó en error genérico?
4. ¿La consulta SQL plantilla confirma que ya estaba operado?
5. ¿Se requiere saneamiento de datos o sólo recarga/flujo?

## Notas de diseño

- Política actual: preferir "fail-safe con recarga" frente a intentar auto-merge de estados en memoria.
- No se toca lógica de negocio DAL/WS en este lote; el endurecimiento es en frontera HH para evitar reproceso con contexto obsoleto.
