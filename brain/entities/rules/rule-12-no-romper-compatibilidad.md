---
id: rule-12-no-romper-compatibilidad
type: rule
title: No romper compatibilidad
status: vigente
severity: critical
applies_to: [productor, consumidor, código WMS, agentes]
sources:
  - skill: wms-tomwms §4 #12
  - validated_at: 2026-04-27
---
# Regla 12 — No romper compatibilidad

## Statement

Cambios al WMS (schema BD, contratos SOAP, formatos de bundle, encoding de archivos) deben ser **retrocompatibles** mientras existan consumidores en versiones anteriores. NO romper sin migración explícita.

## Rationale

Realidad operativa:
- HH (TOMHH2025) en campo: cada operario tiene su handheld, no se actualizan todos a la vez. Backend nuevo + HH viejo conviven semanas.
- Multi-cliente: Killios puede estar en versión X mientras Becofarma está en X-1. Ambos comparten servidor de código pero NO se actualizan juntos.
- Bundles: el consumidor puede aplicar bundle N+1 antes que N+2. Cada bundle debe funcionar standalone con la base anterior.

Romper compatibilidad sin plan = downtime productivo + soporte extra + posible pérdida de datos.

## Cómo cumplir

1. **Schema BD**: agregar columnas con default (no NULL sin default, no rename, no drop sin deprecar primero).
2. **WebMethods**: agregar métodos nuevos en vez de cambiar firma de existentes. Si hay que cambiar, mantener el viejo deprecado por 1+ release ciclo.
3. **JSON/XML payloads**: agregar campos opcionales OK; remover/renombrar requiere coordinación con todos los consumidores.
4. **Bundles**: validar contra estado anterior real, no solo contra estado teórico ideal.
5. **Encoding** (regla 5): UTF-8 BOM siempre, ningún consumidor maneja switch de encoding.
6. **Migraciones destructivas** (rename, drop, type change): solo en release coordinada con downtime aceptado por EJC.

## Excepciones

Ninguna automática. Cualquier ruptura de compatibilidad requiere decisión explícita documentada en `entities/decisions/` con plan de migración.

## Cross-refs

- `rules/rule-02-no-mezclar-hh-backend` — corolario release-coordination
- `rules/rule-06-migracion-xml-json-oportunista` — caso especial protocolo HH
- `rules/rule-11-cambios-incrementales` — corolario táctico
- `db-brain://parametrizacion/README` — multi-cliente requiere multi-versión simultánea
