---
titulo: Queries Ciclo 8a - autonomas SQL READ-ONLY
generado: 28 abril 2026
generado_por: agente brain (sesion replit)
proposito: cerrar 6 sub-preguntas abiertas de la consolidacion-ciclo-7 sin nueva intervencion humana
estado: pending_execution (el agente brain de la sesion replit no tiene acceso TCP al EC2 SQL desde su entorno)
ejecutor_esperado: cliente PowerShell wms-brain-client (rama wms-brain-client) o ejecucion manual de Erik
formato_resultado_esperado: respuestas-tarea-3.md en este mismo folder, con seccion por cada Q-CICLO-8A
---

# Queries Ciclo 8a (autonomas SQL READ-ONLY)

Estas 6 queries cierran las sub-preguntas que la consolidacion-ciclo-7 dejo abiertas y que **no requieren intervencion humana** - solo lectura de las 3 BDs productivas READ-ONLY.

**Conexion esperada**: SQL Server `52.41.114.122,1437`. Credenciales en variable de entorno `WMS_KILLIOS_DB_PASSWORD` (no incluir literal en este doc).

**Bases de datos a consultar**:
- `K7_PRD` (Killios produccion)
- `BB_PRD` (BYB produccion)
- `C9_QAS` (CEALSA QA)

Todas las queries son `SELECT` puras - ningun `INSERT/UPDATE/DELETE` ni DDL. **Auditar antes de ejecutar**.

---

## Q-CICLO-8A-01 - Outbox alcance real (cierra C-03 sub-Q de consolidacion-ciclo-7)

**Origen**: D-02 de consolidacion-ciclo-7. Carol dice que el outbox solo se usa para recepciones y despachos. Schema soporta 4 tipos. Esta query confirma cual es la verdad.

**Base**: `K7_PRD`, `BB_PRD`, `C9_QAS` (las 3, comparar).

```sql
USE K7_PRD;
SELECT
  'K7_PRD' AS bd,
  COUNT(CASE WHEN idordencompra   IS NOT NULL AND idordencompra   <> 0 THEN 1 END) AS con_oc,
  COUNT(CASE WHEN idrecepcionenc  IS NOT NULL AND idrecepcionenc  <> 0 THEN 1 END) AS con_recepcion,
  COUNT(CASE WHEN idpedidoenc     IS NOT NULL AND idpedidoenc     <> 0 THEN 1 END) AS con_pedido,
  COUNT(CASE WHEN iddespachoenc   IS NOT NULL AND iddespachoenc   <> 0 THEN 1 END) AS con_despacho,
  COUNT(*) AS total
FROM dbo.i_nav_transacciones_out;
```

(Repetir cambiando `USE K7_PRD` por `BB_PRD` y `C9_QAS`.)

**Interpretacion**:
- Si `con_pedido` y `con_oc` son cero o casi cero en las 3 BDs → Carol tiene razon operativa. El bridge se simplifica: escribir solo en `idrecepcionenc` y `iddespachoenc`.
- Si distinto de cero → hay uso silencioso del outbox para los 4 tipos. El bridge debe soportar todos. Refinar ADR-011 / ADR-012.

---

## Q-CICLO-8A-02 - Reabasto Killios pre-2024 vs post-2024 (cierra C-04 sub-Q)

**Origen**: D-02 de consolidacion-ciclo-7. Carol dice que es basura no limpiada en instalacion. SQL agente dice que el modulo sigue activo. Esta query separa ambos casos.

**Base**: `K7_PRD`.

```sql
USE K7_PRD;
SELECT
  CASE
    WHEN fec_agr < '2024-01-01' THEN 'pre-2024 (probable basura instalacion)'
    WHEN fec_agr < '2025-01-01' THEN '2024'
    ELSE '2025+'
  END AS periodo,
  COUNT(*) AS filas,
  MIN(fec_agr) AS mas_vieja,
  MAX(fec_agr) AS mas_nueva,
  COUNT(DISTINCT IdProductoConfigInfoCli) AS productos_distintos,
  COUNT(DISTINCT IdUbicacion) AS ubicaciones_distintas
FROM dbo.trans_reabastecimiento_log
GROUP BY
  CASE
    WHEN fec_agr < '2024-01-01' THEN 'pre-2024 (probable basura instalacion)'
    WHEN fec_agr < '2025-01-01' THEN '2024'
    ELSE '2025+'
  END
ORDER BY periodo;
```

