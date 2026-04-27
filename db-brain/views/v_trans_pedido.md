---
id: db-brain-view-v-trans-pedido
type: db-view
title: dbo.v_trans_pedido
schema: dbo
name: v_trans_pedido
kind: view
modify_date: 2016-04-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.v_trans_pedido`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2016-04-11 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedidoEnc` | `bigint` |  |  |
| 2 | `no_documento` | `bigint` | ✓ |  |
| 3 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 4 | `Cliente` | `nvarchar(103)` | ✓ |  |
| 5 | `Estado` | `nvarchar(20)` | ✓ |  |
| 6 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 7 | `Muelle` | `nvarchar(50)` | ✓ |  |
| 8 | `Propietario` | `nvarchar(100)` |  |  |
| 9 | `RoadVendedor` | `nvarchar(61)` | ✓ |  |
| 10 | `RoadRuta` | `nvarchar(69)` |  |  |
| 11 | `Anualdo` | `bit` | ✓ |  |
| 12 | `activo` | `bit` | ✓ |  |

## Consume

- `bodega`
- `bodega_muelles`
- `cliente`
- `propietario_bodega`
- `propietarios`
- `road_p_vendedor`
- `road_ruta`
- `trans_pe_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.v_trans_pedido
AS
SELECT        dbo.trans_pe_enc.IdPedidoEnc, dbo.trans_pe_enc.no_documento, dbo.trans_pe_enc.Fecha_Pedido, 
                         dbo.cliente.codigo + ' - ' + dbo.cliente.nombre_comercial AS Cliente, dbo.trans_pe_enc.estado AS Estado, dbo.bodega.nombre AS Bodega, 
                         dbo.bodega_muelles.nombre AS Muelle, dbo.propietarios.nombre_comercial AS Propietario, 
                         dbo.road_p_vendedor.codigo + ' - ' + dbo.road_p_vendedor.nombre AS RoadVendedor, dbo.road_ruta.CODIGO + '  - ' + dbo.road_ruta.NOMBRE AS RoadRuta, 
                         dbo.trans_pe_enc.anulado AS Anualdo, dbo.trans_pe_enc.activo
FROM            dbo.trans_pe_enc INNER JOIN
                         dbo.bodega ON dbo.trans_pe_enc.IdBodega = dbo.bodega.IdBodega INNER JOIN
                         dbo.propietario_bodega ON dbo.trans_pe_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND 
                         dbo.bodega.IdBodega = dbo.propietario_bodega.IdBodega INNER JOIN
                         dbo.cliente ON dbo.trans_pe_enc.IdCliente = dbo.cliente.IdCliente INNER JOIN
                         dbo.bodega_muelles ON dbo.trans_pe_enc.IdMuelle = dbo.bodega_muelles.IdMuelle AND dbo.bodega.IdBodega = dbo.bodega_muelles.IdBodega INNER JOIN
                         dbo.road_p_vendedor ON dbo.bodega.codigo = dbo.road_p_vendedor.codigo AND dbo.trans_pe_enc.RoadIdVendedor = dbo.road_p_vendedor.IdVendedor INNER JOIN
                         dbo.road_ruta ON dbo.trans_pe_enc.RoadIdRuta = dbo.road_ruta.IdRuta INNER JOIN
                         dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario AND dbo.cliente.IdPropietario = dbo.propietarios.IdPropietario
```
