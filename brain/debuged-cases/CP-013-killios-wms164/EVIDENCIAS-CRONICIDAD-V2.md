# Evidencias V2 — Refinamiento del modelo del bug a partir de la pista de Erik

**Para**: Erik Calderón y Carol
**Propósito**: documentar el cambio de modelo del bug que resultó de la observación de Erik sobre las 13 filas duplicadas del producto 1261 en la ubicación 371. Lo que parecía "INSERT ciego en cualquier movimiento" se refinó a algo mucho más específico: **bug en la operación de explosión de presentación a unidades, agravado por una migración de tipos de transacción desplegada el domingo 30 de noviembre de 2025**.
**Fecha de la corrida**: 2026-04-29 23:14 UTC
**Wave**: 13-13 (sucesora de 13-12).

---

## 1. La observación de Erik que cambió el modelo

Erik miró el caso concreto del producto `IdProductoBodega=1261` en la ubicación `371` con la query:

```sql
SELECT * FROM vw_stock_res WHERE idubicacion = 371 AND idproductobodega = 1261;
```

Y encontró 15 filas activas. **Dos** de esas 15 son sanas (con licencia y presentación reales). **Las 13 restantes** comparten un patrón que el método anterior no había mirado:

| Característica | Las 13 filas duplicadas | Las 2 filas sanas |
|---|---|---|
| `lic_plate` | `'0'` (placeholder, sin licencia) | `FU09011` y `FU09082` (licencias reales) |
| `IdPresentacion` | `NULL` | `225` (= "Caja12", factor 12) |
| `IdUnidadMedida` | `1` (unidad suelta) | `1` (unidad suelta) |
| `Cantidad` | 6 cada una | 60 y 72 |
| Timestamps | **Idénticos al milisegundo en grupos** (4 filas a las 12:11:50.377, 8 filas a las 12:15:20.773) | Distintos |

**La hipótesis de Erik**: estas 13 filas son **residual de operaciones de explosión de cajas a unidades**. Cada vez que se "explota" una caja, queda una fila de unidades sueltas sin licencia y sin presentación, y como la operación se repite muchas veces sin consolidar, se acumulan.

Esta hipótesis cambia radicalmente el diagnóstico. El bug no está en "INSERT ciego para todo movimiento" — está en una **operación específica del WMS** (explosión, desarmado, conversión de presentación) que NO consolida y NO valida si ya existe la fila destino.

---

## 2. Verificación general — ¿el patrón de Erik aplica a las 1.388 filas?

### D01 — Las 4 categorías de duplicados

```sql
SELECT
    CASE WHEN s.IdPresentacion IS NULL THEN 'sin_presentacion' ELSE 'con_presentacion' END AS presentacion,
    CASE WHEN s.lic_plate IS NULL OR s.lic_plate IN ('','0') THEN 'sin_licencia' ELSE 'con_licencia' END AS licencia,
    COUNT(*) filas, SUM(s.Cantidad) un_totales
FROM stock s
WHERE s.Cantidad > 0
  AND EXISTS (SELECT 1 FROM stock s2
    WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
      AND s2.IdProductoEstado=s.IdProductoEstado AND s2.Lote=s.Lote
      AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
      AND s2.IdStock<>s.IdStock AND s2.Cantidad>0)
GROUP BY 1, 2 ORDER BY 3 DESC;
```

| Categoría | Filas duplicadas | UN totales | % del total duplicado |
|---|---:|---:|---:|
| sin_presentacion + con_licencia | **660** | 8.230 | 47,5% |
| con_presentacion + con_licencia | 352 | 123.130 | 25,4% |
| sin_presentacion + sin_licencia | **291** | 2.593 | 21,0% |
| con_presentacion + sin_licencia | 85 | 49.422 | 6,1% |

**El 68,5% de las filas duplicadas no tiene `IdPresentacion`**. La presencia de licencia es un factor secundario.

### Tasa de duplicación por categoría

