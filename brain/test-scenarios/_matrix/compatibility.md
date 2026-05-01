---
id: compatibility
tipo: test-scenario
estado: vigente
titulo: Matriz de compatibilidad escenario × cliente — v3
ramas: [dev_2028_merge]
tags: [test-scenario]
---

# Matriz de compatibilidad escenario × cliente — v3

> Generada 2026-04-27T15:31:09.941Z. Basada en flags reales aprendidos de `i_nav_config_enc` y log observado en `trans_pe_det_log_reserva`.

## Leyenda

- `OK` — cliente cumple los `requires_config` del escenario.
- `OBS` — cliente cumple Y existe evidencia en `trans_pe_det_log_reserva` (caso observado en prod).
- `N/A` — cliente no cumple los `requires_config`.
- `THE` — escenario teorico: el cliente cumple flags pero NO hay evidencia productiva. Es target de **reserva-webapi (dev_2028)**.

## Matriz

| Escenario | KILLIOS | BYB | CEALSA-gen | CEALSA-fiscal | Notas |
|---|---|---|---|---|---|
| RES-001 | THE | THE | THE | N/A | sin evidencia en log |
| RES-006 | THE | THE | THE | N/A | sin evidencia |
| RES-007 | THE | THE | THE | N/A | duplicado funcional con RES-010 |
| RES-008 | **OBS** (1675) | (THE - 2 reg recientes) | THE | N/A | observado en prod |
| RES-009 | OBS (1) | THE | THE | N/A | rarisimo en prod |
| RES-010 | THE | THE | THE | N/A | duplicado con RES-007 |
| RES-011 | THE | THE | THE | N/A | |
| RES-012 | **OBS** (276) | THE | THE | N/A | |
| RES-013 | THE | THE | THE | N/A | |
| RES-014 | THE | THE | THE | N/A | |
| RES-015 | THE | THE | THE | N/A | |
| RES-016 | THE | THE | THE | N/A | |
| RES-017 | THE | THE | THE | N/A | |
| RES-018 | N/A | N/A | N/A | N/A | requiere explosion_automatica_desde_ubicacion_picking=false; ningun cliente lo cumple |
| RES-019 | THE | THE | THE | N/A | ambiguedad pendiente |
| RES-020 | **OBS** (267) | THE | THE | N/A | pero observado en bodega NO almacenamiento |
| RES-021 | **OBS** (1065) | THE | THE | N/A | reservar_umbas_primero=false → expected webapi diverge |
| RES-022 | OBS (44) | THE | THE | N/A | |
| RES-023 | **OBS** (125) | THE | THE | N/A | |
| RES-024 | **OBS** (18587) | THE | THE | N/A | camino feliz dominante |
| RES-DIN | OK | OK | OK | N/A | runner dinamico |

## Resumen

- **Killios** tiene flags compatibles con todos los escenarios FEFO/explosion. 9 escenarios estan **observados en log** productivo, los otros 12 son **teoricos / target webapi**.
- **BYB** cumple los mismos flags. Casi sin actividad reciente en log de reservas (706 registros 2023 con Caso_Reserva NULL + 2 registros 2025).
- **CEALSA-gen** cumple flags de los escenarios basicos pero **el motor del WMS no se invoca por defecto** (`trans_pe_det_log_reserva` vacio). Aplican solo si el tipo de pedido tiene `ReservaStock=true` y el escenario lo solicita explicitamente.
- **CEALSA-fiscal** N/A para todos (passthrough sin flags activos).

## Acciones

1. Para cada escenario `THE`, decidir: ¿es candidato a borrar (no aplica), o se mantiene como target de reserva-webapi?
2. Para cada escenario `OBS`, validar que el expected del bridge coincide con el `MensajeLog` del caso observado.
3. RES-018 esta en N/A para todos los clientes — revisar si tiene sentido conservar o reformular.
