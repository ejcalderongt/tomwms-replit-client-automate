---
id: db-brain-view-vw-ordencomprapreingreso
type: db-view
title: dbo.VW_OrdenCompraPreIngreso
schema: dbo
name: VW_OrdenCompraPreIngreso
kind: view
modify_date: 2018-10-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_OrdenCompraPreIngreso`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2018-10-11 |
| Columnas | 13 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Fecha_Creacion` | `datetime` | ✓ |  |
| 2 | `Proveedor` | `nvarchar(50)` | ✓ |  |
| 3 | `No_Documento` | `nvarchar(30)` | ✓ |  |
| 4 | `No_Linea` | `int` | ✓ |  |
| 5 | `codigo` | `nvarchar(50)` | ✓ |  |
| 6 | `Producto` | `nvarchar(100)` | ✓ |  |
| 7 | `cantidad` | `float` | ✓ |  |
| 8 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 9 | `IdSimbologia` | `int` | ✓ |  |
| 10 | `IdOrdenCompraEnc` | `int` |  |  |
| 11 | `IdOrdenCompraDet` | `int` |  |  |
| 12 | `IdProveedor` | `int` |  |  |
| 13 | `MotivoDevolucion` | `nvarchar(50)` | ✓ |  |

## Consume

- `motivo_devolucion`
- `producto`
- `producto_bodega`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_det`
- `trans_oc_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW dbo.VW_OrdenCompraPreIngreso
AS
SELECT        enc.Fecha_Creacion, p.nombre AS Proveedor, enc.No_Documento, det.No_Linea, pr.codigo, pr.nombre AS Producto, det.cantidad, pr.codigo_barra, pr.IdSimbologia, 
                         enc.IdOrdenCompraEnc, det.IdOrdenCompraDet, p.IdProveedor, dbo.motivo_devolucion.Nombre AS MotivoDevolucion
FROM            dbo.trans_oc_enc AS enc INNER JOIN
                         dbo.trans_oc_det AS det ON enc.IdOrdenCompraEnc = det.IdOrdenCompraEnc INNER JOIN
                         dbo.proveedor_bodega AS pb ON enc.IdProveedorBodega = pb.IdAsignacion INNER JOIN
                         dbo.proveedor AS p ON p.IdProveedor = pb.IdProveedor INNER JOIN
                         dbo.producto_bodega AS prb ON det.IdProductoBodega = prb.IdProductoBodega INNER JOIN
                         dbo.producto AS pr ON prb.IdProducto = pr.IdProducto LEFT OUTER JOIN
                         dbo.motivo_devolucion ON enc.IdMotivoDevolucion = dbo.motivo_devolucion.IdMotivoDevolucion
```
