# Case-Pointers — INDEX

> Inventario de hardcodes encontrados en código fuente legacy que apuntan a casos reales históricos. Cada uno es un fósil: un caso que rompió en producción, dejó marca como bypass de debug en el código, y nunca se limpió.

## Qué es un case-pointer

**Definición**: hardcode encontrado en código fuente (Debug.Print, If con producto/lote/fecha/IdStock específico, comment textual con nombre propio) que apunta a un caso real histórico que rompió en producción y dejó marca en el código como bypass de debug.

**Ejemplo canónico**:
```vb
If ObjM.Codigo = "030772033524" Then
    Debug.Print("Wait a second!")
End If
```

El desarrollador (probablemente Erik) puso este `If` para que el debugger se detenga **solo** en el caso problemático, sin tener que hacer step-through de los 9 casos sanos previos. Cuando el bug se "resolvió" (ad-hoc en producción), el `If` quedó en el código.

## Por qué importan

1. **Pista de bugs históricos**: cada case-pointer es evidencia de un bug real que ocurrió. Aunque hoy parezca "resuelto", la condición que lo causó probablemente sigue presente en el sistema.

2. **Documentación implícita**: el desarrollador que puso el hardcode sabía algo del caso. Documentarlo formalmente convierte conocimiento tribal en conocimiento estructurado.

3. **Detección de regresiones**: si el case-pointer sigue siendo necesario hoy, hay un bug latente. Si ya no es necesario, es deuda muerta que confunde.

4. **Atribución**: comments tipo "(Por error en el cambio de ubicación fecha_vence = now -> JP)" o "Magia por EJC para corregir cagada" identifican a actores históricos del sistema. Útil para entrevistar a quien estuvo involucrado.

## Convención de naming

```
CP-NNN-<contexto>-<hint>
```

- `NNN`: número correlativo (001, 002, ...).
- `<contexto>`: archivo o módulo donde aparece (`stockfecha`, `frommi3`, `reabasto`).
- `<hint>`: pista del caso (`codigo-030772033524`, `fecha-vence-now-jp-bug`).

## Inventario actual

| ID | Estado | Archivo | Línea | Hint | Severidad | Bitácora |
|---|---|---|---|---|---|---|
| `CP-001` | **documentado** | `frmStockEnUnaFecha.vb` | 137-145 | Codigo `030772033524` + Fecha 2019-08-30 + SIN REGISTRO + DESP | alta | [`CP-001`](../../../debuged-cases/CP-001.md) |
| `CP-002` | **documentado** | `frmMovimiento_Reporte.vb` | 126 | "(Por error en el cambio de ubicación fecha_vence = now -> JP.)" | media | [`CP-002`](../../../debuged-cases/CP-002.md) |
| `CP-003` | **documentado** | `frmMovimiento_Reporte.vb` | 125 + 128 (fix comentado) | "Magia por EJC para corregir cagada" | media | [`CP-003`](../../../debuged-cases/CP-003.md) |
| `CP-004` | **documentado** | `frmMovimiento_Reporte.vb` | 87 | `Dim TheGoalDate As Date = New Date(2019, 8, 30)` (trinity) | media | [`CP-004`](../../../debuged-cases/CP-004.md) |
| `CP-005` | **documentado** | `frmMovimiento_Reporte.vb` | 95-97 | `If Fecha_Vence = TheGoalDate Then Debug.Print("Wait a second!")` (trinity, panorámica) | media | [`CP-005`](../../../debuged-cases/CP-005.md) |
| `CP-006` | **documentado** | `frmMovimiento_Reporte.vb` | 99-101 | Triple TheGoalDate + EstadoOrigen=SIN REGISTRO + TipoTarea=DESP (espejo idéntico de CP-001) | alta | [`CP-006`](../../../debuged-cases/CP-006.md) |
| `CP-007` | **documentado** | `frmStockEnUnaFecha.vb` | 401-435 (Llena_Grid) | Marker `Serie = "#EJCAJUSTEDESFASE"` (auto-confirmable por query 06) | alta | [`CP-007`](../../../debuged-cases/CP-007.md) |

