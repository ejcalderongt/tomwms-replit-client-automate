# Skill: TOMHH2025 (Handheld Android del WMS)

Este skill es la contraparte de `wms-tomwms`. Junto con él forman el panorama completo del WMS.
Si solo cargás uno, te perdés la mitad del flujo.

---

## 1. Identidad y rol

**TOMHH2025** es la app **Android** que corre en los handhelds Zebra (terminales de operario).
Es el **cliente físico** que el operario tiene en la mano para ejecutar las tareas del WMS:
recepción, picking, packing, inventario, cambio de ubicación, RFID, etc.

- **No tiene base de datos local** (más allá de SharedPreferences/cache liviano).
- **Toda la lógica de negocio vive en BOF** (TOMWMS_BOF, los `.asmx`).
- **La HH solo orquesta UI + envía/recibe JSON al BOF**.
- **Dispositivo objetivo**: Zebra TC family (impresoras ZSDK, lectores 1D/2D, RFID).

---

## 2. Stack técnico real

| Componente | Valor (validado 2026-04-26 sobre `dev_2028_merge`) |
|---|---|
| Lenguaje | **Java** (no Kotlin) |
| Build system | **Gradle** (Android Gradle Plugin) |
| `compileSdk` | **34** |
| `minSdkVersion` | **24** (Android 7.0 Nougat) |
| `targetSdkVersion` | **24** ⚠ desactualizado vs `compileSdk` — ver §6 |
| `applicationId` | `com.dts.tom` |
| `versionName` | `8.3.6` |
| `versionCode` | `1` ⚠ no se incrementa — ver §6 |
| Módulos Gradle | `:app`, `:RFIDAPI3Library` |
| JSON | **Gson 2.10.1** (`com.google.code.gson:gson`) |
| XML legacy | `org.simpleframework:simple-xml:2.7.1` |
| HTTP | `HttpURLConnection` directo (no Retrofit, no OkHttp) |
| AndroidX | `appcompat:1.7.1`, `constraintlayout:2.2.1` |
| Apache Commons | `lang`, `io 2.4`, `lang3-3.4`, `net-3.1`, `validator-1.4.0` (jars locales en `libs/`) |
| Impresión | **Zebra ZSDK_ANDROID_API.jar** (jar local) |
| RFID | módulo separado `:RFIDAPI3Library` |

---

## 3. Estructura del proyecto

```
TOMHH2025/
├── app/                         <- modulo principal Android
│   ├── build.gradle             <- versions, dependencias
│   └── src/main/
│       ├── AndroidManifest.xml  <- 64 activities + 2 services declarados
│       └── java/com/dts/        <- todo el codigo (package raiz)
├── RFIDAPI3Library/             <- modulo Gradle separado para RFID
├── gradle/, gradlew, gradlew.bat
└── settings.gradle              <- include ':app', ':RFIDAPI3Library'
```

**Distribución de paquetes Java** (top, sobre 405 `.java` totales):

| Paquete | Cant. | Rol |
|---|---|---|
| `com.dts.ladapt.*` | 30 | List adapters (RecyclerView/ListView) |
| `com.dts.base.*` | 13 | Clases base (`WebService.java`, `appGlobals`, `PBase` se referencia desde acá pero vive en `com.dts.tom`) |
| `com.dts.classes.Mantenimientos.*` | múltiples | POJOs de catálogos (Bodega, Empresa, Menu_rol, Propietario...) |
| `com.dts.classes.Transacciones.*` | múltiples | POJOs de operaciones (Picking, Recepcion, Inventario, Stock_res...) |
| `com.dts.tom.*` | 12 (root) + sub | Activities (UI) — sub-organizadas por módulo de negocio |
| `com.dts.tom.Transacciones.<Modulo>.*` | 5-8 c/u | UI por módulo (ver §4) |
| `com.dts.rfid.*` | 11 | Lógica RFID |
| `com.dts.servicios.*` | 5 | Background services |

**Regla mental**: los `.frm_*` son **activities-pantalla**, los `list_adapt_*` son **adapters**, los `srv*` son **services**.

---

## 4. Módulos de negocio (los 13 frentes operativos)

Derivados del `AndroidManifest.xml`. Cada uno corresponde a una sección del menú principal y agrupa varias activities (`frm_*`):

