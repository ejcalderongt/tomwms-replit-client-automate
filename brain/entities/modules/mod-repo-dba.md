---
id: mod-repo-dba
type: module
title: Repo DBA (T-SQL versionado)
status: estable
sources:
  - skill: wms-tomwms §2
  - github: ejcalderongt/DBA
  - validated_at: 2026-04-27
---

# DBA — T-SQL versionado

| Atributo | Valor |
|---|---|
| Hosting | GitHub `ejcalderongt/DBA` (no en Azure) |
| Lenguaje | T-SQL |
| Rol | Schema + SPs SQL Server productivos |
| WIP conocido | `VW_Stock_Res` (vista resumen reservas/disponibilidad) |

## Estado actual

Contiene los scripts SQL versionados que generan/modifican el schema de las BDs WMS productivas (Killios, Becofarma, BYB, Cealsa, etc.).

> **Drift conocido**: el repo NO refleja al 100% el estado real de las BDs productivas. El catálogo extraído desde Killios reporta **345 tablas + 220 vistas + 39 SPs + 17 funciones** (validado 2026-04-27). Verificar contra el repo antes de asumir que un objeto está versionado.

## Cross-refs
- `db-brain://README` — catálogo SQL completo extraído de la BD productiva (branch `wms-db-brain`)
- `modules/mod-conexion-sqlserver`
- `modules/mod-brain-api` (M3) — endpoint `/api/brain/import/sql-catalog` consume este repo cruzado con la BD live
