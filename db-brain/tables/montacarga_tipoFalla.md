---
id: db-brain-table-montacarga-tipofalla
type: db-table
title: dbo.montacarga_tipoFalla
schema: dbo
name: montacarga_tipoFalla
kind: table
rows: 2
modify_date: 2016-05-03
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.montacarga_tipoFalla`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 2 |
| Schema modify_date | 2016-05-03 |
| Columnas | 4 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoFalla` | `nvarchar(50)` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 3 | `Nombre` | `nvarchar(100)` | ✓ |  |
| 4 | `Activo` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_TipoFalla` | CLUSTERED · **PK** | IdTipoFalla |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

