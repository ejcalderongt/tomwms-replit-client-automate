---
id: db-brain-table-sysdiagrams
type: db-table
title: dbo.sysdiagrams
schema: dbo
name: sysdiagrams
kind: table
rows: 35
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.sysdiagrams`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 35 |
| Schema modify_date | 2023-08-21 |
| Columnas | 5 |
| Índices | 2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `name` | `sysname` |  |  |
| 2 | `principal_id` | `int` |  |  |
| 3 | `diagram_id` | `int` |  | ✓ |
| 4 | `version` | `int` | ✓ |  |
| 5 | `definition` | `varbinary(max)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__sysdiagrams__1920BF5C` | CLUSTERED · **PK** | diagram_id |
| `UK_principal_name` | NONCLUSTERED · UNIQUE | principal_id, name |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**7** objetos:

- `sp_alterdiagram` (stored_procedure)
- `sp_creatediagram` (stored_procedure)
- `sp_dropdiagram` (stored_procedure)
- `sp_helpdiagramdefinition` (stored_procedure)
- `sp_helpdiagrams` (stored_procedure)
- `sp_renamediagram` (stored_procedure)
- `sp_upgraddiagrams` (stored_procedure)

