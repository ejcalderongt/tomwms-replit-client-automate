---
id: db-brain-table-producto-rellenado
type: db-table
title: dbo.producto_rellenado
schema: dbo
name: producto_rellenado
kind: table
rows: 1
modify_date: 2021-11-19
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_rellenado`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2021-11-19 |
| Columnas | 19 |
| Índices | 1 |
| FKs | out:3 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRellenado` | `int` |  |  |
| 2 | `IdPresentacion` | `int` | ✓ |  |
| 3 | `IdProductoEstado` | `int` | ✓ |  |
| 4 | `IdUbicacion` | `int` | ✓ |  |
| 5 | `IdTipoAccion` | `int` | ✓ |  |
| 6 | `Minimo` | `float` | ✓ |  |
| 7 | `Maximo` | `float` | ✓ |  |
| 8 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 11 | `fec_mod` | `datetime` | ✓ |  |
| 12 | `Activo` | `bit` | ✓ |  |
| 13 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 14 | `IdBodega` | `int` | ✓ |  |
| 15 | `IdProductoBodega` | `int` | ✓ |  |
| 16 | `IdUmBasAbastercerCon` | `int` | ✓ |  |
| 17 | `IdPresentacionAbastercerCon` | `int` | ✓ |  |
| 18 | `IdPropietario` | `int` | ✓ |  |
| 19 | `IdOperadorDefecto` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_rellenado` | CLUSTERED · **PK** | IdRellenado |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_rellenado_producto_estado` → `producto_estado`
- `FK_producto_rellenado_producto_presentacion` → `producto_presentacion`
- `FK_producto_rellenado_sis_tipo_accion` → `sis_tipo_accion`

## Quién la referencia

**5** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `VW_ProductoRellenado` (view)
- `VW_Revision_Producto` (view)
- `VW_RevisionProducto` (view)

