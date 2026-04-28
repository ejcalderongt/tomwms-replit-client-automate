# Traza 005 — CEALSA NEN025 (AMOXICILINA 500MG)

**Cliente**: CEALSA (IMS4MB_CEALSA_QAS) | **Bodegas**: 2  
**Producto**: IdProducto=4828 | 1798 movimientos historicos  
**Fecha de la traza**: 29-abr-2026  
**Ambiente QAS — operacion no productiva**

## Q1 — Producto

```
IdProducto=4828 codigo=NEN025 nombre=AMOXICILINA 500MG
control_lote=True control_vencimiento=False control_peso=False  ← farma sin vencimiento (raro!)
genera_lp_old=True IdTipoEtiqueta=2 IdUnidadMedidaBasica=2582
```

> **Anomalia**: producto farma con `control_vencimiento=False`. Real
> farma maneja vencimiento. Confirma que CEALSA es ambiente de prueba
> con datos sinteticos.

## Q2 — producto_bodega (2 bodegas)

```
IdProductoBodega=7609 IdBodega=1 B01 BODEGA GENERAL
IdProductoBodega=7614 IdBodega=2 B02 BODEGA FISCAL
```

## Q3 — producto_presentacion

```
0 presentaciones
```

## Q4 — stock_rec actual

```
pb=7614 estado=NULL ubic=B12T01R00P00 cant=8  lote=707251040 lp=MD001765 ingr=2026-01-19 vence=1900-01-01
pb=7614 estado=NULL ubic=B12T01R00P00 cant=21 lote=707251041 lp=MD001764 ingr=2026-01-19 vence=1900-01-01
pb=7614 estado=NULL ubic=B12T01R00P00 cant=24 lote=707251040 lp=MD001763 ingr=2026-01-19 vence=1900-01-01
```

> **Observaciones brutales**:
> 1. `IdProductoEstado=NULL` — el WMS no le asigna estado al producto.
> 2. TODOS en ubicacion `B12T01R00P00` — siempre la misma ubicacion (
>    a pesar de tener 19503 ubicaciones en el catalogo).
> 3. `vence=1900-01-01` — fecha falsa constante.

## Q5 — trans_movimientos (ultimos 8)

```
mov#1440799 RECE bod=2->2 ubic=B12T01R00P00->B12T01R00P00 est=BE->BE cant=8  lote=707251040 fec=2026-01-19 15:48
mov#1440794 RECE bod=2->2 ubic=B12T01R00P00->B12T01R00P00 est=BE->BE cant=21 lote=707251041 fec=2026-01-19 15:47
(todos RECE, mismo origen=destino)
```

> **Observacion**: en RECE el `IdEstadoOrigen=IdEstadoDestino=Buen Estado`,
> pero en stock_rec queda como NULL. Hay una incoherencia entre como se
> registra el movimiento y como queda el stock.

## Q6 — distribucion tipos de tarea

```
IdTipo=1 RECE n=1798  primero=2022-07-12  ultimo=2026-01-19
```

> **Unico tipo de tarea**: RECE = 100%. Confirma que CEALSA NUNCA
> ejecuta put-away, picking, despacho, ajustes, etc.

## Interpretacion del sendero observado

```
RECEPCION
   │
   --[ 1:RECE | stock_rec | sin estado, ubicacion fija ]-->
   ▼
[??? @ B12T01R00P00]
   │
   FIN (no hay mas transiciones)

PARALELO (sin movimientos):
   trans_pe_enc: 3707 pedidos en estado "Despachado" (sin trans_movimientos asociados)
```

## Mapeo al graph-EQL

Coincide con `grafo-eql/por-cliente/CEALSA.md`. Confirma:
- Sendero TRUNCADO en RECEPCION.
- IdProductoEstado=NULL en stock.
- Pedidos seteados directo en BD.

## Tesis confirmada

CEALSA NO es un cliente operativo. Es un ambiente QAS donde:
- Se carga inventario sintetico via CEALSASync.exe (1798 RECE para este producto).
- Los pedidos se setean directamente en BD como "Despachado" sin pasar
  por el flujo del WMS.
- No hay outbox (`i_nav_transacciones_out` vacia).
- No hay catalogos completos (2 estados, sin presentaciones, vencimiento
  falso).

## Recomendacion

**Excluir CEALSA del set de clientes productivos** para validacion de
la WebAPI. Documentarlo como ambiente de QA del equipo PrograX24.

## Pendientes (Q-CEALSA-OUTBOX-VACIO + Q-CEALSA-CEALSASYNC-ERP)

- Confirmar con Erik si hay alguna operacion productiva escondida.
- Identificar el script que setea los 3707 pedidos como "Despachado".
- Si efectivamente es QAS, documentarlo y excluirlo del set para
  evitar conclusiones erroneas.
