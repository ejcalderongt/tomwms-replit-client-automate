---
id: db-brain-view-vw-get-detalle-by-idrecepcionenc
type: db-view
title: dbo.VW_Get_Detalle_By_IdRecepcionEnc
schema: dbo
name: VW_Get_Detalle_By_IdRecepcionEnc
kind: view
modify_date: 2023-02-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Get_Detalle_By_IdRecepcionEnc`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-02-27 |
| Columnas | 41 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` | ✓ |  |
| 2 | `IdRecepcionEnc` | `int` |  |  |
| 3 | `IdRecepcionDet` | `int` |  |  |
| 4 | `IdJornadaSistema` | `int` | ✓ |  |
| 5 | `IdUbicacionRecepcion` | `int` | ✓ |  |
| 6 | `IdPropietarioBodega` | `int` |  |  |
| 7 | `IdProducto` | `int` |  |  |
| 8 | `control_peso` | `bit` | ✓ |  |
| 9 | `IdProductoBodega` | `int` |  |  |
| 10 | `IdPresentacion` | `int` | ✓ |  |
| 11 | `IdUnidadMedida` | `int` | ✓ |  |
| 12 | `IdProductoEstado` | `int` | ✓ |  |
| 13 | `IdOperadorBodega` | `int` | ✓ |  |
| 14 | `IdMotivoDevolucion` | `int` | ✓ |  |
| 15 | `No_Linea` | `int` | ✓ |  |
| 16 | `cantidad_recibida` | `float` | ✓ |  |
| 17 | `nombre_producto` | `nvarchar(100)` | ✓ |  |
| 18 | `nombre_presentacion` | `nvarchar(50)` | ✓ |  |
| 19 | `nombre_unidad_medida` | `nvarchar(50)` | ✓ |  |
| 20 | `nombre_producto_estado` | `nvarchar(50)` | ✓ |  |
| 21 | `lote` | `nvarchar(50)` | ✓ |  |
| 22 | `fecha_vence` | `datetime` | ✓ |  |
| 23 | `fecha_ingreso` | `datetime` | ✓ |  |
| 24 | `peso` | `float` | ✓ |  |
| 25 | `peso_estadistico` | `float` | ✓ |  |
| 26 | `peso_minimo` | `float` | ✓ |  |
| 27 | `peso_maximo` | `float` | ✓ |  |
| 28 | `peso_unitario` | `float` | ✓ |  |
| 29 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 30 | `fec_agr` | `datetime` | ✓ |  |
| 31 | `observacion` | `nvarchar(150)` | ✓ |  |
| 32 | `añada` | `int` | ✓ |  |
| 33 | `costo` | `float` | ✓ |  |
| 34 | `costo_oc` | `float` | ✓ |  |
| 35 | `costo_estadistico` | `float` | ✓ |  |
| 36 | `atributo_variante_1` | `nvarchar(25)` | ✓ |  |
| 37 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 38 | `lic_plate` | `nvarchar(50)` | ✓ |  |
| 39 | `pallet_no_estandar` | `bit` | ✓ |  |
| 40 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 41 | `IdOrdenCompraDet` | `int` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `producto`
- `producto_bodega`
- `propietario_bodega`
- `trans_re_det`
- `trans_re_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Get_Detalle_By_IdRecepcionEnc]
AS
SELECT        enc.IdBodega, det.IdRecepcionEnc, det.IdRecepcionDet, det.IdJornadaSistema, enc.IdUbicacionRecepcion, prb.IdPropietarioBodega, p.IdProducto, p.control_peso, det.IdProductoBodega, det.IdPresentacion, det.IdUnidadMedida, det.IdProductoEstado, 
                         det.IdOperadorBodega, det.IdMotivoDevolucion, det.No_Linea, det.cantidad_recibida, det.nombre_producto, det.nombre_presentacion, det.nombre_unidad_medida, det.nombre_producto_estado, det.lote, det.fecha_vence, 
                         det.fecha_ingreso, det.peso, det.peso_estadistico, det.peso_minimo, det.peso_maximo, det.peso_unitario, det.user_agr, det.fec_agr, det.observacion, det.añada, det.costo, det.costo_oc, det.costo_estadistico, 
                         det.atributo_variante_1, det.codigo_producto, det.lic_plate, det.pallet_no_estandar, det.IdOrdenCompraEnc, det.IdOrdenCompraDet
FROM            dbo.trans_re_det AS det INNER JOIN
                         dbo.producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                         dbo.trans_re_enc AS enc ON det.IdRecepcionEnc = enc.IdRecepcionEnc INNER JOIN
                         dbo.bodega_ubicacion AS b ON enc.IdUbicacionRecepcion = b.IdUbicacion AND enc.IdBodega = b.IdBodega INNER JOIN
                         dbo.propietario_bodega AS prb ON enc.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto
```
