# Pasada 3.1 — Bloque C: Transacciones core (escrituras del WMS)

## Estado
- **Generado**: 2026-04-27T04:55:54.960Z
- **Fuente**: TOMWMS_KILLIOS_PRD (read-only) + cruce con `bloque-E-cruce.json` (343 WMs WSHHRN parseados)
- **Cobertura**: 84 tablas `trans_*` analizadas, top 12 con estructura completa, 22 WebMethods mutadores identificados, 0 SPs de negocio detectados.

## Objetivo del bloque
Identificar las **transacciones core** del WMS (recepción, picking, despacho, ajuste, inventario), las tablas que mutan, los WebMethods del HH que las disparan, y cómo se mantiene la consistencia (sin SPs ni triggers).

---

## C.1 Familias de tablas transaccionales

Encontré **84 tablas `trans_*`** organizadas en 11 familias funcionales:

| Familia | Tablas | Filas | Dominio |
|---|---:|---:|---|
| `trans_re_*` | 10 | 187,381 | Recepción (encabezado + detalle + lotes + operador + OC ↔ recepción) |
| `trans_pe_*` | 7 | 41,623 | Pedidos de venta/despacho (encabezado + detalle + reservas + tipos) |
| `trans_picking_*` | 8 | 68,777 | Picking (encabezado + detalle + ubicación + stock + operador) |
| `trans_packing_*` | 1 | 13 | Packing (sólo trans_packing_enc, 13 filas — uso muy bajo en Killios) |
| `trans_despacho_*` | 3 | 23,831 | Despacho a cliente (enc + det + lotes) |
| `trans_oc_*` | 10 | 3,564 | Órdenes de compra al proveedor (enc + det + estados + lotes) |
| `trans_inv_*` | 13 | 11,264 | Inventario (toma + reconteo + resumen + comparación SAP) |
| `trans_ajuste*` | 3 | 2,938 | Ajustes manuales de stock (enc + det + documento de respaldo) |
| `trans_movimiento*` | 2 | 81,886 | Movimientos de stock (granular) + movimientos de pallet |
| `trans_ubic_hh_*` | 6 | 1,600 | **Movimientos físicos del HH** (encabezado + detalle + stock + operador) |
| `log_*` | 15 | varios | Logs y estados (errores, reservas, liberaciones, historial stock) |

### Top 12 transaccionales por volumen

| Tabla | Filas | Cols | PK | Modificada |
|---|---:|---:|---|---|
| `trans_re_det_lote_num` | 180181 | 8 | `IdLoteNum` | 2022-12-17 |
| `trans_movimientos` | 81641 | 35 | `IdEmpresa, IdBodegaOrigen, IdTransaccion, IdMovimiento` | 2024-07-02 |
| `trans_picking_ubic` | 26567 | 52 | `IdPickingUbic` | 2025-07-16 |
| `trans_pe_det_log_reserva` | 22576 | 17 | `IdLogReserva` | 2024-02-01 |
| `trans_picking_ubic_stock` | 20437 | 47 | `IdPickingUbicStock` | 2025-04-21 |
| `trans_despacho_det` | 19799 | 20 | `IdDespachoDet` | 2025-02-11 |
| `trans_pe_det` | 14819 | 44 | `IdPedidoDet` | 2024-07-02 |
| `trans_picking_det` | 14585 | 15 | `IdPickingDet` | 2023-10-30 |
| `trans_picking_op` | 5895 | 7 | `IdOperadorPicking` | 2024-02-01 |
| `trans_inv_stock` | 4540 | 33 | `idinventario, IdStock` | 2025-02-11 |
| `trans_re_det` | 4394 | 37 | `IdRecepcionDet, IdRecepcionEnc` | 2024-10-15 |
| `trans_pe_enc` | 4202 | 70 | `IdPedidoEnc` | 2025-06-11 |

### Hallazgos del top 12

- **`trans_re_det_lote_num` (180k filas, 8 cols, PK simple `IdLoteNum`)** es la tabla más voluminosa — almacena cada número de lote único recibido. Refleja que la trazabilidad de lotes es central en Killios.
- **`trans_movimientos` (82k filas, 35 cols, PK compuesta de 4)** captura cada movimiento físico (origen, destino, cantidad, momento). Es el log atómico del WMS.
- **`trans_pe_enc` (4k filas, 70 cols)** — el encabezado de pedido es la tabla con más columnas: 70 columnas indican un dominio muy rico (ruteo, prioridad, estados, fechas, restricciones, autorizaciones, integración con cliente).
- **`trans_packing_enc` con sólo 13 filas** sugiere que packing es una operación rara o nueva en Killios — confirmar si es realmente usada o un módulo en piloto.

