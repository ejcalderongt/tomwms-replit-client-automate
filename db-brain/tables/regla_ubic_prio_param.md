---
id: db-brain-table-regla-ubic-prio-param
type: db-table
title: dbo.regla_ubic_prio_param
schema: dbo
name: regla_ubic_prio_param
kind: table
rows: 9
modify_date: 2017-08-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubic_prio_param`

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
| 1 | `IdReglaUbicPrioParam` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(100)` | ✓ |  |
| 3 | `Activo` | `int` | ✓ |  |
| 4 | `Orden` | `int` | ✓ |  |
| 5 | `Tipo` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubic_prio_param` | CLUSTERED · **PK** | IdReglaUbicPrioParam |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

