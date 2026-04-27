---
id: db-brain-table-unidad-medida-conversion
type: db-table
title: dbo.unidad_medida_conversion
schema: dbo
name: unidad_medida_conversion
kind: table
rows: 0
modify_date: 2017-11-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.unidad_medida_conversion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-11-16 |
| Columnas | 5 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdConversion` | `int` |  |  |
| 2 | `IdUnidadMedidaOrigen` | `int` | ✓ |  |
| 3 | `IdUnidadMedidaDestino` | `int` | ✓ |  |
| 4 | `Factor` | `float` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_conversion_unidad_medida` | CLUSTERED · **PK** | IdConversion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

