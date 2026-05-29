---
protocolVersion: 1
type: learning
operator: ejc
createdAt: 2026-05-29T08:00:00Z
clientVersion: replit-agent/1.0
profile:
  codename: LA_CUMBRE
  environment: QA
title: Masa-update de fecha_packing bloquea Inserta_Packing en BOF
tags: [packing, trans_picking_ubic, trans_packing_enc, BOF, Inserta_Packing, QA-testing]
verdict: new-finding
relatedDocs:
  - brain/code-changes/HH/PATTERNS-PACKING-MATCHING.md
  - brain/learnings/L-019-packing-hh-fecha-vence-lic-plate-mismatch.md
---

## Hallazgo

Al hacer masa-update de `trans_picking_ubic.fecha_packing = GETDATE()` para "limpiar"
el picking en QA, el BOF (`Inserta_Packing`) deja de poder procesar nuevas licencias
de packing porque su primera validación busca registros con `fecha_packing = '19000101'`.

## Contexto QA (2026-05-29)

- Picking 3559 / Pedido 7622 / La Cumbre QA (TOMWMS_LA_CUMBRE_QA)
- Se hizo `INSERT INTO trans_packing_enc` + `UPDATE trans_picking_ubic SET fecha_packing=GETDATE()`
  para simular "todo empacado" y dejar la pantalla HH sin PENDIENTE
- El usuario reinició test con nueva licencia MM000021399
- 25 de 30 productos funcionaron correctamente (packing_enc del INSERT anterior
  los cubría con `cantidad_bultos_packing > 0`)
- 5 productos fallaron con: `"No existen ubicaciones verificadas para empacar el
  producto [X], licencia MM000021398"` — licencia MM000021398 es la anterior

## Causa raíz

El BOF en `Inserta_Packing` (o equivalente) valida:
```sql
SELECT * FROM trans_picking_ubic
WHERE IdPickingEnc = @picking AND IdProductoBodega = @prod
  AND fecha_packing = '1900-01-01'  -- o fecha_packing < '19010101'
```
Si retorna 0 filas → error "No existen ubicaciones verificadas...".

Al marcar TODAS las picking_ubic como empacadas, el BOF no puede procesar
las 5 que quedaron pendientes en la HH.

## Por qué 5 tuvieron cantidad=0 en el INSERT masivo

El INSERT masivo usó `SUM(COALESCE(cantidad_verificada, cantidad_solicitada))`.
Para productos con `cantidad_verificada = 0` (CERO, no NULL), COALESCE
devuelve 0 (no NULL), resultando en `cantidad_bultos_packing = 0`.
La HH compara `cant_packing (0) < cant_ubic (N)` → PENDIENTE.

## "licencia MM000021398" en el error

El BOF devuelve la LP que pasó la HH en el parámetro pLicencia. La HH
puede estar pasando la licencia anterior (de la sesión cargada en memoria)
para algunos items, o el BOF lee la no_linea del packing_enc existente
(que tiene no_linea='MM000021398') y la usa en el mensaje.

## Fix aplicado (QA)

```sql
UPDATE trans_picking_ubic
SET fecha_packing = '19000101', user_mod = 'EJC_QA_RESET', fec_mod = GETDATE()
WHERE IdPickingEnc = 3559 AND fecha_packing >= '19010101';
```
Esto devuelve todos los picking_ubic al estado "elegible para BOF" sin afectar
el estado visual de la HH (que lee PROCESADO desde trans_packing_enc, no desde
fecha_packing).

## Regla derivada

**Al limpiar datos QA de packing, NUNCA hacer masa-update de fecha_packing en
trans_picking_ubic.** En su lugar:
- Opción A (reset total): DELETE trans_packing_enc + UPDATE SET fecha_packing='19000101'
- Opción B (completar): INSERT trans_packing_enc con cantidades REALES
  (nunca COALESCE de 0) + UPDATE fecha_packing (pero esto bloquea re-test)

## Log trace confirma flujo correcto

```
WMS-PKD  [VERIF-PACK] EmpaqueTarima=true PackingConsolidado=false IdBodega=89 gIdPickingEnc=3559 gIdPedidoEnc=7622
```
EmpaqueTarima=true → La Cumbre usa el flujo correcto de packing-por-tarima.
PackingConsolidado=false → Packing_Consolidado_Guia está inerte (confirmado).
El trace WMS-PKD funciona correctamente en producción.
