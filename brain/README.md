# brain — del agente Replit que mantiene el WMS de Erik Calderon

Esta rama (`wms-brain`) es el **canal de conocimiento** del agente. Vive
separada de la rama `main` (que tiene los bundles operativos), como
**orphan branch** sin historia compartida.

> **Politica del repo**: este repo de intercambio (`tomwms-replit-client-automate`)
> NO contiene codigo WMS. Solo bundles, scripts y este brain.

---

## Estructura

```
brain/
├── README.md                  <- este archivo
├── BRIDGE.md                  <- mecanismo de actualizacion del brain
├── replit.md                  <- indice maestro WMS
├── skills/                    <- skills versionados (source of truth)
│   └── wms-tomwms/
│       ├── SKILL.md           <- skill canonico
│       └── conventions.md     <- convenciones VB/Java/SQL/JSON
├── agent-context/
│   ├── AGENTS.md              <- protocolo operativo (para agentes locales)
│   └── CASE_INTAKE_TEMPLATE.md
├── sql-catalog/               <- extractor del catalogo SQL productivo
├── wms-agent/                 <- CLI `wmsa` (Python)
├── tasks-historicas/          <- tareas resueltas como referencia
├── _inbox/                    <- eventos del bridge pendientes (.json)
├── _proposals/                <- propuestas generadas por el bridge (.md)
└── _processed/                <- eventos resueltos (.json)
```

---

## Que esta y que no esta versionado aca

### Si esta

- **Skills** (`skills/wms-tomwms/SKILL.md`, `conventions.md`): guia canonica
  del agente.
- **Indice maestro** (`replit.md`): topologia, equipo, reglas duras de alto nivel.
- **Protocolo agentes locales** (`agent-context/AGENTS.md`): como openclaw
  debe pensar el WMS.
- **Tooling**: `sql-catalog`, `wms-agent`.
- **Eventos del bridge**: cola estructurada de notificaciones.

### NO esta

- **Codigo WMS** (.vb, .java, .asmx, .sql del producto). Vive en Azure
  DevOps, no en GitHub. Regla dura.
- **`attached_assets/`**: capturas y data cruda. No es brain estructurado;
  cuando una captura sea referencia para un caso concreto, se incorpora al
  playbook puntual, no como dump.
- **Secrets**: nada con `WMS_*`, `BRAIN_IMPORT_TOKEN`, `AZURE_DEVOPS_PAT`,
  `GITHUB_TOKEN`. Solo referencias por nombre.

---

## Bridge — actualizacion del brain

Cuando se aplica un parche o funcionalidad al WMS, el brain puede necesitar
actualizarse para reflejar el nuevo estado. El **bridge** es el mecanismo
estructurado que conecta esos dos mundos:

1. Tras un `apply_bundle` OK (o por orden explicita), se escribe un evento
   en `brain/_inbox/<id>.json`.
2. El productor (Replit) corre `brain_bridge analyze <id>` que genera una
   propuesta heuristica en `brain/_proposals/<id>.md`.
3. El agente (humano o IA en sesion) revisa, edita los .md del brain si
   corresponde, y marca `brain_bridge apply <id>`.

Ver **`BRIDGE.md`** para flujo completo, comandos y estructura del evento.

---

## Como leer este brain del lado openclaw (consumidor)

```powershell
cd C:\tomwms-exchange
git fetch origin
git switch wms-brain        # cambia el working tree

# o sin cambiar de rama:
git show origin/wms-brain:brain/skills/wms-tomwms/SKILL.md
git show origin/wms-brain:brain/replit.md
```

Para listar eventos pendientes en el inbox:

```powershell
git fetch origin
git ls-tree --name-only origin/wms-brain:brain/_inbox/
```

---

## Reglas

1. La rama `wms-brain` **nunca** se mergea con `main`.
2. Cambios estructurales al SKILL deben ir acompanados de un evento
   `type=skill_update` para trazabilidad.
3. Eventos en `_inbox/` deben procesarse a `_processed/` (apply o skip)
   en cada sesion del productor. No deben acumularse.
4. `_proposals/<id>.md` es **escritura del bridge**. No editar a mano. Si
   se quiere documentar la decision, usar el campo `--note` del apply.

---

## Referencia cruzada

| Documento | Rol |
|---|---|
| `replit.md` | Indice maestro: topologia, reglas duras de alto nivel, infraestructura. |
| `skills/wms-tomwms/SKILL.md` | Skill canonico operativo. **Cargar en cada sesion.** |
| `skills/wms-tomwms/conventions.md` | Convenciones de codigo VB / Java / SQL / JSON. |
| `agent-context/AGENTS.md` | Protocolo para agentes locales (openclaw). |
| `BRIDGE.md` | Mecanismo de actualizacion del brain. |
| `wms-agent/README.md` | CLI `wmsa` para la PC del consumidor. |
