---
id: db-brain-table-trans-oc-imagen
type: db-table
title: dbo.trans_oc_imagen
schema: dbo
name: trans_oc_imagen
kind: table
rows: 0
modify_date: 2018-04-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_oc_imagen`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2018-04-12 |
| Columnas | 5 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraImg` | `int` |  |  |
| 2 | `IdOrdenCompraEnc` | `int` |  |  |
| 3 | `Orden` | `int` |  |  |
| 4 | `Imagen` | `image` | ✓ |  |
| 5 | `descripcion` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_orden_compra_imagen` | CLUSTERED · **PK** | IdOrdenCompraImg, IdOrdenCompraEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_orden_compra_imagen_trans_orden_compra_enc` → `trans_oc_enc`

## Quién la referencia

**4** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)

