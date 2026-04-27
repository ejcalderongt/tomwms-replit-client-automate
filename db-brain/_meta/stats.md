# Stats globales del catálogo Killios

> Snapshot: 2026-04-27
> Source: `TOMWMS_KILLIOS_PRD` @ `52.41.114.122,1437`

## Conteos por tipo

| Tipo SQL | Cantidad |
|---|---:|
| `USER_TABLE` | 345 |
| `VIEW` | 220 |
| `SQL_STORED_PROCEDURE` | 39 |
| `SQL_SCALAR_FUNCTION` | 17 |
| **TOTAL** | **621** |

> Triggers (`TR`) y funciones inline/table-valued (`IF`/`TF`) no incluidos en este snapshot inicial. Se agregan si aparecen en algún caso del `wms-brain`.

> WikiHub Portal reporta "542 objetos BD" — la diferencia es que WikiHub no cuenta vistas o usa otro filtro. El número canónico ahora es 621.

## Top 10 tablas por filas

| Tabla | Filas | Modify date |
|---|---:|---|
| `trans_re_det_lote_num` | 180.181 | 2022-12-17 |
| `trans_movimientos` | 81.641 | 2024-07-02 |
| `log_error_wms` | 66.339 | 2025-06-12 |
| `t_producto_bodega` | 42.357 | 2019-05-21 |
| `trans_picking_ubic` | 26.567 | 2025-07-16 |
| `i_nav_transacciones_out` | 24.193 | 2024-09-18 |
| `trans_pe_det_log_reserva` | 22.576 | 2024-02-01 |
| `trans_picking_ubic_stock` | 20.437 | 2025-04-21 |
| `trans_despacho_det` | 19.799 | 2025-02-11 |
| `stock_hist` | 19.225 | 2021-06-03 |

## Hallazgos clave del snapshot

- **Naming mixto**: convive `t_*` (heredado, ej. `t_producto_bodega` 42k filas, modify_date 2019) con sin-prefijo (`stock`, `cliente`, `trans_*`).
- **Sin FKs declaradas** en las tablas operacionales del modelo de lotes (validado 2026-04-27 en familia `trans_*_lote_*`). Integridad por convención del DAL VB.NET. Ver `wms-brain://entities/rules/rule-no-fk-en-trans`.
- **Backups en BD**: tablas con sufijo `_bk` (ej. `producto_presentacion_bk` 8.6k filas) — no es estructura efímera, está en producción.
- **Snapshots puntuales**: tablas `stock_YYYYMMDD` (ej. `stock_20250624` 3.5k filas) — convención de respaldo manual.
- **Typo en producción**: `ajuste_tipo.momdifica_vencimiento` (debería ser `modifica_*`). NO tocar sin coordinar con EJC — puede haber código que lee la columna por nombre exacto.

## Densidad de comportamiento configurable (flags bit)

| Maestro | # flags bit | Notas |
|---|---:|---|
| `bodega` | ~57 | El maestro más configurable del WMS. Ver `parametrizacion/flags-bodega`. |
| `producto` | 17 | Incluye `control_lote`, `genera_lote`, `control_vencimiento`, etc. |
| `cliente` | 9 | Incluye `despachar_lotes_completos`, `control_ultimo_lote`. |
| `ajuste_tipo` | 5 | Incluye el typo `momdifica_vencimiento`. |

> No existen tablas `config_*` ni `parametros` en la BD (validado 2026-04-27). El SKILL viejo las mencionaba — afirmación incorrecta. La parametrización vive **embebida en los maestros como columnas bit**.
