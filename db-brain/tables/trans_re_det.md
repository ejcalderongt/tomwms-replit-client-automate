---
id: db-brain-table-trans-re-det
type: db-table
title: dbo.trans_re_det
schema: dbo
name: trans_re_det
kind: table
rows: 4394
modify_date: 2024-10-15
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4.394 |
| Schema modify_date | 2024-10-15 |
| Columnas | 37 |
| Índices | 6 |
| FKs | out:7 in:6 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionDet` | `int` |  |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdPresentacion` | `int` | ✓ |  |
| 5 | `IdUnidadMedida` | `int` | ✓ |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `IdOperadorBodega` | `int` | ✓ |  |
| 8 | `IdMotivoDevolucion` | `int` | ✓ |  |
| 9 | `No_Linea` | `int` | ✓ |  |
| 10 | `cantidad_recibida` | `float` | ✓ |  |
| 11 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 12 | `nombre_presentacion` | `nvarchar(50)` | ✓ |  |
| 13 | `nombre_unidad_medida` | `nvarchar(50)` | ✓ |  |
| 14 | `nombre_producto_estado` | `nvarchar(50)` | ✓ |  |
| 15 | `lote` | `nvarchar(50)` | ✓ |  |
| 16 | `fecha_vence` | `datetime` | ✓ |  |
| 17 | `fecha_ingreso` | `datetime` | ✓ |  |
| 18 | `peso` | `float` | ✓ |  |
| 19 | `peso_estadistico` | `float` | ✓ |  |
| 20 | `peso_minimo` | `float` | ✓ |  |
| 21 | `peso_maximo` | `float` | ✓ |  |
| 22 | `peso_unitario` | `float` | ✓ |  |
| 23 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 24 | `fec_agr` | `datetime` | ✓ |  |
| 25 | `observacion` | `nvarchar(150)` | ✓ |  |
| 26 | `añada` | `int` | ✓ |  |
| 27 | `costo` | `float` | ✓ |  |
| 28 | `costo_oc` | `float` | ✓ |  |
| 29 | `costo_estadistico` | `float` | ✓ |  |
| 30 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 31 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 33 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 34 | `pallet_no_estandar` | `bit` | ✓ |  |
| 35 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 36 | `IdOrdenCompraDet` | `int` | ✓ |  |
| 37 | `IdJornadaSistema` | `int` | ✓ |  |
| 38 | `host` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_recepcion_det` | CLUSTERED · **PK** | IdRecepcionDet, IdRecepcionEnc |
| `NCL_Trans_re_Det_20200115_EJC` | NONCLUSTERED | IdPresentacion, IdUnidadMedida, IdProductoEstado, cantidad_recibida, lic_plate, IdProductoBodega |
| `NCL_trans_re_det_rep_20200115_ejc` | NONCLUSTERED | No_Linea, nombre_producto, nombre_presentacion, nombre_unidad_medida, nombre_producto_estado, lote, fecha_vence, fec_agr, observacion, codigo_producto, lic_plate, IdRecepcionEnc |
| `IX_trans_re_det_20230126` | NONCLUSTERED | IdOrdenCompraEnc |
| `IX_trans_re_det_20230126_2` | NONCLUSTERED | IdProductoBodega, IdPresentacion, IdUnidadMedida, IdProductoEstado, IdOperadorBodega, IdMotivoDevolucion, No_Linea, cantidad_recibida, nombre_producto, nombre_presentacion, nombre_unidad_medida, nombre_producto_estado, lote, fecha_vence, fecha_ingreso, peso, peso_estadistico, peso_minimo, peso_maximo, peso_unitario, user_agr, fec_agr, observacion, añada, costo, costo_oc, costo_estadistico, atributo_variante_1, codigo_producto, lic_plate, pallet_no_estandar, IdOrdenCompraEnc, IdOrdenCompraDet |
| `NCL_trans_re_det_20240306` | NONCLUSTERED | IdProductoEstado, cantidad_recibida, codigo_producto, IdPresentacion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_re_det_motivo_devolucion` → `motivo_devolucion`
- `FK_trans_re_det_operador_bodega` → `operador_bodega`
- `FK_trans_re_det_producto_bodega` → `producto_bodega`
- `FK_trans_re_det_unidad_medida` → `unidad_medida`
- `FK_trans_recepcion_det_producto_estado` → `producto_estado`
- `FK_trans_recepcion_det_producto_presentacion` → `producto_presentacion`
- `FK_trans_recepcion_det_trans_recepcion_enc` → `trans_re_enc`

### Entrantes (otra tabla → esta)

- `producto_pallet` (`FK_producto_pallet_trans_re_det`)
- `stock_hist` (`FK_stock_hist_trans_re_det`)
- `stock_jornada` (`FK_stock_jornada_trans_re_det`)
- `stock_rec` (`FK_stock_rec_trans_re_det`)
- `stock` (`FK_stock_trans_re_det`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_trans_re_det`)

## Quién la referencia

**31** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_historico` (view)
- `VW_Get_Detalle_By_IdRecepcionEnc` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `VW_Indicador_Ingresos` (view)
- `VW_Ingresos_Sin_Ticket` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Movimientos_Retroactivos` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_CONOC_FIN` (view)
- `VW_REC_SIN_OC` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_RecepcionConOC` (view)
- `VW_RecepcionCostoArancel` (view)
- `VW_RecepcionSinOC` (view)
- `VW_RecOC_MIX` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Reporte_Recepcion_20190727` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Tiempos_Ingreso_Operador` (view)

