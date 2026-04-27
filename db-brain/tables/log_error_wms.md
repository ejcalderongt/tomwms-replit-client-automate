---
id: db-brain-table-log-error-wms
type: db-table
title: dbo.log_error_wms
schema: dbo
name: log_error_wms
kind: table
rows: 66339
modify_date: 2025-06-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.log_error_wms`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 66.339 |
| Schema modify_date | 2025-06-12 |
| Columnas | 15 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdError` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `Fecha` | `datetime` | ✓ |  |
| 5 | `MensajeError` | `nvarchar(2500)` | ✓ |  |
| 6 | `IdPedidoEnc` | `int` |  |  |
| 7 | `IdPickingEnc` | `int` |  |  |
| 8 | `IdRecepcionEnc` | `int` |  |  |
| 9 | `IdUsuarioAgr` | `int` |  |  |
| 10 | `Line_No` | `int` | ✓ |  |
| 11 | `Item_No` | `nvarchar(50)` | ✓ |  |
| 12 | `UmBas` | `nvarchar(50)` | ✓ |  |
| 13 | `Variant_Code` | `nvarchar(50)` | ✓ |  |
| 14 | `Cantidad` | `float` | ✓ |  |
| 15 | `Referencia_Documento` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_log_error_wms` | CLUSTERED · **PK** | IdError |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

