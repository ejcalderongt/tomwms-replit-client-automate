---
id: db-brain-table-perfil-serializado
type: db-table
title: dbo.perfil_serializado
schema: dbo
name: perfil_serializado
kind: table
rows: 0
modify_date: 2018-01-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.perfil_serializado`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-01-11 |
| Columnas | 3 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPerfilSerializado` | `int` |  |  |
| 2 | `descripcion` | `varchar(50)` |  |  |
| 3 | `activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_perfil_serializado` | CLUSTERED · **PK** | IdPerfilSerializado |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `producto` (`FK_producto_perfil_serializado`)

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)

