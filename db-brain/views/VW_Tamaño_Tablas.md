---
id: db-brain-view-vw-tama-o-tablas
type: db-view
title: dbo.VW_Tamaño_Tablas
schema: dbo
name: VW_Tamaño_Tablas
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Tamaño_Tablas`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 11 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `TableName` | `sysname` |  |  |
| 2 | `SchemaName` | `sysname` | ✓ |  |
| 3 | `rows` | `bigint` | ✓ |  |
| 4 | `TotalSpaceKB` | `bigint` | ✓ |  |
| 5 | `TotalSpaceMB` | `numeric` | ✓ |  |
| 6 | `UsedSpaceKB` | `bigint` | ✓ |  |
| 7 | `UsedSpaceMB` | `numeric` | ✓ |  |
| 8 | `UnusedSpaceKB` | `bigint` | ✓ |  |
| 9 | `TotalDatabaseSpaceKB` | `bigint` | ✓ |  |
| 10 | `UnusedSpaceMB` | `numeric` | ✓ |  |
| 11 | `UsedSpacePercentage` | `numeric` | ✓ |  |

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW VW_Tamaño_Tablas
AS
WITH TableSizes AS (
    SELECT 
        t.name AS TableName,
        s.name AS SchemaName,
        p.rows,
        SUM(a.total_pages) * 8 AS TotalSpaceKB,
        SUM(a.used_pages) * 8 AS UsedSpaceKB,
        (SUM(a.total_pages) - SUM(a.used_pages)) * 8 AS UnusedSpaceKB
    FROM 
        sys.tables t
    INNER JOIN      
        sys.indexes i ON t.object_id = i.object_id
    INNER JOIN 
        sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
    INNER JOIN 
        sys.allocation_units a ON p.partition_id = a.container_id
    LEFT OUTER JOIN 
        sys.schemas s ON t.schema_id = s.schema_id
    WHERE 
        t.name NOT LIKE 'dt%' 
        AND t.is_ms_shipped = 0
        AND i.object_id > 255 
    GROUP BY 
        t.name, s.name, p.rows
), DatabaseSize AS (
    SELECT 
        SUM(TotalSpaceKB) AS TotalDatabaseSpaceKB
    FROM 
        TableSizes
)
SELECT 
    ts.TableName,
    ts.SchemaName,
    ts.rows,
    ts.TotalSpaceKB,
    CAST(ts.TotalSpaceKB / 1024.00 AS NUMERIC(36, 2)) AS TotalSpaceMB,
    ts.UsedSpaceKB,
    CAST(ts.UsedSpaceKB / 1024.00 AS NUMERIC(36, 2)) AS UsedSpaceMB,
    ts.UnusedSpaceKB,
	ds.TotalDatabaseSpaceKB,
    CAST(ts.UnusedSpaceKB / 1024.00 AS NUMERIC(36, 2)) AS UnusedSpaceMB,
    CAST((ts.UsedSpaceKB * 100.0) / NULLIF(ds.TotalDatabaseSpaceKB, 0)AS NUMERIC(36, 2)) AS UsedSpacePercentage
FROM 
    TableSizes ts
CROSS JOIN 
    DatabaseSize ds
```
