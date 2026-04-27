# Cliente BYB

> Cliente de alimentos con módulo activo de reabastecimiento de picking, ERP NAV Dynamics

## Identificación

- **Database (server compartido EC2 `52.41.114.122,1437`)**: `IMS4MB_BYB_PRD`
- **ERP**: Microsoft Dynamics NAV
- **Rubro**: Alimentos (manejan expiración, lote y licencias)
- **Bodegas en producción**: 2

## Bodegas

| idBodega | Código | Nombre |
|---|---|---|
| 2 | BA0002 | PRODUCTO TERMINADO |
| 24 | BA0024 | PRODUCTO TERMINADO DAÑADO |

## Configuración aprendida (i_nav_config_enc)

Aprendida 2026-04-27T14:56:31.611Z desde `IMS4MB_BYB_PRD`. Bodega de referencia: id=2 (BA0002). Para diferencias intra-cliente, ver YAML.

| Flag | Valor | Significado |
|---|---|---|
| `control_vencimiento` | `null` | Activa lógica FEFO/vencimiento y control de fechas de caducidad |
| `control_lote` | `null` | Activa control de lote (trazabilidad por lote) |
| `control_peso` | `null` | Activa control de peso (productos a granel/peso variable) |
| `IdTipoRotacion` | `3` | 1=FIFO, 2=LIFO, 3=FEFO |
| `rechazar_pedido_incompleto` | `0` | 0=permisivo (procesa parcial), 1=estricto (rechaza si falta) - SEMANTICA INVERTIDA respecto al enum VB tRechazarPedidoIncompleto.No/Si |
| `despachar_existencia_parcial` | `1` | 0=no, 1=si |
| `convertir_decimales_a_umbas` | `1` | Convierte fracciones de presentación a unidades base. Critico para Killios+SAP (cajas + UDS decimales) |
| `explosion_automatica` | `true` | Permite explotar cajas a UDS automáticamente al reservar |
| `implosion_automatica` | `true` | Permite agrupar UDS en caja automáticamente |
| `explosion_automatica_desde_ubicacion_picking` | `true` | Si true, solo cajas en picking son candidatas a explotar (no almacenamiento) |
| `explosion_automatica_nivel_max` | `1` | Profundidad máxima de explosión recursiva. -1 = sin límite |
| `reservar_umbas_primero` | `true` | Cuando vencimiento empata, prefiere UDS sueltas antes de romper caja |
| `conservar_zona_picking_clavaud` | `true` | Mantiene asignación zona picking de Clavaud (cliente legacy) |
| `excluir_ubicaciones_reabasto` | `true` | Excluye ubicaciones marcadas como reabasto al reservar |
| `considerar_paletizado_en_reabasto` | `true` | Considera paletizado al generar tareas de reabasto |
| `considerar_disponibilidad_ubicacion_reabasto` | `true` | Verifica disponibilidad antes de planificar reabasto |
| `genera_lp` | `true` | Genera License Plate (pallet/tarima ID) en recepción |
| `interface_sap` | `false` | Activa integración con SAP B1 vía DI-API |
| `sap_control_draft_ajustes` | `false` | Crea ajustes SAP como draft (revisión manual antes de postear) |
| `sap_control_draft_traslados` | `false` | Crea traslados SAP como draft |
| `inferir_bonificacion_pedido_sap` | `false` | Detecta líneas bonificadas (UnitPrice=0) en pedidos SAP |
| `rechazar_bonificacion_incompleta` | `false` | Rechaza pedido si la bonificación no se puede cumplir completa |
| `bodega_prorrateo` | `null` | Bodega contraparte para prorrateo de costos (pares cruzados Z17 ↔ Amatitlan) |
| `bodega_prorrateo1` | `null` | Segunda bodega de prorrateo (cuando hay más de un par) |
| `bodega_facturacion` | `null` | Bodega virtual donde se asienta la facturación (no operativa). Killios usa BOD7 |
| `equiparar_cliente_con_propietario_en_doc_salida` | `false` | En 3PL, el cliente del documento es el propietario del producto |
| `crear_recepcion_de_compra_nav` | `true` | Crea recepción NAV automaticamente al recibir orden de compra |
| `crear_recepcion_de_transferencia_nav` | `true` | Crea recepción NAV al recibir transferencia |
| `push_ingreso_nav_desde_hh` | `false` | Envia ingresos a NAV desde el handheld al cerrar |
| `generar_pedido_ingreso_bodega_destino` | `true` | Auto-genera pedido de ingreso en bodega destino al hacer traslado |
| `generar_recepcion_auto_bodega_destino` | `true` | Auto-genera recepción al recibir el traslado |
| `dias_vida_defecto_perecederos` | `0` | Dias por defecto a sumar como vencimiento si no viene en el documento |
| `IdProductoEstado` | `1` | Estado por defecto del producto al ingresar (1=disponible, 0=sin estado) |

## Modelo de operación

- 2 bodegas operativas. Toda la operación de picking se ordena por **zonas y tramos** definidos.
- Reglas de **rellenado de picking** activas: cuando una ubicación de picking baja del minimo, se genera tarea de reabastecimiento desde almacenamiento.
- Maneja producto en **presentación + unidades enteras**: dificilmente queden cantidades fraccionarias, aunque algunos documentos puntuales sí pueden tener fracciones.

## Particularidades técnicas

- **Módulo de reabastecimiento exclusivo** (no presente operativamente en Killios ni CEALSA):
  - Tablas dedicadas: `zona_picking`, `zona_picking_tramo`, `producto_rellenado`, `operador_zona_picking_tramo`.
  - Vistas: `VW_ProductoRellenado`, `VW_MinimosMaximosPorPresentacion`, `VW_rptMinimosMaximos`, `VW_rptMinimosMaximos_v2`.
  - 38 SPs adicionales vs Killios (la mayoría de mantenimiento Ola Hallengren + ASP.NET Membership legacy).
- **NO usa SAP** (`interface_sap=false`).
- En esta BD se **estabilizaron muchos de los casos de reserva** del legacy. Es la fuente más confiable de comportamiento esperado.
- Variabilidad intra-cliente única: `control_peso` está en `null` para una bodega y `true` para la otra (probable: una maneja peso variable, la otra no).

## Implicancias para el bridge de tests

- Para escenarios de reabastecimiento de picking (no implementados aún en el bridge), BYB es el único cliente válido.
- Para validar comportamiento de reservas que dependen del legacy "estabilizado", BYB es la referencia.
- Si el bridge replica el módulo de reabasto, hay que portar la lógica de `producto_rellenado` y la jerarquía zona→tramo→ubicación.
