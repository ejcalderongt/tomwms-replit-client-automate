---
slug: 2026-05-20-audit-hh-cantidad-umbas-pattern
estado: propuesto
prioridad: alta
autor: replit
fecha_propuesta: 2026-05-20
afecta:
  - repo: TOMHH2025
  - rama: dev_2028_merge
  - cliente: CORE (auditoria general)
no_afecta:
  - TOMWMS_BOF
  - Cualquier codigo (esta es auditoria READ-ONLY)
---

# Objetivo

Auditar TODOS los lugares en TOMHH2025 donde se asigna a `*.Cantidad` de
entidades de la familia stock (`vBeStock*`, `vBeStockRec*`, `vBeStockPicking*`,
etc.), para detectar si el bug corregido en `frm_recepcion_datos.java` (UMBAS
vs Cantidad_Presentacion) esta replicado en otros flujos.

Este handoff es **read-only**. Codex NO debe modificar codigo. Solo reportar.

# Contexto

Origen: handoff `2026-05-20-hh-recepcion-pallet-presentacion-cantidad`.
Se corrigio que `vBeStockRecPallet.Cantidad` debe ir en UMBAS, no en
unidad de presentacion. Ese mismo patron ("asignar Cantidad_Presentacion
directo a un campo de stock") es probablemente replicable en otros
puntos del codigo HH dado que el error es silencioso y nadie lo nota.

Regla canonizada en `wms-brain/brain/code-changes/HH/PATTERNS-UMBAS.md`.

# Cambios a aplicar

NINGUNO. Esta es una auditoria pura. Solo lectura y reporte.

# Que hay que hacer

1. **Buscar** en `app/src/main/java/com/dts/tom/` todos los `.java` que contengan:
   - `.Cantidad =` o `.setCantidad(`
   - Y que el LHS sea una entidad cuyo nombre matchee la regex
     `^v?Be(Stock|StockRec|StockPicking|StockAjuste|StockMov).*`

2. **Por cada hit, clasificar**:
   - **OK**: el RHS ya esta convertido a UMBAS (multiplicacion por Factor visible
     o uso de Cantidad_UMP / Cantidad_UMBAS).
   - **SOSPECHOSO**: el RHS es `Cantidad_Presentacion`, `cantidad_visible`,
     una constante, o un calculo que NO incluye Factor.
   - **DUDOSO**: el RHS no se puede determinar sin contexto (variable
     intermedia, retorno de funcion, etc.).

3. **Para cada SOSPECHOSO y DUDOSO**, capturar:
   - Ruta completa del archivo
   - Linea(s) afectada(s)
   - Snippet de 5 lineas alrededor del hit
   - Razonamiento breve de por que parece sospechoso/dudoso

4. **Escribir RESULT.md** con:
   - Conteo de hits por categoria (OK / SOSPECHOSO / DUDOSO)
   - Tabla con los SOSPECHOSOS (prioritarios)
   - Tabla con los DUDOSOS (requieren revision humana)
   - Lista de archivos completos donde aparecen
   - Sugerencia de proximos handoffs concretos por archivo, si aplica
     (NO los abre Codex; los propone para que Erik o Replit decidan).

# Archivos / objetos tocados

Lectura solamente. Archivos a leer:
- Cualquier `.java` dentro de `app/src/main/java/com/dts/tom/Transacciones/`
- Cualquier `.java` dentro de `app/src/main/java/com/dts/tom/Datos/` que
  manipule entidades `vBeStock*`.

# Reglas vinculantes (recordatorio explicito)

- AUDITORIA READ-ONLY. NO modificar codigo. NO commitear. Si encontras un
  bug que querrias arreglar, anotalo en RESULT.md y dejalo para un handoff
  aparte que abrira Erik/Replit.
- NO mezclar este reporte con cualquier otro cambio.
- Si encontras casos que claramente NO encajan en OK/SOSPECHOSO/DUDOSO,
  anotalos en LEARNINGS.md.
- Si la regex te trae demasiado ruido (variables que se llaman `.cantidad`
  pero no son del modelo stock), filtra y documenta el criterio en
  LEARNINGS.md.

# Riesgos / consideraciones

- Falsos positivos esperables: muchas entidades VB-style en Java tienen
  `.Cantidad` que no es de stock (ej. `BeProductoCantidad`, `BeOrden.Cantidad`).
  Codex debe filtrar por prefijo de tipo, no solo por nombre de campo.
- Falsos negativos posibles: si la entidad se asigna por reflection o via
  HashMap (raro en este codigo pero pasa), no la encontras con grep estatico.
  Asumir cobertura del 80-90%.
- El catalogo definitivo de entidades stock vive en `app/src/main/java/com/dts/tom/Datos/`.
  Empezar leyendo ahi para armar la lista de tipos a buscar.
