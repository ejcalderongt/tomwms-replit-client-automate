---
id: db-brain-sp-sp-importa-stock-bodegas-general-y-da-ado
type: db-sp
title: dbo.SP_Importa_Stock_Bodegas_General_y_Dañado
schema: dbo
name: SP_Importa_Stock_Bodegas_General_y_Dañado
kind: sp
modify_date: 2018-08-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.SP_Importa_Stock_Bodegas_General_y_Dañado`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2018-08-28 |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `bodega_ubicacion_CLC_3`
- `bodega_ubicacion_CLC_dañado_3`
- `cod_barra_clc`
- `cod_barra_clc_dañado`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_estado_clc`
- `producto_estado_clc_dañado`
- `propietario_bodega`
- `stock`
- `stock_clc_3`
- `stock_clc_dañado_3`
- `stock_clc_unificado`
- `tipologia`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
-- =============================================
-- Author:		Carolina Fuentes
-- Create date: 14-08-2018
-- Description:	Importa Stock de las Bodegas General y Dañado
-- =============================================
CREATE PROCEDURE SP_Importa_Stock_Bodegas_General_y_Dañado
AS
BEGIN

Print 'Drop Tables'
IF OBJECT_ID('dbo.bodega_ubicacion_CLC_3', 'U') IS NOT NULL DROP TABLE bodega_ubicacion_CLC_3
IF OBJECT_ID('dbo.bodega_ubicacion_CLC_3', 'U') IS NOT NULL DROP TABLE stock_clc_3
IF OBJECT_ID('dbo.bodega_ubicacion_CLC_3', 'U') IS NOT NULL DROP TABLE cod_barra_clc
IF OBJECT_ID('dbo.producto_estado_clc', 'U') IS NOT NULL DROP TABLE producto_estado_clc
IF OBJECT_ID('dbo.bodega_ubicacion_CLC_dañado_3', 'U') IS NOT NULL DROP TABLE bodega_ubicacion_CLC_dañado_3
IF OBJECT_ID('dbo.stock_clc_dañado_3', 'U') IS NOT NULL DROP TABLE stock_clc_dañado_3
IF OBJECT_ID('dbo.producto_estado_clc_dañado', 'U') IS NOT NULL DROP TABLE producto_estado_clc_dañado
IF OBJECT_ID('dbo.cod_barra_clc_dañado', 'U') IS NOT NULL DROP TABLE cod_barra_clc_dañado
IF OBJECT_ID('dbo.comparativo_ubicaciones_general', 'U') IS NOT NULL DROP TABLE comparativo_ubicaciones_general
IF OBJECT_ID('dbo.comparativo_ubicaciones_dañado', 'U') IS NOT NULL DROP TABLE comparativo_ubicaciones_dañado
IF OBJECT_ID('dbo.comparativo_faltantes_general', 'U') IS NOT NULL DROP TABLE comparativo_faltantes_general
IF OBJECT_ID('dbo.comparativo_faltantes_dañado', 'U') IS NOT NULL DROP TABLE comparativo_faltantes_dañado
IF OBJECT_ID('dbo.stock_clc_3', 'U') IS NOT NULL DROP TABLE stock_clc_3

Print 'Importa tabla de bodega_ubicacion de la BD de CLC version 3'
select *
into bodega_ubicacion_CLC_3
from TOMIMS_CLC.dbo.bodega_ubicacion

Print 'Importa tabla de Stock de la BD de CLC version 3'
select *
into stock_clc_3
from TOMIMS_CLC.dbo.stock

Print 'Importa tabla producto_estado de la BD de CLC version 3'
select *
into producto_estado_clc
from TOMIMS_CLC.dbo.tipologia
where padre = 92

Print 'Importa tabla de bodega_ubicacion de la BD de CLC DE DAÑADO version 3'
select *
into bodega_ubicacion_CLC_dañado_3
from TOMIMSCLCDAÑADO.dbo.bodega_ubicacion

Print 'Importa tabla de Stock de la BD de CLC DE DAÑADO version 3'
select *
into stock_clc_dañado_3
from TOMIMSCLCDAÑADO.dbo.stock

Print 'Importa tabla producto_estado de la BD de CLC de Dañado version 3'
select *
into producto_estado_clc_dañado
from TOMIMSCLCDAÑADO.dbo.tipologia
where padre = 92

Print 'Le cambia el collation a la tabla bodega_ubicacion_CLC_3'
alter table bodega_ubicacion_CLC_3 alter column bodegaid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table bodega_ubicacion_CLC_3 alter column areaid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table bodega_ubicacion_CLC_3 alter column sectorid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table bodega_ubicacion_CLC_3 alter column tramoid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table bodega_ubicacion_CLC_3 alter column ubicacion  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table bodega_ubicacion_CLC_3 alter column descripcion  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table bodega_ubicacion_CLC_3 alter column cod_barra  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table bodega_ubicacion_CLC_3 alter column user_Agr  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table bodega_ubicacion_CLC_3 alter column user_mod  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;

