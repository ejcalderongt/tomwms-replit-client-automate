# Inventario ciclico: contados, paginas y consolidacion

Fecha: 2026-09-11.
Destino autorizado: `dev_2026_estable` en TOMHH2025 y TOMWMS_BOF.
Estado: implementacion local; pendiente validacion integrada con SQL y HH.

## Alcance

- Se incorporaron selectivamente los cambios de la rama de consolidacion, sin fusionar ramas ni copiar configuraciones.
- El listado de la HH conserva el resumen por ubicacion y ofrece paginas de 200 registros o grupos completos. Se consulta un elemento adicional para determinar si existe pagina siguiente.
- Anterior, Siguiente, Ubicaciones y busqueda por producto/nombre/SKU, licencia o gondola. La busqueda se resuelve en servidor dentro de la ubicacion y el estado Pendientes/Contados seleccionado.
- Contados conserva acceso al detalle para modificar cantidades, incluso cero; muestra nuevamente el peso guardado y usa punto decimal al cargar cantidades editables.
- Al volver de guardar pendientes se consulta la primera pagina para evitar saltar pendientes cuando se reduce el conjunto. Una pagina vacia retrocede a la anterior. Una busqueda sin resultados no se interpreta como ubicacion completada.
- La consolidacion agrupa sin licencia y mantiene ubicacion, gondola, producto, lote, vencimiento, estado, presentacion, UM y operador. No divide un grupo al paginar. El guardado queda limitado a la ubicacion del grupo.
- La HH con control talla/color conserva el recorrido individual y caja master. Los registros talla/color tampoco se consolidan en DAL.
- En modo consolidado se abre directamente la captura del grupo; Ver lista regresa al listado paginado. La gondola congelada identifica el grupo y no se edita desde esta captura.
- Se conserva el contrato anterior de los WebMethods; se agregan endpoints para paginas. La nueva HH requiere el servicio actualizado.

## Entrega y base de datos

Migracion incluida en BOF:
`ControlScriptsDataBase/20260828_inventario_ciclico_consolidado_sin_licencia.sql`.

Agrega el indicador al encabezado y las dos tablas de auditoria. No se ejecuto contra ninguna base de datos. Debe aplicarse por el procedimiento habitual antes de publicar el BOF/servicio actualizado; sus consultas requieren la nueva columna, incluso cuando un inventario no consolida.

Orden para pruebas: migracion en base de pruebas, servicio BOF actualizado, HH actualizada. No se hicieron commits, push ni publicacion. No se cambio versionCode/versionName.

Los archivos locales no versionados `clsBe_inv_ubicacion_resumen.java` y `list_adapt_inv_ubicacion_resumen.java` ya estaban presentes al inicio, son necesarios para compilar y deben incluirse cuando el responsable prepare el commit.

## Validacion

- Compilacion Java de HH: correcta.
- Compilacion WSHHRN y dependencias: correcta.
- Compilacion WMS.vbproj (pantalla BOF incluida): correcta, con advertencias de dependencias/DevExpress del entorno.
- 1,000 casos aleatorios sobre el metodo real Distribuir de DAL: total conservado y sin cantidades negativas.
- Casos teoricos [5,7]: total 0 => [0,0], total 8 => [5,3], total 15 => [8,7]. Rechazo de total negativo verificado.
- Prueba de la llave real por reflexion: cambiar licencia conserva grupo; cambiar cada una de las 14 dimensiones lo separa; talla/color mantiene cada linea individual.
- No se midio rendimiento contra 70,000 registros reales ni se probo el flujo fisico en una HH.
- Auditoria automatica CAG ejecutada sobre ambos repositorios: WARN por verificacion de objetos en SQL no ejecutada. En Java tambien detecta el sufijo `First()` de `findFirst()` como si fuera LINQ de VB; esa advertencia generica no demuestra un acceso inseguro. No se declara PASS integrado. Reportes en `%TEMP%/inv-cic-20260911/`.
- Revision de cambios y formato: `git diff --check` correcto; Java UTF-8 sin BOM, sin nuevos indicios de mojibake, saltos CRLF conservados.

## Prueba integrada requerida antes de liberar

1. Ubicacion con mas de 1,000 registros: recorrer todas las paginas de Contados y localizar por busqueda un registro fuera de la primera.
2. Corregir cantidad positiva y cero; salir, volver a entrar y comprobar cantidad/peso y estado guardados.
3. Pendientes: guardar el ultimo elemento de una pagina y de una ubicacion; comprobar navegacion y ausencia de saltos por desplazamiento.
4. Producto sin talla/color: grupo con varias licencias y licencia 0, faltante, sobrante y cero. Buscar cualquier licencia participante debe devolver el grupo completo.
5. Reabrir grupo contado: guardar nueva version y revisar la distribucion/auditoria; dos HH con la misma version deben producir rechazo de la segunda escritura.
6. Talla/color, caja master y reconteo: comprobar recorrido individual y compatibilidad.
7. Grupos con diferentes lotes, estados, presentaciones, UM o gondolas no deben mezclarse.

La paginacion limita la respuesta y memoria de la HH. Para un grupo consolidado el servidor carga las lineas de la ubicacion, necesarias para formar grupos completos; no es una medicion ni garantia de tiempo de SQL para una ubicacion excepcionalmente grande. La regularizacion/linaje mas amplia descrita en la propuesta del 2026-08-28 no se amplio en este cambio.
