---
id: CLIENTES-AFECTADOS
tipo: known-issue
estado: vigente
titulo: BUG-001 — Matriz de clientes afectados
ramas: [dev_2023_estable, dev_2028_merge]
tags: [known-issue]
---

# BUG-001 — Matriz de clientes afectados

> Fuente original de los datos: `debuged-cases/CP-015-bug-danado-picking-transversal/DATOS-COMPARATIVOS.md` (DEPRECATED, ahora consolidado aqui).
> Audit ejecutable: `data-seek-strategy/templates/audit-bug-danado-multi-bd.py`.
> Server EC2 origen: `52.41.114.122,1437` (READ-ONLY, sa + WMS_KILLIOS_DB_PASSWORD).
> Snapshot: 2026-04-30.

---

## A. Tabla principal: estado del bug por BD

| BD | Operacion | Lineas con `dañado_picking=1` | UM fantasma | Productos afectados | Usuarios involucrados | % via BOF | % via HH | Periodo cubierto |
|:---|:---|---:|---:|---:|---:|---:|---:|:---|
| **TOMWMS_KILLIOS_PRD_2026** | PRD activo | **10,565** | **318,191** | 262 | 25 | 99.3% | 0.7% | 2025-06 → 2026-04 (11 meses, en curso) |
| **IMS4MB_MERCOPAN_PRD** | PRD (estado actual UNKNOWN) | **19,598** | **574,155** | 184 | 8 | 97.9% | 2.1% | 2022-02 → 2024-07 (29 meses, backup viejo) |
| TOMWMS_KILLIOS_PRD (vieja) | snapshot historico | 6,499 | 86,424 | 238 | 19 | 98.6% | 1.4% | 2025-06 → 2025-08 (3 meses) |
| **IMS4MB_BYB_PRD** | PRD (estado actual UNKNOWN) | **484** | **10,266** | 75 | 12 | 79.1% | **20.9%** ← outlier | 2023-02 → 2023-12 (11 meses, backup viejo) |
| **TOTAL CONFIRMADO** | — | **37,146** | **989,036** | — | — | **98.18%** | **1.82%** | 2022-02 → 2026-04 |
| TOMWMS_MAMPA_QA | QA | 0 | 0 | — | — | — | — | NO afectado (no usa la feature) |
| IMS4MB_BECOFARMA_PRD | PRD activo | 0 | 0 | — | — | — | — | NO afectado (compensa con AJCANTN intensivo) |
| IMS4MB_CEALSA_QAS | QA | 0 | 0 | — | — | — | — | NO afectado (QA sin operacion real) |
| IMS4MB_MERHONSA_PRD | (backup vacio) | UNKNOWN | UNKNOWN | — | — | — | — | Backup vacio, requiere refresh |

**Cifras a fecha del corte (2026-04-30)**:
- Total lineas dañadas sin descuento: **37,146**
- Total UM fantasma acumuladas en inventario logico: **989,036**
- 100% de los casos sin AJCANTN compensatorio asociado (comportamiento deterministico, no intermitente)

---

## B. Severidad por cliente

### CRITICA — fix urgente

#### Killios PRD 2026

- **Volumen**: 10,565 lineas / 318,191 UM en 11 meses.
- **Crecimiento**: sostenido sin meseta. Mes pico: **abril 2026 (1,904 lineas, 62,149 UM)**.
- **Concentracion de operadores**: Heidi Lopez + Kevin Ramos = 7,462 / 10,565 lineas = **71%**.
- **Compensacion manual existente**: 250 ajustes AJCANTN entre nov-2025 y abr-2026 (insuficiente vs el volumen del bug).
- **Es el cliente que reporto el caso**. Politicamente critico.
- **Rama productiva**: `dev_2023_estable`.
- **Posibilidad de hotfix**: condicional, criterios PLAYBOOK-FIX §H.3.

#### MERCOPAN PRD

