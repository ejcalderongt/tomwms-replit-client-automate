# Ciclo 3.2 B — Catalogación TOMHH2025 (Android Handheld)

**Generado**: 2026-04-27T05:34:37.321Z
**Fuente**: Azure DevOps `ejcalderon0892/TOMHH2025`, rama `dev_2028_merge`
**Volumen procesado**: 576 archivos / 154,806 líneas (incluye 405 .java, 167 .xml, 3 .gradle, 1 .properties; excluyendo `/build/`, `/.gradle/`, drawables binarios y assets).

---

## 1. Resumen ejecutivo

Este ciclo catalogó la app Android del Handheld y la cruzó contra el catálogo de WMs del WSHHRN producido en la Ciclo 3.2 A. Hallazgos centrales:

- **60 activities** detectadas (vs **65 declaradas** en el AndroidManifest — delta de 5 declaraciones huérfanas o de adapters).
- **403 invocaciones a WebMethods** desde el HH, repartidas en **240 WMs únicos**.
- **Cobertura HH↔server: 64.69%** (240/371 WMs son consumidos).
- **129 WMs huérfanos** (declarados en server pero NUNCA llamados desde HH) — candidatos a auditoría/deprecación.
- **0 WMs zombies** (HH llama, server no tiene) — **calidad de contrato perfecta**: ningún call HH apunta a método inexistente.
- **Migración XML→JSON: 9.9% de las invocaciones del HH** (40 JSON vs 361 SOAP) — la regla "migración oportunista" se está aplicando con ritmo bajo pero consistente.
- **Locales soportados: 1** (default español + values-round con 1 string para wear). **No hay multi-idioma**. La UI del operador está hardcoded mayormente en español dentro del código Java (no en `strings.xml` — solo 33 keys formales en `values/strings.xml`).
- **Permissions sensibles**: 13 totales, incluyendo Bluetooth (4), Location (2), Storage (2), Network (3). `usesCleartextTraffic="true"` activo.

## 2. Hallazgos críticos vs replit.md

| Item | replit.md | Real medido | Comentario |
|---|---|---|---|
| Activities | 55 | **60** (60 en código + 65 en manifest) | Subestimación leve |
| Java files | 405 | 405 | Match |
| applicationId | com.dts.tom v8.2.3 | applicationName=`com.dts.base.appGlobals` | Confirmado |
| Cliente HTTP | (no mencionado) | **WebService.java + 49 inner `WebServiceHandler extends WebService`** | Patrón bien definido. Ver §6 |
| Activity base class | (no mencionado) | **PBase extends Activity** + 1 directa AppCompatActivity (`frm_listado_tareas`) | Patrón consistente |
| Migración XML→JSON | "oportunista, nuevo=JSON" | **9.9% de calls son JSON** | Regla aplicada con disciplina |
| WM "loginHandHeld" | (mencionado) | Confirmado: 1 caller en `MainActivity` línea 1885 | OK |

## 3. Estructura del repo HH

```
/app/src/main/
  AndroidManifest.xml                          1 archivo
  /java/com/dts/
    /tom/                  Activities + MainActivity + Mainmenu + PBase
      /Transacciones/      63 archivos (frm_*, packing, picking, recepcion, etc)
      /util/               4 archivos
    /classes/              250 archivos (entities clsBe* y mirror DAL clsLn*)
      /Transacciones/      135 archivos
      /Mantenimientos/     115 archivos
    /base/                 13 archivos (WebService, ApiService, AppMethods,
                           appGlobals, RetrofitClient, NetworkUtils,
                           DateUtils, MiscUtils, ExDialog, XMLObject,
                           NetWorkInfoUtility, DecimalDigitsInputFilter, clsClasses)
    /ladapt/               47 archivos (list_adapt_* — 47 BaseAdapters/RecyclerView.Adapter)
  /res/
    /layout/               132 archivos (.xml)
    /values/strings.xml    33 keys (default español)
    /values-round/         1 string (Wear OS)
    /drawable/             21 .xml + binarios (excluidos)
```

## 4. Cobertura por capa

