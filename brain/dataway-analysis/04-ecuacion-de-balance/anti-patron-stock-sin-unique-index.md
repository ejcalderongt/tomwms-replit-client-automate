# Capa 04 / Anti-patrón estructural: `stock` sin UNIQUE INDEX sobre llave natural

> **Bug `V-DATAWAY-005` (severidad alta)**: la tabla `dbo.stock` no tiene **ningún** índice UNIQUE sobre la llave natural `(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)`. La unicidad del invariante de stock está delegada exclusivamente al código aplicativo. Esto convierte cualquier bug de path-de-mutación (como `V-DATAWAY-004` en CEST) en un bug **silenciosamente persistible**: la BD acepta la duplicación sin objeción.

## TL;DR

| Aspecto | Valor |
|---|---|
| Bug ID | `V-DATAWAY-005` |
| Tipo | anti-patrón **estructural de BD** (no de código) |
| Severidad | Alta — multiplicador de cualquier bug aplicativo de path-de-mutación |
| Estado | **Confirmado por inspección de schema** (wave 13-10, vía `wms-db-brain`) |
| Tabla afectada | `dbo.stock` |
| Caso fundacional | CP-014 (case-pointer estructural derivado de la inspección de schema) |
| Relación con V-DATAWAY-004 | **causal-permisivo**: V-005 es la condición que permite que V-004 deje huella en BD. Sin V-005, V-004 sería detectable inmediato (UNIQUE constraint violation) y no se acumularía silenciosamente. |
| Auto-confirmable | sí — basta con `SELECT name, is_unique FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.stock')` |

## La evidencia (de `wms-db-brain` snapshot 2026-04-27)

Catálogo SQL extraído de `TOMWMS_KILLIOS_PRD` (`db-brain/tables/stock.md`):

```
Tabla:       dbo.stock (4.703 filas, 33 columnas)
Schema mod:  2024-09-12

Índices:     14 total
  PK_stock                              CLUSTERED PK     -> IdStock
  NCLI_STOCK_MERCOPAN_20220503_EJC      NONCLUSTERED     (no UNIQUE)
  NCLI_Stock_20191210_EJC               NONCLUSTERED     (no UNIQUE)
  NCLI_Stock_20200115_EJC               NONCLUSTERED     (no UNIQUE)
  NCLI_Stock_20210304_EJC               NONCLUSTERED     (no UNIQUE)
  NCLI_Stock_EJC20210217                NONCLUSTERED     (no UNIQUE)
  NCLI_BODEGA_UBICACION_20210217_EJC    NONCLUSTERED     (no UNIQUE)
  NCLI_STOCK_20191205_EJC               NONCLUSTERED     (no UNIQUE)
  NCLI_Stock_20200112_EJC               NONCLUSTERED     (no UNIQUE)
  NCLI_STOCK_202210270100_EJC           NONCLUSTERED     (no UNIQUE)
  NCLI_STOCK_LICPLATE_202217120405AM_EJC NONCLUSTERED    (no UNIQUE)
  NCLI_Stock_202308081128_EJC           NONCLUSTERED     (no UNIQUE)
  NCLI_STOCK_202302211402_EJC           NONCLUSTERED     (no UNIQUE)
  NCLI_Stock_20230522_EJC               NONCLUSTERED     (no UNIQUE)

Constraints:
  Stock_NonNegative_20200115_EJC : ([Cantidad]>(0))    (check, no unique)
```

**Observaciones**:

1. **14 índices**, ninguno UNIQUE.
2. **El sufijo `_EJC` sugiere autoría de Erik en todas las migraciones** — la BD nunca tuvo (o nunca se intentó tener) defensa de unicidad.
3. **Hay 4 columnas que se repiten en casi todos los índices NCLI**: `IdProductoBodega`, `IdUbicacion`, `IdProductoEstado`, `lote`, `lic_plate`. Es decir, la llave natural **se reconoce implícitamente** (los índices la usan para acelerar lookups) pero **nunca se enforza** como restricción.
4. El único constraint declarado es `Cantidad > 0`, que de hecho **agrava** V-DATAWAY-004 (ver hipótesis H4 en CP-013 wave-13-10): rechaza UPDATEs que dejarían cantidad en 0, forzando al código a fallback INSERT.

## Por qué V-DATAWAY-005 es estructural

A diferencia de V-DATAWAY-001..004 (que viven en código aplicativo), V-DATAWAY-005 vive en **DDL**. Sus propiedades:

- **No requiere bug de código para causar daño** — basta con que un código tenga un bug y la BD calladamente lo acepta.
- **Multiplica el riesgo de cualquier path nuevo** — cuando se agregue un nuevo path de mutación de stock (futuro CEST-bulk, RECE-via-API, etc.), si tiene el mismo bug que el actual CEST, la BD lo va a permitir igual.
- **Es invisible al testing aplicativo** — los tests unitarios del código aplicativo pasan; sólo se detecta auditando schema.

## Por qué probablemente no se agregó UNIQUE en su momento

Hipótesis sobre por qué Erik nunca agregó el índice UNIQUE (a confirmar con él):