Print 'Le cambia el collation a la tabla stock_clc_3'
alter table stock_clc_3 alter column bodegaid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table stock_clc_3 alter column productoid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table stock_clc_3 alter column ubicacion nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table stock_clc_3 alter column lote nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table stock_clc_3 alter column estado  nvarchar(50) collate MODERN_SPANISH_CI_AS  ;
alter table stock_clc_3 alter column sim_card  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table stock_clc_3 alter column no_telefono  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table stock_clc_3 alter column ubicacion_ant  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table stock_clc_3 alter column no_bulto  nvarchar(50) collate MODERN_SPANISH_CI_AS ;
alter table stock_clc_3 alter column propietarioid  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;

Print 'Le cambia el collation a la tabla produto_estado_clc'
alter table producto_estado_clc alter column bodegaid nvarchar(3) collate MODERN_SPANISH_CI_AS null ;
alter table producto_estado_clc alter column nombre nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table producto_estado_clc alter column descripcion nvarchar(25) collate MODERN_SPANISH_CI_AS null ;
alter table producto_estado_clc alter column valor1 nvarchar(25) collate MODERN_SPANISH_CI_AS null ;
alter table producto_estado_clc alter column valor2  nvarchar(25) collate MODERN_SPANISH_CI_AS  ;

Print 'Le cambia el collation a la tabla bodega_ubicacion_CLC_3'
alter table bodega_ubicacion_CLC_dañado_3 alter column bodegaid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table bodega_ubicacion_CLC_dañado_3 alter column areaid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table bodega_ubicacion_CLC_dañado_3 alter column sectorid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table bodega_ubicacion_CLC_dañado_3 alter column tramoid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table bodega_ubicacion_CLC_dañado_3 alter column ubicacion  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table bodega_ubicacion_CLC_dañado_3 alter column descripcion  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table bodega_ubicacion_CLC_dañado_3 alter column cod_barra  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table bodega_ubicacion_CLC_dañado_3 alter column user_Agr  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table bodega_ubicacion_CLC_dañado_3 alter column user_mod  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;

Print 'Le cambia el collation a la taba stock_clc_3'
alter table stock_clc_dañado_3 alter column bodegaid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table stock_clc_dañado_3 alter column productoid nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table stock_clc_dañado_3 alter column ubicacion nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table stock_clc_dañado_3 alter column lote nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table stock_clc_dañado_3 alter column estado  nvarchar(50) collate MODERN_SPANISH_CI_AS  ;
alter table stock_clc_dañado_3 alter column sim_card  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table stock_clc_dañado_3 alter column no_telefono  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table stock_clc_dañado_3 alter column ubicacion_ant  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;
alter table stock_clc_dañado_3 alter column no_bulto  nvarchar(50) collate MODERN_SPANISH_CI_AS ;
alter table stock_clc_dañado_3 alter column propietarioid  nvarchar(50) collate MODERN_SPANISH_CI_AS not null ;

Print 'Update a la tabla bodega_area campo descripcion AK1'
update bodega_area set Descripcion = 'AK1' where IdBodega = 1 and IdArea =1
update bodega_area set Descripcion = 'AK1' where IdBodega = 3 and IdArea =7

Print 'Insertando propietario 1 en la bodega 3'
insert into propietario_bodega values(2,1,3,1,'2016-08-04 18:26:50.657',1,'2016-08-04 18:26:50.657',1)

Print 'Modifica el codigo de barra de la tabla stock_clc_3 para quitar las ubicaciones que no existen R12 y R14 en la posicion D'
UPDATE stock_clc_3 
set ubicacion = SUBSTRING(ubicacion, 1, 11)+'B'
where SUBSTRING(ubicacion, 12,1) = 'D' and ubicacion in (
SELECT cod_barra 
FROM bodega_ubicacion_CLC_3 WHERE areaid = 'AK1' 
AND SECTORID IN ('S03','S04','S05','S06','S07','S08') AND TRAMOID <>'R16' AND tramoid <>'R08'
AND COD_BARRA NOT IN (SELECT       IMS4MB_CLC.dbo.bodega_area.Descripcion + 
case when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R01' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R02' then 'S01' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R03' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R04' then 'S02' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R05' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R06' then 'S03' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R07' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R08' then 'S04' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R09' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R10' then 'S05' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R11' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R12' then 'S06' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R13' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R14' then 'S07' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R15' THEN 'S08' END + 
substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) +
 case when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 1 then 'A' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 2 then 'B'
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 3 then 'C' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 4 then 'D' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 5 then 'E' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 6 then 'F' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 7 then 'G' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 8 then 'H' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 9 then 'I' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 10 then 'J' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 11 then 'K' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 12 then 'L'
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 13 then 'M' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 14 then 'N' end  + 
 CONVERT( nvarchar(2),IMS4MB_CLC.dbo.bodega_ubicacion.nivel)
 + IMS4MB_CLC.dbo.bodega_ubicacion.orientacion_pos
FROM IMS4MB_CLC.dbo.bodega INNER JOIN
IMS4MB_CLC.dbo.bodega_area ON IMS4MB_CLC.dbo.bodega.IdBodega = IMS4MB_CLC.dbo.bodega_area.IdBodega INNER JOIN
IMS4MB_CLC.dbo.bodega_sector ON IMS4MB_CLC.dbo.bodega_area.IdArea = IMS4MB_CLC.dbo.bodega_sector.IdArea INNER JOIN
IMS4MB_CLC.dbo.bodega_tramo ON IMS4MB_CLC.dbo.bodega_sector.IdSector = IMS4MB_CLC.dbo.bodega_tramo.IdSector INNER JOIN
IMS4MB_CLC.dbo.bodega_ubicacion ON IMS4MB_CLC.dbo.bodega_tramo.IdTramo = IMS4MB_CLC.dbo.bodega_ubicacion.IdTramo
where IMS4MB_CLC.dbo.bodega_tramo.IdTramo <> 5 AND IMS4MB_CLC.dbo.bodega_area.idArea =7 AND IMS4MB_CLC.dbo.bodega_ubicacion.nivel <>5))

