# Traslados con bodega intermedia – Solicitud final idempotente y reintentos

Contexto
- Interfaces: Killios y La Cumbre (posible en otras; no reportadas aún).
- Método actual: `Enviar_Traslado_Desde_Solicitud`.
- Problema: el traslado inicial se crea bien; falla intermitente la generación de la nueva Solicitud de Traslado cuando el destino es una bodega intermedia y debe dispararse la solicitud hacia la bodega final. No existe reintento aislado de la solicitud final (el traslado ya existe), lo que fuerza debugging ad hoc.

Objetivo
- Hacer la fase de “Solicitud de Traslado a bodega final” idempotente, reintentable y observable, separando estados por fase.

Alcance
- Orquestación de dos fases (traslado inicial → solicitud final si intermedia).
- Persistencia de estados y correlación.
- Reintentos seguros (sin duplicados) y telemetría.
- Compatibilidad con Killios y La Cumbre.

Entradas clave
- Cliente (código), IdPedidoEnc.
- `vBodega_Destino` / bodega final (cuando existe intermedia).
- `DocEntryInicial` (resultado de traslado inicial en SAP).
- Conexión/companías SAP/WMS (oCompany, etc.).

Salidas
- `DocEntryInicial` y `DocEntrySolicitudFinal` (si aplica).
- Estados por fase hasta `ENVIADO_OK`.

Diseño propuesto
1) Fases separadas e idempotentes
- Fase 1: Crear traslado inicial en SAP → `DocEntryInicial`.
- Fase 2 (solo si intermedia): Crear Solicitud de Traslado a bodega final con `idempotency_key = Hash(DocEntryInicial + bodega_final)`.
- Antes de crear en fase 2, consultar por correlación en SAP/índice local; si ya existe, sincronizar en WMS y no duplicar.

2) Máquina de estados en WMS
- `TRASLADO_INICIAL_CREADO (DocEntryInicial != null)`
- `SOLICITUD_FINAL_PENDIENTE (si requiere intermedia)`
- `SOLICITUD_FINAL_CREADA (DocEntrySolicitudFinal != null)`
- `ENVIADO_OK (flujo completo)`
- Evitar una sola `bandera_enviado` global hasta completar todas las fases aplicables.

3) Persistencia y outbox transaccional
- Tabla/colección (o extender la existente) tipo `i_nav_transacciones_out` con campos: `correlation_id`, `doc_entry_inicial`, `doc_entry_solicitud_final`, `estado`, `idempotency_key_fase2`, `intentos_fase2`, `ultimo_error_fase2`, `dt_ultimo_intento`, `origen_interfaz` (Killios/La Cumbre).

4) Reintento seguro
- Comando/servicio: `ReintentarSolicitudFinal(correlation_id | doc_entry_inicial)`.
- Flujo: buscar si ya existe solicitud final; si existe, sincronizar; si no, crear y actualizar estado.

5) Telemetría y feature flags
- Métricas: tasa de éxito por fase, duplicados prevenidos, latencia, reintentos.
- Feature flag por interfaz para aislar incidentes.

6) Criterios de aceptación
- Reintentos múltiples no crean más de una solicitud final (idempotencia verificada).
- Casos con y sin bodega intermedia cubiertos.
- Estados reflejan con precisión el progreso por fase.
- Posibilidad de reprocesar historiales “huérfanos” (traslado creado sin solicitud final) sin duplicados.

Plan de implementación mínimo seguro
- Introducir estados por fase en el modelo WMS; migración con valores por defecto.
- Encapsular Fase 2 en `IntentarSolicitudFinalIdempotente()`.
- Añadir búsqueda previa por `idempotency_key`/referencia en SAP.
- Exponer comando/batch de reintentos para pendientes.
- Instrumentar logs/metrics con `correlation_id`.

Palabras clave (para búsqueda en código/brain)
- `Enviar_Traslado_Desde_Solicitud`, `Enviar_Solicitud_Traslado_SAP`, `vBodega_Destino`, `bodega intermedia`, `i_nav_transacciones_out`, `Actualizar_Bandera_Enviado`, `Killios`, `La Cumbre`.

Referencias en wms-brain
- Outbox y tablas: README.md (sección "outbox NAV/SAP").
- `i_nav_transacciones_out`: 
  - analysis/passada-3-2-bof-completo.md
  - analysis/passada-3-2-killios-profundo.md
  - brain/brain-map/tablas-por-funcionalidad.md
  - brain/clients/becofarma*.md
- `Actualizar_Bandera_Enviado`:
  - brain/code-deep-flow/traza-003-sapsyncmampa-interface.md
  - data/passada-3-2-bof/dal-completo.json

Notas
- No se automatiza reenvío de fase 1 cuando ya existe el traslado; el reintento se limita a la fase 2 cuando aplique intermedia.
- Mantener compatibilidad binaria del contrato público; los estados nuevos deben ser back-compatible.
