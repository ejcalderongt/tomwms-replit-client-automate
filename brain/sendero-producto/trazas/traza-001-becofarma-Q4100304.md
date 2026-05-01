---
id: traza-001-becofarma-Q4100304
tipo: sendero-producto
estado: vigente
titulo: Traza 001 — BECOFARMA Q4100304 (AMBIARE 2MG-0.25MG CAJA 10 CAP)
clientes: [becofarma]
tags: [sendero-producto, cliente/becofarma]
---

# Traza 001 — BECOFARMA Q4100304 (AMBIARE 2MG-0.25MG CAJA 10 CAP)

**Cliente**: BECOFARMA (IMS4MB_BECOFARMA_PRD) | **Bodega**: 1=GENERAL  
**Producto**: IdProducto=949 | codigo=Q4100304 | 1457 movimientos historicos  
**Fecha de la traza**: 29-abr-2026

## Q1 — Producto (flags)

```
IdProducto=949
codigo=Q4100304
nombre=AMBIARE 2MG-0.25MG CAJA 10 CAP
control_lote=True
control_vencimiento=True
control_peso=False
genera_lp_old=True
IdTipoEtiqueta=8
IdUnidadMedidaBasica=1
activo=True
```

## Q2 — producto_bodega

```
1 bodega:
  IdProductoBodega=949 IdBodega=1 codigo=01 nombre=GENERAL
```

## Q3 — producto_presentacion

```
0 presentaciones (BECOFARMA no usa modulo de presentaciones)
```

## Q4 — stock_rec actual (top 5)

```
recDet=2 pb=949 estado=Buen Estado ubic=PRODUCTO LIBERADO cant=494 lote=26C13103 lp=JP08747 ingr=2026-04-23 vence=2029-03-01
recDet=5 pb=949 estado=Buen Estado ubic=PRODUCTO LIBERADO cant=3   lote=26C13103 lp=JP08617 ingr=2026-04-22 vence=2029-03-01
recDet=3 pb=949 estado=Buen Estado ubic=CUARENTENA        cant=26  lote=26C13103 lp=JP08437 ingr=2026-04-17 vence=2029-03-01
recDet=1 pb=949 estado=Buen Estado ubic=PRODUCTO LIBERADO cant=1495 lote=26C13103 lp=JP08362 ingr=2026-04-16 vence=2029-03-01
recDet=2 pb=949 estado=Buen Estado ubic=PRODUCTO LIBERADO cant=520 lote=26B13102 lp=JP08196 ingr=2026-04-11 vence=2029-02-01
```

> **Observacion clave**: hay producto en CUARENTENA (LP JP08437, cant=26)
> y producto en PRODUCTO LIBERADO (LPs JP08747/JP08617/JP08362/JP08196).
> AMBOS estan en estado "Buen Estado" — la cuarentena se modela por
> UBICACION, no por estado del producto.

## Q5 — trans_movimientos (ultimos 8)

```
mov#178949 PIK  bod=1->1 ubic=3C1->NULL              est=BE->BE cant=20  lote=26C13103 pall=JP08362 fec=2026-04-27 12:28
mov#178947 PIK  bod=1->1 ubic=3C1->NULL              est=BE->BE cant=40  lote=26C13103 pall=JP08362 fec=2026-04-27 12:27
mov#178829 PIK  bod=1->1 ubic=3C1->NULL              est=BE->BE cant=18  lote=26C13103 pall=JP08362 fec=2026-04-27 11:43
mov#178820 PIK  bod=1->1 ubic=3C1->NULL              est=BE->BE cant=45  lote=26C13103 pall=JP08362 fec=2026-04-27 11:41
mov#178487 PIK  bod=1->1 ubic=3C1->NULL              est=BE->BE cant=5   lote=26C13103 pall=JP08362 fec=2026-04-25 10:30
mov#178486 PIK  bod=1->1 ubic=3C1->NULL              est=BE->BE cant=10  lote=26C13103 pall=JP08362 fec=2026-04-25 10:30
mov#178175 UBIC bod=1->1 ubic=PRODUCTO LIBERADO->1A1 est=BE->BE cant=493 lote=26C13103 pall=JP08747 fec=2026-04-24 17:03
mov#177948 UBIC bod=1->1 ubic=CUARENTENA->PROD.LIB.  est=BE->BE cant=493 lote=26C13103 pall=JP08747 fec=2026-04-24 13:59
```

> **Observacion brutal**: el flujo de liberacion de cuarentena es:
>
> 1. mov#177948 UBIC: CUARENTENA → PRODUCTO LIBERADO (493 unidades, LP JP08747)
> 2. mov#178175 UBIC: PRODUCTO LIBERADO → 1A1 (493 unidades, LP JP08747)
>
> Ambos sin cambiar estado (Buen Estado → Buen Estado).
> Despues empieza el picking desde 3C1 (LP JP08362, otro pallet ya en almacen).

## Q6 — distribucion tipos de tarea historicos

```
IdTipo=8  PIK     n=592  primero=2026-01-06  ultimo=2026-04-27
IdTipo=11 VERI    n=332  primero=2026-01-07  ultimo=2026-03-22
IdTipo=5  DESP    n=318  primero=2026-01-07  ultimo=2026-03-22
IdTipo=2  UBIC    n=173  primero=2022-12-20  ultimo=2026-04-24
IdTipo=1  RECE    n=31   primero=2026-01-08  ultimo=2026-04-23
IdTipo=17 AJCANTN n=8    primero=2026-01-19  ultimo=2026-01-19
IdTipo=6  INVE    n=3    primero=2026-01-04  ultimo=2026-01-04
```

> **Observacion**: VERI y DESP no se mueven desde marzo-2026 mientras
> PIK sigue activo en abril-2026. Esto es CONSISTENTE con la hipotesis
> H29 (cuello de botella entre Pickeado y Despachado).

## Interpretacion del sendero observado

```
RECEPCION
   ▼
[Buen Estado @ CUARENTENA] (mov#177948 origen)
   │
   --[ 2:UBIC ]-->
   ▼
[Buen Estado @ PRODUCTO LIBERADO] (mov#177948 destino)
   │
   --[ 2:UBIC ]-->
   ▼
[Buen Estado @ 1A1 / 3C1 / etc] (mov#178175 destino, ya en almacen)
   │
   --[ 8:PIK ]-->
   ▼
[Buen Estado @ NULL] (mov#178820..178949 destino, sale del WMS)
   │
   --[ 11:VERI + 5:DESP ]-->  ← cuello: VERI parado en marzo
   ▼
DESPACHO
```

## Mapeo al graph-EQL

Coincide 100% con `grafo-eql/por-cliente/BECOFARMA.md`. Confirma:
- Liberacion en 2 pasos (CUAR→PROD.LIB. + PROD.LIB.→estanteria).
- Cuarentena modela por ubicacion, NO por estado.
- Cuello de botella en VERI/DESP visible en los datos.

## Implicaciones

- H29 confirmada: 8 PIK del 2026-04-27 con cant total ~138 que NO han
  llegado a VERI ni DESP. Ese stock ya salio del almacen pero no ha
  entrado al pipeline de salida.
- BECOFARMA NO usa `producto_estado` para cuarentena; usa
  `producto_estado_ubic` para mapear el estado "Cuarentena" inicial a
  la ubicacion fisica CUARENTENA.
