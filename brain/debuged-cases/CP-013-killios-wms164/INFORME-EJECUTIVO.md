# Informe ejecutivo — Caso WMS164 (Killios) y bug sistémico de stock duplicado

**Para**: Erik Calderón (PrograX24) y Carol
**Caso interno**: CP-013 / V-DATAWAY-004
**Fecha del informe**: 2026-04-29
**Base de datos auditada**: `TOMWMS_KILLIOS_PRD_2026` (productiva, lectura READ-ONLY)
**Estado del caso**: Confirmado con datos. Causa raíz acotada a 2 hipótesis dominantes pendientes de confirmación con código.

---

## 1. Qué pasó (en una frase)

El sistema de inventario de Killios tiene **919 filas de stock duplicadas en silencio** que representan **el 18,7% del stock activo total** y comprometen **183.375 unidades de mercadería**. El caso WMS164 que reportó el operario el 23 de abril fue **la punta del iceberg**: el problema arrastra desde **mayo de 2025** (11 meses) y se aceleró fuertemente a partir de **noviembre de 2025**.

---

## 2. Magnitud del daño

### Números duros (medidos en vivo el 29-abr-2026)

| Indicador | Valor |
|---|---|
| Filas de stock activo en Killios | 4.914 |
| Filas duplicadas (basura silenciosa) | **919** |
| Porcentaje del stock contaminado | **18,7%** |
| Combinaciones únicas afectadas | 469 |
| Unidades de mercadería comprometidas | **183.375** |
| Días que lleva el bug activo | **331 días** (28-may-25 → 24-abr-26) |
| Día con más daño registrado | 29-nov-2025 (210 filas duplicadas en un solo día) |
| Combo más degenerado | 13 filas para el mismo producto, lote y ubicación |

### Productos más afectados

Los 5 SKUs con más filas duplicadas y más unidades en juego:

| Código | Producto | Filas duplicadas | Unidades comprometidas |
|---|---|---:|---:|
| WMS229 | Melocotón Killios mitades 12/820 GR | 31 | **7.658** |
| WMS89 | Champiñones Killios 24/400grms | 34 | **6.580** |
| WMS56 | Maíz dulce Miguels 24/425GR | 33 | 3.219 |
| WMS167 | Melocotón Miguels mitades 12/820GR | 30 | 3.165 |
| WMS221 | Mandarina en almíbar Miguels | 42 | 1.615 |

> **Para Carol**: estos son los productos a auditar manualmente primero. Son los que más posibilidad tienen de causar diferencias entre el stock teórico que muestra el sistema y la mercadería real en góndola.

---

## 3. Qué es el bug, en lenguaje simple

Cada fila de stock representa un "lote físico" de mercadería en una ubicación específica con una identidad única: producto + ubicación + estado + lote + matrícula de pallet.

Cuando el operario del HH (handheld) hace una operación que mueve cantidades (cambiar estado, mover de ubicación, picking, despacho, etc.), el sistema debería **buscar la fila existente y sumarle/restarle**. En cambio, en algunos casos el sistema **inserta una segunda fila** con la misma identidad, en vez de consolidar contra la primera.

Resultado visible para el usuario: el stock total parece correcto (la suma da bien), pero el inventario tiene "stocks fantasma". Cuando alguien hace un picking físico, el WMS le manda a tomar de una fila pero quedan UN colgadas en la otra. **Es la receta perfecta para diferencias de inventario que aparecen tarde y nadie sabe explicar de dónde vienen.**

---

## 4. Causa raíz: dónde estamos parados

Tenemos identificadas **dos hipótesis dominantes** que probablemente actúan combinadas:

### Hipótesis A (probabilidad muy alta): el UPDATE que se rompe y cae a INSERT

La base de datos tiene una regla de seguridad que prohíbe que una fila de stock quede en cantidad cero (`Cantidad > 0`). Si el código del HH hace un UPDATE que dejaría la fila en cero o negativo, ese UPDATE **falla con error de SQL Server**. Si el código tiene un "salvavidas" (try/catch) que ante el error inserta una fila nueva en lugar de manejarlo bien, **eso explica exactamente el patrón** que vemos.

**Consecuencia operativa**: el bug se dispara especialmente cuando un movimiento "consume el residual completo" de una fila origen.

### Hipótesis B (probabilidad alta, descubrierta esta semana): el bug NO es del cambio de estado, es de toda la capa común

