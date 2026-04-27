---
id: db-brain-table-regla-ubic-sel-det
type: db-table
title: dbo.regla_ubic_sel_det
schema: dbo
name: regla_ubic_sel_det
kind: table
rows: 0
modify_date: 2017-08-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regla_ubic_sel_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2017-08-07 |
| Columnas | 4 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idReglaUbicOrd` | `int` |  |  |
| 2 | `idRegla` | `int` |  |  |
| 3 | `Orden` | `int` | ✓ |  |
| 4 | `Activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regla_ubic_orden_det` | CLUSTERED · **PK** | idReglaUbicOrd, idRegla |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_regla_ubic_sel_det_regla_ubic_sel_enc` → `regla_ubic_sel_enc`

## Quién la referencia

**1** objetos:

- `CLBD_INICIARBD` (stored_procedure)

