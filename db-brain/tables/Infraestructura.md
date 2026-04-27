---
id: db-brain-table-infraestructura
type: db-table
title: dbo.Infraestructura
schema: dbo
name: Infraestructura
kind: table
rows: 0
modify_date: 2023-10-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Infraestructura`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-29 |
| Columnas | 7 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `InfraestructuraId` | `int` |  | ✓ |
| 2 | `ProspectoId` | `int` | ✓ |  |
| 3 | `Hardware` | `nvarchar(100)` | ✓ |  |
| 4 | `Redes` | `nvarchar(100)` | ✓ |  |
| 5 | `Software` | `nvarchar(100)` | ✓ |  |
| 6 | `Servicios` | `nvarchar(100)` | ✓ |  |
| 7 | `Suministros` | `nvarchar(100)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__Infraest__A5810D824F9680E2` | CLUSTERED · **PK** | InfraestructuraId |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK__Infraestr__Prosp__633CDD53` → `Prospecto`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

