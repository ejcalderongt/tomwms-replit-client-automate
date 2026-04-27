---
id: db-brain-view-vw-servicio
type: db-view
title: dbo.VW_Servicio
schema: dbo
name: VW_Servicio
kind: view
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Servicio`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-08-25 |
| Columnas | 12 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdBodega` | `int` |  |  |
| 2 | `Almacen` | `varchar(7)` |  |  |
| 3 | `IdCliente` | `int` | ✓ |  |
| 4 | `Nombre_Cliente` | `nvarchar(100)` |  |  |
| 5 | `IdPropietario_Enc` | `int` | ✓ |  |
| 6 | `no_orden` | `nvarchar(50)` | ✓ |  |
| 7 | `Tipo_Transaccion` | `varchar(8)` |  |  |
| 8 | `No_Linea` | `int` | ✓ |  |
| 9 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 10 | `Nombre_Producto` | `nvarchar(150)` | ✓ |  |
| 11 | `Cantidad` | `int` | ✓ |  |
| 12 | `Fecha_Servicio` | `date` | ✓ |  |

## Consume

- `bodega`
- `propietarios`
- `trans_servicio_det`
- `trans_servicio_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Servicio]
AS
SELECT        e.IdBodega, CASE WHEN b.es_bodega_fiscal = 1 THEN 'Fiscal' ELSE 'General' END AS Almacen, e.IdPropietario AS IdCliente, p.nombre_comercial AS Nombre_Cliente, e.IdPropietario AS IdPropietario_Enc, e.no_orden, 
                         'SERVICIO' AS Tipo_Transaccion, d.IdServicioDet - MIN(d.IdServicioDet) + 1 AS No_Linea, d.codigo_producto, d.nombre_servicio AS Nombre_Producto, SUM(d.cantidad) AS Cantidad, CONVERT(Date, e.fecha_servicio) 
                         AS Fecha_Servicio
FROM            dbo.trans_servicio_enc AS e INNER JOIN
                         dbo.trans_servicio_det AS d ON e.IdServicioEnc = d.IdServicioEnc INNER JOIN
                         dbo.propietarios AS p ON p.IdPropietario = d.IdPropietario INNER JOIN
                         dbo.bodega AS b ON e.IdBodega = b.IdBodega
GROUP BY e.IdBodega, b.es_bodega_fiscal, e.IdPropietario, p.nombre_comercial, e.no_orden, d.IdServicioDet, d.codigo_producto, d.nombre_servicio, CONVERT(Date, e.fecha_servicio)
```
