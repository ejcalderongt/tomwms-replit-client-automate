---
id: db-brain-table-log-importacion-excel
type: db-table
title: dbo.log_importacion_excel
schema: dbo
name: log_importacion_excel
kind: table
rows: 0
modify_date: 2022-05-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.log_importacion_excel`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-05-06 |
| Columnas | 6 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdImportacion` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `IdUsuario` | `int` | ✓ |  |
| 5 | `hash_archivo` | `nvarchar(150)` | ✓ |  |
| 6 | `fecha` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_log_importacion_excel` | CLUSTERED · **PK** | IdImportacion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

