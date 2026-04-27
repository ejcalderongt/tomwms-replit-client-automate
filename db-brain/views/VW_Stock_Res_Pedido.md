---
id: db-brain-view-vw-stock-res-pedido
type: db-view
title: dbo.VW_Stock_Res_Pedido
schema: dbo
name: VW_Stock_Res_Pedido
kind: view
modify_date: 2022-09-23
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Res_Pedido`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-09-23 |
| Columnas | 51 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `codigo` | `nvarchar(50)` | ✓ |  |
| 2 | `nombre` | `nvarchar(100)` | ✓ |  |
| 3 | `presentacion` | `nvarchar(50)` | ✓ |  |
| 4 | `NomEstado` | `nvarchar(50)` | ✓ |  |
| 5 | `unidadmedida` | `nvarchar(50)` | ✓ |  |
| 6 | `propietario` | `nvarchar(100)` |  |  |
| 7 | `bodegaubicacion` | `nvarchar(50)` | ✓ |  |
| 8 | `cantidadfisica` | `float` |  |  |
| 9 | `factor` | `float` | ✓ |  |
| 10 | `IdStockRes` | `int` |  |  |
| 11 | `IdTransaccion` | `int` |  |  |
| 12 | `Indicador` | `nvarchar(50)` | ✓ |  |
| 13 | `IdPedidoDet` | `int` |  |  |
| 14 | `IdStock` | `int` |  |  |
| 15 | `IdPropietarioBodega` | `int` |  |  |
| 16 | `IdProductoBodega` | `int` |  |  |
| 17 | `IdUbicacion` | `int` |  |  |
| 18 | `estado` | `nvarchar(20)` | ✓ |  |
| 19 | `IdPresentacion` | `int` | ✓ |  |
| 20 | `IdUnidadMedida` | `int` | ✓ |  |
| 21 | `lote` | `nvarchar(50)` | ✓ |  |
| 22 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 23 | `serial` | `nvarchar(50)` | ✓ |  |
| 24 | `cantidad` | `float` |  |  |
| 25 | `peso` | `float` | ✓ |  |
| 26 | `fecha_ingreso` | `datetime` | ✓ |  |
| 27 | `fecha_vence` | `datetime` | ✓ |  |
| 28 | `uds_lic_plate` | `float` | ✓ |  |
| 29 | `ubicacion_ant` | `nvarchar(25)` | ✓ |  |
| 30 | `no_bulto` | `int` | ✓ |  |
| 31 | `IdRecepcion` | `bigint` | ✓ |  |
| 32 | `IdPicking` | `bigint` | ✓ |  |
| 33 | `IdPedido` | `bigint` | ✓ |  |
| 34 | `IdDespacho` | `bigint` | ✓ |  |
| 35 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 36 | `fec_agr` | `datetime` | ✓ |  |
| 37 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 38 | `fec_mod` | `datetime` | ✓ |  |
| 39 | `host` | `nvarchar(50)` | ✓ |  |
| 40 | `añada` | `int` | ✓ |  |
| 41 | `fecha_manufactura` | `datetime` | ✓ |  |
| 42 | `referencia` | `nvarchar(25)` | ✓ |  |
| 43 | `IdBodega` | `int` | ✓ |  |
| 44 | `IdArea` | `int` | ✓ |  |
| 45 | `Columna` | `int` | ✓ |  |
| 46 | `Nivel` | `int` | ✓ |  |
| 47 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 48 | `Estructura` | `nvarchar(156)` | ✓ |  |
| 49 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 50 | `fecha_preparacion` | `date` | ✓ |  |
| 51 | `ubicacion_picking` | `bit` | ✓ |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_res`
- `trans_pe_enc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_Res_Pedido]
AS
SELECT        p.codigo, p.nombre, pp.nombre AS presentacion, pe.nombre AS NomEstado, um.Nombre AS unidadmedida, pr.nombre_comercial AS propietario, bu.descripcion AS bodegaubicacion, s.cantidad AS cantidadfisica, pp.factor, 
                         res.IdStockRes, res.IdTransaccion, res.Indicador, res.IdPedidoDet, res.IdStock, res.IdPropietarioBodega, res.IdProductoBodega, res.IdUbicacion, res.estado, res.IdPresentacion, res.IdUnidadMedida, res.lote, res.lic_plate, 
                         res.serial, res.cantidad, res.peso, res.fecha_ingreso, res.fecha_vence, res.uds_lic_plate, res.ubicacion_ant, res.no_bulto, res.IdRecepcion, res.IdPicking, res.IdPedido, res.IdDespacho, res.user_agr, res.fec_agr, res.user_mod, 
                         res.fec_mod, res.host, res.añada, res.fecha_manufactura, dbo.trans_pe_enc.referencia, dbo.propietario_bodega.IdBodega, 
						 bu.IdArea, bu.indice_x AS Columna, bu.nivel AS Nivel, dbo.bodega_tramo.descripcion AS Tramo, 
                         dbo.bodega_tramo.descripcion + ' -C' + CONVERT(NVARCHAR(50), bu.indice_x) + ' -N' + CONVERT(NVARCHAR(50), bu.nivel) AS Estructura,
						 trans_pe_enc.Fecha_Pedido, trans_pe_enc.fecha_preparacion,bu.ubicacion_picking
FROM            dbo.bodega_tramo INNER JOIN
                         dbo.bodega_ubicacion AS bu ON dbo.bodega_tramo.IdTramo = bu.IdTramo AND dbo.bodega_tramo.IdBodega = bu.IdBodega RIGHT OUTER JOIN
                         dbo.stock_res AS res INNER JOIN
                         dbo.producto_bodega AS pb ON pb.IdProductoBodega = res.IdProductoBodega INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto INNER JOIN
                         dbo.stock AS s ON res.IdStock = s.IdStock INNER JOIN
                         dbo.propietario_bodega ON s.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND s.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
                         dbo.propietarios AS pr ON dbo.propietario_bodega.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.trans_pe_enc ON res.IdPedido = dbo.trans_pe_enc.IdPedidoEnc LEFT OUTER JOIN
                         dbo.unidad_medida AS um ON res.IdUnidadMedida = um.IdUnidadMedida LEFT OUTER JOIN
                         dbo.producto_estado AS pe ON res.IdProductoEstado = pe.IdEstado ON bu.IdUbicacion = res.IdUbicacion AND bu.IdBodega = res.idbodega LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON res.IdPresentacion = pp.IdPresentacion
```
