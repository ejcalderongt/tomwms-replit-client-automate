---
id: EVIDENCIAS-CRONICIDAD
tipo: cp-open
estado: vigente
titulo: Evidencias del hallazgo de cronicidad — CP-013 / V-DATAWAY-004
tags: [cp-open]
---

# Evidencias del hallazgo de cronicidad — CP-013 / V-DATAWAY-004

**Para**: Erik Calderón y Carol
**Propósito**: documentar exactamente cómo se determinó qué filas están duplicadas, qué queries se corrieron, y entregar los datos crudos en CSV para que cualquier persona los pueda abrir en Excel y verificar fila por fila.
**Fecha de la corrida**: 2026-04-29 22:51 UTC
**Base de datos**: `TOMWMS_KILLIOS_PRD_2026` (productiva, lectura READ-ONLY)
**Modo**: 5 SELECT puros, ningún `INSERT`, `UPDATE`, `DELETE`, `ALTER`, `DROP` ni `EXEC sp_*`.

---

## 1. Definición operativa de "duplicado"

Una fila de stock representa, físicamente, **una pila de mercadería identificable** dentro de la bodega. El WMS la identifica por la combinación de 5 campos que llamamos **llave natural**:

```
llave_natural = (IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)
```

| Campo | Qué representa físicamente |
|---|---|
| `IdProductoBodega` | El SKU concreto en una bodega concreta |
| `IdUbicacion` | El bin físico (rack, posición) donde está la mercadería |
| `IdProductoEstado` | El estado lógico (BUEN ESTADO, DESTRUIR, FUGA DE TAPA, etc.) |
| `Lote` | El número de lote del fabricante |
| `lic_plate` | La matrícula del pallet (license plate) |

**La regla del negocio dice**: si dos pilas comparten estos 5 valores, **son la misma pila física** y deberían estar representadas por **una sola fila** en la tabla `stock`. Cualquier movimiento (mover de ubicación, cambiar estado, picking, etc.) debería buscar la fila existente y sumarle/restarle, no crear una segunda.

**Sin embargo**, la base de datos **no impone** esta regla con un índice único. Por eso es posible que existan dos o más filas con la misma llave natural. Cuando eso pasa, decimos que la llave natural está **duplicada**, y las filas extras son **redundantes**.

### Por qué la BD permite la duplicación

La tabla `stock` tiene 14 índices `NONCLUSTERED`, ninguno es único excepto la primary key (`PK_stock` sobre `IdStock`). El query que lo confirma:

```sql
SELECT name, type_desc, is_unique, is_primary_key, is_unique_constraint
FROM sys.indexes
WHERE object_id = OBJECT_ID('dbo.stock')
ORDER BY name;
```

Resultado: 0 unique indexes salvo la PK. Esto está documentado en detalle en `dataway-analysis/04-ecuacion-de-balance/anti-patron-stock-sin-unique-index.md` (caso CP-014 / V-DATAWAY-005).

### Filtro común: solo stock activo

Todas las queries de evidencia usan `WHERE Cantidad > 0`. La razón es que el WMS conserva filas históricas con `Cantidad = 0` que representan stocks ya consumidos/movidos. Esas no son operativamente relevantes — solo nos importan las que tienen mercadería viva.

---

## 2. Las 3 formas de contar el problema

Es importante distinguir tres métricas porque parecen lo mismo pero no lo son:

| Métrica | Qué cuenta | Valor medido (29-abr-2026) |
|---|---|---:|
| **Combos duplicados** | Llaves naturales únicas que aparecen más de una vez | **469** |
| **Filas en combos duplicados** | Total de filas individuales que tienen al menos una "gemela" | **1.388** |
| **Filas redundantes** | Lo que sobraría si consolidáramos todo (1.388 - 469) | **919** |

**Cómo leerlo**: hay 469 "pilas físicas" que el WMS está representando con 1.388 filas. Si el sistema funcionara bien, esas 469 pilas estarían en 469 filas. Sobran 919 filas que son la consecuencia visible del bug.

> En el informe ejecutivo dije "919 filas duplicadas" como simplificación. La cifra técnicamente correcta es **919 filas redundantes** sobre **1.388 filas totales involucradas en duplicación**. Ambas formas son válidas dependiendo del ángulo: 919 es lo que tendríamos que borrar para limpiar; 1.388 es lo que tendríamos que tocar (sumar/consolidar).

