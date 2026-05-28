/* #EJC20260526
   Monitoreo en vivo: bloqueos activos + deadlocks capturados.
*/
SET NOCOUNT ON;

/* A) Bloqueos activos */
SELECT
    GETDATE() AS sample_time,
    r.session_id,
    r.blocking_session_id,
    r.status,
    r.wait_type,
    r.wait_time,
    r.wait_resource,
    DB_NAME(r.database_id) AS db_name,
    SUBSTRING(t.text,
              (r.statement_start_offset/2)+1,
              ((CASE r.statement_end_offset WHEN -1 THEN DATALENGTH(t.text) ELSE r.statement_end_offset END - r.statement_start_offset)/2)+1
    ) AS running_statement
FROM sys.dm_exec_requests r
CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) t
WHERE r.blocking_session_id <> 0
   OR r.wait_type LIKE 'LCK%'
ORDER BY r.wait_time DESC;

/* B) Deadlocks del ring buffer XE */
;WITH rb AS (
    SELECT CAST(t.target_data AS XML) AS target_data
    FROM sys.dm_xe_sessions s
    JOIN sys.dm_xe_session_targets t
      ON s.address = t.event_session_address
    WHERE s.name = 'WMS_LockHealth'
      AND t.target_name = 'ring_buffer'
)
SELECT
    n.value('@timestamp','datetime2') AS utc_time,
    n.query('.') AS deadlock_xml
FROM rb
CROSS APPLY target_data.nodes('//RingBufferTarget/event[@name="xml_deadlock_report"]') AS q(n)
ORDER BY utc_time DESC;
