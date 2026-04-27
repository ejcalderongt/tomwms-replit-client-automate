# Flags de configuracion WMS (i_nav_config_enc) — v2 corregido

> Aprendido de **i_nav_config_enc** en TOMWMS_KILLIOS_PRD, IMS4MB_BYB_PRD e IMS4MB_CEALSA_QAS.
> v2 (2026-04-27T15:25:50.327Z): correcciones de Erik sobre semantica y typos.

## Esquema

Tabla base `i_nav_config_enc`, 69 columnas. PK `idnavconfigenc`, clave funcional `(idempresa, idbodega, idPropietario)`. Todas las consultas filtran `idEmpresa=1`.

`VW_Configuracioninv` solo expone metadatos (8 cols), **NO los flags**.

## Politica de pedido incompleto — INTERPRETACION FIRME

| Flag | Tipo | Semantica CONFIRMADA |
|---|---|---|
| `rechazar_pedido_incompleto` | int | `1` = **ESTRICTO**: WMS DEBE reservar el TOTAL de TODOS los productos solicitados en el documento. Escribe en `i_nav_ped_traslado_enc`/`det`, `trans_pe_enc`/`det`, `stock_res`. **Si NO puede reservar el total, AVISA AL ERP que no pudo procesar el pedido y aborta.** `0` = permisivo: procesa parcial. |

**Killios prod tiene `1` en TODAS las bodegas** → modo estricto.

Esto significa que un escenario de reserva en Killios tiene 2 outcomes posibles:
1. **Reserva completa** → escribe stock_res + trans_pe_det con todas las cantidades.
2. **Falla atomica** → no escribe nada y notifica al ERP. NO hay reserva parcial.

El log `trans_pe_det_log_reserva` registra ambos casos (con `EsError=1` cuando falla).

| Flag | Tipo | Notas |
|---|---|---|
| `despachar_existencia_parcial` | int | Independiente del anterior. Activado en Killios y BYB (=1). |

## Vencimiento, lote, peso

| Columna | Tipo | Descripcion |
|---|---|---|
| `control_vencimiento` | bit | FEFO + control caducidad |
| `control_lote` | bit | Trazabilidad por lote |
| `control_peso` | bit | Productos peso-variable |

## Rotacion

| Columna | Valores |
|---|---|
| `IdTipoRotacion` | 1=FIFO, 2=LIFO, 3=FEFO |
| `IdIndiceRotacion` | indice secundario |

## Conversion cajas/UDS (Killios+SAP)

| Columna | Descripcion |
|---|---|
| `convertir_decimales_a_umbas` | **Killios=1**. Convierte fracciones de presentacion (cajas decimales SAP) a UDS internas. Ver ADR-007. |

## Explosion automatica — CON FUENTE DE VERDAD CORREGIDA

| Columna | Estado |
|---|---|
| `explosion_automatica` | OK — en uso |
| `implosion_automatica` | OK — en uso |
| `explosion_automatica_desde_ubicacion_picking` | OK — solo cajas en picking |
| `explosion_automatica_nivel_max` | **FUENTE DE VERDAD** (con la `n`). Killios = -1 (sin limite). |
| `explosio_automatica_nivel_max` | **DEPRECADA**. Sin la `n`. Resultado de un `ALTER TABLE` historico mal ejecutado. **NO USAR**. Coexiste con valor divergente (Killios = 1) pero el motor lee la version correcta. Ver ADR-010. |
| `Ejecutar_En_Despacho_Automaticamente` | bit, auto-explosion al despachar |

## Prioridad UDS sueltas

| Columna | Notas |
|---|---|
| `reservar_umbas_primero` | TODOS los clientes = false → siempre prefiere romper caja. El expected del escenario RES-021 corresponde a la **reserva-webapi (dev_2028)**, no a la reserva-WMS legacy actual. Ver way-of-thinking. |

## Picking y zonas (BYB usa esto activamente)

| Columna | Notas |
|---|---|
| `conservar_zona_picking_clavaud` | preserva asignacion zona (cliente legacy "Clavaud") |
| `excluir_ubicaciones_reabasto` | excluye ubicaciones reabasto al reservar |
| `considerar_paletizado_en_reabasto` | respeta cantidad por pallet |
| `considerar_disponibilidad_ubicacion_reabasto` | verifica espacio en destino |

## License Plate / pallet

| Columna | |
|---|---|
| `genera_lp` | Genera identificador pallet en recepcion |

## Integracion ERP (SAP / NAV)

