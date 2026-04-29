# Capa 04 / Modelo conceptual de la ecuación de balance

> La ecuación que reconstruye la existencia teórica de un producto desde sus movimientos, con cita textual al código canónico que la implementa.

## Fórmula canónica

```
Existencia_Teorica(t) = Inventario_Inicial(t)
                      + Ingresos(t)
                      + Ajustes_Positivos(t)
                      − Ajustes_Negativos(t)
                      − Salidas(t)
```

**Comparación con realidad**:

```
Diferencia(t) = Existencia_Teorica(t) − Existencia_Actual(t)
```

Si `Diferencia(t) ≠ 0`, hay **gap** que requiere investigación.

## Cita textual del código canónico

**Archivo**: `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Stock_En_Una_Fecha/frmStockEnUnaFecha.vb`
**Línea**: 329 (dentro de `Sub Llena_Grid()`)

```vb
ExistenciasAl = ((Obj.Inventario_Inicial + Obj.Ingresos + Obj.AjustePositivo) - (Obj.AjusteNegativo + Obj.Salidas))
```

**Variables**:
- `Obj` = instancia de `clsBeStockEnUnaFecha` (un punto de la grilla agregada)
- `ExistenciasAl` = resultado de la ecuación, equivalente a `Existencia_Teorica`

## Por qué esta es la fórmula "canónica" (y no `frmMovimiento_Reporte`)

`frmStockEnUnaFecha.vb` es:
1. El único reporte que **explícitamente** muta `Diferencias_movimientos` cuando detecta gap (ver `anti-patron-modo-depuracion.md`).
2. El reporte más usado por el equipo de soporte cuando un cliente reporta desfase (chat con Erik).
3. El que tiene la lógica de balance más simple (no tiene los guards experimentales de `frmMovimiento_Reporte`).

`frmMovimiento_Reporte.vb` es una **divergencia genética experimental** del mismo cuerpo, con guards adicionales que introdujeron bugs propios. Ver `divergencia-reportes-paralelos.md`.

## Componentes de la ecuación

### Inventario_Inicial
**Origen**: movimientos con `TipoTarea = INVE` antes o en la fecha de corte.
**Lógica de acumulación** (`frmStockEnUnaFecha.vb:173-176`):
```vb
If ObjM.TipoTarea = clsDataContractDI.tTipoTarea.INVE Then
    BeStockEnFecha.Inventario_Inicial += ObjM.Cantidad
End If
```

**Comportamiento**: simplemente acumula `ObjM.Cantidad` por cada movimiento INVE. **Sin guard adicional**.

### Ingresos
**Origen**: movimientos con `TipoTarea = RECE`.
**Lógica de acumulación** (`frmStockEnUnaFecha.vb:177-179`):
```vb
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE Then
    BeStockEnFecha.Ingresos += ObjM.Cantidad
```

**Sin guard adicional** (ojo: línea 178 tiene **comentado** `'ElseIf RECE And BeStockEnFecha.IdMovimiento <> ObjM.IdMovimiento Then` — ver `divergencia-reportes-paralelos.md` para análisis del guard abandonado).

### Ajustes_Positivos
**Origen**: movimientos con `TipoTarea ∈ { AJCANTP, AJCANTPI }`.
**Lógica** (`frmStockEnUnaFecha.vb:180-181`):
```vb
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTP OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTPI Then
    BeStockEnFecha.Ajuste_Positivo += ObjM.Cantidad
```

### Ajustes_Negativos
**Origen**: movimientos con `TipoTarea ∈ { AJCANTN, AJCANTNI }`.
**Lógica** (`frmStockEnUnaFecha.vb:182-183`):
```vb
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTN OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI Then
    BeStockEnFecha.Ajuste_Negativo += ObjM.Cantidad
```

### Salidas
**Origen**: movimientos con `TipoTarea = DESP`.
**Lógica** (`frmStockEnUnaFecha.vb:184-186`):
```vb
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
    BeStockEnFecha.Salidas += ObjM.Salidas
```

