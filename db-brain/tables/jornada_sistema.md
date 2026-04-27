---
id: db-brain-table-jornada-sistema
type: db-table
title: dbo.jornada_sistema
schema: dbo
name: jornada_sistema
kind: table
rows: 0
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.jornada_sistema`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-05-28 |
| Columnas | 6 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdJornada` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `Fecha` | `date` | ✓ |  |
| 5 | `IdUsuario` | `int` | ✓ |  |
| 6 | `FechaAgregado` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_jornada_sistema` | CLUSTERED · **PK** | IdJornada |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

