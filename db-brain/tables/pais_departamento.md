---
id: db-brain-table-pais-departamento
type: db-table
title: dbo.pais_departamento
schema: dbo
name: pais_departamento
kind: table
rows: 69
modify_date: 2016-05-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.pais_departamento`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 69 |
| Schema modify_date | 2016-05-27 |
| Columnas | 7 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDepartamento` | `int` |  |  |
| 2 | `IdPais` | `int` |  |  |
| 3 | `Nombre` | `varchar(25)` | ✓ |  |
| 4 | `fec_agr` | `datetime2` | ✓ |  |
| 5 | `fec_mod` | `datetime2` | ✓ |  |
| 6 | `user_agr` | `int` | ✓ |  |
| 7 | `user_mod` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__pais_dep__787A433D5CFF1199` | CLUSTERED · **PK** | IdDepartamento |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_pais_departamento_paises` → `paises`

### Entrantes (otra tabla → esta)

- `pais_municipio` (`FK_pais_municipio_pais_departamento`)

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Pais` (view)
- `VW_Pais_Departamento` (view)

