# Herramientas del brain 2.0

Ejecutar desde la raíz del repositorio. Leer primero `AGENTS.md`.
Usar Python con las dependencias de `requirements.txt`; para modelos locales,
agregar `requirements-semantic.txt`. Los modelos y cachés deben vivir fuera
del repositorio. `source-roots.json` configura las rutas de los repositorios
autorizados; no contiene conexiones a bases de datos.

```powershell
python tools/brain-quality/catalog.py --output analysis/brain-improvement/source-catalog.json
python tools/brain-quality/brain.py "pregunta operativa"
python -m unittest discover -s tools/brain-quality -p 'test_*.py'
python tools/brain-quality/evaluate.py --method bm25 --output analysis/brain-improvement/current-bm25-regression.json
```

El grafo requiere .NET SDK 10 y `requirements-graph.txt`; regenerar con
`build-graph.ps1` y consultar con `graph.py query --help`. Solo se compila
el extractor. No se ejecuta WMS ni se conecta a SQL Server.

`search-policy.json` conserva las decisiones de adopción. Los contratos de
`specialist-contracts.json` y `agent_workflow.py` son experimentales y no
crean agentes ni otorgan autorización por sí mismos.

Los reportes completos e índices son locales y regenerables. La evaluación
congela hashes de fuentes; algunas evidencias pertenecen a handoffs locales
no incluidos en esta versión. No cambiar los casos reservados para forzar
una mejora. Consultar el resumen publicable y los límites en `CHANGELOG.md`.
