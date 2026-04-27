---
id: db-brain-table-cliente-tipo
type: db-table
title: dbo.cliente_tipo
schema: dbo
name: cliente_tipo
kind: table
rows: 6
modify_date: 2017-10-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.cliente_tipo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2017-10-27 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoCliente` | `int` |  |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `NombreTipoCliente` | `nvarchar(50)` | ✓ |  |
| 4 | `Activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_cliente_tipo` | CLUSTERED · **PK** | IdTipoCliente |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_cliente_tipo_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `cliente` (`FK_cliente_cliente_tipo`)

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `VW_Cliente` (view)
- `VW_ClienteTipo` (view)

