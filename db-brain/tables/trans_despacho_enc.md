---
id: db-brain-table-trans-despacho-enc
type: db-table
title: dbo.trans_despacho_enc
schema: dbo
name: trans_despacho_enc
kind: table
rows: 4032
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_despacho_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4.032 |
| Schema modify_date | 2025-02-11 |
| Columnas | 21 |
| Índices | 1 |
| FKs | out:5 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDespachoEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` | ✓ |  |
| 4 | `IdVehiculo` | `int` | ✓ |  |
| 5 | `IdPiloto` | `int` | ✓ |  |
| 6 | `IdRuta` | `int` | ✓ |  |
| 7 | `fecha` | `datetime` | ✓ |  |
| 8 | `no_pase` | `int` | ✓ |  |
| 9 | `observacion` | `nvarchar(500)` | ✓ |  |
| 10 | `hora_ini` | `datetime` | ✓ |  |
| 11 | `hora_fin` | `datetime` | ✓ |  |
| 12 | `estado` | `nvarchar(20)` | ✓ |  |
| 13 | `numero` | `int` | ✓ |  |
| 14 | `marchamo` | `nvarchar(50)` | ✓ |  |
| 15 | `cant_bultos` | `float` | ✓ |  |
| 16 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 17 | `fec_agr` | `datetime` | ✓ |  |
| 18 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 19 | `fec_mod` | `datetime` | ✓ |  |
| 20 | `activo` | `bit` | ✓ |  |
| 21 | `no_documento_externo` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_despacho_enc` | CLUSTERED · **PK** | IdDespachoEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_despacho_enc_bodega` → `bodega`
- `FK_trans_despacho_enc_empresa_transporte_pilotos` → `empresa_transporte_pilotos`
- `FK_trans_despacho_enc_empresa_transporte_vehiculos` → `empresa_transporte_vehiculos`
- `FK_trans_despacho_enc_propietario_bodega` → `propietario_bodega`
- `FK_trans_despacho_enc_road_ruta` → `road_ruta`

### Entrantes (otra tabla → esta)

- `trans_despacho_det` (`FK_trans_despacho_det_trans_despacho_enc`)

## Quién la referencia

**14** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Despacho` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `vw_Indicador_Despachos` (view)
- `VW_Lotes_Despacho` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Packing` (view)

