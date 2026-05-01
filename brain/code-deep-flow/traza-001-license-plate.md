---
id: traza-001-license-plate
tipo: code-deep-flow
estado: vigente
titulo: Traza 001 — License Plate (LP)
tags: [code-deep-flow]
---

# Traza 001 — License Plate (LP)

**Parámetro fuente**: `i_nav_config_enc.genera_lp` + `producto.genera_lp_old` + `producto.genera_lote` + `producto_presentacion.genera_lp_auto`.

**Por qué es la traza fundacional**: el LP es el switch que define si el producto se mueve por bulto/agrupación o por unidades sueltas. Define cómo se interpretan todos los demás flags downstream (control_lote, control_vencimiento, control_peso, serializado, IdTipoRotacion). Sin entender LP no se puede entender el resto.

---

## 0. Resumen ejecutivo y hallazgos brutales

### 0.1 Cuatro flags interrelacionados (modelo triple)

| Flag | Tabla | Tipo | Semántica observada |
|---|---|---|---|
| `genera_lp` | `i_nav_config_enc` | bit | Switch global de la integración: la bodega/empresa opera con LP |
| `genera_lp_old` | `producto` | bit | Capability legacy a nivel producto: este producto admite LP en su versión vieja del flujo |
| `genera_lote` | `producto` | bit | Capability a nivel producto: el sistema genera lotes propios (no ERP) cuando recibe |
| `genera_lp_auto` | `producto_presentacion` | bit | A nivel presentación: la HH autogenera el LP por correlativo (vs ingreso manual) |

**`producto_presentacion1` (variante histórica) duplica `genera_lp_auto`** — confirmar si es tabla muerta.

### 0.2 Invariante de coherencia (lo que Erik mencionó)

Si `producto.<capability>=True`, **todas** las transacciones derivadas deben arrastrar el dato:
- `control_peso=True` → todo `stock_rec.peso`, `stock.peso`, `trans_movimientos.peso` debe estar poblado.
- `control_lote=True` → todo `stock_rec.lote`, `stock.lote`, `trans_movimientos.lote` debe estar poblado.
- `control_vencimiento=True` → ídem `fecha_vence`.
- `IdPresentacion` (presentación) → ídem `IdPresentacion` propagado.
- `genera_lp=True` (bodega) + `genera_lp_old=True` (producto) → todo `stock.lic_plate`, `stock_rec.lic_plate`, `trans_movimientos.lic_plate` debe estar poblado.

**El WebAPI .NET 10 nuevo debe garantizar este invariante en su capa de servicios** — la base actual lo deja al criterio de cada caller (HH / BOF / sync ERP).

### 0.3 Hallazgos por hilo

| Hilo | Hallazgo |
|---|---|
| **BOF** | Modelo dual confirmado: legacy `clsLnProducto.vb` + `clsLnProducto_Partial.vb` (DAL VB) vs Core nuevo `WMS.DALCore/Producto/clsLnProducto.cs`. Ambos tocan `genera_lp_old`+`genera_lote`. Forms `frmRecepcion.vb`, `frmCapturaParametroRecepcionS.vb`, `frmProducto.vb` administran y consumen el flag. |
| **WSHHRN** | 21 endpoints SOAP LP-específicos en `TOMHHWS.asmx.vb` (374 funciones públicas totales). El correlativo se obtiene vía `Get_Nuevo_Correlativo_LicensePlate(_S)`. La autorización por operador-bodega vive en `resolucion_lp_operador`. |
| **WMSWebAPI** | **CERO migración LP**. 25 controllers + 16 services REST cubren maestros, KPI, sync ERP, auth. Operación HH (LP / picking / despacho / recepción) sigue 100% en SOAP/WSHHRN. |
| **HH** | `clsBeProducto.Genera_lp` + `clsBeProducto_Presentacion.Genera_lp_auto` viajan en cada respuesta. `frm_recepcion_datos.java` muestra/oculta `lblLPlate` y `txtLicPlate` según el flag de presentación, y dispara `execws(8)` (correlativo) si el producto tiene `Genera_lp=True`. |
| **DB** | El LP **no es entidad propia**. Es un `nvarchar(100)` que viaja en cols `lic_plate` (stock, stock_rec, stock_res, stock_jornada, faltantes, trans_movimientos, trans_re_det) + `lp_origen`/`lp_destino` (trans_movimiento_pallet) + `barra_pallet` (trans_movimientos). Configuración de longitud y prefijo en `configuracion_barra_pallet`. Correlativos en `resolucion_lp_operador`. |
| **ERP** | `DynamicsNavInterface/WebReference` expone `Get_Nuevo_Correlativo_LicensePlate(_S)` — sugiere que el correlativo puede consumirse desde NAV. Pendiente confirmar para qué clientes (BECO probablemente, BYB usa wsBYBNav* en línea). |

### 0.4 Issues de seguridad descubiertos en este pase (no parte del flujo LP, anotados aparte)

- **Q-SEC-OPENAI-KEY-LEAK**: `WSHHRN/ChatGPTService.vb` línea 9 tiene una API key OpenAI hardcodeada en código fuente (visible, comprometida, debe rotarse).
- **Q-SEC-CONNINI-CREDS**: `WSHHRN/Conn.ini` y variantes por cliente almacenan credenciales SQL Server (usuario `sa` + password en claro). Modelo de despliegue por cliente requiere repensar — ver Q-WMS-EXE-CONFIG-EN-WSHHRN.

---

## 1. Modelo conceptual

### 1.1 ¿Qué es un LP?

Un **License Plate** es una etiqueta única (string `nvarchar(100)`) que identifica una unidad física de manejo (pallet, caja, bulto agrupador). Sirve para:
- Mover stock atómicamente sin re-leer cada producto.
- Trazabilidad: saber qué LP entró cuándo, dónde está, qué contiene.
- Picking/Despacho: pickear "el LP X" en vez de "10 unidades del producto Y".

### 1.2 Anatomía del LP en TOMWMS

```
Configuración        ────► configuracion_barra_pallet (1 row por cliente)
                              ├─ LongLP=7              (longitud del correlativo)
                              ├─ LongCodBodegaOrigen=3
                              ├─ LongCodProducto=6
                              ├─ CodigoNumerico=True
                              └─ IdentificadorInicio="$  "

Autorización         ────► resolucion_lp_operador (rangos por operador+bodega+serie)
                              ├─ idoperador=1, idbodega=1, serie='01'
                              ├─ correlativo_inicial=1
                              ├─ correlativo_final=99999
                              └─ correlativo_actual=NN  ← se incrementa en cada Get_Nuevo

Generación HH        ────► WSHHRN.Get_Nuevo_Correlativo_LicensePlate(_S)
                              ├─ valida resolución activa del operador
                              ├─ incrementa correlativo_actual
                              └─ devuelve string LP listo para etiquetar/imprimir

Almacenamiento       ────► stock.lic_plate / stock_rec.lic_plate / stock_res.lic_plate
                              + uds_lic_plate (cuántas unidades hay en ese LP)
                              + pallet_no_estandar (bit: pallet armado a mano vs estándar)

Movimiento           ────► trans_movimientos.lic_plate          (qué LP se movió)
                              + barra_pallet                    (código de barra impreso)
                          + trans_movimiento_pallet.lp_origen / lp_destino
                              (consolidación / unión / split)
```

