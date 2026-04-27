---
id: db-brain-table-trans-inv-reconteo
type: db-table
title: dbo.trans_inv_reconteo
schema: dbo
name: trans_inv_reconteo
kind: table
rows: 0
modify_date: 2019-09-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_reconteo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2019-09-02 |
| Columnas | 22 |
| Índices | 1 |
| FKs | out:3 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinvreconteo` | `int` |  |  |
| 2 | `idreconteo` | `int` |  |  |
| 3 | `idinvciclico` | `int` |  |  |
| 4 | `idinventarioenc` | `int` |  |  |
| 5 | `IdStock` | `int` |  |  |
| 6 | `IdProductoBodega` | `int` |  |  |
| 7 | `IdProductoEstado` | `int` | ✓ |  |
| 8 | `IdPresentacion` | `int` | ✓ |  |
| 9 | `idUbicacionAnterior` | `int` | ✓ |  |
| 10 | `IdUbicacion` | `int` |  |  |
| 11 | `EsNuevo` | `bit` | ✓ |  |
| 12 | `fecha_vence` | `datetime` | ✓ |  |
| 13 | `lote` | `nvarchar(50)` | ✓ |  |
| 14 | `cantidadAnterior` | `float` | ✓ |  |
| 15 | `cantidad` | `float` |  |  |
| 16 | `pesoAnterior` | `float` | ✓ |  |
| 17 | `peso` | `float` | ✓ |  |
| 18 | `user_agr` | `nvarchar(50)` |  |  |
| 19 | `fec_agr` | `datetime` |  |  |
| 20 | `IdOperador` | `int` | ✓ |  |
| 21 | `EsPallet` | `bit` | ✓ |  |
| 22 | `lic_plate` | `nvarchar(100)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_inv_reconteo` | CLUSTERED · **PK** | idinvreconteo |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_inv_reconteo_producto_bodega` → `producto_bodega`
- `FK_trans_inv_reconteo_trans_inv_ciclico` → `trans_inv_ciclico`
- `FK_trans_inv_reconteo_trans_inv_enc` → `trans_inv_enc`

## Quién la referencia

**3** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

