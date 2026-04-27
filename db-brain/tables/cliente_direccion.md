---
id: db-brain-table-cliente-direccion
type: db-table
title: dbo.cliente_direccion
schema: dbo
name: cliente_direccion
kind: table
rows: 5
modify_date: 2022-12-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.cliente_direccion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 5 |
| Schema modify_date | 2022-12-17 |
| Columnas | 18 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDireccion` | `int` |  |  |
| 2 | `IdCliente` | `int` |  |  |
| 3 | `IdRegion` | `int` | ✓ |  |
| 4 | `IdMunicipio` | `int` | ✓ |  |
| 5 | `Avenida` | `nvarchar(50)` | ✓ |  |
| 6 | `Calle` | `nvarchar(50)` | ✓ |  |
| 7 | `No_Casa` | `nvarchar(50)` | ✓ |  |
| 8 | `Zona` | `nvarchar(50)` | ✓ |  |
| 9 | `Direccion` | `nvarchar(250)` | ✓ |  |
| 10 | `Referencia` | `nvarchar(50)` | ✓ |  |
| 11 | `coordenada_x` | `nvarchar(50)` | ✓ |  |
| 12 | `coordenada_y` | `nvarchar(50)` | ✓ |  |
| 13 | `Local` | `bit` | ✓ |  |
| 14 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 15 | `fec_agr` | `datetime` | ✓ |  |
| 16 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 17 | `fec_mod` | `datetime` | ✓ |  |
| 18 | `activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_cliente_direccion` | CLUSTERED · **PK** | IdDireccion, IdCliente |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_cliente_direccion_pais_municipio` → `pais_municipio`
- `FK_cliente_direccion_pais_region` → `pais_region`

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_ClienteDireccion` (view)

