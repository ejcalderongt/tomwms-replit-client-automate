---
id: db-brain-table-trans-packing-enc
type: db-table
title: dbo.trans_packing_enc
schema: dbo
name: trans_packing_enc
kind: table
rows: 13
modify_date: 2025-07-16
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_packing_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 13 |
| Schema modify_date | 2025-07-16 |
| Columnas | 25 |
| Índices | 3 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idpackingenc` | `int` |  |  |
| 2 | `idbodega` | `int` |  |  |
| 3 | `idpickingenc` | `int` |  |  |
| 4 | `iddespachoenc` | `int` | ✓ |  |
| 5 | `idproductobodega` | `int` |  |  |
| 6 | `idproductoestado` | `int` |  |  |
| 7 | `idpresentacion` | `int` | ✓ |  |
| 8 | `idunidadmedida` | `int` |  |  |
| 9 | `lote` | `nvarchar(50)` | ✓ |  |
| 10 | `fecha_vence` | `date` | ✓ |  |
| 11 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 12 | `no_linea` | `int` |  |  |
| 13 | `cantidad_bultos_packing` | `float` |  |  |
| 14 | `cantidad_camas_packing` | `float` |  |  |
| 15 | `idoperadorbodega` | `int` |  |  |
| 16 | `idempresaservicio` | `int` | ✓ |  |
| 17 | `referencia` | `nvarchar(50)` | ✓ |  |
| 18 | `fecha_packing` | `datetime` |  |  |
| 19 | `IdPedidoEnc` | `int` |  |  |
| 20 | `finalizado` | `bit` |  |  |
| 21 | `IdStock` | `int` | ✓ |  |
| 22 | `fec_agr` | `datetime` |  |  |
| 23 | `usr_agr` | `nvarchar(50)` | ✓ |  |
| 24 | `fec_mod` | `datetime` |  |  |
| 25 | `usr_mod` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_packing_enc` | CLUSTERED · **PK** | idpackingenc |
| `NCI_CKFK_20250203_DespachoDet1` | NONCLUSTERED | no_linea, iddespachoenc, idproductobodega, lic_plate |
| `NCL_CKFK_20250714_Packing` | NONCLUSTERED | idproductobodega, cantidad_bultos_packing, iddespachoenc, IdPedidoEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `VW_Packing` (view)
- `VW_PackingDespachado` (view)

