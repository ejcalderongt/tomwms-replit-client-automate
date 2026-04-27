---
id: db-brain-table-tablas-sync
type: db-table
title: dbo.tablas_sync
schema: dbo
name: tablas_sync
kind: table
rows: 0
modify_date: 2016-08-10
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tablas_sync`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-08-10 |
| Columnas | 5 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTabla` | `int` |  |  |
| 2 | `NombreTabla` | `nvarchar(50)` | ✓ |  |
| 3 | `Sincronizar` | `bit` | ✓ |  |
| 4 | `UltimaSincronizacion` | `timestamp` | ✓ |  |
| 5 | `TimpoSyncSegundos` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tablas_sync` | CLUSTERED · **PK** | IdTabla |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)