Print 'Modifica el codigo de barra de la tabla stock_clc_3 para quitar las ubicaciones que no existen R12 y R14 en la posicion C'
UPDATE stock_clc_3 
set ubicacion = SUBSTRING(ubicacion, 1, 11)+'A'
where SUBSTRING(ubicacion, 12,1) = 'C' and ubicacion in (
SELECT cod_barra 
FROM bodega_ubicacion_CLC_3 WHERE areaid = 'AK1' 
AND SECTORID IN ('S03','S04','S05','S06','S07','S08') AND TRAMOID <>'R16' AND tramoid <>'R08'
AND COD_BARRA NOT IN (SELECT       IMS4MB_CLC.dbo.bodega_area.Descripcion + 
case when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R01' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R02' then 'S01' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R03' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R04' then 'S02' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R05' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R06' then 'S03' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R07' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R08' then 'S04' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R09' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R10' then 'S05' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R11' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R12' then 'S06' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R13' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R14' then 'S07' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R15' THEN 'S08' END + 
substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) +
 case when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 1 then 'A' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 2 then 'B'
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 3 then 'C' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 4 then 'D' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 5 then 'E' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 6 then 'F' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 7 then 'G' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 8 then 'H' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 9 then 'I' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 10 then 'J' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 11 then 'K' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 12 then 'L'
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 13 then 'M' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 14 then 'N' end  + 
 CONVERT( nvarchar(2),IMS4MB_CLC.dbo.bodega_ubicacion.nivel)
 + IMS4MB_CLC.dbo.bodega_ubicacion.orientacion_pos
FROM IMS4MB_CLC.dbo.bodega INNER JOIN
IMS4MB_CLC.dbo.bodega_area ON IMS4MB_CLC.dbo.bodega.IdBodega = IMS4MB_CLC.dbo.bodega_area.IdBodega INNER JOIN
IMS4MB_CLC.dbo.bodega_sector ON IMS4MB_CLC.dbo.bodega_area.IdArea = IMS4MB_CLC.dbo.bodega_sector.IdArea INNER JOIN
IMS4MB_CLC.dbo.bodega_tramo ON IMS4MB_CLC.dbo.bodega_sector.IdSector = IMS4MB_CLC.dbo.bodega_tramo.IdSector INNER JOIN
IMS4MB_CLC.dbo.bodega_ubicacion ON IMS4MB_CLC.dbo.bodega_tramo.IdTramo = IMS4MB_CLC.dbo.bodega_ubicacion.IdTramo
where IMS4MB_CLC.dbo.bodega_tramo.IdTramo <> 5 AND IMS4MB_CLC.dbo.bodega_area.idArea =7 AND IMS4MB_CLC.dbo.bodega_ubicacion.nivel <>5))

