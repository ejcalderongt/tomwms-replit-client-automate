# Resumen de mediciones

Los percentiles incluyen solo intentos exitosos. Revisar fallos, timeouts, workers.csv y CSV originales por separado.
Una sesiÃ³n interrumpida o un worker fallido no demuestra ausencia de incidentes.
Workers no completados: 0.
DNS consulta A al resolver configurado: su timeout es el de Windows, no timeoutSeconds.
TCP por nombre incluye resoluciÃ³n local. HTTP usa proceso/conexiÃ³n nuevos por intento; puede usar proxy del entorno.
waitFirstByteMs incluye envÃ­o de solicitud, transporte, colas y procesamiento; no es tiempo SQL ni CPU puro.
SQL SELECT 1 mide conexiÃ³n/login y roundtrip trivial; no representa la consulta real de WMS.
p99 con pocas muestras no es estable. No comparar escenarios de duraciÃ³n o carga distintas sin indicarlo.

No hay atribuciÃ³n automÃ¡tica de causa. Correlacionar incidents.csv, mÃ©tricas y evidencia de AP/IIS/SQL.
