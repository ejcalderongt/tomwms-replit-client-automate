---
id: README
tipo: heat-map-params
estado: vigente
titulo: Capa 5 — Parametros por TIPO DE DOCUMENTO
tags: [heat-map-params]
---

# Capa 5 — Parametros por TIPO DE DOCUMENTO

> Tipos de documento gobiernan que pasa al recibir (`trans_oc_ti`) y al
> despachar (`trans_pe_tipo`). Cada tipo tiene flags que SI ALTERAN el
> sendero (no son metadata).

## Tabla `trans_oc_ti` — INGRESOS (24 cols)

### Funcionales
| Col | Funcion |
|---|---|
| `Nombre` | etiqueta del tipo (Ingreso, Devolucion, etc.) |
| `es_devolucion` | Es devolucion (vs ingreso normal) |
| `control_poliza` | Requiere poliza |
| `requerir_documento_ref` | Requiere doc de referencia |
| `es_poliza_consolidada` | Poliza consolidada |
| **`genera_tarea_ingreso`** | CRITICO: si genera tarea de ingreso (empuja stock) o no |
| `requerir_proveedor_es_bodega_wms` | Proveedor debe ser otra bodega WMS (transferencia WMS) |
| `requerir_documento_ref_wms` | Doc de referencia WMS obligatorio |
| `requerir_ubic_rec_ingreso` | Exige ubicacion de recepcion |
| `exigir_campo_referencia` | Exige campo de referencia |
| `marcar_registros_enviados_mi3` | Marca como enviado a MI3 |
| `preguntar_en_backorder` | Pregunta si entra a backorder |
| `bloquear_lotes` | Bloquea lotes para uso |
| `permitir_excedente_lotes` | Permite excedente sobre lote esperado |
| `es_importacion` | Es importacion |
| `permitir_vencido_ingreso` | Permite ingresar producto vencido |
| `IdProductoEstado` | Estado por defecto al recibir con este tipo |

### Tipos por cliente (capturados 29-abr-2026)

#### BYB (5 tipos)
```
1 Ingreso
2 Devolución         es_devolucion=True
3 Transferencia
4 Transferencia WMS
5 Poliza/DUCA        control_poliza=True
```

#### CEALSA (8 tipos!)
```
1  Ingreso Almacen General
2  Devolución                          es_devolucion=True
3  Transferencia
4  Transferencia WMS
5  Ingreso Almacén Fiscal              control_poliza=True
7  Ingreso consolidado                 control_poliza=True
8  Transferencia de Ingreso
10 Ingreso Almacén General con Póliza  control_poliza=True
```

#### BECOFARMA, K7, MAMPA — PENDIENTE capturar

## Tabla `trans_pe_tipo` — SALIDAS (30 cols en BYB, 24 en CEALSA — drift!)

### Funcionales
| Col | Funcion |
|---|---|
| `Nombre`, `Descripcion` | etiqueta |
| `Preparar` | Requiere preparacion |
| `Verificar` | Requiere verificacion |
| `ReservaStock` | Reserva stock al crear pedido |
| `ImprimeBarrasPicking` | Imprime barras al pickear |
| `ImprimeBarrasPacking` | Imprime barras al empacar |
| `control_poliza` | Requiere poliza |
| `requerir_documento_ref` | Requiere doc ref |
| **`Generar_pedido_ingreso_bodega_destino`** | CRITICO: genera pedido de ingreso en bodega destino (transferencia WMS<->WMS) |
| `IdTipoIngresoOC` | Tipo doc ingreso a generar en destino |
| `trasladar_lotes_doc_ingreso` | Lleva los lotes al doc de ingreso |
| **`requerir_cliente_es_bodega_wms`** | Cliente debe ser otra bodega WMS |
| `marcar_registros_enviados_mi3` | Marca enviado MI3 |
| `generar_recepcion_auto_bodega_destino` | Genera recepcion automatica en destino |
| `recibir_producto_auto_bodega_destino` | Recibe producto automaticamente en destino |
| `control_cliente_en_detalle` | Control cliente en linea |
| `permitir_despacho_parcial` | Permite despacho parcial |
| `permitir_despacho_multiple` | Permite multiples despachos |
| `fotografia_verificacion` | Foto al verificar |
| `es_devolucion` | Es pedido de devolucion |
| **`empaque_tarima`** | Requiere packing en tarima |
| `IdProductoEstado` | Estado destino al despacho |
| `mover_producto_zona_muelle` | Mueve a zona muelle |
| `escanear_muelle_picking` | Escanea muelle al pickear |
| `transferir_ubicacion` | Transferir ubicacion |
| `genera_guia_remision` | Genera guia de remision |

### Tipos por cliente (capturados 29-abr-2026)

#### BECOFARMA (7 tipos)
```
1  PE0001  Transferencia Fiscal a General | Verificar ReservaStock requerir_cliente_es_bodega_wms Generar_pedido_ingreso_bodega_destino
2  PE0002  PEDIDO SIN PREPARACIÓN Y SIN VERIFICACIÓN | ReservaStock
3  PE0003  Pedido de cliente | ReservaStock
4  PE0004  PEDIDO SIN RESERVA INMEDIATA, PREPARACIÓN... | Verificar
5  PE0005  Pedido de cliente | Verificar ReservaStock
12 DEVPROV Devolución a proveedor | ReservaStock
13 TRAS_SAP Traslado_Por_Estados_SAP | requerir_cliente_es_bodega_wms
```

