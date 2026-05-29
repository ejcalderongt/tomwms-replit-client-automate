Imports System.Collections.Generic
Imports System.IO
Imports System.Threading
Imports System.Runtime.CompilerServices

''' <summary>
''' WmsTrace v2 — Modelo OTel-inspired para BOF/MI3/LN.
'''
''' Adopta del estándar OpenTelemetry:
'''   - Span model (SpanId + ParentSpanId + name + attributes)
'''   - WMS Semantic Conventions (wms.* + db.* + http.*)
'''   - Árbol de Spans (jerarquía padre-hijo por stack thread-local)
'''
''' Cero dependencias externas. El brain es el "backend".
'''
''' Formato de log enriquecido:
'''   YYYY-MM-DD HH:mm:ss.fff  LAYER  TAG        span=XXXX parent=YYYY trace=ZZZZ [attrs] [note]
'''
''' #EJC20260528
''' </summary>
Public Module WmsTrace

    ' ═══════════════════════════════════════════════════════════════════════════
    ' CONFIGURACIÓN
    ' ═══════════════════════════════════════════════════════════════════════════

    ''' <summary>True durante pruebas y observabilidad. False en producción.</summary>
    Public ENABLED As Boolean = False

    ''' <summary>Umbral de SQL lenta (ms). Default 3000ms.</summary>
    Public SLOW_SQL_MS As Long = 3000

    ''' <summary>Umbral de TX lenta (ms). Default 8000ms.</summary>
    Public SLOW_TX_MS As Long = 8000

    ''' <summary>Umbral de N+1: mismo SP > N veces en la misma TX.</summary>
    Public N1_THRESHOLD As Integer = 3

    Private ReadOnly LogDir As String = "C:\TOM\Logs"
    Private ReadOnly logLock As New Object()
    Private traceWriter As StreamWriter = Nothing
    Private traceDate As String = ""

    ' ═══════════════════════════════════════════════════════════════════════════
    ' CONTEXTO POR THREAD — Span stack + TraceId actual
    ' ═══════════════════════════════════════════════════════════════════════════

    ' TraceId global de la operación (recibido de WmsTraceWS o generado local)
    <ThreadStatic>
    Public CurrentTraceId As String

    ' Stack de SpanIds activos en este thread (LIFO: peek = span actual)
    <ThreadStatic>
    Private _spanStack As Stack(Of String)
    Private ReadOnly Property SpanStack() As Stack(Of String)
        Get
            If _spanStack Is Nothing Then _spanStack = New Stack(Of String)()
            Return _spanStack
        End Get
    End Property

    ' Conteo de SPs en la TX actual (detección N+1)
    <ThreadStatic>
    Private _spCount As Dictionary(Of String, Integer)
    Private ReadOnly Property SpCount() As Dictionary(Of String, Integer)
        Get
            If _spCount Is Nothing Then _spCount = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
            Return _spCount
        End Get
    End Property

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 1. SPAN MODEL — Begin/End para cualquier operación lógica
    ' ═══════════════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Abre un nuevo Span. Devuelve el SpanId para pasarlo a EndSpan.
    ''' El ParentSpanId se toma automáticamente del stack del thread.
    '''
    ''' Uso:
    '''   Dim sid = WmsTrace.BeginSpan("LN.Procesar_Recepcion_HH", WmsTrace.A("wms.operation","recepcion"))
    '''   ... código ...
    '''   WmsTrace.EndSpan(sid, "OK", dtMs)
    ''' </summary>
    Public Function BeginSpan(ByVal name As String,
                              Optional ByVal attrs As Dictionary(Of String, Object) = Nothing) As String
        If Not ENABLED Then Return ""
        Dim sid As String = NewSpanId()
        Dim parent As String = If(SpanStack.Count > 0, SpanStack.Peek(), "")
        SpanStack.Push(sid)
        Dim attrStr As String = AttrsToStr(attrs)
        TraceLog("SPAN", ">>", $"span={sid} parent={parent} trace={SafeTid()} name={name}{attrStr}")
        Return sid
    End Function

    ''' <summary>Cierra el Span abierto con BeginSpan. Siempre llamar en Finally.</summary>
    Public Sub EndSpan(ByVal spanId As String, ByVal status As String, ByVal dtMs As Long,
                       Optional ByVal attrs As Dictionary(Of String, Object) = Nothing)
        If Not ENABLED OrElse String.IsNullOrEmpty(spanId) Then Return
        If SpanStack.Count > 0 AndAlso SpanStack.Peek() = spanId Then
            SpanStack.Pop()
        End If
        Dim flag As String = If(status = "ERROR", " [ERROR]", "")
        Dim attrStr As String = AttrsToStr(attrs)
        TraceLog("SPAN", "<<", $"span={spanId} status={status} dt={dtMs}ms{flag}{attrStr}")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 2. OPERACIONES BOF/LN — OpStart / OpEnd
    ' ═══════════════════════════════════════════════════════════════════════════

    ''' <summary>Apertura de una operación LN (un método de negocio completo).</summary>
    Public Function OpStart(ByVal opName As String,
                            Optional ByVal attrs As Dictionary(Of String, Object) = Nothing) As String
        If Not ENABLED Then Return ""
        Return BeginSpan($"LN.{opName}", Merge(A("wms.layer", "BOF"), attrs))
    End Function

    ''' <summary>Cierre de operación LN.</summary>
    Public Sub OpEnd(ByVal spanId As String, ByVal sqlRoundtrips As Integer,
                     ByVal ok As Boolean, ByVal dtMs As Long)
        If Not ENABLED OrElse String.IsNullOrEmpty(spanId) Then Return
        EndSpan(spanId, If(ok, "OK", "ERROR"),
                dtMs, A("db.roundtrips", sqlRoundtrips))
        If sqlRoundtrips > 10 Then
            TraceLog("!!", "HIGH_SQL",
                     $"span={spanId} trace={SafeTid()} sql_roundtrips={sqlRoundtrips} [!! HIGH_SQL_COUNT]")
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 3. TRANSACCIONES SQL
    ' ═══════════════════════════════════════════════════════════════════════════

    ''' <summary>Apertura de bloque transaccional. Devuelve spanId para cerrar.</summary>
    Public Function TxBegin(Optional ByVal note As String = "") As String
        If Not ENABLED Then Return ""
        SpCount.Clear()   ' resetear contadores N+1 para esta TX
        Dim sid = BeginSpan("TX", A("db.system", "mssql"))
        TraceLog("TX", "BEGIN", $"span={sid} trace={SafeTid()}{If(note <> "", " note=" & note, "")}")
        Return sid
    End Function

    ''' <summary>Commit exitoso de la TX.</summary>
    Public Sub TxCommit(ByVal spanId As String, ByVal dtMs As Long)
        If Not ENABLED OrElse String.IsNullOrEmpty(spanId) Then Return
        Dim hint As String = If(dtMs > SLOW_TX_MS, " [!! SLOW_TX]", "")
        TraceLog("TX", "COMMIT", $"span={spanId} trace={SafeTid()} dt={dtMs}ms{hint}")
        EndSpan(spanId, "OK", dtMs)
        SpCount.Clear()
    End Sub

    ''' <summary>Rollback de la TX.</summary>
    Public Sub TxRollback(ByVal spanId As String, ByVal dtMs As Long,
                          Optional ByVal reason As String = "")
        If Not ENABLED OrElse String.IsNullOrEmpty(spanId) Then Return
        TraceLog("TX", "ROLLBACK",
                 $"span={spanId} trace={SafeTid()} dt={dtMs}ms reason={Truncate(reason, 120)} [!! TX_ROLLBACK]")
        EndSpan(spanId, "ERROR", dtMs, A("error.message", reason))
        SpCount.Clear()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 4. SQL — SqlStart / SqlEnd (OTel db.* conventions)
    ' ═══════════════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Inicio de un SqlCommand (SP o query inline).
    ''' Devuelve el spanId + el tiempo de inicio para calcular duración.
    ''' </summary>
    Public Function SqlStart(ByVal spOrQuery As String,
                             Optional ByVal attrs As Dictionary(Of String, Object) = Nothing) As (sid As String, startMs As Long)
        If Not ENABLED Then Return ("", 0)
        Dim sid = BeginSpan($"SQL.{ExtractSpName(spOrQuery)}",
                            Merge(A("db.system", "mssql",
                                   "db.statement", Truncate(spOrQuery, 100)),
                                  attrs))
        Return (sid, NowMs())
    End Function

    ''' <summary>Fin de un SqlCommand.</summary>
    Public Sub SqlEnd(ByVal spanId As String, ByVal startMs As Long,
                      ByVal rows As Integer, ByVal spName As String,
                      Optional ByVal error As String = "")
        If Not ENABLED OrElse String.IsNullOrEmpty(spanId) Then Return
        Dim dtMs As Long = NowMs() - startMs
        Dim ok As Boolean = String.IsNullOrEmpty(error)

        ' N+1 detection: contar en la TX activa
        Dim key As String = ExtractSpName(spName)
        Dim cnt As Integer = 0
        SpCount.TryGetValue(key, cnt)
        cnt += 1
        SpCount(key) = cnt
        Dim n1Flag As String = If(cnt > N1_THRESHOLD, $" [!! N+1 cnt={cnt}]", "")

        ' Slow SQL
        Dim slowFlag As String = If(dtMs > SLOW_SQL_MS, " [!! SLOW_SQL]", "")

        EndSpan(spanId, If(ok, "OK", "ERROR"), dtMs,
                Merge(A("db.rows_affected", rows), If(ok, Nothing, A("error.message", Truncate(error, 100)))))

        If n1Flag <> "" OrElse slowFlag <> "" Then
            TraceLog("!!", $"{If(n1Flag <> "", "N+1", "")}{If(slowFlag <> "", "SLOW", "")}",
                     $"span={spanId} trace={SafeTid()} sp={key} dt={dtMs}ms rows={rows}{n1Flag}{slowFlag}")
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 5. MI3 WCF — MiEntry / MiExit
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Function MiEntry(ByVal service As String, ByVal method As String,
                            Optional ByVal attrs As Dictionary(Of String, Object) = Nothing) As String
        If Not ENABLED Then Return ""
        Return BeginSpan($"MI3.{service}.{method}",
                         Merge(A("wms.layer", "MI3"), attrs))
    End Function

    Public Sub MiExit(ByVal spanId As String, ByVal ok As Boolean,
                      ByVal dtMs As Long, Optional ByVal error As String = "")
        If Not ENABLED OrElse String.IsNullOrEmpty(spanId) Then Return
        EndSpan(spanId, If(ok, "OK", "ERROR"), dtMs,
                If(ok, Nothing, A("error.message", Truncate(error, 100))))
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 6. EVENTO PUNTUAL — para notas dentro de un span existente
    ' ═══════════════════════════════════════════════════════════════════════════

    ''' <summary>Registra un evento puntual (sin duración) dentro del span activo.</summary>
    Public Sub Event(ByVal eventName As String,
                     Optional ByVal attrs As Dictionary(Of String, Object) = Nothing)
        If Not ENABLED Then Return
        Dim sid As String = If(SpanStack.Count > 0, SpanStack.Peek(), "?")
        TraceLog("EVT", eventName, $"span={sid} trace={SafeTid()}{AttrsToStr(attrs)}")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 7. RESET — iniciar nueva sesión de prueba
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Sub Reset(ByVal label As String)
        CurrentTraceId = NewTraceId()
        If _spanStack IsNot Nothing Then _spanStack.Clear()
        If _spCount IsNot Nothing Then _spCount.Clear()
        TraceLog("==", "RESET", $"trace={CurrentTraceId} sesión='{label}'")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 8. ATTRIBUTE BUILDER — helpers para construir attr dicts (fluent)
    ' ═══════════════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Helper para construir dicts de atributos inline.
    ''' Uso: WmsTrace.A("wms.operation","recepcion", "wms.case",16)
    ''' </summary>
    Public Function A(ParamArray ByVal kv() As Object) As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
        Dim i As Integer = 0
        While i + 1 < kv.Length
            If kv(i) IsNot Nothing Then d(kv(i).ToString()) = kv(i + 1)
            i += 2
        End While
        Return d
    End Function

    Private Function Merge(ParamArray ByVal dicts() As Dictionary(Of String, Object)) As Dictionary(Of String, Object)
        Dim result As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
        For Each d In dicts
            If d Is Nothing Then Continue For
            For Each kv In d : result(kv.Key) = kv.Value : Next
        Next
        Return If(result.Count = 0, Nothing, result)
    End Function

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Internals — generadores, formateo, log
    ' ═══════════════════════════════════════════════════════════════════════════

    Public Function NewTraceId() As String
        Return Guid.NewGuid().ToString("N").ToLowerInvariant()  ' 32 hex chars
    End Function

    Public Function NewSpanId() As String
        Return Guid.NewGuid().ToString("N").Substring(0, 8).ToLowerInvariant()  ' 8 hex chars
    End Function

    Private Function SafeTid() As String
        Return If(String.IsNullOrEmpty(CurrentTraceId), "?", CurrentTraceId)
    End Function

    Private Function NowMs() As Long
        Return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
    End Function

    Private Function Truncate(ByVal s As String, ByVal max As Integer) As String
        If String.IsNullOrEmpty(s) Then Return ""
        Return If(s.Length <= max, s, s.Substring(0, max) & "…")
    End Function

    Private Function ExtractSpName(ByVal sp As String) As String
        If String.IsNullOrEmpty(sp) Then Return "?"
        ' "EXEC sp_Insert_Trans_Re_Det @p1, @p2" → "sp_Insert_Trans_Re_Det"
        Dim parts = sp.Trim().Split({" "c}, StringSplitOptions.RemoveEmptyEntries)
        Return If(parts.Length > 0,
                  parts(If(parts(0).ToUpper() = "EXEC" AndAlso parts.Length > 1, 1, 0)),
                  sp)
    End Function

    Private Function AttrsToStr(ByVal attrs As Dictionary(Of String, Object)) As String
        If attrs Is Nothing OrElse attrs.Count = 0 Then Return ""
        Return " " & String.Join(" ", attrs.Select(Function(kv) $"{kv.Key}={kv.Value}"))
    End Function

    Private Sub TraceLog(ByVal layer As String, ByVal tag As String, ByVal msg As String)
        Dim line As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  {layer,-4} {tag,-12} {msg}"
        SyncLock logLock
            Try
                Dim today As String = DateTime.Now.ToString("yyyyMMdd")
                If traceWriter Is Nothing OrElse today <> traceDate Then
                    traceWriter?.Flush() : traceWriter?.Dispose()
                    If Not IO.Directory.Exists(LogDir) Then IO.Directory.CreateDirectory(LogDir)
                    traceWriter = New StreamWriter(Path.Combine(LogDir, $"wms-bof-trace-{today}.log"),
                                                  append:=True) With {.AutoFlush = False}
                    traceDate = today
                End If
                traceWriter.WriteLine(line)
            Catch : End Try
        End SyncLock
        System.Diagnostics.Debug.WriteLine(line)
    End Sub

End Module
