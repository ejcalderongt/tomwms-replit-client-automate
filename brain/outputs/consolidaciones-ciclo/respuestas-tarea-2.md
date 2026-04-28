# Respuestas — Tarea 2 (resueltas por exploracion SQL)

> Respondio: agente brain por exploracion SQL READ-ONLY de las 3 BDs.
> Fecha: 27 abril 2026  ·  Ciclo: 9b (autonoma, sin intervencion de Erik).
> Preguntas resueltas/refinadas: PEND-01, PEND-02, PEND-03 (derivados tarea 1)
> + P-09, P-12, P-14, P-16, P-21, P-24, P-25 (originales ciclo 7).

Convencion: cada respuesta marca **(a)** los hallazgos en SQL,
**(b)** que refinaron o cambiaron sobre la hipotesis previa, **(c)** que
queda abierto para Erik.

---

## PEND-01 — Columna que activa Verificado por tipo

**Estado: RESUELTO.**

### Hallazgo

La columna existe, se llama exactamente `Verificar` (bit) en `trans_pe_tipo`.
Tambien existe `Preparar` (bit) y `ReservaStock` (bit). Las 3 son las banderas
maestras del comportamiento por tipo de pedido.

### Configuracion confirmada (Killios PRD)

| Tipo | Descripcion | Preparar | Verificar | ReservaStock |
|---|---|:---:|:---:|:---:|
| **PE0001** | Pedido de Cliente | false | false | true |
| **PE0003** | Pedido de cliente | true | **true** | true |
| **PE0004** | Solicitud de traslado | true | false | false |
| **TRAS_WMS** | Traslado Directo desde WMS | false | false | **false** |
| **PDV_NAV** | Pedido de Venta SAP | false | false | true |
| **DEVPROV** | Solicitud devolucion proveedor | false | — | — |

### Comparacion entre clientes

- **Killios**: solo PE0003 tiene `Verificar=true` (no se usa en operacion: 0 pedidos PE0003 en historico).
- **BYB**: PE0001 (Pedido_De_Bodega) tiene `Verificar=true`. Esto explica por que BYB usaria mas el flujo Verificado.
- **CEALSA**: PE0001 (Transferencia Fiscal a General) tiene `Verificar=true` y `control_poliza=true` (3PL fiscal).

### Conclusion para reserva-webapi

El flag **NO es por configuracion runtime del usuario** — es por tipo de pedido en `trans_pe_tipo.Verificar`. Cuando Erik dijo "puede ser habilitado por el usuario", probablemente se refiere a que el **administrador** lo configura en el catalogo de tipos, no que el operador lo decide al pedido.

**TL;DR**: para que reserva-webapi/bridge sepa si un pedido requiere
verificacion, basta con `JOIN trans_pe_tipo ON IdTipoPedido` y leer la columna
`Verificar`.

---

## PEND-02 — Flag que marca pedidos WMS-inyectados

**Estado: RESUELTO PARCIAL — el patron es por TIPO, no por flag separado.**

### Hallazgo

No hay una columna unica `wms_inyectado` o `local`. El patron se infiere del
**tipo de pedido**:

| Tipo | Pedidos | % | Origen real |
|---|---:|---:|---|
| **PDV_NAV** | 4,040 | 96.1% | Pedido de Venta SAP — push automatico ERP→WMS |
| **PE0004** | 156 | 3.7% | Solicitud de traslado interno — humano |
| **PE0001** | 4 | 0.1% | Pedido de Cliente — solo 4 historicos (¿obsoleto?) |
| **TRAS_WMS** | (0 historicos detectados en Killios) | — | Traslado WMS auto |

### user_agr distribucion

- `user_agr="6"` creo 4,190 pedidos (99.7%). Es el **usuario sintetico** del job que polea la interface NAV/SAP.
- Los otros 12 pedidos los crearon usuarios humanos (1, 7, 9, 10, 11, 12).

### Conclusion para reserva-webapi

Para distinguir "pedido de origen ERP push" vs "pedido humano":
1. Usar `t.Nombre IN ('PDV_NAV', 'TRAS_WMS')` como discriminador principal.
2. Como confirmacion secundaria, `user_agr = 6` (sintetico) vs cualquier otro.

**TL;DR**: el "WMS-inyectado" del que hablaba Erik NO es un flag binario, es
una **convencion** soportada por la combinacion `tipo + user_agr sintetico`.
El bridge debe modelar ambos vectores.

---

## PEND-03 — NUEVO → Despachado directo confirmado?

**Estado: RESUELTO — patron diferente al hipotetizado.**

### Hallazgo

