---
id: db-brain-view-vw-conteo-by-operador
type: db-view
title: dbo.VW_Conteo_By_Operador
schema: dbo
name: VW_Conteo_By_Operador
kind: view
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Conteo_By_Operador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-02-11 |
| Columnas | 10 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idinventarioenc` | `int` |  |  |
| 2 | `Ubicacion` | `nvarchar(200)` | ✓ |  |
| 3 | `IdStock` | `int` |  |  |
| 4 | `lic_plate` | `nvarchar(100)` | ✓ |  |
| 5 | `Codigo` | `nvarchar(50)` | ✓ |  |
| 6 | `CodigoBarra` | `nvarchar(35)` | ✓ |  |
| 7 | `Nombre` | `nvarchar(100)` | ✓ |  |
| 8 | `Operador` | `nvarchar(201)` |  |  |
| 9 | `Teorico` | `float` | ✓ |  |
| 10 | `Conteo` | `float` | ✓ |  |

## Consume

- `Nombre_Completo_Ubicacion`
- `operador`
- `producto`
- `producto_bodega`
- `trans_inv_ciclico`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
create VIEW [dbo].[VW_Conteo_By_Operador]
AS
SELECT trans_inv_ciclico.idinventarioenc, 
	   dbo.Nombre_Completo_Ubicacion(trans_inv_ciclico.IdUbicacion, trans_inv_ciclico.IdBodega) AS Ubicacion, 
	   IdStock, lic_plate,
	   producto.codigo AS Codigo, producto.codigo_barra AS CodigoBarra, producto.nombre AS Nombre, 
	   CONCAT(operador.nombres,' ', operador.apellidos) AS Operador,
	   trans_inv_ciclico.cant_stock AS Teorico, 
	   SUM(trans_inv_ciclico.cantidad) AS Conteo				 
FROM   trans_inv_ciclico INNER JOIN
       producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
       producto ON producto_bodega.IdProducto = producto.IdProducto LEFT OUTER JOIN
       operador ON trans_inv_ciclico.idoperador = operador.IdOperador
GROUP BY trans_inv_ciclico.idinventarioenc, operador.nombres, producto.codigo, producto.codigo_barra, 
         producto.nombre, trans_inv_ciclico.cant_stock, trans_inv_ciclico.IdUbicacion, 
         trans_inv_ciclico.IdBodega, operador.apellidos, IdStock, lic_plate
```
