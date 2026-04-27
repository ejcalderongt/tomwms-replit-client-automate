---
id: db-brain-table-configuracion-alias-campos
type: db-table
title: dbo.configuracion_alias_campos
schema: dbo
name: configuracion_alias_campos
kind: table
rows: 0
modify_date: 2022-09-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.configuracion_alias_campos`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-09-29 |
| Columnas | 3 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdConfiguracionAlias` | `int` |  |  |
| 2 | `Nombre_WMS` | `nvarchar(50)` | ✓ |  |
| 3 | `Alias_WMS` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_configuracion_alias_campos` | CLUSTERED · **PK** | IdConfiguracionAlias |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

