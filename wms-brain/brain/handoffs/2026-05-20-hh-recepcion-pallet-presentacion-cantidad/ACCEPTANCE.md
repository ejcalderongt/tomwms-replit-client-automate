# Verificaciones obligatorias

## Tecnicas
- [x] Compilacion Gradle debug exitosa de TOMHH2025.
- [ ] Test manual: escanear pallet con presentacion UNIDAD -> Uds_lic_plate
      muestra cantidad_presentacion, stock guarda cantidad_presentacion * Factor.
- [ ] Test manual: escanear pallet con presentacion PALLET -> stock guarda
      cantidad_presentacion * Factor * CamasPorTarima * CajasPorCama.
- [ ] Test manual: escanear sin presentacion seleccionada -> usa Cantidad_UMP.
- [ ] Test manual: Factor=0 -> abortar con mensaje claro (NO permitir 0 en stock).

## Commits (pendiente, Erik decide cuando)
- [ ] Commit a TOMHH2025 con mensaje:
  `#EJCRP fix(recepcion-hh): Cantidad en UMBAS para vBeStockRecPallet [brain:handoffs/2026-05-20-hh-recepcion-pallet-presentacion-cantidad]`

## RESULT
- [x] RESULT.md completado.
- [x] LEARNINGS.md completado.
- [ ] Push a wms-brain.
