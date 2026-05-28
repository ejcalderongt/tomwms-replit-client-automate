---
tipo: other
clientes: [killios]
autores: [erik]
---
# WmsTrace — Guía de uso para pruebas HH

**Commit de implementación:** `#EJC20260528` push #2313  
**Archivos nuevos/modificados:** `WmsTrace.java` (nuevo), `WebService.java`, `frm_recepcion_datos.java`

---

## 1. Activar el trace para una sesión de prueba

En `onCreate()` de `frm_recepcion_datos` (o desde un botón debug en `PBase`):

```java
WmsTrace.ENABLED = true;
WmsTrace.reset("recepcion-cajaMaster-killios-20260528");
```

Desactivar al terminar (o en `onDestroy`):

```java
WmsTrace.dumpStats();
WmsTrace.ENABLED = false;
```

**IMPORTANTE:** `ENABLED = false` hace que el JIT elimine todo el overhead. No hay penalización en producción.

---

## 2. Recolectar el trace

```bash
# Opción A: solo líneas WmsTrace (recomendado para análisis)
adb logcat -v time -s WMS-T:* > trace_$(date +%Y%m%d_%H%M).log

# Opción B: todo el logcat (para correlacionar con otros errores)
adb logcat -v time > trace_full_$(date +%Y%m%d_%H%M).log
```

---

## 3. Analizar el trace

```bash
python3 wms-brain/brain/tools/wms-trace/parse_trace.py trace_20260528_1430.log
```

El script produce:
- **Timeline** ordenada de todas las llamadas WS
- **Anomalías** detectadas automáticamente (races, reloads, timeouts)
- **Estadísticas** por caso: count, avg ms, max ms, min ms
- **Tiempos SOAP** por método (qué WS method es el más lento)
- **Recomendaciones** automáticas

---

## 4. Qué detecta el trace automáticamente

| Tag en logcat | Qué significa | Acción |
|---|---|---|
| `!! OVERLAP` | `case=N` inició mientras `case=M` estaba en vuelo | Race condition — revisar si hay guard `isOperationInProgress` |
| `!! RELOAD` | Mismo `case` relanzado sin completar, gap < 500ms | Posible double-tap, listener duplicado o lógica de retry agresiva |
| `<< ERR dt=Xms [POSIBLE TIMEOUT]` | Error con dt > 10,000ms | WS tardó demasiado — revisar timeout de conexión o carga del server |
| `<< ERR dt=Xms [error rápido/param]` | Error con dt < 200ms | Parámetro inválido enviado al WS — revisar validación antes del call |
| `<< OK dt=Xms [LENTO]` | Éxito pero dt > 5,000ms | Operación lenta en HH o server — candidato a optimización |
| `<< OK dt=Xms [muy rápido?]` | dt < 10ms | Sospechoso — ¿no hizo llamada real? ¿caché? |

---

## 5. Instrumentación opcional granular (bajo demanda)

Agregar en código existente cuando se quiera rastrear un campo o array específico:

```java
// Cambio de campo clave
WmsTrace.state("pLineaOC", oldVal, newVal);

// Mutación de array
WmsTrace.arrayMut("pListBeStockRec", "add", pListBeStockRec.items.size(), BeStock_rec);
WmsTrace.arrayMut("AuxListTransReDet", "add", AuxListTransReDet.items.size(), Auxredet);

// Handler.postDelayed (para detectar si dispara durante operación en vuelo)
WmsTrace.handlerPost("disableFab", 1500);
```

Estas líneas se pueden agregar y quitar sin riesgo — si `ENABLED = false` no ejecutan nada.

---

## 6. Mapa de casos críticos para recepción (frm_recepcion_datos.java)

| case | Método WS | Descripción |
|---|---|---|
| 1  | `Get_Producto_By_IdProductoBodega` | Carga datos del producto al escanear |
| 3  | `Get_All_Presentaciones_By_IdProducto` | Presentaciones del producto |
| 5  | `Get_All_ProductoParametros_By_IdProducto_HH` | Parámetros (temp, peso, etc.) |
| 7  | `Get_Licenses_Plates_By_IdRecepcionEnc` | Lista de LPs de la recepción |
| 8  | `Existe_LP_By_IdRecepcionEnc_And_IdRecepcionDet` | Valida si LP ya existe |
| 16 | `Guardar_Recepcion` | **Más crítico** — guarda CajaMaster/principal |
| 17 | `Guardar_Recepcion_Edita` | Edición de recepción guardada |
| 18 | RecepcionCajaMaster | CajaMaster específico |
| 39 | `Get_Detalle_Rec_By_IdCompra_Licencia_JSON` | Recarga detalle por LP (post-guardar) |
| 42 | `Get_List_Product_By_CodigoBarra_By_OrdenCompraEnc` | Búsqueda por código de barra |

**Patrón normal case 16 → 39:**  
Guardar_Recepcion (case 16) → éxito → processDetalleRecepcion llama case 39 para recargar. Si el gap es < 100ms es encadenamiento normal. Si 39 se llama ANTES de que 16 termine → race condition.

---

## 7. Extender el trace a otros formularios (picking, despacho, etc.)

Cada formulario que tenga su propio `WebServiceHandler extends WebService` necesita las mismas 3 líneas:

```java
// En wsExecute() — primera línea después de try{
WmsTrace.wsExecute(ws.callback); //#EJC trace

// En wsCallBack() — primera línea después de try {
WmsTrace.wsCallBack(ws.callback, throwing, errmsg); //#EJC trace
```

El `import com.dts.base.WmsTrace;` ya está disponible en todos los activities que importan `com.dts.base.*`.

Para `WebService.java` ya está parcheado — el `callStart` aplica globalmente a todos los formularios que usen `callMethod`.

---

## 8. Señales a buscar en las pruebas de Killios

Basado en los bugs conocidos:

1. **BUG-003B (fixed)**: verificar que `case=16` ya no viene seguido de `!! RELOAD case=39` inmediato con gap < 50ms — eso indicaría que el detalle se recarga antes de que el guardado confirme.

2. **Recargas de producto**: si `case=1` aparece más de 2 veces por LP escaneado, hay un reload innecesario en el flujo de validación.

3. **Overlap en doble-tap**: si hay `!! OVERLAP case=16` → usuario presionó Guardar dos veces. Agregar `disableFabTemporarily()` o guard booleano antes del `ws.callback = 16`.

4. **Caso 39 lento (> 3000ms)**: indica que `Get_Detalle_Rec_By_IdCompra_Licencia_JSON` está tardando — revisar índices en `trans_re_det` por `IdOrdenCompraEnc + LicPlate`.
