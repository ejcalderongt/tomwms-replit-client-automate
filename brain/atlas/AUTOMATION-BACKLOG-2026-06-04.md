# Automation Backlog (Safe Outsourcing)
Fecha: 2026-06-04
Scope: Brain Atlas + Handoffs + Agents

## Objetivo
Tercerizar/automatizar tareas repetitivas sin alterar reglas de negocio ni flujos operativos.

## Principios de seguridad
- No automatizar cambios de codigo productivo sin revisión humana.
- Automatizar primero discovery, validacion de metadata y documentacion.
- Todo automatismo debe correr en modo "read/validate/report" por defecto.

## Candidatos de automatizacion (prioridad)
1. Lint de indices Brain (P0)
- Tipo: local script
- Riesgo: bajo
- Resultado: detectar rutas rotas, metadata faltante y handoffs stale.

2. Sincronizacion de inventario de handoffs (P0)
- Tipo: local script
- Riesgo: bajo
- Resultado: comparar carpetas `brain/handoffs/*` vs `brain/handoffs/_index.yml`.

3. Scoring de frescura por dominio (P1)
- Tipo: reporte semanal
- Riesgo: bajo
- Resultado: lista de domains/handoffs a revalidar por antigüedad.

4. Generador de plantilla de nuevo handoff (P1)
- Tipo: script helper
- Riesgo: bajo
- Resultado: crea carpeta + plantilla + alta preliminar en `_index.yml`.

5. CI de validacion documental (P1)
- Tipo: pipeline opcional
- Riesgo: medio-bajo
- Resultado: gate documental antes de merge para `brain/**`.

6. Publicacion a Brain API (P2)
- Tipo: job manual asistido
- Riesgo: medio
- Resultado: ejecutar `/repos/sync` y `/index/*` con token solo cuando Erik lo confirme.

## Qué NO tercerizar por ahora
- Decisiones de negocio de reserva/picking/recepcion.
- Refactors en WinForms legacy basados solo en heuristicas automáticas.
- Cambios SQL productivos.

## SLA recomendado
- Lint indices: en cada PR o cierre de incidente reusable.
- Revisión de staleness: semanal.
- Revisión de atlas/router: quincenal.

## Evidencia esperada por automatismo
- Archivo validado
- Timestamp
- Errores/warnings
- Acción sugerida

## Roadmap corto (2 semanas)
Semana 1:
- Lint de indices (implementado)
- Checklist de sincronizacion handoffs

Semana 2:
- Helper de alta de handoff
- Score de frescura y reporte resumen