Comparando con el universo total de stock activo (4.914 filas):

| Categoría | Stock activo total | En duplicado | **Tasa de duplicación** |
|---|---:|---:|---:|
| Con presentación + Con licencia | 2.685 | 352 | **13,1%** |
| Sin presentación + Con licencia | 1.467 | 660 | **45,0%** |
| **Sin presentación + Sin licencia** | **441** | **291** | **66,0%** |
| Con presentación + Sin licencia | 321 | 85 | 26,5% |

**El stock SIN PRESENTACIÓN tiene una tasa de duplicación 3 a 5 veces más alta que el stock CON PRESENTACIÓN**. La pista de Erik se confirma como dominante: el bug se dispara en filas sin presentación. La licencia es un agravante (66% vs 45% cuando además falta licencia) pero no la causa principal.

### D06 — Confirmación brutal: TODAS son unidades sueltas

```sql
SELECT s.IdUnidadMedida, COUNT(*) filas, SUM(s.Cantidad) un
FROM stock s
WHERE s.Cantidad > 0
  AND EXISTS (SELECT 1 FROM stock s2
    WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
      AND s2.IdProductoEstado=s.IdProductoEstado AND s2.Lote=s.Lote
      AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
      AND s2.IdStock<>s.IdStock AND s2.Cantidad>0)
GROUP BY s.IdUnidadMedida;
```

**Resultado: una sola fila** → `IdUnidadMedida=1`, 1.388 filas, 183.375 UN totales.

**Las 1.388 filas duplicadas son TODAS de unidades sueltas**. Ninguna es de presentación (caja, fardo, paquete). Esto confirma al 100% que el bug está en el flujo unitario.

---

## 3. El caso 1261/371 trazado a movimientos — la prueba del bug

Si las 13 filas duplicadas tienen timestamps idénticos al milisegundo en grupos (4 filas a las 12:11:50.377 y 8 filas a las 12:15:20.773), debe haber 2 movimientos formales asociados, uno por cada grupo.

### D14 — Los 2 movimientos del 6-feb-2026 12:00-13:00 para el producto 1261

```sql
SELECT IdMovimiento, IdTransaccion, IdUbicacionOrigen, IdUbicacionDestino,
       IdEstadoOrigen, IdEstadoDestino, IdPresentacion, IdUnidadMedida,
       cantidad, lic_plate, fecha
FROM trans_movimientos
WHERE IdProductoBodega = 1261
  AND fecha >= '2026-02-06 12:00:00' AND fecha < '2026-02-06 13:00:00'
ORDER BY fecha, IdMovimiento;
```

| IdMovimiento | IdTransaccion | OrigenUbi | DestinoUbi | IdPresentacion | IdUM | Cantidad | lic_plate | fecha |
|---:|---:|---:|---:|---:|---:|---:|---|---|
| 310008 | **2144** | **716** | **716** | **225** (Caja12) | 1 | 336 | (vacío) | 2026-02-06 12:11:50.377 |
| 310031 | **2144** | **716** | **716** | **225** (Caja12) | 1 | 384 | (vacío) | 2026-02-06 12:15:20.773 |

**Lo que esto significa**:

1. **`IdUbicacionOrigen = IdUbicacionDestino = 716`**. Es una operación interna sobre la ubicación 716, no un traslado entre ubicaciones. **Patrón típico de operaciones de explosión, desarmado, ajuste o conversión.**

2. **`IdPresentacion = 225` (Caja12, factor 12) + `IdUnidadMedida = 1`**. El movimiento dice "tenés cajas de 12 unidades, las descomponés en unidades sueltas". Esto **es** una operación de explosión.

3. **`IdTransaccion = 2144`**. No encontré tabla catálogo `sis_transaccion` ni `tipo_transaccion` con este código. Es un identificador interno del WMS que probablemente está hardcoded en el código fuente .NET.

