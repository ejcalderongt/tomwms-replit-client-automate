# Traza 005 - HH Packing implosion

> Rama analizada: `dev_2028_merge`
> Proyecto: `TOMHH2025`
> Enfoque: dejar visible el camino de implosion en `frm_Packing`
> Relacionado con: [L-060 - HH Packing implosion con trazas finas completas](../learnings/L-060-proc-hh-packing-implosion-trazas-finas.md)

## 0. Proposito

Este mapa deja el recorrido minimo que Erik necesita para depurar la implosion de Packing sin perderse entre logs sueltos.

## 1. Punto de entrada

Ruta principal de UI:

`frm_Packing.implo_onScanSku` -> `execws(6)` -> `processProducto` -> `procesarEscaneoImplosion`

Si el usuario escanea un codigo de barra antes de resolver el producto, entra por:

`execws(15)` -> `processExisteCodigoBarraImplosion` -> `procesarEscaneoImplosion`

## 2. Trazas utiles

El prefijo operativo es `TRACE_IMPLO`.

Las marcas que ahora quedan visibles son:

- `SCAN input=...`
- `SCAN encolado-codigo=...`
- `WS producto-start ...`
- `WS producto-result ...`
- `SCAN resolver-pendiente ...`
- `SCAN fallback-exacto ...`
- `SCAN producto-resuelto ...`
- `PROCESS start ...`
- `PROCESS match ...`
- `PROCESS bloqueado-agotado ...`
- `PROCESS selection-applied=...`
- `BARCODE validate-start ...`
- `BARCODE validate-ok ...`
- `ADJUST ...`
- `RESET ...`

## 3. Decision de diagnostico

La implosion ya no debe analizarse solo por el mensaje final al usuario.
La secuencia correcta para revisar un caso es:

1. confirmar que el scan entro a `implo_onScanSku`
2. confirmar que el WS devolvio producto
3. confirmar si el match vino de la lista cargada o del fallback exacto
4. confirmar si la cantidad estaba disponible
5. confirmar si `adjustMixedSelection` aplico o bloqueo el cambio
6. confirmar que `limpiarEscaneoImplosionPendiente` cerro el estado

## 4. Archivo clave

- `app/src/main/java/com/dts/tom/Transacciones/Packing/frm_Packing.java`

## 5. Verificacion

Se ejecuto compilacion del modulo app:

- `:app:compileDebugJavaWithJavac`

Resultado: ok.
