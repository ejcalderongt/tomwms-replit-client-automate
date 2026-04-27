---
id: db-brain-table-trans-inv-enc
type: db-table
title: dbo.trans_inv_enc
schema: dbo
name: trans_inv_enc
kind: table
rows: 3
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_inv_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 3 |
| Schema modify_date | 2024-09-12 |
| Columnas | 28 |
| Índices | 1 |
| FKs | out:0 in:8 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventarioenc` | `int` |  |  |
| 2 | `idpropietario` | `int` | ✓ |  |
| 3 | `idbodega` | `int` | ✓ |  |
| 4 | `idtipoinventario` | `int` | ✓ |  |
| 5 | `tipo_conteo_producto` | `int` | ✓ |  |
| 6 | `doble_verificacion` | `bit` | ✓ |  |
| 7 | `fecha` | `datetime` | ✓ |  |
| 8 | `estado` | `nvarchar(20)` | ✓ |  |
| 9 | `inicial` | `bit` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |
| 11 | `regularizado` | `bit` | ✓ |  |
| 12 | `hora_ini` | `datetime` | ✓ |  |
| 13 | `hora_fin` | `datetime` | ✓ |  |
| 14 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 15 | `fec_agr` | `datetime` | ✓ |  |
| 16 | `user_mod` | `nvarchar(30)` | ✓ |  |
| 17 | `fec_mod` | `datetime` | ✓ |  |
| 18 | `EsSistema` | `bit` | ✓ |  |
| 19 | `cambia_ubicacion` | `bit` | ✓ |  |
| 20 | `fecha_ultimo_inventario` | `date` | ✓ |  |
| 21 | `mostrar_cantidad_teorica_hh` | `bit` | ✓ |  |
| 22 | `IdProductoFamilia` | `int` | ✓ |  |
| 23 | `IdBodegaVirtual` | `int` | ✓ |  |
| 24 | `capturar_no_existente` | `bit` | ✓ |  |
| 25 | `multi_propietario` | `bit` | ✓ |  |
| 26 | `IdCentroCosto` | `int` | ✓ |  |
| 27 | `tipo_asignacion` | `int` |  |  |
| 28 | `capturar_no_asignados` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_inv_enc` | CLUSTERED · **PK** | idinventarioenc |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `trans_inv_ciclico` (`FK_trans_inv_ciclico_trans_inv_enc`)
- `trans_inv_detalle` (`FK_inv_ini_detalle_inv_enc`)
- `trans_inv_enc_reconteo` (`FK_trans_inv_enc_reconteo_trans_inv_enc`)
- `trans_inv_operador` (`FK_trans_inv_oper_trans_inv_enc`)
- `trans_inv_reconteo` (`FK_trans_inv_reconteo_trans_inv_enc`)
- `trans_inv_resumen` (`FK_inv_ini_resumen_inv_enc`)
- `trans_inv_stock` (`FK_inv_ini_stock_inv_enc`)
- `trans_inv_tramo` (`FK_inv_ini_tramo_inv_enc`)

## Quién la referencia

**6** objetos:

- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Inv_Conteo_Operador` (view)
- `VW_Tareas_Operador` (view)
- `VW_Trans_Inv_Conteo` (view)

