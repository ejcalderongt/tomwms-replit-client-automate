---
id: REPORTE-MULTI-BD
tipo: debuged-case
estado: vigente
titulo: "CP-014 — Bug `dañado_picking` sin descuento de stock — TRANSVERSAL"
tags: [debuged-case]
---

# CP-014 — Bug `dañado_picking` sin descuento de stock — TRANSVERSAL

**Fecha del análisis:** 2026-04-30
**Severidad:** CRÍTICA — bug del software TOMWMS, no de configuración por cliente
**Caso padre:** CP-013-killios-wms164 (donde se descubrió originalmente)
**BDs analizadas:** 8 (7 con estructura comparable, 1 vacía — MERHONSA)

---

## 1. Resumen ejecutivo (para informar al cliente Killios)

El bug que descubrimos en Killios (`CP-013`) **no es exclusivo de su instalación**.
Está confirmado en **4 de 7 bases de datos comparables** que tenemos restauradas en EC2.

| Cliente              | Líneas afectadas | UM fantasma | Productos | Usuarios | Período                    | % desde BOF | % sin AJCANTN |
|----------------------|-----------------:|------------:|----------:|---------:|----------------------------|------------:|--------------:|
| **MERCOPAN_PRD**     |       **19,598** | **574,155** |       184 |        8 | 2022-02 → 2024-07 (29 m)   |      97.9 % |      **100 %** |
| **KILLIOS_PRD_2026** |       **10,565** | **318,191** |       262 |       25 | 2025-06 → 2026-04 (11 m)   |      99.3 % |      **100 %** |
| KILLIOS_PRD (vieja)  |            6,499 |      86,424 |       238 |       19 | 2025-06 → 2025-08 (3 m)    |      98.6 % |        100 %  |
| BYB_PRD              |              484 |      10,266 |        75 |       12 | 2023-02 → 2023-12 (11 m)   |      79.1 % |        100 %  |
| **TOTAL multi-BD**   |       **37,146** | **989,036** |         — |        — | 2022-02 → 2026-04          |      ~98 %  |        100 %  |

### Hallazgos transversales

1. **El bug es del software**, no de Killios. Aparece en 4 instalaciones independientes (3 productivas + 1 con histórico).
2. **MERCOPAN es el caso más grave en volumen absoluto**: 574k UM fantasma acumuladas durante 29 meses.
3. **97-99 % de los casos vienen del BOF VB.NET** (`IdOperadorBodega_Pickeo = 0`). La HH aporta poco.
4. **100 % de los casos no tienen AJCANTN asociado** — el bug es determinístico, no intermitente.
5. **MAMPA_QA, BECOFARMA_PRD, CEALSA_QAS no tienen el bug** porque NO usan la feature `dañado_picking` (cero registros con la flag en 1 — ver §4).

### Mensaje para el cliente Killios

> El stock fantasma observado en Killios responde a un comportamiento del WMS que afecta también a otros
> clientes que usan la marca de "dañado en picking" desde el BOF. El equipo de Killios incluso muestra
> conducta correcta: viene compensando el bug con ajustes manuales (1,653 ajustes históricos). El fix
> definitivo requiere modificar el código del BOF VB.NET (ver `PLAYBOOK-FIX.md` en CP-013).

---

## 2. Cronología mensual por BD afectada

### MERCOPAN_PRD (29 meses, peak julio-2024)

```
2022-02 |    14 lin |        192 UM
2022-04 |     3 lin |        119 UM
2022-07 |     7 lin |        200 UM
2022-08 |   178 lin |      8,040 UM   <-- arranque sostenido
2022-09 |   194 lin |      7,542 UM
2022-10 |    74 lin |      3,174 UM
2022-11 |    55 lin |      2,208 UM
2022-12 |   292 lin |      8,686 UM
2023-01 |   293 lin |     10,064 UM
2023-02 |   349 lin |     12,984 UM
2023-03 |   534 lin |     21,145 UM
2023-04 |   631 lin |     27,290 UM
2023-05 |   470 lin |     17,728 UM
2023-06 |   411 lin |     15,123 UM
2023-07 |   526 lin |     25,785 UM
2023-08 |   921 lin |     39,694 UM
2023-09 |   981 lin |     33,455 UM
2023-10 |   588 lin |     14,626 UM
2023-11 | 1,101 lin |     24,022 UM
2023-12 | 1,442 lin |     52,036 UM
2024-01 | 1,611 lin |     41,774 UM
2024-02 | 1,281 lin |     37,284 UM
2024-03 | 1,484 lin |     32,909 UM
2024-04 | 1,113 lin |     25,818 UM
2024-05 | 1,599 lin |     38,234 UM
2024-06 | 1,578 lin |     32,382 UM
2024-07 | 1,868 lin |     41,641 UM   <-- pico, último mes del backup
```

