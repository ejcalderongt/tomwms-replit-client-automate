# Capa 04 / Anti-patrón: INSERT a `stock` sin merge contra llave natural

> **Bug `V-DATAWAY-004` (severidad alta)**: el flujo de CEST (Cambio de Estado) — y posiblemente otros flujos de mutación de stock — inserta una fila nueva en `stock` cuando debería consolidar contra una fila preexistente con la misma llave natural. Resultado: dos o más filas distintas (`IdStock` diferentes) que comparten `(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)`. **Ya impactó 919 filas (18.7%) del stock activo de Killios PRD.**

## TL;DR

| Aspecto | Valor |
|---|---|
| Bug ID | `V-DATAWAY-004` |
| Severidad | Alta |
| Estado | **Confirmado con datos** (CP-013) — análisis estructural completado (Wave 13-10), pendiente bundle HH Android (Wave 13-11) |
| Path inculpado | `CEST` (Cambio de Estado) en flujo **HH Android** (confirmado por FK `FK_trans_movimientos_sis_tipo_tarea_hh`, no BOF VB.NET) — confirmado en M3/M4 del trace WMS164 |
| Anti-patrón estructural asociado | `V-DATAWAY-005` (causal-permisivo): la tabla `stock` no tiene UNIQUE INDEX sobre la llave natural, lo que permite que V-004 deje huella en BD silenciosamente |
| Path bajo sospecha (no confirmados) | `UBIC` (Cambio de Ubicación), `RECE` (Recepción), `AJUS` (Ajuste) |
| Síntoma observable | dos o más filas en `stock` con misma `(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)` y `IdStock` distinto |
| Detección | query simple `GROUP BY` + `HAVING COUNT(*) > 1` (ver query 12 en CP-013) |
| Alcance medido (Killios) | 469 combos / 919 filas / 183.375 UN / 18.7% del stock activo |
| Caso fundacional | CP-013 (Killios ticket WMS164, 23-abr-2026) |
| Auto-confirmable | sí — la query 12 mide el alcance sin necesidad de entrevistar a nadie |

## Caso fundacional: WMS164 (Killios, 23-abr-2026)

5 movimientos del usuario `IdUsuario=31` sobre `IdRecepcion=2179` durante 75 segundos:

```
10:32:18  M1  UBIC  307 -> 308   40 UN  estado=1
10:32:42  M2  UBIC  307 -> 308   95 UN  estado=1
10:33:01  M3  CEST  307 -> 22    40 UN  estado=1->16    -> IdStock 134176
10:33:14  M4  CEST  307 -> 22    30 UN  estado=1->16    -> IdStock 134177  *** BUG ***
10:33:33  M5  UBIC  307 -> 308   65 UN  estado=1
```

M3 y M4 deberían haberse consolidado en una sola fila de stock con cantidad 70. En vez de eso, M4 generó IdStock 134177 nuevo, gemelo natural de 134176.

Ver: `brain/debuged-cases/CP-013-killios-wms164/REPORTE.md` para el trace completo.

## La llave natural de stock

Por convención del WMS (no por constraint en BD), una "fila de stock física" se identifica por:

```
llave_natural_stock = (
    IdProductoBodega,
    IdUbicacion,
    IdProductoEstado,
    Lote,
    lic_plate
)
```

**Atención**: esta llave **no está enforced por una constraint UNIQUE en SQL Server**. La unicidad depende exclusivamente de que el código de aplicación, al insertar un nuevo stock, primero busque si ya existe una fila con la misma llave natural y, en caso afirmativo, **actualice** en lugar de **insertar**.

Cualquier path que mute stock — RECE, UBIC, CEST, AJUS, IMPL (implosión) — debe respetar este contrato. Si **un sólo** path lo viola, aparece el patrón.

## Las 4 hipótesis del bug raíz (ordenadas por probabilidad post Wave 13-10)

### H1: `lic_plate` vacío/NULL rompe el comparador (probabilidad ALTA)

