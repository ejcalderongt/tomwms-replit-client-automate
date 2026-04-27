---
id: db-brain-table-trans-pe-enc
type: db-table
title: dbo.trans_pe_enc
schema: dbo
name: trans_pe_enc
kind: table
rows: 4202
modify_date: 2025-06-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_pe_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4.202 |
| Schema modify_date | 2025-06-11 |
| Columnas | 70 |
| Índices | 4 |
| FKs | out:4 in:11 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedidoEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `IdCliente` | `int` | ✓ |  |
| 4 | `IdMuelle` | `int` | ✓ |  |
| 5 | `IdPropietarioBodega` | `int` | ✓ |  |
| 6 | `IdTipoPedido` | `int` | ✓ |  |
| 7 | `IdPickingEnc` | `int` | ✓ |  |
| 8 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 9 | `hora_ini` | `datetime` | ✓ |  |
| 10 | `hora_fin` | `datetime` | ✓ |  |
| 11 | `ubicacion` | `nvarchar(35)` | ✓ |  |
| 12 | `estado` | `nvarchar(20)` | ✓ |  |
| 13 | `no_despacho` | `bigint` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |
| 17 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 18 | `fec_mod` | `datetime` | ✓ |  |
| 19 | `no_documento` | `bigint` | ✓ |  |
| 20 | `local` | `bit` | ✓ |  |
| 21 | `pallet_primero` | `bit` | ✓ |  |
| 22 | `dias_cliente` | `float` | ✓ |  |
| 23 | `anulado` | `bit` | ✓ |  |
| 24 | `RoadKilometraje` | `float` | ✓ |  |
| 25 | `RoadFechaEntr` | `datetime` | ✓ |  |
| 26 | `RoadDirEntrega` | `nvarchar(255)` | ✓ |  |
| 27 | `RoadTotal` | `float` | ✓ |  |
| 28 | `RoadDesMonto` | `float` | ✓ |  |
| 29 | `RoadImpMonto` | `float` | ✓ |  |
| 30 | `RoadPeso` | `float` | ✓ |  |
| 31 | `RoadBandera` | `nvarchar(5)` | ✓ |  |
| 32 | `RoadStatCom` | `nvarchar(1)` | ✓ |  |
| 33 | `RoadCalcoBJ` | `nvarchar(1)` | ✓ |  |
| 34 | `RoadImpres` | `int` | ✓ |  |
| 35 | `RoadADD1` | `nvarchar(5)` | ✓ |  |
| 36 | `RoadADD2` | `nvarchar(5)` | ✓ |  |
| 37 | `RoadADD3` | `nvarchar(35)` | ✓ |  |
| 38 | `RoadStatProc` | `nvarchar(3)` | ✓ |  |
| 39 | `RoadRechazado` | `bit` | ✓ |  |
| 40 | `RoadRazon_Rechazado` | `nvarchar(50)` | ✓ |  |
| 41 | `RoadInformado` | `bit` | ✓ |  |
| 42 | `RoadSucursal` | `nvarchar(10)` | ✓ |  |
| 43 | `RoadIdDespacho` | `int` | ✓ |  |
| 44 | `RoadIdFacturacion` | `int` | ✓ |  |
| 45 | `RoadIdRuta` | `int` | ✓ |  |
| 46 | `RoadIdVendedor` | `int` | ✓ |  |
| 47 | `RoadIdRutaDespacho` | `int` | ✓ |  |
| 48 | `RoadIdVendedorDespacho` | `int` | ✓ |  |
| 49 | `Observacion` | `nvarchar(255)` | ✓ |  |
| 50 | `PedidoRoad` | `bit` | ✓ |  |
| 51 | `HoraEntregaDesde` | `datetime` | ✓ |  |
| 52 | `HoraEntregaHasta` | `datetime` | ✓ |  |
| 53 | `referencia` | `nvarchar(25)` | ✓ |  |
| 54 | `IdMotivoAnulacionBodega` | `int` | ✓ |  |
| 55 | `Enviado_A_ERP` | `bit` | ✓ |  |
| 56 | `control_ultimo_lote` | `bit` | ✓ |  |
| 57 | `serie` | `nvarchar(25)` | ✓ |  |
| 58 | `correlativo` | `int` | ✓ |  |
| 59 | `Referencia_Documento_Ingreso_Bodega_Destino` | `nvarchar(50)` | ✓ |  |
| 60 | `sync_mi3` | `bit` | ✓ |  |
| 61 | `No_Picking_ERP` | `nvarchar(50)` | ✓ |  |
| 62 | `no_documento_externo` | `nvarchar(50)` | ✓ |  |
| 63 | `requiere_tarimas` | `bit` | ✓ |  |
| 65 | `fecha_preparacion` | `date` | ✓ |  |
| 66 | `IdTipoManufactura` | `int` | ✓ |  |
| 67 | `bodega_origen` | `nvarchar(50)` | ✓ |  |
| 68 | `bodega_destino` | `nvarchar(50)` | ✓ |  |
| 69 | `IdMotivoDevolucion` | `int` | ✓ |  |
| 70 | `Codigo_Empresa_ERP` | `nvarchar(50)` | ✓ |  |
| 71 | `EsExportacion` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_pedido_enc` | CLUSTERED · **PK** | IdPedidoEnc |
| `NCLI_trans_pe_enc_20220322_EJC` | NONCLUSTERED | referencia, no_documento, IdBodega, IdCliente, IdPropietarioBodega |
| `NCLI_Trans_Pe_Enc_202210051623_EJC` | NONCLUSTERED | IdPickingEnc |
| `NCI_trans_pe_det_CKFK250309` | NONCLUSTERED | bodega_destino, IdTipoPedido |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_pedido_enc_bodega` → `bodega`
- `FK_trans_pedido_enc_bodega_muelles` → `bodega_muelles`
- `FK_trans_pedido_enc_cliente` → `cliente`
- `FK_trans_pedido_enc_propietario_bodega` → `propietario_bodega`