### 1.3 Modelo dual `genera_lp` vs `genera_lp_old`

Hipótesis (a validar con Erik): cuando se introdujo el flag a nivel **bodega** (`i_nav_config_enc.genera_lp`), se renombró el flag a nivel **producto** a `genera_lp_old` para preservar la semántica histórica. Hoy:

- `i_nav_config_enc.genera_lp = True` → la integración entera opera con LP.
- `producto.genera_lp_old = True` → este producto específico admite/usa LP.

Ambos están en True para el 99% de los productos en BECO/K7/MAMPA/CEALSA. **Excepción: BYB** tiene 487/623 (78%) productos con `genera_lp_old=False` aunque `genera_lp=True` a nivel config. Esto sugiere que BYB tiene productos que no se manejan por LP aun cuando la bodega sí soporta LP — drift único cross-cliente.

### 1.4 `genera_lote` (rol distinto, no es LP)

A pesar de aparecer siempre junto a `genera_lp_old` en el DAL Core (líneas 51-54, 139-142, 230-233 de `WMS.DALCore/Producto/clsLnProducto.cs`), `genera_lote` controla cosa distinta: si el sistema **genera lotes internos** (ej: `LOTE-AUTO-NNNN`) cuando el producto se recibe sin lote del proveedor.

Distribución real:
- BECO: 72/1835 productos con `genera_lote=True` (3.9%)
- CEALSA: 21/1714 (1.2%)
- K7/MAMPA/BYB: 0%

`genera_lote` y `control_lote` son ortogonales: `control_lote=True` exige lote, `genera_lote=True` lo autogenera si no viene.

---

## 2. Hilo BOF — administración y captura

### 2.1 DAL legacy (VB.NET) — `TOMIMSV4/DAL/Mantenimientos/Producto/`

Archivos relevantes:
- `clsLnProducto.vb` (6 ocurrencias `genera_lp*`, 6 `genera_lote`)
- `clsLnProducto_Partial.vb` (12 ocurrencias `genera_lp*`, 12 `genera_lote`)
- `clsLnProducto_presentacion_Partial.vb` (6 ocurrencias `genera_lp_auto`)

El `_Partial.vb` es el patrón típico VB.NET de partial class: el archivo original se autogenera por el dataset designer y el `_Partial` agrega métodos a mano (selects custom, validaciones, etc).

### 2.2 DAL Core (.NET nuevo) — `WMS.DALCore/`

- `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs` (6 ocurrencias `genera_lp`)
  - Líneas 56-57: GetBool en SELECT
  - Líneas 130-131: SqlParameter en stored proc
  - Líneas 209-210, 334-335: Ins.Add para INSERT (dos métodos distintos de inserción)
  - Líneas 447-448: Upd.Add para UPDATE
  - Líneas 674-675: SqlParameter en select por filtro

- `WMS.DALCore/Producto/clsLnProducto.cs` (10 ocurrencias `genera_lp_old`, 8 `genera_lote`)
  - **Patrón clave**: `serializado` + `genera_lote` + `genera_lp_old` + `control_vencimiento` aparecen siempre como bloque consecutivo (líneas 51-54, 139-142, 230-233, 318-321, 421-424, 508-511, 838-841, 901-904). **El DAL Core los trata como grupo** — confirma que conceptualmente forman un perfil de capabilities del producto.

- `WMS.DALCore/Producto/clsLnProducto_presentacion.cs` (7 ocurrencias `genera_lp_auto`)

### 2.3 Forms BOF (admin) — `TOMIMSV4/`

- `Mantenimientos/Producto/frmProducto.vb` (9 ocurrencias)
  - Form principal de mantenimiento de productos. Admin marca/desmarca los flags.
- `Transacciones/Recepcion/frmRecepcion.vb` (21 ocurrencias `genera_lp*`, 6 `genera_lote`)
  - Form de recepción desde BOF (no-HH). Aplica las reglas según producto.
- `Transacciones/Recepcion/frmCapturaParametroRecepcionS.vb` (12 ocurrencias)
  - Captura parámetros adicionales en recepción (pallet, lote, vence, peso) — depende del LP.
- `Transacciones/Recepcion_BOF/frmRecepcionBOF.vb` (5 ocurrencias)
  - **Existe un segundo flujo de recepción "_BOF"** distinto del Recepcion/. Pendiente entender cuándo se usa cada uno.

### 2.4 ERP sync (BOF a NAV/SAP)

- `DynamicsNavInterface/Web References/WebReference/Reference.vb` (8 ocurrencias `genera_lp*`)
  - Proxy NAV: incluye `Get_Nuevo_Correlativo_LicensePlate` y `Get_Nuevo_Correlativo_LicensePlate_S` como operations expuestas POR NAV. Es decir, el correlativo del LP **puede pedírsele a NAV** en algunos clientes.
- `SAPSYNC_Killios/Clases Interface Sync/Producto/Presentaciones/clsSyncSAPPresentaciones.vb` (6 ocurrencias `genera_lp*`)
  - Sincronización de presentaciones desde SAP a TOMWMS para K7. Mapea el flag desde SAP.

### 2.5 Service References (proxies SOAP)

- `TOMIMSV4/Service References/wsTOMHH/Reference.vb` (10 `genera_lp*`, 10 `genera_lote`)
  - Proxy SOAP que el BOF usa para llamar al WSHHRN.
- `TestMI3/Connected Services/srvProducto/Reference.cs`, `srvProductoBodega`, `srvProducto2`, `WSHH/Reference.cs` (10-12 cada uno)
  - **Hay TRES `srvProducto*` en TestMI3 — duplicados o variantes**. Pendiente investigar qué es MI3 (Q-MI3-QUE-ES) y por qué tiene 3 referencias al mismo servicio.

---

## 3. Hilo WSHHRN — el monstruo SOAP

### 3.1 Inventario de endpoints LP

`WSHHRN/TOMHHWS.asmx.vb` tiene **374 funciones públicas** (Web Services). De ellas, 21 son LP-específicas:

#### Lectura (consulta LP)
| Línea | Endpoint | Devuelve |
|---|---|---|
| 2606 | `Get_BeProducto_By_LP_For_HH(pLic_Plate, pIdBodega, pIdPropietario, pIdProducto)` | `clsBeProducto` con todas las capabilities |
| 9697 | `Get_Pedido_A_Verificar_By_LP(pLP)` | `List(Of clsBeTrans_pe_enc)` para verificación packing |
| 12362 | `Existe_Lp(pLic_Plate, ...)` | Bool |
| 12412 | `Existe_Lp_In_Stock(pLic_Plate)` | Bool — está en stock activo |
| 12500 | `Existe_Lp_By_Licencia_And_IdBodega(pLic_Plate, pIdBodega)` | Bool — está en esa bodega específica |
| 12456 | `Existe_Lp_By_Licencia_And_IdBodega_JSON(...)` | Variante JSON (sin firma de retorno típica) |
| 8651 | `Existe_LP_By_IdRecepcionEnc_And_IdRecepcionDet(...)` | Bool en contexto recepción |
| 8607 | `Get_Licenses_Plates_By_IdRecepcionEnc(pIdRecepcionEnc)` | `List(Of clsBeLicensePlates)` |
| `Get_Stock_By_Lic_Plate(_JSON)`, `_And_Codigo`, `_And_Codigo2`, `_2` | varios | Stock contenido en un LP |
| `Get_Ubicacion_LP(...)` | clsBeUbicacion | Dónde está el LP físicamente |
| `Get_Resoluciones_Lp_By_IdOperador_And_IdBodega(...)` | `clsBeResolucion_lp_operador` | Rango activo del operador |

#### Generación / escritura
| Línea | Endpoint | Hace |
|---|---|---|
| 8465 | `Get_Nuevo_Correlativo_LicensePlate(pIdEmpresa, ...)` | Devuelve siguiente LP libre del operador, incrementa `correlativo_actual` |
| 8513 | `Get_Nuevo_Correlativo_LicensePlate_S(pIdEmpresa, ...)` | Variante "_S" — pendiente diferenciar (¿"S" = Series? ¿Con stock? ¿Simple?) |
| 5942 | `Set_LP_Stock(pMovimiento, pStockRes, pIdResolucionLp)` | Aplica un LP a un movimiento de stock |
| 6000 | `Set_LP_Stock_Mixto(pStockResList, pIdResolucionLp)` | Versión para pallet mixto (multiproducto) |

### 3.2 Contrato del Beneficiario (lo que la HH recibe)

`Get_BeProducto_By_LP_For_HH` devuelve `clsBeProducto` con todos los flags de capability:
- `Genera_lp`, `Genera_lote`, `Serializado`
- `Control_lote`, `Control_vencimiento`, `Control_peso`
- `IdTipoRotacion`
- `Presentacion` anidada (incluye `Genera_lp_auto`, `EsPallet`, `Permitir_paletizar`)

**El WSHHRN materializa la matriz triple en una sola estructura**. La HH recibe el producto con todo el perfil ya armado y decide en cliente cómo dibujar la UI.

### 3.3 Flujo de generación de LP en recepción HH

```
HH frm_recepcion_datos.java
    ├─ línea 3148: if (BeProducto.Genera_lp) execws(8)
    │                 │
    │                 └─► WSHHRN.Get_Nuevo_Correlativo_LicensePlate
    │                         ├─ resolucion_lp_operador WHERE idoperador=X AND idbodega=Y AND activo=1
    │                         ├─ correlativo_actual + 1
    │                         ├─ aplica configuracion_barra_pallet (LongLP, prefijo)
    │                         └─ retorna LP string
    │
    └─ línea 718-721: if (Presentacion.Genera_lp_auto) lblLicPlate.setVisibility(VISIBLE)
                      línea 1861-1863: txtLicPlate.setFocusable(true) — input habilitado
                      línea 3603-3607: ídem en confirmación
```

### 3.4 Cuello de botella arquitectónico

Las 374 funciones públicas en un solo .asmx.vb (19.492 líneas) son **el cuello de botella crítico para migrar a .NET 10**. La nueva WebAPI debe descomponer esto por dominio:
- LpService (correlativo, existencia, autorización)
- StockQueryService (consultas de stock por LP)
- RecepcionService (movimientos de recepción)
- PickingService (etc)

---

## 4. Hilo WMSWebAPI — el gap brutal

### 4.1 Estado actual de la WebAPI .NET moderna

```
WMSWebAPI/Controllers/  (25 controllers):
  Acuerdos, Bodega, CambioEstado, Cliente, Familia, KPI, Marca,
  Movimientos, PreFactura, Presentacion, Productos, Propietario,
  Proveedor, Stock, Sync (Ingresos/Salidas), TipoProducto, Umbas,
  Auth, TestAuth, Ajuste, Clasificacion, CentroCosto

WMSWebAPI/Services/ (16):
  Acuerdos, Ajustes, Cambio_Estado, Centro_Costo, Cliente,
  Ingresos, KPI, Login_JWT, LogPortalUx, Prefactura, Producto,
  Propietarios, Proveedor, Reset_Password, Salidas

WMSWebAPI/Models/ (2):
  ResultadoSync.cs, TransaccionProcesada.cs
```

**Cobertura del WMSWebAPI**:
- Capa 1 — Maestros (CRUD): clientes, productos, marcas, familias, etc.
- Capa 2 — Reporting / KPI: dashboards, queries agregadas.
- Capa 3 — Sync ERP (push/pull): SyncIngresosController, SyncSalidasController.
- Capa 4 — Auth: JWT, reset password.

**LO QUE NO TIENE**:
- LpController / LicensePlateController — **0 endpoints LP**
- RecepcionController — recepción HH no existe
- PickingController — picking HH no existe
- DespachoController — despacho HH no existe
- StockReservationController — reservas no existen
- AjusteInventarioController — ajuste por LP no existe

### 4.2 Lectura del estado real

El WMSWebAPI **NO es el reemplazo del WSHHRN**. Es el backend del **portal web** (`WMSPortal` / `TOMWMSUX`) y de **integraciones con ERPs externos**. El reemplazo del WSHHRN es justamente lo que Erik está construyendo greenfield en el WebAPI .NET 10 nuevo.

Implicación: el equipo debe decidir si:
- (a) Extender el WMSWebAPI actual con todos los dominios HH (gradual, riesgo: hereda stack viejo).
- (b) Construir un nuevo WebAPI .NET 10 separado solo para HH (greenfield, riesgo: dos APIs en paralelo durante años).

Ver Q-WMSWEBAPI-MIGRACION-MAPA en INDEX.

---

## 5. Hilo HH — el cliente Android

### 5.1 Modelos (POJOs) que cargan flags LP

- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/clsBeProducto.java`
  - Línea 52: `@Element(required=false) public boolean Genera_lp = false;`
  - Líneas 466-470: getter/setter `getGenera_lp()` / `setGenera_lp()`
  - Constructor con 8+ flags incluyendo `Serializado`, `Genera_lote`, `Genera_lp`
  - Mapeo XML por `@Element` (Simple Framework) — formato SOAP wire.

- `app/src/main/java/com/dts/classes/Mantenimientos/Producto/Producto_Presentacion/clsBeProducto_Presentacion.java`
  - Tiene `Genera_lp_auto`, `EsPallet`, `Permitir_paletizar`.

- `app/src/main/java/com/dts/classes/Transacciones/Recepcion/LicencePlates/clsBeLicensePlates.java`
  - **Define qué es un LP recibido en la HH**:
    - `IdRecepcionEnc`, `IdProductoBodega`, `IdPresentacion`
    - `LicensePlates` (string) — el código del LP
    - `CantidadUnidadBasica`, `CantidadPresentacion`
    - `CantidadMaximaPresentacion`, `CantidadDisponible`

- `app/src/main/java/com/dts/classes/Transacciones/Inventario/Inv_Stock_Prod/clsBeTrans_inv_stock_prod.java`
  - `License_plate` (string) y `License_Plate` (camelCase variant) en mismo archivo — inconsistencia.

### 5.2 Comportamiento UI condicional — `frm_recepcion_datos.java`

Variables locales que cachean los flags (línea 247):
```java
boolean Pperzonalizados = false, PCap_Manu = false, PCap_Anada = false,
        PGenera_lp = false, PTiene_Ctrl_Peso = false, PTiene_Ctrl_Temp = false,
        PTiene_PorSeries = false, PTiene_Pres = false;
```

**Esta declaración es la matriz completa de capabilities que la HH evalúa para dibujar la pantalla de recepción**. Cada `P*` corresponde a un campo opcional del producto.

Lógica de mostrar/ocultar campo LP:

```java
// Línea 718-721:
if (BeProducto.Presentacion.Genera_lp_auto) {
    TextView lblLicPlate = dialog.findViewById(R.id.lblLPlate);
    lblLicPlate.setVisibility(View.VISIBLE);  // muestra label
}

// Línea 1861-1864:
if (bePresentacion.Genera_lp_auto) {
    txtLicPlate.setFocusable(true);
    txtLicPlate.setFocusableInTouchMode(true);
    // ...
}

