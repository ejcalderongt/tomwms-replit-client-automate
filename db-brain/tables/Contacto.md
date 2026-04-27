---
id: db-brain-table-contacto
type: db-table
title: dbo.Contacto
schema: dbo
name: Contacto
kind: table
rows: 0
modify_date: 2023-10-29
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Contacto`

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
| 1 | `ContactoId` | `int` |  | ✓ |
| 2 | `ProspectoId` | `int` | ✓ |  |
| 3 | `Nombre` | `nvarchar(100)` | ✓ |  |
| 4 | `Apellido` | `nvarchar(100)` | ✓ |  |
| 5 | `Telefono` | `nvarchar(50)` | ✓ |  |
| 6 | `Correo` | `nvarchar(100)` | ✓ |  |
| 7 | `Puesto` | `nvarchar(100)` | ✓ |  |
| 8 | `ComoSupo` | `nvarchar(200)` | ✓ |  |
| 9 | `ObservacionMensaje` | `nvarchar(max)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__Contacto__8E0F85E88792B202` | CLUSTERED · **PK** | ContactoId |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK__Contacto__Prospe__68F5B6A9` → `Prospecto`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

