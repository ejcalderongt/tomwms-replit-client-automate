---
id: db-brain-table-trans-prefactura-det
type: db-table
title: dbo.trans_prefactura_det
schema: dbo
name: trans_prefactura_det
kind: table
rows: 0
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_prefactura_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-09-12 |
| Columnas | 19 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTransPrefacturaDet` | `int` |  |  |
| 2 | `IdTransPrefacturaEnc` | `int` |  |  |
| 3 | `IdAcuerdoEnc` | `int` | ✓ |  |
| 4 | `codigo_acuerdo_enc` | `int` | ✓ |  |
| 5 | `codigo_producto_acuerdo_det` | `nvarchar(50)` | ✓ |  |
| 6 | `IdAcuerdoDet` | `int` | ✓ |  |
| 7 | `correlativo_detalle_acuerdo` | `int` | ✓ |  |
| 8 | `numero_unidades_acuerdo_det` | `int` | ✓ |  |
| 9 | `servicio` | `nvarchar(50)` | ✓ |  |
| 10 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 11 | `monto` | `decimal` | ✓ |  |
| 12 | `porcentaje` | `decimal` | ✓ |  |
| 13 | `dias_eventos` | `int` | ✓ |  |
| 14 | `valor` | `decimal` | ✓ |  |
| 15 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |
| 17 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 18 | `fec_mod` | `datetime` | ✓ |  |
| 19 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_prefactura_det` | CLUSTERED · **PK** | IdTransPrefacturaDet, IdTransPrefacturaEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