---

## 3. Las 5 queries usadas — explicadas paso a paso

Las 5 queries están archivadas en el repo en:
`brain/debuged-cases/CP-013-killios-wms164/queries/wave_13_12_evidencias_cronicidad.py`

Los outputs en CSV están en:
`brain/debuged-cases/CP-013-killios-wms164/outputs/wave-13-12/E0X_*.csv`

### Query E01 — Los 469 combos duplicados (resumen por combo)

Esta es la query base. Usa `EXISTS` para encontrar filas que tienen al menos una gemela, y agrupa por la llave natural para resumir.

```sql
SELECT
    s.IdProductoBodega,
    pb.IdProducto,
    p.codigo  AS producto_codigo,
    p.nombre  AS producto_nombre,
    s.IdUbicacion,
    u.descripcion AS ubicacion_codigo,
    s.IdProductoEstado,
    pe.nombre AS estado,
    s.Lote,
    s.lic_plate,
    COUNT(*)      AS filas_duplicadas,
    SUM(s.Cantidad) AS un_totales,
    MIN(s.fecha_ingreso) AS primera_fecha,
    MAX(s.fecha_ingreso) AS ultima_fecha
FROM stock s
LEFT JOIN producto_bodega pb ON pb.IdProductoBodega = s.IdProductoBodega
LEFT JOIN producto         p ON p.IdProducto         = pb.IdProducto
OUTER APPLY (SELECT TOP 1 descripcion FROM bodega_ubicacion bu
             WHERE bu.IdUbicacion = s.IdUbicacion) u
LEFT JOIN producto_estado pe ON pe.IdEstado = s.IdProductoEstado
WHERE s.Cantidad > 0
  AND EXISTS (
      SELECT 1 FROM stock s2
      WHERE s2.IdProductoBodega = s.IdProductoBodega
        AND s2.IdUbicacion      = s.IdUbicacion
        AND s2.IdProductoEstado = s.IdProductoEstado
        AND s2.Lote             = s.Lote
        AND ISNULL(s2.lic_plate,'') = ISNULL(s.lic_plate,'')
        AND s2.IdStock         <> s.IdStock
        AND s2.Cantidad > 0
  )
GROUP BY
    s.IdProductoBodega, pb.IdProducto, p.codigo, p.nombre,
    s.IdUbicacion, u.descripcion,
    s.IdProductoEstado, pe.nombre,
    s.Lote, s.lic_plate
ORDER BY filas_duplicadas DESC, un_totales DESC;
```

**Resultado**: 469 filas. Cada fila es una llave natural duplicada con su detalle: producto, ubicación, estado, lote, matrícula, cuántas filas tiene, total de UN sumadas, primera y última fecha de creación.

**CSV**: `outputs/wave-13-12/E01_definicion_duplicado.csv` (470 líneas con cabecera).

**Cómo verificar manualmente**: tomá cualquier fila del CSV. Por ejemplo si la fila top dice `IdProductoBodega=1261, IdUbicacion=371, Lote=2C2601, lic_plate='0', filas_duplicadas=13`, podés correr:

```sql
SELECT IdStock, Cantidad, fecha_ingreso
FROM stock
WHERE IdProductoBodega = 1261
  AND IdUbicacion      = 371
  AND IdProductoEstado = <el que diga el CSV>
  AND Lote             = '2C2601'
  AND ISNULL(lic_plate,'') = '0'
  AND Cantidad > 0
ORDER BY IdStock;
```

Y vas a ver las 13 filas con sus IdStock distintos pero misma llave natural.

### Query E02 — Las 1.388 filas individuales (la evidencia atómica)

Si querés ver una por una las filas que componen los 469 combos:

