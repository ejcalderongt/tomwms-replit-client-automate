# Index — Tablas

> Snapshot 2026-04-27: **5 sample tables** publicadas. Quedan **340** por extraer en commit posterior.

## Sample tables (esta versión)

| Tabla | Filas | Status | Entity |
|---|---:|---|---|
| `cliente` | 1.083 | activa | [cliente.md](./cliente.md) |
| `producto` | 319 | activa | [producto.md](./producto.md) |
| `stock` | 4.703 | activa | [stock.md](./stock.md) |
| `cliente_lotes` | 0 | vacia (feature WIP) | [cliente_lotes.md](./cliente_lotes.md) |
| `log_importacion_excel` | 0 | vacia (sin uso desde 2022) | [log_importacion_excel.md](./log_importacion_excel.md) |

## Top 30 por filas (de las 345 totales)

1. `trans_re_det_lote_num` — 180.181 filas
2. `trans_movimientos` — 81.641 filas
3. `log_error_wms` — 66.339 filas
4. `t_producto_bodega` — 42.357 filas
5. `trans_picking_ubic` — 26.567 filas
6. `i_nav_transacciones_out` — 24.193 filas
7. `trans_pe_det_log_reserva` — 22.576 filas
8. `trans_picking_ubic_stock` — 20.437 filas
9. `trans_despacho_det` — 19.799 filas
10. `stock_hist` — 19.225 filas
11. `i_nav_ped_traslado_det` — 14.953 filas
12. `trans_pe_det` — 14.819 filas
13. `trans_picking_det` — 14.585 filas
14. `bodega_ubicacion` — 9.510 filas
15. `producto_presentacion_bk` — 8.630 filas
16. `dh_ocupacion_bodega` — 8.144 filas
17. `i_nav_ejecucion_enc` — 7.762 filas
18. `i_nav_ejecucion_res` — 7.760 filas
19. `trans_picking_op` — 5.895 filas
20. `stock` — 4.703 filas
21. `proveedor_bodega` — 4.594 filas
22. `trans_inv_stock` — 4.540 filas
23. `trans_re_det` — 4.394 filas
24. `stock_rec` — 4.394 filas
25. `i_nav_ped_traslado_enc` — 4.237 filas
26. `trans_pe_enc` — 4.202 filas
27. `trans_despacho_enc` — 4.032 filas
28. `i_nav_ejecucion_det_error` — 4.021 filas
29. `marcaje` — 3.701 filas
30. `stock_20250624` — 3.541 filas

## Familias detectadas (heurística por prefijo/sufijo)

| Familia | Patrón | Ejemplos |
|---|---|---|
| Stock | `stock`, `stock_*`, `stock_YYYYMMDD` | stock, stock_hist, stock_jornada, stock_picking, stock_20250624 |
| Transacciones | `trans_*` | trans_oc_*, trans_re_*, trans_despacho_*, trans_ajuste_*, trans_picking_*, trans_movimientos |
| Lotes (dedicadas) | mezcla | trans_re_det_lote_num, trans_oc_det_lote, trans_despacho_det_lote_num, cliente_lotes, i_nav_*_lote |
| Maestros | sin prefijo | producto, cliente, bodega, ajuste_tipo, ajuste_motivo |
| Interfaces NAV | `i_nav_*` | i_nav_transacciones_out, i_nav_ped_traslado_*, i_nav_ejecucion_* |
| Logs | `log_*` | log_error_wms, log_importacion_excel |
| Diferencias | `diferencias_*`, `dh_*` | diferencias_movimientos, dh_ocupacion_bodega |
| Producto extras | `producto_*` | producto_presentacion_bk, producto_pallet, producto_pallet_rec |
| Backups/snapshots | `*_bk`, `*_YYYYMMDD` | producto_presentacion_bk, stock_20250624 |
| Legacy con prefijo | `t_*` | t_producto_bodega (42k filas, 2019) |

> Familias completas en `parametrizacion/README` cuando se complete extracción full.
