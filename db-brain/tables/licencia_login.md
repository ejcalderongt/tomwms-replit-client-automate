---
id: db-brain-table-licencia-login
type: db-table
title: dbo.licencia_login
schema: dbo
name: licencia_login
kind: table
rows: 38
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.licencia_login`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 38 |
| Schema modify_date | 2023-08-21 |
| Columnas | 2 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idDisp` | `nvarchar(200)` |  |  |
| 2 | `valor` | `nvarchar(200)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_licencia_login` | CLUSTERED · **PK** | idDisp |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

