---
id: db-brain-table-usuario-bodega
type: db-table
title: dbo.usuario_bodega
schema: dbo
name: usuario_bodega
kind: table
rows: 63
modify_date: 2016-04-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.usuario_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 63 |
| Schema modify_date | 2016-04-25 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:3 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUsuarioBodega` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdUsuario` | `int` |  |  |
| 4 | `IdUsuarioSuperior` | `int` | ✓ |  |
| 5 | `IdRol` | `int` | ✓ |  |
| 6 | `Activo` | `bit` | ✓ |  |
| 7 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_usuarios_empresa` | CLUSTERED · **PK** | IdUsuarioBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_usuario_bodega_usuario` → `usuario`
- `FK_usuarios_empresa_bodega` → `bodega`
- `FK_usuarios_empresa_usuario` → `usuario`

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `VW_TransUbicacionHhEnc` (view)

