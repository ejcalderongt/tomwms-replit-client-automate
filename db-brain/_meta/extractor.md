# Extractor del catálogo SQL

## Estado actual (2026-04-27)

**Snapshot inicial generado ad-hoc** con un script Node + driver `mssql` desde Replit. NO usa todavía `brain/sql-catalog/extract_sql_catalog.py` (Python local) porque:

1. El Python script estaba pensado para correr en la PC de EJC y enviar JSON al Brain API REST.
2. Acabamos de validar que se puede conectar desde Replit directo a Killios.
3. El formato salida del Python es JSON para `brain_symbols`/`brain_references` — necesita extender para generar markdown.

## Plan de integración (commit posterior)

Modificar `brain/sql-catalog/extract_sql_catalog.py` para output dual:

```bash
python extract_sql_catalog.py \
    --server "52.41.114.122,1437" \
    --database TOMWMS_KILLIOS_PRD \
    --user sa \
    --upload https://<replit>/api/brain/import/sql-catalog \
    --markdown-out ./db-brain/      # NUEVO
```

El `--markdown-out` regenera idempotentemente:
- `tables/<name>.md` por cada tabla
- `views/<name>.md` por cada vista
- `sps/<name>.md` por cada SP
- `functions/<name>.md` por cada función
- `_index.md` con listado completo
- `_meta/stats.md` y `_meta/extracted_at.txt` actualizados
- Preserva `parametrizacion/*.md` (curated por humano + agente, no auto-overwriteado)

## Frecuencia

Re-ejecutar después de:
- Cambio de schema en producción (nuevo SP, vista modificada, columna agregada/eliminada).
- Importar un nuevo cliente (cambia el set de flags activos en `parametrizacion/matriz-<cliente>.md`).

## Idempotencia

Cada run debe producir un commit limpio (no diff espurio por orden de keys). El script ordena consistentemente por `schema, name, ord`.

## Reglas

- `module.definition` (cuerpo de SPs/vistas/funciones) es **sensible** — el extractor lo escribe en el markdown pero NUNCA lo logea ni lo expone a clientes externos.
- Solo lectura: el extractor usa `SELECT` exclusivamente sobre `sys.*`.
