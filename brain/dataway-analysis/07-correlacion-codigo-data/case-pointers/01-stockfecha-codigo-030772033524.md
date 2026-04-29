# CP-001 — frmStockEnUnaFecha hardcode `Codigo = "030772033524"`

> Hardcode encontrado en `frmStockEnUnaFecha.vb` que apunta a un caso real con producto `030772033524`, fecha de vencimiento `2019-08-30`, EstadoOrigen `SIN REGISTRO` y TipoTarea `DESP`. Es el case-pointer canónico que sirve de template para todos los siguientes.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-001-stockfecha-codigo-030772033524 |
| Estado | documentado (síntoma) — pendiente confirmar bug raíz |
| Severidad | media |
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Stock_En_Una_Fecha/frmStockEnUnaFecha.vb` |
| Líneas | 137-145 (con TheGoalDate definida en línea 92) |
| Fecha del caso (en código) | 2019-08-30 |
| Fecha estimada de aparición | 2019 (basado en TheGoalDate) |
| Wave de descubrimiento | wave-13-1 |

## Cita textual del código

`frmStockEnUnaFecha.vb:92` (declaración de la fecha pivot):
```vb
Dim TheGoalDate As Date = New Date(2019, 8, 30)
```

`frmStockEnUnaFecha.vb:137-145` (el hardcode propiamente):
```vb
If ObjM.Codigo = "030772033524" Then
    Debug.Print("Wait a second!")
End If

If ObjM.Fecha_Vence = TheGoalDate Then
    Debug.Print("Wait a second!")
End If

If ObjM.Fecha_Vence = TheGoalDate AndAlso ObjM.EstadoOrigen = "SIN REGISTRO" AndAlso ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
    Debug.Print("Wait a second!")
End If
```

## Reconstrucción del caso

Lo que el desarrollador estaba debugueando (inferencia):

1. **Producto específico**: `Codigo = "030772033524"`. Probable EAN-13 de un producto del cliente. El primer breakpoint (`If Codigo = X`) dispara solo para este producto durante toda la corrida del reporte.

2. **Fecha de vencimiento específica**: `2019-08-30`. La variable se llama `TheGoalDate` ("la fecha objetivo") — el desarrollador estaba persiguiendo movimientos con vencimiento exactamente en esa fecha. El segundo breakpoint dispara para cualquier producto que tenga esa fecha de vencimiento.

3. **Combinación crítica**: el tercer `If` combina:
   - `Fecha_Vence = TheGoalDate` (2019-08-30)
   - `EstadoOrigen = "SIN REGISTRO"` (estado especial que indica producto sin clasificar de estado)
   - `TipoTarea = DESP` (movimiento de despacho)

   Este es **el caso real** que el desarrollador investigaba: un producto con vencimiento 2019-08-30 que se despachó desde el estado "SIN REGISTRO".

## Por qué es relevante

### Hipótesis del bug raíz

`EstadoOrigen = "SIN REGISTRO"` para un movimiento DESP es **patológico**: significa que se despachó stock que estaba sin estado clasificado. Esto puede ocurrir cuando:

1. **Recepción incompleta**: la mercadería se recibió y se le asignó "SIN REGISTRO" temporalmente, pero el operador la despachó antes de que se clasificara.
2. **Estado borrado**: por alguna razón (bug en cambio de estado HH), el estado del producto se borró y quedó como "SIN REGISTRO".
3. **Importación legacy**: datos importados de un sistema previo donde no había control de estado.

El caso del hardcode probablemente es uno de estos tres.

### Por qué se hardcodeó en un reporte

El reporte `frmStockEnUnaFecha` es el que **detecta** el gap de balance. Si un producto se despachó desde "SIN REGISTRO", probablemente generó un gap (la existencia teórica con estado clasificado vs la real). El desarrollador puso el `If` para parar el debugger justo en ese punto y entender por qué el balance no cuadraba para ese producto en ese día.

## Acción forense

### Query de seguimiento (a agregar a `tools/case-seed/queries/data-discrepancy/`)

```sql
-- 07_movimientos_sin_registro_desp.sql
-- ¿Hay otros productos con DESP desde EstadoOrigen='SIN REGISTRO'?
SELECT TOP 100
    M.Fecha,
    M.IdMovimiento,
    P.Codigo,
    P.Producto,
    M.IdEstadoOrigen,
    EO.Descripcion AS EstadoOrigenDesc,
    M.Cantidad,
    M.Salidas,
    M.Fecha_Vence,
    M.IdStock
