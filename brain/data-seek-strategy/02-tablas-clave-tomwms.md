# 02 — Tablas clave TOMWMS .NET

Catálogo de las tablas que más usamos en investigación, con columnas relevantes y trampas conocidas. La estructura puede variar levemente por versión del WMS; verificar siempre con `sp_help <tabla>` o `sys.columns` antes de asumir.

---

## Maestros

### `producto`
PK `IdProducto`. Cols clave: `codigo`, `nombre`, `IdMarca`, `activo`.

### `producto_bodega`
PK `IdProductoBodega`. Une producto a bodega. Cols: `IdProducto`, `IdBodega`, `activo`.
**Importante:** muchos joins van por `IdProductoBodega` (no por `IdProducto`). El producto puede existir en varias bodegas con presentaciones distintas.

### `producto_presentacion`
Define unidades de empaque por producto. Cols: `IdProducto`, `IdPresentacion`, `factor` (UM por presentación), `nombre`.
**Trampa:** un producto puede tener múltiples presentaciones; si Erik dice "14 cajas" hay que confirmar `factor` antes de convertir.

### `bodega`
PK `IdBodega`. Cols: `nombre`, `activo`.

### `ubicacion`
PK `IdUbicacion`. Cols: `nombre`, `IdBodega`, `IdTipoUbicacion` (1=picking, 2=almacenaje, etc — varía por cliente).

### `usuario`
PK `IdUsuario`. Cols: `nombres`, `apellidos`, `IdRol`, `activo`.
**Trampa:** NO existe columna `usuario` (string login). Para mostrar nombre usar `nombres + ' ' + ISNULL(apellidos, '')`.

---

## Stock

### `stock` (viva)
PK `IdStock`. Cols clave: `IdProductoBodega`, `IdUbicacion`, `IdLicencia` (LP), `lote`, `cantidad`, `fecha_vence`, `activo`.
**Trampa:** filtrar SIEMPRE por `activo=1`. Hay rows con `activo=0` que son stock retirado/reclasificado.
**Cuidado en producción:** agregá `WITH (NOLOCK)` para no bloquear el WMS.

### `stock_res` (reservas vivas contra stock)
Cols: `IdStock`, `cantidad`, `IdTransaccion`, `Indicador` (PED=pedido, UBI=ubicación), `estado` (UNCOMMITED, PICKEADO, VERIFICADO, vacío).
**Trampa:** NO tiene columna `activo`. Para saber si una reserva está viva hay que joinear contra `stock` y filtrar por `stock.activo=1`. Una reserva contra un stock inactivo es zombi.

### `stock_hist` (snapshots)
Cols: `IdStock`, `cantidad`, `fecha_agr`, `usuario_agr`, `IdTipoTarea`. Cada row es un snapshot del valor de `stock.cantidad` después de un movimiento.
**Trampa CRÍTICA:** NO es delta, es el saldo resultante. Restar entre rows consecutivos del mismo IdStock para obtener el delta real. Killios usa user 18 = "Auditoría" para muchas escrituras automáticas.

### `stock_jornada`
Snapshot diario. **Killios NO la usa** (parámetro de configuración false, tabla vacía). Verificar antes de basar análisis en ella.

### `producto_bodega_stock`
Resumen por producto+bodega. Útil para totales agregados rápidos sin sumar `stock`. Tener cuidado con sincronización: si el bug del cliente es que no se descuenta correctamente, esta tabla también va a estar mal.

### `VW_Stock_Resumen` (vista)
Computa `stock.cantidad − SUM(stock_res.cantidad)` por producto. Útil para detectar saldo neto negativo. **No siempre incluye filtros razonables** — leer la vista antes de usarla; varía por cliente.

---

## Transaccionales

### `trans_movimientos`
La tabla más importante. Una row por movimiento de stock. Cols clave:

- `IdMovimiento` (PK)
- `IdProductoBodega`, `IdLicencia`, `IdUbicacionOrigen`, `IdUbicacionDestino`, `lote`
- `cantidad` (delta, signo según tipo de tarea)
- `cantidad_hist` (snapshot del saldo después del movimiento — NO es delta)
- `IdTipoTarea` (ver glosario `06`)
- `fecha_agr`, `usuario_agr` (NO `fec_agr`/`user_agr` — usar nombres completos)
- `referencia` (string libre, ej: número de pedido)
- `IdStock_Origen`, `IdStock_Destino`

