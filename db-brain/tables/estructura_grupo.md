---
id: db-brain-table-estructura-grupo
type: db-table
title: dbo.estructura_grupo
schema: dbo
name: estructura_grupo
kind: table
rows: 238
modify_date: 2023-08-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.estructura_grupo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 238 |
| Schema modify_date | 2023-08-21 |
| Columnas | 14 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdGrupo` | `int` |  |  |
| 2 | `IdTramo` | `int` | ✓ |  |
| 3 | `pos` | `int` | ✓ |  |
| 4 | `cant` | `int` | ✓ |  |
| 5 | `tamano` | `int` | ✓ |  |
| 6 | `offset` | `int` | ✓ |  |
| 7 | `ancho` | `float` | ✓ |  |
| 8 | `alto` | `float` | ✓ |  |
| 9 | `largo` | `float` | ✓ |  |
| 10 | `palet` | `int` |  |  |
| 11 | `orient` | `int` | ✓ |  |
| 12 | `agrupacion` | `int` | ✓ |  |
| 13 | `IdBodega` | `int` | ✓ |  |
| 14 | `orden_descendente` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_estructura_grupos` | CLUSTERED · **PK** | IdGrupo |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_estructura_grupo_estructura_tramo` → `estructura_tramo`

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