**Interpretacion**:
- Si todo es pre-2024 → Carol gana. Solo basura, accion atomica del SP CLBD_PRC resuelve.
- Si hay un volumen significativo post-2024 → modulo activo. Decision adicional: apagar la deteccion automatica en clientes que no usan reabasto?

---

## Q-CICLO-8A-03 - P-16b refinada (cierra D-03 de consolidacion-ciclo-7)

**Origen**: tarea-2 P-16 dejo P-16b abierta. La consolidacion la integra con D-03. Esta query confirma el numero exacto de bypasses para que ADR-012 tenga numero firme.

**Base**: `K7_PRD`.

```sql
USE K7_PRD;
WITH pedidos_marcados_despachados AS (
  SELECT IdPedidoEnc, fec_agr, fec_mod
  FROM dbo.trans_pe_enc
  WHERE estado = 'Despachado'
),
pedidos_con_despacho_real AS (
  SELECT DISTINCT d.IdPedidoEnc
  FROM dbo.trans_despacho_det d
)
SELECT
  COUNT(*) AS pedidos_estado_despachado_total,
  SUM(CASE WHEN dr.IdPedidoEnc IS NULL THEN 1 ELSE 0 END) AS bypass_sin_despacho_real,
  SUM(CASE WHEN dr.IdPedidoEnc IS NOT NULL THEN 1 ELSE 0 END) AS con_despacho_real,
  CAST(
    100.0 * SUM(CASE WHEN dr.IdPedidoEnc IS NULL THEN 1 ELSE 0 END) / NULLIF(COUNT(*), 0)
    AS DECIMAL(5,2)
  ) AS pct_bypass
FROM pedidos_marcados_despachados pm
LEFT JOIN pedidos_con_despacho_real dr ON dr.IdPedidoEnc = pm.IdPedidoEnc;
```

**Interpretacion**:
- Si el numero coincide con los 43 que Carol reporto → confirmado. ADR-012 sigue su curso.
- Si el numero es muy diferente (ej. 200+) → recalibrar. La feature se usa mas de lo esperado, ADR-012 debe explicitar volumetria.
- Si el numero es ~0 → Carol exagero. El feature existe pero casi no se usa, ADR-012 puede simplificarse a "permitir con razon, sin permiso especial".

---

## Q-CICLO-8A-04 - Excepciones del corte de jornada CEALSA (sub-Q4 de respuestas-ciclo-7)

**Origen**: Carol mencion en P-22 que hay "procesos con excepciones" en el corte de jornada CEALSA pero no entro en detalle. Esta query intenta reconstruir esas excepciones via SQL.

**Base**: `C9_QAS`.

```sql
USE C9_QAS;
-- Pedidos con fecha de creacion en un dia y fecha de despacho en otro (cruza jornada)
SELECT TOP 50
  pe.IdPedidoEnc,
  pe.estado,
  pe.fec_agr AS creado,
  de.fec_agr AS despachado,
  DATEDIFF(day, pe.fec_agr, de.fec_agr) AS dias_diferencia,
  pe.IdTipoPedido,
  t.Nombre AS tipo_nombre
FROM dbo.trans_pe_enc pe
JOIN dbo.trans_despacho_det dd ON dd.IdPedidoEnc = pe.IdPedidoEnc
JOIN dbo.trans_despacho_enc de ON de.IdDespachoEnc = dd.IdDespachoEnc
JOIN dbo.trans_pe_tipo t ON t.IdTipoPedido = pe.IdTipoPedido
WHERE DATEDIFF(day, pe.fec_agr, de.fec_agr) > 0
ORDER BY dias_diferencia DESC, pe.fec_agr DESC;
```

```sql
USE C9_QAS;
-- Conteo agrupado por dias-de-cruce
SELECT
  DATEDIFF(day, pe.fec_agr, de.fec_agr) AS dias_cruce,
  COUNT(*) AS pedidos
FROM dbo.trans_pe_enc pe
JOIN dbo.trans_despacho_det dd ON dd.IdPedidoEnc = pe.IdPedidoEnc
JOIN dbo.trans_despacho_enc de ON de.IdDespachoEnc = dd.IdDespachoEnc
GROUP BY DATEDIFF(day, pe.fec_agr, de.fec_agr)
ORDER BY dias_cruce;
```

**Interpretacion**:
- Lista los pedidos cuyo despacho cruza la jornada (creados un dia, despachados otro). Estos son las "excepciones" del corte.
- Distribucion por dias-de-cruce muestra si es tipico (1 dia = normal) o anomalo (3+ dias = excepcion real).

