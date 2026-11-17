# Suite de diagnóstico — La Cumbre

<!-- #EJC20261111_CUMBRE_NETWORK_DIAGNOSTICS -->

Preparada el 11 de noviembre de 2026. Punto de observación acordado: PC conectada directamente a la red de La Cumbre. Ubicación de WS/SQL, topología y clientes afectados pendientes de verificar. Windows 10/11, Windows PowerShell 5.1 o PowerShell 7. No requiere Python, SQL Management Studio ni módulos PowerShell externos. La captura opcional requiere Wireshark/TShark/Npcap.

Para recuperar este trabajo desde el repositorio en una PC nueva: [restauración del Brain y arranque](RESTAURAR-OTRA-PC.md).

## 1. Antes de llevar la PC

Descomprimir el ZIP en `C:\CumbreDiag`. Abrir PowerShell allí. Instalar el agente que normalmente utiliza el equipo y comprobar que puede trabajar con archivos locales; el acceso a Internet del agente es independiente de la conectividad interna de WMS. Copiar el prompt `PROMPT-AGENTE.md` para iniciar la sesión en destino.

```powershell
Set-Location C:\CumbreDiag
.\Prepare-PC.ps1
# Instalación opcional, ejecutar antes del viaje:
.\Prepare-PC.ps1 -InstallWireshark
.\Start-Capture.ps1 -List
Copy-Item .\config.example.json .\config.local.json
```

Wireshark: usar instalador oficial firmado; seleccionar TShark y completar Npcap. El instalador/driver puede requerir administrador. Si no hay winget: https://www.wireshark.org/download.html. Verificar `-List`: la presencia de Wireshark no demuestra que Npcap funcione. No hace falta privilegio de administrador para las pruebas básicas; la política de Npcap determina quién puede capturar. Si una política bloquea scripts, aplicar el procedimiento de TI del cliente; no se cambia permanentemente la política de ejecución.

Comprobar espacio libre (recomendado 2 GB), cargador, adaptador Ethernet y acceso autorizado al SSID/VLAN de las HH. Evitar suspensión durante la ventana según política de la empresa. Llevar el ZIP sin contraseñas y comprobar `PACKAGE-SHA256.csv`. Este paquete transporta un extracto del Brain con procedencia; no contiene el Brain completo ni código/configuraciones productivas.

## 2. Completar configuración en sitio

El ejemplo trae CERO destinos: no ejecuta conexiones por inferencia. Editar `config.local.json` con el técnico. Los valores de abajo son plantillas ficticias, no endpoints verificados.

```json
{
  "site": "La Cumbre",
  "scenario": "wifi-zona-A-misma-VLAN-HH",
  "durationSeconds": 1800,
  "intervalSeconds": 5,
  "timeoutSeconds": 3,
  "icmp": [
    {"name":"gateway", "host":"IP_GATEWAY"},
    {"name":"ws", "host":"IP_WS"},
    {"name":"sql", "host":"IP_SQL"}
  ],
  "dns": [
    {"name":"ws_dns", "host":"NOMBRE_WS", "server":"IP_DNS_INTERNO"}
  ],
  "tcp": [
    {"name":"ws_port", "host":"IP_WS", "port":443},
    {"name":"sql_port", "host":"IP_SQL", "port":1433}
  ],
  "http": [
    {"name":"asmx_page", "url":"https://NOMBRE_WS/WSHHRN/WebService.asmx", "method":"GET", "approvedReadOnly":true, "expectedStatus":[200], "expectedContains":"WebService"}
  ],
  "sql": {
    "enabled":false, "server":"tcp:NOMBRE_SQL,PUERTO_REAL", "database":"BASE_REAL",
    "authentication":"Windows", "encrypt":true, "trustServerCertificate":false,
    "intervalSeconds":15
  }
}
```

Confirmar HTTP/HTTPS, ruta .asmx, puerto SQL estático/dinámico y nombre de instancia. 1433/443 son solo ejemplos. Quitar pruebas de destinos inexistentes. Registrar las IP reales y la equivalencia de cada etiqueta en notas LOCALES del caso. La suite no hace barrido de subredes. DNS de tipo A consulta un resolver explícito; IPv6 se debe investigar aparte cuando forme parte del trayecto.

