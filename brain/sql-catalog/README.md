---
id: README
tipo: sql-catalog
estado: vigente
titulo: brain/sql-catalog
tags: [sql-catalog]
---

# brain/sql-catalog

Extractores y herramientas para el catálogo SQL del WMS.

## Archivos

| Archivo | Output | Para qué |
|---|---|---|
| `extract_sql_catalog.py` | JSON (Brain API REST) | Indexar el catálogo en la API `/api/brain/import/sql-catalog` para queries programáticas (`/search`, `/impact`, `/writers`). Histórico, corre desde la PC de EJC. |
| `extract_for_db_brain.mjs` | Markdown idempotente (`db-brain/`) | Generar entities markdown del catálogo en el branch `wms-db-brain` para análisis humano + cross-link con `wms-brain`. Corre desde Replit o PC. |

## Uso del extractor markdown

Setup mínimo (Replit ya lo tiene):
- Node 20+
- `npm i mssql` en alguna carpeta de la cual ejecutar el script

Ejecutar:

```bash
WMS_KILLIOS_DB_PASSWORD=... node extract_for_db_brain.mjs --out /path/to/db-brain
```

Variables opcionales (con defaults):

```
WMS_KILLIOS_DB_HOST=52.41.114.122
WMS_KILLIOS_DB_PORT=1437
WMS_KILLIOS_DB_USER=sa
WMS_KILLIOS_DB_NAME=TOMWMS_KILLIOS_PRD
```

Output esperado:

```
db-brain/
├── tables/<name>.md         · 345 archivos (Killios)
├── views/<name>.md          · 220 archivos
├── sps/<name>.md            · 39 archivos
├── functions/<name>.md      · 17 archivos
├── tables/_index.md         · listado completo (regenerado)
├── views/_index.md          · idem
├── sps/_index.md            · idem
├── functions/_index.md      · idem
├── _meta/extracted_at.txt   · timestamp + database
└── _meta/stats.md           · conteos + top 15 + hallazgos
```

**Idempotente**: re-correr genera diff limpio, solo cambian objetos que cambiaron en BD desde el último snapshot.

**Preserva** `parametrizacion/` y `dependencias/` (curated por humano + agente).

## Cross-refs
- `brain/entities/modules/mod-repo-dba` — repo donde están los `.sql` versionados
- `brain/entities/modules/mod-conexion-sqlserver` — connection details
- `brain/entities/rules/rule-08-killios-prod-solo-lectura` — el extractor solo lee `sys.*`
- `brain/entities/rules/rule-09-modulo-definition-sensible` — definitions van al markdown pero NO se logean
- `db-brain://README` — el output (en branch `wms-db-brain`)
