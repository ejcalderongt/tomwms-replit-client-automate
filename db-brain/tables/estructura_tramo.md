---
id: db-brain-table-estructura-tramo
type: db-table
title: dbo.estructura_tramo
schema: dbo
name: estructura_tramo
kind: table
rows: 68
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.estructura_tramo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 68 |
| Schema modify_date | 2023-08-21 |
| Columnas | 25 |
| Índices | 1 |
| FKs | out:0 in:2 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTramo` | `int` |  |  |
| 2 | `IdSector` | `int` |  |  |
| 3 | `sistema` | `bit` | ✓ |  |
| 4 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `alto` | `float` | ✓ |  |
| 11 | `largo` | `float` | ✓ |  |
| 12 | `ancho` | `float` | ✓ |  |
| 13 | `margen_izquierdo` | `float` | ✓ |  |
| 14 | `margen_derecho` | `float` | ✓ |  |
| 15 | `margen_superior` | `float` | ✓ |  |
| 16 | `margen_inferior` | `float` | ✓ |  |
| 17 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 18 | `Indice_x` | `int` | ✓ |  |
| 19 | `Orientacion` | `int` | ✓ |  |
| 20 | `IdTipoProductoDefault` | `int` | ✓ |  |
| 21 | `IdFontEnc` | `int` | ✓ |  |
| 22 | `idbodega` | `int` | ✓ |  |
| 23 | `Horizontal` | `bit` | ✓ |  |
| 24 | `idarea` | `int` | ✓ |  |
| 25 | `orden_descendente` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_estructura_tramo` | CLUSTERED · **PK** | IdTramo |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `estructura_grupo` (`FK_estructura_grupo_estructura_tramo`)
- `estructura_ubicacion` (`FK_estructura_ubicacion_estructura_tramo`)

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

