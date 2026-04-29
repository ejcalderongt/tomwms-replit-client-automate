# CP-014 — Killios: tabla `stock` sin UNIQUE INDEX sobre llave natural (anti-patrón estructural de BD)

> **Segundo case-pointer "estructural" del catálogo.** Diferente a CP-013 (caso de campo: ticket reproducido contra BD), CP-014 nace de la **inspección del schema de BD** misma. No hay un ticket de operación detrás — la "fila ofensora" es una **ausencia**: la falta de un índice UNIQUE sobre la llave natural de `stock`. Es la condición permisiva que convierte cualquier bug aplicativo de path-de-mutación (como CP-013/V-DATAWAY-004) en daño persistente y silencioso.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-014-stock-sin-unique-index-llave-natural |
| Tipo | anti-patrón **estructural de BD** (primer ejemplar de la categoría) |
| Estado | confirmado por inspección de schema (auto-confirmable) |
| Severidad | alta — multiplicador de cualquier bug aplicativo de path-de-mutación |
| Persistencia (afecta BD) | sí — la ausencia del UNIQUE permitió 919 filas redundantes en CP-013 |
| Origen del caso | inspección del catálogo SQL `wms-db-brain` snapshot 2026-04-27 |
| Tabla afectada | `dbo.stock` |
| Detectado en | wave 13-10 (sin BD viva — sólo `wms-db-brain`) |
| Bug raíz inferido | `V-DATAWAY-005` (anti-patrón estructural BD) |
| Pattern asociado | aún no formalizado — candidato a `P-003` "invariante de dominio confiado al código sin defensa de BD" |
| Relación con CP-013 | causal-permisivo — CP-014 permite que CP-013 acumule daño en silencio |

## La evidencia (en una imagen)

```
Tabla: dbo.stock
Llave natural por convención del WMS:
  (IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)

Indices declarados sobre la tabla:
  PK_stock                              CLUSTERED PK     IdStock
  NCLI_STOCK_MERCOPAN_20220503_EJC      NONCLUSTERED     {IdBodega, IdProductoBodega, ...}
  NCLI_Stock_20191210_EJC               NONCLUSTERED     {IdUbicacion}
  NCLI_Stock_20200115_EJC               NONCLUSTERED     {IdBodega, IdPresentacion, ...}
  ... 11 indices NCLI mas, todos NONCLUSTERED, todos NO-UNIQUE

Indices UNIQUE: 0   <-- NINGUNO
```

**Las 4 columnas críticas de la llave natural** (`IdProductoBodega`, `IdUbicacion`, `IdProductoEstado`, `lote`, `lic_plate`) **están en casi todos los índices NCLI** — la llave natural se **reconoce implícitamente** para acelerar lookups, pero **nunca se enforza** como restricción.

## Lectura

V-DATAWAY-005 es **estructural**: vive en DDL, no en código. Cualquier bug aplicativo de path-de-mutación de stock (CEST/UBIC/RECE/AJUS/IMPL) que viole el invariante de unicidad **es aceptado calladamente por la BD**. Esto convierte bugs locales en daño sistémico:

- Un solo path bugueado (V-DATAWAY-004 en CEST) generó **919 filas redundantes en Killios** sin que la BD emita ningún error ni warning.
- Si mañana se agrega un path nuevo (futuro CEST-bulk, RECE-via-API, etc.) con un bug similar, el daño se acumulará igual.
- Los tests unitarios del código aplicativo pasan; el problema sólo es detectable auditando schema o midiendo `GROUP BY ... HAVING > 1`.

## Por qué CP-014 es estructural y no de campo

| Dimensión | CP-013 (campo) | CP-014 (estructural) |
|---|---|---|
| Origen | ticket de operación (WMS164) | inspección de schema (`wms-db-brain`) |
| Evidencia primaria | datos en `stock` (filas duplicadas) | DDL (ausencia de índice UNIQUE) |
| Auto-confirmación | `GROUP BY ... HAVING > 1` sobre `stock` | `SELECT name, is_unique FROM sys.indexes` |
| Reproducibilidad | requiere correr operativa que dispare el bug | ninguna — basta con leer `sys.indexes` |
| Necesita BD viva | no, puede usar `wms-db-brain` para ver duplicados (snapshot) | no, puede usar `wms-db-brain` para ver schema (snapshot) |
| Tipo de fix | corregir código aplicativo | aplicar DDL (UNIQUE INDEX filtrado) |

## Por qué amerita una categoría nueva en el INDEX