---

## Q-CICLO-8A-05 - Validar 11 campos obligatorios de poliza CEALSA (sub-Q5 de respuestas-ciclo-7)

**Origen**: Carol mencion en P-11 que los 11 campos de poliza son obligatorios para CEALSA. Esta query valida que ningun pedido fiscal este incompleto en la BD QAS.

**Base**: `C9_QAS`.

```sql
USE C9_QAS;
-- Identificar campos obligatorios primero (estructura de trans_pe_pol)
SELECT
  c.name AS columna,
  t.name AS tipo,
  c.is_nullable,
  c.max_length
FROM sys.columns c
JOIN sys.tables tbl ON tbl.object_id = c.object_id
JOIN sys.types t ON t.user_type_id = c.user_type_id
WHERE tbl.name = 'trans_pe_pol'
ORDER BY c.column_id;
```

```sql
USE C9_QAS;
-- Conteo de pedidos fiscales con poliza vs sin poliza (a refinar segun los 11 campos reales)
SELECT
  COUNT(DISTINCT pe.IdPedidoEnc) AS pedidos_fiscales,
  COUNT(DISTINCT pp.IdPedidoEnc) AS pedidos_con_poliza,
  COUNT(DISTINCT pe.IdPedidoEnc) - COUNT(DISTINCT pp.IdPedidoEnc) AS pedidos_sin_poliza
FROM dbo.trans_pe_enc pe
LEFT JOIN dbo.trans_pe_pol pp ON pp.IdPedidoEnc = pe.IdPedidoEnc
JOIN dbo.trans_pe_tipo t ON t.IdTipoPedido = pe.IdTipoPedido
WHERE t.control_poliza = 1;
```

**Interpretacion**:
- Si `pedidos_sin_poliza` = 0 → la afirmacion de Carol (100% pedidos fiscales tienen poliza) se valida.
- Si > 0 → hay anomalia. Refinar la sub-Q: cuales pedidos, cuando fueron creados, por que no tienen poliza?

---

## Q-CICLO-8A-06 - TOP10 real de tareas HH (cierra P-25 parcial)

**Origen**: tarea-2 P-25 dejo este JOIN pendiente.

**Base**: `K7_PRD`, `BB_PRD`, `C9_QAS`.

```sql
USE K7_PRD;
SELECT TOP 15
  st.IdTipoTarea,
  st.Nombre AS tipo_tarea,
  st.Contabilizar,
  COUNT(*) AS ejecutadas,
  CAST(100.0 * COUNT(*) / SUM(COUNT(*)) OVER () AS DECIMAL(5,2)) AS pct
FROM dbo.tarea_hh th
JOIN dbo.sis_tipo_tarea st ON st.IdTipoTarea = th.IdTipoTarea
GROUP BY st.IdTipoTarea, st.Nombre, st.Contabilizar
ORDER BY ejecutadas DESC;
```

(Repetir cambiando BD.)

**Interpretacion**: el TOP confirma o refuta la lista intuitiva de Carol (Recepcion, Cambio Ubicacion, Cambio Estado, Implosiones, Picking, Verificacion, Despacho). Si la lista real difiere mucho, Carol describio el TOP teorico no el TOP usado.

---

## Plan de ejecucion

**Quien ejecuta** (en orden de preferencia):

1. **Cliente PowerShell wms-brain-client** (rama `wms-brain-client`): el flujo natural - emite eventos `question_request` schema_v2 al `_inbox/`, ejecuta las queries, vuelve con `question_answer`. Este es el caso de uso para el cual fue construido.
2. **Erik manual** desde su SSMS conectado al EC2 - ejecuta las 6 queries y pega los resultados crudos.
3. **Agente brain en otra sesion replit con conectividad** - el actual (sesion 28-abril-2026) confirmo que NO tiene acceso TCP al EC2 desde su entorno (sin pymssql, sin pyodbc, sin sqlcmd, conexion TCP a 52.41.114.122:1437 falla).

**Donde guardar resultados**:

Crear archivo `brain/wms-specific-process-flow/respuestas-tarea-3.md` con:
- Por cada Q-CICLO-8A-NN: query ejecutada + resultado en tabla markdown + interpretacion.
- Cierre de cada sub-pregunta apuntando al doc origen (consolidacion-ciclo-7.md).
- Si alguna query revela hallazgo nuevo accionable, emitir nuevo evento `learning_proposed` al `_inbox/` siguiendo el patron de H-01..H-05.

