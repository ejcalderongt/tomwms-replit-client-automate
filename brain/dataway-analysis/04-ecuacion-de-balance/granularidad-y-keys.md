# Capa 04 / Granularidad y keys del balance

> Qué constituye "un punto" del balance — las keys que definen cuándo dos movimientos suman al mismo punto vs a puntos diferentes.

## Tesis

La ecuación de balance no opera sobre el **producto entero** ni sobre el **idstock**. Opera sobre un **agregado intermedio** definido por una tupla de keys. La elección de qué keys forman parte de esta tupla es **donde se rompe la coherencia entre reportes paralelos** y donde aparecen las clases de bug que `V-DATAWAY-002` documenta.

## Keys del agregado en `frmStockEnUnaFecha` (canónico)

### Matching durante la iteración (`Generar_Reporte`)

Al iterar movimientos uno por uno, el código busca el "punto existente" usando una key de **3 componentes** (líneas 113-117 aprox):

```vb
Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                  AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen _
                                  AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)
```

**Key de matching = `(Codigo, IdEstadoOrigen, Fecha_Vence)`** — 3 componentes.

### Group By de visualización (`Llena_Grid`)

Pero al armar la grilla final que se muestra al usuario, hay un **Group By de 10 componentes** (líneas 276-285):

```vb
ListaResumen = ListaTodos.GroupBy(Function(x) New With { _
                Key x.IdProductoBodega, _
                Key x.Codigo, _
                Key x.Producto, _
                Key x.UMBas, _
                Key x.EstadoOrigen, _
                Key x.IdEstadoOrigen, _
                Key x.IdPresentacion, _
                Key x.IdUnidadMedida, _
                Key x.Lote, _
                Key x.Fecha_Vence }) ...
```

**Key de visualización = `(IdProductoBodega, Codigo, Producto, UMBas, EstadoOrigen, IdEstadoOrigen, IdPresentacion, IdUnidadMedida, Lote, Fecha_Vence)`** — 10 componentes.

### El problema: 3 keys vs 10 keys

```text
Matching usado para acumular (3 keys):
    Codigo + IdEstadoOrigen + Fecha_Vence

Group By final que ve el usuario (10 keys):
    + IdProductoBodega + Producto + UMBas + IdPresentacion 
    + IdUnidadMedida + Lote + EstadoOrigen
```

**Implicación crítica**: dos movimientos del mismo `(Codigo, IdEstadoOrigen, Fecha_Vence)` **se acumulan en el mismo "punto"** durante la iteración, aunque tengan **distinto Lote, distinta IdPresentacion o distinta IdUnidadMedida**. Después, el Group By de 10 keys los vuelve a separar al armar la grilla.

¿Por qué importa? Porque las cantidades acumuladas durante la iteración (`Inventario_Inicial`, `Ingresos`, etc) **podrían estar agregando movimientos que pertenecen a Lotes distintos** y luego repartirse mal entre las filas del Group By final.

**Hipótesis de fix**: usar la misma key de 10 componentes en el matching de `FindIndex`. Pero el código no lo hace — probablemente porque la complejidad de la cascada de matching aumenta exponencialmente (ya frmMovimiento_Reporte intentó esto y rompió de otra forma — ver `divergencia-reportes-paralelos.md`).

**Estado**: hipótesis no confirmada. Requiere caso reproducible con producto que tenga Lote variando dentro del mismo `(Codigo, IdEstadoOrigen, Fecha_Vence)`. Posible test: `tools/case-seed/queries/data-discrepancy/` con producto multi-lote.

## Keys del agregado en `frmMovimiento_Reporte` (divergente)

### Matching cascada (líneas 113-148 aprox)

`frmMovimiento_Reporte` no usa una key fija. Usa una **cascada de hasta 4 niveles**:

```text
Nivel 1: matching exacto Codigo + IdEstadoOrigen + Fecha_Vence
    ├─ encuentra (Idx != -1)
    │  └─ Nivel 1.1: refina por Lote
    │       Codigo + Lote + Fecha_Vence
    │       ├─ encuentra (Idx1 != -1) -> usar Idx (matching original)
    │       └─ no encuentra (Idx1 = -1) -> 
    │              Si BeStockEnFecha.Lote = "" -> 
    │                  Nivel 1.1.1: refinar sin Lote
    │                      Codigo + Fecha_Vence
    │                      ├─ no encuentra -> Idx = -1 (descarta match)
    │                      └─ encuentra -> 
    │                          Nivel 1.1.1.1: refinar con IdEstadoOrigen
    │                          (resultado: Idx = Idx1)
    │
    └─ no encuentra (Idx = -1)
       └─ Nivel 2: matching solo Codigo + Fecha_Vence
            ├─ encuentra -> matching debil (con comment "Magia por EJC para corregir cagada")
            └─ no encuentra -> Idx = -1 (Add nuevo)
```