**Tipos de tarea más usados (verificar por cliente):**
| Id | Sigla | Significado                  |
|---:|:------|:-----------------------------|
| 1  | RECE  | Recepción                    |
| 2  | UBIC  | Ubicación post-recepción     |
| 3  | CEST  | Cambio de estado (BUEN/MAL)  |
| 5  | DESP  | Despacho                     |
| 6  | INVE  | Inventario / conteo formal   |
| 8  | PIK   | Picking                      |
| 11 | VERI  | Verificación pre-despacho    |
| 17 | AJCANTN | Ajuste de cantidad         |
| 20 | EXPLOSION | Explosión de bulto       |
| 25 | REEMP_BE_PICK | Reemplazo en picking |

### `trans_picking_ubic`
Detalle de pickings por ubicación. Cols clave:

- `IdPickingUbic`, `IdPickingEnc`, `IdPickingDet`
- `IdProductoBodega`, `lic_plate` (NO `IdLicencia`), `lote`, `IdUbicacion`, `IdRecepcion`, `IdPedidoEnc`, `IdPedidoDet`
- `cantidad_solicitada`, `cantidad_recibida`, `cantidad_verificada`, `cantidad_despachada`
- `dañado_picking` (BIT) — flag del bug famoso de CP-013
- `dañado_verificacion` (BIT) — flag análogo en etapa de verificación; menos visto en Killios pero conviene chequear
- `encontrado` (BIT), `no_encontrado` (BIT)
- `activo` (BIT) — en Killios siempre 1, nunca se anula
- `IdOperadorBodega_Pickeo`, `IdOperadorBodega_Verifico`, `IdOperadorBodega_Asignado` (0 = procesado desde BOF, no HH)
- Fechas por etapa: `fecha_picking`, `fecha_verificado`, `fecha_packing`, `fecha_despachado` (útiles para reconstruir tiempos por sub-etapa)
- `user_agr`, `fec_agr` (NO `fecha_agr`/`usuario_agr` — esto es solo para `trans_movimientos`)

**Trampa:** si `dañado_picking=1, cantidad_verificada=0, cantidad_despachada=0, activo=1` → es el síntoma del bug CP-013. Ver `audit-danados-sin-ajuste.py`.

### `trans_ajuste_enc` / `trans_ajuste_det`
Encabezado y detalle de ajustes manuales (los que un usuario hace desde el BOF cuando encuentra diferencias).

`trans_ajuste_enc`:
- `idajusteenc` (PK), `fecha`, `idusuario`, `idbodega`, `referencia`, `ajuste_por_inventario` (BIT — true si fue parte de un INVE formal, false si fue manual aislado)

`trans_ajuste_det`:
- `idajustedet` (PK), `idajusteenc`, `IdProductoBodega`, `lic_plate`, `lote_original`
- `cantidad_original`, `cantidad_nueva` (el delta es `cantidad_nueva − cantidad_original`)
- `idtipoajuste` (3=Positivo, 5=Negativo, 6=Estado)
- `idmotivoajuste` (FK a `ajuste_motivo`)
- `observacion` (string libre)

**Trampas:**
- `trans_ajuste_det` NO tiene fecha propia, hay que joinear con `enc.fecha`.
- `cantidad_original` puede tener overflow corruptos (ver CP-013 idajustedet=638 con 4.8B UM). Correr `audit-overflow.py` antes de agregar.
- "Ajuste por inventario" (`ajuste_por_inventario=1`) viene de un INVE formal; los manuales son `=0`.

### `ajuste_motivo`
PK `idmotivoajuste`. Cols: `nombre`. Motivos típicos vistos: "Error en digitación", "Falla de sistema", "Despacho Licencia", "Ajuste contra fisico", "Error recepción". Varían por cliente.

### `trans_recepcion_enc/det`, `trans_despacho_enc/det`, `trans_inventario_enc/det`
Encabezado/detalle por flujo. Mismo patrón que ajustes. Útiles para filtrar por número de pedido o cargar contexto.

---

## Cómo identificar tablas nuevas que no estén acá

```sql
SELECT TOP 30 t.name, p.rows
FROM sys.tables t
JOIN sys.partitions p ON p.object_id = t.object_id AND p.index_id IN (0,1)
WHERE p.rows > 0
ORDER BY p.rows DESC;

-- y para columnas de una tabla específica:
EXEC sp_help 'trans_picking_ubic';
```

Cuando descubras una tabla nueva relevante, agregala a este doc con sus trampas.