**Que pasa despues de ejecutar**:

1. Refinar ADR-012 si Q-CICLO-8A-03 da un numero muy distinto a 43.
2. Refinar el proposal de H-02 si Q-CICLO-8A-02 muestra que el modulo de deteccion sigue activo (decision adicional: apagar?).
3. Cerrar formalmente las sub-preguntas en consolidacion-ciclo-7.md (mover de "abiertas" a "cerradas").
4. Si todo cierra OK, P-16b WebAPI scaffold queda **desbloqueado** para arrancar.

---

## Por que estas queries y no otras

Solo se incluyen las 6 que cumplen 3 criterios:

1. **READ-ONLY**: ningun side effect en BD productiva.
2. **Cierran loops abiertos**: cada una resuelve una sub-pregunta documentada.
3. **No requieren judgment humano**: el resultado es un numero o una lista, no una opinion.

Las preguntas que requieren a Erik o a un experto humano (P-23 prefactura, decisiones arquitectonicas H-01/H-04, P-30 setup-agent) NO se incluyen aqui - son trabajo de la Ciclo 8 con humanos.

---

— Brain TomWMS · Queries Ciclo 8a · 28 abril 2026 —


---

## Addendum 28 abr 2026 (post commit) - mapping a question cards

Las 6 queries quedaron emitidas como **question cards en wms-brain-client/questions/**
(rama `wms-brain-client`, commit `582da718`) en formato `protocolVersion: 2`. El cliente
PowerShell ya las puede ejecutar directamente con `Invoke-WmsBrainQuestion`:

| Ciclo 8a | Question card | Profile(s) PowerShell | Cierra |
|-----------|---------------|------------------------|--------|
| Q-CICLO-8A-01 | `Q-009-outbox-alcance-tipos.md` | `K7-PRD`, `BB-PRD`, `C9-QAS` | C-03 |
| Q-CICLO-8A-02 | `Q-010-killios-reabasto-pre-post-2024.md` | `K7-PRD` | C-04 sub-Q |
| Q-CICLO-8A-03 | `Q-011-killios-bypass-despachado-numero-firme.md` | `K7-PRD` | D-03 / P-16b |
| Q-CICLO-8A-04 | `Q-012-cealsa-qas-corte-jornada-excepciones.md` | `C9-QAS` | sub-Q4 |
| Q-CICLO-8A-05 | `Q-013-cealsa-qas-poliza-11-campos.md` | `C9-QAS` | sub-Q5 |
| Q-CICLO-8A-06 | `Q-014-top15-tareas-hh-3-bds.md` | `K7-PRD`, `BB-PRD`, `C9-QAS` | P-25 |

Comando ejemplo (cualquier maquina con el modulo `WmsBrainClient` instalado y
perfil configurado):

```powershell
# Ejecutar las 6 cards en orden de prioridad (high primero)
Invoke-WmsBrainQuestion -Id Q-009 -Profile K7-PRD
Invoke-WmsBrainQuestion -Id Q-009 -Profile BB-PRD
Invoke-WmsBrainQuestion -Id Q-009 -Profile C9-QAS

Invoke-WmsBrainQuestion -Id Q-011 -Profile K7-PRD

Invoke-WmsBrainQuestion -Id Q-010 -Profile K7-PRD
Invoke-WmsBrainQuestion -Id Q-012 -Profile C9-QAS
Invoke-WmsBrainQuestion -Id Q-013 -Profile C9-QAS

Invoke-WmsBrainQuestion -Id Q-014 -Profile K7-PRD
Invoke-WmsBrainQuestion -Id Q-014 -Profile BB-PRD
Invoke-WmsBrainQuestion -Id Q-014 -Profile C9-QAS
```

Cada llamada genera CSVs en `wms-brain-client/answers/Q-NNN/` y un draft
`answer-draft.md` para completar con interpretacion. Cuando esten todas
listas, emitir `respuestas-tarea-3.md` en este folder cerrando formalmente
las sub-preguntas en `consolidacion-ciclo-7.md`.

**Estado del bloqueo Ciclo 8a**: las queries dejan de estar bloqueadas. La unica
restriccion es tener un host con conectividad TCP al EC2 SQL `52.41.114.122,1437` y
el modulo PowerShell instalado. Erik o el agente brain en otra sesion con
conectividad pueden ejecutar.
