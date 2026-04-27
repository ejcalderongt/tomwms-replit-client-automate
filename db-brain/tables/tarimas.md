---
id: db-brain-table-tarimas
type: db-table
title: dbo.tarimas
schema: dbo
name: tarimas
kind: table
rows: 4
modify_date: 2016-07-20
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tarimas`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4 |
| Schema modify_date | 2016-07-20 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:2 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTarima` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdTipoTarima` | `int` | ✓ |  |
| 4 | `codigo` | `nvarchar(50)` | ✓ |  |
| 5 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 6 | `fec_agr` | `datetime` |  |  |
| 7 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |
| 10 | `disponible` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tarimas` | CLUSTERED · **PK** | IdTarima |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_tarimas_empresa` → `empresa`
- `FK_tarimas_tipo_tarima` → `tipo_tarima`

### Entrantes (otra tabla → esta)

- `trans_ubic_tarima` (`FK_trans_ubic_tarima_tarimas`)

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `VW_Tarimas` (view)
- `VW_TarimasUsadasEnTransaccion` (view)

