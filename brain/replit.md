# TOM WMS — Replit Agent Workspace

Este Replit es el **entorno de trabajo del agente** para el proyecto **TOM WMS** (Warehouse Management System) del usuario Erik Calderón (PrograX24). El agente actúa como ingeniero senior permanente con conocimiento del WMS entre sesiones.

> **Importante**: Este `replit.md` es un **índice + atajos**, no un manual completo. La documentación profunda y exhaustiva del WMS vive en el WikiHub Portal (ver §Wiki Portal). Para detalles operativos y playbooks, leer el skill `.local/skills/wms-tomwms/`.

---

## 1. Contexto del producto

| Campo | Valor |
|---|---|
| **Producto** | TOM WMS — Warehouse Management System multi-cliente |
| **Empresa** | PrograX24 |
| **Owner técnico** | Erik José Calderón (EJC) |
| **Cliente activo en este Replit** | Killios (codename **K7**, BD nombre vive en secreto `WMS_KILLIOS_DB_NAME_DEFAULT`) |
| **Otros clientes con releases productivas** | Becofarma, La Cumbre, Cealsa, Mampa (MHS) |
| **Stack backend** | VB.NET .NET Framework 4.8, SQL Server 2022 (sobre EC2 AWS) |
| **Stack HH (Handheld Android)** | Java, Android API minSdk/targetSdk=24, compileSdk=34, applicationId `com.dts.tom` v8.2.3 |
| **Stack web moderno (parcial)** | C# .NET Core / .NET 8.0 (EntityCore + DALCore + WMSWebAPI, migración incompleta) |

## 2. Repositorios

### 2.1 Azure DevOps (org `ejcalderon0892`)

| Proyecto / Repo | URL clone (HTTPS) | Default branch | Branch trabajo | Tamaño | Path local Erik |
|---|---|---|---|---|---|
| `TOMWMS_BOF` / `TOMWMS_BOF` | `https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF` | `master` | `dev_2028_merge` | 376 MB · 9609 archivos · 1708 dirs | `C:\Users\yejc2\source\repos\TOMWMS` |
| `TOMHH2025` / `TOMHH2025` | `https://dev.azure.com/ejcalderon0892/TOMHH2025/_git/TOMHH2025` | `master` | `dev_2028_merge` | 14 MB · 694 archivos · 405 `.java` · **64 activities** (manifest) | `C:\Users\yejc2\StudioProjects\TOMHH2025` ✓ confirmado 2026-04-26 |

**openclaw = misma máquina que Erik (yejc2), mismo working set**. Confirmado 2026-04-26. Paths locales validados:
- BOF: `C:\Users\yejc2\source\repos\TOMWMS`
- HH: `C:\Users\yejc2\StudioProjects\TOMHH2025` (Android Studio, no `repos/` como se asumió inicialmente)

**BD Killios es VOLÁTIL**: el host puede dejar de existir, migrar de proveedor o restaurarse en otro entorno para análisis puntual. **No asumir que el connection string actual es permanente**. Antes de cualquier sesión que dependa de la BD, validar conectividad y, si cambió, actualizar el archivo restringido `brain/agent-context/AZURE_ACCESS.md` (NO este archivo: es shared/reviewable). La BD es referencia para entender el modelo y validar estados, NO es un servicio de producción gestionado por nosotros.

**Composición de TOMWMS_BOF en `dev_2028_merge` (snapshot 2026-04-26)**:
- Conteo por extensión: `.vb` = 3218, `.cs` = 1083, `.resx` = 657, `.svg` = 1624, `.xml` = 199, `.svc` = 94, `.config` = 119
- Subraíces principales del solution: `TOMIMSV4/`, `WSHHRN/`, `WSSAPSYNC/`, `WMSWebAPI/`, `WMS.AppGlobalCore/`, `WMS.DALCore/`, `WMS.EntityCore/`, `WMSPortal/`, `DMS/`, `MES/`, `MI3/`, `IAService/`, `SAPSYNCCUMBRE/`, `SAPSYNCMAMPA/`, `SAPSYNC_Killios/`
- Solution: `TOMWMS.sln`
- Topología `TOMIMSV4/`: `DAL`, `DataSets`, `Entity`, `Resources`, `Service References`, `TOMIMSV4` (subraíz anidada legacy), `TOMIMS_WCF`, `Transacciones`
- ⚠ Hay duplicación entre `/TOMIMSV4/...` y `/TOMIMSV4/TOMIMSV4/...` (legacy nested). Verificar siempre cuál archivo está activo antes de tocar.

