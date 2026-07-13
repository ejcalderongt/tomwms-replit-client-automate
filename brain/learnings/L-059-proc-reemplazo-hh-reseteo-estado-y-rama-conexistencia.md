---
output_type: aprendizaje
audience: agente-brain + Erik + futuros mantenedores
version: V1
status: ratificado
authored_by: agente-brain
authored_at: 2026-07-13T00:00:00-06:00
---

# L-059 - Reemplazo HH: limpiar estado sin existencia y rearmar la rama con stock al seleccionar candidato

## Regla operativa

En las pantallas de reemplazo de HH, el estado `ConExistencia` no debe tratarse como un valor permanente de la sesión. Es un indicador de la rama activa del flujo y debe volver a `true` cuando el operador selecciona un producto candidato para reemplazo.

Si el proceso venía de una confirmación de "sin existencia" o de "marcar dañado/no encontrado" sin stock, entonces la UI debe limpiar primero el estado anterior:

- `selitem = null`
- `selid = 0`
- `selidx = -1` cuando aplique
- `ConExistencia = false` solo en la ruta sin stock

Luego, al elegir un candidato válido, la pantalla debe rearmar explícitamente la rama con existencia antes de invocar el WS.

## Lo que se aprendió

1. La bandera de "sin existencia" puede quedar viva si solo se cambia de diálogo, aunque el usuario luego elija un producto para reemplazar.
2. Si no se resetea el estado anterior, el siguiente intento puede ir por la rama equivocada aunque el contexto de negocio ya sea de reemplazo con stock.
3. La limpieza del estado debe ser local y quirúrgica: no hay que reiniciar todo el flujo, solo la selección y la bandera que cambia la rama.
4. La rama con stock debe reactivarse al seleccionar una línea candidata, tanto en `frm_list_prod_reemplazo_picking` como en `frm_list_prod_reemplazo_verif`.

## Aplicación práctica

- Al entrar por "marcar dañado/no encontrado" sin selección, se limpia el candidato previo.
- Al elegir un producto para reemplazar, se restaura `ConExistencia = true`.
- La lógica de servidor sigue intacta; el cambio correcto está en la higiene de estado del cliente HH.

## Etiquetas durables

- `#EJC20260713_REEMPL_STATE_RESET`
- `#EJC20260713_REEMPL_CONEXISTENCIA_TRUE`

## Referencia rápida

- Picking: `C:\Users\carol\StudioProjects\TOMHH2025\app\src\main\java\com\dts\tom\Transacciones\Picking\frm_list_prod_reemplazo_picking.java`
- Verificación: `C:\Users\carol\StudioProjects\TOMHH2025\app\src\main\java\com\dts\tom\Transacciones\Verificacion\frm_list_prod_reemplazo_verif.java`
- Grafo/Handoff: `C:\Users\carol\Documents\wms-brain\brain\handoffs\2026-05-22-codex-performance-bof-hh\REEMPLAZO-HH-GRAFO-TRAZA-FINA-2026-05-26.yml`