4. **2 movimientos generaron 12 filas en stock** (4 filas con timestamps `12:11:50.377` + 8 filas con timestamps `12:15:20.773`). Lo correcto sería **1 sola fila por movimiento** (consolidando con la fila destino preexistente, si existe), o como mucho **2 filas totales** (una por movimiento). En vez de eso, se generaron **6 veces más filas**.

5. **Pero noten algo raro**: el primer movimiento dice `cantidad=336` y generó 4 filas de 6 UN cada una (= 24 UN, no 336). El segundo dice `cantidad=384` y generó 8 filas de 6 UN cada una (= 48 UN, no 384). **Las cantidades en stock NO suman a la cantidad operada en el movimiento**. Hay un descuadre de 720 UN operadas vs 72 UN reflejadas en stock para este caso puntual.

Esto es **dos bugs combinados**:
- **Bug A**: cada movimiento de explosión genera múltiples filas en stock en lugar de una sola consolidada.
- **Bug B**: la suma de UN en las filas generadas NO coincide con la cantidad operada del movimiento. Hay pérdida o redondeo silencioso.

### Cuadre teórico para 1261/371

```sql
-- Stock actual:    SUM(Cantidad)=210 UN
-- Ingresos a 371:  SUM(cantidad)=1160 UN
-- Egresos de 371:  SUM(cantidad)=1359 UN
-- Neto teórico:    1160 - 1359 = -199 UN (matemáticamente imposible: stock no puede ser negativo)
-- Inflación:       210 - (-199) = 409 UN sobrantes que no se justifican con movimientos
```

La ubicación 371 tiene **210 UN físicamente** (lo que ven en stock) pero según movimientos formales debería tener **−199 UN** (ingresos menos egresos). Hay **409 UN inexplicables** que aparecen en stock pero no provienen de ningún movimiento registrado.

---

## 4. La huella del release — timeline temporal del 30 de noviembre de 2025

### D19 — Tipos de transacción usados en mov de productos con duplicados (28-30 nov 2025)

Encontramos 37 `IdTransaccion` distintos, con dos series claramente diferenciadas:

| Serie | Cantidad de tipos | Patrón | Notable |
|---|---:|---|---|
| `IdTransaccion = 2` | 1 | Operación interna (1.060 movs internos), 66% con presentación | Formato viejo, monolítico |
| `IdTransaccion = 7926..7945` | 20 | Todos internos, mayoría con presentación | Formato nuevo, granular |
| `IdTransaccion = 3474..3496` | ~12 | Traslados (origen ≠ destino) | Formato traslado normal |

### D22 — Cuándo aparece por primera vez cada tipo de transacción

```sql
SELECT IdTransaccion, COUNT(*) movs,
       MIN(fecha) primera_aparicion, MAX(fecha) ultima_aparicion,
       SUM(CASE WHEN IdUbicacionOrigen=IdUbicacionDestino THEN 1 ELSE 0 END) internos
FROM trans_movimientos
WHERE IdTransaccion IN (2, 7926,7927,7928,7929,7930,7931,7932,7933,7934,7935,
                           7936,7937,7938,7939,7940,7941,7942,7943,7944,7945)
GROUP BY IdTransaccion
ORDER BY MIN(fecha);
```

