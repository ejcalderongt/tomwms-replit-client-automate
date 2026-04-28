# BRAIN PROTOCOL — Contrato canonico de eventos, cards y bundles

> Documento de referencia del **lenguaje** que habla el BRAIN del proyecto
> `tomwms-replit-client-automate`. Este doc es la fuente de verdad humana
> del protocolo. La fuente de verdad de codigo sigue siendo
> `scripts/brain_bridge.mjs` y `scripts/apply_bundle.mjs`. Si hay drift entre
> ambos, gana el codigo y este doc se corrige.

**Schema vigente:** `schema_version = "2"` (string, no entero).
**Audiencia:** desarrolladores de clientes (PowerShell, .NET WebAPI, Android HH, BOF VB.NET) que necesitan emitir, leer o procesar payloads del BRAIN sin reinventar formato.

---

## 1. Ideas centrales

El BRAIN funciona como un **intercambio asincronico via repo Git**. No hay
servidor, no hay DB centralizada, no hay HTTP. Los actores se hablan dejando
archivos JSON y Markdown en ramas orphan del repo
`tomwms-replit-client-automate` y firmando cada operacion con un commit.

Tres conceptos atomicos:

| Concepto | Que es | Donde vive |
|---|---|---|
| **Event** | Un JSON que dispara una conversacion con el brain (ej: "aplique un bundle", "necesito que el brain investigue X"). | `brain/_inbox/<id>.json` (rama `wms-brain`) |
| **Bundle** | Un paquete versionado de patches contra el WMS legacy (TOMIMSV4 VB.NET) con MANIFEST + md5 + marker. | `entregables_ajuste/<fecha>/v<NN>_bundle/` (rama `main`) |
| **Card** | Un archivo Markdown con una pregunta (Q-NNN), respuesta (A-NNN) o aprendizaje (L-NNN) referenciado desde un Event. | `brain/answers/A-NNN-*.md`, `brain/learnings/L-NNN-*.md`, etc. (rama `wms-brain`) |

Las cards Markdown son **artefactos referenciados**, no la entidad principal. La
entidad principal es el **Event**.

---

## 2. Schema versioning

El bridge declara `SCHEMA_VERSION = "2"` y emite todos los eventos nuevos con
ese valor. Schema v1 sigue valido por aditividad (un evento v1 procesa
identico, los analyzers viejos no se rompen).

| Schema | Tipos disponibles | Estados disponibles |
|---|---|---|
| **v1** (legacy WMS) | `apply_succeeded`, `apply_failed`, `skill_update`, `directive`, `merge_completed`, `external_change` | `pending`, `analyzed`, `proposed`, `applied`, `skipped` |
| **v2** (+investigacion SQL) | v1 + `question_request`, `question_answer`, `learning_proposed` | v1 + `answered` |

Override soportado: variable de entorno `WMS_BRAIN_FORCE_V1=1` fuerza emision
en formato legacy desde clientes que aun no migraron.

Negociacion sugerida en HTTP (cliente WebAPI .NET):
```
Header: X-Wms-Brain-Schema-Version: 2
```
Si el header falta, asumir `2`. Si el cliente manda `1`, el server rechaza
emitir v2 y filtra v2 de las respuestas.

---

## 3. Estructura canonica de un Event

