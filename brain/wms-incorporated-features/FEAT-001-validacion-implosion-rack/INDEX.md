# FEAT-001 — Feature #AG29042026: validacion previa de implosion + orquestador unificado cambio estado/ubicacion HH

> **Renombrado 2026-04-30**: este archivo nacio como `CP-016` en
> `debuged-cases/`. Con la taxonomia nueva (ver `agent-context/TAXONOMIA.md`),
> los features incorporados pendientes de validacion cross-cliente viven
> en `wms-incorporated-features/`. Se renombro a **FEAT-001** y se
> conservo el contenido tal cual. La carpeta vieja
> `debuged-cases/CP-016-feature-AG29042026-validacion-implosion-rack/`
> queda vacia en GitHub.

**Tipo**: Feature INCORPORADO en `dev_2028_merge`, requiere VALIDACION CROSS-CLIENTE antes de promover a PRD.

**Estado**: en analisis. Pasó pruebas unitarias en BD del dev (cliente no especificado). NO hay validacion en MAMPA, IDEALSA, MERCOPAN, MERHONSA, KILLIOS, BYB, BECO, CEALSA todavia.

**Fecha de incorporacion**: 2026-04-29 (commits a `dev_2028_merge`).

**Identificadores humanos en el codigo**:
- `#AG29042026` — Abigail Alvarado, commit BOF (push a azure)
- `#AG17042026` — Abigail Alvarado, commits HH (push a azure)
- `#EJC20260416` — Erik Calderón, autor del orquestador y comentarios extensos
- `#MA20260415` — Marcela1306, refactor `Aplica_Implosion`
- `#MA20260427` — Marcela1306, regla rack en orquestador interno
- `#MA20260326` — Marcela1306, header del WS con validacion rack + implosion automatica

**Atribucion correcta** (corrige el reporte original que solo mencionaba a Abigail):
El feature es trabajo COLABORATIVO Erik+Marcela+Abigail. Abigail commiteó la version final pero el código tiene tags de los 3. Importante reconocerlo para revisiones futuras y para git blame.

**Nota sobre apellido**: el reporte de Erik decia "Abigail Gaitan". El git author real es **Abigail-Alvarado** (probable confusion de apellido). El identificador `#AG29042026` es valido en cualquier caso.

---

## 0. Resumen ejecutivo (3 lineas)

Se incorporó en `dev_2028_merge` (2026-04-29) un nuevo flujo unificado HH para cambio de estado / cambio de ubicacion / implosion, con validacion previa de licencias (origen y destino deben tener misma ubicacion + mismo estado) y soporte de "rack" (ubicacion con estado por defecto). El feature se pidio para IDEALSA/MERHONSA/MERCOPAN pero al estar en 2028 se propaga a TODOS los clientes que reciban esa rama. Pasó pruebas en BD del dev pero requiere validacion cross-cliente (queries de pre-flight + casos golden por cliente) antes de promover a PRD.

---

## 1. Commits reales involucrados

El reporte original mencionaba solo `b8ae38a5`. La realidad es 2 commits en BOF + 3 en HH:

### TOMWMS_BOF (rama `dev_2028_merge` unicamente — NO en `dev_2023_estable`)

| SHA | Fecha | Autor | Mensaje | Archivos | Comentario |
|---|---|---|---|---:|---|
| `b8ae38a577581779a2eb5b8dc7f1fd12a1f0c57e` | 2026-04-29 | Abigail-Alvarado | `#AG29042026` | **84** | **NO ATOMICO** — mezcla feature de implosion con cambios RFID, frmCliente, frmMenu, frmAjusteStock, frmPicking, frmPreFactura. Ver §1.1. |
| `5438827d1d643e278126595047f075bf303ca393` | 2026-04-29 | Abigail-Alvarado | `#AG2904 Cambios` | 13 | Mucho mas enfocado: archivos del DAL HH y WS. Probable que sea este el commit "real" del feature. |

### TOMHH2025 (rama `dev_2028_merge`)

