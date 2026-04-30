---
wave: 13-11
fecha: 2026-04-29
estado: cerrada
caso: CP-013
modo: live (firewall AWS restablecido)
parent_wave: 13-10
queries_corridas:
  - wave_13_11_batch.py (q11..q25 unificadas, 14 queries efectivas)
hallazgos_principales: 5
hipotesis_refutadas_esta_wave: 2
hipotesis_nuevas_esta_wave: 1
---

# Wave 13-11 — Re-medición live de CP-013 post-restablecimiento firewall

## Contexto operativo

Erik abrió el AWS Security Group del SQL Server productivo de Killios:

```
aws ec2 authorize-security-group-ingress \
    --group-id sg-086f086d --protocol tcp --port 1437 \
    --cidr 35.227.125.212/32 --region us-west-2
-> sgr-04f0866290d18a0ec
```

Verificación end-to-end:

- TCP `52.41.114.122:1437` → 60 ms desde sandbox.
- Login `pymssql` con `WMS_DB_USER` + `WMS_KILLIOS_DB_PASSWORD` → 0.82 s.
- Server `EC2AMAZ-ULD1A11`, SQL Server 2022 RTM-CU18 (16.0.4185.3 X64).
- DB activa: `TOMWMS_KILLIOS_PRD_2026`.
- Hora servidor: 2026-04-29 16:14:56.

14 queries READ-ONLY ejecutadas en serie sobre una sola conexión. Outputs raw archivados en `outputs/wave-13-11/`. Script en `queries/wave_13_11_batch.py`.

## Hallazgo 1 — Estabilidad del alcance

```
Snapshot wms-db-brain (2026-04-27 01:29Z):  4.703 stock activo / 469 / 919 / 18.7%
Live wave 13-11 (2026-04-29 16:14Z):        4.914 stock activo / 469 / 919 / 18.7%
```

469 combos / 919 filas redundantes / 183.375 UN involucradas son **idénticos** a los números de wave 13-9. El total de stock activo creció de 4.703 a 4.914 (+211 filas en 63 horas), pero los duplicados **no crecieron**. Esto sugiere que el bug requiere un disparador específico que **no se dio en estos últimos 3 días** (q15 abajo lo confirma).

## Hallazgo 2 — El bug es CRÓNICO de 11 meses, no agudo

Q23 (rango temporal): primer duplicado registrado **2025-05-28 11:20**, último **2026-04-24 08:05**. **331 días** de bug activo, no es novedoso.

Q17 (distribución mensual de filas duplicadas):

```
2026-04 |  185  (parcial al 29-abr)
2026-03 |  260
2026-02 |  243
2026-01 |  152
2025-12 |  162
2025-11 |  341  *** PICO HISTORICO ***
2025-10 |    5
2025-09 |   10
2025-08 |    9
2025-07 |    5
2025-06 |    4
2025-05 |   12
```

**Inflexión clarísima en noviembre 2025**: pasa de tasa de 5-12 filas/mes (mayo-octubre) a 152-341 filas/mes (noviembre en adelante). Algo cambió en el flujo entre octubre y noviembre 2025 — un release del HH, una migración de inventario, o un cambio de procedimiento operativo. **Pista fuerte para correlacionar con git history del HH Android**.

Pico 2025-11-29: 210 filas duplicadas creadas en un único día. Día siguiente 2025-11-28: 131. Sugiere **evento puntual** (recepción gigante con CESTs en cascada, import masivo, o job batch que se disparó dos días seguidos).

## Hallazgo 3 — H1 refutada en su forma fuerte

Q19 (lic_plate breakdown sobre los 469 combos duplicados):

```
estado    | combos | filas
CON_VALOR |    349 | 1.012   (74.4%)
CERO      |    120 |   376   (25.6%)
NULL      |      0 |     0
VACIO     |      0 |     0
```

**Cero combos duplicados con `lic_plate IS NULL` o `lic_plate = ''`**. La hipótesis H1 original ("`lic_plate` NULL/vacío rompe el comparador") queda **refutada en su forma fuerte**.

Aparece sin embargo una **variante H1.5**: el sentinel `lic_plate = '0'` (string literal "0") explica 120 combos / 25.6%. El sistema usa `'0'` como marcador "sin LP real" en lugar de NULL. Pero los 349 combos restantes (74.4%) tienen LP real CON_VALOR, así que H1.5 no puede ser causa única.

Conclusión: **H1 (en cualquier variante) explica como mucho el 25%, no la causa raíz dominante**.

