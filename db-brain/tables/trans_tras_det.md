---
id: db-brain-table-trans-tras-det
type: db-table
title: dbo.trans_tras_det
schema: dbo
name: trans_tras_det
kind: table
rows: 0
modify_date: 2018-01-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_tras_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-01-11 |
| Columnas | 17 |
| Índices | 1 |
| FKs | out:4 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTrasladoDet` | `bigint` |  |  |
| 2 | `IdTrasladoEnc` | `int` |  |  |
| 3 | `IdProducto` | `int` |  |  |
| 4 | `IdEstado` | `int` |  |  |
| 5 | `IdPresentacion` | `int` |  |  |
| 6 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 7 | `Cantidad` | `float` | ✓ |  |
| 8 | `Peso` | `float` | ✓ |  |
| 9 | `Precio` | `float` | ✓ |  |
| 10 | `cant_despachada` | `float` | ✓ |  |
| 11 | `nombre_producto` | `nvarchar(50)` | ✓ |  |
| 12 | `nom_presentacion` | `nvarchar(50)` | ✓ |  |
| 13 | `nom_unid_med` | `nvarchar(50)` | ✓ |  |
| 14 | `nom_estado` | `nvarchar(50)` | ✓ |  |
| 15 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |
| 17 | `fecha_especifica` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_tras_det` | CLUSTERED · **PK** | IdTrasladoDet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_tras_det_producto` → `producto`
- `FK_trans_tras_det_producto_estado` → `producto_estado`
- `FK_trans_tras_det_producto_presentacion` → `producto_presentacion`
- `FK_trans_tras_det_trans_tras_enc` → `trans_tras_enc`

## Quién la referencia

**3** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)

