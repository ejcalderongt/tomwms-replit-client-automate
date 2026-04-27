---
id: db-brain-table-trans-servicio-det
type: db-table
title: dbo.trans_servicio_det
schema: dbo
name: trans_servicio_det
kind: table
rows: 0
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_servicio_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-08-25 |
| Columnas | 15 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdServicioDet` | `int` |  |  |
| 2 | `IdServicioEnc` | `int` |  |  |
| 3 | `observacion` | `nvarchar(150)` | ✓ |  |
| 4 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 5 | `nombre_servicio` | `nvarchar(150)` | ✓ |  |
| 6 | `unid_medida` | `int` | ✓ |  |
| 7 | `nombre_unidad` | `nvarchar(50)` | ✓ |  |
| 8 | `corre_detalleacuerdo` | `int` | ✓ |  |
| 9 | `corre_catalogoproductos` | `int` | ✓ |  |
| 10 | `cantidad` | `int` | ✓ |  |
| 11 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 12 | `fec_agr` | `datetime` | ✓ |  |
| 13 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 14 | `fec_mod` | `datetime` | ✓ |  |
| 15 | `IdPropietario` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_servicio_det` | CLUSTERED · **PK** | IdServicioDet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_servicio_det_propietarios` → `propietarios`
- `FK_trans_servicio_det_trans_servicio_det` → `trans_servicio_enc`

## Quién la referencia

**1** objetos:

- `VW_Servicio` (view)

