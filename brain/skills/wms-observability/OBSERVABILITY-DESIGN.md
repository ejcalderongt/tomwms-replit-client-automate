---
tipo: other
autores: [erik]
---
# WMS Observability — Modelo de Trazabilidad Distribuida

**Autor:** EJC  
**Fecha:** 2026-05-28  
**Estado:** Diseño v1 — implementación en curso

---

## 1. La visión: grafo de máquina de estados finitos

Cada operación WMS (recepción, picking, despacho, ajuste) es una **corrida de una FSM**:

```
[HH escanea] → [WS.Guardar_Recepcion_Json] → [LN.Procesar_Recepcion]
    → [TX.BEGIN] → [SQL×N inserts/updates] → [TX.COMMIT]
    → [HH.case=39 recarga detalle] → [END]
```

Con un `TraceId` que atraviesa todas las capas, podemos:
- **Reconstruir** el grafo completo post-ejecución
- **Pinear** datos ↔ código ↔ tiempo: "el insert a `trans_re_det` ocurrió 340ms después del BEGIN"
- **Responder automáticamente**: si algo falló → buscar en estas tablas con estos IDs
- **Optimizar sin romper**: si algo fue lento → estas N SQL calls se pueden batchar

---

## 2. Arquitectura del sistema

```
┌─────────────────────────────────────────────────────────────────────────┐
│  HH Android                                                             │
│  WmsTrace.java  →  adb logcat tag=WMS-T                                 │
│  trace_id propagado vía header X-WMS-Trace-Id en response del WS       │
└────────────────────────────┬────────────────────────────────────────────┘
                             │ HTTP/SOAP call
                             ▼
┌─────────────────────────────────────────────────────────────────────────┐
│  WebService (WSHHRN/TOMHHWS.asmx)                                       │
│  Global.asax.vb  →  genera TraceId en BeginRequest                      │
│  WmsTrace_WS.vb  →  registra entrada, latencia, SP count, errors        │
│  EscribirJsonHH* →  inyecta X-WMS-Trace-Id en response header           │
│  Output: C:\TOM\Logs\wms-ws-trace-YYYYMMDD.log                          │
│  Bitácora diaria: C:\TOM\Logs\wms-ws-daily-YYYYMMDD.log                 │
└────────────────────────────┬────────────────────────────────────────────┘
                             │ TraceId propagado via ThreadLocal
                             ▼
┌─────────────────────────────────────────────────────────────────────────┐
│  BOF / LN / clsTransaccion                                              │
│  WmsTrace.vb    →  OpStart/OpEnd, TxBegin/Commit, SqlStart/SqlEnd       │
│  TraceId tomado de WmsTrace_WS.CurrentTraceId (ThreadLocal)             │
│  Output: C:\TOM\Logs\wms-bof-trace-YYYYMMDD.log                         │
└────────────────────────────┬────────────────────────────────────────────┘
                             │ SqlCommand.Connection
                             ▼
┌─────────────────────────────────────────────────────────────────────────┐
│  SQL Server                                                              │
│  CONTEXT_INFO  →  TraceId embebido en la sesión SQL                     │
│  Query Store   →  latencia y plan por SP                                │
│  sp_GetTrace*  →  (opcional) Extended Events session                    │
└─────────────────────────────────────────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────────────┐
│  COLLECTOR  (collect_trace.py)                                           │
│  Input:  logcat + WS log + BOF log + (DB si habilitado)                 │
│  Key:    TraceId (correlación exacta)                                   │
│  Output: traces/session-{TraceId}.yml   ← COMPUTABLE POR BRAIN         │
│          traces/analysis-{TraceId}.md   ← LEGIBLE POR HUMANO           │
└─────────────────────────────────────────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────────────┐
│  BRAIN (_inbox/{TraceId}.json)                                           │
│  → Detección de anomalías (N+1, race, timeout, orphan TX)               │
│  → Mapa datos↔código: qué tablas tocó, qué SPs, qué entidades           │
│  → Correlación con tickets abiertos (Jira/WikiHub)                      │
│  → Propuesta de fix o optimización con contexto exacto                  │
└─────────────────────────────────────────────────────────────────────────┘
```

---

## 3. Esquema del artefacto YAML de salida

