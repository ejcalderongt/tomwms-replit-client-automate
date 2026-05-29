---
slug: 2026-05-20-hh-recepcion-pallet-presentacion-cantidad
agente: codex-local (con correccion en vivo de Erik)
fecha: 2026-05-20
---

# Reglas descubiertas

- **Regla UMBAS (CRITICA, candidata a regla vinculante global)**:
  Cualquier asignacion al campo `Cantidad` de una entidad que vive en la capa
  `stock` (`vBeStockRecPallet`, `vBeStockRecDet`, `vBeStock`, etc.) DEBE estar
  expresada en UMBAS (Unidad de Medida Basica). Si el origen del dato esta en
  otra unidad (presentacion, pallet, cama, caja), hay que convertirlo
  explicitamente multiplicando por el factor correspondiente ANTES de
  asignarlo.

- **Doble vida de la cantidad en recepcion HH**:
  En el mismo flujo existen dos "cantidades" simultaneamente:
  1. La operativa/visible (`Uds_lic_plate`, mensaje al operador) -> puede ir
     en la unidad de presentacion seleccionada.
  2. La fisica/inventariable (`stock.Cantidad`) -> SIEMPRE en UMBAS.
  Mezclar ambas en el mismo asignamiento es el bug.

- **Jerarquia de fuente de cantidad en pallet de proveedor**:
  1. `Cantidad_Presentacion` si la presentacion seleccionada tiene `Nombre`.
  2. `Cantidad_UMP` como fallback (ya esta en UMBAS).

- **Multiplicadores por nivel de empaque**:
  Cuando la presentacion es pallet:
  `cantidad_umbas = Cantidad_Presentacion * Factor * CamasPorTarima * CajasPorCama`

# Gotchas tecnicos

- El error es **silencioso**: el stock queda con cantidad menor a la real (por
  el factor) pero sin excepcion ni log. Solo se detecta al ajustar inventario
  o al hacer picking que no encuentra unidades. Esto explica una clase entera
  de descuadres "fantasma" en clientes con presentaciones distintas a UMBAS.

- Codex inicialmente replico el patron visual (`Cantidad_Presentacion` a todo)
  porque parecia consistente. Sin el contexto de "stock siempre en UMBAS"
  es razonable equivocarse — la nomenclatura `Cantidad` en ambos campos no
  ayuda a distinguir.

# Suposiciones que tome (REQUIEREN VALIDACION DE ERIK)

- [ ] `Factor` viene siempre cargado y > 0 desde el maestro de producto. Si
      puede venir null o 0, el codigo necesita defensa adicional.
- [ ] `CamasPorTarima` y `CajasPorCama` defaultean a 1 si vienen null/0, o
      el flujo aborta si no estan parametrizados.
- [ ] Esta regla "stock siempre en UMBAS" aplica a TODOS los `vBeStock*` del
      core, no solo a `vBeStockRecPallet`. Si es asi, es regla cross-cliente
      y deberia ir a `replit.md §4`.

# Patrones recurrentes detectados

- En recepcion HH existen al menos 3 puntos donde se asigna `Cantidad` a
  entidades de stock. Auditar:
  - `frm_recepcion_datos.java` (corregido)
  - `frm_recepcion_pallet_*.java` (sospechoso, mismo patron)
  - `frm_recepcion_caja_*.java` (sospechoso, mismo patron)
  Codex no audito los otros — recomendado handoff aparte.

# Sugerencias para el brain

- **Crear** `wms-brain/brain/code-changes/HH/PATTERNS-UMBAS.md` con la regla
  canonizada y la lista de campos `*.Cantidad` que deben ir en UMBAS.
- **Sumar a** `replit.md` §4 (Reglas vinculantes): "Toda asignacion a
  `*.Cantidad` de entidades stock va en UMBAS. Convertir explicitamente
  desde presentacion antes de asignar."
- **Sumar a** `.local/skills/wms-tomwms/conventions.md` un bloque
  "Unidades de medida en HH" con la jerarquia
  Cantidad_Presentacion -> Cantidad_UMP -> Cantidad_UMBAS.

# Sugerencias para futuros BRIEFs

- Cuando el handoff toca cantidades en HH, el BRIEF debe especificar
  explicitamente: "el campo destino X esta en UMBAS / esta en presentacion".
  Sin esa marca, el agente puede asumir mal.
- Listar SIEMPRE los campos `BeINavBarraPallet.*` que el flujo toca, con
  su unidad de medida documentada al lado.
