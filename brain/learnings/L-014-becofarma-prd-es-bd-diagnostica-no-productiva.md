---
protocolVersion: 1
id: L-014
title: IMS4MB_BECOFARMA_PRD en EC2 52.41.114.122,1437 es snapshot diagnostico, NO la productiva
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-28T23:55:00Z
target:
  codename: BECOFARMA
  environment: snapshot-diagnostico
relatedQuestions: [H-025, H-028]
relatedDocs:
  - brain/clients/becofarma.md
  - brain/clients/README.md
  - brain/wms-specific-process-flow/becofarma-mapping.md
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
status: closed
priority: high
tags: [becofarma, sandbox, diagnostico, entrenamiento-agente, productiva, NO-productiva]
---

## Que aprendimos

La BD `IMS4MB_BECOFARMA_PRD` que aparecio el 28-abr-2026 a las 08:32 en
el server EC2 `52.41.114.122,1437` (donde ya estan K7-PRD, BB-PRD,
C9-QAS) **NO es la productiva del cliente BECOFARMA**. Es una
**copia/snapshot restaurada por Erik para entrenamiento y documentacion
del agente**.

Aunque el sufijo es `_PRD`, ese sufijo viene del nombre original de la
BD productiva, no indica el rol de la copia en este server. El rol real
de esta BD aqui es: **sandbox de lectura para que el agente aprenda el
schema, los modulos exclusivos y los patrones operativos del cliente
sin tocar nada productivo**.

## Implicancias directas

### IMP-1: el "85% pendiente del outbox" NO es un problema operativo

El hallazgo H-028 (`i_nav_transacciones_out` con 85% pendiente,
31,263/36,576) **NO indica que SAPBOSync.exe este caido en BECOFARMA**.
Es el **snapshot historico congelado** de la BD productiva al momento
del backup. En la productiva real:

- `SAPBOSync.exe` corre en el server del cliente y procesa el outbox
  con normalidad (ver L-015 sobre el modelo ClickOnce + dispatch).
- El % pendiente de la productiva real puede ser otro completamente
  (probablemente menor, dado que la interface esta activa).
- Esta copia NO recibe el procesamiento de SAPBOSync — por eso el
  contador queda congelado donde quedo.

**Reformulacion del hallazgo H-028**: el 85% pendiente es un dato
estructural de la copia, no un sintoma de salud operativa. Sigue siendo
util como referencia de **volumen historico** pero no como KPI de
disponibilidad de la interface.

### IMP-2: el "44% en estado Pickeado" (H-029) tampoco es necesariamente bug

Si en la productiva existe un workflow donde el `Pickeado→Despachado`
depende del SAPBOSync que ya no aplica a este snapshot, el porcentaje
puede estar inflado por pedidos que en la productiva real ya
transitaron.

H-029 sigue valido como pregunta arquitectonica (¿hay verificacion
intermedia?), pero **no se puede inferir disfuncion operativa de los
porcentajes de esta copia**.

### IMP-3: el principio "afinidad-procesos confirmable, afinidad-datos diferida" se refuerza

Esta BD es perfecta para confirmar **afinidad de procesos** (que tablas
existen, que modulos hay, que naming usan, que catalogos comparten con
otras BDs). Lo que NO se puede inferir es **afinidad de datos vivos**
(cuanto procesa la interface por dia, cuantos pedidos quedan colgados
en la productiva real, etc).

### IMP-4: politica de conexion mantiene SELECT-only por costumbre, no por riesgo de produccion

Aunque la BD es snapshot, el agente sigue operando como si fuera
productiva: solo `SELECT`/`EXEC` de SPs de lectura, sin escrituras.
Razon: (a) reflejo defensivo correcto, (b) la copia podria refrescarse
desde la productiva en el futuro y cualquier modificacion local se
perderia, (c) consistencia con K7/BB/CEALSA-QAS.

## Evidencia

- Q&A directa con Erik el 28-abr-2026:
  > Pregunta: "¿Por que se restauro BECOFARMA hoy en este server?"
  > Erik: "para efectos de tu entrenamiento y documentacion"

- Discrepancia confirmada entre `sys.databases.create_date` (2026-04-28
  08:32) y `i_nav_config_enc.fec_agr` (2017-09-11) que ya habia hecho
  sospechar de restore.

## Accion en el brain

1. Marcar BECOFARMA en `clients/README.md` como **snapshot diagnostico**
   (no productiva), separada de K7/BB.
2. Agregar la aclaracion en `clients/becofarma.md` arriba de todo
   (banner).
3. Reformular interpretacion de H-028 en `becofarma-mapping.md`.
4. Mover H-025 y H-028 a `_processed/` con `decision=applied`.
