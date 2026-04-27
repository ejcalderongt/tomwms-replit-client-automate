---
id: db-brain-table-trans-pe-det
type: db-table
title: dbo.trans_pe_det
schema: dbo
name: trans_pe_det
kind: table
rows: 14819
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_pe_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 14.819 |
| Schema modify_date | 2024-07-02 |
| Columnas | 44 |
| Índices | 5 |
| FKs | out:4 in:4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedidoDet` | `int` |  |  |
| 2 | `IdPedidoEnc` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdEstado` | `int` |  |  |
| 5 | `IdPresentacion` | `int` | ✓ |  |
| 6 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 7 | `Cantidad` | `float` | ✓ |  |
| 8 | `Peso` | `float` | ✓ |  |
| 9 | `Precio` | `float` | ✓ |  |
| 10 | `no_recepcion` | `bigint` | ✓ |  |
| 11 | `ndias` | `int` | ✓ |  |
| 12 | `cant_despachada` | `float` | ✓ |  |
| 13 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 14 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 15 | `nom_presentacion` | `nvarchar(50)` | ✓ |  |
| 16 | `nom_unid_med` | `nvarchar(50)` | ✓ |  |
| 17 | `nom_estado` | `nvarchar(50)` | ✓ |  |
| 18 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 19 | `fec_agr` | `datetime` | ✓ |  |
| 20 | `fecha_especifica` | `bit` | ✓ |  |
| 21 | `RoadDes` | `float` | ✓ |  |
| 22 | `RoadDesMon` | `float` | ✓ |  |
| 23 | `RoadTotal` | `float` | ✓ |  |
| 24 | `RoadPrecioDoc` | `float` | ✓ |  |
| 25 | `RoadVAL1` | `float` | ✓ |  |
| 26 | `RoadVAL2` | `nvarchar(50)` | ✓ |  |
| 27 | `RoadCantProc` | `float` | ✓ |  |
| 28 | `peso_despachado` | `float` | ✓ |  |
| 29 | `no_linea` | `int` | ✓ |  |
| 30 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 31 | `IdStockEspecifico` | `int` | ✓ |  |
| 33 | `EsPadre` | `bit` | ✓ |  |
| 34 | `IdPedidoDetPadre` | `int` | ✓ |  |
| 35 | `Peso_Bruto` | `float` | ✓ |  |
| 36 | `Peso_Neto` | `float` | ✓ |  |
| 37 | `Costo` | `float` | ✓ |  |
| 38 | `valor_aduana` | `float` | ✓ |  |
| 39 | `valor_fob` | `float` | ✓ |  |
| 40 | `valor_iva` | `float` | ✓ |  |
| 41 | `valor_dai` | `float` | ✓ |  |
| 42 | `valor_seguro` | `float` | ✓ |  |
| 43 | `valor_flete` | `float` | ✓ |  |
| 44 | `Total_linea` | `float` | ✓ |  |
| 45 | `IdCliente` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_pedido_det` | CLUSTERED · **PK** | IdPedidoDet |
| `NonClusteredIndex-20190903-100646` | NONCLUSTERED | IdPedidoDet, IdPedidoEnc, IdProductoBodega, IdEstado, IdPresentacion, IdUnidadMedidaBasica, Cantidad |
| `noncltrans_pe_det` | NONCLUSTERED | IdPedidoDet, IdPedidoEnc, IdProductoBodega, IdPresentacion, IdUnidadMedidaBasica, Cantidad, IdEstado |
| `NCLI_Trans_Pe_Det_20210923_EJC` | NONCLUSTERED | IdPedidoDet, IdPedidoEnc |
| `NCLI_VW_PickingUbic_20220411_EJC` | NONCLUSTERED | IdPedidoEnc, IdProductoBodega, IdEstado, IdPresentacion, IdUnidadMedidaBasica |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_pedido_det_producto_bodega` → `producto_bodega`
- `FK_trans_pedido_det_producto_presentacion` → `producto_presentacion`
- `FK_trans_pedido_det_trans_pedido_enc` → `trans_pe_enc`
- `FK_trans_pedido_det_unidad_medida` → `unidad_medida`

### Entrantes (otra tabla → esta)

- `trans_manufactura_det` (`FK_trans_manufactura_det_trans_pe_det`)
- `trans_manufactura_picking` (`FK_trans_manufactura_picking_trans_pe_det`)
- `trans_picking_det` (`FK_trans_picking_det_trans_pedido_det`)
- `trans_picking_img` (`FK_trans_picking_img_trans_pe_det`)

## Quién la referencia

**27** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `sp_eliminar_by_Referencia` (stored_procedure)
- `VW_Despacho_Detalle` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Get_Pedido_Det` (view)
- `VW_Lotes_Despacho` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_PE_CON_DIFERENCIAS` (view)
- `VW_Pedido` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_PickingUbic_By_IdPedidoDet` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_PickingUbicacion` (view)
- `VW_Productividad_Picking` (view)
- `VW_Stock_Reservado_By_IdPedidoEnc` (view)
- `VW_Tiempos_Picking_Operador` (view)
- `VW_UbicacionPicking` (view)
- `VW_Verificacion` (view)
- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Consolidada_LFV` (view)
- `VW_Verificacion_Detallado_Sin_Licencia` (view)

