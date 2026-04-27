---
id: db-brain-table-tipo-tarea-tiempos
type: db-table
title: dbo.tipo_tarea_tiempos
schema: dbo
name: tipo_tarea_tiempos
kind: table
rows: 1
modify_date: 2019-07-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tipo_tarea_tiempos`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2019-07-29 |
| Columnas | 4 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmpresa` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdTipoTarea` | `int` |  |  |
| 4 | `TiempoMedioMinutos` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tipo_tarea_tiempos` | CLUSTERED · **PK** | IdEmpresa, IdBodega, IdTipoTarea |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

