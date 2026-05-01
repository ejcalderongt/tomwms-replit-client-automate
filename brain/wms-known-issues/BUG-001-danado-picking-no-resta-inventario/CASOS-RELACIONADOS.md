---
id: CASOS-RELACIONADOS
tipo: known-issue
estado: vigente
titulo: BUG-001 — Casos relacionados
ramas: [dev_2023_estable, dev_2028_merge]
tags: [known-issue]
---

# BUG-001 — Casos relacionados

> Esta tabla mantiene la trazabilidad entre el BUG (raiz en el producto) y
> los CP-NNN (ocurrencias por cliente). Un BUG puede tener N CPs apuntando
> a el. Un CP puede materializar 0 o 1 BUGs (si materializa 1, debe
> citarse aqui).

---

## Casos de cliente

### CP-013-killios-wms164 (ANCHOR / DESCUBRIMIENTO)

- **Tipo**: caso individual de cliente, materializado en BUG-001.
- **Cliente**: Killios.
- **Producto especifico**: WMS164 — MELOCOTON MIGUELS MITADES 12/820GR 29OZ.
- **Fecha de descubrimiento del caso**: 2026-04 (mes de la primera
  consulta directa de Killios).
- **Estado**: OPEN.
- **Ubicacion**: `customer-open-cases/CP-013-killios-wms164/`.
- **Contenido relevante para el BUG**:
  - `INFORME-EJECUTIVO.md` (12 KB) — reporte para Carolina del cliente.
  - `REPORTE-CONCLUSION-V3.md` (12 KB) — analisis tecnico final.
  - `EVIDENCIAS-CRONICIDAD-V*.md` (5 versiones) — evidencias del
    crecimiento sostenido.
  - **`PLAYBOOK-FIX.md` (24.7 KB)** — el fix tecnico propuesto, vigente
    para todos los clientes afectados. §A-§H. Incluye estrategia de
    ramas (2028 primero) y 6 casos golden de validacion.
  - `outputs/wave-13-11/*.out` — queries ejecutadas con resultados.
- **Definition of Done para cerrar este CP**:
  - El producto WMS164 en Killios tiene su stock reconciliado (AJCANTN
    aplicado o auditoria fisica).
  - Las nuevas marcaciones de dañado en Killios SI generan
    `trans_movimientos` de descuento.
- **Que pasa cuando se cierra el CP**: mover a
  `customer-closed-cases/CP-013-killios-wms164/` con `RESOLUCION.md`.
  Pero el BUG-001 sigue OPEN si otros clientes (MERCOPAN, BYB) no
  recibieron el fix.

---

## Casos historicos / deprecados

### CP-014-bug-danado-picking-transversal (DEPRECATED EN GITHUB)

- **Estado**: deprecado historico. Numero asignado por error sin
  verificar lista en GitHub.
- **Ubicacion**: solo existe en GitHub branch wms-brain (no en workspace
  local).
- **Reemplazado por**: CP-015 (en su momento) y ahora absorbido por este
  BUG-001.

### CP-015-bug-danado-picking-transversal (DEPRECATED 2026-04-30)

- **Estado**: DEPRECATED, absorbido por este BUG-001.
- **Ubicacion**: `debuged-cases/CP-015-bug-danado-picking-transversal/`
  (mantiene archivos como referencia historica, INDEX.md tiene header
  DEPRECATED).
- **Por que se deprecada**: este caso siempre fue una "vista
  transversal" del mismo bug que CP-013. Con la nueva taxonomia, los
  bugs del producto viven en `wms-known-issues/`, los casos de cliente
  en `customer-open-cases/` o `customer-closed-cases/`. CP-015 no era
  un caso de cliente, era una descripcion del bug — su lugar natural es
  ser este BUG-001.
- **Datos absorbidos**:
  - `DATOS-COMPARATIVOS.md` (8 KB) → consolidado en `CLIENTES-AFECTADOS.md`.
  - `REPORTE-MULTI-BD.md` (12 KB) → consolidado en este folder.
  - El INDEX.md viejo se mantiene en GitHub con header DEPRECATED.

---

## Trace de codigo asociado

### code-deep-flow/traza-002-danado-picking.md

- **Tipo**: trace tecnico del flujo de codigo, no es un CP de cliente.
- **Ubicacion**: `code-deep-flow/traza-002-danado-picking.md` (21 KB).
- **Contenido**:
  - 10 archivos VB.NET descargados de Azure DevOps (rama
    `dev_2023_estable` + `dev_2028_merge`).
  - Mapa de los 6 setters de `Dañado_picking=True` en
    `clsLnTrans_ubic_hh_enc_Partial.vb`.
  - Lectores y queries que filtran `dañado_picking=0`.
  - **Hallazgo nuevo**: lineas 1998-2008 de `clsLnStock_res_Partial.vb`
    en `dev_2028_merge` tienen un setter comentado (intento de fix
    parcial, NO aplicado a 2023).
  - 6 colas Q-* (C-006 a C-011) abiertas para cerrar la traza.
- **Importante**: este archivo es el sustento tecnico del BUG-001.
  Cualquier intento de fix debe comenzar por leer la traza-002 entera.

---

## Features hermanos (mismo modulo, archivos vecinos)

### FEAT-001-validacion-implosion-rack

- **Tipo**: feature incorporado, NO bug.
- **Ubicacion**: `wms-incorporated-features/FEAT-001-validacion-implosion-rack/`.
- **Por que aparece aqui**: el feature toca
  `clsLnTrans_ubic_hh_det_Partial.vb` (DETALLE), mientras que el BUG-001
  vive en `clsLnTrans_ubic_hh_enc_Partial.vb` (ENCABEZADO). Son archivos
  hermanos del mismo modulo.
- **Interaccion riesgosa**: cuando se aplique el fix del BUG-001, hay
  que validar que el `trans_movimientos` compensatorio del fix no se
  duplique con el del PASO 2 implosion del orquestador FEAT-001.
- **Cola abierta**: C-019 (en `colas-pendientes.md`).

---

## Casos futuros (placeholders)

Si en el futuro otro cliente reporta el mismo bug (BECOFARMA empieza a
usar la feature, o IDEALSA/MERHONSA al ponerse activos), crear nuevo
CP-NNN en `customer-open-cases/` y agregarlo a la tabla principal de este
archivo + a `CLIENTES-AFECTADOS.md`. Linkar tambien al PLAYBOOK-FIX
existente en CP-013 (es el mismo fix para todos).

---

## Resumen visual

```
                            BUG-001
                              |
            +-----------------+-----------------+
            |                 |                 |
       CP-013 (anchor)    CP-015 (DEPRECATED)  Trace
       OPEN, Killios      vista transversal    code-deep-flow/
       customer-open      debuged-cases/       traza-002-danado-picking.md
       -cases/            (mantiene ref)
            |
       PLAYBOOK-FIX.md (vigente para todos los clientes del BUG-001)
            |
       Validacion en MAMPA QA → promocion a 2028 → eventual hotfix 2023
```
