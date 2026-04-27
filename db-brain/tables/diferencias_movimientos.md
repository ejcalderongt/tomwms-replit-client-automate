---
id: db-brain-table-diferencias-movimientos
type: db-table
title: dbo.diferencias_movimientos
schema: dbo
name: diferencias_movimientos
kind: table
rows: 0
modify_date: 2020-10-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.diferencias_movimientos`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-10-07 |
| Columnas | 19 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDiferencia` | `int` |  |  |
| 2 | `IdProductoBodega` | `int` | ✓ |  |
| 3 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 4 | `Nombre` | `nvarchar(150)` | ✓ |  |
| 5 | `Lote` | `nvarchar(50)` | ✓ |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `Estado` | `nvarchar(50)` | ✓ |  |
| 8 | `FechaVence` | `date` | ✓ |  |
| 9 | `InventarioInicial` | `float` | ✓ |  |
| 10 | `Ingresos` | `float` | ✓ |  |
| 11 | `AjustesPositivos` | `float` | ✓ |  |
| 12 | `AjustesNegativos` | `float` | ✓ |  |
| 13 | `Salidas` | `float` | ✓ |  |
| 14 | `ExistenciaAl` | `float` | ✓ |  |
| 15 | `ExistenciaActual` | `float` | ✓ |  |
| 16 | `ExistenciaSinEstado` | `float` | ✓ |  |
| 17 | `FechaGen` | `date` | ✓ |  |
| 18 | `Diferencia` | `float` | ✓ |  |
| 19 | `FaltaStock` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_diferencias_movimientos` | CLUSTERED · **PK** | IdDiferencia |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

