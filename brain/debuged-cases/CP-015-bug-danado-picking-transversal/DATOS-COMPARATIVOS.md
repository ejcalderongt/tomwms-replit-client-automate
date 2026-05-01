---
id: DATOS-COMPARATIVOS
tipo: debuged-case
estado: vigente
titulo: CP-014 — Datos comparativos crudos
tags: [debuged-case]
---

# CP-014 — Datos comparativos crudos

## A. Estructura de las 8 BDs en el server EC2 `52.41.114.122,1437`

| BD                       | Tablas | trans_picking_ubic | trans_movimientos | trans_ajuste_enc | col `dañado_picking` | col `IdOperadorBodega_Pickeo` | Filas TPU | Rango fec_agr            |
|:-------------------------|------:|:------------------:|:-----------------:|:----------------:|:--------------------:|:-----------------------------:|----------:|:--------------------------|
| TOMWMS_KILLIOS_PRD       |   345 |         SI         |        SI         |        SI        |         SI           |             SI                |    26,567 | 2025-06-02 → 2025-08-19   |
| TOMWMS_KILLIOS_PRD_2026  |   360 |         SI         |        SI         |        SI        |         SI           |             SI                |    58,301 | 2025-06-05 → 2026-04-29   |
| TOMWMS_MAMPA_QA          |   356 |         SI         |        SI         |        SI        |         SI           |             SI                |       437 | 2026-02-17 → 2026-04-16   |
| IMS4MB_BECOFARMA_PRD     |   354 |         SI         |        SI         |        SI        |         SI           |             SI                |    57,325 | 2026-01-06 → 2026-04-27   |
| IMS4MB_BYB_PRD           |   348 |         SI         |        SI         |        SI        |         SI           |             SI                |   420,909 | 2022-05-09 → 2025-11-04   |
| IMS4MB_CEALSA_QAS        |   351 |         SI         |        SI         |        SI        |         SI           |             SI                |    44,737 | 2022-06-29 → 2026-02-05   |
| IMS4MB_MERCOPAN_PRD      |   322 |         SI         |        SI         |        SI        |         SI           |             SI                |   125,472 | 2021-12-09 → 2024-07-31   |
| IMS4MB_MERHONSA_PRD      |   319 |         SI         |        SI         |        SI        |         SI           |             SI                |         0 | (vacía — backup escueto)  |

**Conclusión estructural:** las 8 BDs comparten esquema relevante. IMS4MB y TOMWMS son la misma genealogía (Inventory Mgmt System Multi-Bodega → renombrado a TOMWMS).

## B. Audit del bug por BD

| BD                      | Líneas bug | UM fantasma | Productos | Usuarios | BOF (id=0) | HH (id>0) | UM BOF      | UM HH    | Total dañados | sin AJCANTN |
|:------------------------|-----------:|------------:|----------:|---------:|-----------:|----------:|------------:|---------:|--------------:|------------:|
| TOMWMS_KILLIOS_PRD      |      6,499 |      86,424 |       238 |       19 |      6,411 |        88 |      85,763 |      661 |         6,500 |       6,499 |
| TOMWMS_KILLIOS_PRD_2026 |     10,565 |     318,191 |       262 |       25 |     10,493 |        72 |     317,614 |      577 |        10,565 |      10,565 |
| TOMWMS_MAMPA_QA         |          0 |           0 |         — |        — |          — |         — |           — |        — |             0 |           — |
| IMS4MB_BECOFARMA_PRD    |          0 |           0 |         — |        — |          — |         — |           — |        — |             0 |           — |
| IMS4MB_BYB_PRD          |        484 |      10,266 |        75 |       12 |        383 |       101 |       8,883 |    1,383 |           495 |         484 |
| IMS4MB_CEALSA_QAS       |          0 |           0 |         — |        — |          — |         — |           — |        — |             0 |           — |
| IMS4MB_MERCOPAN_PRD     |     19,598 |     574,155 |       184 |        8 |     19,181 |       417 |     566,822 |    7,333 |        19,607 |      19,598 |
| **TOTAL bug**           | **37,146** | **989,036** |         — |        — |     36,468 |       678 |     979,082 |    9,954 |        37,167 |      37,146 |