```sql
SELECT
    s.IdStock,
    s.IdProductoBodega,
    p.codigo  AS producto_codigo,
    p.nombre  AS producto_nombre,
    s.IdUbicacion,
    u.descripcion AS ubicacion_codigo,
    s.IdProductoEstado,
    pe.nombre AS estado,
    s.Lote,
    s.lic_plate,
    s.Cantidad,
    s.fecha_ingreso
FROM stock s
LEFT JOIN producto_bodega pb ON pb.IdProductoBodega = s.IdProductoBodega
LEFT JOIN producto         p ON p.IdProducto         = pb.IdProducto
OUTER APPLY (SELECT TOP 1 descripcion FROM bodega_ubicacion bu
             WHERE bu.IdUbicacion = s.IdUbicacion) u
LEFT JOIN producto_estado pe ON pe.IdEstado = s.IdProductoEstado
WHERE s.Cantidad > 0
  AND EXISTS (
      SELECT 1 FROM stock s2
      WHERE s2.IdProductoBodega = s.IdProductoBodega
        AND s2.IdUbicacion      = s.IdUbicacion
        AND s2.IdProductoEstado = s.IdProductoEstado
        AND s2.Lote             = s.Lote
        AND ISNULL(s2.lic_plate,'') = ISNULL(s.lic_plate,'')
        AND s2.IdStock         <> s.IdStock
        AND s2.Cantidad > 0
  )
ORDER BY s.IdProductoBodega, s.IdUbicacion, s.IdProductoEstado, s.Lote, s.lic_plate, s.IdStock;
```

**Resultado**: 1.388 filas, ordenadas para que las gemelas queden juntas en filas consecutivas.

**CSV**: `outputs/wave-13-12/E02_filas_duplicadas_completas.csv` (1.389 líneas con cabecera).

**Para Carol**: este es el CSV más útil para una auditoría operativa. Si lo abrís en Excel y agrupás por las 5 columnas de llave natural, vas a ver bloques de filas que deberían ser una sola.

### Query E03 — Distribución mensual

Una vez identificadas las 1.388 filas que están en combos duplicados, las agrupamos por mes de creación (`fecha_ingreso`):

```sql
SELECT
    YEAR(s.fecha_ingreso) anio,
    MONTH(s.fecha_ingreso) mes,
    COUNT(*) filas_duplicadas,
    SUM(s.Cantidad) un_totales,
    COUNT(DISTINCT s.IdProductoBodega) productos_distintos
FROM stock s
WHERE s.Cantidad > 0
  AND EXISTS (
      SELECT 1 FROM stock s2
      WHERE s2.IdProductoBodega = s.IdProductoBodega
        AND s2.IdUbicacion      = s.IdUbicacion
        AND s2.IdProductoEstado = s.IdProductoEstado
        AND s2.Lote             = s.Lote
        AND ISNULL(s2.lic_plate,'') = ISNULL(s.lic_plate,'')
        AND s2.IdStock         <> s.IdStock
        AND s2.Cantidad > 0
  )
GROUP BY YEAR(s.fecha_ingreso), MONTH(s.fecha_ingreso)
ORDER BY YEAR(s.fecha_ingreso) DESC, MONTH(s.fecha_ingreso) DESC;
```

**Resultado** (los números frescos del 29-abr-2026):

| Año | Mes | Filas en combos duplicados | UN totales | Productos distintos |
|---:|---:|---:|---:|---:|
| 2026 | 4 | 185 | 41.480 | 43 |
| 2026 | 3 | 260 | 34.951 | 64 |
| 2026 | 2 | 243 | 18.542 | 43 |
| 2026 | 1 | 152 | 18.062 | 35 |
| 2025 | 12 | 162 | 11.204 | 44 |
| **2025** | **11** | **341** | **46.139** | **79** |
| 2025 | 10 | 5 | 1.356 | 2 |
| 2025 | 9 | 10 | 2.988 | 3 |
| 2025 | 8 | 9 | 2.868 | 3 |
| 2025 | 7 | 5 | 1.446 | 2 |
| 2025 | 6 | 4 | 135 | 1 |
| 2025 | 5 | 12 | 4.204 | 5 |

**Verificación de la suma**: 185+260+243+152+162+341+5+10+9+5+4+12 = **1.388** ✓ (coincide con E02)

**Lectura del salto de noviembre**:
- Mayo a octubre 2025: 4 a 12 filas/mes, 1 a 5 productos afectados.
- Noviembre 2025: **341 filas, 79 productos distintos, 46.139 UN**. Salto de **24x** vs el promedio anterior, **16x más productos afectados**.
- Diciembre 2025 en adelante: nunca vuelve a los niveles bajos de mayo-octubre. Se estabiliza entre 152 y 343 filas/mes.

