---
id: db-brain-table-operador
type: db-table
title: dbo.operador
schema: dbo
name: operador
kind: table
rows: 18
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.operador`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 18 |
| Schema modify_date | 2024-02-01 |
| Columnas | 25 |
| Índices | 1 |
| FKs | out:2 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOperador` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdRolOperador` | `int` | ✓ |  |
| 4 | `IdJornada` | `int` | ✓ |  |
| 5 | `nombres` | `nvarchar(100)` | ✓ |  |
| 6 | `apellidos` | `nvarchar(100)` | ✓ |  |
| 7 | `direccion` | `nvarchar(50)` | ✓ |  |
| 8 | `telefono` | `nvarchar(50)` | ✓ |  |
| 9 | `codigo` | `nvarchar(25)` | ✓ |  |
| 10 | `clave` | `nvarchar(25)` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |
| 12 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 13 | `fec_agr` | `datetime` | ✓ |  |
| 14 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 15 | `fec_mod` | `datetime` | ✓ |  |
| 16 | `costo_hora` | `float` | ✓ |  |
| 17 | `usa_hh` | `bit` | ✓ |  |
| 18 | `foto` | `image` | ✓ |  |
| 19 | `recibe` | `bit` | ✓ |  |
| 20 | `ubica` | `bit` | ✓ |  |
| 21 | `transporta` | `bit` | ✓ |  |
| 22 | `pickea` | `bit` | ✓ |  |
| 23 | `verifica` | `bit` | ✓ |  |
| 24 | `montacarga` | `bit` |  |  |
| 25 | `sistema` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_operador_1` | CLUSTERED · **PK** | IdOperador |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_operador_empresa` → `empresa`
- `FK_operador_jornada_laboral` → `jornada_laboral`

### Entrantes (otra tabla → esta)

- `operador_bodega` (`FK_operador_bodega_operador`)
- `operador_jornada_laboral` (`FK_operador_jornada_laboral_operador`)

## Quién la referencia

**31** objetos:

- `asignar_jornada_laboral` (stored_procedure)
- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Conteo_By_Operador` (view)
- `VW_ControlCalidad_CambioEstado` (view)
- `vw_Indicador_Picking` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Inv_Conteo_Operador` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_N` (view)
- `VW_Operador` (view)
- `VW_Operador_Horario` (view)
- `VW_Packing` (view)
- `VW_PackingDespachado` (view)
- `VW_PickingUbic_By_IdPickingDet` (view)
- `VW_Productividad_Picking` (view)
- `VW_ProductoRellenado` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_REC_CON_OC` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Resoluciones_Operador` (view)
- `VW_Tareas_Operador` (view)
- `VW_Tiempos_Ingreso_Operador` (view)
- `VW_Tiempos_Picking_Operador` (view)
- `VW_TransUbicHhDet` (view)
- `VW_Ubicaciones_Inventario_Ciclico` (view)
- `VW_UbicacionPicking` (view)

