---
id: db-brain-table-trans-oc-estado
type: db-table
title: dbo.trans_oc_estado
schema: dbo
name: trans_oc_estado
kind: table
rows: 6
modify_date: 2018-04-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_estado`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2018-04-12 |
| Columnas | 2 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEstadoOC` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_orden_compra_estado` | CLUSTERED · **PK** | IdEstadoOC |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `trans_oc_enc` (`FK_trans_oc_enc_trans_oc_estado`)

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `VW_Doc_Con_Diferencias` (view)
- `VW_Ingreso_Fiscal` (view)
- `VW_OrdenCompra` (view)

