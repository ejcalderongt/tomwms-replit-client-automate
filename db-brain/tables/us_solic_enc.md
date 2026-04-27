---
id: db-brain-table-us-solic-enc
type: db-table
title: dbo.us_solic_enc
schema: dbo
name: us_solic_enc
kind: table
rows: 0
modify_date: 2020-10-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.us_solic_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-10-07 |
| Columnas | 11 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdSugerenciaSolicEnc` | `int` |  |  |
| 2 | `IdProducto` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `IdProductoBodega` | `int` | ✓ |  |
| 5 | `IdOperadorBodega` | `int` | ✓ |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `Lote` | `nvarchar(50)` | ✓ |  |
| 8 | `Fecha_Vence` | `date` | ✓ |  |
| 9 | `IdUmBas` | `int` | ✓ |  |
| 10 | `IdPresentacion` | `int` | ✓ |  |
| 11 | `Fecha` | `date` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_us_solic_enc` | CLUSTERED · **PK** | IdSugerenciaSolicEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

