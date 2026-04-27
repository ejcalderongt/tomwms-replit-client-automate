---
id: db-brain-table-i-nav-transacciones-out-error
type: db-table
title: dbo.i_nav_transacciones_out_error
schema: dbo
name: i_nav_transacciones_out_error
kind: table
rows: 0
modify_date: 2020-10-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_transacciones_out_error`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-10-07 |
| Columnas | 13 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMensaje` | `int` |  |  |
| 2 | `IdTransaccionWMS` | `int` | ✓ |  |
| 3 | `IdTipoTransaccionWMS` | `nvarchar(50)` | ✓ |  |
| 4 | `IdDirectorio` | `int` | ✓ |  |
| 5 | `ReferenciaERP` | `nvarchar(50)` | ✓ |  |
| 6 | `TransaccionERP` | `nvarchar(50)` | ✓ |  |
| 7 | `Mensaje` | `nvarchar(350)` | ✓ |  |
| 8 | `EsError` | `bit` | ✓ |  |
| 9 | `NumeroError` | `nvarchar(50)` | ✓ |  |
| 10 | `Observacion` | `nvarchar(50)` | ✓ |  |
| 11 | `Fecha` | `datetime` | ✓ |  |
| 12 | `UsuarioERP` | `nvarchar(50)` | ✓ |  |
| 13 | `UsuarioWMS` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_transacciones_out_error` | CLUSTERED · **PK** | IdMensaje |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

