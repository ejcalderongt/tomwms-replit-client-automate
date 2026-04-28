# Respuestas — Tarea 1

> Respondio: Erik Calderon  ·  Fecha: 27 abril 2026  ·  Ciclo: 9
> Preguntas respondidas: P-08, P-10, P-18 (3 de 25)

Convencion: cada respuesta incluye **(a)** la cita literal de Erik y **(b)** las
notas tecnicas derivadas (consecuencias para reserva-webapi, deuda detectada,
proximos pasos). Las preguntas originales viven en `preguntas-ciclo-7.md` y
ahora estan marcadas como respondidas con link a este documento.

---

## P-08 — Transiciones permitidas en la maquina de estados de pedido

**Respondida.** Estado: **CANONICO**.

### Cita literal de Erik

> NUEVO cuando se crea o se envia via ERP. Pendiente si ya se le asigno picking
> pero el mismo esta en proceso. Pickeado, ya el picking concluyo. Verificado,
> ya fue concluido el proceso de verificacion en HH (puede pasar a despachado).
> Anulado: cualquier momento antes de ser despachado, rollback de inventario
> importante asociado.

> NUEVO → Anulado: podria, pero generalmente no pasa. Hay un mecanismo en la
> forma de pedido para prevenir que hayan pedidos sin lineas, entonces
> generalmente si el pedido no tiene lineas se elimina y solo se anula bajo
> demanda y no solo pasa a estado anulado cuando tiene stock reservado
> previamente.

> NUEVO → Pickeado directo (saltarse Pendiente): si puede, para transacciones
> que el mismo WMS inyecta puede ser que por proceso se simule un picking y un
> despacho porque el traslado es entre bodegas virtuales que estan en un mismo
> espacio fisico. En consecuencia WMS podria simular un pedido, insertarlo en
> cualquier otro estado.

> Verificado es opcional. Puede ser habilitado por el usuario. Puede estar
> previamente definido en base al tipo de pedido.

### Definiciones canonicas (para el state-machine)

| Estado | Significado canonico | Disparador |
|---|---|---|
| **NUEVO** | Pedido creado, sin picking asignado todavia | Insert manual desde BackOffice o push desde ERP (SAP/NAV) |
| **Pendiente** | Picking asignado y en proceso | Asignacion del picker (manual o automatica) |
| **Pickeado** | Picking concluido en HH, falta verificacion o despacho | Confirmacion final del marcaje en HH |
| **Verificado** | Verificacion HH concluida — opcional, puede saltarse | Confirmacion del operador de packing/verificacion |
| **Despachado** | Cerrado, push al ERP completado | Confirmacion del despacho + outbox NAV/SAP exitoso |
| **Anulado** | Cancelado antes de despachar — con rollback de stock_res si habia reserva | Cancelacion explicita del usuario (no automatica por sistema) |

### Matriz de transiciones permitidas — confirmada

```
              NUEVO   Pend.   Pick.   Verif.  Desp.   Anul.
  desde:      -----   -----   -----   -----   -----   -----
  (insert) -> [SI]    -       (*)     -       -       -
  NUEVO    -> -       SI      [WMS]   -       -       (raro)
  Pendiente-> -       -       SI      -       -       SI
  Pickeado -> -       -       -       OPC     SI      SI
  Verif.   -> -       -       -       -       SI      SI
  Despach. -> -       -       -       -       -       NO  *terminal*
  Anulado  -> -       -       -       -       -       -   *terminal*

  Leyenda:
    [SI]   = transicion canonica
    [WMS]  = solo cuando el WMS inyecta el pedido (traslados entre bodegas virtuales)
    OPC    = opcional, depende de configuracion (flag por usuario o por tipo de pedido)
    SI     = posible y observado en produccion
    (raro) = posible pero requiere tener stock reservado y orden explicita del usuario
    -      = transicion no permitida
    (*)    = NUEVO → Pickeado posible solo si lo inyecta WMS (no operador humano)
```

### Notas tecnicas para reserva-webapi

1. **Anulado NO es automatico**: requiere accion explicita del usuario. Si
   reserva-webapi llegara a anular automaticamente por timeout o fallo de
   reserva, **se desviaria del comportamiento productivo**. Mejor dejarlo
   en `NUEVO` con flag de error y que el humano decida.

2. **Rollback de stock_res**: cuando un pedido pasa a Anulado **debe** liberar
   las filas correspondientes en `stock_res`. Si el bridge encuentra pedidos
   `Anulado` con `stock_res` activo, es bug del legacy o del nuevo motor —
   ambos casos hay que reportarlos.

3. **Pedidos sin lineas**: el form los **elimina** (no los anula). Esto explica
   por que el conteo de `Anulado` (33) es relativamente bajo — los abortos
   tempranos desaparecen sin dejar rastro de estado.

4. **Pedidos inyectados por WMS**: este es el caso que justifica
   `NUEVO → Pickeado` directo. Son pedidos sinteticos para mover stock entre
   bodegas virtuales en un mismo espacio fisico (ej. BOD7 ↔ bodega real en
   Killios). reserva-webapi debe poder reproducir este atajo sin romper la
   maquina general de estados.

