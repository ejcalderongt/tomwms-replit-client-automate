---
id: db-brain-table-pais-region
type: db-table
title: dbo.pais_region
schema: dbo
name: pais_region
kind: table
rows: 6
modify_date: 2016-05-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.pais_region`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2016-05-27 |
| Columnas | 7 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRegion` | `int` |  |  |
| 2 | `IdPais` | `int` |  |  |
| 3 | `Nombre` | `varchar(25)` | ✓ |  |
| 4 | `fec_agr` | `datetime2` | ✓ |  |
| 5 | `fec_mod` | `datetime2` | ✓ |  |
| 6 | `user_agr` | `int` | ✓ |  |
| 7 | `user_mod` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__pais_reg__8CBC09EBCD0136C9` | CLUSTERED · **PK** | IdRegion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_pais_region_paises` → `paises`

### Entrantes (otra tabla → esta)

- `cliente_direccion` (`FK_cliente_direccion_pais_region`)

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `VW_ClienteDireccion` (view)
- `VW_Pais` (view)
- `VW_Pais_Region` (view)

