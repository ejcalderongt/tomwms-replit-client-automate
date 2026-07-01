# WMS Scale Readiness Kit (50 bodegas / 100 HH)

Autor: `#EJC20260526`  
Objetivo: prevenir y diagnosticar temprano bloqueos, deadlocks y degradación.

## Orden de ejecución

1. `01_enable_query_store_and_xevents.sql` (una vez por ambiente)
2. `02_baseline_snapshot.sql` (guardar baseline antes de pruebas de carga)
3. `03_live_blocking_and_deadlocks.sql` (durante pruebas/operación)
4. `04_top_waits_and_queries.sql` (análisis de cuellos de botella)
5. `05_hot_tables_and_lock_pressure.sql` (contención por tabla)

## Umbrales sugeridos (alerta)

- Deadlocks > 3 por 5 minutos
- Bloqueo activo > 10 segundos
- `LCK_M_%` entre top waits por más de 10 minutos
- p95 de WS críticos > 2.5s
- crecimiento de timeout HH/BOF

## WS/Flujos a vigilar primero

- `Aplica_Cambio_Estado_Ubic_HH*`
- `Insertar_Movimientos_Recepcion*`
- packing/reemplazo/verificación/picking en transacciones concurrentes

## Nota

Todos los scripts son solo lectura excepto el `01_...` que crea sesión Extended Events y habilita Query Store.
