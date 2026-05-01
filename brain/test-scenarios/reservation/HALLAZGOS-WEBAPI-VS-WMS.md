---
id: HALLAZGOS-WEBAPI-VS-WMS
tipo: test-scenario
estado: vigente
titulo: Diferencias entre reserva-webapi (esperado, dev_2028) y reserva-WMS (observado, legacy en prod)
ramas: [dev_2028_merge]
modulo: [reservation]
tags: [test-scenario, modulo/reservation]
---

# Diferencias entre reserva-webapi (esperado, dev_2028) y reserva-WMS (observado, legacy en prod)

> Generado tras aprender configs y log de auditoria de Killios/BYB/CEALSA (2026-04-27T15:31:09.941Z).
> Renombra a HALLAZGOS-PROD anterior. Terminologia formalizada en ADR-010.

## Contexto

El brain documenta el comportamiento ESPERADO de **reserva-webapi (.NET Core, dev_2028, sin publicar)**, que es el target de estabilizacion del bridge. Las divergencias contra el comportamiento OBSERVADO de **reserva-WMS legacy (VB, en prod hoy)** se documentan aqui como **correcciones que la nueva implementacion debe traer**, no como bugs del expected.

## Divergencias detectadas

### RES-021 (CASO 17): mismo vencimiento → preferir UDS sueltas

- **Esperado (reserva-webapi)**: con `reservar_umbas_primero=true`, toma 7 UDS sueltas antes de romper la caja.
- **Observado (reserva-WMS legacy)**: el flag esta en `false` en TODOS los clientes (Killios x6, BYB x2, CEALSA x2). El motor siempre rompe caja. **Nunca se ejecuta el caso del expected**.
- **Correccion que webapi debe traer**: respetar el flag cuando este en true. Si el cliente nunca lo pone en true, el caso es solo para verificacion de la implementacion.

### RES-019 vs RES-020: explosion desde almacenamiento

- **Esperado**: dos casos casi identicos donde uno rechaza y otro explota la caja G en almacenamiento.
- **Observado**: Killios tiene `explosion_automatica_desde_ubicacion_picking=true` → solo cajas en picking explotan, no almacenamiento. RES-020 (que espera explotar G en alm) **nunca aplicara en Killios**.
- **Correccion**: marcar `requires_config: explosion_automatica_desde_ubicacion_picking=false` explicito en RES-020.

### Casos del bridge sin evidencia productiva

De los 21 escenarios RES-001..024 + RES-DIN, solo 9 tienen evidencia en `trans_pe_det_log_reserva` de Killios PRD:

| Con evidencia | Sin evidencia (teoricos o target webapi) |
|---|---|
| RES-008, 009, 012, 020, 021, 022, 023, 024, RES-DIN | RES-001..007, 010, 011, 013..019 |

Ver `reference/casos-reserva-observados.md` para conteos. Los 12 sin evidencia son:
1. **Casos teoricos del legacy** (documentados en VB pero nunca disparados por datos reales), o
2. **Target del nuevo motor (reserva-webapi)** sin contraparte en prod actual.

### Politica estricta vs permisiva

- **Esperado y observado**: `rechazar_pedido_incompleto=1` en Killios (estricto). El motor reserva el TOTAL o aborta. NO hay reserva parcial.
- **Implicancia**: cualquier escenario con `expected_partial: true` es invalido contra Killios — debe marcar `requires_config: rechazar_pedido_incompleto=0` o cambiar el cliente.

### Conversion cajas/decimales SAP

- **Esperado**: con `convertir_decimales_a_umbas=1`, el motor traduce `3.5 cajas` a `N UDS` aplicando el factor de `i_nav_producto_presentacion.equivalencia`.
- **Observado**: activado en Killios prod, pero el algoritmo exacto no esta documentado en este brain. Pendiente de inspeccionar el codigo VB y formalizar.
- **Implicancia**: el bridge necesita parametrizar el factor de equivalencia por producto, NO hardcodearlo.

### Producto 47022 hardcoded

- **Esperado**: bridge parametriza el SKU por cliente.
- **Observado en data**: `47022` no existe en TOMWMS_KILLIOS_PRD. Era hardcoded del legacy en otro ambiente.
- **Correccion**: para Killios, resolver dinamicamente el SKU a usar (ej: el mas movido del ultimo mes via `i_nav_movimiento`).

### CEALSA — el motor del WMS no se invoca

- **Observado**: `trans_pe_det_log_reserva` tiene 0 registros en CEALSA QAS.
- **Modelo confirmado**: 3PL discrecional. El operador elige stock bajo peticion del cliente. La reserva se invoca solo si el tipo de pedido lo dice (`trans_pe_tipo.ReservaStock=true`) o el proceso lo solicita explicitamente.
- **Implicancia**: escenarios de reserva contra CEALSA son N/A salvo que se diseñen casos especificos del modelo 3PL.

## Flags productivos importantes que el legacy doc no consideraba

Aparecen en datos reales y se agregaron al brain en ciclo 7:

- `convertir_decimales_a_umbas` (Killios+SAP)
- `interface_sap` y la familia `sap_control_*`
- `bodega_prorrateo`/`bodega_prorrateo1` (modelo prorrateo cruzado Killios)
- `bodega_facturacion` (Killios = "BOD7" virtual)
- `equiparar_cliente_con_propietario_en_doc_salida` (3PL CEALSA)
- `genera_lp` (license plate)
- `trans_pe_tipo.ReservaStock` (define si el motor se invoca por tipo)
- `trans_pe_tipo.control_poliza` (CEALSA PE0001 y PE0005)

## Flags que mi documentacion previa INVENTO (no existen en SQL)

- Considera_Bloqueo_Articulo, Permite_Mezcla_Lotes_En_Pallet, Considera_Cuarentena, Permite_Stock_Negativo, Bloquea_Pedido_Si_Vencido, Modo_Multi_UM_En_Reserva, Politica_Asignacion_Ubicacion, Reserva_Por_Pedido / Por_Linea.

Fueron inferidos del codigo VB. Pendientes de relocalizar en otras tablas o confirmar como properties solo en memoria.
