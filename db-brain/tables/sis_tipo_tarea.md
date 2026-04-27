---
id: db-brain-table-sis-tipo-tarea
type: db-table
title: dbo.sis_tipo_tarea
schema: dbo
name: sis_tipo_tarea
kind: table
rows: 35
modify_date: 2016-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.sis_tipo_tarea`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 35 |
| Schema modify_date | 2016-09-12 |
| Columnas | 3 |
| Índices | 1 |
| FKs | out:0 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoTarea` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `Contabilizar` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_sis_tipo_tarea_hh` | CLUSTERED · **PK** | IdTipoTarea |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `menu_sistema_op` (`FK_menu_sistema_op_sis_tipo_tarea`)
- `tarea_hh` (`FK_tarea_hh_sis_tipo_tarea`)
- `trans_movimientos` (`FK_trans_movimientos_sis_tipo_tarea_hh`)

## Quién la referencia

**14** objetos:

- `CLBD` (stored_procedure)
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
- `VW_Tareas_Activas_HH` (view)
- `VW_tareas_hh` (view)
- `VW_Tareas_Operador` (view)

