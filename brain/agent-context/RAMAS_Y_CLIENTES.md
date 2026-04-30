---
id: RAMAS_Y_CLIENTES
tipo: agent-context
estado: vigente
clientes: [killios, mercopan, byb, becofarma, cealsa, mampa, idealsa, merhonsa]
ramas: [dev_2023_estable, dev_2028_merge]
tags: [agent-context/clientes, agent-context/ramas]
---
# Ramas y clientes — TOMWMS BOF + HH

**Fecha**: 2026-04-28  
**Confirmado por**: Erik Calderón (Wave 6.1)  
**Aplica a**: TODO scan/análisis de los repos `TOMWMS_BOF` y `TOMHH2025`

---

## 0. TL;DR — qué rama corre cada cliente HOY

| Cliente | Rama producción HOY | Estado | DB |
|---|---|---|---|
| **BECO** | `dev_2023_estable` | Producción estable | `IMS4MB_BECOFARMA_PRD` |
| **K7** | `dev_2023_estable` | Producción estable | `TOMWMS_KILLIOS_PRD` |
| **BYB** | `dev_2023_estable` | Producción estable | `IMS4MB_BYB_PRD` |
| **CEALSA** | `dev_2023_estable` | Producción estable | `IMS4MB_CEALSA_QAS` |
| **MAMPA** | `dev_2028_merge` | **Pruebas integrales en curso** | `TOMWMS_MAMPA_QA` |
| **Cumbre** | `dev_2028_Cumbre` (HH) | Rama dedicada | (pendiente confirmar DB) |
| **MHS** | (no aplica HH) | **Nuevo cliente B2B vía WMSWebAPI** | (no en EC2 — su propio entorno) |

**Implicación crítica para todo el brain**: el scan exhaustivo de Wave 1-6 fue sobre `dev_2028_merge` en ambos repos. Esto significa que **lo que documenté hasta ahora corresponde a la versión FUTURA** que solo está en producción para MAMPA (y aún en pruebas). Para los 4 clientes restantes (BECO/K7/BYB/CEALSA), el código real difiere — ver §3 abajo para qué se diferencia.

---

## 1. Inventario completo de ramas

### 1.1 TOMWMS_BOF (5 ramas)
- `master` ← rama default, contenido pendiente verificar
- `dev_2023_estable` ← producción 4 clientes
- `dev_2025` ← intermedia, propósito pendiente confirmar
- `dev_2028_merge` ← producción MAMPA + scan Wave 1-6
- `backup/dev_2028_merge_backup` ← backup

Tamaño total repo: 377 MB.

### 1.2 TOMHH2025 (25+ ramas)
**Producción / mainline**:
- `master` (default)
- `dev_2023_estable` ← producción 4 clientes
- `dev_2023_estable_merge_dev`
- `dev_2023_merge_WMS5`
- `dev_2025`
- `dev_2028_merge` ← MAMPA + scan Wave 1-6
- `dev_2028_merge_mlc` ← propósito MLC pendiente confirmar
- `dev_2028_Cumbre` ← Cumbre dedicada

**Por cliente** (forks históricos):
- `byb`, `240byb`
- `cealsa`, `240Cealsa`
- `240`, `240ejc`
- `mercopan`, `mercosal`

**Por feature** (en curso o legacy):
- `dev_filtro_picking`
- `dev_operador_bodega`
- `dev_stock_jornada`
- `dev_demo_uma`
- `dev_ssl`
- `dev_2024_cambio_ubic`

**Históricas**:
- `dev`
- `dev_102022`
- `dev_112022`

Tamaño repo: 14 MB.

### 1.3 Otros repos relacionados (Azure DevOps `ejcalderon0892`)
- `TOMWMS_BOF`, `TOMHH2025` ← scope principal
- **`TOMWMS5`** — proyecto existe pero **vacío (0 MB, sin ramas)**. ¿Placeholder? ¿Migración fallida?
- `TOMWMS` — repo separado. Pendiente investigar.
- `TOMWeb` — portal web del WMS, separado del BOF.
- `CEALSA` — ¿solución específica de CEALSA? Pendiente.
- `mPos2025`, `RoadPOD`, `ROAD_FORTUNA`, `ROAD_TOLEDANO` — proyectos de la otra línea (POS/Road).
- `DMF`, `GenCodeSQL` — utilidades.

---

## 2. La transición 2023 → 2028 — major change

**Erik (2026-04-28)**: "Esta es una transición difícil o major change para nuestro WMS, porque debemos llevar a todos nuestros clientes a la nueva rama o versión 2028."

**Estado actual del rollout**:
1. **MAMPA** está en `dev_2028_merge` en pruebas integrales — primer cliente migrado.
2. **Cumbre** tiene su propia rama HH `dev_2028_Cumbre` — probablemente preparándose para migrar también.
3. **BECO, K7, BYB, CEALSA** siguen en `dev_2023_estable` — pendientes migración.

