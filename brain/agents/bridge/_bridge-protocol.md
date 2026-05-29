---
kind: bridge-protocol
version: 1.0
owners: [codex, mary-jane]
maintainer: erik-jose-calderon
created: 2026-05-21
status: active
---

# Puente de comunicación Codex ↔ Mary Jane

Canal estructurado y versionado para que los dos agentes principales del WMS se intercambien hallazgos, preguntas, decisiones y contexto complementario sin depender de Erik como puente humano para cada mensaje.

## Roles

| Rol | Agente | Responsabilidad primaria |
|---|---|---|
| **Lead operativo de descubrimiento** | Mary Jane | Conexión a BD productiva, modificación y prueba de código BOF/HH local, commits a TOMWMS_BOF / TOMHH2025 / DBA, generación de hallazgos primarios |
| **Curador / organizador / contexto complementario** | Codex (este agente) | Indexación de hallazgos en `wms-brain/`, cross-referencing contra patterns/handoffs/ADRs existentes, devolución de contexto complementario, mantenimiento del grafo del brain |
| **Owner del producto** | Erik José Calderón (EJC) | Decisiones de scope, prioridad, autorización de cambios destructivos, validación final |

## Estructura del puente

```
wms-brain/brain/agents/bridge/
├── _bridge-protocol.md        (este archivo — define el contrato)
├── INBOX-FROM-MARY-JANE.md    (Mary Jane escribe acá → Codex lee y procesa)
├── INBOX-FROM-CODEX.md        (Codex escribe acá → Mary Jane lee y procesa)
├── BRIDGE-LOG.md              (log append-only de eventos relevantes)
└── archive/
    └── YYYY-MM/               (mensajes resueltos, no se borran)
```

## Formato de mensaje

Cada mensaje en un INBOX debe tener front-matter YAML + body markdown. Los mensajes son append-only: nadie edita mensajes ajenos, solo agrega actualizaciones al final con `## Update YYYY-MM-DD HH:MM by <agente>`.

```markdown
---
id: 2026-05-21-001
from: codex
to: mary-jane
date: 2026-05-21T13:45:00Z
kind: heads_up | question | action_required | info | cross_ref
status: open | acked | resolved | wont_fix
priority: low | normal | high | blocker
refs:
  commits:
    - sha: 366de68a
      repo: tomwms-replit-client-automate
      branch: wms-brain
  files:
    - wms-brain/brain/agents/coordinator.yml
  handoffs:
    - wms-brain/brain/handoffs/2026-05-20-hh-recepcion-pallet-presentacion-cantidad.md
  patterns:
    - wms-brain/brain/code-changes/BOF/PATTERNS-RESERVA-PARIDAD-LEGACY-VS-CORE.md
  external:
    - azure-devops: TOMWMS_BOF#cb4726b9
    - janeway: indexRunId=55
ack_expected_by: 2026-05-23   # opcional, si es action_required
---

# Título conciso del mensaje

Cuerpo en markdown. Tan breve como sea posible sin perder contexto.

## Update 2026-05-22 09:00 by mary-jane
> Acked. Procediendo según opción B.
```

## Tipos de mensaje (`kind`)

| Kind | Cuándo usarlo | Quién espera qué |
|---|---|---|
| `heads_up` | Aviso informativo, no requiere acción | Receptor lee y marca `acked` o ignora |
| `question` | Pregunta concreta que necesita respuesta | Receptor responde como Update y marca `resolved` cuando se confirma |
| `action_required` | Pedido de hacer algo (commit, query, verificación) | Receptor ejecuta y reporta resultado como Update + `resolved` |
| `info` | Datos puros, contexto histórico, dump de cruce | Receptor incorpora al contexto, marca `acked` |
| `cross_ref` | "Esto que descubriste se relaciona con X que ya existe" | Receptor evalúa, posiblemente actualiza su trabajo, marca `acked` o `resolved` |

## Ciclo de vida de un mensaje

1. **Crear**: el emisor agrega el mensaje al final del INBOX correspondiente con `status: open` y commit con prefijo `#EJCRP brain(bridge): <kind> <título corto>`.
2. **Acked**: el receptor lee, agrega un Update reconociendo recepción, cambia a `status: acked` (si no requiere acción inmediata) o procesa y cambia a `status: resolved`.
3. **Resolved**: cuando el caso está cerrado. Se mueve al `archive/YYYY-MM/` con el commit `#EJCRP brain(bridge): archive <id>`.
4. **Wont_fix / obsolete**: si la condición cambió y el mensaje ya no aplica, se cierra como `wont_fix` con explicación en Update final.

## Reglas operativas (vinculantes)

1. **No editar mensajes ajenos**. Solo agregar Updates al final. Si un dato del mensaje original es incorrecto, se aclara en Update, no se modifica el original.
2. **No borrar mensajes**. Resueltos van a `archive/YYYY-MM/`, no a la papelera.
3. **Un mensaje = un commit**. Para que git refleje cada intercambio como evento atómico y se pueda hacer `git blame` sobre el INBOX.
4. **El BRIDGE-LOG.md se actualiza con eventos macro**, no con cada mensaje. Ej: "Janeway agregó endpoint /api/brain/agents/sync", "Erik aprobó opción A para estructura paralela", "Mary Jane comitteó parche reserva UMBAS en TOMWMS_BOF#cb4726b9".
5. **Priorizar refs concretos sobre prosa**. Si un mensaje cita un PATTERN, handoff, commit o archivo, debe ir en `refs:` con identificador exacto, no enterrado en el body.
6. **No bloquear**. Si Codex no puede responder en X tiempo o Mary Jane está ocupada, el otro agente continúa con su trabajo. El puente es asincrónico.
7. **Idioma**: español rioplatense, sin emojis (preferencia EJC).
8. **`wms-brain/` es la estructura principal** del brain. `brain/` (root) es legacy/consulta. Todo lo nuevo va a `wms-brain/brain/...`.

## Auto-actualización

Este puente es "auto-actualizable" en el sentido de que cada agente, al iniciar su sesión o procesar un caso nuevo, debe:

1. Leer su INBOX correspondiente (`INBOX-FROM-X.md`).
2. Procesar los mensajes con `status: open` que le aplican.
3. Agregar Updates correspondientes.
4. Si genera hallazgos nuevos relevantes para el otro agente, escribir mensaje en el INBOX del otro.
5. Commit con la convención `#EJCRP brain(bridge): ...`.

No requiere infraestructura técnica adicional (no es un MCP server ni un webhook) — es una convención sobre archivos versionados en git que ambos agentes respetan.

## Cambios al protocolo

Cualquier cambio a este archivo requiere aprobación explícita de EJC y debe incrementar `version` en el front-matter. Histórico de versiones se mantiene en git.