Print 'Modifica el codigo de barra de la tabla bodega_ubicacion_CLC_3 para quitar las ubicaciones que no existen R12 y R14 en la posicion D'
UPDATE bodega_ubicacion_CLC_3 
set cod_barra = SUBSTRING(cod_barra, 1, 11)+'B'
where  SUBSTRING(cod_barra, 12, 1) = 'D' and cod_barra in (
SELECT cod_barra 
FROM bodega_ubicacion_CLC_3 WHERE areaid = 'AK1' 
AND SECTORID IN ('S03','S04','S05','S06','S07','S08') AND TRAMOID <>'R16' AND tramoid <>'R08'
AND COD_BARRA NOT IN (SELECT       IMS4MB_CLC.dbo.bodega_area.Descripcion + 
case when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R01' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R02' then 'S01' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R03' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R04' then 'S02' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R05' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R06' then 'S03' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R07' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R08' then 'S04' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R09' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R10' then 'S05' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R11' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R12' then 'S06' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R13' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R14' then 'S07' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R15' THEN 'S08' END + 
substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) +
 case when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 1 then 'A' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 2 then 'B'
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 3 then 'C' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 4 then 'D' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 5 then 'E' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 6 then 'F' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 7 then 'G' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 8 then 'H' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 9 then 'I' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 10 then 'J' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 11 then 'K' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 12 then 'L'
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 13 then 'M' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 14 then 'N' end  + 
 CONVERT( nvarchar(2),IMS4MB_CLC.dbo.bodega_ubicacion.nivel)
 + IMS4MB_CLC.dbo.bodega_ubicacion.orientacion_pos
FROM IMS4MB_CLC.dbo.bodega INNER JOIN
IMS4MB_CLC.dbo.bodega_area ON IMS4MB_CLC.dbo.bodega.IdBodega = IMS4MB_CLC.dbo.bodega_area.IdBodega INNER JOIN
IMS4MB_CLC.dbo.bodega_sector ON IMS4MB_CLC.dbo.bodega_area.IdArea = IMS4MB_CLC.dbo.bodega_sector.IdArea INNER JOIN
IMS4MB_CLC.dbo.bodega_tramo ON IMS4MB_CLC.dbo.bodega_sector.IdSector = IMS4MB_CLC.dbo.bodega_tramo.IdSector INNER JOIN
IMS4MB_CLC.dbo.bodega_ubicacion ON IMS4MB_CLC.dbo.bodega_tramo.IdTramo = IMS4MB_CLC.dbo.bodega_ubicacion.IdTramo
where IMS4MB_CLC.dbo.bodega_tramo.IdTramo <> 5 AND IMS4MB_CLC.dbo.bodega_area.idArea =7 AND IMS4MB_CLC.dbo.bodega_ubicacion.nivel <>5))

Print 'Modifica el codigo de barra de la tabla bodega_ubicacion_CLC_3 para quitar las ubicaciones que no existen R12 y R14 en la posicion C'
UPDATE bodega_ubicacion_CLC_3 
set cod_barra = SUBSTRING(cod_barra, 1, 11) + 'A'
where  SUBSTRING(cod_barra, 12, 1) = 'C' and cod_barra in (
SELECT cod_barra 
FROM bodega_ubicacion_CLC_3 WHERE areaid = 'AK1' 
AND SECTORID IN ('S03','S04','S05','S06','S07','S08') AND TRAMOID <>'R16' AND tramoid <>'R08'
AND COD_BARRA NOT IN (SELECT       IMS4MB_CLC.dbo.bodega_area.Descripcion + 
case when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R01' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R02' then 'S01' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R03' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R04' then 'S02' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R05' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R06' then 'S03' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R07' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R08' then 'S04' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R09' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R10' then 'S05' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R11' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R12' then 'S06' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R13' OR substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R14' then 'S07' 
when substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) = 'R15' THEN 'S08' END + 
substring(IMS4MB_CLC.dbo.bodega_tramo.descripcion,1,3) +
 case when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 1 then 'A' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 2 then 'B'
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 3 then 'C' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 4 then 'D' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 5 then 'E' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 6 then 'F' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 7 then 'G' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 8 then 'H' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 9 then 'I' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 10 then 'J' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 11 then 'K' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 12 then 'L'
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 13 then 'M' 
 when IMS4MB_CLC.dbo.bodega_ubicacion.indice_x = 14 then 'N' end  + 
 CONVERT( nvarchar(2),IMS4MB_CLC.dbo.bodega_ubicacion.nivel)
 + IMS4MB_CLC.dbo.bodega_ubicacion.orientacion_pos
FROM IMS4MB_CLC.dbo.bodega INNER JOIN
IMS4MB_CLC.dbo.bodega_area ON IMS4MB_CLC.dbo.bodega.IdBodega = IMS4MB_CLC.dbo.bodega_area.IdBodega INNER JOIN
IMS4MB_CLC.dbo.bodega_sector ON IMS4MB_CLC.dbo.bodega_area.IdArea = IMS4MB_CLC.dbo.bodega_sector.IdArea INNER JOIN
IMS4MB_CLC.dbo.bodega_tramo ON IMS4MB_CLC.dbo.bodega_sector.IdSector = IMS4MB_CLC.dbo.bodega_tramo.IdSector INNER JOIN
IMS4MB_CLC.dbo.bodega_ubicacion ON IMS4MB_CLC.dbo.bodega_tramo.IdTramo = IMS4MB_CLC.dbo.bodega_ubicacion.IdTramo
where IMS4MB_CLC.dbo.bodega_tramo.IdTramo <> 5 AND IMS4MB_CLC.dbo.bodega_area.idArea =7 AND IMS4MB_CLC.dbo.bodega_ubicacion.nivel <>5))

