---
id: ATLAS
tipo: atlas-maestro
estado: vigente
fecha: 2026-05-05
autores: [agente-brain-replit]
clientes: [becofarma, byb, cealsa, killios, killios_2026, mampa, mercopan, merhonsa]
ramas: [dev_2023_estable, dev_2028_merge]
tags: [atlas, indice, navegacion, F5, cliente-aware]
relacionado_con: [INDEX, BUG-001, F4, F5]
---

# ATLAS WMS — punto de entrada cliente-aware

> Última actualización: **2026-05-05** · Generado por F5.
> Complementa al `INDEX.md` (vista árbol del brain) con una vista **cliente × dimensión**.
> Para entender un cliente: arrancá por su **ficha atlas** (col 'Atlas'), después saltá a la ficha detallada (col 'Ficha').

## 1. Matriz maestra cliente × dimensión

| Cliente | Env | DB | ERP | Rama PRD | BUG-001 | Daños hist. | Pend. ERP | Atlas | Ficha detallada |
|---|---|---|---|---|---|---:|---:|---|---|
| **becofarma** | PRD | `IMS4MB_BECOFARMA_PRD` | SAP B1 | dev_2023_estable | no afectado | 0 | 10/10 | [`becofarma-atlas-2026-05-05.md`](../clients/becofarma-atlas-2026-05-05.md) | [`becofarma.md`](../clients/becofarma.md) |
| **byb** | PRD | `IMS4MB_BYB_PRD` | NAV | dev_2023_estable | MEDIA | 495 | 11/38 | [`byb-atlas-2026-05-05.md`](../clients/byb-atlas-2026-05-05.md) | [`byb.md`](../clients/byb.md) |
| **cealsa** | QAS | `IMS4MB_CEALSA_QAS` | Propio | dev_2028_merge | no afectado | 0 | 368/460 | [`cealsa-atlas-2026-05-05.md`](../clients/cealsa-atlas-2026-05-05.md) | [`cealsa.md`](../clients/cealsa.md) |
| **killios** | PRD | `TOMWMS_KILLIOS_PRD` | SAP B1 | dev_2023_estable | CRÍTICA | 6,500 | 722/825 | [`killios-atlas-2026-05-05.md`](../clients/killios-atlas-2026-05-05.md) | [`killios.md`](../clients/killios.md) |
| **killios_2026** | PRD | `TOMWMS_KILLIOS_PRD_2026` | SAP B1 | dev_2023_estable | CRÍTICA | 10,565 | 709/986 | [`killios_2026-atlas-2026-05-05.md`](../clients/killios_2026-atlas-2026-05-05.md) | [`killios.md`](../clients/killios.md) |
| **mampa** | QA | `TOMWMS_MAMPA_QA` | SAP B1 | dev_2028_merge | no afectado | 0 | 150/154 | [`mampa-atlas-2026-05-05.md`](../clients/mampa-atlas-2026-05-05.md) | _pendiente_ |
| **mercopan** | PRD | `IMS4MB_MERCOPAN_PRD` | NAV | dev_2023_estable | CRÍTICA | 19,607 | 1706/1877 | [`mercopan-atlas-2026-05-05.md`](../clients/mercopan-atlas-2026-05-05.md) | [`mercopan.md`](../clients/mercopan.md) |
| **merhonsa** | PRD | `IMS4MB_MERHONSA_PRD` | a confirmar | dev_2023_estable | n/a | 0 | 766/874 | [`merhonsa-atlas-2026-05-05.md`](../clients/merhonsa-atlas-2026-05-05.md) | [`merhonsa.md`](../clients/merhonsa.md) |

## 2. Vista por dimensión

### 2.1 Por ERP destino

- **NAV**: byb, mercopan
- **Propio**: cealsa
- **SAP B1**: becofarma, killios, killios_2026, mampa
- **a confirmar**: merhonsa

### 2.2 Por modelo de configuración

- **(pendiente análisis)**: merhonsa
- **BODEGA-CENTRIC**: mampa
- **MIXTO**: killios, killios_2026
- **PRODUCT-CENTRIC**: becofarma, mercopan
- **PRODUCT-CENTRIC NULL**: byb
- **PRODUCT-CENTRIC heterogéneo**: cealsa

### 2.3 Por rama productiva

- **`dev_2023_estable`**: becofarma, byb, killios, killios_2026, mercopan, merhonsa
- **`dev_2028_merge`**: cealsa, mampa

### 2.4 Por estado del BUG-001

