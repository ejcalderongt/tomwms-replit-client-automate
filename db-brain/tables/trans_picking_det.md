---
id: db-brain-table-trans-picking-det
type: db-table
title: dbo.trans_picking_det
schema: dbo
name: trans_picking_det
kind: table
rows: 14585
modify_date: 2023-10-30
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_picking_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 14.585 |
| Schema modify_date | 2023-10-30 |
| Columnas | 15 |
| Índices | 5 |
| FKs | out:3 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingDet` | `int` |  |  |
| 2 | `IdPickingEnc` | `int` |  |  |
| 3 | `IdPedidoEnc` | `int` | ✓ |  |
| 4 | `IdPedidoDet` | `int` |  |  |
| 5 | `IdOperadorBodega` | `int` | ✓ |  |
| 6 | `cantidad` | `float` | ✓ |  |
| 7 | `cliente_dias` | `int` | ✓ |  |
| 8 | `cantidad_recibida` | `float` | ✓ |  |
| 9 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 12 | `fec_mod` | `datetime` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `codigo` | `nvarchar(25)` | ✓ |  |
| 15 | `nombre` | `nvarchar(100)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_picking_det` | CLUSTERED · **PK** | IdPickingDet |
| `NCLI_trans_picking_det_20211124` | NONCLUSTERED | IdPedidoDet, IdPickingEnc |
| `NCLI_trans_picking_det_EJC` | NONCLUSTERED | IdPedidoDet, IdPickingEnc |
| `NCLI_Trans_Picking_Det_EJC_20220403` | NONCLUSTERED | IdPedidoDet |
| `NCLI_Trans_Picking_Det_202210051623_EJC` | NONCLUSTERED | IdPickingEnc, IdPedidoDet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_picking_det_operador_bodega` → `operador_bodega`
- `FK_trans_picking_det_trans_pedido_det` → `trans_pe_det`
- `FK_trans_picking_det_trans_picking_enc` → `trans_picking_enc`

### Entrantes (otra tabla → esta)

- `trans_picking_det_parametros` (`FK_trans_picking_det_parametros_trans_picking_det`)
- `trans_picking_img` (`FK_trans_picking_img_trans_picking_det`)
- `trans_picking_ubic` (`FK_trans_picking_ubic_trans_picking_det`)

## Quién la referencia

**27** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `sp_eliminar_by_Referencia` (stored_procedure)
- `VW_Despacho_Detalle` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Indicador_Picking_Detalle` (view)
- `VW_Pedido` (view)
- `VW_Picking` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_PickingUbic_By_IdPedidoDet` (view)
- `VW_PickingUbic_By_IdPickingDet` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_PickingUbicacion` (view)
- `VW_Tareas_Activas_HH` (view)
- `VW_Ubicaciones_Picking_Resumido` (view)
- `VW_UbicacionPicking` (view)
- `VW_Verificacion` (view)
- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Consolidada_LFV` (view)
- `VW_Verificacion_Detallado_Sin_Licencia` (view)

