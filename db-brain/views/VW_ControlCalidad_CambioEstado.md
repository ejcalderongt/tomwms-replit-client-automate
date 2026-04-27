---
id: db-brain-view-vw-controlcalidad-cambioestado
type: db-view
title: dbo.VW_ControlCalidad_CambioEstado
schema: dbo
name: VW_ControlCalidad_CambioEstado
kind: view
modify_date: 2025-06-04
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_ControlCalidad_CambioEstado`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-04 |
| Columnas | 30 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareaUbicacionEnc` | `int` |  |  |
| 2 | `FechaTransacccion` | `datetime` | ✓ |  |
| 3 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 4 | `Producto` | `nvarchar(100)` | ✓ |  |
| 5 | `Licencia` | `varchar(max)` | ✓ |  |
| 6 | `Lote` | `nvarchar(50)` | ✓ |  |
| 7 | `Vence` | `datetime` | ✓ |  |
| 8 | `cantidad` | `float` | ✓ |  |
| 9 | `UbicaiconOrigen` | `nvarchar(200)` | ✓ |  |
| 10 | `UbicaiconDestino` | `nvarchar(200)` | ✓ |  |
| 11 | `IdUbicacionDestino` | `int` | ✓ |  |
| 12 | `IdEstadoOrigen` | `int` | ✓ |  |
| 13 | `IdEstadoDestino` | `int` | ✓ |  |
| 14 | `IdOperadorBodega` | `int` | ✓ |  |
| 15 | `activo` | `bit` | ✓ |  |
| 16 | `Operador` | `nvarchar(100)` | ✓ |  |
| 17 | `EstadoOrigen` | `nvarchar(50)` | ✓ |  |
| 18 | `EstadoDestino` | `nvarchar(50)` | ✓ |  |
| 19 | `fecha_ingreso` | `char(10)` | ✓ |  |
| 20 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 21 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 22 | `IdPropietario` | `int` | ✓ |  |
| 23 | `Propietario` | `nvarchar(100)` | ✓ |  |
| 24 | `IdProducto` | `int` | ✓ |  |
| 25 | `IdProductoBodega` | `int` | ✓ |  |
| 26 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 27 | `IdUbicacionOrigen` | `int` | ✓ |  |
| 28 | `IdPresentacion` | `int` | ✓ |  |
| 29 | `IdBodega` | `int` | ✓ |  |
| 30 | `UsuarioBOF` | `nvarchar(100)` | ✓ |  |

## Consume

