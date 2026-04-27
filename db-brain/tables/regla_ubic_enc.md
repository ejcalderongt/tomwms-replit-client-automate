---
id: db-brain-table-regla-ubic-enc
type: db-table
title: dbo.regla_ubic_enc
schema: dbo
name: regla_ubic_enc
kind: table
rows: 0
modify_date: 2017-07-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubic_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-07-13 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:0 in:7 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaUbicacionEnc` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `Activo` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubic_enc` | CLUSTERED · **PK** | IdReglaUbicacionEnc |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `regla_ubic_det_ir` (`FK_regla_ubic_det_ir_regla_ubic_enc`)
- `regla_ubic_det_pe` (`FK_regla_ubic_det_pe_regla_ubic_enc`)
- `regla_ubic_det_pp` (`FK_regla_ubic_det_pp_regla_ubic_enc`)
- `regla_ubic_det_prop` (`FK_regla_ubic_det_prop_regla_ubic_enc`)
- `regla_ubic_det_tp` (`FK_regla_ubic_det_tp_regla_ubic_enc`)
- `regla_ubic_det_tr` (`FK_regla_ubic_det_tr_regla_ubic_enc`)
- `regla_ubicacion` (`FK_regla_ubicacion_regla_ubic_enc`)

## Quién la referencia

**2** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `vw_ubicaciones_por_regla` (view)

