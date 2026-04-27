# Cliente CEALSA

> 3PL multi-cliente con bodega general + bodega fiscal, sin reabastecimiento, foco en stock jornada y prefacturación

## Identificación

- **Database (server compartido EC2 `52.41.114.122,1437`)**: `IMS4MB_CEALSA_QAS`
- **ERP**: sin ERP integrado
- **Rubro**: 3PL (logística para terceros)
- **Bodegas en producción**: 2

## Bodegas

| idBodega | Código | Nombre |
|---|---|---|
| 1 | B01 | BODEGA GENERAL |
| 2 | B02 | BODEGA FISCAL |

## Configuración aprendida (i_nav_config_enc)

Aprendida 2026-04-27T14:56:31.611Z desde `IMS4MB_CEALSA_QAS`. Bodega de referencia: id=1 (B01). Para diferencias intra-cliente, ver YAML.

| Flag | Valor | Significado |
|---|---|---|
| `control_vencimiento` | `false` | Activa lógica FEFO/vencimiento y control de fechas de caducidad |
| `control_lote` | `false` | Activa control de lote (trazabilidad por lote) |
| `control_peso` | `false` | Activa control de peso (productos a granel/peso variable) |
| `IdTipoRotacion` | `null` | 1=FIFO, 2=LIFO, 3=FEFO |
| `rechazar_pedido_incompleto` | `0` | 0=permisivo (procesa parcial), 1=estricto (rechaza si falta) - SEMANTICA INVERTIDA respecto al enum VB tRechazarPedidoIncompleto.No/Si |
| `despachar_existencia_parcial` | `0` | 0=no, 1=si |
| `convertir_decimales_a_umbas` | `0` | Convierte fracciones de presentación a unidades base. Critico para Killios+SAP (cajas + UDS decimales) |
| `explosion_automatica` | `false` | Permite explotar cajas a UDS automáticamente al reservar |
| `implosion_automatica` | `false` | Permite agrupar UDS en caja automáticamente |
| `explosion_automatica_desde_ubicacion_picking` | `null` | Si true, solo cajas en picking son candidatas a explotar (no almacenamiento) |
| `explosion_automatica_nivel_max` | `null` | Profundidad máxima de explosión recursiva. -1 = sin límite |
| `reservar_umbas_primero` | `false` | Cuando vencimiento empata, prefiere UDS sueltas antes de romper caja |
| `conservar_zona_picking_clavaud` | `null` | Mantiene asignación zona picking de Clavaud (cliente legacy) |
| `excluir_ubicaciones_reabasto` | `false` | Excluye ubicaciones marcadas como reabasto al reservar |
| `considerar_paletizado_en_reabasto` | `false` | Considera paletizado al generar tareas de reabasto |
| `considerar_disponibilidad_ubicacion_reabasto` | `false` | Verifica disponibilidad antes de planificar reabasto |
| `genera_lp` | `true` | Genera License Plate (pallet/tarima ID) en recepción |
| `interface_sap` | `false` | Activa integración con SAP B1 vía DI-API |
| `sap_control_draft_ajustes` | `false` | Crea ajustes SAP como draft (revisión manual antes de postear) |
| `sap_control_draft_traslados` | `false` | Crea traslados SAP como draft |
| `inferir_bonificacion_pedido_sap` | `false` | Detecta líneas bonificadas (UnitPrice=0) en pedidos SAP |
| `rechazar_bonificacion_incompleta` | `false` | Rechaza pedido si la bonificación no se puede cumplir completa |
| `equiparar_cliente_con_propietario_en_doc_salida` | `true` | En 3PL, el cliente del documento es el propietario del producto |
| `crear_recepcion_de_compra_nav` | `false` | Crea recepción NAV automaticamente al recibir orden de compra |
| `crear_recepcion_de_transferencia_nav` | `false` | Crea recepción NAV al recibir transferencia |
| `push_ingreso_nav_desde_hh` | `false` | Envia ingresos a NAV desde el handheld al cerrar |
| `generar_pedido_ingreso_bodega_destino` | `true` | Auto-genera pedido de ingreso en bodega destino al hacer traslado |
| `generar_recepcion_auto_bodega_destino` | `false` | Auto-genera recepción al recibir el traslado |
| `dias_vida_defecto_perecederos` | `0` | Dias por defecto a sumar como vencimiento si no viene en el documento |
| `IdProductoEstado` | `0` | Estado por defecto del producto al ingresar (1=disponible, 0=sin estado) |

## Modelo de operación

- 1 bodega general operativa + 1 bodega fiscal (passthrough, casi todos los flags apagados).
- Outputs principales del cliente: **stock jornada** (snapshot diario) y **prefacturación** (cobro al cliente 3PL por servicios prestados).
- **NO tiene proceso ni datos de reabastecimiento** (las tablas existen pero no se usan).

## Particularidades técnicas

- `equiparar_cliente_con_propietario_en_doc_salida=true` → el cliente del documento de salida es el propietario del producto (propio del modelo 3PL).
- **VW_Stock_Jornada con 74 columnas** → reporte canónico del 3PL para auditoría diaria.
- **trans_prefactura_*** (enc/det/mov, total 54 cols) → estructura completa de prefacturación.
- **Bodega fiscal** (idBodega=2) tiene `control_vencimiento=false`, `IdTipoRotacion=null`, `interface_sap=false` → es una bodega contable, no operativa.
- 12 objetos relacionados a jornada (vs 8 en las otras BDs), incluye `operador_jornada_laboral`, `jornada_laboral`, `jornada_sistema`.

## Implicancias para el bridge de tests

- Para escenarios de reserva, **solo aplica la bodega general**, no la fiscal.
- Para escenarios de stock jornada y prefacturación (no implementados aún en el bridge), CEALSA es el único cliente válido.
- La integración SAP/NAV no aplica → escenarios de ERP se saltan en CEALSA.
