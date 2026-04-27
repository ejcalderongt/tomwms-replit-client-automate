---
protocolVersion: 1
id: Q-005
title: Por que cada cliente SAP tiene su propio SAPSYNC* (PEND-08)
createdBy: agent-replit
createdAt: 2026-04-27T18:30:00Z
priority: low
status: pending
tags: [sap, sapsync, K7, BF, MM, LC, PEND-08, arquitectura]
targets:
  - codename: K7
    environment: PRD
suggestedQueries:
  - id: q1-cols-config-sap
    description: Buscar tabla de configuracion de la interface SAP
    sql: |
      SELECT TABLE_NAME
      FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME LIKE '%config%'
         OR TABLE_NAME LIKE '%parametro%'
         OR TABLE_NAME LIKE '%setting%'
         OR TABLE_NAME LIKE '%sap%'
      ORDER BY TABLE_NAME;
  - id: q2-mapeo-productos
    description: Buscar tablas de mapeo WMS<->ERP
    sql: |
      SELECT TABLE_NAME
      FROM INFORMATION_SCHEMA.TABLES
      WHERE TABLE_NAME LIKE '%map%'
         OR TABLE_NAME LIKE '%erp%'
         OR TABLE_NAME LIKE '%equiv%'
      ORDER BY TABLE_NAME;
  - id: q3-cols-cliente-especificas
    description: Buscar cols con prefijo de cliente en tablas comunes
    sql: |
      SELECT TABLE_NAME, COLUMN_NAME
      FROM INFORMATION_SCHEMA.COLUMNS
      WHERE COLUMN_NAME LIKE 'sap_%'
         OR COLUMN_NAME LIKE '%_sap'
         OR COLUMN_NAME LIKE 'erp_%'
      ORDER BY TABLE_NAME, COLUMN_NAME;
expectedOutputs:
  - id: q1-cols-config-sap
    type: table
  - id: q2-mapeo-productos
    type: table
  - id: q3-cols-cliente-especificas
    type: table
followUp:
  ifFinding: Si la BD tiene cols cliente-especificas dentro de la misma estructura → SAPSYNC* podria unificarse parametrizado
  thenAsk: Q-XXX (viabilidad de unificar SAPSYNC* en una sola interface configurable)
estimatedTimeMinutes: 5
---

## Contexto

PEND-08: tenemos `SAPSYNCKILLIOS.vbproj`, `SAPSYNCMAMPA.vbproj`,
`SAPSYNCCUMBRE.vbproj`, `SAPSYNC.vbproj` (Becofarma). Cuatro interfaces
SAP, una por cliente. Genera deuda tecnica (4 deploys, 4 patches) y
sugiere que cada cliente tiene **algo customizado**.

## Pregunta concreta

¿Que tan diferentes son las BDs de los 4 clientes SAP entre si?
¿Existen tablas/cols cliente-especificas que justifican el split, o
podriamos unificar en una sola interface configurable?

## Que se espera del operador

1. Correr la suite contra K7-PRD y, si es viable, contra BF-PRD/MM-PRD/LC-PRD.
2. Comparar outputs de q1, q2, q3 entre clientes.
3. Notar diferencias estructurales relevantes.
