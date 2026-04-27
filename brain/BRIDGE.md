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

### 2.0 Schema v1 (vigente desde el dia 1)

| type               | cuando |
|---|---|
| `apply_succeeded`  | tras un `apply_bundle` OK del lado consumidor. |
| `apply_failed`     | tras un `apply_bundle` FAIL (raro, util para post-mortem). |
| `skill_update`     | edicion explicita del skill (ej. nueva regla descubierta). |
| `directive`        | orden directa de Erik o del agente: "actualiza esto en el brain". |
| `merge_completed`  | tras un merge a `dev_2028_merge` confirmado. |
| `external_change`  | algo externo (BD, infraestructura, equipo) cambio. |

### 2.1 Schema v2 — investigacion SQL al brain de la BD (desde 2026-04-27)

| type                  | cuando                                                                       |
|-----------------------|------------------------------------------------------------------------------|
| `question_request`  | el cliente PS encola una pregunta SQL al brain de la BD (Q-001..Q-NNN).      |
| `question_answer`   | el agente Replit produjo una answer card en respuesta a un `question_request`. |
| `learning_proposed` | derivacion de varios eventos en una learning card consolidada.               |

Bumpear el schema **es retrocompatible**: eventos schema 1 siguen siendo
validos. Los tres tipos nuevos requieren `schema_version: "2"` o el
bridge los rechaza en `notify`.

Ver propuesta detallada en `wms-brain-client/EXTENSION-V2-PROPOSAL.md`.

---

## 3. Estructura del evento

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
  "status": "pending|analyzed|proposed|applied|skipped|answered",
  "decision": null,
  "history": [{"at": "...", "action": "notify"}]
}
```

---

## 3.1 Estados — schema v1

```
pending -> analyzed -> proposed -> applied
                                \-> skipped
```

| status      | significado                                                              |
|-------------|--------------------------------------------------------------------------|
| `pending` | recien encolado en `brain/_inbox/<id>.json`.                           |
| `analyzed`| el bridge corrio la heuristica (candidatos en `analysis.candidate_files`). |
| `proposed`| se genero `brain/_proposals/<id>.md` con la propuesta human-readable.  |
| `applied` | el agente edito los .md del brain y movio el evento a `_processed/`.   |
| `skipped` | se descarto con `--reason`; movido a `_processed/`.                  |

## 3.2 Estados — schema v2 (desde 2026-04-27)

Se agrega un estado terminal especifico para `question_request`:

| status      | significado                                                              |
|-------------|--------------------------------------------------------------------------|
| `answered`| un `question_request` cuya `question_answer` ya fue producida (terminal antes de aplicar la doctrina). |

Transiciones por type:

```
question_request:   pending -> proposed -> answered -> applied | skipped
question_answer:    pending -> proposed -> applied | skipped
learning_proposed:  pending -> proposed -> applied | skipped
```

El flip a `answered` se hace **automaticamente** cuando se procesa un
`question_answer` cuyo `ref.answers_event_id` apunta a un
`question_request` vivo. No requiere subcomando manual.

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

## 6. Que hace el `analyze` (heuristica v0)

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
