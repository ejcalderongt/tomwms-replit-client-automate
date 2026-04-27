---
id: db-brain-sp-sp-dropdiagram
type: db-sp
title: dbo.sp_dropdiagram
schema: dbo
name: sp_dropdiagram
kind: sp
modify_date: 2015-10-04
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.sp_dropdiagram`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2015-10-04 |
| Parámetros | 2 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@diagramname` | `sysname` |  |
| 2 | `@owner_id` | `int` |  |

## Consume

- `sysdiagrams`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE PROCEDURE dbo.sp_dropdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
```