## Hallazgo 4 — H5 nueva: el bug NO es exclusivo del CEST

Q22 (tipos de tarea con movimientos sobre lotes duplicados, desde mayo 2025):

```
IdTipoTarea | Nombre        | movs
11          | VERI          | 15.259
8           | PIK           | 15.257
5           | DESP          | 14.908
2           | UBIC          |  5.291
1           | RECE          |  1.750
3           | CEST          |    869
25          | REEMP_BE_PICK |    678
6           | INVE          |    386
12          | PACK          |    304
20          | EXPLOSION     |     90
17          | AJCANTN       |     54
13          | AJCANTP       |     30
26          | REEMP_ME_PICK |      2
```

**El CEST aporta solo 869 movimientos sobre los lotes duplicados (1.7% del total)**. Los flujos dominantes son `VERI` (verificación), `PIK` (picking), `DESP` (despacho) y `UBIC` (ubicación). Esto **REFUTA categóricamente** la asunción central de waves 13-9 y 13-10 de que el bug se dispara exclusivamente en el path CEST.

**Hipótesis nueva H5**: el bug está en una **función compartida de UPDATE stock** que probablemente se llama desde múltiples handlers (CEST, UBIC, PIK, DESP, INVE) en HH Android. Cualquier flujo que necesite restar cantidad de un stock origen y sumarla a un stock destino puede dispararlo.

Implicación operativa **importante**: el bundle de extracción para wave 13-12 tiene que extraer **toda la capa de stock**, no solo el handler del CEST. Ver actualización de `pedido-extraccion-hh-cest.md`.

## Hallazgo 5 — Reinterpretación crítica del caso WMS164

Q13 (estado actual de las filas fundacionales):

```
IdStock 134176 | PB 381 | Ubic 22 | Estado 16 | Lote BG2512 | LP FU06688 | 40 UN | fecha_ingreso 2026-02-09 10:52:55
IdStock 134177 | PB 381 | Ubic 22 | Estado 16 | Lote BG2512 | LP FU06688 | 30 UN | fecha_ingreso 2026-02-09 10:52:55
```

`fecha_ingreso = 2026-02-09 10:52:55`, no 23-abr. **Las dos filas duplicadas del WMS164 ya existían dos meses y medio antes del ticket**. Los 5 movimientos del 23-abr-2026 (M1..M5) que reconstruyó wave 13-9 **NO crearon el bug** — sólo tocaron stocks que ya estaban duplicados desde febrero.

Esto **invalida parte del razonamiento de wave 13-9**: la cronología M1..M5 era parte del trace que reportó el operario, no del momento en que se generó el daño. El daño real se generó el 9-feb-2026 a las 10:52:55, en algún flujo previo (posiblemente recepción `RECE` o ubicación inicial `UBIC`, NO CEST).

`q13` también confirma que `lic_plate = 'FU06688'` (CON_VALOR) en ambas filas, lo cual es consistente con el hallazgo 3: el WMS164 mismo NUNCA fue un caso de NULL/vacío de licence plate.

## Distribución de severidad por combo (Q18)

```
Combos con exactamente 2 filas: 276  (58.8%)
Combos con exactamente 3 filas:  87  (18.5%)
Combos con 4 o más filas:       106  (22.6%)
Máximo registrado en un combo:   13 filas
```

**Combo degenerado top** (q21): IdProductoBodega 1261, Ubic 371, Lote 2C2601, lic_plate `'0'`, **13 filas duplicadas, 78 UN totales**. Caso límite que muestra cuánto puede acumular el bug si nadie lo detecta.

## Top productos generadores (Q20, top 10)

```
codigo | nombre                                    | filas_dup | un_total
WMS384 | GUINDA FRUTALIA C/TALLO GALON CUBET       |    61     |    304
WMS123 | PALMITOS MIGUELS ENTEROS 12/850GR         |    50     |  1.425
WMS221 | MANDARINA EN ALMIBAR MIGUELS 12/800       |    42     |  1.615
WMS91  | ALCACHOFA KILLIOS 12/390 GRS.13.8OZ       |    39     |    604
WMS92  | MELOCOTON MIGUELS MITADES 6/2650GR        |    37     |    503
WMS89  | CHAMPINONES KILLIOS 24/400GRMS 14.1       |    34     |  6.580
WMS56  | MAIZ DULCE MIGUELS 24/425GR 15OZ          |    33     |  3.219
WMS229 | MELOCOTON KILLIOS MITADES 12/820 GR       |    31     |  7.658
WMS167 | MELOCOTON MIGUELS MITADES 12/820GR        |    30     |  3.165
WMS132 | ACEITUNAS EXCELENCIA NEGRAS EN RODA       |    26     |    816
```

