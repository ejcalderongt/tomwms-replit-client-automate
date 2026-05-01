---
id: process-map
tipo: process-flow
estado: vigente
titulo: Mapa global de procesos WMS — observado
tags: [process-flow]
---

# Mapa global de procesos WMS — observado

> **Version 2** (ciclo 9). Snapshot 2026-04-27. Conteos reales de TOMWMS_KILLIOS_PRD.
> Incorpora respuestas P-08 y P-18 de `respuestas-tarea-1.md`.

## Top 30 tablas por filas (TOMWMS_KILLIOS_PRD)

| Filas | Tabla | Proceso |
|---:|---|---|
| 180,181 | `trans_re_det_lote_num` | Inbound — auditoria escaneos lote |
| 81,641 | `trans_movimientos` | Movimiento — kardex |
| 66,339 | `log_error_wms` | Errores generales |
| 42,357 | `t_producto_bodega` | Catalogo prod x bodega |
| 26,567 | `trans_picking_ubic` | Picking — pares pedido/ubicacion sugeridos |
| 24,193 | `i_nav_transacciones_out (outbox)` | Push ERP (i_nav_transacciones_out?) |
| 22,576 | `trans_pe_det_log_reserva` | Reserva — auditoria casos (incluye LLR) |
| 20,437 | `trans_picking_ubic_stock` | Picking — pares ubic/stock realmente tomados |
| 19,799 | `trans_despacho_det` | Despacho — lineas |
| 19,225 | `stock_hist` | Stock — historial |
| 14,953 | `i_nav_ped_traslado_det` | Inbound — interface NAV |
| 14,819 | `trans_pe_det` | Pedido — lineas |
| 14,585 | `trans_picking_det` | Picking — detalle |
| 9,510 | `bodega_ubicacion` | Catalogo ubicaciones |
| 8,630 | `producto_presentacion_bk` | Backup historico |
| 8,144 | `dh_ocupacion_bodega` | DataHouse ocupacion |
| 7,762 | `i_nav_ejecucion_enc` | Interface — encabezados |
| 7,760 | `i_nav_ejecucion_res` | Interface — resultados |
| 5,895 | `trans_picking_op` | Picking — operaciones |
| 4,703 | `stock` | Stock actual |
| 4,594 | `proveedor_bodega` | Catalogo proveedores |
| 4,540 | `trans_inv_stock` | Inventario — stock |
| 4,394 | `trans_re_det` | Inbound — detalle |
| 4,394 | `stock_rec` | Stock recibido |
| 4,237 | `i_nav_ped_traslado_enc` | Inbound — interface NAV |
| 4,202 | `trans_pe_enc` | Pedido — encabezados |
| 4,032 | `trans_despacho_enc` | Despacho — encabezados |
| 4,021 | `i_nav_ejecucion_det_error` | Interface — errores |
| 3,701 | `marcaje` | Handheld — marcajes |
| 3,541 | `stock_20250624` | **SNAPSHOT MANUAL** (deuda) |

## Catalogos de tipos/estados

| Tabla | Filas | Notas |
|---|---:|---|
| `producto_estado` | 18 | Estados del producto (P-06 pendiente) |
| `tipo_rotacion` | 4 | FIFO/LIFO/FEFO + 1 mas |
| `trans_oc_estado` | 6 | Estados de OC (P-03 pendiente) |
| `trans_pe_tipo` | 6 | Tipos pedido (con flag ReservaStock) |
| `sis_tipo_tarea` | 35 | Tipos de tarea handheld (P-25 pendiente) |
| `sis_estado_tarea_hh` | 4 | Estados de tarea handheld |
| `ajuste_tipo` | 6 | Tipos de ajuste |
| `tipo_contenedor` | 5 | Contenedores |
| `tipo_etiqueta` | 6 | Etiquetas |
| `tipo_tarima` | 10 | Tarimas |
| `tipo_rack` | 4 | Racks |
| `cliente_tipo` | 6 | Tipos cliente |

## Por proceso (tablas observadas)

### Inbound

| Tabla | Filas | Que registra |
|---|---:|---|
| `i_nav_ped_traslado_enc` | 4,237 | encabezado interface NAV recibida |
| `i_nav_ped_traslado_det` | 14,953 | detalle interface |
| `trans_re_enc` | 576 | recepcion WMS (pocas, son OC fisicas?) |
| `trans_re_det` | 4,394 | detalle recepcion |
| `trans_re_oc` | 576 | OC vinculadas |
| `trans_re_op` | 1,644 | operaciones de recepcion |
| `trans_re_tr` | 10 | trasvases? |
| `trans_re_det_lote_num` | 180,181 | **escaneos de lote/numeracion** (gigante) |
| `trans_re_det_infraccion` | 0 | rechazos por discrepancia |
| `trans_re_det_parametros` | 0 | parametros adicionales |

### Picking / Packing / Despacho

