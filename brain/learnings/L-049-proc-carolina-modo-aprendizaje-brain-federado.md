# L-049 - PROC: Carolina opera en modo aprendizaje para alimentar el brain federado

> Etiqueta: `L-049_PROC_CAROLINA_MODO-APRENDIZAJE_BRAIN-FEDERADO`
> Fecha: 13-jun-2026
> Origen: instruccion Carolina en workspace `wms-brain`

## Hallazgo

En el espacio de Carolina, el agente debe funcionar en **modo aprendizaje**: cuando detecte trazas utiles, validaciones, soportes, descubrimientos de datos, columnas, comprension de escenarios o analisis de datos, debe decidir si corresponde alimentar el brain con un registro estructurado.

El objetivo evolutivo es que `wms-brain-federado` pueda contener eventualmente un agente/brain especializado: **`wms-brain-carolina`**, con sus archivos, contexto, subagentes y conocimiento acumulado, como base para llegar a un agente implementable con Agents SDK.

## Regla operativa

1. Mantener separacion estricta entre brain/conocimiento y repositorios de codigo fuente.
2. Registrar descubrimientos de forma atomica, trazable y reutilizable.
3. Priorizar entradas cuando haya evidencia nueva, validacion de Carolina, soporte de trazas, datos observados, columnas/tablas identificadas, escenarios entendidos o riesgos operativos.
4. No guardar secretos, passwords ni connection strings completos.
5. Distinguir siempre entre:
   - dato observado;
   - inferencia del agente;
   - validacion de Carolina;
   - pregunta abierta.
6. En este contexto de MAMPA, cuando una mejora toque rendimiento, UI o trazabilidad, dejar tags inline breves en el codigo con el formato `CKFKYYMMDDFeature` y reflejar el hallazgo en brain.

## Formato sugerido para nuevos aprendizajes

Cada aprendizaje nuevo debe incluir, cuando aplique:

- **Origen**: conversacion, archivo, traza, query, pantalla, repo, modulo o cliente.
- **Evidencia**: fragmento breve o referencia verificable.
- **Hallazgo**: conclusion puntual.
- **Implicaciones**: impacto en WMS, HH, WebAPI, datos, soporte o migracion.
- **Vinculos**: archivos, tablas, stored procedures, pantallas, issues, aprendizajes relacionados.
- **Q abiertas**: dudas para Carolina, Erik u otro agente.
- **Confianza**: observada, inferida, validada o pendiente.

## Destinos preferidos

- `brain/learnings/L-###-*.md`: aprendizajes atomicos y decisiones operativas.
- `brain/data-deep-dive/`: analisis de datos mas extensos.
- `brain/sql-catalog/`: conocimiento estructural sobre objetos SQL cuando no corresponda al db-brain externo.
- `brain/wms-specific-process-flow/`: flujos funcionales y preguntas por escenario.
- `brain/agents/` y `brain/skills/`: material reusable para agentes/subagentes futuros.

## Implicaciones

Este aprendizaje convierte la captura de conocimiento en una responsabilidad activa del agente. No todo hallazgo requiere archivo nuevo, pero cuando el hallazgo sea durable, reutilizable o reduzca incertidumbre futura, debe quedar registrado en el brain de forma estructurada.

## Q abiertas

- Q-CAROLINA-BRAIN-NOMBRE: confirmar si el paquete futuro se llamara formalmente `wms-brain-carolina`.
- Q-CAROLINA-AGENTS-SDK: definir criterios minimos para promover aprendizajes a skills, subagentes o tools de Agents SDK.
- Q-CAROLINA-CURACION: definir si los aprendizajes capturados por Codex requieren estado `draft`, `validado` o `ratificado`.
