# Brain TOMWMS: entrada local vigente

<!-- #EJC20261027_BRAIN_GOVERNANCE -->

Este checkout contiene el conocimiento WMS. Resolver todas las rutas desde
la carpeta de este archivo; las rutas absolutas antiguas de Replit, OpenClaw
y `source/repos/wms-brain` son referencias históricas y deben comprobarse.

## Gobierno

- Las instrucciones del usuario tienen precedencia.
- No realizar commit/push sin autorización explícita de Erik.
- No modificar `Reference.vb`; preservar UTF-8 con BOM al editar VB.
- No mezclar HH y BOF en un mismo cambio/commit.
- Producción es solo lectura. No revelar secretos ni definiciones SQL sensibles.
- La gestión del brain es local. No usar bridge/OpenClaw ni `_inbox` como
  transporte activo. Replit puede aportar evidencia histórica; no es una
  dependencia obligatoria.
- #EJC20261028: por instrucción explícita de Erik, los repositorios fuente son
  solo para código y pruebas. No dejar allí trazas, notas, informes de auditoría
  ni otros archivos del Brain; guardarlos en este checkout de conocimiento.

- #EJC20261029: los scripts de diagnóstico, replay y validación operativa creados
  por el agente pertenecen al Brain, incluso si ejecutan pruebas del código.
  No ubicarlos en tools/ de BOF/HH ni asumir una excepción por llamarse Test-*.
  Conservar en los repositorios fuente únicamente los cambios propios del producto
  y sus pruebas de proyecto expresamente acordadas. No migrar archivos históricos
  versionados sin revisar su alcance con Erik.

- #EJC20261029: la orden de Erik `go sync` significa commit y push del Brain.
  Publicar los cambios del Brain correspondientes a la tarea, preservando cambios
  ajenos; no interpretarla como autorización de commit/push de BOF, HH ni DBA.

## Resolver conocimiento

1. Consultar `tools/brain-quality/workspace.json` y generar el catálogo local:
   `python tools/brain-quality/catalog.py --output analysis/brain-improvement/source-catalog.json`.
2. El catálogo cubre `brain/` y `wms-brain/brain/`. Ninguna carpeta gana
   automáticamente por antigüedad, fecha o profundidad.
3. `unique` o `byte-identical`: usar la fuente seleccionada.
4. `body-equivalent`: se comparte el cuerpo; conservar la procedencia y
   metadatos de ambas copias. La equivalencia no demuestra vigencia.
5. `unresolved`: leer ambas fuentes. Informar discrepancias aplicables;
   no presentar una de ellas como verdad consolidada. La política es
   `return-all-with-conflict` hasta revisión sustentada en evidencia.
   `additive-superset`: usar la copia que conserva todas las líneas de la
   otra en el mismo orden y agrega contenido. Conservar ambas procedencias;
   la contención textual no equivale a validación funcional ni de vigencia.
   `reviewed-alternatives`: existe una revisión ligada a hashes en
   `tools/brain-quality/source-reviews.json`. Seguir su política explícita,
   conservar ambas fuentes y comunicar el estado `disputed`. No significa
   que el conflicto funcional esté resuelto. Si cambian los bytes, la
   revisión deja de aplicar automáticamente. `exclude-derived` retira de
   consulta un índice generado, sin eliminarlo ni retirar sus documentos.
   `reviewed-selected` selecciona una fuente mediante revisión ligada a
   hashes, conserva las alternativas y no implica validación funcional.
6. Para tareas operativas cargar solo dominios, cliente y traza pertinentes.
   Comprobar rutas existentes antes de seguir enlaces de un router antiguo.
7. El commit del checkout identifica una línea base; los hashes identifican
   los bytes actuales. Un archivo local modificado no está validado por el
   simple hecho de declarar el SHA de HEAD.
8. Para seleccionar dominios ejecutar `python tools/brain-quality/resolve.py
   "<consulta con cliente y proceso>"`. Une ambos esquemas de router,
   conserva sus fuentes alternativas y reporta destinos faltantes. Si el
   catálogo cambió, regenerarlo antes de usarlo. Esta selección por triggers
   es navegación; la evaluación de búsqueda corresponde a las etapas 2 y 3.

## Afirmaciones nuevas

Registrar `id`, `kind` (`fact`, `hypothesis`, `decision`), `statement`,
`sources`, `clients`, `branches`, `validated_at` y `status`.
Cada fuente debe contener ruta, commit y SHA-256. Para código, comprobar
el commit del repositorio de código; no sustituirlo por el commit del brain.
No inferir cliente o rama universal cuando la evidencia tiene alcance limitado.
Las hipótesis conservan su condición hasta una verificación independiente.
Una decisión conserva su motivo y no demuestra por sí sola un hecho operativo.
Si no existe validación, usar `draft` y fecha nula; nunca inventar fechas.
Cuando una afirmación sustituye otra, registrar `supersedes` y conservarla.
Validar registros JSON con `python tools/brain-quality/provenance.py
<archivo.json>`. El validador comprueba hashes; para fuentes declaradas
verificadas y commiteadas también compara los bytes contra el commit.
Los documentos históricos sin estos registros continúan como
`legacy-unvalidated` o `disputed`, nunca como hechos actuales por defecto.
El catálogo registra alcance declarado y errores YAML por fuente. Un cliente
o rama ausente es desconocido, no un comodín universal; no completar esos
campos a partir de una similitud semántica. Una fecha declarada en el
documento no equivale a validación independiente.

## Evaluación reproducible

