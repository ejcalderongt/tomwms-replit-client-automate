---
id: db-brain-table-i-nav-config-det
type: db-table
title: dbo.i_nav_config_det
schema: dbo
name: i_nav_config_det
kind: table
rows: 0
modify_date: 2017-09-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_config_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-09-22 |
| Columnas | 12 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idnavconfigdet` | `int` |  |  |
| 2 | `idnavent` | `int` | ✓ |  |
| 3 | `idnavconfigenc` | `int` |  |  |
| 4 | `dia` | `int` | ✓ |  |
| 5 | `horainicio` | `datetime` | ✓ |  |
| 6 | `horafin` | `datetime` | ✓ |  |
| 7 | `frecuencia` | `int` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_mod` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_config_det` | CLUSTERED · **PK** | idnavconfigdet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_i_nav_config_det_i_nav_config_enc` → `i_nav_config_enc`
- `FK_i_nav_config_det_i_nav_ent` → `i_nav_ent`

## Quién la referencia

**1** objetos:

- `VW_navdetalleconfiguracion` (view)

