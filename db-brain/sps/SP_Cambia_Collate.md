---
id: db-brain-sp-sp-cambia-collate
type: db-sp
title: dbo.SP_Cambia_Collate
schema: dbo
name: SP_Cambia_Collate
kind: sp
modify_date: 2018-08-28
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.SP_Cambia_Collate`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2018-08-28 |

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
-- =============================================
-- Author:		Carolina Fuentes
-- Create date: 14-08-2018
-- Description:	Modificar el Collate bodega, bodega_area, bodega_sector, bodega_tramo, bodega_ubicacion
-- =============================================
CREATE PROCEDURE SP_Cambia_Collate
AS
BEGIN

Print 'Le cambia el collation a la tabla bodega'
alter table bodega alter column [codigo] nvarchar(50) collate MODERN_SPANISH_CI_AS null ;
alter table bodega alter column [codigo_barra] nvarchar(150) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [nombre] nvarchar(50) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [nombre_comercial]   nvarchar(50) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [direccion]  nvarchar(250) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [telefono]  nvarchar(50) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega alter column [email]  nvarchar(50) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [encargado]  nvarchar(50)collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega alter column [ubic_recepcion]  nvarchar(25) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [ubic_picking]  nvarchar(25) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [ubic_despacho] nvarchar(25)collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega alter column [ubic_merma]  nvarchar(50) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [user_agr]  nvarchar(25) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [user_mod]  nvarchar(25) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [coordenada_x]  nvarchar(50) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [coordenada_y]   nvarchar(50) collate MODERN_SPANISH_CI_AS NULL;
alter table bodega alter column [IdTipoTransaccion]   nvarchar(50)collate MODERN_SPANISH_CI_AS  NULL;

Print 'Le cambia el collation a la tabla bodega_area'
alter table bodega_area alter column [Descripcion] [nvarchar](50) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_area alter column [user_agr] [nvarchar](25) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_area alter column [user_mod] [nvarchar](25) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_area alter column [Codigo] [nvarchar](50) collate MODERN_SPANISH_CI_AS  NULL;

Print 'Le cambia el collation a la tabla bodega_sector'
alter table bodega_sector alter column [Descripcion] [nvarchar](50) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_sector alter column [user_agr] [nvarchar](25) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_sector alter column [user_mod] [nvarchar](25) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_sector alter column [Codigo] [nvarchar](50) collate MODERN_SPANISH_CI_AS  NULL;

Print 'Le cambia el collation a la tabla bodega_tramo'
alter table bodega_tramo alter column [Descripcion] [nvarchar](50) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_tramo alter column [user_agr] [nvarchar](25) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_tramo alter column [user_mod] [nvarchar](25) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_tramo alter column [Codigo] [nvarchar](50) collate MODERN_SPANISH_CI_AS  NULL;

Print 'Le cambia el collation a la tabla bodega_ubicacion'
alter table bodega_ubicacion alter column [Descripcion] [nvarchar](50) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_ubicacion alter column [user_agr] [nvarchar](25) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_ubicacion alter column [user_mod] [nvarchar](25) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_ubicacion alter column [Codigo_barra] [nvarchar](50) collate MODERN_SPANISH_CI_AS  NULL;
alter table bodega_ubicacion alter column [Codigo_barra2] [nvarchar](50) collate MODERN_SPANISH_CI_AS  NULL;

END
```