### 2.2 GitHub (org `ejcalderongt`)

| Repo | URL | Rol | Notas |
|---|---|---|---|
| `tomwms-replit-client-automate` | `https://github.com/ejcalderongt/tomwms-replit-client-automate` | Repo intercambio | Ramas: `main` (bundles + scripts) + `wms-brain` (este brain) |
| `DBA` | `https://github.com/ejcalderongt/DBA` | Scripts SQL | Schema y SPs versionados. WIP `VW_Stock_Res`. **No vive en Azure DevOps**. |

### 2.3 Acceso desde Replit (validado 2026-04-26)

- **Sí hay acceso de LECTURA** a TOMWMS_BOF y TOMHH2025 desde el container Replit usando `AZURE_DEVOPS_PAT`.
- **Método preferido**: API REST de Azure DevOps con header `Authorization: Basic $(printf ":%s" "$AZURE_DEVOPS_PAT" | base64 -w0)`. Permite leer árbol y archivos individuales sin clonar 376 MB.
- **Método alternativo**: `git clone` con el mismo header vía `-c http.extraHeader=...`. Solo si se necesita el repo entero local (ej. extractor inicial de code-index).
- **NO se hace push a Azure DevOps desde el agente**. Nunca. Los cambios viajan por bundle vía intercambio GitHub.
- Detalle operativo y comandos pegables en `brain/agent-context/AZURE_ACCESS.md`.

## 3. Equipo y autoría de commits

| Iniciales | Persona | Formato de commit |
|---|---|---|
| **EJC** | Erik José Calderón (owner) | `#EJCRP <tipo>(<área>): <msg>` (TOMWMS_BOF) |
| **GT** | Efren Gustavo Tuyuc | `#GT_DDMMAAAA:` |
| **AG** | Abigail Gaitán | `#AGDDMMAAAA` |
| **MA** | Marcela Álvarez | `#MA DDMMAAAA` o `#MADDMMAAAA` |
| **AT** | Anderly Teleguario | `#AT DDMMAAAA` |
| **MECR** | Melvin Cojtí | `#MECR DDMMAAAA` |
| **CF** | Carolina Fuentes (vista en WikiHub) | (formato por confirmar) |

Para identificar autor de cualquier convención particular (ej. reglas en `WebService.java`), buscar el prefijo en comentarios.

## 4. Reglas establecidas (vinculantes)

1. **Migración XML→JSON: oportunista.** Método legacy estable → NO se migra. Funcionalidad **nueva** → JSON por defecto.
2. **Patrón JSON estándar: Forma A** (wrapper `{data, error}` + `JavaScriptSerializer` + status 200/500). Ver detalle en `.local/skills/wms-tomwms/conventions.md`.
3. **Mantener `ñ`**: eliminar `.replace("ñ","n")` en `WebService.java` línea 352. La HH debe procesar `ñ` correctamente como UTF-8.
4. **NO commit/push automático** sin permiso explícito del usuario.
5. **NO mezclar** cambios de HH (Android) y backend (VB.NET) en el mismo commit.
6. **Acceso al WMS desde el agente Replit = SOLO LECTURA** vía Azure DevOps REST API. Cero `git push` a Azure. Detalle en `brain/agent-context/AZURE_ACCESS.md`.

## 5. Acceso a SQL Server

**Estado validación**: ✅ 2026-04-27 (conexión confirmada desde Replit con queries read-only).

