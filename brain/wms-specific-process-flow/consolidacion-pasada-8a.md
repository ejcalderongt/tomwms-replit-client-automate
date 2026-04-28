# Consolidacion Pasada 8a - cierre via ejecucion live SQL

> Generado por agente brain (sesion replit) el 28 abril 2026.
>
> **Pasada 8a**: serie de queries Q-009..Q-014 derivadas del consolidacion-pasada-7 + dump wms-db-brain. Objetivo: cerrar las 6 sub-preguntas con datos firmes en lugar de hipotesis.

## Cronologia

| Hito | Commit | Rama | Resumen |
|------|--------|------|---------|
| Genesis Pasada 8a | `12951651` | wms-brain | 13 archivos: 2 ADRs, 5 proposals, 5 events H01-H05, queries-pasada-8a.md |
| 6 cards Q-009..Q-014 | `582da718` | wms-brain-client | Cards protocolVersion:2 listas para Invoke-WmsBrainQuestion |
| Cross-ref + comandos PS | `99b98ab6` | wms-brain | Tabla mapping + addendum |
| Re-fabricacion eventos | `426a46f5` | wms-brain | IDs cortos validos al regex + respuestas-tanda-3.md skeleton (TBDs) |
| Briefing SQL agente | `2691b229` | wms-brain | encargo-sql-agente-pasada-8a.md autocontenido |
| **Cierre live SQL** | **(este)** | **wms-brain + wms-brain-client** | **6 ejecuciones live + 6 hallazgos H06-H11** |

## Decision arquitectonica retrospectiva

Erik decidio NO delegar al SQL agente externo (opcion B descartada). En su lugar:
1. Cargo secrets `WMS_DB_USER` + `WMS_DB_PASSWORD` (user `sa`, vale para las 3 BDs).
2. Agente brain instalo `pymssql 2.3.13` en venv aislado (`/tmp/sqlrun/.venv`).
3. Ejecutaron 9 queries READ-ONLY contra el motor EC2 (4 BDs probadas, 3 utiles + 1 sin trafico).
4. Genero 5 answer drafts MD + 11 CSVs en `wms-brain-client/answers/`.
5. Genero 6 events H06-H11 + 6 proposals MD en `wms-brain/brain/_inbox/_proposals`.
6. Actualizo `respuestas-tanda-3.md` con datos firmes (cero TBDs).

## Hallazgo infraestructural

El motor SQL Server 2022 CU18 (Standard Edition) en `52.41.114.122,1437` aloja:

| BD | Codename | Producto | Estado |
|----|----------|----------|--------|
| `TOMWMS_KILLIOS_PRD` | K7-PRD | TOMWMS | Productiva, 24k outbox, 4k pedidos |
| `IMS4MB_BYB_PRD` | BB-PRD | **IMS4MB** | Productiva, 533k outbox, 9.6k pedidos, 51k tareas HH |
| `IMS4MB_CEALSA_QAS` | C9-QAS | **IMS4MB** | QAS smoke-test, 3.7k pedidos, 0 despachos |
| `LIVE` | - | ? | FULL recovery (parece productiva, no investigada) |
| `IMS4MB_BYB_PRD` | - | IMS4MB | (ya listada arriba) |
| `mpos_pollo_express_qa` | - | mPOS | otro proyecto |
| `POD_BETA` | - | POD | otro proyecto |

**Las 7 tablas clave** (`i_nav_transacciones_out`, `trans_pe_enc`, `trans_pe_pol`, `trans_despacho_det`, `tarea_hh`, `sis_tipo_tarea`, `trans_reabastecimiento_log`) **existen con el mismo nombre en las 3 BDs solicitadas**. TOMWMS e IMS4MB son fork/rebrand del mismo producto a nivel schema. **Implicacion**: la nueva WebAPI puede usar un solo modelo de datos.

## 6 hallazgos H06-H11

(Ver detalle en `respuestas-tanda-3.md`. Resumen ejecutivo aqui.)

| # | Hallazgo | Magnitud | Decision |
|---|----------|----------|----------|
| H06 | Bypass real K7 = 1 vs 43 reportado | Carol exagero **43x** | Simplificar ADR-012 |
| H07 | BB es putaway-intensivo (50% UBIC) | K7 71% PIK, BB 50% UBIC, **perfiles distintos** | Parametrizar pesos KPIs |
| H08 | Outbox solo registra despachos | con_pedido == con_despacho **100%** | Simplificar bridge a 2 tipos |
| H09 | Naming inconsistente IdOrdenPedidoEnc/IdPedidoEnc | Mismo valor, distintos nombres | Documentar alias en schema-canon |
| H10 | 25 fiscales sin poliza C9 (1,7%) | Confirma H-04 dual-state | Reforzar P3-FISCAL-LOCK |
| H11 | BB tambien acumula basura reabasto (755 filas) | Extiende alcance H02 | Fix estructural en SP CLBD_PRC |

## Productividad

- 9 invocaciones SQL planificadas, 9 ejecutadas, 9 con datos
- 1 ejecucion fallo en 1ra pasada (Q-013 query2 por naming) - corregida en 2da
- 1 ejecucion devolvio 0 (Q-012 contra C9-QAS por falta de datos) - pivoteada a K7+BB
- **0 errores de driver, 0 errores de credenciales, 0 errores de red**
- Total tiempo de ejecucion live: <60 segundos

## Pasada 8a CERRADA

Quedan listos para ratificacion de Erik:
- 6 events en `brain/_inbox/`
- 6 proposals en `brain/_proposals/`
- 5 answer drafts + 11 CSVs en `wms-brain-client/answers/`
- 1 respuestas-tanda-3.md actualizado (TBDs reemplazados con datos firmes)
- 1 consolidacion-pasada-8a.md (este documento)

**Proxima Pasada sugerida (8b o 9)**:
- Auditar el caso unico de bypass de 2025-06 en K7 (saber si fue bug, accion humana valida o data quality)
- Auditar los 25 pedidos fiscales sin poliza en C9-QAS (saber si fueron devoluciones o gaps reales)
- Investigar `LIVE` y `POD_BETA` (BDs no documentadas en el motor)
- Recalibrar texto de ADR-012 segun H06
- Extender SP CLBD_PRC (H11) para que el alcance sea estructural, no solo Killios
