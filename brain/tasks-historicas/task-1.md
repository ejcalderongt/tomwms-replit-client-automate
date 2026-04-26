---
title: Validar IdStock duplicado en importación Excel
---
# Validar IdStock duplicado en importación Excel

## What & Why
Al importar un archivo Excel de ajuste de stock, el sistema no valida que el mismo `IdStock` no se repita. Esto permite que:
1. Dos filas dentro del mismo Excel con el mismo `IdStock` pasen la validación y se carguen ambas al grid.
2. Un `IdStock` que ya existe en el grid (de una importación previa o agregado manual) se importe nuevamente.

Ambos escenarios generan filas duplicadas que corrompen los ajustes de inventario (el mismo lote se ajusta dos veces).

## Done looks like
- Si el Excel contiene dos o más filas con el mismo `IdStock`, solo la primera se acepta como válida; las siguientes se marcan como error con un mensaje claro indicando la fila donde ya apareció ese `IdStock`.
- Si un `IdStock` del Excel ya existe en el grid principal del ajuste (porque fue agregado manualmente o en una importación previa), esa fila se marca como error indicando que el stock ya está en el detalle del ajuste.
- Los errores aparecen en la grilla de errores del formulario de importación y en el resumen de validación, igual que cualquier otro error de validación existente.

## Out of scope
- Validación de duplicados en el flujo de agregar stock manual (botón Agregar del `frmAjusteStock`) — eso es un cambio separado.
- Cambios en la plantilla Excel o en la estructura de columnas.
- Cambios en el módulo `ImportarAjusteDesdeExcel.vb` (módulo standalone legacy) — solo se modifica el flujo del formulario `frmImportarAjusteExcel.vb` que es el que está activo.

## Steps
1. En `frmImportarAjusteExcel.vb`, dentro del método `Resolver_BD()`, después de resolver el `IdStock` de cada fila desde la base de datos, agregar una validación con un `HashSet(Of Integer)` que acumule los `IdStock` ya procesados. Si el `IdStock` de la fila actual ya está en el HashSet, marcar la fila como inválida con el mensaje "IdStock {X} ya fue incluido en la fila {Y} de este mismo archivo. No se puede ajustar el mismo stock dos veces en una importación."
2. Agregar al constructor de `frmImportarAjusteExcel` un parámetro opcional (o propiedad pública) que reciba la lista de `IdStock` ya existentes en el grid del formulario padre (`frmAjusteStock`). Usar esta lista para validar también en `Resolver_BD()` que el `IdStock` no exista ya en el ajuste actual, con mensaje "IdStock {X} ya existe en el detalle del ajuste actual."
3. En `frmAjusteStock.vb`, en el handler `btnImportarExcel_Click`, antes de instanciar `frmImportarAjusteExcel`, recolectar los `IdStock` actuales del `lBeTransAjusteDet` (y/o `lBeTransAjusteDetBorrador` si es borrador) y pasarlos al formulario de importación para la validación cruzada.

## Relevant files
- `entregables_ajuste/frmImportarAjusteExcel.vb:445-597`
- `entregables_ajuste/frmAjusteStock.vb:5171-5249`