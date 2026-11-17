# Corrección y recuperación preparada

> **Cierre posterior, #CKFK20261117_REC28335_CIERRE:** CKFK confirmó en el chat
> que el caso quedó bien y pidió registrar la recuperación confirmada. Ver
> `confirmacion-ckfk.md` y
> `brain/handoffs/2026-11-17-ckfk-recepcion-28335/README.md`. Los apartados siguientes
> conservan la preparación y sus pendientes en aquel momento; no niegan el cierre
> reportado posteriormente. No hubo una nueva consulta SQL después del cierre.

<!-- #CKFK20261117_REC_CM_FIX -->

## Estado de entrega

Cambios locales en las carpetas habituales de HH y BOF, ambas en
`dev_2026_estable`. Sin commit, push, despliegue, instalación o cambios de datos.
Web Service y dependencias compilan. APK debug generado para pruebas.
Las correcciones de HH y BOF están separadas en sus repositorios y parches.

## Comportamiento corregido

- `clsBeTrans_oc_det` permite una copia superficial independiente. El flujo de
  caja master modifica únicamente las cantidades de la copia; los objetos
  anidados se conservan para consulta y no se modifican en ese recorrido.
- `Procesa_Caja_Master` conserva cada fila de lote, calcula `cantidad-recibida`,
  omite lotes completos y detalles de compra completos, y valida la identidad
  funcional (OC, producto, línea y variante cuando hay control talla/color).
  Así 4+3 conserva 4+3 en cualquier orden, sin cambiar las 7 esperadas en OC.
- `LoadCajaMaster` limpia la lista antes de cargar y asigna temporales negativos
  diferentes a cada detalle. `Guardar_Caja_Master` usa los mismos temporales
  para stock_rec. Los IDs negativos no pueden confundirse con identity positivos
  persistidos al pasar por las comprobaciones de existencia/eliminación.
- `GuardarHH_CM` valida una única licencia documental, PK de lotes, cantidades
  finitas, correlación uno a uno detalle/stock y saldo actual de cada lote bajo
  `UPDLOCK,HOLDLOCK`. Distribuye el ingreso por los lotes de la misma identidad
  funcional, incrementando cada contador por su PK dentro de la transacción.
  La correlación no requiere fabricar una LP física si los parámetros no la
  permiten. No se cambiaron reglas por empresa, firmas SOAP ni esquema SQL.
- Una solicitud desactualizada o con temporales ambiguos se rechaza. Esto exige
  actualizar la HH junto con el servicio. El rollback existente de GuardarHH_CM
  protege el conjunto si falla otra validación o persistencia posterior.

## Script para revisar

Archivo canónico de datos:
`C:/Users/carol/source/repos/DBA/20261117_recuperar_producto_recepcion_28335.sql`.

Está en vista previa por defecto, con `@Aplicar=0` y la confirmación de versiones
instaladas también en 0. No se envió ni ejecutó en SQL Server: únicamente se
extrajeron sus condiciones a SELECT independientes para contrastar el estado.
Las 11 condiciones coincidieron con QA durante la preparación; el script las
vuelve a comprobar y bloquear al aplicar, dentro de una transacción.

Alcance exacto: cinco filas. Los lotes 1348/1349 pasan de recibido 0/0 a 3/3;
stock_rec 48382, stock 649921 y movimiento 856573 pasan del detalle 46491 al
46492. Las dos existencias son idénticas en todos los demás campos, salvo su ID,
y ambas tienen 3 unidades; se conserva el orden de inserción para la vinculación.
No se agregan ni eliminan existencias. Las dos filas ERP 152013/152014 están sin
enviar y ya corresponden a los detalles correctos; no se modifican.

