# Flags de configuracion WMS (i_nav_config_enc)

> Aprendido de **i_nav_config_enc** en TOMWMS_KILLIOS_PRD, IMS4MB_BYB_PRD e IMS4MB_CEALSA_QAS.
> Esta tabla es la **fuente unica de verdad** de configuracion por bodega.
> La vista `VW_Configuracioninv` solo expone metadatos (idBodega, nombre, propietario), NO los flags.

## Esquema

`i_nav_config_enc` tiene **69 columnas**. Clave: `idnavconfigenc` (PK) + `(idempresa, idbodega, idPropietario)` (clave funcional). Todas las consultas filtran `idEmpresa=1`.

## Flags relevantes para reservas

### Vencimiento, lote, peso
| Columna | Tipo | Descripcion |
|---|---|---|
| `control_vencimiento` | bit | Activa logica FEFO y control de caducidad |
| `control_lote` | bit | Activa trazabilidad por lote |
| `control_peso` | bit | Productos peso-variable (granel) |

### Rotacion
| Columna | Tipo | Valores |
|---|---|---|
| `IdTipoRotacion` | int | 1=FIFO, 2=LIFO, 3=FEFO |
| `IdIndiceRotacion` | int | Indice secundario (uso interno) |

### Politica de pedido incompleto **CON TRAMPA SEMANTICA**
| Columna | Tipo | Semantica |
|---|---|---|
| `rechazar_pedido_incompleto` | int | **0 = permisivo (procesa lo que hay), 1 = estricto (rechaza si falta)**. **OJO**: el enum VB `tRechazarPedidoIncompleto` tenia `.No=0` y `.Si=1`. La interpretacion correcta segun datos productivos: `1` = "si rechaza si esta incompleto" = estricto. **PERO Killios prod tiene `1` en todas las bodegas y opera procesando parciales** → revisar la implementacion del lado del motor para confirmar la semantica real. |
| `despachar_existencia_parcial` | int | 0/1. Activado en Killios (=1) y BYB. |

### Conversion cajas/UDS (critico para Killios+SAP)
| Columna | Tipo | Descripcion |
|---|---|---|
| `convertir_decimales_a_umbas` | int | **Killios = 1**. Convierte fracciones de presentacion (cajas decimales SAP) a unidades base internas. Sin este flag, los pedidos SAP con cantidades fraccionarias no se procesan correctamente. |

### Explosion automatica
| Columna | Tipo | Descripcion |
|---|---|---|
| `explosion_automatica` | bit | Permite romper cajas a UDS al reservar |
| `implosion_automatica` | bit | Agrupa UDS en cajas al recepcionar |
| `explosion_automatica_desde_ubicacion_picking` | bit | Si true, solo cajas EN PICKING explotan (no almacenamiento). En Killios = true. |
| `explosion_automatica_nivel_max` | int | Profundidad max recursiva. **Killios = -1 (sin limite)**. |
| `explosio_automatica_nivel_max` | int | **TYPO HISTORICO** sin la `n`. Killios = 1. Coexiste con la otra columna; valores divergentes. **Bug conocido — definir fuente de verdad en ADR**. |
| `Ejecutar_En_Despacho_Automaticamente` | bit | Auto-explosion al despachar |

### Prioridad UDS sueltas
| Columna | Tipo | Descripcion |
|---|---|---|
| `reservar_umbas_primero` | bit | Si true: con vencimiento igual, prefiere UDS sueltas sobre romper caja. **TODOS los clientes en prod = false** → siempre prefiere caja. El escenario RES-021 del legacy (que asumia true) NO refleja produccion. |

### Picking y zonas (BYB usa esto activamente)
| Columna | Tipo | Descripcion |
|---|---|---|
| `conservar_zona_picking_clavaud` | bit | Cliente legacy "Clavaud" — preserva asignacion zona |
| `excluir_ubicaciones_reabasto` | bit | Excluye ubicaciones marcadas reabasto al reservar |
| `considerar_paletizado_en_reabasto` | bit | Considera paletizado al planificar reabasto |
| `considerar_disponibilidad_ubicacion_reabasto` | bit | Verifica disponibilidad antes de planificar |

### License Plate / pallet
| Columna | Tipo | Descripcion |
|---|---|---|
| `genera_lp` | bit | Genera identificador de pallet/tarima en recepcion |