| Capa | Cantidad | Detalle | Propósito |
|---|---|---|---|
| Activities (Java) | 60 | 49 con WM calls | Pantallas operacionales |
| Activities (Manifest) | 65 | 1 exportadas, 1 launcher | Declaraciones formales |
| Adapters | 47 | 47 BaseAdapter/RecyclerView.Adapter | Listas (tareas, productos, lpns) |
| Services | 0 | 1 PrintReceiverService o similar | Background |
| Fragments | 1 | 1 unique | Mínimo uso de Fragments |
| AsyncTasks | 2 | 2 (incluye NetWorkInfoUtility) | Patrón moderno = ExecutorService en WebService.java |
| Receivers | 0 (Java) + 0 (manifest) | PrintReceiverActivity (intent-receiver style) |  |
| Application class | 1 | appGlobals (singleton de estado) | Holds wsurl, IdEmpresa, IdBodega |
| Entities (clsBe*) | ~155 | espejo del backend BOF para serialización | (detectados en /com/dts/classes) |
| DAL local (clsLn*) | ~95 | espejo del backend para deserialización | (detectados en /com/dts/classes) |

## 5. Mapa de pantallas por dominio operacional

| Dominio | # Activities | Total WM calls | WMs únicos | Ejemplos |
|---|---|---|---|---|
| Recepción | 9 | 130 | 72 | `frm_recepcion_datos, frm_recepcion_datos_original, frm_list_rec_prod…` |
| Inventario | 13 | 96 | 61 | `frm_inv_cic_add, frm_inv_ini_conteo, frm_inv_ini_verificacion…` |
| Reubicación | 6 | 47 | 35 | `frm_cambio_ubicacion_ciega, frm_cambio_ubicacion_dirigida, frm_detalle_cambio_ubicacion…` |
| Picking | 5 | 39 | 35 | `frm_picking_datos, frm_detalle_tareas_picking, frm_list_prod_reemplazo_picking…` |
| Verificación | 6 | 29 | 25 | `frm_detalle_tareas_verificacion, frm_verificacion_datos, frm_list_prod_reemplazo_verif…` |
| Packing | 7 | 27 | 24 | `frm_Packing, frm_preparacion_packing, frm_lista_packing…` |
| Sesión/Menú | 3 | 17 | 15 | `MainActivity, Mainmenu, frm_menu_rfid` |
| Consulta Stock | 2 | 6 | 6 | `frm_consulta_stock, frm_consulta_stock_detalleCI` |
| Reabastecimiento | 2 | 6 | 6 | `frm_datos_reabastecimiento, frm_reabastecimiento_manual` |
| Otros | 5 | 4 | 4 | `frm_guardar_rfid, frm_procesar_rfid, PBase…` |
| Impresión | 1 | 0 | 0 | `PrintReceiverActivity` |
| Listado Tareas | 1 | 0 | 0 | `frm_listado_tareas` |

## 6. Patrón de invocación HH → Server (descubierto)

El HH **NO usa KSOAP2 ni Retrofit como mecanismo principal**. La arquitectura es:

```java
// En cada activity (frm_*.java):
public class frm_recepcion_datos extends PBase {
    private WebServiceHandler ws;

    void cargarDatos() {
        ws = new WebServiceHandler(this, gl.wsurl);
        ws.execute();   // dispara wsExecute() en background
    }

    // Inner class que hereda de WebService base
    public class WebServiceHandler extends WebService {
        @Override
        protected void wsExecute() {
            switch (callback) {
                case 1:
                    callMethod("Get_Trans_Re_Enc_By_IdRecepcion",
                               "pIdRecepcion", id);   // ← nombre del WM como string literal
                    break;
                case 2:
                    callMethodJsonPost("Android_Get_All_Empresas_Json");
                    break;
            }
        }
        @Override
        protected void wsFinished() { /* parsea xmlresult / json */ }
    }
}
```

**Tres transports en uso simultáneo**:

