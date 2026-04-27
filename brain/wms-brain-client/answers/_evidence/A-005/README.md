# Evidencia A-005

Generada: 2026-04-27 por agent-replit
Queries ejecutadas read-only contra DBs WMS productivas.

## Archivos

- q1-cols-config-sap.csv (CSV de salida)
- q2-mapeo-productos.csv (CSV de salida)
- q3-cols-cliente-especificas.csv (CSV de salida)
- q4-config-empresa-flags.csv (CSV de salida)
- q1-cols-config-sap.json (JSON crudo: ok/rowCount/rows/ms)
- q2-mapeo-productos.json (JSON crudo: ok/rowCount/rows/ms)
- q3-cols-cliente-especificas.json (JSON crudo: ok/rowCount/rows/ms)
- q4-config-empresa-flags.json (JSON crudo: ok/rowCount/rows/ms)

Convencion de naming: `<src>__<query-id>.csv` cuando hay varias
fuentes. Ejemplo `extra__q5-jobs-sql-agent-bb.csv` = ejecutado en
suite Q-003-EXTRA pero documenta evidencia de A-001.

## Sanitizacion

- Nombres reales de cliente reemplazados por codenames (K7, BB, ...).
- Sin servidores reales, sin credenciales, sin paths locales.
- IDs de transaccion mostrados son productivos pero no contienen PII.
