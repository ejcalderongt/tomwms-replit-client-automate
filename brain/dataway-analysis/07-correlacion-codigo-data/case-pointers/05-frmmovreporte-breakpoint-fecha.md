# CP-005 — frmMovimiento_Reporte breakpoint amplio por `Fecha_Vence = TheGoalDate`

> Hardcode con `Debug.Print("Wait a second!")` que dispara para cualquier movimiento cuya `Fecha_Vence` coincida con `TheGoalDate` (`2019-08-30`). Es el primero de los dos breakpoints fosilizados de la trinity. Vista panorámica del caso.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-005-frmmovreporte-breakpoint-fecha |
| Tipo | hardcode (If + Debug.Print) |
| Estado | documentado — pertenece a trinity |
| Severidad estimada | media (no afecta producción, ruido fosilizado) |
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Fiscales/frmMovimiento_Reporte.vb` |
| Línea | 95-97 |
| Depende de | CP-004 (declara `TheGoalDate`) |
| Espejo | `frmStockEnUnaFecha.vb` — el segundo `If` del CP-001 |

## Cita textual

`frmMovimiento_Reporte.vb:95-97`:

```vb
If ObjM.Fecha_Vence = TheGoalDate Then
    Debug.Print("Wait a second!")
End If
```

## Lectura

Por cada movimiento del loop, si su `Fecha_Vence` es exactamente `2019-08-30`, escupe `"Wait a second!"` a la consola de debug.

`Debug.Print` con texto fijo "Wait a second!" es la firma típica de un breakpoint manual: el desarrollador lo usaba como ancla para que el debugger se detuviera en el primer movimiento de interés.

**En producción no hace nada visible** — solo molesta cuando se ejecuta con debugger conectado y hay productos con esa fecha. Es ruido de debug fosilizado.

## Vista panorámica vs precisa

CP-005 es la **vista panorámica**: dispara amplio (cualquier producto, cualquier estado, cualquier TipoTarea, mientras tenga la fecha objetivo). El siguiente `If` (CP-006) lo refina a la triple condición precisa.

El desarrollador probablemente usaba ambos: el panorámico para entender cuántos movimientos en total tocaban la fecha, el preciso para parar exactamente donde el bug se manifestaba.

## Impacto en producción

- **Funcional**: ninguno. `Debug.Print` no afecta resultado del reporte ni base de datos.
- **Performance**: insignificante (un `If` y un `Debug.Print` por iteración, solo cuando coincide la fecha).
- **Cosmético**: si el cliente afectado tuvo movimientos con esa fecha, la consola de debug se llena. Si nadie usa debugger en producción, invisible.

## Acción forense

### Buscar otros "Wait a second!" en el repo

```bash
rg -n 'Debug\.Print\("Wait a second!"\)' /tmp/repos/TOMWMS_BOF/ --type vb
```

Si aparecen más, son candidatos a `CP-008+`.

### Buscar más fechas hardcodeadas

```bash
rg -n 'New Date\(\s*\d{4}\s*,\s*\d+\s*,\s*\d+\s*\)' /tmp/repos/TOMWMS_BOF/ --type vb
```

Otras fechas hardcodeadas son potenciales hermanos de la trinity TheGoalDate.

## Bitácora de debug

Ver `brain/debuged-cases/CP-005.md`.

## Cross-refs

- `00-INDEX.md`
- `04-frmmovreporte-thegoaldate-declaracion.md` — CP-004 (proveedor de la fecha)
- `06-frmmovreporte-breakpoint-triple.md` — CP-006 (vista precisa de la trinity)
- `01-stockfecha-codigo-030772033524.md` — CP-001 (espejo en estándar, segundo If)
- `brain/debuged-cases/CP-005.md`
