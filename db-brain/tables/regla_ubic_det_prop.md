---
id: db-brain-table-regla-ubic-det-prop
type: db-table
title: dbo.regla_ubic_det_prop
schema: dbo
name: regla_ubic_det_prop
kind: table
rows: 0
modify_date: 2017-07-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubic_det_prop`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-07-13 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaUbicacionDetProp` | `int` |  |  |
| 2 | `IdReglaUbicacionEnc` | `int` | ✓ |  |
| 3 | `IdPropietarioBodega` | `int` | ✓ |  |
| 4 | `Activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubic_det_prop` | CLUSTERED · **PK** | IdReglaUbicacionDetProp |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_regla_ubic_det_prop_propietario_bodega` → `propietario_bodega`
- `FK_regla_ubic_det_prop_regla_ubic_enc` → `regla_ubic_enc`

## Quién la referencia

**2** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `vw_ubicaciones_por_regla` (view)

