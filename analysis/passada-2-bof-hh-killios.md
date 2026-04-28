# Ciclo #2 — Reporte de validación BOF↔HH↔Killios (v2 con clsLn ubicado + Wiki verificada)

**Generado**: 2026-04-27T03:32:50.078Z
**Branch fuente Azure DevOps**: `dev_2028_merge` (TOMWMS_BOF + TOMHH2025).
**Acceso**: API REST Azure DevOps con PAT (Erik), sin clone full (376 MB BOF).
**Estado**: ciclo #2 cerrada, lista para iniciar #3.1 (D+E+A).

## 1. Topología confirmada

- **BOF**: 75 entradas en raíz (TOMIMSV4, WSHHRN, WSSAPSYNC, WMSWebAPI, WMSPortal, DMS, MES, MI3, IAService, varios SAPSYNC*).
- **WSHHRN** = único punto de contacto HH↔BOF: 1 ASMX (`/WSHHRN/TOMHHWS.asmx`), 22 .vb, 12 .wsdl, 13 .config.
- **HH** = 405 .java en `com.dts.*`, 64 activities, 13 módulos operativos.

## 2. WebMethods del WSHHRN (TOMHHWS.asmx)

- **343 WebMethods** expuestos (todos con `<WebMethod(), SoapHeader("mArch")>`).
- **Logging transversal**: 432 invocaciones a `clsLnLog_error_wms*` (todas las WMs loguean errores). Ruido para mapa de dominio.
- **97 clases Logic distintas invocadas** (de las cuales 89 son negocio real, 8 son loggers).

### 2.1 WebMethods por categoría operativa (heurística por nombre)

| Categoría | # WMs |
|--|--|
| READ (Get_*, List_*, Buscar_*) | 197 |
| Otros (sin prefijo claro) | 143 |
| VALIDATE | 2 |
| CREATE | 1 |
| DELETE | 1 |

### 2.2 WebMethods por dominio funcional

| Dominio | # WMs |
|--|--|
| bodega | 75 |
| inventario | 51 |
| recepcion | 35 |
| picking | 34 |
| empresa | 21 |
| producto | 21 |
| ordencompra | 11 |
| pedido | 9 |
| verificacion | 7 |
| reglas | 6 |
| packing | 5 |
| rfid | 2 |
| config | 1 |
| reabastecimiento | 1 |
| otros | 65 |

## 3. Clases Logic — UBICACIÓN RESUELTA ✓

**Hallazgo crítico revertido**: el reporte v1 indicaba que las clases `clsLn*` no estaban en el repo. Erik confirmó y verificamos:

| Origen | Tecnología | Path Azure DevOps | Path local Erik | Total .vb / .cs | clsLn* | Para qué |
|--|--|--|--|--|--|--|
| `DAL.vbproj` | .NET 4.8 | `/TOMIMSV4/DAL/` | `C:\Users\yejc2\source\repos\TOMWMS\TOMIMSV4\DAL` | 641 .vb | **623** | BOF, WSHHRN, WCFTOM4, interfaces generales |
| `WMS.DALCore.csproj` | .NET Core | `/WMS.DALCore/` | (mismo repo) | 141 .cs | **141** | WMSWebAPI |

**Total clases Logic disponibles: 764** vs las **97 invocadas desde TOMHHWS.asmx** (~13% del total). El otro 87% son clases que usan BOF directo, MI3, WMSPortal, WCFTOM4 y otras interfaces.

### 3.1 Subcarpetas de `/TOMIMSV4/DAL/` (.NET 4.8)

`Analitica`, `Configuracion_Usuario`, `DataHistorica`, `Diferencias_Movimientos`, `FixTheBug`, `General`, `Interface`, `Inventario`, `Licencia`, `Log`, `Mantenimientos`, `My Project`, `QA`, `QuickTag`, `RefAppGlobal`, `Resources`, `Road`, `Sistema_Jornada`, `TMS`, **`Transacciones`**, `Ubicacion_Sugerida`.

