---
tipo: other
---
# ADDENDUM al BRIEF — 2026-05-20

Aclaraciones de Erik en chat 2026-05-20, posteriores al BRIEF original.
Codex DEBE leer este addendum antes de empezar la auditoria.

## 1. Alcance correcto por familia de tablas

La regla "Cantidad siempre en UMBAS" NO es global. Auditar SOLO los puntos
donde el destino es Familia A:

- `stock`
- `movimientos` (familia `trans_mov_*`, `mov_stock`, etc.)
- `stock_res`

Las tablas Familia B (`trans_picking_ubic` y operativas similares) **NO son
sospechosas**: en ellas la cantidad va en presentacion si existe, y en UMBAS
si no. Si encontras hits ahi, clasificalos como **OK por diseno**, no como
SOSPECHOSO.

## 2. Producto sin presentacion no es bug

Si encontras codigo que asigna `cantidad_visible` directo a Familia A SIN
multiplicar por Factor, NO marcar como SOSPECHOSO si:
- El flujo verifica antes que el producto NO tiene presentacion (objeto
  presentacion = `Nothing`/null), o
- El valor proviene de `Cantidad_UMP` (que ya esta en UMBAS).

En esos casos clasificar como **OK con nota** y dejar el snippet para
referencia.

## 3. CamasPorTarima / CajasPorCama — auditar tambien

Sumar al alcance: identificar como HH y BOF tratan `CamasPorTarima` y
`CajasPorCama` cuando vienen null/0.

Hipotesis a verificar (segun Erik): el backend puede interpretarlos como
**palletizado nuevo definido** y guardarlos, asi que el codigo HH NO debe
defaultear a 1 ni abortar — los pasa tal cual.

Reportar en RESULT.md una seccion aparte:
- Lugares en HH donde se setean defaults para esos campos.
- Lugares en BOF donde se persisten / se leen esos campos.
- Conclusion: ¿se confirma la hipotesis de "palletizado nuevo definido"?

## 4. Expansion de alcance a BOF (VB.NET)

El BRIEF original limita el scan a TOMHH2025. **Sumar BOF (TOMWMS_BOF)** al
alcance, con el mismo criterio:
- Buscar asignaciones a `.Cantidad` de entidades cuyo destino sea Familia A.
- Mismas categorias OK / SOSPECHOSO / DUDOSO.
- Reportar en seccion aparte de RESULT.md (`## Hallazgos BOF`).

Si el scan completo de BOF es muy pesado, priorizar:
- Endpoints WSHHRN que reciben datos de recepcion / picking / ajuste y los
  persisten en stock.
- Procedimientos en DAL / DALCore que tocan `stock`, `movimientos`,
  `stock_res`.

## 5. Categoria adicional

Sumar al clasificador la categoria **OK_BY_DESIGN** (ademas de OK /
SOSPECHOSO / DUDOSO) para casos Familia B y para casos "producto sin
presentacion". Reportar conteo separado.

## 6. Glosario referencia

Ver `wms-brain/brain/code-changes/HH/PATTERNS-UMBAS.md` (version actualizada
en este mismo commit) para definiciones de Familia A/B/C, UMBAS, UMP, Factor.

## Notas

- El BRIEF original sigue siendo valido; este ADDENDUM lo precisa y expande,
  no lo invalida.
- ACCEPTANCE.md original sigue aplicando, con un check adicional implicito:
  reportar seccion BOF y conteo OK_BY_DESIGN.
