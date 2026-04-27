---
id: db-brain-table-trans-ubic-hh-stock
type: db-table
title: dbo.trans_ubic_hh_stock
schema: dbo
name: trans_ubic_hh_stock
kind: table
rows: 683
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_ubic_hh_stock`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 683 |
| Schema modify_date | 2023-08-21 |
| Columnas | 35 |
| Índices | 3 |
| FKs | out:13 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockTransUbicHHDet` | `int` |  |  |
| 2 | `IdTareaUbicacionEnc` | `int` |  |  |
| 3 | `IdTareaUbicacionDet` | `int` |  |  |
| 4 | `IdStock` | `int` |  |  |
| 5 | `IdPropietarioBodega` | `int` |  |  |
| 6 | `IdProductoBodega` | `int` |  |  |
| 7 | `IdProductoEstado` | `int` | ✓ |  |
| 8 | `IdPresentacion` | `int` | ✓ |  |
| 9 | `IdUnidadMedida` | `int` | ✓ |  |
| 10 | `IdUbicacion` | `int` | ✓ |  |
| 11 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 12 | `IdRecepcionEnc` | `int` | ✓ |  |
| 13 | `IdRecepcionDet` | `int` | ✓ |  |
| 14 | `IdPedidoEnc` | `int` | ✓ |  |
| 15 | `IdPickingEnc` | `int` | ✓ |  |
| 16 | `IdDespachoEnc` | `int` | ✓ |  |
| 17 | `lote` | `nvarchar(50)` | ✓ |  |
| 18 | `lic_plate` | `varchar(max)` | ✓ |  |
| 19 | `serial` | `nvarchar(50)` | ✓ |  |
| 20 | `cantidad` | `float` | ✓ |  |
| 21 | `fecha_ingreso` | `datetime` | ✓ |  |
| 22 | `fecha_vence` | `datetime` | ✓ |  |
| 23 | `uds_lic_plate` | `varchar(max)` | ✓ |  |
| 24 | `no_bulto` | `int` | ✓ |  |
| 25 | `fecha_manufactura` | `datetime` | ✓ |  |
| 26 | `añada` | `int` | ✓ |  |
| 27 | `user_agr` | `nvarchar(50)` |  |  |
| 28 | `fec_agr` | `datetime` |  |  |
| 29 | `user_mod` | `nvarchar(50)` |  |  |
| 30 | `fec_mod` | `datetime` |  |  |
| 31 | `activo` | `bit` |  |  |
| 32 | `peso` | `float` | ✓ |  |
| 33 | `temperatura` | `float` | ✓ |  |
| 34 | `fecha_mov_hist` | `datetime` | ✓ |  |
| 35 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_ubic_hh_stock` | CLUSTERED · **PK** | IdStockTransUbicHHDet |
| `NCLI_trans_ubic_hh_stock_EJC_20220503` | NONCLUSTERED | IdTareaUbicacionEnc, IdTareaUbicacionDet, IdStock |
| `NCLI_Trans_Ubic_HH_Stock_20220503_EJC` | NONCLUSTERED | añada, fecha_vence, fecha_ingreso, serial, lic_plate, lote, IdUnidadMedida, IdPresentacion, IdProductoEstado, IdStock, IdTareaUbicacionDet, IdTareaUbicacionEnc, IdProductoBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_stock_ubic_hh_producto_bodega` → `producto_bodega`
- `FK_stock_ubic_hh_producto_bodega_rec` → `producto_bodega`
- `FK_stock_ubic_hh_producto_estado` → `producto_estado`
- `FK_stock_ubic_hh_producto_estado_rec` → `producto_estado`
- `FK_stock_ubic_hh_producto_presentacion` → `producto_presentacion`
- `FK_stock_ubic_hh_producto_presentacion_rec` → `producto_presentacion`
- `FK_stock_ubic_hh_propietario_bodega` → `propietario_bodega`
- `FK_stock_ubic_hh_propietario_bodega_rec` → `propietario_bodega`
- `FK_stock_ubic_hh_trans_pe_enc` → `trans_pe_enc`
- `FK_stock_ubic_hh_trans_pe_enc_rec` → `trans_pe_enc`
- `FK_stock_ubic_hh_trans_re_det` → `trans_re_det`
- `FK_stock_ubic_hh_unidad_medida` → `unidad_medida`
- `FK_stock_ubic_hh_unidad_medida_rec` → `unidad_medida`

## Quién la referencia

**5** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_ControlCalidad_CambioEstado` (view)
- `VW_TransUbicHhDet` (view)

