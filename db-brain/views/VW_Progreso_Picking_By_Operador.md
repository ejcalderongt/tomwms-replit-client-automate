---
id: db-brain-view-vw-progreso-picking-by-operador
type: db-view
title: dbo.VW_Progreso_Picking_By_Operador
schema: dbo
name: VW_Progreso_Picking_By_Operador
kind: view
modify_date: 2025-06-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Progreso_Picking_By_Operador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-05 |
| Columnas | 26 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedidoEnc` | `int` | ✓ |  |
| 2 | `IdPickingEnc` | `int` |  |  |
| 3 | `Referencia_Pedido` | `nvarchar(25)` | ✓ |  |
| 4 | `Tipo_Documento` | `nvarchar(250)` | ✓ |  |
| 5 | `CodigoProducto` | `nvarchar(50)` | ✓ |  |
| 6 | `NombreProducto` | `nvarchar(100)` | ✓ |  |
| 7 | `Operador_Picking` | `nvarchar(201)` | ✓ |  |
| 8 | `Operador_Verifico` | `nvarchar(201)` | ✓ |  |
| 9 | `cantidad_solicitada` | `float` | ✓ |  |
| 10 | `cantidad_recibida` | `float` | ✓ |  |
| 11 | `cantidad_verificada` | `float` | ✓ |  |
| 12 | `cantidad_despachada` | `float` | ✓ |  |
| 13 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 14 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 15 | `Fecha_Agregado` | `datetime` | ✓ |  |
| 16 | `Fecha_Picking` | `datetime` | ✓ |  |
| 17 | `Fecha_Verificacion` | `datetime` | ✓ |  |
| 18 | `Estado_Picking` | `nvarchar(20)` | ✓ |  |
| 19 | `Estado_Pedido` | `nvarchar(20)` | ✓ |  |
| 20 | `IdBodega` | `int` | ✓ |  |
| 21 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 22 | `IdPropietario` | `int` | ✓ |  |
| 23 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 24 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 25 | `Ubicacion` | `nvarchar(200)` | ✓ |  |
| 26 | `Licencia` | `nvarchar(25)` | ✓ |  |

## Consume

- `bodega`
- `Nombre_Completo_Ubicacion`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `trans_pe_enc`
- `trans_pe_tipo`
- `trans_picking_enc`
- `trans_picking_ubic`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Progreso_Picking_By_Operador]
AS
SELECT  dbo.trans_pe_enc.IdPedidoEnc, dbo.trans_picking_enc.IdPickingEnc, dbo.trans_pe_enc.referencia AS Referencia_Pedido, dbo.trans_pe_tipo.Descripcion AS Tipo_Documento, dbo.producto.codigo AS CodigoProducto, 
                         dbo.producto.nombre AS NombreProducto, dbo.operador.nombres + ' ' + dbo.operador.apellidos AS Operador_Picking, operador_1.nombres + ' ' + operador_1.apellidos AS Operador_Verifico, 
                         dbo.trans_picking_ubic.cantidad_solicitada, dbo.trans_picking_ubic.cantidad_recibida, dbo.trans_picking_ubic.cantidad_verificada, dbo.trans_picking_ubic.cantidad_despachada, dbo.unidad_medida.Nombre AS UMBas, 
                         dbo.producto_presentacion.nombre AS Presentacion, dbo.trans_picking_ubic.fec_agr AS Fecha_Agregado, dbo.trans_picking_ubic.fecha_picking AS Fecha_Picking, dbo.trans_picking_ubic.fecha_verificado AS Fecha_Verificacion, 
                         dbo.trans_picking_enc.estado AS Estado_Picking, dbo.trans_pe_enc.estado AS Estado_Pedido, dbo.trans_pe_enc.IdBodega, dbo.bodega.nombre AS Bodega, dbo.propietarios.IdPropietario, 
                         dbo.propietarios.nombre_comercial AS Propietario, dbo.producto_clasificacion.nombre AS Clasificacion, 
						 dbo.Nombre_Completo_Ubicacion(trans_picking_ubic.IdUbicacion, trans_picking_ubic.IdBodega) Ubicacion, dbo.trans_picking_ubic.lic_plate Licencia
FROM            dbo.trans_picking_ubic INNER JOIN
                         dbo.producto_bodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto ON dbo.trans_picking_ubic.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.trans_picking_enc ON dbo.trans_picking_ubic.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc INNER JOIN
                         dbo.unidad_medida ON dbo.trans_picking_ubic.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                         dbo.producto_clasificacion ON dbo.producto.IdClasificacion = dbo.producto_clasificacion.IdClasificacion LEFT OUTER JOIN
                         dbo.trans_pe_tipo INNER JOIN
                         dbo.trans_pe_enc ON dbo.trans_pe_tipo.IdTipoPedido = dbo.trans_pe_enc.IdTipoPedido INNER JOIN
                         dbo.propietario_bodega INNER JOIN
                         dbo.bodega ON dbo.propietario_bodega.IdBodega = dbo.bodega.IdBodega ON dbo.trans_pe_enc.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND 
                         dbo.trans_pe_enc.IdBodega = dbo.bodega.IdBodega INNER JOIN
                         dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario ON dbo.trans_picking_ubic.IdPedidoEnc = dbo.trans_pe_enc.IdPedidoEnc LEFT OUTER JOIN
                         dbo.operador_bodega AS operador_bodega_1 INNER JOIN
                         dbo.operador ON operador_bodega_1.IdOperador = dbo.operador.IdOperador ON dbo.trans_picking_ubic.IdOperadorBodega_Pickeo = operador_bodega_1.IdOperadorBodega LEFT OUTER JOIN
                         dbo.operador_bodega ON dbo.trans_picking_ubic.IdOperadorBodega_Verifico = dbo.operador_bodega.IdOperadorBodega LEFT OUTER JOIN
                         dbo.operador AS operador_1 ON dbo.operador_bodega.IdOperador = operador_1.IdOperador LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.trans_picking_ubic.IdPresentacion = dbo.producto_presentacion.IdPresentacion
```