5. **Verificado como flag dual**: la opcionalidad puede venir de **dos lugares**:
   - Usuario lo activa ad-hoc en runtime.
   - Tipo de pedido lo trae predefinido (campo a confirmar en `trans_pe_tipo`).

   Esto explica los 7 `Verificado` historicos: probablemente son los que
   tuvieron el flag activo. Pendiente: identificar la columna exacta.

---

## P-10 — Significado de LLR (Llamado Luego de Reserva)

**Respondida.** Estado: **CANONICO**.

### Cita literal de Erik

> Llamado Luego de Reserva. Fue la forma que encontre para recordarme que aunque
> no me gustaba, era lo mas facil para no generar mas codigo: una llamada
> recursiva.

> Es correcto, [los casos #28, #29, #31] son casos en los que el stock necesita
> ser procesado de forma automatica por WMS de alguna forma en particular o son
> el resultado de querer reservar lo que WMS "modifico" durante el proceso de
> reserva, por ejemplo se convirtieron cajas a unidades, las unidades se movieron
> de ubicacion, etc.

> No mas [documentacion en el VB] de la que aparece en la documentacion de estos
> CASE_#X.

### Definicion canonica

**LLR = Llamado Luego de Reserva**. Es el patron implementado por Erik en el
motor legacy donde, despues de ejecutar la reserva principal de un caso
(`CASO_#20`, `#23` o `#24`), el motor se **llama recursivamente a si mismo**
con un caso secundario (`LLR_CASO_#28`, `#29` o `#31`) para procesar el stock
que **fue modificado durante la reserva original**.

Casos tipicos donde se dispara la LLR:

- **Conversion de unidades**: la reserva tomo cajas y necesita reservar las
  unidades resultantes despues de "explotarlas".
- **Movimientos internos**: las unidades fueron movidas de ubicacion durante
  el proceso de reserva (ej. de un pallet abierto a una posicion de picking) y
  hay que reservar la version "post-movimiento".
- **Procesamiento automatico WMS**: el motor decide automaticamente que tiene
  que hacer una operacion adicional (consolidacion, pickup desde otra ubicacion).

### Mapeo confirmado

```
   CASO PRINCIPAL    →  LLR DERIVADO
   --------------       --------------
   CASO_#20          →  LLR_CASO_#28
   CASO_#23          →  LLR_CASO_#29
   CASO_#24          →  LLR_CASO_#31
```

(El mapeo X→X+8 no es accidental; el offset numerico parece intencional pero
no esta documentado el por que. Pendiente confirmar si hay mas pares latentes.)

### Notas tecnicas para reserva-webapi

1. **El patron de recursion debe portarse explicitamente**. Si reserva-webapi
   ejecuta solo el caso principal sin disparar la LLR cuando corresponde,
   **dejara stock huerfano sin reservar** y los pedidos saldran incompletos.

2. **No hay mas documentacion**: lo que ves en los CASE del SP legacy es todo
   lo que hay. Toca leer el VB linea por linea o reconstruirlo desde los logs
   de `trans_pe_det_log_reserva` con sufijo `LLR`.

3. **Erik no ama el patron** ("aunque no me gustaba, era lo mas facil"): esto
   es señal de que reserva-webapi puede explorar implementaciones alternativas
   que no usen recursion (ej. cola de tareas pendientes despues de la reserva
   principal). El bridge debe verificar **resultado**, no **implementacion**:
   si el stock final reservado coincide, da igual si fue por recursion o
   por queue.

4. **No usar el nombre `LLR` en el motor nuevo**: es un acronimo personal de
   Erik. En reserva-webapi conviene renombrar a algo descriptivo como
   `PostReservationProcessing` o `SecondaryReservationPass`. Mantener `LLR`
   solo en logs comparativos contra el legacy.

---

## P-18 — TRAS_WMS sin reserva: como garantiza disponibilidad

**Respondida.** Estado: **PARCIAL — con deuda tecnica detectada**.

### Cita literal de Erik

> TRAS_WMS es una transaccion de transferencia interna para WMS, es decir
> alguien decide que stock se mueve por diferentes criterios, puede ser stock
> especifico, reserva por wms, luego esto se "transfiere" a una bodega que
> tambien existe o esta configurada en WMS, en consecuencia ya la reserva se
> garantizo mediante proceso empirico o discrecional o por flujo de WMS.

> Sin embargo tienes razon, me parece que no se esta validando especificamente
> esa bandera en el flujo.

> Para mejorar tu contexto, esa bandera estaba intencionada para poder recibir
> documentos en WMS, que fueran como un "bolson/bucket" de pedidos, y
> eventualmente se ejecutara un proceso en batch de abastecimiento de los
> mismos en base a criterios de abastecimiento. Por ejemplo, en el futuro se
> podria querer que de X producto se priorice 50% para los CD'S (bodegas
> internas de WMS), 30% nuevos clientes y 20% calidad y merma.

