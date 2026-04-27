---
id: db-brain-table-trans-tras-enc
type: db-table
title: dbo.trans_tras_enc
schema: dbo
name: trans_tras_enc
kind: table
rows: 0
modify_date: 2016-07-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_tras_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-07-12 |
| Columnas | 27 |
| Índices | 1 |
| FKs | out:7 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTrasladoEnc` | `int` |  |  |
| 2 | `IdBodegaOrigen` | `int` | ✓ |  |
| 3 | `IdBodegaDestino` | `int` | ✓ |  |
| 4 | `IdPropietarioBodega` | `int` | ✓ |  |
| 5 | `IdMuelleOrigen` | `int` | ✓ |  |
| 6 | `IdPiloto` | `int` | ✓ |  |
| 7 | `IdVehiculo` | `int` | ✓ |  |
| 8 | `IdRuta` | `int` | ✓ |  |
| 9 | `FechaTraslado` | `datetime` | ✓ |  |
| 10 | `hora_ini` | `datetime` | ✓ |  |
| 11 | `hora_fin` | `datetime` | ✓ |  |
| 12 | `ubicacion` | `nvarchar(35)` | ✓ |  |
| 13 | `estado` | `nvarchar(20)` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |
| 17 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 18 | `fec_mod` | `datetime` | ✓ |  |
| 19 | `no_documento` | `bigint` | ✓ |  |
| 20 | `local` | `bit` | ✓ |  |
| 21 | `pallet_primero` | `bit` | ✓ |  |
| 22 | `anulado` | `bit` | ✓ |  |
| 23 | `FechaEntrega` | `datetime` | ✓ |  |
| 24 | `Observacion` | `nvarchar(max)` | ✓ |  |
| 25 | `HoraEntregaDesde` | `datetime` | ✓ |  |
| 26 | `HoraEntregaHasta` | `datetime` | ✓ |  |
| 27 | `NoGuia` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_tras_enc` | CLUSTERED · **PK** | IdTrasladoEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_tras_enc_bodega` → `bodega`
- `FK_trans_tras_enc_bodega_muelles` → `bodega_muelles`
- `FK_trans_tras_enc_bodega1` → `bodega`
- `FK_trans_tras_enc_empresa_transporte_pilotos` → `empresa_transporte_pilotos`
- `FK_trans_tras_enc_empresa_transporte_vehiculos` → `empresa_transporte_vehiculos`
- `FK_trans_tras_enc_propietario_bodega` → `propietario_bodega`
- `FK_trans_tras_enc_road_ruta` → `road_ruta`

### Entrantes (otra tabla → esta)

- `trans_tras_det` (`FK_trans_tras_det_trans_tras_enc`)
- `trans_tras_op` (`FK_trans_tras_op_trans_tras_enc`)

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)

