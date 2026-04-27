---
id: db-brain-view-vw-stock-serie-parametro
type: db-view
title: dbo.VW_Stock_Serie_Parametro
schema: dbo
name: VW_Stock_Serie_Parametro
kind: view
modify_date: 2017-05-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Stock_Serie_Parametro`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-05-02 |
| Columnas | 13 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `StockId` | `int` |  |  |
| 2 | `Propietario` | `nvarchar(100)` |  |  |
| 3 | `Producto` | `nvarchar(50)` | ✓ |  |
| 4 | `Código Producto` | `nvarchar(50)` | ✓ |  |
| 5 | `Código de Barra` | `nvarchar(35)` | ✓ |  |
| 6 | `Estado` | `nvarchar(50)` | ✓ |  |
| 7 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 8 | `U.M. Bas` | `nvarchar(50)` | ✓ |  |
| 9 | `Cantidad` | `float` | ✓ |  |
| 10 | `Fecha Ingreso` | `datetime` | ✓ |  |
| 11 | `Fecha Vencimiento` | `datetime` | ✓ |  |
| 12 | `Lote` | `nvarchar(50)` | ✓ |  |
| 13 | `Recepción` | `int` | ✓ |  |

## Consume

- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `stock`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW VW_Stock_Serie_Parametro
AS
SELECT s.IdStock AS StockId, p.nombre_comercial AS Propietario,
pr.Nombre AS Producto, pr.Codigo AS 'Código Producto',pr.codigo_barra AS 'Código de Barra',pe.nombre AS Estado, 
pp.nombre AS Presentación,u.Nombre AS 'U.M. Bas',s.Cantidad,s.fecha_ingreso AS 'Fecha Ingreso',
s.Fecha_Vence AS 'Fecha Vencimiento',s.Lote,s.IdRecepcionEnc AS Recepción
FROM stock AS s
INNER JOIN propietario_bodega AS pb ON s.IdPropietarioBodega = pb.IdPropietarioBodega
INNER JOIN propietarios AS p ON pb.IdPropietario = p.IdPropietario
INNER JOIN producto_bodega AS prb ON s.IdProductoBodega = prb.IdProductoBodega
INNER JOIN producto AS pr ON prb.IdProducto = pr.IdProducto
LEFT JOIN producto_estado AS pe ON s.IdProductoEstado = pe.IdEstado AND p.IdPropietario = pe.IdPropietario
LEFT JOIN producto_presentacion AS pp ON s.IdPresentacion = pp.IdPresentacion AND pr.IdProducto = pp.IdProducto
LEFT JOIN unidad_medida AS u ON s.IdUnidadMedida = u.IdUnidadMedida AND p.IdPropietario = u.IdPropietario
```
