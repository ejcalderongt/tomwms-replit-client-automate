/* #EJC20260526
   Detectar tablas calientes y presión de locks.
*/
SET NOCOUNT ON;

/* A) Locks por objeto (tabla) */
SELECT
    OBJECT_NAME(p.object_id) AS table_name,
    tl.resource_type,
    tl.request_mode,
    tl.request_status,
    COUNT(*) AS lock_count
FROM sys.dm_tran_locks tl
LEFT JOIN sys.partitions p
    ON tl.resource_associated_entity_id = p.hobt_id
WHERE tl.resource_database_id = DB_ID()
  AND p.object_id IS NOT NULL
GROUP BY OBJECT_NAME(p.object_id), tl.resource_type, tl.request_mode, tl.request_status
ORDER BY lock_count DESC;

/* B) Uso de índices (buscar scans costosos/faltas de índice) */
SELECT TOP (50)
    OBJECT_NAME(s.object_id) AS table_name,
    i.name AS index_name,
    s.user_seeks,
    s.user_scans,
    s.user_lookups,
    s.user_updates
FROM sys.dm_db_index_usage_stats s
JOIN sys.indexes i
  ON s.object_id = i.object_id
 AND s.index_id = i.index_id
WHERE s.database_id = DB_ID()
ORDER BY (s.user_scans + s.user_lookups) DESC;

/* C) Tablas WMS críticas: row/page lock waits por índice */
SELECT TOP (100)
    OBJECT_NAME(ios.object_id) AS table_name,
    i.name AS index_name,
    ios.row_lock_wait_count,
    ios.row_lock_wait_in_ms,
    ios.page_lock_wait_count,
    ios.page_lock_wait_in_ms
FROM sys.dm_db_index_operational_stats(DB_ID(), NULL, NULL, NULL) ios
JOIN sys.indexes i
  ON ios.object_id = i.object_id
 AND ios.index_id = i.index_id
WHERE OBJECT_NAME(ios.object_id) IN (
    'stock','stock_res','trans_picking_ubic','trans_movimientos','trans_pe_det','trans_pe_enc'
)
ORDER BY (ios.row_lock_wait_in_ms + ios.page_lock_wait_in_ms) DESC;
