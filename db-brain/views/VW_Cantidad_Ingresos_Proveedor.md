---
id: db-brain-view-vw-cantidad-ingresos-proveedor
type: db-view
title: dbo.VW_Cantidad_Ingresos_Proveedor
schema: dbo
name: VW_Cantidad_Ingresos_Proveedor
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Cantidad_Ingresos_Proveedor`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 7 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `nombre` | `nvarchar(100)` | ✓ |  |
| 2 | `Ingresos` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `Fecha_Creacion` | `datetime` | ✓ |  |
| 5 | `TipoDocumento` | `nvarchar(50)` | ✓ |  |
| 6 | `Proveedor` | `nvarchar(100)` | ✓ |  |
| 7 | `Codigo_Bodega` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_enc`
- `trans_oc_ti`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Cantidad_Ingresos_Proveedor]
AS
SELECT p.nombre, COUNT(oc.IdOrdenCompraEnc) AS Ingresos, oc.IdBodega, oc.Fecha_Creacion, ti.Nombre AS TipoDocumento, p.nombre AS Proveedor, dbo.bodega.codigo AS Codigo_Bodega
FROM     dbo.trans_oc_enc AS oc INNER JOIN
                  dbo.proveedor_bodega AS pb ON pb.IdAsignacion = oc.IdProveedorBodega INNER JOIN
                  dbo.proveedor AS p ON p.IdProveedor = pb.IdProveedor INNER JOIN
                  dbo.trans_oc_ti AS ti ON oc.IdTipoIngresoOC = ti.IdTipoIngresoOC INNER JOIN
                  dbo.bodega ON oc.IdBodega = dbo.bodega.IdBodega
WHERE  (oc.IdEstadoOC <> 5)
GROUP BY p.nombre, oc.IdBodega, oc.Fecha_Creacion, ti.Nombre, dbo.bodega.codigo
```
