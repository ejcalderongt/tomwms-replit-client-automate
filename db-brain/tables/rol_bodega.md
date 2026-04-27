---
id: db-brain-table-rol-bodega
type: db-table
title: dbo.rol_bodega
schema: dbo
name: rol_bodega
kind: table
rows: 0
modify_date: 2016-04-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.rol_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-04-25 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRolBodega` | `int` |  |  |
| 2 | `IdRol` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_rol_bodega` | CLUSTERED · **PK** | IdRolBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_rol_bodega_bodega` → `bodega`
- `FK_rol_bodega_rol` → `rol`

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)