```jsonc
{
  "id": "20260427-1845-EJC",          // YYYYMMDD-HHMM-INITIALS (asignado por bridge)
  "schema_version": "2",
  "created_at": "2026-04-27T18:45:00.000Z",  // ISO 8601 UTC
  "type": "question_request",          // ver §4
  "source": "wms-brain-client",        // openclaw|replit|manual|apply_bundle|wms-brain-client
  "host": "DESKTOP-EJC01",             // hostname() del emisor
  "ref": {                             // referencias externas (todos opcionales)
    "bundle": "v23",                   // v1: vinculo al bundle WMS
    "commit_sha": "abc1234",           // v1: sha del commit en TOMIMSV4
    "rama_destino": "dev_2028_merge",  // v1: rama merge target
    "files_changed": ["TOMIMSV4/.../frmAjusteStock.vb"],
    "marker": "#FIX_v23_...",          // anti-doble-apply marker
    // v2 (investigacion):
    "question_card_path": "questions/Q-007-stock-negativo.md",
    "question_id": "Q-007",
    "answer_card_path": "answers/A-007-stock-negativo.md",
    "learning_card_path": "learnings/L-003-rack-ghost-stock.md",
    "cliente": "K7-PRD",               // K7-PRD | BB-PRD | C9-QAS
    "evidence_paths": ["evidence/Q-007/sql-output-1.csv"]
  },
  "context": {
    "message": "Texto libre con contexto humano. RECOMENDADO.",
    "modules_touched": ["frmAjusteStock", "ajuste_rules"],
    "tags": ["validation", "ajuste"]
  },
  "analysis": null,                    // se llena en el step `analyze`
  "proposal": null,                    // se llena en el step `analyze`
  "status": "pending",                 // ver §5
  "decision": null,                    // se llena en `apply` o `skip`
  "history": [
    { "at": "2026-04-27T18:45:00.000Z", "action": "notify", "by": "wms-brain-client" }
  ]
}
```

**Reglas duras (validadas por el bridge):**

- `type` debe estar en la lista de §4. Bridge rechaza tipos desconocidos.
- `schema_version` debe ser exactamente `"2"` (string). Eventos viejos quedan
  con `"1"` y siguen procesando.
- `id`: formato `YYYYMMDD-HHMM-XXXX` donde `XXXX` son hasta 4 chars
  alfanumericos en mayusculas (las iniciales del autor o un random).
- `created_at`: ISO 8601, UTC recomendado.
- `history`: array, append-only. Cada entrada `{at, action, by?}`.
- `status` debe estar en la lista de §5.

---

## 4. Tipos de evento (catalogo completo)

### Familia v1 — eventos del WMS legacy

| Tipo | Significado | Source tipico |
|---|---|---|
| `apply_succeeded` | Un bundle vNN se aplico OK al repo TOMIMSV4. | `apply_bundle` |
| `apply_failed` | Un bundle vNN fallo en alguna validacion. | `apply_bundle` |
| `skill_update` | Una skill del brain (`brain/skills/...`) cambio y hay que propagar. | `replit` o `manual` |
| `directive` | Instruccion humana al brain (no asociada a un cambio). | `manual` |
| `merge_completed` | Un merge de rama efimera a `dev_2028_merge` (u otra) se completo. | `openclaw` |
| `external_change` | Cambio externo detectado (ej: Killios actualizo SAP B1, Cealsa cambio NAV). | `manual` |

### Familia v2 — eventos de investigacion SQL

| Tipo | Significado | Source tipico |
|---|---|---|
| `question_request` | El cliente PowerShell pide al brain investigar algo en una BD. Lleva `ref.question_card_path` y `ref.cliente`. | `wms-brain-client` |
| `question_answer` | Respuesta del brain a un `question_request`. Lleva `ref.answer_card_path` con verdict + confidence. | `replit` |
| `learning_proposed` | Propuesta de promover un hallazgo a regla del brain. Lleva `ref.learning_card_path`. | `replit` o `wms-brain-client` |

Conjunto auxiliar usado por el bridge: `INVESTIGATION_TYPES = {question_request, question_answer, learning_proposed}`. Estos NO pasan por la heuristica de matching contra `.md` del brain — generan una propuesta especializada.

---

## 5. Lifecycle de status

```
                                                          (apply --note "answered")
                                                                     |
                       (analyze)                (apply)              v
   pending  --------------------> proposed --------------> applied  | answered (solo question_request)
      |                              |                              |
      |                              |                              |
      |                              |                              |
      |     (skip --reason)          v                              |
      +----------------------> skipped <----------------------------+
                                                       (skip)

Estado terminal: applied, skipped, answered.
Estado intermedio: pending, proposed, analyzed (poco usado, alias historico).
```

Reglas:
- `skip` y `apply` son terminales: el evento se mueve de `_inbox/` a
  `_processed/` con `git rm + write` (commit atomico).
