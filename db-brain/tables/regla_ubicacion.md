---
id: db-brain-table-regla-ubicacion
type: db-table
title: dbo.regla_ubicacion
schema: dbo
name: regla_ubicacion
kind: table
rows: 0
modify_date: 2020-02-04
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubicacion`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-02-04 |
| Columnas | 3 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdUbicacion` | `int` |  |  |
| 2 | `IdReglaUbicacionEnc` | `int` |  |  |
| 3 | `IdBodega` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubicacion` | CLUSTERED · **PK** | IdUbicacion, IdReglaUbicacionEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_regla_ubicacion_regla_ubic_enc` → `regla_ubic_enc`

## Quién la referencia

**2** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `vw_ubicaciones_por_regla` (view)

