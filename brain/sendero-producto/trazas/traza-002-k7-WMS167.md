# Traza 002 — K7 WMS167 (MELOCOTON MIGUELS MITADES 12/820GR 29OZ)

**Cliente**: K7 (TOMWMS_KILLIOS_PRD) | **Bodegas**: 6  
**Producto**: IdProducto=79 | 3657 movimientos historicos  
**Fecha de la traza**: 29-abr-2026

## Q1 — Producto

```
IdProducto=79 codigo=WMS167 nombre=MELOCOTON MIGUELS MITADES 12/820GR 29OZ
control_lote=True control_vencimiento=True control_peso=False
genera_lp_old=True IdTipoEtiqueta=10 IdUnidadMedidaBasica=1
```

## Q2 — producto_bodega (6 bodegas)

```
IdProductoBodega=391  IdBodega=1 BOD1   Bodega Principal
IdProductoBodega=393  IdBodega=2 PRTOK  Bodega de Prorateo Kilio
IdProductoBodega=394  IdBodega=3 PRTO   Bodega de Prorateo Garesa
IdProductoBodega=395  IdBodega=4 BOD5   Bodega Amatitlan
IdProductoBodega=1963 IdBodega=5 PRTK17 Bodega de Prorateo Kilio Z17
IdProductoBodega=1649 IdBodega=6 PRT17  Bodega de Prorateo Garesa Z17
```

## Q3 — producto_presentacion

```
1 presentacion: IdPresentacion=265 nombre=Caja12 factor=12.0
```

## Q4 — stock_rec actual

```
recDet=22 pb=391 estado=Buen Estado ubic=RECEPCION cant=1320 lote=24215 lp=FU01329 ingr=2025-08-07
recDet=22 pb=391 estado=Buen Estado ubic=9D3       cant=1320 lote=24215 lp=FU01329 ingr=2025-08-07
(nota: el mismo recDet aparece en RECEPCION y 9D3 — split entre
ubicaciones del mismo lote/LP)
```

## Q5 — trans_movimientos (ultimos 8)

```
mov#99194 PIK            bod=1->1 ubic=1A1->NULL est=BE->BE cant=60 lote=24215 pall=FU01317 fec=2025-08-19 07:30:17
mov#99194 PIK            bod=1->1 ubic=9B2->NULL est=BE->BE cant=60 lote=24215 pall=FU01317 fec=2025-08-19 07:30:17
mov#99193 REEMP_BE_PICK  bod=1->1 ubic=1A1->1A1 est=BE->BE cant=60 lote=24200 pall=0 fec=2025-08-19 07:30:05
mov#99193 REEMP_BE_PICK  bod=1->1 ubic=9A2->1A1 est=BE->BE cant=60 lote=24200 pall=0 fec=2025-08-19 07:30:05
```

> **Observacion brutal**: 
> 1. mov#99194 PIK es **MULTI-SOURCE**: el mismo IdMovimiento aparece
>    con ubic_origen=1A1 Y ubic_origen=9B2 — el picking tomo de 2
>    ubicaciones distintas para el mismo pedido.
> 2. mov#99193 REEMP_BE_PICK ocurrio 12 segundos antes del PIK: el
>    sistema reabastecio 1A1 desde 9A2 (almacen) justo antes del picking.
>    Es un patron de "reabasto-bajo-demanda" automatico.

## Q6 — distribucion tipos de tarea

```
IdTipo=11 VERI            n=869  primero=2025-06-02  ultimo=2025-08-18
IdTipo=8  PIK             n=863  primero=2025-06-02  ultimo=2025-08-19
IdTipo=5  DESP            n=812  primero=2025-06-03  ultimo=2025-08-18
IdTipo=25 REEMP_BE_PICK   n=606  primero=2025-06-02  ultimo=2025-08-19
IdTipo=2  UBIC            n=173
```

> **Observacion clave**: REEMP_BE_PICK ocurre con frecuencia comparable
> a PIK (606 vs 863 = 70%). Significa que casi siempre que hay un PIK,
> se necesita un reabasto previo. Ratio caracteristico de K7.
>
> VERI/PIK ratio = 869/863 ≈ 1.0 → SANO (cada picking se verifica)  
> DESP/VERI ratio = 812/869 ≈ 0.93 → SANO (casi todo lo verificado se despacha)

## Interpretacion del sendero observado

```
RECEPCION → [BE @ RECEPCION | Caja12]
                 │
                 --[ 2:UBIC put-away ]-->
                 ▼
            [BE @ 9D3/9A2/etc | almacen]
                 │
                 --[ 25:REEMP_BE_PICK | when ubic_picking < umbral ]-->
                 ▼
            [BE @ ubic_picking 1A1/9B2]
                 │
                 --[ 8:PIK multi-source ]-->
                 ▼
            [BE @ estacion picking | IdPickingEnc]
                 │
                 --[ 11:VERI + 5:DESP | flujo sano ]-->
                 ▼
            DESPACHO (a tiendas Kilio/Garesa Z14/Z17)
```

## Mapeo al graph-EQL

Coincide con `grafo-eql/por-cliente/K7.md`. Confirma:
- Reabasto intensivo (REEMP_BE_PICK ≈ 70% del PIK).
- Multi-source picking funcional.
- Flujo de salida sano (no hay backlog).
- Presentacion (Caja12) se mantiene a lo largo del sendero (no hay explosion).

## Implicaciones

- K7 es un BUEN benchmark del flujo retail healthy con presentaciones.
- La logica de REEMP_BE_PICK es candidata a documentarse como
  capability core de la WebAPI: "reabasto automatico bajo umbral".
