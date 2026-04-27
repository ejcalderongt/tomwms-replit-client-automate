---
id: db-brain-table-operador-montacarga
type: db-table
title: dbo.operador_montacarga
schema: dbo
name: operador_montacarga
kind: table
rows: 0
modify_date: 2022-06-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.operador_montacarga`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-06-13 |
| Columnas | 7 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdAsignacionMontacarga` | `int` |  |  |
| 2 | `IdOperador` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `IdMontacarga` | `int` | ✓ |  |
| 5 | `Fecha_Asignacion` | `datetime` | ✓ |  |
| 6 | `Fecha_Inactivo` | `datetime` | ✓ |  |
| 7 | `Activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_operador_montacarga` | CLUSTERED · **PK** | IdAsignacionMontacarga |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

