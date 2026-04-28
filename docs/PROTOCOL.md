# PROTOCOL.md — WmsBrainClient sobre el Brain Bridge existente

> **Estado**: alineado al contrato real de `scripts/brain_bridge.mjs`
> (rama `main` del repo de exchange, `SCHEMA_VERSION="1"`). Este documento
> ya **no** define un protocolo paralelo: define la integración del cliente
> PowerShell con el bridge existente y propone tres tipos de evento nuevos
> que requieren bumpear `SCHEMA_VERSION` a `"2"`.

---

## 0. Por qué este doc fue reescrito

El ciclo inicial del cliente PowerShell asumió que no existía un bridge
funcional y propuso un protocolo propio (`protocolVersion: 1`, eventos
con `kind: question`, paths inventados). **Estaba equivocada.** El
ecosistema ya tiene:

- `scripts/brain_bridge.mjs` (21 KB) — productor/consumidor de eventos.
- `scripts/apply_bundle.mjs` (20 KB) — aplica bundles, opcionalmente
  emite `brain_event.json` con `--brain-message`.
- `scripts/hello_sync.mjs` (8 KB) — handshake productor/consumidor.
- `brain/BRIDGE.md` (en rama `wms-brain`) — doctrina del bridge.

El cliente PowerShell **se sienta encima** de esto. No reinventa nada.
Solo agrega:

1. Una capa ergonómica PowerShell (verbo-sustantivo, hash result).
2. Un wrapper de `Invoke-Sqlcmd` con perfiles K7-PRD/BB-PRD/C9-QAS/LOCAL_DEV.
3. Tres tipos de evento nuevos para encajar **investigación SQL al brain
   de la BD** (question/answer/learning), porque el bridge actual no los
   contempla.

---

## 1. Contrato de evento — schema vigente del bridge

Tomado **literal** de `scripts/brain_bridge.mjs` (constante
`SCHEMA_VERSION = "1"`) y `brain/BRIDGE.md` §3.

### 1.1 Estructura JSON

```json
{
  "id": "20260427-1845-EJC",
  "schema_version": "1",
  "created_at": "2026-04-27T18:45:00-03:00",
  "type": "apply_succeeded",
  "source": "openclaw",
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
  "status": "pending",
  "decision": null,
  "history": [
    { "at": "2026-04-27T18:45:00-03:00", "action": "notify", "by": "EJC" }
  ]
}
```

### 1.2 Convenciones obligatorias

| Campo            | Regla                                                                |
|------------------|----------------------------------------------------------------------|
| `id`             | `YYYYMMDD-HHMM-INIT` (init = 1-4 letras del autor, ej. `EJC`).       |
| `schema_version` | string `"1"` hoy. `"2"` cuando se aceptin los nuevos types §4.       |
| `created_at`     | ISO-8601 con offset (no `Z` salvo UTC real).                         |
| `type`           | enum cerrado §1.3.                                                   |
| `source`         | `openclaw \| replit \| manual \| apply_bundle` (caja baja).          |
| `status`         | enum cerrado §1.4.                                                   |
| `history`        | array; cada operación del bridge agrega una entrada `{at, action, by}`. |

### 1.3 Tipos de evento — schema_version 1 (vigente)

| `type`             | Cuándo se emite                                                    |
|--------------------|--------------------------------------------------------------------|
| `apply_succeeded`  | tras `apply_bundle` OK del lado consumidor.                        |
| `apply_failed`     | tras `apply_bundle` FAIL (post-mortem).                            |
| `skill_update`     | edición explícita del skill (nueva regla descubierta).             |
| `directive`        | orden directa: "actualizá esto en el brain".                       |
| `merge_completed`  | tras merge a `dev_2028_merge` confirmado.                          |
| `external_change`  | algo externo cambió (BD, infra, equipo).                           |

### 1.4 Estados — schema_version 1 (vigente)

```
pending → analyzed → proposed → applied
                              ↘ skipped
```

| `status`    | Significado                                                         |
|-------------|---------------------------------------------------------------------|
| `pending`   | recién encolado en `brain/_inbox/<id>.json`.                        |
| `analyzed`  | el bridge corrió la heurística (candidatos en `analysis.candidate_files`). |
| `proposed`  | se generó `brain/_proposals/<id>.md` con la propuesta human-readable. |
| `applied`   | el agente Replit editó los `.md` del brain y movió el evento a `brain/_processed/`. |
| `skipped`   | se descartó con `--reason`; movido a `brain/_processed/`.           |

### 1.5 Paths del bridge (en rama `wms-brain` del repo de exchange)

```
brain/_inbox/<id>.json       eventos pending|analyzed|proposed
brain/_proposals/<id>.md     propuestas generadas por analyze
brain/_processed/<id>.json   eventos applied|skipped (terminales)
```

> **Crítico**: el bridge falla con mensaje claro si el clon de exchange no
> está checkouteado en rama `wms-brain`. Ver `brain_bridge.mjs:73`.

---

