---
id: REPORT-PASADA-3
tipo: reporte-pasada
estado: vigente
fecha: 2026-05-05
autores: [agente-brain-replit]
clientes: [becofarma, byb, cealsa, killios, killios_2026, mampa, mercopan, merhonsa]
ramas: [dev_2023_estable, dev_2028_merge]
tags: [reporte, pasada-3, atlas, F0, F1, F2, F3, F4, F5, F6, F7]
relacionado_con: [ATLAS, INDEX, BUG-001, PLAN-PARALELO]
---

# Pasada 3 — Atlas BOF/HH 2023↔2028 cliente-aware

> Reporte de cierre. Fecha: **2026-05-05**. Plan: 7 fases (F0–F7). Estado: **completado**.

## TL;DR

Se construyó un atlas navegable **cliente → rama → flags → código → datos → bugs**
para reducir el TTR (time-to-resolution) de tickets de soporte. El brain ahora cruza
4 capas independientes:

1. **Capa código** — diffs estructurales BOF y HH entre `dev_2023_estable` y `dev_2028_merge`,
   con cross-ref de los 69 flags `i_nav_config_enc` a sus callsites.
2. **Capa datos operativa** — snapshot SQL del 2026-05-05 sobre las 8 BDs productivas
   con métricas BUG-001 reproducibles.
3. **Capa atlas cliente-aware** — fichas por cliente con identificación rápida +
   queries de diagnóstico copy-paste + cross-refs al resto del brain.
4. **Capa metadatos / grafo** — frontmatter normalizado en 167 archivos →
   grafo Obsidian-like con **590 aristas** (4× respecto del baseline).

**Hallazgo crítico productivo confirmado en F4**:
BUG-001 (daño en picking no descuenta inventario, no genera AJCANTN) está activo
a escala industrial en MERCOPAN (19,607 daños / 0 ajustes), KILLIOS_2026 (10,565 / 0)
y KILLIOS (6,500 / 0). Ningún cliente afectado tiene los AJCANTN correspondientes
en `i_nav_transacciones_out`.

## Resumen por fase

| Fase | Estado | Commit | Outputs |
|---|---|---|---|
| F0 Setup | ✅ | _(setup)_ | ls-remote OK a TOMWMS_BOF + TOMHH2025; SELECT @@VERSION OK contra EC2 |
| F1 Inventario código | ✅ | `439e57e` | 4 checkouts BOF/HH × 2023/2028; CSVs intermedios |
| F2 Diffs 2023 vs 2028 | ✅ | `439e57e` | `code-deep-flow/DIFF-BOF-2023-VS-2028.md`, `DIFF-HH-2023-VS-2028.md` |
| F3 Flags callsites | ✅ | `439e57e` | `code-deep-flow/FLAGS-CALLSITES.md` (42 KB, 69 flags) |
| F4 Snapshot operativo | ✅ | `a46827d` | 8 snapshots `.md` + 8 raw `.json` + `CROSS-COMPARATIVA.md` + `snapshot_runner.py` |
| F5 Atlas consolidado | ✅ | `3dceb0f` | `_index/ATLAS.md` + 8 fichas `<cliente>-atlas-2026-05-05.md` + `build_atlas.py` |
| F6 Frontmatter + re-graph | ✅ | `e0d5d5c` | 167 archivos con frontmatter; grafo regenerado a 590 aristas |
| F7 Reporte final | ✅ | _(este commit)_ | `_index/REPORT-PASADA-3.md` |

## Inventario de outputs nuevos

### Capa código (F1+F2+F3)

- `code-deep-flow/DIFF-BOF-2023-VS-2028.md` — diff backend BOF VB.NET por archivo y función
- `code-deep-flow/DIFF-HH-2023-VS-2028.md` — diff handheld Android (Java) por archivo y método
- `code-deep-flow/FLAGS-CALLSITES.md` — 69 flags `i_nav_config_enc` × archivos:línea ambas ramas
- `code-deep-flow/00-mapa-de-cajas.md`, `02-portal-y-dms.md`, `03-implosion-y-merge-lp.md`, `04-mi3-y-reserva-clavaud.md`
- `code-deep-flow/traza-001-license-plate.md`, `traza-002-danado-picking.md`

### Capa datos (F4)

- `data-deep-dive/CROSS-COMPARATIVA.md` — tabla cruzada 8 BDs con 4 hallazgos clave
- `data-deep-dive/<cliente>/snapshot-2026-05-05.md` — snapshot por cliente (8 archivos)
- `data-deep-dive/<cliente>/snapshot-2026-05-05-raw.json` — datos crudos para reprocesar
- `tools/wms-deep-dive/snapshot_runner.py` — runner idempotente capabilities-aware

### Capa atlas (F5)

- `_index/ATLAS.md` — punto de entrada navegable cliente × dimensión
- `clients/<cliente>-atlas-2026-05-05.md` — 8 fichas atlas append-style
- `tools/wms-deep-dive/build_atlas.py` — generador idempotente

### Capa grafo (F6)

