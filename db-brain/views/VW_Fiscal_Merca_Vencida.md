---
id: db-brain-view-vw-fiscal-merca-vencida
type: db-view
title: dbo.VW_Fiscal_Merca_Vencida
schema: dbo
name: VW_Fiscal_Merca_Vencida
kind: view
modify_date: 2022-06-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Fiscal_Merca_Vencida`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-06-28 |
| Columnas | 31 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` |  |  |
| 2 | `Fecha_Creacion` | `date` | ✓ |  |
| 3 | `Fecha_Recepcion` | `date` | ✓ |  |
| 4 | `IdStock` | `int` |  |  |
| 5 | `IdProducto` | `int` |  |  |
| 6 | `codigo` | `nvarchar(50)` | ✓ |  |
| 7 | `material` | `nvarchar(100)` | ✓ |  |
| 8 | `IdPropietario` | `int` |  |  |
| 9 | `nombre_cliente` | `nvarchar(100)` |  |  |
| 10 | `IdProductoBodega` | `int` |  |  |
| 11 | `IdUnidadMedida` | `int` |  |  |
| 12 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 13 | `numero_orden` | `nvarchar(50)` |  |  |
| 14 | `Regimen` | `int` |  |  |
| 15 | `codigo_regimen` | `nvarchar(20)` |  |  |
| 16 | `dias_regimen` | `int` |  |  |
| 17 | `fecha_vencimiento` | `datetime` | ✓ |  |
| 18 | `Poliza` | `nvarchar(150)` |  |  |
| 19 | `IdPresentacion` | `int` |  |  |
| 20 | `Presentacion` | `nvarchar(50)` |  |  |
| 21 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 22 | `cantidad_presentacion` | `float` |  |  |
| 23 | `cantidad_umbas` | `float` |  |  |
| 24 | `cantidad_reservada` | `float` |  |  |
| 25 | `Disponible` | `float` |  |  |
| 26 | `peso` | `float` | ✓ |  |
| 27 | `unidad_peso` | `nvarchar(50)` | ✓ |  |
| 28 | `dias_vida` | `int` | ✓ |  |
| 29 | `clasificacion` | `nvarchar(50)` |  |  |
| 30 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 31 | `IdBodega` | `int` | ✓ |  |

## Consume

- `producto`
- `producto_clasificacion`
- `producto_presentacion`
- `propietarios`
- `regimen_fiscal`
- `stock`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_re_oc`
- `unidad_medida`
- `VW_Stock_Res`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Fiscal_Merca_Vencida]
AS
SELECT OC.IdOrdenCompraEnc, CAST(OC.Fecha_Creacion AS DATE) AS Fecha_Creacion, CAST(OC.Fecha_Recepcion AS DATE) AS Fecha_Recepcion, res.IdStock, res.IdProducto, res.codigo, res.nombre AS material, res.IdPropietario, 
                  prop.nombre_comercial AS nombre_cliente, res.IdProductoBodega, res.IdUnidadMedida, UM.Nombre, ISNULL(POL.numero_orden, 'NA') AS numero_orden, ISNULL(POL.IdRegimen, 0) AS Regimen, ISNULL(RF.codigo_regimen, 'NA') 
                  AS codigo_regimen, ISNULL(RF.dias_vencimiento, 0) AS dias_regimen, CASE WHEN RF.dias_vencimiento > 0 THEN DATEADD(DAY, RF.dias_vencimiento, CAST(OC.Fecha_Creacion AS DATE)) 
                  ELSE Fecha_Creacion END AS fecha_vencimiento, ISNULL(POL.codigo_poliza, 'NA') AS Poliza, ISNULL(res.IdPresentacion, '0') AS IdPresentacion, ISNULL(res.Presentacion, 'ND') AS Presentacion, res.codigo_barra, ISNULL(res.Cantidad, 
                  0) AS cantidad_presentacion, ISNULL(res.CantidadSF, 0) AS cantidad_umbas, ISNULL(res.CantidadReservada, 0) AS cantidad_reservada, ISNULL(res.CantidadSF, 0) - ISNULL(res.CantidadReservada, 0) AS Disponible, PP.peso, 
                  PP.nombre AS unidad_peso, DATEDIFF(DAY, GETDATE(), DATEADD(DAY, RF.dias_vencimiento, CAST(OC.Fecha_Creacion AS DATE))) AS dias_vida, ISNULL(pr_clas.nombre, 'ND') AS clasificacion, res.Bodega, res.IdBodega
FROM     dbo.VW_Stock_Res AS res INNER JOIN
                  dbo.propietarios AS prop ON res.IdPropietario = prop.IdPropietario INNER JOIN
                  dbo.stock AS ST ON res.IdStock = ST.IdStock INNER JOIN
                  dbo.trans_re_oc AS re ON ST.IdRecepcionEnc = re.IdRecepcionEnc INNER JOIN
                  dbo.trans_oc_enc AS OC ON re.IdOrdenCompraEnc = OC.IdOrdenCompraEnc INNER JOIN
                  dbo.unidad_medida AS UM ON res.IdUnidadMedida = UM.IdUnidadMedida LEFT OUTER JOIN
                  dbo.trans_oc_pol AS POL ON OC.IdOrdenCompraEnc = POL.IdOrdenCompraEnc LEFT OUTER JOIN
                  dbo.regimen_fiscal AS RF ON POL.IdRegimen = RF.IdRegimen INNER JOIN
                  dbo.producto AS PR ON res.codigo = PR.codigo LEFT OUTER JOIN
                  dbo.producto_presentacion AS PP ON PR.IdProducto = PP.IdProducto LEFT OUTER JOIN
                  dbo.producto_clasificacion AS pr_clas ON PR.IdClasificacion = pr_clas.IdClasificacion
GROUP BY res.IdProducto, res.codigo, res.IdPropietario, res.IdPresentacion, res.Presentacion, res.codigo_barra, res.Cantidad, res.CantidadSF, res.CantidadReservada, res.IdProductoBodega, res.IdUnidadMedida, prop.IdPropietario, 
                  prop.nombre_comercial, res.nombre, res.IdStock, POL.numero_orden, POL.IdRegimen, POL.codigo_poliza, OC.IdOrdenCompraEnc, OC.Fecha_Creacion, OC.Fecha_Recepcion, RF.codigo_regimen, UM.Nombre, RF.dias_vencimiento, 
                  PP.peso, PP.nombre, pr_clas.nombre, res.Bodega, res.IdBodega
```
