---
id: db-brain-table-trans-pe-det-log-reserva
type: db-table
title: dbo.trans_pe_det_log_reserva
schema: dbo
name: trans_pe_det_log_reserva
kind: table
rows: 22576
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_pe_det_log_reserva`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 22.576 |
| Schema modify_date | 2024-02-01 |
| Columnas | 17 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdLogReserva` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `Fecha` | `datetime` | ✓ |  |
| 4 | `IdPedidoEnc` | `int` |  |  |
| 5 | `Line_No` | `int` | ✓ |  |
| 6 | `Item_No` | `nvarchar(50)` | ✓ |  |
| 7 | `UmBas` | `nvarchar(50)` | ✓ |  |
| 8 | `Variant_Code` | `nvarchar(50)` | ✓ |  |
| 9 | `MensajeLog` | `nvarchar(max)` | ✓ |  |
| 10 | `Cantidad` | `float` | ✓ |  |
| 11 | `Caso_Reserva` | `nvarchar(50)` | ✓ |  |
| 12 | `EsError` | `bit` |  |  |
| 13 | `Referencia_Documento` | `nvarchar(50)` | ✓ |  |
| 14 | `Fecha_Vence` | `date` | ✓ |  |
| 15 | `IdPedidoDet` | `int` | ✓ |  |
| 16 | `IdStock` | `int` | ✓ |  |
| 17 | `IdStockRes` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_pe_det_log_reserva` | CLUSTERED · **PK** | IdLogReserva |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

