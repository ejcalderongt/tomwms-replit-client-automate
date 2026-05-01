---
id: 00-mapa-de-cajas
tipo: code-deep-flow
estado: vigente
titulo: 00 — Mapa de cajas macro de TOMWMS
ramas: [dev_2028_merge]
tags: [code-deep-flow]
---

# 00 — Mapa de cajas macro de TOMWMS

> Vista holistica de toda la arquitectura del WMS (estado 28-abr-2026,
> rama `dev_2028_merge` en Azure DevOps `ejcalderon0892`).
>
> Insumo previo a cualquier traza. Si una traza necesita una caja que no
> figura aca, **primero actualizar este archivo** y despues escribir la
> traza.

---

## 1. Resumen en una linea

TOMWMS es una solucion mono-repo VB.NET de 376 MB con **una HH Android
de 14 MB** que se conectan via **dos canales paralelos**: un WebService
SOAP legacy (`WSHHRN/TOMHHWS.asmx`) y un WebAPI REST nuevo en
construccion (`WMSWebAPI/`, .NET moderno).

A su vez, el backend conversa con **5 ERPs externos** distintos via
syncs dedicados (`SAPBOSync*.exe` para 3 clientes SAP B1, `NavSync.exe`
para BYB, `CEALSASync.exe` para CEALSA). La DB es **SQL Server**, una
instancia por cliente (productiva en su laptop, copia parcial en EC2
`52.41.114.122,1437`).

---

## 2. Cajas (alto nivel)

```
+-------------------+        +-------------------+        +----------------+
|     HH Android    |  SOAP  |   WSHHRN          |  ADO   |   SQL Server   |
|   TOMHH2025/      | <----> |   TOMHHWS.asmx.vb | <----> |   por cliente  |
|   com.dts.*       |  REST? |   (legacy)        |        |   (BECO/K7/    |
|                   |        |                   |        |    MAMPA/BYB/  |
|                   |  REST  |   WMSWebAPI       |        |    CEALSA)     |
|                   | <----> |   (nueva, .NET)   | <----> |                |
+-------------------+        +-------------------+        +----------------+
                                      ^                          ^
                                      | calls                    |
                                      v                          |
                               +-------------------+             |
                               |   BOF (Front      |   ADO       |
                               |   Office)         | ----------->+
                               |   TOMIMSV4/...    |
                               |   VB.NET WinForms |
                               +-------------------+
                                      ^
                                      | spawns / hosts
                                      v
                               +-------------------+
                               |   Sync ERPs       |
                               |   SAPBOSync.exe   |
                               |   NavSync.exe     |
                               |   CEALSASync.exe  |
                               |   PerceptronUbi.. |
                               +-------------------+
                                      ^
                                      | conecta a
                                      v
                               +-------------------+
                               |   ERPs externos   |
                               |   SAP B1 / NAV /  |
                               |   Prefactura      |
                               +-------------------+
```

---

## 3. Repos

| Repo | Tamaño | Branch trabajo | Notas |
|---|---|---|---|
| TOMWMS_BOF | 376 MB | `dev_2028_merge` | Mono-solution con backend, WebService, WebAPI, syncs, BOF UI |
| TOMHH2025 | 14 MB | `dev_2028_merge` | App Android Java, package raiz `com.dts.*` |

GitHub `ejcalderongt/DBA` contiene los esquemas de DB. **No esta en
Azure DevOps.**

---

## 4. Mono-solution `TOMWMS_BOF` — modulos

Esto NO es un repo limpio: es una **solucion historica con multiples
modulos** acumulados a lo largo de años. Algunos activos, otros legacy,
otros experimentales.

### 4.1 Modulos confirmados activos (relevantes para code-deep-flow)

