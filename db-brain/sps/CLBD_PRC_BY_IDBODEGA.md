---
id: db-brain-sp-clbd-prc-by-idbodega
type: db-sp
title: dbo.CLBD_PRC_BY_IDBODEGA
schema: dbo
name: CLBD_PRC_BY_IDBODEGA
kind: sp
modify_date: 2020-10-07
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.CLBD_PRC_BY_IDBODEGA`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2020-10-07 |
| Parámetros | 1 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdBodega` | `int` |  |

## Consume

- `BODEGA`
- `I_nav_transacciones_out`
- `operador_bodega`
- `producto_bodega`
- `producto_pallet`
- `propietario_bodega`
- `stock`
- `stock_hist`
- `stock_parametro`
- `stock_rec`
- `stock_res`
- `stock_se`
- `stock_se_rec`
- `tarea_hh`
- `trans_despacho_det`
- `trans_despacho_enc`
- `trans_inv_ciclico`
- `trans_inv_detalle`
- `trans_inv_enc`
- `trans_inv_operador`
- `trans_inv_reconteo`
- `trans_inv_resumen`
- `trans_inv_stock`
- `trans_inv_stock_prod`
- `trans_inv_tramo`
- `trans_inventario_det`
- `trans_inventario_enc`
- `trans_movimientos`
- `trans_oc_det`
- `trans_oc_det_lote`
- `trans_oc_enc`
- `trans_oc_imagen`
- `trans_oc_pol`
- `trans_pe_det`
- `trans_pe_enc`
- `trans_picking_det`
- `trans_picking_det_parametros`
- `trans_picking_enc`
- `trans_picking_op`
- `trans_picking_ubic`
- `trans_re_det`
- `trans_re_det_infraccion`
- `trans_re_det_parametros`
- `trans_re_enc`
- `trans_re_fact`
- `trans_re_img`
- `trans_re_oc`
- `trans_re_op`
- `trans_ubic_hh_det`
- `trans_ubic_hh_enc`
- `trans_ubic_hh_stock`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE [dbo].[CLBD_PRC_BY_IDBODEGA]
@IdBodega int
AS
BEGIN

