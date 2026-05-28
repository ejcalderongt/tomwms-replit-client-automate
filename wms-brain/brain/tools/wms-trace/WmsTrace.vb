Imports System.Collections.Concurrent
Imports System.IO
Imports System.Threading

''' <summary>
''' WmsTrace — Trazabilidad no-invasiva para BOF WinForms y MI3 WCF.
'''
''' HABILITAR para pruebas:  WmsTrace.ENABLED = True
''' DESHABILITAR producción: WmsTrace.ENABLED = False  (el JIT elimina los If Not ENABLED)
'''
''' Output: C:\TOM\Logs\wms-trace-YYYYMMDD.log  (mismo formato parseable que HH)
''' Análisis: python3 parse_trace_bof.py C:\TOM\Logs\wms-trace-20260528.log
'''
''' Detecta:
'''   OP >> ...    — inicio de operación (LN layer)
'''   OP << OK/ERR — fin de operación con tiempo total
'''   TX BEGIN     — inicio de transacción SQL
'''   TX COMMIT    — commit con tiempo total
'''   TX ROLLBACK  — rollback con motivo
'''   SQL >>       — inicio de roundtrip SQL (SP / query)
'''   SQL <<       — fin de roundtrip SQL con filas y timing
'''   MI >>        — entrada WCF MI3 (boundary ERP→WMS)
'''   MI << OK/ERR — salida WCF MI3 con roundtrips SQL y timing total
'''   !! N+1       — mismo SP llamado >3x en 500ms desde el mismo contexto
'''   !! SLOW_TX   — transacción > 5,000ms
'''   !! SLOW_OP   — operación > 8,000ms
'''   !! TX_ORPHAN — transacción abierta > 30s sin commit/rollback
'''   === STATS    — resumen al final de sesión
'''
''' #EJC20260528
''' </summary>
Public Module WmsTrace

    ''' <summary>True durante pruebas, False en producción.</summary>
    Public ENABLED As Boolean = False

    ' ─── Configuración ────────────────────────────────────────────────────────
    Private ReadOnly LogDir As String = "C:\TOM\Logs"
    Private Const SLOW_OP_MS As Integer = 8000
    Private Const SLOW_TX_MS As Integer = 5000
    Private Const TX_ORPHAN_MS As Integer = 30000
    Private Const NPLUS1_THRESHOLD As Integer = 3
    Private Const NPLUS1_WINDOW_MS As Integer = 500

    ' ─── Estado en vuelo ──────────────────────────────────────────────────────
    ' Operaciones activas: key = contextId (Thread o opName), value = (opName, startMs)
    Private ReadOnly activeOps As New ConcurrentDictionary(Of String, OpState)
    ' Transacciones activas: key = contextId, value = startMs
    Private ReadOnly activeTx As New ConcurrentDictionary(Of String, TxState)
    ' SQL calls activos: key = contextId, value = (sp, startMs)
    Private ReadOnly activeSql As New ConcurrentDictionary(Of String, SqlState)
    ' MI3 calls activos: key = keyId
    Private ReadOnly activeMi As New ConcurrentDictionary(Of String, MiState)

    ' ─── Contadores por operación ─────────────────────────────────────────────
    ' key = opName, value = (count, totalMs, maxMs)
    Private ReadOnly opStats As New ConcurrentDictionary(Of String, OpStats)
    ' SQL roundtrips por contexto de operación activa (para N+1 y por-MI)
    Private ReadOnly sqlCountPerOp As New ConcurrentDictionary(Of String, Integer)
    ' N+1 detection: key = "context|spName", value = (callCount, firstCallMs)
    Private ReadOnly nplus1Map As New ConcurrentDictionary(Of String, NplusState)

    ' ─── Log buffer ───────────────────────────────────────────────────────────
    Private ReadOnly logLock As New Object
    Private logWriter As StreamWriter = Nothing
    Private logDate As String = ""

    ' ─── Estructuras internas ─────────────────────────────────────────────────
    Private Structure OpState
        Public opName As String
        Public startMs As Long
        Public threadName As String
    End Structure

    Private Structure TxState
        Public context As String
        Public startMs As Long
        Public threadName As String
    End Structure

    Private Structure SqlState
        Public spName As String
        Public opContext As String
        Public startMs As Long
    End Structure

    Private Structure MiState
        Public service As String
        Public method As String
        Public keyId As String
        Public startMs As Long
        Public sqlCount As Integer
    End Structure

    Private Structure OpStats
        Public count As Integer
        Public totalMs As Long
        Public maxMs As Long
        Public errCount As Integer
    End Structure

    Private Structure NplusState
        Public callCount As Integer
        Public firstMs As Long
        Public alerted As Boolean
    End Structure

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 1. OPERACIÓN LN — llamar al inicio/fin de métodos de lógica de negocio
    '    Uso: WmsTrace.OpStart("Procesar_Pedido_Compra_MI3", NoEnc)
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Sub OpStart(ByVal opName As String, Optional ByVal context As String = "")
        If Not ENABLED Then Return
        Dim ctxKey As String = ContextKey()
        Dim st As New OpState With {
            .opName = opName,
            .startMs = NowMs(),
            .threadName = Thread.CurrentThread.Name
        }
        activeOps(ctxKey) = st
        sqlCountPerOp(ctxKey) = 0
        Log("OP", ">>", $"op={opName} ctx={context} th={ctxKey}")
    End Sub

    Public Sub OpEnd(ByVal opName As String,
                     Optional ByVal success As Boolean = True,
                     Optional ByVal errMsg As String = "",
                     Optional ByVal context As String = "")
        If Not ENABLED Then Return
        Dim ctxKey As String = ContextKey()
        Dim st As OpState
        Dim dt As Long = -1
        If activeOps.TryRemove(ctxKey, st) Then
            dt = NowMs() - st.startMs
        End If
        Dim sqlCount As Integer = 0
        sqlCountPerOp.TryRemove(ctxKey, sqlCount)

        Dim hint As String = ""
        If dt > SLOW_OP_MS Then hint = " [!! SLOW_OP]"

        If success Then
            Log("OP", "<<", $"op={opName} OK dt={dt}ms sql_roundtrips={sqlCount} ctx={context}{hint}")
        Else
            Log("OP", "<<", $"op={opName} ERR dt={dt}ms sql_roundtrips={sqlCount} ctx={context} err={Truncate(errMsg, 120)}{hint}")
            If dt > SLOW_OP_MS Then
                Log("!!", "SLOW_OP", $"op={opName} dt={dt}ms → evaluar paginación, índices o SP batch")
            End If
        End If

        ' Actualizar estadísticas
        Dim stats As OpStats
        opStats.TryGetValue(opName, stats)
        stats.count += 1
        stats.totalMs += If(dt > 0, dt, 0)
        If dt > stats.maxMs Then stats.maxMs = dt
        If Not success Then stats.errCount += 1
        opStats(opName) = stats
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 2. TRANSACCIÓN — patch a clsTransaccion.vb (3 puntos)
    '    Llamar en Begin_Transaction, Commit_Transaction, RollBack_Transaction
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Sub TxBegin(Optional ByVal context As String = "")
        If Not ENABLED Then Return
        Dim ctxKey As String = ContextKey()
        activeTx(ctxKey) = New TxState With {
            .context = context,
            .startMs = NowMs(),
            .threadName = Thread.CurrentThread.Name
        }
        Log("TX", "BEGIN", $"ctx={context} th={ctxKey}")
    End Sub

    Public Sub TxCommit(Optional ByVal context As String = "")
        If Not ENABLED Then Return
        Dim ctxKey As String = ContextKey()
        Dim st As TxState
        Dim dt As Long = -1
        If activeTx.TryRemove(ctxKey, st) Then
            dt = NowMs() - st.startMs
        End If
        Dim hint As String = If(dt > SLOW_TX_MS, " [!! SLOW_TX]", "")
        Log("TX", "COMMIT", $"ctx={context} dt={dt}ms{hint}")
        If dt > SLOW_TX_MS Then
            Log("!!", "SLOW_TX", $"transacción {dt}ms → revisar SP dentro del bloque o deadlocks")
        End If
    End Sub

    Public Sub TxRollback(Optional ByVal context As String = "",
                          Optional ByVal reason As String = "")
        If Not ENABLED Then Return
        Dim ctxKey As String = ContextKey()
        Dim st As TxState
        Dim dt As Long = -1
        If activeTx.TryRemove(ctxKey, st) Then
            dt = NowMs() - st.startMs
        End If
        Log("TX", "ROLLBACK", $"ctx={context} dt={dt}ms reason={Truncate(reason, 120)}")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 3. SQL ROUNDTRIP — opcional, para granularidad fina en DAL crítico
    '    Usar en métodos DAL de alta frecuencia (insertar/actualizar recepción, etc.)
    '    WmsTrace.SqlStart("sp_Insert_Trans_Re_Det", "GuardarRecepcion")
    '    WmsTrace.SqlEnd("sp_Insert_Trans_Re_Det", cmd.ExecuteNonQuery(), "GuardarRecepcion")
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Sub SqlStart(ByVal spName As String, Optional ByVal opContext As String = "")
        If Not ENABLED Then Return
        Dim ctxKey As String = ContextKey()
        activeSql(ctxKey) = New SqlState With {
            .spName = spName,
            .opContext = opContext,
            .startMs = NowMs()
        }
        ' N+1 detection
        NplusOneCheck(spName, opContext)
        Log("SQL", ">>", $"sp={spName} ctx={opContext} th={ctxKey}")
    End Sub

    Public Sub SqlEnd(ByVal spName As String,
                      Optional ByVal rows As Integer = -1,
                      Optional ByVal opContext As String = "")
        If Not ENABLED Then Return
        Dim ctxKey As String = ContextKey()
        Dim st As SqlState
        Dim dt As Long = -1
        If activeSql.TryRemove(ctxKey, st) Then
            dt = NowMs() - st.startMs
        End If

        ' Incrementar contador de roundtrips de la operación activa
        Dim current As Integer = 0
        sqlCountPerOp.TryGetValue(ctxKey, current)
        sqlCountPerOp(ctxKey) = current + 1

        Dim hint As String = If(dt > 3000, " [SLOW_SQL]", "")
        Log("SQL", "<<", $"sp={spName} rows={rows} dt={dt}ms ctx={opContext}{hint}")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 4. MI3 WCF BOUNDARY — patch a PedidoCompra.svc.vb, PedidoCliente.svc.vb,
    '    TransaccionesOut.svc.vb. Llamar al inicio/fin de cada Public Function.
    '    WmsTrace.MiEntry("PedidoCompra", "Insert", BeINavPedCompraEnc.NoEnc)
    '    WmsTrace.MiExit("PedidoCompra", "Insert", NoEnc, True)
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Sub MiEntry(ByVal service As String,
                       ByVal method As String,
                       Optional ByVal keyId As String = "")
        If Not ENABLED Then Return
        Dim miKey As String = $"{service}.{method}.{keyId}"
        activeMi(miKey) = New MiState With {
            .service = service,
            .method = method,
            .keyId = keyId,
            .startMs = NowMs(),
            .sqlCount = 0
        }
        Log("MI", ">>", $"svc={service} meth={method} key={keyId} th={ContextKey()}")
    End Sub

    Public Sub MiExit(ByVal service As String,
                      ByVal method As String,
                      Optional ByVal keyId As String = "",
                      Optional ByVal success As Boolean = True,
                      Optional ByVal errMsg As String = "")
        If Not ENABLED Then Return
        Dim miKey As String = $"{service}.{method}.{keyId}"
        Dim st As MiState
        Dim dt As Long = -1
        If activeMi.TryRemove(miKey, st) Then
            dt = NowMs() - st.startMs
        End If
        Dim hint As String = If(dt > 5000, " [SLOW_MI]", "")
        If success Then
            Log("MI", $"<< OK", $"svc={service} meth={method} key={keyId} dt={dt}ms{hint}")
        Else
            Log("MI", "<< ERR", $"svc={service} meth={method} key={keyId} dt={dt}ms err={Truncate(errMsg, 120)}")
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 5. CAMBIO DE ESTADO CLAVE — opcional, granular (mismo concepto que HH)
    '    WmsTrace.StateChange("Resultado", "Validando datos", "Procesar OK")
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Sub StateChange(ByVal label As String,
                           ByVal before As Object,
                           ByVal after As Object)
        If Not ENABLED Then Return
        Log("ST", $"[{label}]", $"{before} -> {after} th={ContextKey()}")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 6. ORPHAN TX WATCHER — llamar en un timer de diagnóstico (ej: cada 60s)
    '    Para detectar transacciones que quedaron abiertas (bug clásico en BOF)
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Sub CheckOrphanTransactions()
        If Not ENABLED Then Return
        Dim now As Long = NowMs()
        For Each kv In activeTx
            Dim age As Long = now - kv.Value.startMs
            If age > TX_ORPHAN_MS Then
                Log("!!", "TX_ORPHAN", $"ctx={kv.Value.context} age={age}ms th={kv.Key} → transacción sin commit/rollback")
            End If
        Next
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 7. DUMP DE ESTADÍSTICAS — llamar al cerrar el formulario o desde menú debug
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Sub DumpStats()
        If Not ENABLED Then Return
        Log("==", "STATS", "============================================================")
        Log("==", "STATS", String.Format("{0,-45} {1,6} {2,8} {3,8} {4,6}",
                                         "operación", "count", "avgMs", "maxMs", "errCnt"))
        For Each kv In opStats.OrderByDescending(Function(x) x.Value.maxMs)
            Dim s = kv.Value
            Dim avg As Long = If(s.count > 0, s.totalMs \ s.count, 0)
            Dim flag As String = If(s.maxMs > SLOW_OP_MS, " ← SLOW", "")
            Log("==", "STATS", String.Format("{0,-45} {1,6} {2,8} {3,8} {4,6}{5}",
                                             kv.Key, s.count, avg, s.maxMs, s.errCount, flag))
        Next
        Log("==", "STATS", $"activeTx={activeTx.Count} activeOps={activeOps.Count}")
        Log("==", "STATS", "============================================================")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 8. RESET — llamar al inicio de cada sesión de prueba
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Sub Reset(ByVal sessionLabel As String)
        activeOps.Clear()
        activeTx.Clear()
        activeSql.Clear()
        activeMi.Clear()
        opStats.Clear()
        sqlCountPerOp.Clear()
        nplus1Map.Clear()
        Log("==", "RESET", $"sesión='{sessionLabel}'")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' INTERNOS
    ' ═══════════════════════════════════════════════════════════════════════════

    Private Sub NplusOneCheck(ByVal spName As String, ByVal opContext As String)
        Dim mapKey As String = $"{opContext}|{spName}"
        Dim now As Long = NowMs()
        Dim st As NplusState
        nplus1Map.TryGetValue(mapKey, st)

        ' Reset ventana si pasó NPLUS1_WINDOW_MS
        If now - st.firstMs > NPLUS1_WINDOW_MS Then
            st.callCount = 0
            st.firstMs = now
            st.alerted = False
        End If

        st.callCount += 1
        nplus1Map(mapKey) = st

        If st.callCount > NPLUS1_THRESHOLD AndAlso Not st.alerted Then
            st.alerted = True
            nplus1Map(mapKey) = st
            Log("!!", "N+1", $"sp={spName} ctx={opContext} llamado {st.callCount}x en {NPLUS1_WINDOW_MS}ms → revisar loop con llamada SQL individual")
        End If
    End Sub

    Private Function ContextKey() As String
        Dim t = Thread.CurrentThread
        Return If(String.IsNullOrEmpty(t.Name), $"th{t.ManagedThreadId}", t.Name)
    End Function

    Private Function NowMs() As Long
        Return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
    End Function

    Private Function Truncate(ByVal s As String, ByVal max As Integer) As String
        If String.IsNullOrEmpty(s) Then Return "null"
        Return If(s.Length <= max, s, s.Substring(0, max) & "…")
    End Function

    ''' <summary>Escribe una línea al log rolling diario. Thread-safe.</summary>
    Private Sub Log(ByVal layer As String, ByVal tag As String, ByVal msg As String)
        Dim line As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  {layer,-3} {tag,-12} {msg}"
        SyncLock logLock
            Try
                Dim today As String = DateTime.Now.ToString("yyyyMMdd")
                If logWriter Is Nothing OrElse today <> logDate Then
                    logWriter?.Flush()
                    logWriter?.Dispose()
                    Dim dir As String = LogDir
                    If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
                    logWriter = New StreamWriter(Path.Combine(dir, $"wms-trace-{today}.log"), append:=True) With {
                        .AutoFlush = False
                    }
                    logDate = today
                End If
                logWriter.WriteLine(line)
            Catch
                ' Never fail production code because of tracing
            End Try
        End SyncLock
        Debug.WriteLine(line)   ' también visible en VS Output window
    End Sub

    ''' <summary>Forzar flush del buffer — llamar en puntos de checkpoint o cierre.</summary>
    Public Sub FlushLog()
        If Not ENABLED Then Return
        SyncLock logLock
            logWriter?.Flush()
        End SyncLock
    End Sub

End Module
