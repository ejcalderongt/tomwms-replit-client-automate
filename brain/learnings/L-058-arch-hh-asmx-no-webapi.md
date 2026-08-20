---
output_type: aprendizaje
audience: agente-brain + Erik + futuros mantenedores
version: V1
status: ratificado
authored_by: agente-brain
authored_at: 2026-07-06T20:10:00-06:00
---

# L-058 - ARCH: la HH usa WSHHRN/TOMHHWS.asmx, no el WebAPI de interfaces

## Regla operativa

Para TOMHH2025 / HH Android, el canal real de comunicacion con backend es:

`C:\Users\carol\source\repos\TOMWMS_BOF\WSHHRN\TOMHHWS.asmx.vb`

No confundir con el proyecto `poc/WmsBodegaApi/WmsBodegaApi.WebApi`, que hoy solo sirve como capa de interfaces y no como transporte real de HH.

## Alcance del cambio

Cuando una validacion o un flujo HH requiera leer inventario ciclico, la regla es:

1. Agregar o ajustar `WebMethod` en `TOMHHWS.asmx.vb`.
2. Consumir ese metodo desde la HH Android.
3. Dejar el WebAPI fuera del path critico de HH, salvo que una tarea explicita indique lo contrario.

## Caso actual

El flujo de inventario ciclico quedo alineado a:

- exact key `ubicacion + gondola`
- rechazo si la gondola existe en otra ubicacion
- aceptacion si es una llave nueva para esa ubicacion
- lista de detalle filtrada solo por la llave exacta

## Motivo de la aclaracion

En esta iteracion se confundo el WebAPI de interfaces con el servicio real que habla con HH. Esa confusion hace que los cambios queden en el proyecto equivocado y rompe la validacion del detalle y del conteo.

## Referencia rapida

- HH Android: `C:\Users\carol\StudioProjects\TOMHH2025`
- WebService real HH: `C:\Users\carol\source\repos\TOMWMS_BOF\WSHHRN`
- POC WebAPI: `C:\Users\carol\Documents\wms-brain\poc\WmsBodegaApi`
