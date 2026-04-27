---
id: db-brain-table-trans-ubic-hh-op
type: db-table
title: dbo.trans_ubic_hh_op
schema: dbo
name: trans_ubic_hh_op
kind: table
rows: 145
modify_date: 2018-01-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_ubic_hh_op`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 145 |
| Schema modify_date | 2018-01-19 |
| Columnas | 7 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTransUbicHhOp` | `int` |  |  |
| 2 | `IdTareaUbicacionEnc` | `int` |  |  |
| 3 | `IdOperadorBodega` | `int` |  |  |
| 4 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_ubic_hh_op` | CLUSTERED · **PK** | IdTransUbicHhOp, IdTareaUbicacionEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_ubic_hh_op_operador_bodega` → `operador_bodega`

## Quién la referencia

**7** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `VW_Cantidad_Tareas_Ubicacion_Op` (view)
- `VW_Cantidad_Tareas_Ubicacion_Op_Items` (view)
- `VW_ControlCalidad_CambioEstado` (view)
- `VW_TransUbicHhDet` (view)

