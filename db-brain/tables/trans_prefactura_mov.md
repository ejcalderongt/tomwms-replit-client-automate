---
id: db-brain-table-trans-prefactura-mov
type: db-table
title: dbo.trans_prefactura_mov
schema: dbo
name: trans_prefactura_mov
kind: table
rows: 0
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_prefactura_mov`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-09-12 |
| Columnas | 13 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Idtransprefacturamov` | `int` |  |  |
| 2 | `IdTransPrefacturaEnc` | `int` | ✓ |  |
| 3 | `IdOrdenCompraPol` | `int` | ✓ |  |
| 4 | `poliza_oc_numero_orden` | `nvarchar(50)` | ✓ |  |
| 5 | `cantidad_tarimas` | `int` | ✓ |  |
| 6 | `cantidad_cajas` | `int` | ✓ |  |
| 7 | `costo_x_caja` | `decimal` | ✓ |  |
| 8 | `almacenaje` | `decimal` | ✓ |  |
| 9 | `manejo` | `decimal` | ✓ |  |
| 10 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 11 | `fec_agr` | `datetime` | ✓ |  |
| 12 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 13 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_prefactura_mov` | CLUSTERED · **PK** | Idtransprefacturamov |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

