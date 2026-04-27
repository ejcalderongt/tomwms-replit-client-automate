# Stats globales del catálogo TOMWMS_KILLIOS_PRD

> Snapshot: 2026-04-27
> Source: `TOMWMS_KILLIOS_PRD` @ `52.41.114.122,1437`
> Extractor: `brain/sql-catalog/extract_for_db_brain.mjs`

## Conteos por tipo

| Tipo SQL | Cantidad |
|---|---:|
| `USER_TABLE` | 345 |
| `VIEW` | 220 |
| `SQL_STORED_PROCEDURE` | 39 |
| `SQL_SCALAR_FUNCTION` | 17 |
| **TOTAL** | **621** |

## Top 15 tablas por filas

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
| `i_nav_ped_traslado_det` | 14.953 | 2025-03-18 |
| `trans_pe_det` | 14.819 | 2024-07-02 |
| `trans_picking_det` | 14.585 | 2023-10-30 |
| `bodega_ubicacion` | 9.510 | 2024-07-02 |
| `producto_presentacion_bk` | 8.630 | 2022-06-07 |

## Hallazgos persistentes

- **Naming mixto**: convive `t_*` (heredado, ej. `t_producto_bodega`) con sin-prefijo (`stock`, `cliente`, `trans_*`).
- **Backups en BD**: tablas con sufijo `_bk` están en producción.
- **Snapshots puntuales**: `stock_YYYYMMDD` — convención de respaldo manual.
- **Typo en producción**: `ajuste_tipo.momdifica_vencimiento`. Ver `parametrizacion/flags-ajuste-tipo`.
- Total dependencias `sys.sql_expression_dependencies`: 2625.
- Total FKs declaradas: 389 (la mayoría de tablas operacionales no tiene FKs).
- Total check constraints: 4.
