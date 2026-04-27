---
id: db-brain-table-producto-pallet-rec
type: db-table
title: dbo.producto_pallet_rec
schema: dbo
name: producto_pallet_rec
kind: table
rows: 0
modify_date: 2018-02-24
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.producto_pallet_rec`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-02-24 |
| Columnas | 20 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPallet` | `int` |  |  |
| 2 | `IdPropietarioBodega` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdPresentacion` | `int` |  |  |
| 5 | `IdOperadorBodega` | `int` | ✓ |  |
| 6 | `IdImpresora` | `int` | ✓ |  |
| 7 | `IdRecepcionEnc` | `int` |  |  |
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
| 20 | `IdRecepcionDet` | `int` |  |  |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

