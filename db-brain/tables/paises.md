---
id: db-brain-table-paises
type: db-table
title: dbo.paises
schema: dbo
name: paises
kind: table
rows: 37
modify_date: 2016-05-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.paises`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 37 |
| Schema modify_date | 2016-05-27 |
| Columnas | 6 |
| Índices | 1 |
| FKs | out:0 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPais` | `int` |  |  |
| 2 | `ISONUM` | `int` | ✓ |  |
| 3 | `ISO2` | `nvarchar(50)` | ✓ |  |
| 4 | `ISO3` | `nvarchar(50)` | ✓ |  |
| 5 | `NOMBRE` | `nvarchar(50)` | ✓ |  |
| 6 | `Activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_paises` | CLUSTERED · **PK** | IdPais |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `bodega` (`FK_bodega_paises`)
- `pais_departamento` (`FK_pais_departamento_paises`)
- `pais_region` (`FK_pais_region_paises`)

## Quién la referencia

**5** objetos:

- `CLBD` (stored_procedure)
- `VW_Bodega` (view)
- `VW_Pais` (view)
- `VW_Pais_Departamento` (view)
- `VW_Pais_Region` (view)