| Path | Tipo | Rol |
|---|---|---|
| `/TOMIMSV4/` | VB.NET WinForms | **BOF** — front office del WMS, gestion maestros / inventario / configuracion |
| `/WSHHRN/` | ASMX SOAP | **WebService legacy** que sirve a la HH (`TOMHHWS.asmx.vb`) y proxea a ERPs externos via 11 Web References |
| `/WMSWebAPI/` | .NET WebAPI moderno | **API REST nueva** en construccion (25 Controllers identificados, AutoMapper, JWT) |
| `/WMSPortal/` | Web | Portal web (UX) del WMS |
| `/TOMWMSUX/` | Web | Capa UX adicional (a confirmar diferencia con WMSPortal) |
| `/WMS.AppGlobalCore/` | .NET Class Library | App global compartida nueva (.NET Core / 6+) |
| `/WMS.DALCore/` | .NET Class Library | DAL nueva (Data Access Layer Core) |
| `/WMS.EntityCore/` | .NET Class Library | Entity nueva (modelos de dominio Core) |
| `/AppGlobal/` | .NET Framework | App global vieja (legacy, paralela a Core) |
| `/WSSAPSYNC/` | .NET | WebService de sync SAP |
| `/SAPSYNCCUMBRE/`, `/SAPSYNCMAMPA/`, `/SAPSYNC_Killios/` | Sync ejecutables | Variantes por cliente del sync SAP B1 |
| `/DynamicsNavInterface/` | .NET | Interface NAV (BYB) |
| `/CEALSAMI3/` | .NET | Interface CEALSA prefactura via MI3 |
| `/MI3/` | .NET | MI3 (significado pendiente — Q-MI3 abierta hace tiempo) |
| `/PrintService/`, `/PrintsService/`, `/ServicioImpresion/` | .NET | Servicios de impresion (3 variantes — investigar diferencias) |
| `/RFIDPrint/` | .NET | Impresion de etiquetas RFID |
| `/Quick_Tag/` | .NET | Generador de etiquetas Quick |
| `/WMSBDUpdater/`, `/AWS_WMSBD_Updater/` | .NET | Updaters de schema DB (local + AWS) |
| `/WMS.StockReservation2/`, `/WMS.StockReservation3/` | .NET | Servicio de reserva de stock (v2 y v3 conviven) |
| `/IAService/` | .NET | Servicio de IA (a investigar) |
| `/Perceptron/` | .NET | Modulo Perceptron (PerceptronUbicacion.exe — sugerencia de ubicacion ML) |
| `/PlotWH/` | .NET | Plotter de warehouse (visualizacion 2D layout) |

### 4.2 Modulos probablemente no relevantes para WebAPI .NET 10

| Path | Comentario |
|---|---|
| `/MES/` | Manufacturing Execution System (modulo separado) |
| `/DMS/` | Distribution Management (separado) |
| `/TMS/` | Transport Management (separado) |
| `/DMF/`, `/DashBorlin/` | Dashboards (separados) |
| `/GoCloud/`, `/GoCloudy/` | Experimentos cloud (a confirmar estado) |
| `/InstallerSW/`, `/SW/`, `/Help/`, `/HelpDev/` | Tooling y docs |
| `/Backup/`, `/FíxTheBug/`, `/TestMI3/`, `/TestMI3Console/`, `/TESTWCFHH/` | Backups y tests |
| `/Archivos de copia de seguridad de Crystal Reports/` | Reportes legacy |
| `/SAPMI3PC/`, `/reservastockfrommi3/` | Auxiliares MI3 |
| `/lib/`, `/tools/`, `/.cr/`, `/.github/` | Infraestructura repo |
| `/RFIDPrint/`, `/Quick_Tag/` | Auxiliares impresion |
| `/TomLic/` | Licenciamiento |
| `/WindowsApp1/` | Probable scaffolding Visual Studio sin uso |

### 4.3 TOMIMSV4 — sub-topologia BOF

```
TOMIMSV4/
├── DAL/                     legacy DAL .NET Framework
├── DataSets/                strongly-typed XSD datasets
├── Entity/                  modelos legacy
├── Images/                  recursos UI
├── Resources/
├── Service References/
├── TOMIMS_WCF/              servicio WCF interno
├── Transacciones/           operaciones del WMS (Ajustes, Picking, etc)
└── TOMIMSV4/                **NESTED LEGACY** — duplicacion historica
    └── Transacciones/...    (ver agent-context/AZURE_ACCESS.md sec 6)
```

⚠ La duplicacion `/TOMIMSV4/TOMIMSV4/` contiene archivos con hashes
distintos del de afuera. Convencion: **probable activo** es el de
afuera, salvo confirmacion contraria de Erik para cada archivo.

### 4.4 WSHHRN — handler SOAP unico + 11 Web References

