# TOOLS.md - Local Notes

## Branches and repos

- `main` = repo de intercambio, bundles, logs y contrato operativo.
- `wms-brain` = conocimiento compartido del agente (WMS).
- `mcp-brain` = brain del proyecto MCP (https://github.com/ejcalderongt/mcp-brain) — cada proyecto tiene su propio brain/repo.
- `mpos-brain` = brain del proyecto mPos (placeholder local). Si existe repo dedicado, enlazar (p.ej. https://github.com/ejcalderongt/mpos-brain).
- `openclaw-control-ui` = MVP de bootstrap/control/brain-up.

## Paths

- Exchange repo: `C:\tomwms-exchange`
- Brain repo: `C:\tomwms-brain`

## Brain-up MVP

- Script principal: `scripts/brain-up.ps1`
- Script de consulta: `scripts/brain-query.ps1`
- Estado: `state/brain-up.json`

## Operación

- Antes de operar: `hello sync`.
- Si el sync está bien, responder `Hello Erik` + ASCII.
- Para el MVP: usar rama `openclaw-control-ui`.