Los 25 casos de `tools/brain-quality/eval-cases.json` conservan hashes de
los handoffs consultados, preguntas, evidencia esperada, conceptos y rúbrica.
El split holdout debe mantenerse fuera de ajustes de recuperación.
Ejecutar `python tools/brain-quality/evaluate.py --output
analysis/brain-improvement/lexical-baseline.json` para la línea base léxica.
Esta reproduce el predicado AND del visor sobre documentos; no representa
el servicio remoto ni el razonamiento del modelo. Las métricas de diagnóstico,
dependencias, alcance y afirmaciones sin respaldo permanecen sin calificar
hasta disponer de respuestas generadas y revisión contra sus fuentes.
Comparación léxica: `python tools/brain-quality/evaluate.py --method bm25
--output analysis/brain-improvement/bm25-baseline.json`. Consulta local:
`python tools/brain-quality/retrieve.py "<consulta>" --client MHS
--branch dev_2028_merge`. Los filtros son estrictos sobre metadatos declarados;
pueden omitir evidencia útil cuyo alcance todavía no fue documentado.
BM25 es un candidato léxico; no presentarlo como búsqueda semántica o híbrida.
Entrada de consulta con evidencia: `python tools/brain-quality/brain.py
"<pregunta>"`. Devuelve rutas, hashes, alcance y estado de reconciliación.
BM25 es el método predeterminado medido. Los modos `semantic`, `hybrid` y
`hybrid-rerank` son experimentales hasta terminar sus comparaciones; requieren
el entorno de `requirements-semantic.txt` y un cache local indicado con
`--runtime` (y `--reranker-runtime` para reranking). Modelos y vectores viven
fuera del repositorio. El contenido del corpus se procesa localmente.
No ejecutar dos indexaciones simultáneas contra el mismo cache.
Para análisis profundo está disponible `--method bm25-rerank
--reranker-runtime <cache-reranker>`, sin requerir embeddings. La evaluación
documental mantuvo hit@5=0.96 y elevó MRR@5 de 0.88 a 0.94 sin regresión en
holdout; el costo observado es muy superior al de BM25. Consultar
`tools/brain-quality/search-policy.json` y los reportes antes de cambiar
el método predeterminado. Las variantes semántica e híbrida simple no
superaron BM25 en esta muestra; no promoverlas por su nombre o tecnología.
Para separar calidad de respuesta y calidad de búsqueda, preparar paquetes
con `python tools/brain-quality/prepare_answers.py <reporte-recuperacion.json>
--output <paquetes.json>`. Solo se incluyen los pasajes recuperados; el oráculo
queda fuera del paquete. La persona/agente que genere respuestas no debe
recibir el archivo de casos con sus respuestas esperadas. Validar las citas
y registrar revisiones con `answer_check.py`; los campos sin revisión deben
permanecer nulos. La coincidencia literal de una cita no demuestra que su
contenido respalde la conclusión.

## Grafo funcional de código

Regenerar con `tools/brain-quality/build-graph.ps1`; se pueden pasar
`-BofRoot`, `-HhRoot`, `-DbaRoot`, `-PythonPath` y `-SqlFiles` para otra estación.
Requiere .NET SDK 10 y `requirements-graph.txt`. No compila ni ejecuta WMS;
compila únicamente el extractor. No se conecta a SQL Server.
Consultar sin cargar todo el JSON al contexto:
`python tools/brain-quality/graph.py query
analysis/brain-improvement/functional-graph.json
clsLnTransUbicSugerida2.Get_Preload_Tramo_Ubicaciones --depth 2`.
`--direction impact` recorre hacia llamadores/referencias entrantes.
La consulta comprueba hashes e inventario de fuentes antes de devolver datos.

El grafo contiene relaciones posibles observadas en sintaxis. Conserva
sobrecargas ambiguas y referencias SQL candidatas; no equivale a binding
semántico completo, ejecución observada ni despliegue. No enumera todas las
llamadas a instancias/framework ni SQL dinámico. Condiciones MSBuild,
preprocesador y variantes Gradle no están evaluadas. El inventario SQL contiene
593 archivos: 497 parseados y 96 con errores aislados, sin relaciones derivadas.
No representa el esquema desplegado. Un ALTER TABLE identifica su destino,
sin reconstruir todo el esquema. Los errores de parseo se reportan y esas
fuentes SQL no aportan relaciones; no corregir scripts productivos como
efecto lateral de reconstruir este grafo.

## Especialistas experimentales

Consultar `tools/brain-quality/specialist-contracts.json` y generar contratos
con `agent_workflow.py`. La autorización para usar especialistas debe provenir
del usuario o de instrucciones aplicables; el script no crea agentes.
El flujo tiene tres especialistas, un coordinador y un verificador, una ronda
acotada de evidencia adicional y una de corrección. Mantener fuentes, alcance,
abstenciones y discrepancias. La comparación de 25 casos mejoró los promedios,
pero perdió completitud en holdout: no adoptarlo automáticamente. Los costos
totales de inferencia no fueron medidos. Ver `analysis/brain-improvement/answer-comparison.json`.

## Plan activo

1. Gobernanza, rutas, procedencia y resolución de divergencias.
2. Línea base con 20–30 casos reales y métricas de calidad/tiempo/costo.
3. Búsqueda híbrida, filtros, reranking e índice incremental verificable.
4. Dependencias funcionales HH → WS → DAL → SP → tabla → regla.
5. Agentes especializados evaluados frente a la línea base.

No declarar una etapa terminada solo por disponer de un script o un test.
Las fuentes, resultados de evaluación y limitaciones deben ser revisables.
