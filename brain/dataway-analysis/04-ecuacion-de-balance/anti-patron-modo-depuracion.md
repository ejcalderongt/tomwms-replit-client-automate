# Capa 04 / Anti-patrón: ModoDepuracion muta historia

> **Bug `V-DATAWAY-001` (severidad alta)**: el reporte de inventario teórico, cuando se activa `ModoDepuracion`, deja de ser un reporte y se convierte en una herramienta de mutación destructiva sobre `trans_movimientos` y `Diferencias_movimientos`. Rompe la inmutabilidad histórica del cardex y opera sin trazabilidad de quién hizo el ajuste ni por qué.

## TL;DR

| Aspecto | Valor |
|---|---|
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Stock_En_Una_Fecha/frmStockEnUnaFecha.vb` |
| Función | `Llena_Grid` |
| Activación | Tecla `Ctrl+D` desde el formulario abierto (línea 981-987) |
| Aviso al usuario | `MsgBox("Modo depuración activado, tenga cuidado...")` |
| Default | `Public Property ModoDepuracion As Boolean = False` (línea 16) |
| Persistencia entre sesiones | No (se reinicia al abrir el formulario) |
| Bypass de auditoría | Sí — modifica `trans_movimientos` directamente |
| Marker de la mutación | `M.Serie = "#EJCAJUSTEDESFASE"` (línea 322 del bloque) |
| Bug ID | `V-DATAWAY-001` |
| Severidad | Alta |
| Estado | Pendiente investigación |

## Activación

`frmStockEnUnaFecha.vb:981-987`:

```vb
Private Sub frmStockEnUnaFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    If e.Control AndAlso e.KeyCode = Keys.D Then
        ModoDepuracion = True
        MsgBox("Modo depuración activado, tenga cuidado...", MsgBoxStyle.Information, Text)
    End If
End Sub
```

**Lectura**: el usuario abre el reporte, presiona `Ctrl+D`, ve un MsgBox de advertencia, y a partir de ese momento (hasta cerrar el formulario) **toda corrida del reporte muta datos**. No hay desactivación explícita — solo se desactiva al cerrar el form.

## Las 3 mutaciones que hace en `Llena_Grid`

### Mutación 1: borrar TODO `Diferencias_movimientos`

`frmStockEnUnaFecha.vb:315-321`:

```vb
If ModoDepuracion Then
    vIdDiferencia = clsLnDiferencias_movimientos.MaxID() + 1
    clsLnDiferencias_movimientos.EliminarTodos()
End If
```

**Lectura**: al inicio de cada corrida del reporte en ModoDepuracion, se borra TODA la tabla `Diferencias_movimientos`. No hay filtro por fecha, por producto ni por bodega — borra todo.

**Implicación**: la tabla `Diferencias_movimientos` **no es histórica**. Cualquier registro de gap detectado en una corrida previa se pierde al correr el reporte de nuevo en ModoDepuracion. Si dos analistas corren el reporte en paralelo (uno con ModoDepuracion, otro sin), el segundo no verá los gaps que el primero detectó.

### Mutación 2: insertar a `Diferencias_movimientos`

`frmStockEnUnaFecha.vb:449-471`:

```vb
If Diferencia <> 0 AndAlso ModoDepuracion Then

    Debug.Print("Why difference is Not 0? at: " & Obj.Codigo)

    vExistenciaSinEstado = pBeStock.Cantidad

    BeDiferencia.IdDiferencia = vIdDiferencia
    BeDiferencia.IdProductoBodega = Obj.IdProductoBodega
    BeDiferencia.Codigo = Obj.Codigo
    BeDiferencia.Nombre = Obj.Producto
    BeDiferencia.Lote = Obj.Lote
    BeDiferencia.IdProductoEstado = Obj.IdEstadoOrigen
    BeDiferencia.Estado = Obj.EstadoOrigen
    BeDiferencia.FechaVence = Obj.Fecha_Vence
    BeDiferencia.InventarioInicial = Obj.Inventario_Inicial
    BeDiferencia.Ingresos = Obj.Ingresos
    BeDiferencia.AjustesPositivos = Obj.AjustePositivo
    BeDiferencia.AjustesNegativos = Obj.AjusteNegativo
    BeDiferencia.Salidas = Obj.Salidas
    BeDiferencia.ExistenciaAl = ExistenciasAl
    BeDiferencia.ExistenciaActual = ExistenciaActualConFechaYEstado
    BeDiferencia.ExistenciaSinEstado = vExistenciaSinEstado
    BeDiferencia.Diferencia = Diferencia
    clsLnDiferencias_movimientos.Insertar(BeDiferencia)

    vIdDiferencia += 1
