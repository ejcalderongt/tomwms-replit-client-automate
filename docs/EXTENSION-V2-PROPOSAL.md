# EXTENSION-V2-PROPOSAL.md — bump del bridge a `SCHEMA_VERSION="2"`

> **Propuesta formal** de extensión del Brain Bridge para soportar el
> caso de uso "investigación SQL al brain de la BD" (question cards
> Q-NNN). Requiere modificar 3 constantes en `scripts/brain_bridge.mjs`
> + 2 archivos de doctrina. **Es retrocompatible**: eventos schema 1
> siguen siendo válidos.

---

## 1. Motivación

El bridge actual (`SCHEMA_VERSION="1"`) cubre 6 tipos de evento, todos
**originados en el WMS** (apply de bundles, skill updates, directivas,
merges, cambios externos). El cliente PowerShell `WmsBrainClient`
introduce un caso nuevo:

> *"Erik / agente quiere ejecutar un set de queries dirigidas contra
> K7-PRD/BB-PRD/C9-QAS para responder una pregunta concreta, dejar la
> respuesta en un .md, y consolidar aprendizajes derivados."*

Esto **no es** ni un `apply_succeeded` (no se aplicó nada al WMS), ni
un `skill_update` (todavía no se sabe qué actualizar), ni un
`directive` (no es una orden, es una pregunta). Es un nuevo paradigma:
**investigación → respuesta → aprendizaje**.

Hoy se workarounea con `directive` + `tags=["question","Q-NNN"]`. Es
funcional pero pierde semántica y rompe el `analyze` heurístico que
busca módulos del WMS, no preguntas SQL.

---

## 2. Cambios propuestos

### 2.1 `scripts/brain_bridge.mjs`

```diff
- const SCHEMA_VERSION = "1";
+ const SCHEMA_VERSION = "2";

  const VALID_TYPES = [
    "apply_succeeded",
    "apply_failed",
    "skill_update",
    "directive",
    "merge_completed",
    "external_change",
+   // Schema v2 — investigación SQL del brain de la BD
+   "question_request",
+   "question_answer",
+   "learning_proposed",
  ];

  const VALID_STATUSES = [
    "pending",
    "analyzed",
    "proposed",
    "applied",
    "skipped",
+   // Schema v2 — terminal especifico para question_request resueltos
+   "answered",
  ];
```

### 2.2 Nueva función `analyzeQuestionRequest`

`analyze` actual cruza módulos/tags/marker contra los `.md` del brain.
Para `question_request` la heurística distinta:

1. Lee `ref.question_card_path` del repo `wms-brain-client`.
2. Lista las queries `suggestedQueries[*]` y los `targets[*].codename`.
3. Genera `brain/_proposals/<id>.md` con plan de ejecución:
   - "Esta pregunta requiere correr las queries A, B, C contra `<codename>-PRD`."
   - "Cliente recomendado: `Invoke-WmsBrainQuestion -Id Q-NNN -Profile <codename>-PRD`."
   - "Output esperado: answer card en `wms-brain-client/answers/A-NNN-<slug>.md`."

`analyzeQuestionAnswer` y `analyzeLearningProposed` siguen el mismo
patrón pero sus propuestas apuntan al consumo (no a la ejecución).

### 2.3 `brain/BRIDGE.md` (en rama `wms-brain`)

Agregar sección §2.1 "Tipos de evento — schema v2 (investigación SQL)":

```markdown
| type                  | cuando                                                                       |
|-----------------------|------------------------------------------------------------------------------|
| `question_request`    | el cliente PS encola una pregunta SQL al brain de la BD (Q-001..Q-NNN).      |
| `question_answer`     | el agente Replit produjo una answer card en respuesta a un question_request. |
| `learning_proposed`   | derivación de varios eventos en una learning card consolidada.               |
```

Y agregar §1.4.bis "Estados — schema v2":

```markdown
| status      | significado                                                  |
|-------------|--------------------------------------------------------------|
| `answered`  | un `question_request` cuya `question_answer` ya fue producida (terminal). |
```

### 2.4 `replit.md` (en rama `wms-brain`)

Agregar entry en "Decisiones registradas":

```
- 2026-MM-DD: Bump SCHEMA_VERSION del bridge a "2".
  Motivación: integración con WmsBrainClient (cliente PowerShell).
  Tipos nuevos: question_request, question_answer, learning_proposed.
  Estado nuevo: answered.
  Compatibilidad: total con eventos schema 1.
  Tracking: wms-brain-client/EXTENSION-V2-PROPOSAL.md.
```

---

## 3. Campos extra por tipo nuevo

### 3.1 `question_request`

```json
{
  "id": "20260427-1900-EJC",
  "schema_version": "2",
  "type": "question_request",
  "source": "openclaw",
  "ref": {
    "question_id": "Q-003",
    "question_card_path": "wms-brain-client/questions/Q-003-ingresos-byb-pendientes.md",
    "rama_repo": "wms-brain-client",
    "commit_sha": "abc1234"
  },
  "context": {
    "message": "Por que BB tiene 110k INGRESOS pendientes en outbox (PEND-12)",
    "modules_touched": ["i_nav_transacciones_out", "outbox_processor"],
    "tags": ["outbox", "navsync", "BB", "bandera-roja", "PEND-12"],
    "targets": [{ "codename": "BB", "environment": "PRD" }],
    "expected_outputs": [
      "Distribución temporal de los 110k INGRESOS pendientes",
      "Conteo de INGRESOS que SI se enviaron alguna vez"
    ]
  },
  "status": "pending",
  "history": [{ "at": "...", "action": "notify", "by": "EJC" }]
}
```

