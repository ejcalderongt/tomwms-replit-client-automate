# Graph-EQL — Grafo base (canonico)

> El grafo base representa el sendero MAXIMAL: todas las transiciones
> posibles del modelo. Cada cliente es un sub-grafo (poda) de este.

## Grafo base completo

```
                         ┌────────────────────────┐
                         │      ERP / EXTERIOR    │
                         └───────────┬────────────┘
                                     │
                                     │ trans_oc_enc (OC)
                                     │ + trans_oc_det (lineas)
                                     │ + trans_oc_det_lote (lotes esperados, opcional)
                                     │ + i_nav_ped_compra_det_lote (lotes pre-asignados, opcional)
                                     │ + i_nav_barras_pallet (tarimas pre-recibidas, opcional)
                                     ▼
                            ┌───────────────┐
                            │  RECEPCION    │  ← nodo terminal entrada
                            └───────┬───────┘
                                    │
                                    │ --[ 1:RECE | stock_rec inicial ]-->
                                    │
                                    │ DETERMINACION ESTADO/UBICACION (cascada)
                                    │ a) trans_oc_ti.estado_default
                                    │ b) operario CAMBIA estado en HH (opcional)
                                    │ c) producto_estado_ubic(IdEstado, IdBodega) → ubicacion
                                    │ d) producto_estado(IdEstado).IdUbicacionDefecto
                                    │ e) bodega.ubicacion_recepcion_default
                                    ▼
   ┌─────────────────────────────────────────────────────────────────────┐
   │  ZONA DE INGRESO (estados de entrada)                               │
   │                                                                     │
   │  [Cuarentena @ CUARENTENA]   [Buen Estado @ RECEPCION]    [Dañado @ AREA_DAÑO] │
   │          │                              │                          │
   │          │ 2:UBIC                       │                          │ 17:AJCANTN
   │          │ when QC_aprueba              │ 2:UBIC put-away          │ a baja
   │          ▼                              ▼                          ▼
   │  [Buen Estado @ CUARENTENA]   [Buen Estado @ 1A1...]      [STOCK_BAJA]
   │          │                              │
   │          │ 2:UBIC put-away              │
   │          ▼                              │
   │  [Buen Estado @ PRODUCTO LIBERADO]      │
   │          │                              │
   │          │ 2:UBIC put-away final        │
   │          ▼                              │
   │  [Buen Estado @ 1A1...]  ◄──────────────┘
   │                                                                     │
   └──────────────────┬──────────────────────────────────────────────────┘
                      │
                      ▼
   ┌─────────────────────────────────────────────────────────────────────┐
   │  ZONA DE ALMACEN (estado estable, sufre transiciones internas)      │
   │                                                                     │
   │  [Buen Estado @ ubicacion_almacen]                                  │
   │       │                          │                       │         │
   │       │ 4:TRAS                   │ 25:REEMP_BE_PICK      │ 20:EXPL │
   │       │ a otra bodega            │ a ubicacion_picking   │ pallet→cajas
   │       ▼                          ▼                       ▼         │
   │   [otra bodega]      [Buen Estado @ ubic_picking]  [N x cajas @ ubic] │
   │       │                          │                       │         │
   │       │                          │                       │ 23:REABMAN
   │       │                          ▼                       ▼         │
   │       │              [Buen Estado @ ubic_picking]  [Buen Estado @ otra ubic_pick] │
   │                                                                     │
   │  Eventos cross: 6:INVE (ajuste inventario), 13:AJCANTP (+), 17:AJCANTN (-) │
   │                                                                     │
   └──────────────────┬──────────────────────────────────────────────────┘
                      │
                      │ disparado por:
                      │ trans_pe_enc (pedido) + trans_pi_enc (picking)
                      ▼
   ┌─────────────────────────────────────────────────────────────────────┐
   │  ZONA DE SALIDA (consumo del stock para satisfacer pedido)          │
   │                                                                     │
   │  [Buen Estado @ ubic_picking]                                       │
   │       │                                                             │
   │       │ 8:PIK | stock_rec.IdPickingEnc=X                            │
   │       │      cantidad descuenta de stock_rec                        │
   │       ▼                                                             │
   │  [Buen Estado @ estacion_picking | IdPickingEnc=X]                  │
   │       │                                                             │
   │       │ 12:PACK (opcional, BYB)                                     │
   │       ▼                                                             │
   │  [Buen Estado @ estacion_pack]                                      │
   │       │                                                             │
   │       │ 11:VERI                                                     │
   │       ▼                                                             │
   │  [Buen Estado @ verificacion | OK]                                  │
   │       │                                                             │
   │       │ 5:DESP | stock_rec.IdDespachoEnc=Y, activo=0                │
   │       ▼                                                             │
   │  ┌─────────────────┐                                                │
   │  │   DESPACHO      │  ← nodo terminal salida                        │
   │  └────────┬────────┘                                                │
   │           │ outbox: i_nav_transacciones_out (tipo=SALIDA)           │
   │           ▼                                                         │
   │       ERP / cliente final                                           │
   │                                                                     │
   └─────────────────────────────────────────────────────────────────────┘
```

## Caminos posibles desde RECEPCION hasta DESPACHO

### Camino MIN (recepcion directa a picking, sin cuarentena, sin transformaciones)

```
RECEPCION → [BE @ RECEPCION] → [BE @ ubic_almacen] → [BE @ ubic_picking]
         → [BE @ estacion_picking] → [BE @ verif] → DESPACHO

5 transiciones: RECE → UBIC (put-away) → UBIC (a picking) → PIK → VERI → DESP
(o 4 si se omite VERI)
```

### Camino FARMA (con cuarentena)

```
RECEPCION → [Cuar @ CUARENTENA] → [BE @ CUARENTENA] → [BE @ PRODUCTO LIBERADO]
         → [BE @ ubic_almacen] → [BE @ estacion_picking] → [BE @ verif] → DESPACHO

7 transiciones, pasa por liberacion QC.
```

### Camino DISTRIBUCION (con explosion y reabasto)

```
RECEPCION → [BE @ RECEPCION] → [Pallet @ ubic_almacen] → [Cajas @ ubic_almacen]
         → [Cajas @ ubic_picking] → [Cajas @ estacion_picking] → [Cajas @ pack]
         → [Cajas @ verif] → DESPACHO

8 transiciones, incluye EXPLOSION + REABMAN + PACK.
```

### Camino TRUNCADO (CEALSA)

```
RECEPCION → [? @ B12T01R00P00] → (FIN)

1 transicion. El producto NUNCA sale.
```

## Como anotar el sub-grafo de un cliente

1. Empezar del grafo base (este archivo).
2. **Eliminar nodos y aristas que el cliente NO usa** (segun catalogo
   de tipos de tarea y datos reales).
3. **Anotar predicados especificos** del cliente (que estados, que
   ubicaciones, que flags activan que arista).
4. **Marcar las aristas dominantes** (las que tienen >50% del trafico
   historico).
5. Validar con una traza real de un producto representativo.

Ver `por-cliente/<cliente>.md` para los grafos resultantes.
