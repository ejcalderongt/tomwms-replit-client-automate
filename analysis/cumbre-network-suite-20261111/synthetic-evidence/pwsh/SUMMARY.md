# Resumen de mediciones

Los percentiles incluyen solo intentos exitosos. Revisar fallos, timeouts, workers.csv y CSV originales por separado.
Una sesión interrumpida o un worker fallido no demuestra ausencia de incidentes.
Workers no completados: 0.
DNS consulta A al resolver configurado: su timeout es el de Windows, no timeoutSeconds.
TCP por nombre incluye resolución local. HTTP usa proceso/conexión nuevos por intento; puede usar proxy del entorno.
waitFirstByteMs incluye envío de solicitud, transporte, colas y procesamiento; no es tiempo SQL ni CPU puro.
SQL SELECT 1 mide conexión/login y roundtrip trivial; no representa la consulta real de WMS.
p99 con pocas muestras no es estable. No comparar escenarios de duración o carga distintas sin indicarlo.

No hay atribución automática de causa. Correlacionar incidents.csv, métricas y evidencia de AP/IIS/SQL.
