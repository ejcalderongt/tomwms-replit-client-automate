# Monitor de ocupación y ubicaciones bloqueadas

Tag: #EJC20260911

Aplicado al checkout local TOMWMS_BOF, rama dev_2026_estable. Sin commit ni publicación.

## Cambios y traza

- `frmPrincipal02.chkOcupacionPorArea_CheckedChanged`: antes siempre ejecutaba el gráfico por área. Ahora selecciona área o recarga general mediante bgwTareas. Si hay una carga en curso, su finalización respeta el estado actual del interruptor.
- `bgwTareas_RunWorkerCompleted -> Pintar_Indicador_Ocupacion`: al volver a general, elimina DataSource, SeriesDataMember y el manejador de selección por área; restablece eje X y series manuales. El refresco automático mantiene la selección por área.
- `frmrptUbicacionesVacias -> Cargar_Datos_Async -> clsLnBodega_ubicacion.Get_Ubicaciones`: agrega modo excluyente Bloqueadas y parámetro opcional SoloBloqueadas. Desactivarlo vuelve al modo Vacías, igual que los otros selectores.
- La consulta conserva VW_OcupacionBodega y la relaciona con bodega_ubicacion por IdBodega + IdUbicacion. Filtra bloqueada=1 sin filtrar ocupación, y muestra Sí/No junto a Estado. NULL se representa como No. No modifica tablas, vistas ni datos.
- La columna corresponde al listado resumido (Vacías, Ocupadas, Todas, Bloqueadas). Detallado conserva su listado por producto, que no tiene columna Estado.
- Los diseños guardados se restauran antes de colocar Bloqueada junto a Estado. Impresión y Excel consumen el mismo grid.

## Archivos

1. TOMIMSV4/TOMIMSV4/Mantenimientos/Principales/frmPrincipal02.vb
2. TOMIMSV4/TOMIMSV4/Reportes/Ubicaciones/frmrptUbicacionesVacias.vb
3. TOMIMSV4/TOMIMSV4/Reportes/Ubicaciones/frmrptUbicacionesVacias.Designer.vb
4. TOMIMSV4/DAL/Mantenimientos/Bodega/Bodega_Ubicacion/clsLnBodega_ubicacion_Partial.vb

## Validación

- Compilación de WMS.vbproj Debug/AnyCPU con MSBuild de Visual Studio 18: correcta (exit 0); DAL y BOF generados.
- Advertencias: licencia de evaluación de Dashboard DevExpress y propiedad Visible obsoleta en código existente.
- Pruebas sobre el ensamblado compilado, sin abrir conexión: retorno área -> general con 1086 vacías/1816 ocupadas, cero ubicaciones y solo vacías. Verificados una serie, dos barras, porcentaje y limpieza de enlace/eje.
- Pruebas de los cuatro selectores sobre el método compilado: selección exclusiva y regreso a Vacías al desmarcar. Carga de datos inhibida durante esta prueba.
- Diff de los cuatro archivos sin errores de espacios; UTF-8 BOM verificado.
- Callsite scan: Get_Ubicaciones de esta clase se consume desde este listado. Parámetro opcional conserva compatibilidad de llamadas existentes.
- Catálogo db-brain confirma bodega_ubicacion.bloqueada nullable bit y clave compuesta IdUbicacion/IdBodega. Snapshot Killios de abril: la vista no incluye Area; el código existente ya dependía de Area en la versión del cliente. No se alteró ese contrato y no se verificó la vista de BOC ADELI en vivo.
- Auditoría automática en audit.yml: WARN por no verificar BD en vivo. Lectura de catálogo y revisión del diff completadas; no equivale a prueba funcional en la base del cliente.
- Brain remoto no reindexado: BRAIN_IMPORT_TOKEN no disponible. Nota local conservada aquí.

## Pendiente de validación con datos reales

Abrir la compilación nueva y alternar Ocupación por área (también con actualización automática); seleccionar Bloqueadas y comprobar vacías/ocupadas; revisar Sí/No, diseño guardado, impresión y Excel. No se ejecutó la aplicación conectada a la base del cliente ni se distribuyó el ejecutable.

Los cambios locales preexistentes de inventario se conservaron.

## Seguimiento: espera visible
#EJC20260911_MONITOR_ESPERA: cursor de espera y texto Cargando ocupación durante el cambio; interruptor deshabilitado hasta finalizar. Se restaura cursor y selector en Finally, incluidos errores y carga previa en curso. Eliminada pausa artificial de 2000 ms. Compilador VB sin errores; la copia final del ejecutable está bloqueada por la sesión WMS_QAS abierta en Visual Studio. Cerrar la ejecución y recompilar para probar esta adición.