| SHA | Fecha | Autor | Mensaje | Archivos clave |
|---|---|---|---|---|
| `010d589bef202334049edd5fbee6fb69125eb3eb` | 2026-04-29 | Abigail-Alvarado | `#AG17042026 validaciones para que los parametros de cambio de ubicación no afect[en]` | `frm_cambio_ubicacion_ciega.java` |
| `9b5d3e1d812678707615a3f040583d17ec817a0b` | 2026-04-29 | Abigail-Alvarado | `#AG17042026 validaciones` | `frm_cambio_ubicacion_ciega.java`, `frm_Packing.java`, `clsBeProducto_estado.java` |
| `2678ee1f6e49b506805595b224310bf510e21203` | 2026-04-30 | Carolina Fuentes | `#CKFK20260430 Enviando cambios de Marcela y Abigail` | `appGlobals.java`, `frm_cambio_ubicacion_ciega.java`, `build.gradle`, otros |

### 1.1 Riesgo: el commit `b8ae38a5` es un mamut

84 cambios en un solo commit, mezclando 6 features distintos. Esto:
- Hace IMPOSIBLE el cherry-pick limpio a `dev_2023_estable_hotfix_*` (arrastra todo lo demas).
- Dificulta el `git revert` si se detecta regresion (revierte 6 features juntos).
- Incluye basura: `frmImportarAjusteExcel.vb.bak_20260424_151205` (backup commiteado), `frmPreFactura - Copia.vb` (copia de archivo), `.}` (archivo huerfano eliminado).
- Modifica el form `frmPicking.vb` (272 KB) sin documentacion del cambio — riesgo de regresion en el flujo principal de picking de TODOS los clientes.

**Recomendacion**: para portear el feature a 2023 (caso hotfix), hay que extraer manualmente los archivos del feature (los 13 del commit `5438827d` + los 3 de HH), no hacer cherry-pick directo de `b8ae38a5`. Cola C-014 abajo.

---

## 2. Que cambia funcionalmente (puntos del reporte de Erik, refinados con el codigo)

| # | Reporte de Erik | Realidad confirmada en codigo |
|:-:|---|---|
| 1 | "Se ajustó el flujo de cambio de ubicación/cambio de estado para usar `Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack`" | ✅ Confirmado. Es `Public Shared Function` en `clsLnTrans_ubic_hh_det_Partial.vb` línea 2065. WS wrapper en `WSHHRN/TOMHHWS.asmx.vb` línea 19285. |
| 2 | "Se agregó parámetro `EsCambioEstado` desde Android" | ✅ Confirmado. HH manda `gl.modo_cambio == 2` como flag (`frm_cambio_ubicacion_ciega.java` línea 1854). VB lo recibe como `Optional Boolean = False`. Para licencia completa, NO viene del HH: el server lo infiere de `IdEstadoOrigen <> IdEstadoDestino` (`TOMHHWS.asmx.vb` línea 19432). |
| 3 | "Validación previa para implosión: licencias, misma ubicación, mismo estado" | ✅ Sub `Validar_Implosion_MismaUbicacionEstado` línea 1982-2048. Ver §3.1 para detalle. |
| 4 | "Si las licencias tienen distinta ubicación, bloquea" | ✅ Línea 2036-2040: `Throw New Exception("No se puede implosionar, ubicaciones diferentes...")`. |
| 5 | "Si las licencias tienen distinto estado, bloquea" | ✅ Línea 2042-2046: `Throw New Exception("No se puede implosionar, estados diferentes...")`. |
| 6 | "Si misma ubicación + mismo estado, aplica correctamente" | ✅ Pasa por las 6 validaciones secuenciales y continúa al `Aplica_Implosion`. |
| 7 | "Flujo: estado → implosión → ubicación" | ✅ Confirmado. Comentarios `'#EJC20260416` líneas 2055-2057, 2208 (PASO 1 estado), 2253 (PASO 2 implosión), después PASO 3 ubicación. |
| 8 | "Si la implosión falla, no continúa al cambio de ubicación" | ✅ Después de cada paso hay `If Not exitoPaso Then Throw New Exception(...)` que aborta toda la transacción. |
| 9 | "En Android no se cambió la lógica principal de captura, solo se conectó el flujo" | ✅ El cambio en `frm_cambio_ubicacion_ciega.java` es: línea 1848 `case 29` ahora invoca `Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack` (antes invocaba el método sin `_ConValidacionRack`), y línea 1857 `case 30` invoca `_LicCompleta_ConValidacionRack`. |
| 10 | "Flujo licencia completa/mixta usa el método con validación correspondiente" | ✅ `Aplica_Cambio_Estado_Ubic_HH_LicCompleta_ConValidacionRack` en `TOMHHWS.asmx.vb` línea 19416. |

---