- **Volumen**: 19,598 lineas / **574,155 UM** en 29 meses (2022-02 → 2024-07).
- **El mas grave en volumen absoluto historico.**
- **Crecimiento**: 14 lineas/mes en 2022-02 → 1,868 lineas/mes en 2024-07. Sin reset, sin caida.
- **Concentracion de operadores**: Felix Mariscal + Abel Castillo = 17,290 / 19,598 = **88%**.
- **Compensacion manual existente**: 3,094 AJCANTN historicos (mas que cualquier otro cliente).
- **Top productos afectados**: aceite Purela (2 presentaciones, 2,538 lineas combinadas), lavaplatos Klinpiax limon (1,067 lineas), detergente Ultraklin (1,319 lineas en 2 presentaciones).
- **Estado actual**: UNKNOWN. Backup termina jul-2024. Cola C-002 abre confirmacion.
- **Si sigue operativo**: aplicar fix urgente, reconciliacion AJCANTN masiva (~600k UM).
- **Si dejo de operar**: cerrar cliente en este BUG con nota.

### MEDIA — fix necesario pero menor presion

#### BYB PRD

- **Volumen**: 484 lineas / 10,266 UM en 11 meses.
- **Outlier estructural**: **21% de las marcaciones via HH** (vs 1-3% en el resto).
- **Hipotesis pendiente** (cola C-009): ¿BYB tiene una version distinta del HH Android? ¿O su flujo HH es operativamente distinto?
- **Top productos**: salsa de tomate (80 lin), mayonesa (64 lin), salsa picamas (21 lin).
- **Estado actual**: UNKNOWN. Backup termina dic-2023. Cola C-002.

### NO AFECTADOS — referencia para entender el alcance

#### MAMPA QA

- **Causa**: no usa la feature `dañado_picking` (cero registros con flag en 1).
- **Importante**: rama productiva es `dev_2028_merge` (la rama "moderna"). Si MAMPA empezara a usar la feature, el bug aparece igual porque el fix parcial 2028 esta comentado.
- **Buen cliente para validar el fix**: bajo riesgo, sin operacion productiva masiva.

#### BECOFARMA PRD

- **Causa**: NO marca dañados nunca, pero tiene un volumen ALTO de AJCANTN (591 ajustes en 3 meses).
- **Hipotesis** (cola C-004): su flujo operativo es "ajusto antes de picking" (inspeccion previa o devolucion a recepcion).
- **Posible mitigacion temporal para Killios**: copiar el patron de BECOFARMA mientras se aplica el fix definitivo.

#### CEALSA QAS

- **Causa**: cero AJCANTN, cero dañados. Probablemente QA sin operacion real.

---

## C. Crecimiento mensual (los 3 con bug confirmado)

### Killios PRD 2026

```
2025-06 |     1 lin |          5 UM
2025-07 |    24 lin |        242 UM
2025-08 |   257 lin |      9,019 UM
2025-09 |   421 lin |     12,084 UM
2025-10 |   780 lin |     20,447 UM
2025-11 | 1,067 lin |     31,156 UM
2025-12 | 1,294 lin |     38,492 UM
2026-01 | 1,556 lin |     45,847 UM
2026-02 | 1,489 lin |     45,031 UM
2026-03 | 1,772 lin |     53,719 UM
2026-04 | 1,904 lin |     62,149 UM   <-- mes en curso, ya pico
```

**Lectura**: arranco lento (jun-jul 2025) y se asento alrededor de
**1,500-1,900 lineas/mes desde nov-2025**. El cliente esta acumulando
~50,000 UM fantasma por mes.

### MERCOPAN PRD (29 meses)

```
2022-02 |    14 lin
2022-08 |   178 lin   <-- arranque sostenido
2023-12 | 1,442 lin
2024-07 | 1,868 lin   <-- pico, ultimo mes del backup
```

**Lectura**: 14 → 1,868 lineas/mes en 29 meses. Crecimiento sostenido.

### BYB PRD (11 meses)

```
2023-02 |   46 lin
2023-05 |   62 lin
2023-08 |   61 lin
2023-12 |   12 lin   <-- ultimo mes del backup
```

**Lectura**: meseta baja (~50 lineas/mes), nunca crecio explosivamente
como MERCOPAN/Killios. Pero el outlier HH (21%) requiere investigacion
aparte.

---

