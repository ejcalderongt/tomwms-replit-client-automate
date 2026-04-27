---
id: db-brain-table-impresion-productos-barras
type: db-table
title: dbo.impresion_productos_barras
schema: dbo
name: impresion_productos_barras
kind: table
rows: 4
modify_date: 2019-11-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.impresion_productos_barras`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4 |
| Schema modify_date | 2019-11-18 |
| Columnas | 21 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoBarra` | `int` |  |  |
| 2 | `codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `nombre` | `nvarchar(100)` | ✓ |  |
| 4 | `codigo_barra` | `nvarchar(100)` | ✓ |  |
| 5 | `cantidad_impresiones` | `int` | ✓ |  |
| 6 | `IdPresentacion` | `int` | ✓ |  |
| 7 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 8 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 9 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 10 | `Camas_Por_Tarima` | `int` | ✓ |  |
| 11 | `Cajas_Por_Cama` | `int` | ✓ |  |
| 12 | `Cantidad_Presentacion` | `float` | ✓ |  |
| 13 | `Factor` | `float` | ✓ |  |
| 14 | `Lote` | `nvarchar(50)` | ✓ |  |
| 15 | `Fecha_Ingreso` | `date` | ✓ |  |
| 16 | `Fecha_Vence` | `date` | ✓ |  |
| 17 | `fecha_agr` | `datetime` | ✓ |  |
| 18 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 19 | `impreso` | `bit` | ✓ |  |
| 20 | `activo` | `bit` | ✓ |  |
| 21 | `IdImpresora` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_Impresion_productos_barras` | CLUSTERED · **PK** | IdProductoBarra |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

