Imports System.Collections.Concurrent
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web

''' <summary>
''' WmsTraceWS v2 — WebService ASMX interceptor con modelo OTel-inspired.
'''
''' Adopta:
'''   - W3C traceparent header (00-{32hex}-{16hex}-01)
'''   - SpanId root generado aquí, heredado por BOF via ThreadLocal
'''   - WMS Semantic Conventions en response headers
'''   - Bitácora diaria producción-safe (DAILY_LOG siempre activa)
'''
''' #EJC20260528
''' </summary>
Public Module WmsTraceWS

    Public ENABLED As Boolean = False
    Public DAILY_LOG_ENABLED As Boolean = True

    Private ReadOnly LogDir As String = "C:\TOM\Logs"
    Private Const SLOW_WS_MS As Integer = 5000

    ' ── W3C traceparent regex ─────────────────────────────────────────────────
    ' traceparent: 00-{32hex}-{16hex}-{8bits}
    Private ReadOnly RxTraceparent As New Regex(
        "^00-(?<tid>[0-9a-f]{32})-(?<sid>[0-9a-f]{16})-[0-9a-f]{2}$",
        RegexOptions.Compiled Or RegexOptions.IgnoreCase)

    ' ── ThreadStatic — propagación al BOF en la misma thread ─────────────────
    <ThreadStatic>
    Public CurrentTraceId As String

    <ThreadStatic>
    Public CurrentSpanId As String

    ' ── Rolling writers ───────────────────────────────────────────────────────
    Private ReadOnly methodStats As New ConcurrentDictionary(Of String, WsStats)()
    Private ReadOnly logLock As New Object()
    Private ReadOnly dailyLock As New Object()
    Private traceWriter As StreamWriter = Nothing
    Private dailyWriter As StreamWriter = Nothing
    Private traceDate As String = ""
    Private dailyDate As String = ""

    Private Structure WsStats
        Public calls As Integer
        Public errors As Integer
        Public totalMs As Long
        Public maxMs As Long
    End Structure

    ' ═══════════════════════════════════════════════════════════════════════════
    ' BeginRequest — intercepta ANTES de que entre a cualquier WebMethod
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub OnRequestBegin(ByVal ctx As HttpContext)
        If ctx Is Nothing Then Return

        Dim traceId As String, spanId As String
        Dim incoming As String = ctx.Request.Headers("traceparent")

        If Not String.IsNullOrEmpty(incoming) Then
            ' HH ya mandó un traceparent → continuar la traza
            Dim m As Match = RxTraceparent.Match(incoming.Trim())
            If m.Success Then
                traceId = m.Groups("tid").Value
                spanId  = NewSpanId()  ' nuevo span hijo del span de la HH
            Else
                traceId = NewTraceId()
                spanId  = NewSpanId()
            End If
        Else
            ' Generar nueva traza completa
            traceId = NewTraceId()
            spanId  = NewSpanId()
        End If

        ctx.Items("WmsTraceId")      = traceId
        ctx.Items("WmsRootSpanId")   = spanId
        ctx.Items("WmsTraceStartMs") = NowMs()

        ' Propagar al BOF (misma thread)
        CurrentTraceId = traceId
        CurrentSpanId  = spanId

        ' También propagar a WmsTrace.vb (BOF) si está importado
        WmsTrace.CurrentTraceId = traceId

        If Not ENABLED Then Return

        Dim method As String = ExtractMethod(ctx)
        ctx.Items("WmsMethod") = method
        TraceLog("WS", ">>",
                 $"trace={traceId} span={spanId} parent= name=WS.{method}" &
                 $" http.method={ctx.Request.HttpMethod}" &
                 $" http.url={Truncate(ctx.Request.RawUrl, 100)}" &
                 $" wms.layer=WS")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' EndRequest — después de que el WebMethod escribió el response
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub OnRequestEnd(ByVal ctx As HttpContext)
        If ctx Is Nothing Then Return

        Dim traceId  As String = TryCast(ctx.Items("WmsTraceId"), String)
        Dim spanId   As String = TryCast(ctx.Items("WmsRootSpanId"), String)
        If String.IsNullOrEmpty(traceId) Then Return

        Dim startMs As Long = 0
        Dim so As Object = ctx.Items("WmsTraceStartMs")
        If so IsNot Nothing Then startMs = CLng(so)
        Dim dtMs As Long = NowMs() - startMs

        Dim method As String = TryCast(ctx.Items("WmsMethod"), String) : If method Is Nothing Then method = "?"
        Dim statusCode As Integer = 200
        Try : statusCode = ctx.Response.StatusCode : Catch : End Try
        Dim isError As Boolean = statusCode >= 400
        Dim isSlow  As Boolean = dtMs > SLOW_WS_MS

        ' ── Inyectar W3C traceparent en response ──────────────────────────────
        ' La HH puede leer este header y loguearlo con WmsTrace.java
        Try
            ' traceparent: 00-{32hex traceId}-{16hex spanId}-01
            Dim tp As String = $"00-{traceId.PadRight(32, "0"c).Substring(0, 32)}" &
                               $"-{spanId.PadRight(16, "0"c).Substring(0, 16)}-01"
            ctx.Response.AppendHeader("traceparent", tp)
            ctx.Response.AppendHeader("X-WMS-Trace-Id", traceId)
            ctx.Response.AppendHeader("X-WMS-Duration-Ms", dtMs.ToString())
        Catch : End Try

        ' ── Bitácora diaria (siempre, producción-safe) ────────────────────────
        If DAILY_LOG_ENABLED Then
            DailyLog($"trace={traceId} span={spanId} meth={method} dt={dtMs}ms" &
                     $" status={statusCode}{If(isError, " [ERROR]", "")}{If(isSlow, " [SLOW]", "")}")

            Dim stats As WsStats
            methodStats.TryGetValue(method, stats)
            stats.calls += 1 : stats.totalMs += dtMs
            If dtMs > stats.maxMs Then stats.maxMs = dtMs
            If isError Then stats.errors += 1
            methodStats(method) = stats
        End If

        If Not ENABLED Then
            CurrentTraceId = Nothing : CurrentSpanId = Nothing
            Return
        End If

        ' ── Trace fino: cierre del span raíz ─────────────────────────────────
        TraceLog("WS", "<<",
                 $"trace={traceId} span={spanId} name=WS.{method}" &
                 $" dt={dtMs}ms http.status_code={statusCode}" &
                 $" status={If(isError, "ERROR", "OK")}" &
                 $"{If(isSlow, " [!! SLOW_WS]", "")}{If(isError, " [!! ERROR]", "")}")

        CurrentTraceId = Nothing : CurrentSpanId = Nothing
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Patch point: EscribirJsonHH* — 1 línea en cada uno de los 3 writers
    ' Registra tamaño y status del payload justo antes de escribirlo.
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub OnJsonResponse(ByVal statusCode As Integer, ByVal jsonLen As Integer)
        If Not ENABLED Then Return
        Dim tid As String = If(String.IsNullOrEmpty(CurrentTraceId), "?", CurrentTraceId)
        Dim sid As String = If(String.IsNullOrEmpty(CurrentSpanId), "?", CurrentSpanId)
        TraceLog("RS", "payload",
                 $"trace={tid} span={sid} http.status_code={statusCode}" &
                 $" http.response_size={jsonLen}" &
                 $"{If(statusCode >= 400, " [!! ERROR_RESPONSE]", "")}")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Stats dump y control
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub DumpStats()
        TraceLog("==", "STATS", "─────────────────────────────────────────────")
        TraceLog("==", "STATS", String.Format("{0,-50} {1,6} {2,8} {3,8} {4,6}",
                                               "método", "calls", "avgMs", "maxMs", "errs"))
        For Each kv In methodStats.OrderByDescending(Function(x) x.Value.maxMs)
            Dim s = kv.Value
            Dim avg = If(s.calls > 0, s.totalMs \ s.calls, 0L)
            Dim flag = If(s.maxMs > SLOW_WS_MS, " ← SLOW", "")
            TraceLog("==", "STATS", String.Format("{0,-50} {1,6} {2,8} {3,8} {4,6}{5}",
                                                   kv.Key, s.calls, avg, s.maxMs, s.errors, flag))
        Next
        TraceLog("==", "STATS", "─────────────────────────────────────────────")
    End Sub

    Public Sub Reset(ByVal label As String)
        methodStats.Clear()
        CurrentTraceId = Nothing : CurrentSpanId = Nothing
        TraceLog("==", "RESET", $"sesión='{label}'")
    End Sub

    Public Sub FlushAll()
        SyncLock logLock : traceWriter?.Flush() : End SyncLock
        SyncLock dailyLock : dailyWriter?.Flush() : End SyncLock
    End Sub

    ' ─── Helpers ──────────────────────────────────────────────────────────────
    Private Function NewTraceId() As String
        Return Guid.NewGuid().ToString("N").ToLowerInvariant()
    End Function

    Private Function NewSpanId() As String
        Return Guid.NewGuid().ToString("N").Substring(0, 16).ToLowerInvariant()
    End Function

    Private Function NowMs() As Long
        Return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
    End Function

    Private Function Truncate(ByVal s As String, ByVal max As Integer) As String
        If String.IsNullOrEmpty(s) Then Return ""
        Return If(s.Length <= max, s, s.Substring(0, max) & "…")
    End Function

    Private Function ExtractMethod(ByVal ctx As HttpContext) As String
        Try
            Dim url = ctx.Request.RawUrl
            Dim idx = url.LastIndexOf("/")
            If idx >= 0 AndAlso idx < url.Length - 1 Then
                Dim name = url.Substring(idx + 1)
                Dim qi = name.IndexOf("?")
                Return If(qi >= 0, name.Substring(0, qi), name)
            End If
            Return url
        Catch : Return "?" : End Try
    End Function

    Private Sub DailyLog(ByVal msg As String)
        SyncLock dailyLock
            Try
                Dim today = DateTime.Now.ToString("yyyyMMdd")
                If dailyWriter Is Nothing OrElse today <> dailyDate Then
                    dailyWriter?.Flush() : dailyWriter?.Dispose()
                    If Not Directory.Exists(LogDir) Then Directory.CreateDirectory(LogDir)
                    dailyWriter = New StreamWriter(
                        Path.Combine(LogDir, $"wms-ws-daily-{today}.log"), append:=True) With {.AutoFlush = False}
                    dailyDate = today
                End If
                dailyWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  {msg}")
            Catch : End Try
        End SyncLock
    End Sub

    Private Sub TraceLog(ByVal layer As String, ByVal tag As String, ByVal msg As String)
        Dim line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  {layer,-4} {tag,-12} {msg}"
        SyncLock logLock
            Try
                Dim today = DateTime.Now.ToString("yyyyMMdd")
                If traceWriter Is Nothing OrElse today <> traceDate Then
                    traceWriter?.Flush() : traceWriter?.Dispose()
                    If Not Directory.Exists(LogDir) Then Directory.CreateDirectory(LogDir)
                    traceWriter = New StreamWriter(
                        Path.Combine(LogDir, $"wms-ws-trace-{today}.log"), append:=True) With {.AutoFlush = False}
                    traceDate = today
                End If
                traceWriter.WriteLine(line)
            Catch : End Try
        End SyncLock
        System.Diagnostics.Debug.WriteLine(line)
    End Sub

End Module
