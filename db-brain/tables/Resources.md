---
id: db-brain-table-resources
type: db-table
title: dbo.Resources
schema: dbo
name: Resources
kind: table
rows: 0
modify_date: 2016-05-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Resources`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-05-18 |
| Columnas | 6 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `UniqueID` | `int` |  | ✓ |
| 2 | `ResourceID` | `int` |  |  |
| 3 | `ResourceName` | `nvarchar(50)` | ✓ |  |
| 4 | `Color` | `int` | ✓ |  |
| 5 | `Image` | `image` | ✓ |  |
| 6 | `CustomField1` | `nvarchar(max)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_Resources` | CLUSTERED · **PK** | UniqueID |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)

