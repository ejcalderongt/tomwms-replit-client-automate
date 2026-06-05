' Global.asax.vb — WSHHRN/TOMHHWS Web Application
' Agregar al proyecto WSHHRN como Global.asax + Global.asax.vb
' Este archivo no existía antes — es totalmente nuevo, sin cambios a código existente.
' #EJC20260528

Imports System.Web

Public Class GlobalApplication
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Activar bitácora diaria siempre (liviana, producción-safe)
        WmsTraceWS.DAILY_LOG_ENABLED = True

        ' #EJC20260529 trace fino habilitado temporalmente para diagnóstico bug packing La Cumbre
        ' (excl_fp / excl_vd / excl_sr en wms-ws-trace y wms-bof-trace)
        ' Volver a False cuando concluya el diagnóstico.
        WmsTraceWS.ENABLED = True
        WmsTraceWS.Reset("diag-packing-cumbre-20260529")
    End Sub

    Sub Application_End(sender As Object, e As EventArgs)
        WmsTraceWS.DumpStats()
        WmsTraceWS.FlushAll()
    End Sub

    ' ══════════════════════════════════════════════════════════════════════════
    ' PUNTO DE INTERCEPCIÓN GLOBAL — cubre el 100% de requests sin tocar
    ' ningún WebMethod individual.
    ' ══════════════════════════════════════════════════════════════════════════

    Sub Application_BeginRequest(sender As Object, e As EventArgs)
        ' Genera TraceId, lo guarda en HttpContext.Items y en CurrentTraceId (ThreadLocal)
        ' para que WmsTrace.vb (BOF) pueda leerlo en la misma thread.
        WmsTraceWS.OnRequestBegin(HttpContext.Current)
    End Sub

    Sub Application_EndRequest(sender As Object, e As EventArgs)
        ' Calcula duración, escribe log, inyecta X-WMS-Trace-Id en response header.
        WmsTraceWS.OnRequestEnd(HttpContext.Current)
    End Sub

    Sub Application_Error(sender As Object, e As EventArgs)
        Dim ex As Exception = Server.GetLastError()
        If ex Is Nothing Then Return

        Dim traceId As String = TryCast(HttpContext.Current?.Items("WmsTraceId"), String)
        ' El error ya quedará en el log de EndRequest con status 500
        ' Aquí podemos enriquecer con el mensaje de excepción
        System.Diagnostics.Debug.WriteLine(
            $"[WMS-ERROR] trace={traceId} err={ex.Message?.Substring(0, Math.Min(200, ex.Message.Length))}")
    End Sub

End Class