## D. Comportamiento HH vs BOF (confirmacion de hipotesis Erik)

Erik planteo: *"si la HH va al 50% del picking y marca dañado, ¿el
movimiento se registra en stock?"*. Datos:

| BD | Lineas BOF | Lineas HH | UM BOF | UM HH | % BOF |
|:---|---:|---:|---:|---:|---:|
| Killios PRD | 6,411 | 88 | 85,763 | 661 | 98.6% |
| Killios PRD 2026 | 10,493 | 72 | 317,614 | 577 | 99.3% |
| MERCOPAN PRD | 19,181 | 417 | 566,822 | 7,333 | 97.9% |
| BYB PRD | 383 | **101** | 8,883 | 1,383 | **79.1%** ← outlier |
| **TOTAL** | **36,468** | **678** | **979,082** | **9,954** | **98.18%** |

**Conclusion**:
- El **BOF VB.NET es el camino dominante del bug** (98% global).
- La HH aporta marginalmente excepto en BYB.
- BYB requiere investigacion especifica del HH (cola C-009).

---

## E. AJCANTN existentes por BD

| BD | AJCANTN totales | Primer registro | Ultimo registro | Lectura |
|:---|---:|:---|:---|:---|
| TOMWMS_KILLIOS_PRD | 7 | 2025-06-02 | 2025-07-04 | flujo arrancando |
| **TOMWMS_KILLIOS_PRD_2026** | **250** | 2025-11-30 | 2026-04-28 | compensacion manual creciente |
| TOMWMS_MAMPA_QA | 8 | 2026-02-18 | 2026-04-09 | uso esporadico QA |
| **IMS4MB_BECOFARMA_PRD** | **591** | 2026-01-05 | 2026-03-22 | flujo intensivo (no por bug, por diseño operativo) |
| IMS4MB_BYB_PRD | 49 | 2022-05-10 | 2023-12-12 | uso bajo |
| IMS4MB_CEALSA_QAS | 0 | — | — | nunca ajustan |
| **IMS4MB_MERCOPAN_PRD** | **3,094** | 2021-12-09 | 2024-07-29 | maxima compensacion historica del bug |

**Lectura central**: MERCOPAN y Killios estan **conscientemente
compensando el bug con AJCANTN manuales**. BECOFARMA usa AJCANTN como
parte normal del flujo (no por bug).

---

## F. Patrón de uso de `dañado_picking` por BD

| BD | `dañado_picking=1` | verificada=0 + despachada=0 | verificada > 0 | despachada > 0 |
|:---|---:|---:|---:|---:|
| Killios PRD | 6,500 | 6,499 (99.98%) | 1 | 0 |
| Killios PRD 2026 | 10,565 | 10,565 (100%) | 0 | 0 |
| BYB PRD | 495 | 484 (97.78%) | 11 | 0 |
| MERCOPAN PRD | 19,607 | 19,598 (99.95%) | 9 | 0 |

**Lectura**:
- En TODAS las BDs con uso de la feature, **el 99%+ de los dañados quedan
  fantasma** (verificada=0 y despachada=0).
- Las anomalias minimas (1 en Killios viejo, 11 en BYB, 9 en MERCOPAN) son
  casos aislados — probablemente operador re-proceso manualmente.
- Ninguna linea queda "Despachada > 0" tras marcar dañado. El bug es
  **100% consistente**.

---

## G. Estado de validacion por cliente (para cerrar el BUG)

| Cliente | Fix aplicado en codigo | Validacion post-fix con SQL/golden | Cliente puede cerrarse |
|:---|:---|:---|:---|
| Killios PRD 2026 | NO | NO | NO |
| MERCOPAN | NO (y posiblemente cliente inactivo) | NO | NO |
| BYB | NO (y posiblemente cliente inactivo) | NO | NO |
| MAMPA QA | NO | n/a (no afectado) | n/a |
| BECOFARMA PRD | NO | n/a (no afectado) | n/a |
| CEALSA QAS | NO | n/a (no afectado) | n/a |

**El BUG-001 cierra definitivamente cuando**:
- Killios + (MERCOPAN si activo) + (BYB si activo) tienen los 3 SI en su fila.
