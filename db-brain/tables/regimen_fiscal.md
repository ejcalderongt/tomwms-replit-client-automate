---
id: db-brain-table-regimen-fiscal
type: db-table
title: dbo.regimen_fiscal
schema: dbo
name: regimen_fiscal
kind: table
rows: 0
modify_date: 2021-05-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.regimen_fiscal`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-05-28 |
| Columnas | 4 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdRegimen` | `int` |  |  |
| 2 | `codigo_regimen` | `nvarchar(20)` |  |  |
| 3 | `descripcion` | `nvarchar(500)` | ✓ |  |
| 4 | `dias_vencimiento` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_regimenes` | CLUSTERED · **PK** | IdRegimen |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**7** objetos:

- `VW_Despacho_Rep_Det_I` (view)
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Recepcion_Det` (view)
- `VW_Stock_Jornada` (view)