// Línea 3148:
if (BeProducto.Genera_lp) {
    execws(8);  // dispara Get_Nuevo_Correlativo_LicensePlate
} else {
    PallCorrecto = true;  // no necesita LP, sigue
}
```

Lógica pallet vs LP auto (línea 1536, 1554):
```java
if (bePresentacion.EsPallet && bePresentacion.Genera_lp_auto) {
    // captura cantidad como un pallet completo, autogenera LP
}
else if (bePresentacion.Permitir_paletizar && chkPaletizar.isChecked() 
         && !bePresentacion.Genera_lp_auto) {
    // permite paletizar manualmente, sin auto-generar LP
}
```

### 5.3 Pantallas HH afectadas por LP

Identificadas con búsqueda exhaustiva:
1. `Recepcion/frm_recepcion_datos.java` (27 mentions License_plate / Genera_lp)
2. `Recepcion/frm_recepcion_datos_original.java` (versión vieja, **debería eliminarse**)
3. `Packing/frm_Packing.java`
4. `InventarioInicial/frm_inv_ini_verificacion.java`
5. `InventarioInicial/frm_inv_ini_conteo.java`
6. `ConsultaStock/frm_consulta_stock.java`
7. `CambioUbicacion/frm_cambio_ubicacion_dirigida.java`
8. `CambioUbicacion/frm_cambio_ubicacion_ciega.java`
9. `ReubicarStockRes/frm_datos_stock_res.java`

**Existe `frm_recepcion_datos_original.java` junto al `frm_recepcion_datos.java` actual** — convivencia de versiones, posiblemente código muerto. Q-HH-RECEPCION-DOS-VERSIONES.

### 5.4 Cliente HTTP usado para LP

Las funciones LP se invocan vía SOAP por `WebService.java` — confirmado en imports y método `execws(N)` que dispatchea por número de servicio. **Aún ningún flujo LP migrado a `ApiService.java` REST**.

---

## 6. Hilo DB — schema y propagación

### 6.1 Tablas de configuración LP

#### `i_nav_config_enc` (config integración por bodega/empresa)
Filas por cliente:
- BECO: 1 fila (1 bodega productiva, todas con flags idénticos)
- K7: 6 filas (6 bodegas, todos los 6 con `genera_lp=True, control_lote=True, control_vencimiento=True, control_peso=True`)
- MAMPA: 33 filas (todas con `genera_lp=True`, demás flags False)
- BYB: ND (a confirmar)
- CEALSA: 2 filas (`genera_lp=True`, demás False)

**Hipótesis: cada fila no es "una bodega" sino "una integración NAV por bodega"** — pendiente confirmar el rol del IdBodega vs IdNavConfig.

#### `configuracion_barra_pallet` (1 row por cliente, idéntica)
```
IdConfiguracionPallet=1
LongCodBodegaOrigen=3
LongCodProducto=6
LongLP=7
CodigoNumerico=True
IdentificadorInicio="$  "
```
Define cómo se construye el código del LP. Misma config para los 5 clientes — **¿es un valor seed o realmente todos eligieron lo mismo?** Q-LP-LONG-DEFAULT.

#### `resolucion_lp_operador` (rangos de correlativo por operador+bodega)
Cols: `idresolucionlp, idoperador, idbodega, serie, correlativo_inicial, correlativo_final, correlativo_actual, activo, user_agr, fec_agr, user_mod, fec_mod`

Sample BECO: operador 1, bodega 1, serie '01', rango 1–99999.

Existe variante `resolucion_lp_usuario` también — **Q-LP-OPERADOR-VS-USUARIO**: cuál se usa en la generación, cuál es legacy.

### 6.2 Capabilities a nivel producto (`producto`)

| Col | Tipo | Significado |
|---|---|---|
| `genera_lp_old` | bit | Producto admite LP (legacy) |
| `genera_lote` | bit | Sistema autogenera lote interno si no viene |
| `serializado` | bit | Producto requiere control individual por número de serie |
| `control_lote` | bit | Lote es obligatorio en operaciones |
| `control_vencimiento` | bit | Vencimiento es obligatorio |
| `control_peso` | bit | Peso es obligatorio (peso variable) |
| `IdPerfilSerializado` | int FK | Perfil para productos serializados |
| `noserie` | nvarchar | Patrón de número de serie |

### 6.3 Capabilities a nivel presentación (`producto_presentacion`)

| Col | Tipo | Significado |
|---|---|---|
| `genera_lp_auto` | bit | Esta presentación autogenera LP en HH |
| `EsPallet` | bit | Esta presentación es un pallet |
| `IdPresentacionPallet` | int FK | Apunta a la presentación pallet equivalente |
| `permitir_paletizar` | bit | Permite armar pallet manual con esta presentación |

**Existe `producto_presentacion1`** con cols equivalentes — duplicado histórico, posiblemente código muerto. Q-PRESENTACION1-MUERTA.

### 6.4 Propagación a tablas operativas (idéntico en las 5 DBs)

| Tabla | Cols LP-related |
|---|---|
| `stock` | `lic_plate`, `uds_lic_plate`, `pallet_no_estandar` |
| `stock_jornada` | `lic_plate`, `uds_lic_plate`, `pallet_no_estandar` |
| `stock_rec` | `lic_plate`, `uds_lic_plate`, `pallet_no_estandar` |
| `stock_res` | `lic_plate`, `uds_lic_plate`, `pallet_no_estandar` |
| `faltantes` | `lic_plate`, `uds_lic_plate`, `pallet_no_estandar` |
| `trans_movimientos` | `lic_plate`, `barra_pallet` (DOS cosas: el LP funcional y el código de barra impreso) |
| `trans_re_det` | `lic_plate`, `pallet_no_estandar` |
| `trans_movimiento_pallet` | `lp_origen`, `lp_destino` (consolidación/split, naming distinto) |
| `bodega` | `bloquear_lp_hh` (bit — flag de bloqueo operativo) |

**Inconsistencia de naming**: `lic_plate` (snake_case) vs `lp_origen`/`lp_destino` (snake con prefijo lp_) vs `barra_pallet`. La WebAPI nueva debe normalizar.

### 6.5 Datos reales: ¿se respeta el invariante?

`stock` con `lic_plate` poblado:
| Cliente | Con LP | NULL/empty | % NULL |
|---|---:|---:|---:|
| BECO | 30.176 | 19 | 0.06% |
| K7 | 3.517 | 1.186 | **25.2%** |
| MAMPA | 481 | 49 | 9.2% |
| BYB | 6.887 | 24 | 0.35% |
| CEALSA | 478 | 0 | 0% |

**K7 tiene 25% del stock sin LP poblado** aunque `genera_lp=True` y `genera_lp_old=True` para todos sus productos. Hipótesis: stock viejo ingresado antes de activar LP, o ingresos especiales (ajustes, transferencias internas) que no generaron LP. Esto valida que **el invariante de coherencia NO está garantizado a nivel DB hoy**, y la WebAPI nueva debe enforzarlo en la capa de servicios.

### 6.6 Datos en `trans_movimiento_pallet`

Sample real:
- BECO: `lp_origen='EN00295', lp_destino='R1000001'` (entrada → ubicación rack)
- K7: `lp_origen='0110010010900001', lp_destino='0110010010900002'` (formato distinto: 16 chars vs 7)
- BYB: idéntico formato a K7 (16 chars)
- MAMPA, CEALSA: 0 filas — **no hacen consolidación de pallets**

Los formatos de LP varían entre clientes aunque la `configuracion_barra_pallet` dice `LongLP=7` para todos. **Discrepancia**: el LongLP=7 no se respeta en K7/BYB. Q-LP-LONG-VS-DATOS-REALES.

---

## 7. Hilo ERP — qué dialoga con NAV/SAP

### 7.1 NAV (BECO, BYB, otros)

`DynamicsNavInterface/Web References/WebReference/Reference.vb` expone como operations consumidas DE NAV:
- `Get_Nuevo_Correlativo_LicensePlate`
- `Get_Nuevo_Correlativo_LicensePlate_S`

Es decir, **NAV puede ser fuente del correlativo LP en algunos clientes**, no solamente la tabla `resolucion_lp_operador`. Pendiente: confirmar para qué clientes el correlativo viene de NAV vs de la tabla local.

`WSHHRN/Web References/wsBYBNav*` (4 referencias específicas BYB) — confirmado: **BYB usa NAV en línea para operaciones LP** desde el WebService, no via sync batch.

### 7.2 SAP (K7)

`SAPSYNC_Killios/Clases Interface Sync/Producto/Presentaciones/clsSyncSAPPresentaciones.vb` mapea `genera_lp_auto` desde SAP a TOMWMS. La capability viene definida en SAP y se sincroniza periódicamente. La generación de LP es local (no SAP).

### 7.3 Cumbre / otros (sin EC2)

`WSHHRN/Conn - Cumbre.ini` y `WSHHRN/Conn_Becofarma.ini` muestran que el WebService selecciona la conexión DB según un archivo de configuración por cliente. **Cumbre** y **Becofarma** son los dos config files materiales. Pendiente: cómo se elige el archivo en runtime (header SOAP? hostname?). Q-CONNINI-SELECCION.

---

## 8. Variantes por empresa

### 8.1 Matriz cross-cliente del modelo dual

| Cliente | bodega.genera_lp | producto.genera_lp_old (% True) | producto.genera_lote (True) | producto_presentacion.genera_lp_auto |
|---|---|---|---|---|
| BECO | True (1 bodega) | 100% (1.835/1.835) | 72 (3.9%) | tabla vacía |
| K7 | True (6 bodegas) | 100% (319/319) | 0 | 295 True |
| MAMPA | True (33 bodegas) | 100% (31.397/31.397) | 0 | tabla vacía |
| BYB | True | **78% False** (487/623), 22% True | 0 | 366 True, 14 False |
| CEALSA | True (2 bodegas) | 100% (1.714/1.714) | 21 (1.2%) | tabla vacía |

Patrones:
- **Todos los clientes tienen `genera_lp=True` a nivel bodega** — modelo LP es universal en producción.
- **BYB es el único con productos `genera_lp_old=False`** — drift que requiere explicación operativa (¿es intencional? ¿productos no-LP-able? ¿migración interrupta?).
- **Solo K7 y BYB tienen `producto_presentacion` poblada** — los demás manejan presentación como concepto implícito. Esto significa que la lógica `Genera_lp_auto` solo se evalúa realmente en K7/BYB.

### 8.2 Stock real con LP propagado

(Ver sección 6.5 — solo K7 muestra drift significativo del invariante.)

### 8.3 Casos edge

- **MAMPA**: 31.397 productos, todos con `genera_lp=True` pero **`control_lote=False, control_vencimiento=False, control_peso=False`**. Es el caso más limpio del LP "puro": solo trazabilidad, nada de variantes. La HH solo pide LP y cantidad.
- **K7**: el único con los 4 flags en True (LP + lote + vencimiento + peso). La pantalla de recepción HH muestra los 4 campos.
- **CEALSA**: NULL en `IdTipoRotacion` para los 1.714 productos. Tiene LP pero no rotación definida.

---

## 9. Q-* abiertas

| ID | Pregunta | Cómo resolver |
|---|---|---|
| Q-LP-OPERADOR-VS-USUARIO | ¿`resolucion_lp_operador` vs `resolucion_lp_usuario` cuál se usa? | rg en WSHHRN cuál se SELECT |
| Q-LP-LONG-DEFAULT | ¿`LongLP=7` es seed o decisión real? Datos reales muestran 16 chars en K7/BYB | Erik confirmar + leer constructor del correlativo |
| Q-LP-LONG-VS-DATOS-REALES | Si LongLP=7 pero K7 usa 16 chars, ¿de dónde viene el 16? | Investigar `Get_Nuevo_Correlativo_LicensePlate(_S)` cuerpo |
| Q-LP-S-VARIANTE | `Get_Nuevo_Correlativo_LicensePlate_S` ¿qué es la "_S"? | Leer firma + comparar con la versión sin _S |
| Q-LP-CORRELATIVO-NAV | ¿Qué clientes obtienen el correlativo de NAV vs tabla local? | Leer config + tracear llamadas a `WebReference` |
| Q-LP-EN-K7-DRIFT-25PCT | ¿Por qué 1.186 stocks en K7 sin `lic_plate` si `genera_lp=True`? | Erik: revisar histórico, posibles ajustes manuales |
| Q-LP-BYB-PRODS-SIN-LP | ¿Por qué 487 productos en BYB con `genera_lp_old=False`? | Erik confirmar política BYB |
| Q-PRESENTACION1-MUERTA | ¿`producto_presentacion1` se usa o es código muerto? | rg en BOF + DAL |
| Q-RECEPCION-BOF-FLUJO | `Recepcion_BOF/frmRecepcionBOF.vb` ¿cuándo se usa? | Erik aclarar |
| Q-HH-RECEPCION-DOS-VERSIONES | `frm_recepcion_datos_original.java` vs `frm_recepcion_datos.java` | Confirmar muerto, eliminar |
| Q-MI3-QUE-ES | TestMI3 con 3 srvProducto* refs — ¿qué proyecto es? | Leer .csproj + Erik |
| Q-WMSWEBAPI-MIGRACION-MAPA | ¿WMSWebAPI extiende a HH o WebAPI .NET 10 nuevo es separado? | Decisión arquitectónica con Erik |
| Q-CONNINI-SELECCION | ¿Cómo selecciona WSHHRN el Conn.ini correcto en runtime? | Leer App_Start del WebService |
| Q-LP-NAMING-DB | `lic_plate` vs `lp_origen/destino` vs `barra_pallet` — naming inconsistente | Definir contrato único en WebAPI nueva |
| Q-LP-FALTANTES-PARA-QUE | Tabla `faltantes.lic_plate` ¿qué uso tiene? | rg WSHHRN + BOF |
| Q-SEC-OPENAI-KEY-LEAK | API key OpenAI hardcodeada en `WSHHRN/ChatGPTService.vb` línea 9 | **Rotar la key + mover a appsettings/secrets** |
| Q-SEC-CONNINI-CREDS | Credenciales `sa` en `Conn.ini` en claro, push al repo | Mover a Azure Key Vault / variables de entorno por host |

---

## 10. Lecturas para WebAPI .NET 10 nuevo

### 10.1 Dominio LP debe modelarse explícitamente

Hoy el LP es un string disperso. Recomendaciones:

```
Bounded context: LicensePlate
  ├─ Aggregate: LicensePlate (PK: code string)
  │     ├─ Code (string, unique per empresa)
  │     ├─ EmpresaId, BodegaId
  │     ├─ Status (Active, Consumed, Voided)
  │     ├─ CreatedBy (operadorId), CreatedAt
  │     ├─ Items: List<LpItem> (productoId, presentacionId, lote, fecha_vence, peso, cantidad)
  │     └─ Movements: List<LpMovement> (fromLocation, toLocation, by, at)
  │
  ├─ Domain Service: LpCorrelativoIssuer
  │     ├─ Strategy 1: LocalSequence (resolucion_lp_operador)
  │     ├─ Strategy 2: NavExternal (Get_Nuevo_Correlativo_LicensePlate via NAV)
  │     └─ Strategy 3: ExternalProvider (BYB wsBYBNav)
  │
  └─ Repository: LpRepository (Cosmos / SQL)
