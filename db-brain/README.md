# db-brain — Cerebro de la BD productiva (TOMWMS_KILLIOS_PRD)

> **Catálogo SQL completo** del WMS Killios extraído live desde
> `52.41.114.122,1437` (BD `TOMWMS_KILLIOS_PRD`) y materializado como
> markdown navegable. Es la **fuente de verdad estructural** de la BD: lo que
> existe, cómo está definido, qué depende de qué.
>
> **Acceso**: estrictamente **READ-ONLY** desde Replit usando el secret
> `WMS_KILLIOS_DB_PASSWORD`. Ver `wms-brain` →
> `brain/entities/rules/rule-08-killios-prod-solo-lectura.md`.

Snapshot: 2026-04-27T01:29Z. Mantenedor: agente Replit por encargo de Erik
Calderón.

---

## Mapa de las cuatro ramas del repositorio

| Rama | Propósito | Tamaño | README |
|---|---|---:|---|
| `main` | Repo de intercambio: bundles, scripts, bridge | 24 archivos | `README.md` |
| `openclaw-control-ui` | MVP de bootstrap/control del brain | 15 archivos | `README.md` |
| `wms-brain` | **Cerebro funcional** del agente: doctrina + módulos + ADRs | 149 archivos | `brain/README.md` |
| **`wms-db-brain`** (esta) | **Catálogo SQL** Killios PRD (621 objetos) | 636 archivos | `db-brain/README.md` |

> Las cuatro son **orphan branches**. Para clonar sólo ésta:
> `git clone --single-branch --branch wms-db-brain <url>`.

---

## Para qué sirve esta rama

Cuando el agente Replit necesita responder algo sobre la BD —"¿qué columnas
tiene `stock_res`?", "¿qué SPs tocan `i_nav_config_enc`?", "¿hay alguna
vista que cruce `trans_pe_det` con `stock`?"— **consulta esta rama** en vez
de tener que conectarse a Killios cada vez. Beneficios:

1. **Velocidad**: lectura local de markdown vs query a SQL Server remoto.
2. **Consistencia**: snapshot determinístico en un punto del tiempo.
3. **Auditabilidad**: cada extracción es un commit, se ve el diff.
4. **Sin riesgo**: no toca producción para responder.

Cuando el catálogo cambia (DDL nuevo en Killios), se re-extrae con el
extractor (`brain/sql-catalog/extract_for_db_brain.mjs` en la rama
`wms-brain`) y se commitea acá.

---

## Estructura de la rama

