# brain/debuged-cases — Bitácora viva por case-pointer

> **Propósito**: por cada `case-pointer` (`CP-NNN`) catalogado en `brain/dataway-analysis/07-correlacion-codigo-data/case-pointers/`, este folder mantiene una **bitácora viva** del progreso del debug: hipótesis, queries corridas, observaciones de Erik, decisiones, estado actual.
>
> Los case-pointers son la **estructura estática** del catálogo. Las bitácoras son el **historial dinámico** del trabajo de campo.

## Por qué dos folders

| Folder | Naturaleza | Update frequency | Propósito |
|---|---|---|---|
| `dataway-analysis/07-correlacion-codigo-data/case-pointers/` | Estática | Solo cuando se descubre nuevo evidence | Catálogo formal: dónde está, qué dice, qué significa |
| `brain/debuged-cases/` | Viva | Cada vez que se avanza el debug | Bitácora: qué probamos, qué encontramos, qué falta |

Si los mezclamos en un solo folder, el catálogo se contamina con state mutable y se vuelve imposible saber cuál es la vista canónica de "qué es CP-005" vs "cómo va el debug de CP-005".

## Convenciones

Ver `CONVENCION.yml` para el contrato formal. Resumen:

- Un archivo `CP-NNN.md` por case-pointer.
- Cada archivo tiene **frontmatter YAML** con `status`, `severidad_actual`, `fecha_apertura`, `fecha_ultimo_avance`.
- El cuerpo es **append-only**: nuevo avance se agrega al final con timestamp y autor (`@erik` / `@agent`). No se reescribe historia.
- Solo el **frontmatter** se sobreescribe (status, fecha_ultimo_avance).

## Estados (status)

| status | significado |
|---|---|
| `open` | Case-pointer documentado, debug no iniciado |
| `reproducing` | Estamos intentando reproducir el caso (corriendo queries, leyendo datos) |
| `confirmed` | Caso reproducido / evidencia clara — sabemos qué pasa |
| `solved` | Bug raíz arreglado en código; pendiente verificación post-deploy |
| `wont-fix` | Decisión consciente de no arreglar (con razón documentada) |
| `obsolete` | El case-pointer ya no aplica (refactor, deprecación, etc.) |

## Flujo típico

1. Wave detecta nuevo hardcode/comment/marker → se crea `CP-NNN.md` + `.yml` en `case-pointers/`
2. Se crea bitácora `brain/debuged-cases/CP-NNN.md` con `status: open` y la primera entrada
3. Se corren queries / Erik aporta contexto → se appendean entradas
4. Cambio de status (e.g. `open → reproducing → confirmed`) se hace editando frontmatter
5. Cuando se cierra el caso (`solved` / `wont-fix` / `obsolete`), se agrega entrada final con razón

## Cross-link obligatorio

Cada `CP-NNN.md` debe linkear:

- al `case-pointer` formal: `dataway-analysis/07-correlacion-codigo-data/case-pointers/NN-...md`
- a los `bugs` que confirma o refuta (si los hay): `dataway-analysis/.../V-DATAWAY-NNN.md`

## Estado inicial (esta wave)

Esta wave abre **7 bitácoras** (CP-001..CP-007), todas en `status: open` excepto CP-001 que viene de la wave 13-1 con análisis previo.

Ver `00-INDEX.md`.
