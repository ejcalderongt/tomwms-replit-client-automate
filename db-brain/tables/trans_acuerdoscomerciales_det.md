---
id: db-brain-table-trans-acuerdoscomerciales-det
type: db-table
title: dbo.trans_acuerdoscomerciales_det
schema: dbo
name: trans_acuerdoscomerciales_det
kind: table
rows: 0
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_acuerdoscomerciales_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-07-02 |
| Columnas | 21 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdAcuerdoDet` | `int` |  |  |
| 2 | `IdAcuerdoEnc` | `int` | ✓ |  |
| 3 | `codigo_producto` | `varchar(50)` |  |  |
| 4 | `servicio` | `varchar(100)` | ✓ |  |
| 5 | `nemonico` | `varchar(10)` | ✓ |  |
| 6 | `codigo_acuerdo` | `int` | ✓ |  |
| 7 | `correlativo_detalleacuerdo` | `int` | ✓ |  |
| 8 | `descripcion` | `varchar(200)` | ✓ |  |
| 9 | `numero_unidades` | `numeric` | ✓ |  |
| 10 | `monto` | `numeric` | ✓ |  |
| 11 | `porcentaje` | `numeric` | ✓ |  |
| 12 | `dias_eventos` | `int` | ✓ |  |
| 13 | `corre_cbcatalogoproductos` | `int` | ✓ |  |
| 14 | `estado` | `bit` | ✓ |  |
| 15 | `prioridad` | `tinyint` | ✓ |  |
| 16 | `IdBodega` | `int` | ✓ |  |
| 17 | `IdTipoCobro` | `int` | ✓ |  |
| 18 | `user_agr` | `int` | ✓ |  |
| 19 | `fec_agr` | `datetime` | ✓ |  |
| 20 | `user_mod` | `int` | ✓ |  |
| 21 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_acuerdoscomerciales_det` | CLUSTERED · **PK** | IdAcuerdoDet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_acuerdoscomerciales_det_trans_acuerdoscomerciales_enc` → `trans_acuerdoscomerciales_enc`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

