---
name: wms-tomwms
description: Skill canónico del proyecto TOM WMS (Warehouse Management System) de Erik Calderón / PrograX24. Cargar antes de cualquier tarea sobre el WMS — bug fix, nueva feature, análisis, refactor, consulta. Cubre arquitectura, repos, protocolos HH↔WS, modelo de datos, conexión a BD productiva, fuentes de conocimiento (Brain API, WikiHub, wms-brain repo) y reglas duras del proyecto.
---

# wms-tomwms — Skill canónico

Este skill es la **única referencia local** que el agente Replit debe cargar para trabajar en TOM WMS. Es el desarrollo de los temas que `replit.md` solo lista como índice.

> **Fuente de verdad versionada**: la copia maestra de este skill vive en la rama `wms-brain` del repo de intercambio (`brain/skills/wms-tomwms/SKILL.md`). El archivo en `.local/skills/wms-tomwms/SKILL.md` del workspace es una **working copy** que el agente lee en sesión. Ante divergencia, gana la rama `wms-brain`.

---

## 1. Identidad y entorno

- **Producto**: TOM WMS — Warehouse Management System multi-cliente.
- **Empresa**: PrograX24.
- **Owner técnico**: Erik José Calderón (EJC).
- **Cliente activo en este workspace**: **Killios** (BD `TOMWMS_KILLIOS_PRD`).
- **Otros clientes con releases productivas**: Becofarma, La Cumbre, Cealsa, Mampa (MHS).
- **Rol del agente Replit**: ingeniero senior permanente con conocimiento del WMS entre sesiones. **Productor** de bundles que el consumidor (`openclaw` en Windows) aplica al repo WMS.

### Stack

| Capa | Tecnología |
|---|---|
| Backend principal | VB.NET, .NET Framework 4.8, **VS 2026 + DevExpress 24** |
| BD productiva | SQL Server 2022 CU18 Standard sobre EC2 AWS |
| Handheld (HH) | Java, Android API minSdk/targetSdk=24, compileSdk=34, app `com.dts.tom` v8.2.3 |
| Web moderno (parcial) | C# .NET Core / .NET 8.0 (EntityCore + DALCore + WMSWebAPI), migración **incompleta** |
| Backend tooling agente | Node 24, Python 3.11+, pnpm workspace |

---

## 2. Topología de repos

| Repo | Hosting | Lenguaje | Rol | Branch activa |
|---|---|---|---|---|
| `TOMWMS_BOF` | Azure DevOps `ejcalderon0892` (376 MB · 9609 archivos · 3218 `.vb`) | VB.NET + C# | Backend core: WinForms `TOMIMSV4`, `WSHHRN` (`.asmx`), `WSSAPSYNC`, `WMSWebAPI`, `Entity`, `DAL`, `EntityCore`, `DALCore`, interfaces (NAV, SAP, Cealsa, DMS, AWS). **El cerebro de código.** | `dev_2028_merge` (default Azure: `master`) |
| `TOMHH2025` | Azure DevOps `ejcalderon0892` (14 MB) | Java | App Android Handheld (405 `.java`, 55 `Activity`). | `dev_2028_merge` (default Azure: `master`) |
| `DBA` | GitHub `ejcalderongt/DBA` (no en Azure) | T-SQL | Schema + SPs SQL Server productivos. WIP `VW_Stock_Res`. | — |
| `tomwms-replit-client-automate` | GitHub `ejcalderongt` | Mixto | **Repo de intercambio**: bundles, scripts y este brain (`wms-brain` branch). | `main` (bundles) + `wms-brain` (conocimiento) |
| Workspace Replit | Replit | TS/Node/Python | Productor: scripts + brain + builders. **Tiene acceso de SOLO LECTURA al WMS via Azure DevOps REST API**. Ver `agent-context/AZURE_ACCESS.md`. | sesión |

**Reglas duras de topología** (ver también `entregables_ajuste/AGENTS.md`):

- El repo de intercambio en GitHub **no contiene código WMS**. Solo bundles + scripts + brain.
- Azure DevOps **no recibe push automático** ni del productor ni del consumidor. Los patches viajan por bundle y el merge a `dev_2028_merge` lo hace el humano desde VS.
- El productor (Replit) tiene **acceso de SOLO LECTURA** al WMS vía Azure DevOps REST API (PAT `AZURE_DEVOPS_PAT`, validado 2026-04-26). **NO** clona el repo entero (376 MB) salvo casos de extracción completa puntual. Lee archivos on-demand vía API. **NUNCA** pushea a Azure. Comandos pegables en `agent-context/AZURE_ACCESS.md`.

