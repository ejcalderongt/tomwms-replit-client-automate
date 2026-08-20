# L-056 PROC MAMPA Instrumentacion Frontera SAP Sincronizacion Productos

Fecha: 2026-06-18

## Contexto

En `clsSyncSAPProducto` la conexion a SAP HANA SL puede quedarse en estado "conectando" o fallar sin un mensaje util para operacion.
El sintoma tipico es que el flujo no avanza a `Sesion SAP lista` ni a la lectura de productos, pero tampoco entrega una causa clara.

## Sintomas detectados

1. `LoginAsync()` se consume desde el sincronizador sin medir tiempo de frontera.
2. La lectura de productos usa `SendAsync(...)` por pagina sin timeout ni sello de tiempo visible.
3. En algunos caminos el fallo de autenticacion solo deja texto generico y el usuario siente que la pantalla se quedo colgada.
4. Si la sesion no queda valida, no conviene seguir con marcado o lectura como si nada.

## Regla de instrumentacion segura

La instrumentacion debe ser minima y no romper nada:

1. marcar inicio de login SAP
2. marcar fin de login SAP con duracion
3. marcar inicio de primera lectura de productos
4. marcar fin de cada pagina con duracion
5. marcar timeout o cancelacion con texto claro
6. no mostrar trazas tecnicas como `PERF-MAMPA-PROD` en UI

## Criterio de operacion

Si la UI se queda sin responder y no aparece una frontera de tiempo, falta instrumentacion de login o de primera pagina.
Si la sesion falla, se debe cortar el camino de marcado y devolver error claro.

## Modelo reutilizable

Este mismo patron se puede aplicar a otras interfaces que usen SAP SL y presenten los mismos sintomas:

- login lento o silencioso
- lectura por pagina sin feedback
- errores que solo aparecen al final
- sesiones invalidas que siguen avanzando

## Archivos de apoyo

- `SAPSYNCMAMPA/Clases/SapServiceLayerClient.vb`
- `SAPSYNCMAMPA/Clases Interface Sync/Producto/clsSyncSAPProducto.vb`

## Regla practica

Si no hay marca de tiempo de frontera, no hay diagnostico confiable.
Si el mensaje visible no ayuda a operar, debe quedarse en la traza fina.