> **Nota de privacidad**: este archivo es shared/reviewable. **Hostnames, IPs, nombres de BD y usuarios reales NO van aquí**. Viven en `brain/agent-context/AZURE_ACCESS.md` §9 (acceso restringido) y se inyectan al agente solo via secretos.

| Dato | Donde vive |
|---|---|
| Hostname / IP / port | secretos `WMS_<CODENAME>_<ENV>_DB_HOST`, `..._PORT` |
| BD por cliente | secreto `WMS_<CODENAME>_<ENV>_DB_NAME_DEFAULT` |
| Versión motor | SQL Server 2022 CU18 Standard (16.0.x) |
| Usuario lectura | secreto `WMS_<CODENAME>_<ENV>_DB_USER` (NO usar `sa` ni cuentas privilegiadas; pedir `read_brain` o equivalente) |
| Contraseña | secreto `WMS_<CODENAME>_<ENV>_DB_PASSWORD` (NUNCA en código ni chat) |
| Driver Node | `tedious` (workspace) o `mssql` (instalable) |

Codenames activos: K7 (PRD), BB (PRD), C9 (QAS). Pendiente acceso al grupo Aurora (ID/MH/MC/MP/IN) y resto (MS/BF/MM/LC) — ver Q-013.

**Naming real de tablas** (validado 2026-04-27): SIN prefijo `t_*`. Son `trans_oc_det_lote`, `log_importacion_excel`, `cliente_lotes`, etc. directos.

**Modelo de lotes**: NO existe maestro independiente. Los lotes viven en 6 tablas dedicadas (`trans_re_det_lote_num` 180k filas, `trans_oc_det_lote` 1k filas, `trans_despacho_det_lote_num`, `cliente_lotes`, 2 interfaces Navision) + en muchas tablas con campo `lote` embebido (`stock`, `stock_hist`, `producto_pallet`, etc.). Detalle en `brain/skills/wms-tomwms/SKILL.md` §7.1.

Convención de secretos para nuevas BDs: `WMS_<CODENAME>_<ENV>_DB_<PASSWORD|HOST|PORT|USER|NAME_DEFAULT>`.

Detalle completo (con valores reales) en archivo restringido: `brain/agent-context/AZURE_ACCESS.md` §9.

**Snippet de conexión** (referencia genérica, sin datos sensibles):

```javascript
// Ejecutar con: node scriptName.js (NO desde el sandbox JS porque no ve env vars)
const { Connection, Request } = require('tedious');
const code = 'KILLIOS'; // codename de cliente activo (mayusculas, ver tabla arriba)
const config = {
  server: process.env[`WMS_${code}_DB_HOST`],
  options: {
    port: Number(process.env[`WMS_${code}_DB_PORT`] ?? 1433),
    database: process.env[`WMS_${code}_DB_NAME_DEFAULT`],
    encrypt: false, trustServerCertificate: true,
    requestTimeout: 60000, connectTimeout: 15000,
  },
  authentication: {
    type: 'default',
    options: {
      userName: process.env[`WMS_${code}_DB_USER`],
      password: process.env[`WMS_${code}_DB_PASSWORD`],
    },
  },
};
```

## 6. WikiHub Portal (fuente de verdad humana)

URL: **https://tomwms-wikidev.replit.app**

| Sección | URL | API |
|---|---|---|
| Jira (1,981 issues) | `/jira` | — |
| Release Notes (26 releases v5.0–v5.8.0) | `/changelog`, `/releases` | `GET /api/release-notes`, `GET /api/release-notes/:id`, `GET /api/release-notes/timeline` |
| Wiki Técnica (50 procesos, 542 objetos BD, 1,152 módulos) | `/wiki-tech` | `GET /api/modules`, `GET /api/clients` |
| Cambios en BD (2,385 cambios) | `/db-changes` | _(no expuesta)_ |
| Cambios en Código | `/code-changes` | _(no expuesta)_ |
| Docs Técnicos profundos | `/tech-docs` | `GET /api/tech-docs` (lista; el `:id` actualmente devuelve `{error}`) |
| Stats | `/landing` | `GET /api/stats/dashboard`, `GET /api/stats/by-month` |