## 2. Cómo se enchufa el cliente PowerShell

El cliente NO reimplementa el bridge. **Delega**.

### 2.1 Bootstrap operativo (consumidor / openclaw / Erik)

```powershell
# 1. Handshake (vivía en hello_sync.mjs)
Invoke-WmsBrainHello -Rol consumidor `
  -ExchangeRepo C:\tomwms-exchange `
  -WmsRepo      C:\WMS\TOMWMS_BOF
# Internamente: node scripts/hello_sync.mjs --rol consumidor ...

# 2. (Opcional) Bootstrap del module local PowerShell
Invoke-WmsBrainBootstrap
# Internamente: .\scripts\brain-up.ps1 (existente; instala módulo,
# carga aliases, valida $env:WMS_KILLIOS_DB_PASSWORD).
```

### 2.2 Aplicar un bundle y notificar al brain

```powershell
# 1. Aplicar
Invoke-WmsBrainApplyBundle `
  -Latest `
  -Repo C:\WMS\TOMWMS_BOF `
  -BrainMessage "Eliminada validacion ajuste_rules en borrador" `
  -BrainModules "frmAjusteStock,ajuste_rules" `
  -BrainTags    "validation,ajuste" `
  -Yes
# Internamente: node scripts/apply_bundle.mjs --latest --repo ... \
#               --brain-message ... --brain-modules ... --brain-tags ... --yes
# Esto deja brain_event.json (type=apply_succeeded, status=draft) junto
# al apply_log.json del bundle.

# 2. Notificar (sube al inbox)
Invoke-WmsBrainNotify `
  -ExchangeRepo C:\tomwms-exchange `
  -FromEventFile C:\WMS\TOMWMS_BOF\entregables_ajuste\2026-04-27\v24_bundle\brain_event.json
# Internamente: node scripts/brain_bridge.mjs notify --from-event-file ... \
#               --exchange-repo C:\tomwms-exchange
```

> **Nota dura**: el cliente **nunca** escribe directamente en
> `brain/_inbox/`. Siempre pasa por `brain_bridge.mjs notify`. Esto
> garantiza id único, validación de schema, append a history y commit
> con mensaje correcto.

### 2.3 Emitir un evento sin bundle (skill_update / directive)

```powershell
# Construir el JSON localmente (helper PS) y delegar
$ev = New-WmsBrainEvent `
  -Type     skill_update `
  -Source   openclaw `
  -Message  "Descubierto: Despachar() requiere lock pesimista en stock" `
  -Modules  "Despachar,stock_locking" `
  -Tags     "skill,despacho,locking"
# Devuelve PSCustomObject + path al .json en $env:TEMP\WmsBrainEvents\

Invoke-WmsBrainNotify `
  -ExchangeRepo C:\tomwms-exchange `
  -FromEventFile $ev.Path
