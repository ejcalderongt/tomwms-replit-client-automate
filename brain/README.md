# brain — Cerebro del agente Replit que mantiene el WMS de Erik Calderón

> Esta carpeta es **el cerebro compartido**: doctrina, módulos del WMS,
> decisiones arquitectónicas (ADRs), reglas duras, skills, esquemas y
> análisis de los ciclos. Vive en la rama `wms-brain` del repo de
> intercambio (`tomwms-replit-client-automate`), separada como **orphan
> branch** sin historia compartida con `main`.
>
> **Política**: este repo de intercambio **NO contiene código del WMS**.
> Sólo bundles operativos (`main`), MVP de control (`openclaw-control-ui`),
> el cerebro funcional (`wms-brain`, esta rama) y el catálogo SQL del
> Killios PRD (`wms-db-brain`).

Snapshot: 2026-04-27. Mantenedor: agente Replit por encargo de Erik Calderón.

---

## Mapa de las cuatro ramas del repositorio

| Rama | Propósito | Tamaño | README |
|---|---|---:|---|
| `main` | Repo de intercambio: bundles, scripts, bridge | 24 archivos | `README.md` |
| `openclaw-control-ui` | MVP de bootstrap/control del brain | 15 archivos | `README.md` |
| **`wms-brain`** (esta) | **Cerebro funcional del agente** | 149 archivos | `brain/README.md` |
| `wms-db-brain` | Catálogo SQL extraído de Killios PRD | 636 archivos | `db-brain/README.md` |

> Las cuatro son **orphan branches**. Para clonar sólo ésta:
> `git clone --single-branch --branch wms-brain <url>`.

---

## Para qué sirve esta rama

Cuando el agente Replit necesita responder una pregunta de Erik, o generar
un bundle para corregir algo en el BOF/HH, **primero consulta este cerebro**
para no reinventar:

- Doctrina de cómo está construido el WMS (Solution VB.NET + Android).
- Módulos del dominio (reservas MI3, recepción, picking, despacho, etc.).
- Decisiones tomadas y por qué (ADRs).
- Reglas duras (no tocar X, no romper Y, hello sync antes de operar, etc.).
- Skills versionados (instrucciones reutilizables).
- Catálogo de tablas críticas con esquema validado live contra Killios PRD.

Si el cerebro no tiene la respuesta, el agente la **busca** (en el código,
en Killios, en chats previos), la **valida**, la **escribe acá** y luego la
usa. Cada ciclo de aprendizaje queda como artefacto durable.

---

## Estructura de la rama (detallada)