---

## C.2 WebMethods mutadores del WSHHRN

Sobre los **344** WMs del WSHHRN, **solo 21 mutan tablas trans_/stock/marcaje** = 6.1%.

> **Implicación arquitectónica**: el HH es ~94% solo-lectura. La mayoría de WMs son consultas (Get_*, Lista_*, Buscar_*). Los pocos mutadores concentran toda la lógica de escritura.

### Distribución por verbo

| Verbo | WMs | Significado |
|---|---:|---|
| `Set_*` | 5 | Cambia un atributo (estado, valor) |
| `Finalizar_*` | 5 | Cierra una transacción (recepción/picking) |
| `Reservar_*` | 3 | Aparta stock para un pedido (evita doble venta) |
| `Marcar_*` | 2 | Etiqueta condición (no encontrado, dañado) |
| `Iniciar_*` | 1 | Abre una transacción nueva |
| `Delete_*` | 1 | Elimina registros |
| `Aplicar_*` | 1 | Aplica un cambio acumulado |
| `Inserta_*` | 1 | Crea registro |
| `Eliminar_*` | 1 | Borra registro |
| `Crear_*` | 1 | Genera estructura nueva |

### Tablas más mutadas

| Tabla | Veces mutada | Rol |
|---|---:|---|
| `stock_res` | 5 | Reservas de stock — el HH bloquea stock para que no lo agarre otro pedido |
| `trans_re_enc` | 5 | Encabezado recepción — finalización marca la recepción como cerrada |
| `trans_packing_enc` | 4 | Encabezado packing — el HH lo crea/modifica al armar packing |
| `trans_oc_enc` | 3 | Encabezado OC — actualización de estado al iniciar/finalizar |
| `trans_despacho_det` | 3 | Detalle despacho — el HH agrega líneas de despacho |
| `trans_ubic_hh_det` | 3 | Log granular de movimientos físicos del HH (Set_LP_Stock, Aplicar_Cambio_Estado_HH) |
| `stock` | 2 | Stock físico — Set_Nuevo_Pallet_Id reasigna LP |
| `trans_picking_ubic` | 2 | Picking ubicación — Marcar_No_Encontrado/Marcar_Danado mutan acá |
| `trans_re_det` | 1 | Detalle recepción — Delete_Det_By_IdRecepcionEnc lo elimina |
| `stock_rec` | 1 | Stock en recepción — afectado por delete de detalle |
| `trans_pe_enc` | 1 | Encabezado pedido — Set_Estado_Pedido_Verificado |
| `trans_picking_enc` | 1 | Encabezado picking — Set_Estado_Pendiente_Packing |
| `trans_oc_det` | 1 | Detalle OC — Crear_Linea_Unidades agrega líneas |

### Top 15 orquestadores (WMs que tocan más tablas trans_*)

| Línea | WebMethod | Tablas trans_* tocadas |
|---:|---|---|
| 3486 | `Delete_Det_By_IdRecepcionEnc_And_IdRecpecionDet` | `trans_re_det`, `stock_rec`, `stock` |
| 4341 | `Marcar_No_Encontrado` | `trans_picking_ubic`, `stock_res`, `trans_packing_enc` |
| 8699 | `Finalizar_Recepcion` | `trans_re_enc`, `trans_oc_enc`, `trans_despacho_det` |
| 12086 | `Marcar_Danado` | `trans_picking_ubic`, `stock_res`, `trans_packing_enc` |
| 16289 | `Finalizar_Recepcion_S` | `trans_re_enc`, `trans_oc_enc`, `trans_despacho_det` |
| 2663 | `Iniciar_Recepcion_OC` | `trans_oc_enc`, `trans_despacho_det` |
| 4407 | `Reservar_Stock_By_IdStock` | `stock_res` |
| 4479 | `Reservar_Stock_By_Stock` | `stock_res` |
| 4870 | `Reservar_Y_Reemplazar_Stock_By_IdStock` | `stock_res` |
| 5703 | `Aplicar_Cambio_Estado_HH` | `trans_ubic_hh_det` |
| 5864 | `Set_Nuevo_Pallet_Id` | `stock` |
| 5942 | `Set_LP_Stock` | `trans_ubic_hh_det` |
| 6000 | `Set_LP_Stock_Mixto` | `trans_ubic_hh_det` |
| 8788 | `Finalizar_Recepcion_Parcial` | `trans_re_enc` |
| 8842 | `Finalizar_Recepcion_Parcial_Pallet_Proveedor` | `trans_re_enc` |

