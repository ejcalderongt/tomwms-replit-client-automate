---
id: db-brain-table-trans-despacho-det-lote-num
type: db-table
title: dbo.trans_despacho_det_lote_num
schema: dbo
name: trans_despacho_det_lote_num
kind: table
rows: 0
modify_date: 2019-05-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_despacho_det_lote_num`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2019-05-21 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDespachoDetLote` | `int` |  |  |
| 2 | `IdDespachoEnc` | `int` |  |  |
| 3 | `IdPedidoEnc` | `int` | ✓ |  |
| 4 | `IdProductoBodega` | `int` | ✓ |  |
| 5 | `Lote` | `nvarchar(250)` | ✓ |  |
| 6 | `LoteNum` | `int` | ✓ |  |
| 7 | `CantidadDespachada` | `float` | ✓ |  |
| 8 | `PesoDespachado` | `float` | ✓ |  |
| 9 | `IdProductoEstado` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_despacho_det_lote` | CLUSTERED · **PK** | IdDespachoDetLote |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