---

## 3. Equipo y autoría de commits

| Iniciales | Persona | Formato de commit |
|---|---|---|
| **EJC** | Erik José Calderón (owner) | `#EJCRP <tipo>(<área>): <msg>` (en TOMWMS_BOF) |
| **GT** | Efren Gustavo Tuyuc | `#GT_DDMMAAAA:` |
| **AG** | Abigail Gaitán | `#AGDDMMAAAA` |
| **MA** | Marcela Álvarez | `#MA DDMMAAAA` o `#MADDMMAAAA` |
| **AT** | Anderly Teleguario | `#AT DDMMAAAA` |
| **MECR** | Melvin Cojtí | `#MECR DDMMAAAA` |
| **CF** | Carolina Fuentes | (formato por confirmar) |

Para identificar autor de cualquier convención particular (ej. reglas en `WebService.java`), buscar el prefijo en comentarios.

---

## 4. Reglas duras (no negociables)

Consolidadas de `replit.md` + `tools/agent-context/AGENTS.md`. Si alguna se contradice con instrucciones de tarea, **gana esta lista** y se pide aclaración antes de operar.

1. **No commits ni push automáticos al WMS.** Erik los revisa y los hace él. El productor solo genera bundles; el consumidor solo deja la rama efímera lista.
2. **No mezclar HH (Java) con backend (VB.NET)** en el mismo bundle / commit / PR.
3. **No tocar `Reference.vb`.** Son proxies SOAP autogenerados; la implementación real vive en `*.asmx.vb`.
4. **No reescribir desde cero.** Debuggear primero. Siempre. Si una propuesta requiere reescribir un módulo entero, parar y pedir confirmación explícita.
5. **UTF-8 con BOM en VB.** Mantener `ñ` y acentos. Nunca guardar como ANSI. Eliminar cualquier `.replace("ñ","n")` heredado (ej. `WebService.java` línea 352).
6. **Migración XML→JSON: oportunista.** Método legacy estable → NO se migra. Funcionalidad **nueva** → JSON por defecto, **Forma A** (ver `conventions.md`).
7. **NUNCA loguear, imprimir ni embeber en código** los secrets `WMS_KILLIOS_DB_PASSWORD`, `BRAIN_IMPORT_TOKEN`, `AZURE_DEVOPS_PAT`, `GITHUB_TOKEN`, `SESSION_SECRET`. Solo referencia por nombre.
8. **Conexión a KILLIOS prod = solo lectura** desde el agente. Cualquier write lo hace Erik con el procedimiento normal.
9. **El módulo `definition` del catálogo SQL es sensible.** No exponer a logs ni respuestas que vayan a clientes finales o a terceros.
10. **Hello sync antes de operar.** Sin handshake exitoso (`scripts/hello_sync.mjs` exit 0), no hay autorización para análisis ni propuestas ni aplicación. Ver `entregables_ajuste/AGENTS.md`.
11. **Cambios pequeños e incrementales.** Un bundle = una intención. No mezclar refactors con bug fixes con features.
12. **No romper compatibilidad.** Nombres de funciones, firmas, contratos JSON, orden de columnas, claves de configuración: se preservan salvo instrucción explícita y documentada.

---

## 5. Arquitectura del solution TOMWMS_BOF

Solution `TOMWMS_BOF.sln` agrupa los siguientes proyectos relevantes (no exhaustivo):

| Proyecto | Tipo | Rol |
|---|---|---|
| `TOMIMSV4` | WinForms .NET 4.8 | UI principal: módulos de transacciones (Ajustes, Recepciones, Despachos, Conteos), maestros, reportes, configuración. |
| `Entity` | Class library | DTOs / entidades de dominio (legacy, `cls<Nombre>`). |
| `DAL` | Class library | Data Access Layer legacy: clase principal `cls_DBSql` / `lDB`. Wrap de `SqlConnection`/`SqlCommand`/`SqlDataReader`. |
| `WSHHRN` | ASMX WebService | **WebService de los handhelds**. Endpoints SOAP que consume `TOMHH2025`. Punto de integración HH↔backend. |
| `WSSAPSYNC` | ASMX WebService | Sync con SAP. |
| `WMSWebAPI` | ASP.NET WebAPI | API moderna parcial (migración incompleta a .NET 8). |
| `EntityCore` | .NET Core class library | Versión moderna de Entity (parcial). |
| `DALCore` | .NET Core class library | Versión moderna de DAL (parcial). |
| `Interfaces.*` | Class libraries | Adaptadores: NAV, SAP, Cealsa, DMS, AWS. |

