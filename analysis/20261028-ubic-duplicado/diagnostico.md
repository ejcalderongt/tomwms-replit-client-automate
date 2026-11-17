# Cambio de ubicación BOF: filas duplicadas
#EJC20261028

Evidencia: captura con dos filas idénticas del código 4.
Ruta: frmImplosion.Listar -> clsLnTrans_ubic_hh_enc.Get_All_Filtro -> VW_TransUbicacionHhEnc -> Cargar -> filas del grid. El listado independiente de ubicación y control de calidad también consumen el método compartido.
El catálogo local muestra relaciones con usuario_bodega y rol, susceptibles de multiplicar filas. No se ha consultado la BD de la sesión; no se afirma que existan dos movimientos guardados.
Corrección: SELECT DISTINCT en Get_All_Filtro para suprimir filas completamente idénticas de la vista sin descartar tareas diferentes ni escoger un rol arbitrario. No modifica persistencia, stock ni la vista SQL.
Validación: DAL compilado con Visual Studio 18 y dependencias locales, 0 errores y 0 advertencias; git diff --check correcto. VB conserva UTF-8 BOM.
Pendiente: distribuir DAL.dll junto al BOF usado por la usuaria y verificar código 4 con fechas 28-Oct-2026. No se reemplazó la aplicación en ejecución. Si las filas difieren en campos ocultos o roles, DISTINCT no las fusiona; revisar entonces datos y definición de vista del entorno afectado.
Brain remoto sin reindexar: BRAIN_IMPORT_TOKEN no disponible. Sin commits ni cambios en WMS.vbproj preexistente.
