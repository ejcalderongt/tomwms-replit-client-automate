---
id: BUG-001
tipo: bug
estado: open
severidad: critica
prioridad: alta
modulo: trans_picking_ubic
clientes: [killios, mercopan, byb]
ramas_afectadas: [dev_2023_estable, dev_2028_merge]
casos_relacionados: [CP-013]
trazas_relacionadas: [traza-002]
descubierto: 2026-04-30
tags: [bug/picking, bug/critico, modulo/inventario, modulo/bof-vbnet]
---
# BUG-001 — Killios WMS164 — Producto dañado que no se resta de inventario

> **Primer fine-tune issue de Erik como Director de Desarrollo de TOMWMS.**
> Bug del producto, ya confirmado en multiples clientes. Este BUG nace
> formalmente con la nueva taxonomia (2026-04-30, ver `agent-context/TAXONOMIA.md`).
> Hasta esta fecha vivia disperso entre CP-013 (caso anchor Killios) y
> CP-015 (vista transversal). Ahora se consolida.

---

## Identificacion

| Campo | Valor |
|---|---|
| **ID** | BUG-001 |
| **Titulo corto** | dañado_picking sin descuento de stock |
| **Componente** | TOMWMS BOF VB.NET (97-99%) y HH Android (1-21%) |
| **Severidad** | CRITICA — genera stock fantasma sostenido |
| **Prioridad** | ALTA — bloquea inventarios ciclicos confiables |
| **Estado** | OPEN — fix tecnico definido pero NO aplicado en PRD de ningun cliente |
| **Primera deteccion** | 2026-04 (Killios reporta caso WMS164 puntual) |
| **Primera evidencia historica** | 2022-02 (MERCOPAN, ver §clientes) |
| **Comentario `'#AT 20220110`** | enero 2022, en `clsLnTrans_ubic_hh_enc_Partial.vb` linea 1394, sugiere bug arrastrado desde 2019-2022 |
| **Caso ancla (descubrimiento)** | producto WMS164 en Killios, ver `customer-open-cases/CP-013-killios-wms164/` |
| **Definition of Done** | (1) fix mergeado en rama productiva del cliente; (2) validacion post-fix con query SQL o golden test que confirme no-reproduccion |

---

## Sintoma observable

Cuando un operador marca una linea de picking como **"producto dañado"**:

1. La fila en `trans_picking_ubic` queda con `dañado_picking = 1`,
   `cantidad_verificada = 0`, `cantidad_despachada = 0`.
2. **NO se genera ningun `trans_movimientos`** que descuente el stock dañado
   del inventario.
3. **NO se genera ningun ajuste `AJCANTN`** (`IdTipoTarea = 17`) automatico
   que reconcilie la diferencia.
4. El stock dañado queda **fantasma**: visible en el sistema, contado en
   inventario logico, pero fisicamente no esta (o esta no-utilizable).
5. Como el operador igual sigue su flujo, el sistema "cree" que la cantidad
   sigue disponible. La proxima orden de picking puede asignar ese stock
   fantasma y el problema se cascada.

**Resultado a los meses**: divergencia entre stock logico (BD) y stock
fisico (bodega), que solo se descubre con inventario ciclico o cuando el
operador no encuentra el producto al hacer el picking del siguiente pedido.

---

## Mecánica técnica (resumen)

> Detalle linea-por-linea esta en `code-deep-flow/traza-002-danado-picking.md` (21 KB).

**Donde se prende `Dañado_picking = True`**:

1. **6 setters en BOF VB.NET** (`clsLnTrans_ubic_hh_enc_Partial.vb`, multiple
   ramas, capitalizacion `Dañado_picking` con Ñ mayuscula).
2. **1 setter en HH Android** (`clsBeProducto_estado.java`).
3. Al menos **1 Form BOF** que dispara el flujo desde un boton "Marcar
   dañado" — Form exacto pendiente de identificar (cola C-006).

**Que NO existe**:

- **No hay trigger de BD** que reaccione al UPDATE.
- **No hay stored procedure** que ajuste stock cuando la flag pasa a 1.
- **No hay generacion automatica de `trans_movimientos`** ni `AJCANTN`.

**Toda la logica deberia estar en VB.NET y NO esta**. El bug es por
omision: alguien penso "marco la flag" pero olvido "y descuento el stock".

**Fix parcial encontrado en `dev_2028_merge`** (rama de estabilizacion):
- Lineas 1998-2008 de `clsLnStock_res_Partial.vb` tienen un setter de
  `Dañado_picking=True` **comentado**. Es un intento previo de bloquear
  uno de los 6 puntos de marcaje. Pero:
  - No fue aplicado a `dev_2023_estable` (rama productiva de Killios).
  - Es incompleto: solo tapa un punto, no agrega la logica de descuento
    requerida.
  - Cola C-010 abre git blame para identificar quien lo intento y por
    que no lo termino.

---

## Versiones del producto afectadas

| Rama TOMWMS_BOF | Estado del bug | Notas |
|---|---|---|
| `dev_2023_estable` | BUG ACTIVO sin mitigacion | Productivo en Killios PRD, BYB, BECO, CEALSA, MERCOPAN. Sin fix. |
| `dev_2028_merge` | BUG ACTIVO con fix parcial comentado | Productivo en MAMPA QA. Tiene lineas 1998-2008 de `clsLnStock_res_Partial.vb` comentadas (intento parcial). |
| futuro `dev_2028_merge` post-fix | Target del fix | Aplicar `customer-open-cases/CP-013-killios-wms164/PLAYBOOK-FIX.md` segun §H del playbook. |

---

## Estado del fix

