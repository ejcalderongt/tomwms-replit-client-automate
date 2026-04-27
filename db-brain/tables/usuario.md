---
id: db-brain-table-usuario
type: db-table
title: dbo.usuario
schema: dbo
name: usuario
kind: table
rows: 12
modify_date: 2025-07-14
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.usuario`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 12 |
| Schema modify_date | 2025-07-14 |
| Columnas | 21 |
| Índices | 1 |
| FKs | out:1 in:4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUsuario` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `nombres` | `nvarchar(100)` | ✓ |  |
| 4 | `apellidos` | `nvarchar(100)` | ✓ |  |
| 5 | `cedula` | `nvarchar(25)` | ✓ |  |
| 6 | `direccion` | `nvarchar(100)` | ✓ |  |
| 7 | `telefono` | `nvarchar(25)` | ✓ |  |
| 8 | `email` | `nvarchar(100)` | ✓ |  |
| 9 | `codigo` | `nvarchar(50)` | ✓ |  |
| 10 | `clave` | `nvarchar(50)` | ✓ |  |
| 11 | `ultimo_login` | `datetime` | ✓ |  |
| 12 | `activo` | `bit` | ✓ |  |
| 13 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 14 | `fec_agr` | `datetime` | ✓ |  |
| 15 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 16 | `fec_mod` | `datetime` | ✓ |  |
| 17 | `foto` | `image` | ✓ |  |
| 18 | `sistema` | `bit` | ✓ |  |
| 19 | `clave_autorizacion` | `nvarchar(50)` | ✓ |  |
| 20 | `usuario_sap_b1` | `nvarchar(50)` | ✓ |  |
| 21 | `clave_sap_b1` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_usuario_1` | CLUSTERED · **PK** | IdUsuario |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_usuario_empresa` → `empresa`

### Entrantes (otra tabla → esta)

- `i_nav_config_enc` (`FK_i_nav_config_enc_usuario`)
- `impresora_mensaje` (`FK_impresora_mensaje_usuario`)
- `usuario_bodega` (`FK_usuario_bodega_usuario`)
- `usuario_bodega` (`FK_usuarios_empresa_usuario`)

## Quién la referencia

**11** objetos:

- `CLBD` (stored_procedure)
- `VW_ControlCalidad_CambioEstado` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `VW_Recepcion` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_Reporte_Recepcion_20190726` (view)
- `VW_Resoluciones_Usuario` (view)
- `VW_TransUbicacionHhEnc` (view)

