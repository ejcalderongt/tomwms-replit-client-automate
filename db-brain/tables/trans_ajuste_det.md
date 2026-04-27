---
id: db-brain-table-trans-ajuste-det
type: db-table
title: dbo.trans_ajuste_det
schema: dbo
name: trans_ajuste_det
kind: table
rows: 2113
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_ajuste_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 2.113 |
| Schema modify_date | 2025-02-11 |
| Columnas | 28 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idajustedet` | `int` |  |  |
| 2 | `idajusteenc` | `int` |  |  |
| 3 | `IdStock` | `int` | ✓ |  |
| 4 | `IdPropietarioBodega` | `int` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `IdUnidadMedida` | `int` | ✓ |  |
| 9 | `IdUbicacion` | `int` | ✓ |  |
| 10 | `lote_original` | `nvarchar(50)` | ✓ |  |
| 11 | `lote_nuevo` | `nvarchar(50)` | ✓ |  |
| 12 | `fecha_vence_original` | `datetime` | ✓ |  |
| 13 | `fecha_vence_nueva` | `datetime` | ✓ |  |
| 14 | `peso_original` | `float` | ✓ |  |
| 15 | `peso_nuevo` | `float` | ✓ |  |
| 16 | `cantidad_original` | `float` | ✓ |  |
| 17 | `cantidad_nueva` | `float` | ✓ |  |
| 18 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 19 | `nombre_producto` | `nvarchar(200)` | ✓ |  |
| 20 | `idtipoajuste` | `int` | ✓ |  |
| 21 | `idmotivoajuste` | `int` | ✓ |  |
| 22 | `observacion` | `nvarchar(300)` | ✓ |  |
| 23 | `codigo_ajuste` | `nvarchar(50)` | ✓ |  |
| 24 | `enviado` | `bit` | ✓ |  |
| 25 | `IdBodegaERP` | `int` | ✓ |  |
| 26 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 27 | `referencia_ajuste_erp` | `nvarchar(50)` |  |  |
| 28 | `estado_ajuste_erp` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_ajuste_det` | CLUSTERED · **PK** | idajustedet |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_ajuste_enc_trans_ajuste_det` → `trans_ajuste_enc`

## Quién la referencia

**2** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `VW_Ajustes` (view)

