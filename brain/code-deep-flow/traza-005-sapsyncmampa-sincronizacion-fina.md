# Traza 005 - Sincronizacion fina MAMPA

> Rama analizada: `dev_2028_merge`
> Proyecto: `C:\Users\carol\source\repos\TOMWMS_BOF\SAPSYNCMAMPA`
> Enfoque: identificar cuellos de botella del proceso de sincronizacion de productos
> Relacionado con: [Traza 004 - Productos MAMPA](C:\Users\carol\Documents\wms-brain\brain\code-deep-flow\traza-004-sapsyncmampa-productos-performance.md)

## 0. Proposito

Este archivo sirve como mapa operativo de la sincronizacion de productos SAP -> TOMWMS para que la corrida deje evidencia suficiente de:

- donde se va el tiempo
- que fase repite trabajo
- que lookup se vuelve N+1
- que parte depende de red, SQL o SAP Service Layer
- que paso deja la corrida esperando sin aportar valor

## 1. Flujo principal

Ruta de entrada:

`frmEjecucion.mnuProductosI` -> `clsSyncSAPProducto.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS`

Flujo fino:

1. Validacion UI y confirmacion.
2. Preflight de productos.
3. Login a SAP Service Layer.
4. Lectura de productos desde SAP.
5. Llenado de tabla intermedia.
6. Lectura de staging local.
7. Preparacion de caches.
8. Procesamiento WMS.
9. Marcado de sincronizacion en SAP.
10. Commit final y resumen.

## 2. Puntos de medicion

La traza fina debe dejar timestamp y duracion en cada uno de estos hitos:

- inicio total
- login SAP
- lectura SAP por pagina
- lectura SAP total
- llenado de intermedia
- lectura de intermedia
- preparacion de caches
- procesar WMS
- checkpoint cada 100 productos
- marcado SAP
- commit final
- total de corrida

## 3. Formato esperado de traza

La salida debe ser facil de filtrar en log visual o bitacora:

```text
PERF-MAMPA-PROD | SAP login | 842 ms
PERF-MAMPA-PROD | SAP pagina leida | 1,340 ms | pagina=3; filas=500; acumulado=1500; skip=1000
PERF-MAMPA-PROD | preparar caches | 214 ms | productos=1650
PERF-MAMPA-PROD | procesamiento WMS checkpoint | 5,820 ms | procesados=100; errores=0; ultimos100=1,204 ms
PERF-MAMPA-PROD | marcar SAP sincronizados | 7,331 ms | pendientes=1650; ok=1650
PERF-MAMPA-PROD | total | 18,904 ms | productos=1650
```

## 4. Cuellos de botella a vigilar

### 4.1 Lectura SAP

Si la fase `SAP pagina leida` se dispara, el problema suele estar en:

- payload demasiado grande
- filtros poco selectivos
- red o latencia del Service Layer
- demasiadas paginas por paginado

Indicadores:

- muchas paginas para pocos productos
- paginas con latencia muy desigual
- respuesta lenta aun con `$select`

### 4.2 Consulta de producto existente

El punto mas sensible que aun puede repetir trabajo es la resolucion de productos existentes en WMS.

Si la traza de `preparar caches` crece demasiado, revisar:

- cuantas llamadas hace `clsLnProducto.Existe`
- si conviene precargar productos por lote
- si el lote trae codigos repetidos o inconsistentes

### 4.3 Escritura en WMS

Si la fase `procesar WMS` domina el tiempo:

- revisar costos de `Insertar` / `Actualizar`
- revisar catálogos nuevos
- revisar si la bodega genera demasiadas inserciones de `producto_bodega`

### 4.4 Marcado SAP

Si `marcar SAP sincronizados` crece mucho:

- la red hacia SAP es el cuello
- el `PATCH` por producto es el punto de mayor costo
- conviene revisar si se puede agrupar o posponer el marcado

### 4.5 UI y bitacora

Si la UI se siente lenta pero las fases internas no cambian mucho:

- el problema puede ser el volumen de mensajes a `RichTextBox`
- el `Application.DoEvents()` puede introducir ruido
- conviene reducir trazas por fila y dejar checkpoints por bloque

## 5. Hipotesis de botella por prioridad

1. `Get_Productos_SAP_SL`
2. `PrepararCachesProducto`
3. `ProcesarProductosDesdeSAP`
4. `Marcar_Productos_Sincronizados_SL`
5. `Confirmar_Y_Llenar_Intermedia`

## 6. Decision rapida de diagnostico

Cuando una corrida salga lenta, leer primero estas tres cifras:

- tiempo total
- tiempo de lectura SAP
- tiempo de marcado SAP

Si el total sube pero esas dos fases no, entonces el problema suele estar en:

- lookups SQL repetidos
- catálogos nuevos
- logging excesivo
- validaciones por fila

## 7. Cambios que ya ayudan

- `$select` explicito en SAP
- pagina de 500
- trazas por pagina y no por fila
- cache de entidades de catalogo
- precarga de `producto_bodega` por lote
- una sola sesion SAP por lote de marcado

## 8. Siguiente mejora probable

Si esta traza confirma que `PrepararCachesProducto` sigue pesado, la siguiente optimizacion natural es:

- reemplazar `clsLnProducto.Existe(codigo, ...)` por una precarga batch de productos del lote
- llenar `ProductosExistentesCache` desde un solo query

## 9. Resultado esperado

Con esta traza deberiamos poder contestar en una sola corrida:

- donde se va el tiempo
- si el cuello es SAP, SQL o UI
- si el lote es demasiado grande
- si queda trabajo repetido por producto
- que fase atacar primero

