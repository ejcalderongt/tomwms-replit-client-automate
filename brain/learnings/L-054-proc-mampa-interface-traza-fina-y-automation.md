# L-054 PROC MAMPA Interface Traza Fina y Automation

Fecha: 2026-06-15

## Contexto

La interface MAMPA vive en `SAPSYNCMAMPA` y su flujo sensible se concentra en `clsSyncTransacWMS`.
Para cambios rapidos conviene tratarla como un dominio propio y no como un ajuste mas del cíclico.

## Hallazgo

La validacion de unicidad por ajuste debe quedar en la interface MAMPA, validando por
`Referencia` antes de insertar `trans_ajuste_enc`.

Cuando el cambio es de MAMPA, el cíclico no debe absorber la regla por accidente.

## Mejora de trabajo

Se agrego un mapa operativo y una skill especifica para MAMPA con:

1. traza fina de entrada
2. agente dedicado
3. script de barrido rapido de hotspots
4. referencia clara de donde tocar y donde no tocar

## Archivos de apoyo

- `brain/code-deep-flow/traza-003-sapsyncmampa-interface.yml`
- `brain/code-deep-flow/traza-003-sapsyncmampa-interface.md`
- `brain/agents/domain-mampa.yml`
- `brain/skills/wms-mampa-interface/SKILL.md`
- `brain/skills/wms-mampa-interface/scripts/wms-mampa-scan.ps1`

## Regla practica

Antes de editar MAMPA:

1. correr el scan
2. ubicar el metodo exacto
3. tocar solo la interface
4. dejar traza tecnica y nota humana breve