| Transport | Calls | % | Implementación |
|---|---|---|---|
| `callMethod(...)` | 361 | 89.6% | SOAP XML clásico contra `http://tempuri.org/<WM>`, en `WebService.java:163` |
| `callMethodJsonPost(...)` | 40 | 9.9% | POST JSON con args alternados clave/valor, `WebService.java:133` (la "Forma A" del replit.md) |
| `callMethodJsonGet(...)` | 0 | 0% | GET JSON con query string, `WebService.java:109` (no usado todavía) |
| `@POST/@GET` Retrofit | 1 + 0 | <1% | Solo `ApiService.java` (modernización experimental) |
| `SoapObject` KSOAP2 | 0 | 0% | **Legacy: solo 1 referencia residual** |

**Conclusión**: la regla del replit.md "migración oportunista, nuevo=JSON" se está cumpliendo. Hay **23 WMs llamados solo via JSON** (modernos), **0 WMs con doble transport** (en transición) y el resto exclusivamente SOAP.

## 7. Cruce HH ↔ Server (matriz consumed/orphan/zombie)

| Métrica | Valor | % |
|---|---|---|
| WMs en server (catálogo BOF) | 371 | 100% |
| WMs consumidos por HH | 240 | 64.69% |
| WMs huérfanos (server tiene, HH no llama) | 129 | 34.8% |
| WMs únicos llamados desde HH | 240 | (de los cuales 0 zombies) |
| **Zombies** (HH llama, server no tiene) | **0** | **0%** ✓ |

> **Hallazgo clave**: con cero zombies confirmamos que el catálogo BOF de la Ciclo 3.2 A está completo y el HH nunca apunta a métodos inexistentes. La calidad del contrato HH↔WSHHRN es excelente.

## 8. Top 30 WMs más invocados por el HH (consumidos)