```
.
├── analysis/                          <- 10 docs analíticos de ciclos previas
│   ├── passada-2-bof-hh-killios.md           (8.6 KB)  bof + hh + killios overview
│   ├── passada-3-1-bloque-A-config-infra.md  (7.5 KB)  config e infra base
│   ├── passada-3-1-bloque-B-entidades-y-modelo.md      modelo de datos clasificado
│   ├── passada-3-1-bloque-C-transacciones-core.md      top 12 trans + mutadores
│   ├── passada-3-1-bloque-D-config-y-parametrizacion.md
│   ├── passada-3-1-bloque-E-mapa-profundo.md
│   ├── passada-3-2-bof-completo.md           (27 KB)   BOF VB.NET full
│   ├── passada-3-2-flujos-end-to-end.md      (44 KB)   8 flujos E2E
│   ├── passada-3-2-hh-android.md             (25 KB)   HH Android full
│   └── passada-3-2-killios-profundo.md       (27 KB)   Killios SQL profundo
│
├── brain/                             <- el cerebro propiamente dicho
│   ├── README.md                              <- este archivo
│   ├── BRIDGE.md                              <- mecanismo de actualización
│   ├── replit.md                              <- índice maestro WMS (11.6 KB)
│   │
│   ├── agent-context/                        <- contexto operativo para agentes
│   │   ├── AGENTS.md                                 (8.9 KB) protocolo agentes
│   │   ├── AZURE_ACCESS.md                           (10.6 KB) acceso Azure DevOps
│   │   └── CASE_INTAKE_TEMPLATE.md                   (4.8 KB) plantilla de casos
│   │
│   ├── decisions/                            <- ADRs estratégicos
│   │   └── 003-mi3-reescrito.md                      (15.7 KB) decisión motor MI3
│   │
│   ├── entities/                             <- modelos de dominio
│   │   ├── cases/                                    casos resueltos como ejemplo
│   │   │   └── case-2026-04-importar-lotes-cliente.md
│   │   ├── decisions/                                decisiones operativas (no ADR)
│   │   │   ├── dec-2026-04-killios-acceso-replit.md
│   │   │   └── dec-formato-commits.md
│   │   ├── modules/                                  ★ módulos del WMS
│   │   │   ├── reservation/                              motor MI3 (14 archivos ~270KB)
│   │   │   │   ├── README.md                                    índice del módulo
│   │   │   │   ├── 01-mi3-motor-nuevo-net8.md                   arq motor nuevo
│   │   │   │   ├── 02-mi3-motor-legacy-vb.md                    legacy VB.NET 8K líneas
│   │   │   │   ├── 03-comparison.md                             paridad legacy↔nuevo
│   │   │   │   ├── 04-mi3-config-propietario.md                 flags MI3 i_nav_config_enc
│   │   │   │   ├── 05-mi3-algoritmo-fefo-clavaud.md             FEFO + zona picking
│   │   │   │   ├── 06-mi3-handlers-detalle.md                   handlers pipeline
│   │   │   │   ├── 07-stock-res-ciclo-vida.md                   máquina estados stock_res
│   │   │   │   ├── 08-mi3-tablas-killios.md                     schema validado live 9 tablas
│   │   │   │   ├── 09-mi3-logging-observabilidad.md             IReservationLogger
│   │   │   │   ├── 10-mi3-errores-troubleshooting.md            5 runbooks
│   │   │   │   ├── 11-mi3-tests.md                              estrategia testing + rollout
│   │   │   │   └── 12-mi3-todos-roadmap.md                      19 riesgos + 8 decisiones + 8 sprints
│   │   │   ├── mod-arquitectura-solution.md                  arquitectura de la solución
│   │   │   ├── mod-cliente-lotes.md                          módulo cliente/lotes
│   │   │   ├── mod-conexion-sqlserver.md                     conexión SQL Server
│   │   │   ├── mod-importacion-excel.md                      importación Excel
│   │   │   ├── mod-repo-dba.md                               repo DBA
│   │   │   ├── mod-repo-exchange.md                          repo de intercambio
│   │   │   ├── mod-repo-tomhh2025.md                         repo HH Android
│   │   │   ├── mod-repo-tomwms-bof.md                        repo BOF VB.NET
│   │   │   └── mod-stack-tecnologico.md                      stack global
│   │   └── rules/                                    ★ reglas duras del agente
│   │       ├── rule-01-no-push-automatico-wms.md
│   │       ├── rule-02-no-mezclar-hh-backend.md
│   │       ├── rule-03-no-tocar-reference-vb.md
│   │       ├── rule-04-no-reescribir-desde-cero.md
│   │       ├── rule-05-utf8-bom-vb.md
│   │       ├── rule-06-migracion-xml-json-oportunista.md
│   │       ├── rule-07-nunca-loguear-secrets.md
│   │       ├── rule-08-killios-prod-solo-lectura.md
│   │       ├── rule-09-modulo-definition-sensible.md
│   │       ├── rule-10-hello-sync-antes-de-operar.md
│   │       ├── rule-11-cambios-incrementales.md
│   │       ├── rule-12-no-romper-compatibilidad.md
│   │       └── rule-no-fk-en-trans.md
│   │
│   ├── skills/                               <- skills versionados (source of truth)
│   │   ├── wms-tomwms/
│   │   │   ├── SKILL.md                              (25.5 KB) skill canónico WMS
│   │   │   └── conventions.md                        (6 KB) convenciones VB/SQL/JSON
│   │   └── wms-tomhh2025/
│   │       ├── SKILL.md                              (12.6 KB) skill HH Android
│   │       └── conventions.md                        (4.8 KB) convenciones Java/Android
│   │
│   ├── sql-catalog/                          <- extractor + DDL crítico
│   │   ├── README.md                                 (2.2 KB)
│   │   ├── extract.sql                               (3.4 KB) query SQL extractor
│   │   ├── extract_for_db_brain.mjs                  (21 KB) extractor Node→md
│   │   ├── extract_sql_catalog.py                    (8 KB) extractor Python
│   │   └── reservation-tables.md                     (30 KB) DDL 9 tablas reservation
│   │
│   ├── tasks-historicas/                     <- tareas resueltas como referencia
│   │   ├── task-1.md
│   │   └── validar-idstock-duplicado-excel.md
│   │
│   ├── wms-agent/                            <- CLI Python `wmsa`
│   │   ├── README.md
│   │   ├── pyproject.toml
│   │   ├── wmsa.cmd                                  entry point Windows
│   │   └── wmsa/                                     paquete Python
│   │       ├── __init__.py
│   │       ├── brain.py                              gestión del cerebro
│   │       ├── case.py                               manejo de casos
│   │       ├── cli.py                                CLI principal
│   │       ├── config.py                             config
│   │       ├── killios.py                            cliente Killios READ-ONLY
│   │       └── commands/__init__.py
│   │
│   ├── _inbox/                              <- eventos del bridge pendientes (.json)
│   ├── _proposals/                          <- propuestas del bridge (.md)
│   └── _processed/                          <- eventos resueltos (.json)
│
└── data/                              <- artefactos crudos JSON de extracciones
    ├── dbrain-killios-extracted-2026-04-26.json      (152 KB) snapshot BD
    ├── passada-3-1-bloque-A-*.json                   config files + parsed
    ├── passada-3-1-bloque-B-*.json                   entity files + tables clasificadas
    ├── passada-3-1-bloque-C-*.json                   trans top 12 + mutadores
    ├── passada-3-1-bloque-D-*.json                   config objects + values (5.8 MB)
    ├── passada-3-1-bloque-E-cruce.json               cruce profundo (130 KB)
    ├── passada-3-2-bof/*                             8 archivos BOF (4.4 MB total)
    ├── passada-3-2-flujos/*                          9 flujos E2E (~750 KB)
    ├── passada-3-2-hh-android/*                      7 archivos HH (~600 KB)
    └── passada-3-2-killios-deep/*                    5 archivos Killios deep (~3.9 MB)
```

