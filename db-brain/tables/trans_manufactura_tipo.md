---
id: db-brain-table-trans-manufactura-tipo
type: db-table
title: dbo.trans_manufactura_tipo
schema: dbo
name: trans_manufactura_tipo
kind: table
rows: 0
modify_date: 2024-07-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_manufactura_tipo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-07-02 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoManufactura` | `int` |  |  |
| 2 | `nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `codigo` | `nvarchar(50)` | ✓ |  |
| 4 | `activo` | `bit` |  |  |
| 5 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_manufactura_tipo` | CLUSTERED · **PK** | IdTipoManufactura |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `trans_manufactura_enc` (`FK_trans_manufactura_enc_trans_manufactura_tipo`)

## Quién la referencia

**4** objetos:

- `VW_Verificacion` (view)
- `VW_Verificacion_Consolidada` (view)
- `VW_Verificacion_Consolidada_LFV` (view)
- `VW_Verificacion_Detallado_Sin_Licencia` (view)

