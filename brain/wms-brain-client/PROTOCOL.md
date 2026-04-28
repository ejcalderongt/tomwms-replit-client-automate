# wms-brain — Protocolo de question/answer/learning cards

> **Status**: Draft v1 (2026-04-27, protocolVersion=1).
> Define el formato exacto de los archivos Markdown intercambiados entre
> brain y cliente. **Cualquier cambio incompatible bumps `protocolVersion`**.

## 1. Tipos de cards

| Tipo | Quien crea | Quien consume | Path |
|---|---|---|---|
| **Question** | brain (humano o agente) | cliente local | `learnings/pending/Q-XXX-titulo.md` |
| **Answer** | cliente local | brain (consolidador) | `learnings/answered/<slug>/<fecha>/<CN>-Q-XXX-titulo.md` |
| **Learning** | cliente local (modo libre) | brain (consolidador) | `learnings/answered/<slug>/<fecha>/<CN>-LEARN-NNN-titulo.md` |
| **Closed** | brain (consolidador) | (referencia historica) | `learnings/closed/Q-XXX-titulo.md` |

## 2. Question card

### 2.1 Path y naming

```
learnings/pending/Q-<NNN>-<slug-titulo-corto>.md
```

- `NNN` es secuencial global (Q-001, Q-002, ...). El brain mantiene el contador
  via convencion (revisa el ultimo y suma 1).
- `slug-titulo-corto` es kebab-case, max 50 chars.

Ejemplo: `learnings/pending/Q-007-cadencia-real-navsync-bb.md`

### 2.2 Estructura (front-matter YAML + cuerpo MD)

```markdown
---
protocolVersion: 1
id: Q-007
title: Cadencia real de NavSync en BB
createdBy: agent-replit
createdAt: 2026-04-27T14:30:00Z
priority: medium                         # low | medium | high | critical
status: pending                          # pending | in-progress | answered | closed
tags: [outbox, navsync, BB, cadencia]
targets:
  - codename: BB
    environment: PRD                     # DEV | QAS | PRD | LOCAL
    minRows: 100                         # cliente puede skipear si la BD esta vacia
relatedDocs:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
  - brain/wms-specific-process-flow/preguntas-ciclo-7.md#PEND-07
suggestedQueries:
  - id: q1
    description: Histograma de fec_agr de los enviados, agrupado por hora
    sql: |
      SELECT DATEPART(HOUR, fec_agr) AS hora, COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      WHERE enviado = 1
      GROUP BY DATEPART(HOUR, fec_agr)
      ORDER BY hora;
  - id: q2
    description: Diferencia tipica entre fec_agr y fec_mod en enviados
    sql: |
      SELECT TOP 100
        DATEDIFF(SECOND, fec_agr, fec_mod) AS seg_proceso,
        COUNT(*) AS cnt
      FROM i_nav_transacciones_out
      WHERE enviado = 1
      GROUP BY DATEDIFF(SECOND, fec_agr, fec_mod)
      ORDER BY cnt DESC;
expectedOutputs:
  - id: q1
    type: table
    columns: [hora, cnt]
  - id: q2
    type: table
    columns: [seg_proceso, cnt]
followUp:
  ifFinding: Si hay picos cada N minutos, sugiere job programado con esa cadencia
  thenAsk: Q-008 (identificar nombre exacto del scheduler)
estimatedTimeMinutes: 5
allowFreeFormNotes: true
---

## Contexto

En el ciclo 9b detectamos que `BB.i_nav_transacciones_out` tiene
145,117 SALIDAS pendientes y 277,310 enviadas. Necesitamos entender
**la cadencia real** del job que procesa esas SALIDAS para validar la
hipotesis de que NavSync corre periodicamente.

## Pregunta concreta

¿Cual es la cadencia tipica del job que setea `enviado=1` en
`i_nav_transacciones_out` de BB?

## Que se espera del operador

1. Conectarse al perfil `BB-PRD`.
2. Ejecutar `q1` (histograma por hora) y `q2` (latencia tipica).
3. Adjuntar interpretacion (1-3 lineas) y, si conoce, el nombre del
   scheduler/.exe responsable.
```