**Convenciones de carpetas** dentro de `TOMIMSV4`:

```
TOMIMSV4/TOMIMSV4/
├── Maestros/             <- forms de maestros (clientes, productos, ubicaciones)
├── Transacciones/        <- forms operativos
│   ├── Ajustes/          <- ajuste de stock (donde vive frmAjusteStock.vb)
│   ├── Recepciones/
│   ├── Despachos/
│   └── ...
├── Reportes/
├── Configuracion/
└── Comunes/              <- utilidades transversales
```

---

## 6. Protocolo HH ↔ WS (handheld a backend)

### Transports

- **SOAP clásico** (legacy, mayoría de métodos): el cliente Java HH llama vía `KSOAP2` (ksoap2-android) a los endpoints de `WSHHRN`. Los proxies del lado Java se generan a partir del WSDL.
- **JSON sobre SOAP envelope** (nuevo, **Forma A**): el cuerpo del SOAP envuelve un string JSON. El backend deserializa con `JavaScriptSerializer` (por compatibilidad con la base instalada). Ver `conventions.md` §JSON.

### Reglas de transport

- **HH → WS**: Java HH siempre arma SOAP. El payload puede ser parámetros tipados (legacy) o un único string JSON (Forma A).
- **WS → HH**: el backend devuelve XML SOAP. El body puede ser tipos primitivos o un string JSON con wrapper `{data, error}` (Forma A).
- **Encoding**: UTF-8. Mantener `ñ` y acentos. Sin BOM en payload (BOM solo en archivos VB).

### Anti-patterns conocidos

- `String.replace("ñ","n")` en `WebService.java` (línea 352) — **eliminar**, la HH debe procesar UTF-8 limpio.
- Mezclar parámetros legacy + JSON en el mismo método — elegir uno.
- Olvidar `try/catch` que devuelva el wrapper `{error}` en métodos JSON nuevos.

---

## 7. Modelo de datos crítico

BD `TOMWMS_KILLIOS_PRD` (y análogas por cliente). 542 objetos según el WikiHub. Familias clave que cualquier tarea de stock o transacciones tiene que conocer:

| Familia | Naming | Rol |
|---|---|---|
| **Stock** | `stock`, `stock_*` | Tablas de existencias actuales por ubicación, lote, fecha de vencimiento. |
| **Vistas de stock** | `VW_Stock_Res` | Vista resumen reservas/disponibilidad. **WIP en repo `DBA`**. Cambios pendientes. |
| **Transacciones** | `trans_*` | Movimientos: `trans_recep`, `trans_desp`, `trans_ajuste`, `trans_traslado`, etc. Header + detalle por tipo. |
| **Ajustes** | `trans_ajuste`, `trans_ajuste_det`, `ajuste_motivo` | Ajustes de stock manuales y por importación Excel. Motivos catalogados en `ajuste_motivo`. |
| **Maestros** | `productos`, `clientes`, `ubicaciones`, `lotes` | Datos relativamente estables. |
| **Configuración** | `config_*`, `parametros` | Por cliente / por almacén. |

**Antes de tocar cualquier query, SP o vista**, consultar el **TOMWMS Brain API** (§9) para `impact` y `dependencies`. Cambiar una columna sin conocer blast radius es la causa #1 de regresiones.

---

## 8. Conexión a SQL Server (productiva, solo lectura)

| Dato | Valor |
|---|---|
| Servidor | `52.41.114.122,1437` (EC2 AWS, Windows Server 2022) |
| Puerto | `1437` (no el 1433 default) |
| BD activa | `TOMWMS_KILLIOS_PRD` |
| Versión | SQL Server 2022 CU18 Standard (16.0.4185.3) |
| Usuario | `sa` |
| Password | secret `WMS_KILLIOS_DB_PASSWORD` |
| Driver Node | `tedious` |
| Driver Python | `pyodbc` (ODBC Driver 17+ for SQL Server) |

Convención para futuros clientes: `WMS_<CLIENTE>_<ENV>_DB_PASSWORD`.

### Snippet Node.js (referencia rápida)