| # | Módulo | Activities (cantidad) | Contraparte BOF típica |
|---|---|---|---|
| 1 | **Recepcion** | 7 (frmFirma, frm_lista_tareas_recepcion, frm_recepcion_datos, frm_list_rec_prod, frm_list_rec_prod_detalle, frm_detalle_ingresos, frm_rec_ftos) | módulos Recepción del backend, `Stock` |
| 2 | **Picking** | 5 (frm_picking_datos, frm_detalle_tareas_picking, frm_editar_ubicacion_picking, frm_danado_picking, frm_list_prod_reemplazo_picking) | flujo Picking del backend |
| 3 | **Verificacion** | 7 (frm_verificacion_datos, frm_detalle_tareas_verificacion, frm_verificacion_consolidada_detalle, frm_danado_verificacion, frm_list_prod_reemplazo_verif, frm_list_prod_reemplazados, ...) | Verificación / control de calidad |
| 4 | **Packing** | 7 (frm_Packing, frm_lista_packing, frm_lista_packing_lotes, frm_lista_packing_lp, frm_list_packing_cerrados, frm_preparacion_packing, frm_preparacion_packing_bulto) | módulo Packing |
| 5 | **CambioUbicacion** | 4 (frm_tareas_cambio_ubicacion, frm_cambio_ubicacion_dirigida, frm_cambio_ubicacion_ciega, frm_detalle_cambio_ubicacion) | Movimientos de stock |
| 6 | **Inventario** | 1 (frm_list_inventario) | Inventarios (resumen) |
| 7 | **InventarioCiclico** | 4 (frm_inv_cic_add, frm_inv_cic_nuevo, frm_inv_cic_conteo, frm_inv_cic_guardar) | Inventario_Ciclico backend |
| 8 | **InventarioInicial** | 7 (frm_inv_nuevo_reg, frm_inv_agrega_prd, frm_inv_ini_tramos, frm_inv_ini_conteo, frm_inv_ini_contados, frm_inv_ini_verificacion, frm_inv_ini_verificados) | Inventarios iniciales |
| 9 | **ConsultaStock** | 2 (frm_consulta_stock, frm_consulta_stock_detalleCI) | Consultas de stock |
| 10 | **Reabastecimiento** | 2 (frm_datos_reabastecimiento, frm_reabastecimiento_manual) | Reabastecimiento |
| 11 | **ReubicarStockRes** | 2 (frm_datos_stock_res, frm_lista_stock_res) | Stock reservado |
| 12 | **RFID** | 5 (frm_menu_rfid, frm_inventario_rfid, frm_recepcion_rfid, frm_procesar_rfid, frm_guardar_rfid) | módulos con etiquetas RFID |
| 13 | **ProcesaImagen** | 2 (frm_imagenes, frm_imagen_completa) | adjuntos / fotos de novedad |

Más: `MainActivity`, `Mainmenu`, `PBase` (parent abstract).
**Total activities en manifest: 64.**

**Background services declarados**: `com.dts.servicios.srvBaseJob` y `com.dts.servicios.srvCantTareas` (polling de cantidad de tareas pendientes).

---

## 5. Comunicación HH ↔ BOF

### Mecanismo

- **No hay base de datos local de operación** (solo SharedPreferences / cache simple).
- Toda llamada al backend se hace vía `com.dts.base.WebService` (`/app/src/main/java/com/dts/base/WebService.java`, 759 líneas).
- Stack: `HttpURLConnection` directo + `Gson` para serializar/deserializar JSON + `URLEncoder` para args.
- Timeouts: `CONNECT_TIMEOUT_MS = 15_000`, `READ_TIMEOUT_MS = 30_000`.
- Pool: `ExecutorService WS_EXECUTOR = Executors.newCachedThreadPool()` (uno compartido entre toda la app).
- Callback al hilo de UI vía `Handler(Looper.getMainLooper())`.
- API pública del wrapper (mantenida por compatibilidad histórica): `xmlresult`, `error`, `callback`, `errorflag`. **Atención: el campo se llama `xmlresult` aunque hoy lleva JSON, no XML — es legado**.

### Endpoint

- Apunta a los `.asmx` que viven en `TOMWMS_BOF/TOMIMSV4/...` (TOMWMS.WMSWebService).
- URL base parametrizada por configuración del operario.

### Formato

- Cuerpo: **JSON Forma A** (POJOs serializados con Gson).
- Excepción legacy: `replace("ñ","n")` en el método `normalize()` línea 352 — **regla a eliminar** (ver §6 regla 5).

### Implicancia operativa

- **Si la HH pierde conectividad, no puede operar**. No hay buffer offline para transacciones.
- **Cualquier cambio en un `.asmx` del backend que cambie el contrato JSON rompe la HH instalada en el equipo del operario** salvo que haya retrocompatibilidad. Ver §7.

---

## 6. Reglas duras de la HH

1. **Nunca subir `versionCode` ni `versionName` automáticamente.** Los releases los gestiona Erik en su PC (firma + APK). Solo modificar a pedido explícito.
2. **No migrar a Kotlin.** El proyecto es Java por decisión sostenida. Si querés helpers nuevos, mantené Java.
3. **No introducir Retrofit/OkHttp/Coroutines/RxJava.** El stack es `HttpURLConnection` + `ExecutorService` + `Handler`. Mantener consistencia.
4. **`compileSdk=34` pero `targetSdk=24`**: deliberado por compatibilidad con dispositivos Zebra viejos. **No subir `targetSdk` sin coordinar con Erik**: cambia el modelo de permisos runtime y el comportamiento de WindowInsets.
5. **UTF-8.** Mantener `ñ` y acentos. Eliminar el `.replace("ñ","n")` legacy en `com/dts/base/WebService.java` línea 352 (método `private String normalize`). Validado in situ 2026-04-26: la línea sigue presente con comentario `// si ya no lo necesitas, quítalo`.
6. **Activities = `frm_*` en `com.dts.tom.Transacciones.<Modulo>`**. Mantener este patrón. No mezclar UI fuera de este árbol.
7. **POJOs de dominio en `com.dts.classes.<Categoria>.<Modulo>`** (Mantenimientos / Transacciones). Mantener separación UI/modelo.
8. **Adapters siempre en `com.dts.ladapt.*`** con prefijo `list_adapt_*` (o `list_view_*` legacy).
9. **Servicios en `com.dts.servicios.*`** con prefijo `srv`.
10. **No hay capa de DB local de transacciones**. Si alguna vez se introduce, debe ser decisión de Erik documentada en `brain/agent-context/decisions/`.

