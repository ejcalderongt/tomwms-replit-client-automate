# Ciclo 3.1 — Bloque A: Configuración a nivel infraestructura (App.config / Web.config / AndroidManifest)

## Estado
- **Generado**: 2026-04-27T04:32:46.715Z
- **Fuente**: TOMWMS_BOF/_git/TOMWMS_BOF + TOMHH2025/_git/TOMHH2025 (Azure DevOps, branch `dev_2028_merge`)
- **Cobertura**: 938 archivos config en BOF (657 .resx UI + 162 .xsd datasets + 119 .config), 1 AndroidManifest del HH. Análisis profundo de 5 .config raíz + manifest.

## Objetivo del bloque
Identificar la configuración a nivel **infraestructura** (no de negocio): connection strings, endpoints WCF, settings de aplicación, permisos del HH Android, recursos UI, datasets tipados.

---

## A.1 Inventario de archivos config en TOMWMS_BOF

| Tipo | Total | Focus HH/BOF/WS/MI3 | Propósito |
|---|---:|---:|---|
| `.resx` | 657 | 506 | Recursos UI (textos, imágenes, layouts) |
| `.xsd` | 162 | 79 | Esquemas de DataSet tipados |
| `.config` | 119 | 24 | App.config / Web.config / packages.config |
| **AndroidManifest.xml** | 1 | 1 | Manifiesto del HH (TOMHH2025) |

> Se excluyen del análisis los productos legacy (CEALSAMI3, SAP*, DMS, DynamicsNavInterface, MetaTrader, DashBorlin, AWS_WMSBD_Updater) por estar fuera del alcance del Brain TOMWMS+HH.

---

## A.2 Connection strings y endpoints (sanitizados)

> **Importante**: los valores reales de connection string fueron **sanitizados** (`User ID=***`, `Password=***`). Las credenciales nunca se guardan en este Brain.

### TOMIMSV4 App.config (BOF)
Archivo: `App.config` (3681B)

**appSettings:**

| Key | Valor (sanitizado, primeros 60 chars) |
|---|---|
| `CST` | `Data Source=52.32.154.252;Initial Catalog=IMS4MB_QA;Persist ` |
| `ClientSettingsProvider.ServiceUri` | `` |

**connectionStrings:**

| Name | Sanitizado |
|---|---|
| `TOMIMSV4.My.MySettings.IMS4MB_QAConnectionStringPrograN` | `Data Source=PROGRAN;Initial Catalog=IMS4MB_QA;Persist Security Info=True;User ID=***;Password=***` |

**WCF endpoints:**

| Address | Contract |
|---|---|
| `http://localhost:4180/Despacho/ServiceDespacho.svc` | `WCFDespacho.IServiceDespacho` |

**applicationSettings (My.Settings):**

| Name | Valor (sanitizado) |
|---|---|
| `IMS4MB_ConnectionStringConfigurable` | `Data Source=PROGRAN;Initial Catalog=IMS4MB_QA;Persist Security Info=True;User ID` |

### MI3 Web.config (interface)
Archivo: `Web.config` (13236B)

**appSettings:**

| Key | Valor (sanitizado, primeros 60 chars) |
|---|---|
| `CST` | `Data Source=192.168.126.80;Initial Catalog=IMS4MB_IDEALSA_QA` |

**applicationSettings (My.Settings):**

| Name | Valor (sanitizado) |
|---|---|
| `CST` | `Data Source=192.168.126.80;Initial Catalog=IMS4MB_IDEALSA_QA;User ID=***;Pa` |

---

## A.3 Hallazgos del análisis de configs

**1. Los `.config` en repo apuntan a entornos QA, no a Killios prod.**

- `TOMIMSV4/App.config` → `Data Source=52.32.154.252` / `IMS4MB_QA` (servidor compartido QA en AWS).
- `TOMIMSV4/App.config` (My.Settings) → `Data Source=PROGRAN` / `IMS4MB_QA` (host de desarrollo Erik).
- `MI3/Web.config` → `Data Source=192.168.126.80` / `IMS4MB_IDEALSA_QA` (cliente IDEALSA QA, IP interna).

→ Esto confirma: **los entornos productivos overridean el config en deploy** (DevOps, transformations o configuración manual del cliente). El repo nunca contiene credenciales prod.

**2. Patrón WCF puntual.**