**Q-MIGRACION-2023-A-2028 (abierta)**:
- ¿Cuál es el orden planificado de migración?
- ¿Hay scripts de migración de DB schema entre 2023 y 2028 (cambios estructurales en tablas)?
- ¿Qué cliente sigue después de MAMPA? ¿K7 (DB más compleja)? ¿BECO (más estable)?
- ¿Cuáles son los breaking changes del WMS 2028 que justifican que sea major?

---

## 3. Diferencias 2023 vs 2028 (alto nivel — comparado vía git ls-tree)

### 3.1 BOF — features SOLO 2028

**Componentes nuevos completos**:
- **`WMS.DALCore`** ← capa Data Access Layer en .NET, no existe en 2023
- **`WMS.EntityCore`** ← capa Entity en .NET, no existe en 2023
- **`WMS.AppGlobalCore`** ← capa de configuración global, no existe en 2023
- **`WMS.StockReservation2`**, **`WMS.StockReservation3`** ← reservas de stock nuevas
- **`reservastockfrommi3`** ← bridge nuevo con MI3 para reservas
- `GoCloud`, `GoCloudy` ← componentes cloud
- `InstallerSW`, `WindowsApp1`, `tools` ← installers + utilidades

**Meta-docs nuevas**:
- `AGENTS.md`, `CLAUDE.md`, `CONVENTIONS.md`, `PARCHES_APLICADOS.md`
- `.cursorrules`, `.github` ← uso de IDEs IA + CI/CD

### 3.2 BOF — componentes que existen en AMBAS pero con cambios

**`WMSWebAPI`**: existe en 2023 y 2028 (hashes distintos).
- **Implicación**: el WebAPI **no es greenfield 2028** como yo había documentado en traza-001. Es un componente que se viene desarrollando desde antes.
- Erik aclaró el rol real (Wave 6.1): "El WebAPI solo estará en uso hasta que salgamos en vivo con MHS — primer proyecto B2B vía WebAPI. El cliente tiene su equipo de desarrollo y escribe datos maestros, lee transacciones de entrada, salida y ajuste vía los WebAPI. **NO pretende reemplazar ni a MI3 ni al WS de la HH o para la HH**."
- **Lectura corregida**: WMSWebAPI = canal B2B para integraciones externas (clientes con desarrollo propio). Sigue habiendo MI3 + WSHHRN para el resto.

**`WSHHRN/TOMHHWS.asmx.vb`** (servicio SOAP HH):
- 2023: 17.759 líneas / **340 funciones públicas** / 10 endpoints LP-related
- 2028: 19.492 líneas / **369 funciones públicas** / 11 endpoints LP-related
- Delta: **+1.733 líneas, +29 funciones, +1 endpoint LP en 2028**
- **Implicación**: 340 endpoints HH ya existían en 2023. La traza-001 cuenta 21 endpoints LP — pero al menos 10 ya estaban en 2023. Solo 1 endpoint LP es nuevo 2028. Hay que validar cuál.

**`WSHHRN/ChatGPTService.vb`** (issue de seguridad — API key OpenAI hardcoded):
- **EXISTE EN AMBAS RAMAS** ← el leak es PRE-EXISTENTE 2023.
- **Implicación**: la API key OpenAI lleva expuesta en el repo desde antes de 2023 — más tiempo del estimado. **Severidad sube. Rotar de inmediato.**

### 3.3 HH — diferencias

**Estructura top-level idéntica** entre 2023 y 2028 (ningún directorio nuevo o eliminado).

**`clsBeProducto.java`** (modelo del producto en HH):
- 2023 y 2028: idéntico hash, 8 ocurrencias de `Genera_lp` ambas
- **Implicación**: el modelo del LP en la HH no cambió entre 2023 y 2028.

**`frm_recepcion_datos.java`** (form de recepción HH):
- 2023: 10.448 líneas
- 2028: 11.481 líneas
- Delta: +1.033 líneas (refactor interno)

**`ApiService.java`** (capa REST de la HH):
- **Hash idéntico en ambas ramas** — la capa REST de HH no es feature 2028.

### 3.4 Resumen del impacto sobre el brain Wave 1-6

