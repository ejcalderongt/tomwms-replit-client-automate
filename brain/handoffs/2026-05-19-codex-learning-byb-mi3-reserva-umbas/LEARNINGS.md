---
slug: 2026-05-19-codex-learning-byb-mi3-reserva-umbas
agente: codex-local
fecha: 2026-05-19T16:20:00-06:00
---

# Reglas descubiertas

- En `Reserva_Stock_From_MI3`, si `pStockResSolicitud.IdPresentacion = 0`, la cantidad esta en UMBAS y el pendiente real para recursion debe ser `vCantidadPendiente`.
- `vCantidadDecimalUMBas` no debe tratarse como fuente universal del pendiente; tambien se usa en rutas de fraccion/explosion de presentacion.
- Una reserva en presentacion puede ser valida aunque la linea NAV venga en unidad base, pero el remanente recursivo debe conservar la unidad semantica del pedido original.

# Gotchas tecnicos

- El total mostrado en `Quantity_Reserved_WMS` puede ocultar la mezcla de reservas en presentacion y UMBAS; hay que revisar `stock_res.IdPresentacion` por linea.
- En BYB QA se observo que una linea `15 UNI` de `00194250` quedo con `12 CJ + 997 UMBAS`, total `1009`, porque la recursion tomo el stock UMBAS disponible completo.
- El log `trans_pe_det_log_reserva` ayuda a identificar el camino exacto: `CASO_#8_EJC202310090957`, `LLR_CASO_#29`, luego `CASO_#7_EJC202310090957`.
- El error posterior de NAV "There is nothing to handle" puede tener una causa distinta a la reserva WMS; no debe mezclarse automaticamente con la causa de sobre-reserva.

# Suposiciones que tomé (REQUIEREN VALIDACION DE ERIK)

- [ ] Para solicitudes con `IdPresentacion = 0`, `vCantidadPendiente` siempre esta expresado en UMBAS en este bloque de `Reserva_Stock_From_MI3`.
- [ ] El cambio no debe aplicarse a solicitudes con `IdPresentacion <> 0`, porque alli `vCantidadDecimalUMBas` conserva sentido para explosion/fraccion.
- [ ] El parche es suficiente para evitar el caso BYB `00194250` sin alterar escenarios de Mercopan/Killios documentados en comentarios cercanos.
- [ ] El motor nuevo DALCore debe revisarse por paridad, pero no corregirse en el mismo parche BOF.

# Patrones recurrentes detectados

- Riesgo alto cuando una variable con nombre UMBAS acumula tanto "decimal de presentacion" como "pendiente en unidad base".
- En flujos recursivos de reserva, la unidad de medida del request original debe gobernar la conversion del pendiente.
- Los casos de cliente deben documentarse con: documento, linea, producto, cantidad pedida, reservas por `IdStock`, `IdPresentacion`, factor y logs de reserva.

# Sugerencias para el brain

- Promover al modulo de reserva una regla: "No mezclar variables de fraccion/explosion con pendiente base cuando `IdPresentacion = 0`".
- Agregar un playbook de auditoria de sobre-reserva MI3:
  - cruzar `trans_pe_det` vs `stock_res`;
  - mostrar cantidad pedida, presentacion del pedido, reservas agrupadas por presentacion/factor;
  - detectar `Quantity_Reserved_WMS` mayor a cantidad pedida cuando la linea viene en UMBAS.
- Registrar el tag `#EJCBYB20250519CKF` como parche BYB QA de proteccion de recursion UMBAS en BOF.

# Sugerencias para futuros BRIEFs

- Incluir siempre si la cantidad del documento viene en UMBAS o presentacion.
- Pedir evidencia de `stock_res` con `IdPresentacion`, `factor`, `IdStock`, `lote`, `lic_plate` y `trans_pe_det_log_reserva`.
- Separar diagnostico de reserva WMS de diagnostico posterior NAV Create Pick.