### Entrantes (otra tabla → esta)

- `stock_hist` (`FK_stock_hist_trans_pe_enc`)
- `stock_hist` (`FK_stock_hist_trans_pe_enc_rec`)
- `stock_jornada` (`FK_stock_jornada_trans_pe_enc`)
- `stock_jornada` (`FK_stock_jornada_trans_pe_enc_rec`)
- `stock` (`FK_stock_trans_pe_enc`)
- `stock` (`FK_stock_trans_pe_enc_rec`)
- `trans_manufactura_enc` (`FK_trans_manufactura_enc_trans_pe_enc`)
- `trans_pe_det` (`FK_trans_pedido_det_trans_pedido_enc`)
- `trans_picking_img` (`FK_trans_picking_img_trans_pe_enc`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_trans_pe_enc`)
- `trans_ubic_hh_stock` (`FK_stock_ubic_hh_trans_pe_enc_rec`)

## Quién la referencia

**38** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `sp_eliminar_by_Referencia` (stored_procedure)
- `v_trans_pedido` (view)
- `VW_Cantidad_Pedidos_vrs_Despacho_Clientes` (view)
- `VW_Despacho_Detalle` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Estado_Envios_Nav` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_Single_Pedido` (view)
- `vw_Indicador_Despachos` (view)
- `vw_Indicador_Picking` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_Lotes_Despacho` (view)
- `VW_Movimientos_N1` (view)
- `VW_PackingDespachado` (view)
- `VW_PE_CON_DIFERENCIAS` (view)
- `VW_Pedido` (view)
- `VW_Pedidos_IdPickingEnc` (view)
- `VW_Pedidos_List` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_PickingUbicacion` (view)
- `VW_Productividad_Picking` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Stock_Res_Pedido` (view)
- `VW_Tareas_Activas_HH` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_Tiempos_Despacho_By_IdPedidoEnc` (view)
- `VW_Tiempos_Picking_Operador` (view)
- `VW_UbicacionPicking` (view)

