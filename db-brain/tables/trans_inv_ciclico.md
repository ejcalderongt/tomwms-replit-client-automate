---
id: db-brain-table-trans-inv-ciclico
type: db-table
title: dbo.trans_inv_ciclico
schema: dbo
name: trans_inv_ciclico
kind: table
rows: 0
modify_date: 2025-07-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_ciclico`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-07-16 |
| Columnas | 31 |
| Índices | 2 |
| FKs | out:2 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinvciclico` | `int` |  |  |
| 2 | `idinventarioenc` | `int` |  |  |
| 3 | `IdStock` | `int` |  |  |
| 4 | `IdProductoBodega` | `int` |  |  |
| 5 | `IdProductoEstado` | `int` | ✓ |  |
| 6 | `IdPresentacion` | `int` | ✓ |  |
| 7 | `IdUbicacion` | `int` |  |  |
| 8 | `EsNuevo` | `bit` | ✓ |  |
| 9 | `lote_stock` | `nvarchar(50)` | ✓ |  |
| 10 | `lote` | `nvarchar(50)` | ✓ |  |
| 11 | `fecha_vence_stock` | `datetime` | ✓ |  |
| 12 | `fecha_vence` | `datetime` | ✓ |  |
| 13 | `cant_stock` | `float` | ✓ |  |
| 14 | `cantidad` | `float` |  |  |
| 15 | `cant_reconteo` | `float` | ✓ |  |
| 16 | `peso_stock` | `float` | ✓ |  |
| 17 | `peso` | `float` | ✓ |  |
| 18 | `peso_reconteo` | `float` | ✓ |  |
| 19 | `idoperador` | `int` | ✓ |  |
| 20 | `user_agr` | `nvarchar(50)` |  |  |
| 21 | `fec_agr` | `datetime` |  |  |
| 22 | `IdProductoEst_nuevo` | `int` | ✓ |  |
| 23 | `IdPresentacion_nuevo` | `int` | ✓ |  |
| 24 | `IdUbicacion_nuevo` | `int` | ✓ |  |
| 25 | `EsPallet` | `bit` | ✓ |  |
| 26 | `lic_plate` | `nvarchar(100)` | ✓ |  |
| 27 | `IdUnidadMedida` | `int` | ✓ |  |
| 28 | `IdBodega` | `int` | ✓ |  |
| 29 | `fec_mod` | `datetime` |  |  |
| 30 | `regularizar` | `bit` |  |  |
| 31 | `contado` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_ciclico` | CLUSTERED · **PK** | idinvciclico |
| `NCLI_20241118_Ciclico` | NONCLUSTERED | IdStock, IdProductoBodega, IdProductoEstado, lote_stock, fecha_vence_stock, cant_stock, peso_stock, idinventarioenc, IdProductoEst_nuevo |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_inv_ciclico_producto_bodega` → `producto_bodega`
- `FK_trans_inv_ciclico_trans_inv_enc` → `trans_inv_enc`

### Entrantes (otra tabla → esta)

- `trans_inv_reconteo` (`FK_trans_inv_reconteo_trans_inv_ciclico`)

## Quién la referencia

**10** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Comparativo_NAV_WMS_ConCostos` (view)
- `VW_Conteo_By_Operador` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Inv_Conteo_Operador` (view)
- `VW_Inventario_prg_por_tipo` (view)
- `VW_Trans_Inv_Conteo` (view)
- `VW_Ubicaciones_Inventario_Ciclico` (view)

