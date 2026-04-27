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
| **Cliente activo en este Replit** | Killios (BD `TOMWMS_KILLIOS_PRD`) |
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

**BD Killios (AWS) es VOLÁTIL**: el host puede dejar de existir, migrar de proveedor o restaurarse en otro entorno para análisis puntual. **No asumir que el connection string actual es permanente**. Antes de cualquier sesión que dependa de la BD, validar conectividad y, si cambió, actualizar este archivo + `brain/agent-context/AZURE_ACCESS.md`. La BD es referencia para entender el modelo y validar estados, NO es un servicio de producción gestionado por nosotros.

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

| Dato | Valor |
|---|---|
| Servidor | `52.41.114.122,1437` (EC2 AWS, Windows Server 2022) |
| BD principal | `TOMWMS_KILLIOS_PRD` (345 tablas, 39 SPs) |
| Otras BDs WMS accesibles | `IMS4MB_BYB_PRD` (BYB prod), `IMS4MB_CEALSA_QAS` (CEALSA QA) |
| Versión | SQL Server 2022 CU18 Standard (16.0.4185.3) |
| Usuario | `sa` |
| Contraseña | secreto `WMS_KILLIOS_DB_PASSWORD` (NUNCA en código ni chat) |
| Driver Node | `tedious` (workspace) o `mssql` (instalable) |

**Naming real de tablas** (validado 2026-04-27): SIN prefijo `t_*`. Son `trans_oc_det_lote`, `log_importacion_excel`, `cliente_lotes`, etc. directos.

**Modelo de lotes**: NO existe maestro independiente. Los lotes viven en 6 tablas dedicadas (`trans_re_det_lote_num` 180k filas, `trans_oc_det_lote` 1k filas, `trans_despacho_det_lote_num`, `cliente_lotes`, 2 interfaces Navision) + en muchas tablas con campo `lote` embebido (`stock`, `stock_hist`, `producto_pallet`, etc.). Detalle en `brain/skills/wms-tomwms/SKILL.md` §7.1.

**Secrets recomendados** para que el agente conecte automáticamente entre sesiones (sin pasar datos por chat):
- `WMS_KILLIOS_DB_HOST`, `WMS_KILLIOS_DB_PORT`, `WMS_KILLIOS_DB_USER`, `WMS_KILLIOS_DB_NAME_DEFAULT`.

Convención para futuras BDs: `WMS_<CLIENTE>_<ENV>_DB_PASSWORD`.

Detalle completo: `brain/agent-context/AZURE_ACCESS.md` §9.

**Snippet de conexión** (referencia rápida):

```javascript
// Ejecutar con: node scriptName.js (NO desde el sandbox JS porque no ve env vars)
const { Connection, Request } = require('tedious');
const config = {
  server: '52.41.114.122',
  options: { port: 1437, database: 'TOMWMS_KILLIOS_PRD',
    encrypt: false, trustServerCertificate: true,
    requestTimeout: 60000, connectTimeout: 15000 },
  authentication: { type: 'default',
    options: { userName: 'sa', password: process.env.WMS_KILLIOS_DB_PASSWORD } },
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
