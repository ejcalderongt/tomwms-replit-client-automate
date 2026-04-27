---
id: db-brain-function-scalar-nombre-completo-ubicacion
type: db-function-scalar
title: dbo.Nombre_Completo_Ubicacion
schema: dbo
name: Nombre_Completo_Ubicacion
kind: function-scalar
modify_date: 2023-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Nombre_Completo_Ubicacion`

| Atributo | Valor |
|---|---|
| Tipo | SQL_SCALAR_FUNCTION |
| Schema modify_date | 2023-09-12 |
| Parámetros | 2 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@IdUbicacion` | `int` |  |
| 2 | `@IdBodega` | `int` |  |

## Consume

- `bodega_tramo`
- `bodega_ubicacion`

## Quién la referencia

**48** objetos:

- `VW_BodegaMuelle` (view)
- `VW_BodegaUbicacion` (view)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_Conteo_By_Operador` (view)
- `VW_ControlCalidad_CambioEstado` (view)
- `VW_Detalle_Licencias_Inconsistentes` (view)
- `VW_EstacionalidadProducto` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_historico` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Inv_Ciclico` (view)
- `VW_Licencias_Por_Ubicacion` (view)
- `VW_Movimientos` (view)
- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_N` (view)
- `VW_MovimientosDetalle` (view)
- `VW_OcupacionBodega` (view)
- `VW_PickingUbic_By_IdPedidoDet` (view)
- `VW_PickingUbic_By_IdPickingDet` (view)
- `VW_PickingUbic_By_IdPickingEnc` (view)
- `VW_PickingUbic_Desp_By_IdPedidoDet` (view)
- `VW_PickingUbic_Despachado_By_IdPedidoDet` (view)
- `VW_Producto_Estado_Ubic_Bodega` (view)
- `VW_Producto_Estado_Ubic_Bodega_HH` (view)
- `VW_ProductoEstadoUbic` (view)
- `VW_ProductoRellenado` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_ProximosVencimiento` (view)
- `VW_Revision_Producto` (view)
- `VW_Stock_CambioUbic` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_Stock_Res` (view)
- `VW_Stock_Res_Consolidador` (view)
- `VW_Stock_Reservado_By_IdPedidoEnc` (view)
- `VW_Stock_Resumen` (view)
- `VW_Stock_Resumen_20220407` (view)
- `VW_Tareas_Picking_HH` (view)
- `VW_TransUbicHhDet` (view)
- `VW_Ubicaciones_Inventario_Ciclico` (view)
- `VW_Ubicaciones_Picking` (view)
- `VW_Ubicaciones_Picking_Resumido` (view)
- `vw_ubicaciones_por_regla` (view)
- `VW_Valorizacion_OC` (view)

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE FUNCTION [dbo].[Nombre_Completo_Ubicacion] 
(
	@IdUbicacion INT,
	@IdBodega INT
)
RETURNS NVARCHAR(200)
AS
BEGIN
	
	DECLARE @resultado NVARCHAR(200)
	-- Declare the return variable here
	
	SELECT @resultado = CASE WHEN T.es_rack = 1 THEN
			iif(CHARINDEX('-',t.descripcion,0)=0, t.descripcion, SUBSTRING(REPLACE(t.descripcion,'-',''),1, LEN(t.descripcion)-2)) + ' - ' +
			'C' + CONVERT(NVARCHAR(10),u.indice_x) + ' - ' +
			iif(CHARINDEX('-',t.descripcion,0)=0,'','T' + SUBSTRING(t.descripcion,iif(CHARINDEX('-',t.descripcion,0)<0,0,CHARINDEX('-',t.descripcion,0)+1),1)+ ' - ') +
			'N' + CONVERT(NVARCHAR(10),u.nivel)+ ' - ' +
			ISNULL(u.orientacion_pos,'0')+ ' - ' +
			'#' + CONVERT(NVARCHAR(10),u.IdUbicacion)
			ELSE U.descripcion + ' - #' + CONVERT(NVARCHAR(10),u.IdUbicacion) END
	FROM bodega_ubicacion u 
	inner join bodega_tramo t on u.idtramo = t.idtramo 
	and t.IdBodega = u.IdBodega
	WHERE (u.IdUbicacion=@IdUbicacion) 
	And u.idbodega = @IdBodega

	RETURN @resultado;

END
```