Después de instalar HH y servicio, revisar y aplicar el script por el procedimiento
autorizado. Refrescar/reabrir la recepción para descartar la caché anterior.
Escanear J100000635 debe mostrar solamente AG00034526041365, con 1 pendiente,
correspondiente al lote 1348. Recibir esa unidad mediante HH lleva OC y recepción
a 7 y los lotes a 4/3. No ejecutar la vista previa ni cambiar banderas por inferencia
de aprobación: la usuaria todavía debe revisar el script.

Los contadores históricos de los otros productos no se corrigieron: están fuera
del alcance solicitado. Los detalles completos de OC se omiten al escanear para
evitar volver a recibirlos aunque sus contadores antiguos de lote sean cero.

## Validación

- HH: `:app:compileDebugJavaWithJavac --offline` y `:app:assembleDebug --offline`,
  ambos satisfactorios. APK: `C:/Users/carol/StudioProjects/TOMHH2025/app/build/outputs/apk/debug/app-debug.apk`.
- Servicio: MSBuild de `WSHHRN/WSHHRN.vbproj`, con dependencias, satisfactorio.
  Debe publicarse el proyecto y su DAL actualizado, no solo el archivo ASMX.
- Pruebas con fragmentos extraídos del código real: orden 4+3/3+4, referencias
  independientes, conservación de OC, recuperación de una unidad, omisión de
  líneas completas y rechazo de variante incorrecta en HH. En servicio: cantidades
  4/3 y 3/4, saldo 1, LP física vacía, sin control talla/color, reintento obsoleto,
  sobrecantidad, temporales repetidos, variante y saldo cambiados, licencia
  incorrecta/mezclada, stock desigual, NaN, PK duplicada y existencias sobrantes.
- Las pruebas del servidor utilizan adaptadores SQL en memoria; no verifican los
  bloqueos ni el rollback contra SQL Server. No se realizó recepción de prueba
  en QA porque produciría cambios de datos no autorizados en esta etapa.
- Script parseado con ScriptDom TSql160: cero errores. Condiciones de recuperación
  contrastadas con SELECT: 11 OK. Sin reservas/picking/pedido específico en las
  existencias afectadas y sin triggers activos en las tablas que cambiaría.
- Java conserva UTF-8 sin BOM y CRLF; VB conserva UTF-8 con BOM y CRLF.
  Revisión de diferencias con `core.whitespace=cr-at-eol` satisfactoria.

## Revisión de impacto y límites

Único llamador de `ActualizarLotesCajaMaster`: GuardarHH_CM. No se cambió la
asignación compartida de identity usada por los demás flujos; se entrega una
identidad temporal independiente desde el productor de la caja master.
El orden de locks por lote es estable por PK. Un reintento que conserve el saldo
antiguo se rechaza aun si la primera respuesta se perdió tras el commit.

La validación de reglas de recepción por línea de OC y los parámetros de LP
permanecen activos. No se deshabilitaron controles de duplicación de LP; una
bodega que bloquee reutilizar una LP física conserva esa regla. No se tocó
`Reference.vb` ni una conexión. La compilación incluyó cambios ajenos preexistentes
en WebService.java y frm_consulta_stock.java; no se modificaron como parte de
esta tarea. El APK debug es un artefacto para pruebas, no una publicación release.

La comprobación integral pendiente es publicar servicio, instalar HH y observar
el reintento autorizado después de la conciliación revisada. Las afirmaciones
de funcionamiento desplegado permanecen draft; el código local y las pruebas
están identificados por hashes en `fix-claims.json`.

## Compatibilidad de bodega posterior

#CKFK20261117_BODEGA_COMPATIBILIDAD_WS: se incorporo en clsBeBodega.java el campo opcional permitir_abrir_recepcion_enviada (false por defecto) y sus accesores. Existia en dev_2028_merge, commit 4d8be015, pero no en dev_2026_estable. APK debug regenerado con exito. El parser XML real sobre la clase compilada acepta true, false y campo ausente. No se desactivo la validacion XML ni se modifico el parametro en base de datos. Ver bodega-compat-claims.json y bodega-xml-test.txt.
