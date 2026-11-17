# Corrección Cumbre — 2026-10-29
Rama: dev_2026_estable_prd. Sin commit ni despliegue.
Archivo: SAPSYNCCUMBRE/Clases Interface Sync/Solicitud_Traslado/clsSyncSAPSSolicitudTraslado.vb
Método: Enviar_Traslado_Desde_Solicitud_SAP.
Se conserva Actualizar_Progreso con mensaje completo en RichTextBox y log de ejecución; se elimina XtraMessageBox y se devuelve False explícito para permitir que el For Each de Enviar_Traslados_Desde_Solicitud continúe.
Se suma cada cantidad convertida a Decimal y se comparan ambos valores a seis posiciones. Se conserva el rechazo de excesos reales. La comparación anterior usaba suma Double sin normalización: posible falso exceso aunque se visualice 60.94 en ambos lados. No se han consultado valores internos de producción; causa numérica compatible con la evidencia, pendiente de confirmar con el pedido real.
Rollback, documentos, marcado de enviados y ruta de éxito sin cambios.
Validación: compilación Release con dependencias existentes correcta. Salida aislada tmp/cumbre-fix/bin/SAPBOSyncCumbre.exe. git diff --check correcto. Casos numéricos: igualdad con ruido, parcial, exceso de 0.000001 y exceso de 0.06 correctos.
Pendiente: validar en SAP el pedido 10332 / despacho 13308 y continuidad del lote de tres pedidos. No se ejecutaron envíos ni se reemplazó el ejecutable instalado.
