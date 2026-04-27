---
id: db-brain-view-vw-lotes-despacho
type: db-view
title: dbo.VW_Lotes_Despacho
schema: dbo
name: VW_Lotes_Despacho
kind: view
modify_date: 2021-10-06
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Lotes_Despacho`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-10-06 |
| Columnas | 27 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPickingUbic` | `int` |  |  |
| 2 | `IdStock` | `int` | ✓ |  |
| 3 | `IdPedidoDet` | `int` | ✓ |  |
| 4 | `IdPropietarioBodega` | `int` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `IdProductoEstado` | `int` | ✓ |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `IdUnidadMedida` | `int` | ✓ |  |
| 9 | `IdRecepcion` | `bigint` | ✓ |  |
| 10 | `lote` | `nvarchar(35)` | ✓ |  |
| 11 | `Vence` | `datetime` | ✓ |  |
| 12 | `cantidad_pickeada` | `float` |  |  |
| 13 | `cantidad_verificada` | `float` |  |  |
| 14 | `Peso_Pickeado` | `float` |  |  |
| 15 | `Peso_Verificado` | `float` |  |  |
| 16 | `Encontrado` | `bit` |  |  |
| 17 | `Acepto` | `bit` |  |  |
| 18 | `No_Documento_WMS` | `bigint` | ✓ |  |
| 19 | `No_Referencia` | `nvarchar(25)` | ✓ |  |
| 20 | `Codigo_Cliente` | `nvarchar(150)` | ✓ |  |
| 21 | `Nombre_Cliente` | `nvarchar(150)` | ✓ |  |
| 22 | `idubicacionvirtual` | `int` | ✓ |  |
| 23 | `es_bodega_recepcion` | `bit` | ✓ |  |
| 24 | `es_bodega_traslado` | `bit` | ✓ |  |
| 25 | `IdCliente` | `int` |  |  |
| 26 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 27 | `fecha_despacho` | `datetime` | ✓ |  |

## Consume

- `cliente`
- `trans_despacho_det`
- `trans_despacho_enc`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Lotes_Despacho]
AS
SELECT        dbo.trans_picking_ubic.IdPickingUbic, dbo.trans_picking_ubic.IdStock, dbo.trans_picking_ubic.IdPedidoDet, dbo.trans_picking_ubic.IdPropietarioBodega, dbo.trans_picking_ubic.IdProductoBodega, 
                         dbo.trans_picking_ubic.IdProductoEstado, dbo.trans_picking_ubic.IdPresentacion, dbo.trans_picking_ubic.IdUnidadMedida, dbo.trans_picking_ubic.IdRecepcion, dbo.trans_picking_ubic.lote, 
                         dbo.trans_picking_ubic.fecha_vence AS Vence, ISNULL(dbo.trans_picking_ubic.cantidad_recibida, 0) AS cantidad_pickeada, ISNULL(dbo.trans_picking_ubic.cantidad_verificada, 0) AS cantidad_verificada, 
                         ISNULL(dbo.trans_picking_ubic.peso_recibido, 0) AS Peso_Pickeado, ISNULL(dbo.trans_picking_ubic.peso_verificado, 0) AS Peso_Verificado, ISNULL(dbo.trans_picking_ubic.encontrado, 0) AS Encontrado, 
                         ISNULL(dbo.trans_picking_ubic.acepto, 0) AS Acepto, dbo.trans_pe_enc.no_documento AS No_Documento_WMS, dbo.trans_pe_enc.referencia AS No_Referencia, dbo.cliente.codigo AS Codigo_Cliente, 
                         dbo.cliente.nombre_comercial AS Nombre_Cliente, dbo.cliente.idubicacionvirtual, dbo.cliente.es_bodega_recepcion, dbo.cliente.es_bodega_traslado, dbo.cliente.IdCliente, dbo.trans_pe_enc.Fecha_Pedido, 
                         dbo.trans_despacho_enc.fec_agr AS fecha_despacho
FROM            dbo.trans_despacho_det INNER JOIN
                         dbo.trans_picking_ubic ON dbo.trans_despacho_det.IdPickingUbic = dbo.trans_picking_ubic.IdPickingUbic INNER JOIN
                         dbo.trans_despacho_enc ON dbo.trans_despacho_det.IdDespachoEnc = dbo.trans_despacho_enc.IdDespachoEnc INNER JOIN
                         dbo.cliente INNER JOIN
                         dbo.trans_pe_enc ON dbo.cliente.IdCliente = dbo.trans_pe_enc.IdCliente INNER JOIN
                         dbo.trans_pe_det ON dbo.trans_pe_enc.IdPedidoEnc = dbo.trans_pe_det.IdPedidoEnc ON dbo.trans_despacho_det.IdPedidoDet = dbo.trans_pe_det.IdPedidoDet AND 
                         dbo.trans_despacho_det.IdPedidoEnc = dbo.trans_pe_det.IdPedidoEnc
GROUP BY dbo.trans_picking_ubic.peso_recibido, dbo.trans_picking_ubic.peso_verificado, dbo.trans_picking_ubic.acepto, dbo.trans_picking_ubic.cantidad_recibida, dbo.trans_picking_ubic.cantidad_verificada, 
                         dbo.trans_picking_ubic.encontrado, dbo.trans_picking_ubic.IdPickingUbic, dbo.trans_picking_ubic.IdStock, dbo.trans_picking_ubic.IdPedidoDet, dbo.trans_picking_ubic.IdPropietarioBodega, 
                         dbo.trans_picking_ubic.IdProductoBodega, dbo.trans_picking_ubic.IdProductoEstado, dbo.trans_picking_ubic.IdPresentacion, dbo.trans_picking_ubic.IdUnidadMedida, dbo.trans_picking_ubic.IdRecepcion, 
                         dbo.trans_pe_enc.no_documento, dbo.trans_pe_enc.referencia, dbo.cliente.codigo, dbo.cliente.nombre_comercial, dbo.cliente.idubicacionvirtual, dbo.cliente.es_bodega_recepcion, dbo.cliente.es_bodega_traslado, 
                         dbo.trans_picking_ubic.lote, dbo.trans_picking_ubic.fecha_vence, dbo.cliente.IdCliente, dbo.trans_pe_enc.Fecha_Pedido, dbo.trans_despacho_enc.fec_agr
```
