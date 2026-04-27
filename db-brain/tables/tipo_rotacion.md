---
id: db-brain-table-tipo-rotacion
type: db-table
title: dbo.tipo_rotacion
schema: dbo
name: tipo_rotacion
kind: table
rows: 4
modify_date: 2018-06-03
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tipo_rotacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4 |
| Schema modify_date | 2018-06-03 |
| Columnas | 3 |
| Índices | 1 |
| FKs | out:0 in:3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoRotacion` | `int` |  |  |
| 2 | `Descripcion` | `nvarchar(50)` |  |  |
| 3 | `Activo` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tipo_rotacion` | CLUSTERED · **PK** | IdTipoRotacion |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `bodega_ubicacion` (`FK_bodega_ubicacion_tipo_rotacion`)
- `producto` (`FK_producto_tipo_rotacion`)
- `regla_ubic_det_tr` (`FK_regla_ubic_det_tr_tipo_rotacion`)

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

