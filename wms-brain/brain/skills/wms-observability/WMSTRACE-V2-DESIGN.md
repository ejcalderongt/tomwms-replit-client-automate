# WmsTrace v2 — Modelo OTel-inspired, cero dependencias externas

**Referencia:** OpenTelemetry Specification v1.34 (CNCF)  
**Fecha:** 2026-05-28 | **EJC**

---

## Qué adoptamos de OTel y por qué

OTel define 5 conceptos que son universalmente correctos, independientes del backend:

| Concepto OTel | Qué es | Lo que adoptamos |
|---|---|---|
| **Span** | Unidad de trabajo con inicio, fin, atributos | `WmsSpan` — cada operación, SQL, TX, etc. |
| **Trace** | Árbol de Spans ligados por TraceId | Mismo TraceId → árbol padre-hijo |
| **SpanId + ParentSpanId** | Identidad y jerarquía de cada span | `span=XXXX parent=YYYY` en log |
| **Semantic Conventions** | Nombres estándar para atributos | `wms.*`, `db.*`, `http.*` |
| **W3C traceparent** | Header estándar de propagación | `traceparent: 00-{traceId}-{spanId}-01` |

Lo que **NO** adoptamos: OTLP wire format (proto), otel-collector, Jaeger, Prometheus.  
Lo que SÍ adoptamos: el **modelo de datos** → nuestro brain es el "backend".

---

## La diferencia crítica: árbol vs lista plana

### WmsTrace v1 (lo que tenemos)
```
trace=abc  WS >> Guardar_Recepcion_Json dt=4768ms
trace=abc  OP >> Procesar_Recepcion_HH
trace=abc  TX    BEGIN
trace=abc  SQL >> sp_Insert_Trans_Re_Enc dt=45ms
trace=abc  SQL >> sp_Insert_Trans_Re_Det dt=38ms
trace=abc  SQL >> sp_Insert_Trans_Re_Det dt=41ms   ← ¿es N+1 o era necesario?
trace=abc  SQL << sp_Update_Stock dt=520ms         ← lento, pero ¿dentro del TX o fuera?
```
→ Lista plana. El brain tiene que inferir causalidad por timestamps.

### WmsTrace v2 (árbol de Spans)
```
Span[root]  trace=abc span=0001 parent=     name=WS.Guardar_Recepcion_Json  0→4768ms
Span[A]     trace=abc span=0002 parent=0001 name=LN.Procesar_Recepcion_HH  100→4600ms
Span[A.1]   trace=abc span=0003 parent=0002 name=TX.BLOCK                  110→4400ms
Span[A.1.1] trace=abc span=0004 parent=0003 name=SQL.sp_Insert_Re_Enc       110→155ms  rows=1
Span[A.1.2] trace=abc span=0005 parent=0003 name=SQL.sp_Insert_Re_Det       160→198ms  rows=1
Span[A.1.3] trace=abc span=0006 parent=0003 name=SQL.sp_Insert_Re_Det       200→241ms  rows=1  ← 2do det
Span[A.1.4] trace=abc span=0007 parent=0003 name=SQL.sp_Update_Stock_Rec    245→765ms  rows=3  ← slow, DENTRO del TX
Span[A.2]   trace=abc span=0008 parent=0002 name=LN.Notificar_SAP_HANA    4400→4580ms
```
→ El brain ve INMEDIATAMENTE: `sp_Update_Stock_Rec` fue el cuello de botella, y estaba DENTRO del TX bloqueando 520ms.

---

## WMS Semantic Conventions (nuestro "OTel spec")

Reglas de nomenclatura para atributos. Una vez definidas, NUNCA cambian.
El brain las conoce y puede razonar sobre ellas sin heurísticas frágiles.

```
# Capa y contexto
wms.layer          = "HH" | "WS" | "BOF" | "DB" | "MI3"
wms.operation      = "recepcion" | "picking" | "despacho" | "ajuste" | "traslado" | "inventario" | "ubicacion"
wms.case           = <int>           HH case number
wms.bodega_id      = <int>
wms.operador_id    = <int>
wms.empresa_id     = <int>
wms.client         = "KILLIOS" | "BECOFARMA" | ...

# Entidades de negocio (la "reverse map" automática)
wms.oc_id          = <int>           IdOrdenCompraEnc
wms.recepcion_id   = <int>           IdRecepcionEnc
wms.pedido_id      = <int>           IdPedidoEnc
wms.lp             = <string>        LicensePlate
wms.producto_id    = <int>
wms.ubicacion_id   = <int>

# Base de datos (OTel db.* conventions)
db.system          = "mssql"
db.statement       = <sp o query truncado a 100 chars>
db.rows_affected   = <int>
db.tx_id           = <int o string>  identificador del bloque TX

# HTTP (OTel http.* conventions)
http.method        = "POST" | "GET"
http.url           = "/TOMHHWS.asmx/Guardar_Recepcion_Json"
http.status_code   = <int>
http.response_size = <int>  bytes

# Errores (OTel error.* conventions)
error.type         = "SQLException" | "TimeoutException" | "BusinessRuleError"
error.message      = <string truncado a 200 chars>
error.sp           = <nombre del SP si aplica>
```

---

## W3C traceparent — propagación estándar

Formato: `traceparent: 00-{32hex}-{16hex}-01`

```
traceparent: 00-a3f8b1c2d4e54f6789012345abcdef01-a3f8b1c24567890a-01
               ^^                                 ^^^^^^^^^^^^^^^^^^^^
               version (siempre 00)               span-id del WS root
                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                 trace-id (nuestro TraceId extendido a 32 hex)
```