1. **Migración riesgosa**: si al momento de proponer el UNIQUE ya existían duplicados, el `CREATE UNIQUE INDEX` falla. Habría que limpiar primero. Tarea no trivial.
2. **No había detección del problema**: si nadie había hecho `GROUP BY ... HAVING > 1` sobre la tabla, el problema era invisible. CP-013 es probablemente la primera vez que se mide el alcance.
3. **`lic_plate` puede ser NULL en la BD**: un índice UNIQUE sobre columnas nullable trata cada NULL como distinto en SQL Server (a diferencia de Postgres). Eso quitaría protección sobre el caso más común (lic_plate vacío) — habría que normalizar antes (`UPDATE stock SET lic_plate = '' WHERE lic_plate IS NULL`) y declarar el índice como filtrado.
4. **Convención del WMS = "el código sabe lo que hace"**: histórica decisión cultural de no usar constraints de BD para invariantes de negocio. Se comparte con muchos WMS legacy.

## Recomendaciones (no implementadas, requieren autorización Erik)

### R1: agregar UNIQUE INDEX filtrado (largo plazo, defensivo)

**Pre-requisito**: ejecutar primero la script de consolidación batch (R3 de V-DATAWAY-004) para eliminar los 919 duplicados existentes. Sin esto, el `CREATE UNIQUE INDEX` falla.

**Pre-requisito 2**: normalizar `lic_plate` para evitar el problema de "NULL distinto a NULL en UNIQUE":

```sql
UPDATE stock SET lic_plate = '' WHERE lic_plate IS NULL;
ALTER TABLE stock ALTER COLUMN lic_plate nvarchar(50) NOT NULL;
```

**Aplicar UNIQUE filtrado**:

```sql
CREATE UNIQUE INDEX UX_stock_llave_natural
ON stock (IdProductoBodega, IdUbicacion, IdProductoEstado, lote, lic_plate)
WHERE Cantidad > 0;
```

El filtro `WHERE Cantidad > 0` es importante porque:

- Permite que existan filas históricas con `Cantidad = 0` (residuos de operaciones viejas) sin entrar en conflicto.
- Sólo enforce sobre stock "activo".
- Cualquier futuro path bugueado falla inmediato con `Violation of UNIQUE KEY constraint`, lo que **es visible** y se loguea en `log_error_wms`.

### R2: agregar query de detección continua a `tools/case-seed/`

Aunque se aplique R1, agregar también una query que se corre periódicamente y reporta duplicados (defensa en profundidad — alguien podría hacer DROP del índice por error en una migración futura):

```sql
-- queries/data-discrepancy/15_stocks_duplicate_natural_key.sql
SELECT IdProductoBodega, IdUbicacion, IdProductoEstado, lote, lic_plate,
       COUNT(*) cnt, SUM(Cantidad) total, MIN(IdStock) min_id, MAX(IdStock) max_id
FROM stock WHERE Cantidad > 0
GROUP BY IdProductoBodega, IdUbicacion, IdProductoEstado, lote, lic_plate
HAVING COUNT(*) > 1
ORDER BY cnt DESC, total DESC;
```

### R3: revisar si otras tablas de stock tienen el mismo problema

Inspección preliminar en `wms-db-brain/tables/`:

- `stock_res` — reservas, también nominalmente con llave natural compuesta. Verificar índices UNIQUE.
- `stock_se` — stock entrante. Verificar.
- `stock_transito` — stock en movimiento. Verificar.
- `stock_jornada` — snapshot diario. Probablemente OK pero verificar.

Si tienen el mismo patrón, V-DATAWAY-005 se generaliza a "**dominio stock confiado al código sin defensa de BD**".

## Diferencia clave vs V-DATAWAY-004

| Aspecto | V-DATAWAY-004 | V-DATAWAY-005 |
|---|---|---|
| Capa | Código aplicativo (path CEST en HH) | DDL (schema de BD) |
| Síntoma | INSERT cuando debería UPDATE | BD acepta INSERTs duplicados sin error |
| Causa | bug en lookup o fallback en catch | falta de UNIQUE INDEX |
| Detección | `GROUP BY ... HAVING > 1` sobre `stock` | inspección de `sys.indexes` |
| Fix | corregir el path CEST | `CREATE UNIQUE INDEX UX_stock_llave_natural` |
| Independencia | causa primaria del daño | causa permisiva: amplifica V-004 y cualquier otro bug futuro de path-de-mutación |
| ¿Resuelve solo el otro? | **NO** — fix de código sin UNIQUE deja la puerta abierta a otros paths | **NO** — UNIQUE sin fix de código rompe la operación (errores de constraint en tiempo de uso) |
| Estrategia recomendada | aplicar **ambos** en orden: 1) script consolidación, 2) fix path CEST, 3) UNIQUE | idem |

## Relación con la cultura del WMS

V-DATAWAY-005 es probablemente síntoma de una decisión cultural histórica: "los invariantes los protege el código, no la BD". Esta decisión:

- **Es razonable** en sistemas legacy con migraciones costosas.
- **Es peligrosa** cuando hay múltiples paths que pueden tocar la misma tabla.
- **Se vuelve insostenible** cuando aparece un caso como CP-013/WMS164 donde un sólo path bugueado contamina 18.7% del stock activo.

Si Erik confirma que la decisión fue cultural (no técnica), V-005 se promueve a `brain/conventions/` como una **convención obsoleta a deprecar**.

## Cross-refs

- `dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md` — V-DATAWAY-004 (bug aplicativo en path CEST)
- `dataway-analysis/07-correlacion-codigo-data/case-pointers/14-stock-sin-unique-index-llave-natural.md` — CP-014 (case-pointer estructural)
- `brain/debuged-cases/CP-013.md` — caso fundacional WMS164 que destapó el problema
- `brain/debuged-cases/CP-013-killios-wms164/REPORTE-wave-13-10.md` — wave en la que se descubrió V-005
- `db-brain/tables/stock.md` (rama `wms-db-brain`) — evidencia primaria
