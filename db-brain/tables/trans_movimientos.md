---
id: db-brain-table-trans-movimientos
type: db-table
title: dbo.trans_movimientos
schema: dbo
name: trans_movimientos
kind: table
rows: 81641
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_movimientos`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 81.641 |
| Schema modify_date | 2024-07-02 |
| Columnas | 35 |
| Índices | 6 |
| FKs | out:8 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMovimiento` | `int` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 3 | `IdBodegaOrigen` | `int` |  |  |
| 4 | `IdTransaccion` | `int` |  |  |
| 5 | `IdPropietarioBodega` | `int` | ✓ |  |
| 6 | `IdProductoBodega` | `int` | ✓ |  |
| 7 | `IdUbicacionOrigen` | `int` | ✓ |  |
| 8 | `IdUbicacionDestino` | `int` | ✓ |  |
| 9 | `IdPresentacion` | `int` | ✓ |  |
| 10 | `IdEstadoOrigen` | `int` | ✓ |  |
| 11 | `IdEstadoDestino` | `int` | ✓ |  |
| 12 | `IdUnidadMedida` | `int` | ✓ |  |
| 13 | `IdTipoTarea` | `int` | ✓ |  |
| 14 | `IdBodegaDestino` | `int` | ✓ |  |
| 15 | `IdRecepcion` | `int` | ✓ |  |
| 16 | `cantidad` | `float` | ✓ |  |
| 17 | `serie` | `nvarchar(50)` | ✓ |  |
| 18 | `peso` | `float` | ✓ |  |
| 19 | `lote` | `nvarchar(50)` | ✓ |  |
| 20 | `fecha_vence` | `datetime` | ✓ |  |
| 21 | `fecha` | `datetime` | ✓ |  |
| 22 | `barra_pallet` | `nvarchar(50)` | ✓ |  |
| 23 | `hora_ini` | `datetime` | ✓ |  |
| 24 | `hora_fin` | `datetime` | ✓ |  |
| 25 | `fecha_agr` | `datetime` | ✓ |  |
| 26 | `usuario_agr` | `nvarchar(25)` | ✓ |  |
| 27 | `cantidad_hist` | `float` | ✓ |  |
| 28 | `peso_hist` | `float` | ✓ |  |
| 29 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 31 | `IdOperadorBodega` | `int` | ✓ |  |
| 32 | `IdRecepcionDet` | `int` | ✓ |  |
| 33 | `IdPedidoEnc` | `int` | ✓ |  |
| 34 | `IdPedidoDet` | `int` | ✓ |  |
| 35 | `IdDespachoEnc` | `int` | ✓ |  |
| 36 | `IdDespachoDet` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_movimientos` | CLUSTERED · **PK** | IdEmpresa, IdBodegaOrigen, IdTransaccion, IdMovimiento |
| `NCLI_TransMovimientos_20191211_EJC` | NONCLUSTERED | IdTipoTarea, IdUnidadMedida, IdEstadoDestino, IdEstadoOrigen, IdPresentacion, IdUbicacionDestino, IdUbicacionOrigen, IdPropietarioBodega, IdBodegaOrigen, cantidad, peso, lote, fecha_vence, fecha, barra_pallet, IdProductoBodega, IdBodegaDestino |
| `IX_trans_movimientos` | NONCLUSTERED | IdProductoBodega, IdUbicacionOrigen, IdUbicacionDestino, IdPresentacion, IdEstadoOrigen, IdEstadoDestino, IdUnidadMedida, IdTipoTarea, IdBodegaDestino, IdRecepcion, cantidad, peso, lote, fecha_vence, fecha, barra_pallet, IdPropietarioBodega |
| `NCLI_Trans_Mov_20240122_EJC` | NONCLUSTERED | IdOperadorBodega, IdPedidoEnc, IdTipoTarea |
| `NCL_trans_movimientos_20240306` | NONCLUSTERED | IdProductoBodega, IdUbicacionOrigen, IdUbicacionDestino, IdPresentacion, IdEstadoOrigen, IdEstadoDestino, IdUnidadMedida, IdTipoTarea, IdBodegaDestino, IdRecepcion, cantidad, peso, lote, fecha_vence, fecha, barra_pallet, IdOperadorBodega, IdDespachoEnc, IdBodegaOrigen, IdPropietarioBodega |
| `NCL_EJC20240524_trans_movimientos` | NONCLUSTERED | IdPropietarioBodega, IdProductoBodega, IdUbicacionOrigen, IdUbicacionDestino, IdPresentacion, IdEstadoOrigen, IdEstadoDestino, IdUnidadMedida, IdBodegaDestino, IdRecepcion, cantidad, serie, peso, lote, fecha_vence, fecha, barra_pallet, hora_ini, hora_fin, fecha_agr, usuario_agr, cantidad_hist, peso_hist, lic_plate, IdOperadorBodega, IdRecepcionDet, IdPedidoEnc, IdPedidoDet, IdDespachoEnc, IdDespachoDet, IdTransaccion, IdTipoTarea |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_movimientos_bodega` → `bodega`
- `FK_trans_movimientos_producto_bodega` → `producto_bodega`
- `FK_trans_movimientos_producto_estado` → `producto_estado`
- `FK_trans_movimientos_producto_estado1` → `producto_estado`
- `FK_trans_movimientos_producto_presentacion` → `producto_presentacion`
- `FK_trans_movimientos_propietario_bodega` → `propietario_bodega`
- `FK_trans_movimientos_sis_tipo_tarea_hh` → `sis_tipo_tarea`
- `FK_trans_movimientos_unidad_medida` → `unidad_medida`

## Quién la referencia

**14** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_20211205` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_FIX` (view)
- `VW_Movimientos_N` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_MovimientosDetalle` (view)