### 3.2 Subcarpetas de `/WMS.DALCore/` (.NET Core)

`Acuerdos`, `Ajustes`, `Centro_Costo`, `Cliente`, `Datos_Maestros`, `Despacho`, `I_nav_*` (3), `Interface`, `Log`, `Mensaje_regla`, `Movimientos`, `Operador`, `Pedido`, `Picking`, `Prefactura`, `Producto`, `Propietario`, `Proveedor`, `Reserva_Stock`, `Reset_Password`, `Road`, `Stock`, `Ticket`, `Trans_oc`, `Trans_re`, `Transacciones`, `VW_Despacho_Rep`.

### 3.3 Top 20 clases Logic invocadas desde el WSHHRN (de las 97)

| Clase Logic | # WMs | Dominio inferido |
|--|--|--|
| `clsLnProducto` | 29 | MAE_Producto |
| `clsLnStock` | 28 | MAE_Stock |
| `clsLnTrans_picking_ubic` | 21 | Picking |
| `clsLnTrans_re_enc` | 18 | Recepcion |
| `clsLnStock_res` | 15 | StockReservado |
| `clsLnTarea_hh` | 14 | Sistema_HH |
| `clsLnStock_rec` | 13 | Recepcion |
| `clsLnLog_verificacion_bof` | 12 | Verificacion |
| `clsLnTrans_ubic_hh_det` | 11 | CambioUbicacion |
| `clsLnTrans_picking_enc` | 11 | Picking |
| `clsLnBodega` | 9 | MAE_Bodega |
| `clsLnBodega_ubicacion` | 9 | CambioUbicacion |
| `clsLnTrans_inv_detalle` | 9 | Inventario |
| `clsLnTrans_re_det` | 8 | Recepcion |
| `clsLnTrans_pe_enc` | 8 | Pedido |
| `clsLnEmpresa` | 7 | MAE_Empresa |
| `clsLnProducto_estado` | 6 | MAE_Producto |
| `clsLnProducto_presentacion` | 6 | MAE_Producto |
| `clsLnTrans_inv_ciclico` | 6 | Inventario |
| `clsLnTrans_inv_resumen` | 6 | Inventario |

## 4. Cobertura via TableAdapters (XSDs DataSets)

- **16 XSDs analizados** (8 globales `/TOMIMSV4/TOMIMSV4/*.xsd` + 8 locales en módulos Inventario/Recepcion/Pedido).
- **23 referencias DbSource** (22 a tablas + 1 a vista, 0 a SPs).
- **Match Killios**: 23/23 (100% — todo lo referenciado existe en BD).
- **Cobertura por tipo**: SPs **0/39 (0%)**, Vistas **1/220 (0.5%)**, Tablas **17/345 (4.9%)**.

**Conclusión**: los XSDs NO son la fuente de verdad. La fuente real son las 764 clases `clsLn*` recién ubicadas.

## 5. WebService.java de HH (com.dts.base.WebService)

- Wrapper genérico: 8 métodos (`callMethod`, `callMethodJsonGet`, `callMethodJsonPost`, `callMethodSoap11`, `wsExecute`, `execute`).
- Patrón: SOAP a `http://tempuri.org/<MethodName>` con header `mArch=Andr`.
- **Implicancia**: para mapear "activity HH → WM" hay que grep de los 405 .java por strings literales (los 343 nombres del WSHHRN).

## 6. Asimetría BOF vs HH

**HH = subset estricto del BOF para flujos físicos**: Recepción, Picking, Packing, Inventario, CambioUbicación, Verificación, RFID, ConsultaStock, Reabastecimiento, ReubicarStockRes, ProcesaImagen.

**13 módulos BOF que el HH NO toca**: Ajustes, Pedido (7 forms), OrdenCompra*, Despacho, Manufactura, PreFactura, PreIngreso, Servicios, StockReservado (forms BOF), ControlCalidad, Implosion, PedidosMI3, RevisionProducto.

## 7. WikiHub Portal — VERIFICADA ✓

