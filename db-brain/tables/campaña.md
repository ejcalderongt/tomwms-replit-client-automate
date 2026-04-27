---
id: db-brain-table-campa-a
type: db-table
title: dbo.campaña
schema: dbo
name: campaña
kind: table
rows: 0
modify_date: 2025-05-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.campaña`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-05-13 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdCampaña` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(100)` |  |  |
| 3 | `FechaInicio` | `datetime` |  |  |
| 4 | `FechaFin` | `datetime` |  |  |
| 5 | `fec_agr` | `datetime` |  |  |
| 6 | `user_agr` | `nvarchar(25)` |  |  |
| 7 | `fec_mod` | `datetime` |  |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__campaña__168520509BD8C08D` | CLUSTERED · **PK** | IdCampaña |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto_talla_color` (`FK__producto___IdCam__56C1F7D5`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