- `Nombre_Completo_Ubicacion`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietarios`
- `trans_ubic_hh_det`
- `trans_ubic_hh_enc`
- `trans_ubic_hh_op`
- `trans_ubic_hh_stock`
- `unidad_medida`
- `usuario`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
create VIEW [dbo].[VW_ControlCalidad_CambioEstado]
AS
SELECT dbo.trans_ubic_hh_det.IdTareaUbicacionEnc, dbo.trans_ubic_hh_enc.fec_agr AS FechaTransacccion, dbo.producto.codigo AS Codigo, dbo.producto.nombre AS Producto, dbo.trans_ubic_hh_stock.lic_plate AS Licencia, 
                  dbo.trans_ubic_hh_stock.lote AS Lote, dbo.trans_ubic_hh_stock.fecha_vence AS Vence, dbo.trans_ubic_hh_det.cantidad, dbo.Nombre_Completo_Ubicacion(dbo.trans_ubic_hh_det.IdUbicacionOrigen, dbo.trans_ubic_hh_det.IdBodega) 
                  AS UbicaiconOrigen, dbo.Nombre_Completo_Ubicacion(dbo.trans_ubic_hh_det.IdUbicacionDestino, dbo.trans_ubic_hh_det.IdBodega) AS UbicaiconDestino, dbo.trans_ubic_hh_det.IdUbicacionDestino, 
                  dbo.trans_ubic_hh_det.IdEstadoOrigen, dbo.trans_ubic_hh_det.IdEstadoDestino, CASE WHEN dbo.trans_ubic_hh_op.IdOperadorBodega IS NULL 
                  THEN dbo.trans_ubic_hh_det.IdOperadorBodega ELSE dbo.trans_ubic_hh_op.IdOperadorBodega END AS IdOperadorBodega, dbo.trans_ubic_hh_det.activo, dbo.operador.nombres AS Operador, 
                  producto_estado_1.nombre AS EstadoOrigen, dbo.producto_estado.nombre AS EstadoDestino, CONVERT(char(10), dbo.trans_ubic_hh_stock.fecha_ingreso, 120) AS fecha_ingreso, dbo.producto_presentacion.nombre AS Presentacion, 
                  dbo.unidad_medida.Nombre AS UnidadMedida, dbo.producto.IdPropietario, dbo.propietarios.nombre_comercial AS Propietario, dbo.producto.IdProducto, dbo.producto_bodega.IdProductoBodega, dbo.producto.IdUnidadMedidaBasica, 
                  dbo.trans_ubic_hh_det.IdUbicacionOrigen, dbo.producto_presentacion.IdPresentacion, dbo.trans_ubic_hh_det.IdBodega, dbo.usuario.nombres AS UsuarioBOF
FROM     dbo.producto_estado INNER JOIN
                  dbo.producto_estado AS producto_estado_1 INNER JOIN
                  dbo.trans_ubic_hh_det INNER JOIN
                  dbo.trans_ubic_hh_enc ON dbo.trans_ubic_hh_det.IdTareaUbicacionEnc = dbo.trans_ubic_hh_enc.IdTareaUbicacionEnc INNER JOIN
                  dbo.usuario ON dbo.trans_ubic_hh_enc.user_agr = dbo.usuario.IdUsuario ON producto_estado_1.IdEstado = dbo.trans_ubic_hh_det.IdEstadoOrigen ON 
                  dbo.producto_estado.IdEstado = dbo.trans_ubic_hh_det.IdEstadoDestino LEFT OUTER JOIN
                  dbo.unidad_medida INNER JOIN
                  dbo.producto_bodega INNER JOIN
                  dbo.trans_ubic_hh_stock ON dbo.producto_bodega.IdProductoBodega = dbo.trans_ubic_hh_stock.IdProductoBodega INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto ON dbo.unidad_medida.IdUnidadMedida = dbo.trans_ubic_hh_stock.IdUnidadMedida AND 
                  dbo.unidad_medida.IdUnidadMedida = dbo.trans_ubic_hh_stock.IdUnidadMedida INNER JOIN
                  dbo.propietarios ON dbo.producto.IdPropietario = dbo.propietarios.IdPropietario ON dbo.trans_ubic_hh_det.IdTareaUbicacionEnc = dbo.trans_ubic_hh_stock.IdTareaUbicacionEnc AND 
                  dbo.trans_ubic_hh_det.IdTareaUbicacionDet = dbo.trans_ubic_hh_stock.IdTareaUbicacionDet AND dbo.trans_ubic_hh_det.IdStock = dbo.trans_ubic_hh_stock.IdStock LEFT OUTER JOIN
                  dbo.operador_bodega INNER JOIN
                  dbo.trans_ubic_hh_op ON dbo.operador_bodega.IdOperadorBodega = dbo.trans_ubic_hh_op.IdOperadorBodega INNER JOIN
                  dbo.operador ON dbo.operador_bodega.IdOperador = dbo.operador.IdOperador ON dbo.trans_ubic_hh_det.IdTareaUbicacionEnc = dbo.trans_ubic_hh_op.IdTareaUbicacionEnc AND 
                  dbo.trans_ubic_hh_det.IdOperadorBodega = dbo.trans_ubic_hh_op.IdOperadorBodega LEFT OUTER JOIN
                  dbo.producto_presentacion ON dbo.trans_ubic_hh_stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND dbo.trans_ubic_hh_stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion
GROUP BY dbo.trans_ubic_hh_det.IdTareaUbicacionEnc, dbo.producto.nombre, dbo.trans_ubic_hh_det.IdUbicacionDestino, dbo.trans_ubic_hh_det.IdEstadoOrigen, dbo.trans_ubic_hh_det.IdEstadoDestino, 
                  dbo.trans_ubic_hh_det.IdOperadorBodega, dbo.trans_ubic_hh_det.cantidad, dbo.operador.nombres, dbo.producto.codigo, producto_estado_1.nombre, dbo.trans_ubic_hh_stock.lote, CONVERT(char(10), 
                  dbo.trans_ubic_hh_stock.fecha_ingreso, 120), dbo.producto_presentacion.nombre, dbo.unidad_medida.Nombre, dbo.trans_ubic_hh_stock.fecha_vence, dbo.producto.IdPropietario, dbo.propietarios.nombre_comercial, 
                  dbo.producto.IdProducto, dbo.producto_bodega.IdProductoBodega, dbo.producto.IdUnidadMedidaBasica, dbo.trans_ubic_hh_det.IdUbicacionOrigen, dbo.producto_presentacion.IdPresentacion, dbo.trans_ubic_hh_det.activo, 
                  dbo.trans_ubic_hh_stock.lic_plate, dbo.trans_ubic_hh_stock.fecha_vence, dbo.trans_ubic_hh_op.IdOperadorBodega, dbo.trans_ubic_hh_det.IdBodega, dbo.trans_ubic_hh_enc.fec_agr, dbo.usuario.nombres, dbo.producto_estado.nombre
```