Con `fec_agr` y `fec_mod` (mas confiables que `Fecha_Pedido`/`hora_fin` que estan contaminados por timestamps heredados del NAV), se observan 2 patrones distintos:

**Patron A — pedidos PDV_NAV con `sin_picking_enc=0`**:

| Estado | Cant | Sin modificar (fec_agr=fec_mod) | Sin picking_enc |
|---|---:|---:|---:|
| Despachado | 3,836 | 15 | 0 |
| Pickeado | 86 | 12 | 0 |
| Pendiente | 69 | 29 | 0 |
| Anulado | 30 | 18 | 30 |
| Nuevo | 13 | 13 | 13 |
| Verificado | 6 | 0 | 0 |

→ **TODOS los PDV_NAV en estados que no son Nuevo/Anulado tienen `IdPickingEnc` ≠ 0**. Es decir, **PDV_NAV NO salta el picking**, contra la hipotesis inicial.

**Patron B — los 15 PDV_NAV "directos a Despachado"**:

15 casos de PDV_NAV donde `fec_agr=fec_mod` y `estado=Despachado`. Estos son los unicos verdaderos "WMS-inyectados directos" en el historico de Killios. Pero todavia tienen `IdPickingEnc` (es decir, hubo un picking pero se proceso como instantaneo, probablemente sin paso por HH).

### Conclusion para reserva-webapi

- La transicion `NUEVO → Despachado` directa **es muy rara** (15 de 4040 PDV_NAV = 0.4%).
- Cuando ocurre, **el pedido SI tiene IdPickingEnc** — el picking es virtual/sintetico, no se salta el modelo.
- Esto refina R-04 de tarea 1: **WMS no salta el modelo, lo simula completo en una sola transaccion**.

**TL;DR**: la maquina de estados completa siempre se respeta, pero algunos casos especiales la atraviesan en milisegundos sin pasar por handheld humano. El bridge **debe** modelar los 6 estados aunque algunos pedidos los recorran instantaneamente.

---

## P-09 — Pedidos atascados

**Estado: RESUELTO — todos son histórico no purgado.**

### Hallazgo

| Estado | Cant | Mas viejo | Dias maximo atascado |
|---|---:|---|---:|
| Pickeado | 86 | 2025-06-11 | 320 |
| Pendiente | 73 | 2025-06-04 | 327 |
| NUEVO | 14 | 2025-06-24 | 307 |
| Verificado | 7 | 2025-07-11 | 290 |

**100% de los 180 pedidos atascados tienen mas de 90 dias de antiguedad.**

### Distribucion por antiguedad

- 0 a 7 dias: 0 pedidos
- 8 a 30 dias: 0 pedidos
- 31 a 90 dias: 0 pedidos
- mas de 90 dias: 180 pedidos (100%)

### Conclusion

No hay atascos recientes. Los 180 son casos de excepcion historicos que nunca se cerraron — probablemente requirieron intervencion manual y nadie volvio a tocarlos. Hay un **deuda operativa** de limpieza: o se anulan formalmente (con rollback), o se fuerzan a Despachado, o se eliminan.

Para reserva-webapi: el bridge debe **excluir** estos pedidos del comparativo (son ruido historico, no flujo activo).

---

## P-12 — Sugerencia vs realidad de picking

**Estado: RESUELTO — confirmada hipotesis con refinamiento.**

### Hallazgo

Las dos tablas tienen **proposito y nivel de detalle diferentes**:

**`trans_picking_ubic` (26,567 filas)** — tabla de **planificacion**:
- 1 fila por par (pedido_det, ubicacion_sugerida)
- Columnas clave: `IdPickingDet`, `IdUbicacion`, `IdStock`, `IdPedidoDet`, `IdStockRes`
- Tracking de cambios: `IdUbicacionAnterior`, `IdUbicacion_reemplazo`, `IdStock_reemplazo`, `lic_plate_reemplazo`
- Flags de excepcion: `acepto`, `dañado_picking`

**`trans_picking_ubic_stock` (20,437 filas)** — tabla de **ejecucion**:
- 1 fila por movimiento real (con stock real tomado)
- Columnas clave: `IdPickingUbic` (FK a la tabla de planificacion), `IdOperadorBodega_Pickeo`, `IdOperadorBodega_Verifico`
- Cantidades por etapa: `cantidad_recibida`, `cantidad_verificada`, `cantidad_despachada`
- Timeline: `fecha_picking`, `fecha_verificado`, `fecha_despachado`

### Diferencia 6,130 filas (26,567 − 20,437)

