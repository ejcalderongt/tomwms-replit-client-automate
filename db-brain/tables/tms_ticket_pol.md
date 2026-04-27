---
id: db-brain-table-tms-ticket-pol
type: db-table
title: dbo.tms_ticket_pol
schema: dbo
name: tms_ticket_pol
kind: table
rows: 0
modify_date: 2022-03-24
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tms_ticket_pol`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2022-03-24 |
| Columnas | 25 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenTkmPol` | `int` |  |  |
| 2 | `IdOrdenTkmEnc` | `int` |  |  |
| 3 | `NoPoliza` | `nvarchar(50)` | ✓ |  |
| 4 | `dua` | `nvarchar(50)` | ✓ |  |
| 5 | `fecha_poliza` | `datetime` | ✓ |  |
| 6 | `pais_procede` | `nvarchar(50)` | ✓ |  |
| 7 | `tipo_cambio` | `float` | ✓ |  |
| 8 | `total_valoraduana` | `float` | ✓ |  |
| 9 | `total_bultos_peso` | `float` | ✓ |  |
| 10 | `total_usd` | `float` | ✓ |  |
| 11 | `total_flete` | `float` | ✓ |  |
| 12 | `total_seguro` | `float` | ✓ |  |
| 13 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 14 | `fec_agr` | `datetime` | ✓ |  |
| 15 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 16 | `fec_mod` | `datetime` | ✓ |  |
| 17 | `clave_aduana` | `varchar(50)` | ✓ |  |
| 18 | `nit_imp_exp` | `varchar(50)` | ✓ |  |
| 19 | `clase` | `varchar(50)` | ✓ |  |
| 20 | `mod_transporte` | `varchar(50)` | ✓ |  |
| 21 | `total_liquidar` | `float` | ✓ |  |
| 22 | `total_general` | `float` | ✓ |  |
| 23 | `IdRegimen` | `int` | ✓ |  |
| 24 | `Codigo_Barra` | `nvarchar(1000)` | ✓ |  |
| 25 | `IdTicket` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tms_ticket_pol` | CLUSTERED · **PK** | IdOrdenTkmPol, IdOrdenTkmEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

