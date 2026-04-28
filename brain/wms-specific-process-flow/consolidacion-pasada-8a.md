# Consolidacion Pasada 8a - cierre via ejecucion live SQL

> Generado por agente brain (sesion replit) el 28 abril 2026.
>
> **Pasada 8a**: serie de queries Q-009..Q-014 derivadas del consolidacion-pasada-7 + dump wms-db-brain. Objetivo: cerrar las 6 sub-preguntas con datos firmes en su dimension de **afinidad de procesos**.

## Principio operativo establecido en esta pasada

**Afinidad de PROCESOS vs afinidad de DATOS** - separacion conceptual permanente para todas las pasadas futuras del brain:

- **Afinidad de procesos**: estructura del modelo, tablas, columnas, SPs, relaciones, flags, catalogos, caminos posibles. Confirmable desde nuestro snapshot del EC2.
- **Afinidad de datos**: cantidades, distribuciones, ratios, cardinalidades absolutas. NO confirmable - las personas del cliente trabajan con backups recientes que NO son nuestros snapshots. Comparar es metodologicamente invalido.

**Politica de comunicacion**: en respuestas de afinidad-de-procesos, los numeros observados se reportan como "snapshot dependientes" sin compararlos con los reportados por las personas. La comparacion cuantitativa queda diferida a un segmento dedicado de afinidad-de-datos.

## Cronologia

| Hito | Commit | Rama | Resumen |
|------|--------|------|---------|
| Genesis Pasada 8a | `12951651` | wms-brain | 13 archivos: 2 ADRs, 5 proposals, 5 events H01-H05, queries-pasada-8a.md |
| 6 cards Q-009..Q-014 | `582da718` | wms-brain-client | Cards protocolVersion:2 |
| Cross-ref + comandos PS | `99b98ab6` | wms-brain | Tabla mapping + addendum |
| Re-fabricacion eventos | `426a46f5` | wms-brain | IDs cortos validos al regex + respuestas-tanda-3.md skeleton (TBDs) |
| Briefing SQL agente | `2691b229` | wms-brain | encargo-sql-agente-pasada-8a.md autocontenido |
| Cierre live SQL primera version | `35219b5` | wms-brain | 6 events + 6 proposals + respuestas (con framing incorrecto de afinidad-datos) |
| **Reframing afinidad-procesos** | **(este)** | **wms-brain + wms-brain-client** | **Reescritura para separar procesos de datos, sin senalamientos a fuentes humanas** |

## Decision metodologica retrospectiva

Erik decidio NO delegar al SQL agente externo. En su lugar:
1. Cargo secrets `WMS_DB_USER` + `WMS_DB_PASSWORD` (user `sa`, vale para las 3 BDs).
2. Agente brain instalo `pymssql 2.3.13` en venv aislado (`/tmp/sqlrun/.venv`).
3. Ejecutaron 9 queries READ-ONLY contra el motor EC2.
4. Genero 5 answer drafts MD + 11 CSVs en `wms-brain-client/answers/`.
5. Genero 6 events H06-H11 + 6 proposals MD en `wms-brain/brain/_inbox/_proposals`.
6. Actualizo `respuestas-tanda-3.md` con hallazgos firmes de PROCESO.

## Hallazgo infraestructural

El motor SQL Server 2022 CU18 (Standard Edition) en `52.41.114.122,1437` aloja:

| BD | Codename | Producto | Estado |
|----|----------|----------|--------|
| `TOMWMS_KILLIOS_PRD` | K7-PRD | TOMWMS | Productiva |
| `IMS4MB_BYB_PRD` | BB-PRD | **IMS4MB** | Productiva |
| `IMS4MB_CEALSA_QAS` | C9-QAS | **IMS4MB** | QAS smoke-test (sin trafico de despacho) |
| `LIVE` | - | ? | FULL recovery, parece productiva (no investigada) |
| `mpos_pollo_express_qa` | - | mPOS | otro proyecto |
| `POD_BETA` | - | POD | otro proyecto |

**Las 7 tablas clave** (`i_nav_transacciones_out`, `trans_pe_enc`, `trans_pe_pol`, `trans_despacho_det`, `tarea_hh`, `sis_tipo_tarea`, `trans_reabastecimiento_log`) **existen con el mismo nombre y estructura en las 3 BDs solicitadas**. TOMWMS e IMS4MB son fork/rebrand del mismo producto a nivel schema. **Implicacion**: la nueva WebAPI puede usar un solo modelo de datos.

## 6 hallazgos H06-H11 (todos de PROCESO)

| # | Hallazgo de PROCESO | Decision |
|---|---------------------|----------|
| H06 | El bypass `estado=Despachado` sin `trans_despacho_det` es camino tecnicamente posible | Sostiene ADR-012; frecuencia diferida |
| H07 | Cada deployment tiene perfil operativo distinto sobre el mismo catalogo de tareas | Parametrizar pesos KPIs |
| H08 | Outbox emite confirmacion-de-despacho, NO events de pedido | Simplificar bridge a 2 tipos |
| H09 | Naming inconsistente `IdOrdenPedidoEnc/IdPedidoEnc` (alias) | Documentar en schema-canon |
| H10 | El modelo permite cerrar pedido fiscal sin `trans_pe_pol` | Reforzar P3-FISCAL-LOCK |
| H11 | SP CLBD_PRC no limpia `trans_reabastecimiento_log` (afecta cualquier deploy) | Fix estructural en instalador (extiende H02) |

## Productividad

- 9 invocaciones SQL planificadas, 9 ejecutadas, 9 con datos
- 1 ejecucion fallo en 1ra pasada (Q-013 query2 por naming) - corregida en 2da
- 1 ejecucion devolvio 0 (Q-012 contra C9-QAS por falta de datos) - pivoteada a K7+BB
- 0 errores de driver, 0 errores de credenciales, 0 errores de red
- Total tiempo de ejecucion live: <60 segundos

## Pasada 8a CERRADA (afinidad de procesos)

Quedan listos para ratificacion:
- 6 events en `brain/_inbox/`
- 6 proposals en `brain/_proposals/`
- 5 answer drafts + 11 CSVs en `wms-brain-client/answers/`
- 1 respuestas-tanda-3.md con principio "afinidad procesos vs datos"
- 1 consolidacion-pasada-8a.md (este documento)

**Proxima Pasada sugerida (8b o 9) - segmento afinidad-de-PROCESOS**:
- Investigar las BDs `LIVE` y `POD_BETA` (no documentadas en el motor)
- Auditar otras tablas de modulos no-usados que pudieron quedar pobladas (extension H11)
- Recalibrar texto de ADR-012 segun H06 (definir como tratar el camino bypass)
- Definir el endpoint "tipos activos" filtrado para HH (de H07)

**Segmento futuro - afinidad-de-DATOS** (requiere sincronizar backups del cliente con nuestro snapshot):
- Auditar el caso unico de bypass de 2025-06 en K7
- Auditar los 25 pedidos fiscales sin poliza en C9-QAS
- Comparar cardinalidades reportadas vs observadas
