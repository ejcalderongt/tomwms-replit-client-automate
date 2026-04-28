# BRIDGE — mecanismo de actualizacion del brain

Este documento describe **como un cambio operativo en el WMS llega al brain**.
Es la respuesta a la pregunta: *despues de aplicar un parche o funcionalidad,
como decide el brain si necesita actualizarse?*

---

## 1. Concepto

Un **evento** es un mensaje estructurado (JSON) que dice:
*"paso esto en el WMS, capaz el brain necesita reaccionar"*.

El brain NO se autoactualiza. El bridge solo:

1. **Recibe** el evento en `brain/_inbox/<id>.json`.
2. **Analiza** (heuristica simple) y genera una **propuesta** en
   `brain/_proposals/<id>.md` con candidatos de archivos a editar.
3. Espera que un **agente o humano** decida y edite los .md del brain.
4. Marca el evento como aplicado (mueve a `brain/_processed/<id>.json`).

**Politica fundamental**: el bridge **NO edita brain automaticamente**. Solo
propone. Un humano (o el agente Replit en sesion explicita) decide.

---

## 2. Tipos de evento

### 2.1 Schema v1 — eventos originados en el WMS

| type               | cuando |
|---|---|
| `apply_succeeded`  | tras un `apply_bundle` OK del lado consumidor. |
| `apply_failed`     | tras un `apply_bundle` FAIL (raro, util para post-mortem). |
| `skill_update`     | edicion explicita del skill (ej. nueva regla descubierta). |
| `directive`        | orden directa de Erik o del agente: "actualiza esto en el brain". |
| `merge_completed`  | tras un merge a `dev_2028_merge` confirmado. |
| `external_change`  | algo externo (BD, infraestructura, equipo) cambio. |

### 2.2 Schema v2 — eventos de investigacion SQL al brain de la BD

Originados en el cliente PowerShell **WmsBrainClient** (rama `wms-brain-client`),
que corre en la maquina Windows de Erik via Open Claw.

| type                 | cuando |
|---|---|
| `question_request`   | el cliente PS levanta una question card (`questions/Q-NNN-*.md`) y pide investigar el brain de la BD productiva (read-only). |
| `question_answer`    | el cliente PS produce un answer card (`answers/A-NNN-*.md`) con verdict + confidence + evidencia CSV. |
| `learning_proposed`  | el cliente PS propone elevar un hallazgo a regla del brain (learning card → `brain/skills/...` o `brain/learnings/L-NNN-*.md`). |

**Workaround obsoleto** (schema v1): hasta el bump v2, las preguntas se cursaban
como `type=directive` con `tags=["question","Q-NNN"]`. Ya no es necesario; el
cliente PS emite `question_request` nativo.

**Estados** (v2 agrega `answered`):

| status      | descripcion |
|---|---|
| `pending`   | recien notificado, sin analyze. |
| `analyzed`  | reservado (no usado en v0). |
| `proposed`  | analyze genero `_proposals/<id>.md`. |
| `applied`   | el agente edito el brain o registro la accion; evento movido a `_processed/`. |
| `skipped`   | descartado, no impacta el brain. |
| `answered`  | terminal para `question_request` cuyo `question_answer` fue aceptado. |

---

## 3. Estructura del evento

### 3.1 Schema v1 (eventos WMS — sin cambios)

```json
{
  "id": "20260427-1845-EJC",
  "schema_version": "1",
  "created_at": "2026-04-27T18:45:00-03:00",
  "type": "apply_succeeded",
  "source": "openclaw|replit|manual|apply_bundle",
  "host": "ERIK-DESK",
  "ref": {
    "bundle": "v23",
    "commit_sha": "abc1234",
    "rama_destino": "dev_2028_merge",
    "files_changed": ["TOMIMSV4/.../frmAjusteStock.vb"],
    "marker": "#FIX_v23_..."
  },
  "context": {
    "message": "texto libre describiendo el cambio y el porque",
    "modules_touched": ["frmAjusteStock", "ajuste_rules"],
    "tags": ["validation", "ajuste"]
  },
  "analysis": null,
  "proposal": null,
  "status": "pending|analyzed|proposed|applied|skipped",
  "decision": null,
  "history": [{"at": "...", "action": "notify"}]
}
```

### 3.2 Schema v2 (eventos de investigacion)

Mismo envelope. Diferencia: `type` es uno de los 3 nuevos y `ref` puede traer
campos opcionales especificos de investigacion. Todos los campos viejos siguen
siendo validos (ningun campo se removio).

```json
{
  "id": "20260427-1845-EJC",
  "schema_version": "2",
  "created_at": "2026-04-27T18:45:00-03:00",
  "type": "question_request",
  "source": "wms-brain-client",
  "host": "ERIK-DESK",
  "ref": {
    "question_id": "Q-003",
    "question_card_path": "questions/Q-003-ingresos-byb-pendientes.md",
    "answer_card_path": null,
    "learning_card_path": null,
    "cliente": "BB-PRD",
    "evidence_paths": []
  },
  "context": {
    "message": "Pregunta Q-003: 110795 INGRESOS pendientes en BB outbox - hipotesis NavSync solo procesa SALIDAS",
    "modules_touched": [],
    "tags": ["sap-sync", "navsync", "byb", "outbox"]
  },
  "analysis": null,
  "proposal": null,
  "status": "pending",
  "decision": null,
  "history": [{"at": "...", "action": "notify", "by": "wms-brain-client"}]
}
```