Hasta esta semana creíamos que el bug era exclusivo del flujo CEST (cambio de estado). **No es así**. La medición del 29-abr cuenta cuántos movimientos de cada tipo tocaron lotes ya duplicados:

| Tipo de tarea | Movimientos sobre lotes duplicados |
|---|---:|
| VERI (verificación) | 15.259 |
| PIK (picking) | 15.257 |
| DESP (despacho) | 14.908 |
| UBIC (ubicación) | 5.291 |
| RECE (recepción) | 1.750 |
| CEST (cambio de estado) | **869** (apenas 1,7%) |

El CEST es minoría. **El bug vive en una función compartida de "mover stock"** que se llama desde muchos handlers del HH. Probablemente sea una clase tipo `StockService` / `LnStock` con un método tipo `moverCantidad()` o `consolidarStock()`.

### Lo que NO es el bug (caminos descartados con datos)

Para evitar que perdamos tiempo en hipótesis muertas:

- **NO es la matrícula de pallet vacía o nula**. Esta era nuestra primera hipótesis. Refutada con datos: cero combinaciones afectadas tienen matrícula nula o vacía. (Hay un patrón menor del 25% donde la matrícula es el literal `'0'`, que sí necesita normalizarse, pero no es la causa principal.)
- **NO es exclusivo del cambio de estado**. CEST es el 1,7% de los movimientos sobre lotes duplicados.
- **NO se generó el día del ticket WMS164** (23-abr). Las filas fundacionales del WMS164 ya estaban duplicadas desde el **9-feb-2026** a las 10:52:55. El operario no causó el bug, simplemente lo destapó.

---

## 5. La pista temporal: algo cambió en noviembre 2025

Distribución mensual de filas duplicadas creadas:

```
2025-05  |  12       2025-11  | 341  *** PICO HISTÓRICO ***
2025-06  |   4       2025-12  | 162
2025-07  |   5       2026-01  | 152
2025-08  |   9       2026-02  | 243
2025-09  |  10       2026-03  | 260
2025-10  |   5       2026-04  | 185 (parcial al 29)
```

De mayo a octubre 2025 el sistema generaba **5 a 12 filas duplicadas por mes** (ruido de fondo, casi imperceptible). En **noviembre 2025 saltó a 341 filas/mes** y se mantuvo alto desde entonces. El día 29 de noviembre solo se generaron 210 filas duplicadas.

**Sospecha fuerte**: hubo un release del HH Android entre octubre y noviembre 2025 que introdujo el bug en el método compartido de stock, o que activó un camino de código que antes no se ejecutaba. Erik puede correlacionar esto buscando en el git log del repo `TOMHH2025` los commits de ese período que tocaron temas de stock, update, insert o consolidación.

---

## 6. Recomendaciones de acción

### Acción inmediata (esta semana)

1. **Auditoría manual física** de los 5 productos top (WMS229, WMS89, WMS56, WMS167, WMS221) para confirmar si el stock que ve el sistema coincide con la mercadería real. Esto da evidencia operativa concreta para Killios.
2. **Erik extrae el código del HH** según el contrato de extracción ya armado (alcance ampliado: handlers CEST, UBIC, PIK, DESP, INVE más la capa común de stock). Con el código a la vista podemos confirmar las hipótesis A y B con cita de archivo y línea.
3. **Erik revisa el git log de `TOMHH2025`** entre octubre y noviembre 2025 buscando commits relacionados con stock.

### Acción de mediano plazo (próximas 2-3 semanas)

4. **Detener el sangrado primero, limpiar después**. Una vez identificado el código bugueado, **el fix de código va antes que el script de consolidación**. Si limpiamos los 919 duplicados sin antes arreglar el código, en pocos meses el bug los vuelve a generar.
5. **Script de consolidación batch** (a aplicar después del fix): unir las filas duplicadas sumando sus cantidades respetando la regla `Cantidad > 0`.
6. **Defensa estructural en BD**: agregar un índice único filtrado sobre `stock` para que la BD misma rechace cualquier intento futuro de duplicar la llave natural. Esto es nuestro caso CP-014 que ya está documentado.

### Acción de largo plazo

