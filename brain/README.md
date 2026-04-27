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
├── entities/                  <- modelos de dominio del WMS (modulos)
│   └── modules/
│       ├── reservation/       <- motor MI3 de reservas (13 docs ~270KB)
│       ├── mod-arquitectura-solution.md
│       ├── mod-cliente-lotes.md
│       ├── mod-conexion-sqlserver.md
│       ├── mod-importacion-excel.md
│       ├── mod-repo-dba.md
│       ├── mod-repo-exchange.md
│       ├── mod-repo-tomhh2025.md
│       ├── mod-repo-tomwms-bof.md
│       └── mod-stack-tecnologico.md
├── decisions/                 <- decisiones arquitectonicas (ADR-style)
│   └── 003-mi3-reescrito.md   <- decision de reescribir motor MI3 en .NET 8
├── sql-catalog/               <- extractor + DDL del catalogo SQL productivo
│   └── reservation-tables.md  <- DDL de las 9 tablas del modulo reservation
├── wms-agent/                 <- CLI `wmsa` (Python)
├── tasks-historicas/          <- tareas resueltas como referencia
├── _inbox/                    <- eventos del bridge pendientes (.json)
├── _proposals/                <- propuestas generadas por el bridge (.md)
└── _processed/                <- eventos resueltos (.json)
```

---

## Modulos documentados

### `entities/modules/reservation/` — Motor MI3 de reservas

Documentacion exhaustiva del motor `Insertar_Stock_Res_MI3`: el legacy
VB.NET (8K lineas) y el motor nuevo .NET 8 (en construccion). 13 archivos,
~270 KB. **Verifica antes de tocar reservas o estrategia FEFO/Clavaud.**

Entrada principal: `entities/modules/reservation/README.md` (indice del modulo).

| Archivo | Tema |
|---|---|
| `01-mi3-motor-nuevo-net8.md` | Arquitectura motor nuevo (handlers, pipeline, DI) |
| `02-mi3-motor-legacy-vb.md` | Anatomia del legacy VB.NET 8K lineas |
| `03-comparison.md` | Mapeo legacy <> nuevo, paridad funcional, riesgos |
| `04-mi3-config-propietario.md` | Config en `i_nav_config_enc` (69 cols) — flags MI3 |
| `05-mi3-algoritmo-fefo-clavaud.md` | FEFO + zona picking + paquetes completos/incompletos |
| `06-mi3-handlers-detalle.md` | Detalle por handler de la cadena |
| `07-stock-res-ciclo-vida.md` | Maquina de estados de `stock_res`, invariantes |
| `08-mi3-tablas-killios.md` | Schema validado live de 6 tablas criticas |
| `09-mi3-logging-observabilidad.md` | `IReservationLogger`, vocabulario checkpoints |
| `10-mi3-errores-troubleshooting.md` | Catalogo errores + 5 runbooks operativos |
| `11-mi3-tests.md` | Estrategia testing (unit/integration/canary) + rollout |
| `12-mi3-todos-roadmap.md` | TODOs + 19 riesgos + 8 decisiones + roadmap 8 sprints |
| `README.md` | Indice consolidado del modulo + lectura por rol |

**Cross-refs**: `decisions/003-mi3-reescrito.md` + `sql-catalog/reservation-tables.md`.

### `entities/modules/mod-*.md` — Modulos transversales

Documentacion de aspectos transversales del WMS (arquitectura, conexion
SQL, repos, etc.). Cada `mod-*.md` es un documento autocontenido.

---

## Que esta y que no esta versionado aca

### Si esta

- **Skills** (`skills/wms-tomwms/SKILL.md`, `conventions.md`): guia canonica
  del agente.
- **Indice maestro** (`replit.md`): topologia, equipo, reglas duras de alto nivel.
- **Protocolo agentes locales** (`agent-context/AGENTS.md`): como openclaw
  debe pensar el WMS.
- **Modulos del dominio** (`entities/modules/`): documentacion funcional
  por subsistema.
- **Decisiones arquitectonicas** (`decisions/`): ADRs trazables.
- **Catalogo SQL** (`sql-catalog/`): DDLs validados live contra produccion.
- **Tooling**: `wms-agent`.
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
git show origin/wms-brain:brain/entities/modules/reservation/README.md
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
5. **Toda afirmacion sobre BD productiva** (Killios `52.41.114.122,1437`)
   debe validarse READ-ONLY contra `INFORMATION_SCHEMA` antes de versionar.
   Las versiones v2 de los archivos del modulo `reservation` son ejemplo
   de correccion tras descubrir campos inventados.

---

## Referencia cruzada

| Documento | Rol |
|---|---|
| `replit.md` | Indice maestro: topologia, reglas duras de alto nivel, infraestructura. |
| `skills/wms-tomwms/SKILL.md` | Skill canonico operativo. **Cargar en cada sesion.** |
| `skills/wms-tomwms/conventions.md` | Convenciones de codigo VB / Java / SQL / JSON. |
| `agent-context/AGENTS.md` | Protocolo para agentes locales (openclaw). |
| `BRIDGE.md` | Mecanismo de actualizacion del brain. |
| `entities/modules/reservation/README.md` | Indice del modulo motor MI3 de reservas. |
| `decisions/003-mi3-reescrito.md` | Decision de reescribir motor MI3 en .NET 8. |
| `sql-catalog/reservation-tables.md` | DDL de las 9 tablas del modulo reservation. |
| `wms-agent/README.md` | CLI `wmsa` para la PC del consumidor. |