- `answered` es solo aplicable a `question_request`. Se dispara automaticamente
  si en `apply --note "..."` aparece la palabra `answered` o si se pasa
  `--status answered` explicitamente.
- `analyze` deja el evento en `_inbox/` con `status=proposed` y genera
  `brain/_proposals/<id>.md`.

---

## 6. Drafts (eventos sin id)

Algunos productores (notablemente `apply_bundle.mjs --brain-message ...` y el
modulo PowerShell `WmsBrainClient`) emiten un evento como **draft**:

```jsonc
{
  "schema_version": "1",        // o "2"
  "created_at": "2026-04-27T18:45:00",
  "type": "apply_succeeded",
  "source": "apply_bundle",
  "host": "...",
  "ref": { ... },
  "context": { ... },
  "analysis": null,
  "proposal": null,
  "status": "draft",            // <-- NO es uno de los 6 status validos
  "decision": null,
  "history": [
    { "at": "...", "action": "apply_bundle_draft" }
  ]
  // <-- NO tiene "id"
}
```

El draft se guarda como `<bundle>/brain_event.json` localmente. Despues, alguien
(humano o automatizacion) ejecuta:

```bash
node scripts/brain_bridge.mjs notify \
  --from-event-file <bundle>/brain_event.json \
  --exchange-repo <clon en rama wms-brain> \
  --initials EJC
```

El bridge **hidrata** el draft:
- Asigna `id = newEventId(initials)`.
- Setea `schema_version = "2"`.
- Setea `status = "pending"`.
- Hace `created_at` si falta.
- Appendea `{ action: "notify", by: source }` al `history`.
- Escribe en `brain/_inbox/<id>.json` y commit + push.

**Validacion de drafts:** el unico requerido es `type`. El resto puede venir
vacio. Validadores del lado cliente (ej: `Test-WmsBrainEventShape -AllowDraft`
del modulo PS) deben aceptar drafts y NO requerir `id`.

---

## 7. IDs (convencion oficial)

| Tipo de ID | Formato | Quien lo asigna |
|---|---|---|
| **Event ID** | `YYYYMMDD-HHMM-XXXX` | `brain_bridge.mjs` en `notify` (no el productor) |
| **Bundle version** | `vNN` (ej: `v23`) | El productor humano al armar el bundle |
| **Question card** | `Q-NNN-slug-corto.md` | El cliente PowerShell al crear la pregunta |
| **Answer card** | `A-NNN-slug-corto.md` | El brain al responder |
| **Learning card** | `L-NNN-slug-corto.md` | El brain o el cliente al proponer un learning |

`XXXX` en Event ID: hasta 4 chars alfanumericos en mayusculas. Si vienen
iniciales (`--initials EJC`) se usan; si no, un random base36 upper.

`NNN` en cards: tres digitos zero-padded, asignados secuencialmente por familia
(Q-001, Q-002, ..., Q-127, etc.). El contador no es atomico; se resuelve por
inspeccion del repo.

---

## 8. Bundles — formato de MANIFEST.json

Bundles viven en la rama `main` del repo (no en `wms-brain`). Estructura:

```
entregables_ajuste/
└── 2026-04-25/
    └── v23_bundle/
        ├── MANIFEST.json         <- contrato
        ├── patches/
        │   ├── 001-frmAjusteStock.patch
        │   └── 002-ajuste_rules.patch
        ├── brain_event.json      <- draft opcional generado por apply_bundle
        └── apply_log.json        <- escrito por apply_bundle al ejecutar
```

### MANIFEST.json — schema canonico

```jsonc
{
  "version": "v23",                    // o "bundle": "v23_bundle" (legacy)
  "date": "2026-04-25",                // o "fecha" (legacy)
  "rama_destino": "dev_2028_merge",
  "base_commit": "abc1234567",         // sha de TOMIMSV4 sobre el cual se genero
  "marker": "#FIX_v23_ELIMINAR_AJUSTE_RULES_2026-04-25",  // anti-doble-apply
  "description": "Eliminada validacion de borrador en ajuste de stock",
  "files": [                            // o "archivos" (legacy)
    {
      "path": "TOMIMSV4/Forms/frmAjusteStock.vb",
      "encoding": "utf8",               // o "utf-8"
      "patch_name": "001-frmAjusteStock.patch",
      "md5_orig": "a1b2c3d4...",
      "md5_mod":  "e5f6g7h8...",
      "summary": "Quita validacion lineas 234-267"
    }
  ],
  "compat": {                           // o "compatibilidad" (legacy)
    "min_bundle_version": "v22",
    "max_bundle_version": null
  }
}
```

