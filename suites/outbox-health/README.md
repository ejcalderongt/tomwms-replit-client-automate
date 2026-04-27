# Suite: outbox-health

Diagnostica el estado de `i_nav_transacciones_out` (el outbox de
integracion entre WMS y los ERPs cliente: NavSync, SAPSYNC*, MI3, WebAPI).

## Como se ejecuta

Desde el cliente local:

```powershell
Invoke-WmsBrainAnalysis -Suite outbox-health -Profile K7-PRD
Invoke-WmsBrainAnalysis -Suite outbox-health -Profile BB-PRD -OutputJson .\bb-outbox.json
```

Tambien soporta correr solo un subset:

```powershell
Invoke-WmsBrainAnalysis -Suite outbox-health -Profile K7-PRD -OnlyQueries O2,O3,O6
```

## Queries

| Id | Tipo | Para que |
|---|---|---|
| O1 | schema | Columnas del outbox (detecta drift entre clientes) |
| O2 | data | Distribucion enviado x tipo (matriz 2x2 con %) |
| O3 | data | Pendientes actuales con fecha mas vieja/reciente |
| O4 | data | Top 15 pendientes mas viejos (sample) |
| O5 | schema | Cols relacionadas con envio/error/proceso |
| O6 | data | Histograma de pendientes por edad |

## Red flags

- **RF1**: si O2 muestra >80% pendientes en algun (tipo, enviado=0).
  Caso real: BB tiene 99.9% INGRESOS pendientes (ver
  `wms-specific-process-flow/interfaces-erp-por-cliente.md` apendice).
- **RF2**: si O6 muestra pendientes con edad >90 dias. Posible bug o
  falta de monitoreo.
- **RF3**: si O1 muestra cols nuevas no documentadas en SPEC. Drift
  de schema. Considerar `Save-WmsBrainSchemaSnapshot`.

## Resultados conocidos (snapshot 27/abr/2026)

### K7 (Killios) — saludable
- INGRESO: 3 pendientes (0.07%) + 4,391 enviados
- SALIDA: 0 pendientes + 19,799 enviados (100%)
- Edad: los 3 pendientes son de hace >90 dias (revisar)

### BB (BYB) — bandera roja
- INGRESO: 110,795 pendientes (99.90%) vs 107 enviados
- SALIDA: 145,117 pendientes (34.4%) vs 277,310 enviados (65.6%)
- Hipotesis: NavSync solo procesa SALIDAS, INGRESOS van por otro canal

### C9 (CEALSA) — outbox vacio
- Tabla existe pero esquema diferente o no usada
- Probable: 3PL con mecanismo distinto (no confirmado)

## Origen

Esta suite es la portacion del script ad-hoc
`/tmp/dbq/scripts/14-outbox-marca-envio.cjs` usado por el agente brain
en la pasada 9b (abr 2026). Se promovio a suite reutilizable porque las
mismas 6 queries respondieron PEND-10 y abrieron PEND-12.