- 167 archivos `.md` con frontmatter normalizado (id, tipo, estado, clientes, ramas, tags)
- Grafo regenerado: **590 aristas** (vs ~150 baseline)

## Métricas clave del snapshot 2026-05-05

### BUG-001 — daños en picking sin ajuste

| Cliente | DB | Daños históricos | AJCANTN en outbox | Severidad |
|---|---|---:|---:|---|
| **mercopan** | `IMS4MB_MERCOPAN_PRD` | **19,607** | 0 | CRÍTICA |
| **killios_2026** | `TOMWMS_KILLIOS_PRD_2026` | **10,565** | 0 | CRÍTICA (cliente reportante) |
| **killios** | `TOMWMS_KILLIOS_PRD` | 6,500 | 0 | CRÍTICA (snapshot viejo) |
| **byb** | `IMS4MB_BYB_PRD` | 495 | 0 | MEDIA (outlier 21% HH) |
| **becofarma**, **cealsa**, **mampa**, **merhonsa** | varias | 0 | 0 | NO AFECTADO |

### Schema drift

- 4 BDs (BECOFARMA, BYB, CEALSA, MAMPA) **sin** `tipo_documento varchar` en `i_nav_transacciones_out`
  — usan legacy IMS4MB con `IdTipoDocumento int` (1/2/3).
- 4 BDs (KILLIOS, KILLIOS_2026, MERCOPAN, MERHONSA) **con** `tipo_documento varchar`.
- El runner detecta capability y reescribe Q3 dinámicamente.

### Backlog Enviado_A_ERP=0

- 7 de 8 clientes con ≥70% ajustes locales pendientes de sincronizar al ERP
- BYB es el único caso "maduro" con 29% pendientes
- MERCOPAN: 1,706/1,877 (91%); MERHONSA: 766/874 (88%); KILLIOS_2026: 709/986 (72%)

### Casos atípicos

- **MERHONSA**: 0 filas en `trans_picking_ubic` pero **1.014 M** filas en outbox →
  patrón de uso atípico documentado en `clients/merhonsa-atlas-2026-05-05.md`
- **BECOFARMA**: BD diagnóstica, no productiva real (L-014 ya sabido, confirmado por F4)

## Cómo usar el brain a partir de hoy

**Punto de entrada general**: `_index/INDEX.md` (vista árbol)
**Punto de entrada cliente-aware**: `_index/ATLAS.md` (matriz cliente × dimensión)

Casos típicos documentados en `_index/ATLAS.md` §4:

1. **"Hay un ticket de Killios sobre daños"** → ficha atlas Killios → ejecutar Q1 contra EC2 → cruzar con `BUG-001/CLIENTES-AFECTADOS.md`
2. **"Qué clientes están afectados por el flag X"** → `FLAGS-CALLSITES.md` → `heat-map-params/cross-cliente/01-i_nav_config_enc.md`
3. **"Un cliente nuevo se queja de que el outbox no envía"** → Q3 → `learnings/L-022` (naming sync) + `L-010` (NAV no procesa ingresos)
4. **"Diferencias entre BOF 2023 y 2028 en módulo Y"** → `DIFF-BOF-2023-VS-2028.md` → cruzar con cliente productivo en col 'Rama PRD' del ATLAS

## Decisiones de diseño tomadas durante la pasada

- **Append-style en F5**: las fichas existentes `clients/<x>.md` (densamente curadas)
  no se modificaron. Las fichas atlas son archivos paralelos con sufijo `-atlas-2026-05-05.md`.
  Si más adelante se decide consolidar, los generadores son idempotentes.
- **Snapshot runner capabilities-aware**: cada query SQL va envuelta en try/except
  y consulta primero `INFORMATION_SCHEMA` para detectar columnas opcionales.
  Esto le da a la pasada robustez contra el schema drift cross-cliente.
- **Push GitHub vía Python urllib + GITHUB_TOKEN con header `Authorization: token`**:
  el header `Bearer` da 401 con este PAT clásico en POST `/git/blobs`. Documentado para futuras pasadas.
- **F4 NO modificó `BUG-001/CLIENTES-AFECTADOS.md`**: ese archivo tiene curación humana
  por operador. F4 solo agregó un párrafo de validación al pie.

## Pendientes / próximas pasadas sugeridas

1. **Cerrar `clients/mampa.md`** — única ficha base que falta (atlas ya existe).
2. **Pasada 4 sugerida**: snapshot incremental semanal automatizado vía cron en EC2 →
   delta vs último snapshot → alerta si BUG-001 crece.
3. **Investigar MERHONSA**: 0 daños pero 1.014M outbox. ¿Está usando otro flujo?
   ¿Sincroniza pero no genera picking en WMS? Ticket de discovery sugerido.
4. **Ramificar dev_2028_merge a producción**: hoy solo CEALSA y MAMPA están ahí.
   Validar fix BUG-001 en 2028 antes de migrar KILLIOS_2026 / MERCOPAN.

---
*Cierre Pasada 3 — agente-brain-replit · 2026-05-05*