7. **Auditar las otras tablas de stock** (`stock_res`, `stock_se`, `stock_transito`, `stock_jornada`) por el mismo patrón. Probablemente sufren el mismo problema porque comparten la convención de "llave natural sin enforce de BD".
8. **Verificar si SAP recibió cantidades correctas o duplicadas** cruzando con `i_nav_transacciones_out`. Si SAP recibió duplicado, hay un problema contable adicional.

---

## 7. Riesgos de no hacer nada

- **El bug sigue activo y crónico**. Las mediciones del 27 al 29 de abril muestran 0 nuevos duplicados en 3 días, pero no hay garantía: el día 24 de abril se generaron varios nuevos. Cualquier operación normal puede dispararlo.
- **Diferencias de inventario crecientes** que aparecen sin explicación obvia, generando desgaste con Killios.
- **Picking inconsistente**: el WMS puede mandar al operario a tomar de una fila vacía (porque la cantidad real está en la fila gemela), generando ineficiencia operativa y reclamos.
- **Posible desfase con SAP** si las cantidades se duplicaron en algún punto del flujo de envío.

---

## 8. Estado de la investigación: 11 waves completadas

Resumen de lo hecho hasta acá:

| Wave | Foco | Resultado |
|---|---|---|
| 13-1 a 13-9 | Localizar producto, posición, movimientos del WMS164. Reconstrucción de los 5 movimientos del 23-abr. | Trace completo. Bug confirmado en BD. |
| 13-10 | Análisis estructural offline del esquema de la BD (snapshot). | Hipótesis H1 (matrícula vacía) y H4 (UPDATE rechazado) elevadas. Caso CP-014 abierto (falta de índice único). |
| **13-11** | **Re-medición en vivo post restablecimiento de firewall**. **5 hallazgos clave que cambian el modelo del caso.** | **H1 refutada en duro. H5 nueva (multi-tipo de tarea). Cronicidad de 11 meses confirmada.** |

### Próximas waves planificadas

- **13-12**: análisis del código del HH Android una vez Erik emita el bundle ampliado.
- **13-13**: confirmación o refutación final de las hipótesis A y B con cita de archivo y línea.
- **13-14**: redacción del fix propuesto + script de consolidación + DDL del índice único.
- **13-15**: auditoría cross-cliente (BYB, CEALSA) para medir si el bug es Killios-only o sistémico.

---

## 9. Garantías de método

Toda esta investigación se hizo cumpliendo estrictamente:

- **Solo lecturas** sobre la BD productiva. Ningún `INSERT`, `UPDATE`, `DELETE`, `ALTER`, `DROP` ni `EXEC sp_*` se ejecutó en producción.
- **Auditabilidad total**: cada afirmación numérica de este informe está respaldada por un archivo de salida raw archivado en el repo `wms-brain` en `brain/debuged-cases/CP-013-killios-wms164/outputs/wave-13-11/`.
- **Reproducibilidad**: las queries usadas están archivadas en `brain/debuged-cases/CP-013-killios-wms164/queries/wave_13_11_batch.py` y se pueden re-correr en cualquier momento.
- **Trazabilidad de hipótesis**: la bitácora del caso (`brain/debuged-cases/CP-013.md`) registra cada hipótesis con su probabilidad pre y post wave, qué se midió para confirmarla o refutarla, y qué quedó pendiente.

---

## 10. Qué te pedimos a vos, Carol

Con esta evidencia podés:

1. Comunicar a Killios que el caso WMS164 no es un incidente aislado del 23 de abril, es un patrón sistémico de 11 meses que ya estamos rastreando con datos.
2. Coordinar la auditoría física de los 5 productos top (WMS229, WMS89, WMS56, WMS167, WMS221) para tener evidencia operativa que respalde el caso.
3. Estimar con Killios el impacto comercial de los 183.375 UN potencialmente comprometidas (sabiendo que es un techo, no un piso).
4. Definir junto con Erik la ventana operativa para aplicar el fix + script de consolidación cuando esté listo (probablemente fin de semana o ventana nocturna, porque va a tocar miles de filas).

---

**Contacto técnico para profundizar**: Erik Calderón. La bitácora completa con los 11 waves está en el repo `wms-brain` (rama `wms-brain` del repo `tomwms-replit-client-automate`).

**Reporte técnico detallado del wave 13-11** (para revisión de un ingeniero de datos): `brain/debuged-cases/CP-013-killios-wms164/REPORTE-wave-13-11.md`.