```

### 10.2 Invariante de coherencia debe ser policy enforced

En la capa de servicios (no opcional):

```csharp
public class StockMovementService
{
    public Result Apply(StockMovement m, Producto p, Presentacion pr)
    {
        if (p.ControlLote && string.IsNullOrEmpty(m.Lote))
            return Result.Fail("ControlLote=True requiere lote");
        if (p.ControlVencimiento && m.FechaVence == null)
            return Result.Fail("ControlVencimiento=True requiere fecha_vence");
        if (p.ControlPeso && m.Peso == null)
            return Result.Fail("ControlPeso=True requiere peso");
        if (config.GeneraLp && p.GeneraLpOld && string.IsNullOrEmpty(m.LicPlate))
            return Result.Fail("Bodega+Producto exigen LP");
        // ... etc
        return _repo.Save(m);
    }
}
```

### 10.3 Endpoints LP mínimos (mapeo desde WSHHRN)

| WSHHRN | WebAPI nuevo (REST) | Auth |
|---|---|---|
| `Get_Nuevo_Correlativo_LicensePlate` | `POST /api/lp/issue` | JWT operador |
| `Get_BeProducto_By_LP_For_HH` | `GET /api/lp/{code}/producto` | JWT |
| `Get_Stock_By_Lic_Plate` | `GET /api/lp/{code}/stock` | JWT |
| `Existe_Lp_By_Licencia_And_IdBodega` | `HEAD /api/bodegas/{id}/lp/{code}` | JWT |
| `Set_LP_Stock` | `PUT /api/stock/movement` (con body LpMovement) | JWT |
| `Get_Ubicacion_LP` | `GET /api/lp/{code}/ubicacion` | JWT |
| `Get_Resoluciones_Lp_By_IdOperador_And_IdBodega` | `GET /api/operadores/{id}/lp-resoluciones` | JWT admin |
| `Set_LP_Stock_Mixto` | `POST /api/lp/{code}/items` (multi-producto) | JWT |
| `Get_Pedido_A_Verificar_By_LP` | `GET /api/lp/{code}/pedidos-pendientes` | JWT |

### 10.4 Tabla maestra de capabilities (refactor)

Hoy las capabilities están dispersas en 3 tablas. Para el WebAPI nuevo, exponer una vista materializada:

```sql
CREATE VIEW vw_producto_capabilities AS
SELECT 
    p.IdProducto,
    p.IdEmpresa,
    nce.genera_lp                AS bod_genera_lp,
    p.genera_lp_old              AS prod_genera_lp,
    p.genera_lote,
    p.serializado,
    p.control_lote,
    p.control_vencimiento,
    p.control_peso,
    p.IdTipoRotacion,
    pp.genera_lp_auto            AS pres_genera_lp_auto,
    pp.EsPallet                  AS pres_es_pallet,
    pp.permitir_paletizar
