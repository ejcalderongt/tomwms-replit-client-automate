---
id: db-brain-table-motivo-ubicacion
type: db-table
title: dbo.motivo_ubicacion
schema: dbo
name: motivo_ubicacion
kind: table
rows: 12
modify_date: 2016-05-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.motivo_ubicacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 12 |
| Schema modify_date | 2016-05-06 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmpresa` | `int` |  |  |
| 2 | `IdMotivoUbicacion` | `int` |  |  |
| 3 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 5 | `fec_agr` | `datetime` | ✓ |  |
| 6 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 7 | `fec_mod` | `datetime` | ✓ |  |
| 8 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_motivo_ubicacion` | CLUSTERED · **PK** | IdMotivoUbicacion |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_motivo_ubicacion_empresa` → `empresa`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_TransUbicacionHhEnc` (view)

