---
id: db-brain-table-i-nav-acuerdo
type: db-table
title: dbo.i_nav_acuerdo
schema: dbo
name: i_nav_acuerdo
kind: table
rows: 0
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_acuerdo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-08-25 |
| Columnas | 6 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdAcuerdo` | `int` |  |  |
| 2 | `codigo_acuerdo` | `nvarchar(50)` |  |  |
| 3 | `descripcion` | `nvarchar(500)` |  |  |
| 4 | `tipo_cobro` | `nvarchar(1)` |  |  |
| 5 | `cod_moneda` | `int` |  |  |
| 6 | `nom_moneda` | `nvarchar(20)` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_acuerdo` | CLUSTERED · **PK** | IdAcuerdo |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