Si el comparador que decide consolidar usa `WHERE lic_plate = @lp` directo, y `@lp` viene NULL (o el cliente envía `''` y el SP lo convierte a NULL al asignar al parámetro), el lookup retorna 0 filas porque en SQL Server `NULL = NULL` evalúa a `UNKNOWN`. El código entra al branch INSERT y el merge nunca ocurre.

**Evidencia (Wave 13-10)**: schema de `dbo.stock` confirma columna 16 `lic_plate nvarchar(50) NULL`. Semántica de SQL Server confirma `NULL = NULL → UNKNOWN`. Sube de "hipótesis posible" a **"hipótesis con respaldo de schema"**.

**Evidencia indirecta del caso**: las 5 filas de `trans_movimientos` del WMS164 tienen `lic_plate=''` en CEST y UBIC.

**Cómo verificar**: leer el método del CEST en HH Android (Wave 13-11) y confirmar si el `WHERE` del lookup hace `lic_plate = @lp` directo (vulnerable) o `ISNULL(lic_plate,'') = ISNULL(@lp,'')` (defensivo).

### H4: UPDATE rechazado por check `Cantidad > 0` → fallback INSERT (probabilidad ALTA, NUEVA en Wave 13-10)

Constraint declarado en BD: `Stock_NonNegative_20200115_EJC : ([Cantidad]>(0))`.

Si el código del CEST hace `UPDATE stock SET Cantidad = Cantidad - @cantidad` sobre el origen y el resultado da 0 o negativo, el constraint **rechaza** el UPDATE con error de SQL Server. Si el código tiene `try/catch` con fallback a INSERT para no romper la operación, eso explica exacto la cronología del WMS164:

```
M3 (CEST 40 UN): UPDATE origen Cantidad = 40 OK   -> INSERT IdStock 134176 con 40   -> consolida OK
M4 (CEST 30 UN): UPDATE origen Cantidad = 0 RECHAZADO -> catch -> INSERT IdStock 134177 con 30 -> NO consolida
```

**Por qué la probabilidad es alta**:
- Calza con los números exactos del WMS164 (M3 deja 40, M4 vacía a 0).
- Calza con el patrón de "el último CEST de un lote es el que falla" (consume el residual completo).
- Es consistente con que el bug afecte 18.7% del stock activo: cualquier CEST que vacía el origen al 100% activa el path bugueado.

**H4 es independiente de H1** — pueden coexistir o ser causa única alternativa.

**Cómo verificar**: leer el método del CEST en HH Android (Wave 13-11) y buscar `try/catch` alrededor del UPDATE de stock origen + branch INSERT en el catch.

**Forma defensiva** (que el código probablemente NO tiene):
```vb
If cantidad_resultante = 0 Then
    DELETE stock WHERE IdStock = @stock_origen
Else
    UPDATE stock SET Cantidad = cantidad_resultante WHERE IdStock = @stock_origen
End If
```

### H2: concurrencia inter-segundo (probabilidad MEDIA)

M3 y M4 están a 13 segundos. Si el flujo del CEST permite que dos hilos disparen la mutación concurrentemente (por ej. la HH dispara un job y mientras tanto el operador escanea otra etiqueta), ambos hilos hacen `SELECT count(*) FROM stock WHERE llave_natural=...` antes de que el primero haya commiteado. Ambos ven `count = 0` y deciden hacer `INSERT`. Race condition clásica de upsert sin lock.

**Cómo verificar**: ver si el código del CEST tiene `SELECT ... WITH (UPDLOCK, HOLDLOCK)` o lock equivalente en el comparador.

### H3: CEST por lote partido HH permite dos eventos separados (probabilidad MEDIA)

La HH puede haber permitido al operador confirmar el CEST en dos eventos separados (40 y luego 30) sobre la misma posición destino. El flujo interpreta "dos eventos = dos filas" en lugar de "un destino = un merge".

