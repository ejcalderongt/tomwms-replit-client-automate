---
id: db-brain-table-trans-oc-servicios
type: db-table
title: dbo.trans_oc_servicios
schema: dbo
name: trans_oc_servicios
kind: table
rows: 0
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_servicios`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-08-25 |
| Columnas | 17 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraServicio` | `int` |  |  |
| 2 | `IdOrdenCompraEnc` | `int` |  |  |
| 3 | `IdAcuerdo` | `int` | ✓ |  |
| 4 | `IdAcuerdoDet` | `int` |  |  |
| 5 | `observacion` | `nvarchar(150)` | ✓ |  |
| 6 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 7 | `nombre_servicio` | `nvarchar(150)` | ✓ |  |
| 8 | `unid_medida` | `int` | ✓ |  |
| 9 | `nombre_unidad` | `nvarchar(50)` | ✓ |  |
| 10 | `corre_detalleacuerdo` | `int` | ✓ |  |
| 11 | `corre_catalogoproductos` | `int` | ✓ |  |
| 12 | `cantidad` | `int` | ✓ |  |
| 13 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 14 | `fec_agr` | `datetime` | ✓ |  |
| 15 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 16 | `fec_mod` | `datetime` | ✓ |  |
| 17 | `IdPropietarioBodega` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_oc_Serv` | CLUSTERED · **PK** | IdOrdenCompraServicio |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