DECLARE @CODIGO_BODEGA NVARCHAR(25)
DECLARE @NOMBRE_BODEGA NVARCHAR(25)
DECLARE @REGISTROS INT

	SELECT @CODIGO_BODEGA = (SELECT CODIGO FROM BODEGA WHERE IdBodega = @IdBodega)
	SELECT @NOMBRE_BODEGA = (SELECT NOMBRE FROM BODEGA WHERE IdBodega = @IdBodega)

	if (@CODIGO_BODEGA IS NULL)
		BEGIN
			PRINT 'El identificador de bodega no coincide con ninguna bodega existente!.'
		END
	ELSE 
		BEGIN
			
			PRINT 'Se eliminarán registros para la bodega: ' + ISNULL(@CODIGO_BODEGA,0) + ' ' + ISNULL(@NOMBRE_BODEGA,'ND')

			print GETDATE()

			--BEGIN TRANSACTION																						

				PRINT 'stock: ?' 
				DELETE a
				FROM stock a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				AND a.IdBodega = b.IdBodega
				WHERE b.idbodega = @IdBodega
				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'stock: ' 

				DELETE a
				FROM stock_hist a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				AND a.IdDespachoEnc = b.IdBodega
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'stock_hist: ' 

				DELETE 
				FROM stock_parametro WHERE IdStock in (
				select idstock
				FROM stock a
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				AND a.IdBodega = b.IdBodega
				WHERE b.idbodega = @IdBodega)

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'stock_parametro: ' 

				DELETE a
				FROM stock_res a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				AND a.IdBodega = b.IdBodega
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'stock_res: ' 

				DELETE a
				FROM stock_se_rec a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.idbodega = @IdBodega	

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'stock_se_rec: ' 

				DELETE a
				FROM stock_se a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.idbodega = @IdBodega	

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'stock_se: ' 

				DELETE a
				FROM stock_rec a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				AND a.IdBodega = b.IdBodega
				WHERE b.idbodega = @IdBodega
	
				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'stock_rec: ' 

				DELETE FROM trans_re_img WHERE IdRecepcionEnc IN (
				SELECT idrecepcionenc FROM trans_re_enc a INNER JOIN propietario_bodega b
				ON a.idpropietariobodega = b.idpropietariobodega 
				WHERE b.idbodega = @IdBodega)

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_re_img: ' 
				DELETE a
				FROM producto_pallet a
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.IdBodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'producto_pallet: ' 	

				DELETE a
				FROM trans_picking_ubic a
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.IdBodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_picking_ubic: ' 
		
				DELETE a
				FROM trans_picking_op a
				INNER JOIN operador_bodega b
				ON a.IdOperadorBodega = b.IdOperadorBodega
				WHERE b.IdBodega = @IdBodega	

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_picking_op: ' 

				DELETE FROM trans_picking_det_parametros WHERE IdPickingDet IN (
				SELECT IdPickingDet FROM trans_picking_det a INNER JOIN trans_picking_enc b
				ON a.IdPickingEnc = b.IdPickingEnc INNER JOIN propietario_bodega c
				ON b.IdPropietarioBodega = c.IdPropietarioBodega
				WHERE c.idbodega = @IdBodega)	 

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_picking_det_parametros: ' 

				DELETE a
				FROM trans_picking_det a
				INNER JOIN trans_picking_enc b
				ON a.IdPickingEnc = b.IdPickingEnc 
				INNER JOIN propietario_bodega c
				ON b.IdPropietarioBodega = c.IdPropietarioBodega
				WHERE c.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_picking_det: ' 

				DELETE a
				FROM trans_picking_enc a	
				INNER JOIN propietario_bodega b
				ON a.IdPropietarioBodega = b.IdPropietarioBodega
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_picking_enc: ' 	

				PRINT 'stock_ref_to_det'

				delete from stock where IdRecepcionDet in 
				(select IdRecepcionDet 
				FROM trans_re_det a
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.IdBodega = @IdBodega)
				AND IdBodega = @IdBodega
				PRINT 'Fin _RF'

				DELETE a
				FROM trans_re_det a
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.IdBodega = @IdBodega
				PRINT 'trans_re_det: '

				DELETE FROM trans_re_det_infraccion WHERE IdRecepcionEnc IN (
				SELECT idrecepcionenc FROM trans_re_enc a INNER JOIN propietario_bodega b
				ON a.idpropietariobodega = b.idpropietariobodega 
				WHERE b.idbodega = @IdBodega)

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_re_det_infraccion: ' 

				DELETE from trans_re_det_parametros where IdRecepcionEnc in (
				select idrecepcionenc from trans_re_enc a inner join propietario_bodega b
				on a.idpropietariobodega = b.idpropietariobodega 
				WHERE b.idbodega = @IdBodega)

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_re_det_parametros: ' 
				
				DELETE a
				FROM trans_pe_det a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_pe_det: ' 

				DELETE a
				FROM trans_re_op a
				INNER JOIN trans_re_enc b
				ON a.IdRecepcionEnc = b.IdRecepcionEnc
				INNER JOIN propietario_bodega c 
				ON b.IdPropietarioBodega = c.IdPropietario
				WHERE c.IdBodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_re_op: '

				DELETE FROM trans_re_fact WHERE IdRecepcionEnc IN (
				SELECT idrecepcionenc FROM trans_re_enc a INNER JOIN propietario_bodega b
				ON a.idpropietariobodega = b.idpropietariobodega 
				WHERE b.idbodega = @IdBodega)

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'transtrans_re_fact_re_img: '							 

				DELETE a
				FROM trans_re_enc a
				INNER JOIN propietario_bodega b
				ON a.IdPropietarioBodega = b.IdPropietarioBodega
				WHERE b.IdBodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_re_enc: ' 

				DELETE FROM trans_re_oc WHERE IdRecepcionEnc IN (
				SELECT idrecepcionenc FROM trans_re_enc a INNER JOIN propietario_bodega b
				ON a.idpropietariobodega = b.idpropietariobodega 
				WHERE b.idbodega = @IdBodega)

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_re_oc: ' 			

				DELETE a
				FROM trans_pe_enc a	
				WHERE IdBodega = @IdBodega	

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_pe_enc: ' 

				DELETE a
				FROM trans_ubic_hh_stock a
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.IdBodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_ubic_hh_stock: '

				DELETE a
				FROM trans_oc_pol a	
				INNER JOIN trans_oc_enc b
				ON a.IdOrdenCompraEnc = b.IdOrdenCompraEnc
				INNER JOIN propietario_bodega c
				ON b.IdPropietarioBodega = c.IdPropietarioBodega
				WHERE c.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_oc_pol: ' 

				Delete from trans_ubic_hh_det where idtareaubicacionenc in (
				SELECT IdTareaUbicacionEnc
				FROM trans_ubic_hh_enc a
				INNER JOIN propietario_bodega b
				ON a.IdPropietarioBodega = b.IdPropietarioBodega
				WHERE b.IdBodega = @IdBodega)

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_ubic_hh_det: '

				DELETE a
				FROM trans_ubic_hh_enc a
				INNER JOIN propietario_bodega b
				ON a.IdPropietarioBodega = b.IdPropietarioBodega
				WHERE b.IdBodega = @IdBodega

				PRINT 'trans_ubic_hh_enc: ' 
				SELECT @REGISTROS = (select @@ROWCOUNT )

				DELETE a
				FROM trans_oc_imagen a	
				INNER JOIN trans_oc_enc b
				ON a.IdOrdenCompraEnc = b.IdOrdenCompraEnc
				INNER JOIN propietario_bodega c
				ON b.IdPropietarioBodega = c.IdPropietarioBodega
				WHERE c.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_oc_imagen: ' 

				DELETE a
				FROM trans_oc_det_lote a	
				INNER JOIN trans_oc_enc b
				ON a.IdOrdenCompraEnc = b.IdOrdenCompraEnc
				INNER JOIN propietario_bodega c
				ON b.IdPropietarioBodega = c.IdPropietarioBodega
				WHERE c.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_oc_det_lote: ' 

				DELETE a
				FROM trans_oc_det a	
				INNER JOIN trans_oc_enc b
				ON a.IdOrdenCompraEnc = b.IdOrdenCompraEnc
				INNER JOIN propietario_bodega c
				ON b.IdPropietarioBodega = c.IdPropietarioBodega
				WHERE c.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_oc_det: ' 

				DELETE b
				FROM trans_oc_enc b 
				INNER JOIN propietario_bodega c
				ON b.IdPropietarioBodega = c.IdPropietarioBodega
				WHERE c.idbodega = @IdBodega	

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_oc_enc: ' 

				DELETE FROM trans_movimientos 
				WHERE IdBodegaOrigen = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_movimientos: ' 

				DELETE a
				FROM trans_inventario_det a	
				INNER JOIN trans_inventario_enc b
				ON a.IdInventarioEnc = b.IdInventarioEnc
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inventario_det: ' 

				DELETE FROM trans_inventario_enc 
				WHERE idbodega = @IdBodega
	
				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inventario_enc: ' 

				DELETE a
				FROM trans_despacho_det a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.idbodega = @IdBodega		

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_despacho_det: ' 

				DELETE FROM trans_despacho_enc 
				WHERE idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_despacho_enc: ' 

				DELETE FROM tarea_hh
				WHERE idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'tarea_hh: ' 				

				DELETE a
				FROM trans_inv_detalle a	
				INNER JOIN trans_inv_enc b
				ON a.idinventarioenc = b.idinventarioenc
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inv_detalle: ' 

				DELETE a
				FROM trans_inv_resumen a	
				INNER JOIN trans_inv_enc b
				ON a.idinventarioenct = b.idinventarioenc
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inv_resumen: ' 

				DELETE a
				FROM trans_inv_reconteo a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.idbodega = @IdBodega	
	
				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inv_reconteo: ' 

				DELETE a
				FROM trans_inv_ciclico a	
				INNER JOIN producto_bodega b
				ON a.IdProductoBodega = b.IdProductoBodega
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inv_ciclico: ' 	

				DELETE a
				FROM trans_inv_tramo a	
				INNER JOIN trans_inv_enc b
				ON a.idinventario = b.idinventarioenc
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inv_tramo: ' 	

				DELETE a
				FROM trans_inv_operador a	
				INNER JOIN trans_inv_enc b
				ON a.idinventarioenc = b.idinventarioenc
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inv_operador: ' + CONVERT(NVARCHAR(25),@REGISTROS) + ' Registros eliminados.'

				DELETE a
				FROM trans_inv_stock a	
				INNER JOIN trans_inv_enc b
				ON a.idinventario = b.idinventarioenc
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inv_stock: ' + CONVERT(NVARCHAR(25),@REGISTROS) + ' Registros eliminados.'
					
				DELETE a
				FROM trans_inv_stock_prod a	
				INNER JOIN trans_inv_enc b
				ON a.idinventario = b.idinventarioenc
				WHERE b.idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inv_stock_prod: ' + CONVERT(NVARCHAR(25),@REGISTROS) + ' Registros eliminados.'

				DELETE FROM trans_inv_enc 
				WHERE idbodega = @IdBodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'trans_inv_enc: ' + CONVERT(NVARCHAR(25),@REGISTROS) + ' Registros eliminados.'

				DELETE FROM I_nav_transacciones_out WHERE Idbodega = @idbodega

				SELECT @REGISTROS = (select @@ROWCOUNT )
				PRINT 'I_nav_transacciones_out: ' + CONVERT(NVARCHAR(25),@REGISTROS) + ' Registros eliminados.'
				print GETDATE()
				PRINT '#EJC20191219'

				--COMMIT TRANSACTION

		END

END
```
