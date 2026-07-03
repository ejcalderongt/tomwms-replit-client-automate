# 2026-07-03 - Ubicacion sugerida preload trace

## Objetivo
Dejar trazado el punto específico del flujo de ubicacion sugerida para que
pueda retomarse en otra PC sin perder el contexto del fix.

## Hallazgos validados
- El SP `dbo.ml_get_ubicacion_sugerida_preload_tramo` quedó funcional en QA
  y ya no falla por scope de CTE.
- El contrato se estabilizó usando `#temp tables` y alias explícito
  `Indice_X`.
- La DAL `clsLnTransUbicSugerida2.Get_Ubicaciones_Sugeridas` consume el SP
  nuevo y ya mapea `Indice_X` de forma defensiva.
- El WS `ml_get_ubicacion_sugerida_v2_JSON` llama a la DAL y expone JSON
  compatible para la HH.
- La HH `frm_cambio_ubicacion_ciega` ya tiene la ruta v2, pero todavía
  conviven algunos llamados legacy mientras se completa el corte.

## Puntos a seguir
- Si la HH sigue mostrando error de ubicacion destino, revisar el tramo de
  validacion posterior a la sugerencia, no el preload del SP.
- Si se quiere eliminar ruido, cortar la coexistencia legacy/v2 en HH una vez
  validado el flujo completo.

## Archivos clave
- `C:\Users\yejc2\source\repos\DBA\20260703_ml_get_ubicacion_sugerida_preload_tramo.sql`
- `C:\Users\yejc2\source\repos\TOMWMS\TOMIMSV4\DAL\Transacciones\Movimiento\clsLnTransUbicSugerida2.vb`
- `C:\Users\yejc2\source\repos\TOMWMS\WSHHRN\TOMHHWS.asmx.vb`
- `C:\Users\yejc2\StudioProjects\TOMHH2025\app\src\main\java\com\dts\tom\Transacciones\CambioUbicacion\frm_cambio_ubicacion_ciega.java`

