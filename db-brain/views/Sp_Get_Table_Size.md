---
id: db-brain-view-sp-get-table-size
type: db-view
title: dbo.Sp_Get_Table_Size
schema: dbo
name: Sp_Get_Table_Size
kind: view
modify_date: 2023-08-15
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.Sp_Get_Table_Size`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2023-08-15 |
| Columnas | 9 |

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
| 9 | `UnusedSpaceMB` | `numeric` | ✓ |  |

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[Sp_Get_Table_Size]
AS
SELECT 
    t.name AS TableName,
    s.name AS SchemaName,
    p.rows,
    SUM(a.total_pages) * 8 AS TotalSpaceKB, 
    CAST(ROUND(((SUM(a.total_pages) * 8) / 1024.00), 2) AS NUMERIC(36, 2)) AS TotalSpaceMB,
    SUM(a.used_pages) * 8 AS UsedSpaceKB, 
    CAST(ROUND(((SUM(a.used_pages) * 8) / 1024.00), 2) AS NUMERIC(36, 2)) AS UsedSpaceMB, 
    (SUM(a.total_pages) - SUM(a.used_pages)) * 8 AS UnusedSpaceKB,
    CAST(ROUND(((SUM(a.total_pages) - SUM(a.used_pages)) * 8) / 1024.00, 2) AS NUMERIC(36, 2)) AS UnusedSpaceMB
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
```
