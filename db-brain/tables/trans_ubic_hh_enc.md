---
id: db-brain-table-trans-ubic-hh-enc
type: db-table
title: dbo.trans_ubic_hh_enc
schema: dbo
name: trans_ubic_hh_enc
kind: table
rows: 77
modify_date: 2026-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_ubic_hh_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 77 |
| Schema modify_date | 2026-02-11 |
| Columnas | 21 |
| Índices | 1 |
| FKs | out:1 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareaUbicacionEnc` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` |  |  |
| 3 | `IdMotivoUbicacion` | `int` | ✓ |  |
| 4 | `FechaInicio` | `date` | ✓ |  |
| 5 | `HoraInicio` | `datetime` | ✓ |  |
| 6 | `FechaFin` | `date` | ✓ |  |
| 7 | `HoraFin` | `datetime` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_agr` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_mod` | `datetime` | ✓ |  |
| 12 | `Observacion` | `nvarchar(150)` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `operador_por_linea` | `bit` | ✓ |  |
| 15 | `ubicacion_con_hh` | `bit` | ✓ |  |
| 16 | `estado` | `nvarchar(50)` | ✓ |  |
| 17 | `cambio_estado` | `bit` | ✓ |  |
| 18 | `IdReabastecimientoLog` | `int` | ✓ |  |
| 19 | `es_traslado_sap` | `bit` |  |  |
| 20 | `no_documento` | `nvarchar(50)` | ✓ |  |
| 21 | `sync_mi3` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_ubic_hh_enc_1` | CLUSTERED · **PK** | IdTareaUbicacionEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_ubic_hh_enc_propietario_bodega` → `propietario_bodega`

### Entrantes (otra tabla → esta)

- `trans_ubic_hh_det` (`FK_trans_ubic_hh_det_trans_ubic_hh_enc`)
- `trans_ubic_tarima` (`FK_trans_ubic_tarima_trans_ubic_hh_enc`)

## Quién la referencia

**10** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Cantidad_Tareas_Ubicacion_Op` (view)
- `VW_Cantidad_Tareas_Ubicacion_Op_Items` (view)
- `VW_ControlCalidad_CambioEstado` (view)
- `VW_TransUbicacionHhEnc` (view)

