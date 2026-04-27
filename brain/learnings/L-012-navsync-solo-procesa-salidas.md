---
protocolVersion: 1
id: L-012
title: NavSync hoy procesa solo SALIDAs tipo_doc=3 (writer consistente con externo, latencia ~0 seg)
operator: agent-replit
operatorRole: developer
createdAt: 2026-04-27T15:00:00Z
target:
  codename: BB
  environment: PRD
relatedQuestions: [Q-001, Q-003]
relatedDocs:
  - brain/wms-specific-process-flow/interfaces-erp-por-cliente.md
  - brain/wms-brain-client/answers/A-001-cadencia-navsync.md
  - brain/wms-brain-client/answers/A-003-ingresos-byb-pendientes.md
status: open
priority: high
tags: [outbox, navsync, BB, cadencia, PEND-07, arquitectura]
---

## Que aprendimos

NavSync (en BB) **hoy procesa de forma fiable solo SALIDAs
`IdTipoDocumento=3`**. La descripcion "por diseño" es una
hipotesis razonable pero **no totalmente verificada** desde el
WMS — los hechos comprobados son:

1. **Cadencia historica de SALIDAs tipo 3**: 99.48% de las
   marcadas `enviado=1` lo fueron en **0 segundos** entre
   `fec_agr` y `fec_mod` → patron consistente con trigger /
   llamada sincronica post-insert (no batch periodico).

2. **Writer consistente con externo al motor SQL** *(hipotesis
   fuerte, no prueba absoluta)*: la busqueda de SPs locales con
   `UPDATE ... enviado=1 ... i_nav_transacciones_out` devolvio
   0 resultados, y SQL Agent en BB solo tiene
   `syspolicy_purge_history`. Esto es **consistente con** un
   proceso `.exe` / Windows Service externo. La inspeccion **NO
   incluyo** triggers DML ni procesos cross-server / linked
   server, por lo que esos vectores quedan abiertos hasta Q-009.

3. **Solo SALIDAs `IdTipoDocumento=3` operan al 99%**: 277,309
   enviados / 28 pendientes. Los INGRESOs (tipos 6, 8) tuvieron
   actividad parcial entre 2022-05 y 2023-09 (ver L-010); los
   tipos {1, 4, 12} **nunca** se procesaron en el historico
   completo.

4. **Posible pausa de NavSync en BB** *(no concluyente)*: la
   consulta de actividad en los ultimos 30 dias devolvio 0
   filas marcadas. Puede ser ventana de consulta, baja
   estacional, o pausa real del binario. Q-009 lo confirmara.

## Evidencia

- Answer cards: A-001, A-003
- Queries: `Q-001/q2-latencia-tipica`, `Q-001/q4-sps-relacionados`
  (vacio), `Q-003-EXTRA/q5-jobs-sql-agent-bb`,
  `Q-003-EXTRA/q8-comparacion-tipo-doc-salidas`
- Snippet:

```
SALIDA enviado=1 latencia 0 seg: 275,866 (99.48%)
SALIDA enviado=1 latencia 1-5 seg: 1,401 (0.51%)
SALIDA enviado=1 latencia > 1 hora: 43 (0.015%)
SPs locales con UPDATE+enviado: 0
SQL Agent jobs activos: solo syspolicy_purge_history
```

## Implicancias

### Para el codigo

- El bridge **no debe duplicar la logica de writer**: NavSync
  externo ya marca `enviado=1` instantaneo. El bridge consume,
  no marca.
- Si el bridge en el futuro cubre INGRESOS, necesita su propio
  mecanismo (NavSync no lo va a hacer).
- Para BB no hay SP que reaccionar — el contrato es "leer cuando
  `enviado=1` aparezca".

### Para la operacion

- Si NavSync se cae, el outbox crece sin alerta automatica
  (no hay scheduler en SQL Server que vigile). Necesario
  monitoreo externo (ej. % procesado por hora).
- La latencia 0-seg es **excelente** cuando funciona — no hay
  margen para detectar "lento" antes de "muerto".

### Para el equipo

- Documentar el binario NavSync (host, version, scheduler) en
  `interfaces-erp-por-cliente.md`.
- Considerar mover el binario NavSync a un Windows Scheduled
  Task con alerting (en lugar de un Service silencioso).

## Acciones propuestas

- [ ] Identificar binario y host del NavSync de BB (Q-009)
- [ ] Implementar alerta de "outbox SALIDA tipo_doc=3 con
      `enviado=0` y `fec_agr` > 1 hora"
- [ ] Confirmar si el cero-actividad de los ultimos 30 dias en
      BB es real o ventana de consulta

## Como se cierra esta learning

Cerrar cuando: (a) este identificado y monitoreado el binario
NavSync, (b) el monitoreo de % procesamiento esta en produccion,
y (c) la regla "NavSync = solo SALIDAS tipo_doc=3" este
documentada en doc estable de interfaces.
