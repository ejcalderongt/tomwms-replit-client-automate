---
slug: 2026-05-19-codex-learning-bof-veri-movimientos-duplicados
agente: codex-local
fecha: 2026-05-19T15:20:00-06:00
repo_afectado: TOMWMS
area: BOF/BD
---

# Reglas descubiertas

- En movimientos `VERI`, `trans_movimientos.cantidad` debe almacenarse en UMBAS, no en cantidad de presentación.
- En `trans_picking_ubic`, la cantidad operativa visible para verificación puede estar expresada en presentación cuando existe `IdPresentacion`; por eso no siempre puede copiarse directamente hacia `trans_movimientos.cantidad`.
- Para comparar `VERI` contra la línea verificada, el cruce debe considerar la llave lógica completa: picking, pedido, pedido detalle, recepción si aplica, producto bodega, licencia/pallet, lote, fecha vence, ubicación origen/destino, presentación, estados y unidad de medida.
- Una línea BOF marcada como no verificada debe limpiar movimientos `VERI` asociados aunque la verificación no venga del flujo `Verificacion_Auto`.
- Antes de insertar un movimiento `VERI`, el proceso debe ser idempotente: si ya existe un movimiento equivalente por llave lógica y cantidad, no debe insertar otro registro.

# Gotchas técnicos

- El patrón de duplicado exacto se puede esconder si solo se agrupa por producto o pedido; es necesario agrupar por barra, producto, pedido detalle, recepción detalle, ubicación origen/destino, lote, fecha vence y cantidad.
- Algunas columnas de texto involucradas en cruces (`Lic_plate`, `barra_pallet`, `Lote`) pueden tener collation distinta; las consultas de auditoría deben usar `COLLATE DATABASE_DEFAULT`.
- Después de eliminar duplicados exactos pueden quedar diferencias reales: movimientos `VERI` únicos con cantidad de presentación en vez de UMBAS.
- Para las diferencias de cantidad, el movimiento `PIK` fue útil como referencia de cantidad UMBAS esperada cuando coincide la misma llave logística.
- `Eliminar_Movimiento_Verificacion_By_PickingUbic` depende de que la cantidad del `VERI` coincida con la cantidad esperada; por eso los históricos con cantidad incorrecta pueden requerir regularización previa antes de validar bien la limpieza por BOF.

# Suposiciones que tomé (REQUIEREN VALIDACION DE ERIK)

- [ ] Se asume que para `VERI`, cuando hay `IdPresentacion` con factor válido, la cantidad correcta en `trans_movimientos.cantidad` es `Cantidad_Presentacion * Factor`.
- [ ] Se asume que para el caso auditado el movimiento `PIK` equivalente representa la cantidad UMBAS correcta contra la cual se puede regularizar `VERI`.
- [ ] Se asume que la validación idempotente por llave lógica completa no bloquea escenarios válidos donde se requieran dos movimientos `VERI` separados con la misma llave y misma cantidad.
- [ ] Se asume que el flujo BOF/manual debe comportarse igual que el flujo automático al desverificar: debe eliminar el `VERI` asociado.

# Patrones recurrentes detectados

- Hay riesgo de divergencia cuando una pantalla opera en presentación pero la tabla de movimientos espera UMBAS.
- Los procesos BOF que regeneran movimientos necesitan idempotencia explícita para evitar duplicados por re-proceso.
- La regularización histórica debe separarse de la prevención en código: primero scripts/SPs auditables, luego parche BOF.
- Para casos de datos productivos, conviene documentar siempre tres piezas: diagnóstico cliente, guía operativa y script/SP reversible o ejecutable en simulación.

# Sugerencias para el brain

- Promover una regla global de inventario/movimientos: `trans_movimientos.cantidad` debe documentarse como cantidad base/UMBAS salvo excepción explícita.
- Agregar un playbook de auditoría `VERI`: detectar duplicados exactos, detectar cantidad presentación vs UMBAS, validar contra `PIK`, ejecutar post-check.
- Registrar que `Procesar_Verificacion_Desde_BOF` y `Procesar_Picking_Desde_BOF` son puntos críticos para generación de movimientos `VERI`.
- Registrar que `cmdNoVerificado` / `Marcar_Linea_No_Verificada` debe limpiar `VERI` también en verificación BOF/manual.

# Sugerencias para futuros BRIEFs

- Cuando el cambio toque movimientos de inventario, especificar explícitamente si las cantidades esperadas son UMBAS, presentación, peso o unidad alterna.
- Incluir siempre una muestra de datos con `IdPickingEnc`, `IdPedidoDet`, `IdProductoBodega`, `Lic_plate`, `Lote`, `Fecha_Vence`, `IdPresentacion` y factor.
- Pedir post-check con totales antes/después y estado final: duplicados exactos restantes, mismatch presentación restante y líneas sin `VERI`.
- Separar desde el BRIEF si el alcance es corrección histórica BD, prevención BOF o ambos.
