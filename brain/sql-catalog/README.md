# SQL Catalog Extractor

Extracts the SQL Server catalog (objects, dependencies, module definitions)
from `TOMWMS_KILLIOS_PRD` (or any other SQL Server DB) and uploads it to the
TOMWMS Brain running on Replit.

This script runs **on your local machine** so the production database password
never leaves your PC and the production server never has to allow connections
from Replit.

## Why local

- The prod DB lives behind your firewall on `52.41.114.122`. Replit would
  need an inbound IP allowlist to reach it.
- `WMS_KILLIOS_DB_PASSWORD` is sensitive — keep it on your machine.
- SQL Server's native ODBC driver is what you already have installed for SSMS.
- Re-running takes ~2 seconds; no need for a server-side cron.

## Setup (one-time)

```bash
pip install pyodbc requests
```

You also need an ODBC driver for SQL Server. On Windows it's installed with
SSMS by default. Verify with:

```powershell
Get-OdbcDriver -Name "*SQL Server*"
```

## Usage

### Option A — extract to file, then upload manually

```bash
# Set the password in the environment (don't pass it on the CLI!)
$env:WMS_KILLIOS_DB_PASSWORD = "..."

python extract_sql_catalog.py \
    --server "52.41.114.122,1437" \
    --database TOMWMS_KILLIOS_PRD \
    --user wmsuser \
    --output ./tomwms_killios_prd.json
```

Then upload via curl:

```bash
curl -X POST -H "Content-Type: application/json" \
  --data-binary @tomwms_killios_prd.json \
  https://<your-replit-url>/api/brain/import/sql-catalog
```

### Option B — extract + upload in one shot

The Brain's write endpoints are protected by a shared token. Set it on your
machine the same way you set the DB password (it must match the
`BRAIN_IMPORT_TOKEN` secret configured on Replit):

```powershell
$env:WMS_KILLIOS_DB_PASSWORD = "..."
$env:BRAIN_IMPORT_TOKEN       = "..."   # same value as the Replit secret

python extract_sql_catalog.py `
    --server "52.41.114.122,1437" `
    --database TOMWMS_KILLIOS_PRD `
    --user wmsuser `
    --upload https://<your-replit-dev-domain>/api/brain/import/sql-catalog
```

The script will send the token in the `X-Brain-Token` header.

### Option C — SSMS only (no Python)

If you don't want to install Python, run the queries in `extract.sql` directly
in SSMS, export each result grid as JSON, then assemble them into the payload
shape documented in `../../artifacts/api-server/src/services/importers/sqlCatalog.ts`
(`SqlCatalogPayloadSchema`).

## What gets extracted

| Section | Source | Notes |
|---|---|---|
| `objects` | `sys.objects` + `sys.schemas` + row counts | tables, views, SPs, functions, triggers (no `is_ms_shipped`) |
| `dependencies` | `sys.sql_expression_dependencies` | who references whom |
| `modules` | `sys.sql_modules` | full SQL text of views/SPs/funcs/triggers |

## Output JSON shape

```jsonc
{
  "database": "TOMWMS_KILLIOS_PRD",
  "server": "52.41.114.122,1437",
  "extracted_at": "2026-04-21T05:30:00Z",
  "extractor_version": "1.0.0",
  "objects":      [{ "schema":"dbo","name":"VW_Stock_Res","kind":"sql-view","object_id":1234,... }],
  "dependencies": [{ "from_schema":"dbo","from_name":"VW_Stock_Res","to_schema":"dbo","to_name":"Stock","to_kind_hint":"sql-table","is_ambiguous":false }],
  "modules":      [{ "schema":"dbo","name":"VW_Stock_Res","definition":"CREATE VIEW dbo.VW_Stock_Res AS ..." }]
}
```

## What the import does

The Replit endpoint atomically replaces all rows in `brain_symbols` and
`brain_references` belonging to the virtual repo named after the database
(e.g. `TOMWMS_KILLIOS_PRD`).

After import you can query things like:

```sql
-- All views that depend on the Stock table
SELECT s.name FROM brain_symbols s
JOIN brain_references r ON r.from_symbol_id = s.id
WHERE r.to_symbol_name = 'Stock' AND s.kind = 'sql-view';

-- Cross-layer impact: VB methods + SQL views that touch a table
SELECT s.name, s.kind FROM brain_symbols s
JOIN brain_references r ON r.from_symbol_id = s.id
WHERE LOWER(r.to_symbol_name) = 'stock';
```

## Frequency

Re-run after any production schema change (new SP, modified view, dropped
column). The endpoint is idempotent — repeated runs simply replace the
catalog.