El GET del ejemplo es la página .asmx: NO mide una llamada de negocio ni SQL. El WSDL real (`?WSDL`) puede comprobarse manualmente: el runner rechaza URL con query para evitar incorporar parámetros privados. Elegir un GET sin query verificable o un POST de lectura conocido para medir automáticamente.

Para consumir SOAP de verdad, añadir un destino aparte tras revisar el método con el responsable WMS: no usar métodos de reserva, picking, movimientos, login con efectos secundarios ni escrituras como prueba repetitiva. Ejemplo:

```json
{"name":"consulta_soap_lectura", "url":"https://NOMBRE_WS/WSHHRN/WebService.asmx", "method":"POST", "approvedReadOnly":true, "expectedStatus":[200], "expectedContains":"NOMBRE_REAL_DEL_RESULTADO", "headersFile":"private/soap-headers.txt", "bodyFile":"private/consulta.xml"}
```

El técnico/agente prepara `consulta.xml` con sobre SOAP y parámetros reales de lectura. `soap-headers.txt` contiene, según contrato, `Content-Type: text/xml; charset=utf-8` y `SOAPAction: "ACCION_REAL"`. Para REST puede usarse otro método de lectura GET/POST con marcador de respuesta específico. `approvedReadOnly` es una declaración que debe comprobarse, no un analizador automático del código. Una coincidencia textual es validación mínima; revisar también `error`, `Process_Result` y contrato del método. HTTP 200 no garantiza éxito de negocio. SOAP Fault se reconoce como fallo.

Proteger `private/` y configuración con permisos Windows del operador. La suite no guarda el cuerpo de respuesta; usa temporalmente un archivo de usuario y lo elimina al terminar cada llamada. Una terminación abrupta puede dejar temporales. Cabeceras/cuerpos privados se pasan por archivo, sin contraseñas en argumentos. No introducir secretos en nombres de destino, etiquetas de escenario ni notas de incidentes. No enviar PCAP, IP internas o configuraciones a servicios públicos. Las capturas incluso truncadas pueden contener datos privados.

SQL admite `Windows` o `SqlPrompt` (pide credencial sin guardarla). Usar cuenta existente de diagnóstico con mínimo privilegio. No copiar Conn.ini, no usar sa por comodidad, no crear usuarios ni conceder permisos automáticamente. Mantener cifrado y validación de certificado; si falla TLS, documentarlo y resolver confianza/nombre con TI. No desactivar validación para ocultar el problema.

## 3. Ejecutar y marcar lentitud

```powershell
.\Run-Diagnostics.ps1 -ConfigPath .\config.local.json
```

La consola muestra la carpeta de sesión. En otra terminal, ANTES de que el operador lance una acción:

```powershell
.\Mark-Incident.ps1 -Run 'C:\CumbreDiag\evidence\CARPETA_SESION' -Phase start -Operation 'consulta_existencias_01' -Device 'HH01' -Zone 'pasillo-A'
# Cuando la pantalla responde:
.\Mark-Incident.ps1 -Run 'C:\CumbreDiag\evidence\CARPETA_SESION' -Phase end -Operation 'consulta_existencias_01' -Device 'HH01' -Zone 'pasillo-A' -Note 'respuesta lenta observada'
```

Relojes: se registra UTC con offset en las pruebas, zona Windows y estado `w32tm`; no se cambia la hora. Anotar el desfase de HH/AP/IIS/SQL respecto a la PC antes de correlacionar. Los marcadores son observaciones humanas, no duración exacta del código. Vincular un identificador de operación y repetirlo en logs cuando sea posible sin modificar producción.

Cada destino corre en un worker independiente, no detrás de un HTTP lento. Por destino solo hay una solicitud activa; no hay retries ni prueba de carga. Mantener pocos destinos, 5 s básico / 15 s SQL y registrar carga operativa. DNS usa el timeout de Windows; SQL puede consumir timeout de conexión más timeout de comando. El tiempo total de sesión puede exceder ligeramente la duración configurada por inicio/cierre y solicitudes pendientes. Ctrl+C detiene los jobs de esta sesión; revisar `workers.csv` y evidencia parcial. No concluir que una prueba fallida o ausente fue exitosa.

## 4. Captura durante la reproducción

