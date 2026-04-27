---
id: db-brain-table-i-nav-acuerdo-enc
type: db-table
title: dbo.i_nav_acuerdo_enc
schema: dbo
name: i_nav_acuerdo_enc
kind: table
rows: 0
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_acuerdo_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-08-25 |
| Columnas | 8 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdAcuerdo` | `int` |  |  |
| 2 | `IdCliente` | `int` | ✓ |  |
| 3 | `codigo_acuerdo` | `nvarchar(100)` |  |  |
| 4 | `descripcion` | `nvarchar(500)` |  |  |
| 5 | `tipo_cobro` | `nvarchar(1)` |  |  |
| 6 | `cod_moneda` | `int` |  |  |
| 7 | `nom_moneda` | `nvarchar(20)` |  |  |
| 8 | `procesado_wms` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_acuerdo_enc` | CLUSTERED · **PK** | IdAcuerdo |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

