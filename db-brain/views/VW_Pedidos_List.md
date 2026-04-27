---
id: db-brain-view-vw-pedidos-list
type: db-view
title: dbo.VW_Pedidos_List
schema: dbo
name: VW_Pedidos_List
kind: view
modify_date: 2025-07-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Pedidos_List`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-07-11 |
| Columnas | 33 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Correlativo` | `int` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `no_documento` | `bigint` | ✓ |  |
| 4 | `referencia` | `nvarchar(25)` | ✓ |  |
| 5 | `Referencia2` | `nvarchar(50)` | ✓ |  |
| 6 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 7 | `Cliente` | `nvarchar(303)` | ✓ |  |
| 8 | `estado` | `nvarchar(20)` | ✓ |  |
| 9 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 10 | `Muelle` | `nvarchar(50)` | ✓ |  |
| 11 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 12 | `RoadVendedor` | `nvarchar(61)` | ✓ |  |
| 13 | `RoadRuta` | `nvarchar(68)` | ✓ |  |
| 14 | `Fecha` | `datetime` | ✓ |  |
| 15 | `anulado` | `bit` | ✓ |  |
| 16 | `activo` | `bit` | ✓ |  |
| 17 | `Enviado_A_ERP` | `bit` | ✓ |  |
| 18 | `fec_agr` | `datetime` | ✓ |  |
| 19 | `IdPickingEnc` | `int` | ✓ |  |
| 20 | `TipoDocumento` | `nvarchar(250)` | ✓ |  |
| 21 | `IdDespachoEnc` | `bigint` | ✓ |  |
| 22 | `Observacion` | `nvarchar(255)` | ✓ |  |
| 23 | `RutaDespacho` | `nvarchar(66)` | ✓ |  |
| 24 | `No_Picking_ERP` | `nvarchar(50)` | ✓ |  |
| 25 | `no_documento_externo` | `nvarchar(50)` | ✓ |  |
| 26 | `bodega_origen` | `nvarchar(50)` | ✓ |  |
| 27 | `bodega_destino` | `nvarchar(50)` | ✓ |  |
| 28 | `IdPrioridadPicking` | `int` | ✓ |  |
| 29 | `Codigo_Empresa_ERP` | `nvarchar(50)` | ✓ |  |
| 30 | `esexportacion` | `bit` |  |  |
| 31 | `Direccion` | `nvarchar(255)` | ✓ |  |
| 32 | `fec_mod` | `datetime` | ✓ |  |
| 33 | `Ult_Despacho` | `bigint` | ✓ |  |

## Consume

- `bodega`
- `bodega_muelles`
- `cliente`
- `propietario_bodega`
- `propietarios`
- `road_p_vendedor`
- `road_ruta`
- `trans_pe_enc`
- `trans_pe_tipo`
- `trans_picking_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Pedidos_List]
AS
SELECT dbo.trans_pe_enc.IdPedidoEnc AS Correlativo, dbo.trans_pe_enc.IdBodega, dbo.trans_pe_enc.no_documento, dbo.trans_pe_enc.referencia, dbo.trans_pe_enc.Referencia_Documento_Ingreso_Bodega_Destino AS Referencia2, 
                  dbo.trans_pe_enc.Fecha_Pedido, dbo.cliente.codigo + ' - ' + dbo.cliente.nombre_comercial AS Cliente, dbo.trans_pe_enc.estado, dbo.bodega.nombre AS Bodega, dbo.bodega_muelles.nombre AS Muelle, 
                  dbo.propietarios.nombre_comercial AS Propietario, dbo.road_p_vendedor.codigo + ' - ' + dbo.road_p_vendedor.nombre AS RoadVendedor, dbo.road_ruta.CODIGO + ' - ' + dbo.road_ruta.NOMBRE AS RoadRuta, 
                  dbo.trans_pe_enc.fec_agr AS Fecha, dbo.trans_pe_enc.anulado, dbo.trans_pe_enc.activo, dbo.trans_pe_enc.Enviado_A_ERP, dbo.trans_pe_enc.fec_agr, dbo.trans_pe_enc.IdPickingEnc, 
                  dbo.trans_pe_tipo.Descripcion AS TipoDocumento, dbo.trans_pe_enc.no_despacho AS IdDespachoEnc, dbo.trans_pe_enc.Observacion, Roadruta1.CODIGO + ' ' + Roadruta1.NOMBRE AS RutaDespacho, 
                  dbo.trans_pe_enc.No_Picking_ERP, dbo.trans_pe_enc.no_documento_externo, dbo.trans_pe_enc.bodega_origen, dbo.trans_pe_enc.bodega_destino, dbo.trans_picking_enc.IdPrioridadPicking, 
                  dbo.trans_pe_enc.Codigo_Empresa_ERP, trans_pe_enc.esexportacion, trans_pe_enc.RoadDirEntrega AS Direccion, trans_pe_enc.fec_mod, trans_pe_enc.no_despacho as Ult_Despacho
FROM     dbo.trans_pe_enc INNER JOIN
                  dbo.trans_pe_tipo ON dbo.trans_pe_enc.IdTipoPedido = dbo.trans_pe_tipo.IdTipoPedido LEFT OUTER JOIN
                  dbo.trans_picking_enc ON dbo.trans_pe_enc.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc AND dbo.trans_pe_enc.IdBodega = dbo.trans_picking_enc.IdBodega LEFT OUTER JOIN
                  dbo.bodega ON dbo.trans_pe_enc.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
                  dbo.propietario_bodega ON dbo.trans_pe_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega LEFT OUTER JOIN
                  dbo.cliente ON dbo.trans_pe_enc.IdCliente = dbo.cliente.IdCliente LEFT OUTER JOIN
                  dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario LEFT OUTER JOIN
                  dbo.bodega_muelles ON dbo.trans_pe_enc.IdMuelle = dbo.bodega_muelles.IdMuelle LEFT OUTER JOIN
                  dbo.road_p_vendedor ON dbo.trans_pe_enc.RoadIdVendedor = dbo.road_p_vendedor.IdVendedor LEFT OUTER JOIN
                  dbo.road_ruta ON dbo.trans_pe_enc.RoadIdRuta = dbo.road_ruta.IdRuta LEFT OUTER JOIN
                  dbo.road_ruta AS Roadruta1 ON dbo.trans_pe_enc.RoadIdRutaDespacho = Roadruta1.IdRuta
```
