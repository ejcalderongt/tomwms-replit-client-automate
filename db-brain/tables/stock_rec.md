---
id: db-brain-table-stock-rec
type: db-table
title: dbo.stock_rec
schema: dbo
name: stock_rec
kind: table
rows: 4394
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_rec`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4.394 |
| Schema modify_date | 2023-08-21 |
| Columnas | 37 |
| Índices | 2 |
| FKs | out:6 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockRec` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdProductoEstado` | `int` | ✓ |  |
| 5 | `IdPresentacion` | `int` | ✓ |  |
| 6 | `IdUnidadMedida` | `int` | ✓ |  |
| 7 | `IdUbicacion` | `int` |  |  |
| 8 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 9 | `IdRecepcionEnc` | `int` | ✓ |  |
| 10 | `IdRecepcionDet` | `int` | ✓ |  |
| 11 | `IdPedidoEnc` | `int` | ✓ |  |
| 12 | `IdPickingEnc` | `int` | ✓ |  |
| 13 | `IdDespachoEnc` | `int` | ✓ |  |
| 14 | `lote` | `nvarchar(50)` |  |  |
| 15 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 16 | `serial` | `nvarchar(50)` | ✓ |  |
| 17 | `cantidad` | `float` | ✓ |  |
| 18 | `fecha_ingreso` | `datetime` | ✓ |  |
| 19 | `fecha_vence` | `datetime` | ✓ |  |
| 20 | `uds_lic_plate` | `float` | ✓ |  |
| 21 | `no_bulto` | `int` | ✓ |  |
| 22 | `fecha_manufactura` | `datetime` | ✓ |  |
| 23 | `añada` | `int` | ✓ |  |
| 24 | `user_agr` | `nvarchar(50)` |  |  |
| 25 | `fec_agr` | `datetime` |  |  |
| 26 | `user_mod` | `nvarchar(50)` |  |  |
| 27 | `fec_mod` | `datetime` |  |  |
| 28 | `activo` | `bit` |  |  |
| 29 | `peso` | `float` | ✓ |  |
| 30 | `temperatura` | `float` | ✓ |  |
| 31 | `regularizado` | `bit` | ✓ |  |
| 32 | `fecha_regularizacion` | `datetime` | ✓ |  |
| 33 | `no_linea` | `int` | ✓ |  |
| 34 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 35 | `impreso` | `bit` | ✓ |  |
| 36 | `IdBodega` | `int` | ✓ |  |
| 37 | `pallet_no_estandar` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_stock_rec` | CLUSTERED · **PK** | IdStockRec |
| `NCLI_STOCK_REC_LICPLATE_EJC20221712` | NONCLUSTERED | lic_plate, IdBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_stock_rec_producto_bodega` → `producto_bodega`
- `FK_stock_rec_producto_estado` → `producto_estado`
- `FK_stock_rec_producto_presentacion` → `producto_presentacion`
- `FK_stock_rec_propietario_bodega` → `propietario_bodega`
- `FK_stock_rec_trans_re_det` → `trans_re_det`
- `FK_stock_rec_unidad_medida` → `unidad_medida`

### Entrantes (otra tabla → esta)

- `stock_se_rec` (`FK_stock_se_rec_stock_rec`)

## Quién la referencia

**6** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `GetCantidadPesoByProductoBodega` (stored_procedure)
- `VW_Impresion_Pallet_Rec` (view)

