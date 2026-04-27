---
id: db-brain-table-auditoria
type: db-table
title: dbo.Auditoria
schema: dbo
name: Auditoria
kind: table
rows: 0
modify_date: 2023-10-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Auditoria`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-29 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `AuditoriaId` | `int` |  | ✓ |
| 2 | `Tabla` | `nvarchar(100)` | ✓ |  |
| 3 | `RegistroId` | `int` | ✓ |  |
| 4 | `TipoOperacion` | `nvarchar(50)` | ✓ |  |
| 5 | `Campo` | `nvarchar(100)` | ✓ |  |
| 6 | `ValorAnterior` | `nvarchar(max)` | ✓ |  |
| 7 | `ValorNuevo` | `nvarchar(max)` | ✓ |  |
| 8 | `Usuario` | `nvarchar(50)` | ✓ |  |
| 9 | `FechaOperacion` | `datetime2` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__Auditori__095694C36FCA8011` | CLUSTERED · **PK** | AuditoriaId |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

