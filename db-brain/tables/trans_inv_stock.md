---
id: db-brain-table-trans-inv-stock
type: db-table
title: dbo.trans_inv_stock
schema: dbo
name: trans_inv_stock
kind: table
rows: 4540
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_stock`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 4.540 |
| Schema modify_date | 2025-02-11 |
| Columnas | 33 |
| Índices | 2 |
| FKs | out:2 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventario` | `int` |  |  |
| 2 | `IdStock` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdProductoBodega` | `int` |  |  |
| 5 | `IdProductoEstado` | `int` | ✓ |  |
| 6 | `IdPresentacion` | `int` | ✓ |  |
| 7 | `IdUnidadMedida` | `int` | ✓ |  |
| 8 | `IdUbicacion` | `int` |  |  |
| 9 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 10 | `IdRecepcionEnc` | `int` | ✓ |  |
| 11 | `IdRecepcionDet` | `int` | ✓ |  |
| 12 | `IdPedidoEnc` | `int` | ✓ |  |
| 13 | `IdPickingEnc` | `int` | ✓ |  |
| 14 | `IdDespachoEnc` | `int` | ✓ |  |
| 15 | `lote` | `nvarchar(50)` | ✓ |  |
| 16 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 17 | `serial` | `nvarchar(50)` | ✓ |  |
| 18 | `cantidad` | `float` | ✓ |  |
| 19 | `fecha_ingreso` | `datetime` | ✓ |  |
| 20 | `fecha_vence` | `datetime` | ✓ |  |
| 21 | `uds_lic_plate` | `float` | ✓ |  |
| 22 | `no_bulto` | `nvarchar(20)` | ✓ |  |
| 23 | `fecha_manufactura` | `datetime` | ✓ |  |
| 24 | `añada` | `int` | ✓ |  |
| 25 | `user_agr` | `nvarchar(50)` |  |  |
| 26 | `fec_agr` | `datetime` |  |  |
| 27 | `user_mod` | `nvarchar(50)` |  |  |
| 28 | `fec_mod` | `datetime` |  |  |
| 29 | `activo` | `bit` |  |  |
| 30 | `peso` | `float` | ✓ |  |
| 31 | `temperatura` | `float` | ✓ |  |
| 32 | `fecha_copia` | `datetime` | ✓ |  |
| 33 | `IdBodega` | `int` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_inv_stock` | CLUSTERED · **PK** | idinventario, IdStock |
| `NCLI_EJC_241211` | NONCLUSTERED | IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion, lote, cantidad, fecha_vence, peso, IdBodega, IdProductoBodega |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_inv_ini_stock_inv_enc` → `trans_inv_enc`
- `FK_inv_ini_stock_inv_ini_stock` → `trans_inv_stock`

### Entrantes (otra tabla → esta)

- `trans_inv_stock` (`FK_inv_ini_stock_inv_ini_stock`)

## Quién la referencia

**6** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Comparativo_NAV_WMS_ConCostos` (view)
- `VW_Trans_Inv_Conteo` (view)
- `VW_Trans_Inv_Stock` (view)

