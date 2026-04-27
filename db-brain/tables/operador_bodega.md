---
id: db-brain-table-operador-bodega
type: db-table
title: dbo.operador_bodega
schema: dbo
name: operador_bodega
kind: table
rows: 84
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.operador_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 84 |
| Schema modify_date | 2021-05-28 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:2 in:8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOperadorBodega` | `int` |  |  |
| 2 | `IdOperador` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_operador_bodega` | CLUSTERED · **PK** | IdOperadorBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_operador_bodega_bodega` → `bodega`
- `FK_operador_bodega_operador` → `operador`

### Entrantes (otra tabla → esta)

- `producto_pallet` (`FK_producto_pallet_operador_bodega`)
- `trans_picking_det` (`FK_trans_picking_det_operador_bodega`)
- `trans_picking_op` (`FK_trans_picking_op_operador_bodega`)
- `trans_re_det` (`FK_trans_re_det_operador_bodega`)
- `trans_re_op` (`FK_trans_re_op_operador_bodega`)
- `trans_tras_op` (`FK_trans_tras_op_operador_bodega`)
- `trans_ubic_hh_det` (`FK_trans_ubic_hh_det_operador_bodega`)
- `trans_ubic_hh_op` (`FK_trans_ubic_hh_op_operador_bodega`)

## Quién la referencia

**25** objetos:

- `asignar_jornada_laboral` (stored_procedure)
- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_ControlCalidad_CambioEstado` (view)
- `vw_Indicador_Picking` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_N` (view)
- `VW_Operador_Horario` (view)
- `VW_Packing` (view)
- `VW_PackingDespachado` (view)
- `VW_PickingUbic_By_IdPickingDet` (view)
- `VW_Productividad_Picking` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_REC_CON_OC` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Tareas_Operador` (view)
- `VW_Tiempos_Ingreso_Operador` (view)
- `VW_Tiempos_Picking_Operador` (view)
- `VW_TransUbicHhDet` (view)
- `VW_UbicacionPicking` (view)

