---
id: db-brain-table-pais-municipio
type: db-table
title: dbo.pais_municipio
schema: dbo
name: pais_municipio
kind: table
rows: 401
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.pais_municipio`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 401 |
| Schema modify_date | 2023-08-21 |
| Columnas | 7 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMunicipio` | `int` |  |  |
| 2 | `IdDepartamento` | `int` |  |  |
| 3 | `Nombre` | `varchar(255)` | ✓ |  |
| 4 | `fec_agr` | `datetime2` | ✓ |  |
| 5 | `fec_mod` | `datetime2` | ✓ |  |
| 6 | `user_agr` | `int` | ✓ |  |
| 7 | `user_mod` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_pais_municipio` | CLUSTERED · **PK** | IdMunicipio |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_pais_municipio_pais_departamento` → `pais_departamento`

### Entrantes (otra tabla → esta)

- `cliente_direccion` (`FK_cliente_direccion_pais_municipio`)

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_ClienteDireccion` (view)
- `VW_Pais` (view)

