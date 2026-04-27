---
id: db-brain-table-trans-inventario-det
type: db-table
title: dbo.trans_inventario_det
schema: dbo
name: trans_inventario_det
kind: table
rows: 0
modify_date: 2020-02-14
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inventario_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2020-02-14 |
| Columnas | 21 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdInventarioDet` | `int` |  |  |
| 2 | `IdInventarioEnc` | `bigint` |  |  |
| 3 | `IdStock` | `int` | ✓ |  |
| 4 | `IdProducto` | `int` |  |  |
| 5 | `IdUbicacion` | `int` |  |  |
| 6 | `IdEstado` | `int` | ✓ |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `IdUnidadMedida` | `int` | ✓ |  |
| 9 | `lote` | `nvarchar(50)` |  |  |
| 10 | `lic_plate` | `int` |  |  |
| 11 | `serial` | `nvarchar(50)` |  |  |
| 12 | `cantidad` | `float` | ✓ |  |
| 13 | `peso` | `float` | ✓ |  |
| 14 | `conteo` | `float` | ✓ |  |
| 15 | `estado` | `nvarchar(20)` | ✓ |  |
| 16 | `fecha_ingreso` | `datetime` | ✓ |  |
| 17 | `fecha_vence` | `datetime` | ✓ |  |
| 18 | `ubicacion_ant` | `nvarchar(25)` | ✓ |  |
| 19 | `no_bulto` | `nvarchar(20)` | ✓ |  |
| 20 | `recuento` | `float` | ✓ |  |
| 21 | `inicial` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inventario_det` | CLUSTERED · **PK** | IdInventarioDet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_inventario_det_stock` → `stock`
- `FK_trans_inventario_det_trans_inventario_enc` → `trans_inventario_enc`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

