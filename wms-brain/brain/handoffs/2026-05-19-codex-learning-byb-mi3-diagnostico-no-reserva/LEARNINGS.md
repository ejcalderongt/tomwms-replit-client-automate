---
slug: 2026-05-19-codex-learning-byb-mi3-diagnostico-no-reserva
agente: codex-local
fecha: 2026-05-19
---

# Reglas descubiertas

- Un resultado `No se pudo completar la reserva` no es suficiente para operar
  ni para validar si la regla de reserva esta correcta. Debe indicar por que el
  stock no era aplicable.

- En MI3/BOF, la no-reserva debe distinguir al menos tres capas:
  1. **No hay stock fisico aplicable**.
  2. **Hay stock fisico, pero no hay disponible despues de reservas vigentes**.
  3. **Hay disponible, pero una regla de negocio impide reservarlo**.

- `Process_Result` debe ser mensaje operativo breve. `log_error_wms` y la
  traza YAML deben conservar el diagnostico tecnico con cantidades, fechas,
  zonas y filtros activos.

- El patron de `WMS.StockReservation3` es reusable: contexto compartido,
  logger acumulativo y resultado con mensajes. No es necesario migrar el
  algoritmo legacy para adoptar ese patron de diagnostico.

# Taxonomia inicial recomendada

- `SIN_STOCK_APLICABLE`
- `RESERVADO_POR_OTROS`
- `SIN_VENCIMIENTO_VALIDO`
- `FEFO_BLOQUEA_PICKING`
- `SOLO_NO_PICKING_SIN_EXPLOSION`
- `EXPLOSION_NIVEL_NO_APLICA`
- `PRESENTACION_NO_APLICA`
- `UBICACION_CLIENTE_OBLIGATORIA`
- `TALLA_COLOR_NO_APLICA`
- `PARCIAL_NO_PERMITIDA`

# Gotchas tecnicos

- FEFO puede producir falsos negativos si la decision usa una lista con stock
  historico o fisico, pero no filtra `Cantidad > 0` despues de restar
  reservas vigentes.

- Una linea puede tener stock visible en pantalla y aun asi no ser reservable:
  no-picking, vencimiento, presentacion, ubicacion obligatoria, talla/color o
  reservas previas pueden excluirlo correctamente.

- Las trazas de performance (`perf_restar_stock_reservado_fin`) ayudan a ver
  costo, pero por si solas no explican la causa de negocio. Deben coexistir
  con eventos `motivo_no_reserva`.

- El texto libre con codigos historicos (`#ERROR_...`) sirve para debug, pero
  no es estable para reporteria ni para tomar decisiones operativas.

# Suposiciones que tome (requieren validacion de Erik)

- [ ] `I_nav_ped_traslado_det.Process_Result` puede contener un mensaje mas
      largo que el generico actual sin romper la pantalla de existencias.
- [ ] `log_error_wms` sigue siendo el lugar correcto para detalle tecnico
      cuando no se agrega tabla nueva de diagnostico.
- [ ] Para el primer refactor no conviene tocar schema SQL; basta con
      tipificar motivos en VB y registrar el detalle en trace/log.
- [ ] DAL Core / WebAPI debe copiar el mismo catalogo de motivos para mantener
      paridad cuando el flujo migre.

# Sugerencias para el brain

- Crear una ficha canonica de diagnostico de no-reserva MI3 y enlazarla desde
  los escenarios naturales de reserva.

- Agregar a `wms-tomwms` una regla: "Cuando un flujo de reserva falla, no
  devolver solo `No se pudo completar la reserva`; devolver motivo operativo
  tipificado y datos minimos para reproducir la decision".

- Convertir cada motivo en un test scenario:
  - stock solo no-picking;
  - FEFO bloquea picking;
  - vencimiento invalido;
  - reservado por otros;
  - ubicacion obligatoria sin disponible;
  - presentacion no aplicable.

# Sugerencias para futuros BRIEFs

- Todo BRIEF de reserva debe especificar:
  - unidad de medida de la solicitud;
  - presentacion y factor;
  - stock fisico antes/despues de restar reservas;
  - fechas minimas por zona;
  - si aplica FEFO;
  - si la ubicacion de cliente es obligatoria.

- Cuando el objetivo sea "confirmar que la no-reserva es correcta", pedir
  evidencia de:
  - `stock`;
  - `stock_res`;
  - `trans_pe_det_log_reserva`;
  - resultado visible de `I_nav_ped_traslado_det`;
  - trace YAML de `Reserva_Stock_From_MI3`.