**Campos `ref` opcionales (v2)**:

| campo                 | tipo                                  | uso |
|---|---|---|
| `question_id`         | string (`Q-NNN`)                      | id corto de la question card. |
| `question_card_path`  | path relativo a la rama wms-brain-client | apunta al .md de la pregunta. |
| `answer_card_path`    | path relativo                         | solo para `question_answer`. |
| `learning_card_path`  | path relativo                         | solo para `learning_proposed`. |
| `cliente`             | `K7-PRD`/`BB-PRD`/`C9-QAS`/etc        | BD productiva contra la que se investigo. |
| `evidence_paths`      | array de paths CSV                    | dumps SQL de evidencia. |

---

## 4. Flujo completo

```
+----------------------+   apply OK       +-------------------------+
| openclaw             |  ------------->  | apply_bundle.mjs         |
| (consumidor Win)     |                  | --brain-message "..."    |
+----------------------+                  +-------------------------+
                                                       |
                                                       | escribe
                                                       v
                                          +-------------------------+
                                          | brain_event.json         |
                                          | (draft, en bundle dir)   |
                                          +-------------------------+
                                                       |
                                          brain_bridge notify
                                          --from-event-file ...
                                                       |
                                                       v
+----------------------+   git push       +-------------------------+
| repo intercambio     |  <-------------- | brain/_inbox/<id>.json   |
| rama wms-brain       |                  | status=pending           |
+----------------------+                  +-------------------------+
          |
          | git fetch (sesion productor)
          v
+----------------------+
| Replit (productor)   |
| brain_bridge list    |
| brain_bridge analyze |  -> brain/_proposals/<id>.md
| (humano edita .md)   |
| brain_bridge apply   |  -> brain/_processed/<id>.json
+----------------------+
```

---

## 5. Comandos

### 5.1 Lado consumidor (openclaw / Windows)

**Tras un apply OK**, agregar el flag al `apply_bundle`:

```cmd
node scripts\apply_bundle.mjs --latest --repo C:\WMS\TOMWMS_BOF --yes ^
  --brain-message "Eliminada validacion de ajuste_rules en borrador" ^
  --brain-modules "frmAjusteStock,ajuste_rules" ^
  --brain-tags "validation,ajuste"
```

Esto escribe `brain_event.json` en el bundle dir junto al `apply_log.json`.
**No empuja nada todavia.**

Para empujar al inbox del brain:

```cmd
node scripts\brain_bridge.mjs notify ^
  --exchange-repo C:\tomwms-exchange ^
  --from-event-file C:\WMS\TOMWMS_BOF\entregables_ajuste\2026-04-25\v23_bundle\brain_event.json
```

> El `--exchange-repo` debe ser un clon **ya checkouteado en rama
> wms-brain**. Si no, el bridge falla con mensaje claro.

### 5.2 Lado productor (Replit)

```bash
# Ver eventos pendientes
node scripts/brain_bridge.mjs list --exchange-repo /tmp/exchange-rw

# Inspeccionar uno
node scripts/brain_bridge.mjs show --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC

# Generar propuesta heuristica (busca menciones de modulos/tags/marker en .md del brain)
node scripts/brain_bridge.mjs analyze --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC

# (humano edita brain/skills/.../*.md o lo que corresponda)

# Marcar como aplicado
node scripts/brain_bridge.mjs apply --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC \
  --note "Actualizada SKILL §6: regla ajuste_rules ya no aplica" --by EJC

# O descartar (el cambio no impacta el brain)
node scripts/brain_bridge.mjs skip --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC \
  --reason "Solo refactor cosmetico, brain no se entera"
```

---

## 6. Que hace el `analyze`

El `analyze` dispatcheza por `event.type`:

### 6.1 Eventos WMS (schema v1) — heuristica de matches en .md

1. Lee todos los `.md` bajo `brain/` (excluye `_inbox/`, `_proposals/`,
   `_processed/`).
2. Por cada `module_touched`, `tag`, `marker` y `basename` de archivo
   cambiado, busca menciones case-insensitive en cada .md.
3. Los archivos con >=1 match se listan como **candidatos**, con la linea
   exacta del match.
4. Genera `brain/_proposals/<id>.md` con:
   - Contexto del evento.
   - Lista de archivos candidatos + extractos.
   - Sugerencia de accion (mantener / actualizar / eliminar).

**No** llama a un LLM. **No** edita el brain. Solo dirige la atencion del
revisor a los archivos relevantes.

### 6.2 Eventos de investigacion (schema v2) — propuesta dirigida

Para `question_request`, `question_answer`, `learning_proposed` el `analyze`
NO busca matches en .md. Genera una propuesta especializada que:

- Resume el evento (id, tipo, contexto, cliente/BD).
- Lista los siguientes pasos especificos del subtipo:
  - **`question_request`**: abrir question card → ejecutar SQL → producir answer card → emitir `question_answer` → marcar como `answered`.
  - **`question_answer`**: validar verdict + confidence → opcionalmente emitir `learning_proposed` → marcar el `question_request` original como `answered`.
  - **`learning_proposed`**: decidir destino (`brain/skills/`, `brain/agent-context/`, `brain/learnings/`) → editar el .md → marcar como `applied`.

Misma regla dura: el bridge NO edita el brain. Solo propone.

### Evolucion futura (no en v0)

- LLM-driven analysis: enviar el evento + extractos de los .md candidatos a
  un modelo y pedirle un diff propuesto.
- Auto-apply de cambios triviales (ej. actualizar fechas, version numbers).
- Web UI para revisar la cola de eventos.

---

## 7. Reglas duras del bridge

1. El bridge **nunca** edita `brain/skills/`, `brain/agent-context/`,
   `brain/replit.md` u otros archivos de conocimiento. Solo escribe en
   `brain/_inbox/`, `brain/_proposals/`, `brain/_processed/`.
2. Cualquier edit al brain (post analyze) es **manual** y queda commiteado
   con el mismo `apply` (que stagea con `git add brain/`).
3. Toda decision (apply, skip) queda registrada en `history` del evento y
   en el commit de git con la `--note` o `--reason`.
4. La rama `wms-brain` **nunca** se mergea con `main`. Son canales
   independientes (orphan branch).
5. Eventos sin `message` se aceptan pero el analyze va a tener menos data
   util. Recomendado **siempre** pasar `--brain-message` o `--message`.

---

## 8. Que **no** es el bridge

- **No es un sistema de tickets**. No reemplaza Jira ni el flujo de issues.
- **No es un CI/CD del brain**. No corre tests ni valida coherencia entre
  archivos del brain.
- **No es un trigger de operaciones en el WMS**. Solo registra que pasaron.
- **No es un agente autonomo**. Es una cola estructurada que el agente o el
  humano consumen.

---

## 9. Roadmap

- [x] v0: notify + list + show + analyze + apply + skip, heuristica simple.
- [ ] v0.1: `brain_bridge stats` — historial agregado por tipo, modulo, autor.
- [ ] v1: `brain_bridge analyze --llm` — usar LLM para diff propuesto.
- [ ] v1.1: integracion con WikiHub (notificar cambios al portal humano).
- [ ] v2: web UI para revisar inbox.

---

## 10. Versionado de este documento

Cualquier cambio al bridge debe:

1. Reflejarse en este BRIDGE.md.
2. Bumpear el `SCHEMA_VERSION` en `scripts/brain_bridge.mjs` si cambia el
   formato del evento.
3. Documentar la migracion si los eventos viejos no son compatibles.

### 10.1 Changelog

#### v2 — 2026-04-27 (ciclo-11)

**Motivo**: el cliente PowerShell `WmsBrainClient` (rama `wms-brain-client`)
necesita emitir eventos de investigacion SQL al brain de la BD productiva
(Killios/BYB/CEALSA, read-only). En v1 se cursaban como `directive` con
`tags=["question","Q-NNN"]`, lo que perdia semantica y rompia el analyze
heuristico (matcheaba "question" en cualquier .md).

**Cambios**:

- `SCHEMA_VERSION` `"1"` → `"2"`.
- 3 tipos nuevos en `VALID_TYPES`: `question_request`, `question_answer`,
  `learning_proposed`.
- 1 estado nuevo en `VALID_STATUSES`: `answered` (terminal para
  `question_request` cuyo answer fue aceptado).
- Constante `INVESTIGATION_TYPES` (Set) para dispatch en `analyze`.
- Funcion `cmdAnalyzeInvestigation`: genera propuesta dirigida segun subtipo
  (no busca matches en .md).
- `cmdApply` reconoce el case `question_request + note=answered` y bumpea
  `status=answered` en lugar de `applied`.
- `cmdHelp` actualizado.

**Retrocompatibilidad**:

- Eventos `schema_version="1"` siguen procesandose normal — los 6 tipos viejos
  pasan por el analyzer heuristico igual que antes.
- Ningun campo se removio. Los 3 campos nuevos en `ref` (`question_id`,
  `question_card_path`, etc.) son opcionales.
- El estado `answered` solo aplica a `question_request`; el resto sigue con
  los 5 estados originales.

**Migracion** (workaround → v2):

- Eventos viejos cursados como `directive + tags=["question","Q-NNN"]` se
  pueden dejar como estan (estan en `_processed/` o `_inbox/` legacy). El
  cliente PS de aqui en adelante emite el tipo nativo `question_request`.
- Documentacion de la transicion: `wms-brain-client/questions/MIGRATION-NOTE.md`.

#### v1 — 2026-04 (inicial)

- 6 tipos: `apply_succeeded`, `apply_failed`, `skill_update`, `directive`,
  `merge_completed`, `external_change`.
- 5 estados: `pending`, `analyzed`, `proposed`, `applied`, `skipped`.
- `analyze` heuristico (matches en .md).
- `notify` / `list` / `show` / `analyze` / `apply` / `skip`.