**Regla**: cuando necesite info detallada del WMS y no esté en este replit.md ni en el skill, **primero consultar el wiki vía `/api/*`** antes de buscar en código.

## 7-bis. wms-brain-client (questions ↔ answers ↔ learnings)

El brain WMS usa un protocolo **question/answer card** versionado
(`protocolVersion: 1`) para destilar conocimiento operativo del
WMS productivo de manera reproducible y auditable.

### Layout en `brain/wms-brain-client/`

- `templates/` — plantillas (`answer-card.template.md`,
  `learning-card.template.md`).
- `examples/` — questions iniciales Q-001..Q-008 (referencia).
- `questions/` — questions activas pendientes (Q-009..Q-013 ahora).
- `answers/` — answer cards A-NNN respondiendo a Q-NNN.
- `answers/_evidence/A-NNN/` — CSV + JSON de cada query ejecutada
  (sanitizado: codenames de cliente, sin servidores ni claves).
- `brain/learnings/` — destilado long-lived (L-NNN) con
  implicancias y acciones.

### Tarea 1 ejecutada (2026-04-27)

Respondidas 8 questions (Q-001..Q-008 → A-001..A-008) ejecutando
queries read-only contra K7-PRD y BB-PRD. Resumen de veredictos:

| Card | Verdict | Confidence | Hallazgo clave |
|---|---|---|---|
| A-001 | partial | medium | NavSync BB SALIDA: 99.5% en 0 seg, writer consistente con externo (no se descartaron triggers/cross-server), 0 actividad ultimos 30d |
| A-002 | confirmed | high | K7 outbox 100% enteros — SAPSYNCKILLIOS sin redondeo |
| A-003 | partial | medium | NavSync BB hoy procesa solo SALIDA tipo 3; INGRESOs sí se procesaron 2022-05→2023-09 (107 filas) y luego se detuvieron |
| A-004 | confirmed | high | `log_error_wms`: 15 cols, sin severidad, mezcla AVISOs + trazas + errores |
| A-005 | partial | medium | 27 tablas config + 13 flags `*_sap` confirman multi-tenancy |
| A-006 | inconclusive | low | DBs Aurora no accesibles desde el endpoint autorizado del agente |
| A-007 | confirmed | high | Outbox K7 100% por linea de detalle (1:1 con `IdRecepcionDet`/`IdDespachoDet`) |
| A-008 | confirmed | high | Outbox NO maneja devoluciones hoy (esquema preparado, 0 uso real) |

### Learnings consolidadas (`brain/learnings/`)

- **L-009** SAPSYNCKILLIOS solo procesa cantidades enteras
- **L-010** NavSync de BB dejo de procesar INGRESOs en sep-2023 (110k pendientes acumulados)
- **L-011** `log_error_wms` es bitacora mixta, no log de errores puros
- **L-012** NavSync de BB hoy procesa solo SALIDAs tipo_doc=3 (writer consistente con externo, no exhaustivamente probado)
- **L-013** Outbox WMS es por linea de detalle (1 outbox = 1 RecepcionDet/DespachoDet)

### Questions abiertas tarea 2 (`brain/wms-brain-client/questions/`)

- **Q-009** Identificar binario y host del NavSync de BB (esta corriendo?)
- **Q-010** BB tiene 145k SALIDAs `IdTipoDocumento=1` y 30k tipo=4 sin procesar — son devoluciones?
- **Q-011** Cadencia y caller real del SAPSYNCKILLIOS (writer de `enviado=1` en K7)
- **Q-012** Convencion de severidad / prefijos en `log_error_wms` (PEND-11 follow-up)
- **Q-013** Habilitar acceso read-only a DBs Aurora y resto de clientes

### Convenciones del protocolo