**CSV**: `outputs/wave-13-12/E03_distribucion_mensual.csv` (13 líneas con cabecera).

### Query E04 — Top 60 días con más filas duplicadas

Para detectar si el daño se concentra en eventos puntuales o se reparte uniformemente:

```sql
SELECT TOP 60
    CAST(s.fecha_ingreso AS DATE) dia,
    DATENAME(WEEKDAY, s.fecha_ingreso) dia_semana,
    COUNT(*) filas_duplicadas,
    SUM(s.Cantidad) un_totales,
    COUNT(DISTINCT s.IdProductoBodega) productos_distintos
FROM stock s
WHERE s.Cantidad > 0
  AND EXISTS (...mismo EXISTS de arriba...)
GROUP BY CAST(s.fecha_ingreso AS DATE), DATENAME(WEEKDAY, s.fecha_ingreso)
ORDER BY COUNT(*) DESC;
```

**Top 15 días**:

| Día | Día de la semana | Filas | UN totales |
|---|---|---:|---:|
| **2025-11-29** | **Saturday** | **210** | **29.331** |
| **2025-11-28** | **Friday** | **131** | **16.808** |
| 2025-12-05 | Friday | 29 | 289 |
| 2026-03-04 | Wednesday | 28 | 503 |
| 2026-03-26 | Thursday | 27 | 794 |
| 2026-03-13 | Friday | 27 | 2.985 |
| 2026-01-07 | Wednesday | 26 | 7.287 |
| 2026-02-06 | Friday | 25 | 1.434 |
| 2026-03-18 | Wednesday | 25 | 5.479 |
| 2025-12-26 | Friday | 23 | 705 |
| 2026-04-23 | Thursday | 23 | 7.064 |
| 2026-04-16 | Thursday | 23 | 2.949 |
| 2025-12-18 | Thursday | 22 | 3.129 |
| 2026-03-12 | Thursday | 22 | 1.026 |
| 2026-04-21 | Tuesday | 22 | 1.777 |

**Lectura**: el día 28 y 29 de noviembre 2025 son **enormes outliers**. Ningún otro día del histórico se acerca. El tercer día del ranking (5-dic-2025) tiene 29 filas — **7x menos** que el día pico.

**CSV**: `outputs/wave-13-12/E04_distribucion_diaria_top60.csv` (61 líneas con cabecera).

### Query E05 — Desglose día por día de noviembre 2025 (mes pico)

Para confirmar qué pasó dentro del mes de noviembre:

```sql
SELECT
    CAST(s.fecha_ingreso AS DATE) dia,
    DATENAME(WEEKDAY, s.fecha_ingreso) dia_semana,
    COUNT(*) filas_duplicadas,
    SUM(s.Cantidad) un_totales
FROM stock s
WHERE s.Cantidad > 0
  AND s.fecha_ingreso >= '2025-11-01'
  AND s.fecha_ingreso <  '2025-12-01'
  AND EXISTS (...mismo EXISTS de arriba...)
GROUP BY CAST(s.fecha_ingreso AS DATE), DATENAME(WEEKDAY, s.fecha_ingreso)
ORDER BY CAST(s.fecha_ingreso AS DATE);
```

**Resultado** — esto es lo que descubrimos hoy y es **dramático**:

| Día | Día de la semana | Filas en combos duplicados |
|---|---|---:|
| 2025-11-28 | Friday | 131 |
| 2025-11-29 | Saturday | 210 |
| **TOTAL noviembre 2025** |   | **341** |

**No hubo daño en ningún otro día de noviembre 2025**. Las **341 filas** del mes pico se concentraron en **dos días consecutivos** (viernes 28 y sábado 29). El resto del mes: cero filas duplicadas creadas.

**CSV**: `outputs/wave-13-12/E05_pico_noviembre_2025.csv` (3 líneas con cabecera).

---

## 4. Implicaciones de los hallazgos de E04 + E05

Antes de la wave 13-12 creíamos que noviembre fue "el mes en que cambió el comportamiento del sistema y desde entonces se mantuvo elevado". El detalle día por día **cambia esa interpretación**:

