---
id: db-brain-table-producto-pallet
type: db-table
title: dbo.producto_pallet
schema: dbo
name: producto_pallet
kind: table
rows: 61
modify_date: 2023-02-14
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_pallet`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 61 |
| Schema modify_date | 2023-02-14 |
| Columnas | 21 |
| Índices | 1 |
| FKs | out:7 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPallet` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdPresentacion` | `int` | ✓ |  |
| 5 | `IdOperadorBodega` | `int` | ✓ |  |
| 6 | `IdImpresora` | `int` | ✓ |  |
| 7 | `IdRecepcionEnc` | `int` | ✓ |  |
| 8 | `codigo_barra` | `nvarchar(35)` |  |  |
| 9 | `cantidad` | `float` | ✓ |  |
| 10 | `lote` | `nvarchar(25)` | ✓ |  |
| 11 | `Impreso` | `bit` | ✓ |  |
| 12 | `Reimpresiones` | `int` | ✓ |  |
| 13 | `fecha_vence` | `datetime` | ✓ |  |
| 14 | `fecha_ingreso` | `datetime` | ✓ |  |
| 15 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |
| 17 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 18 | `fec_mod` | `datetime` | ✓ |  |
| 19 | `activo` | `bit` | ✓ |  |
| 20 | `IdRecepcionDet` | `int` | ✓ |  |
| 21 | `codigo_producto` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_producto_pallet` | CLUSTERED · **PK** | IdPallet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_producto_pallet_Impresora` → `impresora`
- `FK_producto_pallet_operador_bodega` → `operador_bodega`
- `FK_producto_pallet_producto_bodega` → `producto_bodega`
- `FK_producto_pallet_producto_presentacion` → `producto_presentacion`
- `FK_producto_pallet_propietario_bodega` → `propietario_bodega`
- `FK_producto_pallet_trans_re_det` → `trans_re_det`
- `FK_producto_pallet_trans_re_enc` → `trans_re_enc`

## Quién la referencia

**5** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Impresion_Pallet` (view)