```

### 2.4 Lado productor (Replit / agente del brain)

El cliente PowerShell no se usa del lado productor (eso vive en
`brain_bridge.mjs list|show|analyze|apply|skip` directamente). Documentado
para referencia en CMDLETS.md §7.

---

## 3. Política — qué hace el cliente y qué NO

| ✅ El cliente hace                                              | ❌ El cliente NO hace                              |
|----------------------------------------------------------------|----------------------------------------------------|
| Wrappear los `.mjs` con cmdlets PowerShell ergonómicos.        | Reimplementar la cola de eventos.                  |
| Construir `brain_event.json` con schema válido.                | Escribir directo en `brain/_inbox/` (eso es del bridge). |
| Validar `$env:WMS_KILLIOS_DB_PASSWORD` antes de cualquier query. | Ejecutar writes contra K7-PRD / BB-PRD jamás.    |
| Correr suites/scenarios local (read-only) y emitir eventos con resultados. | Decidir merge a `dev_2028_merge`.                  |
| Resolver perfiles de conexión (K7-PRD/BB-PRD/C9-QAS/LOCAL_DEV). | Modificar `Reference.vb`.                          |
| Empujar question/answer/learning como eventos extendidos §4.   | Ejecutar `brain_bridge apply` sin revisión humana. |

---

## 4. Extensión propuesta — schema_version "2"

> **Estado**: PROPUESTA. Requiere aprobación de Erik + bump del
> `SCHEMA_VERSION` en `scripts/brain_bridge.mjs` + entrada en `VALID_TYPES`
> + un nuevo estado `answered`. Mientras no esté aceptada, las question
> cards viajan como `directive` con `tags=["question","Q-001"]` (workaround).

### 4.1 Tres tipos nuevos

| `type`               | Cuándo se emite                                                         |
|----------------------|-------------------------------------------------------------------------|
| `question_request`   | el cliente PS encola una pregunta SQL al brain de la BD (Q-001..Q-NNN). |
| `question_answer`    | el agente Replit produjo una answer card en respuesta a un question_request. |
| `learning_proposed`  | el cliente o el agente derivó una learning card (consolidación de varios answer/eventos). |

### 4.2 Un estado nuevo

| `status`    | Significado                                                         |
|-------------|---------------------------------------------------------------------|
| `answered`  | un `question_request` cuya `question_answer` ya fue producida (terminal). |

Transiciones:

```
question_request:   pending → analyzed → answered → applied | skipped
question_answer:    pending → analyzed → applied | skipped
learning_proposed:  pending → analyzed → proposed → applied | skipped
```

### 4.3 Campos obligatorios extra

- `question_request`:
  - `ref.question_id` (ej. `Q-001`)
  - `ref.question_card_path` (ej. `wms-brain/questions/sql/Q-001-stock-impacto-zonas.md`)
  - `context.expected_outputs[]` (lista de queries SQL clave / KPIs esperados)
- `question_answer`:
  - `ref.answers_question_id` (ej. `Q-001`)
  - `ref.answer_card_path` (ej. `wms-brain/answers/A-001-stock-impacto-zonas.md`)
  - `ref.evidence_paths[]` (CSVs / capturas / queries ejecutadas)
- `learning_proposed`:
  - `ref.learning_card_path`
  - `context.derived_from[]` (ids de events fuente)

### 4.4 Ejemplos canónicos

Ver `examples/brain_event_question_request.json`,
`examples/brain_event_question_answer.json`,
`examples/brain_event_learning_proposed.json`.

### 4.5 Compatibilidad

- Eventos schema_version `"1"` siguen siendo válidos sin tocar nada.
- Eventos schema_version `"2"` con `type` viejo son válidos.
- Eventos schema_version `"1"` con `type` nuevo son **inválidos** (rechazo
  en `notify`). Esto fuerza al cliente a setear `schema_version: "2"` cuando
  emite los nuevos tipos.

### 4.6 Migración

Bump del `SCHEMA_VERSION` constante en `brain_bridge.mjs` + extensión
de `VALID_TYPES` y `VALID_STATUSES`. **No requiere migración de eventos
viejos** porque son aditivos.

---

## 5. Workaround mientras la extensión §4 no esté aprobada

Para empezar a usar las 8 question cards Q-001..Q-008 ya escritas, sin
esperar el bump de schema, los emitimos como `directive`:

```powershell
$ev = New-WmsBrainEvent `
  -Type      directive `
  -Source    openclaw `
  -Message   "Q-001 Stock impacto por zonas. Ver wms-brain/questions/sql/Q-001-stock-impacto-zonas.md" `
  -Modules   "stock,zonas,locations" `
  -Tags      "question,Q-001,sql-research,wms-brain-client"

Invoke-WmsBrainNotify `
  -ExchangeRepo C:\tomwms-exchange `
  -FromEventFile $ev.Path
```

El campo `tags` con `question,Q-NNN` permite filtrar después con
`brain_bridge list --tag question`. Cuando se acepte §4, se migra el
helper `New-WmsBrainQuestionEvent` a usar `type: "question_request"`
nativo y este workaround se borra.

---

## 6. Errores comunes y diagnóstico

| Síntoma                                              | Causa probable / fix                                         |
|------------------------------------------------------|--------------------------------------------------------------|
| `brain_bridge: exchange-repo no esta en rama wms-brain` | `git -C $exchange checkout wms-brain` antes de notify.    |
| `brain_bridge: id duplicado en _inbox`               | Usar `New-WmsBrainEvent` (regenera id si colisiona).         |
| `apply_bundle: md5 mismatch`                         | Falta aplicar bundle previo. Ver `apply_log.json` del último OK. |
| `apply_bundle: marker ya presente`                   | Bundle ya aplicado. No es error, es idempotencia.            |
| `Invoke-Sqlcmd: Login failed`                        | `$env:WMS_KILLIOS_DB_PASSWORD` no seteada o expirada.        |
| `brain_event.json` con `type` rechazado              | type no está en `VALID_TYPES`. Si es un tipo §4, falta bump. |

---

## 7. Versionado de este documento

Cualquier cambio al PROTOCOL del cliente debe:

1. Reflejarse en este `PROTOCOL.md` con `### Cambios YYYY-MM-DD`.
2. Si afecta el shape del evento, coordinar con `brain_bridge.mjs`
   `SCHEMA_VERSION` (bump + entry en `VALID_TYPES`/`VALID_STATUSES`).
3. Si los eventos viejos no son compatibles, agregar nota de migración
   en `BRIDGE.md` §10.

### Cambios 2026-04-27

- Reescritura completa: alineado al `SCHEMA_VERSION="1"` real del bridge.
- Eliminado el `protocolVersion: 1` propio (era duplicación).
- Eliminado el `kind: question` propio (no respetaba `VALID_TYPES`).
- Documentadas las cmdlets como wrappers de los `.mjs` existentes.
- Propuesta formal §4: tipos `question_request`/`question_answer`/`learning_proposed` + estado `answered` para `SCHEMA_VERSION="2"`.
- Documentado workaround §5 con `directive` + `tags` mientras §4 no esté aprobada.
