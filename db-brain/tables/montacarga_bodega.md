---
id: db-brain-table-montacarga-bodega
type: db-table
title: dbo.montacarga_bodega
schema: dbo
name: montacarga_bodega
kind: table
rows: 8
modify_date: 2016-04-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.montacarga_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 8 |
| Schema modify_date | 2016-04-25 |
| Columnas | 8 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMontacargaBodega` | `int` |  |  |
| 2 | `IdMontacarga` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_montacarga_bodega` | CLUSTERED · **PK** | IdMontacargaBodega |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

