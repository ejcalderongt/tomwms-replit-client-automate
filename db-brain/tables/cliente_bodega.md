---
id: db-brain-table-cliente-bodega
type: db-table
title: dbo.cliente_bodega
schema: dbo
name: cliente_bodega
kind: table
rows: 3400
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.cliente_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 3.400 |
| Schema modify_date | 2024-09-12 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:4 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdClienteBodega` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdCliente` | `int` |  |  |
| 4 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` |  |  |
| 9 | `IdAreaDestino` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_cliente_bodega` | CLUSTERED · **PK** | IdClienteBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_cliente_bodega_area` → `bodega_area`
- `FK_cliente_bodega_bodega` → `bodega`
- `FK_cliente_bodega_bodega1` → `bodega`
- `FK_cliente_bodega_cliente` → `cliente`

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_Cliente` (view)