**Tolerancia de campos:** el loader acepta nombres en espanol (`fecha`,
`archivos`, `marcador_global`, `descripcion`, `compatibilidad`, `cambios`) por
compatibilidad con bundles producidos antes de la normalizacion. La WebAPI
deberia **emitir solo en ingles** y **aceptar ambos**.

### apply_log.json — resultado de ejecutar el bundle

Escrito por `apply_bundle.mjs` al final de su ejecucion. Forma OK:

```jsonc
{
  "version": "v23",
  "date": "2026-04-25",
  "applied_at": "2026-04-25T19:15:32-03:00",  // ISO local con TZ
  "result": "OK",                              // OK | FAIL
  "branch": "agent/v23-20260425-e5f6",         // rama efimera
  "commit_sha": "9f8e7d6c5b...",
  "rama_destino": "dev_2028_merge",
  "files": [
    {
      "path": "...",
      "md5_orig_expected": "...",
      "md5_orig_actual":   "...",
      "md5_mod_expected":  "...",
      "md5_mod_actual":    "..."
    }
  ],
  "marker": "#FIX_v23_...",
  "marker_hits_actual": 1,
  "host": "DESKTOP-EJC01",
  "agent": "apply_bundle.mjs"
}
```

Forma FAIL:

```jsonc
{
  "version": "v23",
  "date": "2026-04-25",
  "applied_at": "2026-04-25T19:15:32-03:00",
  "result": "FAIL",
  "failed_at_check": "md5_orig_match",        // que precondicion fallo
  "error": "md5 mismatch en TOMIMSV4/...",
  "branch": "agent/v23-...",                   // si llego a crearla
  "branch_rolled_back": true,                  // si la efimera se borro
  "rama_destino": "dev_2028_merge"
}
```

---

## 9. Layout del repo (rama `wms-brain`)

```
brain/
├── _inbox/              <- eventos pending y proposed (JSON)
│   └── <id>.json
├── _proposals/          <- propuestas markdown (output del bridge analyze)
│   └── <id>.md
├── _processed/          <- eventos applied/skipped/answered (JSON)
│   └── <id>.json
├── skills/
│   └── wms-tomwms/
│       └── SKILL.md     <- regla operativa
├── agent-context/
│   └── AGENTS.md        <- regla del agente (humano o IA)
├── learnings/
│   └── L-NNN-*.md       <- hallazgos
├── answers/
│   └── A-NNN-*.md       <- respuestas a question_request
├── architecture/
│   └── adr/
│       └── ADR-NNN-*.md
├── clients/
│   ├── byb.md
│   ├── killios.md       <- (ejemplo) ficha de cliente
│   └── cealsa.md
├── decisions/
├── entities/
├── replit.md
├── README.md
└── way-of-thinking.md
```

Las carpetas que empiezan con `_` (`_inbox`, `_proposals`, `_processed`) son
**zona de mensajeria**. El resto es el cuerpo de conocimiento.

`ensureBrainCheckout()` del bridge **exige** que el clone este parado en
`wms-brain` antes de cualquier operacion. La WebAPI debe garantizar esto en
sus invocaciones al proceso hijo node.

---

## 10. Sub-comandos de brain_bridge (referencia rapida)

| Comando | Que hace | Status resultante |
|---|---|---|
| `notify` | Crea evento en `_inbox/`, hidrata draft si aplica, commit+push. | `pending` |
| `list` | Lista eventos por status. | (no muta) |
| `show` | Imprime un evento por id. | (no muta) |
| `analyze` | Heuristica + genera propuesta `.md` + actualiza evento. | `proposed` |
| `apply` | Marca evento como `applied` (o `answered`), mueve a `_processed/`, commit+push. | `applied` o `answered` |
| `skip` | Marca evento como `skipped`, mueve a `_processed/`, commit+push. | `skipped` |