```powershell
.\Start-Capture.ps1 -List
.\Start-Capture.ps1 -Interface 'NUMERO_OBSERVADO' -Filter '(host IP_WS or host IP_SQL) and (tcp or icmp)' -Run 'C:\CumbreDiag\evidence\CARPETA_SESION' -Seconds 300
# Incluir host DNS y port 53 si el incidente lo requiere.
.\Analyze-Capture.ps1 -Capture 'C:\CumbreDiag\evidence\CARPETA_SESION\ARCHIVO_REAL.pcapng'
.\Summarize.ps1 -Run 'C:\CumbreDiag\evidence\CARPETA_SESION'
```

Filtro obligatorio y específico, sin promiscuidad. Ventana por defecto 5 min, snapshot 256 bytes, rotación 8 archivos de 16 MiB (aprox. 128 MiB); conservar pronto la ventana del incidente porque el anillo sobreescribe archivos antiguos. Los 256 bytes permiten indicadores TCP pero pueden truncar contenido. Aumentar snaplen solo cuando la evidencia lo requiera; HTTPS no se descifra por capturarlo y no se distribuyen claves TLS. Revisar log de dumpcap para drops. Analizar cada archivo retenido; para flujos repartidos entre archivos se pueden unir con `mergecap` y analizar el resultado, advirtiendo intervalos sobrescritos. Los indicadores por archivo pueden perder estado TCP del anterior.

Una captura desde la PC no ve por defecto el unicast HH↔WS ni WS↔SQL en una red conmutada. Para esos trayectos pedir SPAN/TAP o captura local en IIS/SQL; AP/controller para retransmisiones 802.11, canal, airtime, utilización, roaming y razones de desasociación. No activar monitor mode ni cambiar switches/AP automáticamente. La misma intensidad de señal no garantiza el mismo BSSID, canal, VLAN, banda o experiencia de una HH.

## 5. Matriz mínima en sitio

| Caso | Condición | Ventana inicial |
|---|---|---|
| A | PC por cable, misma VLAN y destino que HH si está permitido | 20–30 min con operación normal y lentitud |
| B | PC Wi-Fi fija, mismo SSID/banda/zona de HH | 20–30 min, repetir misma lectura |
| C | PC Wi-Fi durante recorrido entre zonas | 10–15 min, anotar cambios de BSSID |
| D | HH afectada + HH que funciona en la misma zona | Reproducir 5–10 operaciones comparables |
| E | Pruebas básicas desde el servidor IIS hacia SQL | Durante el mismo incidente |
| F | Instalación sana, si está disponible | Mismo método, tamaño de respuesta, versión y carga comparable |

Registrar fecha, operador, zona, SSID/BSSID, adaptador, IP/VLAN, cable/Wi-Fi, versiones HH/BOF/WS y tamaño de datos. Una interfaz activa por escenario cuando sea viable; comprobar ruta/IP remota antes de comparar. No deshabilitar interfaces o VPN que mantengan el acceso remoto del técnico. PC cableada y PC Wi-Fi sirven como controles; no reproducen por sí solas la radio/controlador/ahorro de batería de Android.

## 6. Evidencia opcional del servidor

En la PC de diagnóstico, con cuenta autorizada y SQL configurado:

```powershell
.\Collect-SqlSnapshot.ps1 -Run 'C:\CumbreDiag\evidence\CARPETA_SESION'
# Repetir durante el problema y después para obtener deltas.
```

Las DMV pueden requerir VIEW SERVER STATE o VIEW SERVER PERFORMANCE STATE (según versión). No concederlos automáticamente. Sin visibilidad suficiente no afirmar «sin bloqueos». Snapshot: requests/bloqueadores/esperas activas, IO por archivo de la base y waits acumulados; sin textos de consultas ni planes. Usar diferencias entre snapshots, ignorar contadores reiniciados tras reboot/restart, normalizar por período y actividad. Los waits globales por sí solos no identifican culpable.

En el servidor Windows (IIS y/o SQL), copiar scripts y ejecutar localmente:

```powershell
.\Observe-Server.ps1 -Output C:\CumbreDiag\server-evidence -Seconds 300
```

CPU total, memoria libre, cola de disco, procesos w3wp/sqlservr. `CPU` de proceso es acumulado en segundos; calcular delta / intervalo / núcleos para %. Cola de disco sola no mide latencia de almacenamiento: cruzar con deltas de IO SQL. No se conecta por administración remota ni instala servicios.

