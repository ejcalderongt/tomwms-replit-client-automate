/* #EJC20260526
   Top waits + top queries por tiempo (usar después de ventana de carga).
*/
SET NOCOUNT ON;

/* A) Waits actuales */
SELECT TOP (25)
    wait_type,
    waiting_tasks_count,
    wait_time_ms,
    signal_wait_time_ms
FROM sys.dm_os_wait_stats
WHERE wait_type NOT LIKE 'SLEEP%'
  AND wait_type NOT IN ('BROKER_EVENTHANDLER','BROKER_RECEIVE_WAITFOR','SQLTRACE_INCREMENTAL_FLUSH_SLEEP')
ORDER BY wait_time_ms DESC;

/* B) Top consultas por elapsed time */
SELECT TOP (30)
    qs.total_elapsed_time / 1000 AS total_elapsed_ms,
    qs.execution_count,
    (qs.total_elapsed_time / NULLIF(qs.execution_count,0)) / 1000 AS avg_elapsed_ms,
    (qs.total_worker_time / NULLIF(qs.execution_count,0)) / 1000 AS avg_cpu_ms,
    (qs.total_logical_reads / NULLIF(qs.execution_count,0)) AS avg_logical_reads,
    DB_NAME(st.dbid) AS db_name,
    SUBSTRING(st.text,
              (qs.statement_start_offset/2)+1,
              ((CASE qs.statement_end_offset WHEN -1 THEN DATALENGTH(st.text) ELSE qs.statement_end_offset END - qs.statement_start_offset)/2)+1
    ) AS statement_text
FROM sys.dm_exec_query_stats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) st
ORDER BY qs.total_elapsed_time DESC;