```

**Lectura**: por cada punto del balance con gap ≠ 0, inserta un registro a `Diferencias_movimientos` con todos los componentes del balance + ExistenciaActual + Diferencia.

**Aspecto positivo**: este insert **no es destructivo** — registra la diferencia para análisis posterior.

**Aspecto negativo**: no se asocia un usuario (`IdUsuario`), ni una fecha de auditoría (`FechaAuditoria`), ni una sesión. **No hay trazabilidad de quién corrió el reporte que generó ese registro**.

### Mutación 3: la peligrosa — `trans_movimientos` Eliminar/Actualizar

`frmStockEnUnaFecha.vb:401-435`:

```vb
If Diferencia <> 0 AndAlso ModoDepuracion Then

    If ExistenciasAl < 0 AndAlso ExistenciaActualConFechaYEstado = 0 Then

        lMovimientos = clsLnTrans_movimientos.Get_Movimiento_Despacho_By_Stock(pBeStock)

        If Not lMovimientos Is Nothing Then

            If lMovimientos.Count > 0 Then

                For Each M In lMovimientos

                    If Diferencia <> 0 Then

                        Debug.Print("IdMovimiento: " & M.IdMovimiento & " " & M.Cantidad)

                        If M.Cantidad >= Math.Abs(Diferencia) Then
                            M.Cantidad += Diferencia
                            Diferencia += 1                ← ¿bug? debería ser += M.Cantidad ?
                        Else
                            M.Cantidad = 0
                            Diferencia += M.Cantidad       ← M.Cantidad ya es 0, no suma nada
                        End If

                        M.Serie = "#EJCAJUSTEDESFASE"

                        If M.Cantidad = 0 Then
                            clsLnTrans_movimientos.Eliminar(M)
                        Else
                            clsLnTrans_movimientos.Actualizar(M)
                        End If

                        lblPrg.Text = "Ajustando: " & Obj.Codigo & " Cantidad: " & Diferencia
                        lblPrg.Refresh()

                    End If

                Next

            End If
        ...
