---
id: db-brain-view-vw-get-pedido-det
type: db-view
title: dbo.VW_Get_Pedido_Det
schema: dbo
name: VW_Get_Pedido_Det
kind: view
modify_date: 2022-06-24
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Get_Pedido_Det`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-24 |
| Columnas | 45 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedidoDet` | `int` |  |  |
| 2 | `IdPedidoEnc` | `int` |  |  |
| 3 | `IdProductoBodega` | `int` |  |  |
| 4 | `IdEstado` | `int` |  |  |
| 5 | `IdPresentacion` | `int` | ✓ |  |
| 6 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 7 | `Cantidad` | `float` | ✓ |  |
| 8 | `Peso` | `float` | ✓ |  |
| 9 | `Precio` | `float` | ✓ |  |
| 10 | `no_recepcion` | `bigint` | ✓ |  |
| 11 | `ndias` | `int` | ✓ |  |
| 12 | `cant_despachada` | `float` | ✓ |  |
| 13 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 14 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 15 | `nom_presentacion` | `nvarchar(50)` | ✓ |  |
| 16 | `nom_unid_med` | `nvarchar(50)` | ✓ |  |
| 17 | `nom_estado` | `nvarchar(50)` | ✓ |  |
| 18 | `user_agr` | `nvarchar(30)` | ✓ |  |
| 19 | `fec_agr` | `datetime` | ✓ |  |
| 20 | `fecha_especifica` | `bit` | ✓ |  |
| 21 | `RoadDes` | `float` | ✓ |  |
| 22 | `RoadDesMon` | `float` | ✓ |  |
| 23 | `RoadTotal` | `float` | ✓ |  |
| 24 | `RoadPrecioDoc` | `float` | ✓ |  |
| 25 | `RoadVAL1` | `float` | ✓ |  |
| 26 | `RoadVAL2` | `nvarchar(50)` | ✓ |  |
| 27 | `RoadCantProc` | `float` | ✓ |  |
| 28 | `peso_despachado` | `float` | ✓ |  |
| 29 | `no_linea` | `int` | ✓ |  |
| 30 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 31 | `IdStockEspecifico` | `int` | ✓ |  |
| 32 | `EsPadre` | `bit` | ✓ |  |
| 33 | `IdPedidoDetPadre` | `int` | ✓ |  |
| 34 | `Peso_Bruto` | `float` | ✓ |  |
| 35 | `Peso_Neto` | `float` | ✓ |  |
| 36 | `Costo` | `float` | ✓ |  |
| 37 | `valor_aduana` | `float` | ✓ |  |
| 38 | `valor_fob` | `float` | ✓ |  |
| 39 | `valor_iva` | `float` | ✓ |  |
| 40 | `valor_dai` | `float` | ✓ |  |
| 41 | `valor_seguro` | `float` | ✓ |  |
| 42 | `valor_flete` | `float` | ✓ |  |
| 43 | `Total_linea` | `float` | ✓ |  |
| 44 | `IdCliente` | `int` | ✓ |  |
| 45 | `IdProducto` | `int` | ✓ |  |

## Consume

- `producto_bodega`
- `trans_pe_det`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Get_Pedido_Det]
AS
SELECT det.IdPedidoDet, det.IdPedidoEnc, det.IdProductoBodega, det.IdEstado, det.IdPresentacion, det.IdUnidadMedidaBasica, det.Cantidad, det.Peso, det.Precio, det.no_recepcion, det.ndias, det.cant_despachada, det.codigo_producto,
det.nombre_producto, det.nom_presentacion, det.nom_unid_med, det.nom_estado, det.user_agr, det.fec_agr, det.fecha_especifica, det.RoadDes, det.RoadDesMon, det.RoadTotal, det.RoadPrecioDoc, det.RoadVAL1, det.RoadVAL2,
det.RoadCantProc, det.peso_despachado, det.no_linea, det.atributo_variante_1, det.IdStockEspecifico, det.EsPadre, det.IdPedidoDetPadre, det.Peso_Bruto, det.Peso_Neto, det.Costo, det.valor_aduana, det.valor_fob, det.valor_iva,
det.valor_dai, det.valor_seguro, det.valor_flete, det.Total_linea, det.IdCliente, pb.IdProducto
FROM dbo.trans_pe_det AS det INNER JOIN
dbo.producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
```
