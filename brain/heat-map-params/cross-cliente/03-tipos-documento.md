---
id: 03-tipos-documento
tipo: heat-map-params
estado: vigente
titulo: "Cross-cliente: tipos de documento (`trans_oc_ti` + `trans_pe_tipo`)"
tags: [heat-map-params]
---

# Cross-cliente: tipos de documento (`trans_oc_ti` + `trans_pe_tipo`)

Estas dos tablas catalogan los tipos de movimiento que el WMS sabe procesar.
`trans_oc_ti` para INGRESOS (Órdenes de Compra y similares) y `trans_pe_tipo`
para SALIDAS (Pedidos / Egresos).

Cada tipo es una combinación de flags que define el comportamiento al procesarlo.

## `trans_oc_ti` — tipos de ingreso

### Conteos
| Cliente | Cols | Tipos activos |
|---|---:|---:|
| BECOFARMA | 24 | (ver abajo) |
| K7 | 24 | (ver abajo) |
| MAMPA | 24 | 11 |
| BYB | 24 | 11 |
| CEALSA | **22** | 8 |
| Union | 24 | — |

### Schema drift
- **CEALSA NO tiene 2 cols** que el resto sí: `IdProductoEstado`, `IdPropietario`.
  Confirma que CEALSA no maneja estado de producto al recibir (no hay
  cuarentena/calidad) ni propietarios (no es 3PL).

### Tipos activos por cliente

#### MAMPA (11 tipos)
| Id | Nombre | Flags |
|---:|---|---|
| 1 | Ingreso Almacen General | - |
| 2 | Devolución | `es_devolucion` |
| 3 | Transferencia | - |
| 4 | Transferencia WMS | - |
| 8 | Transferencia de Ingreso | - |
| 13 | Factura_Reserva_Proveedor Imp. | `es_importacion` |
| 14 | Ingreso_Inventario_Inicial | `es_devolucion` |
| 15 | Ingreso por nota crédito cancelada | `genera_tarea_ingreso` |
| 16 | Factura_Reserva_Proveedor | - |
| 17 | Ingreso por nota de crédito | `es_devolucion`, `genera_tarea_ingreso` |
| 18 | Ingreso por factura cancelada | `es_devolucion` |

**Interesante MAMPA**: 5 tipos especiales para reversos / notas de crédito /
inventario inicial (14, 15, 16, 17, 18). El catálogo refleja flujo financiero
de tienda con muchos puntos de venta y políticas de devolución / cancelación
formalizadas.

#### BYB (11 tipos)
| Id | Nombre | Flags |
|---:|---|---|
| 1 | Ingreso | - |
| 2 | Devolución | `es_devolucion` |
| 3 | Transferencia | - |
| 4 | Transferencia WMS | - |
| 5 | Poliza/DUCA | `control_poliza` |
| 6 | Orden de Producción | - |
| 7 | Ingreso consolidado | `control_poliza` |
| 8 | Transferencia de Ingreso | - |
| 10 | Ingreso Almacén General con Póliza | `control_poliza` |
| 11 | Liquidación de ruta (Devolución) | - |
| 12 | Devolución venta | - |

**Interesante BYB**: 3 tipos con `control_poliza` (5, 7, 10). Tiene `Orden de
Producción` (6) — recibe producción in-house. Tiene `Liquidación de ruta` —
modelo de venta en ruta con devolución al final del recorrido.

#### CEALSA (8 tipos)
| Id | Nombre | Flags |
|---:|---|---|
| 1 | Ingreso Almacen General | - |
| 2 | Devolución | `es_devolucion` |
| 3 | Transferencia | - |
| 4 | Transferencia WMS | - |
| 5 | Ingreso Almacén Fiscal | `control_poliza` |
| 7 | Ingreso consolidado | `control_poliza` |
| 8 | Transferencia de Ingreso | - |
| 10 | Ingreso Almacén General con Póliza | `control_poliza` |

**CEALSA NO tiene**: Orden de Producción, Liquidación de ruta, Devolución
venta, Importación dedicada, ni los tipos de notas de crédito de MAMPA.
Es el catálogo más reducido (8 tipos) — coherente con su perfil austero.

#### BECOFARMA (capturado parcialmente — el output anterior se cortó)
Vistos: 4 (Recepcion de Transferencia), 6 (Orden de Producción), 8
(Recepcion de Traslado), 9 (Recepción de Compra), 11 (Liquidación de ruta
Rechazo, `es_devolucion`), 12 (Devolución de Venta, `es_devolucion`,
`IdProdEstado=-1`), 13 (Ingreso de importación, `es_importacion`), 14
(Ingreso directo WMS), 15 (Ingreso por nota crédito anulada,
`genera_tarea_ingreso`).

**Interesante BECOFARMA**:
- Tiene `Orden de Producción` (6) → BECOFARMA recibe producción farmacéutica
  in-house? Pendiente Q-BECO-PRODUCCION confirmar.
- Tiene `Devolución de Venta` (12) con `IdProdEstado=-1` → caso especial,
  estado negativo (probable cuarentena automática para devoluciones).
- Numeración salta — tipos 1, 2, 3, 5, 7, 10 no aparecen activos.

#### K7 (no capturado en este barrido)
Pendiente próxima vuelta.

### Comparativa cross-cliente
| Capability | BECO | K7 | MAMPA | BYB | CEALSA |
|---|:-:|:-:|:-:|:-:|:-:|
| Devolución dedicada (es_devolucion) | si | ? | si | si | si |
| Importación dedicada (es_importacion) | si | ? | si | no | no |
| Póliza / DUCA (control_poliza) | ? | ? | no | si (3 tipos) | si (3 tipos) |
| Producción interna (Orden de Producción) | si | ? | no | si | no |
| Notas de crédito como ingreso | si (1) | ? | si (4 tipos) | no | no |
| Liquidación de ruta | si | ? | no | si | no |
| Almacén fiscal con tipo dedicado | no | ? | no | no | si |