FROM producto p
INNER JOIN i_nav_config_enc nce ON nce.IdEmpresa = p.IdEmpresa
LEFT JOIN producto_presentacion pp ON pp.IdProducto = p.IdProducto
```

Y resolver el modelo dual `genera_lp` vs `genera_lp_old` en una sola property derivada:
```csharp
public bool RequiresLP => bod_genera_lp && prod_genera_lp;
```

### 10.5 Tests cross-cliente obligatorios

Cada caso de uso del WebAPI nuevo debe tener tests con datos de los 5 clientes:
- BECO: caso "todo prendido salvo peso"
- K7: caso "todo prendido"
- MAMPA: caso "solo LP, nada más"
- BYB: caso "drift parcial — algunos productos sin LP"
- CEALSA: caso "LP sin rotación definida"

---

## Notas de versionado de la traza

- v1 — 2026-04-28 — Erik + Agente: bootstrap, 9 secciones completas con datos cross-cliente verificados en EC2.
- Pendiente v2: resolver Q-* arriba, agregar diagrama secuencia recepción-LP completo (HH ↔ WSHHRN ↔ DB ↔ NAV).

---

## 11. Wave 6.1 — Respuestas de Erik + validaciones

### 11.1 Q-LP-EN-K7-DRIFT-25PCT — resuelta parcial

**Respuesta de Erik (2026-04-28)**: Cuando un producto se "explosiona" (se transforma de su presentación a su unidad básica de medida), se le **quita la licencia**. Si el LP se conservara en el stock derivado, pediríamos productos sueltos a una agrupación física que ya no existe. El 25% de drift en K7 puede explicarse por este patrón.

**Validación con datos** (`stock` K7 SIN `lic_plate`, n=1.186):

| Sub-grupo | Cantidad | % del SIN LP | Lectura |
|---|---:|---:|---|
| `IdPresentacion = NULL` | 269 | 22.7% | **Consistente con explosión a unidad básica** (la presentación se disolvió) |
| `IdPresentacion` con `genera_lp_auto=True` (Caja12/Caja6/Caja24/etc) | 917 | 77.3% | **Drift residual real** — la presentación pide LP auto pero el stock no lo tiene |

**Lectura final**:
- La hipótesis "explosión consume LP" se valida para los 269 stocks con presentación NULL.
- Los 917 restantes son drift residual a investigar: ingresos previos a activar `genera_lp_auto`, ajustes manuales por BOF, o transferencias internas que no respetan el invariante.

**Nuevas Q-* derivadas**:
- **Q-LP-EXPLOSION-COMO-OPERA**: ¿qué SP / función ejecuta la explosión y limpia el `lic_plate`? Buscar en `WSHHRN/TOMHHWS.asmx.vb`, `WMS.DALCore`, y SPs de la DB. Probables candidatos por nombre: `Set_Stock_Explosion`, `Explosionar_Stock`, `Cambio_Presentacion`, `Fraccion_Stock`.
- **Q-LP-917-DRIFT-RESIDUAL**: ¿qué origen tienen los 917 stocks K7 con presentación que pide `genera_lp_auto=True` pero sin LP? Investigar `trans_movimientos` históricos por esas filas: ¿IdTipoMovimiento de ingreso original?
- **Q-LP-CICLO-VIDA**: documentar formalmente el ciclo de vida del LP: emitido → activo (poblado en stock) → consumido (despacho/explosión/desarmado) → vacío (referencias removidas). Hoy el sistema no tiene una columna de estado del LP — el "consumo" se manifiesta como remoción del valor en `lic_plate`.

### 11.2 Q-LP-CORRELATIVO-NAV — resuelta

**Respuesta de Erik (2026-04-28)**: Es muy específico para BYB — de momento solo BYB tiene NAV. Todo lo relacionado con NAV está indexado a BYB. Los push que hace el WebService de la HH son porque NAV funciona también con web services, entonces eligieron el WSHHRN como plataforma de enlace para que la HH pudiera disparar acciones hasta el ERP de ellos. **En general los procesos de ingreso y salida se procesan por interfaces ad-hoc por cliente según el ERP, tratando de reutilizar lógica común. Esto es lo que están modelando en el WMSWebAPI**.

**Implicaciones validadas**:

1. **NAV-en-línea es BYB-only**. Los 4 `wsBYBNav*` proxies y el `Reference.vb` de `DynamicsNavInterface` solo se invocan en clientes con NAV. Resto de clientes (BECO, K7, MAMPA, CEALSA, Cumbre) usa interfaces propias por ERP.

2. **WSHHRN como puente NAV**: la elección de usar el WebService SOAP como puente para BYB es porque NAV expone web services nativos — la "compatibilidad de protocolo" es lo que motivó el puente. La HH llama al WSHHRN, el WSHHRN consume NAV, NAV responde, el WSHHRN reenvía a la HH. Ese loop es el patrón de integración BYB.

3. **WMSWebAPI .NET (los `Sync/*` services) ES la capa de generalización emergente**:
   - `WMSWebAPI/Services/Ingresos/` y `WMSWebAPI/Services/Salidas/`
   - `WMSWebAPI/Controllers/SyncIngresosController.cs` y `SyncSalidasController.cs`
   - `WMSWebAPI/Models/ResultadoSync.cs` y `TransaccionProcesada.cs`
   
   Estos componentes son donde se está intentando **abstraer lo común de las integraciones ad-hoc por cliente**. Cada cliente sigue teniendo su interfaz específica, pero la lógica reutilizable vive aquí.

4. **El WebAPI .NET 10 nuevo debe respetar este patrón**: el dominio de integración ERP debe modelarse como interfaz pluggable por cliente, no como implementación única.

### 11.3 Patrón emergente: arquitectura objetivo del WebAPI .NET 10

```
WebAPI .NET 10 (greenfield)
├─ Domain (modelo único, no por cliente)
│  ├─ LicensePlate (con ciclo de vida explícito)
│  ├─ Stock, Producto, Bodega, Presentacion
│  ├─ Movimiento, Recepcion, Despacho, Picking
│  └─ Capabilities (perfil de control_lote/peso/vence/serie/lp por producto)
│
├─ Application Services (lógica común reutilizable)
│  ├─ LpIssuanceService          ← genera correlativo
│  ├─ LpConsumptionService       ← explosión, despacho, desarmado
│  ├─ StockMovementService       ← enforce invariante de coherencia
│  └─ ReceptionService, PickingService, DespachoService
│
├─ ERP Integration (pluggable por cliente)  ← LO QUE ERIK ESTÁ MODELANDO
│  ├─ IErpAdapter (interfaz)
│  ├─ NavErpAdapter         (BYB)
│  ├─ SapErpAdapter         (K7)
│  ├─ Ims4mbErpAdapter      (BECO, BYB sec, CEALSA)
│  ├─ MampaErpAdapter       (MAMPA)
│  └─ CumbreErpAdapter      (Cumbre)
│
└─ Infrastructure
   ├─ SQL Server (TOMWMS_*)
   ├─ JWT Auth (Login_JWT)
   └─ Logging / Telemetry
```

**Principio de diseño confirmado por Erik**: la lógica de dominio (ej: explosión consume LP) es **universal**, las integraciones ERP son **por cliente con interfaz común**.

---

## 12. Principios de dominio aprendidos esta sesión

1. **El LP es identificador efímero atado a un estado físico de agrupación**. Operaciones que desarman la agrupación (explosión a unidad básica, despacho, desconsolidación) **deben consumir el LP** (limpiarlo del stock derivado). El WebAPI nuevo debe modelar este ciclo de vida explícitamente.

2. **No hay "integración ERP universal"**. Cada cliente tiene su interfaz ad-hoc. Lo que se reutiliza es la **lógica de dominio** (qué hace el WMS) y los **contratos** (qué información pide/devuelve). Lo que cambia es el adaptador ERP. El WMSWebAPI `Sync/*` es la primera generación de esa abstracción.

3. **El invariante de coherencia es responsabilidad de la capa de servicios**, no del modelo de datos. La DB acepta NULL en `lic_plate` aunque la bodega tenga `genera_lp=True` — la enforzación queda al caller. El WebAPI nuevo debe enforzar en la capa Application Services antes de tocar el repositorio.

4. **El modelo dual `genera_lp` (bodega) + `genera_lp_old` (producto) es el resultado de una migración interrupta histórica**, no un diseño deliberado. El WebAPI nuevo puede unificar en una capability derivada `RequiresLP = bod_genera_lp AND prod_genera_lp`.

5. **NAV-en-línea via SOAP es el puente BYB-específico**, no un patrón general. La elección fue forzada por la arquitectura de NAV (también web services). Otros ERPs no necesariamente tienen este puente — usan sync batch o adapter custom.


---

## 13. Disclaimer retroactivo de ramas (Wave 6.1)

**Esta traza fue escrita escaneando `dev_2028_merge` en BOF + HH.**

**Lo que ESTO significa para cada sección**:

| Sección | Validez `dev_2023_estable` | Notas |
|---|---|---|
| §1 Caso de invariante (DDLs) | Probable 100% válida | Las DBs son las mismas para ambas ramas — el schema vive en SQL Server, no en código |
| §2 Mapa código BOF — `WMS.DAL/clsLnProducto.vb` | Pendiente verificar | Puede tener diferencias |
| §2 Mapa código BOF — `WMS.DALCore/clsLnProducto.cs` | **NO APLICA A 2023** | Toda la capa Core es greenfield 2028 |
| §2 Mapa código HH — `clsBeProducto.java` | **100% válida** | Hash idéntico 2023 vs 2028 |
| §3 Endpoints SOAP `WSHHRN/TOMHHWS.asmx.vb` (21) | ~95% válida | 10/11 endpoints LP ya en 2023. Solo `Existe_Lp_By_Licencia_And_IdBodega_JSON` es nuevo 2028 |
| §4 SQL evidencia (4 DBs sin MAMPA) | 100% válida para `dev_2023_estable` | Porque corresponde justamente a clientes 2023 |
| §5 SQL evidencia MAMPA | **Solo aplica `dev_2028_merge`** | MAMPA es el cliente del scan 2028 |
| §6-§9 Ciclo de vida | Universal por dominio | Explosión consume LP aplica a ambas ramas |
| §11.3 Arquitectura objetivo WebAPI .NET 10 | **Era proyección incorrecta mía** | Ver §13.1 abajo |
| §12 Principios de dominio | Universal | Independientes de la rama |

### 13.1 Corrección retroactiva — WebAPI

En §11.3 proyecté un "WebAPI .NET 10 nuevo greenfield" para reemplazar la operación HH. **Esta proyección era mía, no plan de Erik.**

**Lo que es realidad** (Wave 6.1 confirmado por Erik):
1. **WMSWebAPI existe desde 2023** (probado por hash diff entre ramas).
2. **WMSWebAPI = canal B2B-only** para clientes con desarrollo propio (MHS = primer caso).
3. **NO reemplaza MI3 ni WSHHRN** — son canales paralelos para audiencias distintas:
   - **MI3** = bridge legacy con MercaERP
   - **WSHHRN** = servicio SOAP para HH Android (340+ endpoints)
   - **WMSWebAPI** = REST para integraciones B2B externas
4. **WMS.DALCore SÍ es greenfield 2028** — esta capa nueva en .NET es lo que merece llamarse "WebAPI .NET 10 nuevo" en términos arquitectónicos, pero su rol es **capa de acceso a datos**, no API HTTP.

**Lectura correcta del rol de cada componente**:
```
HH Android       → SOAP/REST → WSHHRN          → MI3 → ERP cliente
ERP cliente      → SOAP      → MI3                                   (legacy)
Integrador B2B   → REST/JSON → WMSWebAPI       → DAL/DALCore → DB    (MHS, futuros)
BOF VB.NET dual  → directo   → DAL/DALCore     → DB
```

### 13.2 Endpoint LP nuevo en 2028 (validado)

```vb
' Solo en dev_2028_merge:
Public Sub Existe_Lp_By_Licencia_And_IdBodega_JSON(
    ByVal pLic_Plate As String,
    ByVal pIdBodega As Integer)
```

Es la versión JSON del endpoint SOAP existente `Existe_Lp_By_Licencia_And_IdBodega(pLic_Plate, pIdBodega) As Boolean`. 

**Patrón confirmado** (Q-SOAP-A-JSON-2028 abierta): la transición 2028 incluye agregar variantes JSON de endpoints SOAP existentes. ¿Estrategia general? ¿Solo casos puntuales? Pendiente preguntar a Erik.

### 13.3 Issue Q-SEC-OPENAI-KEY-LEAK — severidad sube

`WSHHRN/ChatGPTService.vb` línea 9 tiene la API key de OpenAI hardcoded. **Existe en `dev_2023_estable` también** (hash a verificar pero el archivo está). **Lleva expuesta más tiempo del que estimé**. Rotar la key + scrubbing del git history es ahora urgencia alta.