`CP-013` abrió la categoría "casos de campo confirmados con datos reales". `CP-014` requiere otra: **"casos estructurales (anti-patrones de DDL/schema)"**. Diferencias clave:

- No nacen de operación. Nacen de auditoría.
- No requieren ningún caso real para confirmarse. La ausencia es la evidencia.
- Su fix es DDL, no código aplicativo.
- Su impacto se mide en "qué bugs aplicativos amplifica", no en "qué filas ya rompió".

Categoría sugerida en INDEX: **"Casos estructurales (anti-patrones de DDL/schema, auto-confirmables por inspección de catálogo)"**.

## Relación con CP-013 (causal-permisivo)

```
CP-013 (campo)         CP-014 (estructural)
    |                       |
    | bug aplicativo:       | bug estructural:
    | path CEST hace        | tabla stock no tiene
    | INSERT en vez         | UNIQUE INDEX sobre
    | de UPDATE             | llave natural
    |                       |
    +---------+-------------+
              |
              v
    Daño persistente y silencioso:
    919 filas redundantes en stock
    (18.7% del stock activo de Killios)
```

**Sin CP-014**: V-DATAWAY-004 fallaría inmediato con `Violation of UNIQUE KEY constraint` en el primer INSERT bugueado, se loguea en `log_error_wms`, alguien lo ve, alguien lo arregla.

**Con CP-014**: V-DATAWAY-004 acumula filas durante meses sin que nadie note hasta que un usuario reporta un caso (WMS164) o alguien por casualidad corre `GROUP BY ... HAVING > 1`.

CP-014 **no es la causa de CP-013** (la causa es V-004, en código). CP-014 es la causa de que **CP-013 se vuelve invisible y crónico**.

## Acción propuesta esta wave

Solo medición y promoción a brain. **Ninguna mutación de DDL**. Ninguna recomendación de aplicar `CREATE UNIQUE INDEX` sin que antes:

1. Se resuelvan los 919 duplicados existentes (script de consolidación batch — R3 de V-DATAWAY-004).
2. Se normalice `lic_plate NULL` a `''` (R1 paso 1 de V-DATAWAY-005).
3. Se confirme con Erik la decisión arquitectónica (¿agregar UNIQUE va en línea con la cultura del WMS o requiere conversación previa con stakeholders?).

## Acciones propuestas (próximas waves)

1. **Wave 13-11**: bundle del HH Android (path CEST) para confirmar V-DATAWAY-004. CP-014 queda inerte hasta entonces porque su fix depende de que V-004 esté arreglado primero.
2. **Wave 13-13** (después de V-004 confirmado): proponer DDL completo de R1 a Erik:
   - script de consolidación batch
   - normalización de `lic_plate NULL`
   - `ALTER TABLE stock ALTER COLUMN lic_plate nvarchar(50) NOT NULL`
   - `CREATE UNIQUE INDEX UX_stock_llave_natural ... WHERE Cantidad > 0`
3. **Wave 13-14**: revisar si `stock_res`, `stock_se`, `stock_transito`, `stock_jornada` tienen el mismo problema (R3 de V-DATAWAY-005).

## Por qué este case-pointer es distinto de CP-001..CP-013

- **CP-001..CP-006, CP-009..CP-012**: breakpoint arqueológico (pattern P-001) — hardcode en código sin efecto persistente en BD.
- **CP-007 / CP-008**: marker persistente (`#EJCAJUSTEDESFASE`) — hardcode en código que sí deja huella en BD.
- **CP-013**: caso de campo confirmado con datos — ticket de operación reproducido contra BD productiva.
- **CP-014**: anti-patrón estructural de DDL — ausencia de constraint que convierte bugs aplicativos en daño persistente.

Cada una es una **categoría nueva** en el catálogo de case-pointers. Cuando aparezca un segundo CP-XXX estructural (por ej. en `stock_res`), se promueve a pattern formal `P-003`.

## Cross-refs

- `dataway-analysis/04-ecuacion-de-balance/anti-patron-stock-sin-unique-index.md` — anti-patrón formalizado V-DATAWAY-005
- `brain/debuged-cases/CP-013.md` — caso fundacional fenoménico que motivó el descubrimiento estructural
- `brain/debuged-cases/CP-013-killios-wms164/REPORTE-wave-13-10.md` — wave en la que se descubrió
- `dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md` — V-DATAWAY-004 (bug aplicativo causal)
- `db-brain/tables/stock.md` (rama `wms-db-brain`) — evidencia primaria del catálogo SQL
