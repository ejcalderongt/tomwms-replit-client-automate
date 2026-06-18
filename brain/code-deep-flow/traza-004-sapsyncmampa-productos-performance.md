# Traza 004 - Productos MAMPA (`SAPSYNCMAMPA`)

> Rama analizada: `dev_2028_merge`
> Proyecto: `C:\Users\carol\source\repos\TOMWMS_BOF\SAPSYNCMAMPA`
> Fecha de esta traza: 2026-06-18
> Estado: traza fina de rendimiento para importacion/sincronizacion de productos

## 0. Resumen ejecutivo

El flujo de productos de MAMPA se concentra en `clsSyncSAPProducto`. La traza actual deja visibles las fases calientes y los puntos donde aun podia aparecer comportamiento N+1.

Ruta principal:

`frmEjecucion.mnuProductosI` -> `clsSyncSAPProducto.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS` -> `Confirmar_Y_Llenar_Intermedia` -> `Importar_Productos_Desde_SAP_A_TablaIntermediaAsync` -> `Get_Productos_SAP_SL` -> `ProcesarProductosDesdeSAP`

## 1. Objetivo operativo

Medir la importacion de productos con trazas finas por fase para poder comparar corridas y detectar:

- costo real de lectura SAP
- costo de llenado de staging
- costo de caches y lookups de catalogos
- costo de escritura en WMS
- costo de marcado de sincronizacion en SAP

## 2. Trazas agregadas

En `SAPSYNCMAMPA/Clases Interface Sync/Producto/clsSyncSAPProducto.vb` se agrego el prefijo:

`PERF-MAMPA-PROD`

Las fases que quedan trazadas son:

- `inicio`
- `SAP login`
- `SAP lectura productos`
- `SAP pagina leida`
- `SAP lectura total`
- `llenar intermedia`
- `leer intermedia`
- `preparar caches`
- `procesamiento WMS inicio`
- `procesamiento WMS checkpoint`
- `procesar WMS + marcar SAP`
- `marcar SAP sincronizados`
- `commit WMS`
- `total`

## 3. Mejoras ya aplicadas

### 3.1 Lectura SAP

- `Get_Productos_SAP_SL` usa `$select` explicito.
- La pagina paso de 100 a 500.
- Se elimino el log por cada producto encontrado.
- La traza ahora se emite por pagina y por corrida completa.

### 3.2 Caches de lote

- `ProductosExistentesCache`
- `ClasificacionCache`
- `TipoProductoCache`
- `MarcaCache`
- `FamiliaCache`
- `UnidadMedidaCache`
- `ProductoBodegaCache`

### 3.3 Producto-bodega

Se elimino el N+1 de validacion por producto existente para `producto_bodega`:

- antes: una consulta por producto
- ahora: precarga por lote en chunks

## 4. N+1 que seguian visibles

### 4.1 Lookup de producto existente

`PrepararCachesProducto` sigue llamando `clsLnProducto.Existe(codigo, lConnection, lTransaction)` por cada codigo del lote.

Riesgo:

- si el lote es grande, esa es la siguiente fuente de costo repetitivo
- la traza dira si esto domina el tiempo total

Siguiente mejora probable:

- reemplazar el lookup por una consulta batch de `producto` por lista de codigos
- poblar `ProductosExistentesCache` desde un solo resultset

### 4.2 Marcado en SAP

`Marcar_Productos_Sincronizados_SL` sigue haciendo un `PATCH` por item.

Esto ya no es N+1 de login porque usa una sola sesion, pero sigue siendo:

- una llamada HTTP por producto
- sensible a latencia de red

La mejora solo vale la pena si Service Layer soporta batch estable para este caso.

### 4.3 Insercion de catalogos nuevos

Las altas de:

- clasificacion
- tipo
- marca
- familia
- unidad de medida

todavia pueden hacer consultas individuales al no existir cache.
La traza actual sirve para ver si el volumen real de nuevos catalogos es bajo o si conviene precargar esos diccionarios desde SQL.

## 5. Puntos de control para la siguiente corrida

- comparar tiempo de lectura SAP por pagina
- comparar tiempo total de staging vs procesamiento WMS
- revisar si el costo principal sigue en `clsLnProducto.Existe`
- revisar cuantos productos se marcan en SAP y cuanto cuesta el `PATCH`

## 6. Riesgos

- introducir más traza por producto puede distorsionar el tiempo medido
- convertir a batch lookups sin respetar la logica de catalogos puede cambiar resultados
- tocar el marcado SAP sin validar contrato puede dejar productos sin sincronizar

## 7. Conclusión

La traza de productos ya quedo lista para medir corridas reales.
El siguiente N+1 mas claro a atacar, si la traza lo confirma, es `clsLnProducto.Existe` dentro de `PrepararCachesProducto`.

