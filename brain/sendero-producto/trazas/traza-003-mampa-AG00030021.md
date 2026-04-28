# Traza 003 — MAMPA AG00030021 (Dama bajo tenis urbano)

**Cliente**: MAMPA (TOMWMS_MAMPA_QA) | **Bodegas**: 31  
**Producto**: IdProducto=31345 | 1120 movimientos historicos  
**Fecha de la traza**: 29-abr-2026

## Q1 — Producto

```
IdProducto=31345 codigo=AG00030021 nombre=Dama bajo tenis urbano
control_lote=False control_vencimiento=False control_peso=False
genera_lp_old=True
```

> **Caracteristica**: lote=False (calzado no tiene lote) pero genera_lp_old=True
> (cada par lleva su LP).

## Q2 — producto_bodega (31 bodegas — extracto)

```
IdProductoBodega=376687  IdBodega=1  TIENDA CENTRAL
IdProductoBodega=376688  IdBodega=2  PUNTO DE SERVICIO TECULUTAN
IdProductoBodega=376689  IdBodega=3  PUNTO DE SERVICIO ESCUINTLA
IdProductoBodega=376690  IdBodega=4  PUNTO DE SERVICIO BARBERENA
IdProductoBodega=376691  IdBodega=5  PUNTO DE SERVICIO XELA
IdProductoBodega=376692  IdBodega=6  PUNTO DE SERVICIO SAN MARCOS
IdProductoBodega=376693  IdBodega=7  PUNTO DE SERVICIO COBAN
IdProductoBodega=376694  IdBodega=12 CAMBIOS
IdProductoBodega=376695  IdBodega=21 CEDIS SAN JUAN
IdProductoBodega=376696  IdBodega=30 BODEGA PRORRATEO
IdProductoBodega=376697  IdBodega=31 BODEGA TMK
IdProductoBodega=410160  IdBodega=13 BODEGA DE IMPARES
IdProductoBodega=441523  IdBodega=14 BODEGA DE SEGUNDAS
IdProductoBodega=472886  IdBodega=15 BODEGA DE TERCERAS
IdProductoBodega=504249  IdBodega=17 MUESTRAS MAMPA
IdProductoBodega=535612  IdBodega=18 MUESTRAS DISEÑO
IdProductoBodega=566975  IdBodega=19 GARANTIAS PROVEEDORES
... (y 14+ bodegas mas: DIFERENCIAS, DAÑADOS DE ORIGEN, MUESTRAS x persona)
```

> **Patron MAMPA**: las bodegas modelan estados/contextos en lugar de
> usar `producto_estado`. Ej: producto dañado → bodega 25 (DAÑADOS DE
> ORIGEN CEDIS) en vez de cambiar estado.

## Q3 — producto_presentacion

```
0 presentaciones (calzado va por par, sin presentacion)
```

## Q4 — stock_rec actual (top 5)

```
pb=376687 estado=Buen Estado ubic=RECEPCION cant=1 lote='' lp=J100000158 ingr=2026-03-04
pb=376687 estado=Buen Estado ubic=PICKING   cant=1 lote='' lp=J100000158 ingr=2026-03-04
pb=376687 estado=Buen Estado ubic=1B1       cant=1 lote='' lp=J100000158 ingr=2026-03-04
pb=376687 estado=Buen Estado ubic=2A2       cant=1 lote='' lp=J100000158 ingr=2026-03-04
```

> **Observacion**: cant=1 por LP (un par de tenis). lote vacio. Misma
> LP J100000158 aparece en 4 ubicaciones distintas (RECEPCION, PICKING,
> 1B1, 2A2). Esto sugiere que el producto SE MUEVE constantemente entre
> ubicaciones de la misma bodega.

## Q5 — trans_movimientos (ultimos 8)

```
mov#2964 UBIC PICKING  ubic=PICKING->PICKING est=BE->BE cant=1 lote= pall=J100000165 fec=2026-04-09
mov#2963 UBIC PICKING  ubic=PICKING->PICKING est=BE->BE cant=1 lote= pall=J100000165 fec=2026-04-09
```

> **Observacion**: tipo 21 (UBIC PICKING) con ubic_origen = ubic_destino
> = "PICKING". No es un movimiento fisico — es un re-asignacion logica
> de la ubicacion de picking. Probablemente cambia el atributo
> "ubicacion preferida para pickear este LP" sin mover stock.

## Q6 — distribucion tipos de tarea

```
IdTipo=11 VERI         n=289
IdTipo=8  PIK          n=236
IdTipo=1  RECE         n=192
IdTipo=2  UBIC         n=155
IdTipo=5  DESP         n=136
IdTipo=4  TRAS         n=90    ← caracteristico (traslados entre bodegas)
IdTipo=21 UBIC PICKING n=12
IdTipo=13 AJCANTP      n=6
IdTipo=17 AJCANTN      n=4
```

## Interpretacion del sendero observado

```
RECEPCION (TIENDA CENTRAL bodega 1)
   │
   --[ 1:RECE ]-->
   ▼
[BE @ RECEPCION @ 1]
   │
   --[ 2:UBIC put-away ]-->
   ▼
[BE @ 1B1/2A2/PICKING @ 1]
   │
   ├─[ 4:TRAS | a otra bodega N ]──────► [BE @ ubic @ N]  ← reabasto inter-tienda
   │                                          │
   │                                          --[ 8:PIK + 11:VERI + 5:DESP ]→ DESPACHO
   │
   └─[ 21:UBIC PICKING | re-asignar ]────► [BE @ ubic_picking_actualizada]
                                              │
                                              --[ 8:PIK + 11:VERI + 5:DESP ]→ DESPACHO
```

## Mapeo al graph-EQL

Coincide con `grafo-eql/por-cliente/MAMPA.md`. Confirma:
- TRAS como mecanismo principal de reabasto inter-tienda.
- UBIC PICKING (21) como re-asignacion logica.
- Cant=1 por LP (un par).
- Bodegas-como-estados (CAMBIOS, IMPARES, SEGUNDAS, TERCERAS).

## Pendientes

- Trazar un caso de TRAS completo (desde TIENDA CENTRAL hasta venta en
  PUNTO DE SERVICIO XELA).
- Capturar talla/color del producto (¿como se modela? ¿productos
  distintos por talla?).