---

## Subcarpetas: qué es cada una y cuándo consultarla

### `analysis/` — Ciclos analíticas

Documentos producto de ciclos de descubrimiento. Cada `passada-X-*.md` es
**autocontenido** y narra qué se aprendió en ese ciclo. Son lectura obligada
si te metés en un módulo nuevo: te ahorran semanas.

- **Ciclo 2** (`passada-2-bof-hh-killios.md`): primer overview BOF + HH +
  Killios.
- **Ciclo 3.1** (bloques A–E): partición exhaustiva del WMS por capas
  (config-infra, entidades, transacciones, parametrización, mapa profundo).
- **Ciclo 3.2**: ciclos profundas por subdominio (BOF completo, flujos
  E2E, HH Android, Killios profundo).

### `brain/agent-context/` — Cómo opera el agente

- `AGENTS.md`: protocolo operativo para agentes locales (cómo arrancar, qué
  validar, cómo loguear).
- `AZURE_ACCESS.md`: cómo acceder a Azure DevOps (`ejcalderon0892/`), qué
  scopes pedir al PAT, cómo manejar `dev_2028_merge`.
- `CASE_INTAKE_TEMPLATE.md`: plantilla para abrir un caso nuevo (qué
  preguntas hacerse, qué archivos tocar, qué validar al final).

### `brain/decisions/` — ADRs estratégicos

Estilo Architecture Decision Record. Cada decisión tiene contexto, opciones
evaluadas, decisión tomada, consecuencias.

