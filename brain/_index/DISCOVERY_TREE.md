# DISCOVERY_TREE — Mapa-metadato del brain

> **Para qué existe este archivo**: es la primera lectura que debe hacer cualquier programador (Carolina, Erik en otro día, un sucesor, un agente futuro) que llega al brain por primera vez. Responde tres preguntas al mismo tiempo:
>
> 1. **¿Qué hay acá?** — inventario de qué carpetas existen y qué información vive en cada una.
> 2. **¿Cómo llegamos hasta acá?** — historial humano-legible de descubrimientos por wave (qué gatilló qué, vía qué método).
> 3. **¿Qué tenés que saber adicional para no perderte?** — convenciones, glosario, jerarquías, recorrido recomendado.
>
> **No es** un índice exhaustivo de cada archivo (eso es [`INDEX.md`](INDEX.md)) ni una vista temática por dominio (eso es [`CONCEPT_MAP.md`](CONCEPT_MAP.md)). Es el **metadato** que hace navegables a los otros dos.

**Última actualización**: 2026-04-29 (post-Wave 9 + bloque 13 cuestionario)
**Versión del brain**: 9
**Total de archivos brain documentados**: ~95 .md + 35 directorios

---

## Tabla de contenidos

1. [Cómo leer este archivo](#1-cómo-leer-este-archivo)
2. [Glosario mínimo](#2-glosario-mínimo)
3. [Recorrido recomendado para newcomer](#3-recorrido-recomendado-para-newcomer)
4. [Mapa del brain — qué vive en cada carpeta](#4-mapa-del-brain--qué-vive-en-cada-carpeta)
5. [Árbol cronológico Wave 1 → Wave 9](#5-árbol-cronológico-wave-1--wave-9)
   - [Wave 1 — Fingerprints cross-cliente](#wave-1--fingerprints-cross-cliente-2026-04-28)
   - [Wave 4 — Heat-map params cross-cliente](#wave-4--heat-map-params-cross-cliente-2026-04-28)
   - [Wave 5 — Capa producto cerrada](#wave-5--capa-producto-cerrada-2026-04-28)
   - [Wave 6 — Code-deep-flow bootstrap + traza-001 LP](#wave-6--code-deep-flow-bootstrap--traza-001-lp-2026-04-28)
   - [Wave 6.1 — DALCore + DMS + Portal CEALSA + ramas 2023-2028](#wave-61--dalcore--dms--portal-cealsa--ramas-2023-2028-2026-04-28)
   - [Wave 6.2 — Quick wins SQL/grep](#wave-62--quick-wins-sqlgrep-2026-04-28)
   - [Wave 7 — Holding IDEALSA + implosión + capabilities](#wave-7--holding-idealsa--implosión--capabilities-2026-04-29)
   - [Wave 8 — Clavaud + MI3 + algoritmo de reserva 2028](#wave-8--clavaud--mi3--algoritmo-de-reserva-2028-2026-04-29)
   - [Wave 9 — Casos naturales de reserva + diario naked-erik](#wave-9--casos-naturales-de-reserva--diario-naked-erik-2026-04-29)
6. [DAG de dependencias entre waves](#6-dag-de-dependencias-entre-waves)
7. [Snapshot estado actual del brain](#7-snapshot-estado-actual-del-brain)
8. [Apéndice A — Inventario completo de aprendizajes (L-*)](#apéndice-a--inventario-completo-de-aprendizajes-l)
9. [Apéndice B — Inventario completo de hipótesis (H-*)](#apéndice-b--inventario-completo-de-hipótesis-h)
10. [Apéndice C — Cómo agregar una wave nueva a este árbol](#apéndice-c--cómo-agregar-una-wave-nueva-a-este-árbol)

---

## 1. Cómo leer este archivo

Este archivo está pensado para tres modos de lectura distintos, según para qué llegues:

**Modo "soy newcomer absoluto"**: leé secciones 1 → 2 → 3 → 4 → 5 (de corrido). Tiempo estimado: 20 minutos. Al final tenés contexto suficiente para abrir cualquier doc del brain sin estar perdido.

**Modo "necesito entender de dónde salió tal hallazgo"**: andá directo a sección 5 y buscá la wave que tenga sentido por fecha o por tema. Cada wave tiene `Trigger`, `Hallazgos brutales`, `Q-* tocadas`, `Archivos generados`. Los archivos están enlazados.

**Modo "ya conozco el brain pero hace tiempo no toco"**: secciones 6 (DAG) + 7 (snapshot) en 5 minutos te ponen al día.

Una nota brutalmente honesta: este archivo **no reemplaza** a [`INDEX.md`](INDEX.md). El INDEX es la cronología densa con métricas; este archivo es el meta-orientador con links. Son complementarios y se cruzan.

---

## 2. Glosario mínimo

Antes de leer cualquier wave, asegurate de manejar estas siglas. Todas aparecen en el resto del brain sin disclaimer adicional:

### Tipos de entrada (con su prefijo)

| Sigla | Nombre completo | Dónde vive | Propósito |
|---|---|---|---|
| **Q-*** | Pregunta abierta (Question) | [`agent-context/CUESTIONARIO_CAROLINA.md`](../agent-context/CUESTIONARIO_CAROLINA.md) + dispersas en docs | Hueco de conocimiento que requiere respuesta de Erik o investigación |
| **L-*** | Aprendizaje (Learning) | [`learnings/`](../learnings/) | Verdad confirmada y atómica sobre el WMS, numerada (L-009..L-024) |
| **H-*** | Hipótesis (Hypothesis) | [`_proposals/`](../_proposals/) → [`_processed/`](../_processed/) | Sospecha que se confirma, refuta o queda pendiente. Numerada con timestamp |
| **ADR-*** | Architecture Decision Record | [`decisions/`](../decisions/) + [`architecture/adr/`](../architecture/adr/) | Decisión arquitectónica con contexto, opciones, consecuencias |
| **Wave N** | Ciclo de descubrimiento | Acumulado en [`_index/INDEX.md`](INDEX.md) | Sesión cronológica con un detonante + hallazgos + cierre |

### Estados (sufijo de los IDs)

| Estado | Significado |
|---|---|
| `OPEN` | Abierto, sin trabajar todavía |
| `APPLIED` | Resuelto y aplicado al brain |
| `INVALID` | La hipótesis fue refutada por datos posteriores |
| `DEF_PEND` | Definición pendiente, esperando confirmación de Erik o cliente |
| `WAIT_CLIENT` | Esperando validación con cliente productivo |
| `FIX_PEND` | Bug detectado, fix pendiente |

### Fingerprint, capability, traza

- **Fingerprint** = retrato compacto de un cliente (ERP, modelo de config, capabilities, bandera de drift). Vive en [`fingerprint/`](../fingerprint/).
- **Capability** = funcionalidad opcional del WMS que se activa por flag (ej. `picking-by-voice`, `recommend-location-ml`). Mapeadas en [`heat-map-params/`](../heat-map-params/).
- **Traza** = recorrido end-to-end de un parámetro o concepto a través de BOF + WSHHRN + WMSWebAPI + HH + DB + ERP. Vive en [`code-deep-flow/`](../code-deep-flow/).

### Categorías (segundo segmento del ID human-readable)

`DIAG` diagnóstico operativo · `ARCH` arquitectura · `FIX` corrección · `FEAT` feature · `DATA` patrón de datos · `PROC` proceso · `PARAM` capability flag · `INTEG` integración ERP · `CLIENT` específico de cliente · `META` meta-aprendizaje sobre cómo trabajamos.

### Vocabulario WMS clave

- **LP** = License Plate = identificador físico de pallet/caja (campo `lic_plate` en stock). Ver [`code-deep-flow/traza-001-license-plate.md`](../code-deep-flow/traza-001-license-plate.md).
- **MI3** = Módulo de Integración con Terceros (eufemismo Erik). Es el endpoint WCF/SOAP que el WMS expone al ERP. Ver [`code-deep-flow/04-mi3-y-reserva-clavaud.md`](../code-deep-flow/04-mi3-y-reserva-clavaud.md).
- **CEALSAMI3** = variante específica de MI3 para CEALSA (app standalone, no WCF).
- **WSHHRN** = el web service SOAP que la HH consume (`TOMHHWS.asmx`). 374+ funciones, 19.492+ líneas, cuello de botella crítico.
- **WMSWebAPI** = API REST .NET moderna en construcción. Hoy cubre maestros + portal, no operativo HH.
- **DALCore / EntityCore / AppGlobalCore** = duplicación obligada de WMS.DAL/Entity legacy en .NET Core. Ver [`code-deep-flow/02-portal-y-dms.md`](../code-deep-flow/02-portal-y-dms.md).
- **DMS** = Data Management System. EXE de Efren que replica on-prem → cloud fila por fila para CEALSA portal.
- **stock_jornada** = tabla que cierra inventario diariamente para auditoría regulatoria (CEALSA + MAMPA). Ver Wave 6.1.
- **3PL** = Third Party Logistics. CEALSA es el único 3PL real entre los clientes; usa propietarios.
- **Clavaud** = apellido del gerente de logística de IDEALSA Panamá. Su estrategia de "no vaciar picking" quedó como flag `conservar_zona_picking_clavaud`.
- **UMB / UMBA** = Unidad de Medida Básica. La unidad atómica del producto.

---

## 3. Recorrido recomendado para newcomer

Si estás llegando al brain por primera vez, leé estos tres archivos en este orden. Te toma 30-45 minutos y al final tenés mapa mental sólido:

### Lectura 1 — Filosofía de Erik (10 min)
[`way-of-thinking.md`](../way-of-thinking.md)

Cubre: 4 principios operativos (no hardcoded, datos > docs, read-only en prod, fuente de verdad correcta), terminología `reserva-WMS` vs `reserva-webapi`, convenciones de comunicación (español rioplatense, sin emojis, sin "deploy"). Sin esto, vas a malinterpretar el resto del brain.

### Lectura 2 — Vista temática por dominio (20 min)
[`_index/CONCEPT_MAP.md`](CONCEPT_MAP.md)

Las ~85+ Q-* abiertas agrupadas en 15 dominios temáticos (A. WMS Core, B. Ramas y migración, C. DALCore, D. Portal/DMS, E. Multi-tenancy 3PL, F. stock_jornada, G. Integraciones, H. Generador de código, I. Verificación, J. Reservas, K. Recepción/despacho, L. Cliente-específicas, M. Web BOF, N. Seguridad, O. Otros). Te muestra **dónde están los huecos hoy**.

### Lectura 3 — Convenciones del agente (10 min)
[`agent-context/RAMAS_Y_CLIENTES.md`](../agent-context/RAMAS_Y_CLIENTES.md) + [`agent-context/AGENTS.md`](../agent-context/AGENTS.md)

Te dice qué cliente está en qué rama de producción, qué reglas duras debés respetar al tocar el brain, cómo opera el bridge con Erik.

### Bonus opcional — el diario subjetivo
[`naked-erik-anatomy/000-prologo.md`](../naked-erik-anatomy/000-prologo.md)

Si querés entender el tono y el "por qué" del brain más allá del qué. Es opcional pero ilumina.

---

## 4. Mapa del brain — qué vive en cada carpeta

Estructura detallada con propósito + cuándo consultarla + naming convention. Los enlaces apuntan a la carpeta o al archivo principal.

### Núcleo de orientación

| Carpeta | Propósito | Cuándo entrar | Convención de naming |
|---|---|---|---|
| [`_index/`](../_index/) | Cronología, vista temática, este archivo metadato | Para orientarte temporal o temáticamente | Archivos en raíz |
| [`agent-context/`](../agent-context/) | Cómo operan los agentes (Erik, Carolina, Replit) | Para arrancar a trabajar el brain | UPPERCASE.md |
| [`_conventions/`](../_conventions/) | Convenciones de naming y codificación | Antes de crear nombres nuevos | nomenclatura.md |

### Conocimiento estructurado del WMS

| Carpeta | Propósito | Cuándo entrar | Naming |
|---|---|---|---|
| [`brain-map/`](../brain-map/) | Matrices funcionalidad × cliente, tablas × funcionalidad, parámetros × cliente | Para resolver "qué cliente tiene tal feature" | `<dimension>-por-<dimension>.md` |
| [`fingerprint/`](../fingerprint/) | Retrato sintético por cliente | Para arrancar trabajo en un cliente nuevo | `<CLIENTE>.md` mayúsculas |
| [`clients/`](../clients/) | Notas adicionales por cliente | Para detalles operativos | `<cliente>.md` minúsculas |
| [`heat-map-params/`](../heat-map-params/) | Drift de parámetros entre clientes (capa empresa/bodega/interface/producto/tipo-doc) | Para ver qué flag es heterogéneo | Subcarpetas numeradas `01-empresa`, `02-bodega`, etc. + `cross-cliente/` |
| [`ddl-funcional/`](../ddl-funcional/) | Schema de tablas core en lenguaje funcional | Para entender una tabla sin leer DDL | `<tabla>.md` |
| [`sql-catalog/`](../sql-catalog/) | DDL crítico extraído live de Killios PRD | Para validar schema real vs documentado | `<tema>.md` |

### Procesos y trazas

| Carpeta | Propósito | Cuándo entrar | Naming |
|---|---|---|---|
| [`code-deep-flow/`](../code-deep-flow/) | Trazas end-to-end de un concepto/parámetro a través de las capas | Para entender flujo BOF→HH→DB→ERP | `<NN>-<tema>.md` numerado, `traza-NNN-<param>.md` para trazas formales |
| [`wms-specific-process-flow/`](../wms-specific-process-flow/) | Mapas de proceso por área (state machines, recepción, despacho) | Para state machines | `<tema>.md` |
| [`sendero-producto/`](../sendero-producto/) | Modelo de "sendero" del producto + matriz flujos + grafo EQL | Para vista producto-céntrica | `matriz-flujos.md`, `grafo-eql/`, `modelo/`, `trazas/` |
| [`wms-test-natural-cases/`](../wms-test-natural-cases/) | Casos de uso operativos descritos en lenguaje natural (matriz reservas, Clavaud, explosión, lote correlativo, tolerancia) | Para entender qué hace el WMS desde la operación | `<NN>-<caso>.md` numerado + `00-INDEX.md` |
| [`test-scenarios/`](../test-scenarios/) | Escenarios de testing organizados por tipo (ajustes, despacho, picking, reservation) | Para validación QA | Subcarpetas por tipo |

### Conocimiento adquirido (cronología-agnóstico)

| Carpeta | Propósito | Cuándo entrar | Naming |
|---|---|---|---|
| [`learnings/`](../learnings/) | Aprendizajes atómicos confirmados | Para "qué sabemos hoy de tal cosa" | `L-NNN-<slug>.md` |
| [`decisions/`](../decisions/) | ADRs estratégicos | Para entender por qué se eligió X en vez de Y | `NNN-<slug>.md` numerado |
| [`architecture/`](../architecture/) + `architecture/adr/` | Arquitectura objetivo + ADRs adicionales | Para diseño futuro | ADRs numerados |
| [`entities/`](../entities/) | Modelos de dominio (cases, decisions operativas, modules, rules) | Para saber qué módulos están documentados a profundidad | Subcarpetas |

### Casos vivos (cronología-sí, mediante timestamp)

| Carpeta | Propósito | Naming |
|---|---|---|
| [`_inbox/`](../_inbox/) | Eventos pendientes del bridge con Erik | `<ulid>.json` |
| [`_proposals/`](../_proposals/) | Hipótesis crudas + propuestas | `YYYYMMDD-HHMM-<H_ID>-<slug>.md` |
| [`_processed/`](../_processed/) | Eventos resueltos (audit trail) | `YYYYMMDD-HHMM-<ID>.json` |
| [`outputs/`](../outputs/) | Artefactos generados (consolidaciones, explicaciones, guías, ratificaciones, respuestas) | Subcarpetas |
| [`tasks-historicas/`](../tasks-historicas/) | Tareas resueltas como referencia | `<slug>.md` |

### Diario subjetivo y contexto humano

| Carpeta | Propósito | Naming |
|---|---|---|
| [`naked-erik-anatomy/`](../naked-erik-anatomy/) | Diario técnico-poético-sarcástico (rationale Wave 9+) | `NNN-YYYY-MM-DD-<slug>.md` |
| [`agent-context/CUESTIONARIO_CAROLINA.md`](../agent-context/CUESTIONARIO_CAROLINA.md) | 76 Q-* en 13 bloques temáticos | Bloques numerados |
| [`agent-context/HOLDING_IDEALSA.md`](../agent-context/HOLDING_IDEALSA.md) | Análisis del holding (MERHONSA + MERCOPAN) | Único archivo |

### Tooling del agente

| Carpeta | Propósito |
|---|---|
| [`skills/`](../skills/) | Skills versionados (wms-test-bridge, wms-tomhh2025, wms-tomwms) |
| [`wms-agent/`](../wms-agent/) | CLI Python `wmsa` para operar el brain |
| [`wms-brain-client/`](../wms-brain-client/) | Cliente del bridge brain (PROTOCOL, CMDLETS, ALIASES, SPEC) |

---

## 5. Árbol cronológico Wave 1 → Wave 9

Cada wave sigue este formato:

> **Trigger**: qué la inició (anécdota Erik / hallazgo SQL / grep / lectura código / pregunta Carolina)
> **Hallazgos brutales**: lista de descubrimientos
> **Q-* tocadas**: resueltas + abiertas + invalidadas
> **L-* / ADR / docs generados**: artefactos creados
> **Estado**: ✅ cerrada / 🔄 en progreso / ❓ pendiente

---

### Wave 1 — Fingerprints cross-cliente (2026-04-28)

**Trigger**: Erik aprueba enfoque holístico → micro. Necesidad de tener un "retrato" de cada cliente antes de bajar a código.

**Hallazgos brutales**:
- 5 clientes activos perfilados completamente: MAMPA, KILLIOS, BECOFARMA, BYB, CEALSA
- 3 modelos distintos de configuración: BODEGA-CENTRIC (MAMPA), PRODUCT-CENTRIC (BECOFARMA, BYB, CEALSA), MIXTO (KILLIOS)
- 4 ERPs distintos en juego: SAP B1 (MAMPA, KILLIOS, BECOFARMA), NAV (BYB), propio CEALSASync (CEALSA)
- BYB OPERACIÓN COLAPSADA en 2024: outbox parado entre dic-2023 y oct-2025
- BYB tiene `Verificado` pero sin tablas de soporte ("half-implemented")
- CEALSA es el único 3PL real, ambiente QAS sin tráfico productivo

**Q-* abiertas en esta wave**:
- Q-BYB-CORTE-2024 (high) — ¿BYB sigue activo?
- Q-BYB-VERIF-INCOMPLETA (medium) — ¿cómo se setearon los 8 Verificado?
- Q-CEALSA-OUTBOX-VACIO (medium) — ¿por qué outbox=0?
- Q-CEALSA-CEALSASYNC-ERP (medium) — ¿qué ERP destino?

**L-* y artefactos generados**:
- [`L-022 INTEG_NAMING-SYNC-EXE`](../learnings/L-022-patron-naming-sincronizador.md) — patrón `SAPBOSync<Cliente>.exe`, `NavSync.exe`, `<Cliente>Sync.exe`
- [`L-023 DIAG_BYB-CORTE-OPERATIVO-2024`](../learnings/L-023-byb-corte-operativo-2024.md)
- [`L-024 FEAT_VERIF-HALF-IMPLEMENTED-BYB`](../learnings/L-024-byb-verificacion-half-implemented.md)
- [`fingerprint/BYB.md`](../fingerprint/BYB.md), [`fingerprint/CEALSA.md`](../fingerprint/CEALSA.md), [`fingerprint/MAMPA.md`](../fingerprint/MAMPA.md), [`fingerprint/BECOFARMA.md`](../fingerprint/BECOFARMA.md), [`fingerprint/KILLIOS.md`](../fingerprint/KILLIOS.md)

**Estado**: ✅ cerrada — los 5 clientes APPLIED. Pendientes CUMBRE, IDEALSA, INELAC quedan para waves posteriores (IDEALSA se resuelve en Wave 7).

---

### Wave 4 — Heat-map params cross-cliente (2026-04-28)

**Trigger**: Erik confirma orden A1 → B (cerrar capas de configuración antes de arrancar code-deep-flow). Faltaba ver el drift real de parámetros entre clientes.

**Hallazgos brutales**:
- Carpeta nueva [`brain/heat-map-params/cross-cliente/`](../heat-map-params/cross-cliente/) con 4 archivos
- **Taxonomía ERP corregida**: 3 clientes son SAP B1 (no 2). Solo BYB es NAV. CEALSA es PREFACTURA dedicada.
- MAMPA tiene **31.397 productos activos** (catálogo masivo por talla × color)
- CEALSA tiene **3.200 estados de producto** (vs 10-24 en el resto) — pendiente Q-CEALSA-3200-ESTADOS
- **NINGÚN cliente usa propietarios** (no hay 3PL operativo en TOM hoy — CEALSA es QAS)
- **K7 tiene case-mismatch + typo + duplicados** en `i_nav_config_enc`: `Codigo_Bodega_ERP_NC` (Camel) vs `codigo_bodega_erp_nc` (BYB lower) + `codigo_bodega_nc_erp` (reordered) + `lote_defecto_nc` (variante adicional) + `explosio_automatica_nivel_max` (typo le falta "n")
- K7 case-mismatch en bodega: `PERMITIR_BUEN_ESTADO_EN_REEMPLAZO` (BYB upper) vs `permitir_buen_estado_en_reemplazo` (K7 lower)
- CEALSA tiene typo propio: `liberar_stock_depachos_parciales` (le falta una "s")
- MAMPA usa **pick por voz + control talla/color en TODAS sus 33 bodegas**
- K7 usa **ML para sugerencia de ubicación en TODAS sus 6 bodegas** — capability `recommend-location-ml` exclusiva K7 (BYB la tiene parcial)

**Q-* abiertas en esta wave (~16)**:
- Q-K7-DUPLICADOS-CONFIG, Q-K7-TYPO-EXPLOSION, Q-CASE-NAME-K7
- Q-CEALSA-TYPO-DESPACHOS, Q-BECO-AJUSTE-BYB
- Q-K7-ML-MODELO, Q-K7-BOD5-AMATITLAN-NOSAP
- Q-CEALSA-3200-ESTADOS, Q-CEALSA-AUSENTES-7
- Q-BECO-PRODUCCION, Q-MAMPA-IDPRODESTADO-3, Q-MAMPA-IDINDICE-4
- Q-CEALSA-IDACUERDO-1, Q-MAMPA-BOD23-FALTANTES
- Q-K7-BOD7-FACTURACION, Q-VERIFICACION-CONSOL

**Capabilities nuevas identificadas**:
- `picking-by-voice`, `recommend-location-ml`, `same-operator-pick-and-verify`, `verify-with-photo`, `assign-all-operators`, `fiscal-warehouse-segregated`, `dispatch-auto-from-hh`, `verify-consolidated`, `confirm-code-in-picking`, `dispatch-by-pallet-mixed`

**Deprecation confirmada**:
- `industria_motriz` (bodega) y `IDPRODUCTOPARAMETROA/B` (producto) son DEPRECATED. Diseñados para una venta única de repuestos automotriz que nunca llegó a operar.

**Artefactos generados**:
- [`heat-map-params/cross-cliente/01-i_nav_config_enc.md`](../heat-map-params/cross-cliente/) (78 cols, schema drift severo)
- [`heat-map-params/cross-cliente/02-bodega.md`](../heat-map-params/cross-cliente/) (123 cols)
- [`heat-map-params/cross-cliente/03-tipos-documento.md`](../heat-map-params/cross-cliente/)

**Estado**: ✅ cerrada — capa producto pendiente para Wave 5.

---

### Wave 5 — Capa producto cerrada (2026-04-28)

**Trigger**: cierre del inventario A1 (las 4 capas fundamentales) antes de pasar a code-deep-flow. Faltaba la capa producto.

**Hallazgos brutales** (15 numerados):

1. **Schema producto prácticamente sin drift**: 60 cols en TODOS menos CEALSA con 59. Es la tabla MÁS estandarizada de las 4 capas.
2. **`producto_bodega` es pure join N:M sin parámetros**: 9 cols idénticas en los 5 clientes.
3. **MAMPA tiene 1.036.101 filas en `producto_bodega`**: caso de presión para WebAPI.
4. **CASO CEALSA 3.200 ESTADOS RESUELTO**: 3.197 son scaffolding ruido — patrón automático que crea "Buen Estado" por cada IdPropietario nuevo. Solo 4 estados útiles.
5. **CAPABILITIES NUNCA IMPLEMENTADAS EN TOM**: serializado, kit, materia_prima, temperatura_recepcion/despacho, capturar_aniada, captura_arancel, es_hardware, tolerancia=0, ciclo_vida=0, IdCamara, IdPerfilSerializado, IdUnidadMedidaCobro, IdArancel.
6. **MAMPA NO usa lote ni vencimiento en NINGÚN producto**: `control_lote=False` y `control_vencimiento=False` en los 31.397.
7. **CEALSA prácticamente NO controla vencimiento**: solo 24 productos (1.4%) con `control_vencimiento=True`.
8. **CEALSA es el ÚNICO con productos por peso**: 6 productos exactos.
9. **CEALSA es el ÚNICO con soft delete real**: 79 productos con `activo=False`.
10. **CEALSA tiene IdSimbologia poblado en 47%** — el resto residual.
11. **Cada cliente tiene IdTipoEtiqueta dedicado**: BECO=8, K7=10, MAMPA=12, BYB=2, CEALSA=2.
12. **IdTipoRotacion=3 (FEFO) dominante** en BECO/K7/BYB. MAMPA=1 (FIFO). CEALSA=NULL.
13. **`genera_lp_old=True` en TODOS menos BYB**: BYB es el único con drift (78% False).
14. **Catálogo de estados con drift**: BECO 10, K7 18, MAMPA 19, BYB 24, CEALSA 4 útiles.
15. **`producto_estado_ubic` prácticamente vacío en TODOS**.

**Q-* nuevas (capa producto, 7)**:
- Q-RESERVAR-EN-UMBAS, Q-CEALSA-ORIGEN-PROP-3197, Q-CEALSA-RH-HR
- Q-CEALSA-IDTIPO-NULL, Q-BYB-NO-DISPONIBLE-NAV-BD
- Q-MAMPA-MERMA-CARNE-FLUJO, Q-GENERA-LP-OLD-LEGADO

**Artefactos generados**:
- [`heat-map-params/cross-cliente/04-producto.md`](../heat-map-params/cross-cliente/) (60 cols + estados resueltos)

**Estado**: ✅ cerrada — inventario A1 completo. Listo para arrancar Wave 6 (code-deep-flow).

---

### Wave 6 — Code-deep-flow bootstrap + traza-001 LP (2026-04-28)

**Trigger**: Erik confirma arrancar B (code-deep-flow) tras cerrar A1. Acceso PAT a Azure DevOps confirmado.

**Hallazgos brutales del scan inicial**:

1. **WSHHRN no es solo el WebService de la HH, es además un router proxy a ERPs externos**: 11 Web References cliente a ERPs (4 son BYB-NAV-specific en línea, no via sync).
2. **WMSWebAPI tiene 25 Controllers** en .NET moderno con AutoMapper y JWT. Convivencia activa con WSHHRN.
3. **HH tiene DOS clientes HTTP en paralelo**: `WebService.java` (SOAP a WSHHRN) y `ApiService.java` + `RetrofitClient.java` (REST a WMSWebAPI).
4. **TOMWMS_BOF es mono-solution con muchos módulos históricos**: TMS, MES, MI3, IAService, GoCloud, GoCloudy, Perceptron, PlotWH, 3 PrintServices, 2 versiones StockReservation (v2 y v3 conviven), DAL/Entity Core paralelo a AppGlobal legacy.
5. **Conn.ini por cliente en WSHHRN**: Conn.ini default, Conn_Becofarma.ini, Conn - Cumbre.ini.

**Traza-001 LP — hallazgos brutales**:

1. **WMSWebAPI no migra LP**. 25 controllers + 16 services REST cubren maestros, KPI, sync ERP, auth — **0 endpoints LP/picking/recepción HH**.
2. **WSHHRN tiene 374 funciones públicas en un solo .asmx.vb (19.492 líneas)**. 21 son LP-específicas. Cuello de botella crítico.
3. **El LP no es entidad propia en DB**. Es `nvarchar(100)` que viaja como `lic_plate` en stock/stock_rec/stock_res/stock_jornada/faltantes/trans_movimientos/trans_re_det + `lp_origen`/`lp_destino` en trans_movimiento_pallet + `barra_pallet` en trans_movimientos. Naming inconsistente.
4. **Modelo triple confirmado**: `i_nav_config_enc.genera_lp` (bodega) + `producto.genera_lp_old` (producto) + `producto_presentacion.genera_lp_auto` (presentación).
5. **Invariante de coherencia NO enforced en DB**. K7 tiene 25% del stock sin `lic_plate` poblado.
6. **NAV expone correlativo LP**: `Get_Nuevo_Correlativo_LicensePlate(_S)` consumido DE NAV en BYB.
7. **HH muestra/oculta UI según los 3 flags**: `frm_recepcion_datos.java` declara `PGenera_lp`, `PTiene_Ctrl_Peso`, `PTiene_Ctrl_Temp`, `PTiene_PorSeries`, `PTiene_Pres`.

**Issues de seguridad descubiertos**:
- **Q-SEC-OPENAI-KEY-LEAK** (CRÍTICA): API key OpenAI hardcodeada en `WSHHRN/ChatGPTService.vb` línea 9
- **Q-SEC-CONNINI-CREDS**: credenciales SQL Server con usuario `sa` y password en claro en `Conn.ini`/`Conn_Becofarma.ini`/`Conn - Cumbre.ini`

**Q-* arquitectura abiertas (17)**: Q-LP-OPERADOR-VS-USUARIO, Q-LP-LONG-DEFAULT, Q-LP-LONG-VS-DATOS-REALES, Q-LP-S-VARIANTE, Q-LP-CORRELATIVO-NAV, Q-LP-EN-K7-DRIFT-25PCT, Q-LP-BYB-PRODS-SIN-LP, Q-PRESENTACION1-MUERTA, Q-RECEPCION-BOF-FLUJO, Q-HH-RECEPCION-DOS-VERSIONES, Q-MI3-QUE-ES, Q-WMSWEBAPI-MIGRACION-MAPA, Q-CONNINI-SELECCION, Q-LP-NAMING-DB, Q-LP-FALTANTES-PARA-QUE, Q-SEC-OPENAI-KEY-LEAK, Q-SEC-CONNINI-CREDS

**Artefactos generados**:
- [`code-deep-flow/README.md`](../code-deep-flow/README.md) (método holístico → micro)
- [`code-deep-flow/00-mapa-de-cajas.md`](../code-deep-flow/00-mapa-de-cajas.md)
- [`code-deep-flow/traza-001-license-plate.md`](../code-deep-flow/traza-001-license-plate.md) (~600 líneas, 9 secciones)

**Estado**: ✅ cerrada.

---

### Wave 6.1 — DALCore + DMS + Portal CEALSA + ramas 2023-2028 (2026-04-28)

**Trigger**: Erik resuelve Q-DALCORE-PROPOSITO con historia completa, lo que desencadena documentación del DMS, Portal CEALSA, generador de código, multi-tenant 3PL, y diferencias entre ramas 2023 y 2028.

**Hallazgos brutales**:

1. **DALCore + EntityCore + AppGlobalCore = duplicación obligada** de WMS.DAL/Entity legacy porque las DLLs VB.NET Framework no son compatibles con .NET Core.
2. **DMS = Data Management System** — herramienta server-side de Efren. EXE parametrizado que aprovecha la infra del license-server. Replica data on-prem → cloud **fila por fila** (NO bulk insert) por principio Erik de "control total para debug".
3. **Portal CEALSA — origen del DMS**. CEALSA = 3PL guatemalteco regulado por SAT/SIB/aseguradora. Necesidad: exponer inventario de propietarios "pagantes" sin replicar 10+ GB.
4. **Generador de código (Efren / Erik)** — la "primera automatización" del WMS. Genera por tabla: `clsBe<Tabla>` (Entity autogen 100%) + `clsLn<Tabla>` (CRUD base autogen) + `clsLn<Tabla>.partial` (negocio adhoc manual).
5. **Modelo multi-tenant 3PL** — propietarios. Estados aceptables varían por propietario ("tropicalización"). 6 tablas validadas en CEALSA QAS.
6. **`stock_jornada` validado por SQL** — 25+ columnas que replica TODA la traza transaccional. **Activo en CEALSA y MAMPA (21.883 filas)**. Inactivo (vacía) en BECO/K7/BYB.
7. **CEALSAMI3** = módulo de sync CEALSA con su ERP. **Anomalía**: clases con prefijo `Nav` — Q-NAV-PREFIJO-CEALSA abierta.
8. **TOMWeb** — repo separado en Azure DevOps. ASP.NET clásico + PHP. Distinto al portal CEALSA. Q-TOMWEB-PROPOSITO abierta.
9. **CEALSA repo en Azure DevOps**: existe pero **vacío** — igual que `TOMWMS5`.

**Q-DALCORE-PROPOSITO resuelta**: DALCore NO reemplaza WMS.DAL legacy — coexisten. Consumidores principales: WMSWebAPI (canal B2B-only, MHS = primer caso) + DMS + futuros desarrollos web.

**Diferencias 2023 vs 2028 BOF documentadas**:
- **Solo 2028**: `WMS.DALCore`, `WMS.EntityCore`, `WMS.AppGlobalCore`, `WMS.StockReservation2/3`, `reservastockfrommi3`, `GoCloud`, `GoCloudy`, `InstallerSW`
- **Existen en ambas, hashes distintos**: `WMSWebAPI` (NO greenfield 2028), `WSHHRN/TOMHHWS.asmx.vb` (340 → 369 funciones, +29 públicas, +1 LP endpoint), `WSHHRN/ChatGPTService.vb` (issue OpenAI key existe en 2023 también — severidad sube)

**Q-* nuevas (15+)**: Q-DMS-USA-DALCORE, Q-DMS-DESTINO-CLOUD, Q-DMS-PROPIETARIO-FILTER, Q-PORTAL-STACK, Q-PORTAL-AUTH, Q-PORTAL-MULTITENANCY-DECISION, Q-GENERADOR-UBICACION, Q-GENERADOR-INPUTS, Q-LN-DRIFT-AUDIT, Q-DALCORE-PARIDAD, Q-DALCORE-CONSUMERS, Q-DALCORE-COMPORTAMIENTO, Q-STOCK-JORNADA-PROCESO, Q-STOCK-JORNADA-CONSUMER, Q-STOCK-JORNADA-DESFASE, Q-STOCK-JORNADA-MAMPA, Q-WEB-BOF-STACK, Q-WEB-BOF-TIMELINE, Q-GENERADOR-WEB-VARIANTE, Q-NAV-PREFIJO-CEALSA, Q-TOMWEB-PROPOSITO, Q-CEALSA-REPO, Q-PROPIETARIO-ESTADO-MODELO, Q-PROPIETARIO-AGNOSTICO, Q-MIGRACION-2023-A-2028, Q-MHS-COMO-CLIENTE, Q-WMS5-VACIO, Q-HH-RAMAS-25, Q-MERCOPAN-MERCOSAL, Q-CUMBRE-RAMA-DEDICADA, Q-DEV2025-PROPOSITO, Q-MASTER-PROPOSITO, Q-SOAP-A-JSON-2028

**Principios Erik confirmados**:
1. Control total > performance
2. "Como es arriba es abajo" (replicación estructural)
3. Duplicación obligada como oportunidad
4. Generación de código como base de mantenibilidad
5. Tropicalización por propietario
6. Reingeniería gradual con coexistencia
7. Infra existente como apalancamiento

**Artefactos generados**:
- [`code-deep-flow/02-portal-y-dms.md`](../code-deep-flow/02-portal-y-dms.md) (~340 líneas)
- [`agent-context/RAMAS_Y_CLIENTES.md`](../agent-context/RAMAS_Y_CLIENTES.md) (disclaimer global)
- [`_index/CONCEPT_MAP.md`](CONCEPT_MAP.md) (~430 líneas, vista temática 15 dominios)

**Estado**: ✅ cerrada.

---

### Wave 6.2 — Quick wins SQL/grep (2026-04-28)

**Trigger**: cerrar 8 Q-* baratas resolubles en 1-2 turnos solo con SQL/GREP, antes de la próxima traza profunda.

**Q-* RESUELTAS (7)**:
- Q-LP-LONG-VS-DATOS-REALES → BYB avg 19 chars (NAV correlativo); resto 6-9 chars
- Q-CEALSA-AUSENTES-7 → en realidad **37** propietarios huérfanos (no 7) de 3.197 totales
- Q-DALCORE-PARIDAD → DALCore al **22%** del legacy (256/1162 archivos)
- Q-DEV2025-PROPOSITO + Q-MASTER-PROPOSITO → alias de `dev_2023_estable` (mismo commit `1f5cc2c4`)
- Q-LN-DRIFT-AUDIT → convención es `_partial.vb` (underscore). Solo **10% de las LN tienen partial** → drift 90%
- Q-PROPIETARIO-AGNOSTICO → no-3PL usan **1 propietario default**; CEALSA único 3PL real

**Q-* reformulada**:
- Q-LP-LONG-DEFAULT → no hay columna específica en `producto_parametros`. Límite por `varchar(N)` o convención cliente

**Q-* nuevas derivadas (5)**:
- Q-GENERADOR-ABANDONO (alta) — 90% drift en clases base
- Q-LP-CORRELATIVO-NAV-FORMATO (media) — estructura LP BYB
- Q-PORTAL-AUTH-CREDENCIALES-EN-PROPIETARIOS (alta) — cols `codigo_acceso` + `clave_acceso`
- Q-LP-DATA-DIRTY-MIN (baja) — 8 filas BECO LP="0"
- Q-RAMA-MASTER-DEV2025-DUPLICADAS (baja)

**Artefactos generados**:
- [`_index/WAVE-6.2-QUICK-WINS.md`](WAVE-6.2-QUICK-WINS.md) (~250 líneas)

**Estado**: ✅ cerrada.

---

### Wave 7 — Holding IDEALSA + implosión + capabilities (2026-04-29)

**Trigger**: Erik conectó dos BDs nuevas en EC2 (`IMS4MB_MERHONSA_PRD` y `IMS4MB_MERCOPAN_PRD`) — filiales del holding IDEALSA. Pidió recorrido paralelo + detección de gaps. Adicionalmente reveló la lógica de **implosión / merge LP en cambio de ubicación 2028**.

**Hallazgos brutales**:
- **WMS soporta NAV + SAP simultáneamente** (`interface_sap` flag). Antes asumido NAV-only.
- **`frmImplosion.vb` SÍ existe en BOF** (1332 líneas, sin cambios entre 2023 y 2028). Erik creía que no. Hipótesis: oculto por permisos en clientes nuevos, visible en Cumbre.
- **MERCOPAN tiene rol "cocinero"** (`StockCocinero`, `stock_BK_Cocinero`) — único entre los clientes.
- **Holding IDEALSA confirmado**: 98% schema común MERHONSA↔MERCOPAN (315 de ~320 tablas).
- **MERCOPAN ya tiene 323K movimientos** en producción. MERHONSA solo 0 movimientos pero ya 16K detalles de tareas HH (arranque operativo en curso).

**Q-* RESUELTAS (4)**:
- ✅ **Q-LP-WHEN-DESTROYED** — 5 caminos: despacho completo, implosión BOF manual, implosión HH Cumbre, cambio ubicación 2028 con LP destino preexistente (auto si flag ON), anulación de recepción
- ✅ **Q-LP-MERGE-EN-DESTINO** — `frmCambioUbicacion.vb` reescrito (+113% líneas) detecta LP destino y mergea automáticamente si `i_nav_config_enc.implosion_automatica=True`
- ✅ **Q-CAPABILITY-FLAG** — la tabla maestra de capabilities **ES** `i_nav_config_enc`. Tiene 50+ flags
- ✅ **Q-CONTROL-LOTE-TABLA** — descartada: `control_lote` y `control_vencimiento` son columnas (bit) en `i_nav_config_enc`, no tablas

**Q-* nuevas (10)** — bloque 11 cuestionario:
- Q-MERGE-LP-LOG-PATRON, Q-IMPLOSION-BOF-VISIBILIDAD, Q-CLAVAUD-MEANING
- Q-SAP-CLIENTES, Q-UMB-CONCEPT, Q-LP-ZOMBIE
- Q-IDEALSA-MASTER-DATA, Q-IDEALSA-OTROS-PAISES, Q-MERHONSA-PARADOJA-LP
- Q-COCINERO-ROLE-PANAMA, Q-SCHEMA-PRODUCTOS-MERHONSA

**Artefactos generados**:
- [`agent-context/HOLDING_IDEALSA.md`](../agent-context/HOLDING_IDEALSA.md) (~280 líneas)
- [`code-deep-flow/03-implosion-y-merge-lp.md`](../code-deep-flow/03-implosion-y-merge-lp.md) (~410 líneas)

**Estado**: ✅ cerrada.

---

### Wave 8 — Clavaud + MI3 + algoritmo de reserva 2028 (2026-04-29)

**Trigger**: Erik contó la anécdota de **Marcelo Clavaud** (gerente logística IDEALSA Panamá) y reveló que **MI3 = Módulo de Integración con Terceros** (eufemismo interno). Esto destrabó 3 Q-* y permitió mapear el algoritmo de reserva completo.

**Hallazgos brutales**:
- **El algoritmo de reserva 2028 está completamente reescrito**. `clsLnStock_res_Partial.vb` pasó de ~600 a 4374 líneas (Wave 9 lo corrigió a >26.680). Tiene 13+ funciones de reserva.
- **El ranking se hace en MEMORIA con LINQ**, no en SQL. Líneas 4564-4736 tienen el `Select Case` por `IdTipoRotacion`.
- **`IdTipoRotacion` vive en 3 tablas** (cascada de precedencia probable): `producto.IdTipoRotacion`, `bodega_ubicacion.IdTipoRotacion`, `i_nav_config_enc.IdTipoRotacion`.
- **`tipo_rotacion` tiene 4 valores**: 1=FIFO, 2=LIFO, 3=FEFO, **4=UPSR** (acrónimo aún sin definir).
- **Función `Reserva_Stock_NAV_BYB`** específica para BECO+NAV (única función nombrada por cliente).
- **Carpeta `CEALSAMI3/`** — variante específica de MI3 para CEALSA. Contiene `dsUbicSug` (motor de ubicación sugerida).
- **`clsLnTrans_picking_det_Partial.vb` 1925 líneas modificadas en 2028** — algoritmo de picking también reescrito masivamente.

**Q-* RESUELTAS (4)**:
- ✅ **Q-CLAVAUD-MEANING** — apellido del gerente de logística cliente IDEALSA Panamá. Estrategia anti-vaciamiento de picking codificada como flag `i_nav_config_enc.conservar_zona_picking_clavaud` (bit, default 0, migration 2022-11-07 commit `EJC202211071706`)
- ✅ **Q-MI3-IDENTIDAD** — MI3 es proyecto WCF/SOAP en `MI3/` con services para Bodega, Cliente, Direcciones, Documentos, Barras_Pallet
- ✅ **Q-UMB-CONCEPT** — UMB = Unidad de Medida Básica. Confirmado en código: `vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad * BePres.Factor`
- ✅ **Q-PROPIETARIO-AGNOSTICO** (refinada) — branching por `empresas.Operador_logistico`. 3PL (CEALSA): config por (IdBodega, IdEmpresa). 1PL/2PL: config por (IdBodega, IdPropietario)

**Q-* nuevas (7)** — bloque 12 cuestionario:
- Q-UPSR-MEANING, Q-ROTACION-PRECEDENCIA, Q-CLAVAUD-THRESHOLD
- Q-MI3-VS-CEALSAMI3, Q-DSUBICSUG-ALGORITMO
- Q-RESERVA-MULTIPLE-VARIANTES, Q-REEMPLAZO-AUTO

**Artefactos generados**:
- [`code-deep-flow/04-mi3-y-reserva-clavaud.md`](../code-deep-flow/04-mi3-y-reserva-clavaud.md) (~480 líneas, 7 capas)

**Estado**: ✅ cerrada.

---

### Wave 9 — Casos naturales de reserva + diario naked-erik (2026-04-29)

**Trigger**: Erik pidió mapear matriz "función de reserva × cliente × caso de uso" después de mencionar que **los 4 módulos más complejos del WMS son**: algoritmo de reserva, ubicación sugerida, reemplazo en HH durante picking, verificación. Reveló además 5 parámetros operativos nuevos. Pidió crear un **diario `naked-erik-anatomy`** versionado con tono técnico-poético-sarcástico.

**Hallazgos brutales**:
- **`clsLnStock_res_Partial.vb` tiene >26.680 líneas** (no 4.374 como Wave 8 estimó). Tres adapters específicos por canal: `Reserva_Stock_From_Reabasto` (línea 9856), `Reserva_Stock_From_MI3` (línea 18192), `Reserva_Stock_From_SAP` (línea 26680).
- **`producto_estado` tiene 14 estados granulares** con flags `utilizable` + `dañado` + `tolerancia_dias_vencimiento`.
- **`cliente` tabla tiene 9 flags operativos** no estaban mapeados: `IdUbicacionAbastecerCon`, `IdUbicacionManufactura`, `realiza_manufactura`, `despachar_lotes_completos`, `control_ultimo_lote`, `control_calidad`, `es_bodega_recepcion`, `es_bodega_traslado`, `es_proveedor`.
- **`cliente_tiempos` es N×N×N** (cliente × familia × clasificación → días tolerados). `Dias_Local` y `Dias_Exterior` distintos.
- **MERHONSA tiene typo histórico**: dos columnas `explosion_automatica_nivel_max` y `explosio_automatica_nivel_max` (sin 'n') conviven en schema. Deuda técnica visible.
- **`Reemplazo_Automatico` NO es WMS-driven**: siempre se llama desde HH (TOMHHWS.asmx + frmCantidadreemplazo). Operador-driven con asistencia automática.
- **`Lote_Numerico` aparece en 5 tablas** — sistema completo de lote correlativo transversal.

**Q-* RESUELTAS (5)**:
- ✅ **Q-EXPLOSION-EXISTE** — `explosion_automatica` + `explosion_automatica_desde_ubicacion_picking` + `explosion_automatica_nivel_max` en `i_nav_config_enc`
- ✅ **Q-RESTRICCION-UBICACION-CLIENTE** — `cliente.IdUbicacionAbastecerCon`
- ✅ **Q-LOTE-NUM-EXISTE** — `cliente.control_ultimo_lote` + `trans_re_det_lote_num` + `trans_despacho_det_lote_num`
- ✅ **Q-TOLERANCIA-MULTI-NIVEL** — cascada `cliente_tiempos` > `producto.tolerancia` > `producto_estado.tolerancia_dias_vencimiento` > `i_nav_config_enc.dias_vida_defecto_perecederos`
- ✅ **Q-FROM-CHANNEL-FUNCTIONS** — 3 adapters confirmados: `From_MI3`, `From_SAP`, `From_Reabasto`

**Q-* nuevas formalizadas (15)** — bloque 13 cuestionario, numeradas Q62-Q76:
- Q62-Q65 Adapters: Q-FROM-MI3-DIFF, Q-FROM-SAP-DIFF, Q-FROM-REABASTO-DIFF, Q-FROM-NAV-FALTA
- Q66-Q67 Restricción ubicación: Q-UBICACION-RESTRINGIDA-FALLBACK, Q-UBICACION-RESTRINGIDA-REABASTO
- Q68-Q69 Lote correlativo: Q-LOTE-NUM-GAP, Q-LOTE-NUM-RESET
- Q70-Q71 Tolerancia: Q-TOLERANCIA-PRECEDENCIA, Q-CLIENTE-TIEMPOS-NXNXN
- Q72-Q73 Explosión: Q-EXPLOSION-NIVEL-MAX-COMPORTAMIENTO, Q-MERHONSA-TYPO-COLUMNA
- Q74 Reemplazo: Q-REEMPLAZO-PATH-BOF
- Q75 Estados: Q-PRODUCTO-ESTADO-RESERVABLE
- Q76 Mantenibilidad: Q-CLSLNSTOCK-RES-DESCOMPOSICION

**Artefactos generados**:
- Carpeta nueva [`wms-test-natural-cases/`](../wms-test-natural-cases/) — 7 docs:
  - [`00-INDEX.md`](../wms-test-natural-cases/00-INDEX.md)
  - [`01-matriz-funcion-cliente-canal.md`](../wms-test-natural-cases/01-matriz-funcion-cliente-canal.md)
  - [`03-caso-clavaud-conservar-picking.md`](../wms-test-natural-cases/03-caso-clavaud-conservar-picking.md)
  - [`04-caso-explosion-cajas-a-unidades.md`](../wms-test-natural-cases/04-caso-explosion-cajas-a-unidades.md)
  - [`05-caso-restriccion-ubicacion-por-cliente.md`](../wms-test-natural-cases/05-caso-restriccion-ubicacion-por-cliente.md)
  - [`06-caso-lote-numerico-correlativo.md`](../wms-test-natural-cases/06-caso-lote-numerico-correlativo.md)
  - [`07-caso-tolerancia-vencimiento.md`](../wms-test-natural-cases/07-caso-tolerancia-vencimiento.md)
- Carpeta nueva [`naked-erik-anatomy/`](../naked-erik-anatomy/) — 2 docs:
  - [`000-prologo.md`](../naked-erik-anatomy/000-prologo.md)
  - [`001-2026-04-29-clavaud-mi3-y-el-rio-desviado.md`](../naked-erik-anatomy/001-2026-04-29-clavaud-mi3-y-el-rio-desviado.md)
- Bloque 13 agregado a [`agent-context/CUESTIONARIO_CAROLINA.md`](../agent-context/CUESTIONARIO_CAROLINA.md)
- Este archivo: [`_index/DISCOVERY_TREE.md`](DISCOVERY_TREE.md)

**Estado**: ✅ cerrada.

---

## 6. DAG de dependencias entre waves

Quién parió a quién, leído de izquierda a derecha:

```
Wave 1 (fingerprints 5 clientes)
   │
   └──> Wave 4 (heat-map params cross-cliente)
           │
           └──> Wave 5 (capa producto cerrada)
                   │
                   └──> Wave 6 (code-deep-flow bootstrap + traza-001 LP)
                           │
                           ├──> Wave 6.1 (DALCore + DMS + Portal + ramas 2023-2028)
                           │       │
                           │       └──> Wave 6.2 (quick wins SQL/grep)
                           │
                           └──> Wave 7 (Holding IDEALSA + implosión)
                                   │
                                   └──> Wave 8 (Clavaud + MI3 + algoritmo reserva)
                                           │
                                           └──> Wave 9 (casos naturales + diario)
                                                   │
                                                   └──> Wave 10 (próxima)
```

### Cadena causal explícita

- **Wave 1 → Wave 4**: tener fingerprints permitió ver el drift de parámetros de manera estructurada
- **Wave 4 → Wave 5**: cerrar 3 capas (config_enc + bodega + tipos-doc) dejó pendiente la 4ta (producto)
- **Wave 5 → Wave 6**: cerrar A1 (las 4 capas) era precondición acordada para arrancar B (code-deep-flow)
- **Wave 6 → Wave 6.1**: el scan inicial dejó muchas Q-* arquitectónicas (DALCore, DMS, Portal, ramas)
- **Wave 6.1 → Wave 6.2**: 8 Q-* identificadas como resolubles con SQL/grep barato
- **Wave 6 → Wave 7**: la traza-001 LP destrabó el flujo "qué pasa con un LP cuando…" — 5 caminos
- **Wave 7 → Wave 8**: IDEALSA = MERHONSA + MERCOPAN. Clavaud nace en MERCOPAN. La conexión se hizo cuando Erik contó la anécdota
- **Wave 8 → Wave 9**: el algoritmo de reserva mostró 13+ funciones — Erik pidió mapear cuándo se usa cada una

### Ramas paralelas

Ningún par de waves corrió en paralelo todavía. El brain es estrictamente secuencial.

---

## 7. Snapshot estado actual del brain

**Fecha snapshot**: 2026-04-29 (post-Wave 9 + bloque 13)

### Métricas globales

| Métrica | Valor |
|---|---|
| Total Q-* formales en cuestionario | **76** (13 bloques temáticos) |
| Q-* resueltas a la fecha | **~21** (28%) |
| Q-* alta prioridad abiertas | **~24** |
| Q-* críticas | **1** (Q-SEC-OPENAI-KEY-LEAK, sin avance — rotación pendiente Erik) |
| L-* aprendizajes confirmados | **16** (L-009 a L-024) |
| ADRs estratégicos | **3** documentados (003 MI3 reescrito, 004 bridge wms-test, 005 bridge schema v2) |
| Hipótesis trabajadas (H-*) | **30+** (H01..H30 + P3) |
| Fingerprints de cliente | **5** APPLIED (BECO, BYB, CEALSA, KILLIOS, MAMPA) + **3** DEF_PEND (CUMBRE, IDEALSA, INELAC) |
| Trazas formales (`traza-NNN`) | **1** (traza-001 LP) — `traza-002 control_lote` planificada |
| Casos naturales (`wms-test-natural-cases/`) | **7** docs |
| Líneas de markdown brain total | **~6.800** |
| Total .md en brain | **~95** |
| Total directorios | **35** |

### Lo más urgente abierto

1. **Q-SEC-OPENAI-KEY-LEAK** (CRÍTICA) — API key OpenAI hardcoded en `WSHHRN/ChatGPTService.vb` desde antes de 2023. Acción: Erik debe rotar la key + auditar git history. Sin avance desde Wave 6.
2. **Q70 Q-TOLERANCIA-PRECEDENCIA** (alta) — la cascada de 4 niveles puede no ser override sino suma/min/max. Si cambia la regla, hay que reescribir los pseudo-códigos del caso 07.
3. **Q66 Q-UBICACION-RESTRINGIDA-FALLBACK** (alta) — UX operativa cambia totalmente según si falla duro o degrada.
4. **Q-MIGRACION-2023-A-2028** (alta) — orden de migración de los 4 clientes restantes. Sin cronograma todavía.

### Lo más reciente cerrado

- 5 Q-* en Wave 9 (Q-EXPLOSION-EXISTE, Q-RESTRICCION-UBICACION-CLIENTE, Q-LOTE-NUM-EXISTE, Q-TOLERANCIA-MULTI-NIVEL, Q-FROM-CHANNEL-FUNCTIONS)
- 4 Q-* en Wave 8 (Clavaud, MI3-identidad, UMB, PROPIETARIO-AGNOSTICO)
- 4 Q-* en Wave 7 (LP-WHEN-DESTROYED, LP-MERGE-EN-DESTINO, CAPABILITY-FLAG, CONTROL-LOTE-TABLA)

### Dominios con más cobertura

1. **WMS Core / reserva** (waves 6 + 8 + 9) — algoritmo, LP, lote, vencimiento, peso, rotación, restricción ubicación, control lote, tolerancia
2. **Cliente-específicas** (waves 1 + 4 + 5) — fingerprint completo de los 5 clientes principales
3. **Configuración / capabilities** (waves 4 + 7) — `i_nav_config_enc` mapeada con 50+ flags, `cliente` con 9 flags

### Dominios con menos cobertura

1. **Web BOF futuro** (M en CONCEPT_MAP) — solo 2 Q-* abiertas, sin definir
2. **Verificación detallada** (I en CONCEPT_MAP) — sabemos quién la usa pero no el flujo end-to-end
3. **Stock_jornada regulatorio** (F en CONCEPT_MAP) — sabemos qué hace en MAMPA + CEALSA pero no quién la cierra ni cómo

---

## Apéndice A — Inventario completo de aprendizajes (L-*)

Los aprendizajes son hechos confirmados sobre el WMS. Atómicos, citables, numerados secuencialmente.

| ID | Tema | Aplicado en wave | Archivo |
|---|---|---|---|
| L-009 | SAP B1 (KILLIOS) solo procesa enteros | Pre-Wave 1 | [`L-009`](../learnings/L-009-sapsync-killios-solo-enteros.md) |
| L-010 | NAVSync (BYB) NO procesa ingresos | Pre-Wave 1 | [`L-010`](../learnings/L-010-navsync-bb-no-procesa-ingresos.md) |
| L-011 | log_error_wms es bitácora del legacy | Pre-Wave 1 | [`L-011`](../learnings/L-011-log-error-wms-es-bitacora.md) |
| L-012 | NAVSync solo procesa salidas | Pre-Wave 1 | [`L-012`](../learnings/L-012-navsync-solo-procesa-salidas.md) |
| L-013 | Outbox tiene granularidad por línea | Pre-Wave 1 | [`L-013`](../learnings/L-013-outbox-granularidad-por-linea.md) |
| L-014 | BECOFARMA PRD es BD diagnóstica, no productiva real | Pre-Wave 1 | [`L-014`](../learnings/L-014-becofarma-prd-es-bd-diagnostica-no-productiva.md) |
| L-015 | WMS ClickOnce empaqueta todas las interfaces — dispatch dinámico | Pre-Wave 1 | [`L-015`](../learnings/L-015-wms-clickonce-empaqueta-todas-interfaces-dispatch-dinamico.md) |
| L-016 | log_error_wms — segmentación fue mejora, decisión arquitectónica pendiente | Pre-Wave 1 | [`L-016`](../learnings/L-016-log-error-wms-segmentacion-fue-mejora-decision-pendiente.md) |
| L-017 | i_nav_transacciones_out FKs sentinela cero (no NULL) | Pre-Wave 1 | [`L-017`](../learnings/L-017-i-nav-transacciones-out-fks-sentinela-cero.md) |
| L-018 | Verificación etiquetas y license_plate | Pre-Wave 1 | [`L-018`](../learnings/L-018-verificacion-etiquetas-y-license-plate.md) |
| L-019 | `i_nav_config_enc` es fuente maestra de capability flags | Wave 1 | [`L-019`](../learnings/L-019-i-nav-config-enc-fuente-maestra.md) |
| L-020 | Tres modelos de configuración: BODEGA-CENTRIC, PRODUCT-CENTRIC, MIXTO | Wave 1 | [`L-020`](../learnings/L-020-tres-modelos-configuracion.md) |
| L-021 | Verificación etiqueta — flags coordinados (no flag único) | Wave 1 | [`L-021`](../learnings/L-021-verificacion-etiqueta-flags-coordinados.md) |
| L-022 | Patrón naming sincronizador `<ERP>Sync<Cliente>.exe` | Wave 1 | [`L-022`](../learnings/L-022-patron-naming-sincronizador.md) |
| L-023 | BYB corte operativo 2024 | Wave 1 | [`L-023`](../learnings/L-023-byb-corte-operativo-2024.md) |
| L-024 | BYB verificación half-implemented | Wave 1 | [`L-024`](../learnings/L-024-byb-verificacion-half-implemented.md) |

**Próxima L-*** sugerida: L-025 — algoritmo de reserva 2028 reescrito completo con 18+ funciones (Wave 8+9 lo amerita pero no se ha cristalizado).

---

## Apéndice B — Inventario completo de hipótesis (H-*)

Las hipótesis son sospechas con timestamp. Pueden confirmarse (mover a `_processed/` con datos), refutarse (`INVALID`) o quedar pendientes.

| ID | Slug | Estado | Archivo |
|---|---|---|---|
| H01 | `tras-wms-reservastock-muerto` | abierta | [`H01`](../_proposals/20260428-1900-H01-tras-wms-reservastock-muerto.md) |
| H02 | `clbd-prc-falta-reabasto-log` | abierta | [`H02`](../_proposals/20260428-1901-H02-clbd-prc-falta-reabasto-log.md) |
| H03 | `logs-estructurados-mudos` | abierta | [`H03`](../_proposals/20260428-1902-H03-logs-estructurados-mudos.md) |
| H04 | `despacho-fantasma-bypass-estado` | abierta | [`H04`](../_proposals/20260428-1903-H04-despacho-fantasma-bypass-estado.md) |
| H05 | `prefactura-sin-informante` | abierta | [`H05`](../_proposals/20260428-1904-H05-prefactura-sin-informante.md) |
| H06 | `q011-bypass-real-1-no-43-simplificar-adr-012` | abierta | [`H06`](../_proposals/20260428-1905-H06-q011-bypass-real-1-no-43-simplificar-adr-012.md) |
| H07 | `bb-putaway-intensivo-50pct-ubic` | abierta | [`H07`](../_proposals/20260428-1906-H07-bb-putaway-intensivo-50pct-ubic.md) |
| H08 | `outbox-registra-solo-despachos` | abierta | [`H08`](../_proposals/20260428-1907-H08-outbox-registra-solo-despachos-no-pedidos-sueltos.md) |
| H09 | `naming-inconsistente-idordenpedidoenc-vs-idpedidoenc` | abierta | [`H09`](../_proposals/20260428-1908-H09-naming-inconsistente-idordenpedidoenc-vs-idpedidoenc.md) |
| H10 | `25-pedidos-fiscales-sin-poliza-cealsa-qas` | abierta | [`H10`](../_proposals/20260428-1909-H10-25-pedidos-fiscales-sin-poliza-cealsa-qas-1-7-pct.md) |
| H11 | `bb-tambien-tiene-basura-trans-reabastecimiento-log` | abierta | [`H11`](../_proposals/20260428-1910-H11-bb-tambien-tiene-basura-trans-reabastecimiento-log.md) |
| H25 | `becofarma-bd-restaurada-config-2017` | procesada | [`H25 propuesta`](../_proposals/20260428-2345-H25-becofarma-bd-restaurada-config-2017.md) + [`H25 procesada`](../_processed/20260428-2345-H25-becofarma-bd-restaurada-config-2017.json) |
| H26 | `becofarma-logging-segmentado-por-proceso` | procesada con followup | [`H26 propuesta`](../_proposals/20260428-2346-H26-becofarma-logging-segmentado-por-proceso.md) + [`H26 procesada`](../_processed/20260428-2346-H26-becofarma-logging-segmentado-por-proceso.json) |
| H27 | `becofarma-modulo-verificacion-etiquetas-zpl-inkjet` | abierta | [`H27`](../_proposals/20260428-2347-H27-becofarma-modulo-verificacion-etiquetas-zpl-inkjet.md) |
| H28 | `becofarma-outbox-85pct-pendiente` | procesada | [`H28 propuesta`](../_proposals/20260428-2348-H28-becofarma-outbox-85pct-pendiente.md) + [`H28 procesada`](../_processed/20260428-2348-H28-becofarma-outbox-85pct-pendiente.json) |
| H29 | `becofarma-pedidos-pickeado-terminal-44pct` | abierta WAIT_CLIENT | [`H29`](../_proposals/20260428-2349-H29-becofarma-pedidos-pickeado-terminal-44pct.md) |
| H30 | `becofarma-outbox-fk-universal-invalida-h08` | INVALID por L-017 | [`H30 propuesta`](../_proposals/20260428-2350-H30-becofarma-outbox-fk-universal-invalida-h08.md) + [`H30 procesada`](../_processed/20260428-2350-H30-becofarma-outbox-fk-universal-invalida-h08.json) |
| P3 | `RELOC-RULE-STRICT` | propuesta | [`P3`](../_proposals/P3-2026-04-28-RELOC-RULE-STRICT.md) |

---

## Apéndice C — Cómo agregar una wave nueva a este árbol

Cuando termines una wave, este archivo debe actualizarse junto con [`INDEX.md`](INDEX.md). Pasos:

1. **En [`INDEX.md`](INDEX.md)**: agregá la sección detallada con métricas, hallazgos, Q-* tocadas (formato existente).
2. **En este archivo**: agregá una entrada en [Sección 5](#5-árbol-cronológico-wave-1--wave-9) usando este template:

```markdown
### Wave N — <título corto> (YYYY-MM-DD)

**Trigger**: qué la inició (anécdota / SQL / grep / lectura código / pregunta)

**Hallazgos brutales**:
- ...

**Q-* RESUELTAS (n)**:
- ✅ **Q-XXXX** — explicación corta

**Q-* nuevas (n)**:
- Q-YYYY (alta/media/baja)

**Artefactos generados**:
- [`path/al/archivo.md`](../path/al/archivo.md) (líneas, secciones)

**Estado**: ✅ cerrada / 🔄 en progreso / ❓ pendiente
```

3. **Actualizá el [DAG sección 6](#6-dag-de-dependencias-entre-waves)** agregando la nueva wave y la flecha causal.
4. **Actualizá [snapshot sección 7](#7-snapshot-estado-actual-del-brain)** con las métricas frescas.
5. **Si surgieron L-* o ADRs**: agregá filas en los apéndices A o B.
6. **Si surgieron Q-* nuevas**: agregalas al cuestionario en un bloque nuevo del archivo [`CUESTIONARIO_CAROLINA.md`](../agent-context/CUESTIONARIO_CAROLINA.md).
7. **Diario opcional**: si la wave tuvo un momento humano destacable, escribí entry en [`naked-erik-anatomy/`](../naked-erik-anatomy/).
8. **Commit y push** con mensaje formato: `Wave N <título>: <Q-* resueltas/nuevas>`.

### Regla de oro

Este archivo NO debe convertirse en duplicado de INDEX.md. Si vas a copiar más de 3 párrafos del INDEX acá, parate y reformulá. **Acá va la metainfo + links + recorrido**, no el contenido detallado.

---

**Fin del DISCOVERY_TREE.** Si encontraste algo desactualizado o ambiguo, abrí una Q-* o editá directo. Este archivo está vivo.
