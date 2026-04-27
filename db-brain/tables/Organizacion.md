---
id: db-brain-table-organizacion
type: db-table
title: dbo.Organizacion
schema: dbo
name: Organizacion
kind: table
rows: 0
modify_date: 2023-10-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Organizacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-29 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `OrganizacionId` | `int` |  | ✓ |
| 2 | `ProspectoId` | `int` | ✓ |  |
| 3 | `GerenteGeneral` | `nvarchar(100)` | ✓ |  |
| 4 | `GerenteCompras` | `nvarchar(100)` | ✓ |  |
| 5 | `GerenteLogistica` | `nvarchar(100)` | ✓ |  |
| 6 | `GerenteServicios` | `nvarchar(100)` | ✓ |  |
| 7 | `GerenteTecnologia` | `nvarchar(100)` | ✓ |  |
| 8 | `GerenteOperaciones` | `nvarchar(100)` | ✓ |  |
| 9 | `GerenteVentas` | `nvarchar(100)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__Organiza__5B2781CF41EBEDAE` | CLUSTERED · **PK** | OrganizacionId |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK__Organizac__Prosp__661949FE` → `Prospecto`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

