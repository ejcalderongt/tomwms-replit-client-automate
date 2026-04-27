---
id: db-brain-table-appointments
type: db-table
title: dbo.Appointments
schema: dbo
name: Appointments
kind: table
rows: 0
modify_date: 2016-05-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Appointments`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-05-18 |
| Columnas | 15 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `UniqueID` | `int` |  | ✓ |
| 2 | `Type` | `int` | ✓ |  |
| 3 | `StartDate` | `smalldatetime` | ✓ |  |
| 4 | `EndDate` | `smalldatetime` | ✓ |  |
| 5 | `AllDay` | `bit` | ✓ |  |
| 6 | `Subject` | `nvarchar(50)` | ✓ |  |
| 7 | `Location` | `nvarchar(50)` | ✓ |  |
| 8 | `Description` | `nvarchar(max)` | ✓ |  |
| 9 | `Status` | `int` | ✓ |  |
| 10 | `Label` | `int` | ✓ |  |
| 11 | `ResourceID` | `int` | ✓ |  |
| 12 | `ResourceIDs` | `nvarchar(max)` | ✓ |  |
| 13 | `ReminderInfo` | `nvarchar(max)` | ✓ |  |
| 14 | `RecurrenceInfo` | `nvarchar(max)` | ✓ |  |
| 15 | `Ubicacion` | `nvarchar(max)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_Appointments` | CLUSTERED · **PK** | UniqueID |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)