```
.
├── .gitkeep                                 (mantiene la rama vacía-raíz)
└── db-brain/
    ├── README.md                            <- este archivo
    │
    ├── _meta/                               <- metadata del snapshot
    │   ├── extracted_at.txt                       timestamp ISO + servidor
    │   ├── extractor.md                           cómo se generó este snapshot
    │   └── stats.md                               conteos + top tablas + hallazgos
    │
    ├── tables/                              <- 346 archivos .md (uno por tabla)
    │   ├── _index.md                              índice general
    │   ├── stock.md                               tabla de stock (33 cols)
    │   ├── stock_res.md                           reservas de stock (35 cols)
    │   ├── trans_pe_enc.md                        encabezado pedido (70 cols)
    │   ├── trans_pe_det.md                        detalle pedido (44 cols)
    │   ├── i_nav_config_enc.md                    config nav (69 cols, flags MI3)
    │   ├── log_error_wms.md                       log errores (15 cols)
    │   ├── propietarios.md                        propietarios (23 cols)
    │   ├── trans_movimientos.md                   81K filas
    │   ├── trans_re_det_lote_num.md               180K filas (la más grande)
    │   ├── ... (336 tablas más)
    │   └── (incluye tablas _bk, t_*, snapshots stock_YYYYMMDD)
    │
    ├── views/                               <- 221 archivos .md (una por vista)
    │   ├── _index.md                              índice general
    │   ├── VW_Stock_Transito.md                   stock en tránsito
    │   ├── VW_TMSTickets_Sin_Retroactivo.md       tickets TMS
    │   ├── VW_Tareas_Activas_HH.md                tareas activas HH
    │   ├── VW_Tareas_Picking_HH.md                picking HH
    │   ├── VW_TransUbicHhDet.md                   transferencias ubicación HH
    │   ├── VW_Trans_Inv_Stock.md                  inventario y stock
    │   ├── VW_Verificacion_*.md                   familia verificación
    │   ├── vw_Indicador_*.md                      indicadores
    │   └── ... (213 vistas más)
    │
    ├── sps/                                 <- 40 archivos .md (uno por SP)
    │   ├── _index.md                              índice general
    │   ├── CLBD.md                                close BD del día (19 KB de definición)
    │   ├── CLBD_INICIARBD.md                      iniciar BD (18 KB)
    │   ├── CLBD_PRC_BY_IDBODEGA.md                close BD por bodega (15 KB)
    │   ├── SP_Importa_Stock_Bodegas_*.md          importación stock (familia 3 SPs)
    │   ├── SP_STOCK_JORNADA_DESFASE*.md           jornada desfase (familia 2 SPs)
    │   ├── sp_Blitz*.md                           toolkit Brent Ozar (Blitz, BlitzCache,
    │   │                                          BlitzIndex, BlitzLock, etc.) — instalados
    │   ├── sp_WhoIsActive.md                      sp_WhoIsActive de Adam Machanic (174 KB)
    │   └── ... (otros SPs operacionales)
    │
    ├── functions/                           <- 18 archivos .md (uno por función escalar)
    │   ├── _index.md
    │   ├── ConvertSecondsFormatoFecha.md
    │   ├── Get_Codigo_Area_By_IdUbicacion.md
    │   ├── Get_Porcentaje_Avance_Pedido.md
    │   ├── Get_Porcentaje_Avance_Picking.md
    │   ├── Nombre_Area.md
    │   ├── Nombre_Completo_Ubicacion.md / _1 / _2 / _3
    │   ├── Nombre_Completo_Ubicacion_Barra.md / _1 / _2 / _3
    │   ├── Nombre_Tramo.md
    │   ├── fdias_Exterior_by_IdCliente.md
    │   ├── fdias_locales_by_IdCliente.md
    │   └── fn_diagramobjects.md
    │
    └── parametrizacion/                     <- ★ flags y matrices por cliente
        ├── README.md                              filosofía multi-cliente
        ├── flags-bodega.md                        ~57 flags bit en bodega
        ├── flags-producto.md                      17 flags en producto
        ├── flags-cliente.md                       9 flags en cliente
        ├── flags-ajuste-tipo.md                   5 flags (incluye typo!)
        └── matriz-killios.md                      matriz de flags activos en Killios PRD
```

---

## `_meta/` — Metadatos del snapshot

#### `_meta/extracted_at.txt`

Timestamp ISO de la última extracción + servidor + usuario + extractor.
Ejemplo:

```
2026-04-27T01:29:47.537Z
Database: TOMWMS_KILLIOS_PRD
Server: 52.41.114.122:1437
User: sa
Extractor: brain/sql-catalog/extract_for_db_brain.mjs v1.0
```

#### `_meta/stats.md`

Conteos globales + top 15 tablas por filas + hallazgos persistentes:

| Tipo SQL | Cantidad |
|---|---:|
| `USER_TABLE` | 345 |
| `VIEW` | 220 |
| `SQL_STORED_PROCEDURE` | 39 |
| `SQL_SCALAR_FUNCTION` | 17 |
| **TOTAL** | **621** |

> Diferencia con conteos de archivos: la rama tiene 346 tablas (= 345 + 1
> índice), 221 vistas (= 220 + 1 índice), 40 SPs (= 39 + 1 índice), 18
> funciones (= 17 + 1 índice), porque cada subdir incluye su `_index.md`.

**Top 5 tablas por filas:**

| Tabla | Filas | Modify date |
|---|---:|---|
| `trans_re_det_lote_num` | 180.181 | 2022-12-17 |
| `trans_movimientos` | 81.641 | 2024-07-02 |
| `log_error_wms` | 66.339 | 2025-06-12 |
| `t_producto_bodega` | 42.357 | 2019-05-21 |
| `trans_picking_ubic` | 26.567 | 2025-07-16 |

#### `_meta/extractor.md`

Cómo se generó este snapshot. Resume:

- Generador: Node + driver `mssql` desde Replit (no el Python local).
- Plan futuro: integrar con `brain/sql-catalog/extract_sql_catalog.py` con
  flag `--markdown-out ./db-brain/` para regenerar idempotentemente.
- Idempotencia: cada run debe producir un commit limpio (orden estable por
  `schema, name, ord`).
- Frecuencia: re-correr cuando hay cambio de schema o cuando se importa un
  nuevo cliente (cambia matriz de flags).
- **Sensibilidad**: `module.definition` (cuerpo de SPs/vistas/funciones)
  va al markdown pero NUNCA se logea ni se expone a clientes externos.

---

## `tables/` — 346 markdowns (uno por tabla)

Cada `tables/<nombre>.md` tiene:

1. **Header**: nombre, schema, esquema, modify date, row count.
2. **Columnas**: lista ordenada con tipo SQL, nullable, default, identity.
3. **Índices**: PK, índices únicos, NC indexes con sus columnas.
4. **FKs**: referencias salientes (esta tabla apunta a) e entrantes (otras
   tablas la apuntan a ella).
5. **Check constraints** (si existen — sólo 4 en toda la BD).
6. **Triggers** (si existen).
7. **Computed columns**.
8. **Notas / hallazgos** (curated por el agente — typos, defaults raros,
   patrones de uso conocidos).

Tablas más críticas para el módulo de reservas (módulo `reservation` en
`wms-brain`):

- `stock` (33 cols) — stock disponible.
- `stock_res` (35 cols) — reservas de stock (máquina de estados clave).
- `trans_pe_enc` (70 cols) — encabezado del pedido.
- `trans_pe_det` (44 cols) — detalle del pedido.
- `i_nav_config_enc` (69 cols) — config del WMS, **flags MI3** (incluye el
  typo histórico `explosio_automatica_nivel_max` coexistiendo con
  `explosion_automatica_nivel_max`).
- `i_nav_ped_traslado_det` (22 cols) — detalle traslados.
- `log_error_wms` (15 cols) — log de errores (66K filas).
- `propietarios` (23 cols) — propietarios.
- `propietario_bodega` (8 cols) — pivote propietario↔bodega.

Para el DDL exhaustivo (con índices recomendados y CHECKs sugeridos) ver
`wms-brain` → `brain/sql-catalog/reservation-tables.md`.

### Convenciones de naming detectadas

- **`t_*`**: heredado del schema viejo (ej. `t_producto_bodega`).
- **`stock`, `cliente`, `trans_*`**: convención sin prefijo, más nueva.
- **`*_bk`**: backups en producción (anti-patrón pero histórico).
- **`stock_YYYYMMDD`**: snapshots manuales por fecha (ej. `stock_20240715`).
- **`trans_*`** son las tablas operacionales — la mayoría sin FKs declaradas
  por performance (ver `wms-brain` → `rule-no-fk-en-trans.md`).

---

## `views/` — 221 markdowns (una por vista)

Familias notables:

- **`VW_Tareas_*`**: tareas operacionales (activas, picking, operador,
  HH).
- **`VW_TransUbic*`**: transferencias de ubicación.
- **`VW_Stock_*`**: vistas de stock (en tránsito, jornada, mercadería).
- **`VW_Trans_Inv_*`**: inventario y stock.
- **`VW_Verificacion_*`**: familia de verificación (consolidada, LFV,
  detallada sin licencia).
- **`VW_Tiempos_*`**: KPIs de tiempos (ingreso, picking, operador).
- **`vw_Indicador_*`**: indicadores agregados (despachos, picking,
  verificaciones).
- **`VW_TMS*`**: tickets TMS.

Cada `views/<nombre>.md` incluye:
1. Header (schema, modify date, dependencias).
2. **Definición SQL completa** (la del `sys.sql_modules`).
3. Columnas con tipo.
4. Dependencias entrantes y salientes.
5. Indicadores de uso (cuántos otros objetos la referencian).

---

## `sps/` — 40 markdowns (uno por SP)

Dos grupos diferenciados:

### SPs operacionales del WMS (~15)

- `CLBD.md`, `CLBD_INICIARBD.md`, `CLBD_PRC.md`, `CLBD_PRC_BY_IDBODEGA.md`:
  familia de **cierre de BD del día**.
- `SP_Importa_Stock_Bodegas_General_y_Dañado*.md`: familia de **importación
  de stock**.
- `SP_STOCK_JORNADA_DESFASE*.md`: cálculo de **jornada y desfase**.
- `GetCantidadPesoByProductoBodega.md`, `GetListaStockByProductoBodega.md`,
  `GetResumenStockCantidad.md`, `Get_Ubicaciones_Vacias_By_IdTramo_And_IdBodega.md`:
  consultas pesadas con SP.
- `Concurrencia.md`, `asignar_jornada_laboral.md`,
  `sp_eliminar_by_Referencia.md`: utilidades operacionales.

### SPs de tooling DBA (~25)

- **Toolkit Brent Ozar** (`sp_Blitz`, `sp_BlitzCache`, `sp_BlitzFirst`,
  `sp_BlitzIndex`, `sp_BlitzLock`, `sp_BlitzWho`, `sp_BlitzBackups`,
  `sp_AllNightLog*`, `sp_DatabaseRestore`, `sp_foreachdb`, `sp_ineachdb`,
  `sp_index_maintenance_daily`): herramientas DBA instaladas en la BD.