```yaml
# traces/session-{trace_id}.yml
# Generado por: collect_trace.py
# Consumible por: brain, scripts de análisis, CI/CD diagnosis

trace_id: "a3f8b1c2-d4e5-4f67-8901-abcdef012345"
timestamp_start: "2026-05-28T14:23:01.123Z"
timestamp_end:   "2026-05-28T14:23:05.891Z"
duration_ms: 4768
status: "OK"  # OK | ERROR | TIMEOUT | PARTIAL

# Contexto operativo
operation:
  name: "Guardar_Recepcion"
  source: "HH.frm_recepcion_datos"
  case: 16
  client: "KILLIOS"
  user: "operador_001"
  warehouse: "BODEGA_01"
  key: "OC-20260528-001"  # contexto de negocio (OC, LP, pedido, etc.)

# Flujo por capa
layers:
  hh:
    duration_ms: 4780
    events:
      - { t: 0,    type: "EXEC",     case: 16, thread: "OkHttp Dispatcher" }
      - { t: 4780, type: "CALLBACK", case: 16, status: "OK" }

  webservice:
    method: "Guardar_Recepcion_Json"
    url: "/TOMHHWS.asmx/Guardar_Recepcion_Json"
    duration_ms: 4700
    sql_count: 12
    response_bytes: 2048
    status_code: 200
    events:
      - { t: 0,    type: "REQUEST_IN",  method: "Guardar_Recepcion_Json" }
      - { t: 4700, type: "RESPONSE_OUT", status: 200, bytes: 2048 }

  bof:
    operations:
      - name: "Procesar_Recepcion_HH"
        duration_ms: 4500
        sql_roundtrips: 12
        status: OK
    transactions:
      - { begin_t: 100, commit_t: 4400, duration_ms: 4300 }
    sql_calls:
      - { sp: "sp_Insert_Trans_Re_Enc",   duration_ms: 45,  rows: 1 }
      - { sp: "sp_Insert_Trans_Re_Det",   duration_ms: 38,  rows: 1 }
      - { sp: "sp_Insert_Trans_Re_Det",   duration_ms: 41,  rows: 1 }
      - { sp: "sp_Update_Stock_Rec",      duration_ms: 520, rows: 3 }

# Datos tocados (reverse map: llamadas → tablas)
data_touched:
  tables:
    - { name: trans_re_enc,  operation: INSERT, rows: 1 }
    - { name: trans_re_det,  operation: INSERT, rows: 4 }
    - { name: stock_rec,     operation: UPDATE, rows: 3 }
  key_ids:
    IdRecepcionEnc: 5821
    IdOrdenCompraEnc: 1047
    LicPlate: "LP-20260528-001"

# Anomalías detectadas
anomalies: []
# Si hubiera anomalías:
# - type: N+1
#   sp: sp_Get_BeProducto
#   count: 5
#   window_ms: 320
#   recommendation: "Consolidar en SELECT...WHERE IdProducto IN (...)"

# Ruta de estado (FSM path)
state_path:
  - "HH.EXEC(case=16)"
  - "WS.Guardar_Recepcion_Json"
  - "LN.Procesar_Recepcion_HH"
  - "TX.BEGIN"
  - "SQL.sp_Insert_Trans_Re_Enc"
  - "SQL.sp_Insert_Trans_Re_Det ×4"
  - "SQL.sp_Update_Stock_Rec"
  - "TX.COMMIT"
  - "WS.OK(200)"
  - "HH.CALLBACK(case=16,OK)"

# Metadatos del brain
brain:
  analyzed: false
  support_tickets: []
  suggested_improvements: []
```

---

## 4. Flujo de activación para prueba completa

```bash
# 1. Activar en la HH (en onCreate de frm_recepcion_datos):
WmsTrace.ENABLED = true
WmsTrace.reset("recepcion-completa-killios-20260528")

# 2. Activar en el WS (en Global.asax o config):
WmsTraceWS.ENABLED = True
WmsTraceWS.Reset("recepcion-completa-killios-20260528")

# 3. Activar en BOF (en Form_Load o App.config):
WmsTrace.ENABLED = True
WmsTrace.Reset("recepcion-completa-killios-20260528")

# 4. EJECUTAR EL PROCESO COMPLETO EN LA HH

# 5. Recolectar logs:
adb pull /sdcard/...  # o vía logcat directo
scp ec2@52.41.114.122:"C:\TOM\Logs\wms-ws-trace-20260528.log" .
scp ec2@52.41.114.122:"C:\TOM\Logs\wms-bof-trace-20260528.log" .

# 6. Generar artefacto:
python3 collect_trace.py \
  --hh-log trace_hh_20260528.log \
  --ws-log wms-ws-trace-20260528.log \
  --bof-log wms-bof-trace-20260528.log \
  --out traces/

# 7. El brain analiza automáticamente al detectar el nuevo archivo en traces/
```

---

## 5. Comparativa de los 3 WmsTrace

| Aspecto | HH (Android) | WebService (ASMX) | BOF/MI3 (WinForms/WCF) |
|---|---|---|---|
| Clase | `WmsTrace.java` | `WmsTraceWS.vb` | `WmsTrace.vb` |
| Output | adb logcat (WMS-T) | Rolling file (ws-trace-*.log) | Rolling file (bof-trace-*.log) |
| Interceptor | `wsExecute`/`wsCallBack` | `Global.asax` (pipeline) | `clsTransaccion` + LN |
| TraceId | Correlación por tiempo | **GENERA** el TraceId | Recibe de WS vía ThreadLocal |
| Cobertura | 1 form × 42 cases | 100% requests sin tocar WebMethods | TX + Ops + SQL por operación |
| Patch invasivo | 3 líneas | 0 (Global.asax nuevo) | 3 (clsTransaccion) + LNs |

---

## 6. Estado de implementación

| Componente | Archivo | Estado |
|---|---|---|
| HH trace | `com.dts.base.WmsTrace.java` | ✅ Implementado (push #2313) |
| HH parser | `parse_trace.py` | ✅ Implementado |
| BOF trace | `WmsTrace.vb` en DAL | ✅ Diseñado, pendiente push ADO |
| BOF parser | `parse_trace_bof.py` | ✅ Implementado |
| **WS trace** | `WmsTraceWS.vb` + `Global.asax.vb` | 🔄 En diseño (esta sesión) |
| **Collector** | `collect_trace.py` | 🔄 En diseño (esta sesión) |
| **YAML schema** | `trace_schema.yml` | ✅ Definido arriba |
| Brain analyzer | `analyze_trace.py` | 🔲 Roadmap |
| DB tracing | `sp_WmsTrace_Context.sql` | 🔲 Roadmap |
