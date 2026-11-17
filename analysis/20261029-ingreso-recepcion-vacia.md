# Eliminar ingreso con recepción sin detalle

#EJC20261029_OC_ELIMINAR_RECEPCION_VACIA

Rama: `codex/byb-unidad-medida`, checkout principal TOMWMS_BOF.

Regla confirmada por Carolina: cualquier fila de `trans_re_det` en cualquier recepción asociada impide eliminar el documento de ingreso. Cantidad cero no significa detalle vacío. Solo se permite cuando no hay filas de detalle. Se conserva la restricción previa del ingreso en estado NUEVA.

En esta rama, el evento del formulario y `clsLnTrans_oc_enc.Eliminar_OC` rechazaban encabezados asociados sin revisar detalle. No se cambió el significado general de `Tiene_Recepciones`, porque otros procesos lo utilizan.

## Cambios

- `TOMIMSV4/TOMIMSV4/Transacciones/Orden_Compra/frmOrdenCompra.vb`: quitar bloqueo por mera presencia de encabezado; DAL resuelve validación final tras confirmación.
- `TOMIMSV4/DAL/Transacciones/OrdenCompra/OC_Encabezado/clsLnTrans_oc_enc_Partial.vb`: estado actual validado bajo transacción SERIALIZABLE; bloqueo de encabezados/rangos de detalles; no excluir recepciones anuladas. Eliminar vínculos vacíos y, si no quedan otros ingresos vinculados, auxiliares de encabezado (operadores/imágenes/facturas) y encabezado vacío. Nunca borrar detalle, stock ni movimientos. Si hay otras dependencias con FK, la operación falla y se revierte.
- Conservar encabezados compartidos, sus vínculos con otros ingresos y auxiliares.
- Mover commit después de la bitácora para no utilizar una transacción ya finalizada ni informar fallo después de haber eliminado.

## Verificación

`analysis/test-ingreso-recepcion-vacia-20261029.ps1`: toma el SQL directamente del método corregido, cambia nombres a tablas temporales y lo ejecuta en SQL Server local, base tempdb, sin tocar tablas reales. Doce escenarios aprobados: sin recepción; vacía; anulada vacía; detalle positivo; detalle cero; anulada con detalle; compartida vacía; compartida con detalle; varias recepciones con una no vacía; estado cambiado; documento inexistente; rollback ante fallo posterior. Estas pruebas no sustituyen la prueba completa de interfaz ni la de concurrencia entre terminales.

`git diff --check` correcto; ambos archivos conservan UTF-8 BOM y CRLF. Los demás cambios previos de la rama se conservan. Sin commits, push, despliegue ni borrado de documentos reales.

Catálogo consultado: snapshot local db-brain de trans_re_enc, trans_re_oc y trans_re_det, incluidas referencias de trans_re_enc. Brain remoto ya había agotado timeout y no hay token de reindexación en esta sesión.

Compilación aislada de WMS y dependencias completada con exit 0, usando restauración desde paquetes locales. Salida: `analysis/ingreso-recepcion-vacia-build/WMS_PRD.exe`. Hay advertencias de dependencias/licenciamiento/obsolescencia; sin errores. Registro: `analysis/ingreso-recepcion-vacia-build.log`.
