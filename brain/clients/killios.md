---
id: killios
tipo: client
estado: vigente
titulo: Cliente KILLIOS
clientes: [killios]
tags: [client, cliente/killios]
---

# Cliente KILLIOS

> Centro de abastecimiento de alimentos en Amatitlan, sucursal en Z17. ERP SAP Business One. **Modo estricto de reserva**.

## Identificacion

- **Database**: `TOMWMS_KILLIOS_PRD` (productiva, server EC2 `52.41.114.122,1437`)
- **ERP**: SAP Business One via DI-API
- **Rubro**: Alimentos perecederos
- **Bodegas operativas**: 6 + 1 virtual de facturacion

## Bodegas

| idBodega | Codigo | Nombre | Tipo |
|---|---|---|---|
| 1 | BOD1 | Bodega Principal | operativa (Amatitlan) |
| 2 | PRTOK | Bodega de Prorateo Kilio | prorrateo |
| 3 | PRTO | Bodega de Prorateo Garesa | prorrateo |
| 4 | BOD5 | Bodega Amatitlan | operativa (sucursal) |
| 5 | PRTK17 | Bodega de Prorateo Kilio Z17 | prorrateo |
| 6 | PRT17 | Bodega de Prorateo Garesa Z17 | prorrateo |
| (virtual) | **BOD7** | (no figura en SQL) | **facturacion** |

**BOD7** es bodega de facturacion virtual: aparece como `bodega_facturacion='BOD7'` en `i_nav_config_enc` para las 6 bodegas operativas, **pero no esta en la tabla `bodega`**. Hipotesis confirmada: desfase data Amazon vs SQL — en otras vistas/sistemas BOD7 sí existe. Se trata como bodega de facturacion real en el modelo del bridge.

## Configuracion aprendida (i_nav_config_enc)

Las 6 bodegas operativas tienen flags identicos salvo `bodega_prorrateo`/`bodega_prorrateo1`. Bodega de referencia: id=1 (BOD1).

| Flag | Valor | Significado |
|---|---|---|
| `control_vencimiento` | true | FEFO + control caducidad |
| `control_lote` | true | Trazabilidad por lote |
| `control_peso` | true | Productos peso-variable |
| `IdTipoRotacion` | 3 | FEFO |
| **`rechazar_pedido_incompleto`** | **1** | **ESTRICTO**: WMS reserva el TOTAL o aborta y avisa al ERP. NUNCA reserva parcial. |
| `despachar_existencia_parcial` | 1 | activado |
| **`convertir_decimales_a_umbas`** | **1** | **CRITICO**: convierte cajas decimales SAP a UDS internas |
| `explosion_automatica` | true | rompe cajas a UDS |
| `implosion_automatica` | true | agrupa UDS en cajas |
| `explosion_automatica_desde_ubicacion_picking` | true | solo cajas en picking explotan |
| `explosion_automatica_nivel_max` | -1 | sin limite (fuente de verdad) |
| `explosio_automatica_nivel_max` | 1 | DEPRECADA (typo historico, ALTER mal hecho) |
| `reservar_umbas_primero` | false | siempre prefiere romper caja |
| `conservar_zona_picking_clavaud` | true | zona Clavaud preservada |
| `excluir_ubicaciones_reabasto` | false | |
| `considerar_paletizado_en_reabasto` | false | (Killios no usa modulo reabasto) |
| `considerar_disponibilidad_ubicacion_reabasto` | false | |
| `genera_lp` | true | License Plate en recepcion |
| **`interface_sap`** | **true** | DI-API SAP B1 activo |
| `sap_control_draft_ajustes` | false | postea directo |
| `sap_control_draft_traslados` | false | postea directo |
| `inferir_bonificacion_pedido_sap` | false | NO infiere |
| `rechazar_bonificacion_incompleta` | false | |
| **`bodega_prorrateo`** | varia (PRT17/PRTO/etc.) | par cruzado por bodega |
| **`bodega_prorrateo1`** | varia (PRTK17/PRTOK/etc.) | segundo par |
| **`bodega_facturacion`** | **"BOD7"** | virtual no operativa |
| `bodega_faltante` | null | |
| `equiparar_cliente_con_propietario_en_doc_salida` | false | (no es 3PL) |
| `crear_recepcion_de_compra_nav` | false | Killios usa SAP, no NAV |
| `crear_recepcion_de_transferencia_nav` | false | |
| `push_ingreso_nav_desde_hh` | false | |
| `generar_pedido_ingreso_bodega_destino` | true | auto-pedido al trasladar |
| `generar_recepcion_auto_bodega_destino` | true | auto-recepcion en destino |
| `dias_vida_defecto_perecederos` | 0 | sin defecto (toma del doc) |
| `IdProductoEstado` | 1 | disponible por defecto |