**URL**: `https://tomwms-wikidev.replit.app`.
**Auth**: público para lectura. Solo módulo escenarios ROI usa Clerk.
**Stack**: Express + Postgres + arrays TS hardcoded. Postgres → release-notes + cambios BD/código. TS arrays → tech-docs + changelog. Jira en vivo → vía PAT (no expuesto al Brain).

**Endpoints probados (HTTP 200, fetch JSON directo, sin scraping HTML):**

| Endpoint | Tamaño | Estado | Uso para Brain |
|--|--|--|--|
| `/api/modules` | 773 B | ✓ 10 módulos | entities `wiki-module` |
| `/api/clients` | 787 B | ✓ 11 clientes | entities `wiki-client` |
| `/api/release-notes` | 7.5 KB | ✓ 26 releases (v5.0-v5.8.0) | entities `wiki-release-note` |
| `/api/release-notes/timeline` | 2 B | ⚠️ vacío `[]` | descartar |
| `/api/tech-docs` | 1.9 KB | ✓ tech docs | entities `wiki-tech-doc` |
| `/api/stats/dashboard` | 1.9 KB | ✓ totales y byProductLine | metadato global |
| `/api/stats/by-month` | 2 B | ⚠️ vacío `[]` | descartar |

**Stats actuales** (de `/api/stats/dashboard`): 26 releases, 11 clientes, 10 módulos. Por producto: BOF 18 / TODOS 5 / HH 2 / MI3 1.

## 8. Validaciones de SKILL contra fuente real

| Afirmación SKILL | Realidad | Estado |
|--|--|--|
| HH: 13 módulos operativos | 13 confirmados en AndroidManifest | ✓ |
| HH: 64 activities | 64 confirmadas | ✓ |
| BOF: "10 módulos operativos" | **23 carpetas en /Transacciones/** | ✗ corregir SKILL |
| WSHHRN: comunicación HH↔BOF | 1 ASMX único confirmado | ✓ |
| Killios: 345 tablas + 39 SPs | confirmado en catálogo (621 obj total) | ✓ |
| Wiki: documentada con 50 procesos / 542 obj BD / 1.152 módulos | API responde con 10 módulos / 11 clientes / 26 releases | ⚠️ stats SKILL vs API difieren — ver §7 |

## 9. Camino habilitado para #3.1 (D + E + A)

Con los hallazgos de #2 y los datos provistos por Erik, queda habilitado:

- **Capa E (HH↔BOF)**: bajar 97 clsLn invocadas + parsear → mapear los 343 WMs a SPs/tablas reales. **Cobertura proyectada Killios: 75-80%** (vs 5% solo con XSDs).
- **Capa D (Parametrizaciones)**: cruzar tablas `valores_fijos*`, `config_*`, `i_nav_config_*`, `cliente_config*`, `bodega_config*` en Killios + parsear `clsLnConfiguracion_*.vb` (existen `_det`, `_enc`, `_partial` confirmados en `/DAL/Configuracion_Usuario/`). Lectura **read-only de valores reales en producción**.
- **Capa A (no-código)**: 16 XSDs (ya descargados) + .config + .resx + AndroidManifest (ya parseado).
- **Capa C (Wiki)** [se hace en 3.2]: scrap directo de 5 endpoints vivos (modules, clients, release-notes, tech-docs, stats/dashboard).

## 10. Pendientes auditados

1. **Stats Wiki** difieren del SKILL (10 vs 50 procesos / 11 clientes / 26 releases). Consultar a Erik si hay otra fuente o si el SKILL tiene números legacy.
2. **WMS.DALCore (.NET Core)** todavía no fue cruzado contra WSHHRN — ese DAL alimenta WMSWebAPI, no el WSHHRN. Cruzarlo en #3.1 si Erik lo pide.
3. **Mapa HH activity → WebMethod**: requiere grep de los 405 .java HH por strings literales (los 343 nombres). Costo estimado 1-2 h. Programado para #3.1.E2.