### 2.3 Campos obligatorios vs opcionales

**Obligatorios** (`null` rechazado):
- `protocolVersion`, `id`, `title`, `createdBy`, `createdAt`, `priority`, `status`, `targets[].codename`, `targets[].environment`.

**Opcionales pero recomendados**:
- `tags`, `relatedDocs`, `suggestedQueries`, `expectedOutputs`, `followUp`.

**Opcionales libres**:
- `estimatedTimeMinutes`, `allowFreeFormNotes`, `attachments`.

## 3. Answer card

### 3.1 Path y naming

```
learnings/answered/<operatorSlug>/<YYYY-MM-DD>/<CODENAME>-Q-<NNN>-<slug-titulo>.md
```

Ejemplo: `learnings/answered/ejc/2026-04-27/BB-Q-007-cadencia-navsync.md`

### 3.2 Estructura

```markdown
---
protocolVersion: 1
type: answer
questionId: Q-007
operator: ejc
answeredAt: 2026-04-27T15:12:00Z
clientVersion: WmsBrainClient/1.0.3
profile:
  codename: BB
  environment: PRD
  serverFingerprint: sha256:abcd1234   # hash del @@SERVERNAME + DB_NAME, no el server real
  rowsScanned: 277310
queriesExecuted:
  - id: q1
    durationMs: 1234
    rowCount: 24
    status: ok
  - id: q2
    durationMs: 567
    rowCount: 100
    status: ok
verdict: confirmed                        # confirmed | partial | inconclusive | rejected | error
confidence: high                          # low | medium | high
freeFormNotes: true
attachments:
  - results/q1-output.json
  - results/q2-output.json
signature:
  algorithm: sha256
  contentHash: <hash del cuerpo MD>
  commitSha: <se llena al commitear>
---

## Resultado q1 — Histograma por hora

| hora | cnt    |
|-----:|-------:|
| 0    | 3      |
| 1    | 5      |
| ...  | ...    |
| 7    | 21,431 |
| 8    | 18,902 |
| ...  | ...    |

**Interpretacion**: hay un pico marcado entre 06:00 y 09:00 (horario de
arranque de jornada), y otro entre 14:00 y 17:00 (cierre). El job NO
parece ser cada-N-minutos-fijos sino disparado por eventos del WMS.

## Resultado q2 — Latencia tipica

| seg_proceso | cnt   |
|------------:|------:|
| 0           | 21,034 |
| 1           | 18,902 |
| 2           | 12,103 |
| ...         | ...    |

**Interpretacion**: el 85% de los registros tienen `fec_mod = fec_agr` (0
segundos). Eso sugiere que el job procesa **inmediatamente al insertar**
(quiza un trigger o llamada directa post-insert).

## Hallazgo extra (free-form)

Reviso `sys.dm_exec_procedure_stats` y veo que el SP
`sp_NavSync_ProcesarSalidas` se ejecuto 12,331 veces en las ultimas 24h
con tiempo promedio 0.4s. Eso refuerza la hipotesis de "post-insert".

## Conclusion

NavSync para SALIDAS de BB **NO es un scheduler periodico** sino que se
dispara post-insert al outbox via SP. Pendiente: identificar el trigger
o el llamador del SP.
```

### 3.3 Adjuntos

Resultados de queries pueden ir embebidos (en MD) o adjuntos como JSON
en una subcarpeta `results/` adyacente:

```
learnings/answered/ejc/2026-04-27/
  BB-Q-007-cadencia-navsync.md
  BB-Q-007-cadencia-navsync.results/
    q1-output.json
    q2-output.json
```

Formato JSON estandar:

```json
{
  "queryId": "q1",
  "executedAt": "2026-04-27T15:12:00Z",
  "durationMs": 1234,
  "columns": [
    {"name": "hora", "type": "int"},
    {"name": "cnt", "type": "int"}
  ],
  "rows": [
    {"hora": 0, "cnt": 3},
    {"hora": 1, "cnt": 5}
  ],
  "metadata": {
    "rowCount": 24,
    "truncated": false,
    "maxRows": 10000
  }
}
```

