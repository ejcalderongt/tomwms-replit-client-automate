---
slug: 2026-07-06-hh-inventario-ciclico-asmx-validaciones
estado: documentado
prioridad: alta
autor: codex-local
fecha_propuesta: 2026-07-06
afecta:
  - repo: TOMHH2025
  - rama: dev_2028_merge
  - cliente: HH inventario ciclico
no_afecta:
  - TOMWMS_BOF (salvo ajuste puntual en WSHHRN)
---

# Objetivo

Dejar clasificado el conocimiento de la validacion de inventario ciclico para
que Erik continue desde su maquina sin volver a confundir WebAPI con el
servicio real de HH.

# Contexto

La HH consume `TOMHHWS.asmx.vb` en `C:\Users\carol\source\repos\TOMWMS_BOF\WSHHRN`.
Para este flujo, la decision correcta es por llave exacta `ubicacion + gondola`.

Durante la iteracion previa aparecieron dos fuentes de error:

1. La validacion se estaba cruzando contra gondolas de otra ubicacion.
2. El metodo JSON nuevo no estaba establemente publicado y genero el error
   `Web Service method name is not valid`.

# Cambios de criterio ya cerrados

- La llave de negocio es `ubicacion + gondola`.
- Si la gondola existe en otra ubicacion, se rechaza para la ubicacion actual.
- Si la combinacion no existe, se permite continuar el flujo.
- No deben listarse conteos de otra ubicacion aunque compartan la misma
  gondola.
- Si el metodo JSON no esta listo en runtime, usar el camino estable de
  `Get_Detalle_Ciclic_By_Gondola` mientras se documenta la brecha de despliegue.

# Archivos de referencia

- `brain/agents/domain-hh-android.yml`
- `brain/learnings/L-058-arch-hh-asmx-no-webapi.md`
- `brain/learnings/L-059-proc-hh-inventario-ciclico-exact-key-fallback-wshhrn.md`

# Riesgos

- Si alguien reintroduce la lectura por WebAPI, la HH vuelve a perder el
  contrato real de backend.
- Si se vuelve a mezclar la lista de otra ubicacion, el conteo mostrado en
  pantalla parecera valido pero el detalle operativo sera incorrecto.

