---
slug: 2026-05-20-hh-recepcion-pallet-presentacion-cantidad
estado: aplicado
prioridad: media
autor: replit (retroactivo)
fecha_propuesta: 2026-05-20
afecta:
  - repo: TOMHH2025
  - rama: dev_2028_merge
  - cliente: CORE (regla del nucleo de recepcion)
no_afecta:
  - TOMWMS_BOF (cambio solo en HH)
---

# Objetivo

Corregir el manejo de `cantidad` en el flujo de recepcion HH cuando se escanea un
pallet de proveedor, para que `vBeStockRecPallet.Cantidad` viaje SIEMPRE en
unidad de medida basica (UMBAS), independientemente de la presentacion
seleccionada por el operador.

# Contexto

Este BRIEF es **retroactivo**: el cambio ya fue aplicado por Codex local con
guia directa de Erik en sesion de chat. Se documenta a posteriori para dejar
trazabilidad y promover la regla aprendida al brain canonico.

Caso operativo: en `frm_recepcion_datos.java`, al confirmar un pallet escaneado
con `BeINavBarraPallet`, el codigo original asignaba `Cantidad_Presentacion`
directamente a multiples destinos, incluido `vBeStockRecPallet.Cantidad`. Eso
descuadra el stock porque la capa de stock del core trabaja en UMBAS, no en la
unidad de presentacion del usuario.

# Cambios aplicados

1. **`Uds_lic_plate` y mensaje al operador**:
   - Si la presentacion seleccionada tiene `Nombre` valido,
     usar `BeINavBarraPallet.Cantidad_Presentacion`.
   - Si no, usar `BeINavBarraPallet.Cantidad_UMP` como fallback.

2. **`vBeStockRecPallet.Cantidad` (CRITICO, va a stock)**:
   - SIEMPRE en UMBAS.
   - Si hay presentacion:
     `Cantidad = Cantidad_Presentacion * Factor`
   - Si la presentacion es pallet, ademas:
     `Cantidad = Cantidad_Presentacion * Factor * CamasPorTarima * CajasPorCama`
   - Si no hay presentacion valida: `Cantidad = Cantidad_UMP` (que ya viene en UMBAS).

# Archivos tocados

- `app/src/main/java/com/dts/tom/Transacciones/Recepcion/frm_recepcion_datos.java`
  - Bloque que asigna cantidades despues del escaneo de `BeINavBarraPallet`.

# Reglas vinculantes

- NO mezclar este cambio con cambios de BOF en el mismo commit.
- Verificar que el factor de conversion (`Factor`) viene cargado del maestro
  de producto/presentacion, no hardcoded.
- Mantener `n` (no aplicar `.replace("ñ","n")` en mensajes al operador).

# Riesgos / consideraciones

- Si el `Factor` viene en 0 o null, el calculo de `Cantidad` da 0 y el stock
  queda vacio sin error visible. Validar defensivamente y abortar la
  recepcion con mensaje claro si pasa.
- Si `CamasPorTarima` o `CajasPorCama` vienen en 0, mismo problema. Defaults
  a 1 si null/0 son aceptables operativamente, pero conviene confirmarlo con
  Erik antes de generalizar.