### Pares de prorrateo

| Bodega | bodega_prorrateo | bodega_prorrateo1 |
|---|---|---|
| BOD1 | PRT17 | PRTK17 |
| PRTOK | PRTO | PRTOK |
| (otras bodegas) | (ver killios.yaml) | |

## Tipos de pedido (trans_pe_tipo)

| Id | Nombre | ReservaStock |
|---|---|---|
| 1 | PE0001 | SI |
| 3 | PE0003 | SI |
| 4 | PE0004 | NO |
| 6 | TRAS_WMS | NO (traslado interno) |
| 9 | PDV_NAV | SI |
| 12 | DEVPROV | SI |

`PE0004` y `TRAS_WMS` **no ejecutan el motor de reserva**. Son utiles para escenarios de "no reserva".

## Modelo de operacion

- **Centro de abastecimiento principal**: `BOD1` (Amatitlan).
- **Sucursal de despacho**: `BOD5` (Amatitlan sucursal) y bodegas Z17 (PRTK17/PRT17).
- **Bodegas de prorrateo**: `PRTOK`/`PRTO` (Amatitlan, dos entidades legales Kilio/Garesa) y `PRTK17`/`PRT17` (Z17). Ahi ingresa el inventario de compra internacional antes del costeo SAP. Tras costeo se ejecuta traslado al operativo.
- **Bodega de facturacion virtual**: `BOD7`, no operativa.

## Particularidades tecnicas

- **Modo estricto**: `rechazar_pedido_incompleto=1`. Si el motor no puede reservar el TOTAL, aborta y avisa al ERP. Sin reservas parciales.
- **Procesa cajas + decimales SAP** simultaneamente con `convertir_decimales_a_umbas=1`. Es el reto principal del cliente.
- **Integracion SAP B1 via DI-API**, NO modo draft.
- **Bonificaciones SAP**: NO se infieren ni se rechazan automaticamente.
- **Explosion automatica sin limite** (nivel_max=-1).
- **NO usa modulo de reabastecimiento de picking**: solo `trans_reabastecimiento_log` como auditoria.
- **Producto 47022** del legacy NO existe en Killios PRD. Parametrizar el SKU por cliente (no hardcoded).

## Comportamiento observado en reserva-WMS legacy

`trans_pe_det_log_reserva` registra estos casos en Killios PRD (top):
- CASO_#24 — 18,587 registros (camino feliz dominante)
- CASO_#8 — 1,675
- CASO_#21 — 1,065
- CASO_#12 — 276, CASO_#20 — 267, CASO_#23 — 125, CASO_#22 — 44, CASO_#9 — 1

Casos #1..7, #10, #11, #13..19, #25..27 **nunca se observaron en prod**. Ver `reference/casos-reserva-observados.md` y `adr/ADR-010`.

## Implicancias para el bridge

- Los escenarios contra Killios deben **simular cantidades fraccionarias** del lado del input para ejercitar la conversion decimal.
- El expected debe distinguir "esperado en reserva-webapi" vs "observado en reserva-WMS legacy" (ver ADR-010).
- Para escenarios SAP B1 (postear ajustes, traslados, recepciones de compra) usar Killios. Escenarios NAV-only van a BYB.
- Las 6 bodegas operativas son funcionalmente equivalentes salvo `bodega_prorrateo`/`bodega_prorrateo1`. Una corrida cubre las 6.
- Para escenarios sin reserva, usar `trans_pe_tipo` con `PE0004` o `TRAS_WMS`.