**Total documentados**: 7 / 7 identificados (wave 13-7 cierra el barrido inicial).

## Agrupaciones

### Trinity TheGoalDate (limpieza atómica)

Tres case-pointers en `frmMovimiento_Reporte.vb` que se sostienen entre sí:

- **CP-004** (línea 87) declara `TheGoalDate = 2019-08-30`
- **CP-005** (línea 95) consume amplio: cualquier producto con esa `Fecha_Vence`
- **CP-006** (línea 99) consume preciso: triple condición

Limpieza: si se decide eliminar, los tres se eliminan juntos.

### Espejos entre los dos reportes

- **CP-001** ↔ **CP-006** — mismo caso histórico debugueado en los dos reportes (estándar y fiscal). Si era cliente con control de póliza, CP-006 es el más cercano al caso original.

### Pareja fix-bug (mismo bloque)

- **CP-002** (bug introducido por JP)
- **CP-003** (intento de fix por EJC, **comentado** — no se ejecuta)

### Único con efecto persistente en BD (auto-confirmable)

- **CP-007** — `Serie = "#EJCAJUSTEDESFASE"` se escribe a `trans_movimientos`. La query 06 puede confirmar/refutar el impacto sin entrevistar a nadie.

## Bitácoras vivas

Cada case-pointer tiene su bitácora de debug en `brain/debuged-cases/CP-NNN.md`. Las bitácoras son **append-only** y rastrean status (`open` → `reproducing` → `confirmed` → `solved`/`wont-fix`/`obsolete`), avances, queries corridas y observaciones.

Ver [`brain/debuged-cases/00-INDEX.md`](../../../debuged-cases/00-INDEX.md).

## Heurística de búsqueda (para sub-waves siguientes)

Para barrido sistemático del código legacy:

```bash
# Búsqueda 1: hardcodes de Codigo de producto
rg -n 'Codigo\s*=\s*"[0-9]{8,}"' /tmp/repos/TOMWMS_BOF/

# Búsqueda 2: fechas hardcodeadas
rg -n 'New Date\(\s*\d{4}\s*,\s*\d+\s*,\s*\d+\s*\)' /tmp/repos/TOMWMS_BOF/

# Búsqueda 3: Debug.Print con texto específico (no genérico)
rg -n 'Debug\.Print\s*\(\s*"[^"]+"\s*\)' /tmp/repos/TOMWMS_BOF/ \
   | grep -v -E 'Debug\.Print\("(Wait|Espera|Step|Iteracion|Linea)\b'

# Búsqueda 4: comments con nombres propios o "magia/cagada/bug"
rg -n -i "magia|cagada|hack|workaround|fix\s+by|por\s+error|JP|EJC|MA\b|GT\b" /tmp/repos/TOMWMS_BOF/ \
   --type vb

# Búsqueda 5: IdStock hardcodeado
rg -n 'IdStock\s*=\s*\d{4,}' /tmp/repos/TOMWMS_BOF/

# Búsqueda 6: marker series fijas
rg -n 'Serie\s*=\s*"[#@]' /tmp/repos/TOMWMS_BOF/
```

## Promoción de case-pointer a learnings/

Cuando un case-pointer:
1. Se confirma con datos reales de producción (vía case-seed),
2. Se identifica el bug raíz (no solo el síntoma),
3. Se determina si está vigente o resuelto,

→ promover a `brain/learnings/` con `L-DATAWAY-NNN`.

## Cross-refs

- `dataway-analysis/04-ecuacion-de-balance/anti-patron-modo-depuracion.md` — origen de `CP-007`
- `dataway-analysis/04-ecuacion-de-balance/divergencia-reportes-paralelos.md` — origen de `CP-002`, `CP-003`, `CP-006`
- `dataway-analysis/04-ecuacion-de-balance/granularidad-y-keys.md` — referencia al bug histórico de JP
- `brain/scan-comments-tree-map/` — herramienta complementaria que escanea comentarios firmados (Wave 9 followup)
- `tools/case-seed/` — herramienta para extraer evidencia de cada case-pointer desde BD productiva
