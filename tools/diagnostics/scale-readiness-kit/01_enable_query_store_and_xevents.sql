/* #EJC20260526
   Setup inicial de observabilidad para carga concurrente.
*/

SET NOCOUNT ON;

DECLARE @db SYSNAME = DB_NAME();
DECLARE @sql NVARCHAR(MAX);

/* 1) Query Store */
IF EXISTS (SELECT 1 FROM sys.database_query_store_options WHERE actual_state_desc = 'OFF')
BEGIN
    SET @sql = N'ALTER DATABASE [' + @db + N'] SET QUERY_STORE = ON;';
    EXEC(@sql);
END

SET @sql = N'
ALTER DATABASE [' + @db + N']
SET QUERY_STORE (
    OPERATION_MODE = READ_WRITE,
    QUERY_CAPTURE_MODE = AUTO,
    INTERVAL_LENGTH_MINUTES = 5,
    MAX_STORAGE_SIZE_MB = 2048,
    CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 15),
    SIZE_BASED_CLEANUP_MODE = AUTO
);';
EXEC(@sql);

/* 2) Extended Events: deadlocks + bloqueos largos */
IF EXISTS (SELECT 1 FROM sys.server_event_sessions WHERE name = 'WMS_LockHealth')
BEGIN
    DROP EVENT SESSION [WMS_LockHealth] ON SERVER;
END
GO
CREATE EVENT SESSION [WMS_LockHealth] ON SERVER
ADD EVENT sqlserver.xml_deadlock_report,
ADD EVENT sqlserver.blocked_process_report
ADD TARGET package0.ring_buffer
WITH (
    MAX_MEMORY = 20 MB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 5 SECONDS,
    STARTUP_STATE = ON
);
GO

/* blocked process threshold: 10 segundos */
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'blocked process threshold (s)', 10;
RECONFIGURE;
GO

ALTER EVENT SESSION [WMS_LockHealth] ON SERVER STATE = START;
GO
