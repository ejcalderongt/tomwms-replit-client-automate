---
protocolVersion: 1
id: L-016
title: log_error_wms_pe/rec/reab/ubic/pick fue mejora reciente (no legacy); decision pendiente sobre revertir tras IDENTITY
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-29T00:30:00Z
target:
  codename: tomwms-arquitectura
  environment: cross-cliente
relatedQuestions: [H-026]
relatedDocs:
  - brain/wms-specific-process-flow/becofarma-mapping.md
  - brain/learnings/L-011-log-error-wms-es-bitacora.md
status: open
priority: medium
tags: [logging, log_error_wms, identity, colision-maxid, decision-pendiente, webapi-design]
---

## Que aprendimos

Las tablas `log_error_wms_pe`, `log_error_wms_rec`, `log_error_wms_reab`,
`log_error_wms_ubic`, `log_error_wms_pick` que existen exclusivamente en
BECOFARMA (la copia diagnostica del 28-abr-2026) NO son legacy: son una
**mejora reciente** que Erik introdujo para resolver un problema concreto.

### Historia (segun Erik, 28-abr-2026):

1. **Modelo original**: una sola tabla `log_error_wms` recibia errores
   de **miles de clases/procesos**.
2. **Problema operativo**: la PK NO era IDENTITY. El ID se calculaba
   con `MAX(id)+1` (o equivalente). En produccion con multiples procesos
   escribiendo concurrentemente se producian **colisiones por MAX(id)**.
3. **Mitigacion temporal**: segmentar el log por proceso/modulo
   (`_pe`=pedido, `_rec`=recepcion, `_reab`=reabasto, `_ubic`=ubicacion,
   `_pick`=picking). Cada tabla tiene su propio MAX(id), reduce la
   superficie de colision.
4. **Estado actual**: Erik ya migro la tabla a IDENTITY. La razon de
   ser de la segmentacion (evitar colisiones por MAX(id) sin identity)
   **ya no aplica**.

### Decision pendiente (de Erik):

> "estoy analizando si (ahora que ya identity), regreso al modelo viejo
> o mantengo esta segmentacion de log_error por proceso"

## Implicancias para el diseño

### Modelo unificado (volver a una sola tabla)

**Pros**:
- Consultas de error mas simples (no hay que saber por modulo donde
  buscar).
- Menos tablas que mantener.
- Schema mas limpio.
- Indices y particionado nativo de SQL Server siguen sirviendo.

**Contras**:
- Tabla potencialmente muy grande (en BECOFARMA acumula 84K+ filas).
  Particionado por fecha mitiga.
- En picos de error masivo, todos los procesos compiten por el mismo
  hot path de inserts (aun con IDENTITY el waitlist puede ser tema en
  cargas extremas).

### Modelo segmentado (mantener tablas por modulo)

**Pros**:
- Cada modulo tiene su propio espacio, lecturas selectivas mas rapidas.
- Diagnostico operativo mas focalizado.
- Si un modulo genera errores en cascada, no satura el log de los demas.

**Contras**:
- Cualquier consulta cross-modulo requiere UNION explicito.
- Si nace un modulo nuevo (ej. webapi, conteo ciclico, devoluciones)
  hay que decidir si crear su propia tabla o mandar a una existente.
- Schema crece de forma no estructurada con cada nueva area funcional.

### Recomendacion para WebAPI .NET 10

Mi propuesta: **modelo unificado con discriminador**. Una sola tabla
`log_error_wms` con columnas:

```
id              BIGINT IDENTITY PK
proceso         VARCHAR(40)  NOT NULL  -- 'pedido','recepcion','reabasto','ubicacion','picking','webapi','conteo',...
nivel           VARCHAR(10)            -- 'INFO','WARN','ERROR','FATAL'
codigo_error    VARCHAR(40)
mensaje         NVARCHAR(MAX)
contexto_json   NVARCHAR(MAX)          -- payload estructurado
correlation_id  VARCHAR(50)            -- traza distribuida WMS<->WebAPI<->ERP
fec_agr         DATETIME2 DEFAULT SYSUTCDATETIME()
user_agr        VARCHAR(50)
host            VARCHAR(80)
maquina         VARCHAR(80)            -- para tracking ClickOnce
INDEX IX_proceso_fecha (proceso, fec_agr DESC)
```

Razones:
- **Cubre los pros del segmentado** via index `(proceso, fec_agr)`:
  filtrar por modulo es tan rapido como en tabla separada.
- **Mantiene el schema unico**: agregar un proceso nuevo = nuevo valor
  de `proceso`, no nueva tabla.
- **Habilita analisis cruzado** sin UNION (top errores totales del dia,
  errores por host, errores por usuario, etc).
- **Soporta correlation_id** para trazas WMS->WebAPI->ERP, critico para
  diagnostico distribuido.

**El log segmentado actual de BECOFARMA quedaria como fase de transicion**
y se consolidaria a la unificada en una migracion posterior (script de
backfill que mueve `log_error_wms_pe.*` -> `log_error_wms` con
`proceso='pedido'` y trunca la tabla origen).

### Pregunta abierta para Erik:

Esta decision afecta directamente al diseño de la WebAPI nueva. Pendiente
de su respuesta. Una vez que decida, esto pasa a `_processed/` con
`decision=applied`.

## Evidencia

- Q&A directa con Erik el 28-abr-2026:
  > Pregunta: "¿es mejora reciente para adoptar en otros clientes, o legacy?"
  > Erik: "fue una mejora, anteriormente todas las tablas depositaban su
  > error a una tabla comun, la tabla crecio exponencialmente y empezo a
  > ser utilizada por miles de clases o proceso, en la practica generaba
  > colisiones por el maxid por no ser identity, estoy analizando si
  > (ahora que ya identity), regreso al modelo viejo o mantengo esta
  > segmentacion de log_error por proceso"

- Existencia de las 5 tablas segmentadas SOLO en BECOFARMA (snapshot
  diagnostico): consultar `becofarma-mapping.md` §Modulos exclusivos.
- Las otras 3 BDs (K7/BB/CEALSA-QAS) tienen unicamente `log_error_wms`
  -> no recibieron la mejora segmentada todavia.

## Accion en el brain

1. Marcar H-026 como `applied-with-followup` en `_processed/`.
2. Documentar la propuesta de modelo unificado-con-discriminador en el
   diseño de la WebAPI cuando arranquemos el ADR correspondiente.
3. Actualizar L-011 (`log-error-wms-es-bitacora`) con referencia a este
   L-016.