## 3. Mecánica del código (cita líneas exactas, rama `dev_2028_merge`)

### 3.1 Sub `Validar_Implosion_MismaUbicacionEstado`

**Archivo**: `TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det/clsLnTrans_ubic_hh_det_Partial.vb` líneas 1982-2048.

Validaciones secuenciales (devuelve void, lanza Exception en fallo):

```
1. pLicenciaOrigen vacia?    -> "licencia origen vacia"
2. pLicenciaDestino vacia?   -> "licencia destino vacia"
3. Get_Stock_Implosion_By_LicPlate(origen)  -> stockOrigen
4. Get_Stock_Implosion_By_LicPlate(destino) -> stockDestino
5. stockOrigen == null?      -> "no se encontro stock licencia origen"
6. stockDestino == null?     -> "no se encontro stock licencia destino"
7. stockOrigen.IdUbicacion != stockDestino.IdUbicacion?
       -> "ubicaciones diferentes. Origen: X, destino: Y"
8. stockOrigen.IdProductoEstado != stockDestino.IdProductoEstado?
       -> "estados diferentes. Origen: X, destino: Y"
```

Mensajes incluyen `Nombre_Completo` de la ubicación y `NomEstado` del estado (legibles para el operador HH). Si los nombres vienen vacíos, muestra el ID como fallback (líneas 2020-2034).

**Llamada desde**:
- `Aplica_Implosion_LP_Stock(...)` con `Optional ValidarImplosionDirecta As Boolean = False` línea 621 → si flag true, invoca la validación línea 647.
- `Aplica_Implosion(...)` (refactor `'#MA20260415`) línea 1278 → invoca la validación línea 1300.

### 3.2 Función orquestadora `Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack`

**Archivo**: `clsLnTrans_ubic_hh_det_Partial.vb` líneas 2065-2475.

**Firma**:
```vbnet
Public Shared Function Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack(
    ByVal pMovimiento As clsBeTrans_movimientos,
    ByVal pStockRes As clsBeVW_stock_res,
    ByRef pIdStockNuevo As Integer,
    ByRef pIdMovimientoNuevo As Integer,
    ByVal pPosiciones As Integer,
    Optional ByVal EsCambioEstado As Boolean = False) As Boolean
```

**Lógica de flags** (líneas 2112-2197):

| Flag | Cuándo se prende |
|---|---|
| `requiereCambioEstado` | (`tieneEstadoDestino AND IdProductoEstadoDestino <> IdProductoEstadoOrigen`) OR `EsCambioEstado=True` OR (NO `EsCambioEstado` AND `esRack` AND `estadoRackDefecto > 0` AND estado origen ≠ estado defecto rack) |
| `requiereImplosion` | NO `EsCambioEstado` AND `tieneLicenciaDestino` AND licencia destino ≠ licencia origen |
| `requiereCambioUbicacion` | NO `EsCambioEstado` AND `IdUbicacionDestino > 0` AND `IdUbicacionDestino <> IdUbicacionOrigen` |

**Seguro final** (línea 2194-2197):
```vbnet
If EsCambioEstado Then
    requiereImplosion = False
    requiereCambioUbicacion = False
End If
```
→ Cuando viene de cambio estado, el flujo SOLO ejecuta cambio estado, garantizando que la implosión y la ubicación no se mezclen.

**Pasos** (en orden estricto):
- PASO 1 — Cambio Estado (líneas 2208-2249): invoca `Aplica_Cambio_Estado_Ubic` con `IdTipoTarea=3`, mantiene la ubicación origen, después actualiza `pStockRes.IdProductoEstado` para que el siguiente paso tenga el contexto correcto.
- PASO 2 — Implosión (líneas 2251 en adelante): invoca `Aplica_Implosion` con la licencia destino. Si falla, aborta la transacción entera.
- PASO 3 — Cambio Ubicación: invoca `Aplica_Cambio_Estado_Ubic` con la ubicación destino. Mismo patrón.

### 3.3 Versión interna `_Interno`

**Archivo**: misma archivo, líneas 2479-2719.

`Private Shared Function Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack_Interno(...)` con `EsCambioEstado` ahora REQUERIDO (no opcional). Recibe `lConnection` y `lTransaction` ya abiertos para reutilizarse dentro de un flujo padre. Tiene una validación de rack adicional en líneas 2593-2627 que la versión pública no tiene (`'#MA20260427`).

