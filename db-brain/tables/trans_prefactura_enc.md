---
id: db-brain-table-trans-prefactura-enc
type: db-table
title: dbo.trans_prefactura_enc
schema: dbo
name: trans_prefactura_enc
kind: table
rows: 0
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_prefactura_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-09-12 |
| Columnas | 22 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTransPrefacturaEnc` | `int` |  |  |
| 2 | `IdBodega` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdClienteBodega` | `int` | ✓ |  |
| 5 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 6 | `IdTipoCuenta` | `int` | ✓ |  |
| 7 | `Tipo_Cambio` | `decimal` | ✓ |  |
| 8 | `IdOrdenCompraPol` | `int` | ✓ |  |
| 9 | `poliza_oc_numero_orden` | `nvarchar(50)` | ✓ |  |
| 10 | `IdOrdenPedidoEnc` | `int` | ✓ |  |
| 11 | `IdOrdenPedidoPol` | `int` | ✓ |  |
| 12 | `poliza_pe_numero_orden` | `nvarchar(50)` | ✓ |  |
| 13 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 14 | `fec_agr` | `datetime` | ✓ |  |
| 15 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 16 | `fec_mod` | `datetime` | ✓ |  |
| 17 | `anulada` | `bit` |  |  |
| 18 | `fecha_desde` | `datetime` | ✓ |  |
| 19 | `fecha_hasta` | `datetime` | ✓ |  |
| 20 | `es_consolidador` | `bit` | ✓ |  |
| 21 | `procesado_erp` | `bit` | ✓ |  |
| 22 | `autorizacion_erp` | `nvarchar(200)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_Trans_Prefactura_1` | CLUSTERED · **PK** | IdTransPrefacturaEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