Flag global util: `--no-push` (ejecuta cambios locales pero no toca git).

---

## 11. Mapping a la WebAPI .NET (preview, sera detallado en P-16b)

Endpoints sugeridos que **hablan nativo BRAIN** (sin traducir nombres):

```
GET    /api/v1/events?status=pending&type=question_request&cliente=K7-PRD
GET    /api/v1/events/{id}
POST   /api/v1/events                       # body: payload BRAIN canonico (con o sin id)
POST   /api/v1/events/{id}/analyze
POST   /api/v1/events/{id}/apply            # body: { note, by, status? }
POST   /api/v1/events/{id}/skip             # body: { reason }

POST   /api/v1/bundles/apply                # body: { bundleDir, ramaDestino, dryRun }
GET    /api/v1/bundles                      # lista bundles disponibles
GET    /api/v1/bundles/{version}            # MANIFEST + apply_log

POST   /api/v1/sync                         # equivalente a hello_sync.mjs
GET    /api/v1/system/version               # SCHEMA_VERSION + git refs + bridge version
GET    /healthz                             # liveness
GET    /healthz/ready                       # readiness (SQL + bridge + git)

POST   /api/v1/tenants/seed                 # registro de nueva empresa/bodega
```

**DTOs C# (esqueleto):**

```csharp
public abstract class BrainEvent {
    [JsonPropertyName("id")]              public string? Id { get; set; }
    [JsonPropertyName("schema_version")]  public string SchemaVersion { get; set; } = "2";
    [JsonPropertyName("created_at")]      public DateTimeOffset? CreatedAt { get; set; }
    [JsonPropertyName("type")]            public string Type { get; set; } = "";
    [JsonPropertyName("source")]          public string Source { get; set; } = "wms-brain-webapi";
    [JsonPropertyName("host")]            public string? Host { get; set; }
    [JsonPropertyName("ref")]             public EventRef? Ref { get; set; }
    [JsonPropertyName("context")]         public EventContext? Context { get; set; }
    [JsonPropertyName("analysis")]        public JsonElement? Analysis { get; set; }
    [JsonPropertyName("proposal")]        public JsonElement? Proposal { get; set; }
    [JsonPropertyName("status")]          public string Status { get; set; } = "draft";
    [JsonPropertyName("decision")]        public JsonElement? Decision { get; set; }
    [JsonPropertyName("history")]         public List<HistoryEntry> History { get; set; } = new();
}

public class EventRef {
    [JsonPropertyName("bundle")]              public string? Bundle { get; set; }
    [JsonPropertyName("commit_sha")]          public string? CommitSha { get; set; }
    [JsonPropertyName("rama_destino")]        public string? RamaDestino { get; set; }
    [JsonPropertyName("files_changed")]       public List<string>? FilesChanged { get; set; }
    [JsonPropertyName("marker")]              public string? Marker { get; set; }
    // v2:
    [JsonPropertyName("question_card_path")]  public string? QuestionCardPath { get; set; }
    [JsonPropertyName("question_id")]         public string? QuestionId { get; set; }
    [JsonPropertyName("answer_card_path")]    public string? AnswerCardPath { get; set; }
    [JsonPropertyName("learning_card_path")]  public string? LearningCardPath { get; set; }
    [JsonPropertyName("cliente")]             public string? Cliente { get; set; }
    [JsonPropertyName("evidence_paths")]      public List<string>? EvidencePaths { get; set; }
}

public class EventContext {
    [JsonPropertyName("message")]          public string? Message { get; set; }
    [JsonPropertyName("modules_touched")]  public List<string>? ModulesTouched { get; set; }
    [JsonPropertyName("tags")]             public List<string>? Tags { get; set; }
}

public class HistoryEntry {
    [JsonPropertyName("at")]      public DateTimeOffset At { get; set; }
    [JsonPropertyName("action")]  public string Action { get; set; } = "";
    [JsonPropertyName("by")]      public string? By { get; set; }
}
```