1. **El 28-29 de noviembre 2025 fue un evento puntual**, no un cambio gradual de comportamiento. Un viernes y sábado consecutivos con un volumen de daño 7x mayor que cualquier otro día del histórico.

2. **La elevación posterior** (152-260 filas/mes desde diciembre) es una **nueva línea de base** que se instaló después del evento. No volvió al nivel pre-noviembre.

### Hipótesis derivadas de este patrón

| Hipótesis | Apoyo |
|---|---|
| **Migración masiva de inventario** los días 28-29 de noviembre (carga de stock inicial, conversión de bodega, importación batch desde planilla) | Alta. Calza con el volumen y la concentración temporal. |
| **Job batch nocturno** que se disparó dos noches consecutivas con un dataset que activa el bug | Media. Habría que ver si Killios tiene jobs programados de stock. |
| **Release del HH Android desplegado el viernes 28** y operación intensa el sábado 29 (típico fin de semana de cierre de mes con DESP altos) | Alta. Calza con la teoría de release que cambió el comportamiento. |
| **Fin de mes con cierre contable** que disparó una serie de movimientos masivos (regularizaciones, ajustes) | Media. El 30 de noviembre fue domingo, así que tendría sentido cerrar el viernes 28 y el sábado 29. |

**Acción para Erik**: revisar en el git log de `TOMHH2025` los commits del **27 al 29 de noviembre 2025**. Si hay un release ese viernes que tocó stock, es **probablemente** el commit que introdujo el bug, o que activó un camino de código que antes no se ejecutaba. También vale revisar el calendario operativo de Killios: ¿hubo migración, importación, recepciones gigantes esos dos días?

**Acción para Carol**: preguntar a Killios qué pasó operativamente los días viernes 28 y sábado 29 de noviembre 2025. Cualquier evento "fuera de rutina" (carga inicial, importación, migración, capacitación intensiva) es un candidato.

---

## 5. Garantías de método

- **Read-only**: las 5 queries son `SELECT` puros. Cero modificación a la BD productiva.
- **Reproducibilidad**: el script Python que las ejecutó está archivado en `queries/wave_13_12_evidencias_cronicidad.py`. Podés re-correrlo en cualquier momento (requiere `WMS_DB_USER` y `WMS_KILLIOS_PRD_PASSWORD` en variables de entorno) y vas a obtener los mismos números (o números nuevos si pasaron días).
- **Auditabilidad por terceros**: los 5 CSVs están en el repo. Cualquier persona con SQL Management Studio puede tomar una fila de cualquier CSV y verificar con un `SELECT` directo a la BD que la fila existe y tiene los valores que decimos.
- **Trazabilidad**: este documento referencia las queries por nombre y los CSVs por path exacto. Cero hand-waving.

---

## 6. Archivos relacionados (todos en el repo `wms-brain`)

| Archivo | Contenido |
|---|---|
| `EVIDENCIAS-CRONICIDAD.md` (este archivo) | Explicación + queries + tablas |
| `queries/wave_13_12_evidencias_cronicidad.py` | Script Python con las 5 queries |
| `outputs/wave-13-12/E01_definicion_duplicado.csv` | 469 combos duplicados (resumen) |
| `outputs/wave-13-12/E02_filas_duplicadas_completas.csv` | 1.388 filas individuales (auditoría atómica) |
| `outputs/wave-13-12/E03_distribucion_mensual.csv` | Mes a mes |
| `outputs/wave-13-12/E04_distribucion_diaria_top60.csv` | Top 60 días |
| `outputs/wave-13-12/E05_pico_noviembre_2025.csv` | Día por día de noviembre 2025 |
| `INFORME-EJECUTIVO.md` | Documento ejecutivo del caso (lo que armamos antes) |
| `REPORTE-wave-13-11.md` | Reporte técnico previo |
| `../CP-013.md` | Bitácora viva del caso |
| `../../dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md` | Documento del anti-patrón V-DATAWAY-004 |
| `../../dataway-analysis/04-ecuacion-de-balance/anti-patron-stock-sin-unique-index.md` | Documento del anti-patrón V-DATAWAY-005 (causal-permisivo) |