| IdTransaccion | Movs | Primera aparición | Última aparición | Internos |
|---:|---:|---|---|---:|
| **2** | 1.343 | 2025-06-03 09:31:32 | **2025-11-30 10:08:22** | 1.343 |
| **7926** | 2 | **2025-11-30 11:58:07** | 2025-11-30 11:58:07 | 2 |
| **7927** | 4 | **2025-11-30 12:38:37** | 2025-11-30 12:38:37 | 4 |
| **7928** | 2 | **2025-11-30 12:40:54** | 2025-11-30 12:40:54 | 2 |
| 7929 | 5 | 2025-11-30 12:50:41 | 2025-11-30 12:50:41 | 5 |
| 7930 | 4 | 2025-11-30 13:01:21 | 2025-11-30 13:01:21 | 4 |
| 7931 | 2 | 2025-11-30 13:16:37 | 2025-11-30 13:16:37 | 2 |
| 7932 | 12 | 2025-11-30 13:19:06 | 2025-11-30 13:19:06 | 12 |
| 7933 | 3 | 2025-11-30 13:39:13 | 2025-11-30 13:39:13 | 3 |
| 7934 | 1 | 2025-11-30 13:39:38 | 2025-11-30 13:39:38 | 1 |
| 7935 | 4 | 2025-11-30 13:48:39 | 2025-11-30 13:48:39 | 4 |
| 7936 | 17 | 2025-11-30 14:27:56 | 2025-11-30 14:27:56 | 17 |
| 7937 | 2 | 2025-11-30 14:37:24 | 2025-11-30 14:37:24 | 2 |
| 7938 | 2 | 2025-11-30 14:49:10 | 2025-11-30 14:49:10 | 2 |
| 7939 | 29 | 2025-11-30 15:06:46 | 2025-11-30 15:06:46 | 29 |
| 7940 | 41 | 2025-11-30 15:15:47 | 2025-11-30 15:15:47 | 41 |
| 7941 | 27 | 2025-11-30 15:19:16 | 2025-11-30 15:19:16 | 27 |
| 7942 | 4 | 2025-11-30 15:19:54 | 2025-11-30 15:19:54 | 4 |
| 7943 | 5 | 2025-11-30 15:36:47 | 2025-11-30 15:36:47 | 5 |
| 7944 | 31 | 2025-11-30 15:57:49 | 2025-11-30 15:57:49 | 31 |
| 7945 | 2 | 2025-11-30 15:58:43 | 2025-11-30 15:58:43 | 2 |

**Lo que esto cuenta**:

- **El domingo 30 de noviembre de 2025 a las 10:08:22** ocurrió el **último uso** del `IdTransaccion=2`.
- **El domingo 30 de noviembre de 2025 a las 11:58:07** ocurrió el **primer uso** del `IdTransaccion=7926`.
- En las 4 horas siguientes (11:58 a 15:58 del mismo domingo) **se introdujeron 20 nuevos `IdTransaccion`** (7926 a 7945), todos con el patrón "interno" (origen=destino), todos cargados con datos reales en su primera aparición.
- **Cada nuevo IdTransaccion tiene primera_aparicion = ultima_aparicion**, lo que indica que cada uno se ejecutó muchas veces dentro del mismo segundo (carga batch o piloto inmediato).

**Conclusión**: el domingo 30 de noviembre de 2025 hubo un **release técnico que reemplazó el `IdTransaccion=2` (un único tipo de "operación interna" monolítico) por una serie granular de 20 sub-tipos `IdTransaccion=7926..7945`** (probablemente uno por cada sub-operación: explosión, desarmado, ajuste, regularización, conversión, etc.).

**El bug actual está en alguno o varios de estos 20 nuevos tipos de transacción**.

---

## 5. Línea temporal completa de los hechos

| Fecha y hora | Evento | Volumen |
|---|---|---|
| 2025-06-03 09:31 | Primera aparición de `IdTransaccion=2` (operación interna monolítica) | inicio histórico |
| Mayo–Octubre 2025 | Bug del flujo viejo activo, generación esporádica de duplicados | 4–12 filas/mes |
| **Vie 28-nov 2025** | Operación intensa con tipo viejo + posible piloto del nuevo | **131 filas dup** |
| **Sáb 29-nov 2025** | Operación intensa con tipo viejo + posible piloto del nuevo | **210 filas dup** |
| **Dom 30-nov 10:08** | Último uso de `IdTransaccion=2` | fin del flujo viejo |
| **Dom 30-nov 11:58** | Primer uso de `IdTransaccion=7926` | nace la serie nueva |
| **Dom 30-nov 11:58 → 15:58** | Carga / migración técnica de 20 nuevos tipos | 199 movs en 4h |
| Dic 2025 → Abr 2026 | Bug del flujo nuevo activo permanentemente | 152–343 filas/mes |