```

**Lectura paso a paso**:

1. **Condición de entrada**: `ExistenciasAl < 0 AndAlso ExistenciaActualConFechaYEstado = 0`. Es decir: el balance teórico dio negativo pero la existencia real es 0. Esto significa que en `trans_movimientos` hay más Salidas que Ingresos+Inicial+Ajustes — el cardex está "sobre-despachado".

2. **Get_Movimiento_Despacho_By_Stock**: trae los movimientos DESP relevantes para ese punto.

3. **Para cada DESP M**:
   - Si `M.Cantidad >= |Diferencia|`: ajusta el movimiento `M.Cantidad += Diferencia` (Diferencia es negativa porque ExistenciasAl < 0). Esto **disminuye** la cantidad del DESP histórico para que el balance cuadre.
   - Si `M.Cantidad < |Diferencia|`: pone `M.Cantidad = 0`.

4. **Marker**: `M.Serie = "#EJCAJUSTEDESFASE"`. Este es el único rastro de que el movimiento fue tocado por el ModoDepuracion.

5. **Persistencia**:
   - Si `M.Cantidad = 0` → `Eliminar(M)` (DELETE de `trans_movimientos`).
   - Sino → `Actualizar(M)` (UPDATE de `trans_movimientos`).

**Bug aritmético sospechoso**: `Diferencia += 1` en línea 416. Si la intención era "consumí toda la diferencia con este movimiento", debería ser `Diferencia += Math.Abs(M.Cantidad_consumida)` — no `+= 1`. Esto puede dejar `Diferencia` perpetuamente cerca de 0 sin nunca llegar, y el loop sigue mutando movimientos. **Pendiente confirmar con caso reproducible**. Lo registro como `V-DATAWAY-001a` (sub-bug del 001).

## Por qué esto es un anti-patrón sistémico

### 1. Rompe la inmutabilidad del cardex

`trans_movimientos` debería ser **append-only** para preservar la auditoría histórica. Cualquier herramienta que lo modifique destruye la capacidad de reconstruir "qué pasó realmente" cuando hay disputa.

### 2. Sin auditoría de quién y cuándo

El único rastro es `Serie = "#EJCAJUSTEDESFASE"`. No hay:
- `FechaAuditoria`
- `UsuarioAuditoria`
- `MotivoAjuste`
- `ValorOriginal` (lo que era antes del ajuste)

Si mañana un cliente disputa "¿por qué este movimiento dice 50 cuando yo despaché 100?", no hay forma de reconstruir cuándo cambió ni quién lo hizo.

### 3. El marker es un hardcode con iniciales personales

`"#EJCAJUSTEDESFASE"`: el `EJC` son las iniciales de Erik José Calderón. Esto sugiere que la herramienta fue construida por Erik para resolver un caso específico y quedó en producción — **sin que necesariamente sea política aprobada del equipo**. Caso clásico de **case-pointer histórico**: hardcode personal que sobrevivió.

### 4. La condición de entrada es muy estrecha

Solo entra al loop si `ExistenciasAl < 0 AndAlso ExistenciaActualConFechaYEstado = 0`. Esto cubre **un solo escenario**: balance teórico negativo y existencia real cero. Otros escenarios de gap (positivos, o negativos con existencia > 0) **no son tocados**. Esto sugiere que la herramienta nació para resolver un patrón muy específico, no como solución general.

### 5. El loop puede mutar múltiples movimientos por un solo gap

Si la diferencia es grande, el loop itera por todos los DESP y va aplicando ajustes acumulativos. Si hay 50 DESP y la diferencia es -1000, modifica los 50 hasta que la diferencia llegue a 0 (o "casi 0" por el bug `+= 1`). **Esto puede tocar movimientos legítimos antiguos sin razón clara**.

## Recomendaciones (no implementadas)

### R1: deprecar `ModoDepuracion` (corto plazo)

Comentar todo el bloque y exigir que cualquier ajuste se haga vía AJCANTP/AJCANTN movimientos nuevos (que son auditables y no destruyen historia).

### R2: si se mantiene, agregar auditoría

```vb
Dim audit As New clsBeAuditoriaAjuste With {
    .IdMovimientoOriginal = M.IdMovimiento,
    .CantidadOriginal = M.Cantidad,
    .CantidadNueva = nueva,
    .Diferencia = Diferencia,
    .IdUsuario = AP.IdUsuario,
    .FechaAuditoria = Now,
    .Motivo = "ModoDepuracion automatico desde frmStockEnUnaFecha"
}
clsLnAuditoriaAjustes.Insertar(audit)
```

### R3: cambiar de DELETE a soft-delete

Si `M.Cantidad = 0`, no hacer `Eliminar(M)`. Marcar `M.Anulado = True` y dejar la fila para auditoría.

### R4: NO borrar `Diferencias_movimientos` en `EliminarTodos()`

Filtrar por fecha de corrida o por sesión de auditoría. La tabla debería ser histórica.

### R5: fix del bug aritmético `+= 1`

Reemplazar:
```vb
M.Cantidad += Diferencia
Diferencia += 1
```
por:
```vb
Dim consumido = Math.Min(M.Cantidad, Math.Abs(Diferencia))
M.Cantidad += Diferencia
Diferencia += consumido   ' quitar lo consumido (signo opuesto)
```

(Asumiendo signos coherentes — revisar con casos de prueba.)

## Acción inmediata sugerida

1. Confirmar con Erik: ¿el `ModoDepuracion` aún se usa? Si sí, ¿quién lo usa? Si nadie, deprecar de inmediato.
2. Auditar la tabla `trans_movimientos` para detectar registros con `Serie = "#EJCAJUSTEDESFASE"` y dimensionar el impacto histórico:
   ```sql
   SELECT COUNT(*) cnt, MIN(Fecha) primer, MAX(Fecha) ultimo
   FROM trans_movimientos
   WHERE Serie = '#EJCAJUSTEDESFASE'
   ```
   (A agregar a `tools/case-seed/queries/data-discrepancy/` como nuevo query.)

## Cross-refs

- `modelo-conceptual.md` — fórmula de balance
- `divergencia-reportes-paralelos.md` — `frmMovimiento_Reporte` no tiene esta lógica (no muta historia)
- `dataway-analysis/00-modelo-identidad-idstock.md` — la mutación de `trans_movimientos` también afecta la inferencia de linaje del IdStock
- `tools/case-seed/queries/data-discrepancy/` — pendiente agregar query `06_movimientos_ejcajustedesfase.sql`
