# CP-014 — Index del caso

**Título:** Bug `dañado_picking` sin descuento de stock — TRANSVERSAL multi-cliente
**Severidad:** CRÍTICA
**Fecha de descubrimiento:** 2026-04-30
**Caso padre:** [CP-013-killios-wms164](../CP-013-killios-wms164/)
**Estado:** EN INVESTIGACIÓN — fix encolado en CP-013

## Archivos en este caso

| Archivo                  | Descripción                                                        |
|:-------------------------|:-------------------------------------------------------------------|
| `INDEX.md`               | Este archivo                                                       |
| `REPORTE-MULTI-BD.md`    | Reporte principal: 4 BDs afectadas, cronologías, top, conclusiones |
| `DATOS-COMPARATIVOS.md`  | Tablas comparativas crudas: estructuras, métricas BD por BD        |

## TL;DR

- Bug del software, no de configuración Killios.
- Confirmado en **4 de 7 BDs** comparables (Killios x2, BYB, MERCOPAN).
- **MERCOPAN es el caso más grave** en volumen: 19,598 líneas / 574,155 UM en 29 meses.
- **BECOFARMA, CEALSA, MAMPA NO tienen el bug** porque NO usan la feature `dañado_picking` (cero registros con la flag en 1).
- **97-99 % desde el BOF VB.NET**, 1-21 % desde HH (BYB es el outlier con 21 %).
- **100 % sin AJCANTN asociado** — comportamiento determinístico.
- **Fix técnico:** ya documentado en `CP-013/PLAYBOOK-FIX.md`. Aplica al BOF VB.NET (cambio en código).

## Datos crudos

- `data-seek-strategy/templates/outputs/_estructura-multi-bd.json` — esquemas comparados
- `data-seek-strategy/templates/outputs/_audit-bug-multi-bd.json` — métricas del bug por BD
- `data-seek-strategy/templates/outputs/_datos-cp014.json` — cronologías, top productos, top usuarios

## Scripts reproducibles

- `data-seek-strategy/templates/audit-bug-danado-multi-bd.py` — auditoría parametrizable contra cualquier conjunto de BDs
- `data-seek-strategy/templates/audit-linea-tiempo-producto.py` — programa general de línea de tiempo de un producto

## Próximas acciones encoladas

1. Aislar 5-10 casos HH para ver si el webservice deshace los movimientos (ver `colas-pendientes`).
2. Confirmar con cliente Killios estado actual de MERCOPAN y BYB; reflashear backups si siguen activos.
3. Ejecutar fix BOF en QA, validar con replay de casos del último mes Killios.
