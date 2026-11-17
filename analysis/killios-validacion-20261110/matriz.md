# Killios: matriz previa a publicación

Preparación: 10/11/2026. No constituye aprobación para publicar.

Ramas objetivo: `codex/killios-decimales-estados` en BOF y HH. BOF: `82f2e5d3404dc9e5e59c37910026f515f63bca90`. HH: `5df5530c4cc95ecb928ff9062cc14deb2e29d415`.
Los checkouts permanecen en La Cumbre; no se cambiaron ramas, configuraciones ni fuentes. No se hizo staging, commit, push ni despliegue.

## Cómo interpretar la evidencia

Las ejecuciones antiguas en Killios QA son antecedentes, no una ejecución actual del paquete que se publicará. Sus documentos son `legacy-unvalidated`; los hashes actuales de sus archivos y de los fuentes de la rama se conservan en `evidencia.json`. La coincidencia de un fuente no certifica una DLL, sus dependencias ni el servicio desplegado.

Separar tres defectos: captura de cajas interpretada como UN y generadora de fracciones; fragmentos que conservan indebidamente presentación de caja; conversión que podía fabricar existencia (9 UN convertidas en 12). Conservar el total por sí solo no comprueba los otros dos.

## Matriz

| ID | Caso y criterio de aprobación | Evidencia disponible | Pendiente antes de publicar |
|---|---|---|---|
| K01 | Clase de captura: factor único; licencia completa ya en UMBAS no se multiplica otra vez; CEST rechaza cajas fraccionarias y UN fraccionarias | Actual: 18 comprobaciones PASS sobre el archivo real local; `helper-check.txt` | Incorporar la clase a la entrega HH; compilación limpia del código entregable y prueba de pantalla |
| K02 | Cambios de estado de 3/9 UN desde Caja12: conservar total; fragmentos en UN; cajas completas mantienen presentación | Histórico: grupo de 29 casos en `66-regresion-restablecida.json` | Reejecutar localmente con la DAL compilada del commit objetivo; dirigido, no dirigido y BOF |
| K03 | Reservas ajenas intactas; reserva propia dirigida respetada; saldo negativo/exceso rechazados | Histórico: mismo grupo de 29; no son 29 casos adicionales | Repetir y conciliar por IdStock, estado de reserva y presentación |
| K04 | Factores 6/24, tarima 72 y residuos libres: factor físico correcto, reservas respaldadas | Histórico: 8 casos en `68-variantes-cambio-estado-qa.json` | Repetir; marcar factores sintéticos y separar los presentes en la copia local |
| K05 | Dos sesiones: saldo, estado y reservas desactualizados; rollback de la sesión competidora | Histórico: 4 casos en `67-concurrencia-cest-qa.json` | Repetir bloqueo/relectura; después dos operaciones reales desde HH. SQL simulado no equivale a dos terminales |
| K06 | Explosión MI3 con reserva previa: 36/12 reservadas/pedir3; 120/96/pedir3; 120/96/pedir15 | Histórico: 3 casos en `70-44-explosion-parcial-reservada.json` | Reejecutar motor real local. En último caso: 108 UN en caja +12 sueltas; reserva previa96 y nueva12+3; total120 |
| K07 | Dato inconsistente 9 o 9.25 UN con Caja12 nunca produce12; reserva no supera existencia disponible | Históricos: `25-prueba-motor-completo-qa.json`, `26-helper-regresion.json` | Fixture local sintético; diferencia entre tolerar dato histórico y permitir nueva captura fraccionaria |
| K08 | Pedidos 0.5/0.25/1.5 CA con factor12 equivalen a6/3/18 UN; no heredan restricción de captura CEST | Históricos: `stable-35-pedidos-fracciones-caja-wms272.json` y comparación baseline | Repetir reserva real local y recorrido desde entrada de pedido |
| K09 | Secuencia de cambios desde264 UN no genera nuevas fracciones de UN desde captura válida | Histórico: `36-replay-cest-wms272.json` documenta reproducción del problema; no es PASS del arreglo | Reproducir secuencia contra versión corregida con capturas válidas y rechazar solicitudes fraccionarias; conservar total y metadatos |
| K10 | Un error deja cero cambios parciales y reintentar no duplica movimiento/reserva | Histórico parcial: rollback/rechazos DAL en grupo29 | Fallo controlado y reintento desde el servicio; no inferir idempotencia desde rollback del ejecutor |
| K11 | Pedido → reserva → picking → verificación → despacho; saldo y salida física concilian | Sin evidencia completa de pantalla/HTTP vinculada al paquete actual | Ejecución local completa con servicio y HH identificados; registrar cantidades por etapa |
| K12 | Servicio, DAL y APK corresponden a commits/hashes de entrega; errores y validaciones intactos | Compilaciones históricas; helper Java actual aprobado aisladamente | Compilación de entrega y prueba HTTP/HH del mismo conjunto. Verificar configuración y destino sin registrar secretos |
| K13 | Conservación de lote, vencimiento, licencia, propietario y unidad; parámetros/peso cuando apliquen | Metadatos parcialmente cubiertos en grupos históricos; conservación integral de peso no certificada | Comparación por fixture; casos específicos con parámetros requieren datos preparados (local stock_parametro vacío) |
| K14 | Motor C# WebAPI, si forma parte de la publicación, conserva stock y reservas | Hallazgos separados en `analisis-webapi.md`; no resueltos por validar DAL VB | Confirmar alcance de publicación; no extender PASS de MI3/VB a WebAPI/C# |

