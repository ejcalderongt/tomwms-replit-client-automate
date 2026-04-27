---
id: db-brain-table-i-nav-acuerdo-det
type: db-table
title: dbo.i_nav_acuerdo_det
schema: dbo
name: i_nav_acuerdo_det
kind: table
rows: 0
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_acuerdo_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-08-25 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdContrato` | `int` |  |  |
| 2 | `IdServicio` | `int` |  |  |
| 3 | `correlativo` | `int` |  |  |
| 4 | `descripcion` | `nvarchar(200)` |  |  |
| 5 | `numero_unidades` | `numeric` |  |  |
| 6 | `dias_eventos` | `int` |  |  |
| 7 | `corre_cbcatalogoproductos` | `int` |  |  |
| 8 | `servicio` | `nvarchar(500)` | ✓ |  |
| 9 | `nombre_unidad` | `nvarchar(100)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_acuerdo_det` | CLUSTERED · **PK** | IdContrato, IdServicio |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