- **`003-mi3-reescrito.md`**: decisión clave de **reescribir el motor
  `Insertar_Stock_Res_MI3` en .NET 8** desacoplado del legacy VB.NET 8K
  líneas. Justificación, opciones descartadas (parche vs reescribir vs
  microservicio), consecuencias (paridad funcional obligatoria, rollout
  canario por propietario, observabilidad reforzada).

### `brain/entities/` — Modelos de dominio

#### `entities/modules/reservation/` — Motor MI3 de reservas (★ flagship)

Documentación exhaustiva del motor `Insertar_Stock_Res_MI3`: el legacy
VB.NET (8K líneas) y el motor nuevo .NET 8 (en construcción). **14 archivos,
~270 KB**. Punto de entrada: `entities/modules/reservation/README.md`
(índice consolidado + lectura por rol: arquitecto / dev / QA / DBA / DevOps).

| Archivo | Tema | Tamaño |
|---|---|---:|
| `README.md` | Índice consolidado + lectura por rol + 4 hallazgos importantes | 8.7 KB |
| `01-mi3-motor-nuevo-net8.md` | Arquitectura motor nuevo (handlers, pipeline, DI) | 32 KB |
| `02-mi3-motor-legacy-vb.md` | Anatomía del legacy VB.NET 8K líneas | 35 KB |
| `03-comparison.md` | Mapeo legacy ↔ nuevo, paridad funcional, riesgos | 22 KB |
| `04-mi3-config-propietario.md` | Config en `i_nav_config_enc` (69 cols) — flags MI3 | 19 KB |
| `05-mi3-algoritmo-fefo-clavaud.md` | FEFO + zona picking + paquetes completos/incompletos | 18 KB |
| `06-mi3-handlers-detalle.md` | Detalle por handler de la cadena | 15 KB |
| `07-stock-res-ciclo-vida.md` | Máquina de estados `stock_res`, invariantes | 16 KB |
| `08-mi3-tablas-killios.md` | Schema validado live de 9 tablas críticas | 30 KB |
| `09-mi3-logging-observabilidad.md` | `IReservationLogger`, vocabulario checkpoints | 22 KB |
| `10-mi3-errores-troubleshooting.md` | Catálogo errores + 5 runbooks operativos | 18 KB |
| `11-mi3-tests.md` | Estrategia testing (unit/integration/canary) + rollout | 20 KB |
| `12-mi3-todos-roadmap.md` | TODOs + 19 riesgos + 8 decisiones D-01..D-08 + roadmap 8 sprints + DoD | 16 KB |

**Cross-refs**: `decisions/003-mi3-reescrito.md` + `sql-catalog/reservation-tables.md`.

**Verificá antes de tocar reservas o estrategia FEFO/Clavaud.**

#### `entities/modules/mod-*.md` — Módulos transversales

Documentación de aspectos transversales del WMS. Cada `mod-*.md` es un
documento autocontenido:

- `mod-arquitectura-solution.md`: la solución VB.NET y sus proyectos.
- `mod-cliente-lotes.md`: módulo cliente y manejo de lotes.
- `mod-conexion-sqlserver.md`: conexión SQL Server (cadena, pooling, retry).
- `mod-importacion-excel.md`: importación de Excel a la BD.
- `mod-repo-dba.md`: repo del DBA.
- `mod-repo-exchange.md`: este repo (`tomwms-replit-client-automate`).
- `mod-repo-tomhh2025.md`: repo Android HH (`TOMHH2025`).
- `mod-repo-tomwms-bof.md`: repo BOF (`TOMWMS_BOF`).
- `mod-stack-tecnologico.md`: stack global (.NET Framework, VB.NET, Android,
  SQL Server, Replit, etc.).

#### `entities/rules/` — Reglas duras del agente

Cada regla es **innegociable** y precede a cualquier opinión del agente.
Se leen siempre antes de decidir un cambio.

