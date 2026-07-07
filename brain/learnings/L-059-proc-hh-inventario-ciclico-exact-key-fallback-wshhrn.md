---
output_type: aprendizaje
audience: agente-brain + Erik + futuros mantenedores
version: V1
status: ratificado
authored_by: agente-brain
authored_at: 2026-07-06T22:15:00-06:00
---

# L-059 - PROC: inventario ciclico HH por llave exacta ubicacion + gondola

## Regla operativa

En HH Android, la validacion de inventario ciclico debe resolverse por la
llave exacta `ubicacion + gondola`.

## Regla de negocio

- Si la combinacion `ubicacion + gondola` existe, se usa ese detalle.
- Si la gondola existe en otra ubicacion pero no en la ubicacion actual,
  se rechaza.
- Si la combinacion es nueva para esa ubicacion, se permite continuar el
  flujo.
- Solo deben listarse los conteos de la llave exacta, nunca los de otra
  ubicacion con la misma gondola.

## Regla de flujo

La validacion es perimetral. No debe contaminar ni reescribir los conteos
que ya viven en memoria para otra ubicacion. El objetivo es decidir si el
flujo sigue, no mezclar inventarios distintos en la lista visible.

## Regla de arquitectura

HH no se conecta al WebAPI para este caso. El camino real sigue siendo
`C:\Users\carol\source\repos\TOMWMS_BOF\WSHHRN\TOMHHWS.asmx.vb`.

Cuando el servicio JSON no esta publicado o devuelve error de nombre de
metodo invalido, mantener el camino estable de SOAP y dejar la divergencia
documentada antes de migrar la llamada.

## Contexto para Erik

1. Partir de `brain/agents/domain-hh-android.yml`.
2. Leer `brain/handoffs/2026-07-06-hh-inventario-ciclico-asmx-validaciones/`.
3. Consultar `L-058-arch-hh-asmx-no-webapi.md` como regla base.
4. Mantener el criterio de exact key antes de tocar listas, trazas o parsers.

