---
id: db-brain-table-i-nav-ent
type: db-table
title: dbo.i_nav_ent
schema: dbo
name: i_nav_ent
kind: table
rows: 0
modify_date: 2017-09-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_ent`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-09-22 |
| Columnas | 3 |
| Índices | 1 |
| FKs | out:0 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idnavent` | `int` |  |  |
| 2 | `nombre` | `varchar(256)` | ✓ |  |
| 3 | `endpoint` | `varchar(512)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_ent` | CLUSTERED · **PK** | idnavent |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `i_nav_config_det` (`FK_i_nav_config_det_i_nav_ent`)
- `i_nav_config_ent` (`FK_i_nav_config_ent_i_nav_ent`)

## Quién la referencia

**1** objetos:

- `VW_navdetalleconfiguracion` (view)

