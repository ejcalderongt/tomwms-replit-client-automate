---
id: db-brain-sp-sp-importa-stock-bodegas-general-y-da-ado-actualizacion-sin-importacion
type: db-sp
title: dbo.SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion
schema: dbo
name: SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion
kind: sp
modify_date: 2018-09-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2018-09-25 |

## Consume

- `bodega_ubicacion`
- `producto`
- `producto_bodega`
- `stock`
- `stock_clc_3`
- `stock_clc_dañado_3`
- `stock_clc_unificado`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
-- ==========================================================================
-- Author:		Carolina Fuentes
-- Create date: 13-09-2018
-- Description:	Actualiza Stock de las Bodegas General y Dañado
-- ===========================================================================
CREATE PROCEDURE [dbo].[SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion]
AS
BEGIN

IF OBJECT_ID('dbo.comparativo_ubicaciones_general', 'U') IS NOT NULL DROP TABLE comparativo_ubicaciones_general
IF OBJECT_ID('dbo.comparativo_ubicaciones_dañado', 'U') IS NOT NULL DROP TABLE comparativo_ubicaciones_dañado
IF OBJECT_ID('dbo.comparativo_faltantes_general', 'U') IS NOT NULL DROP TABLE comparativo_faltantes_general
IF OBJECT_ID('dbo.comparativo_faltantes_dañado', 'U') IS NOT NULL DROP TABLE comparativo_faltantes_dañado
IF OBJECT_ID('dbo.comparativo_inventarios', 'U') IS NOT NULL DROP TABLE comparativo_inventarios

delete from stock_clc_unificado
delete from stock

Print 'Stock de la Bodega General de CLC'
Insert into stock_clc_unificado
select 2 as IdPropietarioBodega, pb.IdProductobodega,no_telefono AS IdProductoEstado, 
Null as IdPresentacion,p.IdUnidadMedidaBasica, u.Idubicacion, u2.IdUbicacion as IdUbicacion_Anterior, 
no_recepcion As IdRecepcionEnc, Null As IdRecepcionDet,
Null As IdPedidEnc, Null IdPickingEnc, Null As IdDespachoEnc,
lote, lic_plate, '' as Serial, count(s.productoid) as cantidad, 
max(fecha_ingreso) as fecha_ingreso, fecha_vence, uds_lic_plate, no_bulto,'19000101' fecha_manufactura, 
Null as añada, '1' as user_agr, GetDate() as fec_agr, '1' as user_mod, GetDate() as fec_mod, 
1 as activo, 0 as peso, 0 as temperatura, '' atributo_variante_1
from stock_clc_3 s inner join producto p on s.productoid = p.codigo
inner join producto_bodega pb on pb.IdProducto = p.IdProducto and pb.idbodega = 3
inner join bodega_ubicacion u on u.codigo_barra2  = s.ubicacion
inner join bodega_ubicacion u2 on u2.codigo_barra2  = s.ubicacion_ant
group by no_recepcion,pb.IdProductobodega, s.productoid,p.IdUnidadMedidaBasica,u.Idubicacion, u2.IdUbicacion,
lote, lic_plate,  fecha_vence, uds_lic_plate, no_bulto,no_telefono

Print 'Stock en tabla temporal de bodega CLC Dañado'
Insert into stock_clc_unificado
select 1 as IdPropietarioBodega, pb.IdProductobodega,no_telefono AS IdProductoEstado, 
Null as IdPresentacion,p.IdUnidadMedidaBasica, u.Idubicacion, u2.IdUbicacion as IdUbicacion_Anterior, 
no_recepcion As IdRecepcionEnc, Null As IdRecepcionDet,
Null As IdPedidEnc, Null IdPickingEnc, Null As IdDespachoEnc,
lote, lic_plate, '' as Serial, count(s.productoid) as cantidad, 
max(fecha_ingreso) as fecha_ingreso, fecha_vence, uds_lic_plate, no_bulto,'19000101' fecha_manufactura, 
Null as añada, '1' as user_agr, GetDate() as fec_agr, '1' as user_mod, GetDate() as fec_mod, 
1 as activo, 0 as peso, 0 as temperatura, '' atributo_variante_1
from stock_clc_dañado_3 s inner join producto p on s.productoid = p.codigo
inner join producto_bodega pb on pb.IdProducto = p.IdProducto and pb.idbodega = 1
inner join bodega_ubicacion u on u.codigo_barra2  = s.ubicacion
inner join bodega_ubicacion u2 on u2.codigo_barra2  = s.ubicacion_ant
where ubicacion <> '' 
group by no_recepcion,pb.IdProductobodega, s.productoid,p.IdUnidadMedidaBasica,u.Idubicacion, u2.IdUbicacion,
lote, lic_plate,  fecha_vence, uds_lic_plate, no_bulto,no_telefono

Print 'Inserta el stock de ambas bodegas en la tabla Stock'
Insert into stock
select * from stock_clc_unificado

Print 'Inserta en tablas comparativas'
select 'V3.0 Bodega General' as Version, sum(cantidad) as cant
into comparativo_inventarios
from stock_clc_3
union 
select 'V4.0 Bodega General' as Version, sum(cantidad) as cant
from stock 
where idpropietariobodega = 2
union
select 'V3.0 Bodega Dañado' as Version, sum(cantidad) as cant
from stock_clc_dañado_3
union 
select 'V4.0 Bodega Dañado' as Version, sum(cantidad) as cant
from stock 
where idpropietariobodega = 1

select *
into comparativo_ubicaciones_general
from stock_clc_3
where ubicacion not in (select codigo_barra2 from bodega_ubicacion)

select *
into comparativo_ubicaciones_dañado
from stock_clc_dañado_3
where ubicacion not in (select codigo_barra2 from bodega_ubicacion)

select distinct ubicacion, sum(cantidad) as cant, productoid
into comparativo_faltantes_general
from stock_clc_3
where ubicacion not in (select codigo_barra2 from bodega_ubicacion u inner join stock s on u.IdUbicacion = s.IdUbicacion )
group by ubicacion,productoid

select distinct ubicacion, sum(cantidad) as cant, productoid
into comparativo_faltantes_dañado
from stock_clc_dañado_3
where ubicacion not in (select codigo_barra2 from bodega_ubicacion u inner join stock s on u.IdUbicacion = s.IdUbicacion )
group by ubicacion,productoid

END
```
