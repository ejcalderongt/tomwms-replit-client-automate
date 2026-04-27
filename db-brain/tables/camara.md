---
id: db-brain-table-camara
type: db-table
title: dbo.camara
schema: dbo
name: camara
kind: table
rows: 2
modify_date: 2018-01-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.camara`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 2 |
| Schema modify_date | 2018-01-11 |
| Columnas | 12 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdCamara` | `int` |  |  |
| 2 | `codigo` | `varchar(50)` | ✓ |  |
| 3 | `nombre` | `varchar(50)` | ✓ |  |
| 4 | `modelo` | `varchar(50)` | ✓ |  |
| 5 | `serie` | `varchar(50)` | ✓ |  |
| 6 | `Ip` | `varchar(50)` | ✓ |  |
| 7 | `IdUbicacion` | `int` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_mod` | `datetime` | ✓ |  |
| 12 | `activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_camara` | CLUSTERED · **PK** | IdCamara |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto` (`FK_producto_camara`)

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

