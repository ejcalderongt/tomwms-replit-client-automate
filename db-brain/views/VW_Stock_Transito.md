---
id: db-brain-view-vw-stock-transito
type: db-view
title: dbo.VW_Stock_Transito
schema: dbo
name: VW_Stock_Transito
kind: view
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Transito`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-02-11 |
| Columnas | 18 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Documento_Ingreso` | `int` |  |  |
| 2 | `Fecha` | `datetime` | ✓ |  |
| 3 | `No_Documento` | `nvarchar(30)` | ✓ |  |
| 4 | `Tipo_Documento` | `nvarchar(50)` | ✓ |  |
| 5 | `Propietario` | `nvarchar(100)` |  |  |
| 6 | `Código` | `nvarchar(50)` | ✓ |  |
| 7 | `Producto` | `nvarchar(100)` | ✓ |  |
| 8 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 9 | `Bodega_Origen` | `nvarchar(100)` | ✓ |  |
| 10 | `cantidad` | `float` | ✓ |  |
| 11 | `Cantidad Recibida` | `float` | ✓ |  |
| 12 | `Cantidad Pendiente` | `float` | ✓ |  |
| 13 | `costo` | `float` | ✓ |  |
| 14 | `Total` | `float` | ✓ |  |
| 15 | `BodegaDestino` | `int` |  |  |
| 16 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 17 | `Observacion` | `text` | ✓ |  |
| 18 | `IdPropietario` | `int` | ✓ |  |

## Consume

- `bodega`
- `producto`
- `producto_bodega`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_det`
- `trans_oc_enc`
- `trans_oc_ti`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Stock_Transito]
AS
SELECT TOP (100) PERCENT enc.IdOrdenCompraEnc AS Documento_Ingreso, enc.Fecha_Creacion AS Fecha, enc.No_Documento, dbo.trans_oc_ti.Nombre AS Tipo_Documento, pr.nombre_comercial AS Propietario, p.codigo AS Código, 
                  p.nombre AS Producto, pp.nombre AS Presentación, dbo.proveedor.nombre AS Bodega_Origen, det.cantidad, det.cantidad_recibida AS [Cantidad Recibida], det.cantidad - det.cantidad_recibida AS [Cantidad Pendiente], det.costo, 
                  det.total_linea AS Total, dbo.bodega.IdBodega AS BodegaDestino, enc.Referencia, enc.Observacion, prb.IdPropietario
FROM     dbo.trans_oc_enc AS enc INNER JOIN
                  dbo.propietario_bodega AS prb ON enc.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                  dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
                  dbo.trans_oc_det AS det ON enc.IdOrdenCompraEnc = det.IdOrdenCompraEnc INNER JOIN
                  dbo.producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                  dbo.producto AS p ON pb.IdProducto = p.IdProducto INNER JOIN
                  dbo.proveedor ON pr.IdPropietario = dbo.proveedor.IdPropietario INNER JOIN
                  dbo.proveedor_bodega ON enc.IdProveedorBodega = dbo.proveedor_bodega.IdAsignacion AND dbo.proveedor.IdProveedor = dbo.proveedor_bodega.IdProveedor INNER JOIN
                  dbo.bodega ON enc.IdBodega = dbo.bodega.IdBodega INNER JOIN
                  dbo.trans_oc_ti ON enc.IdTipoIngresoOC = dbo.trans_oc_ti.IdTipoIngresoOC LEFT OUTER JOIN
                  dbo.producto_presentacion AS pp ON det.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                  dbo.unidad_medida AS u ON det.IdUnidadMedidaBasica = u.IdUnidadMedida
WHERE  (det.cantidad - det.cantidad_recibida > 0)
ORDER BY Documento_Ingreso
```