FROM trans_movimientos M
INNER JOIN producto P ON P.IdProducto = M.IdProducto
LEFT JOIN producto_estado EO ON EO.IdEstado = M.IdEstadoOrigen
WHERE M.TipoTarea = 'DESP'
  AND EO.Descripcion = 'SIN REGISTRO'
  AND M.Fecha BETWEEN @From AND @To
ORDER BY M.Fecha DESC
```

(Adjustar nombre de columna `Descripcion` según schema del cliente.)

### Verificación específica del caso histórico

```sql
-- 08_caso_cp001_codigo_030772033524.sql
SELECT
    M.Fecha,
    M.IdMovimiento,
    M.TipoTarea,
    M.Cantidad,
    M.Salidas,
    M.Fecha_Vence,
    M.IdStock,
    M.IdEstadoOrigen,
    M.Lote
FROM trans_movimientos M
INNER JOIN producto P ON P.IdProducto = M.IdProducto
WHERE P.Codigo = '030772033524'
  AND M.Fecha_Vence = '2019-08-30'
ORDER BY M.Fecha
```

## Estado del bug

**No confirmado**. Para confirmar:

1. Ejecutar query 08 en el cliente original donde apareció el caso (probable cliente Killios u otro de la primera generación 2019).
2. Si retorna filas: el caso histórico aún tiene rastro en BD. Analizar el contexto (¿se resolvió? ¿quedó como gap permanente?).
3. Si no retorna filas: el caso fue migrado o limpiado. El hardcode es deuda muerta.

## Limpieza recomendada

**Si el bug está resuelto** (cualquiera fuera la razón histórica):

1. Borrar las 3 condiciones `If ... Debug.Print("Wait a second!")` (líneas 137-145).
2. Borrar la declaración `Dim TheGoalDate As Date = New Date(2019, 8, 30)` (línea 92).
3. Documentar la limpieza en `learnings/L-DATAWAY-NNN-cleanup-cp001.md`.

**Si el bug aún ocurre**:

1. Mantener los hardcodes pero agregar comment con `CP-001-stockfecha-codigo-030772033524` para que el próximo lector sepa qué documenta.
2. Implementar fix real (ej: validación que prohíba DESP desde "SIN REGISTRO").
3. Promover a `learnings/L-DATAWAY-NNN-bug-desp-sin-registro.md`.

## Pendientes

- [ ] Confirmar con Erik si recuerda este caso (probable que sí — el código es de él o de alguien cercano).
- [ ] Identificar el cliente donde apareció (probable Killios, primera generación 2019).
- [ ] Ejecutar queries 07 y 08 en BD productiva con `tools/case-seed/`.
- [ ] Decidir si limpiar o mantener.

## Cross-refs

- `00-INDEX.md` — inventario de todos los case-pointers
- `dataway-analysis/04-ecuacion-de-balance/tipos-tarea-relevantes.md` — análisis del TipoTarea DESP y EstadoOrigen "SIN REGISTRO"
- `dataway-analysis/04-ecuacion-de-balance/divergencia-reportes-paralelos.md` — el mismo TheGoalDate aparece en `frmMovimiento_Reporte` (case-pointers `CP-004`, `CP-005`, `CP-006` pendientes)
- `tools/case-seed/queries/data-discrepancy/` — queries 07 y 08 a agregar
