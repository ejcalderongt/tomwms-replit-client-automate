---
id: db-brain-table-trans-inv-resumen
type: db-table
title: dbo.trans_inv_resumen
schema: dbo
name: trans_inv_resumen
kind: table
rows: 2571
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_resumen`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 2.571 |
| Schema modify_date | 2024-09-12 |
| Columnas | 16 |
| Índices | 3 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventariores` | `int` |  |  |
| 2 | `idinventarioenct` | `int` |  |  |
| 3 | `idtramo` | `int` |  |  |
| 4 | `idproducto` | `int` |  |  |
| 5 | `idoperador` | `int` | ✓ |  |
| 6 | `IdUnidadMedida` | `int` | ✓ |  |
| 7 | `idpresentacion` | `int` | ✓ |  |
| 8 | `idproductoestado` | `int` | ✓ |  |
| 9 | `cantidad` | `float` | ✓ |  |
| 10 | `fecha_captura` | `datetime` | ✓ |  |
| 11 | `host` | `nvarchar(50)` | ✓ |  |
| 12 | `nom_producto` | `nvarchar(250)` | ✓ |  |
| 13 | `nom_operador` | `nvarchar(50)` | ✓ |  |
| 14 | `idbodega` | `int` |  |  |
| 15 | `idubicacion` | `int` |  |  |
| 16 | `lic_plate` | `nvarchar(100)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_ini_resumen` | CLUSTERED · **PK** | idinventariores |
| `NCLI_trans_inv_resumen_20220508` | NONCLUSTERED | idproducto, idoperador, idpresentacion, cantidad, nom_operador, idinventarioenct, idtramo |
| `NCLI_20220508_Inv_Resumen` | NONCLUSTERED | idtramo, idproducto, idoperador, idpresentacion, cantidad, nom_operador, idinventarioenct |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_inv_ini_resumen_inv_enc` → `trans_inv_enc`

## Quién la referencia

**3** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