- **`sp_WhoIsActive`** de Adam Machanic (174 KB de definición): otra utilidad
  DBA estándar.
- **Sistema de diagramas** (`sp_alterdiagram`, `sp_creatediagram`,
  `sp_dropdiagram`, `sp_helpdiagramdefinition`, `sp_helpdiagrams`,
  `sp_renamediagram`, `sp_upgraddiagrams`): SQL Server diagram support.

> **Nota**: separar visualmente "WMS" vs "tooling DBA" es un TODO de
> futura iteración (subdir `sps/wms/` y `sps/dba/`).

Cada `sps/<nombre>.md` incluye:
1. Header (schema, modify date, parámetros).
2. **Definición SQL completa**.
3. Parámetros (in/out con tipos).
4. Dependencias salientes (tablas/vistas/SPs que lee/escribe).
5. Indicadores de uso.

---

## `functions/` — 18 markdowns (una por función escalar)

Sólo funciones escalares (no `SQL_TABLE_VALUED_FUNCTION` ni
`SQL_INLINE_TABLE_VALUED_FUNCTION` — esas no aparecen en este snapshot).

Familias notables:
- **`Nombre_Completo_Ubicacion*`** (4 variantes) y **`Nombre_Completo_Ubicacion_Barra*`**
  (4 variantes): formateo de nombres de ubicación. La proliferación de
  variantes (`_1`, `_2`, `_3`) sugiere historial de A/B testing nunca
  cerrado — candidato a refactor.
- **`Get_Porcentaje_Avance_Pedido` / `_Picking`**: cálculo de avance.
- **`fdias_Exterior_by_IdCliente` / `_locales_by_IdCliente`**: días de
  cliente.
- **`Nombre_Area`, `Nombre_Tramo`, `Get_Codigo_Area_By_IdUbicacion`**:
  formateo de áreas/tramos.
- **`ConvertSecondsFormatoFecha`**: conversión de tiempo.

---

## `parametrizacion/` — Flags y matrices por cliente (★)

**Subcarpeta crítica**: documenta la filosofía **multi-cliente con código
único**.

> El WMS no tiene tablas `config_*` ni `parametros`. La parametrización
> está **embebida en los maestros como columnas bit**. Cada cliente
> (Killios, Becofarma, BYB, Cealsa, Mampa) tiene una combinación de flags
> distinta que cambia el comportamiento.

#### `parametrizacion/README.md`

Filosofía + densidad de configuración:

| Maestro | # flags bit | Detalle |
|---|---:|---|
| `bodega` | ~57 | El maestro más configurable |
| `producto` | 17 | |
| `cliente` | 9 | |
| `ajuste_tipo` | 5 | Incluye el typo histórico `momdifica_vencimiento` |

#### `parametrizacion/flags-*.md`

Catálogo de cada flag bit por maestro:
- Nombre exacto de la columna (incluyendo typos históricos).
- Significado de bit=1 vs bit=0.
- Quién lo lee (qué SPs/vistas/módulos del WMS).
- Default histórico.
- Notas de impacto.

#### `parametrizacion/matriz-killios.md`

**Matriz de flags activos en Killios PRD** validada live el 2026-04-27.
Permite responder rápido "en Killios, ¿el flag X está prendido?"
sin abrir Killios.

#### Otras matrices por cliente (TBD)

| Cliente | BD | Estado | Matriz |
|---|---|---|---|
| Killios | `TOMWMS_KILLIOS_PRD` | Validado 2026-04-27 | `matriz-killios.md` |
| Becofarma | (TBD) | Sin acceso aún | placeholder |
| BYB | `IMS4MB_BYB_PRD` | Acceso pendiente extracción | placeholder |
| Cealsa | `IMS4MB_CEALSA_QAS` | QA solamente | placeholder |
| La Cumbre | (TBD) | — | placeholder |
| Mampa (MHS) | (TBD) | — | placeholder |

Cuando haya ≥2 matrices completas, se publica `diff-cross-cliente.md` con
las diferencias.

#### Cómo se referencia esto desde `wms-brain`

Cuando una entity de `wms-brain` (módulo, regla, caso) depende de un
comportamiento configurable, debe linkear:

```yaml
db_brain_refs:
  - db-brain://parametrizacion/flags-producto#control_lote
```

Y la matriz por cliente le dice si ese flag está prendido o apagado en
cada cliente productivo.

---

## Hallazgos persistentes del snapshot (2026-04-27)

- **Naming mixto**: convive `t_*` (heredado) con sin-prefijo (`stock`,
  `cliente`, `trans_*`).
