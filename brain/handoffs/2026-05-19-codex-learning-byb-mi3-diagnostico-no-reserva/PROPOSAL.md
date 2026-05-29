---
slug: 2026-05-19-codex-learning-byb-mi3-diagnostico-no-reserva
agente: codex-local
fecha: 2026-05-19T18:40:00-06:00
cliente: BYB
ambiente: QA
repo_afectado: TOMWMS
branch: dev_2028_merge
area: BOF / reserva MI3 / diagnostico operativo
relacionado:
  - 2026-05-19-codex-learning-byb-mi3-reserva-umbas
---

# Propuesta para Brain Keeper

## Resumen

Durante la validacion de `EA-153305` / `PA-172112`, la reserva MI3 dejo de
colgarse y el proceso completo llego a crear picking. La pantalla de
existencias mostro lineas verdes con reserva correcta y lineas rojas con el
resultado generico:

```text
No se pudo completar la reserva.
```

El hallazgo nuevo es que ese resultado ya no basta operativamente. Para
confirmar si una no-reserva es correcta, el usuario necesita saber si realmente
no hay existencia aplicable o si hay existencia fisica que queda excluida por
una regla de negocio: FEFO, vencimiento, ubicacion no picking, reservas previas,
presentacion, ubicacion obligatoria de cliente, talla/color, etc.

## Mapa Brain actual

Brain ya mapea la topologia del flujo, aunque no la semantica fina de motivos:

- `stock_res`: tabla central de reservas.
- `VW_Stock_Res_Pedido` y vistas de stock reservado: lectura operativa de
  reserva por pedido.
- `trans_pe_det_log_reserva`: bitacora historica por linea de pedido.
- `log_error_wms`: bitacora tecnica/operativa usada por el proceso.
- `I_nav_ped_traslado_det.Process_Result`: resultado visible para interface.
- `clsLnI_nav_ped_traslado_enc_Partial.vb`: capa NAV/MI3 que muestra el
  resultado por linea.
- `clsLnStock_res_Partial.vb`: motor legacy `Reserva_Stock_From_MI3`.

Brain sirve para ubicar dependencias y writers; la mejora pendiente es
convertir los puntos de decision del motor de reserva en un diagnostico de
negocio estable.

## Evidencia del caso

Documento/flujo:

- Envio NAV: `EA-153305`
- Pedido WMS: `37320`
- Picking creado: `PA-172112`
- Resultado operativo: proceso finalizo en ~48 segundos.
- Traza MI3: `reserva-mi3-20260519-174925-533-13124-e2481f68` y trazas
  siguientes `20260519-1756*`.

Ejemplo observado:

- `00025004`, linea `50000`, solicitud original `1.291667 CJ`.
- La linea se normalizo a UMBAS: `62 UNI`, `IdPresentacionSolicitud=0`.
- El proceso encontro stock y termino con reserva completa `62`.

Lineas rojas:

- Varias no-reservas parecen explicables por FEFO o por existencia en
  ALM/no-picking que no se puede explosionar.
- Caso a revisar: `00025001`, linea `40000`. La traza sugiere que ALM queda
  con disponible `0` despues de restar reservas, pero su fecha minima podria
  seguir bloqueando stock de picking disponible. Esto puede ser una falsa
  negativa si la decision FEFO se toma con listas sin filtrar `Cantidad > 0`.

## Cambio local ya iniciado

En TOMWMS local se agrego una primera mejora:

- `clsLnI_nav_ped_traslado_enc_Partial.vb`
  - toma el motivo real dejado en `Process_Result`;
  - lo copia al resultado visible y a `log_error_wms`;
  - evita reemplazar todo por el generico "No se pudo completar la reserva".

- `clsLnStock_res_Partial.vb`
  - agrega `Marcar_Motivo_No_Reserva_MI3`;
  - sube causas detectadas desde ramas profundas de `Reserva_Stock_From_MI3`.

Esta mejora es util, pero sigue siendo texto libre. El siguiente paso deberia
ser tipificar el motivo.

## Patron reusable desde WebAPI / DAL Core

El motor nuevo `WMS.StockReservation3` contiene una arquitectura que sirve como
molde:

- `ReservationContext`: contexto unico con request, listas de stock, fechas
  minimas, cantidad pendiente, reservas creadas y error.
- `IPipelineStep`: pasos separados (`ValidationStep`, `StockQueryStep`,
  `DateCalculationStep`, `ReservationLoopStep`, `PostProcessingStep`).
- `IReservationLogger`: logger acumulativo de checkpoints, warnings, errores y
  reservas.
- `ReservationResult.Messages`: resultado con mensajes acumulados.

La propuesta no es migrar el algoritmo legacy ahora, sino copiar esa idea:
centralizar el diagnostico en un objeto/contexto de reserva y emitir motivos
tipificados desde cada punto de decision.