Print 'Crea tabla para relacionar el IdUbicacion con el codigo de barra de CLC  bodega General de CLC'
SELECT   u1.idubicacion,   a1.Descripcion + 
case when substring(t1.descripcion,1,3) = 'R01' OR substring(t1.descripcion,1,3) = 'R02' then 'S01' 
when substring(t1.descripcion,1,3) = 'R03' OR substring(t1.descripcion,1,3) = 'R04' then 'S02' 
when substring(t1.descripcion,1,3) = 'R05' OR substring(t1.descripcion,1,3) = 'R06' then 'S03' 
when substring(t1.descripcion,1,3) = 'R07' OR substring(t1.descripcion,1,3) = 'R08' then 'S04' 
when substring(t1.descripcion,1,3) = 'R09' OR substring(t1.descripcion,1,3) = 'R10' then 'S05' 
when substring(t1.descripcion,1,3) = 'R11' OR substring(t1.descripcion,1,3) = 'R12' then 'S06' 
when substring(t1.descripcion,1,3) = 'R13' OR substring(t1.descripcion,1,3) = 'R14' then 'S07' 
when substring(t1.descripcion,1,3) = 'R15' THEN 'S08' END + 
substring(t1.descripcion,1,3) +
 case when u1.indice_x = 1 then 'A' 
 when u1.indice_x = 2 then 'B'
 when u1.indice_x = 3 then 'C' 
 when u1.indice_x = 4 then 'D' 
 when u1.indice_x = 5 then 'E' 
 when u1.indice_x = 6 then 'F' 
 when u1.indice_x = 7 then 'G' 
 when u1.indice_x = 8 then 'H' 
 when u1.indice_x = 9 then 'I' 
 when u1.indice_x = 10 then 'J' 
 when u1.indice_x = 11 then 'K' 
 when u1.indice_x = 12 then 'L'
 when u1.indice_x = 13 then 'M' 
 when u1.indice_x = 14 then 'N' end  + 
 CONVERT( nvarchar(2),u1.nivel)
 + u1.orientacion_pos AS cod_barra2
 into cod_barra_clc
FROM IMS4MB_CLC.dbo.bodega b1 INNER JOIN
IMS4MB_CLC.dbo.bodega_area  a1 ON b1.IdBodega = a1.IdBodega INNER JOIN
IMS4MB_CLC.dbo.bodega_sector s1 ON a1.IdArea = s1.IdArea INNER JOIN
bodega_tramo t1 ON s1.IdSector = t1.IdSector INNER JOIN
IMS4MB_CLC.dbo.bodega_ubicacion u1 ON t1.IdTramo = u1.IdTramo
where t1.IdTramo <> 5 AND a1.idArea =7 AND u1.nivel <>5 

Print 'Actualizar el cod_barra2 de la ubicacion para que sea igual al de la version 3.0 --  bodega General de CLC'
update bodega_ubicacion set codigo_barra2 = (select isnull(cod_barra2,'')
from cod_barra_clc 
where bodega_ubicacion.IdUbicacion = cod_barra_clc.idubicacion)

Print 'Insertar estados de productos bodega General de CLC'
INSERT INTO producto_estado
select tipoid, 1, descripcion, valor2,  valor1,activo, 1, getdate(),1,getdate(), 
case when tipoid = 93 then 1 else 0 end
from producto_estado_clc

Print 'Insertar bodega area bodega General de CLC'
INSERT INTO bodega_area values(8,3,'Sistema',1,1,'20180806',1,'20180806','8',1,0,0,0,0,0,0,0)
INSERT INTO bodega_area values(9,3,'Piso',1,1,'20180806',1,'20180806','9',1,0,0,0,0,0,0,0)

Print 'Insertar bodega_sector bodega General de CLC'
INSERT INTO bodega_sector values(19,9,0,'Piso',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'S00',0,0,0,0,0)
INSERT INTO bodega_sector values(20,8,0,'Picking',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'P00',0,0,0,0,0)
INSERT INTO bodega_sector values(21,8,0,'Despacho',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'D00',0,0,0,0,0)
INSERT INTO bodega_sector values(22,8,0,'Merma',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'M00',0,0,0,0,0)

Print 'Insertar bodega_tramo bodega General de CLC'
INSERT INTO bodega_tramo values(17,19,0,'Piso',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'17',0,0,0,0,0,0)
INSERT INTO bodega_tramo values(18,20,0,'Picking',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'18',0,0,0,0,0,0)
INSERT INTO bodega_tramo values(19,21,0,'Despacho',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'19',0,0,0,0,0,0)
INSERT INTO bodega_tramo values(20,22,0,'Merma',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'20',0,0,0,0,0,0)

Print 'Insertar bodega_ubicacion Piso  bodega General de CLC'
insert into bodega_ubicacion
SELECT        cod_barra, 17 as idtramo, descripcion, 0,0,0,0,0,0,1,0,cod_barra,cod_barra as cod_barra2,1,'20180806',1,'20180806',
0,1,0,1,0,0,0,0,0,0,0,0,'',0
FROM            bodega_ubicacion_CLC_3
WHERE        (LEN(cod_barra) <= 3) and convert(int, cod_barra)>10

Print 'Recepción bodega General de CLC'
insert into bodega_ubicacion
SELECT        cod_barra, 17 as idtramo, descripcion, 0,0,0,0,0,0,1,0,cod_barra,cod_barra,1,'20180806',1,'20180806',
0,1,0,1,0,1,0,0,0,0,0,0,'',0
FROM            bodega_ubicacion_CLC_3
WHERE        (LEN(cod_barra) <= 3) and convert(int, cod_barra)=1

