---
id: db-brain-table-trans-picking-img
type: db-table
title: dbo.trans_picking_img
schema: dbo
name: trans_picking_img
kind: table
rows: 0
modify_date: 2023-10-30
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_picking_img`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-30 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:4 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdImagen` | `int` |  |  |
| 2 | `IdPickingEnc` | `int` |  |  |
| 3 | `IdPickingDet` | `int` |  |  |
| 4 | `IdPedidoEnc` | `int` |  |  |
| 5 | `IdPedidoDet` | `int` |  |  |
| 6 | `Imagen` | `image` |  |  |
| 7 | `user_agr` | `nvarchar(50)` |  |  |
| 8 | `fec_agr` | `datetime` |  |  |
| 9 | `observacion` | `nvarchar(150)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_picking_img_1` | CLUSTERED · **PK** | IdImagen |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_picking_img_trans_pe_det` → `trans_pe_det`
- `FK_trans_picking_img_trans_pe_enc` → `trans_pe_enc`
- `FK_trans_picking_img_trans_picking_det` → `trans_picking_det`
- `FK_trans_picking_img_trans_picking_enc` → `trans_picking_enc`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

