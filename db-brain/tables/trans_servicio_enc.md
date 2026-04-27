---
id: db-brain-table-trans-servicio-enc
type: db-table
title: dbo.trans_servicio_enc
schema: dbo
name: trans_servicio_enc
kind: table
rows: 0
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_servicio_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-08-25 |
| Columnas | 19 |
| Índices | 1 |
| FKs | out:4 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdServicioEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdEmpresa` | `int` |  |  |
| 4 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 5 | `IdPedidoEnc` | `int` | ✓ |  |
| 6 | `IdPropietario` | `int` | ✓ |  |
| 7 | `no_poliza` | `nvarchar(50)` | ✓ |  |
| 8 | `no_orden` | `nvarchar(50)` | ✓ |  |
| 9 | `fecha_doc_ingreso` | `datetime` | ✓ |  |
| 10 | `fecha_doc_salida` | `datetime` | ✓ |  |
| 11 | `fecha_servicio` | `datetime` | ✓ |  |
| 12 | `enviado_a_erp` | `bit` | ✓ |  |
| 13 | `activo` | `bit` | ✓ |  |
| 14 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 15 | `fec_agr` | `datetime` | ✓ |  |
| 16 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 17 | `fec_mod` | `datetime` | ✓ |  |
| 18 | `Estado` | `nvarchar(50)` | ✓ |  |
| 19 | `es_ingreso` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_servicio_enc` | CLUSTERED · **PK** | IdServicioEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_servicio_enc_trans_oc_enc` → `trans_oc_enc`
- `FK_trans_servicio_enc_bodega` → `bodega`
- `FK_trans_servicio_enc_empresa` → `empresa`
- `FK_trans_servicio_enc_propietarios` → `propietarios`

### Entrantes (otra tabla → esta)

- `trans_servicio_det` (`FK_trans_servicio_det_trans_servicio_det`)

## Quién la referencia

**2** objetos:

- `VW_Servicio` (view)
- `VW_Trans_Servicios` (view)