Print 'Picking bodega General de CLC'
insert into bodega_ubicacion
SELECT        cod_barra,18 as idtramo, descripcion, 0,0,0,0,0,0,1,0,cod_barra,cod_barra,1,'20180806',1,'20180806',
0,1,0,1,0,1,0,0,0,0,0,0,'',0
FROM            bodega_ubicacion_CLC_3
WHERE        (LEN(cod_barra) <= 3) and convert(int, cod_barra)=5

Print 'Despacho bodega General de CLC'
insert into bodega_ubicacion
SELECT        cod_barra,19 as idtramo, descripcion, 0,0,0,0,0,0,1,0,cod_barra,cod_barra,1,'20180806',1,'20180806',
0,1,0,1,0,1,0,0,1,0,0,0,'',0
FROM            bodega_ubicacion_CLC_3
WHERE        (LEN(cod_barra) <= 3) and convert(int, cod_barra)=3

Print 'Merma bodega General de CLC'
insert into bodega_ubicacion
SELECT        cod_barra,20 as idtramo, descripcion, 0,0,0,0,0,0,1,0,cod_barra,cod_barra,1,'20180806',1,'20180806',
0,1,0,1,0,0,0,1,0,0,0,0,'',0
FROM            bodega_ubicacion_CLC_3
WHERE        (LEN(cod_barra) <= 3) and convert(int, cod_barra)=4

Print 'Insertar bodega area de bodega de dañado CLC'
INSERT INTO bodega_area values(12,1,'Piso',1,1,'20180806',1,'20180806','12',1,0,0,0,0,0,0,0)

Print 'Insertar bodega_sector de bodega de dañado CLC'
INSERT INTO bodega_sector values(23,12,0,'Piso',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'23',0,0,0,0,0)
INSERT INTO bodega_sector values(24,6,0,'Picking',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'24',0,0,0,0,0)
INSERT INTO bodega_sector values(25,6,0,'Merma',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'25',0,0,0,0,0)

Print 'Insertar bodega_tramo de bodega de dañado CLC'
INSERT INTO bodega_tramo values(21,23,0,'Piso',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'21',0,0,0,0,0,0)
INSERT INTO bodega_tramo values(22,24,0,'Picking',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'22',0,0,0,0,0,0)
INSERT INTO bodega_tramo values(23,25,0,'Merma',1,'20180806',1,'20180806',1,0,0,0,0,0,0,0,'23',0,0,0,0,0,0)

Print 'Insertar bodega_ubicacion Piso  bodega de Dañado CLC'
insert into bodega_ubicacion
SELECT        21, 21 as idtramo, descripcion, 0,0,0,0,0,0,1,0,'21','21' as cod_barra2,1,'20180806',1,'20180806',
0,1,0,1,0,0,0,0,0,0,0,0,'',0
FROM            bodega_ubicacion_CLC_dañado_3
WHERE       cod_barra in ('1')

Print 'Picking bodega de Dañado CLC'
insert into bodega_ubicacion
SELECT        22,22 as idtramo, descripcion, 0,0,0,0,0,0,1,0,'22','22',1,'20180806',1,'20180806',
0,1,0,1,0,1,0,0,0,0,0,0,'',0
FROM            bodega_ubicacion_CLC_3
WHERE      cod_barra in ('4')

Print 'Merma bodega de Dañado CLC'
insert into bodega_ubicacion
SELECT        23, 23 as idtramo, descripcion, 0,0,0,0,0,0,1,0,'23','23',1,'20180806',1,'20180806',
0,1,0,1,0,1,0,0,0,0,0,0,'',0
FROM            bodega_ubicacion_CLC_3
WHERE      cod_barra in ('5')

Print 'Modificar las ubicaciones en el stock de la bodega de dañado'
UPDATE stock_clc_dañado_3 SET ubicacion = '21' where ubicacion = '1'
UPDATE stock_clc_dañado_3 SET ubicacion = '22' where ubicacion = '4'
UPDATE stock_clc_dañado_3 SET ubicacion = '23' where ubicacion = '5'

Print 'Modificar las ubicaciones anteriores en el stock de la bodega de dañado'
UPDATE stock_clc_dañado_3 SET ubicacion_ant = '21' where ubicacion_ant = '1'
UPDATE stock_clc_dañado_3 SET ubicacion_ant = '22' where ubicacion_ant = '4'
UPDATE stock_clc_dañado_3 SET ubicacion_ant = '23' where ubicacion_ant = '5'

Print 'Modificar las ubicaciones por defecto de los estados de los productos de la bodega de dañado'
UPDATE producto_estado_clc_dañado SET valor2 = '21' where valor2 = '1'
UPDATE producto_estado_clc_dañado SET valor2 = '22' where valor2 = '4'
UPDATE producto_estado_clc_dañado SET valor2 = '23' where valor2 = '5'