> Entonces en base a esa politica se podria mejorar/optimizar la demanda de
> producto en la bodega pero habria que tener la configuracion en el cliente,
> o en las bodegas origenes y destinos quiza para afinar bien la transicion
> y consumo entre ellas. Esto tambien puede servir para establecer en base a
> peticiones durante un periodo de tiempo, una necesidad de reserva o
> proyeccion de demanda.

### Definicion canonica

**TRAS_WMS** = transferencia interna entre bodegas configuradas en WMS, donde
el stock movido **ya fue reservado o seleccionado por proceso previo** (manual,
discrecional, o por flujo upstream del propio WMS). El motor de reserva no
se invoca porque, conceptualmente, **la reserva ya ocurrio antes**.

### Hallazgo tecnico (deuda detectada)

> **DEUDA-001**: La bandera `trans_pe_tipo.ReservaStock=NO` en TRAS_WMS **no
> esta siendo validada explicitamente** en el flujo del motor (Erik confirma:
> "tienes razon, me parece que no se esta validando especificamente esa
> bandera"). Hoy funciona por convencion implicita: el operador o el flujo
> upstream se asume que ya reservo. Si alguien crea un TRAS_WMS sin reserva
> previa, el motor lo dejaria pasar sin alarma.
>
> **Riesgo**: TRAS_WMS sin reserva previa podria mover stock que esta
> reservado por otro pedido, generando inconsistencia (doble reserva).
>
> **Probabilidad observada**: baja en operacion normal (ningun caso reportado),
> pero el riesgo existe.
>
> **Recomendacion para reserva-webapi**: validar la bandera explicitamente.
> Si `ReservaStock=NO`, exigir que el pedido venga con un flag/campo que
> indique de que proceso vino la reserva previa (idempotencia). Si no viene,
> rechazar o marcar para revision manual.

### Vision futura (NO implementada — capacidad latente)

Erik documenta una intencion arquitectonica que **NO esta implementada hoy**
pero que TRAS_WMS habilita conceptualmente:

```
   +------------------------------------------------+
   |  TRAS_WMS hoy:                                 |
   |  "transfiere stock ya reservado entre bodegas" |
   +------------------------------------------------+
                       |
                       v
   +------------------------------------------------+
   |  TRAS_WMS futuro (intencion original):         |
   |  "bolson/bucket de pedidos para abastecimiento |
   |   batch con politicas de prorrateo"            |
   |                                                |
   |  Politica ejemplo (Erik):                      |
   |    Producto X →                                |
   |      50% CDs (bodegas internas WMS)            |
   |      30% nuevos clientes                       |
   |      20% calidad y merma                       |
   +------------------------------------------------+
```

**Capacidades requeridas para activarlo en el futuro**:

1. Configuracion de politica por cliente y/o por bodega origen/destino.
2. Job batch que procese el bolson aplicando la politica.
3. Proyeccion de demanda en base a peticiones historicas durante un
   periodo de tiempo (analytics).
4. Validacion de capacidad disponible en bodegas destino.

### Notas tecnicas para reserva-webapi

1. **Comportamiento minimo (paridad con legacy)**: aceptar TRAS_WMS sin
   pasar por motor de reserva, asumiendo reserva previa. **Pero loggear
   la asuncion** para que sea visible en auditoria.

2. **Mejora propuesta sin romper paridad**: implementar la validacion explicita
   de la bandera con flag de bypass declarado. Esto es agregar seguridad sin
   romper el flujo legacy.

3. **No implementar el bolson/bucket en esta fase**: es vision futura, no
   requisito. Pero **dejar el codigo extensible** para que la politica de
   prorrateo se pueda enchufar despues sin reescribir TRAS_WMS.

4. **Tag para tests del bridge**: marcar TRAS_WMS como "casos de paridad
   permisiva" — no esperar que el bridge valide la reserva previa, solo
   que el resultado del movimiento sea identico al legacy.

---

## Resumen de la tarea

| Pregunta | Estado | Hallazgo principal |
|---|---|---|
| **P-08** | Canonica | Maquina de estados confirmada con 6 transiciones documentadas. Anulado requiere accion explicita; pedidos inyectados por WMS pueden saltarse Pendiente; Verificado es opcional configurable. |
| **P-10** | Canonica | LLR = Llamado Luego de Reserva (recursion del motor). Mapeo: #20→#28, #23→#29, #24→#31. Patron a portar pero permitido reimplementar sin recursion en webapi. |
| **P-18** | Parcial + Deuda | TRAS_WMS asume reserva previa pero **no valida la bandera** (DEUDA-001). Vision futura: bolson/bucket con politicas de prorrateo (no implementado). |

### Documentos actualizados en este ciclo

- `state-machine-pedido.md` → version 2 con matriz de transiciones confirmada.
- `process-map.md` → seccion "Traslado interno" ampliada con vision futura TRAS_WMS.
- `preguntas-ciclo-7.md` → marcadas P-08, P-10, P-18 como respondidas.

### Siguientes preguntas con prioridad alta (esperan respuesta)

P-04 (decimales SAP), P-12 (sugerido vs realizado picking), P-16 (discrepancia
despacho vs estado), P-17 (outbox), P-15 (Verificado opcional — necesario para
identificar la columna que lo activa por tipo de pedido).