**Cómo verificar**: ver si el código del CEST tiene branch tipo `IF cant_total_lote = cant_a_mover THEN UPDATE ELSE INSERT`.

## Hipótesis refutadas (Wave 13-10)

### Refutada: `SP_STOCK_JORNADA_DESFASE` es el SP del bug

Sospechoso por nombre (eco del marker `#EJCAJUSTEDESFASE` de CP-007/008). Inspección de `db-brain/sps/SP_STOCK_JORNADA_DESFASE.md`:

- Autor: Carolina Fuentes, 17-oct-2022.
- Naturaleza: SP de **detección, no de mutación**. Detecta huecos en `stock_jornada` por lic_plate-fecha consecutiva (DROP+INSERT a tablas temporales `stock_jornada_consecutivo`, `stock_jornada_fecha_consecutiva`, `stock_jornada_desfase`).
- **No toca `stock` principal**. Independiente de V-DATAWAY-004.

**Refutado** — no perder tiempo en próximas waves.

### Refutada: `stock_res_ped_164` está relacionado con WMS164

Sospechoso por número (eco de WMS164). Inspección de `db-brain/tables/stock_res_ped_164.md`:

- 38 filas, schema modify_date 2022-01-13.
- Sin FKs (entrantes ni salientes). Sin referencias desde SPs/vistas/funciones.
- Es snapshot viejo del pedido número interno 164 (debug/respaldo de 2022).

**Refutado** — el "164" coincidió por casualidad.

## Por qué este anti-patrón es sistémico

### 1. La llave natural no está enforced en BD

Si hubiera un `CREATE UNIQUE INDEX UX_stock_llave_natural ON stock(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)`, la BD rechazaría el INSERT bugueado. **No existe**. El contrato vive sólo en el código.

### 2. Hay múltiples paths que mutan stock

Cada path (RECE/UBIC/CEST/AJUS/IMPL) tiene su propia implementación del "verificar y consolidar". Si **un** path lo hace mal, el patrón aparece. Pero como el problema no se detecta en el momento (las cantidades cuadran, los reportes agregados muestran totales correctos), pasa desapercibido y se acumula.

### 3. Las queries de operación enmascaran el problema

Las queries que el negocio mira (existencia disponible por SKU, picking sugerido, reportes consolidados) usan `SUM` por llave natural. **Devuelven la suma correcta**. El problema sólo aparece cuando una query itera por `IdStock` o cuando la HH al pickear agarra el de cantidad menor primero, fragmentando aún más.

### 4. Auto-perpetuante

Una vez que el patrón aparece, cada operación posterior sobre cualquiera de las filas gemelas crea más oportunidades de fragmentación. CEST sobre 134176 puede generar 134179, CEST sobre 134177 puede generar 134180, y así. La cantidad de filas redundantes crece con cada operación.

## Diferencia clave vs V-DATAWAY-001 (ModoDepuracion)

| Aspecto | V-DATAWAY-001 (ModoDepuracion) | V-DATAWAY-004 (insert-sin-merge) |
|---|---|---|
| Tabla afectada | `trans_movimientos` (cardex) | `stock` (foto actual) |
| Tipo de daño | mutación destructiva del histórico | duplicación silenciosa de filas |
| Marker en BD | `Serie = "#EJCAJUSTEDESFASE"` | ninguno (sin marker) |
| Activación | manual (Ctrl+D + ModoDepuracion=True) | automática en flujo normal |
| Tipo de operador necesario | usuario consciente del modo debug | cualquier operador en uso normal |
| Auto-confirmable | sí (count Serie marker) | sí (GROUP BY HAVING) |
| Confirmado en Killios | no — 0 ocurrencias | **sí** — 919 filas |

**Conclusión**: V-DATAWAY-004 es **mucho más peligroso operativamente** que V-DATAWAY-001 porque opera en flujos normales sin necesidad de activación consciente, no deja marker, y ya impacta producción.

## Recomendaciones (no implementadas)

