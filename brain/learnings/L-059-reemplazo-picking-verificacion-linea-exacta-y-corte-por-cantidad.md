---
output_type: aprendizaje
audience: agente-brain + Erik + futuros mantenedores
version: V1
status: ratificado
authored_by: agente-brain
authored_at: 2026-07-13T09:45:00-06:00
---

# L-059 - Reemplazo en Picking y Verificacion: linea exacta, corte por cantidad y no fan-out

## Regla operativa

Cuando una pantalla HH de reemplazo envía una linea operativa valida, el DAL debe trabajar sobre esa linea exacta y no sobre el conjunto de lineas hermanas que comparten el mismo stock, producto o llave operativa.

La identidad prioritaria es:

1. `IdPickingUbic`
2. `IdPickingDet`
3. Fallback historico solo si la identidad exacta no viene informada

## Lo que se aprendio del incidente

En los escenarios de Picking y Verificacion, el fan-out aparecia cuando el reemplazo resolvia varias `trans_picking_ubic` relacionadas por stock compartido. Eso permitia que una accion de 3 o 5 unidades se propagara a dos pedidos distintos aunque la UI hubiera seleccionado una sola linea.

La correccion efectiva fue:

- resolver la linea exacta antes de reservar o marcar;
- cortar por cantidad completada;
- evitar que el estado "sin existencia" bloquee una rama de reemplazo con stock cuando ya existe una linea valida;
- agregar trazas de entrada, resolucion y salida para detectar desvio de contexto.

## Criterio de seguridad

Si el filtro funcional de Verificacion vacia la lista, pero HH envio una linea exacta valida, no se debe abortar de inmediato: primero se preserva la linea exacta y luego se reevalua el filtro.

## Casos validados

- Picking con reemplazo: sin duplicacion.
- Picking sin existencia / no encontrado: corte correcto por cantidad.
- Verificacion con reemplazo: preserva la linea exacta y evita falso negativo.

## Tags

- `#EJC20260713_REEMPL_LINEA_EXACTA`
- `#EJC20260713_REEMPL_VERIF_LINEA_EXACTA`
- `#EJC20260713_REEMPL_PICK_CORTE_CANTIDAD`

