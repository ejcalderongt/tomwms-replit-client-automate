# Scripts de salida en vivo MHS

Scripts preparados contra el inventario real de `TOMWMS_MHS_DEV` consultado el 2026-09-14.

## Archivos

1. `01_inicializar_identity_transaccionales.sql`
   - Sincroniza las columnas `IDENTITY` de tablas transaccionales con el `MAX(id)` cargado.
   - Incluye stock, recepción, pedido, picking, movimientos, inventario, licencias e interfaces operativas.
   - Excluye catálogos, agenda, notificaciones, tablas del sistema y tablas `log_error_*`.

2. `02_reiniciar_correlativos_licencias_BOF_HH.sql`
   - Reinicia a cero todas las resoluciones BOF de `resolucion_lp_usuario`.
   - Reinicia a cero todas las resoluciones HH de `resolucion_lp_operador`.

## Seguridad

Ambos scripts arrancan con `@Aplicar = 0`; en ese modo solo muestran el diagnóstico.
Cambiar a `@Aplicar = 1` únicamente durante la ventana de salida en vivo, con aplicaciones detenidas y respaldo disponible.

Los scripts no contienen servidor, usuario ni contraseña.
