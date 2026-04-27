---
id: db-brain-table-font-det
type: db-table
title: dbo.font_det
schema: dbo
name: font_det
kind: table
rows: 1
modify_date: 2017-08-08
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.font_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2017-08-08 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdFontDet` | `int` |  |  |
| 2 | `IdFontEnc` | `int` | ✓ |  |
| 3 | `Letra` | `nvarchar(50)` | ✓ |  |
| 4 | `Tamaño` | `float` | ✓ |  |
| 5 | `Negrita` | `bit` | ✓ |  |
| 6 | `Cursiva` | `bit` | ✓ |  |
| 7 | `Subrayado` | `bit` | ✓ |  |
| 8 | `ColorFont` | `nvarchar(50)` | ✓ |  |
| 9 | `ColorFondo` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_font_det` | CLUSTERED · **PK** | IdFontDet |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

