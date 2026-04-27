---
id: db-brain-table-trans-manufactura-picking
type: db-table
title: dbo.trans_manufactura_picking
schema: dbo
name: trans_manufactura_picking
kind: table
rows: 0
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_manufactura_picking`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-07-02 |
| Columnas | 13 |
| Índices | 1 |
| FKs | out:4 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdManufacturaPicking` | `int` |  |  |
| 2 | `IdManufacturaDet` | `int` |  |  |
| 3 | `IdManufacturaEnc` | `int` |  |  |
| 4 | `IdPedidoDet` | `int` |  |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 7 | `licencia` | `nvarchar(50)` | ✓ |  |
| 8 | `cantidad` | `float` | ✓ |  |
| 9 | `licencia_manufactura` | `nvarchar(50)` | ✓ |  |
| 10 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 11 | `fec_agr` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 13 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_manufactura_picking` | CLUSTERED · **PK** | IdManufacturaPicking |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_manufactura_picking_producto_bodega` → `producto_bodega`
- `FK_trans_manufactura_picking_trans_manufactura_det` → `trans_manufactura_det`
- `FK_trans_manufactura_picking_trans_manufactura_enc` → `trans_manufactura_enc`
- `FK_trans_manufactura_picking_trans_pe_det` → `trans_pe_det`

## Quién la referencia

**2** objetos:

- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Consolidada_LFV` (view)