### 3.4 Wrapper para licencia completa

**Archivo**: `WSHHRN/TOMHHWS.asmx.vb` líneas 19416-19490.

```vbnet
<WebMethod(), SoapHeader("mArch")>
Public Function Aplica_Cambio_Estado_Ubic_HH_LicCompleta_ConValidacionRack(
    ByVal pStockResList As List(Of clsBeVW_stock_res)) As Boolean
```

**Diferencia clave**: `EsCambioEstado` se calcula del lado server (línea 19432):
```vbnet
Dim EsCambioEstado As Boolean = (pStockResList.FirstOrDefault.Movimiento.IdEstadoOrigen
                                <> pStockResList.FirstOrDefault.Movimiento.IdEstadoDestino)
```

Esto significa que para licencia completa el HH NO necesita saber si es cambio de estado o de ubicación — el server lo infiere. Es ASIMETRICO con respecto al wrapper unitario. Para licencia mixta (escaneo línea por línea) el HH SI manda el flag explícito. Cola C-013.

### 3.5 Lado HH

**Archivo**: `app/src/main/java/com/dts/tom/Transacciones/CambioUbicacion/frm_cambio_ubicacion_ciega.java` líneas 1848-1858.

```java
case 29:
    callMethod("Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack",
            "pMovimiento", gMovimientoDet,
            "pStockRes", vStockRes,
            "pIdStockNuevo", vIdStockNuevo,
            "pIdMovimientoNuevo", vIdMovimientoNuevo,
            "pPosiciones", vPosiciones,
            "EsCambioEstado", gl.modo_cambio == 2);
    break;
case 30:
    callMethod("Aplica_Cambio_Estado_Ubic_HH_LicCompleta_ConValidacionRack",
            "pStockResList", stockList);
    break;
```

**Regla de negocio**: `gl.modo_cambio == 2` significa "cambio de estado". Verificar en `appGlobals.java` qué otros valores usa `modo_cambio` (cola C-015).

Método `Crear_Movimiento_Ubicacion_ND(boolean EsCambioEstado)` línea 4929 setea:
- `IdTipoTarea = 3` si EsCambioEstado
- `IdTipoTarea = 2` si cambio ubicación normal
- `IdTipoTarea = 20` si explosión manual

---

## 4. Riesgos cross-cliente concretos

### 4.1 Dependencia de columna `es_rack` en `bodega_ubicacion`

**Línea 2099** (público) y **2506** (interno):
```vbnet
esRack = CBool(row("es_rack"))
```
Si la BD del cliente no tiene la columna o viene NULL, `CBool(NULL)` lanza `InvalidCastException`. **Killios, BYB, BECO, CEALSA**: hay que confirmar que la columna existe y está populada.

**Verificación SQL** (correr READ-ONLY contra cada BD):
```sql
SELECT 'es_rack' AS col,
       CASE WHEN EXISTS (
            SELECT 1 FROM sys.columns
            WHERE object_id = OBJECT_ID('bodega_ubicacion') AND name = 'es_rack')
       THEN 'EXISTE' ELSE 'FALTA' END AS estado;

-- Si existe, contar nulls vs valores
SELECT COUNT(*) AS total,
       SUM(CASE WHEN es_rack IS NULL THEN 1 ELSE 0 END) AS nulls,
       SUM(CASE WHEN es_rack = 1 THEN 1 ELSE 0 END) AS racks,
       SUM(CASE WHEN es_rack = 0 THEN 1 ELSE 0 END) AS no_racks
FROM bodega_ubicacion;
```

### 4.2 Dependencia del SP `Get_Estado_Defecto_Rack`

**Línea 2104** (público) y **2511** (interno):
```vbnet
Dim estadoRackDefecto As Integer = clsLnBodega.Get_Estado_Defecto_Rack(...)
```
Si el SP no existe en la BD del cliente o no devuelve un Integer, falla todo el flujo. Verificar:
```sql
SELECT name FROM sys.procedures WHERE name LIKE '%Estado_Defecto_Rack%';
SELECT name FROM sys.objects WHERE type IN ('FN','IF','TF') AND name LIKE '%Estado_Defecto_Rack%';
```

### 4.3 Dependencia de columnas `Nombre_Completo` y `NomEstado` en `vw_stock_res` / `vw_stock_implosion`