```javascript
// Ejecutar con: node script.js (NO desde sandbox JS porque no ve env vars)
const { Connection, Request } = require("tedious");
const config = {
  server: "52.41.114.122",
  options: {
    port: 1437,
    database: "TOMWMS_KILLIOS_PRD",
    encrypt: false,
    trustServerCertificate: true,
    requestTimeout: 60000,
    connectTimeout: 15000,
  },
  authentication: {
    type: "default",
    options: { userName: "sa", password: process.env.WMS_KILLIOS_DB_PASSWORD },
  },
};
```

### Snippet Python (vía wmsa)

```cmd
wmsa db query "SELECT TOP 10 idstock, codigo, cantidad FROM stock"
wmsa db tables --like %trans_ajuste%
wmsa db sp-text Get_Stock_Actual
```

`wmsa db` valida toda query — bloquea cualquier statement que no sea `SELECT|WITH|EXEC|EXECUTE|SET`. Rechaza `INSERT|UPDATE|DELETE|MERGE|DROP|ALTER|CREATE|TRUNCATE`.

---

## 9. TOMWMS Brain API (servicio Replit privado)

API que indexa los 3 repos de código + el catálogo SQL productivo. Permite buscar símbolos, ver impact (callers) y dependencies (callees) cross-layer (Java HH → VB WebMethod → SP/tabla SQL).

```
BRAIN_BASE_URL=https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain
```

### Endpoints GET (sin token)

| Endpoint | Para qué |
|---|---|
| `/health` | Sanity check + totales (símbolos, refs). |
| `/search?q=&kind=&repo=&limit=` | Buscar símbolo por nombre. `kind`: `vb-class`, `vb-method`, `java-class`, `java-method`, `sql-table`, `sql-view`, `sql-sp`, `sql-fn`, `sql-trigger`. |
| `/impact?symbol=&depth=&limit=` | Quién depende del símbolo (blast radius). |
| `/dependencies?symbol=&depth=&limit=` | De qué depende el símbolo (upstream). |
| `/writers?symbol=&op=&kind=` | Quién escribe esta tabla/SP. `op`: `insert,update,delete,merge,exec`. |

### Endpoints POST (requieren `X-Brain-Token: $BRAIN_IMPORT_TOKEN`)

| Endpoint | Para qué |
|---|---|
| `/import/sql-catalog` | Subir el catálogo extraído por `tools/sql-catalog/extract_sql_catalog.py`. |
| `/index/vb` | Re-indexar VB tras cambios. |
| `/index/java` | Re-indexar Java HH tras cambios. |
| `/repos/sync` | Pull de los repos en el server Brain. |

### Cuándo usar el Brain (vs leer código)

| Situación | Acción |
|---|---|
| Necesito saber **quién llama** a un método/SP/tabla | `/impact` |
| Necesito saber **de qué depende** un método | `/dependencies` |
| Quiero ver el cuerpo de un SP | `wmsa db sp-text <nombre>` o `/search` + ir al repo |
| Quiero el WSDL/firma de un endpoint asmx | leer `*.asmx.vb` directamente (pedir al usuario) |
| Necesito el modelo conceptual de una entidad | WikiHub Portal §10, no el Brain |

---

## 10. WikiHub Portal (documentación humana)

Wiki técnica del WMS, separada del Brain. Pensada para humanos.

```
URL: https://tomwms-wikidev.replit.app
```

| Sección | Path | API REST |
|---|---|---|
| Jira (1.981 issues) | `/jira` | — |
| Release Notes (26 releases v5.0–v5.8.0) | `/changelog`, `/releases` | `/api/release-notes`, `/api/release-notes/:id`, `/api/release-notes/timeline` |
| Wiki Técnica (50 procesos, 542 objetos BD, 1.152 módulos) | `/wiki-tech` | `/api/modules`, `/api/clients` |
| Cambios en BD (2.385) | `/db-changes` | _(no expuesta)_ |
| Cambios en código | `/code-changes` | _(no expuesta)_ |
| Tech Docs profundos | `/tech-docs` | `/api/tech-docs` (lista; `:id` actualmente devuelve `{error}`) |
| Stats | `/landing` | `/api/stats/dashboard`, `/api/stats/by-month` |

### Cuándo consultar WikiHub

- Necesito el **proceso de negocio** detrás de una funcionalidad → `/wiki-tech` + `/api/modules`.
- Necesito saber **qué hizo cada release** → `/api/release-notes`.
- Necesito ticket Jira de referencia → `/jira` (UI manual).
- Necesito modelo de datos conceptual → `/wiki-tech` (vista humana, no estructural).