| Regla | Resumen |
|---|---|
| `rule-01-no-push-automatico-wms.md` | El agente NUNCA pushea a TOMWMS_BOF / TOMHH2025 |
| `rule-02-no-mezclar-hh-backend.md` | No mezclar lógica HH y backend en un mismo cambio |
| `rule-03-no-tocar-reference-vb.md` | No tocar `Reference.vb` generado por wsdl |
| `rule-04-no-reescribir-desde-cero.md` | No reescribir módulos enteros sin ADR previo |
| `rule-05-utf8-bom-vb.md` | Archivos VB.NET con UTF-8 BOM |
| `rule-06-migracion-xml-json-oportunista.md` | Migrar XML→JSON sólo cuando se toca el archivo |
| `rule-07-nunca-loguear-secrets.md` | Nunca loguear secrets |
| `rule-08-killios-prod-solo-lectura.md` | **Killios PRD es READ-ONLY**. Ninguna excepción. |
| `rule-09-modulo-definition-sensible.md` | Módulos sensibles (reservas, despacho) requieren validación extra |
| `rule-10-hello-sync-antes-de-operar.md` | Siempre `hello sync` antes de cualquier operación |
| `rule-11-cambios-incrementales.md` | Cambios pequeños, atómicos, reversibles |
| `rule-12-no-romper-compatibilidad.md` | No romper compat hacia atrás sin migración |
| `rule-no-fk-en-trans.md` | No agregar FKs en tablas `trans_*` (perf y volumen) |

### `brain/skills/` — Skills versionados

Source of truth de los skills:

- **`wms-tomwms/SKILL.md`** (25.5 KB): skill canónico para trabajar con el
  BOF VB.NET. Cubre: arquitectura, convenciones VB, patrones DAL, manejo de
  errores, integración con Killios.
- **`wms-tomwms/conventions.md`** (6 KB): convenciones VB/SQL/JSON específicas.
- **`wms-tomhh2025/SKILL.md`** (12.6 KB): skill para el HH Android. Cubre:
  Activities, services, HTTP calls, cruce HH↔server.
- **`wms-tomhh2025/conventions.md`** (4.8 KB): convenciones Java/Android.

### `brain/sql-catalog/` — Extractor + DDL crítico

- **`extract.sql`**: query SQL que se corre contra Killios para extraer el
  catálogo completo (tablas, vistas, SPs, funciones, columnas, FKs).
- **`extract_for_db_brain.mjs`** (21 KB): runner Node que toma el resultado
  del query y genera los markdown de la rama `wms-db-brain`.
- **`extract_sql_catalog.py`** (8 KB): variante Python.
- **`reservation-tables.md`** (30 KB): DDL completo de las **9 tablas críticas
  del módulo reservation** (`stock`, `stock_res`, `trans_pe_enc`,
  `trans_pe_det`, `i_nav_config_enc`, `i_nav_ped_traslado_det`,
  `log_error_wms`, `propietarios`, `propietario_bodega`) + índices recomendados
  + CHECK constraints sugeridos.

### `brain/wms-agent/` — CLI `wmsa` (Python)

CLI auxiliar en Python para que Erik (o el agente) opere el cerebro desde
línea de comandos:

- `wmsa case new` — crea un caso nuevo desde plantilla.
- `wmsa case list` — lista casos abiertos.
- `wmsa brain status` — estado del cerebro (sync, integridad).
- `wmsa killios query <sql>` — query READ-ONLY a Killios.

Punto de entrada Windows: `wmsa.cmd`. Config: `wmsa/config.py` (lee env
vars como `WMS_KILLIOS_DB_PASSWORD`).

### `brain/_inbox/`, `_processed/`, `_proposals/`

Buzones del bridge (ver `BRIDGE.md`):

- `_inbox/<ulid>.json`: eventos pendientes (preguntas de Erik, casos nuevos).
- `_processed/<ulid>.json`: eventos resueltos (con timestamp de resolución).
- `_proposals/<ulid>.md`: propuestas autogeneradas (cambios sugeridos al
  cerebro o al WMS).

### `data/` — Artefactos crudos JSON

JSONs producto de los ciclos de extracción. **NO se leen a mano**: son insumo
para los `analysis/*.md`. Se versionan para reproducibilidad.

