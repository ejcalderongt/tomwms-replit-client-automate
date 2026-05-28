---
tipo: other
---
# Verificaciones obligatorias

## Cobertura
- [ ] Lista de entidades stock auditadas explicita en RESULT.md (ej:
      `vBeStockRecPallet, vBeStockRecDet, vBeStockPicking, vBeStockAjuste, ...`)
- [ ] Todos los `.java` dentro de `Transacciones/` y `Datos/` fueron escaneados.
- [ ] Conteo total de hits + breakdown OK / SOSPECHOSO / DUDOSO.

## Reporte
- [ ] Tabla de SOSPECHOSOS con: archivo, linea, snippet, razonamiento.
- [ ] Tabla de DUDOSOS con: archivo, linea, snippet, que falta saber.
- [ ] Sugerencia de proximos handoffs por archivo sospechoso (no abrir).

## Disciplina
- [ ] CERO modificaciones a codigo de TOMHH2025.
- [ ] CERO commits.
- [ ] LEARNINGS.md llenado (o "# Sin learnings" si no hubo).

## RESULT.md
- [ ] Frontmatter con estado=aplicado, fecha, agente.
- [ ] Push a wms-brain con commit `[handoff 2026-05-20-audit-hh-cantidad-umbas-pattern] result: auditoria completada`.
