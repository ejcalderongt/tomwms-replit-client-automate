---
id: README
tipo: test-scenario
estado: vigente
titulo: Catálogo de escenarios de RESERVA
modulo: [reservation]
tags: [test-scenario, modulo/reservation]
---

# Catálogo de escenarios de RESERVA

Carpeta: `brain/test-scenarios/reservation/`

Cada archivo `RES-NNN.yaml` describe un escenario de test del componente de reserva
de stock del WMS. Los IDs son estables: una vez asignado, no se renumera.

## Estado actual

| ID | Título | Origen legacy | Severity | Estado |
|---|---|---|---|---|
| RES-001 | Caja en alm vence antes que caja en pick — reservar desde alm | CASO 1 | high | activo |
| RES-002 | (reservado) | — | — | hueco |
| RES-003 | (reservado) | — | — | hueco |
| RES-004 | (reservado) | — | — | hueco |
| RES-005 | (reservado) | — | — | hueco |
| RES-006 | FEFO domina ubicación: alm fc cubre, pick fs completa | CASO 2 | high | activo |
| RES-007 | Duplicado redaccional del CASO 2 | CASO 3 | high | activo (candidato a colapso) |
| RES-008 | Salto alm-pick-alm por FEFO (3 lotes) | CASO 4 | high | activo |
| RES-009 | Salto pick-alm-pick por FEFO | CASO 5 | high | activo |
| RES-010 | Variante CASO 5 | CASO 6 | high | activo (candidato a colapso) |
| RES-011 | Stock únicamente en almacenamiento | CASO 7 | high | activo |
| RES-012 | Stock insuficiente con flag estricto → rechazo | CASO 8 | high | activo |
| RES-013 | Explosión de caja en picking para completar UDS | CASO 9 | high | activo |
| RES-014 | Explosión insuficiente → rechazo | CASO 10 | high | activo |
| RES-015 | FEFO desde alm aunque haya stock en pick | CASO 11 | high | activo |
| RES-016 | Como RES-015 pero solicitud cubre alm + 1 de pick | CASO 12 | high | activo |
| RES-017 | Caja en alm no es candidata a explosión → rechaza | CASO 13 | high | activo |
| RES-018 | Caja en pick sí explota → completa | CASO 14 | high | activo |
| RES-019 | Mix complejo 7 lotes, sin explosión, rechaza | CASO 15 | high | activo (con ambigüedad) |
| RES-020 | Mix complejo con explosión desde alm | CASO 16 | high | activo |
| RES-021 | Mismo vencimiento alm: prefiere UDS sobre caja | CASO 17 | medium | activo |
| RES-022 | BYB transferencia | CASO 18 | medium | stub (datos por completar) |
| RES-023 | BYB transferencia variante 2 | CASO 19 | medium | stub |
| RES-024 | BYB transferencia 45 CJS presentación | CASO 20 | medium | stub |
| RES-DIN | Runner dinámico desde configuracion_qa | CASO Dinamico | low | scenario runner |

## Convenciones

- `requires_config`: lista de flags + valores requeridos (ver
  `brain/skills/wms-test-bridge/reference/config-flags.md`).
- `input.product_code`, `input.owner`, `input.doc_type`, `input.invocation`:
  todos hardcoded en el legacy; en el bridge se parametrizan por cliente.
- `setup.inventory`: estado inicial del inventario antes de la reserva.
- `expected`: pasos verificables. El runner ejecuta y aserta.
- `legacy_origin`: trazabilidad al CASO original con archivo + líneas.

## Política de duplicados

Los CASOs 3 (RES-007) y 6 (RES-010) son duplicados funcionales de los CASOs 2 y 5.
Se mantienen como YAMLs separados por trazabilidad (un YAML por CASO legacy), pero
el runner del bridge puede colapsarlos si implementa idempotencia por hash de
`(input + setup + expected)`.

## Ambigüedades pendientes

1. **RES-019 vs RES-020**: el legacy describe expected contradictorios (rechaza vs
   explota G). Resolver leyendo el cuerpo completo de los CASOs 15 y 16, o
   confirmando con Erik cuál es la política correcta por cliente.
2. **RES-022, RES-023, RES-024**: stubs; los XML docs del legacy no documentan los
   datos de entrada. Hay que extraerlos del cuerpo VB.
3. **RES-007, RES-010**: confirmar si son intencionalmente duplicados o variantes
   con datos sutilmente distintos.

## Cómo agregar un nuevo escenario

1. Tomar el siguiente ID libre (RES-025 al momento de escribir).
2. Crear `RES-025.yaml` siguiendo el formato de cualquier YAML existente.
3. Agregar la fila a la tabla de arriba.
4. Actualizar `brain/test-scenarios/_matrix/compatibility.md` corriendo
   `compute-matrix.cjs`.