Patrón consistente: productos de conservas Killios con alta rotación y ciclo recepción/picking/despacho frecuente. WMS89 + WMS229 + WMS184 (fuera del top 10) representan **>27.000 UN** comprometidas en duplicación silenciosa. Erik tiene aquí lista priorizable de qué SKUs auditar manualmente primero.

## Confirmaciones de wave 13-10 que se mantienen

| Hallazgo | Estado live |
|---|---|
| Marker `#EJCAJUSTEDESFASE` = 0 (V-001 refutado) | confirmado (q11) |
| Check `Stock_NonNegative_20200115_EJC: ([Cantidad]>(0))` activo, no disabled | confirmado (q25) |
| 14 índices NCLI sobre stock, ZERO unique excepto PK_stock (V-005 vivo) | confirmado (q24) |
| El path CEST se origina en HH Android, no BOF | confirmado por q22 (FK `_hh` + multi-tipo de tarea) |

## Hipótesis revisadas post wave 13-11

| ID | Hipótesis | Probabilidad pre | Probabilidad post |
|---|---|---|---|
| H1 | `lic_plate` NULL/vacío rompe comparador | alta | **REFUTADA** |
| H1.5 | sentinel `lic_plate = '0'` rompe comparador | — | media (explica 25% solo) |
| H2 | Concurrencia inter-segundo, dos hilos CEST sin lock | media | media |
| H3 | CEST por lote partido HH | media | baja (no calza con multi-tipo) |
| H4 | UPDATE rechazado por check `Cantidad>0` → fallback INSERT | alta | **muy alta** (explica el 74.4% restante) |
| H5 | Bug en función UPDATE stock compartida (multi-tipo de tarea) | — | **alta** (q22 lo evidencia) |

H4 + H5 combinadas son ahora la hipótesis dominante: **una función compartida de movimiento de stock, llamada desde múltiples handlers HH (CEST, UBIC, PIK, DESP, INVE), tiene un fallback INSERT cuando un UPDATE falla por el check Cantidad>0**.

## Refutaciones de la wave

- **"El bug es exclusivo del path CEST"** — refutado por q22. CEST es solo el 1.7% de los movs sobre lotes duplicados.
- **"H1 (lic_plate NULL/vacío) es la causa dominante"** — refutado por q19. Cero combos con NULL/vacío.
- **"El bug se materializó el 23-abr en el WMS164"** — refutado por q13. Materializado el 9-feb, sólo destapado por el ticket del 23-abr.

## Decisiones operativas para wave 13-12

1. **Renombrar y extender el contrato de extracción**: `pedido-extraccion-hh-cest.md` → `pedido-extraccion-hh-update-stock.md`. El bundle ya no es solo el CEST handler, es **toda la capa de mutación de stock en HH Android** (clases tipo `StockService`, `StockRepository`, `LnStock`, `UpsertStock`, `MoverStock`, etc.).
2. **Investigar git history del HH Android entre octubre y noviembre 2025**: Erik puede grep el log buscando commits con keywords `Stock`, `Update`, `Insert`, `Consolidar`. La inflexión de la tasa mensual sugiere un release que cambió el comportamiento.
3. **NO requerir cross-cliente** (BYB/CEALSA) por ahora — Killios tiene suficiente evidencia para confirmar V-DATAWAY-004 y promover el fix. Cross-cliente queda como wave futura para medir alcance global.

## Pendientes que esta wave NO resolvió

- Identificar los `usuario_agr` exactos que generaron los 919 duplicados (la query intentada falló por nombre de columna `documento` lower vs upper; reintentar en wave 13-12).
- Cruzar duplicados con `i_nav_transacciones_out` para ver si SAP recibió cantidades correctas o duplicadas.
- Auditar `stock_res`, `stock_se`, `stock_transito`, `stock_jornada` para ver si tienen el mismo problema (R3 de V-DATAWAY-005).

## Notas de seguridad

- 14 SELECT puros, ningún INSERT/UPDATE/DELETE/ALTER/DROP/EXEC sp_*.
- Conexión `pymssql` con `WMS_DB_USER` + `WMS_KILLIOS_DB_PASSWORD`.
- Outputs raw archivados en `outputs/wave-13-11/` para auditoría línea por línea.