**% BOF global:** 36,468 / 37,146 = **98.18 %**
**% sin AJCANTN global:** 37,146 / 37,146 = **100 %**

## C. AJCANTN (`IdTipoTarea = 17`) por BD

| BD                       | AJCANTN totales | Primer registro          | Último registro          |
|:-------------------------|----------------:|:-------------------------|:-------------------------|
| TOMWMS_KILLIOS_PRD       |               7 | 2025-06-02 20:39:42      | 2025-07-04 13:07:13      |
| TOMWMS_KILLIOS_PRD_2026  |             250 | 2025-11-30 11:38:50      | 2026-04-28 09:07:13      |
| TOMWMS_MAMPA_QA          |               8 | 2026-02-18 14:52:25      | 2026-04-09 12:03:24      |
| IMS4MB_BECOFARMA_PRD     |             591 | 2026-01-05 11:05:14      | 2026-03-22 16:04:20      |
| IMS4MB_BYB_PRD           |              49 | 2022-05-10 18:15:02      | 2023-12-12 13:03:26      |
| IMS4MB_CEALSA_QAS        |               0 | —                        | —                        |
| IMS4MB_MERCOPAN_PRD      |           3,094 | 2021-12-09 14:42:57      | 2024-07-29 17:31:43      |

**Lecturas:**

- **BECOFARMA usa AJCANTN intensamente** (591 ajustes en 3 meses) sin marcar dañados nunca: probablemente su flujo es "ajusto antes de picking" o "ajusto en recepción".
- **MERCOPAN compensa el bug** (3,094 ajustes) — comportamiento idéntico al de Killios pero a mayor escala.
- **CEALSA no ajusta nunca**: posible que sea QA real sin operación.

## D. Patrón de uso de `dañado_picking` por BD

| BD                       | Total `dañado_picking=1` | Verificada=0 y Despachada=0 | Verificada > 0 | Despachada > 0 | Inactivo |
|:-------------------------|-------------------------:|----------------------------:|---------------:|---------------:|---------:|
| TOMWMS_KILLIOS_PRD       |                    6,500 |                       6,499 |              1 |              0 |        0 |
| TOMWMS_KILLIOS_PRD_2026  |                   10,565 |                      10,565 |              0 |              0 |        0 |
| TOMWMS_MAMPA_QA          |                        0 |                           — |              — |              — |        — |
| IMS4MB_BECOFARMA_PRD     |                        0 |                           — |              — |              — |        — |
| IMS4MB_BYB_PRD           |                      495 |                         484 |             11 |              0 |        0 |
| IMS4MB_CEALSA_QAS        |                        0 |                           — |              — |              — |        — |
| IMS4MB_MERCOPAN_PRD      |                   19,607 |                      19,598 |              9 |              0 |        0 |

**Lecturas:**

- En TODAS las BDs con uso de la feature, **el 99 %+ de los dañados quedan fantasma** (verificada=0 y despachada=0).
- Las anomalías mínimas (1 en KILLIOS_PRD vieja, 11 en BYB, 9 en MERCOPAN) merecen investigación de caso aislado: ¿operador re-procesó manualmente? ¿quizá esos sí dispararon AJCANTN?
- Ninguna línea queda "Despachada > 0" tras marcar dañado. **El comportamiento es 100 % consistente** con la hipótesis de bug de software.

## E. Comportamiento HH vs BOF

| BD                       | BOF (id=0) lineas | BOF UM     | HH (id>0) lineas | HH UM   | % BOF |
|:-------------------------|------------------:|-----------:|-----------------:|--------:|------:|
| TOMWMS_KILLIOS_PRD       |             6,411 |     85,763 |               88 |     661 | 98.6 % |
| TOMWMS_KILLIOS_PRD_2026  |            10,493 |    317,614 |               72 |     577 | 99.3 % |
| IMS4MB_BYB_PRD           |               383 |      8,883 |              101 |   1,383 | 79.1 % |
| IMS4MB_MERCOPAN_PRD      |            19,181 |    566,822 |              417 |   7,333 | 97.9 % |
| **TOTAL**                |        **36,468** | **979,082**|          **678** | **9,954**| **98.18 %** |

**BYB es el outlier (21 % HH).** Vale revisar específicamente la versión HH desplegada en BYB y comparar con la de Killios/MERCOPAN.