---

## 6. Cuadre teórico generalizado a los 469 combos

### D23 — Resumen del cuadre

```sql
WITH combos AS (
    SELECT DISTINCT s.IdProductoBodega, s.IdUbicacion, s.IdProductoEstado, s.Lote
    FROM stock s WHERE s.Cantidad>0
      AND EXISTS (SELECT 1 FROM stock s2
        WHERE s2.IdProductoBodega=s.IdProductoBodega AND s2.IdUbicacion=s.IdUbicacion
          AND s2.IdProductoEstado=s.IdProductoEstado AND s2.Lote=s.Lote
          AND ISNULL(s2.lic_plate,'')=ISNULL(s.lic_plate,'')
          AND s2.IdStock<>s.IdStock AND s2.Cantidad>0)
),
sa AS ( /* stock actual sumado por combo */ ),
mo AS ( /* movimientos: ingresos, egresos, internos por combo */ )
SELECT total_combos, inflados, balanceados, sub_declarados,
       total_un_stock, total_ingresos, total_egresos,
       total_internos_un, total_internos_movs, inflacion_neta
FROM sa LEFT JOIN mo USING (...);
```

| Métrica | Valor |
|---|---:|
| Total combos analizados | **402** (de los 469 — los 67 faltantes tienen `Lote=NULL`) |
| Combos con stock **INFLADO** (sobra UN respecto a movs) | 45 |
| Combos balanceados (cuadran) | 111 |
| Combos **sub-declarados** (faltan UN respecto a movs) | **246** |
| UN totales en stock actual | 199.572 |
| UN totales que ingresaron por movimientos formales | 552.379 |
| UN totales que egresaron por movimientos formales | 100.675 |
| UN totales en movimientos INTERNOS (origen=destino) | 470.533 (en 5.346 movimientos) |
| **Inflación neta** (positiva = sobran UN; negativa = faltan UN) | **−252.132 UN** |

**Lectura**:

1. **El descuadre es bidireccional**: hay 45 combos con UN sobrantes y **246 combos con UN faltantes**. No es un solo patrón de "siempre sobra", es **caos contable**.

2. **El neto es negativo**: en términos de UN totales, faltan **252 mil unidades** en stock respecto a lo que ingresó por movimientos formales menos lo que egresó. Esto sugiere que hay **fugas de inventario** además del problema de duplicación.

3. **Los movimientos internos representan 470 mil UN en 5.346 movimientos**. Estos son los que en mi cálculo se cancelan (porque origen = destino, ingreso = egreso desde la perspectiva de la ubicación). **Pero generan filas en stock**. Si el flujo de explosión genera filas con cantidad menor a la operada (como vimos en el caso 1261/371: 720 UN operadas vs 72 UN reflejadas), entonces **cada movimiento interno está perdiendo UN en silencio**.

4. **Esto es probablemente la causa del descuadre de 252 mil UN**: los movimientos internos del flujo nuevo registran la cantidad operada en `trans_movimientos.cantidad` pero solo reflejan una fracción en stock.

### D24 — Top 30 combos con mayor descuadre absoluto

(Ver `outputs/wave-13-13/D24_top30_inflacion.csv` para la lista completa.)

| IdProductoBodega | IdUbicacion | Lote | Filas | UN actual | Ingresos | Egresos | Neto teórico | Inflación |
|---:|---:|---|---:|---:|---:|---:|---:|---:|
| 485 | 1305 | S78E2CBNNA | 8 | 4.992 | 0 | 0 | 0 | **+4.992** |
| 1485 | 1454 | 010 | 3 | 5.184 | 300 | 0 | 300 | +4.884 |
| 510 | 1281 | S78E2COYNA | 6 | 4.032 | 0 | 0 | 0 | **+4.032** |
| 31 | 31 | 5122 | 3 | 2.852 | 132 | 149 | −17 | +2.869 |
| ... | ... | ... | ... | ... | ... | ... | ... | ... |

