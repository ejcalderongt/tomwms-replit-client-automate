---
id: notacion
tipo: sendero-producto
estado: vigente
titulo: Graph-EQL — Notacion (propuesta Erik)
tags: [sendero-producto]
---

# Graph-EQL — Notacion (propuesta Erik)

> "Haz un graph-eql (de Erik jaja), de como es el flujo de un producto
> en base a su configuracion end-to-end, para cada bodega para cada
> set de configuraciones."

## Que es

Graph-EQL es una **notacion textual** para representar el sendero
del producto end-to-end como un grafo dirigido, anotado con:

- **Nodos** = estados del producto (combinacion de estado + ubicacion)
- **Aristas** = transiciones (IdTipoTarea), con la tabla origen del cambio
- **Predicados** = condiciones (flags del producto, config de bodega)
  que disparan o filtran la transicion.

Inspirado en Cypher / EQL / Mermaid, pero adaptado al modelo de TomWMS.

## Sintaxis basica

```
NODO :: <Estado> @ <Ubicacion> [ @ <Bodega> ]
ARISTA :: --[ IdTipoTarea : <Nombre> | <tabla_origen> | <predicado>? ]-->
```

### Ejemplo simple

```
RECEPCION                                    DESPACHO
   ▼                                            ▲
[Buen Estado @ RECEPCION]                       │
   │                                            │
   --[ 2:UBIC | trans_movimientos ]-->          │
   ▼                                            │
[Buen Estado @ 1A1]                             │
   │                                            │
   --[ 8:PIK | stock_rec.IdPickingEnc ]-->      │
   ▼                                            │
[Buen Estado @ 1A1 → estacion picking]          │
   │                                            │
   --[ 11:VERI | trans_movimientos ]-->         │
   ▼                                            │
[Buen Estado @ verificacion]                    │
   │                                            │
   --[ 5:DESP | stock_rec.IdDespachoEnc ]-->----┘
```

### Predicados (decoradores de arista)

Cuando una arista solo se dispara bajo ciertas condiciones:

```
[Cuarentena @ CUARENTENA]
   │
   --[ 2:UBIC | trans_movimientos | when QC_aprueba ]-->
   ▼
[Buen Estado @ CUARENTENA]
```

```
[Buen Estado @ ubicacion_picking]
   │
   --[ 25:REEMP_BE_PICK | trans_movimientos | when stock < umbral ]-->
   ▼
[Buen Estado @ ubicacion_picking ∪ pallet_de_almacen]
```

### Predicados (config dependiente)

```
[Buen Estado @ ALMACEN]
   │
   --[ 20:EXPLOSION | trans_movimientos | when implosion_automatica=False
                                          and pedido.cantidad < pallet ]-->
   ▼
[Buen Estado @ ALMACEN | presentacion=Caja]
```

## Convenciones

- Nodos en `[corchetes]` con `@ ubicacion` y opcional `@ bodega`.
- Aristas `--[ ... ]-->` con 3 secciones separadas por `|`:
  1. `IdTipoTarea : Nombre` — tipo de movimiento
  2. `tabla_origen` — donde se observa el cambio (`stock_rec`, `trans_movimientos`, `trans_pe_enc`, etc)
  3. `when <predicado>?` — opcional, condicion que dispara
- Nodos terminales: `RECEPCION` (entrada al WMS), `DESPACHO` (salida).
- Bifurcaciones: lineas que arrancan del mismo nodo a distintos siguientes.
- Multinodo en stock_rec: `[Estado1 @ Ubic1 ∪ Estado2 @ Ubic2]` cuando
  el mismo lote/LP esta en N ubicaciones (caso BYB que distribuye un
  lote a 8 ubicaciones).

## Cuando hace sentido sub-grafos

Para clientes complejos (BYB con 14+ tipos de tarea), se puede dividir
en sub-grafos:

- **Sub-grafo de ingreso** (RECE → UBIC inicial)
- **Sub-grafo de transicion interna** (UBIC, REEMP_*_PICK, EXPLOSION, etc)
- **Sub-grafo de salida** (PIK → VERI → PACK → DESP)

## Ejemplo completo (BECOFARMA)

```
=== INGRESO ===
RECEPCION
   │
   --[ 1:RECE | stock_rec inicial | tipo_oc=farma ]-->
   ▼
[Cuarentena @ CUARENTENA]   ← producto_estado_ubic(8, 1) define ubicacion

=== TRANSICION INTERNA ===
[Cuarentena @ CUARENTENA]
   │
   --[ 2:UBIC | trans_movimientos | when QC_aprueba ]-->
   ▼
[Buen Estado @ PRODUCTO LIBERADO]
   │
   --[ 2:UBIC | trans_movimientos | put-away final ]-->
   ▼
[Buen Estado @ 1A1]   ← (o 3C1, 9D3, etc — ubicacion fisica de almacen)

=== SALIDA ===
[Buen Estado @ 1A1]
   │
   --[ 8:PIK | stock_rec.IdPickingEnc ]-->
   ▼
[Buen Estado @ estacion picking]
   │
   --[ 11:VERI | trans_movimientos ]-->
   ▼
[Buen Estado @ verificacion]
   │
   --[ 5:DESP | stock_rec.IdDespachoEnc, activo=0 ]-->
   ▼
DESPACHO
```

## Compatibilidad con visualizacion

El graph-EQL es texto plano. Para generar visualizacion automatica se
puede convertir trivialmente a:

- **Mermaid** (`graph LR`)
- **GraphViz** (`digraph G { }`)
- **Cytoscape JSON**
- **D3.js force layout**

Pero la fuente canonica es el texto plano en este folder porque:
- Es diff-friendly (cambios en git son legibles)
- Es busqueda-friendly (grep por nodo o tipo de tarea)
- Se versiona junto con la documentacion conceptual

## Pendientes

- Definir notacion para forks (un mismo movimiento que produce N
  registros en stock_rec, ej explosion).
- Definir notacion para joins (varios stock_rec que se consumen en
  un mismo PIK).
- Ver si vale la pena un parser que valide la notacion.
