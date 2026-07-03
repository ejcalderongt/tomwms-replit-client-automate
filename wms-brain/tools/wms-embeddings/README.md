# WMS Embeddings MVP

Indexador local para convertir contexto operativo de WMS en chunks con
embeddings y permitir búsqueda semántica.

## Fuente canónica

- `brain/agents/**/*.yml`
- `brain/handoffs/**/*.md`
- `brain/code-changes/**/*.md`
- `codex-context-*.yml`
- `TRAZAS-FINAS-*.yml`

## Funciones

1. `index`: recorre archivos, parte en chunks y genera embeddings.
2. `query`: busca por similitud semántica sobre el índice generado.
3. `export-html`: genera una vista HTML simple para inspección humana.

## Requisito de embeddings

El script usa un endpoint OpenAI-compatible de embeddings:

- `EMBEDDINGS_BASE_URL` por defecto: `http://127.0.0.1:1234/v1`
- `EMBEDDINGS_MODEL` por defecto: el primero disponible en `/models`

Si el servidor no expone embeddings, la indexación no puede completarse.

## Uso

```powershell
python .\tools\wms-embeddings\wms_embeddings.py index
python .\tools\wms-embeddings\wms_embeddings.py query "reserva mi3 con stock desfasado"
python .\tools\wms-embeddings\wms_embeddings.py export-html
```

## Salidas

- `tools/wms-embeddings/out/chunks.jsonl`
- `tools/wms-embeddings/out/index.json`
- `tools/wms-embeddings/out/search-results.md`
- `tools/wms-embeddings/out/search-results.html`

## Guardrails

- No toca código productivo.
- No escribe en BD.
- No publica a Jira.
- No expone secretos ni `definition` SQL.