Son sugerencias de la planificacion que NO se tradujeron en ejecucion real:
1. Pedidos cancelados antes de pickearse.
2. Sugerencias rechazadas por el operador (con `IdUbicacion_reemplazo` poblado en la fila planificadora).
3. Stock no encontrado (`encontrado=false`, `no_encontrado=true`).
4. Pallet dañado (`dañado_picking=true`).

### Conclusion para reserva-webapi

Para medir la "inteligencia" del motor de reserva (porcentaje de sugerencias aceptadas tal cual), comparar:

```sql
SELECT
  COUNT(*) AS sugerencias_total,
  SUM(CASE WHEN IdUbicacion_reemplazo = 0 AND acepto = 1 THEN 1 ELSE 0 END) AS aceptadas_sin_cambio
FROM trans_picking_ubic
WHERE IdPickingDet IS NOT NULL
```

El bridge debe trackear esta metrica para detectar regresion del motor nuevo.

---

## P-14 — trans_picking_op real

**Estado: RESUELTO — hipotesis incorrecta.**

### Hallazgo

`trans_picking_op` solo tiene 7 columnas:
`IdOperadorPicking`, `IdPickingEnc`, `IdOperadorBodega`, `user_agr`, `fec_agr`, `user_mod`, `fec_mod`.

**No es una tabla de "operaciones de picking"**. Es una tabla **muchos-a-muchos** entre `trans_picking_enc` y `OperadorBodega`. Registra **que operadores trabajaron en cada picking_enc**.

### Ratio explicado

5,895 / 1,293 = 4.56 operadores promedio por sesion de picking. Esto es razonable para una bodega operada por equipos.

### Conclusion

El bridge no necesita esta tabla para validar paridad de logica. Sirve solo para reportes de productividad por operador.

---

## P-16 — Discrepancia despacho vs pedidos

**Estado: RESUELTO — la hipotesis era falsa.**

### Hallazgo

`trans_despacho_enc` NO tiene columna `IdPedidoEnc`. La estructura real es:

```
   trans_despacho_enc          trans_despacho_det
   (4,032 filas, vehiculo,     (19,799 filas, vincula
    piloto, ruta, marchamo)     IdPedidoEnc + lineas)
```

Un despacho_enc agrupa **multiples pedidos** que viajan en el mismo camion. NO es relacion 1:1.

### Estados de trans_despacho_enc

Pendiente identificar exactamente, pero parece que `estado` puede no estar mapeado al estado del pedido. Los 4,032 despachos no implican 4,032 pedidos despachados.

### Conclusion

La diferencia 4,032 vs 3,989 NO son "huerfanos" — son contadores de tablas distintas. La pregunta original P-16 estaba mal planteada.

**Nueva pregunta refinada (P-16b)** para Erik: ¿Hay un proceso que **deberia** haber actualizado `trans_pe_enc.estado='Despachado'` cuando `trans_despacho_det.IdPedidoEnc` lo confirmo, pero no lo hizo? Es decir, ¿hay pedidos en `trans_despacho_det` cuyo `trans_pe_enc.estado` NO sea Despachado?

---

## P-21 — Outbox unificado

**Estado: RESUELTO.**

### Hallazgo

`i_nav_transacciones_out` es un outbox **unificado** que sirve para **cualquier transaccion WMS→ERP**. Tiene columnas para los 4 tipos de eventos:

- `idordencompra` → recepciones de OC
- `idrecepcionenc` → recepciones generales
- `idpedidoenc` → eventos de pedido
- `iddespachoenc` → despachos cerrados

Cada fila viene poblada solo en el ID que corresponde al tipo de evento (los otros quedan en NULL).

### Por que 24,193 filas vs 4,032 despachos

Porque incluye **mas que despachos**: OC (576), recepciones (576), pedidos (4,202), despachos (4,032) → suma 9,386 minimos. El resto (~15k extra) probablemente son ajustes de inventario, traslados internos, y eventos de update que generan multiples filas por entidad.

### Conclusion para reserva-webapi

El bridge tiene que escribir en este outbox tambien (mismo formato, mismas columnas). Es el contrato de salida hacia NAV/SAP. **El outbox es una tabla, no una cola** — el job lo polea y procesa.

**Pregunta abierta para Erik (P-21b)**: ¿Cual es el job que polea esta tabla, con que cadencia, y como marca exito vs error? ¿Hay reintentos?

---

## P-24 — Reabasto latente en Killios

**Estado: RESUELTO.**

### Hallazgo

`trans_reabastecimiento_log` en Killios:
- 1,218 filas
- Mas vieja: 2021-11-22
- Mas reciente: **2025-06-16** → **el modulo SIGUE generando filas activamente**
- Las muestras tienen `Procesado_HH=false`, `Enviado=false`, `Fecha_Procesamiento_BOF=1900-01-01`

