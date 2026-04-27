---
id: db-brain-view-vw-cambios-estado
type: db-view
title: dbo.VW_Cambios_Estado
schema: dbo
name: VW_Cambios_Estado
kind: view
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Cambios_Estado`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-02-11 |
| Columnas | 23 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodegaOrigen` | `int` |  |  |
| 2 | `TipoTarea` | `nvarchar(50)` | ✓ |  |
| 3 | `Código` | `nvarchar(50)` | ✓ |  |
| 4 | `Producto` | `nvarchar(100)` | ✓ |  |
| 5 | `Cantidad` | `float` | ✓ |  |
| 6 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 7 | `Peso` | `float` | ✓ |  |
| 8 | `Lote` | `nvarchar(50)` | ✓ |  |
| 9 | `LP` | `nvarchar(50)` | ✓ |  |
| 10 | `Vence` | `datetime` | ✓ |  |
| 11 | `Estado` | `nvarchar(50)` | ✓ |  |
| 12 | `Motivo` | `nvarchar(50)` | ✓ |  |
| 13 | `Propietario` | `nvarchar(100)` |  |  |
| 14 | `Fecha` | `datetime` | ✓ |  |
| 15 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 16 | `IdPresentacion` | `int` | ✓ |  |
| 17 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 18 | `Factor` | `float` | ✓ |  |
| 19 | `IdProductoBodega` | `int` | ✓ |  |
| 20 | `IdPropietarioBodega` | `int` | ✓ |  |
| 21 | `Ubicacion_Origen` | `nvarchar(200)` | ✓ |  |
| 22 | `Ubicacion_Destino` | `nvarchar(200)` | ✓ |  |
| 23 | `operador` | `nvarchar(201)` | ✓ |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `motivo_ubicacion`
- `Nombre_Completo_Ubicacion`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `sis_tipo_tarea`
- `trans_movimientos`
- `trans_oc_pol`
- `trans_re_oc`
- `trans_ubic_hh_enc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Cambios_Estado] AS
SELECT 
dbo.trans_movimientos.IdBodegaOrigen, dbo.sis_tipo_tarea.Nombre AS TipoTarea, dbo.producto.codigo AS Código, dbo.producto.nombre AS Producto, dbo.trans_movimientos.cantidad AS Cantidad, 
                  dbo.unidad_medida.Nombre AS UMBas, dbo.trans_movimientos.peso AS Peso, dbo.trans_movimientos.lote AS Lote, dbo.trans_movimientos.barra_pallet AS LP, dbo.trans_movimientos.fecha_vence AS Vence, 
                  dbo.producto_estado.nombre AS Estado, dbo.motivo_ubicacion.Nombre AS Motivo, dbo.propietarios.nombre_comercial AS Propietario, dbo.trans_movimientos.fecha AS Fecha, enc.codigo_poliza AS Poliza, 
                  dbo.trans_movimientos.IdPresentacion, dbo.producto_presentacion.nombre AS Presentacion, dbo.producto_presentacion.factor AS Factor, dbo.trans_movimientos.IdProductoBodega, dbo.trans_movimientos.IdPropietarioBodega, 
				  dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) AS Ubicacion_Origen,
				  dbo.Nombre_Completo_Ubicacion(bodega_ubicacion_1.IdUbicacion, bodega_ubicacion_1.IdBodega) AS Ubicacion_Destino
				  , dbo.operador.nombres +' '+ dbo.operador.apellidos as operador
FROM    dbo.propietarios INNER JOIN
                  dbo.trans_movimientos INNER JOIN
                  dbo.sis_tipo_tarea ON dbo.trans_movimientos.IdTipoTarea = dbo.sis_tipo_tarea.IdTipoTarea 
				  INNER JOIN
                  dbo.producto_bodega ON dbo.trans_movimientos.IdProductoBodega = dbo.producto_bodega.IdProductoBodega 
				  INNER JOIN
                  dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto 
				  INNER JOIN
                  dbo.producto_estado ON dbo.trans_movimientos.IdEstadoOrigen = dbo.producto_estado.IdEstado AND dbo.trans_movimientos.IdEstadoDestino = dbo.producto_estado.IdEstado 
				  INNER JOIN
                  dbo.bodega_ubicacion ON dbo.trans_movimientos.IdUbicacionOrigen = dbo.bodega_ubicacion.IdUbicacion AND dbo.trans_movimientos.IdBodegaOrigen = dbo.bodega_ubicacion.IdBodega 
				  INNER JOIN
                  dbo.bodega_tramo ON dbo.bodega_ubicacion.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.bodega_ubicacion.IdBodega = dbo.bodega_tramo.IdBodega AND dbo.bodega_ubicacion.IdArea = dbo.bodega_tramo.IdArea AND 
                  dbo.bodega_ubicacion.IdSector = dbo.bodega_tramo.IdSector 
				  INNER JOIN
                  dbo.bodega_ubicacion AS bodega_ubicacion_1 ON dbo.trans_movimientos.IdUbicacionDestino = bodega_ubicacion_1.IdUbicacion AND dbo.trans_movimientos.IdBodegaDestino = bodega_ubicacion_1.IdBodega 
				  INNER JOIN
                  dbo.bodega_tramo AS bodega_tramo_1 ON bodega_ubicacion_1.IdTramo = bodega_tramo_1.IdTramo AND bodega_ubicacion_1.IdSector = bodega_tramo_1.IdSector AND bodega_ubicacion_1.IdArea = bodega_tramo_1.IdArea AND 
                  bodega_ubicacion_1.IdBodega = bodega_tramo_1.IdBodega 
				  ON dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario AND dbo.propietarios.IdPropietario = dbo.producto_estado.IdPropietario 
				  INNER JOIN
                  dbo.propietario_bodega ON dbo.trans_movimientos.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega AND dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario
				  INNER JOIN
                  dbo.unidad_medida ON dbo.propietarios.IdPropietario = dbo.unidad_medida.IdPropietario AND 
				  dbo.trans_movimientos.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida AND 
                  dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida 
				  LEFT OUTER JOIN
                  dbo.producto_presentacion ON dbo.trans_movimientos.IdPresentacion = dbo.producto_presentacion.IdPresentacion AND dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto 
				  LEFT OUTER JOIN
                  dbo.motivo_ubicacion 
				  INNER JOIN
                  dbo.trans_ubic_hh_enc ON dbo.motivo_ubicacion.IdMotivoUbicacion = dbo.trans_ubic_hh_enc.IdMotivoUbicacion ON dbo.trans_movimientos.IdTransaccion = dbo.trans_ubic_hh_enc.IdTareaUbicacionEnc LEFT OUTER JOIN
                  dbo.trans_re_oc re ON dbo.trans_movimientos.IdRecepcion = re.IdRecepcionEnc 
				  LEFT OUTER JOIN
                  dbo.trans_oc_pol enc ON re.IdOrdenCompraEnc = enc.IdOrdenCompraEnc 
				  --LEFT OUTER JOIN dbo.trans_ubic_hh_det
				  --on dbo.trans_ubic_hh_enc.IdTareaUbicacionEnc = dbo.trans_ubic_hh_det.IdTareaUbicacionEnc 
				  INNER JOIN dbo.operador_bodega ON
				  dbo.trans_movimientos.IdOperadorBodega = dbo.operador_bodega.IdOperadorBodega and  dbo.operador_bodega.IdBodega = dbo.trans_movimientos.IdBodegaOrigen
				  INNER JOIN dbo.operador on 
				  dbo.operador_bodega.IdOperador = dbo.operador.IdOperador
WHERE  (dbo.sis_tipo_tarea.Nombre = 'CEST')
```
