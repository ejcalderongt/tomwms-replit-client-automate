---
id: db-brain-table-i-nav-ent-filtros
type: db-table
title: dbo.i_nav_ent_filtros
schema: dbo
name: i_nav_ent_filtros
kind: table
rows: 0
modify_date: 2021-09-23
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ent_filtros`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-09-23 |
| Columnas | 4 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idnaventfiltro` | `int` |  |  |
| 2 | `idnavent` | `int` | ✓ |  |
| 3 | `valor` | `nvarchar(50)` | ✓ |  |
| 4 | `tipo_filtro` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_env_filtros` | CLUSTERED · **PK** | idnaventfiltro |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

