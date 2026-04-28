---
output_type: convencion
audience: agente-brain + Erik + futuros mantenedores
version: V1
status: ratificado
authored_by: agente-brain
authored_at: 2026-04-28T22:00:00-03:00
ratificado_por: Erik Calderon (PrograX24)
ratificado_at: 2026-04-28T22:00:00-03:00
---

# Nomenclatura del trabajo del agente brain

## Metafora rectora: el procesador

El agente brain trabaja como un **procesador**. El procesador:
- Recibe trabajos (consultas, hallazgos, requests).
- Organiza el trabajo en **ciclos** (unidad mayor, planificada y cerrada).
- Cada ciclo se descompone y delega en **tareas** (unidad menor, paralelizable o secuencial).

```
Procesador (agente brain)
   |
   +-- Ciclo N      (objetivo, hallazgos, decisiones, consolidacion)
   |     +-- Tarea 1  (lote de respuestas, batch de queries, sub-investigacion)
   |     +-- Tarea 2
   |     +-- Tarea 3
   |
   +-- Ciclo N+1
         +-- Tarea 1
         +-- Tarea 2
```

## Definiciones formales

### Ciclo

Unidad mayor de trabajo del agente brain. Tiene:
- **Numero y opcionalmente letra** (Ciclo 7, Ciclo 8a, Ciclo 8b).
- **Foco tematico unico** declarado al inicio.
- **Hallazgos H##** producidos durante la ejecucion.
- **Documentos asociados**: `consolidacion-ciclo-N.md`, `respuestas-ciclo-N.md`, `preguntas-ciclo-N.md`, `queries-ciclo-N.md`, `encargo-sql-agente-ciclo-N.md`.
- **Cierre formal** mediante el documento `consolidacion-ciclo-N.md`.

Sustituye al termino antiguo "pasada" (deprecado el 2026-04-28).

### Tarea

Unidad menor dentro de un ciclo. Es un lote o sub-bloque procesado de una vez. Tiene:
- **Numero secuencial dentro del ciclo** (Tarea 1, Tarea 2, ...).
- **Documento asociado tipico**: `respuestas-tarea-N.md` (lote de respuestas de Erik a un grupo de preguntas del agente).
- **No requiere cierre formal propio** - se consolida dentro del ciclo padre.

Sustituye al termino antiguo "tanda" (deprecado el 2026-04-28).

## Convencion de nombres de archivo

| Patron | Ejemplo | Vive en |
|---|---|---|
| `consolidacion-ciclo-N.md` | `consolidacion-ciclo-8a.md` | `outputs/consolidaciones-ciclo/` |
| `respuestas-ciclo-N.md` | `respuestas-ciclo-7.md` | `outputs/consolidaciones-ciclo/` |
| `respuestas-tarea-N.md` | `respuestas-tarea-3.md` | `outputs/consolidaciones-ciclo/` |
| `preguntas-ciclo-N.md` | `preguntas-ciclo-7.md` | `wms-specific-process-flow/` |
| `queries-ciclo-N.md` | `queries-ciclo-8a.md` | `wms-specific-process-flow/` |
| `encargo-sql-agente-ciclo-N.md` | `encargo-sql-agente-ciclo-8a.md` | `wms-specific-process-flow/` |

## Identificadores en texto

- **Ciclos** se escriben capitalizados: `Ciclo 7`, `Ciclo 8a`. Nunca "ciclo 7" en titulos o referencias formales.
- **Tareas** se escriben con numero secuencial: `Tarea 1 del Ciclo 8a`.
- **Hallazgos** mantienen su prefijo `H##` y se asocian al ciclo: `H06 (Ciclo 8a)`.
- **Queries** dentro de un ciclo se enumeran como `Q-CICLO-NN-MM`: ejemplo `Q-CICLO-8A-01`.

## Por que se cambio (deprecacion 2026-04-28)

- "Pasada" sugeria una sola lectura/recorrido y se prestaba a confundirse con el adjetivo cotidiano ("la semana pasada").
- "Tanda" tenia connotacion mas de batch industrial / cervecero, no de unidad de trabajo en un sistema.
- La metafora del procesador (ciclos + tareas + delegacion a cores) modela mejor como el agente realmente opera: planifica, divide, ejecuta en bloques, consolida.


## Excepciones documentadas (artefactos legacy)

Los archivos en `analysis/passada-*.md` y `data/passada-*.json` (con doble S, typo historico) NO se renombran. Son artefactos de un analisis anterior del WMS y se conservan tal como estan por dos razones:
1. Estan fuera de la carpeta `brain/` y no son producidos por el agente brain actual.
2. Renombrarlos rompe referencias en scripts externos que los leen por nombre.

Si se referencian desde docs nuevos del agente, se citan textualmente con su nombre original entre comillas, dejando claro que es legacy: por ejemplo "ver `analysis/passada-3-1-bloque-A` (legacy)".

## Vigencia

Esta convencion es retroactiva: se aplico rename masivo en commit del 2026-04-28 sobre todo el contenido y los nombres de archivo de los repos `wms-brain` y `wms-brain-client`. Los terminos "pasada" y "tanda" no deben aparecer mas en documentos nuevos del agente.

## Referencias

- Commit del rename masivo: HEAD de `wms-brain` rama `wms-brain` al 2026-04-28.
- Carpeta de outputs: `brain/outputs/README.md`.
- Decision conversacional: chat agente <-> Erik, 2026-04-28.
