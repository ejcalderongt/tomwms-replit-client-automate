---
protocolVersion: 1
id: Q-006
title: Cadencia y trigger de MI3 WCF para grupo Aurora (PEND-06)
createdBy: agent-replit
createdAt: 2026-04-27T18:30:00Z
priority: medium
status: pending
tags: [mi3, wcf, aurora, ID, MH, MC, MP, IN, PEND-06]
targets:
  - codename: ID
    environment: PRD
  - codename: MH
    environment: PRD
suggestedQueries:
  - id: q1-tabla-mi3-out
    description: Buscar tabla outbox de MI3
    sql: |
      SELECT TABLE_NAME
      FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME LIKE 'i_%mi3%'
         OR TABLE_NAME LIKE '%mi3%out%'
         OR TABLE_NAME LIKE 'i_aurora%'
         OR TABLE_NAME LIKE '%wcf%'
      ORDER BY TABLE_NAME;
  - id: q2-jobs-sql-server
    description: Jobs del SQL Server Agent que tocan tablas de integracion
    sql: |
      SELECT
        j.name AS job_name,
        s.name AS step_name,
        s.command AS step_command_preview,
        j.enabled
      FROM msdb.dbo.sysjobs j
      JOIN msdb.dbo.sysjobsteps s ON s.job_id = j.job_id
      WHERE j.enabled = 1
        AND (s.command LIKE '%mi3%'
          OR s.command LIKE '%aurora%'
          OR s.command LIKE '%i_nav%'
          OR s.command LIKE '%wcf%')
      ORDER BY j.name, s.step_id;
  - id: q3-triggers-en-pedidos
    description: Triggers en tablas de pedido que pudieran disparar MI3
    sql: |
      SELECT
        OBJECT_NAME(t.parent_id) AS tabla,
        t.name AS trigger_name,
        t.is_disabled
      FROM sys.triggers t
      WHERE OBJECT_NAME(t.parent_id) IN (
        'pedido_enc', 'pedido_det', 'despacho_enc', 'despacho_det',
        'recepcion_enc', 'recepcion_det'
      )
      ORDER BY tabla, trigger_name;
expectedOutputs:
  - id: q1-tabla-mi3-out
    type: table
  - id: q2-jobs-sql-server
    type: table
  - id: q3-triggers-en-pedidos
    type: table
followUp:
  ifFinding: Si q2 muestra job de SQL Agent → cadencia es ese schedule. Si q3 muestra trigger AFTER → cadencia es post-evento.
  thenAsk: Q-XXX (ver SP llamado por el trigger/job para entender contrato WCF)
estimatedTimeMinutes: 5
---

## Contexto

PEND-06: el grupo Aurora (ID, MH, MC, MP, IN) usa MI3 (Modulo
Integracion 3) sobre WCF. No sabemos si es push del WMS, pull desde
Aurora, scheduler periodico, o trigger por evento.

## Pregunta concreta

¿Cuando y como se dispara MI3 contra el ERP Aurora? Job programado,
trigger SQL, llamada del BackOffice.NET, o handler WCF en Aurora que
hace pull?
