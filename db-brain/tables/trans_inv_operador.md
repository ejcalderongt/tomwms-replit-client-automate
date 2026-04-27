---
id: db-brain-table-trans-inv-operador
type: db-table
title: dbo.trans_inv_operador
schema: dbo
name: trans_inv_operador
kind: table
rows: 0
modify_date: 2021-12-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_operador`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-12-01 |
| Columnas | 6 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinvoperador` | `int` |  |  |
| 2 | `idinventarioenc` | `int` |  |  |
| 3 | `idinvencreconteo` | `int` |  |  |
| 4 | `idubic` | `int` |  |  |
| 5 | `idoperador` | `int` |  |  |
| 6 | `idbodega` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_oper_1` | CLUSTERED · **PK** | idinvoperador |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_inv_oper_trans_inv_enc` → `trans_inv_enc`

## Quién la referencia

**5** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Tareas_Operador` (view)
- `VW_Ubicaciones_Inventario_Ciclico` (view)