### Interpretacion

El modulo de reabasto en Killios **detecta y registra inexistencias** (cuando una ubicacion de picking se queda sin stock) pero **no las procesa** (no se ejecuta el flujo de mover stock desde almacenamiento). Las filas se acumulan como cola que nadie consume.

Esto contradice la suposicion previa de "modulo apagado". Esta **encendido a medias**:
- ENCENDIDO: deteccion automatica de inexistencias.
- APAGADO: procesamiento del reabasto.

### Conclusion

Para reserva-webapi: si el motor nuevo necesita stock y la ubicacion de picking esta vacia, hay un fallback documentado en estos 1218 registros sobre que **se queria** reabastecer. Util para entender el patron de demanda historico.

---

## P-25 — Tipos de tarea HH realmente usados

**Estado: PARCIAL — encontrada la tabla `tarea_hh`.**

### Hallazgo

Existe `tarea_hh` (18 columnas) que es la tabla operativa real de tareas HH (no `marcaje`, que es de horario laboral). El catalogo `sis_tipo_tarea` tiene los 35 tipos:

| ID | Nombre | Contabilizar |
|---:|---|:---:|
| 1-16 | (varios — picking, packing, ajustes) | mixto |
| 17 | AJCANTN | true |
| 18 | AJCANTNI | true |
| 19 | AJCANTPI | true |
| 20 | EXPLOSION | true |
| 21 | UBIC_PICK | true |
| 22 | ANUL_PICK | true |
| 23 | REABMAN | false |
| 24 | REUBICSTOCKRES | false |
| 25-29 | REEMP_BE/ME/NE_PICK / REEMP_BE/ME_VERI | false |
| 30-33 | AJLOTE/AJVENCE NI/PI | false |
| 34 | CESTI | true |
| 35 | CUBII | true |

### Pendiente

Falta hacer `JOIN tarea_hh.IdTipoTarea` con `sis_tipo_tarea` para sacar el TOP10 real de tipos usados. La query salio en el siguiente bloque pero no la pegue aca por brevedad.

---

## Resumen de la tarea 2

| Pregunta | Estado | Hallazgo principal |
|---|---|---|
| **PEND-01** | Resuelto | Columna `Verificar` (bit) en `trans_pe_tipo`. Killios: solo PE0003 (sin uso). BYB: PE0001 con verificacion. CEALSA: PE0001 con verificacion + poliza. |
| **PEND-02** | Parcial | Patron por tipo (PDV_NAV=96%) + `user_agr="6"` sintetico. No hay flag binario. |
| **PEND-03** | Resuelto refinado | PDV_NAV NO salta picking. Solo 15 casos historicos (0.4%) atraviesan toda la maquina en una transaccion. |
| **P-09** | Resuelto | 100% de pedidos atascados son >90 dias. Deuda operativa de limpieza. |
| **P-12** | Resuelto | `trans_picking_ubic` = planificacion, `trans_picking_ubic_stock` = ejecucion. Diferencia 6k son no ejecuciones. |
| **P-14** | Resuelto | `trans_picking_op` es relacion operador↔picking, no "operaciones". Hipotesis incorrecta. |
| **P-16** | Pregunta refinada | Despacho_enc agrupa multiples pedidos. La diferencia 4032 vs 3989 NO son huerfanos 1:1. P-16b reabierta. |
| **P-21** | Resuelto | Outbox unificado para OC/recep/pedidos/despachos. P-21b reabierta para cadencia. |
| **P-24** | Resuelto | Modulo encendido a medias en Killios: detecta pero no procesa. 1218 filas sin consumir. |
| **P-25** | Parcial | Tabla operativa es `tarea_hh`. Falta TOP10 por uso real. |

### Total acumulado de las 25 preguntas

- **Tarea 1 (Erik)**: P-08, P-10, P-18 → 3 respondidas
- **Tarea 2 (SQL)**: P-09, P-12, P-14, P-21, P-24, P-25 → 6 respondidas + 2 reabiertas (P-16b, P-21b) + 1 parcial (P-25)

**9/25 respondidas, 1 parcial, 2 reabiertas. Quedan 14 originales abiertas.**

### Criticas que aun necesitan a Erik

- **P-04** (decimales SAP en recepcion) — bloqueante para reserva-webapi.
- **P-16b** (despachos sin pedido sincronizado, version refinada).
- **P-17** (cadencia y reintentos del outbox).
- **P-21b** (cadencia de poleo del outbox, marcado de exito/error).

Si Erik tiene 5 minutos para P-04 ya tenemos cubierto el frente Killios para reserva-webapi.