**Ventaja:** si en el futuro instalas Jaeger (gratis, Docker), ya entiende estos headers sin cambiar código.

En HH (Android): leer de response headers → loguear en WmsTrace.java.
En WSHHRN: leer de request headers (si vienen de HH futura) o GENERAR si no vienen.
En BOF: heredar de WS vía ThreadLocal.

---

## Cambios concretos por capa

### 1. WmsTrace.vb (BOF) — agregar SpanId + ParentSpanId + Attributes

```vb
' Stack de SpanIds en el thread actual (para parent tracking)
<ThreadStatic>
Private spanStack As Stack(Of String)

Public Function BeginSpan(name As String, attrs As Dictionary(Of String, Object)) As String
    Dim sid = NewSpanId()
    Dim parent = If(spanStack?.Count > 0, spanStack.Peek(), "")
    If spanStack Is Nothing Then spanStack = New Stack(Of String)()
    spanStack.Push(sid)
    TraceLog($"SPAN>> name={name} span={sid} parent={parent} {AttrsToStr(attrs)}")
    Return sid
End Function

Public Sub EndSpan(sid As String, status As String, dtMs As Long)
    If spanStack?.Count > 0 Then spanStack.Pop()
    TraceLog($"SPAN<< span={sid} status={status} dt={dtMs}ms")
End Sub
```

### 2. WmsTraceWS.vb — generar/parsear traceparent + SpanId root

```vb
Public Sub OnRequestBegin(context As HttpContext)
    ' Parsear traceparent entrante (futuro: HH envía header)
    Dim incoming = context.Request.Headers("traceparent")
    Dim traceId As String, spanId As String
    If Not String.IsNullOrEmpty(incoming) Then
        ParseTraceParent(incoming, traceId, spanId)
    Else
        traceId = NewTraceId()
        spanId = NewSpanId()
    End If
    context.Items("WmsTraceId") = traceId
    context.Items("WmsRootSpanId") = spanId
    CurrentTraceId = traceId
    CurrentSpanId = spanId
End Sub

Public Sub OnRequestEnd(context As HttpContext)
    ' Devolver traceparent para que HH pueda correlacionar
    Dim tid = TryCast(context.Items("WmsTraceId"), String)
    Dim sid = TryCast(context.Items("WmsRootSpanId"), String)
    context.Response.AppendHeader("traceparent", $"00-{Pad32(tid)}-{sid}-01")
End Sub
```

### 3. WmsTrace.java (HH) — leer traceparent de response headers

```java
// En callEnd (ya existe):
String traceparent = response.header("traceparent");
if (traceparent != null && ENABLED) {
    Log.d(TAG, "traceparent=" + traceparent
               + " case=" + currentCase);
}
```

### 4. collect_trace.py — reconstruir árbol de Spans

```python
# Parsear span=XXXX parent=YYYY de los logs
# Construir dict: spanId → {children: [...], ...}
# Exportar como árbol anidado en el YAML
def build_span_tree(spans):
    by_id = {s['span_id']: s for s in spans}
    roots = []
    for s in spans:
        pid = s.get('parent_span_id', '')
        if pid and pid in by_id:
            by_id[pid].setdefault('children', []).append(s)
        else:
            roots.append(s)
    return roots
```

---

## La "salida OTLP-compatible" (nuestro formato nativo)

Generada por collect_trace.py. El brain puede consumirla directamente.
Si en el futuro se quiere Jaeger: un script de 50 líneas la convierte a OTLP JSON.

```yaml
trace_id: "a3f8b1c2d4e54f6789012345abcdef01"
spans:
  - span_id: "0001"
    parent_span_id: ""
    name: "WS.Guardar_Recepcion_Json"
    start_ms: 0
    end_ms: 4768
    status: OK
    attributes:
      http.method: POST
      http.status_code: 200
      wms.layer: WS
      wms.operation: recepcion
      wms.case: 16
      wms.bodega_id: 1
    children:
      - span_id: "0002"
        parent_span_id: "0001"
        name: "LN.Procesar_Recepcion_HH"
        start_ms: 100
        end_ms: 4600
        status: OK
        attributes:
          wms.layer: BOF
          db.rows_affected: 5
        children:
          - span_id: "0003"
            parent_span_id: "0002"
            name: "TX"
            start_ms: 110
            end_ms: 4400
            children:
              - span_id: "0004"
                name: "SQL.sp_Insert_Trans_Re_Enc"
                start_ms: 110
                end_ms: 155
                attributes:
                  db.system: mssql
                  db.statement: sp_Insert_Trans_Re_Enc
                  db.rows_affected: 1
                  wms.recepcion_id: 5821
              - span_id: "0005"
                name: "SQL.sp_Insert_Trans_Re_Det"
                start_ms: 160
                end_ms: 198
                attributes:
                  db.rows_affected: 1
```

---

## Roadmap de activación (sin impacto en producción)

| Fase | Qué | Impacto prod |
|---|---|---|
| **v1 (ya)** | TraceId + log plano + DAILY_LOG | ninguno |
| **v2a** | SpanId + ParentSpanId en BOF | ninguno (solo enriquece log) |
| **v2b** | traceparent header WS→HH | HH solo lo loguea, no lo usa |
| **v2c** | Semantic attributes WMS en spans clave | ninguno |
| **v2d** | collect_trace.py con árbol de spans | solo en análisis offline |
| **v3** | Brain usa árbol para auto-diagnóstico | ninguno |
| **v4 (futuro)** | Exporter OTLP → Jaeger Docker | opcional, infra propia |
