---
id: db-brain-table-stock
type: db-table
title: dbo.stock
schema: dbo
name: stock
status: activa
sources:
  - extracted_from: TOMWMS_KILLIOS_PRD @ 52.41.114.122,1437
  - extracted_at: 2026-04-27T01:21:57.791Z
referenced_by:
  # (sin entities en wms-brain todavía)
---

# `dbo.stock`

| Atributo | Valor |
|---|---|
| Filas | 4.703 |
| Última modificación schema | 2024-09-12 |
| Columnas | 33 |
| Índices | 14 |
| Flags bit (parametrización) | 0 |

## Columnas

| # | Nombre | Tipo | Null | PK | Flag |
|---:|---|---|:-:|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |  |
| 2 | `IdStock` | `int` |  |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |  |
| 4 | `IdProductoBodega` | `int` |  |  |  |
| 5 | `IdProductoEstado` | `int` |  |  |  |
| 6 | `IdPresentacion` | `int` | ✓ |  |  |
| 7 | `IdUnidadMedida` | `int` | ✓ |  |  |
| 8 | `IdUbicacion` | `int` |  |  |  |
| 9 | `IdUbicacion_anterior` | `int` | ✓ |  |  |
| 10 | `IdRecepcionEnc` | `int` | ✓ |  |  |
| 11 | `IdRecepcionDet` | `int` | ✓ |  |  |
| 12 | `IdPedidoEnc` | `int` | ✓ |  |  |
| 13 | `IdPickingEnc` | `int` | ✓ |  |  |
| 14 | `IdDespachoEnc` | `int` | ✓ |  |  |
| 15 | `lote` | `nvarchar(50)` |  |  |  |
| 16 | `lic_plate` | `nvarchar(50)` | ✓ |  |  |
| 17 | `serial` | `nvarchar(50)` | ✓ |  |  |
| 18 | `cantidad` | `float` |  |  |  |
| 19 | `fecha_ingreso` | `datetime` | ✓ |  |  |
| 20 | `fecha_vence` | `datetime` | ✓ |  |  |
| 21 | `uds_lic_plate` | `float` | ✓ |  |  |
| 22 | `no_bulto` | `int` | ✓ |  |  |
| 23 | `fecha_manufactura` | `datetime` | ✓ |  |  |
| 24 | `añada` | `int` | ✓ |  |  |
| 25 | `user_agr` | `nvarchar(50)` |  |  |  |
| 26 | `fec_agr` | `datetime` |  |  |  |
| 27 | `user_mod` | `nvarchar(50)` |  |  |  |
| 28 | `fec_mod` | `datetime` |  |  |  |
| 29 | `activo` | `bit` |  |  |  |
| 30 | `peso` | `float` | ✓ |  |  |
| 31 | `temperatura` | `float` | ✓ |  |  |
| 32 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |  |
| 33 | `pallet_no_estandar` | `bit` | ✓ |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `NCLI_BODEGA_UBICACION_20210217_EJC` | NONCLUSTERED | IdStock, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion, IdUbicacion_anterior, IdRecepcionEnc, lote, lic_plate, serial, cantidad, fecha_ingreso, fecha_vence, añada, peso, IdBodega, IdPropietarioBodega |
| `NCLI_STOCK_20191205_EJC` | NONCLUSTERED | IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion, lote, lic_plate, cantidad, fecha_ingreso, fecha_vence, IdPropietarioBodega |
| `NCLI_Stock_20191210_EJC` | NONCLUSTERED | IdUbicacion |
| `NCLI_Stock_20200112_EJC` | NONCLUSTERED | IdBodega, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion, IdUbicacion_anterior, IdRecepcionEnc, lote, lic_plate, serial, cantidad, fecha_ingreso, fecha_vence, añada, peso, atributo_variante_1, IdPropietarioBodega |
| `NCLI_Stock_20200115_EJC` | NONCLUSTERED | IdBodega, IdPresentacion, IdRecepcionEnc, IdRecepcionDet, lote, cantidad, fecha_vence, IdProductoBodega |
| `NCLI_Stock_20210304_EJC` | NONCLUSTERED | cantidad, IdBodega, IdProductoBodega, IdProductoEstado, IdUnidadMedida |
| `NCLI_STOCK_202210270100_EJC` | NONCLUSTERED | IdBodega, IdUbicacion |
| `NCLI_STOCK_202302211402_EJC` | NONCLUSTERED | IdBodega, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion, IdUbicacion_anterior, IdRecepcionEnc, IdRecepcionDet, lote, lic_plate, serial, cantidad, fecha_ingreso, fecha_vence, añada, peso, atributo_variante_1, pallet_no_estandar, IdPropietarioBodega |
| `NCLI_Stock_20230522_EJC` | NONCLUSTERED | IdBodega, IdPropietarioBodega, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion, IdUbicacion_anterior, IdRecepcionDet, lote, lic_plate, serial, cantidad, fecha_ingreso, fecha_vence, añada, peso, atributo_variante_1, pallet_no_estandar, IdRecepcionEnc |
| `NCLI_Stock_202308081128_EJC` | NONCLUSTERED | IdBodega, IdUbicacion, lote, lic_plate, cantidad, IdPresentacion |
| `NCLI_Stock_EJC20210217` | NONCLUSTERED | IdStock, IdPropietarioBodega, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUbicacion, IdUbicacion_anterior, IdRecepcionEnc, lote, lic_plate, serial, cantidad, fecha_ingreso, fecha_vence, añada, peso, atributo_variante_1, IdBodega, IdUnidadMedida |
| `NCLI_STOCK_LICPLATE_202217120405AM_EJC` | NONCLUSTERED | IdBodega, lic_plate |
| `NCLI_STOCK_MERCOPAN_20220503_EJC` | NONCLUSTERED | IdBodega, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion, IdUbicacion_anterior, IdRecepcionEnc, lote, lic_plate, serial, cantidad, fecha_ingreso, fecha_vence, añada, peso, atributo_variante_1, IdPropietarioBodega |
| `PK_stock` | CLUSTERED · **PK** · UNIQUE | IdStock |

