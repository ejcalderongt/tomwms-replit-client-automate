# Factura de reserva de ingreso: traza de WarehouseCode

Carolina reportó fallo sin depurador y éxito posterior al depurar para DocEntry 137463 / DocNum 31080, IdWMS 28360. Es evidencia reportada por la operadora; no se inspeccionó SAP ni la base WMS.

Alcance: checkout habitual TOMWMS_BOF, rama dev_2026_estable. Baseline de código: 31c73d606d4c0d47c6015e8e5f1c68b779ead6b2. Cambio local sin commit. No se ejecutó sincronización ni se modificaron datos, configuración o cambios ajenos.

El punto que lanza el error es FacturaReservaMapper.ObtenerBodegaUnica. La sobrecarga del lector llamada por Importar_Facturas_Reserva_Desde_SAP_A_TablaIntermedia espera tanto SendAsync como ReadAsStringAsync antes de analizar el JSON. No se ha demostrado una carrera de tiempos. No hay respuesta histórica del intento fallido para comparar.

Se instrumentó únicamente esa sobrecarga, conservando el filtro, el mapeo, las validaciones y las escrituras existentes. La otra sobrecarga con SqlConnection/SqlTransaction no llama a ObtenerBodegaUnica y permanece sin cambios.

Registro JSONL por consulta en `%LOCALAPPDATA%\TOMWMS\SapSyncMampa\Traces\FacturaReserva-*.jsonl`. La pantalla muestra el nombre exacto. Registra filtro, bodega configurada, ejecutable, versión, depurador conectado, tiempos, estado HTTP, DocEntry/DocNum y estado/tipo/bodega de cada línea al recibir y antes de validar. No agrega cookies, credenciales, direcciones de servidor, nombres de proveedor ni payload completo al archivo. El error identifica el documento actual en pantalla. Una falla de escritura de traza avisa sin reemplazar el procesamiento normal.

## Verificación

- Compilación Debug/AnyCPU exitosa con MSBuild 18, sin recompilar proyectos dependientes, usando sus DLL locales. Salida y temporales en esta carpeta, no se reemplazó el ejecutable habitual. Ver build.log.
- check-trace.ps1 ejecuta por reflexión el resumen, el escritor y la validación del ejecutable compilado. Seis fixtures: ausente, null, vacío, espacios, válido y múltiples bodegas. Verifica JSONL, ausencia de campos no autorizados e inmutabilidad del JSON de entrada. Resultado PASS. No constituye prueba contra SAP.
- UTF-8 BOM y CRLF conservados. Diff limitado a un archivo de producto; los cambios previos del checkout se preservaron.

## Próxima prueba

1. Compilar SAPSYNCMAMPA en Visual Studio en dev_2026_estable y cerrar cualquier ejecutable anterior.
2. Ejecutar sin depurar con el documento nuevo. Si la pantalla permite indicar el número, usarlo para delimitar la consulta.
3. Conservar la salida completa y el archivo indicado por `Traza factura de reserva`, tanto si funciona como si falla.
4. Revisar DocumentoRecibido / AntesValidarBodega / BodegaValidada / ErrorLectura. El filtro existente puede traer varios documentos pendientes; no atribuir el error al documento esperado sin comprobar DocEntry/DocNum.
5. No usar la bodega configurada como reemplazo automático de WarehouseCode ni introducir esperas/reintentos sin evidencia. Evitar volver a importar el documento que ya fue sincronizado solo para probar.

La versión del ensamblado puede repetirse entre compilaciones; la ruta y versión ayudan a identificar ejecuciones, pero no son una huella criptográfica del binario usado por la operadora.

## Aclaración y ampliación del mismo día

Carolina aclaró que el fallo ocurre al confirmar sin indicar documento: se consultan los pendientes y uno sin bodega interrumpe la lectura completa. No se comprobó que el intento posterior al depurar consultara el mismo conjunto de documentos.

Se agregó CKFK261117ReservaLote en el mismo archivo de producto. La validación de bodega se ejecuta por documento, antes de consultar vendedor o crear datos auxiliares. Si ObtenerBodegaUnica rechaza las líneas con ApplicationException, se muestra `NO IMPORTADA: Factura SAP DocNum … (DocEntry …): motivo`, se registra DocumentoOmitido y continúa la lectura con la siguiente factura. Se conservan los criterios existentes de bodega única; no se asigna una bodega alternativa.

El resumen indica facturas aptas para importar y omitidas por bodega. No afirma que las aptas ya se hayan importado. Las omitidas no entran a la lista de importación ni se marcan sincronizadas en SAP. Si no hay ninguna apta, el importador retorna antes de limpiar las tablas intermedias. Esto también aplica si se solicita individualmente una factura inválida. Errores distintos a la validación de bodega mantienen el comportamiento previo de interrupción.

Compilación repetida con éxito tras esta ampliación. check-trace.ps1 verifica además el nuevo validador contra los seis casos anteriores y una secuencia de documentos válido/inválido/válido, más documentos sin líneas o con arreglo vacío. Verifica selección y rechazo, no ejecuta el lector HTTP completo ni escrituras SAP/WMS. La prueba real de lote mixto sigue pendiente. El script de aplicación y el diff están en tools/diagnostics/prepare-sap-reserva-batch-20261117.py y batch.patch, respectivamente.
