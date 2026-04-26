-- ============================================================
-- TOMWMS Brain — SQL Catalog Extractor (raw queries)
-- ============================================================
-- Run these 3 queries against TOMWMS_KILLIOS_PRD (read-only).
-- Permissions required: SELECT on sys.objects, sys.sql_expression_dependencies,
--   sys.sql_modules, sys.partitions. No write access needed.
--
-- Used by: tools/sql-catalog/extract_sql_catalog.py
-- Output  : sql_objects.json, sql_dependencies.json, sql_modules.json
-- ============================================================

-- 1. sys.objects: tables/views/SPs/functions/triggers
SELECT
    s.name                                                AS schema_name,
    o.name                                                AS object_name,
    CASE o.type
        WHEN 'U'  THEN 'sql-table'
        WHEN 'V'  THEN 'sql-view'
        WHEN 'P'  THEN 'sql-sp'
        WHEN 'FN' THEN 'sql-fn'
        WHEN 'IF' THEN 'sql-fn'
        WHEN 'TF' THEN 'sql-fn'
        WHEN 'TR' THEN 'sql-trigger'
    END                                                   AS kind,
    o.object_id,
    CONVERT(VARCHAR(33), o.create_date, 126)              AS create_date,
    CONVERT(VARCHAR(33), o.modify_date, 126)              AS modify_date,
    ISNULL(DATALENGTH(m.definition), 0)                   AS definition_length,
    CASE WHEN o.type = 'U'
         THEN (SELECT TOP 1 SUM(p.rows)
                 FROM sys.partitions p
                WHERE p.object_id = o.object_id
                  AND p.index_id IN (0,1))
         ELSE NULL
    END                                                   AS row_count
FROM sys.objects        o
JOIN sys.schemas        s ON s.schema_id = o.schema_id
LEFT JOIN sys.sql_modules m ON m.object_id = o.object_id
WHERE o.type IN ('U','V','P','FN','IF','TF','TR')
  AND o.is_ms_shipped = 0
ORDER BY s.name, o.name;

-- 2. sys.sql_expression_dependencies: who references whom
SELECT
    s_from.name                       AS from_schema,
    o_from.name                       AS from_name,
    d.referenced_schema_name          AS to_schema,
    d.referenced_entity_name          AS to_name,
    CASE d.referenced_class
        WHEN 1 THEN
            CASE
                WHEN o_to.type = 'U'  THEN 'sql-table'
                WHEN o_to.type = 'V'  THEN 'sql-view'
                WHEN o_to.type = 'P'  THEN 'sql-sp'
                WHEN o_to.type IN ('FN','IF','TF') THEN 'sql-fn'
                WHEN o_to.type = 'TR' THEN 'sql-trigger'
                ELSE NULL
            END
        ELSE NULL
    END                               AS to_kind_hint,
    CAST(d.is_ambiguous AS BIT)       AS is_ambiguous
FROM sys.sql_expression_dependencies d
JOIN sys.objects  o_from ON o_from.object_id = d.referencing_id
JOIN sys.schemas  s_from ON s_from.schema_id = o_from.schema_id
LEFT JOIN sys.objects  o_to   ON o_to.object_id   = d.referenced_id
WHERE o_from.type IN ('U','V','P','FN','IF','TF','TR')
  AND o_from.is_ms_shipped = 0
  AND d.referenced_entity_name IS NOT NULL;

-- 3. sys.sql_modules: full text definitions of procs/views/funcs/triggers
SELECT
    s.name        AS schema_name,
    o.name        AS object_name,
    m.definition  AS definition
FROM sys.sql_modules m
JOIN sys.objects     o ON o.object_id = m.object_id
JOIN sys.schemas     s ON s.schema_id = o.schema_id
WHERE o.is_ms_shipped = 0
  AND o.type IN ('V','P','FN','IF','TF','TR');
