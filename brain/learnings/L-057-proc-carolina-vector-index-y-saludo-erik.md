# L-057 - PROC: Carolina, Erik y el brain con indexado vectorial

> Etiqueta: `L-057_PROC_CAROLINA_VECTOR-INDEX_SALUDO-ERIK`
> Fecha: 02-jul-2026
> Origen: instruccion de Erik en workspace `wms-brain`, via Carolina

## Hallazgo

Erik saluda a Carolina y confirma que el conocimiento nuevo descubierto al trabajar con ella debe mapearse en el brain, pero ahora considerando la estructura actualizada que incorpora vectores para mejorar indexado y busqueda.

La regla nueva no reemplaza `brain/learnings/`; agrega una capa de descubrimiento semantico. El conocimiento durable debe seguir registrandose como aprendizaje atomico, pero tambien debe quedar conectado a los entrypoints que el indexador vectorial recorre.

## Regla operativa

1. Cuando Carolina pida integrar conocimiento nuevo, primero hacer `git fetch` y sincronizar el repo antes de escribir, porque el brain federado y los indices pueden haber cambiado.
2. Preservar cambios locales con stash/merge o flujo equivalente antes de integrar remoto.
3. Registrar el aprendizaje atomico en `brain/learnings/L-###-*.md`.
4. Si el aprendizaje debe ser encontrable por busqueda semantica, conectarlo tambien dentro de `wms-brain/brain/...`, especialmente en `atlas`, `handoffs`, `code-changes` o `learnings` anidado segun corresponda.
5. Distinguir siempre: instruccion de Erik/Carolina, evidencia observada en repo, inferencia del agente y pregunta abierta.
6. No ejecutar indexado vectorial si no hay endpoint local de embeddings disponible; dejarlo como validacion pendiente, no como fallo del aprendizaje.

## Evidencia

- El repo remoto `origin/wms-brain` incorporo `wms-brain/tools/wms-embeddings/`.
- `wms-brain/tools/wms-embeddings/README.md` define index, query y export-html.
- `wms-brain/brain/atlas/index.yml` centraliza discovery y carga selectiva de paquetes.
- `brain/learnings/L-049-proc-carolina-modo-aprendizaje-brain-federado.md` ya define el modo aprendizaje de Carolina.

## Implicaciones

- Los aprendizajes de Carolina deben escribirse para dos consumidores:
  - humanos/agentes que leen `brain/learnings`;
  - buscador semantico que chunkifica paquetes bajo `wms-brain/brain`.
- Una entrada atomica sin enlace al atlas o al brain anidado puede quedar menos visible para busqueda vectorial.
- La sincronizacion con GitHub debe ser parte del ritual antes de curar conocimiento nuevo.

## Vinculos

- `brain/learnings/L-049-proc-carolina-modo-aprendizaje-brain-federado.md`
- `wms-brain/tools/wms-embeddings/README.md`
- `wms-brain/brain/atlas/index.yml`
- `wms-brain/brain/learnings/contexto-reutilizable-TOMWMS.md`

## Q abiertas

- Q-CAROLINA-VECTOR-CANON: confirmar si `brain/learnings` seguira siendo canon humano y `wms-brain/brain/learnings` sera el espejo vectorizable, o si debe migrarse todo al arbol anidado.
- Q-ERIK-EMBEDDINGS-ENDPOINT: confirmar endpoint/modelo local esperado para regenerar indices vectoriales.

## Confianza

Alta en la instruccion y en la evidencia observada en repo. Pendiente la politica canonica final entre `brain/` raiz y `wms-brain/brain/` anidado.
