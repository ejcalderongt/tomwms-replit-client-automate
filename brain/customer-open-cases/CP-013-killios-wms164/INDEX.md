---
id: CP-013
tipo: cp-open
cliente: killios
producto: WMS164
estado: open
severidad: alta
materializa_bug: BUG-001
trazas_relacionadas: [traza-002]
modulo: trans_picking_ubic
ramas_afectadas: [dev_2023_estable]
descubierto: 2026-04
tags: [cliente/killios, producto/wms164, bug/picking, bug/critico]
---
# CP-013 — Killios WMS164 — Stock fantasma por dañado_picking sin descuento

> **Indice maestro del caso**. Caso de cliente ABIERTO. Caso anchor del
> bug del producto **`BUG-001`**.

---

## TL;DR

Killios opera el WMS desde 2022. El operativo cuando marca producto
dañado durante el picking (campo `trans_picking_ubic.dañado_picking = 1`)
genera lineas que **NO descuentan stock**. Resultado: stock contable
infla mientras stock fisico baja. A 2026-04, la BD productiva de Killios
muestra **6,500 / 26,567 lineas (24 %)** con la flag `dañado_picking=1`,
todas sin AJCANTN asociado, todas con saldo neto inflado.

El bug es del producto (vive en VB.NET BOF + HH Android, no en
configuracion del cliente). Confirmado en 4 BDs distintas (Killios x2,
BYB, MERCOPAN). Total acumulado: ~37 K lineas / ~989 K UM fantasma.

**Estado del caso al 2026-04-30**: el trace de codigo esta completo
(`code-deep-flow/traza-002-danado-picking.md`). El fix esta encolado
en `PLAYBOOK-FIX.md`. NO aplicado todavia. **OPEN** hasta que se
aplique el fix en `dev_2023_estable` Killios + se valide post-fix.

---

## Archivos en esta carpeta

### Reportes ejecutivos / consolidados

| Archivo | Que es |
|---|---|
| `INFORME-EJECUTIVO.md` | Reporte para equipo no-tecnico (PM, comercial) |
| `REPORTE.md` | Reporte tecnico inicial (wave 13-9) |
| `REPORTE-CONCLUSION-V2.md` | Cierre de la conclusion (wave 13-10) |
| `REPORTE-CONCLUSION-V3.md` | Cierre actualizado con datos de wave 17 |
| `REPORTE-STOCK-EN-FECHA-V1.md` | Analisis especifico del reporte `frmStockEnUnaFecha` |
| `REPORTE-wave-13-10.md` | Analisis estructural offline (sin BD viva) |
| `REPORTE-wave-13-11.md` | Datos de wave 13-11 (queries con BD viva) |

### Evidencia cronicidad

| Archivo | Que es |
|---|---|
| `EVIDENCIAS-CRONICIDAD.md` | Primera tanda de evidencias (cronologia 2022-2026) |
| `EVIDENCIAS-CRONICIDAD-V2.md` | Re-corrida con queries refinadas |
| `EVIDENCIAS-CRONICIDAD-V3.md` | Wave 13-13 (explosion del problema) |
| `EVIDENCIAS-CRONICIDAD-V5.md` | Wave 13-14 (V3 + filtros adicionales) |
| `EVIDENCIAS-CRONICIDAD-V6.md` | Final pre-trace de codigo |

### Playbook de fix

| Archivo | Que es |
|---|---|
| `PLAYBOOK-FIX.md` | Pasos detallados del fix encolado: `clsLnStock_res_Partial.vb` lineas 1394-... + validacion post-fix con SQL |

### Inputs y outputs de queries

| Carpeta | Que tiene |
|---|---|
| `queries/` | Scripts Python que ejecutan queries SQL contra Killios (q01..q12, waves 13-11..18) |
| `outputs/` | Resultados crudos (`.out`, `.csv`) de cada wave (13-11, 13-12, 13-13, 13-14, 15, 16, 17, 18, 19, 21) |

### Pedidos de extraccion

| Archivo | Que es |
|---|---|
| `pedido-extraccion-hh-cest.md` | Contrato wave 13-11 con Erik para extraer datos del HH Android |

---

## Cross-refs

- **Bug del producto que materializa este caso**: [`BUG-001`](../../wms-known-issues/BUG-001-danado-picking-no-resta-inventario/INDEX.md)
- **Trace de codigo profundo**: [`traza-002`](../../code-deep-flow/traza-002-danado-picking.md)
- **Caso transversal historico (DEPRECATED)**: [`CP-015`](../../debuged-cases/CP-015-bug-danado-picking-transversal/INDEX.md)
- **Feature relacionado** (otro tema, mismo modulo HH): [`FEAT-001`](../../wms-incorporated-features/FEAT-001-validacion-implosion-rack/INDEX.md)
- **Taxonomia del brain**: [`TAXONOMIA.md`](../../agent-context/TAXONOMIA.md)
- **Convencion de numeracion**: [`NUMERACION.md`](../../agent-context/NUMERACION.md)

---

## Definition of Done para CERRAR este CP-013

1. Fix de `BUG-001` aplicado en `dev_2023_estable` Killios.
2. Validacion post-fix con queries SQL (las del PLAYBOOK-FIX.md) confirmando:
   - **0 lineas nuevas** con `dañado_picking=1` SIN AJCANTN asociado, en
     ventana de 30 dias post-fix.
   - **Saldo neto reconciliado** en una muestra de 50 productos top-volumen
     (ver `outputs/wave-21/W21-07-top-productos-ajustados.csv`).
3. Mover esta carpeta a `customer-closed-cases/CP-013-killios-wms164/`.
4. Agregar archivo `RESOLUCION.md` con: SHA del fix, fecha de aplicacion,
   query de validacion, lessons learned.

---

## Historia (timeline)

- **2022-01**: bug introducido (commit `'#AT 20220110 Se cambio del valor a true`).
- **2022-2026**: 6,500 lineas con `dañado_picking=1` acumuladas en Killios.
- **2026-04-29**: Erik me reporta el caso WMS164 (1 producto, 919 filas duplicadas).
- **2026-04-30 (wave 13-9 a 13-14)**: investigacion estructural + datos vivos. Confirmado bug del software.
- **2026-04-30 (wave 17)**: trace de codigo completo en `traza-002`.
- **2026-04-30 (wave 19+21)**: scope cross-cliente confirmado: 4 BDs afectadas.
- **2026-04-30 (post)**: caso transversal CP-015 deprecado, datos absorbidos en BUG-001.
- **PENDIENTE**: aplicar fix.
