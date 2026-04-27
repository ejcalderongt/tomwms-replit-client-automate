---
id: db-brain-table-impresora
type: db-table
title: dbo.impresora
schema: dbo
name: impresora
kind: table
rows: 13
modify_date: 2025-03-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.impresora`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 13 |
| Schema modify_date | 2025-03-27 |
| Columnas | 18 |
| Índices | 1 |
| FKs | out:1 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdImpresora` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `nombre` | `nvarchar(50)` |  |  |
| 4 | `direccion_Ip` | `nvarchar(50)` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` |  |  |
| 6 | `fec_agr` | `datetime` |  |  |
| 7 | `user_mod` | `nvarchar(50)` |  |  |
| 8 | `fec_mod` | `datetime` |  |  |
| 9 | `activo` | `bit` |  |  |
| 10 | `mac_adress` | `nvarchar(25)` | ✓ |  |
| 11 | `IdBodega` | `int` | ✓ |  |
| 12 | `numero_serie` | `nvarchar(50)` | ✓ |  |
| 13 | `IdImpresoraMarca` | `int` | ✓ |  |
| 14 | `IdLenguaje` | `int` | ✓ |  |
| 15 | `IdTipoConexion` | `int` | ✓ |  |
| 16 | `puerto` | `int` | ✓ |  |
| 17 | `es_movil` | `bit` |  |  |
| 18 | `velocidad` | `float` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_Impresora` | CLUSTERED · **PK** | IdImpresora |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_impresora_empresa` → `empresa`

### Entrantes (otra tabla → esta)

- `impresora_mensaje` (`FK_impresora_mensaje_impresora`)
- `producto_pallet` (`FK_producto_pallet_Impresora`)

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