**Implicaciones**:
1. **El matching no es determinístico** en el sentido tradicional: dos corridas del mismo reporte sobre los mismos datos producen el mismo resultado, pero el **conjunto de keys** que define un punto **varía según los datos previos** procesados (porque el `RepMovEnUnaFecha` se va llenando incrementalmente).
2. La cascada existe para "corregir" un bug histórico: comments mencionan "(Por error en el cambio de ubicación fecha_vence = now -> JP)" — bug histórico de JP donde el cambio de ubicación seteaba `fecha_vence = now`.
3. La complejidad agrega bugs nuevos: `V-DATAWAY-004` (línea 201, suma `ObjM.Salidas` en vez de `ObjM.Cantidad`).

## El bug histórico "fecha_vence = now" de JP

**Comment textual** (`frmMovimiento_Reporte.vb:118-120`):
```vb
If Idx1 = -1 Then 'No coincide la fecha de vencimiento para el mismo lote en el mismo movimiento
    '(Por error en el cambio de ubicación fecha_vence = now -> JP.)
    Debug.Print("Espera")
```

**Interpretación**: en algún momento, una operación de cambio de ubicación (probablemente de la HH) seteaba `Fecha_Vence = Now` en lugar de respetar la fecha de vencimiento original del producto. Esto generaba filas en `trans_movimientos` con fecha de vencimiento "incorrecta", y al armar el cardex el matching exacto fallaba.

**Solución pragmática del autor de `frmMovimiento_Reporte`**: cascada con fallback "matching por Lote en vez de por Fecha_Vence" + comment "Magia por EJC para corregir cagada" (línea 138).

**Estado actual**:
- ¿El bug de JP se arregló en el código fuente que generaba esos movimientos? **Pendiente confirmar con Erik**.
- Si NO se arregló: la "magia" de frmMovimiento_Reporte es necesaria para clientes con datos legacy.
- Si SÍ se arregló: la "magia" es deuda muerta que confunde al lector.

**Esto es un case-pointer histórico**: documentar como `CP-002-fecha-vence-now-jp-bug` en `07-correlacion-codigo-data/case-pointers/` (sub-wave siguiente).

## Tabla resumen de keys

| Reporte | Matching durante iteración | Group By final visualización |
|---|---|---|
| `frmStockEnUnaFecha` | 3 keys: `Codigo + IdEstadoOrigen + Fecha_Vence` | 10 keys (incluye IdProductoBodega, IdPresentacion, IdUnidadMedida, Lote) |
| `frmMovimiento_Reporte` | Cascada variable (1-4 niveles) con keys `{Codigo, IdEstadoOrigen, Fecha_Vence, Lote}` en distintas combinaciones | (Pendiente confirmar Group By final — probable que sea similar) |

## Recomendaciones para análisis forense

Cuando un caso reportado muestra gap inexplicable:

1. **Verificar si el producto tiene Lote varying dentro de `(Codigo, IdEstadoOrigen, Fecha_Vence)`**. Si sí, aplicar el caveat de matching de 3 keys vs Group By de 10 keys.

2. **Verificar si hay movimientos UBIC con `Fecha_Vence = Now`** (residuo del bug de JP). Query sugerida (a agregar a `tools/case-seed/queries/data-discrepancy/05_ubic_fecha_vence_now.sql`):
   ```sql
   SELECT TOP 100 *
   FROM trans_movimientos
   WHERE TipoTarea = 'UBIC'
     AND CAST(Fecha_Vence AS DATE) = CAST(Fecha AS DATE)  -- fecha vence == fecha movimiento
     AND IdProducto = @IdProducto
     AND Fecha BETWEEN @From AND @To
   ORDER BY Fecha
   ```
   (Asumiendo schema; ajustar según cliente.)

3. **Verificar TipoTarea no listado en el `If/ElseIf`**. Query sugerida:
   ```sql
   SELECT TipoTarea, COUNT(*) cnt
   FROM trans_movimientos
   WHERE IdProducto = @IdProducto
     AND Fecha BETWEEN @From AND @To
     AND TipoTarea NOT IN ('INVE','RECE','AJCANTP','AJCANTPI','AJCANTN','AJCANTNI','DESP','UBIC')
   GROUP BY TipoTarea
   ```

## Cross-refs

- `modelo-conceptual.md` — fórmula de balance
- `divergencia-reportes-paralelos.md` — `V-DATAWAY-002`
- `07-correlacion-codigo-data/case-pointers/` — pendiente `CP-002-fecha-vence-now-jp-bug`
- `tools/case-seed/queries/data-discrepancy/` — extensiones sugeridas