```
WSHHRN/
├── TOMHHWS.asmx             handler SOAP (1 unico endpoint para HH)
├── TOMHHWS.asmx.vb          codigo del handler — TODOS los metodos
├── ChatGPTService.vb        servicio ChatGPT integrado (?)
├── Conn.ini                 conexion default
├── Conn_Becofarma.ini       conexion BECOFARMA
├── Conn - Cumbre.ini        conexion Cumbre (?)
├── CEALSASync.exe           sync CEALSA (deployado dentro de WSHHRN)
├── NavSync.exe.config
├── SAPBOSync.exe.config
├── PerceptronUbicacion.exe.config
├── WMS.exe.config           BOF deployado dentro de WSHHRN tambien (?)
├── WMS_PrintService.exe.config
├── DataSets/
├── My Project/              VB project metadata
├── Reportes/                rdlc / rpt
├── Solicitud_Traslado/
│   ├── clsInterfaceBase.vb
│   └── clsSyncSAPSSolicitudTraslado.vb
└── Web References/          11 proxies SOAP a ERPs externos
    ├── WSDevolucionVentaNAV/
    ├── WSLotePedidoCompra/
    ├── WSPaginaLotes/
    ├── WSPedidosCompraNAV/
    ├── WSRecepAlm/
    ├── WSTransferenciaEnvio/
    ├── wWSPicking/
    ├── wsBYBNavCUWMS/         BYB-specific
    ├── wsBYBNavMovProd/        BYB-specific
    ├── wsBYBNavUInternas/      BYB-specific
    ├── wsBYBNavUbicarAlmacen/  BYB-specific
    └── wsTransferenciaIngresoNAV/
```

**Lectura clave**: WSHHRN no es solo "el WebService de la HH". Es
ademas el **router proxy hacia los ERPs externos**. Cada metodo SOAP de
`TOMHHWS.asmx.vb` puede a su vez llamar a uno o mas Web References
para reflejar la operacion en el ERP del cliente.

### 4.5 WMSWebAPI — la API .NET nueva

```
WMSWebAPI/
├── Program.cs                      bootstrap .NET
├── ApplicationDbContext.cs         EF Core context
├── WMSWebAPI.csproj
├── appsettings.json + Development
├── web.config
├── Controllers/                    25 controllers HTTP
├── Services/                       16 carpetas de business logic
├── Models/                         DTOs / entidades
├── Mapping_Profile/                AutoMapper profiles
├── AppGlobal/                      shim a la app global Core
├── Logs/
├── Properties/
└── .config/
```

#### Controllers (25)
Maestros: Acuerdo, AcuerdoComercialWebhook, Bodega, CambioEstado,
CentroCosto, Clasificacion, Cliente, Familia, Marca, Presentacion,
Productos, Propietario, PropietarioWebhook, Proveedor, TipoProducto.

Operativo: Ajuste, KPI, Movimientos, PreFactura, Stock, Umbas.

Sync: SyncIngresos, SyncSalidas.

Auth: Auth, TestAuth.

#### Services (16 carpetas)
Acuerdos, Ajustes, Cambio_Estado, Centro_Costo, Cliente, Ingresos, KPI,
LogPortalUx, Login_JWT, Prefactura, Producto, Propietarios, Proveedor,
Reset_Password, Salidas + raiz Services.

**Observacion**: hay Controllers SIN carpeta Services correspondiente
(Bodega, CambioEstado, Marca, Movimientos, Stock, Umbas, etc) — la
logica esta inline en el Controller o vive en otra capa (DALCore /
EntityCore). Investigar caso por caso al hacer cada traza.

---

## 5. App Android `TOMHH2025` — sub-topologia HH

```
app/src/main/java/com/dts/
├── base/                    infraestructura compartida
│   ├── WebService.java          cliente SOAP a WSHHRN (legacy)
│   ├── ApiService.java          cliente REST nuevo
│   ├── RetrofitClient.java      bootstrap Retrofit (REST)
│   ├── XMLObject.java           parser XML SOAP
│   ├── appGlobals.java          singleton de configuracion
│   ├── clsClasses.java          clases base de dominio
│   ├── AppMethods.java          utilidades transversales
│   ├── DateUtils.java
│   ├── DecimalDigitsInputFilter.java
│   ├── ExDialog.java
│   ├── MiscUtils.java
│   ├── NetworkUtils.java + NetWorkInfoUtility.java
├── classes/                 POJOs de dominio (Mantenimientos / Transacciones)
├── ladapt/                  RecyclerView adapters (30 archivos)
├── rfid/                    RFID (11 archivos)
├── servicios/               Android Services background
│   ├── srvBaseJob.java
│   ├── srvCantTareas.java        servicio de cant. tareas
│   ├── startCantTareas.java      starter
│   ├── wsBase.java               base WebService client
│   └── wsCantTareas.java         cliente especifico cantidad tareas
└── tom/                     UI: MainActivity, Mainmenu, PBase, 61 frm_*
```