**Líneas 2020-2034** del Validar_Implosion: lee `stockOrigen.Nombre_Completo` y `stockOrigen.NomEstado`. Si la vista no tiene esas columnas, el getter de la entity tira `MissingFieldException`.

```sql
SELECT name FROM sys.columns
WHERE object_id IN (OBJECT_ID('vw_stock_res'), OBJECT_ID('vw_stock_implosion'))
  AND name IN ('Nombre_Completo', 'NomEstado', 'IdProductoEstado', 'IdUbicacion');
```

### 4.4 Cambio de contrato del web service

El método antiguo `Aplica_Cambio_Estado_Ubic_HH` (sin `_ConValidacionRack`) puede o no haberse mantenido. Si se eliminó:
- HHs antiguos (apk previas) que sigan llamando al método viejo van a fallar con SOAP fault.
- Las versiones de apk en producción de Killios/BYB/CEALSA deben revisarse.

```bash
# Grep en el WSHHRN para confirmar si el metodo antiguo sigue
rg "Aplica_Cambio_Estado_Ubic_HH\\(" /tmp/wms-azure-snippets/AG29042026/BOF_TOMHHWS.asmx.vb
```

Si NO aparece sin `_ConValidacionRack`: el contrato cambió y se rompe compatibilidad con apks viejas. Cola C-016.

### 4.5 `frmPicking.vb` modificado en el mismo commit

El commit `b8ae38a5` modificó `frmPicking.vb` (272 KB) sin que el reporte de Abigail mencione picking. Es ortogonal al feature pero entra en el merge. **Riesgo de regresión en el flujo principal de picking de todos los clientes**.

Acción: diff `frmPicking.vb` entre `dev_2023_estable` y `dev_2028_merge` para identificar los cambios y descartar regresiones. Cola C-017.

### 4.6 Bitácora `PARCHES_APLICADOS.md` no incluye AG29042026

Última entrada del archivo: v23 del 25-04. Falta agregar:
```markdown
| 2026-04-29 XX:XX | AG29042026 | feature implosion + orquestador rack | manual | ✅ aplicado | Validacion previa licencias + EsCambioEstado |
```

Cola C-018 (lo agrega Erik en su próximo trabajo, no lo hago yo en el brain porque es archivo del repo de Erik no del brain).

### 4.7 Estado por cliente (matriz)

| Cliente | BD | Rama PRD actual | Pidió feature | Recibirá feature al subir 2028 a PRD | Riesgo |
|---|---|:-:|:-:|:-:|---|
| Killios | TOMWMS_KILLIOS | 2023 | NO | Sí, cuando 2028 entre a PRD | ALTO — corre flujos críticos hoy, cualquier regresión rompe operación |
| BYB | TOMWMS_BYB | 2023 | NO | Sí | MEDIO |
| BECO | TOMWMS_BECO | 2023 | NO | Sí | BAJO (poco volumen) |
| CEALSA | TOMWMS_CEALSA | 2023 | NO | Sí | BAJO (poco volumen) |
| MERCOPAN | TOMWMS_MERCOPAN | 2023 | SÍ | Sí | BAJO (esperan el feature) |
| MAMPA | TOMWMS_MAMPA | 2028 QA | implícito (testbed) | YA | LOW — testbed |
| IDEALSA | n/a | n/a | SÍ | Sí | BAJO (esperan el feature) |
| MERHONSA | n/a | n/a | SÍ | Sí | BAJO (esperan el feature) |

---

## 5. Plan de validación cross-cliente

### 5.1 Pre-flight checks (correr ANTES de promover 2028 a PRD)

Las queries de §4.1, §4.2, §4.3 contra cada una de las 8 BDs. Si alguna falla, no promover sin parche compensatorio.

### 5.2 Casos golden (ejecutar en cada BD QA después de pasar pre-flight)

