---
id: dec-2026-04-killios-acceso-replit
tipo: decision
fecha: 2026-04-27
estado: implementada
autor: Erik Calderón + Replit Agent
casos_relacionados:
  - case-2026-04-importar-lotes-cliente
docs_actualizados:
  - brain/agent-context/AZURE_ACCESS.md (sección 9)
  - brain/skills/wms-tomwms/SKILL.md (sección 7, 8)
  - brain/replit.md (sección 5)
commits:
  - 09eb8e5 (wms-brain — actualización docs Killios)
---

# DECISION: Habilitar acceso directo a Killios desde Replit

## 1. Contexto

Antes del 2026-04-27, el agente Replit no tenía acceso directo a la BD Killios:

- El secret `WMS_KILLIOS_DB_PASSWORD` existía pero faltaban host/puerto/usuario/base.
- Cualquier query analítica requería que Erik la corriera localmente con `wmsa` en su PC y compartiera resultados por chat.
- Latencia alta para cualquier validación de modelo.
- Bloqueaba la viabilidad de la **Fase 3** (extractor de code-index) y **Fase 2 nueva** (sql-catalog auto-generado).

## 2. Decisión

**Habilitar conexión read-only desde Replit a Killios** con los siguientes parámetros:

- Endpoint: `52.41.114.122:1437` (puerto custom, no el 1433 default).
- Usuario: `sa`.
- Driver: `mssql` (Node) o `tedious` (ya en workspace).
- Encrypt: `false`, TrustServerCertificate: `true`.
- Bases en scope: `TOMWMS_KILLIOS_PRD`, `IMS4MB_BYB_PRD`, `IMS4MB_CEALSA_QAS` (las demás del server son fuera de WMS).

**Guardrail obligatorio**: el agente sólo ejecuta `SELECT/WITH/EXEC/SET/DECLARE/PRINT`. Bloquea `INSERT/UPDATE/DELETE/MERGE/DROP/ALTER/CREATE/TRUNCATE`. Patrón canónico en `brain/wms-agent/wmsa/killios.py`.

## 3. Alternativas consideradas

| Alternativa | Rechazada porque |
|---|---|
| **Mantener flujo actual** (Erik corre queries localmente, comparte resultados) | Latencia alta, bloquea automatización de F2/F3, no escala con volumen de queries necesario para sql-catalog. |
| **Replicar Killios a una BD en Replit** | Volátil de origen + costo + riesgo de drift entre copia y original. Killios ya es de Erik para análisis, no es prod cliente — replicar duplicaría la confusión. |
| **Exponer Killios via API REST custom** | Sobre-ingeniería para read-only directo. Agrega capa sin valor. |

## 4. Consecuencias

### Positivas

- **F2 (sql-catalog) viable**: extractor corre 100% en Replit, sin tu intervención.
- **F3 (code-index) más rica**: cruza referencias VB con schema/SPs vivos.
- **F4 (`wms-brain ask`) puede consultar datos vivos** (conteos, fecha mod) en tiempo real.
- **Análisis de casos más rápido**: validación de modelo en segundos, no horas.
- **Caso D (`cliente_lotes`) avanza sin que Erik corra nada**: schema, conteos, mod date, todo desde Replit.

### Negativas / riesgos

- **Volatilidad del endpoint**: si el host AWS muere/migra, hay que actualizar 4-5 secrets. Mitigado por:
  - Documentación explícita en `AZURE_ACCESS §9.3`.
  - Catálogo SQL cacheado en archivos versionados (sigue sirviendo aunque Killios desaparezca temporalmente).
- **Datos productivos accesibles** (1083 clientes, 319 productos): mitigado por compromiso explícito de **construir catálogo con metadata, no con `SELECT *` de tablas de negocio**. Si una muestra es necesaria para un caso, se pide con mínimos.
- **Riesgo de inventar shape de tabla si Killios no responde**: explícitamente prohibido por regla 4 de `AZURE_ACCESS §9.3` ("detenerse y reportar").

## 5. Implementación (2026-04-27)

1. Erik proporcionó endpoint + usuario por chat.
2. Validación de conexión con script Node + driver `mssql` → SQL Server 2022 CU18 confirmado.
3. Exploración inicial: 345 tablas, 39 SPs, 10 BDs visibles (3 WMS).
4. Hallazgos clave registrados en commit `09eb8e5`:
   - Naming real sin prefijo `t_*`.
   - Modelo de lotes mapeado (6 tablas dedicadas, 8 con campo embebido, sin maestro).
   - Patrón staging Excel (`col1..colN`).
5. Secrets recomendados para futuras sesiones (registro pendiente por parte de Erik):
   - `WMS_KILLIOS_DB_HOST`, `WMS_KILLIOS_DB_PORT`, `WMS_KILLIOS_DB_USER`, `WMS_KILLIOS_DB_NAME_DEFAULT`.

## 6. Re-evaluación

Si en 6 meses (2026-10) Killios sigue siendo el único punto de validación de modelo, considerar:
- Snapshot periódico del schema (no de los datos) versionado en `brain/sql-catalog/_snapshots/` para sobrevivir cualquier outage.
- Mecanismo de "Killios-down mode" donde el agente usa el snapshot cacheado y avisa de la degradación.
