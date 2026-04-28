# fingerprint/ — fingerprint funcional por cliente

> Macro-perfil de cada cliente: que features tiene activas, que volumen
> maneja, que interface al ERP usa, que tablas exclusivas tiene.
>
> Objetivo Erik 29-abr: "saber rapidamente los macro-datos de
> funcionamiento de cada instancia para decir: este patron de datos,
> esta asi en idealsa? — y comparar A contra B".
>
> A futuro este folder permite generar **seeds limpios** para nuevos
> clientes: tomamos el fingerprint de un cliente similar, limpiamos
> transacciones, y arrancamos.

## Estructura del fingerprint (template)

Cada `<CLIENTE>.md` debe responder:

1. **Macro-tag**: 1 linea con keywords. Ej: "MAMPA: talla y color
   (zapateria), 18 bodegas, modulo verificacion ON, log segmentado ON,
   31K productos, ambiente QAS".
2. **Identidad**: BD, ambiente (PRD/QAS), version del codebase.
3. **Volumen**: total tablas, total productos, total transacciones
   outbox, periodo de operacion.
4. **Features ON/OFF**: matriz contra el catalogo del brain-map.
5. **Bodegas**: listado y configuracion (`control_talla_color`,
   `tipo_pantalla_*`).
6. **Interface ERP**: SAP B1 / NAV / Otro. Binario sincronizador.
7. **Tablas exclusivas**: que tiene este cliente que no tienen los demas.
8. **Sub-perfiles internos**: bodegas o productos con perfil distinto
   al promedio del cliente.
9. **Diagnosticos abiertos**: hipotesis del brain que afectan a este
   cliente.
10. **Aprendizajes especificos**: que aprendimos sobre este cliente.

## Clientes con fingerprint completo

- `MAMPA.md` — APPLIED 29-abr-2026

## Clientes pendientes

- `KILLIOS.md` (K7) — esqueleto
- `BECOFARMA.md` — esqueleto + datos H29
- `BYB.md` (B&B)
- `CEALSA.md` (C9-QAS)
- `CUMBRE.md` (no consultado en EC2 todavia)
- `IDEALSA.md` (no consultado todavia)
- `INELAC.md` (no consultado todavia)