### Integracion ERP (SAP / NAV)
| Columna | Tipo | Descripcion |
|---|---|---|
| `interface_sap` | bit | **Killios = true**. Activa DI-API SAP B1. |
| `sap_control_draft_ajustes` | bit | Postea ajustes SAP como draft (review manual). Killios = false → directo. |
| `sap_control_draft_traslados` | bit | Postea traslados SAP como draft. Killios = false. |
| `inferir_bonificacion_pedido_sap` | bit | Detecta lineas con UnitPrice=0 como bonificacion. Killios = false. |
| `rechazar_bonificacion_incompleta` | bit | Rechaza si bonificacion no se cumple completa. Killios = false. |
| `crear_recepcion_de_compra_nav` | bit | Auto-recepcion NAV en orden compra |
| `crear_recepcion_de_transferencia_nav` | bit | Auto-recepcion NAV en transferencia |
| `push_ingreso_nav_desde_hh` | bit | Push automatico al cerrar handheld |

### Bodegas relacionadas (modelo Killios prorrateo)
| Columna | Tipo | Descripcion |
|---|---|---|
| `bodega_prorrateo` | nvarchar | Bodega contraparte de costeo. En Killios pares cruzados: BOD1↔PRT17, PRTOK↔PRTO, etc. |
| `bodega_prorrateo1` | nvarchar | Segunda bodega de prorrateo cuando hay mas de un par |
| `bodega_facturacion` | nvarchar | Bodega virtual no operativa para asiento contable. Killios = "BOD7" |
| `bodega_faltante` | nvarchar | Bodega contable de mermas/faltantes |
| `Codigo_Bodega_ERP_NC` | nvarchar | Mapeo codigo bodega en ERP para nota credito |

### Traslados auto
| Columna | Tipo | Descripcion |
|---|---|---|
| `generar_pedido_ingreso_bodega_destino` | bit | Auto-pedido de ingreso al hacer traslado |
| `generar_recepcion_auto_bodega_destino` | bit | Auto-recepcion en destino |

### 3PL (CEALSA)
| Columna | Tipo | Descripcion |
|---|---|---|
| `equiparar_cliente_con_propietario_en_doc_salida` | bit | **CEALSA = true**. Cliente del doc = propietario del producto. |
| `idPropietario` | int | Propietario del producto |

### Otros relevantes
| Columna | Tipo | Descripcion |
|---|---|---|
| `dias_vida_defecto_perecederos` | int | Dias por defecto a sumar como vencimiento si no viene en doc |
| `IdProductoEstado` | int | Estado por defecto del producto al ingresar (1=disponible, 0=sin estado) |
| `IdProductoEstado_NC` | int | Estado por defecto en nota credito |
| `vence_defecto_nc` | datetime | Vencimiento por defecto en nota credito |
| `Lote_Defecto_Entrada_NC` | nvarchar | Lote por defecto en nota credito |
| `IdAcuerdoEnc` | int | Acuerdo comercial por defecto |
| `Rango_Dias_Importacion` | int | Ventana de dias para importar pedidos del ERP |
| `equiparar_productos` | bit | Politica de equiparacion de productos equivalentes |
| `valida_solo_codigo` | bit | Valida solo por codigo (sin lote/vencimiento) |
| `excluir_recepcion_picking` | bit | Excluye picking en recepciones |

## Flags que aparecian en mi documentacion previa pero **NO existen en SQL**

Estos los habia inferido del codigo VB pero no estan en `i_nav_config_enc`. Probablemente estan en otra tabla o son properties solo en VB:

- `Considera_Bloqueo_Articulo` → buscar en `i_nav_producto` o tabla de bloqueos
- `Permite_Mezcla_Lotes_En_Pallet`
- `Considera_Cuarentena` → estado del producto, no flag
- `Permite_Stock_Negativo`
- `Bloquea_Pedido_Si_Vencido`
- `Modo_Multi_UM_En_Reserva`
- `Politica_Asignacion_Ubicacion`
- `Reserva_Por_Pedido` vs `Por_Linea`

A confirmar/relocalizar en proximas pasadas.

## Origen aprendido

| Cliente | DB | Bodega ref | Aprendido |
|---|---|---|---|
| KILLIOS | TOMWMS_KILLIOS_PRD | id=1 (BOD1) + 5 mas | 2026-04-27T14:56:31.611Z |
| BYB | IMS4MB_BYB_PRD | id=1 + 1 mas | 2026-04-27T14:56:31.611Z |
| CEALSA | IMS4MB_CEALSA_QAS | id=1 (general) + id=2 (fiscal) | 2026-04-27T14:56:31.611Z |