**Lectura clave**: la HH tiene **dos clientes HTTP en paralelo**:
- `WebService.java` → SOAP a `TOMHHWS.asmx` (WSHHRN). Camino legacy.
- `ApiService.java` + `RetrofitClient.java` → REST a `WMSWebAPI`.
  Camino nuevo.

Convivencia activa. Cada nueva pantalla puede usar uno u otro segun
estado de migracion. **Confirmar caso por caso en cada traza**.

64 activities declaradas en `AndroidManifest.xml`. Solo
`MainActivity.java` y `PrintReceiverActivity.java` usan sufijo
`Activity` en filename. El resto son `frm_*`. Para mapear activities
reales, parsear el manifest, no listar filenames.

---

## 6. Relacion BOF ↔ WSHHRN ↔ WMSWebAPI ↔ HH ↔ DB

### 6.1 Quien escribe el parametro
Casi siempre el BOF (`TOMIMSV4/`). Cuando hay admin via Portal Web,
puede escribirse desde `WMSPortal/` o `TOMWMSUX/`.

### 6.2 Quien lo lee primero
- BOF lo lee directo de DB via DAL (legacy o Core).
- WSHHRN lo lee via DAL legacy y lo expone a la HH.
- WMSWebAPI lo lee via DALCore + EntityCore y lo expone a la HH (REST).
- HH lo recibe via SOAP o REST y lo cachea en `appGlobals.java`.

### 6.3 ¿Se sincroniza al ERP externo?
Solo si el sync correspondiente del cliente lo lee. Los syncs son
ejecutables independientes (`*Sync.exe`) que corren en background y
mueven datos de DB a ERP y viceversa.

### 6.4 ¿Pasa por reserva de stock?
Si el parametro afecta picking / reserva, los servicios
`WMS.StockReservation2/` y `/3` participan. Conviven dos versiones —
investigar cual usa cada cliente.

---

## 7. Hipotesis abiertas a nivel arquitectura

- **Q-WSHHRN-AS-PROXY-BYB**: las 4 Web References `wsBYBNav*` indican
  que el WebService llama al ERP NAV de BYB en linea (no via sync). Es
  asi para todas las operaciones BYB? Solo para las criticas?
- **Q-WMSWEBAPI-MIGRACION-MAPA**: que % de la funcionalidad de WSHHRN
  ya esta migrada a WMSWebAPI? Cuales endpoints siguen solo en SOAP?
- **Q-PERCEPTRON-USO-REAL**: PerceptronUbicacion.exe esta deployado en
  los 5 clientes pero solo K7+BYB tienen `calcular_ubicacion_sugerida_ml`.
  Que hace en los otros 3?
- **Q-MI3-QUE-ES**: MI3 / CEALSAMI3 / SAPMI3PC / TestMI3 — modulo
  recurrente que no tenemos identificado. Erik lo definio como capa
  intermedia de prefactura?
- **Q-WMS-EXE-CONFIG-EN-WSHHRN**: por que WMS.exe.config (config del BOF)
  esta dentro de WSHHRN? Deploy compartido?
- **Q-CHATGPT-SERVICE**: ChatGPTService.vb en WSHHRN — feature en uso
  o experimento?
- **Q-3-PRINT-SERVICES**: PrintService / PrintsService / ServicioImpresion
  — 3 variantes? Cuales activos por cliente?
- **Q-STOCK-RESERVATION-2-VS-3**: cual usa cada cliente?
- **Q-TOMWMSUX-VS-WMSPORTAL**: dos webs distintas — diferencia funcional?
- **Q-DALCORE-VS-DAL-LEGACY**: WebAPI usa DALCore. WSHHRN usa DAL
  legacy. ¿Conviven con misma DB? ¿Mismas queries?

---

## 8. Como esta usado este mapa

- Las trazas (`traza-NNN-*`) lo referencian en lugar de re-explicar la
  arquitectura.
- Cuando una traza encuentra una caja nueva, **se actualiza este
  archivo primero**, despues se escribe la traza.
- Cuando se confirma una hipotesis Q-* de la seccion 7, se mueve a
  `_index/INDEX.md` como hallazgo cerrado y se quita de aca.