**Polymorphism:** opcional para v2. Los 9 types comparten estructura, no es
estrictamente necesario subclasear. Si la API lo hace, es solo para validacion
mas estricta de campos requeridos por type (ej: `question_request` exige
`ref.question_card_path` y `ref.cliente`).

---

## 12. Ejemplos de payload por tipo

### 12.1 `apply_succeeded` (v1, generado por apply_bundle)

```json
{
  "id": "20260425-1915-EJC",
  "schema_version": "1",
  "created_at": "2026-04-25T19:15:32-03:00",
  "type": "apply_succeeded",
  "source": "apply_bundle",
  "host": "DESKTOP-EJC01",
  "ref": {
    "bundle": "v23",
    "commit_sha": "9f8e7d6c5b",
    "rama_destino": "dev_2028_merge",
    "files_changed": ["TOMIMSV4/Forms/frmAjusteStock.vb"],
    "marker": "#FIX_v23_ELIMINAR_AJUSTE_RULES_2026-04-25"
  },
  "context": {
    "message": "v23 aplicado OK en TOMIMSV4 local. Falta merge a dev_2028_merge.",
    "modules_touched": ["frmAjusteStock", "ajuste_rules"],
    "tags": ["validation", "ajuste"]
  },
  "analysis": null,
  "proposal": null,
  "status": "pending",
  "decision": null,
  "history": [
    { "at": "2026-04-25T19:15:32-03:00", "action": "apply_bundle_draft" },
    { "at": "2026-04-25T19:18:01.000Z", "action": "notify", "by": "apply_bundle" }
  ]
}
```

### 12.2 `question_request` (v2, desde el cliente PowerShell)

```json
{
  "id": "20260427-1845-EJC",
  "schema_version": "2",
  "created_at": "2026-04-27T18:45:00.000Z",
  "type": "question_request",
  "source": "wms-brain-client",
  "host": "DESKTOP-EJC01",
  "ref": {
    "question_card_path": "questions/Q-007-rack-A12-stock-negativo.md",
    "question_id": "Q-007",
    "cliente": "K7-PRD"
  },
  "context": {
    "message": "Detecte stock negativo en rack A-12. Necesito que el brain investigue si es un caso del motor de reserva o un bug de ajuste.",
    "modules_touched": ["motor_reserva", "ajuste_stock"],
    "tags": ["stock-negativo", "rack-A12", "K7-PRD"]
  },
  "analysis": null,
  "proposal": null,
  "status": "pending",
  "decision": null,
  "history": [
    { "at": "2026-04-27T18:45:00.000Z", "action": "notify", "by": "wms-brain-client" }
  ]
}
```

### 12.3 `question_answer` (v2, respuesta del brain)

```json
{
  "id": "20260427-2030-BRN",
  "schema_version": "2",
  "created_at": "2026-04-27T20:30:00.000Z",
  "type": "question_answer",
  "source": "replit",
  "host": "replit-agent",
  "ref": {
    "question_card_path": "questions/Q-007-rack-A12-stock-negativo.md",
    "question_id": "Q-007",
    "answer_card_path": "answers/A-007-rack-A12-stock-negativo.md",
    "cliente": "K7-PRD",
    "evidence_paths": [
      "evidence/Q-007/sql-ajuste-historial.csv",
      "evidence/Q-007/sql-trans-stock-rack.csv"
    ]
  },
  "context": {
    "message": "verdict=confirmed confidence=high. Es bug de ajuste #L-003 (rack ghost stock).",
    "tags": ["stock-negativo", "ajuste", "L-003"]
  },
  "analysis": null,
  "proposal": null,
  "status": "pending",
  "decision": null,
  "history": [
    { "at": "2026-04-27T20:30:00.000Z", "action": "notify", "by": "replit" }
  ]
}
```

### 12.4 `learning_proposed` (v2, propuesta de regla)

