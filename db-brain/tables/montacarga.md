---
id: db-brain-table-montacarga
type: db-table
title: dbo.montacarga
schema: dbo
name: montacarga
kind: table
rows: 2
modify_date: 2022-06-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.montacarga`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 2 |
| Schema modify_date | 2022-06-13 |
| Columnas | 15 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMontacarga` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `Modelo` | `nvarchar(50)` | ✓ |  |
| 5 | `Serie` | `nvarchar(50)` | ✓ |  |
| 6 | `capacidad_basica` | `float` | ✓ |  |
| 7 | `desplazamiento_motor` | `float` | ✓ |  |
| 8 | `tipo_combustible` | `nvarchar(50)` | ✓ |  |
| 9 | `tipo_montacarga` | `nvarchar(50)` | ✓ |  |
| 10 | `fecha_compra` | `datetime` | ✓ |  |
| 11 | `fecha_inicio_operaciones` | `datetime` | ✓ |  |
| 12 | `proximo_mantenimiento` | `datetime` | ✓ |  |
| 13 | `costo_hora` | `float` | ✓ |  |
| 14 | `nivel_desde` | `int` |  |  |
| 15 | `nivel_hasta` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_montacarga` | CLUSTERED · **PK** | IdMontacarga |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

