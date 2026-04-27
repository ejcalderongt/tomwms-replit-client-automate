# Cliente KILLIOS

> Centro de abastecimiento de alimentos en Amatitlan, sucursal en Z17, ERP SAP Business One

## Identificación

- **Database (server compartido EC2 `52.41.114.122,1437`)**: `TOMWMS_KILLIOS_PRD`
- **ERP**: SAP Business One (DI-API para lectura/escritura de documentos)
- **Rubro**: Alimentos (perecederos)
- **Bodegas en producción**: 6

## Bodegas

| idBodega | Código | Nombre |
|---|---|---|
| 1 | BOD1 | Bodega Principal |
| 2 | PRTOK | Bodega de Prorateo Kilio |
| 3 | PRTO | Bodega de Prorateo Garesa |
| 4 | BOD5 | Bodega Amatitlan |
| 5 | PRTK17 | Bodega de Prorateo Kilio Z17 |
| 6 | PRT17 | Bodega de Prorateo Garesa Z17 |

## Configuración aprendida (i_nav_config_enc)

Aprendida 2026-04-27T14:56:31.611Z desde `TOMWMS_KILLIOS_PRD`. Bodega de referencia: id=1 (BOD1). Para diferencias intra-cliente, ver YAML.

| Flag | Valor | Significado |
|---|---|---|
| `control_vencimiento` | `true` | Activa lógica FEFO/vencimiento y control de fechas de caducidad |
| `control_lote` | `true` | Activa control de lote (trazabilidad por lote) |
| `control_peso` | `true` | Activa control de peso (productos a granel/peso variable) |
| `IdTipoRotacion` | `3` | 1=FIFO, 2=LIFO, 3=FEFO |
| `rechazar_pedido_incompleto` | `1` | 0=permisivo (procesa parcial), 1=estricto (rechaza si falta) - SEMANTICA INVERTIDA respecto al enum VB tRechazarPedidoIncompleto.No/Si |
| `despachar_existencia_parcial` | `1` | 0=no, 1=si |
| `convertir_decimales_a_umbas` | `1` | Convierte fracciones de presentación a unidades base. Critico para Killios+SAP (cajas + UDS decimales) |
| `explosion_automatica` | `true` | Permite explotar cajas a UDS automáticamente al reservar |
| `implosion_automatica` | `true` | Permite agrupar UDS en caja automáticamente |
| `explosion_automatica_desde_ubicacion_picking` | `true` | Si true, solo cajas en picking son candidatas a explotar (no almacenamiento) |
| `explosion_automatica_nivel_max` | `-1` | Profundidad máxima de explosión recursiva. -1 = sin límite |
| `explosio_automatica_nivel_max` | `1` | TYPO HISTORICO del schema. Coexiste con la columna "explosion_". Definir fuente de verdad en ADR |
| `reservar_umbas_primero` | `false` | Cuando vencimiento empata, prefiere UDS sueltas antes de romper caja |
| `conservar_zona_picking_clavaud` | `true` | Mantiene asignación zona picking de Clavaud (cliente legacy) |
| `excluir_ubicaciones_reabasto` | `false` | Excluye ubicaciones marcadas como reabasto al reservar |
| `considerar_paletizado_en_reabasto` | `false` | Considera paletizado al generar tareas de reabasto |
| `considerar_disponibilidad_ubicacion_reabasto` | `false` | Verifica disponibilidad antes de planificar reabasto |
| `genera_lp` | `true` | Genera License Plate (pallet/tarima ID) en recepción |
| `interface_sap` | `true` | Activa integración con SAP B1 vía DI-API |
| `sap_control_draft_ajustes` | `false` | Crea ajustes SAP como draft (revisión manual antes de postear) |
| `sap_control_draft_traslados` | `false` | Crea traslados SAP como draft |
| `inferir_bonificacion_pedido_sap` | `false` | Detecta líneas bonificadas (UnitPrice=0) en pedidos SAP |
| `rechazar_bonificacion_incompleta` | `false` | Rechaza pedido si la bonificación no se puede cumplir completa |
| `bodega_prorrateo` | `"PRT17"` | Bodega contraparte para prorrateo de costos (pares cruzados Z17 ↔ Amatitlan) |
| `bodega_prorrateo1` | `"PRTK17"` | Segunda bodega de prorrateo (cuando hay más de un par) |
| `bodega_facturacion` | `"BOD7"` | Bodega virtual donde se asienta la facturación (no operativa). Killios usa BOD7 |
| `bodega_faltante` | `null` | Bodega contable destino de faltantes/mermas |
| `equiparar_cliente_con_propietario_en_doc_salida` | `false` | En 3PL, el cliente del documento es el propietario del producto |
| `crear_recepcion_de_compra_nav` | `false` | Crea recepción NAV automaticamente al recibir orden de compra |
| `crear_recepcion_de_transferencia_nav` | `false` | Crea recepción NAV al recibir transferencia |
| `push_ingreso_nav_desde_hh` | `false` | Envia ingresos a NAV desde el handheld al cerrar |
| `generar_pedido_ingreso_bodega_destino` | `true` | Auto-genera pedido de ingreso en bodega destino al hacer traslado |
| `generar_recepcion_auto_bodega_destino` | `true` | Auto-genera recepción al recibir el traslado |
| `dias_vida_defecto_perecederos` | `0` | Dias por defecto a sumar como vencimiento si no viene en el documento |
| `IdProductoEstado` | `1` | Estado por defecto del producto al ingresar (1=disponible, 0=sin estado) |