**Atención**: suma `ObjM.Salidas`, **no** `ObjM.Cantidad`. En `frmStockEnUnaFecha` esto está estable porque el campo `Salidas` se calcula vía vista `VW_Movimientos` (probable). En `frmMovimiento_Reporte` la misma línea tiene un patrón sospechoso documentado en `V-DATAWAY-004`.

### TipoTarea no reconocido
**Lógica** (`frmStockEnUnaFecha.vb:187-189`):
```vb
Else
    Debug.Print(ObjM.TipoTarea)
End If
```

**Anti-patrón `V-DATAWAY-003` (severidad media)**: si en el futuro se introduce un nuevo TipoTarea (ej: `TRANS` para transferencia entre bodegas), este reporte lo **ignora silenciosamente**. El `Debug.Print` solo aparece en el output del IDE en modo debug, no en el log de producción. Resultado: el balance subestimaría el volumen de movimientos sin que nadie se entere.

**Mitigación propuesta** (no implementada):
```vb
Else
    Throw New NotSupportedException($"TipoTarea no soportada en balance: {ObjM.TipoTarea}")
End If
```
(O loggear a `clsLog` en producción).

### UBIC: el TipoTarea ausente
**Comportamiento**: `UBIC` **no aparece** en el `If/ElseIf` de `frmStockEnUnaFecha.vb`. Esto es **correcto y deliberado**:
- `UBIC` representa cambio de ubicación (movimiento físico interno).
- No suma ni resta cantidad neta.
- Si se contara como ingreso o salida, distorsionaría el balance.

Pero por construcción del `If/ElseIf`, los movimientos UBIC caen en el `Else` final → `Debug.Print(ObjM.TipoTarea)`. **Esto significa que actualmente UBIC y "TipoTarea desconocido" se manejan idéntico**: silencio. Si se quiere distinguir (para alertar de TipoTarea nuevo sin alertar de UBIC), hay que filtrar UBIC explícitamente:

```vb
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.UBIC Then
    ' neutro: no afecta balance
Else
    ' alertar TipoTarea desconocida
End If
```

Ver `tipos-tarea-relevantes.md` para el catálogo completo.

## Una iteración del cuerpo (vista lógica)

```text
Para cada ObjM en ListaMovimientos (ordenado por EstadoOrigen):
    
    1. Localizar el "punto" BeStockEnFecha en RepMovEnUnaFecha que matchee:
        - Codigo + IdEstadoOrigen + Fecha_Vence
        (matching exacto en frmStockEnUnaFecha; cascada compleja en frmMovimiento_Reporte)
    
    2. Si no existe el punto -> agregarlo nuevo
       Si existe -> tomar referencia (puntero)
    
    3. Acumular ObjM.Cantidad/Salidas en el campo correspondiente
       segun TipoTarea (INVE/RECE/AJCANTP*/AJCANTN*/DESP)
    
    4. Continuar con siguiente ObjM
```

## Key del puntero (granularidad de agregación)

Ver `granularidad-y-keys.md` para el detalle. Resumen:
- `frmStockEnUnaFecha`: agrupa por **3 keys mínimas** durante el matching (`Codigo + IdEstadoOrigen + Fecha_Vence`), pero el grid final muestra **10 keys** (incluye Lote, IdPresentacion, IdUnidadMedida, etc).
- `frmMovimiento_Reporte`: matching cascada de hasta 4 niveles, con keys variables según presencia/ausencia de Lote.

## Cross-refs

- `granularidad-y-keys.md` — qué keys define cada "punto" del balance
- `tipos-tarea-relevantes.md` — catálogo completo de TipoTarea
- `anti-patron-modo-depuracion.md` — `V-DATAWAY-001` (ModoDepuracion muta historia)
- `divergencia-reportes-paralelos.md` — `V-DATAWAY-002` y `V-DATAWAY-004` (frmMovimiento_Reporte)
- `dataway-analysis/00-modelo-identidad-idstock.md` — por qué la cantidad agregada por código+lote+fecha+estado no es lo mismo que la suma de cantidades por idstock
- `tools/case-seed/queries/data-discrepancy/02_movements_window.sql` — query SQL que extrae la ventana de movimientos para reproducir esta ecuación localmente
