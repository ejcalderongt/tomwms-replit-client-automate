# Capa 04 / TipoTarea relevantes para el balance

> Catálogo de los `clsDataContractDI.tTipoTarea` que el balance reconoce, los que ignora deliberadamente, y los que ignora silenciosamente (riesgo).

## Catálogo completo

| TipoTarea | Significado | Reconocido por balance | Suma a | Notas |
|---|---|---|---|---|
| `INVE` | Inventario inicial / conteo | ✅ | `Inventario_Inicial` | Acumula `ObjM.Cantidad` |
| `RECE` | Recepción / ingreso de mercadería | ✅ | `Ingresos` | Acumula `ObjM.Cantidad`. Guard `IdMovimiento` comentado en frmMovimiento_Reporte. |
| `AJCANTP` | Ajuste cantidad positiva | ✅ | `Ajustes_Positivos` | Acumula `ObjM.Cantidad` |
| `AJCANTPI` | Ajuste cantidad positiva inverso | ✅ | `Ajustes_Positivos` | Acumula `ObjM.Cantidad`. **¿Por qué "inverso" suma positivo?** Pendiente confirmar — hipótesis: revertir un ajuste negativo previo equivale a sumar. |
| `AJCANTN` | Ajuste cantidad negativa | ✅ | `Ajustes_Negativos` | Acumula `ObjM.Cantidad` |
| `AJCANTNI` | Ajuste cantidad negativa inverso | ✅ | `Ajustes_Negativos` | Acumula `ObjM.Cantidad`. Mismo caveat que AJCANTPI. |
| `DESP` | Despacho / salida | ✅ | `Salidas` | Acumula `ObjM.Salidas` (NO `ObjM.Cantidad`). Guard `IdMovimiento` comentado en frmMovimiento_Reporte. |
| `UBIC` | Cambio de ubicación | ⚠ silenciosamente ignorado | (ninguno) | Cae en el `Else` de `Debug.Print`. Es **correcto** que no sume al balance (cambio interno no genera ingreso ni salida). Pero por construcción del `If/ElseIf`, no se distingue de "TipoTarea desconocido". |
| (otros) | TipoTarea no listado | ⚠ silenciosamente ignorado | (ninguno) | `V-DATAWAY-003`. |

## Cita textual del switch

`frmStockEnUnaFecha.vb:173-189`:

```vb
If ObjM.TipoTarea = clsDataContractDI.tTipoTarea.INVE Then
    If BeStockEnFecha.Codigo = ObjM.Codigo And BeStockEnFecha.Cantidad = 0 Then
        BeStockEnFecha.Inventario_Inicial += ObjM.Cantidad
    End If
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE Then
    BeStockEnFecha.Ingresos += ObjM.Cantidad
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTP OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTPI Then
    BeStockEnFecha.Ajuste_Positivo += ObjM.Cantidad
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTN OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI Then
    BeStockEnFecha.Ajuste_Negativo += ObjM.Cantidad
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
    BeStockEnFecha.Salidas += ObjM.Salidas
Else
    Debug.Print(ObjM.TipoTarea)
End If
```

## Atención al guard de INVE en `frmStockEnUnaFecha`

```vb
If ObjM.TipoTarea = clsDataContractDI.tTipoTarea.INVE Then
    If BeStockEnFecha.Codigo = ObjM.Codigo And BeStockEnFecha.Cantidad = 0 Then
        BeStockEnFecha.Inventario_Inicial += ObjM.Cantidad
    End If
```

**Guard**: solo suma INVE si `BeStockEnFecha.Cantidad = 0`. Esto significa que **solo se suma el primer INVE** que llega para ese punto (los siguientes con `Cantidad > 0` son ignorados).

**Hipótesis del por qué**: en una corrida de inventario inicial, debería haber un solo movimiento INVE por (Codigo, Estado, Lote, Fecha_Vence) que establece el punto de partida. Si llegan múltiples, los siguientes son re-conteos del mismo y no deben sumarse.

**Riesgo**: si el cliente hace **inventarios cíclicos** (recontar periódicamente), el segundo INVE sería ignorado silenciosamente. Esto puede explicar gaps observados.

**Pendiente confirmar**: validar comportamiento en cliente con inventarios cíclicos.

## Diferencia con `frmMovimiento_Reporte`

`frmMovimiento_Reporte.vb:188-201` tiene el mismo switch, **pero con diferencias**:

1. **Guard de INVE más permisivo**:
   ```vb
   If BeStockEnFecha.Codigo = ObjM.Codigo And BeStockEnFecha.Cantidad = 0 Then
       BeStockEnFecha.Inventario_Inicial += ObjM.Cantidad
   End If
   ```
   (Mismo guard que frmStockEnUnaFecha — esta línea es idéntica.)

2. **Guard `IdMovimiento` para RECE y DESP comentado** (líneas 178 y 200):
   ```vb
   'ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE And BeStockEnFecha.IdMovimiento <> ObjM.IdMovimiento Then
   'ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP And BeStockEnFecha.IdMovimiento <> ObjM.IdMovimiento Then
   ```
   Lo que **estaba** intentando: evitar contar dos veces el mismo movimiento (anti-doble-conteo).
   Lo que **quedó**: sin guard, suma todo.

   **Implicación**: si por algún motivo `Get_Lista_Movimientos` retorna duplicados (por ejemplo: misma fila vista a través de dos vistas paralelas que se unen en LINQ), los duplicados se suman dos veces. Esto puede explicar gaps positivos en `Ingresos` y `Salidas`.

   **Pendiente confirmar**: ¿por qué se abandonó el guard? Hipótesis: porque el `IdMovimiento` cambió de semántica (¿de PK del movimiento a PK del Lote?) y ya no funcionaba como anti-doble-conteo.

3. **`V-DATAWAY-004`** en línea 201 (ya documentado en `granularidad-y-keys.md` y `divergencia-reportes-paralelos.md`).

## Catálogo completo del enum `tTipoTarea`

**Pendiente extraer del código fuente**. Probable ubicación:

- `TOMWMS_BOF/TOMIMSV4/Entity/clsDataContractDI.vb`
- O dentro de `TOMWMS_BOF/TOMIMSV4/DataContract/`

Acción sugerida (sub-wave siguiente): hacer `rg "tTipoTarea\s+As\s+Enum|Public\s+Enum\s+tTipoTarea"` y catalogar **todos** los valores del enum, marcando para cada uno:
- ¿está en el switch del balance?
- ¿es neutro deliberadamente?
- ¿es candidato a futuro bug si aparece?

## Recomendaciones forenses

1. **Si el caso reportado es gap negativo** (faltan unidades): verificar si hay TipoTarea no en el switch que sumaría a Salidas. Query en `recomendaciones-forenses.md` (pendiente).

2. **Si el caso reportado es gap positivo** (sobran unidades): verificar si frmMovimiento_Reporte está siendo usado (vs frmStockEnUnaFecha) y hay duplicados de RECE/DESP por el guard `IdMovimiento` abandonado.

3. **Si el cliente tiene inventarios cíclicos**: validar el guard de INVE — múltiples INVE para el mismo punto se ignoran silenciosamente. Esto puede explicar el gap.

## Cross-refs

- `modelo-conceptual.md` — fórmula completa del balance
- `divergencia-reportes-paralelos.md` — `V-DATAWAY-002` y `V-DATAWAY-004`
- `tools/case-seed/queries/data-discrepancy/` — query 03 muestra cuántos movimientos hay por TipoTarea
