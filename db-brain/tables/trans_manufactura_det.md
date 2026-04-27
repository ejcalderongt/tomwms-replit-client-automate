---
id: db-brain-table-trans-manufactura-det
type: db-table
title: dbo.trans_manufactura_det
schema: dbo
name: trans_manufactura_det
kind: table
rows: 0
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_manufactura_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-07-02 |
| Columnas | 13 |
| Índices | 1 |
| FKs | out:4 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdManufacturaDet` | `int` |  |  |
| 2 | `IdManufacturaEnc` | `int` |  |  |
| 3 | `IdPedidoDet` | `int` |  |  |
| 4 | `IdPropietarioBodega` | `int` | ✓ |  |
| 5 | `IdProductoBodega` | `int` |  |  |
| 6 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 7 | `nombre_producto` | `nvarchar(150)` | ✓ |  |
| 8 | `cantidad_esperada` | `float` | ✓ |  |
| 9 | `cantidad_recibida` | `float` | ✓ |  |
| 10 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 11 | `fec_agr` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 13 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_manufactura_det` | CLUSTERED · **PK** | IdManufacturaDet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_manufactura_det_producto_bodega` → `producto_bodega`
- `FK_trans_manufactura_det_propietario_bodega` → `propietario_bodega`
- `FK_trans_manufactura_det_trans_manufactura_enc` → `trans_manufactura_enc`
- `FK_trans_manufactura_det_trans_pe_det` → `trans_pe_det`

### Entrantes (otra tabla → esta)

- `trans_manufactura_picking` (`FK_trans_manufactura_picking_trans_manufactura_det`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

