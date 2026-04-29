# CP-004 — frmMovimiento_Reporte declaración `Dim TheGoalDate As Date = New Date(2019, 8, 30)`

> Constante hardcodeada con la fecha `2019-08-30` declarada dentro del cuerpo de `Generar_Reporte`. No es configuración del cliente — es la "fecha objetivo" de un caso real que en su momento se debugueaba. Pertenece a la **trinity TheGoalDate** junto con `CP-005` y `CP-006`.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-004-frmmovreporte-thegoaldate-declaracion |
| Tipo | hardcode (declaración de constante) |
| Estado | documentado — pertenece a trinity |
| Severidad estimada | media (no introduce bug, alimenta los breakpoints CP-005 y CP-006) |
| Archivo | `TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/Fiscales/frmMovimiento_Reporte.vb` |
| Línea | 87 |
| Espejo en otro archivo | `frmStockEnUnaFecha.vb:92` (mismo `TheGoalDate` — `CP-001` triple) |

## Cita textual

`frmMovimiento_Reporte.vb:87`:

```vb
Dim TheGoalDate As Date = New Date(2019, 8, 30)
```

Contexto (L80-92):
```vb
prg.Properties.Step = 1
prg.Properties.PercentView = True
prg.Properties.Maximum = ListaMovimientos.Count
prg.Properties.Minimum = 0

If Not ListaMovimientos Is Nothing Then

    Dim TheGoalDate As Date = New Date(2019, 8, 30)

    For Each ObjM In ListaMovimientos.OrderBy(Function(x) x.EstadoOrigen)
```

## Lectura

La constante se declara **dentro del `If`** del loop principal del reporte. Se reasigna en cada corrida del reporte (no es global ni estática). Solo sirve para alimentar los `If` de las líneas 95 y 99 (CP-005 y CP-006).

**No introduce bug** por sí sola. Si los `If` que la consumen se borraran, esta declaración sería deuda muerta inmediata.

## Trinity TheGoalDate

Los tres se sostienen entre sí:

| ID | Línea | Rol |
|---|---|---|
| CP-004 | 87 | Declara `TheGoalDate = 2019-08-30` |
| CP-005 | 95 | Breakpoint amplio: cualquier producto con esa `Fecha_Vence` |
| CP-006 | 99 | Breakpoint preciso: `TheGoalDate + SIN REGISTRO + DESP` |

**Limpieza atómica**: si se decide limpiar, los tres se limpian juntos.

## Espejo en `frmStockEnUnaFecha.vb`

El `CP-001` (documentado en wave 13-1) es la versión espejo en el reporte estándar. Mismo caso histórico, debugueado en los dos reportes.

Esto fortalece la hipótesis (`CP-001`) de que el caso original venía de un cliente con control de póliza (afecta al fiscal) **y al mismo tiempo aparecía en el reporte estándar** — es decir, el cliente afectado probablemente corría ambos reportes por algún motivo, o el caso se estudió desde dos ángulos.

## Acción forense

Inseparable de `CP-005` y `CP-006`. Las acciones forenses están en esos dos documentos. Esta declaración solo es relevante como dato.

## Bitácora de debug

Ver `brain/debuged-cases/CP-004.md`.

## Cross-refs

- `00-INDEX.md`
- `05-frmmovreporte-breakpoint-fecha.md` — CP-005 (consumidor)
- `06-frmmovreporte-breakpoint-triple.md` — CP-006 (consumidor)
- `01-stockfecha-codigo-030772033524.md` — CP-001 (espejo en reporte estándar)
- `brain/debuged-cases/CP-004.md`
