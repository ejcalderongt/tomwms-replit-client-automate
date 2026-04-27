---
id: db-brain-table-empresa-transporte-bodega
type: db-table
title: dbo.empresa_transporte_bodega
schema: dbo
name: empresa_transporte_bodega
kind: table
rows: 0
modify_date: 2016-02-08
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.empresa_transporte_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-02-08 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdAsignacion` | `int` |  |  |
| 2 | `IdEmpresaTransporte` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_empresa_transporte_bodega` | CLUSTERED · **PK** | IdAsignacion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_empresa_transporte_bodega_bodega` → `bodega`
- `FK_empresa_transporte_bodega_empresa_transporte` → `empresa_transporte`

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

