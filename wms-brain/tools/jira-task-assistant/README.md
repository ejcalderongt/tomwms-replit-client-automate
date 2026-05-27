# Jira Task Assistant (Draft)

Herramienta local para cruzar commits vs bitácoras y proponer tareas Jira no clasificadas.

## Objetivo

- Automatizar el cruce entre:
  - commits Git (`TOMWMS`, `TOMHH2025`, etc.),
  - `BITACORA_JIRA.md`,
  - `LOG_DIARIO.md`.
- Generar salida **draft_only** (sin publicar en Jira).

## Variables de entorno Jira (integradas)

Configurar una vez en Windows:

```powershell
setx JIRA_URL "https://dtsolutionsdevops.atlassian.net"
setx JIRA_EMAIL "dtsolutionsgt@gmail.com"
setx JIRA_ACCOUNT_ID "61d3d6500586a20069465469"
```

Token (ya existente en entorno local):

```powershell
setx JIRA_TOKEN "<tu_token_atlassian>"
```

Nota: `setx` aplica para nuevas terminales. En la sesión actual se puede usar `$env:...`.

## Uso

```powershell
python .\tools\jira-task-assistant\main.py `
  --since "2026-05-19 00:00" `
  --until "2026-05-26 23:59" `
  --author-hint "ejcalderon" `
  --repo "C:\Users\yejc2\source\repos\TOMWMS" `
  --repo "C:\Users\yejc2\StudioProjects\TOMHH2025" `
  --bitacora "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\handoffs\2026-05-22-codex-performance-bof-hh\BITACORA_JIRA.md" `
  --log-diario "C:\Users\yejc2\source\repos\wms-brain\wms-brain\brain\handoffs\2026-05-22-codex-performance-bof-hh\LOG_DIARIO.md" `
  --out-dir "C:\Users\yejc2\source\repos\wms-brain\wms-brain\tools\jira-task-assistant\out"
```

## Salidas

- `out/jira_candidates.md`: listado legible de tareas sugeridas.
- `out/jira_payload.json`: estructura JSON para pipeline posterior.

## Notas

- Filtra commits de ruido (`merge`, `sync`, etc.).
- Detecta commits documentados por hash corto/largo dentro de bitácoras.
- Clasifica de forma heurística (Bug/Mejora/Técnica) y sugiere épica/horas.
- No hace llamadas Jira; pensado para revisión previa.
