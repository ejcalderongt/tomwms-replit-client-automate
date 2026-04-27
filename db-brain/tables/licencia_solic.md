---
id: db-brain-table-licencia-solic
type: db-table
title: dbo.licencia_solic
schema: dbo
name: licencia_solic
kind: table
rows: 1
modify_date: 2022-01-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.licencia_solic`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2022-01-21 |
| Columnas | 5 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idDisp` | `nvarchar(200)` |  |  |
| 2 | `identificacion` | `nvarchar(200)` | ✓ |  |
| 3 | `tipo` | `int` | ✓ |  |
| 4 | `estado` | `nvarchar(200)` | ✓ |  |
| 6 | `fecha_solicitud` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_licencia_solic` | CLUSTERED · **PK** | idDisp |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

