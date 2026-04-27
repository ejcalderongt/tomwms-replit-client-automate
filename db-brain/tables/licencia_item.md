---
id: db-brain-table-licencia-item
type: db-table
title: dbo.licencia_item
schema: dbo
name: licencia_item
kind: table
rows: 23
modify_date: 2022-12-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.licencia_item`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 23 |
| Schema modify_date | 2022-12-17 |
| Columnas | 6 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idDisp` | `nvarchar(200)` |  |  |
| 2 | `identificacion` | `nvarchar(200)` | ✓ |  |
| 3 | `tipo` | `int` | ✓ |  |
| 4 | `bandera` | `int` | ✓ |  |
| 5 | `estado` | `nvarchar(200)` | ✓ |  |
| 6 | `fecha_sistema` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_licencia_item` | CLUSTERED · **PK** | idDisp |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

