# Capa 04 / Divergencia entre reportes paralelos

> **Bug `V-DATAWAY-002` (severidad alta)**: existen al menos dos reportes que pretenden calcular el mismo balance teórico — `frmStockEnUnaFecha` (canónico) y `frmMovimiento_Reporte` (cuasi-clon experimental). Tienen lógica divergente. Si el equipo de soporte usa uno o el otro según preferencia personal, el resultado puede ser distinto para el mismo caso.

## TL;DR

| Aspecto | `frmStockEnUnaFecha.vb` | `frmMovimiento_Reporte.vb` |
|---|---|---|
| Ubicación | `Reportes/Stock_En_Una_Fecha/` | `Reportes/Fiscales/` |
| Propósito declarado | Inventario teórico al cierre | Reporte fiscal de movimientos |
| Lógica de matching | Exacto 3 keys | Cascada 1-4 niveles |
| Guard `IdMovimiento` (anti-doble-conteo) | Ausente (nunca existió) | **Comentado** (existió y se abandonó) |
| ModoDepuracion (muta historia) | Sí (`V-DATAWAY-001`) | No |
| Bug aritmético en suma de Salidas | No (suma `ObjM.Salidas` desde vista) | **Sí (`V-DATAWAY-004`)** suma `ObjM.Salidas` que puede venir en 0 |
| Cantidad de líneas | ~1111 | ~705 |
| Cita textual de "magia" | No | "Magia por EJC para corregir cagada" |

## La pregunta de fondo

¿Cómo aparecieron dos reportes que calculan lo mismo?

**Hipótesis cronológica**:

1. **T0**: nace `frmStockEnUnaFecha` como reporte canónico de inventario teórico. Lógica simple: matching exacto + acumulación lineal por TipoTarea.

2. **T1**: aparece la necesidad de un reporte **fiscal** (más estricto en granularidad y agrupación, para presentación regulatoria). Se clona `frmStockEnUnaFecha.Generar_Reporte` en `frmMovimiento_Reporte.Generar_Reporte`. La línea 105 del `frmStockEnUnaFecha` parece referenciar un loop muy similar al del `frmMovimiento_Reporte`.

3. **T2**: aparecen casos en producción donde el matching exacto falla (ej: bug de JP, cambio de ubicación con `Fecha_Vence = Now`). En **frmMovimiento_Reporte** se introduce la cascada con fallback ("Magia por EJC para corregir cagada"). En **frmStockEnUnaFecha** no se toca — sigue con matching exacto.

4. **T3**: alguien intenta agregar el guard `IdMovimiento` (anti-doble-conteo) en `frmMovimiento_Reporte`. Se prueba, no funciona como esperado, se comenta. **No se documenta el por qué del abandono**.

5. **T4**: se introduce un bug nuevo en línea 201 de `frmMovimiento_Reporte` (suma `ObjM.Salidas` cuando la fuente probablemente lo trae en 0). Pasa desapercibido porque el reporte se usa poco.

6. **T5 (hoy)**: ambos reportes coexisten. `frmStockEnUnaFecha` es el "default" pero `frmMovimiento_Reporte` está disponible y produce números distintos.

## Las 4 divergencias concretas

### Divergencia 1: lógica de matching

`frmStockEnUnaFecha.vb:113-117` (matching exacto):
```vb
Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                  AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen _
                                  AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)
If Idx = -1 Then
    RepMovEnUnaFecha.Add(BeStockEnFecha)
Else
    BeStockEnFecha = RepMovEnUnaFecha(Idx) 'Puntero =>
End If
```

`frmMovimiento_Reporte.vb:113-148` (cascada 4 niveles):
```vb
Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                  AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen _
                                  AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)

If Idx <> -1 Then 'Lo encontró por lote.
    Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
      AndAlso x.Lote = BeStockEnFecha.Lote _
      AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date)

    If Idx1 = -1 Then 'No coincide la fecha de vencimiento para el mismo lote en el mismo movimiento
        '(Por error en el cambio de ubicación fecha_vence = now -> JP.)
        Debug.Print("Espera")
        ' [...lógica de re-matching cascada...]
    End If

    'Si no tiene control por lote...
    If BeStockEnFecha.Lote = "" Then
        ' [otra cascada para producto sin lote]
    End If

Else
    Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                     AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)

    If Idx <> -1 Then 'Lo encontró por FechaVence.
        Debug.Print(BeStockEnFecha.Codigo)
        ' [matching débil con comment "Magia por EJC para corregir cagada"]
    End If
End If
```

**Implicación**: para el mismo set de movimientos, los dos reportes pueden agrupar los datos en **conjuntos distintos de "puntos"** del balance. Resultado: la suma `Inventario_Inicial + Ingresos + Ajustes` puede repartirse de forma diferente entre las filas finales de la grilla, y la diferencia con `Existencia_Actual` puede aparecer en una fila u otra.

### Divergencia 2: guard `IdMovimiento` abandonado

`frmMovimiento_Reporte.vb:178` (comentado):
```vb
'ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE And BeStockEnFecha.IdMovimiento <> ObjM.IdMovimiento Then
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE Then
    BeStockEnFecha.Ingresos += ObjM.Cantidad
```

`frmMovimiento_Reporte.vb:200` (comentado):
```vb
'ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP And BeStockEnFecha.IdMovimiento <> ObjM.IdMovimiento Then
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
    BeStockEnFecha.Salidas += ObjM.Salidas
```

**Lectura**: alguien intentó evitar contar dos veces el mismo `IdMovimiento` para el mismo punto. Se comentó. `frmStockEnUnaFecha` **nunca tuvo este guard** (no aparece ni comentado).

