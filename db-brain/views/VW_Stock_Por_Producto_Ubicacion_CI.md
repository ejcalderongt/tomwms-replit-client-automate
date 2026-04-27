---
id: db-brain-view-vw-stock-por-producto-ubicacion-ci
type: db-view
title: dbo.VW_Stock_Por_Producto_Ubicacion_CI
schema: dbo
name: VW_Stock_Por_Producto_Ubicacion_CI
kind: view
modify_date: 2022-03-09
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Por_Producto_Ubicacion_CI`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-03-09 |
| Columnas | 66 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdPropietario` | `int` |  |  |
| 3 | `IdPropietarioBodega` | `int` |  |  |
| 4 | `IdProducto` | `int` | ✓ |  |
| 5 | `IdProductoBodega` | `int` | ✓ |  |
| 6 | `IdStock` | `int` |  |  |
| 7 | `IdUbicacion_anterior` | `int` | ✓ |  |
| 8 | `IdUnidadMedida` | `int` |  |  |
| 9 | `IdProductoEstado` | `int` | ✓ |  |
| 10 | `IdPresentacion` | `int` | ✓ |  |
| 11 | `IdRecepcionEnc` | `int` | ✓ |  |
| 12 | `Propietario` | `nvarchar(100)` |  |  |
| 13 | `codigo` | `nvarchar(50)` | ✓ |  |
| 14 | `Barra` | `nvarchar(35)` | ✓ |  |
| 15 | `nombre` | `nvarchar(100)` | ✓ |  |
| 16 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 17 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 18 | `lote` | `nvarchar(50)` |  |  |
| 19 | `Ingreso` | `datetime` | ✓ |  |
| 20 | `Vence` | `datetime` | ✓ |  |
| 21 | `Cantidad_UMBas` | `float` |  |  |
| 22 | `CantidadReservadaUmBas` | `float` | ✓ |  |
| 23 | `Disponible_UMBas` | `float` |  |  |
| 24 | `peso` | `float` | ✓ |  |
| 25 | `UbicacionCompleta` | `nvarchar(200)` | ✓ |  |
| 26 | `dañado` | `bit` | ✓ |  |
| 27 | `factor` | `float` | ✓ |  |
| 28 | `EstadoUtilizable` | `bit` | ✓ |  |
| 29 | `IdUbicacion` | `int` |  |  |
| 30 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 31 | `serial` | `nvarchar(50)` | ✓ |  |
| 32 | `añada` | `int` | ✓ |  |
| 33 | `IdIndiceRotacion` | `int` | ✓ |  |
| 34 | `alto` | `float` | ✓ |  |
| 35 | `largo` | `float` | ✓ |  |
| 36 | `ancho` | `float` | ✓ |  |
| 37 | `IdTramo` | `int` |  |  |
| 38 | `Ancho_ubicacion` | `float` | ✓ |  |
| 39 | `Largo_ubicacion` | `float` | ✓ |  |
| 40 | `Alto_ubicacion` | `float` | ✓ |  |
| 41 | `IndiceRotacion` | `nvarchar(50)` | ✓ |  |
| 42 | `Existencia_min_umbas` | `float` | ✓ |  |
| 43 | `Existencia_max_umbas` | `float` | ✓ |  |
| 44 | `costo` | `float` | ✓ |  |
| 45 | `Existencia_min_pres` | `float` | ✓ |  |
| 46 | `Existencia_max_pres` | `float` | ✓ |  |
| 47 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 48 | `IdUbicacionActual` | `int` |  |  |
| 49 | `Ubicacion_Nivel` | `int` | ✓ |  |
| 50 | `Ubicacion_Indice_X` | `int` | ✓ |  |
| 51 | `Ubicacion_Nombre` | `nvarchar(50)` | ✓ |  |
| 52 | `Ubicacion_Tramo` | `nvarchar(50)` | ✓ |  |
| 53 | `MotivoDevolucion` | `nvarchar(50)` |  |  |
| 54 | `NoPoliza` | `nvarchar(50)` |  |  |
| 55 | `IdClasificacion` | `int` | ✓ |  |
| 56 | `IdFamilia` | `int` | ✓ |  |
| 57 | `Dias_Local` | `int` | ✓ |  |
| 58 | `Dias_Exterior` | `int` | ✓ |  |
| 59 | `IdCliente` | `int` | ✓ |  |
| 60 | `Aplica` | `varchar(2)` |  |  |
| 61 | `Estado` | `nvarchar(50)` | ✓ |  |
| 62 | `IdPicking` | `bigint` | ✓ |  |
| 63 | `IdPedido` | `bigint` | ✓ |  |
| 64 | `IdTipoEtiqueta` | `int` | ✓ |  |
| 65 | `NombreArea` | `nvarchar(200)` | ✓ |  |
| 66 | `Clasificacion` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`
- `cliente_tiempos`
- `indice_rotacion`
- `motivo_devolucion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `stock_res`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_re_oc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_Por_Producto_Ubicacion_CI]
AS
SELECT dbo.producto_bodega.IdBodega, dbo.propietarios.IdPropietario, dbo.propietario_bodega.IdPropietarioBodega, dbo.producto.IdProducto, dbo.producto_bodega.IdProductoBodega, dbo.stock.IdStock,
dbo.stock.IdUbicacion_anterior, dbo.unidad_medida.IdUnidadMedida, dbo.stock.IdProductoEstado, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.propietarios.nombre_comercial AS Propietario, dbo.producto.codigo,
dbo.producto.codigo_barra AS Barra, dbo.producto.nombre, dbo.unidad_medida.Nombre AS UnidadMedida, dbo.producto_presentacion.nombre AS Presentacion, dbo.stock.lote, dbo.stock.fecha_ingreso AS Ingreso,
dbo.stock.fecha_vence AS Vence, dbo.stock.cantidad AS Cantidad_UMBas, SUM(ISNULL(dbo.stock_res.cantidad, 0)) AS CantidadReservadaUmBas, dbo.stock.cantidad - ISNULL(SUM(dbo.stock_res.cantidad), 0)
AS Disponible_UMBas, dbo.stock.peso, dbo.Nombre_Completo_Ubicacion(dbo.stock.IdUbicacion, dbo.stock.IdBodega) AS UbicacionCompleta, dbo.producto_estado.dañado, dbo.producto_presentacion.factor,
dbo.producto_estado.utilizable AS EstadoUtilizable, dbo.stock.IdUbicacion, dbo.stock.lic_plate, dbo.stock.serial, dbo.stock.añada, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto,
dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho AS Ancho_ubicacion, dbo.bodega_ubicacion.largo AS Largo_ubicacion,
dbo.bodega_ubicacion.alto AS Alto_ubicacion, dbo.indice_rotacion.Descripcion AS IndiceRotacion, dbo.producto.existencia_min AS Existencia_min_umbas, dbo.producto.existencia_max AS Existencia_max_umbas,
dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia AS Existencia_min_pres, dbo.producto_presentacion.MaximoExistencia AS Existencia_max_pres, dbo.stock.atributo_variante_1,
dbo.bodega_ubicacion.IdUbicacion AS IdUbicacionActual, dbo.bodega_ubicacion.nivel AS Ubicacion_Nivel, dbo.bodega_ubicacion.indice_x AS Ubicacion_Indice_X, dbo.bodega_ubicacion.descripcion AS Ubicacion_Nombre,
dbo.bodega_tramo.descripcion AS Ubicacion_Tramo, ISNULL(dbo.motivo_devolucion.Nombre, 'N/A') AS MotivoDevolucion, ISNULL(dbo.trans_oc_pol.NoPoliza, 'N/D') AS NoPoliza, dbo.producto_clasificacion.IdClasificacion,
dbo.producto_familia.IdFamilia, dbo.cliente_tiempos.Dias_Local, dbo.cliente_tiempos.Dias_Exterior, dbo.cliente_tiempos.IdCliente, CASE WHEN DATEDIFF(DAY, GETDATE(), dbo.stock.fecha_vence)
>= dbo.cliente_tiempos.Dias_Local OR
DATEDIFF(DAY, GETDATE(), dbo.stock.fecha_vence) >= dbo.cliente_tiempos.Dias_Exterior THEN 'Si' ELSE 'No' END AS Aplica, dbo.producto_estado.nombre AS Estado, dbo.stock_res.IdPicking, dbo.stock_res.IdPedido,
dbo.producto.IdTipoEtiqueta, dbo.Nombre_Area(dbo.bodega_ubicacion.IdArea, dbo.bodega_ubicacion.IdBodega) AS NombreArea, dbo.producto_clasificacion.nombre AS Clasificacion
FROM dbo.producto_bodega INNER JOIN
dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto LEFT OUTER JOIN
dbo.cliente_tiempos INNER JOIN
dbo.producto_familia ON dbo.cliente_tiempos.IdFamilia = dbo.producto_familia.IdFamilia ON dbo.producto.IdFamilia = dbo.producto_familia.IdFamilia LEFT OUTER JOIN
dbo.producto_clasificacion ON dbo.producto.IdClasificacion = dbo.producto_clasificacion.IdClasificacion RIGHT OUTER JOIN
dbo.unidad_medida INNER JOIN
dbo.propietarios INNER JOIN
dbo.stock INNER JOIN
dbo.propietario_bodega ON dbo.stock.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega ON dbo.propietarios.IdPropietario = dbo.propietario_bodega.IdPropietario ON
dbo.unidad_medida.IdUnidadMedida = dbo.stock.IdUnidadMedida INNER JOIN
dbo.bodega_tramo INNER JOIN
dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector AND
dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega ON dbo.stock.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.stock.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion ON
dbo.producto_bodega.IdProductoBodega = dbo.stock.IdProductoBodega LEFT OUTER JOIN
dbo.trans_oc_pol RIGHT OUTER JOIN
dbo.trans_re_oc INNER JOIN
dbo.trans_oc_enc ON dbo.trans_re_oc.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc LEFT OUTER JOIN
dbo.motivo_devolucion ON dbo.trans_oc_enc.IdMotivoDevolucion = dbo.motivo_devolucion.IdMotivoDevolucion ON dbo.trans_oc_pol.IdOrdenCompraEnc = dbo.trans_oc_enc.IdOrdenCompraEnc ON
dbo.stock.IdRecepcionEnc = dbo.trans_re_oc.IdRecepcionEnc LEFT OUTER JOIN
dbo.indice_rotacion ON dbo.producto.IdIndiceRotacion = dbo.indice_rotacion.IdIndiceRotacion LEFT OUTER JOIN
dbo.stock_res ON dbo.stock.IdStock = dbo.stock_res.IdStock LEFT OUTER JOIN
dbo.producto_estado ON dbo.stock.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
dbo.producto_presentacion ON dbo.stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion
WHERE (dbo.stock.IdUbicacion NOT IN
(SELECT IdUbicacion
FROM dbo.bodega_ubicacion AS bodega_ubicacion_1
WHERE (ubicacion_despacho = 1)))
GROUP BY dbo.propietarios.nombre_comercial, dbo.propietarios.IdPropietario, dbo.stock.IdStock, dbo.bodega_ubicacion.IdUbicacion, dbo.stock.IdUbicacion_anterior, dbo.propietario_bodega.IdPropietarioBodega,
dbo.producto_bodega.IdProductoBodega, dbo.unidad_medida.IdUnidadMedida, dbo.unidad_medida.Nombre, dbo.producto_presentacion.nombre, dbo.producto.IdProducto, dbo.producto.codigo, dbo.producto.nombre,
dbo.stock.lote, dbo.stock.fecha_ingreso, dbo.stock.serial, dbo.stock.añada, dbo.producto_bodega.IdBodega, dbo.stock.fecha_vence, dbo.stock.IdProductoEstado, dbo.producto_estado.nombre, dbo.producto_estado.utilizable,
dbo.producto_estado.dañado, dbo.stock.IdUbicacion, dbo.stock.IdPresentacion, dbo.stock.IdRecepcionEnc, dbo.stock.lic_plate, dbo.stock.peso, dbo.producto.IdIndiceRotacion, dbo.producto_presentacion.alto,
dbo.producto_presentacion.largo, dbo.producto_presentacion.ancho, dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdTramo, dbo.bodega_ubicacion.ancho, dbo.bodega_ubicacion.largo, dbo.bodega_ubicacion.alto,
dbo.indice_rotacion.Descripcion, dbo.producto.existencia_min, dbo.producto.existencia_max, dbo.producto.codigo_barra, dbo.producto.costo, dbo.producto_presentacion.MinimoExistencia,
dbo.producto_presentacion.MaximoExistencia, dbo.stock.cantidad, dbo.stock.cantidad / dbo.producto_presentacion.factor, dbo.stock.atributo_variante_1, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.indice_x,
dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.bodega_ubicacion.orientacion_pos, dbo.motivo_devolucion.Nombre, dbo.trans_oc_pol.NoPoliza, dbo.producto_clasificacion.IdClasificacion,
dbo.producto_familia.IdFamilia, dbo.cliente_tiempos.Dias_Local, dbo.cliente_tiempos.Dias_Exterior, dbo.cliente_tiempos.IdCliente, dbo.stock.IdBodega, dbo.stock_res.IdPicking, dbo.stock_res.IdPedido,
dbo.producto.IdTipoEtiqueta, dbo.bodega_ubicacion.IdArea, dbo.bodega_ubicacion.IdBodega, dbo.producto_clasificacion.nombre
```
