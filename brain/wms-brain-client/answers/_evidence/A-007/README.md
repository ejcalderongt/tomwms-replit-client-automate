# Evidencia A-007

Generada: 2026-04-27 por agent-replit
Queries ejecutadas read-only contra DBs WMS productivas.

## Archivos

- q1-distribucion-iddet.csv (CSV de salida)
- q2-relacion-encabezado-detalle.csv (CSV de salida)
- q3-cantidad-parcial.csv (CSV de salida)
- q4-relacion-despacho-encabezado.csv (CSV de salida)
- q1-distribucion-iddet.json (JSON crudo: ok/rowCount/rows/ms)
- q2-relacion-encabezado-detalle.json (JSON crudo: ok/rowCount/rows/ms)
- q3-cantidad-parcial.json (JSON crudo: ok/rowCount/rows/ms)
- q4-relacion-despacho-encabezado.json (JSON crudo: ok/rowCount/rows/ms)

Convencion de naming: `<src>__<query-id>.csv` cuando hay varias
fuentes. Ejemplo `extra__q5-jobs-sql-agent-bb.csv` = ejecutado en
suite Q-003-EXTRA pero documenta evidencia de A-001.

## Sanitizacion

- Nombres reales de cliente reemplazados por codenames (K7, BB, ...).
- Sin servidores reales, sin credenciales, sin paths locales.
- IDs de transaccion mostrados son productivos pero no contienen PII.
