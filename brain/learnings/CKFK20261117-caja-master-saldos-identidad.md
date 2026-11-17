---
title: "CKFK: caja master necesita saldo e identidad por lote"
kind: decision
clients: [MAMPA_QA]
branches: [dev_2026_estable]
status: draft
validated_at: null
---

# Caja master: cantidades, identidad y recuperación

<!-- #CKFK20261117_REC_CM_APRENDIZAJE -->

Aprendizajes del [caso 28335](../handoffs/2026-11-17-ckfk-recepcion-28335/README.md),
con cierre comunicado por CKFK. Son criterios de revisión derivados de este caso,
no una certificación universal de todos los recorridos WMS.

1. Dos posiciones de una lista no implican dos objetos: mutar un detalle OC
   compartido puede convertir 4/3 en 3/3 o 4/4. Conservar datos originales y
   construir una copia por lote antes de modificar cantidades de captura.
2. Un detalle de compra puede tener varios lotes bajo una misma licencia.
   No usar producto/línea como identidad suficiente para asignar stock a una
   recepción. Correlacionar temporales únicos y después identities reales.
3. El reintento debe recibir **saldo**, no cantidad original, y revalidarlo en
   servidor bajo la misma transacción que actualiza recepción y contadores.
   La pantalla orienta al operador; el servidor debe rechazar snapshots obsoletos.
4. Una LP documental y la obligación de generar LP física son conceptos distintos.
   La actualización del lote no debe depender de fabricar una LP que los
   parámetros no requieren.
5. Arreglar el productor no repara las filas que ya quedaron inconsistentes.
   Si OC registra 6 de 7 y lotes registran 0/0, una pantalla 4/3 aún no representa
   una unidad pendiente. Conciliar lo recibido sin agregar unidades artificiales
   y completar el faltante por el proceso normal de HH.
6. Publicar un servicio y actualizar su cliente son comprobaciones distintas.
   Un campo XML conocido por el servidor puede faltar en otra rama de la HH.
   Verificar el contrato real con el parser; usar campos opcionales y valores
   predeterminados compatibles cuando el significado funcional lo permita.
7. Toda regla se activa por parámetros y condiciones del proceso; no por nombre,
   ID o alias de empresa. La firma del trabajo solicitado por Carolina es CKFK.

Procedencia y límites en `analysis/20261117-brain-pending/claims.json` y el handoff.
