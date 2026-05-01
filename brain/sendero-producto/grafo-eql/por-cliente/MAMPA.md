---
id: MAMPA
tipo: sendero-producto
estado: vigente
titulo: Graph-EQL — MAMPA
clientes: [mampa]
tags: [sendero-producto, cliente/mampa]
---

# Graph-EQL — MAMPA

**Caracter**: retail multi-tienda de calzado, **31 bodegas** (TIENDA CENTRAL + 6 puntos de
servicio + CEDIS + bodegas especiales), **TRASLADOS** entre bodegas, sin presentacion,
sin lote (control_lote=False), genera_lp_old=True (LP por par).

## Sub-grafo

```
=== INGRESO ===
RECEPCION (trans_oc_enc importacion calzado a TIENDA CENTRAL)
   │
   --[ 1:RECE | stock_rec | bodega 1 = TIENDA CENTRAL ]-->
   ▼
[Buen Estado @ RECEPCION @ bodega 1]   ← LP por par (lic_plate=J100000158)

=== TRANSICION INTERNA EN TIENDA CENTRAL ===
[Buen Estado @ RECEPCION @ 1]
   │
   --[ 2:UBIC | trans_movimientos | put-away ]-->
   ▼
[Buen Estado @ 1B1/2A2/PICKING @ 1]
   │
   --[ 21:UBIC PICKING | trans_movimientos | re-asignar ubicacion picking ]-->
   ▼
[Buen Estado @ ubic_picking_actualizada @ 1]

=== TRASLADO A PUNTOS DE SERVICIO (caracteristico MAMPA) ===
[Buen Estado @ ubic @ bodega 1 (CENTRAL)]
   │
   --[ 4:TRAS | trans_movimientos | IdBodegaOrigen=1, IdBodegaDestino=N ]-->
   ▼
[Buen Estado @ ubic @ bodega N (TECULUTAN/ESCUINTLA/etc)]

=== SALIDA EN TIENDA O EN CENTRAL ===
[Buen Estado @ ubic_picking @ bodega N]
   │
   --[ 8:PIK | stock_rec.IdPickingEnc ]-->
   ▼
[Buen Estado @ estacion picking @ N]
   │
   --[ 11:VERI | trans_movimientos ]-->
   ▼
[Buen Estado @ verif @ N]
   │
   --[ 5:DESP | stock_rec.IdDespachoEnc ]-->
   ▼
DESPACHO (a cliente final desde la tienda/punto de servicio)

=== AJUSTES (operativa de retail) ===
[Buen Estado @ ubic] --[ 13:AJCANTP | recibo manual ]--> [+1]
[Buen Estado @ ubic] --[ 17:AJCANTN | merma ]--> [-1]
```

## Aristas dominantes (datos historicos producto AG00030021 Dama bajo tenis)

| Tipo | n | % |
|---|---|---|
| 11 VERI | 289 | 25% |
| 8 PIK | 236 | 21% |
| 1 RECE | 192 | 17% |
| 2 UBIC | 155 | 14% |
| 5 DESP | 136 | 12% |
| **4 TRAS** | **90** | **8%** ← caracteristico |
| 21 UBIC PICKING | 12 | 1% |
| 13 AJCANTP | 6 | 0.5% |
| 17 AJCANTN | 4 | 0.3% |

## Caracteristicas del sendero MAMPA

- **Multi-bodega activa**: el producto se mueve fisicamente entre 31 bodegas.
  No es un caso de "central + sucursales virtuales" sino que cada bodega
  es operativa con su propio picking/despacho.
- **TRAS = traslado entre bodegas**: 90 movimientos para AG00030021.
  Es el unico cliente con uso significativo de IdTipoTarea=4. Implica
  ciclos: TIENDA CENTRAL → punto de servicio (cuando hay venta planeada),
  o devolucion (punto de servicio → CAMBIOS).
- **UBIC PICKING (tipo 21)**: 12 movs. Es DISTINTO al UBIC normal (tipo 2).
  Probablemente re-asigna una ubicacion de picking sin mover stock fisico
  (cambio logico).
- **Sin lote**: lote vacio (control_lote=False). Solo se trazea por LP.
- **Sin presentacion**: 0 productos con presentacion. Calzado va por par.
- **Sin estados intermedios complejos**: solo Buen Estado en las trazas.
- **Bodegas especiales**: IMPARES (par desemparejado), SEGUNDAS (calidad B),
  TERCERAS (calidad C), DAÑADOS DE ORIGEN, GARANTIAS PROVEEDORES, MUESTRAS
  (varias por persona), DIFERENCIAS, etc. Estas bodegas modelan estados
  como bodegas separadas en lugar de cambios de estado del producto.

## Patron clave: bodega-como-estado

> MAMPA usa **bodegas en lugar de estados** para clasificar producto.
> En vez de cambiar `IdProductoEstado` a "Dañado" o "Calidad B", mueve
> el producto a la bodega correspondiente (DAÑADOS DE ORIGEN, SEGUNDAS).
> Esto es semanticamente equivalente pero estructuralmente distinto.

Implicacion para WebAPI: la abstraccion "estado del producto" debe
tener una variante "estado-por-bodega" que mapea bodegas especificas
a categorias logicas de estado.

## Pendientes

- Documentar el flujo de RETORNO de tienda → CENTRAL (devolucion).
- Identificar si TRAS dispara una OC interna o se hace directo.
- Confirmar diferencia entre UBIC y UBIC PICKING (tipo 21).
- Capturar talla/color: AG00030021 es "Dama bajo tenis urbano" → ¿tienen
  variantes por talla en producto distinto o como atributo?
