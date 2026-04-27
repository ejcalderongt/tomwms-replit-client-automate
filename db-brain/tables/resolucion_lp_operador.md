---
id: db-brain-table-resolucion-lp-operador
type: db-table
title: dbo.resolucion_lp_operador
schema: dbo
name: resolucion_lp_operador
kind: table
rows: 67
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.resolucion_lp_operador`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 67 |
| Schema modify_date | 2021-05-28 |
| Columnas | 12 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idresolucionlp` | `int` |  |  |
| 2 | `idoperador` | `int` |  |  |
| 3 | `idbodega` | `int` | ✓ |  |
| 4 | `serie` | `nvarchar(50)` | ✓ |  |
| 5 | `correlativo_inicial` | `int` | ✓ |  |
| 6 | `correlativo_final` | `int` | ✓ |  |
| 7 | `correlativo_actual` | `int` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |
| 9 | `user_agr` | `nvarchar(25)` |  |  |
| 10 | `fec_agr` | `datetime` |  |  |
| 11 | `user_mod` | `nvarchar(25)` |  |  |
| 12 | `fec_mod` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_resolucion_lp_operador` | CLUSTERED · **PK** | idresolucionlp |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `VW_Resoluciones_Operador` (view)