### R1: agregar UNIQUE constraint a la BD (corto plazo, defensivo)

```sql
-- Verificar duplicados existentes ANTES de aplicar:
SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate, COUNT(*)
FROM stock WHERE Cantidad > 0
GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
HAVING COUNT(*) > 1;

-- Si están limpios, aplicar:
CREATE UNIQUE INDEX UX_stock_llave_natural
ON stock(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)
WHERE Cantidad > 0;
```

**Atención**: no aplicar sin antes resolver los 919 duplicados existentes.

### R2: identificar y arreglar el path bugueado del CEST (Wave 13-10)

Localizar en `TOMWMS_BOF` la función que hace la mutación de stock en el flujo CEST y revisar si:
- normaliza `lic_plate=''` vs `NULL`
- usa `SELECT ... WITH (UPDLOCK, HOLDLOCK)` o equivalente
- tiene branch correcto INSERT vs UPDATE

### R3: script de consolidación batch (one-shot)

Sobre los 919 stocks afectados: agrupar por llave natural, sumar cantidades, mantener el `IdStock` mínimo, eliminar el resto. **Atención**: las reservas (`stock_res`) apuntan al `IdStock` viejo — el script tiene que actualizarlas también.

### R4: log de detección continuo

Agregar a `tools/case-seed/queries/data-discrepancy/` una query nueva que se corre periódicamente:

```sql
-- query 13 sugerida: stocks con llave natural duplicada
SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate,
       COUNT(*) cnt, SUM(Cantidad) total, MIN(IdStock) min_id, MAX(IdStock) max_id
FROM stock WHERE Cantidad > 0
GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate
HAVING COUNT(*) > 1
ORDER BY cnt DESC, total DESC;
```

(A agregar a `tools/case-seed/queries/data-discrepancy/13_stocks_llave_natural_duplicada.sql` cuando Erik apruebe.)

## Acción inmediata sugerida

1. Confirmar con Erik si autoriza correr la query 12 sobre `IMS4MB_BYB_PRD` y `IMS4MB_CEALSA_QAS` para medir si el patrón es Killios-only o sistémico cross-cliente.
2. Wave 13-10: trace del código del CEST en `TOMWMS_BOF` (probablemente `clsLnTrans_movimientos.Insertar_CEST` + `clsLnStock`).
3. NO ejecutar R3 (script de consolidación) sin antes:
   - Localizar el bug raíz en código
   - Detener el sangrado (fix del path bugueado)
   - Sino, los 919 duplicados se reproducen de inmediato.

## Update Wave 13-11 (re-medición live, 2026-04-29)

Re-corrida de las queries críticas contra `TOMWMS_KILLIOS_PRD_2026` con firewall AWS restablecido. **Cambios sustanciales en el modelo del bug**:

### Cronicidad confirmada: 11 meses de bug activo

Rango temporal de filas duplicadas en `stock`: **2025-05-28 → 2026-04-24 (331 días)**. Distribución mensual:

```
2025-05 |  12     2025-08 |   9     2025-11 | 341 ***
2025-06 |   4     2025-09 |  10     2025-12 | 162
2025-07 |   5     2025-10 |   5     2026-01 | 152
                                    2026-02 | 243
                                    2026-03 | 260
                                    2026-04 | 185 (parcial)
```

**Inflexión clarísima en noviembre 2025**: pasa de 5-12 filas/mes a 152-341. Pico histórico **2025-11-29 con 210 filas duplicadas creadas en un único día**. Sugiere **release del HH Android entre octubre y noviembre 2025** que cambió el comportamiento del UPDATE de stock.

### H1 refutada en su forma fuerte

Distribución de `lic_plate` sobre los 469 combos duplicados:

```
estado    | combos | filas
CON_VALOR |    349 | 1.012   (74.4%)
CERO ('0')|    120 |   376   (25.6%)
NULL      |      0 |     0
VACIO     |      0 |     0
```

