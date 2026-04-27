---
id: db-brain-table-tarifa-tipo-transaccion-det
type: db-table
title: dbo.tarifa_tipo_transaccion_det
schema: dbo
name: tarifa_tipo_transaccion_det
kind: table
rows: 0
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tarifa_tipo_transaccion_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-05-28 |
| Columnas | 2 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoTransaccion` | `int` |  |  |
| 2 | `IdServicio` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_transaccion_servicio_det` | CLUSTERED · **PK** | IdTipoTransaccion, IdServicio |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