Print 'Crea tabla para relacionar el IdUbicacion con el codigo de barra de CLC de la bodega de dañado'
SELECT   u1.idubicacion,   a1.Descripcion + 
case when substring(t1.descripcion,1,3) = 'R01' OR substring(t1.descripcion,1,3) = 'R02' then 'S01' 
when substring(t1.descripcion,1,3) = 'R03' OR substring(t1.descripcion,1,3) = 'R04' then 'S02' 
when substring(t1.descripcion,1,3) = 'R05' OR substring(t1.descripcion,1,3) = 'R06' then 'S03' 
when substring(t1.descripcion,1,3) = 'R07' OR substring(t1.descripcion,1,3) = 'R08' then 'S04' 
when substring(t1.descripcion,1,3) = 'R09' OR substring(t1.descripcion,1,3) = 'R10' then 'S05' 
when substring(t1.descripcion,1,3) = 'R11' OR substring(t1.descripcion,1,3) = 'R12' then 'S06' 
when substring(t1.descripcion,1,3) = 'R13' OR substring(t1.descripcion,1,3) = 'R14' then 'S07' 
when substring(t1.descripcion,1,3) = 'R15' THEN 'S08' END + 
substring(t1.descripcion,1,3) +
 case when u1.indice_x = 1 then 'A' 
 when u1.indice_x = 2 then 'B'
 when u1.indice_x = 3 then 'C' 
 when u1.indice_x = 4 then 'D' 
 when u1.indice_x = 5 then 'E' 
 when u1.indice_x = 6 then 'F' 
 when u1.indice_x = 7 then 'G' 
 when u1.indice_x = 8 then 'H' 
 when u1.indice_x = 9 then 'I' 
 when u1.indice_x = 10 then 'J' 
 when u1.indice_x = 11 then 'K' 
 when u1.indice_x = 12 then 'L'
 when u1.indice_x = 13 then 'M' 
 when u1.indice_x = 14 then 'N' end  + 
 CONVERT( nvarchar(2),u1.nivel)
 + u1.orientacion_pos AS cod_barra2
into cod_barra_clc_dañado
FROM IMS4MB_CLC.dbo.bodega b1 INNER JOIN
IMS4MB_CLC.dbo.bodega_area  a1 ON b1.IdBodega = a1.IdBodega INNER JOIN
IMS4MB_CLC.dbo.bodega_sector s1 ON a1.IdArea = s1.IdArea INNER JOIN
bodega_tramo t1 ON s1.IdSector = t1.IdSector INNER JOIN
IMS4MB_CLC.dbo.bodega_ubicacion u1 ON t1.IdTramo = u1.IdTramo
where t1.IdTramo <> 5 AND a1.idArea =1 AND u1.nivel <>5 

Print 'Actualizar el cod_barra2 de la ubicacion para que sea igual al de la version 3.0  de la bodega de dañado'
update bodega_ubicacion set codigo_barra2 = (select isnull(cod_barra2,'')
from cod_barra_clc_dañado 
where bodega_ubicacion.IdUbicacion = cod_barra_clc_dañado.idubicacion) 
where idtramo in (1,2,3,4)

Print 'Crear Estados de producto  de la bodega de dañado'
INSERT INTO producto_estado
select (select max(idestado) +1 from producto_estado), 1, descripcion, valor2,  
valor1,activo, 1, getdate(),1,getdate(), 0
from producto_estado_clc_dañado
where tipoid = 93

Print 'Hacer el update de los estados de producto de la bodega de dañado'
UPDATE stock_clc_dañado_3 
set no_telefono = (select max(idestado) from producto_estado)
where no_telefono = 93

INSERT INTO producto_estado
select (select max(idestado) +1 from producto_estado), 1, descripcion, valor2,  
valor1,activo, 1, getdate(),1,getdate(), 0
from producto_estado_clc_dañado
where tipoid = 106

Print 'Hacer el update de los estados de producto  de la bodega de dañado'
UPDATE stock_clc_dañado_3 
set no_telefono = (select max(idestado) from producto_estado)
where no_telefono = 106

UPDATE stock_clc_dañado_3 
set no_telefono = 109
where no_telefono = 108

UPDATE stock_clc_dañado_3 
set no_telefono = 121
where no_telefono = 95

UPDATE stock_clc_dañado_3 
set no_telefono = 104
where no_telefono = 100

UPDATE stock_clc_dañado_3 
set no_telefono = 106
where no_telefono = 107

UPDATE stock_clc_dañado_3 
set no_telefono =97
where no_telefono = 98

