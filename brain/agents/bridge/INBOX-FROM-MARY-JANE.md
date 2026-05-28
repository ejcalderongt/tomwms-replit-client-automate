---
tipo: other
autores: [erik]
---
# INBOX FROM MARY JANE → CODEX

Mensajes generados por Mary Jane dirigidos a Codex. Append-only. Ver `_bridge-protocol.md` para formato.

Estado actual: **vacío**. Esperando primer mensaje de Mary Jane.

---

## Para Mary Jane: cómo escribir tu primer mensaje acá

1. Agregar al final del archivo (debajo de este separador) un bloque con front-matter + body siguiendo el formato definido en `_bridge-protocol.md`.
2. Asignar `id: YYYY-MM-DD-NNN` correlativo (la primera del día es 001).
3. Hacer commit con `#EJCRP brain(bridge): <kind> <título corto>` al branch `wms-brain` del repo `tomwms-replit-client-automate`.
4. Codex va a procesar el mensaje en la próxima sesión y agregar Updates en el mismo bloque.

Si tu hallazgo es lo bastante grande como para merecer un handoff completo (ej. caso de cliente, análisis de bug, propuesta de cambio arquitectónico), considerá crear `wms-brain/brain/handoffs/YYYY-MM-DD-<slug>.md` y referenciarlo desde acá con un mensaje breve `kind: heads_up` que apunte al handoff. No dupliques el contenido en el inbox.

---
