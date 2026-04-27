---
id: db-brain-table-regla-vencimiento
type: db-table
title: dbo.regla_vencimiento
schema: dbo
name: regla_vencimiento
kind: table
rows: 0
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_vencimiento`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-02-01 |
| Columnas | 15 |
| Índices | 1 |
| FKs | out:6 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaVencimiento` | `int` |  | ✓ |
| 2 | `NombreRegla` | `nvarchar(255)` | ✓ |  |
| 3 | `IdBodega` | `int` |  |  |
| 4 | `IdProductoFamilia` | `int` | ✓ |  |
| 5 | `IdProductoClasificacion` | `int` | ✓ |  |
| 6 | `TiempoVencimientoDias` | `int` |  |  |
| 7 | `TipoNotificacion` | `nvarchar(50)` | ✓ |  |
| 8 | `IdPropietarioMercancia` | `int` | ✓ |  |
| 9 | `IdProveedor` | `int` | ✓ |  |
| 10 | `IdCliente` | `int` | ✓ |  |
| 11 | `Activa` | `bit` |  |  |
| 12 | `FechaCreacion` | `datetime` |  |  |
| 13 | `UsuarioCreacion` | `nvarchar(255)` | ✓ |  |
| 14 | `FechaModificacion` | `datetime` | ✓ |  |
| 15 | `UsuarioModificacion` | `nvarchar(255)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__regla_ve__6A511C705C0B3BDC` | CLUSTERED · **PK** | IdReglaVencimiento |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK__regla_ven__IdBod__7BD380F3` → `bodega`
- `FK__regla_ven__IdCli__00983610` → `cliente`
- `FK__regla_ven__IdPro__7CC7A52C` → `producto_familia`
- `FK__regla_ven__IdPro__7DBBC965` → `producto_clasificacion`
- `FK__regla_ven__IdPro__7EAFED9E` → `propietarios`
- `FK__regla_ven__IdPro__7FA411D7` → `proveedor`

### Entrantes (otra tabla → esta)

- `reglas_vencimiento_contacto` (`FK__reglas_ve__IdReg__0374A2BB`)

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