## Modelo de operación

- **Centro de abastecimiento principal**: bodega `BOD1` (Bodega Principal, en Amatitlan).
- **Sucursal de despacho a clientes finales**: bodega `BOD5` (Amatitlan, sucursal) y bodegas Z17.
- **Bodegas de prorrateo** (donde ingresa el inventario por compras internacionales antes de costeo):
  - `PRTOK` (Bodega de Prorateo Kilio, principal) → bodega de prorrateo de `BOD1`.
  - `PRTO` (Bodega de Prorateo Garesa) → bodega de prorrateo cruzado de la otra entidad legal.
  - `PRTK17` y `PRT17` → equivalentes pero para la sucursal Z17.
- Una vez costeado en SAP, se ejecuta un traslado de prorrateo → bodega general.
- Desde Z17 se abastecen pedidos a clientes finales y/o transferencias a las bodegas operativas.
- **Bodega de facturación virtual**: `BOD7`, no operativa, solo asiento contable.

## Particularidades técnicas

- **Procesa pedidos en cajas Y unidades decimales** simultáneamente. Reto principal: SAP envia cantidades fraccionarias y el WMS las convierte a unidades base via flag `convertir_decimales_a_umbas=1`.
- **Integración SAP B1 via DI-API** (`interface_sap=true`). NO usa modo draft (`sap_control_draft_*=false` → postea directo).
- **Bonificaciones SAP**: NO se infieren ni se rechazan automáticamente (`inferir_bonificacion_pedido_sap=false`).
- **Explosión automática sin límite** (`explosion_automatica_nivel_max=-1`): puede romper cajas recursivamente hasta UDS sueltas.
- **NO usa módulo de reabastecimiento de picking**: solo tiene la tabla `trans_reabastecimiento_log` como auditoría; las tablas `zona_picking`, `producto_rellenado` están presentes pero sin uso operativo.
- **Producto 47022 NO existe en Killios PRD** — el hardcode del legacy era de otro ambiente. El bridge debe parametrizar el SKU.

## Implicancias para el bridge de tests

- Para escenarios de reserva, **todas las bodegas Killios son equivalentes** salvo en `bodega_prorrateo`/`bodega_prorrateo1`. Una sola corrida cubre las 6.
- Para escenarios SAP B1 (postear ajustes, traslados, recepciones de compra) hay que usar Killios. NAV-only escenarios van a BYB.
- El escenario RES-021 (mismo vencimiento → prefiere UDS sueltas) **NO se cumple en Killios**: `reservar_umbas_primero=false` significa que prefiere romper caja. Revisar expected.
- Validar conversión cajas↔UDS con factor de presentación SAP (esto requiere joins con `i_nav_producto_presentacion`).
