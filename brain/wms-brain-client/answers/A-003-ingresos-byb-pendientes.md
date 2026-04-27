---
protocolVersion: 1
id: A-003
answersQuestion: Q-003
title: Por que BB tiene 110k INGRESOS pendientes en outbox (PEND-12)
operator: agent-replit
operatorRole: developer
target:
  codename: BB
  environment: PRD
executedAt: 2026-04-27T15:00:00Z
durationSeconds: 18
verdict: partial
confidence: medium
status: answered
tags: [outbox, navsync, BB, bandera-roja, PEND-12]
---

## Resumen

Confirmado parcialmente: **NavSync de BB hoy procesa de forma
fiable solo las SALIDAs `IdTipoDocumento=3`** (>99% enviadas).
Los INGRESOs (`IdTipoDocumento` ∈ {6, 8}) **fueron procesados
historicamente**, pero el flujo se interrumpio: el ultimo INGRESO
con `enviado=1` es de **2023-09-18** y desde entonces no se ha
enviado ninguno mas (≈2.5 años sin procesar). Las 110,795 filas
pendientes resultan de la combinacion de (a) tipos que NavSync
**nunca** ha procesado (`IdTipoDocumento` ∈ {1, 12}) y (b) tipos
que se procesaron en 2022-2023 pero el flujo se cayo (`tipo` ∈ {6, 8}).

No tenemos evidencia desde el WMS para confirmar si la causa es
un cambio de SP en el cliente externo, una migracion del ERP, o
una decision de negocio (no hay rastro en SQL Agent ni en SPs
locales — ver Q-009).

## Hallazgos

### q1: edad de pendientes (todo el historico)

```
anio | mes | cnt
--- | --- | ---
2022 | 5 | 3997
2022 | 6 | 5165
2022 | 7 | 5000
... (acumulacion mensual continua) ...
2023 | 11 | 6437
2023 | 12 | 2786
2024 | 6 | 1
2025 | 9 | 4
2025 | 10 | 13
```

**Interpretacion**: 23 meses con miles de filas pendientes desde
mayo 2022 hasta dic-2023, luego casi cero. La caida en volumen
mensual coincide con el corte de procesamiento en sep-2023.

### q2: cuando se enviaron INGRESOs (los unicos 107)

Distribucion por mes y tipo doc (extraido del CSV completo):

```
anio-mes | IdTipoDocumento | cnt
--- | --- | ---
2022-05 | 8 | 2
2022-06 | 8 | 2
2022-07 | 8 | 13
2022-08 | 8 | 2
2022-09 | 8 | 2
2022-10 | 8 | 2
2022-11 | 8 | 9
2022-12 | 8 | 9
2023-01 | 8 | 12
2023-04 | 8 | 18
2023-05 | 8 | 1
2023-06 | 8 | 2
2023-07 | 6 | 4
2023-07 | 8 | 16
2023-08 | 6 | 12
2023-09 | 8 | 1
```

**Interpretacion**: Los 107 INGRESOs enviados se distribuyen entre
**mayo-2022 y septiembre-2023** (16 meses). El ritmo nunca fue alto
(maximo 18/mes) y se concentra en `IdTipoDocumento=8`. El
`IdTipoDocumento=6` solo aparece procesado en jul-ago 2023 (16 filas
unicas, todas con prefijo de pedido `RE-*`). **Despues del 2023-09-18:
0 INGRESOs enviados** en todo el historico hasta hoy (2026-04).

### q3: tipo doc pendiente vs enviado para INGRESOs

```
IdTipoDocumento | enviado | cnt
--- | --- | ---
1 | 0 | 22
6 | 0 | 94993
6 | 1 | 16
8 | 0 | 6901
8 | 1 | 91
12 | 0 | 8879
```

**Interpretacion**:
- `IdTipoDocumento=8`: 91 enviados / 6,901 pendientes (1.3%) — flujo
  vivo en 2022-23, hoy interrumpido.
- `IdTipoDocumento=6`: 16 enviados / 94,993 pendientes (0.017%) —
  procesado solo brevemente en jul-ago 2023.
- `IdTipoDocumento=12`: 0 enviados / 8,879 pendientes — **nunca**
  procesado.
- `IdTipoDocumento=1`: 0 enviados / 22 pendientes — **nunca**
  procesado.

### q6: SPs locales BB que tocan outbox

```
sp_name | modify_date
--- | ---
CLBD_PRC | 2023-12-09T23:20:17.730Z
CLBD_PRC_SIN_INV_INI | 2022-05-08T20:32:41.007Z
CLBD_PRC_BY_IDBODEGA | 2020-01-20T17:38:19.183Z
```

**Interpretacion**: Solo 3 SPs en BD BB tocan la tabla outbox y
ninguno actualiza `enviado=1` (todos hacen `INSERT` o calculo de
inventario). Esto **es consistente con** un writer externo que
actualiza `enviado=1`, pero no lo prueba (podria haber un proceso
en otra BD/servidor).

### q8 (extra contraste): SALIDAs por tipo doc

```
IdTipoDocumento | enviado | cnt
--- | --- | ---
1 | 0 | 115165
1 | 1 | 1
3 | 0 | 28
3 | 1 | 277309
4 | 0 | 29924
```

**Interpretacion**: SALIDAs `IdTipoDocumento=3` estan al 99.99%
procesadas (277,309/28). El resto de tipos (1 y 4) acumulan 145k
filas sin procesar. Esto refuerza que NavSync **hoy procesa
exclusivamente SALIDA tipo doc=3**.

## Conclusion

- Los 110k INGRESOs pendientes **no son una falla puntual**: son
  el resultado acumulado de (a) tipos que NavSync nunca ha procesado
  y (b) un flujo que se proceso parcialmente entre 2022-05 y
  2023-09 y luego se detuvo.
- **Causa raiz no determinable desde el WMS**: el writer es externo,
  no hay logs de NavSync accesibles, no hay job de SQL Agent. Hace
  falta inspeccionar el host donde corre NavSync (Q-009).
- Hipotesis razonable: el ERP cambio el origen de las recepciones
  (las consume directamente) y NavSync dejo de leer el outbox para
  INGRESOs. Necesita confirmacion del lado cliente.
- **Recomendacion operativa**: agregar un flag `procesar=0` o purgar
  filas con `tipo_transaccion='INGRESO'` mas viejas que N meses para
  no envenenar metricas de SLO basadas en `count(enviado=0)`.

## Anomalias detectadas (no pedidas)

- **115k SALIDAs `IdTipoDocumento=1` pendientes** — siguiente bandera
  roja, abre Q-010.
- **30k SALIDAs `IdTipoDocumento=4` pendientes** — mismo Q-010.
- 18 INGRESOs en abr-2023 sugieren un mini-batch retroactivo (ratio
  arriba de la media). Sin acceso a logs de NavSync no se puede
  confirmar.

## Sugerencia de follow-up

- Q-009: identificar binario y host del NavSync de BB (esta corriendo?).
- Q-010: devoluciones BB / SALIDAs tipo doc 1+4 — por que no se procesan.

## Notas del operador

Inicialmente este card afirmaba que los INGRESOs `enviado=1` se
concentraban solo en mayo-julio 2022 (lectura apresurada de las
primeras filas del CSV). Revisado el dataset completo, el rango
correcto es 2022-05 → 2023-09. Verdict bajado a `partial` y confianza
a `medium` porque la causa de la interrupcion en 2023-09 no es
verificable desde el WMS.
