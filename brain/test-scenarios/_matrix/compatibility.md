# Matriz de compatibilidad escenario × cliente

> Generada automaticamente desde flags reales aprendidos (2026-04-27T14:56:31.611Z).
> Para clientes con varias bodegas funcionalmente identicas, se mostro una entrada.
> CEALSA se desdobla en CEALSA-gen (bodega 1) y CEALSA-fiscal (bodega 2, passthrough).

## Leyenda

- `OK`: cliente cumple los `requires_config` del escenario; correr y validar.
- `N/A`: cliente NO cumple `requires_config`; el escenario no aplica.
- `?`: ambiguedad documentada en el escenario.

## Matriz

| Escenario | KILLIOS | BYB | CEALSA-gen | CEALSA-fiscal | Notas |
|---|---|---|---|---|---|
| RES-001 | OK | N/A | N/A | N/A |  |
| RES-006 | OK | N/A | N/A | N/A |  |
| RES-007 | OK | N/A | N/A | N/A | duplicado funcional, candidato a colapso |
| RES-008 | OK | N/A | N/A | N/A |  |
| RES-009 | OK | N/A | N/A | N/A |  |
| RES-010 | OK | N/A | N/A | N/A | duplicado funcional, candidato a colapso |
| RES-011 | OK | N/A | N/A | N/A |  |
| RES-012 | OK | N/A | N/A | N/A |  |
| RES-013 | OK | N/A | N/A | N/A |  |
| RES-014 | OK | N/A | N/A | N/A |  |
| RES-015 | OK | N/A | N/A | N/A |  |
| RES-016 | OK | N/A | N/A | N/A |  |
| RES-017 | OK | N/A | N/A | N/A |  |
| RES-018 | OK | N/A | N/A | N/A |  |
| RES-019 | OK | N/A | N/A | N/A | ambiguedad pendiente (rechaza vs explota G) |
| RES-020 | OK | N/A | N/A | N/A |  |
| RES-021 | OK | N/A | N/A | N/A | reservar_umbas_primero=false en TODOS → expected del legacy no se cumple en prod |
| RES-022 | OK | N/A | N/A | N/A |  |
| RES-023 | OK | N/A | N/A | N/A |  |
| RES-024 | OK | N/A | N/A | N/A |  |
| RES-DIN | OK | OK | OK | OK |  |

## Resumen

- **Killios** cumple todos los escenarios FEFO/explosion (control_vencimiento=true, explosion_automatica=true, explosion_automatica_desde_ubicacion_picking=true). Atencion al typo `explosio_automatica_nivel_max=1` vs `explosion_automatica_nivel_max=-1`.
- **BYB** cumple los mismos. Ademas es el unico para escenarios de reabastecimiento.
- **CEALSA-gen** cumple basicos, no los SAP-especificos.
- **CEALSA-fiscal** N/A para todos los escenarios de reserva (bodega passthrough sin flags activos).
- **RES-021** reservar_umbas_primero=false en los 3 clientes → revisar el expected del escenario, el legacy asumia true.
