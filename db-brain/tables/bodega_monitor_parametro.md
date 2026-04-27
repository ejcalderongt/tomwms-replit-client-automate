---
id: db-brain-table-bodega-monitor-parametro
type: db-table
title: dbo.bodega_monitor_parametro
schema: dbo
name: bodega_monitor_parametro
kind: table
rows: 0
modify_date: 2016-08-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.bodega_monitor_parametro`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-08-01 |
| Columnas | 4 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMonitor` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `TiempoActualizacion` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_bodega_monitor_parametro` | CLUSTERED · **PK** | IdMonitor |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_bodega_monitor_parametro_bodega` → `bodega`

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

