---
id: db-brain-view-vw-recepcioncostoarancel
type: db-view
title: dbo.VW_RecepcionCostoArancel
schema: dbo
name: VW_RecepcionCostoArancel
kind: view
modify_date: 2017-10-13
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_RecepcionCostoArancel`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2017-10-13 |
| Columnas | 18 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `fecha_recepcion` | `datetime` | ✓ |  |
| 2 | `Proveedor` | `nvarchar(50)` | ✓ |  |
| 3 | `hora_ini_pc` | `datetime` | ✓ |  |
| 4 | `hora_fin_pc` | `datetime` | ✓ |  |
| 5 | `No_Marchamo` | `nvarchar(50)` | ✓ |  |
| 6 | `No_Documento` | `nvarchar(25)` | ✓ |  |
| 7 | `NoPoliza` | `nvarchar(50)` | ✓ |  |
| 8 | `No_Linea` | `int` | ✓ |  |
| 9 | `codigo` | `nvarchar(50)` | ✓ |  |
| 10 | `nombre` | `nvarchar(50)` | ✓ |  |
| 11 | `cantidad` | `float` | ✓ |  |
| 12 | `cantidad_recibida` | `float` | ✓ |  |
| 13 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 14 | `Estado` | `nvarchar(20)` | ✓ |  |
| 15 | `costo` | `float` | ✓ |  |
| 16 | `Arancel` | `nvarchar(150)` | ✓ |  |
| 17 | `IdOrdenCompraEnc` | `int` |  |  |
| 18 | `IdRecepcionEnc` | `int` |  |  |

## Consume

- `arancel`
- `producto`
- `producto_bodega`
- `producto_presentacion`
- `proveedor`
- `proveedor_bodega`
- `trans_oc_det`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_re_det`
- `trans_re_enc`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_RecepcionCostoArancel]
AS
SELECT        reenc.fecha_recepcion, p.nombre AS Proveedor, reenc.hora_ini_pc, reenc.hora_fin_pc, oc.No_Marchamo, oc.No_Documento, pol.NoPoliza, det.No_Linea, pr.codigo, 
                         pr.nombre, det.cantidad, det.cantidad_recibida, pp.nombre AS Presentacion, reenc.estado AS Estado, det.costo, a.nombre AS Arancel, re.IdOrdenCompraEnc, 
                         re.IdRecepcionEnc
FROM            dbo.trans_re_oc AS re INNER JOIN
                         dbo.trans_re_enc AS reenc ON re.IdRecepcionEnc = reenc.IdRecepcionEnc INNER JOIN
                         dbo.trans_oc_enc AS oc ON re.IdOrdenCompraEnc = re.IdOrdenCompraEnc INNER JOIN
                         dbo.trans_oc_pol AS pol ON oc.IdOrdenCompraEnc = pol.IdOrdenCompraEnc INNER JOIN
                         dbo.trans_oc_det AS det ON oc.IdOrdenCompraEnc = det.IdOrdenCompraEnc INNER JOIN
                         dbo.producto_bodega AS prb ON det.IdProductoBodega = prb.IdProductoBodega INNER JOIN
                         dbo.producto AS pr ON prb.IdProducto = pr.IdProducto INNER JOIN
                         dbo.producto_presentacion AS pp ON pr.IdProducto = pp.IdProducto INNER JOIN
                         dbo.proveedor_bodega ON oc.IdProveedorBodega = dbo.proveedor_bodega.IdAsignacion INNER JOIN
                         dbo.proveedor AS p ON dbo.proveedor_bodega.IdProveedor = p.IdProveedor LEFT OUTER JOIN
                         dbo.trans_re_det AS redet ON reenc.IdRecepcionEnc = redet.IdRecepcionEnc LEFT OUTER JOIN
                         dbo.arancel AS a ON det.IdArancel = a.IdArancel
```
