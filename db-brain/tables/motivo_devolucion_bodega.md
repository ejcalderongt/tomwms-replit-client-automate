---
id: db-brain-table-motivo-devolucion-bodega
type: db-table
title: dbo.motivo_devolucion_bodega
schema: dbo
name: motivo_devolucion_bodega
kind: table
rows: 12
modify_date: 2016-06-10
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.motivo_devolucion_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 12 |
| Schema modify_date | 2016-06-10 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMotivoDevolucionBodega` | `int` |  |  |
| 2 | `IdMotivoDevolucion` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_motivo_devolucion_bodega` | CLUSTERED · **PK** | IdMotivoDevolucionBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_motivo_devolucion_bodega_bodega` → `bodega`
- `FK_motivo_devolucion_bodega_motivo_devolucion` → `motivo_devolucion`

## Quién la referencia

**1** objetos:

- `CLBD` (stored_procedure)

