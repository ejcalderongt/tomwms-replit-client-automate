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

| ID | Estado | Archivo | Línea | Hint | Severidad estimada |
|---|---|---|---|---|---|
| `CP-001` | **documentado** | `frmStockEnUnaFecha.vb` | 137-145 | Codigo `030772033524` + Fecha 2019-08-30 + SIN REGISTRO + DESP | media |
| `CP-002` | pendiente | `frmMovimiento_Reporte.vb` | 118-120 | "Por error en el cambio de ubicación fecha_vence = now -> JP" | alta |
| `CP-003` | pendiente | `frmMovimiento_Reporte.vb` | 138 | "Magia por EJC para corregir cagada" | media |
| `CP-004` | pendiente | `frmMovimiento_Reporte.vb` | 87 | `Dim TheGoalDate As Date = New Date(2019, 8, 30)` | media |
| `CP-005` | pendiente | `frmMovimiento_Reporte.vb` | 95 | `If ObjM.Fecha_Vence = TheGoalDate Then Debug.Print("Wait a second!")` | media |
| `CP-006` | pendiente | `frmMovimiento_Reporte.vb` | 99 | Combinación TheGoalDate + EstadoOrigen=SIN REGISTRO + TipoTarea=DESP | alta |
| `CP-007` | pendiente | `frmStockEnUnaFecha.vb` | (varias) | Marker `"#EJCAJUSTEDESFASE"` en mutación Modo Depuracion | alta |

**Total documentados**: 1 / 7+ identificados. Resto pendiente para sub-waves siguientes.

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
