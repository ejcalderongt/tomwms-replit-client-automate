---
id: db-brain-table-stock-hist
type: db-table
title: dbo.stock_hist
schema: dbo
name: stock_hist
kind: table
rows: 19225
modify_date: 2021-06-03
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.stock_hist`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 19.225 |
| Schema modify_date | 2021-06-03 |
| Columnas | 33 |
| Índices | 1 |
| FKs | out:13 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdStockHist` | `int` |  |  |
| 2 | `IdStock` | `int` |  |  |
| 3 | `IdNuevoStock` | `int` |  |  |
| 4 | `IdPropietarioBodega` | `int` |  |  |
| 5 | `IdProductoBodega` | `int` |  |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `IdUnidadMedida` | `int` | ✓ |  |
| 9 | `IdUbicacion` | `int` | ✓ |  |
| 10 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 11 | `IdRecepcionEnc` | `int` | ✓ |  |
| 12 | `IdRecepcionDet` | `int` | ✓ |  |
| 13 | `IdPedidoEnc` | `int` | ✓ |  |
| 14 | `IdPickingEnc` | `int` | ✓ |  |
| 15 | `IdDespachoEnc` | `int` | ✓ |  |
| 16 | `lote` | `nvarchar(50)` | ✓ |  |
| 17 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 18 | `serial` | `nvarchar(50)` | ✓ |  |
| 19 | `cantidad` | `float` | ✓ |  |
| 20 | `fecha_ingreso` | `datetime` | ✓ |  |
| 21 | `fecha_vence` | `datetime` | ✓ |  |
| 22 | `uds_lic_plate` | `float` | ✓ |  |
| 23 | `no_bulto` | `nvarchar(20)` | ✓ |  |
| 24 | `fecha_manufactura` | `datetime` | ✓ |  |
| 25 | `añada` | `int` | ✓ |  |
| 26 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 27 | `fec_agr` | `datetime` | ✓ |  |
| 28 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 29 | `fec_mod` | `datetime` | ✓ |  |
| 30 | `activo` | `bit` | ✓ |  |
| 31 | `peso` | `float` | ✓ |  |
| 32 | `temperatura` | `float` | ✓ |  |
| 33 | `posiciones` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_stock_hist` | CLUSTERED · **PK** | IdStockHist |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_stock_hist_producto_bodega` → `producto_bodega`
- `FK_stock_hist_producto_bodega_rec` → `producto_bodega`
- `FK_stock_hist_producto_estado` → `producto_estado`
- `FK_stock_hist_producto_estado_rec` → `producto_estado`
- `FK_stock_hist_producto_presentacion` → `producto_presentacion`
- `FK_stock_hist_producto_presentacion_rec` → `producto_presentacion`
- `FK_stock_hist_propietario_bodega` → `propietario_bodega`
- `FK_stock_hist_propietario_bodega_rec` → `propietario_bodega`
- `FK_stock_hist_trans_pe_enc` → `trans_pe_enc`
- `FK_stock_hist_trans_pe_enc_rec` → `trans_pe_enc`
- `FK_stock_hist_trans_re_det` → `trans_re_det`
- `FK_stock_hist_unidad_medida` → `unidad_medida`
- `FK_stock_hist_unidad_medida_rec` → `unidad_medida`

## Quién la referencia

**3** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