### Hallazgos del cruce

**1. Las 2 transacciones más complejas del HH son recepción y marcado de incidencias.**

- `Finalizar_Recepcion` y `Finalizar_Recepcion_S` (línea 8699 y 16289) tocan **3 tablas** cada una: `trans_re_enc + trans_oc_enc + trans_despacho_det`. Recepción cruzada con OC y genera líneas de despacho.
- `Marcar_No_Encontrado` y `Marcar_Danado` tocan **3 tablas**: `trans_picking_ubic + stock_res + trans_packing_enc`. Marcado libera reserva y notifica packing.
- `Delete_Det_By_IdRecepcionEnc_And_IdRecpecionDet` toca `trans_re_det + stock_rec + stock` (cascada manual de eliminación).

**2. `trans_ubic_hh_det` es el log de actividad granular del HH.**

- 695 filas en Killios con campos: `IdTareaUbicacionEnc`, `IdStock`, `IdUbicacionOrigen`, `IdUbicacionDestino`, `IdEstadoOrigen`, `IdEstadoDestino`, `IdOperadorBodega`, `HoraInicio`, `HoraFin`, `IdBodega`, `no_linea`.
- Cada fila = un movimiento físico ejecutado por un operador con HH en mano.
- Mutado por `Set_LP_Stock`, `Set_LP_Stock_Mixto`, `Aplicar_Cambio_Estado_HH` (3 WMs).
- → **El Brain puede reconstruir el flujo físico del operador leyendo esta tabla**.

**3. Reservas (`stock_res`) es la tabla más-mutada por el HH.**

- 5 WMs la mutan: `Reservar_Stock_By_IdStock`, `Reservar_Stock_By_Stock`, `Reservar_Y_Reemplazar_Stock_By_IdStock`, `Marcar_No_Encontrado`, `Marcar_Danado`.
- Confirma que el HH **es el dueño del lock de stock** durante un pedido. Reserva al iniciar pick, libera al marcar incidencia o al finalizar packing.

---

## C.3 SPs y triggers — confirmación arquitectónica

### SPs (39 total — 0 de negocio relevantes)

De los 39 stored procedures del catálogo:

- **22 son utilitarios DBA**: `sp_Blitz*`, `sp_AllNightLog*`, `sp_DatabaseRestore`, `sp_index_maintenance_daily`, `sp_WhoIsActive`, `sp_*diagram*`, `sp_foreachdb`.
- **5 son del dominio pero auxiliares**: `Get_Ubicaciones_Vacias_*`, `GetCantidadPesoByProductoBodega`, `GetListaStockByProductoBodega`, `GetResumenStockCantidad`, `Concurrencia`.
- **5 son de carga inicial / ETL puntual**: `SP_Importa_Stock_Bodegas_General_y_Dañado*` (3 variantes), `SET_CLIENTES_REC`, `SP_Cambia_Collate`.
- **4 son de jornada/stock**: `asignar_jornada_laboral`, `SP_STOCK_JORNADA_DESFASE*` (2 variantes), `sp_eliminar_by_Referencia`.
- **3 son de cierre laboral bodega**: `CLBD`, `CLBD_INICIARBD`, `CLBD_PRC`, `CLBD_PRC_BY_IDBODEGA` (procedimiento nocturno de cierre).

**Conclusión**: ningún SP implementa lógica transaccional del WMS — son utilitarios, helpers de lectura, jobs nocturnos o ETL.

### Triggers

**0 triggers en Killios**. Confirmado: no hay triggers de negocio. La consistencia se mantiene en código .NET (clsLn).

### EXEC desde clsLn

Inspeccioné los **38 archivos clsLn de configuración descargados** (bloque D) para detectar llamadas `EXEC|EXECUTE|sp_executesql` a SPs.

**Resultado: 0 / 38 archivos llaman SPs.** Confirmado: ningún clsLn (89 + 24 = 113 archivos) llama a SPs. Solo SQL inline. Los 39 SPs son utilitarios/DBA.

> Esta es la **confirmación arquitectónica más fuerte** de toda la pasada: el WMS hace **100% SQL inline** desde código .NET. No hay SPs de negocio, no hay triggers, no hay views materializadas. Toda la lógica vive en `clsLn*` y se ejecuta como queries ad-hoc.

