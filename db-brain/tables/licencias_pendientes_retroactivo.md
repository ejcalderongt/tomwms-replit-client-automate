---
id: db-brain-table-licencias-pendientes-retroactivo
type: db-table
title: dbo.licencias_pendientes_retroactivo
schema: dbo
name: licencias_pendientes_retroactivo
kind: table
rows: 0
modify_date: 2023-04-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.licencias_pendientes_retroactivo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-04-17 |
| Columnas | 3 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `licencia` | `nvarchar(50)` |  |  |
| 2 | `fecha_ticket` | `datetime` |  |  |
| 3 | `fecha_inicial` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_licencias_pendientes_retroactivo` | CLUSTERED · **PK** | licencia |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `SP_STOCK_JORNADA_DESFASE_RETROACTIVO` (stored_procedure)

