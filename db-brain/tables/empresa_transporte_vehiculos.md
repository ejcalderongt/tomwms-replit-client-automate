---
id: db-brain-table-empresa-transporte-vehiculos
type: db-table
title: dbo.empresa_transporte_vehiculos
schema: dbo
name: empresa_transporte_vehiculos
kind: table
rows: 0
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.empresa_transporte_vehiculos`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-05-28 |
| Columnas | 19 |
| Índices | 1 |
| FKs | out:1 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdVehiculo` | `int` |  |  |
| 2 | `IdEmpresaTransporte` | `int` |  |  |
| 3 | `IdTipoContenedor` | `int` | ✓ |  |
| 4 | `placa` | `nvarchar(20)` | ✓ |  |
| 5 | `marca` | `nvarchar(50)` | ✓ |  |
| 6 | `modelo` | `nvarchar(50)` | ✓ |  |
| 7 | `peso` | `float` | ✓ |  |
| 8 | `volumen` | `float` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 11 | `fec_agr` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 13 | `fec_mod` | `datetime` | ✓ |  |
| 14 | `tipo` | `nvarchar(50)` | ✓ |  |
| 15 | `alto` | `float` | ✓ |  |
| 16 | `largo` | `float` | ✓ |  |
| 17 | `ancho` | `float` | ✓ |  |
| 18 | `placa_comercial` | `nvarchar(50)` | ✓ |  |
| 19 | `es_contedor` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_empresa_transporte_vehiculos_1` | CLUSTERED · **PK** | IdVehiculo |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_empresa_transporte_vehiculos_empresa_transporte` → `empresa_transporte`

### Entrantes (otra tabla → esta)

- `tms_ticket` (`FK_tms_ticket_empresa_transporte_vehiculos`)
- `trans_despacho_enc` (`FK_trans_despacho_enc_empresa_transporte_vehiculos`)
- `trans_tras_enc` (`FK_trans_tras_enc_empresa_transporte_vehiculos`)

## Quién la referencia

**13** objetos:

- `CLBD` (stored_procedure)
- `VW_Despacho` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Packing` (view)
- `VW_REC_CON_OC` (view)
- `VW_REC_SIN_OC` (view)
- `VW_Recepcion_Det` (view)
- `VW_TMS_Tikcet` (view)

