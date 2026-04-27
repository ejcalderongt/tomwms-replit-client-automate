---
id: db-brain-table-motivo-anulacion
type: db-table
title: dbo.motivo_anulacion
schema: dbo
name: motivo_anulacion
kind: table
rows: 16
modify_date: 2016-03-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.motivo_anulacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 16 |
| Schema modify_date | 2016-03-07 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:1 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMotivoAnulacion` | `int` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 4 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 5 | `activo` | `bit` | ✓ |  |
| 6 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_motivo_anulacion` | CLUSTERED · **PK** | IdMotivoAnulacion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_motivo_anulacion_empresa` → `empresa`

### Entrantes (otra tabla → esta)

- `motivo_anulacion_bodega` (`FK_motivo_anulacion_bodega_motivo_anulacion`)

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `v_motivo_anulacion` (view)
- `VW_MotivoAnulacion` (view)
- `VW_MotivoAnulacionBodega` (view)

