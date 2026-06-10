# Jira field update reference (WMS)

Guia rapida para Codex/agentes al actualizar issues existentes.

## Campos via PUT `/rest/api/3/issue/{issueKey}`

| Campo visible | Field ID | Formato |
|---|---|---|
| Resumen | `summary` | string |
| Descripcion | `description` | ADF |
| Start date | `customfield_10015` | `YYYY-MM-DD` |
| Fecha de vencimiento | `duedate` | `YYYY-MM-DD` |
| Prioridad | `priority` | `{ "name": "High" }` |
| Etiquetas | `labels` | `["tag1","tag2"]` |
| Story points | `customfield_10016` | number |
| Version | `customfield_10036` | number |
| Project Section | `customfield_10039` | `["label"]` |

## Horas utilizadas (worklog)

No va en PUT. Se registra con:

`POST /rest/api/3/issue/{issueKey}/worklog`

`timeSpent` valido:
- `30m`
- `2h`
- `1h 30m`
- `1d` (8h)
- `1w` (40h)

Cada worklog suma al total (`timespent`).

## Estructura minima de descripcion recomendada

1. Problema reportado.
2. Que arruinaba / impacto operativo.
3. Causa raiz.
4. Correccion aplicada.
5. Archivos y commits de trazabilidad.

## Reglas operativas

1. Actualizar siempre `customfield_10015` al iniciar.
2. Actualizar siempre `duedate` con fecha objetivo.
3. Registrar horas al terminar cada bloque real de trabajo.
4. Mantener nomenclatura de summary segun convenciones del brain.