**Cero combos con `lic_plate IS NULL` o `lic_plate = ''`**. La hipótesis H1 ("NULL/vacío rompe el comparador") queda **REFUTADA**. Aparece variante débil **H1.5** (sentinel `lic_plate = '0'`) que explica solo el 25%.

### H5 nueva: el bug NO es exclusivo del path CEST

Tipos de tarea con movimientos sobre lotes duplicados (desde mayo 2025):

```
VERI           15.259    UBIC            5.291
PIK            15.257    RECE            1.750
DESP           14.908    CEST              869   <-- solo 1.7%
                         REEMP_BE_PICK     678
                         INVE              386
                         PACK              304
```

**El CEST aporta solo el 1.7% de los movimientos sobre lotes duplicados**. Los flujos dominantes son VERI/PIK/DESP/UBIC. Esto **REFUTA** la asunción central de waves 13-9 y 13-10 ("bug exclusivo del CEST"). Hipótesis nueva **H5**: el bug está en una **función UPDATE stock compartida** que se llama desde múltiples handlers HH (CEST, UBIC, PIK, DESP, INVE).

### Reinterpretación del WMS164

Filas fundacionales `IdStock` 134176 y 134177 tienen `fecha_ingreso = 2026-02-09 10:52:55`. **Las filas duplicadas ya existían 2.5 meses antes del ticket del 23-abr**. Los movimientos M1..M5 reconstruidos en wave 13-9 NO crearon el bug, sólo tocaron stocks ya duplicados desde febrero. Y `lic_plate = 'FU06688'` en ambas filas (CON_VALOR): el WMS164 nunca fue caso de NULL/vacío.

### Modelo actualizado del bug raíz

| ID | Hipótesis | Probabilidad post Wave 13-11 |
|---|---|---|
| H1 | `lic_plate` NULL/vacío rompe comparador | **REFUTADA** |
| H1.5 | sentinel `lic_plate = '0'` rompe comparador | media (explica 25%) |
| H2 | Concurrencia inter-segundo, dos hilos sin lock | media |
| H3 | CEST por lote partido HH | baja (incompatible con multi-tipo) |
| H4 | UPDATE rechazado por check `Cantidad>0` → fallback INSERT | **muy alta** (explica 74.4% restante) |
| H5 | Función UPDATE stock compartida, multi-tipo de tarea | **alta** (q22 evidencia) |

**Hipótesis dominante combinada (H4 + H5)**: una **función compartida de movimiento de stock**, llamada desde múltiples handlers HH, tiene un fallback INSERT cuando un UPDATE falla por el check `Stock_NonNegative`. Esa función no consolida correctamente la fila destino.

### Implicación: alcance ampliado del bundle de extracción

El contrato de extracción para wave 13-12 ya no es solo el handler del CEST — es **toda la capa de mutación de stock en HH Android**. Ver `brain/debuged-cases/CP-013-killios-wms164/pedido-extraccion-hh-cest.md` (sección "Update Wave 13-11").

Reporte completo: `brain/debuged-cases/CP-013-killios-wms164/REPORTE-wave-13-11.md`.

## Cross-refs

- `dataway-analysis/07-correlacion-codigo-data/case-pointers/13-stock-insert-no-consolida-killios-wms164.md` — case-pointer formal
- `brain/debuged-cases/CP-013.md` — bitácora viva
- `brain/debuged-cases/CP-013-killios-wms164/REPORTE.md` — reporte de trazabilidad completo
- `brain/debuged-cases/CP-013-killios-wms164/queries/` — 12 queries reproducibles
- `dataway-analysis/04-ecuacion-de-balance/anti-patron-modo-depuracion.md` — V-DATAWAY-001 (contraste)
- `dataway-analysis/00-modelo-identidad-idstock.md` — patrón maestro DELETE+INSERT (cuando funciona bien) vs duplicación (cuando falla)
- `brain/agent-context/glossary.md` — términos: llave natural de stock, consolidación, stock fantasma
