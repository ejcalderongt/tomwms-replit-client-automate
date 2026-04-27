---
id: db-brain-view-vw-fiscal-historico
type: db-view
title: dbo.VW_Fiscal_historico
schema: dbo
name: VW_Fiscal_historico
kind: view
modify_date: 2023-05-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Fiscal_historico`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-05-18 |
| Columnas | 29 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdOrdenCompraEnc` | `int` | ✓ |  |
| 2 | `IdRecepcionEnc` | `int` | ✓ |  |
| 3 | `IdBodega` | `int` | ✓ |  |
| 4 | `regimen` | `varchar(7)` | ✓ |  |
| 5 | `codigo_poliza` | `nvarchar(50)` | ✓ |  |
| 6 | `es_bodega_fiscal` | `bit` | ✓ |  |
| 7 | `IdPropietario` | `int` |  |  |
| 8 | `Fecha` | `date` | ✓ |  |
| 9 | `IdPropietarioBodega` | `int` | ✓ |  |
| 10 | `cliente` | `nvarchar(100)` | ✓ |  |
| 11 | `nombre_bodega` | `nvarchar(50)` | ✓ |  |
| 12 | `fecha_inventario` | `datetime` | ✓ |  |
| 13 | `Fecha_Recepcion` | `datetime` | ✓ |  |
| 14 | `numero_orden` | `nvarchar(50)` | ✓ |  |
| 15 | `producto` | `nvarchar(100)` | ✓ |  |
| 16 | `licencia` | `nvarchar(50)` | ✓ |  |
| 17 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 18 | `ubicacion` | `nvarchar(200)` | ✓ |  |
| 19 | `IdUbicacion` | `int` | ✓ |  |
| 20 | `costo` | `float` | ✓ |  |
| 21 | `cantidad` | `float` | ✓ |  |
| 22 | `cif` | `float` | ✓ |  |
| 23 | `dai` | `float` | ✓ |  |
| 24 | `iva` | `float` | ✓ |  |
| 25 | `TOTAL_VALOR` | `float` | ✓ |  |
| 26 | `area` | `nvarchar(200)` | ✓ |  |
| 27 | `Clasificacion` | `nvarchar(100)` | ✓ |  |
| 28 | `Referencia` | `nvarchar(100)` | ✓ |  |
| 29 | `shipper` | `nvarchar(150)` | ✓ |  |

## Consume

- `bodega`
- `bodega_ubicacion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `propietario_bodega`
- `propietarios`
- `stock_jornada`
- `trans_oc_det`
- `trans_oc_embarcador`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_re_det`
- `trans_re_oc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE view [dbo].[VW_Fiscal_historico] as
SELECT sj.IdOrdenCompraEnc, sj.IdRecepcionEnc, sj.IdBodega, CASE WHEN bd.es_bodega_fiscal = 1 THEN 'Fiscal' WHEN bd.es_bodega_fiscal = 0 THEN 'General' END AS regimen, sj.no_poliza AS codigo_poliza, bd.es_bodega_fiscal, 
                  pr.IdPropietario, sj.Fecha, sj.IdPropietarioBodega, sj.Propietario AS cliente, sj.Bodega AS nombre_bodega, sj.fecha_ingreso AS fecha_inventario, sj.Fecha_Recepcion, 
				  --sj.numero_orden,
				  oc_pol.numero_orden,
				  sj.nombre_producto AS producto, 
                  sj.lic_plate AS licencia, sj.codigo_barra_producto AS codigo_barra, dbo.Nombre_Completo_Ubicacion(sj.IdUbicacion, sj.IdBodega) AS ubicacion, sj.IdUbicacion,
				  --oc_det.cantidad_recibida as cantidad_recibida,
				  re_det.costo,sj.cantidad, 
				  --sj.valor_aduana AS cif, 
				  (sj.valor_aduana/oc_det.cantidad_recibida )*sj.cantidad as cif, 
				  --sj.valor_dai AS dai, 
				  (sj.valor_dai/oc_det.cantidad_recibida)*sj.cantidad as dai, 
				  --sj.valor_iva AS iva, 
				  (sj.valor_iva/oc_det.cantidad_recibida)*sj.cantidad as iva,
                  --ISNULL(sj.valor_aduana, 0) + ISNULL(sj.valor_dai, 0) + ISNULL(sj.valor_iva, 0) AS TOTAL_VALOR,

				   (sj.valor_aduana/oc_det.cantidad_recibida )*sj.cantidad +  (sj.valor_dai/oc_det.cantidad_recibida)*sj.cantidad+  (sj.valor_iva/oc_det.cantidad_recibida)*sj.cantidad as TOTAL_VALOR,

				  dbo.Nombre_Area(bu.IdArea, sj.IdBodega) AS area, sj.Clasificacion, oc_enc.Referencia,embarcador.Nombre shipper
FROM     dbo.stock_jornada AS sj INNER JOIN
                  dbo.bodega AS bd ON sj.IdBodega = bd.IdBodega INNER JOIN
                  dbo.propietario_bodega AS pb ON sj.IdPropietarioBodega = pb.IdPropietarioBodega INNER JOIN
                  dbo.propietarios AS pr ON pb.IdPropietario = pr.IdPropietario INNER JOIN
                  dbo.bodega_ubicacion AS bu ON bu.IdBodega = sj.IdBodega AND sj.IdUbicacion = bu.IdUbicacion INNER JOIN
                  dbo.trans_re_det AS re_det ON sj.IdRecepcionEnc = re_det.IdRecepcionEnc AND sj.IdRecepcionDet = re_det.IdRecepcionDet INNER JOIN
                  dbo.trans_re_oc AS re_oc ON re_det.IdRecepcionEnc = re_oc.IdRecepcionEnc INNER JOIN
                  dbo.trans_oc_enc AS oc_enc ON re_oc.IdOrdenCompraEnc = oc_enc.IdOrdenCompraEnc
				  INNER JOIN trans_oc_det oc_det on re_det.IdOrdenCompraEnc = oc_det.IdOrdenCompraEnc and re_det.IdOrdenCompraDet = oc_det.IdOrdenCompraDet
				  LEFT OUTER JOIN  dbo.trans_oc_pol oc_pol on oc_enc.IdOrdenCompraEnc=oc_pol.IdOrdenCompraEnc
				  LEFT OUTER JOIN dbo.trans_oc_embarcador embarcador on oc_det.IdEmbarcador= embarcador.IdEmbarcador
```
