---
id: db-brain-table-trans-picking-op
type: db-table
title: dbo.trans_picking_op
schema: dbo
name: trans_picking_op
kind: table
rows: 5895
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_picking_op`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 5.895 |
| Schema modify_date | 2024-02-01 |
| Columnas | 7 |
| Índices | 2 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOperadorPicking` | `int` |  |  |
| 2 | `IdPickingEnc` | `int` |  |  |
| 3 | `IdOperadorBodega` | `int` |  |  |
| 4 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_picking_op` | CLUSTERED · **PK** | IdOperadorPicking |
| `NCL_trans_picking_op_20231204` | NONCLUSTERED | IdOperadorBodega, IdPickingEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_picking_op_operador_bodega` → `operador_bodega`
- `FK_trans_picking_op_trans_picking_enc` → `trans_picking_enc`

## Quién la referencia

**8** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `sp_eliminar_by_Referencia` (stored_procedure)
- `VW_Tareas_Operador` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_UbicacionPicking` (view)