Print 'Crea tabla para guardar inventario'
IF OBJECT_ID('dbo.bodega_ubicacion_CLC_3', 'U') IS NOT NULL 
CREATE TABLE [dbo].[stock_clc_unificado](
	[IdStock] [int] NOT NULL identity(1,1),
	[IdPropietarioBodega] [int] NOT NULL,
	[IdProductoBodega] [int] NOT NULL,
	[IdProductoEstado] [int] NULL,
	[IdPresentacion] [int] NULL,
	[IdUnidadMedida] [int] NULL,
	[IdUbicacion] [int] NOT NULL,
	[IdUbicacion_anterior] [int] NULL,
	[IdRecepcionEnc] [int] NULL,
	[IdRecepcionDet] [int] NULL,
	[IdPedidoEnc] [int] NULL,
	[IdPickingEnc] [int] NULL,
	[IdDespachoEnc] [int] NULL,
	[lote] [nvarchar](50) NULL,
	[lic_plate] [nvarchar](50) NULL,
	[serial] [nvarchar](50) NULL,
	[cantidad] [float] NULL CONSTRAINT [DF_stock_uni_cantidad]  DEFAULT ((0)),
	[fecha_ingreso] [datetime] NULL CONSTRAINT [DF_stock_uni_fecha_ingreso]  DEFAULT (getdate()),
	[fecha_vence] [datetime] NULL CONSTRAINT [DF_stock_uni_fecha_vence]  DEFAULT (getdate()),
	[uds_lic_plate] [float] NULL,
	[no_bulto] [int] NULL,
	[fecha_manufactura] [datetime] NULL,
	[añada] [int] NULL,
	[user_agr] [nvarchar](50) NOT NULL,
	[fec_agr] [datetime] NOT NULL,
	[user_mod] [nvarchar](50) NOT NULL,
	[fec_mod] [datetime] NOT NULL,
	[activo] [bit] NOT NULL,
	[peso] [float] NULL CONSTRAINT [DF_stock_uni_peso]  DEFAULT ((0)),
	[temperatura] [float] NULL CONSTRAINT [DF_stock_uni_temperatura]  DEFAULT ((0)),
	[atributo_variante_1] [nvarchar](25) NULL,
 CONSTRAINT [PK_stock_unificado] PRIMARY KEY CLUSTERED 
(
	[IdStock] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


delete from stock_clc_unificado
delete from stock

Print 'Stock de la Bodega General de CLC'
Insert into stock_clc_unificado
select 2 as IdPropietarioBodega, pb.IdProductobodega,no_telefono AS IdProductoEstado, 
Null as IdPresentacion,p.IdUnidadMedidaBasica, u.Idubicacion, u2.IdUbicacion as IdUbicacion_Anterior, 
no_recepcion As IdRecepcionEnc, Null As IdRecepcionDet,
Null As IdPedidEnc, Null IdPickingEnc, Null As IdDespachoEnc,
lote, lic_plate, '' as Serial, count(s.productoid) as cantidad, 
fecha_ingreso, fecha_vence, uds_lic_plate, no_bulto,'19000101' fecha_manufactura, 
Null as añada, '1' as user_agr, GetDate() as fec_agr, '1' as user_mod, GetDate() as fec_mod, 
1 as activo, 0 as peso, 0 as temperatura, '' atributo_variante_1
from stock_clc_3 s inner join producto p on s.productoid = p.codigo
inner join producto_bodega pb on pb.IdProducto = p.IdProducto and pb.idbodega = 3
inner join bodega_ubicacion u on u.codigo_barra2  = s.ubicacion
inner join bodega_ubicacion u2 on u2.codigo_barra2  = s.ubicacion_ant
group by no_recepcion,pb.IdProductobodega, s.productoid,p.IdUnidadMedidaBasica,u.Idubicacion, u2.IdUbicacion,
lote, lic_plate,  fecha_ingreso, fecha_vence, uds_lic_plate, no_bulto,no_telefono

Print 'Stock en tabla temporal de bodega CLC Dañado'
Insert into stock_clc_unificado
select 1 as IdPropietarioBodega, pb.IdProductobodega,no_telefono AS IdProductoEstado, 
Null as IdPresentacion,p.IdUnidadMedidaBasica, u.Idubicacion, u2.IdUbicacion as IdUbicacion_Anterior, 
no_recepcion As IdRecepcionEnc, Null As IdRecepcionDet,
Null As IdPedidEnc, Null IdPickingEnc, Null As IdDespachoEnc,
lote, lic_plate, '' as Serial, count(s.productoid) as cantidad, 
fecha_ingreso, fecha_vence, uds_lic_plate, no_bulto,'19000101' fecha_manufactura, 
Null as añada, '1' as user_agr, GetDate() as fec_agr, '1' as user_mod, GetDate() as fec_mod, 
1 as activo, 0 as peso, 0 as temperatura, '' atributo_variante_1
from stock_clc_dañado_3 s inner join producto p on s.productoid = p.codigo
inner join producto_bodega pb on pb.IdProducto = p.IdProducto and pb.idbodega = 1
inner join bodega_ubicacion u on u.codigo_barra2  = s.ubicacion
inner join bodega_ubicacion u2 on u2.codigo_barra2  = s.ubicacion_ant
where ubicacion <> '' 
group by no_recepcion,pb.IdProductobodega, s.productoid,p.IdUnidadMedidaBasica,u.Idubicacion, u2.IdUbicacion,
lote, lic_plate,  fecha_ingreso, fecha_vence, uds_lic_plate, no_bulto,no_telefono

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