- Naming: `Q-NNN-<slug>.md`, `A-NNN-<slug>.md`, `L-NNN-<slug>.md`.
- Frontmatter YAML con `protocolVersion: 1`.
- Veredictos validos: `confirmed | partial | inconclusive | rejected`.
- Confianza: `high | medium | low`.
- Codenames de cliente: K7=Killios, BB=BYB, C9=CEALSA, ID=Idealsa,
  MH=Merhonsa, MC=Mercosal, MP=Mercopan, IN=Inelac, MS=MHS,
  BF=Becofarma, MM=Mampa, LC=LaCumbre.
- Operator slug `agent-replit` cuando el card es generado por el
  agente (sin intervencion humana directa).
- Las queries en suggestedQueries deben ser **read-only**; el
  runner local del agente las valida con un check sintactico
  (rechaza INSERT/UPDATE/DELETE/MERGE/EXEC etc., excluyendo
  comentarios y string literals).

## 7. Skill local

Para cualquier tarea WMS (bug fix, nueva feature, análisis, etc.), **leer primero** `.local/skills/wms-tomwms/SKILL.md`. Contiene playbooks específicos:

- Arquitectura del solution TOMWMS (Entity/DAL/WSHHRN/WebAPI)
- Protocolo HH ↔ WS (SOAP + JSON, transports, parsing)
- Convenciones de migración XML→JSON (Forma A)
- Modelo de datos crítico (familias `trans_*`, `stock_*`, `VW_Stock_Res`, etc.)
- Cómo conectarse a la BD productiva
- Cómo consultar el WikiHub Portal vía API

---

# Workspace técnico (este Replit)

## Overview

pnpm workspace monorepo TypeScript. Hospeda artefactos de soporte al WMS (no es el WMS en sí — el WMS vive en Azure DevOps).

## Stack

- **Monorepo tool**: pnpm workspaces
- **Node.js version**: 24
- **Package manager**: pnpm
- **TypeScript version**: 5.9
- **API framework**: Express 5
- **Database**: PostgreSQL + Drizzle ORM (local Replit, NO el SQL Server productivo)
- **Validation**: Zod (`zod/v4`), `drizzle-zod`
- **API codegen**: Orval (from OpenAPI spec)
- **Build**: esbuild (CJS bundle)
- **SQL Server driver** (para conectar al WMS): `tedious`

## Artefactos registrados

- `artifacts/api-server` (id `3B4_FFSkEVBkAeYMFRJ2e`) — API server Express
- `artifacts/mockup-sandbox` (id `XegfDyZt7HqfW2Bb8Ghoy`) — Canvas para previews

## Key Commands

- `pnpm run typecheck` — full typecheck across all packages
- `pnpm run build` — typecheck + build all packages
- `pnpm --filter @workspace/api-spec run codegen` — regenerate API hooks and Zod schemas from OpenAPI spec
- `pnpm --filter @workspace/db run push` — push DB schema changes (dev only)
- `pnpm --filter @workspace/api-server run dev` — run API server locally

Ver el skill `pnpm-workspace` para estructura, TypeScript setup y detalles de paquetes.


---

## Análisis de impacto cruzado HH ↔ BOF

El brain mantiene **dos skills paralelos** porque el WMS es un sistema de dos cabezas:

- `brain/skills/wms-tomwms/` → backend (TOMWMS_BOF, VB.NET, `.asmx`).
- `brain/skills/wms-tomhh2025/` → frontend Android (TOMHH2025, Java).

**Cualquier cambio que toque el contrato HH↔BOF debe pasar por análisis cruzado** antes de aplicarse. La sección 7 del SKILL de HH tiene la matriz de riesgos completa. Resumen mínimo:

- **Tocás un `.asmx` del BOF** → ¿qué `com.dts.tom.*` lo llama? ¿el cambio de shape rompe Gson?
- **Tocás el JSON que la HH manda** → ¿el método `.asmx` lo deserializa con tolerancia o estricto?
- **Cambiás reglas de negocio en SP/BOF** → ¿la HH muestra el nuevo error de manera entendible?

Si cualquiera de estas tiene respuesta negativa o desconocida, el cambio no es seguro y se documenta en `brain/_inbox/_proposals/` antes de aplicarlo.