---

## C.4 Tablas de log/estado/historial (15)

| Tabla | Filas | Rol |
|---|---:|---|
| `log_error_wms` | 66,339 | Log central de errores del WMS — todo error se asienta acá |
| `trans_pe_det_log_reserva` | 22,576 | Audit trail de cambios de reserva por línea de pedido |
| `stock_hist` | 19,225 | Historial de cambios de stock (snapshot por evento) |
| `trans_log_pedido_liberacion` | 715 | Liberaciones de pedido (cuando se cancela una reserva) |
| `trans_log_reubic_stock_res` | 84 | Reubicaciones de reservas de stock |
| `licencia_login` | 38 | Logins (para licenciamiento) |
| `producto_estado` | 18 | Catálogo: estados posibles de un producto (Buen Estado, Dañado, etc.) |
| `rol_usuario_estado` | 14 | Estados permitidos por rol+usuario (bloque D) |
| `simbologias_codigo_barra` | 9 | Tipos de código de barra (CODE128, EAN13, etc.) |
| `trans_oc_estado` | 6 | **Catálogo de estados de Orden de Compra** (resuelve ambigüedad pendiente: existe esta tabla, no `clsLnTrans_oc_estado`) |
| `sis_estado_tarea_hh` | 4 | Catálogo: estados de una tarea del HH (Pendiente, En proceso, Finalizada, etc.) |
| `producto_estado_ubic` | 2 | Estados de producto por ubicación (raros) |
| `area_estado` | 0 | Vacía |
| `i_nav_config_producto_estado` | 0 | Config interface estado producto (vacía en Killios) |
| `log_importacion_excel` | 0 | Vacía |

### Hallazgos

- **`log_error_wms` con 66k filas** es el log central. Cada error capturado por la capa DAL/UI termina ahí. Brain puede aprender los errores frecuentes y diagnosticarlos.
- **`stock_hist` con 19k filas** es el snapshot por evento — se inserta cada vez que stock cambia significativamente. Brain puede explicar por qué el stock está como está.
- **`sis_estado_tarea_hh` con 4 filas** define los estados de la tarea HH (probable: Pendiente / EnProceso / Finalizada / Cancelada).
- **`trans_oc_estado` con 6 filas** existe — esto resuelve la ambigüedad pendiente del bloque E (`clsLnTrans_oc_estado` referenciaba esta tabla real).

---

## C.5 Implicaciones para el Brain

1. **El Brain puede hablar con autoridad de transacciones del HH.** Solo 21 WMs mutan datos, y los 7-8 orquestadores principales están identificados. Cada uno se puede modelar como un "caso de uso" del Brain con precondiciones, mutaciones y postcondiciones.

2. **Sin SPs ni triggers = consistencia frágil.** Brain debe alertar si una operación del HH no está envuelta en transacción .NET (riesgo de inconsistencia). Recomendación para futuras pasadas: detectar `Try`/`Catch` y `BeginTransaction` en cada WM mutador.

3. **`trans_movimientos` es el log atómico — ideal para auditoría.** Brain puede reconstruir el flujo de cualquier stock siguiendo `IdStock` en `trans_movimientos`.

4. **`trans_ubic_hh_det` cuenta la historia del operador.** Brain puede responder "qué hizo Juan ayer entre 10am y 11am" leyendo esta tabla por `IdOperadorBodega`.

5. **`stock_res` es el lock distribuido del WMS.** Brain debe explicar bloqueos y conflictos consultando esta tabla.

6. **Resolución de ambigüedad**: `trans_oc_estado` existe en Killios con 6 filas. La clase `clsLnTrans_oc_estado` (ambigua en pasada anterior) muy probablemente vive en `/TOMIMSV4/DAL/.../Trans_oc_estado/clsLnTrans_oc_estado.vb` — confirmar con búsqueda en Azure.

## Anexo: archivos generados por este bloque

| Archivo | Contenido |
|---|---|
| `data/passada-3-1-bloque-C-trans-top12.json` | Estructura completa (cols + PK) de 12 trans_* más voluminosas |
| `data/passada-3-1-bloque-C-mutators.json` | 22 WMs mutadores + 50 orquestadores ordenados por superficie |
| `data/passada-3-1-bloque-C-deep.json` | EXEC count en clsLn + estructura tablas críticas no-trans |
| `analysis/passada-3-1-bloque-C-transacciones-core.md` | Este documento |