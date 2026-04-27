---
id: db-brain-table-i-nav-config-ent
type: db-table
title: dbo.i_nav_config_ent
schema: dbo
name: i_nav_config_ent
kind: table
rows: 0
modify_date: 2017-09-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_config_ent`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-09-19 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idnavconfigent` | `int` |  |  |
| 2 | `idnavent` | `int` | ✓ |  |
| 4 | `endpoint` | `nvarchar(256)` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `user_mod` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_config_ent` | CLUSTERED · **PK** | idnavconfigent |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_i_nav_config_ent_i_nav_ent` → `i_nav_ent`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

