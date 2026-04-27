---
id: db-brain-view-vw-ajustes
type: db-view
title: dbo.VW_Ajustes
schema: dbo
name: VW_Ajustes
kind: view
modify_date: 2025-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Ajustes`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-02-11 |
| Columnas | 31 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idajusteenc` | `int` |  |  |
| 2 | `idajustedet` | `int` |  |  |
| 3 | `fecha` | `date` | ✓ |  |
| 4 | `referencia` | `nvarchar(50)` | ✓ |  |
| 5 | `codigo_producto` | `nvarchar(50)` | ✓ |  |
| 6 | `nombre_producto` | `nvarchar(200)` | ✓ |  |
| 7 | `IdPresentacion` | `int` | ✓ |  |
| 8 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 9 | `IdBodegaERP` | `int` | ✓ |  |
| 10 | `Codigo_Bodega` | `nvarchar(150)` | ✓ |  |
| 11 | `Nombre_Bodega` | `nvarchar(150)` | ✓ |  |
| 12 | `cantidad_original` | `float` | ✓ |  |
| 13 | `cantidad_nueva` | `float` | ✓ |  |
| 14 | `peso_nuevo` | `float` | ✓ |  |
| 15 | `peso_original` | `float` | ✓ |  |
| 16 | `fecha_vence_nueva` | `datetime` | ✓ |  |
| 17 | `fecha_vence_original` | `datetime` | ✓ |  |
| 18 | `lote_original` | `nvarchar(50)` | ✓ |  |
| 19 | `lote_nuevo` | `nvarchar(50)` | ✓ |  |
| 20 | `Tipo_Ajuste` | `nvarchar(50)` | ✓ |  |
| 21 | `modifica_cantidad` | `bit` | ✓ |  |
| 22 | `enviado` | `bit` | ✓ |  |
| 23 | `Motivo_Ajuste` | `nvarchar(50)` | ✓ |  |
| 24 | `observacion` | `nvarchar(300)` | ✓ |  |
| 25 | `codigo_ajuste` | `nvarchar(50)` | ✓ |  |
| 26 | `IdProductoFamilia` | `int` | ✓ |  |
| 27 | `Nombre_Presentacion` | `nvarchar(50)` | ✓ |  |
| 28 | `Factor` | `float` |  |  |
| 29 | `Codigo_Centro_Costo` | `nvarchar(50)` | ✓ |  |
| 30 | `Nombre_Centro_Costo` | `nvarchar(150)` | ✓ |  |
| 31 | `ajuste_por_inventario` | `int` | ✓ |  |

## Consume

- `ajuste_motivo`
- `ajuste_tipo`
- `centro_costo`
- `cliente`
- `producto`
- `producto_presentacion`
- `trans_ajuste_det`
- `trans_ajuste_enc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Ajustes]
AS
SELECT       dbo.trans_ajuste_enc.idajusteenc, dbo.trans_ajuste_det.idajustedet, dbo.trans_ajuste_enc.fecha, dbo.trans_ajuste_enc.referencia, dbo.trans_ajuste_det.codigo_producto, dbo.trans_ajuste_det.nombre_producto, 
                         dbo.trans_ajuste_det.IdPresentacion, dbo.unidad_medida.Nombre AS UMBas, dbo.trans_ajuste_enc.idbodega AS IdBodegaERP, dbo.cliente.codigo AS Codigo_Bodega, dbo.cliente.nombre_comercial AS Nombre_Bodega, 
                         dbo.trans_ajuste_det.cantidad_original, dbo.trans_ajuste_det.cantidad_nueva, dbo.trans_ajuste_det.peso_nuevo, dbo.trans_ajuste_det.peso_original, dbo.trans_ajuste_det.fecha_vence_nueva, dbo.trans_ajuste_det.fecha_vence_original, 
                         dbo.trans_ajuste_det.lote_original, dbo.trans_ajuste_det.lote_nuevo, dbo.ajuste_tipo.nombre AS Tipo_Ajuste, dbo.ajuste_tipo.modifica_cantidad, dbo.trans_ajuste_det.enviado, dbo.ajuste_motivo.nombre AS Motivo_Ajuste, 
                         dbo.trans_ajuste_det.observacion, dbo.trans_ajuste_det.codigo_ajuste, dbo.trans_ajuste_enc.IdProductoFamilia, dbo.producto_presentacion.nombre AS Nombre_Presentacion, ISNULL(dbo.producto_presentacion.factor, 0) AS Factor, 
                         dbo.centro_costo.Codigo AS Codigo_Centro_Costo, dbo.centro_costo.Nombre AS Nombre_Centro_Costo, dbo.trans_ajuste_enc.ajuste_por_inventario
FROM            dbo.trans_ajuste_enc INNER JOIN
                         dbo.trans_ajuste_det ON dbo.trans_ajuste_enc.idajusteenc = dbo.trans_ajuste_det.idajusteenc INNER JOIN
                         dbo.unidad_medida ON dbo.trans_ajuste_det.IdUnidadMedida = dbo.unidad_medida.IdUnidadMedida INNER JOIN
                         dbo.producto ON dbo.unidad_medida.IdUnidadMedida = dbo.producto.IdUnidadMedidaBasica INNER JOIN
                         dbo.ajuste_tipo ON dbo.trans_ajuste_det.idtipoajuste = dbo.ajuste_tipo.idtipoajuste INNER JOIN
                         dbo.ajuste_motivo ON dbo.trans_ajuste_det.idmotivoajuste = dbo.ajuste_motivo.idmotivoajuste INNER JOIN
                         dbo.centro_costo ON dbo.trans_ajuste_enc.IdCentroCosto = dbo.centro_costo.IdCentroCosto LEFT OUTER JOIN
                         dbo.cliente ON dbo.trans_ajuste_det.IdBodegaERP = dbo.cliente.IdCliente LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.trans_ajuste_det.IdPresentacion = dbo.producto_presentacion.IdPresentacion
GROUP BY dbo.trans_ajuste_enc.idajusteenc, dbo.trans_ajuste_enc.fecha, dbo.trans_ajuste_enc.referencia, dbo.trans_ajuste_det.codigo_producto, dbo.trans_ajuste_det.nombre_producto, dbo.trans_ajuste_enc.idbodega, dbo.cliente.codigo, 
                         dbo.cliente.nombre_comercial, dbo.unidad_medida.Nombre, dbo.trans_ajuste_det.IdPresentacion, dbo.trans_ajuste_det.cantidad_original, dbo.trans_ajuste_det.cantidad_nueva, dbo.trans_ajuste_det.peso_nuevo, 
                         dbo.trans_ajuste_det.peso_original, dbo.trans_ajuste_det.fecha_vence_nueva, dbo.trans_ajuste_det.fecha_vence_original, dbo.ajuste_tipo.nombre, dbo.ajuste_tipo.modifica_cantidad, dbo.ajuste_motivo.nombre, 
                         dbo.trans_ajuste_det.enviado, dbo.trans_ajuste_det.lote_original, dbo.trans_ajuste_det.lote_nuevo, dbo.trans_ajuste_det.idajustedet, dbo.trans_ajuste_det.observacion, dbo.trans_ajuste_det.codigo_ajuste, 
                         dbo.trans_ajuste_enc.IdProductoFamilia, dbo.producto_presentacion.nombre, dbo.producto_presentacion.factor, dbo.centro_costo.Codigo, dbo.centro_costo.Nombre, dbo.trans_ajuste_enc.ajuste_por_inventario
```
