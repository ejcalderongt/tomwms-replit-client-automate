# Patterns — INDEX

> Patrones meta que emergen al observar varios case-pointers juntos. Cada pattern es un molde repetido; los case-pointers son las instancias.

## Por qué patterns

Cuando dos o más case-pointers comparten **forma estructural** (no contenido), estamos viendo un **estilo de programación** o **estilo de debugging** repetido por uno o varios actores históricos. Documentar el pattern:

1. Comprime el inventario (en vez de N case-pointers similares, hay 1 pattern + N instancias).
2. Permite predecir dónde aparecerán nuevas instancias (búsquedas dirigidas).
3. Identifica firmas de autoría (cada autor tiene su pattern preferido).
4. Facilita refactor sistemático (resolver N instancias del mismo pattern de la misma manera).

## Inventario actual

| ID | Nombre | Instancias | Severidad agregada | Documento |
|---|---|---|---|---|
| `P-001` | Breakpoint arqueológico con código hardcoded | CP-001, CP-009, CP-010, CP-011, CP-012 | media | [`breakpoint-arqueologico-codigo-hardcoded.md`](./breakpoint-arqueologico-codigo-hardcoded.md) |

## Patterns candidatos para próximas waves

- **P-002 (candidato)**: intención abandonada con scaffolding nulo (guards `If X = 0 Then X = 0 End If`). Visto en CP-012. Requiere búsqueda dirigida.
- **P-003 (candidato)**: bloque copy-pasted entre reportes hermanos con bug propagado (visto en CP-008 con el bloque `M.Serie = "#EJCAJUSTEDESFASE"` en 3 reportes). Si aparece en otros lugares, vale formalizarlo.
- **P-004 (candidato)**: label GoTo firmada con timestamp `<INICIALES>_<YYYYMMDDHHMM>_<DESCRIPCION>:` (visto en CP-010, label `EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING:`). Es una **firma de bloque** que documenta el momento del refactor — es señal positiva, no deuda. Vale formalizar como **convención de autoría** además de pattern.

## Promoción de pattern

Para promover un candidato a pattern formal:

1. Al menos 2 instancias confirmadas en el repo.
2. Forma estructural reconocible (no solo "cosas similares").
3. Documento dedicado con: definición, plantilla canónica, instancias, hipótesis de origen/autoría, recomendación de tratamiento.