Subcarpetas:
- `passada-3-1-bloque-*`: JSONs de el ciclo 3.1 por bloque (A..E).
- `passada-3-2-bof/`: 8 JSONs del BOF (DAL completo, entity completo, UI
  BOF, modern API, WS-SQL inline, etc.).
- `passada-3-2-flujos/`: 9 JSONs de flujos E2E (recepción, picking, packing,
  despacho, ajuste, inventario, reabastecimiento, reubicación, traslado).
- `passada-3-2-hh-android/`: 7 JSONs del HH Android.
- `passada-3-2-killios-deep/`: 5 JSONs del Killios profundo (SPs completos,
  tablas completas, funciones, log_error_wms, stock_hist patterns).

---

## Cómo opera el cerebro (BRIDGE)

Lectura obligada: `BRIDGE.md`.

```
Erik (Windows)                                Agente Replit
-------------                                 --------------
brain-query.ps1 -Question "..."   --->   _inbox/<ulid>.json (push a wms-brain)
                                              |
                                              v
                                         scripts/brain_bridge.mjs (en main)
                                              |
                                              v
                                         lee inbox, busca contexto en brain/
                                              |
                                              v
                                         _proposals/<ulid>.md (respuesta)
   <--- git pull         <---           push a wms-brain
                                              |
                                              v
                                         _processed/<ulid>.json (audit trail)
```

---

## Reglas de oro de esta rama

1. **Killios PRD es READ-ONLY.** Ninguna excepción. Ver `rule-08`.
2. **No reescribir desde cero.** Ver `rule-04`. Hay que partir del legacy
   y migrar incrementalmente.
3. **No loguear secrets.** Ver `rule-07`. Especialmente
   `WMS_KILLIOS_DB_PASSWORD`, `AZURE_DEVOPS_PAT`, `GITHUB_TOKEN`.
4. **Hello sync antes de operar.** Ver `rule-10`. Si `brain-up.ps1` falla,
   abortar.
5. **Cambios incrementales.** Ver `rule-11`. PRs pequeños, atómicos,
   reversibles.
6. **El cerebro no es opcional.** Antes de generar un bundle, leer el
   módulo correspondiente. Si falta documentación, ciclo de descubrimiento
   primero, bundle después.

---

## Cross-refs a otras ramas

- **Para entender los bundles operativos** → `main` →
  `entregables_ajuste/AGENTS.md`.
- **Para arrancar el ambiente** → `openclaw-control-ui` → `scripts/brain-up.ps1`.
- **Para ver el catálogo SQL Killios completo** → `wms-db-brain` →
  `db-brain/README.md` (tablas, vistas, SPs, funciones, parametrización).

---

## Estado actual (snapshot 2026-04-27)

- **149 archivos versionados** (~9 MB con `data/`).
- **brain/**: 51 archivos de doctrina (~ 600 KB).
- **brain/entities/modules/reservation/**: 14 archivos (~270 KB) — flagship.
- **brain/entities/rules/**: 13 reglas duras.
- **brain/skills/**: 4 skills (2 wms-tomwms + 2 wms-tomhh2025).
- **brain/sql-catalog/**: extractor + 1 DDL crítico (reservation-tables.md).
- **analysis/**: 10 docs de ciclos (~190 KB).
- **data/**: 25+ JSONs de extracciones crudas (~9 MB).
- **Última extracción Killios**: 2026-04-27T01:29Z (fresh).
- **Último ADR**: 003-mi3-reescrito.md.

---

## Roadmap pendiente del cerebro (no comprometido)

- Documentar módulos restantes con el mismo nivel que `reservation/`:
  `picking`, `packing`, `despacho`, `recepcion`, `ajuste`, `inventario`,
  `reubicacion`, `traslado`.
- Migrar `entities/modules/mod-*.md` planos a subcarpetas con README + docs
  por aspecto (cuando crezcan).
- Sumar más ADRs (`004-...`, `005-...`) según vayan apareciendo decisiones
  estratégicas.
- Expandir `brain/wms-agent/` con comandos para auditar el `_inbox/_processed`.
