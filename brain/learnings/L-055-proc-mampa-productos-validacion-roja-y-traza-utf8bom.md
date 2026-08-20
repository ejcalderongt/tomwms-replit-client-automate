# L-055 PROC MAMPA Productos Validacion Roja y Traza UTF8 BOM

Fecha: 2026-06-18

## Contexto

El proceso de productos de MAMPA vive en `clsSyncSAPProducto` y ahora genera una traza fina real por corrida en archivo, no solo mensajes en UI.

## Hallazgo

Cuando un producto llega sin UMBas o con validacion fallida, la corrida no debe seguir como si nada.
El proceso debe:

1. mostrar el aviso en la UI con un color de alerta menos agresivo que `Firebrick` cuando sea posible
2. registrar el error en la bitacora de ejecucion
3. escribir el evento en la traza fina de archivo
4. seguir con el proceso del lote y solo omitir el item invalido con `Continue For`

## Aprendizaje

La traza de archivo debe ser facil de editar y compartir, por eso se escribio en `UTF-8 BOM`.
Eso evita el problema clasico de abrirla con encoding incorrecto y verla rota en el editor.

Para la UI conviene un mensaje corto y operativo, por ejemplo:

- `	AVISO: AG0001072 - No esta definida la UM del producto en SAP`

El detalle tecnico completo debe quedarse en la traza fina y en la bitacora, no en el texto principal de progreso.
La visualizacion de progreso no debe mostrar los mensajes `PERF-MAMPA-PROD`; esos pertenecen a la traza de archivo, no a la lectura diaria de operacion.
La linea de aviso debe ir tabulada hacia adentro para que se lea como detalle del item, no como cabecera de proceso.

Regla practica de presentacion:

- UI: resumen corto, legible y accionable
- traza/bitacora: detalle completo de validacion, codigo y contexto
- perf: solo archivo, nunca pantalla principal

## Archivos de apoyo

- `SAPSYNCMAMPA/Clases Interface Sync/Producto/clsSyncSAPProducto.vb`
- `brain/code-deep-flow/traza-004-sapsyncmampa-productos-performance.md`
- `brain/code-deep-flow/traza-005-sapsyncmampa-sincronizacion-fina.md`

## Regla practica

Si la validacion falla y el proceso sigue sin alertar en rojo, falta feedback de UI.
Si la traza no se puede abrir en editor limpio, falta BOM UTF-8.
Si el mensaje de error ocupa demasiado, falta separarlo entre resumen UI y detalle tecnico.
