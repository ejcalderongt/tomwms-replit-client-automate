---
id: db-brain-table-trans-pe-servicios
type: db-table
title: dbo.trans_pe_servicios
schema: dbo
name: trans_pe_servicios
kind: table
rows: 0
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_pe_servicios`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-05-28 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenPedidoServicio` | `int` |  |  |
| 2 | `IdOrdenPedidoEnc` | `int` |  |  |
| 3 | `IdServicio` | `int` |  |  |
| 4 | `cantidad` | `int` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `observacion` | `nvarchar(150)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_pe_Serv` | CLUSTERED · **PK** | IdOrdenPedidoServicio |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

