Imports System.Collections.Concurrent
Imports System.IO
Imports System.Threading
Imports System.Web

''' <summary>
''' WmsTraceWS — Trazabilidad del WebService TOMHHWS.asmx.
'''
''' Genera un TraceId por request en Global.asax.BeginRequest.
''' Enriquece TODOS los responses con header X-WMS-Trace-Id.
''' Escribe bitácora diaria de consumos y trazas de operación.
'''
''' NO requiere modificar ningún WebMethod individual.
''' ENABLED = False → ningún overhead (early return).
'''
''' Logs:
'''   C:\TOM\Logs\wms-ws-trace-YYYYMMDD.log   ← trace fino por TraceId
'''   C:\TOM\Logs\wms-ws-daily-YYYYMMDD.log   ← bitácora diaria de consumos
'''
''' #EJC20260528
''' </summary>
Public Module WmsTraceWS

    ''' <summary>True durante pruebas / observabilidad, False en producción.</summary>
    Public ENABLED As Boolean = False

    ''' <summary>
    ''' True siempre activa la bitácora diaria de consumos (liviana, producción-safe).
    ''' Independiente de ENABLED.
    ''' </summary>
    Public DAILY_LOG_ENABLED As Boolean = True

    Private ReadOnly LogDir As String = "C:\TOM\Logs"
    Private Const SLOW_WS_MS As Integer = 5000
    Private Const SLOW_SQL_MS As Integer = 3000

    ' ─── TraceId por thread (ThreadLocal para propagación a BOF) ─────────────
    ' Compartido con WmsTrace.vb BOF vía acceso directo al ThreadLocal
    <ThreadStatic>
    Public CurrentTraceId As String

    ' ─── Contadores de request activos por método ─────────────────────────────
    Private ReadOnly activeCalls As New ConcurrentDictionary(Of String, Long)()  ' traceId → startMs
    Private ReadOnly activeMethod As New ConcurrentDictionary(Of String, String)()  ' traceId → methodName

    ' ─── Estadísticas de la sesión ────────────────────────────────────────────
    Private ReadOnly methodStats As New ConcurrentDictionary(Of String, WsStats)()
    Private ReadOnly logLock As New Object()
    Private ReadOnly dailyLock As New Object()
    Private traceWriter As StreamWriter = Nothing
    Private dailyWriter As StreamWriter = Nothing
    Private traceDate As String = ""
    Private dailyDate As String = ""
    Private sessionLabel As String = "default"

    Private Structure WsStats
        Public callCount As Integer
        Public errorCount As Integer
        Public totalMs As Long
        Public maxMs As Long
    End Structure

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 1. ENTRY POINT — llamar en Global.asax.Application_BeginRequest
    '    Genera TraceId, guarda en HttpContext.Items Y en ThreadLocal.
    '    Escribe la línea de apertura del trace.
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub OnRequestBegin(ByVal context As HttpContext)
        If context Is Nothing Then Return

        ' Siempre registrar en bitácora diaria (liviana)
        Dim traceId As String = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}-{Guid.NewGuid().ToString("N").Substring(0, 8)}"
        context.Items("WmsTraceId") = traceId
        context.Items("WmsTraceStartMs") = NowMs()

        ' Propagar a ThreadLocal (para WmsTrace.vb BOF)
        CurrentTraceId = traceId

        If Not ENABLED Then Return

        Dim method As String = ExtractMethodName(context)
        activeMethod(traceId) = method
        activeCalls(traceId) = NowMs()

        TraceLog("WS", ">>", $"trace={traceId} meth={method} th={Thread.CurrentThread.ManagedThreadId} url={context.Request.RawUrl}")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 2. EXIT POINT — llamar en Global.asax.Application_EndRequest
    '    Calcula duración total, escribe línea de cierre, enriquece response header.
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub OnRequestEnd(ByVal context As HttpContext)
        If context Is Nothing Then Return

        Dim traceId As String = TryCast(context.Items("WmsTraceId"), String)
        If String.IsNullOrEmpty(traceId) Then Return

        Dim startMs As Long = 0
        Dim startObj As Object = context.Items("WmsTraceStartMs")
        If startObj IsNot Nothing Then startMs = CLng(startObj)
        Dim dt As Long = NowMs() - startMs

        ' ── Inyectar TraceId en TODOS los responses (trazabilidad HH↔WS) ──────
        ' La HH puede leer este header y loguearlo con WmsTrace.java
        Try
            context.Response.AppendHeader("X-WMS-Trace-Id", traceId)
            context.Response.AppendHeader("X-WMS-Duration-Ms", dt.ToString())
        Catch
            ' No fallar si el response ya fue enviado
        End Try

        Dim method As String = String.Empty
        activeMethod.TryGetValue(traceId, method)

        ' ── Bitácora diaria (siempre, independiente de ENABLED) ───────────────
        If DAILY_LOG_ENABLED Then
            Dim statusCode As Integer = 200
            Try : statusCode = context.Response.StatusCode : Catch : End Try
            Dim isError As Boolean = statusCode >= 400
            DailyLog($"trace={traceId} meth={method} dt={dt}ms status={statusCode}{If(isError, " [ERROR]", "")}{If(dt > SLOW_WS_MS, " [SLOW]", "")}")

            ' Actualizar estadísticas
            Dim stats As WsStats
            methodStats.TryGetValue(method, stats)
            stats.callCount += 1
            stats.totalMs += dt
            If dt > stats.maxMs Then stats.maxMs = dt
            If isError Then stats.errorCount += 1
            methodStats(method) = stats
        End If

        If Not ENABLED Then
            CurrentTraceId = Nothing
            Return
        End If

        ' ── Trace fino ─────────────────────────────────────────────────────────
        Dim statusTxt As String = "OK"
        Try
            If context.Response.StatusCode >= 400 Then statusTxt = "ERR"
        Catch : End Try

        Dim hint As String = If(dt > SLOW_WS_MS, " [!! SLOW_WS]", "")
        TraceLog("WS", $"<< {statusTxt}", $"trace={traceId} meth={method} dt={dt}ms{hint}")

        ' Limpieza
        activeCalls.TryRemove(traceId, Nothing)
        activeMethod.TryRemove(traceId, Nothing)
        CurrentTraceId = Nothing
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 3. SQL ROUNDTRIP desde el WS — para los métodos que hacen SQL directo
    '    (los que no delegan 100% al BOF DAL)
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub SqlStart(ByVal spOrQuery As String)
        If Not ENABLED Then Return
        Dim tid As String = If(String.IsNullOrEmpty(CurrentTraceId), "?", CurrentTraceId)
        TraceLog("SQL", ">>", $"trace={tid} sp={Truncate(spOrQuery, 80)}")
    End Sub

    Public Sub SqlEnd(ByVal spOrQuery As String, ByVal rows As Integer, ByVal dtMs As Long)
        If Not ENABLED Then Return
        Dim tid As String = If(String.IsNullOrEmpty(CurrentTraceId), "?", CurrentTraceId)
        Dim hint As String = If(dtMs > SLOW_SQL_MS, " [SLOW_SQL]", "")
        TraceLog("SQL", "<<", $"trace={tid} sp={Truncate(spOrQuery, 80)} rows={rows} dt={dtMs}ms{hint}")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 4. PUNTO DE INTERCEPCIÓN EN EscribirJsonHH* — patch a las 3 funciones
    '    Llamar al INICIO de cada EscribirJsonHH / EscribirJsonHHSeguro / EscribirJsonHHRaw
    '    para registrar el tamaño del payload y el status.
    '
    '    Patch (1 línea en cada método):
    '    WmsTraceWS.OnJsonResponse(pStatusCode, json.Length)   '#EJC20260528
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub OnJsonResponse(ByVal statusCode As Integer, ByVal jsonLen As Integer)
        If Not ENABLED Then Return
        Dim tid As String = If(String.IsNullOrEmpty(CurrentTraceId), "?", CurrentTraceId)
        Dim hint As String = If(statusCode >= 400, " [ERROR_RESPONSE]", "")
        TraceLog("RS", ">>", $"trace={tid} status={statusCode} len={jsonLen}b{hint}")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 5. DUMP DE ESTADÍSTICAS — llamar al apagar el AppPool o desde Admin
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub DumpStats()
        TraceLog("==", "STATS", "============================================================")
        TraceLog("==", "STATS", String.Format("{0,-50} {1,6} {2,8} {3,8} {4,6}",
                                               "método", "calls", "avgMs", "maxMs", "errs"))
        For Each kv In methodStats.OrderByDescending(Function(x) x.Value.maxMs)
            Dim s = kv.Value
            Dim avg As Long = If(s.callCount > 0, s.totalMs \ s.callCount, 0)
            Dim flag As String = If(s.maxMs > SLOW_WS_MS, " ← SLOW", "")
            TraceLog("==", "STATS", String.Format("{0,-50} {1,6} {2,8} {3,8} {4,6}{5}",
                                                   kv.Key, s.callCount, avg, s.maxMs, s.errorCount, flag))
        Next
        TraceLog("==", "STATS", "============================================================")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' 6. RESET — llamar al inicio de cada sesión de prueba
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub Reset(ByVal label As String)
        sessionLabel = label
        activeCalls.Clear()
        activeMethod.Clear()
        methodStats.Clear()
        CurrentTraceId = Nothing
        TraceLog("==", "RESET", $"sesión='{label}'")
    End Sub

    ' ─── Helpers privados ─────────────────────────────────────────────────────

    Private Function ExtractMethodName(ByVal ctx As HttpContext) As String
        Try
            Dim url As String = ctx.Request.RawUrl
            ' /TOMHHWS.asmx/Guardar_Recepcion_Json → "Guardar_Recepcion_Json"
            Dim idx As Integer = url.LastIndexOf("/")
            If idx >= 0 AndAlso idx < url.Length - 1 Then
                Dim name As String = url.Substring(idx + 1)
                Dim qi As Integer = name.IndexOf("?")
                Return If(qi >= 0, name.Substring(0, qi), name)
            End If
            Return url
        Catch
            Return "?"
        End Try
    End Function

    Private Function NowMs() As Long
        Return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
    End Function

    Private Function Truncate(ByVal s As String, ByVal max As Integer) As String
        If String.IsNullOrEmpty(s) Then Return ""
        Return If(s.Length <= max, s, s.Substring(0, max) & "…")
    End Function

    ''' <summary>Bitácora diaria liviana — siempre activa independiente de ENABLED.</summary>
    Private Sub DailyLog(ByVal msg As String)
        Dim line As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  {msg}"
        SyncLock dailyLock
            Try
                Dim today As String = DateTime.Now.ToString("yyyyMMdd")
                If dailyWriter Is Nothing OrElse today <> dailyDate Then
                    dailyWriter?.Flush() : dailyWriter?.Dispose()
                    If Not Directory.Exists(LogDir) Then Directory.CreateDirectory(LogDir)
                    dailyWriter = New StreamWriter(Path.Combine(LogDir, $"wms-ws-daily-{today}.log"), append:=True) With {.AutoFlush = False}
                    dailyDate = today
                End If
                dailyWriter.WriteLine(line)
            Catch : End Try
        End SyncLock
    End Sub

    ''' <summary>Trace fino — solo cuando ENABLED = True.</summary>
    Private Sub TraceLog(ByVal layer As String, ByVal tag As String, ByVal msg As String)
        Dim line As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  {layer,-3} {tag,-12} {msg}"
        SyncLock logLock
            Try
                Dim today As String = DateTime.Now.ToString("yyyyMMdd")
                If traceWriter Is Nothing OrElse today <> traceDate Then
                    traceWriter?.Flush() : traceWriter?.Dispose()
                    If Not Directory.Exists(LogDir) Then Directory.CreateDirectory(LogDir)
                    traceWriter = New StreamWriter(Path.Combine(LogDir, $"wms-ws-trace-{today}.log"), append:=True) With {.AutoFlush = False}
                    traceDate = today
                End If
                traceWriter.WriteLine(line)
            Catch : End Try
        End SyncLock
        System.Diagnostics.Debug.WriteLine(line)
    End Sub

    ''' <summary>Forzar flush — llamar en Application_End.</summary>
    Public Sub FlushAll()
        SyncLock logLock : traceWriter?.Flush() : End SyncLock
        SyncLock dailyLock : dailyWriter?.Flush() : End SyncLock
    End Sub

End Module
