---
protocolVersion: 1
slug: 2026-05-28-hh-packing-fecha-vence-lic-plate
estado: aplicado
fecha_aplicado: 2026-05-28T21:30:00Z
operador: replit
---

# Resultado

## Resumen

Fix aplicado directamente por Replit en sesión del 2026-05-28.
No requirió handoff a Codex local — los cambios son solo en Java HH.

## Commits

| Repo | Rama | SHA | Mensaje |
|---|---|---|---|
| TOMHH2025 | dev_2028_merge | `4eba8be7` | `#EJCRP fix(packing-hh): creaListaLotes guard neto<=0 + listItems PENDIENTE/PROCESADO independiente #EJC20260528` |
| TOMHH2025 | dev_2028_merge | `1fb9f125` | `#EJCRP fix(packing-hh): normalizar fecha_vence + Lic_plate|No_linea en listItems y creaListaLotes #EJC20260528` |
| TOMWMS_BOF | dev_2028_merge | `2d845e6` | fix Get_All_PickingUbic_For_Packing filtro fecha_packing |
| TOMWMS_BOF | dev_2028_merge | `5d7fb80f` | `#EJCRP fix(packing): Inserta_Packing separa INSERT/UPDATE + reset Fecha_packing en Elimina_Packing` |

## Validación QA

- Cliente: La Cumbre QA (`TOMWMS_LA_CUMBRE_QA`)
- Picking: 3559, Pedido: 7623
- Resultado: **PENDIENTE: 0 / PROCESADO: 4** tras empacar todas las líneas
- LP en lista: muestra `JJ000020918` (picking LP correcto)

## Incidentes

Ninguno.

## Pendiente

Evaluar en siguiente sesión si el WS `Get_All_PickingUbic_By_PickingEnc`
debe corregirse en BOF para devolver el `Lic_plate` original del ubic
en lugar del `No_linea` del packing — el fix HH actual es tolerante a
ambos valores (LP original o LP de packing), lo que es robusto pero
no elimina la ambigüedad en el WS.
