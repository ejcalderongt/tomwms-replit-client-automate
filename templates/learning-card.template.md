---
protocolVersion: 1
id: L-XXX
title: <una frase con la conclusion clave>
operator: <slug>
operatorRole: developer
createdAt: <ISO 8601>
target:
  codename: <K7|BB|C9|ID|MH|MC|MP|IN|MS|BF|MM|LC|TODOS>
  environment: <PRD|QAS|DEV>
relatedQuestions: [Q-XXX, Q-YYY]
relatedDocs:
  - <ej. brain/wms-specific-process-flow/interfaces-erp-por-cliente.md>
status: open
priority: <high|medium|low>
tags: [<area, modulo, cliente>]
---

## Que aprendimos

<3-6 lineas. Resumen ejecutivo de la conclusion. Lo que SI sabemos
ahora que antes no sabiamos.>

## Evidencia

- Answer card: A-XXX (ver /learnings/answered/<slug>/<fecha>/A-XXX.md)
- Query especifica que lo confirmo: <id de query y archivo>
- Output relevante (sanitizado):

```
<snippet>
```

## Implicancias

### Para el codigo

- <ej. SAPSYNCKILLIOS necesita logica de redondeo explicita>
- <ej. NavSync deberia cubrir INGRESOS para BB>

### Para la operacion

- <ej. monitoreo del outbox debe incluir alerta por % pendiente x tipo>
- <ej. los 110k INGRESOS pendientes de BB requieren purga o reproceso>

### Para el equipo

- <ej. documentar en SPEC.md sec X la politica de redondeo>
- <ej. agregar suite outbox-health al monitoreo regular>

## Acciones propuestas

- [ ] <accion 1, con responsable sugerido>
- [ ] <accion 2>
- [ ] <accion 3>

## Como se cierra esta learning

Cuando todas las acciones esten resueltas y la conclusion este
consolidada en un doc estable (ej. en `wms-specific-process-flow/`),
mover este archivo a `learnings/closed/` y agregar al final:

> **Cerrada**: <fecha> por <slug>. Consolidado en
> `<path/al/doc/estable.md>`.