| Sección documentada | Validez en `dev_2023_estable` | Acción |
|---|---|---|
| Endpoints LP del `TOMHHWS.asmx.vb` (21) | ~95% válida (10/11 LP ya en 2023) | Marcar 1 como "solo 2028" tras identificar |
| Modelo `clsBeProducto` HH | 100% válida | Sin cambios |
| `genera_lp` (bodega) + `genera_lp_old` (producto) en BOF | Pendiente verificar — si la columna existe en DB, lo más probable es que sí (DBs son las mismas) | Validar SQL |
| **Capa `WMS.DALCore` con patrón `serializado+genera_lote+genera_lp_old+control_vencimiento` consecutivo** | **NO EXISTE EN 2023** | **Solo aplica a MAMPA y futura migración** |
| `WMSWebAPI` Sync/* | Existe en ambas, pero contenido pudo cambiar | Validar archivo por archivo si se quiere usar como referencia universal |
| Issue `Q-SEC-OPENAI-KEY-LEAK` | Aplica a AMBAS ramas | Severidad alta, rotar key + scrubbing del repo |
| Issue `Q-SEC-CONNINI-CREDS` | Pendiente verificar en 2023 | Probable que aplique a ambas |

---

## 4. Clientes — perfil expandido

### 4.1 BECO (Becofarma)
- DB: `IMS4MB_BECOFARMA_PRD`
- Rama: `dev_2023_estable`
- ERP: IMS4 (suite propia DTS)
- Particularidades: control_lote + control_vencimiento críticos (farma)

### 4.2 K7 (Killios)
- DB: `TOMWMS_KILLIOS_PRD`
- Rama: `dev_2023_estable`
- ERP: SAP (probable — pendiente confirmar)
- Particularidades: serializado + LP intensivos

### 4.3 BYB
- DB: `IMS4MB_BYB_PRD`
- Rama: `dev_2023_estable`
- ERP: **NAV (Microsoft Dynamics NAV)** ← único cliente con NAV
- Particularidades: integración WSHHRN ↔ NAV vía web services SOAP

### 4.4 CEALSA
- DB: `IMS4MB_CEALSA_QAS`
- Rama: `dev_2023_estable`
- ERP: IMS4 (probable)
- Repo dedicado existe: `CEALSA` (separado, propósito pendiente)

### 4.5 MAMPA
- DB: `TOMWMS_MAMPA_QA`
- Rama: **`dev_2028_merge` (pruebas integrales)**
- ERP: pendiente confirmar
- Estado: primer cliente en migrar a 2028

### 4.6 Cumbre
- Rama HH: **`dev_2028_Cumbre`** (dedicada)
- DB: pendiente confirmar (no aparece en EC2 que escaneamos)
- Estado: probablemente preparándose para 2028

### 4.7 MHS — nuevo cliente B2B
- **NO está en EC2 (no en mi scan)**
- **Primer caso B2B vía WMSWebAPI**
- El cliente tiene su propio equipo de desarrollo
- Patrón de uso:
  - **Escribe datos maestros** vía WebAPI (`POST/PUT` a controllers de Producto, Bodega, etc.)
  - **Lee transacciones** de entrada, salida y ajuste vía WebAPI (`GET` a controllers Sync)
- Sin HH en línea con WSHHRN — la integración es 100% asincrónica vía WebAPI

---

## 5. Q-* abiertas relacionadas a ramas y clientes

- **Q-MIGRACION-2023-A-2028**: orden de rollout, cronograma, breaking changes.
- **Q-DALCORE-PROPOSITO**: ¿reemplaza a `WMS.DAL` legacy? ¿coexiste? ¿se invoca desde WSHHRN o desde WebAPI?
- **Q-MHS-COMO-CLIENTE**: scope completo del WebAPI con MHS, fecha go-live, qué datos maestros escriben (productos? bodegas?), qué transacciones leen.
- **Q-WMS5-VACIO**: ¿qué fue/iba a ser TOMWMS5? ¿abandonado? ¿en migración?
- **Q-HH-RAMAS-25**: política de ramas HH — ¿se planea consolidar? ramas por cliente activo (`byb`, `cealsa`, `mercopan`, `mercosal`) ¿están aún en uso o son históricas?
- **Q-MERCOPAN-MERCOSAL**: ramas con nombres de cliente que NO están en la lista de clientes activos (BECO/K7/BYB/CEALSA/MAMPA/Cumbre/MHS) — ¿clientes históricos? ¿prospectos?
- **Q-CUMBRE-RAMA-DEDICADA**: ¿por qué Cumbre tiene rama propia HH y no comparte con `dev_2028_merge`?
- **Q-DEV2025-PROPOSITO**: existe rama `dev_2025` en BOF y HH — ¿qué contiene? ¿es paso intermedio entre 2023 y 2028?
- **Q-MASTER-PROPOSITO**: `master` es default en ambos repos — ¿en qué se diferencia de `dev_2023_estable`? ¿es liberación oficial vs trabajo activo?

---

## 6. Disclaimer global a aplicar en TODO el brain

> **Salvo indicación explícita de "código 2023" o "código 2028", el brain documenta sobre el código de `dev_2028_merge` (BOF + HH). Para clientes BECO/K7/BYB/CEALSA en producción, el código en uso es `dev_2023_estable` y puede tener diferencias significativas — ver `agent-context/RAMAS_Y_CLIENTES.md`.**

Aplicar este disclaimer en:
- `_index/INDEX.md` (header)
- `code-deep-flow/00-mapa-de-cajas.md` (sección 0)
- Cada `traza-NNN-*.md` nueva (sección 0)
- `traza-001-license-plate.md` ← retroactivo (ya escrita Wave 6)