## Foreign Keys

### FKs salientes (esta tabla → otra)
- `FK_stock_bodega_ubicacion` → `bodega_ubicacion`
- `FK_stock_bodega_ubicacion1` → `bodega_ubicacion`
- `FK_stock_trans_pe_enc` → `trans_pe_enc`
- `FK_stock_trans_pe_enc_rec` → `trans_pe_enc`
- `FK_stock_propietario_bodega` → `propietario_bodega`
- `FK_stock_producto_bodega` → `producto_bodega`
- `FK_stock_producto_estado` → `producto_estado`
- `FK_stock_unidad_medida` → `unidad_medida`
- `FK_stock_producto_presentacion` → `producto_presentacion`
- `FK_stock_producto_presentacion_rec` → `producto_presentacion`
- `FK_stock_producto_bodega_rec` → `producto_bodega`
- `FK_stock_producto_estado_rec` → `producto_estado`
- `FK_stock_propietario_bodega_rec` → `propietario_bodega`
- `FK_stock_unidad_medida_rec` → `unidad_medida`
- `FK_stock_trans_re_det` → `trans_re_det`

### FKs entrantes (otra tabla → esta)
- `stock_se` (`FK_stock_se_stock`)
- `trans_inventario_det` (`FK_trans_inventario_det_stock`)
- `stock_parametro` (`FK_stock_parametro_stock`)

## Dependencias (quién la referencia)

**59 objetos** la referencian:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `GetListaStockByProductoBodega` (stored_procedure)
- `GetResumenStockCantidad` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion` (stored_procedure)
- `Stock_NonNegative_20200115_EJC` (check_constraint)
- `VW_CalculoVencimientos` (view)
- `VW_Detalle_Licencias_Inconsistentes` (view)
- `VW_EstacionalidadProducto` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `vw_existencias_producto_categoria` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Ingresos_Sin_Ticket` (view)
- `VW_Licencias_Por_Ubicacion` (view)
- `VW_MinimosMaximosPorPresentacion` (view)
- `VW_Movimientos_Retroactivos` (view)
- `VW_OcupacionBodega` (view)
- `VW_OcupacionBodegaTramo` (view)
- `VW_Productividad_Picking` (view)
- `VW_ProductoDimension` (view)
- `VW_ProximosVencimiento` (view)
- `VW_Reporte_Detalle_Stock_DataSet` (view)
- `VW_Revision_Producto` (view)
- `VW_RevisionProducto` (view)

_... +29 más_

## Notas

- Núcleo del modelo. **59 objetos** la referencian (CLBD, varias VW_*, SP_Importa_Stock_*).
- Campo `lote` es parte del stock, **no FK** a maestro (no existe maestro de lotes).
- Tiene `CHECK_CONSTRAINT` `Stock_NonNegative_20200115_EJC` — no permite cantidades negativas.
- Snapshots periódicos: `stock_YYYYMMDD`, `stock_hist`, `stock_jornada`, etc.