### 3.4 Sanitizacion antes de commit

El cliente DEBE remover automaticamente:
- Nombres reales de clientes (reemplaza por codename via `aliases.local.json`).
- IPs y nombres de server (reemplaza por `<SERVER>` o hash).
- Usuarios SQL reales (reemplaza por `<USER>`).
- Cualquier campo marcado como sensible en suite/scenario metadata.

## 4. Learning card (modo libre)

### 4.1 Path y naming

```
learnings/answered/<slug>/<YYYY-MM-DD>/<CN>-LEARN-<NNN>-<titulo>.md
```

`NNN` es contador local del operador para ese dia. Ejemplo:
`learnings/answered/ejc/2026-04-27/K7-LEARN-001-stock-hist-tiene-33-cols.md`

### 4.2 Estructura

```markdown
---
protocolVersion: 1
type: learning
operator: ejc
createdAt: 2026-04-27T16:30:00Z
clientVersion: WmsBrainClient/1.0.3
profile:
  codename: K7
  environment: PRD
title: stock_hist tiene 33 columnas (mas que las documentadas)
tags: [stock, schema, K7]
relatedDocs:
  - brain/wms-specific-process-flow/state-machine-pedido.md
verdict: new-finding                   # new-finding | refinement | correction | question
attachments: []
signature:
  contentHash: ...
  commitSha: ...
---

## Hallazgo

Mientras revisaba el caso del bug P-16b note que `stock_hist` tiene cols
que no estan documentadas en el state-machine: ...

## Por que importa

Porque el stock historico podria contener la cronologia exacta del bug
P-16b a nivel stock (no solo a nivel pedido). Ver casos 4156, 4155, 4152.

## Que sugiero

Crear Q-XXX para investigar formalmente.
```

## 5. Closed card

Cuando una question es consolidada en un doc estable, el brain mueve la
card a `learnings/closed/` y le agrega front-matter:

```markdown
---
protocolVersion: 1
id: Q-007
status: closed
closedBy: agent-replit
closedAt: 2026-04-30T10:00:00Z
consolidatedInto:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md#PEND-07
answersConsidered:
  - learnings/answered/ejc/2026-04-27/BB-Q-007-cadencia-navsync.md
  - learnings/answered/juanp/2026-04-29/BB-Q-007-cadencia-navsync.md
verdict: confirmed
followUpQuestions: [Q-014]
---

(cuerpo original de la question + nota final del consolidador)
```

## 6. Convenciones de commit

### 6.1 Mensaje de commit

Formato: `<scope>: <accion> <id>-<titulo> [<codename>]`

Ejemplos:
- `pending: add Q-007 cadencia NavSync [BB]`
- `answered: ejc Q-007 cadencia NavSync [BB] verdict=confirmed`
- `learning: ejc K7-LEARN-001 stock_hist 33 cols`
- `closed: Q-007 consolidated into interfaces-erp-por-cliente.md`

### 6.2 Co-author

Cuando un answer se base parcialmente en sugerencia de otro:

```
Co-authored-by: <slug> <email>
```

## 7. Sanitizacion automatica del cliente

Al generar una answer card, el cliente:

1. Reemplaza nombres reales por codenames via `aliases.local.json`.
2. Reemplaza IPs/hosts por placeholders.
3. Reemplaza usuarios SQL por placeholders.
4. Hashea el `serverFingerprint` (no commitea servidor real).
5. Limita `attachments` a `Resources.maxAttachmentSizeMB` (default 5MB).
6. Aborta si detecta patrones tipo PAN, RFC, DUI (regex configurables).

## 8. Versionado del protocolo

- `protocolVersion: 1` es esta version inicial.
- Cambios compatibles (nuevo campo opcional): NO bump.
- Cambios incompatibles (rename, remove, semantica diferente): bump.
- El cliente rechaza cards de protocolo superior y sugiere upgrade del modulo.