- **Backups en BD productiva**: tablas con sufijo `_bk` siguen en producción
  (anti-patrón asumido).
- **Snapshots puntuales**: `stock_YYYYMMDD` — convención de respaldo manual.
- **Typos en producción que conviven con los nombres correctos**:
  - `ajuste_tipo.momdifica_vencimiento` (debiera ser `modifica_vencimiento`).
  - `i_nav_config_enc.explosio_automatica_nivel_max` coexiste con
    `explosion_automatica_nivel_max`.
- **2625 dependencias** en `sys.sql_expression_dependencies`.
- **389 FKs declaradas** — la mayoría de tablas operacionales (`trans_*`)
  no tiene FKs (decisión de performance, ver `wms-brain` →
  `rule-no-fk-en-trans.md`).
- **4 check constraints** en toda la BD — escasez crónica de validación
  declarativa.

---

## Reglas de oro de esta rama

1. **READ-ONLY estricto.** Esta rama refleja Killios PRD, que es read-only
   desde Replit. NUNCA se ejecuta nada distinto de `SELECT` sobre `sys.*`.
2. **Snapshot inmutable por commit.** Si la BD cambia, se re-extrae y se
   commitea — nunca se edita el markdown a mano (excepto `parametrizacion/`
   que es curated).
3. **`module.definition` es sensible.** El cuerpo de SPs/vistas/funciones
   se versiona acá pero no se logea ni se expone a terceros.
4. **`parametrizacion/` es curated.** El extractor automático lo **preserva**,
   no lo sobrescribe.
5. **No mezclar con `wms-brain`.** Esta rama es estructura cruda; las
   interpretaciones, decisiones y módulos viven en `wms-brain`.

---

## Cómo regenerar este snapshot

Desde la rama `wms-brain`:

```bash
# Variables de entorno necesarias
export WMS_KILLIOS_DB_HOST="52.41.114.122,1437"
export WMS_KILLIOS_DB_NAME="TOMWMS_KILLIOS_PRD"
export WMS_KILLIOS_DB_USER="sa"
export WMS_KILLIOS_DB_PASSWORD="..."   # secret de Replit, no commitear

# Correr el extractor (genera markdowns idempotentes)
node brain/sql-catalog/extract_for_db_brain.mjs \
     --output-dir /tmp/db-brain-out

# Validar diff
diff -r /tmp/db-brain-out ../wms-db-brain/db-brain
```

Si el diff es razonable, commit + push a `wms-db-brain`. Cada extracción es
una entrada en el log de auditoría.

---

## Cross-refs a otras ramas

- **Para entender el cerebro funcional que interpreta esta BD** →
  `wms-brain` → `brain/README.md`.
- **Para entender el módulo de reservas que toca 9 de estas tablas** →
  `wms-brain` → `brain/entities/modules/reservation/README.md`.
- **Para el DDL crítico de las 9 tablas de reservation con índices
  recomendados** → `wms-brain` → `brain/sql-catalog/reservation-tables.md`.
- **Para el extractor que generó este snapshot** → `wms-brain` →
  `brain/sql-catalog/extract_for_db_brain.mjs`.

---

## Estado actual (snapshot 2026-04-27)

- **636 archivos versionados** en la rama (incluye índices y meta).
- **621 objetos SQL** del catálogo Killios PRD (345 tablas + 220 vistas + 39
  SPs + 17 funciones escalares).
- **6 archivos curated** en `parametrizacion/` (1 README + 4 flags + 1
  matriz Killios).
- **3 archivos de meta** en `_meta/`.
- **Última extracción**: 2026-04-27T01:29:47Z.
- **Server**: `52.41.114.122,1437` BD `TOMWMS_KILLIOS_PRD` user `sa`.

---

## Roadmap pendiente (no comprometido)

- Separar `sps/` en `sps/wms/` y `sps/dba/` (mover toolkit Brent Ozar y
  `sp_WhoIsActive` a un subdir).
- Completar matrices `parametrizacion/matriz-<cliente>.md` para BYB,
  Becofarma, Cealsa, La Cumbre, Mampa.
- Publicar `parametrizacion/diff-cross-cliente.md` cuando haya ≥2 matrices.
- Sumar `tables/_groups.md` clasificando las 345 tablas en familias
  (maestros, transaccionales, log, snapshots, backups, navegación,
  configuración, etc.).
- Documentar las **4 check constraints** existentes (¿son útiles? ¿se
  pueden ampliar?).
- Sumar `functions/` los `TABLE_VALUED_FUNCTION` (si los hay).
