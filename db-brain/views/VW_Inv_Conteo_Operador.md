---
id: db-brain-view-vw-inv-conteo-operador
type: db-view
title: dbo.VW_Inv_Conteo_Operador
schema: dbo
name: VW_Inv_Conteo_Operador
kind: view
modify_date: 2018-10-03
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Inv_Conteo_Operador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-10-03 |
| Columnas | 17 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventarioenc` | `int` |  |  |
| 2 | `codigo` | `nvarchar(50)` | ✓ |  |
| 3 | `Producto` | `nvarchar(100)` | ✓ |  |
| 4 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 5 | `Propietario` | `nvarchar(100)` |  |  |
| 6 | `Estado_Producto` | `nvarchar(50)` | ✓ |  |
| 7 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 8 | `lote` | `nvarchar(50)` | ✓ |  |
| 9 | `fecha_vence` | `datetime` | ✓ |  |
| 10 | `Cantidad_Conteo` | `float` |  |  |
| 11 | `Peso_Conteo` | `float` | ✓ |  |
| 12 | `Fecha_Ingreso` | `datetime` | ✓ |  |
| 13 | `Operador` | `nvarchar(201)` | ✓ |  |
| 14 | `IdStock` | `int` |  |  |
| 15 | `Cantidad_Stock` | `float` | ✓ |  |
| 16 | `peso_stock` | `float` | ✓ |  |
| 17 | `UMBas` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `operador`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietarios`
- `trans_inv_ciclico`
- `trans_inv_enc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Inv_Conteo_Operador]
AS
SELECT dbo.trans_inv_enc.idinventarioenc, dbo.producto.codigo, dbo.producto.nombre AS Producto, dbo.bodega.nombre AS Bodega,
dbo.propietarios.nombre_comercial AS Propietario, dbo.producto_estado.nombre AS Estado_Producto, dbo.producto_presentacion.nombre AS Presentacion,
dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.cantidad AS Cantidad_Conteo, dbo.trans_inv_ciclico.peso AS Peso_Conteo,
dbo.trans_inv_enc.fec_agr AS Fecha_Ingreso, dbo.operador.nombres + ' ' + dbo.operador.apellidos AS Operador, dbo.trans_inv_ciclico.IdStock,
dbo.trans_inv_ciclico.cant_stock AS Cantidad_Stock, dbo.trans_inv_ciclico.peso_stock, dbo.unidad_medida.Nombre AS UMBas
FROM dbo.unidad_medida INNER JOIN
dbo.operador INNER JOIN
dbo.trans_inv_ciclico INNER JOIN
dbo.trans_inv_enc ON dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc INNER JOIN
dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
dbo.producto ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto INNER JOIN
dbo.propietarios ON dbo.trans_inv_enc.idpropietario = dbo.propietarios.IdPropietario INNER JOIN
dbo.bodega ON dbo.trans_inv_enc.idbodega = dbo.bodega.IdBodega ON dbo.operador.IdOperador = dbo.trans_inv_ciclico.idoperador ON
dbo.unidad_medida.IdUnidadMedida = dbo.producto.IdUnidadMedidaBasica LEFT OUTER JOIN
dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
```
