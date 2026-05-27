/* #EJC20260526
   Baseline rápido de capacidad (ejecutar antes y después de pruebas).
*/
SET NOCOUNT ON;

SELECT GETDATE() AS sample_time, @@SERVERNAME AS server_name, DB_NAME() AS db_name;

/* CPU / memoria / presión */
SELECT
    scheduler_count,
    cpu_count,
    hyperthread_ratio,
    physical_memory_kb / 1024 AS physical_memory_mb
FROM sys.dm_os_sys_info;

/* Esperas acumuladas (snapshot) */
SELECT TOP (20)
    wait_type,
    waiting_tasks_count,
    wait_time_ms,
    signal_wait_time_ms
FROM sys.dm_os_wait_stats
WHERE wait_type NOT LIKE 'SLEEP%'
ORDER BY wait_time_ms DESC;

/* Top tablas por volumen (hot candidates) */
SELECT TOP (20)
    t.name AS table_name,
    SUM(p.rows) AS row_count
FROM sys.tables t
JOIN sys.partitions p ON t.object_id = p.object_id AND p.index_id IN (0,1)
GROUP BY t.name
ORDER BY row_count DESC;
