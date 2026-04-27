# wms-specific-process-flow

> Tuning de la comprension del comportamiento de WMS basado en evidencia productiva.
> Folder pedido por Erik (pasada 7+) para profundizar el mapeo de operaciones reales.

## Proposito

Este folder NO documenta como deberia funcionar WMS (eso es la skill `wms-test-bridge`). Documenta **como funciona realmente HOY** en las 3 BDs productivas, segun:

- Conteos reales de tablas y SPs.
- Estados observados en `trans_pe_enc.estado`, `trans_oc_estado`, etc.
- `Caso_Reserva` registrados en `trans_pe_det_log_reserva`.
- Discrepancias entre lo configurado y lo ejecutado.

Aqui es donde se aterriza la **terminologia ADR-010** (reserva-webapi vs reserva-WMS legacy) en cada proceso.

## Contenido

| Archivo | Que es |
|---|---|
| `README.md` | este archivo |
| `process-map.md` | mapa global de procesos del WMS con tablas activas, SPs y conteos productivos |
| `state-machine-pedido.md` | maquina de estados observada de `trans_pe_enc` con transiciones reales |
| `preguntas-pasada-7.md` | 25+ preguntas para Erik que afinan el mapeo. Se responden en futuras pasadas. |

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
