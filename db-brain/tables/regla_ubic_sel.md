---
id: db-brain-table-regla-ubic-sel
type: db-table
title: dbo.regla_ubic_sel
schema: dbo
name: regla_ubic_sel
kind: table
rows: 0
modify_date: 2017-08-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubic_sel`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-08-07 |
| Columnas | 2 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUbicacion` | `int` |  |  |
| 2 | `IdReglaUbicacionEnc` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubic_orden_1` | CLUSTERED · **PK** | IdUbicacion, IdReglaUbicacionEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

