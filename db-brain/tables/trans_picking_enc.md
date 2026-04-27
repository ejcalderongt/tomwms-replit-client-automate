---
id: db-brain-table-trans-picking-enc
type: db-table
title: dbo.trans_picking_enc
schema: dbo
name: trans_picking_enc
kind: table
rows: 1293
modify_date: 2025-06-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_picking_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.293 |
| Schema modify_date | 2025-06-25 |
| Columnas | 27 |
| Índices | 3 |
| FKs | out:2 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` | ✓ |  |
| 4 | `IdUbicacionPicking` | `int` | ✓ |  |
| 5 | `fecha_picking` | `datetime` | ✓ |  |
| 6 | `hora_ini` | `datetime` | ✓ |  |
| 7 | `hora_fin` | `datetime` | ✓ |  |
| 8 | `estado` | `nvarchar(20)` | ✓ |  |
| 9 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 10 | `fec_agr` | `datetime` | ✓ |  |
| 11 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 12 | `fec_mod` | `datetime` | ✓ |  |
| 13 | `detalle_operador` | `bit` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `verifica_auto` | `bit` | ✓ |  |
| 16 | `procesado_bof` | `bit` | ✓ |  |
| 17 | `requiere_preparacion` | `bit` | ✓ |  |
| 18 | `tipo_preparacion` | `nvarchar(50)` | ✓ |  |
| 19 | `estado_preparacion` | `nvarchar(50)` | ✓ |  |
| 20 | `fecha_inicio_preparacion` | `datetime` | ✓ |  |
| 21 | `fecha_fin_preparacion` | `datetime` | ✓ |  |
| 22 | `referencia` | `varchar(50)` | ✓ |  |
| 23 | `fotografia_verificacion` | `bit` |  |  |
| 24 | `IdBodegaMuelle` | `int` | ✓ |  |
| 25 | `IdPrioridadPicking` | `int` | ✓ |  |
| 26 | `IdTipoPicking` | `int` | ✓ |  |
| 28 | `Observacion` | `nvarchar(3000)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_picking_enc` | CLUSTERED · **PK** | IdPickingEnc |
| `NCLI_trans_picking_enc_20211124_EJC` | NONCLUSTERED | IdPickingEnc, IdUbicacionPicking, fecha_picking, hora_ini, hora_fin, estado, detalle_operador, IdBodega, IdPropietarioBodega, activo |
| `NCLI_Trans_Picking_Enc_20240609_EJC` | NONCLUSTERED | IdBodega, IdUbicacionPicking, fecha_picking, hora_ini, hora_fin, estado, detalle_operador, activo, procesado_bof, IdBodegaMuelle, IdPropietarioBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_picking_enc_bodega` → `bodega`
- `FK_trans_picking_enc_propietario_bodega` → `propietario_bodega`

### Entrantes (otra tabla → esta)

- `trans_picking_det` (`FK_trans_picking_det_trans_picking_enc`)
- `trans_picking_img` (`FK_trans_picking_img_trans_picking_enc`)
- `trans_picking_op` (`FK_trans_picking_op_trans_picking_enc`)

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
- `vw_Indicador_Picking` (view)
- `VW_Indicador_Picking_Detalle` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_Pedido` (view)
- `VW_Pedidos_List` (view)
- `VW_Picking` (view)
- `VW_PickingUbic_By_IdPickingDet` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_PickingUbicacion` (view)
- `VW_Productividad_Picking` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_Tareas_Activas_HH` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_Tiempos_Picking_Operador` (view)
- `VW_UbicacionPicking` (view)
- `VW_Verificacion` (view)
- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Consolidada_LFV` (view)
- `VW_Verificacion_Detallado_Sin_Licencia` (view)

