---
id: db-brain-table-contenedor
type: db-table
title: dbo.contenedor
schema: dbo
name: contenedor
kind: table
rows: 0
modify_date: 2016-07-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.contenedor`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-07-12 |
| Columnas | 7 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdContenedor` | `int` |  |  |
| 2 | `IdTipoContenedor` | `int` | ✓ |  |
| 3 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 4 | `fec_agr` | `datetime` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_mod` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_contenedor` | CLUSTERED · **PK** | IdContenedor |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)

