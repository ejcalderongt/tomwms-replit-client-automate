---
id: db-brain-sp-sp-index-maintenance-daily
type: db-sp
title: dbo.sp_index_maintenance_daily
schema: dbo
name: sp_index_maintenance_daily
kind: sp
modify_date: 2022-12-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.sp_index_maintenance_daily`

| Atributo | Valor |
|---|---|
| Tipo | SQL_STORED_PROCEDURE |
| Schema modify_date | 2022-12-17 |
| Parámetros | 1 |

## Parámetros

| # | Nombre | Tipo | Out |
|---:|---|---|:-:|
| 1 | `@DatabaseName` | `varchar(50)` |  |

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
Create procedure sp_index_maintenance_daily
@DatabaseName varchar(50)
as
begin
declare @i int=0
declare @IndexCount int
declare @IndexFregQuery nvarchar(max)
declare @IndexRebuildQuery nvarchar(max)
declare @IndexName varchar(500)
declare @TableName varchar(500)
declare @SchemaName varchar(500)
create table #Fregmentedindex(Index_name varchar(max),table_name varchar(max),schema_name varchar(max))
set @IndexFregQuery='SELECT i.[name],o.name,sch.name
	FROM   [' + @DatabaseName + '].sys.dm_db_index_physical_stats (DB_ID('''+ @DatabaseName +'''), NULL, NULL, NULL, NULL) AS s
	INNER JOIN [' + @DatabaseName + '].sys.indexes AS i ON s.object_id = i.object_id AND s.index_id = i.index_id
	INNER JOIN [' + @DatabaseName + '].sys.objects AS o ON i.object_id = o.object_id
	INNER JOIN [' + @DatabaseName + '].sys.schemas AS sch ON o.schema_id=sch.schema_id
	WHERE (s.avg_fragmentation_in_percent > 30 ) and i.name is not null'
insert into #Fregmentedindex(Index_name,table_name,schema_name) exec sp_executesql @IndexFregQuery
set @IndexCount=(select count(1) from #Fregmentedindex)
While (@IndexCount>@i)
begin 
(select top 1 @TableName=table_name, @IndexName=Index_name,@SchemaName= schema_name from #Fregmentedindex)
Set @IndexRebuildQuery ='Alter index [' + @IndexName +'] on ['+@DatabaseName +'].['+@SchemaName+'].[' + @TableName +'] rebuild'
exec sp_executesql @IndexRebuildQuery 
set @i=@i+1
delete from #Fregmentedindex where Index_name=@IndexName and table_name=@TableName
End
End
```