### 3.2 `question_answer`

```json
{
  "id": "20260428-0930-EJC",
  "schema_version": "2",
  "type": "question_answer",
  "source": "openclaw",
  "ref": {
    "answers_question_id": "Q-003",
    "answers_event_id": "20260427-1900-EJC",
    "answer_card_path": "wms-brain-client/answers/A-003-ingresos-byb-pendientes.md",
    "evidence_paths": [
      "wms-brain-client/answers/_evidence/A-003/q1-edad.csv",
      "wms-brain-client/answers/_evidence/A-003/q2-enviados.csv"
    ]
  },
  "context": {
    "message": "Confirmado: 110,795 INGRESOS pendientes desde 2024-07. Solo 107 INGRESOS enviados alguna vez. Sospecha: NavSync.vbproj nunca tuvo flag para INGRESOS, quedó solo SALIDAS.",
    "verdict": "confirmed",
    "confidence": "high",
    "modules_touched": ["NavSync.vbproj", "i_nav_transacciones_out"],
    "tags": ["answer", "A-003", "Q-003", "BB", "navsync"]
  },
  "status": "pending",
  "history": [{ "at": "...", "action": "notify", "by": "EJC" }]
}
```

### 3.3 `learning_proposed`

```json
{
  "id": "20260430-1100-EJC",
  "schema_version": "2",
  "type": "learning_proposed",
  "source": "replit",
  "ref": {
    "learning_card_path": "wms-brain/learnings/L-012-navsync-solo-salidas.md",
    "derived_from": [
      "20260427-1900-EJC",
      "20260428-0930-EJC",
      "20260429-1430-EJC"
    ]
  },
  "context": {
    "message": "Patrón confirmado: NavSync.vbproj implementa solo SALIDAS, ignora INGRESOS por diseño original. Documentar en doctrina de interfaces ERP.",
    "modules_touched": ["NavSync", "interfaces-erp"],
    "tags": ["learning", "L-012", "navsync", "BB", "doctrina"]
  },
  "status": "pending",
  "history": [{ "at": "...", "action": "notify", "by": "agent-replit" }]
}
```

---

## 4. Compatibilidad y migración

### 4.1 Eventos vivos al momento del bump

- Todos los eventos schema 1 en `_inbox/`, `_proposals/`, `_processed/`
  siguen siendo válidos. El bridge los acepta y procesa igual.
- No se requiere "migración" de eventos viejos.

### 4.2 Validación en notify

```javascript
// Pseudo-código
if (event.schema_version === "1" && NEW_TYPES.includes(event.type)) {
  throw new Error(`type '${event.type}' requiere schema_version "2", recibido "1"`);
}
if (event.schema_version === "2" && !VALID_TYPES.includes(event.type)) {
  throw new Error(`type '${event.type}' no esta en VALID_TYPES`);
}
```

### 4.3 Análisis (`analyze`)

El switch por type:

```javascript
switch (event.type) {
  case "apply_succeeded":
  case "apply_failed":
  case "skill_update":
  case "directive":
  case "merge_completed":
  case "external_change":
    return analyzeWmsChange(event);    // heurística existente
  case "question_request":
    return analyzeQuestionRequest(event);  // §2.2
  case "question_answer":
    return analyzeQuestionAnswer(event);
  case "learning_proposed":
    return analyzeLearningProposed(event);
  default:
    throw new Error(`type ${event.type} sin analyzer`);
}
```

---

## 5. Plan de aceptación

1. **Revisión Erik** — leer este documento + ejemplos en `examples/`.
2. **Sign-off** — Erik aprueba el bump y los nombres.
3. **Implementación** — alguien (yo, Open Claw, o Erik directo) edita
   `scripts/brain_bridge.mjs` con el diff §2.1 + agrega los 3
   `analyze*` de §2.2.
4. **Documentación** — actualizo `brain/BRIDGE.md` y `replit.md` con
   los cambios §2.3 y §2.4.
5. **Cliente PS** — `New-WmsBrainQuestionEvent` y compañía pasan a
   emitir los tipos nativos. Borro el workaround `directive`+`tags` del
   PROTOCOL.md §5 con un changelog que lo deje grabado.
6. **Test** — emito un `question_request` de prueba (Q-001) y verifico
   ciclo completo notify → list → analyze → proposed → answered.

---

## 6. Riesgos y mitigaciones

| Riesgo                                              | Mitigación                                                |
|-----------------------------------------------------|-----------------------------------------------------------|
| `analyze` heurístico actual cae en infinite loop si type es desconocido. | Switch explícito con `default: throw`. |
| Eventos viejos quedan inconsistentes en history.    | No: history es append-only, sigue funcionando.            |
| Cliente PS emite v2 antes de bump del bridge.       | `Test-WmsBrainEnvironment` lee `SCHEMA_VERSION` y refusa. |
| Question card cambia de path post-emisión.          | `ref.commit_sha` apunta al snapshot exacto.               |
| Answer card sobreescribe pregunta sin auditoría.    | `decision.note` obligatorio en apply de `question_answer`.|

---

## 7. Estado de esta propuesta

- [x] **2026-04-27**: redactada por agente Replit en ciclo de cliente PS.
- [ ] **PENDIENTE**: revisión Erik.
- [ ] **PENDIENTE**: sign-off / changelog en `replit.md` de `wms-brain`.
- [ ] **PENDIENTE**: implementación en `brain_bridge.mjs`.
- [ ] **PENDIENTE**: borrar workaround `directive`+`tags` del PROTOCOL.md §5.
