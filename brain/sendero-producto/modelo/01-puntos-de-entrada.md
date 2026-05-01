---
id: 01-puntos-de-entrada
tipo: sendero-producto
estado: vigente
titulo: Modelo del sendero — Punto de entrada
tags: [sendero-producto]
---

# Modelo del sendero — Punto de entrada

## Concepto Erik (textual)

> "El sendero del producto esta definido en la transaccion de ingreso
> por: a) el estado en el que se recibe en la HH si se cambia, b) el
> estado puede estar asociado a una ubicacion diferente o no de la
> ubicacion por defecto de recepcion de la bodega, c) el producto puede
> ir a la ubicacion por defecto configurada en la recepcion, d) el
> producto puede ir a la ubicacion por defecto asociada al estado de la
> recepcion. Por ejemplo en INELAC se recibe producto de produccion,
> por defecto el sistema en base al tipo de la trans_oc_ti puede
> definir el estado por defecto para un ingreso."

## Sub-flujos de entrada

### A. Por documento de ingreso desde ERP (caso comun)

```
ERP
 │ envia documento (factura/OC/transferencia)
 │ via SAPBOSync<Cliente>.exe / NavSync.exe / <Cliente>Sync.exe
 │
 ▼
trans_oc_enc (orden de compra encabezado)
 │ IdTipoIngresoOC → tipo de la "OC"
 │ IdEstadoOC      → estado actual del documento (pendiente, en recepcion, etc)
 │ IdBodega        → bodega destino
 │
 ├── trans_oc_det (detalle por linea)
 │     IdProductoBodega
 │     IdPresentacion
 │     cantidad esperada
 │
 ├── trans_oc_ti (tipo de ingreso — FK a clasificacion del tipo)
 │     define el comportamiento DEFAULT del estado del producto al recibir
 │
 └── (opcional) i_nav_ped_compra_det_lote
       lotes pre-asignados que el ERP envia para esta OC
       (ej: ya viene con numero de lote y vencimiento desde ERP)
```

### B. Determinacion del estado y ubicacion default

El sistema decide donde poner el producto por la cadena:

```
1. Tipo de OC          (trans_oc_ti.<config>)
        │
        ▼
2. Estado por defecto  (producto_estado.IdEstado)
        │ por ejemplo: "Buen Estado" (id=3), "Cuarentena" (id=8),
        │             "Producto Producido" (id=11), "Dañado" (id=2)
        ▼
3. Ubicacion default   (resolucion en cascada)
        │
        ├── 3a. Si producto_estado_ubic(IdEstado=X, IdBodega=B) existe
        │       → usa esa ubicacion (especifica por bodega)
        │
        ├── 3b. Si no, usa producto_estado(IdEstado=X).IdUbicacionDefecto
        │       (ubicacion default global del estado)
        │
        └── 3c. Si no, usa la ubicacion default de RECEPCION de la bodega
                (bodega.IdUbicacionRecepcionDefecto si existe, o la primera
                bodega_ubicacion con flag ubicacion_recepcion=1)
```

> **Concepto fundamental**: el estado del producto NO es solo metadata,
> es un **driver de routing** que define donde fisicamente termina el
> producto al ingresar.

### C. Caso "lotes pre-asignados" (Erik mencion explicita)

```
ERP envia: "voy a despacharte estos lotes con estos numeros y vencimientos"
        │
        ▼
i_nav_ped_compra_det_lote
i_nav_ped_traslado_det_lote
        │ pre-carga lotes esperados antes de la recepcion fisica
        ▼
HH al recibir, ya sabe que lote esperar
        │
        ▼
trans_re_det / trans_oc_det_lote
        confirma o rechaza el lote pre-asignado
```

> **No todos los clientes usan esto**. Por ejemplo BECOFARMA confia
> en el ERP para el lote, mientras que BYB recibia desde NAV con
> estos pre-asignados antes del corte 2024.

### D. Recepcion fisica en HH

```
HH abre el documento (trans_oc_enc)
        │
        ▼
Operario escanea producto / cantidad / lote / vencimiento / pallet
        │ (cuales campos pide depende de los flags del producto:
        │  control_lote, control_vencimiento, genera_lp_old, control_peso)
        ▼
Operario opcionalmente CAMBIA el estado (de "Buen Estado" a "Dañado", etc)
        │
        ▼
trans_re_enc + trans_re_det (recepcion encabezado y detalle)
        │
        ├── trans_re_oc (link recepcion ↔ orden de compra)
        │
        ├── i_nav_barras_pallet (si genera_lp_old=True)
        │     IdPallet, Camas_Por_Tarima, Cajas_Por_Cama, Bodega_Origen, Bodega_Destino
        │
        └── stock_rec (UN REGISTRO POR ENTRADA UNICA AL STOCK)
              IdRecepcionDet, IdProductoBodega, IdProductoEstado,
              IdUbicacion (DESTINO RESUELTO), cantidad, peso, lote, lic_plate, ...
```

### E. Resultado: primer registro en stock_rec

`stock_rec` es la tabla magistral del sendero. Cada entrada al stock
genera un registro con:

- IdProductoEstado (estado resuelto en recepcion)
- IdUbicacion (ubicacion resuelta segun cascada B)
- cantidad, peso (si control_peso=True)
- lote, lic_plate, fecha_ingreso, fecha_vence
- Links: IdRecepcionEnc, IdRecepcionDet
- `IdUbicacion_anterior=NULL` en el primer registro (no hay anterior)

A partir de aca empieza el **sendero interno** (transiciones).

## Variantes cross-cliente del punto de entrada

| Cliente | Tipo OC dominante | Estado default | Ubicacion default | Lotes pre-asignados |
|---|---|---|---|---|
| BECOFARMA | importacion farma | Buen Estado → CUARENTENA | producto_estado_ubic(8, 1) → CUARENTENA | NO usa |
| K7 | compra alimentos | Buen Estado | RECEPCION (bodega 1) | NO usa |
| MAMPA | importacion calzado | Buen Estado | RECEPCION (bodega 1=TIENDA CENTRAL) | NO usa |
| BYB | compra desde NAV | Buen Estado | RECEPCION | SI usaba (corte 2024) |
| CEALSA | recepcion sintetica | (NULL) | B12T01R00P00 (unica) | NO usa |

> **Observacion clave**: BECOFARMA es el unico que tiene un mapeo
> explicito `producto_estado_ubic` que rutea producto que llega a
> "Cuarentena" hacia la ubicacion fisica CUARENTENA. Esto es lo que
> Erik quiso ilustrar con el ejemplo de INELAC. Los demas clientes
> usan la ubicacion default de recepcion sin diferenciacion por estado.

## Pendientes

- Confirmar con Erik el mecanismo exacto de `IdTipoIngresoOC` → estado
  por defecto. ¿Es por trigger, por SP, por logica del HH?
- Capturar `producto_estado_ubic` completo de cada cliente.
- Documentar caso INELAC (recepcion de produccion, no esta en EC2).
