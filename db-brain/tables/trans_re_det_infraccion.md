---
id: db-brain-table-trans-re-det-infraccion
type: db-table
title: dbo.trans_re_det_infraccion
schema: dbo
name: trans_re_det_infraccion
kind: table
rows: 0
modify_date: 2018-10-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_re_det_infraccion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-10-11 |
| Columnas | 11 |
| Índices | 1 |
| FKs | out:5 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRecepcionDetInfraccion` | `int` |  |  |
| 2 | `IdReglaPropietarioEnc` | `int` | ✓ |  |
| 3 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 4 | `IdRecepcionEnc` | `int` | ✓ |  |
| 5 | `IdPresentacion` | `int` | ✓ |  |
| 6 | `IdProductoBodega` | `int` | ✓ |  |
| 7 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_re_det_infraccion` | CLUSTERED · **PK** | IdRecepcionDetInfraccion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_re_det_infraccion_producto_bodega` → `producto_bodega`
- `FK_trans_re_det_infraccion_producto_presentacion` → `producto_presentacion`
- `FK_trans_re_det_infraccion_propietario_reglas_enc` → `propietario_reglas_enc`
- `FK_trans_re_det_infraccion_trans_oc_enc` → `trans_oc_enc`
- `FK_trans_re_det_infraccion_trans_re_enc` → `trans_re_enc`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

