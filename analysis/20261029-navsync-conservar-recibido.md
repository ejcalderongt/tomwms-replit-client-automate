# NAV: conservar recibido al reimportar ingresos

#EJC20261029_NAVSYNC_CONSERVAR_RECIBIDO

Rama: codex/byb-unidad-medida. Checkout: C:/Users/carol/source/repos/TOMWMS_BOF.

## Causa y comparación
Las rutas de actualización de detalle existente en compras, devoluciones y una ruta de traslados asignaban Quantity_Received directamente a Cantidad_recibida. Actualizar_Desde_Interface persiste ese valor. El mismo riesgo existe en dev_2023_estable; no se demostró que esta actualización introdujera la regresión. La protección #EJC20220420 existe en rutas hermanas pero no en estas tres.

## Corrección
En clsSyncNavPedidoCompra.vb (955), clsSyncNavDevolucionVenta.vb (987) y clsSyncNavPedidoTraslado.vb (1233), aceptar Quantity_Received solo cuando supera Cantidad_recibida cargada del detalle existente. Se mantienen las actualizaciones de cantidad solicitada, costo y demás campos, y las inserciones nuevas. No se cambia la DAL compartida ni los procesos de anulación/reversión de recepción.

## Validación
- Build NavSync Debug/AnyCPU y dependencias: exit 0. Registro: analysis/navsync-conservar-build.log.
- Revisión del diff: tres sustituciones, exclusivamente en ramas de detalle existente. Cotejo con la protección existente en rutas hermanas.
- git diff --check: OK. UTF-8 BOM y CRLF: OK.
- Catálogo consultado: db-brain/tables/trans_oc_det.md.
- Sin cambios de esquema, datos, commits, push ni despliegue.

## Prueba funcional pendiente
En ambiente de pruebas, para cada tipo (compra, devolución, traslado), reimportar una línea con 8 recibidas enviando desde NAV 0, 3, 8 y 10: resultados esperados 8, 8, 8 y 10. Verificar que los cambios de cantidad solicitada y costo se actualicen y que una línea nueva se inserte normalmente. No se ejecutó prueba integrada SQL/NAV.

## Límite
La protección compara contra el valor cargado por el importador; no introduce sincronización adicional con recepciones concurrentes. No reconstruye cantidades históricas ya sobrescritas. Brain remoto no reindexado en esta ejecución.