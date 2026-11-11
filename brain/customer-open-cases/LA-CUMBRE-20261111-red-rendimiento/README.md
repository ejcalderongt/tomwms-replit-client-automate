---
tipo: caso-abierto
clientes: [CUMBRE]
estado: abierto-pendiente-correlacion
fecha: 2026-11-11
---

# La Cumbre — diagnóstico de red y rendimiento

Caso abierto para retomar y cerrar con evidencia de una operación lenta. Causa raíz: **no determinada**. Hallazgos preliminares; no hay validación independiente ni cambios de configuración productiva.

## Informes y evidencia publicable

- [Correlación del servidor](correlacion-servidor-la-cumbre.html): ventana 12:21:17–12:26:18, UTC−06; 136 POST, 15 IP, todos HTTP 200. Mediana IIS 50 ms, p95 117 ms, máximo 373 ms. CPU media 1.04%, máxima 8%; memoria libre mínima 39.89 GiB. Sin saturación sostenida observada; el muestreo no descarta picos breves, esperas SQL o almacenamiento.
- [Resumen estructurado del servidor](servidor-resumen.json) y [hashes de evidencia recibida](servidor-evidencia-SHA256.csv).
- [Red y HTTP](diagnostico-la-cumbre.html), [paquetes](tomografia-paquetes-la-cumbre.html) y [UniFi Centro Logístico](evaluacion-unifi-centro-logistico.html).
- [Procedencia de afirmaciones](claims.json). Los hashes comprueban bytes; no validan causalidad. Fuentes sin commit en su registro original son material preparado localmente, ahora incluido en este caso.

## Hallazgos que orientan la siguiente visita

La captura anterior registró unos 13.34 s desde la petición GET hasta la respuesta HTTP, después del ACK del servidor. Ocurrió antes de la ventana del servidor; no puede atribuirse a CPU, SQL, IIS o WiFi con los datos actuales. Las sondas previas no mostraron fallos sostenidos de TCP/ICMP. Las marcas TCP aisladas y RSC no demuestran colisiones o pérdida sostenida. Este adaptador no permitió captura 802.11 en modo monitor.

UniFi mostró P9/P4 con uplink de 100 Mbps, P14/P16 desconectados y concentración de canales 5 GHz. Verificar primero cableado/puertos de P9/P4 y estado real de los AP; cualquier ajuste RF debe pasar por un piloto medido. La IP 10.12.2.240 (máximo IIS de 373 ms) aparecía antes como CK62 en P9: coincidencia útil, sin asociación histórica o causalidad demostrada. EDA61K 10.12.2.104 aparecía en P8/2.4 GHz; en la ventana IIS tuvo máximo 117 ms.

## Plan para afinar y cerrar

1. Registrar hora, duración, dispositivo, operación WMS y resultado funcional de una lentitud real.
2. Recopilar simultáneamente métricas del servidor, IIS, cliente y AP; vincular IP/MAC/AP con su hora y corregir o cuantificar el desfase de relojes. El servidor informó sincronización NTP; el PC no estaba sincronizado en la toma anterior.
3. Exportar IIS retrospectivo de 2026-11-11 17:10–17:16 UTC para el GET inicial (cliente 10.12.2.157), verificando retención, desfase y cobertura.
4. Obtener snapshots SQL de solo lectura antes/durante/después: sesiones activas, bloqueos y esperas. La prueba SQL anterior quedó bloqueada por confianza del certificado TLS; resolver la cadena sin desactivar su validación. No se recopilaron DMV en esta sesión.
5. Verificar uplinks P9/P4 y realizar un piloto RF/roaming con terminales representativos; comparar antes/después de la misma operación. No aplicar cambios generales por una sola instantánea.
6. Cerrar únicamente con causa respaldada y resultado medido tras la corrección, o documentar explícitamente que no fue reproducible y el alcance de la observación.

## Límites y conservación

HTTP 200 no prueba éxito funcional SOAP; no se guardaron cuerpos ni SOAPAction. IIS puede tener cola de escritura; falta cs-version. Eventos de Aplicación no disponibles (System.Exception); no confundirlo con ausencia de errores. Sistema no mostró eventos coincidentes en la ventana. El manifiesto original falló al incluirse a sí mismo; se corrigió el generador local y se calcularon hashes al recibir los archivos, sin necesidad de repetir los cinco minutos.

Este caso publica informes y agregados. PCAP, datos detallados de clientes, configuraciones privadas, credenciales y entornos virtuales permanecen fuera del commit. La evidencia original está localmente en `analysis/cumbre-network/2026-11-11/`, incluida `server-evidence/20261111-122116-7910c64a/`; no se presupone disponible en otro equipo. Sus hashes permiten reconocerla si se transfiere por un canal autorizado.

Runbook: [diagnóstico en sitio](../../runbooks/cumbre-diagnostico-latencia-en-sitio.md). Base Brain al recopilar: `18fc415b07775cd766615df6448be1688924889f`. No hubo cambios en WMS, SQL ni red productiva.