Crecimiento sostenido: 14 → 1,868 líneas/mes en 29 meses. Sin reset, sin caída. El backup termina en julio 2024.

### KILLIOS_PRD_2026 (11 meses, en producción)

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
2026-04 | 1,904 lin |     62,149 UM   <-- pico, mes en curso
```

Mismo patrón que MERCOPAN: crecimiento sostenido sin meseta.

### BYB_PRD (11 meses, ya cerrado)

```
2023-02 |   46 lin |       790 UM
2023-03 |   49 lin |     1,099 UM
2023-04 |   52 lin |       972 UM
2023-05 |   62 lin |     1,547 UM
2023-06 |   61 lin |     1,233 UM
2023-07 |   55 lin |     1,154 UM
2023-08 |   61 lin |     1,266 UM
2023-09 |   45 lin |       988 UM
2023-10 |   24 lin |       522 UM
2023-11 |   17 lin |       340 UM
2023-12 |   12 lin |       355 UM
```

Volumen mucho menor que MERCOPAN/Killios. El backup termina en diciembre 2023.

---

## 3. Top productos y usuarios por BD afectada

### MERCOPAN — Top 5 productos
| Código    | Nombre                                      | Líneas | UM      |
|:----------|:--------------------------------------------|-------:|--------:|
| 440292    | 0.410 L ACEITE PURELA 100 SOYA CJ-24        |  1,380 |  76,716 |
| 40292     | 0.430 L ACEITE PURELA 100 SOYA              |  1,158 |  68,118 |
| 265725    | 1 KG LAVAPLATOS KLINPIAX LIMÓN CJ12         |  1,067 |  20,777 |
| 212040M11 | 25 KG ULTRAKLIN F NATURAL                   |    723 |  17,939 |
| 212040M01 | 1 KG DETERG ULTRAKLIN FUERZA NATURAL CJ10   |    596 |  12,030 |

### MERCOPAN — Top 5 usuarios
| ID | Usuario           | Líneas | UM        |
|---:|:------------------|-------:|----------:|
| 10 | Felix Mariscal    |  8,840 | 259,574   |
|  8 | Abel Castillo     |  8,450 | 265,009   |
|  9 | Felix Ramirez     |  1,881 |  33,739   |
|  7 | Marcelo Clavaud   |    279 |  11,722   |
|  4 | (sin nombre)      |    125 |   3,879   |

> **Observación:** dos operadores (Felix Mariscal + Abel Castillo) generan 17,290 de 19,598 líneas (88 %).
> Mismo patrón concentrado que en Killios (Heidi López + Kevin Ramos = 7,462 / 10,565 = 71 %).

### KILLIOS_PRD_2026 — Top 5 productos
| Código  | Nombre                                            | Líneas | UM     |
|:--------|:--------------------------------------------------|-------:|-------:|
| WMS92   | MELOCOTON MIGUELS MITADES 6/2650GR 93.5OZ GALON   |    600 |  5,231 |
| WMS92   | MELOCOTON MIGUELS MITADES 6/2650GR 93.5OZ GALON   |    586 | 21,421 |
| WMS167  | MELOCOTON MIGUELS MITADES 12/820GR 29OZ           |    542 | 24,580 |
| WMS61   | MELOCOTON DE LA CIMA MITADES 12/820 GRS 29OZ      |    414 | 18,978 |
| WMS56   | MAIZ DULCE MIGUELS 24/425GR 15OZ                  |    371 | 13,535 |

> Notar el `WMS92` duplicado: hay dos `IdProductoBodega` distintos para el mismo producto (presentaciones distintas). Anti-patrón a tener presente.

### BYB_PRD — Top 3
| Código    | Nombre                            | Líneas | UM    |
|:----------|:----------------------------------|-------:|------:|
| 00191651  | SALSA DE TOMATE DULCE 32/400 g    |     80 | 3,240 |
| 00101605  | MAYONESA 32/375 g BOLSA WM        |     64 | 1,920 |
| 00170440  | SALSA PICAMAS VERDE 48/100 g      |     21 |   355 |

---

## 4. ¿Por qué BECOFARMA, CEALSA, MAMPA NO tienen el bug?

| BD                   | Líneas pic | `dañado_picking=1` | `dañado_verificacion=1` | `no_encontrado=1` | AJCANTN total |
|:---------------------|-----------:|-------------------:|------------------------:|------------------:|--------------:|
| MAMPA_QA             |        437 |                  0 |                       0 |                 0 |             8 |
| BECOFARMA_PRD        |     57,325 |                  0 |                       0 |                 0 |           591 |
| CEALSA_QAS           |     44,737 |                  0 |                       0 |                 0 |             0 |

**Conclusión:** estos clientes simplemente **no marcan dañados nunca**. No es que tengan una versión arreglada del WMS; es que su flujo operativo no usa la feature.

Esto es importante porque:

- **No invalida la hipótesis del bug.** El código del BOF y la HH siguen siendo los mismos en todas las instalaciones.
- **Sí explica por qué el bug es invisible para esos clientes.** Si nunca disparan `dañado_picking=1`, nunca acumulan stock fantasma por esta vía.
- **Pregunta abierta para el equipo Killios:** ¿BECOFARMA y CEALSA tienen otro mecanismo para reportar mercadería dañada en picking? (¿inspección previa? ¿devolución a recepción? ¿ajuste manual directo desde el BOF?)
- **MERCOPAN tiene también un volumen alto de AJCANTN** (3,094 ajustes en 29 meses) — están compensando manualmente, igual que Killios (250 ajustes recientes).

---

## 5. Comportamiento HH vs BOF — confirmación de hipótesis

Erik planteó: *"si la HH va al 50 % del picking y marca dañado, ¿el movimiento se registra en stock?"*. La respuesta de los datos es:

| BD                   | Origen BOF (`IdOperadorBodega_Pickeo=0`) | Origen HH (>0) | % BOF |
|:---------------------|-----------------------------------------:|---------------:|------:|
| KILLIOS_PRD          |                                    6,411 |             88 | 98.6 % |
| KILLIOS_PRD_2026     |                                   10,493 |             72 | 99.3 % |
| MERCOPAN_PRD         |                                   19,181 |            417 | 97.9 % |
| BYB_PRD              |                                      383 |            101 | 79.1 % |
| TOTAL                |                                   36,468 |            678 | 98.2 % |

**Hipótesis confirmada con matiz:**

- **El BOF VB.NET es el origen dominante del bug** (98 % global). El proceso "Procesar picking por BOF" que asume HH parece ser el camino más afectado.
- **La HH también aporta** (~2 % de los casos, 678 líneas globales), pero en mucho menor medida. Posibles causas:
  - La HH sí intenta mover stock pero el webservice del BOF lo descarta.
  - La HH solo marca dañado cuando ya hubo verificación parcial, y el BOF lo reprocesa mal.
  - **BYB es la excepción**: 21 % HH. Más distribuido — vale revisar el código de su instalación HH si difiere.

**Próximo paso técnico para Erik (encolado en `colas_pendientes`):**
- Aislar 5-10 casos HH (`IdOperadorBodega_Pickeo > 0`) y trazar su línea de tiempo completa.
- Comparar contra los 5-10 casos BOF equivalentes.
- Determinar si el código del webservice WMS deshace lo que la HH intenta hacer.

---

## 6. Conclusiones finales y acciones

1. **El bug es del software TOMWMS**, no atribuible a operación Killios.
2. **Está crónico desde al menos 2022** (evidencia MERCOPAN). Probablemente desde 2019 (evidencia ajustes históricos Killios — ver CP-013).
3. **El BOF VB.NET es el camino crítico**. La HH aporta marginalmente.
4. **Tres acciones encoladas:**
   - **a) Patch BOF VB.NET** (crítica) — ver `brain/debuged-cases/CP-013-killios-wms164/PLAYBOOK-FIX.md`. Aplica a Killios + MERCOPAN + cualquier nuevo cliente que use la feature.
   - **b) Reconciliación masiva AJCANTN** — primero Killios (10,565 líneas, 318k UM), después MERCOPAN si están operativos hoy.
   - **c) Inventarios cíclicos** — protocolo nuevo para detectar este tipo de bug en producción antes de que acumule.
5. **Pendiente: validar BYB y MERCOPAN actuales.** Estos backups son viejos (BYB diciembre 2023, MERCOPAN julio 2024). Confirmar con el cliente si siguen operando y reflashear backup actualizado.

---

## 7. Datos crudos

- JSON con cronologías + top productos + top usuarios por BD: `data-seek-strategy/templates/outputs/_datos-cp014.json`
- Estructura comparativa de las 8 BDs: `data-seek-strategy/templates/outputs/_estructura-multi-bd.json`
- Audit ejecutable y reproducible: `data-seek-strategy/templates/audit-bug-danado-multi-bd.py`