## `trans_pe_tipo` — tipos de pedido / salida

### Conteos
| Cliente | Cols |
|---|---:|
| BECOFARMA | 31 |
| K7 | 30 |
| MAMPA | 32 |
| BYB | 30 |
| CEALSA | **24** |
| Union | 33 |

### Schema drift (vs BYB)

#### BECOFARMA: 1 menos, 2 extras
```
NO TIENE: control_cliente_en_detalle  (BYB lower)
EXTRA:    Control_Cliente_En_Detalle  (Camel - duplicado),
          verificar_con_imagen
```

#### MAMPA: 1 menos, 3 extras
```
NO TIENE: control_cliente_en_detalle
EXTRA:    Control_Cliente_En_Detalle (Camel),
          asignar_todos_operadores,
          verificar_con_imagen
```

#### CEALSA: 7 menos, 1 extra
```
NO TIENE: IdProductoEstado, IdPropietario, control_cliente_en_detalle,
          empaque_tarima, escanear_muelle_picking, genera_guia_remision,
          mover_producto_zona_muelle
EXTRA:    Control_Cliente_En_Detalle (Camel)
```

### Lecturas operativas

#### `verificar_con_imagen` — solo BECOFARMA y MAMPA
Capability `verify-with-photo` activa solo en BECOFARMA y MAMPA. La WebAPI
debe permitir adjuntar fotografía a la verificación de salida en estos dos
clientes. K7, BYB, CEALSA no lo soportan.

#### `asignar_todos_operadores` — solo MAMPA
Capability `assign-all-operators` exclusiva MAMPA. Confirma reparto masivo
de tareas (33 puntos de servicio, alto volumen, asignación broad-cast).

#### `Control_Cliente_En_Detalle` (CamelCase) — universal
Aparece en BECOFARMA, MAMPA, CEALSA, **y K7 también** (asumido — falta
confirmar si la versión K7 es Camel también). En BYB existe como
`control_cliente_en_detalle` lower. CASO TÍPICO de case-mismatch que el
WebAPI debe normalizar.

#### CEALSA NO genera guía de remisión desde WMS
La col `genera_guia_remision` no existe en CEALSA. Esto significa que la
guía la genera el ERP (probablemente desde la prefactura), no el WMS.
Capability `generate-remision-from-wms` NO aplica a CEALSA.

#### CEALSA NO maneja zona de muelle ni escanea muelle de picking
No existen `mover_producto_zona_muelle` ni `escanear_muelle_picking`. La
operación CEALSA es directa: del pasillo de picking al despacho.

#### CEALSA NO usa packing por tarima
No existe `empaque_tarima`. Capability `packing-by-pallet` NO aplica a
CEALSA.

#### CEALSA NO maneja `IdProductoEstado` ni `IdPropietario` en pedidos
Coherente con `trans_oc_ti`: la operación CEALSA es 1 estado (Buen Estado
implícito) y 0 propietarios (no es 3PL).

### Tipos de pedido (capturados anteriormente, persisten válidos)
| Cliente | Tipos | Códigos |
|---|---:|---|
| BECOFARMA | 7 | PE0001-5, DEVPROV, TRAS_SAP |
| K7 | 6 | PE0001/3/4, TRAS_WMS, PDV_NAV, DEVPROV |
| MAMPA | 9 | PE0001-6, DEVPROV, TRAS_SAP, FACT_DEUDOR |
| BYB | 6 | PE0001-4, Requisicion, PDV_NAV |
| CEALSA | 5 | PE0001-5 (sin tipos especiales) |

### Comparativa cross-cliente (capabilities de pedido)
| Capability | BECO | K7 | MAMPA | BYB | CEALSA |
|---|:-:|:-:|:-:|:-:|:-:|
| verify-with-photo | si | no | si | no | no |
| assign-all-operators | no | no | si | no | no |
| generate-remision-from-wms | si | si | si | si | **no** |
| dock-zone-management | si | si | si | si | **no** |
| packing-by-pallet | si | si | si | si | **no** |
| product-state-on-order | si | si | si | si | **no** |
| owner-on-order | si | si | si | si | **no** |
| control-cliente-detalle | (Camel) | (?) | (Camel) | (lower) | (Camel) |

CEALSA es el outlier sistemático: 5 capabilities ausentes que los demás 4
sí soportan. Coherente con el patrón P-PREFACTURA-SIN-INTERFACE-STOCK.

## Pendientes Q-* derivadas
- **Q-CASE-CONTROL-CLIENTE**: confirmar variantes case por cliente y armar
  capa de mapeo en WebAPI.
- **Q-CEALSA-AUSENTES-7**: validar con Erik que las 7 cols ausentes en
  trans_pe_tipo CEALSA reflejan funcionalidad real ausente, no schema
  obsoleto que el código todavía espera.
- **Q-BECO-PRODUCCION**: BECOFARMA tiene tipo `Orden de Producción` (6) —
  ¿realmente recibe producción farmacéutica in-house?
- **Q-K7-OC-TIPOS**: capturar tipos de ingreso K7 (faltó en este barrido).
- **Q-BECO-DEVVENTA-IDPRODESTADO-NEG1**: por qué BECOFARMA usa
  `IdProductoEstado=-1` en Devolución de Venta — ¿estado especial cuarentena
  automática?
