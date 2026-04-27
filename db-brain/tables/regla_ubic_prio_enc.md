---
id: db-brain-table-regla-ubic-prio-enc
type: db-table
title: dbo.regla_ubic_prio_enc
schema: dbo
name: regla_ubic_prio_enc
kind: table
rows: 1
modify_date: 2017-08-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubic_prio_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2017-08-05 |
| Columnas | 9 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaUbicPrioEnc` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `Activo` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubic_prio_enc` | CLUSTERED · **PK** | IdReglaUbicPrioEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

