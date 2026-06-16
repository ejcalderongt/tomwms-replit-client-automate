# Handoff Operativo - MAMPA Interface Learning

Fecha: 2026-06-15
Branch base: `dev_2028_merge`
Capas: `BOF/VB legacy`, `WSHHRN/TOMHHWS`, `DB SQL`

## Objetivo

Consolidar el aprendizaje reutilizable del flujo MAMPA relacionado con:

- Ajustes con Talla/Color.
- Contrato de ingreso en la interface.
- Evitar que el borrador herede datos de destino antes de confirmar.
- Hacer que el router del brain encuentre este caso por `MAMPA` o `SAPSYNCMAMPA`.

## Lo que se observo

1. En ajuste MAMPA, la UI debe leer el producto talla/color con el dataset tipado y no depender de etiquetas visuales.
2. El contrato estable es `IdTalla` + `IdColor`; la pantalla puede transformar eso a lo que necesite mostrar, pero no debe inferirlo desde campos ambiguos.
3. El borrador del ajuste no debe copiar la talla/color destino antes de la confirmacion.
4. En la interface de ingreso, el punto correcto para validar el contrato no debe ocultar el error real si la request viene mal formada.

## Regla reutilizable

### A) Talla/Color en ajuste

- Tomar el origen desde `Get_Single_Dt_By_IdProductoTallaColor`.
- Usar `IdTalla` e `IdColor` como contrato de dato.
- No mezclar origen y destino en el borrador.

### B) Contrato de ingreso

- Si la capa superior ya construye la request, no repetir blindajes que escondan el fallo de origen.
- La validacion debe conservar el error real y no convertirlo en un mensaje opaco.

### C) Log de ejecucion MAMPA / TRANSAC_WMS

- Mantener `i_nav_ejecucion_det_error.error` como mensaje tecnico completo para diagnostico/Brain, por ejemplo `ORIGEN=...;SENTIDO=...;PROCESO=...;MSG=...`.
- Guardar la lectura operativa en `human_error`: documento, referencia, docentry, etapa, causa, producto, linea y problema cuando el contexto este disponible.
- Al registrar errores por documento, completar `no_linea`, `codigo_producto`, `umbas` y `codigo_presentacion` desde el detalle del documento si existe; no dejar esos campos vacios cuando el documento trae lineas.
- En el reporte, `Proceso` puede completarse desde el contexto tecnico cuando `idnavconfigdet` cae al fallback y no resuelve `i_nav_ent`, pero sin reemplazar ni fragmentar el campo `error`.

## Archivos de referencia

- `TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb`
- `TOMIMSV4/DAL/Mantenimientos/Ajustes/clsLnTrans_ajuste_enc_Partial.vb`
- `SAPSYNCMAMPA/Clases Interface Sync/Transacciones_WMS/clsSyncTransacWMS.vb`
- `TOMIMSV4/DAL/Interface/Bitacora/Encabezado/clsLnI_nav_ejecucion_enc_Partial.vb`
- `WSHHRN/TOMHHWS.asmx.vb`
- `TOMIMSV4/DAL/Transacciones/Recepcion/clsLnTrans_re_enc_Partial.vb`

## Validacion minima

1. Abrir ajuste MAMPA con producto control T/C.
2. Confirmar que el borrador conserva solo el origen hasta confirmar.
3. Probar ingreso con request valida y request invalida.
4. Verificar que el router del brain cargue este handoff cuando aparezcan `MAMPA` o `SAPSYNCMAMPA`.

## Guardrails

- No tocar `Reference.vb`.
- Mantener VB con UTF-8 BOM.
- No mezclar HH y BOF en el mismo cambio.
