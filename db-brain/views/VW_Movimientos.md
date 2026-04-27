---
id: db-brain-view-vw-movimientos
type: db-view
title: dbo.VW_Movimientos
schema: dbo
name: VW_Movimientos
kind: view
modify_date: 2025-03-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Movimientos`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-03-18 |
| Columnas | 32 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Propietario` | `nvarchar(100)` |  |  |
| 2 | `IdBodega` | `int` | ✓ |  |
| 3 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 4 | `Producto` | `nvarchar(100)` | ✓ |  |
| 5 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 6 | `Estado Origen` | `nvarchar(50)` | ✓ |  |
| 7 | `Estado Destino` | `nvarchar(50)` | ✓ |  |
| 8 | `Unidad de Medida` | `nvarchar(50)` | ✓ |  |
| 9 | `cantidad` | `float` | ✓ |  |
| 10 | `peso` | `float` | ✓ |  |
| 11 | `lote` | `nvarchar(50)` | ✓ |  |
| 12 | `Origen` | `nvarchar(200)` | ✓ |  |
| 13 | `Destino` | `nvarchar(200)` | ✓ |  |
| 14 | `Tipo Tarea` | `nvarchar(50)` | ✓ |  |
| 15 | `IdBodegaOrigen` | `int` |  |  |
| 16 | `fecha` | `datetime` | ✓ |  |
| 17 | `IdProducto` | `int` |  |  |
| 18 | `codigo` | `nvarchar(50)` | ✓ |  |
| 19 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 20 | `barra_pallet` | `nvarchar(50)` | ✓ |  |
| 21 | `fecha_vence` | `datetime` | ✓ |  |
| 22 | `Cantidad_Presentacion` | `float` | ✓ |  |
| 23 | `IdDocIngreso` | `int` | ✓ |  |
| 24 | `IdTransaccion` | `int` |  |  |
| 25 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 26 | `Area_Origen` | `nvarchar(200)` | ✓ |  |
| 27 | `Operador` | `nvarchar(201)` |  |  |
| 28 | `IdDocSalida` | `int` | ✓ |  |
| 29 | `IdPropietario` | `int` |  |  |
| 30 | `fecha_recepcion` | `datetime` | ✓ |  |
| 31 | `IdTipoTarea` | `int` | ✓ |  |
| 32 | `dias_piso` | `int` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `sis_tipo_tarea`
- `trans_movimientos`
- `trans_oc_pol`
- `trans_re_oc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
/****** #CKFK20240910 Agregué los campos faltantes de cambios realizados por Efren el año pasado ******/
CREATE VIEW [dbo].[VW_Movimientos]
AS
SELECT pr.nombre_comercial AS Propietario, u1.IdBodega, enc.codigo_poliza AS Poliza, p.nombre AS Producto, 
       pp.nombre AS Presentación, pe1.nombre AS [Estado Origen], pe2.nombre AS [Estado Destino], 
	   u.Nombre AS [Unidad de Medida],SUM(m.cantidad) AS cantidad, SUM(m.peso) AS peso, m.lote, 
	   dbo.Nombre_Completo_Ubicacion(u1.IdUbicacion, u1.IdBodega) AS Origen, 
	   dbo.Nombre_Completo_Ubicacion(u2.IdUbicacion, u2.IdBodega) AS Destino,
       stt.Nombre AS [Tipo Tarea], m.IdBodegaOrigen, m.fecha, p.IdProducto, p.codigo, p.codigo_barra, 
	   m.barra_pallet, m.fecha_vence, SUM(m.cantidad)/ CASE WHEN pp.factor = 0 THEN 1 ELSE pp.factor END AS Cantidad_Presentacion, 
	   re.IdOrdenCompraEnc IdDocIngreso, m.IdTransaccion, dbo.producto_clasificacion.nombre AS Clasificacion, 
	   dbo.Nombre_Area(u1.IdArea, u1.IdBodega) AS Area_Origen, ISNULL(o.nombres,'') + ' ' +  ISNULL(o.apellidos,'') Operador,
	   m.IdDespachoEnc IdDocSalida, pr.IdPropietario,re.fec_agr fecha_recepcion,m.IdTipoTarea,
case when m.IdTipoTarea=5 then DATEDIFF(DAY, cast(re.fec_agr as date), cast(m.fecha as date))else 0 end as dias_piso
FROM dbo.trans_movimientos AS m LEFT OUTER JOIN
	dbo.propietario_bodega AS prb ON m.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
	dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
	dbo.producto_bodega AS pb ON m.IdProductoBodega = pb.IdProductoBodega INNER JOIN
	dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
	dbo.producto_clasificacion ON p.IdClasificacion = dbo.producto_clasificacion.IdClasificacion LEFT OUTER JOIN
	dbo.bodega_ubicacion AS u1 ON m.IdUbicacionOrigen = u1.IdUbicacion AND m.IdBodegaOrigen = u1.IdBodega LEFT OUTER JOIN
	dbo.bodega_ubicacion AS u2 ON m.IdUbicacionDestino = u2.IdUbicacion AND m.IdBodegaDestino = u2.IdBodega LEFT OUTER JOIN
	dbo.producto_presentacion AS pp ON m.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
	dbo.producto_estado AS pe1 ON m.IdEstadoOrigen = pe1.IdEstado AND pe1.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
	dbo.producto_estado AS pe2 ON m.IdEstadoDestino = pe2.IdEstado AND pe2.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
	dbo.unidad_medida AS u ON m.IdUnidadMedida = u.IdUnidadMedida AND u.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
	dbo.sis_tipo_tarea AS stt ON m.IdTipoTarea = stt.IdTipoTarea LEFT OUTER JOIN
	dbo.trans_re_oc AS re ON m.IdRecepcion = re.IdRecepcionEnc LEFT OUTER JOIN
	dbo.trans_oc_pol AS enc ON re.IdOrdenCompraEnc = enc.IdOrdenCompraEnc LEFT OUTER JOIN
	dbo.operador_bodega ob on m.IdOperadorBodega = ob.IdOperadorBodega LEFT OUTER JOIN
	dbo.operador o ON o.IdOperador = ob.IdOperador
GROUP BY pr.nombre_comercial, p.nombre, pp.nombre, pe1.nombre, pe2.nombre, u.Nombre, m.cantidad, m.peso, 
         m.lote, u1.descripcion, u2.descripcion, stt.Nombre, m.IdBodegaOrigen, m.fecha, p.IdProducto, p.codigo, 
		 p.codigo_barra, m.barra_pallet, m.fecha_vence, pp.IdPresentacion, pp.factor, 
		 m.cantidad / pp.factor, enc.codigo_poliza, m.IdTransaccion, u1.IdUbicacion, u2.IdUbicacion, u1.IdBodega, 
		 u2.IdBodega, dbo.producto_clasificacion.nombre, u1.IdArea, re.IdOrdenCompraEnc,
		 o.nombres, o.apellidos,m.IdDespachoEnc, pr.IdPropietario,re.fec_agr, cast(m.fecha as date),m.IdTipoTarea
```
