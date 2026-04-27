---
id: db-brain-table-producto-sustituto
type: db-table
title: dbo.producto_sustituto
schema: dbo
name: producto_sustituto
kind: table
rows: 0
modify_date: 2018-01-22
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_sustituto`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-01-22 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:4 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdProductoSustituto` | `int` |  |  |
| 2 | `IdProductoOriginal` | `int` |  |  |
| 3 | `IdProductoPresentacionOriginal` | `int` | ✓ |  |
| 4 | `IdProductoReemplazo` | `int` |  |  |
| 5 | `IdProductoPresentacionReemplazo` | `int` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_sustituto` | CLUSTERED · **PK** | IdProductoSustituto |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_sustituto_producto` → `producto`
- `FK_producto_sustituto_producto_presentacion` → `producto_presentacion`
- `FK_producto_sustituto_producto_presentacion1` → `producto_presentacion`
- `FK_producto_sustituto_producto1` → `producto`

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_ProductoSustituto` (view)

