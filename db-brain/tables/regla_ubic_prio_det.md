---
id: db-brain-table-regla-ubic-prio-det
type: db-table
title: dbo.regla_ubic_prio_det
schema: dbo
name: regla_ubic_prio_det
kind: table
rows: 9
modify_date: 2017-08-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubic_prio_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 9 |
| Schema modify_date | 2017-08-06 |
| Columnas | 5 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaUbicPrioDet` | `int` |  |  |
| 2 | `IdReglaUbicPrioParam` | `int` | ✓ |  |
| 3 | `IdReglaUbicPrioEnc` | `int` | ✓ |  |
| 4 | `Orden` | `int` | ✓ |  |
| 5 | `Activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubic_prio_enc¿` | CLUSTERED · **PK** | IdReglaUbicPrioDet |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