- `TOMIMSV4/App.config` define cliente WCF a `http://localhost:4180/Despacho/ServiceDespacho.svc` con contract `IServiceDespacho`. Aparentemente para integración con un servicio interno de Despacho que corre local en máquinas BOF.
- `MI3/Web.config` define `basicHttpBinding` con `transferMode=Streamed` y timeouts de 10min — pensado para volcar archivos grandes de interfaces.

**3. Sin connection string del HH en el repo.**

El HH (TOMHH2025) **no tiene App.config**. Toda su comunicación es vía WSHHRN (HTTP SOAP) y la URL del WS se configura desde la app Android (probablemente preference/settings o login screen). El HH no tiene acceso directo a la BD.

---

## A.4 AndroidManifest del HH (TOMHH2025)

Archivo: `/app/src/main/AndroidManifest.xml` (9604B)

> Nota: en proyectos AGP 7+, los atributos `package`, `versionCode`, `versionName`, `minSdkVersion`, `targetSdkVersion` ya **no van en el manifest** sino en `build.gradle`. Por eso el manifest puro sólo declara permisos, features y componentes.

### Permisos (13)

| Permiso | Para qué lo usa el HH |
|---|---|
| `android.permission.WAKE_LOCK` | Mantener pantalla encendida durante operación |
| `android.permission.WRITE_EXTERNAL_STORAGE` | Guardar logs / archivos temp / templates locales |
| `android.permission.READ_EXTERNAL_STORAGE` | Leer templates / archivos descargados |
| `android.permission.BLUETOOTH` | Conectar a impresoras Zebra / lectores BT |
| `android.permission.BLUETOOTH_ADMIN` | Administrar pairing BT |
| `android.permission.ACCESS_FINE_LOCATION` | Geolocalización exacta (req por Android para escanear BT) |
| `android.permission.ACCESS_COARSE_LOCATION` | Geolocalización aproximada |
| `android.permission.ACCESS_NETWORK_STATE` | Detectar conectividad para sync/offline |
| `android.permission.INTERNET` | Llamar al WSHHRN |
| `android.permission.ACCESS_WIFI_STATE` | Detectar wifi para sync |
| `android.permission.BLUETOOTH_SCAN` | Escanear dispositivos BT (Android 12+) |
| `android.permission.BLUETOOTH_CONNECT` | Conectar a BT pareado (Android 12+) |
| `android.permission.BLUETOOTH_ADVERTISE` | (sin documentar) |

### Features hardware

- `android.hardware.camera` — cámara para escaneo barcode/2D

### Componentes Android

- **64 activities** — pantallas del HH (login, menú, recepción, despacho, ajustes, conteos, etc).
- **2 services** — background workers (probable: sync offline + heartbeat).
- **0 receivers** — sin broadcast receivers declarados.

### Implicaciones

- El HH es un **monolito Android nativo** (no MAUI/Xamarin/Flutter — todas activities con nombres .NET-style sugieren Android Studio + Java/Kotlin).
- Bluetooth + Cámara → flujo típico: escanear código de barras con cámara, imprimir etiqueta vía Bluetooth (Zebra portátil).
- 64 activities sin alias significa una pantalla por flujo — podría modelarse en el Brain como un grafo de activities → WebMethods (cada activity llama a N WMs del WSHHRN).

---

## A.5 Implicaciones para el Brain

1. **El Brain modela CONFIG DE NEGOCIO (bloque D), no infra (bloque A).** Connection strings y endpoints son operacionales, no parte del modelo de razonamiento del Brain. Los menciono para completitud.

2. **Templates XML del HH son artefacto del lado BOF.** Si Brain debe explicar cómo se imprime una etiqueta, hay que mapear `Nombre_Template` → archivo XML real (no en este bloque).

3. **Permisos del HH revelan capacidades.** El Brain sabe que el HH puede: imprimir BT, escanear cámara, persistir local, geolocalizar (¿para qué? — pendiente).

4. **64 activities = oportunidad de mapeo.** En un ciclo futura conviene listar las activities del manifest y emparejar cada una con los WMs que invoca, lo que daría el grafo `pantalla HH → WebMethod → clsLn → tabla`.

## Anexo: archivos generados por este bloque

| Archivo | Contenido |
|---|---|
| `data/passada-3-1-bloque-A-config-files.json` | Inventario completo 938 archivos config |
| `data/passada-3-1-bloque-A-focus.json` | Subset 609 archivos focus HH/BOF/WS/MI3 |
| `data/passada-3-1-bloque-A-parsed.json` | Análisis profundo de 5 .config raíz + manifest (sanitizado) |
| `analysis/passada-3-1-bloque-A-config-infra.md` | Este documento |