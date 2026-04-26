# Convenciones canónicas — TOMHH2025

Reglas de estilo y patrones validados sobre el código real (`dev_2028_merge`, snapshot 2026-04-26).
Cualquier desviación encontrada en el código nuevo debe corregirse en el código, **no** en este documento — salvo decisión explícita registrada en `brain/agent-context/decisions/`.

---

## 1. Naming

### Activities (pantallas)

- Prefijo: `frm_*` (snake_case, todo en minúscula).
- Ejemplo: `frm_recepcion_datos`, `frm_picking_datos`, `frm_consulta_stock`.
- Excepciones históricas (camelCase): `frmFirma` — **no replicar**, dejar como está.
- Excepciones core (sin `frm_`): `MainActivity`, `Mainmenu`, `PBase`.
- Ubicación: `com.dts.tom.Transacciones.<Modulo>` (ej. `com.dts.tom.Transacciones.Picking`).

### Adapters

- Prefijo: `list_adapt_*` (preferido) o `list_view_*` (legado).
- Ubicación: `com.dts.ladapt` o `com.dts.ladapt.<Modulo>`.
- Ejemplo: `list_adapt_detalle_tareas_picking`, `list_view_tareas_cambio_ubic`.

### Services

- Prefijo: `srv`.
- Ubicación: `com.dts.servicios`.
- Ejemplo: `srvBaseJob`, `srvCantTareas`.

### Modelos / POJOs de dominio

- Sin prefijo, PascalCase (clase Java estándar).
- Ubicación: `com.dts.classes.<Categoria>.<Modulo>`.
  - `Mantenimientos/` — catálogos (Bodega, Empresa, Menu_rol, Propietario, etc.).
  - `Transacciones/` — operaciones (Picking, Recepcion, Stock, Inventario, Packing, etc.).

### Clases base / utilidades

- Ubicación: `com.dts.base`.
- Ejemplo: `WebService`, `appGlobals`.

---

## 2. Estructura típica de una activity de transacción

Patrón heredado:

```java
public class frm_<modulo>_<accion> extends PBase {
    // 1. Declaracion de widgets
    // 2. Estado local de la pantalla (DTO en curso)
    // 3. onCreate -> bind UI, setup listeners
    // 4. Llamadas a WebService para cargar/enviar
    // 5. Callbacks via parent (PBase).callback(...)
}
```

`PBase` es el parent abstract que centraliza:
- Wiring del `WebService`.
- Manejo común de error/loading.
- Helpers de navegación.

---

## 3. Manejo de errores en llamadas WebService

API histórica del wrapper (mantener):

```java
ws.callback = N;          // identificador del request
ws.errorflag = false;
ws.<metodoLlamada>(...);  // dispara async, devuelve via parent.callback(N, ...)
```

- En `callback(int n, ...)`: chequear `ws.errorflag` y `ws.error` antes de procesar `ws.xmlresult`.
- Mostrar error genérico al operario con texto humano (no stack trace).
- Loggear el error completo para debug.

---

## 4. Logs

- Usar `android.util.Log.d/e/w` con TAG = nombre simple de la clase (`getClass().getSimpleName()`).
- **No loggear PII del operario** (usuario, password) ni el cuerpo completo del JSON si contiene datos sensibles.
- En errores de red, loggear: timestamp, endpoint, código HTTP, snippet de respuesta (max 500 chars).

---

## 5. Threading

- **UI thread**: solo actualización de widgets.
- **Network**: siempre via `WebService` (que ya usa `ExecutorService`).
- **Callback al UI**: via `Handler(Looper.getMainLooper())` (ya implementado en `WebService`).
- **No usar `AsyncTask`** (deprecated en API 30+). Usar el pool de `WebService`.

---

## 6. JSON

- Serialización: **Gson** (`new Gson().toJson(...)` / `fromJson(...)`).
- Atributo legacy XML (`org.simpleframework:simple-xml`) está SOLO para configuraciones viejas. **No usar en código nuevo**.

---

## 7. Recursos (`res/`)

- Strings duros en código: evitar. Usar `R.string.<nombre>`.
- Iconos: prefijo `ic_`. Layouts de activity: `activity_<nombre_snake>.xml`. Layouts de adapter: `item_<nombre_snake>.xml` o `list_<nombre>.xml`.

---

## 8. Permisos

Permisos declarados en manifest (snapshot 2026-04-26):
`INTERNET`, `ACCESS_NETWORK_STATE`, `ACCESS_WIFI_STATE`, `BLUETOOTH`, `BLUETOOTH_ADMIN`, `WAKE_LOCK`, `READ_EXTERNAL_STORAGE`, `WRITE_EXTERNAL_STORAGE`, `ACCESS_FINE_LOCATION`, `ACCESS_COARSE_LOCATION`.

- **No agregar permisos sin justificación documentada** (impacta el aviso al operario en la instalación del APK).
- Permisos runtime: respetar el modelo de `targetSdk=24` (no API 30+).

---

## 9. Commits

Formato sugerido del mensaje:
```
HH/<modulo>: <verbo en infinitivo, en español>
[línea en blanco]
[contexto: qué problema resuelve, qué tocó, riesgos]
[ref: ticket / case_id si aplica]
```

Ejemplos:
- `HH/Picking: corregir cálculo de saldo en frm_picking_datos`
- `HH/WebService: eliminar replace ñ legacy (línea 352)`

---

## 10. Cosas que están en el código pero NO replicar

- `AsyncTask` (deprecated).
- Llamadas directas `new HttpURLConnection` fuera de `WebService`.
- Hardcodear URLs.
- `replace("ñ","n")` o transformaciones de charset compensatorias.
- Mezclar UI y modelo en el mismo paquete.
- Crear nuevos patrones de naming (siempre seguir §1).
