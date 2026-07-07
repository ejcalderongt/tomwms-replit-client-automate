---
slug: 2026-07-06-hh-inventario-ciclico-asmx-validaciones
estado: documentado
fecha_aplicado: 2026-07-06
agente: codex-local
---

# Output

## Estado del conocimiento

Quedo clasificado el punto critico del flujo de inventario ciclico HH:

- backend real: `WSHHRN/TOMHHWS.asmx.vb`
- llave de decision: `ubicacion + gondola`
- lista permitida: solo la llave exacta
- fallback estable: SOAP mientras el metodo JSON no este disponible en
  runtime

## Siguiente paso para Erik

Tomar el mapa desde `brain/agents/domain-hh-android.yml` y continuar la
implementacion o depuracion desde la misma linea de trabajo, sin reabrir la
confusion entre WebAPI y WSHHRN.

