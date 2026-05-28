---
name: Observability v2 — Estado de implementación
description: Qué está deployed en ADO vs qué está pendiente. Fechas exactas y commits.
---

# WmsTrace v2 — Estado de Implementación (al 2026-05-28)

## ✅ Deployed en ADO `dev_2028_merge`

### Archivo 1: `TOMIMSV4/DAL/General/Servidor/WmsTrace.vb`
**Commit:** `e7f9016199`  
**Estado:** REEMPLAZA v1. Módulo BOF con modelo OTel-inspired completo.

Capacidades en producción:
- `BeginSpan(name, parentId)` / `EndSpan(spanId, status, durationMs)` — árbol padre-hijo
- `SpanId + ParentSpanId` vía `ThreadLocal(Of Stack(Of String))` — safe en async/multi-thread
- WMS Semantic Conventions: `wms.operation`, `wms.oc_id`, `wms.recepcion_id`, `db.statement`, `db.rows_affected`, etc.
- `TxBegin(label)` / `TxCommit(spanId, dt)` / `TxRollback(spanId, dt)` — trace automático de TX
- N+1 detection inline (≥4 SPs idénticos consecutivos → log `N+1`)
- SLOW_SQL detection (>500ms → log `SLOW_SQL`)
- Toggle producción-safe: `WmsTrace.ENABLED` (default=False)

---

### Archivo 2: `WSHHRN/WmsTraceWS.vb`
**Commit:** `96afca437e`  
**Estado:** NUEVO archivo en proyecto WSHHRN.

Capacidades:
- `OnRequestBegin(context)` — genera TraceId W3C, guarda en `HttpContext.Items` + ThreadStatic
- `OnJsonResponse(statusCode, length)` — cierra el span del request con duración y status
- `OnRequestEnd(context)` — cleanup
- W3C `traceparent: 00-{32hex}-{16hex}-01` devuelto en response header
- `X-WMS-Trace-Id` header para que HH pueda loguearlo
- `ENABLED = True` → bitácora de trace completa (`wms-ws-trace-YYYYMMDD.log`)
- `DAILY_LOG_ENABLED = True` → bitácora diaria SIEMPRE (producción-safe, `wms-ws-daily-YYYYMMDD.log`)

---

### Archivo 3: `WSHHRN/Global.asax` + `WSHHRN/Global.asax.vb`
**Commit:** `96afca437e`  
**Estado:** NUEVO. Interceptor ASP.NET pipeline — cubre 100% de requests sin tocar WebMethods.

```vb
' Global.asax.vb — los 3 hooks:
Sub Application_BeginRequest  → WmsTraceWS.OnRequestBegin(Context)
Sub Application_EndRequest    → WmsTraceWS.OnRequestEnd(Context)
Sub Application_Error         → log de excepción no capturada con trace=...
```

**Nota deploy:** En el servidor hay que agregar `Global.asax` al proyecto WSHHRN.vbproj si no se auto-descubre. IIS lo detecta automáticamente en el root.

---

### Archivo 4: `WSHHRN/TOMHHWS.asmx.vb` — 3 patches quirúrgicos
**Commit:** `7bf707387a`  
**Estado:** PATCH aplicado sobre 20,516 líneas. Los 3 únicos writers JSON.

```vb
' EscribirJsonHH     (L≈130) — antes del Response.Write(json):
WmsTraceWS.OnJsonResponse(pStatusCode, json.Length) '#EJC20260528

' EscribirJsonHHSeguro (L≈152) — antes del Response.Write(json):
WmsTraceWS.OnJsonResponse(pStatusCode, json.Length) '#EJC20260528

' EscribirJsonHHRaw  (L≈168) — antes del Response.Write(json):
WmsTraceWS.OnJsonResponse(pStatusCode, json.Length) '#EJC20260528
```

**Cobertura:** 100% de responses JSON al HH pasan por `OnJsonResponse`.

---

### Archivo 5: `TOMIMSV4/DAL/General/clsTransaccion.vb`
**Commit:** `aa50b031`  
**Estado:** PATCH sobre clase base de transacciones SQL del BOF. 122→132 líneas.

```vb
' Campos privados agregados (2 líneas):
Private _traceSpanId As String = ""
Private _txStartMs As Long = 0

' Begin_Transaction() — 1 línea agregada:
_traceSpanId = WmsTrace.TxBegin() : _txStartMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()

' Begin_Transaction_Async() — 1 línea agregada:
_traceSpanId = WmsTrace.TxBegin("Async") : _txStartMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()

' Commit_Transaction() — 1 línea agregada:
WmsTrace.TxCommit(_traceSpanId, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _txStartMs)

' RollBack_Transaction() — 1 línea agregada:
WmsTrace.TxRollback(_traceSpanId, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _txStartMs)
```

**Impacto:** TODO el BOF que use `clsTransaccion` (recepción, picking, despacho, ajuste, traslado, etc.) queda trazado automáticamente sin tocar más archivos.

---

## ✅ Deployed en GitHub brain `wms-brain`

| Archivo | Commit | Descripción |
|---|---|---|
| `tools/wms-trace/WmsTrace_v2.vb` | `204fe867` | Código fuente de referencia v2 |
| `tools/wms-trace/WmsTraceWS_v2.vb` | `74914246` | Código fuente de referencia v2 |
| `tools/wms-trace/GlobalAsax.vb` | `ddcafcfe` | Interceptor reference |
| `tools/wms-trace/collect_trace.py` | `d9909acb` | Colector v2 + span tree builder |
| `skills/wms-observability/WMSTRACE-V2-DESIGN.md` | `f90622ec` | Arquitectura completa |
| `skills/wms-observability/OBSERVABILITY-DESIGN.md` | `32d841d5` | Arquitectura v1 + YAML schema |

---

## ⏳ Pendiente (HH Android — `TOMHH2025`)

**Leer `traceparent` del response en `WmsTrace.java`:**
```java
// En callEnd() del OkHttp interceptor:
String traceparent = response.header("traceparent");
if (traceparent != null) {
    Log.d("WMS-T", "traceparent=" + traceparent);
}
String traceId = response.header("X-WMS-Trace-Id");
if (traceId != null) {
    Log.d("WMS-T", "X-WMS-Trace-Id=" + traceId);
}
```
**Impacto:** Permite correlacionar logs HH con los logs WS/BOF por TraceId exacto (en vez de aproximación temporal).

---

## Cómo activar para prueba

```batch
REM 1. En el servidor (EC2), editar Web.config del WSHHRN:
REM    <add key="WmsTraceEnabled" value="true"/>
REM    (o modificar WmsTraceWS.ENABLED = True directamente antes de compilar)

REM 2. Compilar y publicar WSHHRN
REM 3. Compilar y publicar TOMIMSV4
REM 4. Ejecutar operación desde HH

REM 5. Recolectar logs:
adb logcat -v time -s WMS-T:* > hh.log
scp ec2:C:\TOM\Logs\wms-ws-trace-*.log .
scp ec2:C:\TOM\Logs\wms-bof-trace-*.log .

REM 6. Generar YAML para brain:
python3 brain/tools/wms-trace/collect_trace.py \
  --hh-log hh.log \
  --ws-log wms-ws-trace-*.log \
  --bof-log wms-bof-trace-*.log \
  --out brain/_inbox/
```