```
GOLD-AG-01: Implosión válida (misma ubicación + mismo estado)
  Dado: LP-A en UBIC1 estado=Disponible / LP-B en UBIC1 estado=Disponible
  Cuando: HH manda implosión LP-A -> LP-B
  Entonces: pasa, se ejecuta Aplica_Implosion, stock LP-A se mueve a LP-B

GOLD-AG-02: Implosión bloqueada por ubicación distinta
  Dado: LP-A en UBIC1 / LP-B en UBIC2
  Cuando: HH manda implosión LP-A -> LP-B
  Entonces: bloquea con "ubicaciones diferentes. Origen: UBIC1, destino: UBIC2"
  NO se modifica stock

GOLD-AG-03: Implosión bloqueada por estado distinto
  Dado: LP-A en UBIC1 estado=Disponible / LP-B en UBIC1 estado=Cuarentena
  Cuando: HH manda implosión LP-A -> LP-B
  Entonces: bloquea con "estados diferentes"

GOLD-AG-04: Cambio de estado puro (EsCambioEstado=true)
  Dado: stock con estado origen X
  Cuando: HH modo_cambio=2 manda cambio a estado Y, sin licencia destino
  Entonces: se ejecuta SOLO PASO 1 (cambio estado), no implosión ni ubicación
  Verificar trans_movimientos con IdTipoTarea=3

GOLD-AG-05: Cambio de ubicación a rack con estado diferente
  Dado: stock estado=X en UBIC-NORMAL / UBIC-RACK con estado_defecto=Y
  Cuando: HH manda cambio ubicación a UBIC-RACK
  Entonces: el sistema fuerza cambio estado a Y antes del cambio ubicación
  Verificar 2 movimientos: tipo 3 (estado) + tipo 2 (ubicación)

GOLD-AG-06: Bodega sin columna es_rack o sin SP Get_Estado_Defecto_Rack
  Dado: cliente con BD que no tiene la infraestructura rack
  Cuando: HH ejecuta cualquier flujo
  Entonces: el sistema debe tener fallback (devolver es_rack=false, estadoRackDefecto=0)
           NO debe lanzar InvalidCastException ni MissingProcedure
  Si falla: bloquea promoción a PRD del cliente

GOLD-AG-07: Compatibilidad con APKs viejas
  Dado: HH con apk anterior que llama a Aplica_Cambio_Estado_Ubic_HH (sin _ConValidacionRack)
  Cuando: la apk vieja se conecta al WS nuevo
  Entonces: o bien el método viejo sigue existiendo (compatibilidad), o bien el HH viejo recibe SOAP fault gestionable
  NO debe quedar el operador sin poder operar
```

### 5.3 Replay con datos reales

Una vez pasado §5.2 en QA por cliente, ejecutar 50-100 movimientos del último mes contra el código nuevo en QA y comparar con el resultado real en PRD. Diferencias cero = OK para promover.

---

## 6. Intersección con CP-013 (bug `dañado_picking`)

El feature toca `clsLnTrans_ubic_hh_det_Partial.vb` (DETALLE del flujo HH).
El bug `dañado_picking` está en `clsLnTrans_ubic_hh_enc_Partial.vb` (ENCABEZADO del flujo HH).

Son archivos hermanos del mismo módulo, con responsabilidades distintas. El feature de AG **NO repara ni agrava** el bug `dañado_picking`:
- No modifica los 6 setters de `Dañado_picking=True` documentados en `traza-002`.
- No modifica el bloque comentado `clsLnStock_res_Partial.vb 1998-2008` que es el otro intento de fix.
- Es ortogonal.

**PERO** son flujos vecinos. Cuando se aplique el fix del CP-013/PLAYBOOK-FIX (descomentar el bloque + agregar generación de `trans_movimientos` compensatorio), hay que verificar que NO interfiera con el orquestador nuevo de AG. En particular:
- Si `Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack` invoca internamente `Aplica_Cambio_Estado_Ubic` (línea 2225) y este último termina llamando a `clsLnStock_res.Update_Reemplazo_*` (donde está el setter de `Dañado_picking`), el fix del CP-013 se ejecuta dentro del orquestador del feature AG.
- Validar que la generación del `trans_movimientos` del fix CP-013 no se duplica con la generación del PASO 1/2/3 del orquestador AG.

Cola C-019 (cuando CP-013 entre en implementación, validar interacción con feature AG).

---

## 7. Archivos involucrados (con tamaños y líneas exactas)

Snapshots locales en `/tmp/wms-azure-snippets/AG29042026/` (ephemerals, no commiteados):

