# L-055 PROC MAMPA Productos Validacion Roja y Traza UTF8 BOM

Fecha: 2026-06-18

## Contexto

El proceso de productos de MAMPA vive en `clsSyncSAPProducto` y ahora genera una traza fina real por corrida en archivo, no solo mensajes en UI.

## Hallazgo

Cuando un producto llega sin UMBas o con validacion fallida, la corrida no debe seguir como si nada.
El proceso debe:

1. mostrar el error en rojo en la UI
2. registrar el error en la bitacora de ejecucion
3. escribir el evento en la traza fina de archivo
4. cortar ese item con `Continue For` para que no contamine el resto del pipeline

## Aprendizaje

La traza de archivo debe ser facil de editar y compartir, por eso se escribio en `UTF-8 BOM`.
Eso evita el problema clasico de abrirla con encoding incorrecto y verla rota en el editor.

## Archivos de apoyo

- `SAPSYNCMAMPA/Clases Interface Sync/Producto/clsSyncSAPProducto.vb`
- `brain/code-deep-flow/traza-004-sapsyncmampa-productos-performance.md`
- `brain/code-deep-flow/traza-005-sapsyncmampa-sincronizacion-fina.md`

## Regla practica

Si la validacion falla y el proceso sigue sin alertar en rojo, falta feedback de UI.
Si la traza no se puede abrir en editor limpio, falta BOM UTF-8.