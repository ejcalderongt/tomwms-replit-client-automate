---
id: db-brain-table-trans-despacho-det
type: db-table
title: dbo.trans_despacho_det
schema: dbo
name: trans_despacho_det
kind: table
rows: 19799
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_despacho_det`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 19.799 |
| Schema modify_date | 2025-02-11 |
| Columnas | 20 |
| Índices | 4 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDespachoDet` | `int` |  |  |
| 2 | `IdDespachoEnc` | `int` |  |  |
| 3 | `IdPickingUbic` | `int` |  |  |
| 4 | `Fecha` | `datetime` |  |  |
| 5 | `user_agr` | `nvarchar(50)` |  |  |
| 6 | `fec_agr` | `datetime` |  |  |
| 7 | `user_mod` | `nvarchar(50)` |  |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |
| 9 | `activo` | `bit` |  |  |
| 10 | `IdPedidoEnc` | `int` | ✓ |  |
| 11 | `IdPedidoDet` | `int` | ✓ |  |
| 12 | `IdProductoBodega` | `int` | ✓ |  |
| 13 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 14 | `IdPresentacion` | `int` | ✓ |  |
| 15 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 16 | `NombreProducto` | `nvarchar(250)` | ✓ |  |
| 17 | `NombreEstado` | `nvarchar(50)` | ✓ |  |
| 18 | `CantidadDespachada` | `float` | ✓ |  |
| 19 | `PesoDespachado` | `float` | ✓ |  |
| 20 | `IdProductoEstado` | `int` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_despacho_det` | CLUSTERED · **PK** | IdDespachoDet |
| `NCLI_trans_despacho_det_20210908_EJC` | NONCLUSTERED | IdDespachoDet, IdDespachoEnc, IdPedidoEnc, CantidadDespachada, PesoDespachado, IdPickingUbic, IdProductoBodega, IdUnidadMedidaBasica |
| `NCLI_Trans_Despacho_Det_20220322_EJC` | NONCLUSTERED | IdPickingUbic, Fecha, IdUnidadMedidaBasica, IdPresentacion, Codigo, NombreEstado, CantidadDespachada, PesoDespachado, IdDespachoEnc, IdPedidoEnc, IdProductoBodega |
| `NCI_CKFK_20250203_DespachoDet` | NONCLUSTERED | IdDespachoEnc, IdPickingUbic, user_agr, fec_agr, IdPedidoDet, IdProductoBodega, NombreEstado, CantidadDespachada, IdPedidoEnc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_trans_despacho_det_trans_despacho_enc` → `trans_despacho_enc`
- `FK_trans_despacho_det_trans_picking_ubic` → `trans_picking_ubic`

## Quién la referencia

**18** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `CLBD_PRC` (stored_procedure)
- `CLBD_PRC_BY_IDBODEGA` (stored_procedure)
- `VW_Despacho` (view)
- `VW_Despacho_Detalle` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `vw_Indicador_Despachos` (view)
- `VW_Lotes_Despacho` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_N1` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_Tiempos_Despacho_By_IdPedidoEnc` (view)

