# H29 explicado: el "44% de Pickeado terminal" en BECOFARMA es un corte real del flujo de despacho en marzo 2026

> Evidencia recolectada: 29-abr-2026 sobre `IMS4MB_BECOFARMA_PRD` (snapshot diagnostico restaurado el 28-abr-2026 por Erik).
>
> Estado: **diagnostico operativo de la productiva real, NO artefacto del snapshot**.

## Lo que se ve a primera vista

```
Distribucion estados de trans_pe_enc en BECOFARMA:
  Despachado    cnt= 4,411  agr=[2026-01-06..2026-04-20]  mod=[2026-01-06..2026-04-20]
  Pickeado      cnt= 3,802  agr=[2026-01-06..2026-04-27]  mod=[2026-01-06..2026-04-27]
  Pendiente     cnt=   282  agr=[2026-01-06..2026-04-22]
  Anulado       cnt=    22
  NUEVO         cnt=    18
  Verificado    cnt=     4  agr=[2026-01-16..2026-03-10]
```

44.3% de los pedidos estan en estado `Pickeado`. Si se asume que el flujo
normal es `NUEVO -> Pickeado -> Despachado` (con `Verificado` como paso
opcional intermedio), un porcentaje tan alto en `Pickeado` aparenta ser
un estado terminal de hecho — los pedidos quedan ahi y no transitan.

Eso es lo que reporto el evento H29 inicialmente.

## Lo que muestran los datos cruzados

### 1) Histograma semanal de creacion de pedidos por estado actual

```
semana    Pickeado  Despachado  Verificado  NUEVO
W10 (mar 1-7)     55       427           1      0
W11 (mar 8-14)   307       137           1      0
W12 (mar 15-21)  676         2           0      3
W13 (mar 22-28)  584         3           0      2
W14 (mar 29-abr 4) 234       0           0      0
W15 (abr 5-11)   500         2           0      0
W16 (abr 12-18)  523         1           0      1
W17 (abr 19-25)  685         1           0      1
W18 (abr 26-27)   28         0           0      0
```

### 2) Filas en `trans_despacho_enc` (la tabla destino del proceso)

```
trans_despacho_enc total = 4,897 filas
fec_min = 2026-01-06, fec_max = 2026-04-21

Distribucion por semana (mar-abr 2026):
  W10:  611
  W11:  182
  W12:    2
  W13:    8
  W14:    -
  W15:    3
  W16:    1
  W17:    2
```

### 3) Cruce: lo que se rompio

| Semana | Pedidos creados | Pasaron a Despachado | Filas en trans_despacho_enc |
|---|---|---|---|
| W10 | 482 | 427 | 611 |
| W11 | 444 | 137 | 182 |
| **W12** | **678** | **2** | **2** ← corte abrupto |
| W13 | 587 | 3 | 8 |
| W14-W17 | 234-685/sem | 0-2/sem | 0-3/sem |

## Hipotesis fuerte

A mediados de marzo 2026 (semana W11/W12) **se rompio o se detuvo el
proceso operativo que materializa el despacho** en BECOFARMA productiva.
Posibles causas (ordenadas por probabilidad):

1. **El SP `MarcarComoDespachado` (o equivalente) dejo de ejecutarse**
   por error transaccional, deploy fallido, deshabilitado por alguien.
2. **El modulo de verificacion de etiquetas** (`trans_verificacion_etiqueta`,
   `verificacion_estado`, `log_verificacion_bof`) que es exclusivo de
   BECOFARMA y aparenta ser un paso intermedio (`Pickeado -> Verificado
   -> Despachado`) **se rompio o quedo sin operadores asignados**.
   El estado `Verificado` solo tiene 4 filas en TODA la BD, todas con
   `fec_mod <= 2026-03-10`. Es decir, la verificacion tampoco ha
   corrido despues de mediados de marzo.
3. **El binario `SAPBOSync.exe` que corre en el server productivo de
   BECOFARMA** (confirmado por Erik que SI corre, ver L-015) puede
   haber dejado de procesar la cola hacia SAP B1 desde marzo, lo que
   generaria backlog en `i_nav_transacciones_out` (las 31,263 filas
   con `enviado=0` apuntan a esto) y, si el WMS espera ack del ERP
   antes de marcar Despachado, quedaria todo trabado en Pickeado.

Las 3 hipotesis convergen en el mismo periodo y son coherentes entre si.

## Lo que NO es

- **NO es bug del modelo de estados** (`Pickeado` no es un estado terminal
  por diseño — es un estado en transito que en operacion sana fluye a
  `Despachado` en horas).
- **NO es artefacto del snapshot del 28-abr-2026**. Si el snapshot
  hubiera atrapado pedidos "en vuelo" del ultimo dia, esperaria ver
  unas decenas de Pickeado de los ultimos 1-2 dias. Lo que vemos es
  **acumulacion sostenida de 7 semanas** (W12 hasta W18), con creacion
  normal de pedidos pero zero/casi-zero despachos.

## Recomendacion

1. Validar con Erik / equipo BECOFARMA si **conocian este corte** o si
   este analisis es la primera vez que lo ven explicitado. La copia
   diagnostica acaba de aterrizar; en la productiva pueden o no estar
   al tanto.
2. Si lo conocian: documentar en el brain la causa raiz que ya
   identificaron y archivar este H29 con esa explicacion.
3. Si NO lo conocian: **levantar como issue operativo critico** del
   cliente. 3,800 pedidos sin despachar acumulados en 7 semanas es
   bloqueante operativo (no se factura, no se libera stock reservado).
4. Independiente del resultado: **el modulo de verificacion de etiquetas
   y el flujo `Pickeado -> Verificado -> Despachado` necesitan
   monitoreo activo en la WebAPI .NET 10**, con dashboard de pedidos
   "varados" mas de N horas en cada estado.

## Evidencia consultable

Queries ejecutadas:
- Distribucion de estados con ventanas de fecha.
- Histograma semanal por estado (mar-abr 2026).
- Conteo de filas y rango de fechas en `trans_despacho_enc`.
- Inspeccion de `trans_verificacion_etiqueta` y `verificacion_estado`.

Todas reproducibles SELECT-only sobre `IMS4MB_BECOFARMA_PRD`. Snapshots
crudos en `brain/agent-context/sql-evidencia/2026-04-29-h29-corte-despacho.md`
(pendiente de archivar).
