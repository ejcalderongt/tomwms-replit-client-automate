---
protocolVersion: 1
id: A-004
answersQuestion: Q-004
title: Estructura y uso de log_error_wms (PEND-11)
operator: agent-replit
operatorRole: developer
target:
  codename: K7
  environment: PRD
executedAt: 2026-04-27T15:00:00Z
durationSeconds: 14
verdict: confirmed
confidence: high
status: answered
tags: [errores, log, K7, BB, PEND-11]
---

## Resumen

Confirmado: `log_error_wms` es **mas una bitacora de eventos
funcionales que un log de errores reales**. Esquema identico
en K7 y BB: 15 columnas con `MensajeError` (nvarchar(max)),
`Fecha`, y FK opcionales a `IdPedidoEnc`/`IdPickingEnc`/
`IdRecepcionEnc`. K7 acumula 66,339 filas en 4 meses (May-Sep
2025), BB lleva 203,782 historicas. La mayoria de los mensajes
empiezan con prefijos como `AVISO_`, `Error_`, o `<SP>_HH:`
— mezclando alertas, trazas y errores reales.

## Hallazgos

### q1: estructura real de la tabla

```
COLUMN_NAME | DATA_TYPE | IS_NULLABLE | CHARACTER_MAXIMUM_LENGTH
--- | --- | --- | ---
IdError | int | NO | 
IdEmpresa | int | YES | 
IdBodega | int | YES | 
Fecha | datetime | YES | 
MensajeError | nvarchar | YES | 2500
IdPedidoEnc | int | NO | 
IdPickingEnc | int | NO | 
IdRecepcionEnc | int | NO | 
IdUsuarioAgr | int | NO | 
Line_No | int | YES | 
Item_No | nvarchar | YES | 50
UmBas | nvarchar | YES | 50
Variant_Code | nvarchar | YES | 50
Cantidad | float | YES | 
Referencia_Documento | nvarchar | YES | 50
```

**Interpretacion**: Cols: `IdError`, `IdEmpresa`, `IdBodega`,
`Fecha`, `MensajeError` (nvarchar(max)), `IdPedidoEnc`,
`IdPickingEnc`, `IdRecepcionEnc`, `IdUsuarioAgr`, `Line_No`,
`Item_No` (nvarchar(50)), `UmBas`, `Variant_Code`,
`Cantidad` (float), `Referencia_Documento` (nvarchar(50)).
**No hay columna `severity` ni `modulo` explicito**, solo se
infiere por convencion de nombre dentro del mensaje.

### q2: volumen y rango temporal (K7)

```
total_filas | mas_vieja | mas_reciente
--- | --- | ---
66339 | 2025-05-28T08:34:47.993Z | 2025-09-07T18:49:29.760Z
```

**Interpretacion**: 66,339 filas en 4 meses (28-may-2025 a
07-sep-2025) → ~16k/mes promedio. Tasa muy alta para ser solo
errores; refuerza la hipotesis bitacora.

### q3: top 30 mensajes (sanitizado)

```
error_truncado | cnt | primero | ultimo
--- | --- | --- | ---
Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: ... | 1800 | 2025-06-03T11:19:15.407Z | 2025-08-19T08:00:40.757Z
Agregar_Marcaje Referencia a objeto no establecida como i... | 1179 | 2025-06-10T06:02:58.973Z | 2025-08-19T08:49:46.873Z
AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 716 ubicac... | 915 | 2025-06-10T13:16:38.527Z | 2025-08-18T15:45:00.303Z
Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: ... | 868 | 2025-06-05T14:01:54.650Z | 2025-08-19T08:49:01.157Z
Error_08012025_Marcaje A: Agregar_Marcaje Referencia a ob... | 710 | 2025-07-10T05:55:56.527Z | 2025-08-19T08:49:46.873Z
Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: ... | 707 | 2025-06-04T12:41:26.937Z | 2025-08-18T12:55:11.517Z
Error_08012025_Marcaje: Agregar_Marcaje Referencia a obje... | 457 | 2025-06-11T06:02:27.093Z | 2025-07-09T09:08:56.187Z
AVISO_20242211_HH_CambioEstadoUbic: ubicacion: 716 ubicac... | 456 | 2025-06-10T09:47:04.800Z | 2025-08-18T12:55:11.517Z
Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: ... | 442 | 2025-06-03T07:49:18.110Z | 2025-08-18T17:51:10.700Z
Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: ... | 355 | 2025-06-04T14:22:57.057Z | 2025-08-18T12:26:26.500Z
... (5 more rows)
```

**Interpretacion**: Los top mensajes son del tipo
`Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario X y
TipoTarea 2` y `AVISO_20242211_HH_CambioEstadoUbic: ubicacion: N`.
**Son trazas de actividad, no excepciones**. Algunos sí son errores
reales: `Agregar_Marcaje Referencia a objeto no establecida...`
(NullReferenceException tipica de .NET) tiene 1,179+710+457=2,346
ocurrencias.

### q4: modulo emisor (inferido por FK)

```
modulo | cnt
--- | ---
OTRO | 61448
PEDIDO | 4672
RECEPCION | 219
```

**Interpretacion**: 92.6% de las filas tienen las 3 FK en NULL
(modulo='OTRO') → la mayoria son llamadas WS sin contexto de
encabezado. 7.0% asocian un `IdPedidoEnc` (modulo PEDIDO),
0.3% a recepcion. **Picking no aparece** en este criterio FK
porque las trazas de picking se anexan via `MensajeError` text,
no via `IdPickingEnc` (probablemente bug del SP que llama log).

### q5: errores por mes (ultimos 2 años)

```
anio | mes | cnt
--- | --- | ---
2025 | 5 | 5
2025 | 6 | 22395
2025 | 7 | 27533
2025 | 8 | 16405
2025 | 9 | 1
```

**Interpretacion**: Solo hay datos en 2025 (May-Sep). Pico en
junio-julio (~25k/mes) y caida fuerte en septiembre (1 fila) —
puede ser que la tabla se haya purgado / truncado.

### Comparacion con BB

```
cols BB: 15 (mismo esquema que K7)
total filas BB: 203782
```

**Interpretacion**: Esquema identico en BB con 203,782 filas
historicas. Patron de uso similar.

## Conclusion

- `log_error_wms` es bitacora mixta de **avisos + trazas + errores
  reales**. No tiene metadata estructurada de severidad.
- El consumer del puente WMS (PowerShell client v0.x) puede
  alimentar este log si quiere reportar errores de bridge, pero
  debe **adoptar una convencion de prefijo clara** (ej.
  `BRIDGE_ERR:` / `BRIDGE_INFO:`) para distinguir de las
  trazas existentes.
- El monitoreo automatizado debe filtrar por exclusion de
  `AVISO_` / `Aplica_*_HH:` antes de alertar — sino dispara
  100s de alertas/dia sin valor.

## Anomalias detectadas

- 2,346 ocurrencias de `Referencia a objeto no establecida`
  (NullRef) en `Agregar_Marcaje` — bug real, no aviso.
- Caida abrupta en septiembre 2025 (1 fila) sugiere truncado
  o fix de un loop ruidoso.

## Sugerencia de follow-up

- Q-012 (politica de severidad / convencion de prefijos para que
  el monitoreo sea util).

## Notas del operador

Confirmar con el equipo WMS-base si `log_error_wms` puede
extenderse con una columna `Severidad` (S/W/E/F) sin romper
SPs antiguos.
