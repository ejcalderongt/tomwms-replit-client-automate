# Validación y límites

<!-- #EJC20261111_CUMBRE_NETWORK_DIAGNOSTICS -->

Pruebas sintéticas locales del 11 de noviembre de 2026, usando únicamente loopback, en Windows PowerShell 5.1 y PowerShell 7. No se contactó la red de La Cumbre.

Comprobado en ambos entornos:

- ICMP loopback y conexiones TCP abiertas/rechazadas.
- HTTP correcto, HTTP 503, SOAP Fault con HTTP 200, marcador de respuesta ausente y timeout real de curl.
- POST con cuerpo/cabeceras por archivos locales y método declarado de lectura.
- Fallo de conexión SQL identificado como SqlException; no como fallo del constructor de configuración.
- Workers completos, CSV, percentiles de éxitos y denominador de fallos incluyendo timeouts.
- Marcadores de incidente y exclusión de snapshots DMV del resumen de sondas.
- Rechazo de un método HTTP que no haya sido declarado de lectura.
- No se copian cuerpos de respuesta ni configuración privada a la evidencia de prueba.

Evidencia y pruebas reproducibles en el Brain de preparación: `analysis/cumbre-network-suite-20261111/validation.json` y `validate_suite.py` (no se distribuyen en el ZIP operativo).

Pendiente de validar en destino: destinos reales, resolución DNS interna, SQL login/SELECT 1 exitoso, autenticación SQL por credencial, permisos/visibilidad de DMV, observación de servidor, instalación Npcap, captura real y análisis TShark. Wireshark no estaba instalado en la máquina de preparación. No se afirma compatibilidad de hardware de captura ni causa raíz del incidente con estas pruebas.

La configuración de ejemplo no ejecuta sondas hasta indicar destinos. La suite es un instrumento de diagnóstico de baja frecuencia; no sustituye captura del trayecto HH/servidor, métricas del AP ni medición de la operación real.
