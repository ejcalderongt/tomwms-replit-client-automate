# Historial de versiones

## 2.0.0 — 2026-10-27

- Gobierno local y catálogo de fuentes con hashes, procedencia, alcance y revisiones de divergencias que caducan al cambiar sus fuentes.
- Batería de 25 incidentes documentados, separando recuperación de evidencia y evaluación de respuestas; 20 casos de desarrollo y cinco reservados.
- BM25 como búsqueda predeterminada y reranking local opcional. MRR@5 pasó de 0.88 a 0.94, manteniendo hit@5 de 0.96. Semántica e híbrida permanecen experimentales.
- Caché incremental de embeddings corregido, con comprobación de compatibilidad de modelo y proveedor.
- Extractores de sintaxis VB, Java y SQL, grafo de dependencias e impacto y candidatos de reglas ligados a fuentes. No representa ejecución ni despliegue; 96 de 593 scripts SQL quedaron aislados por errores de parseo.
- Contratos y evaluación de especialistas de código, SQL, funcional, coordinación y verificación. Uso experimental y opcional: mejor promedio, pero regresión de completitud en los casos reservados.
- Skill operativa con resolución de rutas actuales, instalación con respaldo e integración de las herramientas.

Validación: 38 pruebas automatizadas, skill válida y citas comprobadas para las 25 respuestas finales. La revisión semántica fue realizada por el mismo modelo en contexto independiente; los resultados corregidos son posteriores a feedback. No se dispone de costo total fiable de inferencia.

El resumen publicable está en `analysis/brain-improvement/release-2.0.0-summary.json`. Los extractos, índices, grafos y paquetes de evaluación quedan locales. Parte de los handoffs usados en el experimento no se incluye en esta versión; sus hashes permiten detectar fuentes ausentes o modificadas. Un clon nuevo por sí solo no reproduce todas las mediciones. No se incluyen cambios de código productivo.