Los 44 casos históricos son 29+8+4+3. Otras filas reutilizan evidencia o añaden cobertura; no se suman como casos independientes aprobados.

El fuente de reserva de la rama reproduce el hash de la corrección histórica al representarlo con CRLF. Git conserva LF; por eso los hashes de bytes originales difieren. Esta comparación respalda la continuidad textual del arreglo de reserva, sin reemplazar su reejecución.

## Base local revisada

Consulta exclusivamente de lectura contra `.\SQL2022`, base `TOMWMS_KILLIOS_PRD`. El nombre PRD no demuestra que sea laboratorio aislado ni que tenga la fecha del respaldo QAS anterior. No se consultaron servidores remotos. Ver `base-local.txt` y el script `tools/brain-quality/inspect-killios-local.sql`.

- 4907 stocks, 579 reservas, 8501 encabezados de pedido.
- WMS272 / producto-bodega941:45 stocks,6936 UN. WMS282 /2199:5 stocks,686 UN. También existen WMS215 y otros productos-bodega de estos códigos; seleccionar siempre producto+bodega, no solo código.
- Factores disponibles:4,5,6,7,8,10,12,15,16,18,20,24,30,32,36.
- Cero UN fraccionarias usando tolerancia1E-6; cero stocks positivos menores al factor. Esto no demuestra ausencia de cualquier otra inconsistencia de presentación.
- IdStock, IdStockRes, IdMovimiento e IdPickingUbic son identity. Cantidades siguen siendo float: medir ruido binario por separado de fracciones operativas. No redondear indiscriminadamente para hacer pasar pruebas.
- `stock_parametro` tiene cero filas. No certifica casos con parámetros.

Conclusión: la copia sirve como fuente de datos y candidata para el laboratorio. Los registros y saldos difieren de la QAS histórica: no reutilizar IdStock/IdPedido/IdUbicacion de los scripts antiguos a ciegas. No se ejecutó todavía negocio contra ella.

## Preparación local recomendada

1. Confirmar que la copia local se puede usar como laboratorio, preferentemente restaurándola como `TOMWMS_KILLIOS_DECIMAL_LAB`. No crear ni restaurar bases automáticamente en esta tarea. No es una carpeta o checkout de código adicional.
2. Elegir las ramas Killios en las carpetas habituales, preservando archivos locales. Incluir `CantidadCambioEstado.java` en la entrega HH junto al formulario; no moverlo ni versionarlo en La Cumbre por inferencia.
3. Compilar servicio/DAL/HH de esos fuentes. Guardar logs y hashes en el Brain. Ninguna prueba debe reutilizar una DAL de La Cumbre por compartir carpeta de salida.
4. Adaptar los ejecutores antiguos: destino local explícito y comprobación exacta de base/servidor; consultar IDs y metadatos vigentes; licencias de ensayo únicas; no conservar credenciales ni conexiones completas en evidencias.
5. Primero pruebas DAL con fixtures y rollback; luego concurrencia con fixtures visibles y limpieza comprobada. Los identity pueden avanzar aun con rollback. No disparar SAP, correo, sincronizadores ni integraciones externas.
6. Para pantallas usar datos de ensayo persistentes en el laboratorio, ya que una transacción externa de prueba no engloba llamadas independientes desde HH/HTTP. Fotografías antes/después y limpieza específica; no editar pedidos reales para simplificar fixtures.
7. Cerrar K01–K13 aplicables y definir K14 según alcance. Registrar no-aplica con justificación; no convertir pendientes en PASS. La publicación sigue pendiente de validación.

## CantidadCambioEstado

La rama HH referencia la clase en tres llamadas (aproximadamente líneas1579,5555,6063 del formulario); `git ls-tree` no contiene la clase. El archivo existe sin seguimiento en el checkout habitual. Las 18 comprobaciones actuales verifican esa copia exacta y su hash queda registrado. No se modificó ni se agregó al índice. Se requiere incluirla en el commit/paquete Killios y compilar desde contenido entregable para evitar depender de un archivo local oculto.