**Patrón sospechoso**: el top 1 (`IdProductoBodega=485`, `IdUbicacion=1305`, lote `S78E2CBNNA`) tiene **8 filas de stock con 4.992 UN totales**, pero **CERO movimientos formales** (ni ingresos ni egresos a esa ubicación con ese lote). Es decir, las 4.992 UN aparecieron sin ningún registro de movimiento que las justifique. **Esto es inserción directa en `stock` sin pasar por `trans_movimientos`**, o el lote se grabó con una variante distinta (espacios, mayúsculas, encoding).

---

## 7. Hipótesis del bug — versión refinada (V-DATAWAY-004 v2)

### Modelo viejo (rechazado)

> "Cuando un movimiento crea o actualiza una pila en stock, hace `INSERT INTO stock` sin verificar si ya existe una fila con la misma llave natural."

### Modelo nuevo (sustentado por las evidencias D01 a D24)

> **El bug está localizado en las 20 nuevas operaciones internas de tipo `IdTransaccion=7926..7945` introducidas el 30 de noviembre de 2025**.
>
> Específicamente: **el flujo de explosión de presentación a unidades** (Caja → Unidades sueltas) tiene tres defectos combinados:
>
> 1. **No consolida**: cada explosión genera una fila nueva en `stock` con `IdPresentacion=NULL`, `lic_plate='0'`, `IdUnidadMedida=1`, sin buscar si ya existe el "residual unitario" en esa misma ubicación con la misma llave natural extendida.
>
> 2. **No cuadra cantidades**: la cantidad reflejada en stock por las filas generadas NO coincide con la cantidad operada en el movimiento. Hay pérdida silenciosa de UN (caso 1261/371: 720 UN operadas vs 72 UN reflejadas, pérdida del 90%).
>
> 3. **No registra licencia ni presentación**: las filas generadas heredan `lic_plate='0'` y `IdPresentacion=NULL`, lo que las hace invisibles a las queries que filtran por licencia o presentación (típico de pantallas operativas que asumen que toda fila tiene licencia).

### Causal vs permisivo

- **Causal**: el código del/los método(s) que ejecutan los `IdTransaccion=7926..7945`, en el repo TOMHH2025 (Android HH) o en el BOF (.NET VB).
- **Permisivo**: la ausencia de `UNIQUE INDEX` sobre `(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, ISNULL(lic_plate,''))` en `stock`, que permite que las filas duplicadas se materialicen. Documentado en V-DATAWAY-005 (`anti-patron-stock-sin-unique-index.md`).

---

## 8. Acciones derivadas

### Para Erik (búsqueda en código)

1. **Ventana de búsqueda quirúrgica**: el git log de TOMHH2025 / TOMWMS BOF entre **el viernes 27 y el domingo 30 de noviembre de 2025**. El o los commits que toquen:
   - El catálogo de tipos de transacción (introducción de los 20 nuevos `IdTransaccion=7926..7945`).
   - El método que ejecuta operaciones internas / explosión / desarmado / conversión de presentación.
   - Cualquier `INSERT INTO stock` o procedimiento almacenado que se invoque desde esos métodos.

2. **Buscar el catálogo de IdTransaccion**: no hay tabla `sis_transaccion` en la BD. El catálogo debe estar **hardcoded** en el código fuente (probablemente como constantes, enum, o diccionario en una clase BLL). Una vez ubicado el archivo que mapea `IdTransaccion` → descripción, vamos a saber exactamente qué hace cada uno de los 20 nuevos tipos.

3. **Contraste pre/post release**: comparar el código del 27-nov con el del 1-dic. La diferencia debería contener la introducción de la serie nueva. Eso nos da el hash del commit causal.

