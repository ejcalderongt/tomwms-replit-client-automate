---
id: db-brain-view-vw-tiempos-picking-operador
type: db-view
title: dbo.VW_Tiempos_Picking_Operador
schema: dbo
name: VW_Tiempos_Picking_Operador
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Tiempos_Picking_Operador`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 30 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdPedidoEnc` | `int` |  |  |
| 2 | `IdPickingEnc` | `int` |  |  |
| 3 | `Codigo_Bodega` | `nvarchar(50)` | ✓ |  |
| 4 | `Propietario` | `nvarchar(100)` |  |  |
| 5 | `TipoDocumento` | `nvarchar(50)` | ✓ |  |
| 6 | `Pe_Hora_Creacion` | `datetime` | ✓ |  |
| 7 | `Creacion_Picking` | `datetime` | ✓ |  |
| 8 | `Fecha_Pedido` | `datetime` | ✓ |  |
| 9 | `fecha_picking` | `datetime` | ✓ |  |
| 10 | `fecha_verificado` | `datetime` | ✓ |  |
| 11 | `fecha_despachado` | `datetime` | ✓ |  |
| 12 | `Operador_Pickeo` | `nvarchar(254)` | ✓ |  |
| 13 | `Operador_Verifico` | `nvarchar(254)` |  |  |
| 14 | `Ini_Picking_Ubi` | `datetime` | ✓ |  |
| 15 | `Fin_Picking_Ubi` | `datetime` | ✓ |  |
| 16 | `Dif_Picking_Ubi` | `nvarchar(50)` | ✓ |  |
| 17 | `Dif_Picking_Ubi_H` | `int` | ✓ |  |
| 18 | `Ini_Picking_Enc` | `datetime` | ✓ |  |
| 19 | `Fin_Picking_Enc` | `datetime` | ✓ |  |
| 20 | `Dif_Picking_Enc` | `nvarchar(50)` | ✓ |  |
| 21 | `Dif_Picking_Enc_H` | `int` | ✓ |  |
| 22 | `Cant_Lineas` | `int` | ✓ |  |
| 23 | `cant_pedida` | `float` | ✓ |  |
| 24 | `cant_despachada` | `float` | ✓ |  |
| 25 | `cantidad_solicitada` | `float` | ✓ |  |
| 26 | `cantidad_recibida` | `float` | ✓ |  |
| 27 | `cantidad_verificada` | `float` | ✓ |  |
| 28 | `cantidad_despachada` | `float` | ✓ |  |
| 29 | `nom_presentacion` | `nvarchar(50)` | ✓ |  |
| 30 | `nom_unid_med` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega`
- `ConvertSecondsFormatoFecha`
- `operador`
- `operador_bodega`
- `propietario_bodega`
- `propietarios`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_pe_tipo`
- `trans_picking_enc`
- `trans_picking_ubic`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Tiempos_Picking_Operador]
AS
SELECT  pe.IdPedidoEnc, 
		pu.IdPickingEnc, 
		dbo.bodega.codigo AS Codigo_Bodega, 
		dbo.propietarios.nombre_comercial AS Propietario, 
		dbo.trans_pe_tipo.Nombre AS TipoDocumento, 
		MAX(pe.Fec_Agr) AS Pe_Hora_Creacion,
		MAX(pu.Fec_Agr) as Creacion_Picking,
		MAX(pe.Fecha_Pedido) as Fecha_Pedido,
		MAX(pu.fecha_picking) AS fecha_picking,
		MAX(pu.fecha_verificado) AS fecha_verificado,
		MAX(pu.fecha_despachado) AS fecha_despachado,
		CONVERT(NVARCHAR(50),pu.IdOperadorBodega_Pickeo) + ' - ' + 
		op.nombres + ' ' + op.apellidos AS Operador_Pickeo,
		ISNULL(CONVERT(NVARCHAR(50),pu.IdOperadorBodega_Verifico) + ' - ' + 
		ov.nombres + ' ' + ov.apellidos,'Automático') AS Operador_Verifico,
		MIN(pu.fecha_picking) AS Ini_Picking_Ubi,
		MAX(pu.fecha_picking) AS Fin_Picking_Ubi, 
		dbo.ConvertSecondsFormatoFecha(DateDiff(SECOND,MIN(pu.fecha_picking),MAX(pu.fecha_picking))) AS Dif_Picking_Ubi,
		DateDiff(HOUR,MIN(pu.fecha_picking),MAX(pu.fecha_picking)) AS Dif_Picking_Ubi_H,
		MAX(ke.hora_ini) AS Ini_Picking_Enc,
		MAX(ke.hora_fin) AS Fin_Picking_Enc, 
		dbo.ConvertSecondsFormatoFecha(DateDiff(SECOND,MAX(ke.hora_ini),MAX(ke.hora_fin))) AS Dif_Picking_Enc,
		DateDiff(HOUR,MAX(ke.hora_ini),MAX(ke.hora_fin)) AS Dif_Picking_Enc_H,
		count(pu.IdPickingUbic) AS Cant_Lineas,
		sum(pd.Cantidad) AS cant_pedida, 
		sum(pd.cant_despachada) AS cant_despachada, 
		sum(pu.cantidad_solicitada) AS cantidad_solicitada, 
		sum(pu.cantidad_recibida) AS cantidad_recibida, 
		sum(pu.cantidad_verificada) AS cantidad_verificada, 
		sum(pu.cantidad_despachada) AS cantidad_despachada, 
		pd.nom_presentacion,
		pd.nom_unid_med
FROM  dbo.trans_pe_enc AS pe INNER JOIN
	  dbo.trans_pe_det AS pd ON pe.IdPedidoEnc = pd.IdPedidoEnc INNER JOIN
	  dbo.trans_picking_ubic AS pu ON pd.IdPedidoDet = pu.IdPedidoDet  INNER JOIN
      dbo.trans_picking_enc AS ke ON ke.IdPickingEnc = pu.IdPickingEnc  INNER JOIN
      dbo.trans_pe_tipo ON pe.IdTipoPedido = trans_pe_tipo.IdTipoPedido INNER JOIN
      dbo.propietario_bodega ON pe.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
      dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
	  dbo.bodega ON pe.IdBodega = dbo.bodega.IdBodega AND dbo.propietario_bodega.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
				  dbo.operador_bodega obp ON pu.IdOperadorBodega_Pickeo = obp.IdOperadorBodega LEFT OUTER  JOIN
				  dbo.operador op ON op.IdOperador = obp.IdOperador  LEFT OUTER  JOIN
				  dbo.operador_bodega obv ON pu.IdOperadorBodega_Verifico = obv.IdOperadorBodega LEFT OUTER  JOIN
				  dbo.operador ov ON ov.IdOperador = obv.IdOperador
WHERE (pe.Fecha_Pedido > '20190201') and pe.anulado = 0 and pu.activo = 1 and pu.IdPickingEnc<>0 and ke.procesado_bof = 0
     and pu.IdOperadorBodega_Pickeo <> 0
GROUP BY pe.IdPedidoEnc, 
		pu.IdPickingEnc, 
		dbo.bodega.codigo, 
		dbo.propietarios.nombre_comercial,
		pu.IdOperadorBodega_Pickeo,
		op.nombres, op.apellidos,
		pu.IdOperadorBodega_Verifico,
		ov.nombres, ov.apellidos, 
		pd.nom_presentacion,
		pd.nom_unid_med,
		dbo.trans_pe_tipo.Nombre
```
