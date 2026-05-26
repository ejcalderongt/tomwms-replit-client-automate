# La Cumbre QA - Matriz funcional de Cambio de Ubicación + Implosión

Fecha: 2026-05-26  
Ambiente: `TOMWMS_LA_CUMBRE_QA`  
Objetivo: homologar criterio funcional para flujo dirigido y no dirigido, respetando `ubic_implosion_auto`.

---

## Resumen ejecutivo

La regla de negocio debe ser la misma en ambos flujos (dirigido y no dirigido):

1. `ubic_implosion_auto` controla si se permite implosionar automáticamente licencia.
2. Si la ubicación destino no define licencia, no hay implosión.
3. Si la licencia destino es igual a la de origen, no hay implosión.
4. Solo cuando `ubic_implosion_auto=1` y licencia destino es distinta, se aplica implosión.

---

## Matriz de decisión

| Escenario | `ubic_implosion_auto` | ¿Ubicación destino tiene licencia? | ¿Licencia destino distinta a origen? | Resultado esperado |
|---|---:|---:|---:|---|
| A | 0 | No | N/A | Solo cambio de ubicación/estado. Licencia se conserva. |
| B | 0 | Sí | Sí/No | Solo cambio de ubicación/estado. No forzar implosión. |
| C | 1 | No | N/A | Solo cambio de ubicación/estado. No hay implosión. |
| D | 1 | Sí | No | Solo cambio de ubicación/estado. No hay implosión. |
| E | 1 | Sí | Sí | Flujo integrado: estado (si aplica) -> implosión -> ubicación. Licencia final = destino. |

---

## Contrato funcional homologado (Dirigido / No Dirigido)

- Homologar **la regla funcional**, no necesariamente la misma implementación técnica.
- La evaluación mínima previa al movimiento:
  - leer `ubic_implosion_auto`;
  - resolver licencia destino de la ubicación;
  - comparar con licencia origen.
- Resultado:
  - condición E: ejecutar flujo integrado;
  - resto de condiciones: ejecutar flujo estándar sin implosión.

---

## Riesgo evitado con esta homologación

Sin esta regla unificada, un flujo puede mover ubicación y otro además implosionar licencia para el mismo escenario, contaminando pruebas y generando diferencias operativas.

---

## Checklist QA sugerido

1. Probar escenarios A-E en flujo dirigido.
2. Probar escenarios A-E en flujo no dirigido.
3. Validar `stock`:
   - licencia final;
   - ubicación final;
   - `IdUbicacion_anterior`.
4. Validar `trans_movimientos`:
   - secuencia de movimientos;
   - consistencia de cantidades.
5. Validar visual HH:
   - consulta de existencias en ubicación destino.

---

## SQL de apoyo para validación

```sql
-- Parametros de bodega
SELECT IdBodega, ubic_implosion_auto, cambio_ubicacion_auto
FROM bodega
WHERE IdBodega = 89;

-- Stock de licencias involucradas
SELECT IdStock, IdProductoBodega, IdUbicacion, IdUbicacion_anterior, lic_plate, cantidad, activo, fec_agr, fec_mod
FROM stock
WHERE IdBodega = 89
  AND (lic_plate = 'QW000005636' OR lic_plate = 'JG000019879')
ORDER BY IdStock DESC;

-- Traza de movimientos recientes
SELECT TOP 200 IdMovimiento, IdTransaccion, IdProductoBodega,
       IdUbicacionOrigen, IdUbicacionDestino, cantidad, lic_plate, fecha, usuario_agr
FROM trans_movimientos
WHERE IdBodegaOrigen = 89
  AND fecha >= DATEADD(HOUR, -48, GETDATE())
  AND (lic_plate = 'QW000005636' OR lic_plate = 'JG000019879'
       OR IdUbicacionOrigen IN (112,113)
       OR IdUbicacionDestino IN (112,113))
ORDER BY IdMovimiento DESC;
```

---

## Referencias internas

- Fix BOF: `TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH/Transaccion_Ubicacion_Hh_Det/clsLnTrans_ubic_hh_det_Partial.vb`
- Diagnóstico de caso: `brain/debuged-cases/2026-05-26-la-cumbre-cambio-ubic-implosion/ANALISIS_Y_FIX.md`
- Script de traza: `tools/wms-deep-dive/diagnostics/cambio_ubicacion_implosion_trace.py`

