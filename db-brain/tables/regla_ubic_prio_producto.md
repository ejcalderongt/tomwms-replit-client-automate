---
id: db-brain-table-regla-ubic-prio-producto
type: db-table
title: dbo.regla_ubic_prio_producto
schema: dbo
name: regla_ubic_prio_producto
kind: table
rows: 1
modify_date: 2017-08-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubic_prio_producto`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2017-08-07 |
| Columnas | 3 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdReglaUbicPrioProd` | `int` |  |  |
| 2 | `IdReglaUbicPrioEnc` | `int` | ✓ |  |
| 3 | `IdProducto` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubic_prio_ubic` | CLUSTERED · **PK** | IdReglaUbicPrioProd |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

