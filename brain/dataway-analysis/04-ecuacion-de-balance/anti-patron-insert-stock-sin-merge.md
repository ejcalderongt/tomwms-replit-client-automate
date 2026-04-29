# Capa 04 / Anti-patrón: INSERT a `stock` sin merge contra llave natural

> **Bug `V-DATAWAY-004` (severidad alta)**: el flujo de CEST (Cambio de Estado) — y posiblemente otros flujos de mutación de stock — inserta una fila nueva en `stock` cuando debería consolidar contra una fila preexistente con la misma llave natural. Resultado: dos o más filas distintas (`IdStock` diferentes) que comparten `(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)`. **Ya impactó 919 filas (18.7%) del stock activo de Killios PRD.**

## TL;DR

| Aspecto | Valor |
|---|---|
| Bug ID | `V-DATAWAY-004` |
| Severidad | Alta |
| Estado | **Confirmado con datos** (CP-013) — pendiente trace de código (Wave 13-10) |
| Path inculpado | `CEST` (Cambio de Estado) en flujo HH/BOF — confirmado en M3/M4 del trace WMS164 |
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

## Las 3 hipótesis del bug raíz (ordenadas por probabilidad)

### H1: `lic_plate` vacío rompe el comparador

Si el comparador que decide consolidar trata `lic_plate=''` como NULL (o no normaliza vacío vs NULL), dos filas con `lic_plate=''` no se consideran iguales y el merge nunca ocurre.

**Evidencia indirecta**: en el caso WMS164, las 5 filas de `trans_movimientos` tienen `lic_plate=''` (vacío) en los CEST y UBIC. Esto es ya una grieta de trazabilidad propia (no se sabe sobre qué tarima física se hizo cada CEST), y sospechosamente coincide con el sitio del bug.

**Cómo verificar**: query SQL contando combos duplicados pero filtrando por `lic_plate=''`.

### H2: concurrencia inter-segundo

M3 y M4 están a 13 segundos. Si el flujo del CEST permite que dos hilos disparen la mutación concurrentemente (por ej. la HH dispara un job y mientras tanto el operador escanea otra etiqueta), ambos hilos hacen `SELECT count(*) FROM stock WHERE llave_natural=...` antes de que el primero haya commiteado. Ambos ven `count = 0` y deciden hacer `INSERT`. Race condition clásica de upsert sin lock.

**Cómo verificar**: ver si el código del CEST tiene `SELECT ... WITH (UPDLOCK, HOLDLOCK)` o lock equivalente en el comparador.

### H3: CEST por lote partido (HH permite dos eventos separados)

La HH puede haber permitido al operador confirmar el CEST en dos eventos separados (40 y luego 30) sobre la misma posición destino. El flujo interpreta "dos eventos = dos filas" en lugar de "un destino = un merge".

**Cómo verificar**: ver si el código del CEST tiene branch tipo `IF cant_total_lote = cant_a_mover THEN UPDATE ELSE INSERT`.

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

## Cross-refs

- `dataway-analysis/07-correlacion-codigo-data/case-pointers/13-stock-insert-no-consolida-killios-wms164.md` — case-pointer formal
- `brain/debuged-cases/CP-013.md` — bitácora viva
- `brain/debuged-cases/CP-013-killios-wms164/REPORTE.md` — reporte de trazabilidad completo
- `brain/debuged-cases/CP-013-killios-wms164/queries/` — 12 queries reproducibles
- `dataway-analysis/04-ecuacion-de-balance/anti-patron-modo-depuracion.md` — V-DATAWAY-001 (contraste)
- `dataway-analysis/00-modelo-identidad-idstock.md` — patrón maestro DELETE+INSERT (cuando funciona bien) vs duplicación (cuando falla)
- `brain/agent-context/glossary.md` — términos: llave natural de stock, consolidación, stock fantasma