| Etapa | Estado | Donde vive |
|---|---|---|
| Identificacion del bug | DONE | CP-013 + traza-002 |
| Confirmacion cross-cliente | DONE | CP-015 → este archivo |
| Trace de codigo VB.NET y Java HH | DONE | `code-deep-flow/traza-002-danado-picking.md` |
| Diseño del fix tecnico | DONE | `customer-open-cases/CP-013-killios-wms164/PLAYBOOK-FIX.md` (24.7 KB, §A-§H) |
| Estrategia de ramas (2028 primero, 2023 si urge hotfix) | DONE | PLAYBOOK-FIX §H |
| 6 casos golden definidos para validacion QA | DONE | PLAYBOOK-FIX §G.4 |
| Aplicacion en `dev_2028_merge` | PENDIENTE | — |
| Validacion en MAMPA QA con golden | PENDIENTE | — |
| Promocion a `dev_2023_estable_hotfix_*` (si urge) | CONDICIONAL | criterios PLAYBOOK-FIX §H.3 |
| Reconciliacion masiva AJCANTN historico | PENDIENTE | (10,565 lineas Killios, 19,598 MERCOPAN) |
| Inventario ciclico nuevo como red de seguridad | PENDIENTE | propuesta abierta |

---

## Casos relacionados (resumen)

Lista detallada con SHAs, fechas, links → `CASOS-RELACIONADOS.md`.

| Caso | Tipo | Cliente | Volumen | Estado |
|---|---|---|---:|---|
| CP-013-killios-wms164 | anchor (descubrimiento) | Killios | 1 producto puntual | OPEN, en `customer-open-cases/` |
| CP-015 (DEPRECATED) | vista transversal | 4 clientes | 37,146 lineas / 989,036 UM | absorbido por este BUG |
| (futuros) | nuevos casos por cliente | — | — | apuntar a este BUG-001 |

---

## Clientes afectados (resumen)

Matriz completa con cifras → `CLIENTES-AFECTADOS.md`.

| Cliente | Volumen del bug | Severidad cliente | Notas |
|---|---:|---|---|
| **MERCOPAN** | 19,598 lineas / **574,155 UM** en 29 meses | CRITICA | Mayor volumen acumulado historicamente. Backup termina jul-2024, validar si sigue activo. |
| **Killios PRD 2026** | 10,565 lineas / 318,191 UM en 11 meses | CRITICA | Cliente que reporto. Crecimiento sostenido sin meseta. Mes pico: abril 2026 (1,904 lineas). |
| **BYB** | 484 lineas / 10,266 UM en 11 meses | MEDIA | Outlier: **21% via HH** (vs 1-3% en otros clientes). Backup termina dic-2023. |
| Killios PRD 2025 (vieja) | 6,499 lineas / 86,424 UM en 3 meses | (referencia historica) | Backup viejo, util para comparar evolucion. |
| MAMPA QA | 0 | NO AFECTADO | No usa la feature `dañado_picking` |
| BECOFARMA PRD | 0 | NO AFECTADO | No usa la feature; compensa con AJCANTN intensivo (591/3 meses) |
| CEALSA QAS | 0 | NO AFECTADO | No usa la feature; QA sin operacion |
| MERHONSA PRD | 0 | UNKNOWN | Backup vacio, no se puede confirmar |

**TOTAL multi-BD confirmado: 37,146 lineas / 989,036 UM fantasma.**

---

## Proximos pasos

1. **Cerrar las 6 colas Q-* abiertas** del trace de codigo (C-006 a C-011 en `colas-pendientes.md`).
2. **Aplicar PLAYBOOK-FIX en `dev_2028_merge`** y validar con MAMPA QA (sin riesgo a clientes productivos).
3. **Decidir hotfix a `dev_2023_estable_hotfix_*`** segun criterios del PLAYBOOK §H.3 — solo si Killios no aguanta el release general 2028.
4. **Coordinar con CP-016 / FEAT-001** (feature de Abigail Alvarado) — son archivos hermanos del mismo modulo. Validar interaccion segun cola C-019.
5. **Reconciliacion masiva AJCANTN historico** una vez aplicado el fix (Killios primero, MERCOPAN si sigue activo).
6. **Confirmar estado actual de MERCOPAN y BYB** — backups viejos, posible que ya no operen (cola C-002).

---

## Definition of Done para cerrar este BUG

Cuando se cumplan **ambas** condiciones, mover este folder a un futuro
`wms-known-issues-resolved/` (o equivalente):

1. **Codigo**: el fix del PLAYBOOK-FIX esta mergeado en la rama productiva
   de **TODOS los clientes afectados** (Killios, BYB, MERCOPAN si activo).
2. **Validacion**: existe query SQL ejecutada en cada cliente que confirma
   que ya no se reproduce (lineas nuevas con `dañado_picking=1` SI tienen
   `trans_movimientos` asociado de descuento).

Mientras un solo cliente afectado quede sin fix aplicado, este BUG sigue OPEN.

---

## Cross-refs

- Caso anchor: `customer-open-cases/CP-013-killios-wms164/`
- Caso transversal historico (DEPRECATED): `debuged-cases/CP-015-bug-danado-picking-transversal/`
- Trace de codigo profundo: `code-deep-flow/traza-002-danado-picking.md`
- Feature hermano (mismo modulo, archivos vecinos): `wms-incorporated-features/FEAT-001-validacion-implosion-rack/`
- Convencion taxonomica: `agent-context/TAXONOMIA.md`
- Convencion numeracion: `agent-context/NUMERACION.md`
- Colas abiertas: `colas-pendientes.md` (C-006 a C-011 son del bug; C-019 es de la interaccion con FEAT-001)
