---
id: db-brain-table-licencia-llave
type: db-table
title: dbo.licencia_llave
schema: dbo
name: licencia_llave
kind: table
rows: 1
modify_date: 2017-10-31
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.licencia_llave`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2017-10-31 |
| Columnas | 2 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idLlave` | `int` |  |  |
| 2 | `Llave` | `nvarchar(250)` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_licencia_llave` | CLUSTERED · **PK** | idLlave |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

