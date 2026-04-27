---
id: db-brain-table-trans-oc-det-lote
type: db-table
title: dbo.trans_oc_det_lote
schema: dbo
name: trans_oc_det_lote
kind: table
rows: 1078
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_det_lote`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1.078 |
| Schema modify_date | 2023-08-21 |
| Columnas | 21 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` |  |  |
| 2 | `IdOrdenCompraDet` | `int` |  |  |
| 3 | `IdOrdenCompraDetLote` | `int` |  |  |
| 4 | `IdProductoBodega` | `int` |  |  |
| 5 | `no_linea` | `int` | ✓ |  |
| 6 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 7 | `cantidad` | `float` | ✓ |  |
| 8 | `cantidad_recibida` | `float` | ✓ |  |
| 9 | `lote` | `nvarchar(50)` | ✓ |  |
| 10 | `fecha_vence` | `date` | ✓ |  |
| 11 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 12 | `Ubicacion` | `nvarchar(50)` | ✓ |  |
| 13 | `reclasificar` | `bit` |  |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `no_documento` | `nvarchar(50)` | ✓ |  |
| 16 | `IdPresentacion` | `int` | ✓ |  |
| 17 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 18 | `user_agr` | `nvarchar(50)` |  |  |
| 19 | `fec_agr` | `datetime` |  |  |
| 20 | `user_mod` | `nvarchar(50)` |  |  |
| 21 | `fec_mod` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_oc_det_lote` | CLUSTERED · **PK** | IdOrdenCompraDetLote |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

