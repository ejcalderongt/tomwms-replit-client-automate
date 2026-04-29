# dataway-analysis

> Análisis forense de la correlación entre código y datos en TOMWMS, con foco en el ciclo de vida del IdStock, el particionamiento de licencias, y la ecuación de balance teórico vs actual.

## Propósito

El TOMWMS legacy expone una característica que en su momento fue diseño consciente y hoy es deuda interpretativa: **el IdStock no es una identidad estable**. Cada vez que ocurre una operación significativa (cambio de ubicación vía HH, cambio de estado, implosión, ajuste, reubicación parcial, picking parcial, split físico durante reserva), el flujo destruye el IdStock origen e inserta uno nuevo con cantidad/ubicación/estado modificados. Una sola **licencia** física puede terminar asociada a un conjunto creciente de IdStocks.

El sistema **no** mantiene un linaje linkeado tipo blockchain del IdStock. El historial está **implícito** en `trans_movimientos` (tabla append-only de movimientos), pero reconstruirlo requiere conocimiento tribal de qué operación pudo haber generado cada split.

`dataway-analysis/` es la **biblioteca de interpretación** que convierte ese conocimiento tribal en documentación consultable, y se conecta con `tools/case-seed/` (la herramienta de captura forense ya existente) para cerrar el loop síntoma → seed → diagnóstico.

## Glosario corto (ver `brain/agent-context/glossary.md` para versión completa)

- **operador**: bodeguero que opera la HH o la BOF. Persona física que mueve mercadería.
- **operación** (del cliente): la operatoria del negocio del cliente que usa el WMS.
- **IdStock**: identificador interno de una fila de la tabla `stock`. **No es estable**: muta por DELETE+INSERT en cada operación significativa.
- **licencia**: identificador de la unidad logística física (`lic_plate`). Una licencia puede mapear a 1..N IdStocks.
- **puntero**: en analogía con C++, una fila de `stock_res` apunta a un IdStock. La suma de cantidades de los punteros que apuntan al mismo IdStock se resta de `stock.cantidad` para obtener disponible.
- **balance**: ecuación `Inventario_Inicial + Ingresos + Ajustes_Positivos − Ajustes_Negativos − Salidas = Existencia_Actual`. Si no cuadra, hay **gap**.
- **case-pointer**: hardcode encontrado en código fuente (Debug.Print con producto/lote/fecha/IdStock específico) que apunta a un caso real histórico que rompió en producción.

## Doble público: machine-structure (.yml) + human-vector (.md)

Cada unidad de contenido se publica en dos formatos:

- **`.yml`** — schema-strict, consumible por agentes/scripts. Contiene refs a código (file+line), tablas, queries SQL ejecutables, vínculos a otras secciones del brain.
- **`.md`** — narrativa, ejemplos, citas textuales, diagramas conceptuales. Pensado para lectura humana.

Ambos contienen la misma información semántica. Si en el futuro se quiere generar uno desde el otro, el contenido es coherente.

## Mapa de capas

```
dataway-analysis/
├── README.md                                 ← este archivo
├── 00-relacion-con-case-seed.md              ← frontera con tools/case-seed/
├── 00-modelo-identidad-idstock.{yml,md}      ← CORE: por qué IdStock no es estable
│
├── 01-operaciones-que-mutan-idstock/         ← (sub-wave siguiente)
│   catálogo de las 12+ operaciones que mutan el IdStock
│
├── 02-particionamiento-licencia/             ← (sub-wave siguiente)
│   cómo una licencia XY termina asociada a N idstocks
│
├── 03-tablas-de-trazabilidad/                ← (sub-wave siguiente)
│   inventario de tablas y vistas relevantes
│
├── 04-ecuacion-de-balance/                   ← INCLUIDO EN ESTE PUSH
│   ├── modelo-conceptual.{yml,md}
│   ├── granularidad-y-keys.{yml,md}
│   ├── tipos-tarea-relevantes.{yml,md}
│   ├── anti-patron-modo-depuracion.{yml,md}
│   └── divergencia-reportes-paralelos.{yml,md}
│
├── 05-flujo-e2e-health-check/                ← (sub-wave siguiente)
│   guion paso a paso del health check OC → despacho
│
├── 06-blockchain-deseado/                    ← (sub-wave siguiente)
│   propuesta del linaje linkeado de IdStock
│
└── 07-correlacion-codigo-data/               ← INCLUIDO EN ESTE PUSH (parcial)
    └── case-pointers/                        ← inventario de hardcodes forenses
        ├── 00-INDEX.md
        └── 01-stockfecha-codigo-030772033524.{yml,md}
```

