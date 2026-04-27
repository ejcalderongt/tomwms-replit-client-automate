---
id: db-brain-table-empresa-transporte-pilotos
type: db-table
title: dbo.empresa_transporte_pilotos
schema: dbo
name: empresa_transporte_pilotos
kind: table
rows: 1
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.empresa_transporte_pilotos`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2021-05-28 |
| Columnas | 23 |
| Índices | 1 |
| FKs | out:1 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPiloto` | `int` |  |  |
| 2 | `IdEmpresaTransporte` | `int` |  |  |
| 3 | `nombres` | `nvarchar(150)` | ✓ |  |
| 4 | `apellidos` | `nvarchar(150)` | ✓ |  |
| 5 | `telefono` | `nvarchar(50)` | ✓ |  |
| 6 | `correo_electronico` | `nvarchar(150)` | ✓ |  |
| 7 | `no_carnet` | `nvarchar(50)` | ✓ |  |
| 8 | `fecha_expiracion_carnet` | `datetime` | ✓ |  |
| 9 | `no_dpi` | `nvarchar(50)` | ✓ |  |
| 10 | `no_licencia` | `nvarchar(50)` | ✓ |  |
| 11 | `fecha_expiracion_licencia` | `datetime` | ✓ |  |
| 12 | `codigo_barra` | `nvarchar(50)` | ✓ |  |
| 13 | `direccion` | `nvarchar(50)` | ✓ |  |
| 14 | `foto` | `image` | ✓ |  |
| 15 | `fecha_nacimiento` | `datetime` | ✓ |  |
| 16 | `fecha_ingreso` | `datetime` | ✓ |  |
| 17 | `fecha_salida` | `datetime` | ✓ |  |
| 18 | `IdTipoLicencia` | `nvarchar(50)` | ✓ |  |
| 19 | `user_agr` | `nvarchar(50)` |  |  |
| 20 | `fec_agr` | `datetime` |  |  |
| 21 | `user_mod` | `nvarchar(50)` |  |  |
| 22 | `fec_mod` | `datetime` |  |  |
| 23 | `activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_empresa_transporte_pilotos_1` | CLUSTERED · **PK** | IdPiloto |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_empresa_transporte_pilotos_empresa_transporte` → `empresa_transporte`

### Entrantes (otra tabla → esta)

- `tms_ticket` (`FK_tms_ticket_empresa_transporte_pilotos`)
- `trans_despacho_enc` (`FK_trans_despacho_enc_empresa_transporte_pilotos`)
- `trans_tras_enc` (`FK_trans_tras_enc_empresa_transporte_pilotos`)

## Quién la referencia

**11** objetos:

- `CLBD` (stored_procedure)
- `VW_Despacho` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Packing` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_SIN_OC` (view)
- `VW_TMS_Tikcet` (view)