| Tabla | Filas | Que registra |
|---|---:|---|
| `trans_picking_enc` | 1,293 | sesiones de picking |
| `trans_picking_det` | 14,585 | lineas pickeadas |
| `trans_picking_op` | 5,895 | operaciones (pasos en handheld?) |
| `trans_picking_ubic` | 26,567 | pares pedido/ubicacion sugeridos |
| `trans_picking_ubic_stock` | 20,437 | pares ubic/stock realmente tomados |
| `trans_picking_prioridad` | 0 | prioridades — sin uso |
| `trans_picking_img` | 0 | imagenes — sin uso |
| `trans_packing_enc` | **13** | **MUY pocas — verificacion opcional (R-05 confirmado)** |
| `trans_despacho_enc` | 4,032 | encabezado despacho |
| `trans_despacho_det` | 19,799 | detalle despacho |

**Discrepancia detectada (P-16 abierta)**: `trans_pe_enc.estado='Despachado'` = 3,989 vs `trans_despacho_enc` = 4,032 → diferencia de 43. ¿Son despachos sin pedido o estado no actualizado?

### Reserva

`trans_pe_enc` (4,202) → `trans_pe_det` (14,819) → motor → `stock_res` + `trans_pe_det_log_reserva` (22,576).

**Patron LLR confirmado en tarea 1** (P-10): el motor se llama recursivamente
para los casos #20→#28, #23→#29, #24→#31 cuando el stock fue modificado
durante la reserva original (conversion de unidades, movimientos internos).

### Interface ERP (SAP/NAV)

| Tabla | Filas |
|---|---:|
| `i_nav_transacciones_out` | 24,193 |
| `i_nav_ejecucion_enc` | 7,762 |
| `i_nav_ejecucion_res` | 7,760 |
| `i_nav_ejecucion_det_error` | 4,021 |
| `log_error_wms` | 66,339 |

### Stock / Movimiento

| Tabla | Filas |
|---|---:|
| `stock` | 4,703 |
| `stock_hist` | 19,225 |
| `stock_rec` | 4,394 |
| `stock_res` | (varia) |
| `trans_movimientos` | 81,641 |
| `trans_inv_stock` | 4,540 |
| `dh_ocupacion_bodega` | 8,144 |

### Reabasto

| Tabla | Filas |
|---|---:|
| `trans_reabastecimiento_log` | 1,218 |

(Killios no tiene el modulo activo segun config, pero hay 1218 registros — algo los genera. P-24 abierta.)

### Traslado interno (TRAS_WMS) — ampliado en ciclo 9

**Definicion confirmada (P-18)**: TRAS_WMS es transferencia interna entre
bodegas configuradas en WMS, donde el stock movido **ya fue reservado por
proceso previo** (manual, discrecional o flujo upstream).

**Hoy** — uso real:

```
   +--------------+      +--------------+
   |  Bodega A    | ---> |  Bodega B    |
   |  (origen)    |      |  (destino)   |
   |  stock       |      |  stock       |
   |  reservado   |      |  recibe      |
   |  upstream    |      |  reserva     |
   +--------------+      +--------------+
            |                    ^
            +-- TRAS_WMS --------+
              (sin pasar por
               motor de reserva)
```

**Deuda detectada (DEUDA-001)**: la bandera `trans_pe_tipo.ReservaStock=NO` en
TRAS_WMS no se valida explicitamente. El flujo asume reserva previa por
convencion. Riesgo: doble reserva si alguien crea TRAS_WMS sin reserva
upstream verificada.

**Vision futura (NO implementada — capacidad latente)**:

```
   +-----------------------------------------------------+
   |  TRAS_WMS como bolson/bucket de pedidos             |
   |  para abastecimiento batch                          |
   |                                                     |
   |  Politica configurable por cliente / bodega:        |
   |    Producto X →                                     |
   |      50% CDs (bodegas internas WMS)                 |
   |      30% nuevos clientes                            |
   |      20% calidad y merma                            |
   |                                                     |
   |  + Proyeccion de demanda en base a peticiones      |
   |    historicas durante un periodo de tiempo         |
   +-----------------------------------------------------+
```

Capacidades requeridas para activarlo (no implementadas):

1. Configuracion de politica por cliente y/o bodega origen/destino.
2. Job batch que procese el bolson aplicando la politica.
3. Analytics de demanda historica.
4. Validacion de capacidad disponible en bodegas destino.

### SP por proceso

Los SPs no usan prefijos consistentes (`sp_recep%`, `sp_pedido%`, etc. devolvieron 0). El agrupamiento sera por exploracion individual en ciclos posteriores.

---

## Referencias cruzadas

- Estados del pedido y transiciones: ver `state-machine-pedido.md` (v2).
- Respuestas detalladas P-08, P-10, P-18: ver `respuestas-tarea-1.md`.
- Status de las 25 preguntas: ver `preguntas-ciclo-7.md`.