## Estado actual (Wave 13 sub-wave 13-1)

**Incluido en este push**:
- README + relación con case-seed + modelo de identidad de IdStock
- Capa 04 completa (5 unidades de contenido sobre la ecuación de balance, basadas en lectura de `frmStockEnUnaFecha.vb` y `frmMovimiento_Reporte.vb`)
- Capa 07 con el primer case-pointer documentado completo como template

**Pendiente (sub-waves siguientes)**:
- Capa 01: catálogo exhaustivo de operaciones que mutan IdStock (recepción, cambio ubicación HH/BOF, implosión, ajustes, reubicación parcial, UBIC parcial HH, splits físicos de Reabasto/MI3, picking, despacho)
- Capa 02: modelo de particionamiento de licencia
- Capa 03: tablas de trazabilidad (`stock`, `stock_res`, `trans_movimientos`, `Diferencias_movimientos`, vistas `VW_*`)
- Capa 05: guion E2E del health check
- Capa 06: propuesta de blockchain de IdStock
- Capa 07: barrido completo de case-pointers (estimado 30-50)

## Hallazgos abiertos (registrados en este push)

| ID | Severidad | Estado | Resumen |
|---|---|---|---|
| `V-DATAWAY-001` | alta | pendiente-investigacion | `Llena_Grid` en `ModoDepuracion=True` muta `trans_movimientos` (Actualizar/Eliminar) y borra todo `Diferencias_movimientos` previo. Anti-patrón de inmutabilidad histórica. |
| `V-DATAWAY-002` | alta | pendiente-investigacion | Divergencia genética entre `frmStockEnUnaFecha` y `frmMovimiento_Reporte`: matching cascada distinto + guard `IdMovimiento` abandonado. |
| `V-DATAWAY-003` | media | abierto | TipoTarea no reconocido es silenciosamente ignorado (`Else: Debug.Print`) — riesgo de subestimar balance si se introduce nuevo TipoTarea. |
| `V-DATAWAY-004` | alta | abierto | `frmMovimiento_Reporte` L201 suma `ObjM.Salidas` (campo derivado) en vez de `ObjM.Cantidad` (bruta). Probable bug de salidas en cero. |
| `V-DATAWAY-005` | media | abierto | `Get_Lista_Movimientos` declara tipo `clsBeVW_Movimientos` pero la fuente real es `trans_movimientos` (tabla, no vista). Decisión de eficiencia documentada por Erik (la vista nació para evitar Get adicionales por LN). |

## Convenciones del folder

- **Prefijo de bugs**: `V-DATAWAY-NNN` para bugs sistémicos del balance/datos. Bugs locales de un módulo siguen su prefijo (`V-MI3-NNN`, `V-FROMR-NNN`, etc).
- **Prefijo de case-pointers**: `CP-NNN-<contexto>-<hint>`, ej: `CP-001-stockfecha-codigo-030772033524`.
- **Refs a código legacy**: siempre `<repo>/<path>:<line>` (ej: `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Stock_En_Una_Fecha/frmStockEnUnaFecha.vb:329`).
- **Refs a queries SQL**: cuando se den queries ejecutables en `.yml`, marcarlas siempre como `read_only: true` y nunca ejecutables sin parámetros.

## Vínculos al resto del brain

- `brain/_index/DISCOVERY_TREE.md` — mapa global del brain
- `brain/agent-context/glossary.md` — glosario completo
- `brain/agent-context/CASE_INTAKE_TEMPLATE.md` — template de captura de caso (armonizar con `tools/case-seed/templates/CASE_INTAKE_TEMPLATE.md`)
- `brain/entities/modules/reservation/legacy-process-flow/` — inventario de funciones del pipeline de reserva (Waves 12B-12C). Cross-ref con `01-operaciones-que-mutan-idstock/09-reserva-split-fisico-reabasto.md` (pendiente).
