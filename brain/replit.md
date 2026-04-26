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

## 2. Repositorios (Azure DevOps `ejcalderon0892`)

| Proyecto | Repo | Rama de trabajo | Path local | Notas |
|---|---|---|---|---|
| `TOMWMS_BOF` | `TOMWMS_BOF` | `dev_2028_merge` | `/tmp/TOMWMS_v2/` | Backend principal: WinForms TOMIMSV4, WSHHRN (.asmx), WSSAPSYNC, WMSWebAPI, Entity, DAL, EntityCore, DALCore, interfaces (NAV, SAP, Cealsa, DMS, AWS) |
| `TOMHH2025` | `TOMHH2025` | `dev_2028_merge` | `/tmp/TOMHH2025/` | App Android Handheld (405 .java, 55 activities) |
| `ejcalderongt/DBA` (GitHub) | `DBA` | — | — | Scripts SQL versionados (incluye trabajo en curso `VW_Stock_Res`) |

**Auth Azure DevOps en sandbox:** `AUTH=$(printf ":%s" "$AZURE_DEVOPS_PAT" | base64 -w0)` (secreto disponible). NO commit/push automático sin permiso explícito del usuario.

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

## 5. Acceso a SQL Server

| Dato | Valor |
|---|---|
| Servidor | `52.41.114.122,1437` (EC2 AWS, Windows Server 2022) |
| BD activa | `TOMWMS_KILLIOS_PRD` |
| Versión | SQL Server 2022 CU18 Standard (16.0.4185.3) |
| Usuario | `sa` |
| Contraseña | secreto `WMS_KILLIOS_DB_PASSWORD` (NUNCA en código ni chat) |
| Driver Node | `tedious` (instalado en raíz del workspace) |

Convención para futuras BDs: `WMS_<CLIENTE>_<ENV>_DB_PASSWORD`.

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
