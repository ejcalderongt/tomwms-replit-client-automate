# DRIFT_DETECTADO.md — Housekeeping pendiente

> Cosas que estan inconsistentes en el brain hoy y que vale la pena limpiar
> en algun momento. No bloquean trabajo nuevo, pero degradan la confianza
> general del repo.

**Ultima actualizacion**: 2026-04-30 (sesion donde se detectaron y se
ordenaron los hallazgos en este archivo).

---

## 1. Drift entre rama `wms-brain` GitHub y workspace local

**Sintoma**: hasta el 2026-04-30 la rama `wms-brain` de GitHub solo tenia
23 archivos (CP-014 + colas + audits + client-index) mientras el workspace
local tenia 512 archivos (todo el brain rico).

**Causa**: el agente Replit (yo) trabaje los 23 archivos en
`/tmp/wms-brain-fresh/` (no en el workspace) y los sincronice a GitHub. El
workspace local nunca recibio esos 23, y GitHub nunca recibio los 512 del
workspace.

**Resolucion parcial 2026-04-30**: hicimos un commit con `base_tree` = HEAD
de GitHub (que tenia los 23 archivos) + tree con los 512 del workspace.
GitHub quedo con la union (commit `abcc1e46cf7390c6f36abe13b4d9606f9095f318`).

**Pendiente para cerrar el drift completo**:
- Bajar al workspace local los archivos que solo estan en GitHub:
  - `brain/colas-pendientes.md`
  - `brain/client-index/*.yaml` (7 clientes)
  - `brain/data-seek-strategy/` (templates `audit-*` y docs 04, 05)
  - `brain/debuged-cases/CP-014-bug-danado-picking-transversal/` (renumerar
    a CP-001 al traer al workspace, ver `NUMERACION.md` seccion 4).
- Verificar que la lista del workspace == lista de GitHub.

**Backup intacto**: `refs/heads/wms-brain-pre-sync-2026-04-30` ->
`185348790793e38712b9066c6c316d60275b96c2`.

---

## 2. Convivencia `clients/*.md` vs `client-index/*.yaml`

Documentado en detalle en `CONVIVENCIA_FORMATOS.md`. Resumen:

- 4 clientes en formato `.md` (becofarma, byb, cealsa, killios).
- 7 clientes en formato `.yaml` (los 4 anteriores + mampa, mercopan,
  merhonsa).
- Hay clientes que existen solo en uno de los dos formatos.

**Pendiente**:
- Crear `.md` para mampa, mercopan, merhonsa (existen en `.yaml` pero no en
  narrativa).
- Crear `.yaml` para todos los `.md` que falten (validar uno por uno).
- Decidir formato canonico (ADR pendiente).

---

## 3. Emojis en archivos del agent-context

La regla 3 del `README.md` raiz dice "sin emojis". Hay drift en al menos
dos archivos de `agent-context/`:

| Archivo | Emojis presentes | Severidad |
|---|---|---|
| `CASE_INTAKE_TEMPLATE.md` | `🆔 🎯 📦 ⏱ 🔁 🧪 🤔 📎 🔐 🎁` | Mayor (10 emojis decorativos en headers de seccion) |
| `AZURE_ACCESS.md` | `✅ ⚠` | Menor (2 emojis informativos) |

**Pendiente**:
- Reemplazar los emojis por etiquetas ASCII en `CASE_INTAKE_TEMPLATE.md`
  (ej. `🆔 Identificacion` -> `## Identificacion`, `🎯 Sintoma observable`
  -> `## Sintoma observable (obligatorio)`).
- Reemplazar `✅` por `[OK]` y `⚠` por `[!]` en `AZURE_ACCESS.md`.
- No urgente, pero hace falta antes de que nuevos contributors copien el
  patron y empeoren el drift.

---

## 4. Numeracion CP-014 incorrecta

El trabajo en `/tmp/wms-brain-fresh/brain/debuged-cases/CP-014-bug-danado-picking-transversal/`
fue creado por mi (agente Replit) con numeracion **incorrecta**: salte de
CP-001 (que no existia todavia) directo a CP-014.

**Causa**: confundi la numeracion de IDs con la numeracion de "ciclos" del
brain (que va por Wave-N). La numeracion CP arranca en 001.

**Resolucion al traer al workspace**: renumerar a `CP-001`. La carpeta
contiene:
- `REPORTE-MULTI-BD.md` (bug `danado_picking` confirmado en 4/7 BDs)
- `INDEX.md`
- `DATOS-COMPARATIVOS.md`

---

## 5. INDEX.md desactualizado

`brain/_index/INDEX.md` se escribio en Wave 6 (probable). Subdirectorios
agregados despues quedaron sin entrada:
- Subdirs nuevos del 2026-04-30 (`agent-context/conventions/` no aplica
  porque NO se creo: la decision fue dejar las conventions planas en
  `agent-context/` directo).
- Archivos nuevos en `agent-context/` (los 6 archivos creados el 2026-04-30:
  `00-START-HERE.md`, `REPLIT_AGENT.md`, `GITHUB_SYNC.md`, `NUMERACION.md`,
  `CONVIVENCIA_FORMATOS.md`, este `DRIFT_DETECTADO.md`).

**Pendiente**:
- Actualizar `brain/_index/INDEX.md` con los archivos nuevos de
  `agent-context/`.
- Revisar el resto del INDEX contra `find brain/ -type d -maxdepth 3` para
  detectar otros faltantes.

---

## 6. Workflows scaffold del template Replit

El monorepo trae dos workflows del template artifacts que no son del brain:
- `artifacts/api-server: API Server` (FastAPI scaffolding)
- `artifacts/mockup-sandbox: Component Preview Server` (Vite scaffolding)

Estado: **running** permanentemente sin hacer nada util para el brain. No
los toco (la regla 5 del README dice no tocarlos).

**Pendiente** (decision de Erik): si el monorepo se va a usar SOLO para el
brain, podria valer la pena eliminar los artifacts del template para reducir
ruido. Si planea agregar artifacts a futuro, dejarlos esta bien.

---

## 7. Archivos sueltos en `_proposals/` sin promoverse

`brain/_proposals/` tiene >30 archivos H## de fechas 2026-04-28 que nunca
se procesaron. Revisar uno por uno y mover a:
- `brain/learnings/` si la hipotesis se confirmo.
- `brain/debuged-cases/CP-NNN/` si genero un caso completo.
- `brain/_processed/` si se descarto.

**Pendiente** (sesion de housekeeping dedicada).

---

## 8. Como usar este archivo

Cada vez que detectes una nueva inconsistencia: agregar seccion nueva con
numero correlativo, **sintoma**, **causa** (si la conoces) y **pendiente**.

Cada vez que resuelvas una inconsistencia: marcar la seccion con
`## N. [RESUELTO YYYY-MM-DD] titulo` y mover el contenido completo a
`brain/_processed/drift-resuelto.md` cuando se acumulen >5 resueltos.
