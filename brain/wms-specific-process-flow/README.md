---
id: README
tipo: process-flow
estado: vigente
titulo: wms-specific-process-flow
ramas: [dev_2028_merge]
tags: [process-flow]
---

# wms-specific-process-flow

> Tuning de la comprension del comportamiento de WMS basado en evidencia productiva.
> Folder pedido por Erik (ciclo 7+) para profundizar el mapeo de operaciones reales.

## Proposito

Este folder NO documenta como deberia funcionar WMS (eso es la skill `wms-test-bridge`). Documenta **como funciona realmente HOY** en las 3 BDs productivas, segun:

- Conteos reales de tablas y SPs.
- Estados observados en `trans_pe_enc.estado`, `trans_oc_estado`, etc.
- `Caso_Reserva` registrados en `trans_pe_det_log_reserva`.
- Discrepancias entre lo configurado y lo ejecutado.

Aqui es donde se aterriza la **terminologia ADR-010** (reserva-webapi vs reserva-WMS legacy) en cada proceso.

## Contenido

| Archivo | Que es | Estado |
|---|---|---|
| `README.md` | este archivo | vivo |
| `process-map.md` | mapa global de procesos del WMS con tablas activas, SPs y conteos productivos | v1 (ciclo 7) |
| `state-machine-pedido.md` | maquina de estados observada de `trans_pe_enc` con transiciones reales | v2 (ciclo 9, actualizado por tarea-1) — pendiente refinar con consolidacion-ciclo-7 (D-01, D-04) |
| `interfaces-erp-por-cliente.md` | mapeo de interfaces ERP (NAV/SAP) por cliente, con direccion y cardinalidad de filas | v1 |
| `bug-report-p16b.md` | reporte de bug refinado de la pregunta P-16 (despachos vs estado) | abierto — esperar D-03 de consolidacion-ciclo-7 |
| `preguntas-ciclo-7.md` | 25 preguntas para Erik que afinan el mapeo. Marcadas como respondidas. | cerrado |
| `respuestas-tarea-1.md` | Respuestas de Erik a P-08, P-10, P-18 (3 de 25), ciclo 9 | cerrado |
| `respuestas-tarea-2.md` | Respuestas del agente brain via SQL READ-ONLY a 9 preguntas (PEND-01..03 + P-09/12/14/16/21/24/25), ciclo 9b | cerrado |
| `respuestas-ciclo-7.md` | Respuestas de Carol Karina Flores Klee (CKFK) a las 25 preguntas + addendum EJC en P-18, ciclo 10 | cerrado |
| `consolidacion-ciclo-7.md` | Cross-reference de las 3 fuentes de respuesta. Detecta 4 contradicciones, 5 refinamientos consensuados, 4 hallazgos derivados, 8 sub-preguntas abiertas. | vivo (28 abril 2026) |

## Procesos identificados (resumen)

```
INBOUND                    PUTAWAY                STORAGE
========                   ========               =======
- Recepcion OC (SAP/NAV)   - Asignar ubicacion    - Movimiento entre ubicaciones
- Recepcion transferencia  - Generar LP           - Reabasto picking (BYB)
- Recepcion poliza (3PL)   - Cuarentena           - Conteo ciclico

OUTBOUND                   ACCESORIOS             3PL (CEALSA)        SAP-ESPECIFICO (KILLIOS)
========                   ==========             ============        ========================
- Pedido                   - Traslado interno     - Stock jornada     - Postear ajuste DI-API
- Reserva (CASO_#X)        - Ajuste inventario    - Prefactura        - Postear traslado
- Picking                  - Devolucion proveedor - Polizas in/out    - Bonificaciones SAP
- Packing/Verificacion     - PDV                  - Servicios fact.   - Conversion decimales/UDS
- Despacho
- Push doc al ERP
```

## Convencion

Cada documento aqui debe:

1. Citar el **dato que lo respalda** (count, query, fecha de aprendizaje).
2. Distinguir **observado** (lo que esta pasando hoy en el legacy) vs **esperado** (lo que reserva-webapi/dev_2028 deberia hacer).
3. Marcar **brechas** entre catalogo (`*_tipo`, `*_estado`) y uso real.

## Metodo de aprendizaje

Las queries usadas estan en `/tmp/dbq/scripts/` del entorno del agente. Resultados materializados en `/tmp/dbq/out/`. Cuando una pregunta se responda, se actualiza el documento correspondiente y se cita el archivo JSON origen.

## Linaje de la Ciclo 7 (estado actual)

```
preguntas-ciclo-7.md (25 Q)
        |
        +---> respuestas-tarea-1.md      (Erik responde 3: P-08, P-10, P-18)
        +---> respuestas-tarea-2.md      (SQL agente responde/refina 9)
        +---> respuestas-ciclo-7.md     (Carol responde 25 + addendum EJC en P-18)
                       |
                       v
              consolidacion-ciclo-7.md  (cruce de las 3, sintesis maestra)
                       |
                       +---> _inbox/H-01..H-05.json (5 hallazgos accionables al brain)
                       +---> state-machine-pedido.md (a actualizar con D-01, D-04)
                       +---> bug-report-p16b.md (a actualizar con D-03)
```

**Resumen de avance:** 23/25 preguntas con respuesta consensuada. 1 vacio (P-23 Prefactura, sin informante). 4 contradicciones documentadas (C-01..C-04) con resolucion. 8 sub-preguntas abiertas, de las cuales 5 son respondibles por el agente SQL READ-ONLY (candidatas a Ciclo 8a autonoma).