| Archivo | Tamaño | Líneas relevantes |
|---|---:|---|
| `BOF_clsLnTrans_ubic_hh_det_Partial.vb` | 137 KB | 1982-2048 (Validar_Implosion), 2065-2475 (orquestador público), 2479-2719 (orquestador interno) |
| `BOF_TOMHHWS.asmx.vb` | 855 KB | 19283-19339 (wrapper unitario), 19416-19490 (wrapper licencia completa) |
| `BOF_clsLnVW_stock_res.vb` | 78 KB | revisar `Get_Stock_Implosion_By_LicPlate` (signature, columnas devueltas) |
| `BOF_frmPicking_2028.vb` | 272 KB | cambios no documentados, diff vs 2023 (cola C-017) |
| `BOF_PARCHES_APLICADOS.md` | 2.8 KB | bitácora Erik, falta entrada AG29042026 (cola C-018) |
| `HH_frm_cambio_ubicacion_ciega.java` | 216 KB | 1848-1858 (callMethod cases 29 y 30), 4929-5050 (Crear_Movimiento_Ubicacion_ND) |
| `HH_frm_Packing.java` | 120 KB | revisar uso de los nuevos métodos WS |
| `HH_clsBeProducto_estado.java` | 6 KB | revisar nuevas propiedades |

---

## 8. Bloqueantes y colas abiertas

| Cola | Descripción | Bloquea promoción a |
|---|---|---|
| C-012 | Pre-flight `es_rack` (§4.1) en 8 BDs | TODOS los clientes |
| C-013 | Documentar asimetría `EsCambioEstado` (HH vs server) | desarrollo continuo |
| C-014 | Plan de extracción quirúrgica del feature desde `b8ae38a5` (84 changes) si hay que hotfix a 2023 | Killios PRD si urge |
| C-015 | Auditar `gl.modo_cambio` valores válidos en `appGlobals.java` | desarrollo continuo |
| C-016 | Verificar si `Aplica_Cambio_Estado_Ubic_HH` (sin `_ConValidacionRack`) sigue existiendo para apks viejas | TODOS |
| C-017 | Diff `frmPicking.vb` entre 2023 y 2028 — descartar regresión picking | TODOS |
| C-018 | Agregar entrada AG29042026 a `PARCHES_APLICADOS.md` | bitácora interna |
| C-019 | Validar interacción CP-013 fix vs feature AG cuando se implemente CP-013 | promoción del fix CP-013 |

---

## 9. Cross-refs

- Bug del flujo hermano: `brain/debuged-cases/CP-013-killios-wms164/PLAYBOOK-FIX.md`
- Trace base que ayuda a entender el flujo HH: `brain/code-deep-flow/traza-002-danado-picking.md`
- Patrón de doble rama (2028 estabilización vs 2023 productivo) y por qué 2028 es el target principal: `brain/debuged-cases/CP-013-killios-wms164/PLAYBOOK-FIX.md` §H

## 10. URLs Azure DevOps

### Commits

- BOF #AG29042026 (mamut, 84 changes):
  https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF/commit/b8ae38a577581779a2eb5b8dc7f1fd12a1f0c57e
- BOF #AG2904 (real del feature, 13 changes):
  https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF/commit/5438827d1d643e278126595047f075bf303ca393
- HH #AG17042026 frm_cambio_ubicacion_ciega:
  https://dev.azure.com/ejcalderon0892/TOMHH2025/_git/TOMHH2025/commit/010d589bef202334049edd5fbee6fb69125eb3eb
- HH #AG17042026 validaciones (3 archivos):
  https://dev.azure.com/ejcalderon0892/TOMHH2025/_git/TOMHH2025/commit/9b5d3e1d812678707615a3f040583d17ec817a0b
- HH Carolina (envío):
  https://dev.azure.com/ejcalderon0892/TOMHH2025/_git/TOMHH2025/commit/2678ee1f6e49b506805595b224310bf510e21203

### Archivos clave (rama dev_2028_merge)

- clsLnTrans_ubic_hh_det_Partial.vb:
  https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF?path=/TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det/clsLnTrans_ubic_hh_det_Partial.vb&version=GBdev_2028_merge
- TOMHHWS.asmx.vb (web service):
  https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF?path=/WSHHRN/TOMHHWS.asmx.vb&version=GBdev_2028_merge
- frm_cambio_ubicacion_ciega.java:
  https://dev.azure.com/ejcalderon0892/TOMHH2025/_git/TOMHH2025?path=/app/src/main/java/com/dts/tom/Transacciones/CambioUbicacion/frm_cambio_ubicacion_ciega.java&version=GBdev_2028_merge
- PARCHES_APLICADOS.md (bitácora Erik, falta entrada):
  https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF?path=/PARCHES_APLICADOS.md&version=GBdev_2028_merge
