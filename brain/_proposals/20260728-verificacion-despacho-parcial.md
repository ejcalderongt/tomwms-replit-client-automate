---
id: 20260728-verificacion-despacho-parcial
tipo: business-rule
estado: aplicado-local
ramas: [dev_2026_estable]
tags: [verificacion, packing, picking, despacho-parcial, la-cumbre, "#EJC20260728"]
---

# Verificacion de pedidos con despacho parcial

## Regla

`trans_pe_tipo.permitir_despacho_parcial` define si un pedido puede finalizar
la verificacion aunque conserve unidades pendientes de picking o de
verificacion.

- Si el tipo permite despacho parcial, esos pendientes no bloquean el cierre.
- Si el tipo no permite despacho parcial, ambos pendientes siguen bloqueando.
- `LineasInvalidas` siempre bloquea, independientemente de la parametrizacion.

En packing, el cierre depende exclusivamente de `PendientePacking`. Si el
pendiente es cero (con tolerancia tecnica de `0.000001`), debe continuar. Los
pendientes de picking y verificacion, `LineasInvalidas` y otras comparaciones
del reconciliador no participan en este guard.

## Trazabilidad

- HH: `frm_detalle_tareas_verificacion.java`
- WebMethod: `Actualizar_PickingEnc_Verificado` /
  `Set_Estado_Pedido_Verificado`
- Guard: `Validar_Flujo_Para_Finalizar_Verificacion`
- Packing: `Actualizar_Estado_Packing`
- Parametro: `trans_pe_tipo.permitir_despacho_parcial`

## Cambio local

`#EJC20260728_VERIFICACION_PARCIAL` consulta el tipo de cada pedido reconciliado
antes de aplicar el bloqueo por cantidades pendientes. No cambia el contrato
HH-BOF y no requiere nueva APK.

`#EJC20260728_PACKING_PENDIENTE` bloquea solamente cuando
`PendientePacking > 0.000001`.
