---
id: db-brain-table-empresa-transporte
type: db-table
title: dbo.empresa_transporte
schema: dbo
name: empresa_transporte
kind: table
rows: 18
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.empresa_transporte`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 18 |
| Schema modify_date | 2021-05-28 |
| Columnas | 8 |
| Índices | 1 |
| FKs | out:1 in:4 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmpresaTransporte` | `int` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 3 | `nombre` | `nvarchar(100)` | ✓ |  |
| 4 | `activo` | `bit` | ✓ |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_empresa_transporte_1` | CLUSTERED · **PK** | IdEmpresaTransporte |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_empresa_transporte_empresa` → `empresa`

### Entrantes (otra tabla → esta)

- `empresa_transporte_bodega` (`FK_empresa_transporte_bodega_empresa_transporte`)
- `empresa_transporte_pilotos` (`FK_empresa_transporte_pilotos_empresa_transporte`)
- `empresa_transporte_vehiculos` (`FK_empresa_transporte_vehiculos_empresa_transporte`)
- `tms_ticket` (`FK_tms_ticket_empresa_transporte`)

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `VW_TMS_Tikcet` (view)