| Columna | Notas |
|---|---|
| `interface_sap` | Killios=true. Activa DI-API SAP B1 |
| `sap_control_draft_ajustes` | Killios=false → postea directo |
| `sap_control_draft_traslados` | Killios=false |
| `inferir_bonificacion_pedido_sap` | Killios=false. Detecta lineas UnitPrice=0 |
| `rechazar_bonificacion_incompleta` | Killios=false |
| `crear_recepcion_de_compra_nav` | Auto-recepcion NAV en orden compra |
| `crear_recepcion_de_transferencia_nav` | Auto-recepcion NAV en transferencia |
| `push_ingreso_nav_desde_hh` | Push automatico al cerrar handheld |

## Bodegas relacionadas (modelo Killios prorrateo)

| Columna | Notas |
|---|---|
| `bodega_prorrateo` | Pares cruzados Killios: BOD1↔PRT17, PRTOK↔PRTO, etc. |
| `bodega_prorrateo1` | Segunda bodega de prorrateo |
| `bodega_facturacion` | **Killios = "BOD7"** en las 6 bodegas. Bodega virtual no operativa para asiento contable. **Anomalia documentada**: BOD7 NO aparece en la tabla `bodega` (probable desfase data Amazon vs SQL). Tratada como bodega de facturacion real en el modelo. |
| `bodega_faltante` | Bodega contable de mermas/faltantes |
| `Codigo_Bodega_ERP_NC` | Mapeo codigo bodega ERP para nota credito |

## Traslados auto

| Columna | |
|---|---|
| `generar_pedido_ingreso_bodega_destino` | Auto-pedido de ingreso al hacer traslado |
| `generar_recepcion_auto_bodega_destino` | Auto-recepcion en destino |

## 3PL (CEALSA)

| Columna | Notas |
|---|---|
| `equiparar_cliente_con_propietario_en_doc_salida` | CEALSA=true. Cliente del doc = propietario del producto |
| `idPropietario` | Propietario del producto |

## Otros

`dias_vida_defecto_perecederos`, `IdProductoEstado`, `IdProductoEstado_NC`, `vence_defecto_nc`, `Lote_Defecto_Entrada_NC`, `IdAcuerdoEnc`, `Rango_Dias_Importacion`, `equiparar_productos`, `valida_solo_codigo`, `excluir_recepcion_picking`.

## Tablas afectadas por el flujo de reserva

Cuando el motor ejecuta una reserva (estricta o parcial), las tablas que se escriben son:

- `i_nav_ped_traslado_enc` / `i_nav_ped_traslado_det` / `i_nav_ped_traslado_det_lote` — pedido de traslado contra el ERP
- `trans_pe_enc` (70 cols) / `trans_pe_det` (44 cols) — pedido interno del WMS
- `stock_res` (35 cols) — reservas de stock activas
- `trans_pe_det_log_reserva` (17 cols) — auditoria de cada caso de reserva ejecutado (`Caso_Reserva`, `MensajeLog`, `EsError`)

Detalle de schemas en `reference/reserva-tables.md`.

## Tipos de pedido (trans_pe_tipo)

El flag `ReservaStock` por tipo determina si el motor de reserva se invoca:

| Cliente | Tipos con ReservaStock=SI | Tipos con ReservaStock=NO |
|---|---|---|
| Killios | PE0001, PE0003, PDV_NAV, DEVPROV | PE0004, TRAS_WMS |
| BYB | PE0001, PE0002, PE0003, Requisicion, PDV_NAV | PE0004 |
| CEALSA | PE0001, PE0002, PE0003, PE0005, Requisicion, PDV_NAV | PE0004 |

CEALSA ademas tiene `control_poliza=SI` en PE0001 y PE0005 (modelo 3PL).

## Flags que aparecian en mi documentacion previa pero NO existen en SQL

Inferidos del codigo VB. A confirmar/relocalizar (probablemente en otras tablas o solo en memoria):

- `Considera_Bloqueo_Articulo`, `Permite_Mezcla_Lotes_En_Pallet`, `Considera_Cuarentena`, `Permite_Stock_Negativo`, `Bloquea_Pedido_Si_Vencido`, `Modo_Multi_UM_En_Reserva`, `Politica_Asignacion_Ubicacion`, `Reserva_Por_Pedido` vs `Por_Linea`.

## Origen aprendido

Aprendido el 2026-04-27T15:25:50.327Z desde `52.41.114.122,1437` con `SELECT c.*, b.* FROM i_nav_config_enc c LEFT JOIN bodega b ON ... WHERE c.idEmpresa=1`. Bodegas: Killios=6, BYB=2, CEALSA=2.
