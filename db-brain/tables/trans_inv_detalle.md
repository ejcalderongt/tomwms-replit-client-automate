---
id: db-brain-table-trans-inv-detalle
type: db-table
title: dbo.trans_inv_detalle
schema: dbo
name: trans_inv_detalle
kind: table
rows: 2942
modify_date: 2023-01-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_detalle`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 2.942 |
| Schema modify_date | 2023-01-25 |
| Columnas | 28 |
| Índices | 3 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventariodet` | `int` |  | ✓ |
| 2 | `idinventarioenc` | `int` |  |  |
| 3 | `idtramo` | `int` | ✓ |  |
| 4 | `IdUbicacion` | `int` |  |  |
| 5 | `idoperador` | `int` | ✓ |  |
| 6 | `idproducto` | `int` | ✓ |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `idunidadmedida` | `int` | ✓ |  |
| 9 | `lote` | `nvarchar(50)` | ✓ |  |
| 10 | `fecha_vence` | `date` | ✓ |  |
| 11 | `serie` | `nvarchar(50)` | ✓ |  |
| 12 | `idproductoestado` | `nchar(10)` | ✓ |  |
| 13 | `cantidad` | `float` | ✓ |  |
| 14 | `fecha_captura` | `datetime` | ✓ |  |
| 15 | `host` | `nvarchar(50)` | ✓ |  |
| 16 | `nom_producto` | `nvarchar(250)` | ✓ |  |
| 17 | `nom_operador` | `nvarchar(50)` | ✓ |  |
| 18 | `carga` | `int` | ✓ |  |
| 19 | `peso` | `float` | ✓ |  |
| 20 | `IdPropietarioBodega` | `int` | ✓ |  |
| 21 | `nombre_propietario` | `nvarchar(150)` | ✓ |  |
| 22 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 23 | `cod_variante` | `nvarchar(50)` | ✓ |  |
| 24 | `idbodega` | `int` | ✓ |  |
| 25 | `costo` | `float` | ✓ |  |
| 26 | `precio` | `float` | ✓ |  |
| 27 | `IdProductoParametroA` | `int` | ✓ |  |
| 28 | `IdProductoParametroB` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_inv_ini_detalle_1` | CLUSTERED · **PK** | idinventariodet |
| `NCLI_INV_DET_EJC_20220508` | NONCLUSTERED | idtramo, idoperador, idproducto, IdPresentacion, cantidad, nom_operador, idinventarioenc |
| `NCLI_inv_DET_EJC_20220508A` | NONCLUSTERED | idoperador, idproducto, IdPresentacion, cantidad, nom_operador, idinventarioenc, idtramo |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_inv_ini_detalle_inv_enc` → `trans_inv_enc`

## Quién la referencia

**3** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

