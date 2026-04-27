---
id: db-brain-table-tarea-hh
type: db-table
title: dbo.tarea_hh
schema: dbo
name: tarea_hh
kind: table
rows: 1817
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tarea_hh`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.817 |
| Schema modify_date | 2025-02-11 |
| Columnas | 18 |
| Índices | 1 |
| FKs | out:6 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareahh` | `int` |  |  |
| 2 | `IdPropietario` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `IdMuelle` | `int` | ✓ |  |
| 5 | `IdEstado` | `int` | ✓ |  |
| 6 | `IdPrioridad` | `int` | ✓ |  |
| 7 | `IdTipoTarea` | `int` | ✓ |  |
| 8 | `IdTransaccion` | `int` | ✓ |  |
| 9 | `Tipo` | `int` | ✓ |  |
| 10 | `FechaInicio` | `datetime` | ✓ |  |
| 11 | `FechaFin` | `datetime` | ✓ |  |
| 12 | `DiaCompleto` | `bit` | ✓ |  |
| 13 | `Asunto` | `nvarchar(50)` | ✓ |  |
| 14 | `Ubicacion` | `nvarchar(50)` | ✓ |  |
| 15 | `Descripcion` | `nvarchar(50)` | ✓ |  |
| 16 | `Recordatorio` | `nvarchar(max)` | ✓ |  |
| 17 | `IdOperadorBodega_Cerro` | `int` | ✓ |  |
| 18 | `Host_Cerro` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tarea_hh` | CLUSTERED · **PK** | IdTareahh |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_tarea_hh_bodega` → `bodega`
- `FK_tarea_hh_bodega_muelles` → `bodega_muelles`
- `FK_tarea_hh_propietarios` → `propietarios`
- `FK_tarea_hh_sis_estado_tarea_hh` → `sis_estado_tarea_hh`
- `FK_tarea_hh_sis_prioridad_tarea_hh` → `sis_prioridad_tarea_hh`
- `FK_tarea_hh_sis_tipo_tarea` → `sis_tipo_tarea`

## Quién la referencia

**7** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Tareas_Activas_HH` (view)
- `VW_tareas_hh` (view)
- `VW_Tareas_Operador` (view)