| Severidad | Clientes | Acción |
|---|---|---|
| **CRÍTICA** | killios_2026 (cliente reportante), mercopan (mayor volumen histórico), killios (snapshot viejo) | Fix urgente; hotfix condicional según PLAYBOOK §H.3 |
| **MEDIA** | byb (484 líneas, outlier 21% HH) | Investigar HH específico; cliente posiblemente inactivo |
| **NO AFECTADO** | mampa, becofarma, cealsa, merhonsa | n/a |

## 3. Cross-refs maestros

### Capa código (F1+F2+F3)

- `code-deep-flow/DIFF-BOF-2023-VS-2028.md` — diff completo backend BOF VB.NET
- `code-deep-flow/DIFF-HH-2023-VS-2028.md` — diff handheld Android
- `code-deep-flow/FLAGS-CALLSITES.md` — 69 flags `i_nav_config_enc` × callsites código
- `code-deep-flow/00-mapa-de-cajas.md`, `02-portal-y-dms.md`, `03-implosion-y-merge-lp.md`, `04-mi3-y-reserva-clavaud.md`
- `code-deep-flow/traza-001-license-plate.md`, `traza-002-danado-picking.md`

### Capa datos (F4)

- `data-deep-dive/CROSS-COMPARATIVA.md` — tabla cruzada 8 BDs
- `data-deep-dive/<cliente>/snapshot-2026-05-05.md` — snapshot por cliente (8 archivos)
- `data-deep-dive/<cliente>/snapshot-2026-05-05-raw.json` — datos crudos para reprocesar

### Capa heat-map de capabilities

- `heat-map-params/cross-cliente/01-i_nav_config_enc.md` — 78 cols, schema drift severo
- `heat-map-params/cross-cliente/02-bodega.md` — 123 cols, capabilities por bodega
- `heat-map-params/cross-cliente/03-tipos-documento.md`
- `heat-map-params/cross-cliente/04-producto.md`

### Bugs y casos

- `wms-known-issues/BUG-001-danado-picking-no-resta-inventario/INDEX.md`
- `wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CLIENTES-AFECTADOS.md`
- `wms-known-issues/BUG-001-danado-picking-no-resta-inventario/CASOS-RELACIONADOS.md`
- `customer-open-cases/CP-013-killios-wms164/`
- `debuged-cases/CP-015-bug-danado-picking-transversal/`

### Arquitectura y decisiones

- ADRs por cliente: `architecture/adr/ADR-007-killios-sap-b1-integration.md`, `ADR-008-byb-replenishment-module.md`, `ADR-009-cealsa-3pl-jornada-prefactura.md`
- ADRs transversales: `ADR-005-identity-migration.md`, `ADR-006-multi-environment-config.md`, `ADR-010..012`
- Reglas: `entities/rules/` (12 reglas)
- Módulos: `entities/modules/` (incluye `mod-repo-tomwms-bof.md`, `mod-repo-tomhh2025.md`)

### Learnings cross-cliente más usados

- L-014 → BECOFARMA es BD diagnóstica, no productiva real
- L-015 → ClickOnce empaqueta TODAS las interfaces, dispatch dinámico
- L-017 → FK sentinela cero en outbox (no usa NULL)
- L-018 → lic_plate universal en outbox (99-100%)
- L-019 → `i_nav_config_enc` es la fuente maestra de capability flags
- L-022 → Patrón naming sincronizador (`SAPBOSync<Cliente>.exe`, `NavSync.exe`, `<Cliente>Sync.exe`)
- L-023 → BYB outbox parado dic-2023 → oct-2025

## 4. Cómo usar este atlas

**Caso 1: "hay un ticket de Killios sobre daños"**
→ abrir `clients/killios_2026-atlas-2026-05-05.md` → ver métricas BUG-001 → ejecutar Q1 contra EC2 → cruzar con `BUG-001/CLIENTES-AFECTADOS.md` para ver concentración por operador.

**Caso 2: "qué clientes están afectados por el flag X"**
→ abrir `code-deep-flow/FLAGS-CALLSITES.md` → ver callsites del flag → mapear a clientes con `heat-map-params/cross-cliente/01-i_nav_config_enc.md`.

**Caso 3: "un cliente nuevo se queja de que el outbox no envía"**
→ ejecutar Q3 (outbox por tipo) → cruzar con `learnings/L-022` (naming sync) y `L-010` (NAV no procesa ingresos) → ver fingerprint del cliente en `INDEX.md`.

**Caso 4: "diferencias entre BOF 2023 y 2028 en módulo Y"**
→ abrir `code-deep-flow/DIFF-BOF-2023-VS-2028.md` → buscar el módulo → cruzar con cliente productivo en col 'Rama PRD' del cuadro 2.

---
*F5 Atlas consolidado · generado 2026-05-05 por agente-brain-replit*