```json
{
  "id": "20260427-2045-BRN",
  "schema_version": "2",
  "created_at": "2026-04-27T20:45:00.000Z",
  "type": "learning_proposed",
  "source": "replit",
  "host": "replit-agent",
  "ref": {
    "learning_card_path": "learnings/L-003-rack-ghost-stock.md",
    "cliente": "K7-PRD"
  },
  "context": {
    "message": "Promover el hallazgo Q-007/A-007 a regla del brain: cuando un rack tiene ajustes con offset > 5 unidades en menos de 24h, marcar para revision manual.",
    "tags": ["learning", "ajuste", "rack"]
  },
  "analysis": null,
  "proposal": null,
  "status": "pending",
  "decision": null,
  "history": [
    { "at": "2026-04-27T20:45:00.000Z", "action": "notify", "by": "replit" }
  ]
}
```

### 12.5 Draft (sin id, listo para hidratar)

```json
{
  "schema_version": "2",
  "created_at": "2026-04-27T18:44:00.000Z",
  "type": "question_request",
  "source": "wms-brain-client",
  "host": "DESKTOP-EJC01",
  "ref": {
    "question_card_path": "questions/Q-007-rack-A12-stock-negativo.md",
    "question_id": "Q-007",
    "cliente": "K7-PRD"
  },
  "context": {
    "message": "Stock negativo rack A-12 en K7-PRD. Investigar."
  },
  "analysis": null,
  "proposal": null,
  "status": "draft",
  "decision": null,
  "history": [
    { "at": "2026-04-27T18:44:00.000Z", "action": "client_draft" }
  ]
}
```

Notar: sin `id`, `status="draft"`, `history` con accion del productor (no
`notify`). El bridge lo hidrata al subirlo.

---

## 13. Reglas operativas (no opcionales)

1. **READ-ONLY a las 3 BDs SQL Server** (K7-PRD, BB-PRD, C9-QAS). La WebAPI
   solo acepta `SELECT` (validado server-side, no por privilegios SQL).
2. **Bundles versionados o nada**: cualquier mutacion al WMS legacy pasa por
   `entregables_ajuste/<fecha>/v<NN>_bundle/` con MANIFEST + md5 + marker.
3. **Marker anti-doble-apply** obligatorio en todo bundle. `apply_bundle` falla
   si el marker ya esta en el archivo.
4. **No se inventa nada en el brain**: todo learning/answer cita evidencia
   (SQL output, screenshot, log). Sin evidencia => no se merge.
5. **Drafts no son terminales**: un draft sin hidratar (sin `id`) NO es un
   evento valido. Solo el bridge puede asignar `id`.
6. **`history` es append-only**: nunca se sobreescribe ni se borra entrada.
7. **Sin secrets en payloads**: ninguna connection string, password, token o
   API key viaja en el JSON. Cifrado DPAPI fuera del payload.

---

## 14. Cambios futuros (politica)

Si el BRAIN evoluciona el schema (ej: schema_version `"3"`), la regla es:

1. Bridge sube `SCHEMA_VERSION = "3"`.
2. Eventos `"1"` y `"2"` siguen procesando (aditividad).
3. Este doc se actualiza con la familia nueva de tipos/campos.
4. La WebAPI agrega el `JsonDerivedType` correspondiente y los DTOs nuevos.
5. Contract tests del lado WebAPI validan round-trip con bridge real antes de
   merge.

Si una v3 rompe compatibilidad (ej: rename de campo), se discute primero,
se documenta deprecation path, y se da una ventana de al menos 2 pasadas para
que los clientes migren.

---

## 15. Referencias

- `scripts/brain_bridge.mjs` — implementacion canonica del bridge.
- `scripts/apply_bundle.mjs` — implementacion canonica del aplicador de bundles.
- `scripts/hello_sync.mjs` — handshake productor/consumidor.
- `entregables_ajuste/AGENTS.md` — contrato detallado de bundles.
- Rama `wms-brain` → `brain/README.md` — manifesto del cerebro.
- Rama `wms-brain-client` → `clients/wms-brain-client/` — cliente PowerShell.
- Rama `wms-brain-webapi` → (proxima) WebAPI .NET 8 hosted en IIS.

---

> Mantenedor: equipo PrograX24 (Erik Calderon).
> Ultima sincronizacion con bridge: schema_version `"2"` (rama main).
