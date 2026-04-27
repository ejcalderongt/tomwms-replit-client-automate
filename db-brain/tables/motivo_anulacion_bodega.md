---
id: db-brain-table-motivo-anulacion-bodega
type: db-table
title: dbo.motivo_anulacion_bodega
schema: dbo
name: motivo_anulacion_bodega
kind: table
rows: 25
modify_date: 2016-03-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.motivo_anulacion_bodega`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 25 |
| Schema modify_date | 2016-03-07 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMotivoAnulacionBodega` | `int` |  |  |
| 2 | `IdMotivoAnulacion` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_motivo_anulacion_bodega` | CLUSTERED · **PK** | IdMotivoAnulacionBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_motivo_anulacion_bodega_bodega` → `bodega`
- `FK_motivo_anulacion_bodega_motivo_anulacion` → `motivo_anulacion`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `v_motivo_anulacion` (view)
- `VW_MotivoAnulacionBodega` (view)

