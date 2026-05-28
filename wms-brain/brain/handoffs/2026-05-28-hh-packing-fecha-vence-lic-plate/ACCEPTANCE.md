---
tipo: other
autores: [replit]
---
# Verificaciones obligatorias

## Técnicas
- [x] `listItems()`: comparación `fecha_vence` normalizada a primeros 10 chars en ambos operandos
- [x] `listItems()`: match por `Lic_plate` OR `No_linea` (null-safe) — cubre ubics pre y post packing
- [x] `creaListaLotes()`: stream aplica mismos filtros normalizados
- [x] Prueba manual en La Cumbre QA, picking 3559, pedido 7623: PENDIENTE: 0 / PROCESADO: 4 confirmado
- [x] Lista `frm_lista_packing_lp` muestra LP de picking (JJ000020918) — no LP de packing

## Commits
- [x] `4eba8be7` TOMHH2025 dev_2028_merge: `#EJCRP fix(packing-hh): creaListaLotes guard neto<=0 + listItems PENDIENTE/PROCESADO independiente #EJC20260528`
- [x] `1fb9f12` TOMHH2025 dev_2028_merge: `#EJCRP fix(packing-hh): normalizar fecha_vence + Lic_plate|No_linea en listItems y creaListaLotes #EJC20260528`

## RESULT
- [x] Aplicado directamente por Replit (este handoff es post-facto, no requería Codex local)
- [x] Validación QA exitosa con capturas de pantalla del dispositivo Android