**Regla**: si la info no está en `replit.md`, en este skill, ni en `wms-brain` rama → consultar **primero** WikiHub vía `/api/*` antes de buscar en código.

---

## 11. wmsa CLI (atajo del agente local)

`wmsa` es una CLI Python que vive en `tools/wms-agent/`. Pensada para correr en la PC de Erik junto a `openclaw`. Centraliza:

```cmd
wmsa config init           # configura Brain URL + token + DB
wmsa brain search stock --kind sql-table
wmsa brain impact VW_Stock_Res --depth 2
wmsa brain writers stock --op update,delete
wmsa db query "SELECT TOP 10 ..."
wmsa db sp-text Get_Stock_Actual
wmsa case new              # intake estructurado para escalar a Replit
wmsa case escalate <CASE_ID>  # imprime markdown listo para pegar acá
```

Credenciales en **keyring del SO** (Credential Manager en Windows). Nunca en archivo plano.

Para detalles de comandos: `tools/wms-agent/README.md` (también versionado en `wms-brain` rama).

---

## 12. Workflow obligatorio según el caso

### Caso 1 — Cambio en SP, vista, tabla o columna SQL

```
1. Localizar:           wmsa brain search <obj> --kind sql-table
2. Blast radius:        wmsa brain impact <obj> --depth 2
3. Writers:             wmsa brain writers <obj>
4. Si toca VB:          revisar callers VB con /impact ?repo=TOMWMS_BOF
5. Si toca HH:          revisar callers Java con /impact ?repo=TOMHH2025
6. Construir bundle:    seguir SKILL bundle-builder, marcador #FIX_vNN_<NOMBRE>
7. Validar:             apply_bundle --dry-run en clon de prueba
8. Empujar:             productor pushea bundle a exchange repo (rama main)
9. Notificar brain:     scripts/brain_bridge.mjs notify --event-type code_change ...
```

### Caso 2 — Cambio en WebMethod del WSHHRN

```
1. Identificar el método en *.asmx.vb (NO Reference.vb).
2. brain dependencies <Metodo> -> qué consume (DAL, Entity, SP).
3. brain impact <Metodo> --repo=TOMHH2025 -> qué Activity Java lo llama.
4. Si es JSON nuevo: aplicar Forma A (ver conventions.md).
5. Si es legacy SOAP: NO migrar a JSON salvo orden explícita.
6. Construir bundle backend-only (no mezclar con HH).
```

### Caso 3 — Cambio en HH (Java)

```
1. Localizar Activity en TOMHH2025 (55 Activities).
2. brain dependencies <Activity> -> qué WebMethods llama.
3. Verificar contrato HH↔WS NO se rompe (firma del SOAP).
4. Bundle separado del backend (regla #2).
5. UTF-8: respetar ñ.
```

### Caso 4 — Bug reportado por usuario

```
1. wmsa case new (lado openclaw): intake estructurado.
2. wmsa case escalate <ID>: pegar markdown en chat Replit.
3. Productor (acá) lee el case, decide si necesita más data antes de proponer.
4. Si necesita data: pedir queries específicas vía wmsa db query (Erik las corre).
5. Construir bundle vNN.
6. Después de apply OK: brain_bridge notify para que brain se autoactualice si aplica.
```

---

## 13. Documentos relacionados

| Doc | Path | Cuándo leer |
|---|---|---|
| Convenciones código | `.local/skills/wms-tomwms/conventions.md` | Antes de escribir VB / Java / SQL nuevo. |
| Contrato bundles | `entregables_ajuste/AGENTS.md` (rama `main`) | Antes de armar / aplicar bundle. |
| Bridge brain | `brain/BRIDGE.md` (rama `wms-brain`) | Después de un apply, o ante orden explícita de actualizar brain. |
| Skill productor de bundles | `.local/skills/bundle-builder/SKILL.md` (si existe) | Antes de armar un bundle. |
| README wmsa | `tools/wms-agent/README.md` | Antes de usar wmsa o agregarle un comando. |

---

## 14. Versionado

Cualquier cambio a este skill debe:

1. Aplicarse en **ambas copias**: `.local/skills/wms-tomwms/SKILL.md` (workspace) y `brain/skills/wms-tomwms/SKILL.md` (rama `wms-brain`).
2. Idealmente entrar al brain vía `scripts/brain_bridge.mjs` con un evento `type=skill_update` que documente la razón.
3. Quedar reflejado en el commit log de `wms-brain` con mensaje `wms-brain: skill <sección> — <razón>`.
