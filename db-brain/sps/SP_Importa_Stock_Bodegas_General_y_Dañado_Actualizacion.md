---
id: db-brain-sp-sp-importa-stock-bodegas-general-y-da-ado-actualizacion
type: db-sp
title: dbo.SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion
schema: dbo
name: SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion
kind: sp
modify_date: 2018-09-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2018-09-17 |

## Consume

- `bodega`
- `bodega_area`
- `bodega_sector`
- `bodega_tramo`
- `bodega_ubicacion`
- `bodega_ubicacion_CLC_3`
- `producto`
- `producto_bodega`
- `stock`
- `stock_clc_3`
- `stock_clc_dañado_3`
- `stock_clc_unificado`
- `tipologia`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
-- ==========================================================================
-- Author:		Carolina Fuentes
-- Create date: 13-09-2018
-- Description:	Actualiza Stock de las Bodegas General y Dañado
-- ===========================================================================
CREATE PROCEDURE [dbo].[SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion]
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
IF OBJECT_ID('dbo.comparativo_inventarios', 'U') IS NOT NULL DROP TABLE comparativo_inventarios

Print 'Importa tabla de bodega_ubicacion de la BD de CLC version 3'
select *
into bodega_ubicacion_CLC_3
from TOMIMSCLCGENERAL.dbo.bodega_ubicacion

Print 'Importa tabla de Stock de la BD de CLC version 3'
select *
into stock_clc_3
from TOMIMSCLCGENERAL.dbo.stock

Print 'Importa tabla producto_estado de la BD de CLC version 3'
select *
into producto_estado_clc
from TOMIMSCLCGENERAL.dbo.tipologia
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

Print 'Hacer el update de los estados de producto  de la bodega de dañado'
UPDATE stock_clc_dañado_3 
set no_telefono = 131
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
