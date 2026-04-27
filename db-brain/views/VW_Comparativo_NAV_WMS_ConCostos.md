---
id: db-brain-view-vw-comparativo-nav-wms-concostos
type: db-view
title: dbo.VW_Comparativo_NAV_WMS_ConCostos
schema: dbo
name: VW_Comparativo_NAV_WMS_ConCostos
kind: view
modify_date: 2018-08-31
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Comparativo_NAV_WMS_ConCostos`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-08-31 |
| Columnas | 16 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdInventario` | `int` |  |  |
| 2 | `Tipo` | `nvarchar(50)` | ✓ |  |
| 3 | `codigo` | `nvarchar(50)` | ✓ |  |
| 4 | `Nombre` | `nvarchar(100)` | ✓ |  |
| 5 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 6 | `Stock_WMS` | `float` | ✓ |  |
| 7 | `Teorico_ERP` | `float` | ✓ |  |
| 8 | `Dif_ERP` | `float` | ✓ |  |
| 9 | `Conteo` | `float` | ✓ |  |
| 10 | `Dif_Conteo` | `float` | ✓ |  |
| 11 | `lote` | `nvarchar(50)` | ✓ |  |
| 12 | `Fecha_Vence` | `datetime` | ✓ |  |
| 13 | `Costo_Nav` | `float` | ✓ |  |
| 14 | `Costo_Conteo` | `float` | ✓ |  |
| 15 | `Dif_Costo` | `float` | ✓ |  |
| 16 | `costo` | `float` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_presentacion`
- `producto_tipo`
- `trans_inv_ciclico`
- `trans_inv_stock`
- `trans_inv_stock_prod`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Comparativo_NAV_WMS_ConCostos]
AS
SELECT        TOP (100) PERCENT IdInventario, TipoProducto AS Tipo, codigo, Producto AS Nombre, UMBas, SUM(Inventario) AS Stock_WMS, SUM(Stock) AS Teorico_ERP,
                         ROUND(SUM(Inventario) - SUM(Stock), 6) AS Dif_ERP, SUM(Conteo) AS Conteo, ROUND(SUM(Conteo) - SUM(Stock), 6) AS Dif_Conteo, lote, CONVERT(datetime,
                         fecha_vence) AS Fecha_Vence, ROUND(SUM(Stock) * SUM(costo), 6) AS Costo_Nav, ROUND(SUM(Conteo) * SUM(costo), 6) AS Costo_Conteo, ROUND(SUM(Stock)
                         * SUM(costo), 6) - ROUND(SUM(Conteo) * SUM(costo), 6) AS Dif_Costo, costo
FROM            (SELECT        dbo.trans_inv_stock.idinventario AS IdInventario, dbo.producto.codigo, dbo.producto.IdProducto, dbo.producto.nombre AS Producto,
                                                    SUM(dbo.trans_inv_stock.cantidad) AS Inventario, 0 AS Stock, 0 AS Conteo, SUM(dbo.trans_inv_stock.peso) AS Peso, dbo.trans_inv_stock.lote,
                                                    CONVERT(date, dbo.trans_inv_stock.fecha_vence) AS fecha_vence, dbo.producto_tipo.NombreTipoProducto AS TipoProducto,
                                                    dbo.unidad_medida.Nombre AS UMBas, dbo.producto.costo
                          FROM            dbo.trans_inv_stock INNER JOIN
                                                    dbo.producto ON dbo.trans_inv_stock.IdProductoBodega = dbo.producto.IdProducto INNER JOIN
                                                    dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto INNER JOIN
                                                    dbo.unidad_medida ON dbo.trans_inv_stock.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                                                    dbo.producto_presentacion ON dbo.trans_inv_stock.IdPresentacion = dbo.producto_presentacion.IdPresentacion
                          GROUP BY dbo.trans_inv_stock.idinventario, dbo.producto.codigo, dbo.producto.nombre, dbo.producto_presentacion.IdPresentacion, dbo.producto.IdProducto,
                                                    dbo.trans_inv_stock.lote, dbo.trans_inv_stock.fecha_vence, dbo.producto_tipo.NombreTipoProducto, dbo.unidad_medida.Nombre,
                                                    dbo.producto.costo
                          UNION
                          SELECT        dbo.trans_inv_stock_prod.idinventario AS IdInventario, producto_2.codigo, producto_2.IdProducto, producto_2.nombre AS Producto, 0 AS Detalle,
                                                   SUM(dbo.trans_inv_stock_prod.cant) AS Stock, 0 AS Conteo, 0 AS Peso, dbo.trans_inv_stock_prod.lote, dbo.trans_inv_stock_prod.fecha_vence,
                                                   producto_tipo_2.NombreTipoProducto AS TipoProducto, unidad_medida_2.Nombre AS UMBas, producto_2.costo
                          FROM            dbo.trans_inv_stock_prod INNER JOIN
                                                   dbo.producto AS producto_2 ON dbo.trans_inv_stock_prod.idProducto = producto_2.IdProducto INNER JOIN
                                                   dbo.producto_tipo AS producto_tipo_2 ON producto_2.IdTipoProducto = producto_tipo_2.IdTipoProducto INNER JOIN
                                                   dbo.unidad_medida AS unidad_medida_2 ON dbo.trans_inv_stock_prod.idUnidadMedida = unidad_medida_2.IdUnidadMedida LEFT OUTER JOIN
                                                   dbo.producto_presentacion AS producto_presentacion_2 ON dbo.trans_inv_stock_prod.idPresentacion = producto_presentacion_2.IdPresentacion
                          GROUP BY dbo.trans_inv_stock_prod.idinventario, producto_2.codigo, producto_2.nombre, producto_2.IdProducto, dbo.trans_inv_stock_prod.lote,
                                                   dbo.trans_inv_stock_prod.fecha_vence, producto_tipo_2.NombreTipoProducto, unidad_medida_2.Nombre, producto_2.costo
                          UNION
                          SELECT        dbo.trans_inv_ciclico.idinventarioenc AS IdInventario, producto_1.codigo, producto_1.IdProducto, producto_1.nombre AS Producto, 0 AS Detalle,
                                                   0 AS stock, SUM(dbo.trans_inv_ciclico.cantidad) AS Conteo, 0 AS Peso, dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.fecha_vence,
                                                   producto_tipo_1.NombreTipoProducto AS TipoProducto, unidad_medida_1.Nombre AS UMBas, producto_1.costo
                          FROM            dbo.trans_inv_ciclico INNER JOIN
                                                   dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                                                   dbo.producto AS producto_1 ON producto_1.IdProducto = dbo.producto_bodega.IdProducto INNER JOIN
                                                   dbo.producto_tipo AS producto_tipo_1 ON producto_1.IdTipoProducto = producto_tipo_1.IdTipoProducto INNER JOIN
                                                   dbo.unidad_medida AS unidad_medida_1 ON producto_1.IdUnidadMedidaBasica = unidad_medida_1.IdUnidadMedida LEFT OUTER JOIN
                                                   dbo.producto_presentacion AS producto_presentacion_1 ON dbo.trans_inv_ciclico.IdPresentacion = producto_presentacion_1.IdPresentacion
                          GROUP BY dbo.trans_inv_ciclico.idinventarioenc, producto_1.codigo, producto_1.IdProducto, producto_1.nombre, producto_presentacion_1.nombre,
                                                   dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.fecha_vence, producto_tipo_1.NombreTipoProducto, unidad_medida_1.Nombre, producto_1.costo)
                         AS T
GROUP BY lote, codigo, Producto, fecha_vence, TipoProducto, UMBas, IdInventario, costo
ORDER BY codigo
```