---

## 7. Cross-impact con TOMWMS_BOF (lo más importante)

Esta es la razón por la que ambos skills viven en el mismo brain. Reglas para análisis de impacto cruzado:

### Cuando tocás BOF, ¿qué puede romper en HH?

| Cambio en BOF | Riesgo en HH |
|---|---|
| Renombrar/borrar/cambiar firma de un método `[WebMethod]` en un `.asmx` | **CRÍTICO**: la HH llama por nombre. Rompe en runtime. |
| Cambiar el shape de un DTO devuelto/aceptado por `.asmx` (agregar campo: OK; quitar/renombrar: rompe) | **ALTO**: Gson en HH falla silencioso o asigna null. |
| Cambiar reglas de validación server-side (ej. ahora exige campo X) | **MEDIO**: HH muestra error genérico, operario no sabe qué hacer. |
| Cambiar comportamiento del SP detrás del `.asmx` (ej. ahora descuenta stock distinto) | **VARIABLE**: HH puede no enterarse pero el efecto en negocio cambia. |
| Subir un nuevo flujo nuevo en BOF sin habilitar pantalla en HH | Bajo (no rompe), pero feature inutilizable hasta release de HH. |

### Cuando tocás HH, ¿qué puede romper en BOF?

| Cambio en HH | Riesgo en BOF |
|---|---|
| Mandar campos nuevos en el JSON de request | Bajo si el backend hace deserialización tolerante. **MEDIO** si valida estricto. |
| Dejar de mandar un campo opcional | Bajo. |
| Dejar de mandar un campo obligatorio | **ALTO**: backend tira excepción, operario queda bloqueado. |
| Llamar más seguido a un `.asmx` (ej. polling más agresivo) | **MEDIO**: carga sobre SQL Server Killios. |
| Cambiar formato de fecha/decimal enviado | **ALTO**: rechazo silencioso o datos corruptos. |

### Procedimiento obligatorio cuando un cambio toca el contrato HH↔BOF

1. **Identificar el `.asmx` y método involucrado** en TOMWMS_BOF.
2. **Identificar el wrapper del lado HH** (`com.dts.base.WebService` + el caller específico).
3. **Mapear DTO de request y response** en ambos lados.
4. **Definir estrategia de compatibilidad**:
   - ¿Es backwards-compatible? (campos nuevos opcionales, no remoción)
   - ¿Requiere release coordinado HH+BOF? Si sí, dejarlo escrito en el bundle.
5. **Documentar la decisión** en `brain/_inbox/_proposals/` antes de aplicar.

---

## 8. Snapshot de archivos clave

| Archivo | Path | Tamaño | Rol |
|---|---|---|---|
| `WebService.java` | `/app/src/main/java/com/dts/base/WebService.java` | 759 líneas / 27 KB | Capa HTTP/JSON al BOF. **Punto único de modificación** del transporte. |
| `PBase.java` | `/app/src/main/java/com/dts/tom/PBase.java` | (a inspeccionar) | Parent abstract de activities (referenciado por `WebService`). |
| `MainActivity.java` | `/app/src/main/java/com/dts/tom/MainActivity.java` | (a inspeccionar) | Entrypoint (login + selección de bodega típicamente). |
| `Mainmenu.java` | `/app/src/main/java/com/dts/tom/Mainmenu.java` | (a inspeccionar) | Menú principal post-login. |
| `AndroidManifest.xml` | `/app/src/main/AndroidManifest.xml` | 9.6 KB | **Fuente de verdad de activities/services**. |
| `build.gradle` (app) | `/app/build.gradle` | 89 líneas | Versions y dependencias. |

---

## 9. Convenciones canónicas

Ver `conventions.md` (en este mismo directorio) para naming, logs, manejo de errores, formato de commits.

---

## 10. Acceso al código

`brain/agent-context/AZURE_ACCESS.md` tiene los comandos pegables. Resumen:

- **URL**: `https://dev.azure.com/ejcalderon0892/TOMHH2025/_git/TOMHH2025`
- **Branch trabajo**: `dev_2028_merge` (default Azure: `master`)
- **PAT**: secret `AZURE_DEVOPS_PAT`
- **Replit**: solo lectura vía API REST. **NUNCA push.**

---

## 11. Cambios sobre este skill

Cualquier modificación a este archivo debe:
- Indicar fecha y motivo en el commit.
- Si la realidad del código no coincide, **actualizar el skill, no modificar el código** sin antes documentarlo en una decisión.