## Taxonomia propuesta de motivos

Motivos operativos candidatos:

| Codigo | Cuando aplica | Datos minimos a guardar |
|---|---|---|
| `SIN_STOCK_APLICABLE` | No queda ningun stock despues de filtros de producto, bodega, estado, presentacion, ubicacion y vencimiento. | solicitado, pendiente, filtros activos |
| `RESERVADO_POR_OTROS` | Hay stock fisico, pero despues de restar `stock_res` disponible queda 0 o insuficiente. | stock fisico, reservado vigente, disponible |
| `SIN_VENCIMIENTO_VALIDO` | Hay stock, pero `Fecha_vence` no cumple `DiasVencimiento` / politica de cliente. | fecha minima, dias requeridos |
| `FEFO_BLOQUEA_PICKING` | Hay stock en picking, pero existe stock mas viejo en ALM/no-picking y la regla FEFO no permite saltarlo. | fecha picking, fecha ALM, cantidades |
| `SOLO_NO_PICKING_SIN_EXPLOSION` | Solo hay stock aplicable en no-picking/ALM y no se puede explosionar o tomar desde esa zona. | cantidad no-picking, ubicacion, nivel |
| `EXPLOSION_NIVEL_NO_APLICA` | Explosion automatica esta activa, pero el nivel/ubicacion no cumple la regla. | ubicacion, nivel, max nivel |
| `PRESENTACION_NO_APLICA` | La presentacion/variant solicitada no coincide o no puede convertirse de forma segura. | id presentacion, factor, UM |
| `UBICACION_CLIENTE_OBLIGATORIA` | Cliente exige abastecer desde una ubicacion y no hay stock aplicable alli. | ubicacion obligatoria, disponible alli |
| `TALLA_COLOR_NO_APLICA` | Bodega controla talla/color y no hay stock con combinacion valida. | size, color, id talla-color |
| `PARCIAL_NO_PERMITIDA` | Existe reserva parcial posible, pero la configuracion rechaza incompleto. | solicitado, reservado, faltante |

## Formato sugerido de mensaje operativo

Visible en `Process_Result`:

```text
No se pudo completar la reserva: FEFO_BLOQUEA_PICKING - existe stock en picking,
pero hay stock mas antiguo en ALM/no-picking y la regla FEFO no permite saltarlo.
Solicitado: 121, disponible picking: 10, fecha picking: 2027-04-13,
fecha ALM: 2027-04-12.
```

Para `log_error_wms` / trace YAML se recomienda guardar mas detalle tecnico:

- `CodigoMotivo`
- `Categoria`
- `MensajeOperativo`
- `CantidadSolicitada`
- `CantidadReservada`
- `CantidadPendiente`
- `StockFisico`
- `StockDisponiblePostReserva`
- `PickingDisponible`
- `NoPickingDisponible`
- `FechaMinimaPicking`
- `FechaMinimaNoPicking`
- `IdUbicacion`
- `IdPresentacion`
- `IdStock` si aplica
- `NoEnc`, `Line_No`, `Item_No`

## Beneficio esperado

1. Operacion deja de ver un rojo ambiguo y puede actuar:
   - mover stock a picking;
   - ajustar dias de vencimiento;
   - liberar/resolver reservas previas;
   - corregir presentacion/variant;
   - revisar configuracion de ubicacion obligatoria.

2. Desarrollo puede distinguir regla correcta vs falsa negativa:
   - si el motivo es FEFO, pero el stock ALM disponible post-reserva es 0,
     hay bug de decision;
   - si el motivo es `RESERVADO_POR_OTROS`, la reserva es correcta y el foco
     es auditoria de `stock_res`;
   - si el motivo es `SIN_VENCIMIENTO_VALIDO`, el ajuste puede estar en
     parametros del cliente, no en codigo.

3. Brain puede aprender patrones de no-reserva y convertirlos en casos de
   prueba naturales (`brain/test-scenarios/reservation`).

## Solicitud a Brain Keeper

Se solicita promover este aprendizaje como regla de diagnostico del modulo de
reserva:

1. Crear o actualizar una ficha canonica de "diagnostico de no-reserva MI3".
2. Relacionarla con los test scenarios de reserva existentes.
3. Recomendar si la taxonomia debe vivir como:
   - learning card;
   - regla de skill `wms-tomwms`;
   - nota en `sendero-producto` / reservation scenarios;
   - handoff futuro para refactor incremental.

## Pendientes

- Validar con datos de stock de BYB QA si cada linea roja de `EA-153305`
  corresponde a un motivo correcto.
- Confirmar el posible falso bloqueo FEFO cuando las listas de ALM/no-picking
  quedan en cantidad 0 despues de restar reservas.
- Reindexar Brain API despues de que el parche VB sea revisado, comiteado y
  empujado al repo `TOMWMS_BOF`.

