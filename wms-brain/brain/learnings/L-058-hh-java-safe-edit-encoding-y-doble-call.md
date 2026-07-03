# L-058 HH Java safe edit: encoding, BOM y blindaje de flujo

Fecha: 2026-07-02
Dominio: HH Android / TOMHH2025
Origen: LC001 cambio de ubicacion por producto sin licencia
Tags: [hh, java, encoding, utf8, bom, mojibake, logcat, doble_enter, procesando, cambio_ubicacion]

## Aprendizaje

En archivos Java de TOMHH2025, especialmente `frm_cambio_ubicacion_ciega.java`, el riesgo principal al editar no es la ausencia de BOM sino la mezcla de texto legado/mojibake, comentarios en espanol, line endings mixtos y patches que hacen match sobre bytes fragiles.

Para Java/Android HH, la regla operacional debe ser:

- Preferir UTF-8 sin BOM.
- No agregar BOM como normalizacion automatica.
- Preservar EOL existente salvo que el cambio sea explicitamente de normalizacion.
- Usar `apply_patch` con contexto ASCII estable cuando existan comentarios mojibakeados.
- Inspeccionar encoding/EOL antes de cambios grandes.
- Validar con `:app:compileDebugJavaWithJavac` despues de cambios Java.

La regla global historica "codigo siempre en UTF-8 con BOM" no debe aplicarse mecanicamente a Java HH. Para HH, el estandar practico es preservar UTF-8 sin BOM y evitar rewrites masivos.

## Skill federada

Se crea y versiona la skill:

- Runtime local: `C:\Users\carol\.codex\skills\tomwms-java-safe-edit`
- Backup/versionado brain: `brain/skills/tomwms-java-safe-edit`

La skill incluye:

- `Inspect-JavaEncoding.ps1`: inspeccion de BOM, CRLF/LF, non-ASCII y senales de mojibake.
- `Test-JavaEdit.ps1`: validacion post-edicion y compilacion HH opcional.

## Principio de flujo HH

En pantallas HH con WS asincrono, no liberar guards de proceso por temporizadores fijos. Un `postDelayed(... procesando=false, 1000)` puede permitir doble Enter/doble procesamiento si el WS tarda mas de un segundo.

Patron recomendado:

- Activar `procesando=true` solo al iniciar un flujo real.
- Rechazar Enter/click si `procesando=true`.
- Liberar `procesando=false` en callback final, error controlado o rama que devuelve control al usuario.
- Registrar bloqueos con traza fina (`blocked_procesando`) para Logcat.

## Caso LC001

En cambio de ubicacion ciega por producto, el flujo puede encadenar validaciones remotas (`cb=12`, `cb=28`, `cb=27`, `cb=19`, `cb=20`, aplicacion final). Para diagnosticar feeling de UI, registrar `elapsedMs` por callback y evitar que el progress se cierre antes del callback real.

## Pendientes

- Reindexar embeddings/vector index para que este learning y la skill sean recuperables por busqueda semantica.
- Revisar si otros flujos HH usan temporizadores para liberar guards de proceso.