#### K7 (6 tipos)
```
1  PE0001    Pedido de Cliente | ReservaStock
3  PE0003    Pedido de cliente | Verificar ReservaStock
4  PE0004    Solicitud de traslado | Generar_pedido_ingreso_bodega_destino
6  TRAS_WMS  Traslado_Directo_desde_WMS | requerir_cliente_es_bodega_wms Generar_pedido_ingreso_bodega_destino
9  PDV_NAV   Pedido de Venta SAP | ReservaStock
12 DEVPROV   Solicitud de devolución a proveedor | ReservaStock es_devolucion
```

#### MAMPA (9 tipos!)
```
1  PE0001       Transferencia Fiscal a General | ReservaStock Generar_pedido_ingreso_bodega_destino
2  PE0002       Factura TMK/Tiendas virtuales | ReservaStock
3  PE0003       Pedido de cliente | Verificar ReservaStock
4  PE0004       Transferencia a puntos de servicio
5  PE0005       Pedido de cliente | ReservaStock
6  PE0006       Transferencia directa Prorrateo | Verificar requerir_cliente_es_bodega_wms Generar_pedido_ingreso_bodega_destino
12 DEVPROV      Solicitud de devolución a proveedor | ReservaStock es_devolucion
13 TRAS_SAP     Traslado_Por_Estados_SAP | requerir_cliente_es_bodega_wms
14 FACT_DEUDOR  Factura_Deudor | requerir_cliente_es_bodega_wms
```

#### BYB (6 tipos)
```
1  PE0001        Pedido_De_Bodega | Verificar ReservaStock
2  PE0002        Pedido_Consolidado | ReservaStock
3  PE0003        Pedido_De_Cliente | ReservaStock
4  PE0004        Transferencia_Interna_WMS
7  Requisicion   Despacho Requisicion de Materiales | ReservaStock
9  PDV_NAV       Pedido de Venta NAV | ReservaStock
```

#### CEALSA (5 tipos, schema mas viejo)
```
1 PE0001 Transferencia Fiscal a General           | Verificar ReservaStock
2 PE0002 PEDIDO SIN PREPARACIÓN Y SIN VERIFICACIÓ | ReservaStock
3 PE0003 Pedido de cliente                        | Verificar ReservaStock
4 PE0004 PEDIDO SIN RESERVA INMEDIATA, PREPARACIÓ | Verificar
5 PE0005 Pedido de cliente                        | Verificar ReservaStock

(no tiene los tipos especiales DEVPROV, TRAS_SAP, etc — la prefactura
no requiere tipos especiales para devolucion/traslado)
```

## Patrones cross-cliente

### Patron de transferencia WMS ↔ WMS
- Tipo "TRAS_WMS" en K7
- Tipo "PE0001 Transferencia Fiscal a General" + "PE0006 Transferencia directa Prorrateo" en MAMPA
- Tipo "Transferencia_Interna_WMS" (PE0004) en BYB
- Tipo "Transferencia Fiscal a General" (PE0001) en BECOFARMA + "TRAS_SAP" (PE13)
- Discriminador: flag `requerir_cliente_es_bodega_wms` + `Generar_pedido_ingreso_bodega_destino`

### Patron de devolucion a proveedor
- Tipo "DEVPROV" en BECOFARMA, K7, MAMPA
- Discriminador: `es_devolucion=True` + cliente es proveedor

### Patron de pedido CON verificacion vs SIN verificacion
- BECOFARMA tiene PE0002 (sin verificacion) y PE0004 (sin reserva, con preparacion)
- K7 tiene PE0001 (sin verificacion) y PE0003 (con verificacion)
- BYB tiene PE0001 (con verificacion) y PE0002 Consolidado (sin verificacion)
- Discriminador: flag `Verificar`

### Patron de pedido consolidado
- "Pedido_Consolidado" PE0002 BYB
- "PE0002 Factura TMK/Tiendas virtuales" MAMPA
- Discriminador: descripcion contiene "Consolidad*" + flags especificos

### Patron de salida ERP
- "PDV_NAV" K7 y BYB → Pedido de Venta NAV/SAP (viene del ERP)
- "Pedido de Venta SAP" K7
- Discriminador: nombre PDV_*

## Schema drift cross-cliente (detectado 29-abr-2026)

```
trans_pe_tipo cols en BYB pero NO en CEALSA:
  IdProductoEstado, IdPropietario, control_cliente_en_detalle,
  empaque_tarima, escanear_muelle_picking, genera_guia_remision,
  mover_producto_zona_muelle

trans_pe_tipo cols en CEALSA pero NO en BYB:
  Control_Cliente_En_Detalle (mayuscula distinta — duplicado!)
```

## Implicaciones para WebAPI

- Los `trans_oc_ti` y `trans_pe_tipo` son las **maquinas de estado por
  documento**. La WebAPI debe cargarlos al inicio de la sesion y
  routear cada operacion al handler correcto segun los flags.
- Schema drift requiere mapeo defensivo: si la col no existe (CEALSA),
  asumir valor default (False).
- Tipos compartidos cross-cliente (DEVPROV, TRAS_SAP, PDV_NAV) sugieren
  que hay un set ESTANDAR de tipos que casi todos los clientes
  implementan, mas tipos especificos por cliente.

## Pendientes

- Capturar `trans_oc_ti` para BECOFARMA, K7, MAMPA.
- Documentar el ESTANDAR de tipos cross-cliente vs los tipos especificos.
- Mapear cada tipo a su sub-grafo de sendero correspondiente.
