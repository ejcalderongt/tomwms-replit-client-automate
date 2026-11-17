# DOWN de identities — IODESA

Estos scripts reconstruyen las tablas sin `IDENTITY` y conservan los identificadores actuales.

## Orden sugerido

Ejecutar durante una ventana de mantenimiento, sin escrituras de aplicaciones ni interfaces:

1. `02.DOWN_trans_picking_ubic.sql`
2. `03.DOWN_trans_re_det.sql`
3. `04.DOWN_trans_pe_det.sql`
4. `05.DOWN_stock_res.sql`
5. `06.DOWN_stock.sql`
6. `07.DOWN_i_nav_transacciones_out.sql`
7. `08.DOWN_stock_rec.sql`
8. `09.DOWN_Tablas_Ejecucion_Interface.sql`
9. `10.DOWN_trans_movimientos.sql`

Cada archivo maneja su propia transacción. Hacer primero un respaldo completo y probar sobre una restauración reciente de producción.

## Advertencias

- El UP `07.Identity_i_nav_transacciones_out.sql` eliminó la columna original y generó valores nuevos. El DOWN conserva esos valores nuevos; no recupera los anteriores sin respaldo.
- `10.DOWN_trans_movimientos.sql` conserva una tabla de respaldo llamada `dbo.trans_movimientos__pre_down_20260814`.
- Verificar después de ejecutar que las columnas objetivo reporten `is_identity = 0`, que los conteos coincidan y que la versión anterior de la aplicación pueda insertar IDs explícitos.