**Implicación**: si por algún motivo `Get_Lista_Movimientos` retorna duplicados de RECE/DESP (mismo IdMovimiento, dos filas), **ninguno de los dos reportes lo evita**. La diferencia es que `frmMovimiento_Reporte` tiene el remanente del intento de fix.

**Pregunta abierta**: ¿por qué se abandonó? Hipótesis:
- (a) El `IdMovimiento` cambió de semántica (de PK del movimiento a PK del Lote o del Despacho).
- (b) La vista `VW_Movimientos` empezó a generar varias filas con el mismo `IdMovimiento` legítimamente (por presentación, por ubicación) y el guard rompía el conteo correcto.
- (c) Se descubrió que era duplicado falso (el `Get_Lista_Movimientos` ya hacía DISTINCT).

### Divergencia 3: `V-DATAWAY-004` — suma `ObjM.Salidas` en frmMovimiento_Reporte

`frmMovimiento_Reporte.vb:201`:
```vb
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
    BeStockEnFecha.Salidas += ObjM.Salidas
```

**vs `frmStockEnUnaFecha.vb:184-186`**:
```vb
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
    BeStockEnFecha.Salidas += ObjM.Salidas
```

**A primera vista son idénticos**. Pero la diferencia está en **de dónde viene `ObjM`**:

- En `frmStockEnUnaFecha`: `ObjM` viene de `Get_Lista_Movimientos` que **probablemente** consume la vista `VW_Movimientos`. La vista pre-calcula el campo `Salidas` desde `Cantidad` para movimientos DESP. Por eso `ObjM.Salidas` siempre tiene valor coherente para DESP.

- En `frmMovimiento_Reporte`: `ObjM` también viene de `Get_Lista_Movimientos`, **pero pendiente confirmar si es la misma fuente**. Si `frmMovimiento_Reporte` consume una variante distinta (`VW_Movimientos_Poliza` por ej, o directamente la tabla raw `trans_movimientos`), el campo `Salidas` puede venir nulo o en 0.

**Acción para confirmar**:
```bash
# Localizar Get_Lista_Movimientos en cada archivo y ver de qué fuente lee
rg "Get_Lista_Movimientos|Get_All_Movimientos" /tmp/repos/TOMWMS_BOF/TOMIMSV4/
```
(Pendiente sub-wave siguiente.)

**Severidad alta** porque: si `ObjM.Salidas = 0` en frmMovimiento_Reporte, **las salidas nunca se acumulan** y el balance teórico será siempre `Inventario_Inicial + Ingresos + Ajustes_Positivos − Ajustes_Negativos − 0`, que es **artificialmente alto**. Resultado: el reporte fiscal mostraría existencia teórica > existencia real para todo despacho, y los analistas atribuirían a "robo" o "merma" lo que en realidad es **un campo no leído**.

### Divergencia 4: ModoDepuracion ausente

`frmMovimiento_Reporte` **no tiene** `ModoDepuracion`. No muta `trans_movimientos`. No borra `Diferencias_movimientos`. No tiene Ctrl+D. Es un reporte **puro de lectura**.

**Implicación**: el riesgo del `V-DATAWAY-001` (anti-patrón ModoDepuracion) está confinado a `frmStockEnUnaFecha`. Si se quisiera usar un reporte sin riesgo de mutación accidental, `frmMovimiento_Reporte` sería más seguro — **pero introduce las divergencias 1, 2 y 3**.

## Recomendaciones

### R1: deprecar el reporte experimental

`frmMovimiento_Reporte` parece ser un experimento abandonado a medio camino. Deprecar con:

1. Confirmar con el equipo si alguien lo usa hoy.
2. Si nadie lo usa: marcar como obsoleto en el menú, redirigir al canónico.
3. Si alguien lo usa para reporte fiscal: extraer la lógica de Group By fiscal específica a un nuevo reporte, **sin clonar el cuerpo del cálculo**.

### R2: extraer la lógica de cálculo a una clase única

Hoy el cálculo del balance vive **clonado** en dos archivos. Si se arregla un bug en uno, el otro queda con el bug.

Proponer: clase `clsCalculadorBalance` con método `Calcular(ListaMovimientos, FechaCorte) As List(Of clsBeBalancePunto)`. Ambos reportes consumen esta clase y se diferencian solo en cómo presentan el resultado.

### R3: confirmar `V-DATAWAY-004` con datos reales

Antes de marcar como bug confirmado, ejecutar el reporte fiscal sobre un cliente con despachos conocidos y verificar si las salidas suman correctamente. Si suman → el campo `ObjM.Salidas` viene poblado y el bug es teórico. Si no suman → bug confirmado.

Query de verificación (sub-wave siguiente):
```sql
-- Para un IdProducto y rango con despachos conocidos
SELECT Fecha, TipoTarea, Cantidad, Salidas, IdMovimiento
FROM VW_Movimientos
WHERE IdProducto = @IdProducto
  AND TipoTarea = 'DESP'
  AND Fecha BETWEEN @From AND @To
ORDER BY Fecha
```
Verificar si `Salidas` viene poblado para DESP.

### R4: documentar como case-pointer

El comment "(Por error en el cambio de ubicación fecha_vence = now -> JP)" merece un case-pointer dedicado: `CP-002-fecha-vence-now-jp-bug` (sub-wave siguiente).

## Cross-refs

- `modelo-conceptual.md` — fórmula canónica (basada en frmStockEnUnaFecha)
- `granularidad-y-keys.md` — detalle de la cascada de matching de frmMovimiento_Reporte
- `tipos-tarea-relevantes.md` — switch de TipoTarea con guards comentados
- `anti-patron-modo-depuracion.md` — `V-DATAWAY-001` (solo frmStockEnUnaFecha)
- `07-correlacion-codigo-data/case-pointers/` — pendiente `CP-002-fecha-vence-now-jp-bug`