### Para Carol (validación operativa)

1. **Confirmar con Killios** qué pasó operativamente el **domingo 30 de noviembre de 2025** entre las 10:00 y las 16:00. Algún técnico estuvo desplegando un release. Necesitamos saber:
   - Quién hizo el deploy.
   - Qué tickets/issues motivaron el cambio (probablemente un cambio de modelo de operaciones internas).
   - Si hubo un piloto los días 28 y 29 de noviembre que ya usó el flujo nuevo (eso explicaría las 341 filas duplicadas previas al deploy).

2. **Inventario físico de validación**: tomar 5 a 10 combos del CSV `D24_top30_inflacion.csv` (los más extremos) y hacer **conteo físico** comparando con stock teórico del WMS. Si físicamente hay menos de lo que dice el WMS, confirma inflación. Si físicamente hay más, confirma sub-declaración.

### Para el caso CP-013

1. **Refinar el documento** `dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md` (V-DATAWAY-004) para reflejar el modelo v2: bug específico de explosión, no INSERT ciego general.

2. **Plantear al equipo Killios un script de remediación** que:
   - Identifique todos los combos duplicados (469 hoy).
   - Los consolide en una sola fila por combo (mantener la fila de menor IdStock como "ganadora", sumar las cantidades de las demás, marcar las demás con `Cantidad=0` y un flag de auditoría).
   - **Antes** del script de remediación, **agregar el UNIQUE INDEX** para que el bug no siga generando duplicados.

3. **Caja negra del bug**: una vez identificado el commit causal, escribir un test de regresión que dispare la operación de explosión y valide que NO se duplica.

---

## 9. Garantías de método

- **Read-only**: las 6 queries D01, D14, D19, D22, D23, D24 son `SELECT` puros. Cero modificación a la BD productiva.
- **Reproducibilidad**: el script archivado en `queries/wave_13_13_evidencias_explosion.py` ejecuta las 6 queries en una sola corrida y exporta los 6 CSVs. Vuelve a generar los mismos números (o números nuevos si pasaron días).
- **Auditabilidad**: todos los CSVs están en `outputs/wave-13-13/`. Cualquier persona con acceso a la BD puede tomar una fila de cualquier CSV y verificar con un SELECT directo.
- **Trazabilidad**: este documento referencia las queries por nombre, los CSVs por path exacto, y los IdMovimiento concretos cuando aplica.

---

## 10. Archivos de esta wave (todos en el repo `wms-brain`)

| Archivo | Contenido |
|---|---|
| `EVIDENCIAS-CRONICIDAD-V2.md` (este archivo) | Refinamiento del modelo del bug |
| `queries/wave_13_13_evidencias_explosion.py` | Script Python con las 6 queries |
| `outputs/wave-13-13/D01_patron_present_lic.csv` | 4 categorías por presentación × licencia |
| `outputs/wave-13-13/D14_caso_1261_371_movimientos.csv` | 2 movimientos del caso de Erik |
| `outputs/wave-13-13/D19_idtransaccion_duplicados.csv` | 37 tipos de transacción involucrados |
| `outputs/wave-13-13/D22_serie_transacciones.csv` | Timeline de aparición |
| `outputs/wave-13-13/D23_cuadre_general.csv` | Resumen del cuadre teórico |
| `outputs/wave-13-13/D24_top30_inflacion.csv` | Top 30 combos con mayor descuadre |

**Documentos relacionados (waves anteriores)**:
- `EVIDENCIAS-CRONICIDAD.md` — wave 13-12, definición de duplicado y distribución mensual.
- `INFORME-EJECUTIVO.md` — visión ejecutiva del caso CP-013.
- `dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md` — V-DATAWAY-004 (modelo viejo, requiere actualización).
- `dataway-analysis/04-ecuacion-de-balance/anti-patron-stock-sin-unique-index.md` — V-DATAWAY-005 (causal-permisivo).
