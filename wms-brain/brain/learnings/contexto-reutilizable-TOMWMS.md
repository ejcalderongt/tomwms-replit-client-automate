# Contexto reutilizable, agentes y skills en TOMWMS

## 1. Archivos de contexto y reglas de agentes

- `.cursorrules`, `AGENTS.md`, `CLAUDE.md`, `CONVENTIONS.md`
  - Definen reglas duras, workflows, y cómo operar como agente técnico en TOMWMS.
  - Instrucciones para uso del Brain, manejo de secretos, encoding, y flujos de trabajo para cambios en SQL, VB, Java HH.

## 2. Skills y herramientas reutilizables

- **TOMWMS Brain API** (repositorio local: `C:\Users\yejc2\source\repos\wms-brain`)
  - El Brain es el indexador y coordinador de contexto cross-repo (VB, Java HH, SQL).
  - La capa "brain federado" debe entenderse como una federación de overlays locales y dominios curados, no como un bridge runtime activo.
  - La federación se materializa con `brain/agents/_index.yml`, `brain/agents/coordinator.yml`, `brain/atlas/index.yml` y los `domain-*.yml`/`client-*.yml` que correspondan al caso.
  - Endpoints GET: `/search`, `/impact`, `/dependencies`, `/writers`
  - Endpoints POST: `/import/sql-catalog`, `/index/vb`, `/index/java`, `/repos/sync`
  - Scripts para extracción y subida de catálogo SQL: `tools/sql-catalog/extract_sql_catalog.py`
  - Workflows detallados para cambios en SPs, métodos VB, renames de WebMethods, análisis de blast radius y dependencias.
  - Para integración federada, se consulta primero el router/coordinator local y luego se amplía solo con los dominios y clientes relevantes. Eso permite obtener relaciones y dependencias entre TOMWMS y otros sistemas conectados sin cargar todo el árbol.

## 3. Contexto incremental y traza viva

- `codex-context-mi3-di-estatus.yml`
  - Contexto incremental para cambios recientes en API/MI3.
  - Debe leerse y actualizarse con cada sesión.

- Archivos de traza fina y runbooks operativos (referenciados, no en este repo):
  - `wms-brain/brain/agents/coordinator.yml`
  - `wms-brain/brain/handoffs/2026-05-22-codex-performance-bof-hh/`
  - Usar como entry-point antes de debug/cambios en procesos operativos.

## 4. Gestión de contexto selectivo y modularidad

- Modelo de agentes: coordinador + paquetes de contexto por dominio.
  - Este es el mecanismo operativo de la capa federada.
  - Si hace falta depurar o reestructurar, la regla es consolidar primero el router y luego desplazar contenido duplicado hacia el domain o handoff correcto.
  - `brain/agents/_index.yml`, `domain-*.yml`
  - Cargar solo los paquetes necesarios según el trigger del proceso.

## 5. Skills explícitos y flujos reutilizables

- Extracción y subida de catálogo SQL.
- Reindexado tras cambios en VB o Java.
- Análisis de impacto y dependencias vía Brain.
- Diagnóstico y validación de procesos operativos.

## 6. Reglas de seguridad y compliance

- Prohibido loguear/imprimir secretos (`WMS_KILLIOS_DB_PASSWORD`, `BRAIN_IMPORT_TOKEN`).
- Conexión a KILLIOS prod solo lectura.
- No tocar archivos autogenerados (`Reference.vb`).
- Código siempre en UTF-8 con BOM.

## 7. Otros contextos reutilizables

- Documentación interna: `README.md`, `PARCHES_APLICADOS.md`, `informes-internos/`
- Scripts utilitarios: `clean_wms.ps1`, `f.ps1`, etc.

---

**Resumen**: El repo TOMWMS implementa un modelo de agentes técnicos con reglas estrictas, contexto incremental, y skills reutilizables centrados en el uso del Brain API, flujos de trabajo para cambios cross-layer, y gestión modular de contexto por dominio. Toda intervención debe seguir los workflows y reglas descritas en los archivos de contexto y traza.