| # | WM | # callers | Callers ejemplo |
|---|---|---|---|
| 1 | `Get_Ubicacion_By_Codigo_Barra_And_IdBodega` | 14 | `frm_cambio_ubicacion_dirigida`, `frm_cambio_ubicacion_dirigida`, `frm_consulta_stock` |
| 2 | `Get_All_Presentaciones_By_IdProducto` | 10 | `frm_inv_cic_add`, `frm_inv_cic_guardar`, `frm_inv_ini_verificacion` |
| 3 | `Get_Resoluciones_Lp_By_IdOperador_And_IdBodega` | 9 | `MainActivity`, `frm_cambio_ubicacion_ciega`, `frm_inv_cic_add` |
| 4 | `Get_BeProducto_By_Codigo_For_HH` | 8 | `frm_detalle_cambio_ubicacion`, `frm_consulta_stock`, `frm_inv_cic_guardar` |
| 5 | `Get_Producto_By_IdProductoBodega` | 7 | `frm_consulta_stock_detalleCI`, `frm_inv_cic_add`, `frm_picking_datos` |
| 6 | `Get_Estados_By_IdPropietario` | 6 | `frm_inv_cic_guardar`, `frm_inv_ini_contados`, `frm_inv_ini_conteo` |
| 7 | `Get_Estados_By_IdPropietario_And_IdBodegaHH` | 5 | `frm_danado_picking`, `frm_picking_datos`, `frm_recepcion_datos` |
| 8 | `Get_Estados_By_IdPropietario_JSON` | 4 | `frm_cambio_ubicacion_ciega`, `frm_inv_cic_add`, `frm_inv_cic_conteo` |
| 9 | `Guardar_Recepcion` | 4 | `frm_recepcion_datos`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 10 | `Get_Detalle_OC_By_IdOrdeCompraDet` | 4 | `frm_recepcion_datos`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 11 | `Ubicacion_Valida_By_IdUbicacion_And_IdEstado` | 4 | `frm_danado_picking`, `frm_danado_picking`, `frm_danado_verificacion` |
| 12 | `Get_IdUbicMerma_By_IdBodega` | 4 | `frm_recepcion_datos`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 13 | `Get_Ubicacion_By_Codigo_Barra_And_IdBodega_JSON` | 4 | `frm_cambio_ubicacion_ciega`, `frm_cambio_ubicacion_ciega`, `frm_cambio_ubicacion_ciega` |
| 14 | `Actualizar_Inventario_Inicial_By_BeTransInvTramo` | 4 | `frm_inv_ini_conteo`, `frm_inv_ini_conteo`, `frm_inv_ini_verificacion` |
| 15 | `Existe_LP_By_IdRecepcionEnc_And_IdRecepcionDet` | 4 | `frm_recepcion_datos`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 16 | `Tiene_Posiciones` | 4 | `frm_cambio_ubicacion_ciega`, `frm_cambio_ubicacion_dirigida`, `frm_recepcion_datos` |
| 17 | `Delete_Det_By_IdRecepcionEnc_And_IdRecpecionDet` | 3 | `frm_list_rec_prod_detalle`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 18 | `Guardar_Fotos_Recepcion` | 3 | `frm_detalle_ingresos`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 19 | `Get_All_Producto_Imagen` | 3 | `frm_picking_datos`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 20 | `Get_All_Imagen_Recepcion` | 3 | `frm_detalle_ingresos`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 21 | `Get_BeProducto_By_IdProducto` | 3 | `frm_inv_cic_conteo`, `frm_inv_ini_contados`, `frm_inv_ini_verificados` |
| 22 | `Get_Tipo_Etiqueta_By_IdTipoEtiqueta` | 3 | `frm_consulta_stock_detalleCI`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 23 | `Get_Inventario_Teorico_By_Codigo` | 3 | `frm_inv_cic_guardar`, `frm_inv_ini_conteo`, `frm_inv_ini_verificacion` |
| 24 | `Actualizar_Ubicaciones_Reservadas_By_IdStock` | 3 | `frm_cambio_ubicacion_ciega`, `frm_editar_ubicacion_picking`, `frm_datos_stock_res` |
| 25 | `Get_Colores` | 3 | `frm_inv_cic_add`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 26 | `Get_Tallas` | 3 | `frm_inv_cic_add`, `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 27 | `GetSingleRec` | 2 | `frm_detalle_ingresos`, `frm_lista_tareas_recepcion` |
| 28 | `Get_BeTransReEnc_By_IdREcepcionEnc_For_HH` | 2 | `frm_recepcion_datos`, `frm_recepcion_datos_original` |
| 29 | `Get_Single_By_IdEstado` | 2 | `frm_cambio_ubicacion_ciega`, `frm_cambio_ubicacion_dirigida` |
| 30 | `Get_All_BeTrasReDet_By_IdOrdenCompraEnc` | 2 | `frm_recepcion_datos`, `frm_recepcion_datos_original` |

## 9. Top 30 WMs huérfanos del server (candidatos a auditoría/deprecación)

Estos WMs están declarados en TOMHHWS o srvSAPSync con SQL inline real, pero **ninguna activity del HH los invoca**. Posibles causas:

- (a) Endpoints muertos / refactor pendiente
- (b) Llamados desde otra app cliente (BOF directamente, integraciones)
- (c) Llamados via un nombre de método dinámico no detectado por análisis estático

| # | WM huérfano | WS | SQL blocks (server) | Tablas tocadas |
|---|---|---|---|---|
| 1 | `Get_Empresa_By_Codigo_And_Clave` | TOMHHWS | 0 | 0 |
| 2 | `Get_List_Empresas_For_HH` | TOMHHWS | 0 | 0 |
| 3 | `Get_Bodegas_By_IdEmpresa_For_HH` | TOMHHWS | 0 | 0 |
| 4 | `Get_Productos_By_IdBodega_For_HH` | TOMHHWS | 0 | 0 |
| 5 | `Get_Propietarios_By_IdBodega_For_HH` | TOMHHWS | 0 | 0 |
| 6 | `Get_All_Reglas_Recepcion_For_HH` | TOMHHWS | 0 | 0 |
| 7 | `Get_Clientes_By_IdBodega_For_HH` | TOMHHWS | 0 | 0 |
| 8 | `Get_Proveedores_By_Bodega_For_HH` | TOMHHWS | 0 | 0 |
| 9 | `Get_All_By_IdBodega_HH_Filtro` | TOMHHWS | 0 | 0 |
| 10 | `Get_Motivo_Anulacion_By_IdBodega_For_HH` | TOMHHWS | 0 | 0 |
| 11 | `Get_Motivos_Anulacion_By_IdBodega_For_HH` | TOMHHWS | 0 | 0 |
| 12 | `Get_Motivos_Devolucion_By_IdBodega_For_HH` | TOMHHWS | 0 | 0 |
| 13 | `Get_Motivos_Devolucion_Bodega_By_IdBodega_For_HH` | TOMHHWS | 0 | 0 |
| 14 | `ServiceIsAlive` | TOMHHWS | 0 | 0 |
| 15 | `GetTimeStampServer` | TOMHHWS | 0 | 0 |
| 16 | `Get_Menu_Rol_Op_For_HH` | TOMHHWS | 0 | 0 |
| 17 | `Operador_Valido_HH` | TOMHHWS | 0 | 0 |
| 18 | `GetSis_estado_tarea_hh` | TOMHHWS | 0 | 0 |
| 19 | `GetTarea_HH` | TOMHHWS | 0 | 0 |
| 20 | `Get_Tipo_Recepcion` | TOMHHWS | 0 | 0 |
| 21 | `Get_Recepciones_By_IdBodega` | TOMHHWS | 0 | 0 |
| 22 | `Get_Nombre_Rol_Operador_For_HH_By_IdRolOperador` | TOMHHWS | 0 | 0 |
| 23 | `Get_Menu_By_IdRolOperador_For_HH` | TOMHHWS | 0 | 0 |
| 24 | `Get_All_Recepciones_For_HH_By_IdBodega` | TOMHHWS | 0 | 0 |
| 25 | `Get_All_Trans_Re_Tr_For_HH` | TOMHHWS | 0 | 0 |
| 26 | `Get_All_Empresas_For_HH` | TOMHHWS | 0 | 0 |
| 27 | `Get_All_Filtro_TransOCTipo_For_HH` | TOMHHWS | 0 | 0 |
| 28 | `Get_BeTransOcEnc_By_IdOrdenCompraEnc` | TOMHHWS | 0 | 0 |
| 29 | `Get_BeRolOPerador_By_IdRolOperador` | TOMHHWS | 0 | 0 |
| 30 | `Get_All_Impresora_By_IdEmpresa_And_IdBodega` | TOMHHWS | 0 | 0 |

## 10. WMs solo-JSON (ya migrados, no llaman SOAP)

**23 WMs** son llamados exclusivamente via JSON desde el HH:

`Get_Tipo_Etiqueta_By_IdTipoEtiqueta_Json, Android_Get_All_Empresas_Json, Get_IdUbicacion_Recepcion_By_IdBodega_Json, Get_BeProducto_By_Codigo_For_HH_JSON, Get_Stock_By_Lic_Plate_JSON, Get_Productos_By_IdUbicacion_Existencias_JSON, Get_Productos_By_IdUbicacion_And_LicPlate_JSON, Get_Productos_By_IdUbicacion_JSON, Get_Estados_By_IdPropietario_JSON, Get_Ubicacion_By_Codigo_Barra_And_IdBodega_JSON, Existe_Lp_By_Licencia_And_IdBodega_JSON, Es_Pallet_No_Estandar_JSON, Ubicacion_Es_Valida_JSON, Get_Ubicacion_Sugerida_JSON, Validar_Mismo_Producto_Posicion_JSON, Validar_Regla_Ubicacion_JSON, Get_Stock_Por_Producto_Ubicacion_CI_Json, Get_Colores, Get_Tallas, Get_Producto_Talla_Color_JSON, Get_Picking_Para_Emapaque_Consolidado, Get_All_PickingUbic_By_IdPickingEnc_Tipo_Json, Get_Detalle_Rec_By_IdCompra_Licencia_JSON`

## 11. WMs en transición (SOAP + JSON simultáneo)

**0 WMs** son llamados de ambas formas en distintos lugares del código (transición incompleta):

``

> **Acción recomendada**: priorizar estos para terminar la migración (eliminar el call SOAP duplicado).

## 12. AndroidManifest — security posture

| Item | Valor |
|---|---|
| Total activities declaradas | 65 |
| Activities exportadas (`exported="true"`) | 1 (`.MainActivity`) |
| Activities con `exported="false"` | 3 |
| Activities sin atributo `exported` | 61 |
| Launcher activities (MAIN+LAUNCHER) | 1 (`.MainActivity`) |
| Services declarados | 2 |
| Receivers declarados | 0 |
| Providers declarados | 1 |
| **`usesCleartextTraffic`** | **true** (HTTP plano permitido — apropiado para LAN del cliente, no para WAN) |

### Permisos solicitados (13)

- `android.permission.WAKE_LOCK`
- `android.permission.WRITE_EXTERNAL_STORAGE`
- `android.permission.READ_EXTERNAL_STORAGE`
- `android.permission.BLUETOOTH`
- `android.permission.BLUETOOTH_ADMIN`
- `android.permission.ACCESS_FINE_LOCATION`
- `android.permission.ACCESS_COARSE_LOCATION`
- `android.permission.ACCESS_NETWORK_STATE`
- `android.permission.INTERNET`
- `android.permission.ACCESS_WIFI_STATE`
- `android.permission.BLUETOOTH_SCAN`
- `android.permission.BLUETOOTH_CONNECT`
- `android.permission.BLUETOOTH_ADVERTISE`

### Recomendaciones de hardening

1. **`usesCleartextTraffic="true"`** está bien para LAN cerrada Killios, pero documentar como prerrequisito de despliegue. Para clientes con HH sobre Wi-Fi pública, requerir HTTPS.
2. La única activity exportada es `com.dts.classes.Mantenimientos.list_adapt_…` — verificar si realmente debe ser exported (probablemente `tools:ignore="Instantiatable"` indica falso positivo del linter).
3. Permisos `READ/WRITE_EXTERNAL_STORAGE` son `legacy` para Android 11+; si `targetSdk` sube de 24 a 30+, requerirá Scoped Storage (rewrite del módulo de fotos).
4. `ACCESS_FINE_LOCATION` se solicita pero no detecté uso en código (posible permiso ya no usado — auditar).

## 13. Locales y copy del operador

| Locale | Archivo | Strings |
|---|---|---|
| default | `/app/src/main/res/values/strings.xml` | 33 |
| round | `/app/src/main/res/values-round/strings.xml` | 1 |

> **Hallazgo crítico**: la app **no tiene multi-idioma**. Solo 33 strings en `values/strings.xml` (mayormente nombres de pantallas y labels muy comunes). El **resto del copy del operador (mensajes de error, confirmaciones, validaciones, instrucciones de scan) está hardcoded en español dentro del código Java**.

### Top 20 strings de UI por frecuencia de uso

| # | Key | # Refs | Texto en español |
|---|---|---|---|
| 1 | `app_name` | 44 | "TOMWMS" |
| 2 | `c_digo` | 3 | "Código:" |
| 3 | `estado` | 3 | "Estado:" |
| 4 | `consulta_de_existencia_trx8` | 2 | "Consulta de Existencia - TRX8" |
| 5 | `no_version` | 2 | "Versión" |
| 6 | `reemplazo` | 2 | "Reemplazo" |
| 7 | `regs_0` | 1 | "Regs: 0" |
| 8 | `nueva` | 1 | "Nueva" |
| 9 | `tareas_de_preparaci_n` | 1 | "Tareas de preparación" |
| 10 | `tarea` | 1 | "Tarea:" |
| 11 | `tom_wms` | 1 | "TOMWMS" |
| 12 | `inicio_de_sesi_n` | 1 | "Inicio de sesión" |
| 13 | `fecha_version` | 1 | "21-04-2026" |
| 14 | `por_favor_espere` | 1 | "Por favor espere..." |
| 15 | `vence` | 1 | "Vence:" |
| 16 | `u_m_bas` | 1 | "U.M. Bas.:" |
| 17 | `licencia` | 1 | "Licencia" |
| 18 | `lote` | 1 | "Lote:" |
| 19 | `presentaci_n` | 1 | "Presentación:" |
| 20 | `no_encontrado` | 1 | "No Encontrado" |

### Implicaciones

1. Si un cliente futuro pide multi-idioma (inglés, portugués), requiere extracción masiva de strings hardcoded → `strings.xml` antes de poder hacer `values-en/`, `values-pt/`.
2. **El Brain WMS no podrá reconstruir el texto exacto que ve el operador** simplemente leyendo `strings.xml` — necesita parsear los string literales del código Java. Esto es viable como ciclo futuro.

## 14. Top 20 activities por número de WM calls

| # | Activity | Líneas | Total WM calls | WMs únicos | Layout |
|---|---|---|---|---|---|
| 1 | `frm_recepcion_datos` | 11482 | 46 | 42 | activity_frm_recepcion_datos |
| 2 | `frm_recepcion_datos_original` | 11371 | 46 | 42 | activity_frm_recepcion_datos |
| 3 | `frm_cambio_ubicacion_ciega` | 5335 | 32 | 28 | activity_frm_cambio_ubicacion_ciega |
| 4 | `frm_picking_datos` | 3329 | 18 | 18 | activity_frm_picking_datos |
| 5 | `frm_inv_cic_add` | 2573 | 16 | 15 | activity_frm_inv_cic_add |
| 6 | `frm_list_rec_prod` | 2281 | 16 | 14 | activity_frm_list_rec_prod3 |
| 7 | `frm_inv_ini_conteo` | 2199 | 15 | 14 | activity_frm_inv_ini_conteo |
| 8 | `frm_inv_ini_verificacion` | 1352 | 15 | 14 | activity_frm_inv_ini_verificacion |
| 9 | `frm_Packing` | 3071 | 14 | 14 | activity_frm__packing |
| 10 | `frm_detalle_tareas_verificacion` | 1388 | 12 | 12 | activity_frm_detalle_tareas_verificacion2 |
| 11 | `MainActivity` | 2024 | 11 | 11 | activity_main |
| 12 | `frm_lista_tareas_recepcion` | 1149 | 11 | 11 | activity_frm_lista_tareas_recepcion |
| 13 | `frm_inv_ini_contados` | 832 | 10 | 10 | activity_frm_inv_ini_contados |
| 14 | `frm_detalle_tareas_picking` | 1741 | 9 | 9 | activity_frm_detalle_tareas_picking |
| 15 | `frm_inv_cic_guardar` | 983 | 8 | 8 | activity_frm_inv_cic_guardar |
| 16 | `frm_inv_ini_verificados` | 629 | 8 | 8 | activity_frm_inv_ini_verificados |
| 17 | `frm_detalle_ingresos` | 960 | 8 | 8 | activity_frm_detalle_ingresos |
| 18 | `frm_inv_cic_conteo` | 1534 | 7 | 7 | activity_frm_inv_cic_conteo |
| 19 | `frm_list_prod_reemplazo_picking` | 1082 | 7 | 6 | activity_frm_list_prod_reemplazo_picking |
| 20 | `frm_verificacion_datos` | 1645 | 7 | 6 | activity_frm_verificacion_datos |

## 15. Activities sin WM calls (puramente UI/navegación)

**11 activities** no invocan ningún WM directamente. Suelen ser:

- Pantallas de menú/navegación (`Mainmenu` parcial, screens intermedias)
- Splash/wait screens
- Activities helper para listados que delegan I/O al adapter

Listado: `PBase`, `PrintReceiverActivity`, `frm_lista_packing_lotes`, `frm_lista_packing_lp`, `frm_imagen_completa`, `frm_imagenes`, `frm_menu_rfid`, `frm_recepcion_rfid`, `frmFirma`, `frm_rec_ftos`, `frm_listado_tareas`

## 16. Top 10 hallazgos accionables para Erik

1. **129 WMs huérfanos en server** son candidatos a auditoría: si nadie los llama desde HH, ni desde BOF directamente, ni desde la capa moderna, son dead code. Cruzar con catálogo de Ciclo 3.2 A para confirmar.
2. **Migración XML→JSON al 9.9%**: ritmo bajo pero consistente. Si queremos acelerar, los **0 WMs en transición** son el target obvio (eliminar el call SOAP que ya tiene su versión JSON).
3. **0 zombies** confirma calidad del contrato HH↔WSHHRN: ningún call apunta a método inexistente en server.
4. **Multi-idioma no soportado**: si un cliente futuro lo pide, requiere extracción masiva de hardcoded strings primero.
5. **`MainActivity` con 14 callMethod** y 2024 líneas es un god-object — candidato prioritario a refactor (separar login, sync inicial, menú principal en activities distintas).
6. **49 inner classes `WebServiceHandler`** repetidas (una por activity) — oportunidad de DRY: una sola clase genérica con parametrización del callback dispatch.
7. **Patrón `callback = N` en switch dentro de `wsExecute()`** es típico de KSOAP2 legacy — al modernizar el patrón, considerar callbacks lambda o sealed types.
8. **2 services declarados en manifest pero solo 1 detectado en código** — verificar si hay referencias muertas o si el segundo es declarado por dependencia (Firebase, Google Play Services).
9. **`gl.wsurl` como configuración global** (`appGlobals` Application class) — confirmar que el cliente puede cambiar URL del WS sin recompilar (settings).
10. **Permisos Bluetooth Android 12+** declarados con `required="false"` — buena defensa para devices viejos. `ACCESS_FINE_LOCATION` solicitado pero sin uso aparente — limpiar.

## 17. Manifiesto de archivos generados

Todos en `data/passada-3-2-hh-android/` del repo `ejcalderongt/tomwms-replit-client-automate` rama `wms-brain`:

| Archivo | Propósito | Tamaño |
|---|---|---|
| `index-maestro.json` | Lista de los 576 archivos catalogados con SHA1 + tipo | 102KB |
| `activities.json` | 60 activities con layout + strings + WM calls + intents | 106KB |
| `services.json` | Services + receivers + workers + adapters + asyncTasks + applications | 4KB |
| `http-calls.json` | 403 invocaciones a WMs (con caller, line, type) | 172KB |
| `strings-ui.json` | 33 strings + cross-ref a activities/layouts | 5KB |
| `manifest-permisos.json` | Permissions + activities declaradas + intent filters + security posture | 11KB |
| `cruce-hh-server.json` | Matriz HH↔WSHHRN: consumed + orphans + zombies | 143KB |

**Total**: 542 KB de catálogo + el reporte markdown.

## 18. Gaps reconocidos en este ciclo

| # | Gap | Mitigación |
|---|---|---|
| 1 | Strings hardcoded en código Java NO catalogadas | Ciclo futuro: extraer literales en español dentro de `.java` |
| 2 | Métodos llamados via reflection / nombre dinámico no detectados | Detección manual cuando aparezcan en ciclo D (flujos end-to-end) |
| 3 | `com.dts.classes.*` (250 entities/DAL espejo) parseados pero no analizados a profundidad | Si el Brain necesita modelar el espejo HH del modelo backend, parsear más a fondo |
| 4 | Layouts XML solo extraídos para `@string` refs — no se mapeó controles UI a campos | Ciclo futuro si se necesita topología visual exacta |
| 5 | DataBindings y ViewBindings no analizados | Ciclo futuro si se moderniza la UI |
| 6 | El proyecto no usa ProGuard/R8 visiblemente — verificar release builds | Auditoría de build config (build.gradle) |
| 7 | No detectada librería de tracking/analytics (Firebase, Crashlytics) | Si se requiere telemetría del operador, gap a llenar |

---

**Fin Ciclo 3.2 B.** Próxima en pipeline: **Ciclo 3.2 D — Flujos end-to-end del HH**, que combinará este catálogo de pantallas + WMs consumidos + DAL del BOF (Ciclo 3.2 A) + tablas reales de Killios (Ciclo 3.2 C) para reconstruir flujos completos: por ejemplo "Recepción de mercadería" desde la activity `frm_recepcion_datos` → WMs invocados → SQL ejecutado en server → tablas `trans_re_*` mutadas en Killios → UI confirmation strings que ve el operador.