Solicitar a TI logs IIS de la ventana (campos fecha/hora UTC, método, URI sin secretos, status/substatus/win32-status, time-taken), reinicios de app pool, eventos del sistema, CPU/RAM/disco y logs AP/switch. Si un campo no existe, registrarlo como faltante; habilitar logging nuevo es un cambio separado. `time-taken` IIS no equivale siempre a tiempo puro de aplicación. WS local rápido / remoto lento apunta al trayecto, pero aún exige validar equivalencia de autenticación, proxy y caché.

## 7. Interpretación sin saltos causales

| Observación coincidente con el incidente | Siguiente discriminador |
|---|---|
| Gateway lento/pérdida solo en Wi-Fi | Cable, AP airtime/retries/roaming, otra HH en misma zona |
| ICMP falla, TCP/HTTP normales | Filtrado/priorización ICMP; no declarar pérdida de aplicación |
| DNS lento, TCP por IP rápido | Resolver configurado, sufijos, caché, respuestas y split DNS |
| TCP rápido, HTTP espera antes de primer byte | Comparar IIS local, colas/hilos, llamada real y SQL; espera incluye transporte |
| HTTP llega bien, pantalla HH tarda | Número de llamadas, reintentos, parseo, SQLite, impresión, UI/hilo principal |
| SELECT 1 rápido, consulta real lenta | Bloqueos, plan/índices/datos/parametrización y WS↔SQL; no culpar a red por descarte |
| Login SQL lento, SELECT 1 rápido | DNS, TLS, autenticación, distancia y pooling; la sonda abre conexión nueva |
| Retransmisiones/zero-window/RST | Dirección y momento, drops de captura/offload, receptor/CPU; indicador no prueba causa |

HTTP: DNS/TCP/TLS, TTFB, espera desde pretransfer, descarga, total y status. Cada intento abre proceso y conexión nuevos; no reproduce pooling/keepalive de HH. No sigue redirecciones, no reintenta ni ignora TLS. Puede heredar proxy del entorno del proceso: contrastarlo con el de WMS. El resultado por nombre incluye DNS; para aislarlo añadir TCP por IP al mismo destino, manteniendo HTTPS por nombre con SNI/certificado correcto.

CSV contiene intentos y fallos; summary.csv usa solo éxitos y nearest-rank p50/p95/p99/máximo. Fallos HTTP conservan duración; revisar aparte, porque excluir timeouts reduce percentiles artificialmente. No usar umbrales universales ni promedio como única medida. Establecer referencia por operación, ventana y carga. En un episodio de 2 segundos una sonda cada 5 segundos puede no coincidir: usar captura y logs o ajustar intervalo sin convertirlo en carga.

## 8. Cierre y mantenimiento del Brain

Entregar `REPORT-TEMPLATE.md` completado, CSV, incidentes, snapshots y hashes; preservar PCAP local con acceso restringido. `Summarize.ps1` regenera SHA256.csv después de añadir evidencia. No subir material bruto al Brain remoto automáticamente; revisar/redactar primero.

El agente mantiene un Brain local del caso con hechos, hipótesis, decisiones, fuentes y pendientes. El extracto `brain-context/` no prueba versión desplegada ni topología. Antes del viaje/diagnóstico actualizar el checkout Brain por el procedimiento vigente del equipo; nunca hacer reset/clean ni sobreescribir trabajo local para «sincronizar». Conservar commit y hashes de fuentes. Si no hay checkout completo, usar una carpeta local del caso y entregar un handoff para integrarlo al Brain; no simular sincronización remota. No commit/push ni correcciones de infraestructura/producto implícitas.

Referencias oficiales consultadas para la suite: [instalación Wireshark/Npcap](https://www.wireshark.org/docs/wsug_html_chunked/ChBuildInstallWinInstall.html), [dumpcap: filtros y anillo](https://www.wireshark.org/docs/man-pages/dumpcap.html), [TShark](https://www.wireshark.org/docs/man-pages/tshark.html), [curl: tiempos](https://curl.se/docs/manpage.html), [SQL dm_exec_requests y permisos](https://learn.microsoft.com/en-us/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql?view=sql-server-ver17).
