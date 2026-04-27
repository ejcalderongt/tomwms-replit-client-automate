---
id: db-brain-table-tarifa-tipo-transaccion
type: db-table
title: dbo.tarifa_tipo_transaccion
schema: dbo
name: tarifa_tipo_transaccion
kind: table
rows: 0
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tarifa_tipo_transaccion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-05-28 |
| Columnas | 4 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoTransaccion` | `int` |  |  |
| 2 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `Activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_transaccion_tipo` | CLUSTERED · **PK** | IdTipoTransaccion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

