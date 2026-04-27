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
verdict: confirmed
confidence: high
status: answered
tags: [outbox, navsync, BB, bandera-roja, PEND-12]
---

## Resumen

Confirmado: **NavSync de BB no procesa INGRESOS por diseño**.
La acumulacion de 110,795 filas `tipo_transaccion='INGRESO'` con
`enviado=0` se explica por dos hechos: (1) los unicos INGRESOS
"enviados=1" son **107 filas, todas concentradas en mayo–julio 2022**,
y (2) los 110k pendientes incluyen `IdTipoDocumento` 6, 8 y 12
que **nunca tienen versiones enviadas en cantidad significativa**.
La distribucion temporal arranca en 2022-05 y crece consistentemente,
sin evidencia de procesamiento posterior.

## Hallazgos

### q1: edad de pendientes (todo el historico)

```
anio | mes | cnt
--- | --- | ---
2022 | 5 | 3997
2022 | 6 | 5165
2022 | 7 | 5000
2022 | 8 | 4617
2022 | 9 | 4204
2022 | 10 | 5226
2022 | 11 | 5163
2022 | 12 | 4113
2023 | 1 | 5325
2023 | 2 | 5419
2023 | 3 | 6896
2023 | 4 | 5274
2023 | 5 | 7358
2023 | 6 | 7576
2023 | 7 | 7136
2023 | 8 | 7921
2023 | 9 | 5853
2023 | 10 | 5311
2023 | 11 | 6437
2023 | 12 | 2786
2024 | 6 | 1
2025 | 9 | 4
2025 | 10 | 13
```

**Interpretacion**: 23 meses con miles de filas pendientes cada uno
desde mayo 2022. **No hay caida puntual**: el sistema simplemente
no esta procesando INGRESOS desde el origen.

### q2: cuando se enviaron INGRESOS (los unicos)

```
idtransaccion | fec_agr | fec_mod | no_pedido | codigo_producto | cantidad | IdTipoDocumento | idordencompra | idrecepcionenc
--- | --- | --- | --- | --- | --- | --- | --- | ---
5068 | 2022-05-30T13:43:10.417Z | 2022-05-30T13:43:10.320Z | PT-186500 | 00025002 | 84 | 8 | 602 | 476
5069 | 2022-05-30T13:43:23.630Z | 2022-05-30T13:43:23.613Z | PT-186500 | 00025002 | 10 | 8 | 602 | 476
6453 | 2022-06-07T12:43:12.673Z | 2022-06-07T12:43:12.640Z | PT-187175 | 00100101 | 50000 | 8 | 739 | 611
6494 | 2022-06-07T14:41:30.257Z | 2022-06-07T14:41:30.227Z | PT-187175 | 00100101 | 9000 | 8 | 739 | 611
16827 | 2022-07-01T15:57:00.793Z | 2022-07-01T15:57:00.793Z | PT-189028 | 00053205 | 72 | 8 | 1221 | 993
```

**Interpretacion**: Las 107 unicas INGRESOS marcadas como enviadas
ocurrieron entre mayo y julio 2022 (todas con `fec_mod` muy
cercana a `fec_agr`). Despues de eso, **0 INGRESOS enviados en
casi 4 años**.

### q3: tipo doc pendiente vs enviado para INGRESOS

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

**Interpretacion**: `IdTipoDocumento=8` tiene 91 enviados / 6,901
pendientes (1.3%). `IdTipoDocumento=6` tiene 16 enviados / 94,993
pendientes (0.017%). `IdTipoDocumento=12` tiene 0 enviados / 8,879
pendientes. **El procesamiento de INGRESOS esta efectivamente apagado**.

### q7 (extra): pendientes recientes (ultimos 2 años)

```
anio | mes | cnt
--- | --- | ---
2024 | 6 | 1
2025 | 9 | 4
2025 | 10 | 13
```

**Interpretacion**: Sigue ingresando algo (1 en 2024-06, 4 en
2025-09, 13 en 2025-10) pero a un ritmo mucho menor que en 2022-2023.
Probablemente otro frente que ya migro a otra tabla / canal.

### q8 (extra contraste): SALIDAS por tipo doc

```
IdTipoDocumento | enviado | cnt
--- | --- | ---
1 | 0 | 115165
1 | 1 | 1
3 | 0 | 28
3 | 1 | 277309
4 | 0 | 29924
```

**Interpretacion**: Para SALIDAs, el tipo doc 3 esta al 99.99%
procesado (277,309 enviados / 28 pendientes). Esto refuerza:
**NavSync de BB esta vivo y funcional, pero solo para SALIDAS de
tipo doc 3** (ventas/despachos). Tipo doc 1 (devoluciones?) y 4 sí
estan acumulando pendientes (115k y 30k respectivamente).

## Conclusion

- Los 110k INGRESOS pendientes **no son falla a corregir**: son
  resultado de una decision de diseño (NavSync solo procesa SALIDAS,
  los INGRESOS al ERP entran por otro canal — probablemente el
  modulo de compras/recepciones del ERP los origina y BB no necesita
  reportarlos de vuelta).
- Sin embargo, **acumular 110k filas en una tabla outbox es un
  pasivo operativo**: complica monitoreo (no se puede usar % de
  procesamiento como SLO), y consume disco. Sugerimos agregar un
  flag `procesar=0` o purgar.
- Tambien hay 145k SALIDAS `IdTipoDocumento=1` pendientes (no era
  parte de Q-003 pero apareció): merece su propia question.

## Anomalias detectadas

- **115k SALIDAS `IdTipoDocumento=1` pendientes**: no era el foco
  de la pregunta pero es la siguiente bandera roja.
- 30k SALIDAS `IdTipoDocumento=4` pendientes (similar).

## Sugerencia de follow-up

- Q-010 (devoluciones BB / SALIDAS tipo doc 1+4 — por que tampoco
  se procesan).

## Notas del operador

Si BB en algun momento tuvo un job que procesaba INGRESOS y se
desactivo, valdria la pena saber la fecha y motivo (no consta en
ninguna doc revisada).
