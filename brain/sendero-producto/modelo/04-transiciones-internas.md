---
id: 04-transiciones-internas
tipo: sendero-producto
estado: vigente
titulo: Modelo del sendero — Transiciones internas
tags: [sendero-producto]
---

# Modelo del sendero — Transiciones internas

## Concepto Erik

> "Mientras el producto no sale de la bodega, puede sufrir procesos
> de cambio de ubicacion/estado, implosion/explosion y estos se
> registran en la tabla trans_movimientos, vista vw_movimientos_n1."

## Tipos de transicion interna

Una transicion interna es un cambio en `stock_rec` que NO es ni la
recepcion inicial ni el despacho final. Genera siempre una fila en
`trans_movimientos` con `IdBodegaOrigen=IdBodegaDestino` (mismo cliente,
misma bodega, salvo TRAS).

### A. Cambio de ubicacion (put-away)

```
Estado: Buen Estado
   │
   │ producto en RECEPCION
   ▼
trans_movimientos:
   IdTipoTarea = 2 (UBIC)
   IdUbicacionOrigen = RECEPCION
   IdUbicacionDestino = 1A1 (ej: estanteria)
   IdEstadoOrigen = IdEstadoDestino = 3 (Buen Estado)
   cantidad = N
   barra_pallet = LP

stock_rec:
   IdUbicacion_anterior = RECEPCION
   IdUbicacion = 1A1
```

> Caso BECOFARMA mov#178175: PRODUCTO LIBERADO → 1A1 (cant=493, lote 26C13103)

### B. Cambio de estado (sin cambio de ubicacion)

```
Estado: Cuarentena
   │
   │ inspeccion QC aprueba
   ▼
trans_movimientos:
   IdTipoTarea = 2 (UBIC) ← caso especial: cambia ESTADO no UBICACION
   IdUbicacionOrigen = IdUbicacionDestino = CUARENTENA
   IdEstadoOrigen = 8 (Cuarentena)
   IdEstadoDestino = 3 (Buen Estado)

stock_rec:
   IdProductoEstado = 3 (Buen Estado)  ← cambia
   IdUbicacion = CUARENTENA            ← NO cambia
```

> Caso BECOFARMA: cuando la cuarentena se libera. Aunque el producto
> queda en la misma ubicacion fisica, el estado cambia y eso lo libera
> para picking.

### C. Cambio de estado + ubicacion (liberacion completa)

```
Estado: Cuarentena, Ubicacion: CUARENTENA
   │
   │ liberacion + put-away al estante final
   ▼
trans_movimientos:
   IdTipoTarea = 2 (UBIC)
   IdUbicacionOrigen = CUARENTENA
   IdUbicacionDestino = 1A1
   IdEstadoOrigen = 8 (Cuarentena)
   IdEstadoDestino = 3 (Buen Estado)

stock_rec:
   IdProductoEstado = 3
   IdUbicacion = 1A1
```

> Caso BECOFARMA mov#177948: CUARENTENA → PRODUCTO LIBERADO (cambio
> de ubicacion de zona) + mov#178175: PRODUCTO LIBERADO → 1A1 (put-away
> final). En BECOFARMA el flujo de liberacion son DOS movimientos.

### D. Reabasto a picking (REABMAN o REEMP_*_PICK)

```
Estado: Buen Estado
   │
   │ picking se quedo sin stock en ubicacion de picking
   │ Reabasto: traer pallet de almacen → ubicacion picking
   ▼
trans_movimientos:
   IdTipoTarea = 25 (REEMP_BE_PICK)  ← K7 dominante
   o          = 23 (REABMAN)         ← BYB
   IdUbicacionOrigen = 9A2 (estanteria almacen)
   IdUbicacionDestino = 1A1 (ubicacion picking)
   IdEstadoOrigen = IdEstadoDestino = 3
```

> Caso K7 mov#99193 REEMP_BE_PICK: 9A2 → 1A1 (LP=0 → reabasto en bulk
> sin LP especifico).

### E. Explosion (1 pallet → N unidades)

```
Estado: Buen Estado, presentacion = Pallet (factor=240)
   │
   │ explosion: 1 pallet → 12 cajas (presentacion=Caja, factor=20)
   ▼
trans_movimientos:
   IdTipoTarea = 20 (EXPLOSION)
   IdPresentacion (origen → destino) implicito en N filas
   cantidad: 240 unidades total = 12 cajas x 20 unidades

stock_rec:
   se DELETE el registro original (Pallet)
   se INSERT 12 registros nuevos (1 por caja)
   o se actualiza con nueva presentacion
```

> Caso BYB: 57 explosiones historicas. Es el unico cliente que la usa
> activamente. Documentar cuando se dispara: ¿picking pide cantidad < pallet?
> ¿manual desde HH? ¿automatica si flag `explosion_automatica=True`?

### F. Implosion (N unidades → 1 pallet)

> No se vio en trazas, pero `i_nav_config_enc.implosion_automatica=True`
> existe. Es el inverso de explosion. Caso de uso: agrupar en un pallet
> el stock disperso del mismo lote.

### G. Traslado entre bodegas (TRAS — solo MAMPA)

```
trans_movimientos:
   IdTipoTarea = 4 (TRAS)
   IdBodegaOrigen ≠ IdBodegaDestino  ← UNICO caso
   IdUbicacionOrigen = ubicacion en bodega A
   IdUbicacionDestino = ubicacion en bodega B
```

> Caso MAMPA: 90 traslados (entre TIENDA CENTRAL y los 6 puntos de
> servicio). MAMPA es el unico cliente cuyo modelo de operacion implica
> mover stock entre tiendas, los demas clientes manejan una bodega
> central que despacha a clientes externos.

## Estados del producto (cross-cliente)

| Estado tipico | IdEstado en BECOFARMA | Significado |
|---|---|---|
| Buen Estado | 3 | producto utilizable, listo para picking |
| Cuarentena | 8 | producto bloqueado por QC, no pickable |
| Dañado | 2 | producto no utilizable, va a baja |
| Producto Producido | 11 (a verificar) | (caso INELAC mencionado por Erik) recien producido, requiere QC |
| ... | | (cada cliente puede tener mas estados, hasta 24 en BYB) |

## Implicaciones para el sendero

1. **Toda transicion = una fila en `trans_movimientos`**: la trazabilidad
   es completa. La WebAPI debe garantizar que TODA modificacion de
   `stock_rec` genera el movimiento equivalente.
2. **`IdEstadoOrigen ≠ IdEstadoDestino` es semanticamente fuerte**:
   significa una decision (liberacion QC, marcar dañado, etc). La WebAPI
   debe validar transiciones permitidas en una matriz de transicion
   estado-a-estado.
3. **Explosion/Implosion afectan la presentacion**: requieren validacion
   de factor de conversion (si Pallet→Caja, factor debe coincidir con
   `producto_presentaciones_conversiones`).
