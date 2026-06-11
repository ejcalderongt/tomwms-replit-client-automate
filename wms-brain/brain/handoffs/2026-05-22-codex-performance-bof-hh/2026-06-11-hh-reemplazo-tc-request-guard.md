# 2026-06-11 - HH reemplazo TC persistencia request guard

## Contexto

Incidente en `TOMHH2025` (MAMPA_QA): en reemplazo de picking/verificacion algunos requests salian con `IdProductoTallaColor=0`, provocando persistencia sin talla/color en reemplazo.

## Evidencia de traza (logcat)

- Entrada con TC valido en linea origen:
  - `TC_TRACE [PICK_DAT] loadDet ... IdPTC=1880 T=220 C=001`
- Falla en reemplazo sin seleccion efectiva:
  - `TC_TRACE [LIST_PICK] ws4 srcPTC=1880 tmpPTC=0 selPTC=-1`
- Caso correcto con seleccion:
  - `TC_TRACE [LIST_PICK] Continua_procesando_registro srcPTC=2023 selPTC=2023 tmpPTC=2023`

## Causa raiz

`tmpBeStock_Res` se reinicializa en flujos de reemplazo y, en rutas sin `selitem` util o con stock no seleccionado, `IdProductoTallaColor` no se completa antes del `callMethod`, quedando en `0`.

## Fix aplicado (quirurgico)

- Se agrego guard clause previa a envio WS para completar `tmpBeStock_Res.IdProductoTallaColor` desde linea origen si `Control_Talla_Color=true` y el request viene en `<=0`.
- Se agrego traza fina por etapa (`ws3/ws4/ws2`) para verificar valor final enviado.

## Archivos tocados

- `app/src/main/java/com/dts/tom/Transacciones/Picking/frm_list_prod_reemplazo_picking.java`
  - Metodo nuevo: `asegurarTallaColorEnStockReq(String etapa)`
  - Invocado antes de `execws` casos 3 y 4.
- `app/src/main/java/com/dts/tom/Transacciones/Verificacion/frm_list_prod_reemplazo_verif.java`
  - Metodo nuevo: `asegurarTallaColorEnStockReq(String etapa)`
  - Invocado en `wsExecute` caso 2, antes de `callMethod("Reemplazar_ListPickingUbic_Verificacion", ...)`.

## Validacion tecnica

- Compilacion local:
  - `./gradlew.bat :app:compileDebugJavaWithJavac`
  - Resultado: `BUILD SUCCESSFUL`.

## Validacion funcional sugerida

1. Picking TC con reemplazo sin stock seleccionado: confirmar que `tmpPTC` ya no sale `0`.
2. Picking TC con stock seleccionado: confirmar que conserva `selPTC`.
3. Verificacion TC (con/sin sustituto): confirmar que request WS lleva `IdProductoTallaColor` > 0.
