---
id: HALLAZGOS-PROD
tipo: test-scenario
estado: vigente
titulo: Hallazgos productivos vs escenarios del legacy
modulo: [reservation]
tags: [test-scenario, modulo/reservation]
---

# Hallazgos productivos vs escenarios del legacy

> Generado tras aprender configs reales de Killios/BYB/CEALSA (2026-04-27T14:56:31.611Z).

## Discrepancias detectadas

### RES-021 (CASO 17): mismo vencimiento alm → prefiere UDS sueltas

**Legacy**: asume `reservar_umbas_primero=true` y por eso el expected toma 7 UDS sueltas antes que romper la caja.

**Produccion**: `reservar_umbas_primero=false` en **TODAS** las bodegas de **TODOS** los clientes (Killios x6, BYB x2, CEALSA x2).

**Implicancia**: el escenario RES-021, tal como esta, **NO se cumple en ningun cliente productivo**. El comportamiento real sera "reserva 7 UDS rompiendo la caja A". Hay que revisar:
- Si el flag jamas estuvo activo en produccion (entonces el escenario es teorico).
- Si la semantica del flag esta invertida en mi documentacion (pero parece consistente).
- Si el escenario debe re-escribirse con expected "rompe caja".

### RES-019 vs RES-020 (CASOs 15 y 16): ambiguedad de explosion desde almacenamiento

**Legacy**: dos casos con datos casi identicos donde uno rechaza y otro explota la caja G en almacenamiento.

**Produccion**: Killios tiene `explosion_automatica_desde_ubicacion_picking=true` en todas las bodegas. Eso significa que **caja en almacenamiento NO es candidata a explotar**. Por lo tanto, **RES-020 (que espera explotar G en alm) NO se cumple en Killios**.

**Implicancia**: RES-020 podria aplicar solo a clientes con `explosion_automatica_desde_ubicacion_picking=false`. Marcar requires_config explicito.

### RES-022/023/024 (BYB): stubs sin datos

Estos escenarios siguen como stubs. **Accion**: capturar datos reales de BYB con SELECT contra `i_nav_ped_compra_enc`/`det`/`det_lote` para casos de transferencia tipicos y reemplazar los placeholders.

### Producto 47022 (hardcoded legacy)

NO existe en TOMWMS_KILLIOS_PRD. Era de otro ambiente o quedo huerfano. **Accion**: parametrizar el SKU por cliente. Killios tiene >X miles de productos; tomar uno representativo (ej: el mas movido en `i_nav_movimiento` ultimo mes).

## Flags que mi config-flags.md previo INVENTO (no existen en SQL)

Los siguientes nombres NO existen en `i_nav_config_enc`. Se inferian del codigo VB pero o estan en otra tabla o solo viven como properties en memoria:

- Considera_Bloqueo_Articulo
- Permite_Mezcla_Lotes_En_Pallet
- Considera_Cuarentena
- Permite_Stock_Negativo
- Bloquea_Pedido_Si_Vencido
- Modo_Multi_UM_En_Reserva
- Politica_Asignacion_Ubicacion
- Reserva_Por_Pedido / Por_Linea

**Accion**: en proximo ciclo, buscar estos comportamientos en otras tablas (`i_nav_producto`, `producto_estado`, `i_nav_ubicaciones`) o confirmar que solo viven en codigo.

## Flags productivos importantes que NO estaban en el legacy doc

Aparecieron en los datos reales y los agregue:

- `convertir_decimales_a_umbas` — CRITICO para Killios+SAP
- `interface_sap` y la familia `sap_control_*`
- `bodega_prorrateo`/`bodega_prorrateo1` — modelo de prorrateo cruzado Killios
- `bodega_facturacion` — bodega virtual no operativa
- `equiparar_cliente_con_propietario_en_doc_salida` — propio del 3PL
- `genera_lp` — generacion de license plate

Documentado en `reference/config-flags.md`.
