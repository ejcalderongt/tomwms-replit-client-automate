---
id: db-brain-table-tipo-contenedor
type: db-table
title: dbo.tipo_contenedor
schema: dbo
name: tipo_contenedor
kind: table
rows: 5
modify_date: 2016-07-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tipo_contenedor`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 5 |
| Schema modify_date | 2016-07-12 |
| Columnas | 14 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoContenedor` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `Largo` | `float` | ✓ |  |
| 4 | `Ancho` | `float` | ✓ |  |
| 5 | `Alto` | `float` | ✓ |  |
| 6 | `Pies` | `float` | ✓ |  |
| 7 | `Tonealadas` | `float` | ✓ |  |
| 8 | `VolumenUtil` | `float` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_mod` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `Tara` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tipo_contenedor` | CLUSTERED · **PK** | IdTipoContenedor |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

