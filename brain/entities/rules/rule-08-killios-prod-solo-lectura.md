---
id: rule-08-killios-prod-solo-lectura
type: rule
title: Conexión a Killios productivo = SOLO LECTURA desde Replit
status: vigente
severity: critical
applies_to: [agente Replit, scripts del productor]
sources:
  - skill: wms-tomwms §4 #8
  - validated_at: 2026-04-27
---
# Regla 08 — Conexión a Killios productivo = SOLO LECTURA desde Replit

## Statement

Cualquier query desde Replit hacia `TOMWMS_KILLIOS_PRD` debe ser `SELECT` (o `WITH ... SELECT`, `EXEC` de SP de lectura, `SET NOCOUNT ON`). PROHIBIDO `INSERT`, `UPDATE`, `DELETE`, `MERGE`, `DROP`, `ALTER`, `CREATE`, `TRUNCATE`.

## Rationale

Killios es producción real con clientes activos. Una escritura accidental desde el agente puede:
- Corromper stock, lotes, transacciones.
- Disparar facturación errónea o roturas de inventario.
- Forzar restore desde backup → downtime.

El agente Replit es **productor de conocimiento + bundles**, NO es operador del WMS. Las escrituras las hace EJC con el procedimiento normal (SSMS + scripts versionados en repo DBA).

## Cómo cumplir

1. **CLI `wmsa db`**: implementar whitelist (`SELECT|WITH|EXEC|EXECUTE|SET`) y blacklist (`INSERT|UPDATE|DELETE|MERGE|DROP|ALTER|CREATE|TRUNCATE`). Rechazar cualquier query que no pase ambos filtros.
2. **Scripts ad-hoc Node/Python**: validar manualmente que el SQL es `SELECT` antes de ejecutar. Code review obligatorio.
3. **Conexión**: usar `User=sa` o `User=wmsuser` con password del secret. NO crear usuarios nuevos para escritura.
4. **Transacciones**: si un SP de lectura tiene `BEGIN TRAN`/`COMMIT` interno, evaluar si es seguro ejecutar.
5. **Pool**: timeout corto para evitar locks largos en producción.

## Excepciones

Si surge necesidad real de escribir (ej. backfill puntual), **PARAR** y avisar a EJC. La escritura la hace él, no el agente.

## Cross-refs

- `modules/mod-conexion-sqlserver` — driver y endpoint
- `decisions/dec-2026-04-killios-acceso-replit` — rationale de tener acceso desde Replit
- `db-brain://README` — el catálogo se genera con `SELECT` de `sys.*` exclusivamente
