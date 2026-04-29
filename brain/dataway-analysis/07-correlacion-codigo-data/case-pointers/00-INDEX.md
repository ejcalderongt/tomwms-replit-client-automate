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
| `CP-007` | **documentado** | `frmStockEnUnaFecha.vb` | 401-435 (Llena_Grid) | Marker `Serie = "#EJCAJUSTEDESFASE"` (auto-confirmable por query 06) — predecesor de CP-008 | alta | [`CP-007`](../../../debuged-cases/CP-007.md) |
| `CP-008` | **documentado** | 3 reportes (StockEnUnaFecha + MovimientoReporte + AnaliticaA) | L425 / L487 / L624 | Marker `#EJCAJUSTEDESFASE` copy-pasted en 3 reportes (expande V-DATAWAY-001 a familia) | alta | [`CP-008`](../../../debuged-cases/CP-008.md) |
| `CP-009` | **documentado** | `frmRegularizarInventario.vb` | 526 | Triple hardcode `01007121 / 01007011 / IdStock=4427` con Debug.Print comentado (Congelado) | media | [`CP-009`](../../../debuged-cases/CP-009.md) |
| `CP-010` | **documentado** | `clsLnStock_res_Partial.vb` | 20947 | Breakpoint `Codigo "00190454"` + `Debug.Print("Aqui")` activo (zona picking) | baja | [`CP-010`](../../../debuged-cases/CP-010.md) |
| `CP-011` | **documentado** | `clsLnStock_res_Partial.vb` | 27264 | Breakpoint `Codigo "00091035"` + `Debug.Write("Espera")` activo | baja | [`CP-011`](../../../debuged-cases/CP-011.md) |
| `CP-012` | **documentado** | `frmExistenciasConReserva.vb` | 283 | Breakpoint `Codigo "01008076"` + `Debug.Print("Espera")` + 2 guards no-op | baja | [`CP-012`](../../../debuged-cases/CP-012.md) |
| `CP-013` | **confirmado** | (caso de campo Killios — sin hardcode origen) | — | Killios WMS164: stock partido en 2 filas con misma llave natural (CEST sin merge); 919 filas / 18.7% del stock activo afectado | alta | [`CP-013`](../../../debuged-cases/CP-013.md) |

**Total documentados**: 13 / 13 (wave 13-9 abre nueva categoría: "casos de campo confirmados con datos reales", primer ejemplar CP-013).

## Agrupaciones

### Trinity TheGoalDate (limpieza atómica)

Tres case-pointers en `frmMovimiento_Reporte.vb` que se sostienen entre sí:

- **CP-004** (línea 87) declara `TheGoalDate = 2019-08-30`
- **CP-005** (línea 95) consume amplio: cualquier producto con esa `Fecha_Vence`
- **CP-006** (línea 99) consume preciso: triple condición

Limpieza: si se decide eliminar, los tres se eliminan juntos.

### Espejos entre los reportes con `ModoDepuracion`

- **CP-001** ↔ **CP-006** — mismo caso histórico debugueado en los reportes estándar y fiscal. Si era cliente con control de póliza, CP-006 es el más cercano al caso original.

### Pareja fix-bug (mismo bloque)

- **CP-002** (bug introducido por JP)
- **CP-003** (intento de fix por EJC, **comentado** — no se ejecuta)

### Familia con efecto persistente en BD (auto-confirmables)

- **CP-007** — caso original `frmStockEnUnaFecha`. La query 06 puede confirmar/refutar el impacto sin entrevistar a nadie.
- **CP-008** — extensión a tres reportes. La query 06 mide el agregado de los tres mutadores, no de uno. CP-008 **expande** a CP-007, no lo reemplaza (CP-007 sigue siendo el caso singular del reporte estándar).

### Instancias del pattern P-001 (breakpoint arqueológico con código hardcoded)

- **CP-001** — `030772033524` (frmStockEnUnaFecha)
- **CP-009** — triple `01007121 / 01007011 / IdStock=4427` (frmRegularizarInventario)
- **CP-010** — `00190454` (clsLnStock_res_Partial L20947)
- **CP-011** — `00091035` (clsLnStock_res_Partial L27264)
- **CP-012** — `01008076` (frmExistenciasConReserva)

Ver `case-pointers/patterns/breakpoint-arqueologico-codigo-hardcoded.md` para el pattern formal.

### Casos de campo (auto-confirmables sin entrevista) — categoría nueva en wave 13-9

Distinta a las dos anteriores (breakpoint arqueológico y marker persistente). **No nacen de un hardcode encontrado en código fuente** — nacen de tickets de operación reproducidos contra BD productiva con queries READ-ONLY. Confirman un anti-patrón sistémico **antes** de que se localice el código bugueado.

- **CP-013** — Killios WMS164: stock partido en 2 filas con misma llave natural (CEST sin merge). Anti-patrón sistémico medido: 469 combos / 919 filas / 18.7% del stock activo de Killios / 183.375 UN involucradas. **Bug raíz inferido**: `V-DATAWAY-004` (ver `dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md`). Refuta categóricamente la hipótesis ModoDepuracion (V-DATAWAY-001) — query 11 confirma `0` ocurrencias del marker `#EJCAJUSTEDESFASE` en Killios.

**Candidato a pattern P-002**: "INSERT sin merge contra llave natural" (a formalizar cuando aparezcan más instancias).

## Bitácoras vivas

Cada case-pointer tiene su bitácora de debug en `brain/debuged-cases/CP-NNN.md`. Las bitácoras son **append-only** y rastrean status (`open` → `reproducing` → `confirmed` → `solved`/`wont-fix`/`obsolete`), avances, queries corridas y observaciones.

Ver [`brain/debuged-cases/00-INDEX.md`](../../../debuged-cases/00-INDEX.md).

## Patterns

Los case-pointers que comparten una forma común se promueven a `patterns/`. Ver:

- [`patterns/00-INDEX.md`](./patterns/00-INDEX.md)
- `P-001`: breakpoint arqueológico con código hardcoded (instancias: CP-001, CP-009, CP-010, CP-011, CP-012)

## Heurística de búsqueda (corrida en wave 13-8)

```bash
# B1: comments con nombres propios o "magia/cagada/bug"  -> CP-002, CP-003 (ya documentados)
rg -n -i "magia|cagada|hack|workaround|fix\s+by|por\s+error|JP|EJC|MA\b|GT\b" /tmp/repos/TOMWMS_BOF/ --type vb

# B2: Debug.Print con texto específico no genérico  -> CP-010, CP-011 (ya documentados)
rg -n 'Debug\.Print\s*\(\s*"[^"]+"\s*\)' /tmp/repos/TOMWMS_BOF/ --type vb \
   | grep -v -E 'Debug\.Print\("(Wait|Espera|Step|Iteracion|Linea)\b'

# B3: fechas hardcodeadas New Date(YYYY,M,D)  -> trinity TheGoalDate ya cubierta, no aparecieron nuevas
rg -n 'New Date\(\s*\d{4}\s*,\s*\d+\s*,\s*\d+\s*\)' /tmp/repos/TOMWMS_BOF/ --type vb

# B4: marker series fijas  -> CP-008 (3 reportes)
rg -n 'Serie\s*=\s*"#[A-Za-z]' /tmp/repos/TOMWMS_BOF/ --type vb

# B5: hardcodes de Codigo de producto  -> CP-009, CP-010, CP-011, CP-012
rg -n 'Codigo\s*=\s*"[0-9]{8,}"' /tmp/repos/TOMWMS_BOF/ --type vb

# B6: IdStock hardcodeado  -> CP-009 (triple, ya cubierto)
rg -n 'IdStock\s*=\s*\d{4,}' /tmp/repos/TOMWMS_BOF/ --type vb
```

Las 6 búsquedas se corrieron en wave 13-8. Los hallazgos se cerraron con CP-002..CP-012.

## Búsquedas pendientes para próximas waves

- **B7**: comments firmados `'#EJC<YYYYMMDD>...:` cerca de bloques sospechosos (3270 totales en repo, top en `clsLnStock_res_Partial.vb` con 343). Filtrar los firmados en bloques con hardcode/breakpoint dentro de N líneas.
- **B8**: lotes específicos (`Lote = "..."`) hardcodeados.
- **B9**: nombres de funciones tipo `Resuelve_Caso_X`, `Hack_Y`, `Workaround_Z`.
- **B10**: tagged labels GoTo `<INICIALES>_<TIMESTAMP>_<DESCRIPCION>` (CP-010 reveló el ejemplo `EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING:`).

## Promoción de case-pointer a learnings/

Cuando un case-pointer:
1. Se confirma con datos reales de producción (vía case-seed),
2. Se identifica el bug raíz (no solo el síntoma),
3. Se determina si está vigente o resuelto,

→ promover a `brain/learnings/` con `L-DATAWAY-NNN`.

## Cross-refs

- `dataway-analysis/04-ecuacion-de-balance/anti-patron-modo-depuracion.md` — origen de `CP-007`/`CP-008`
- `dataway-analysis/04-ecuacion-de-balance/divergencia-reportes-paralelos.md` — origen de `CP-002`, `CP-003`, `CP-006`; expandido en wave 13-8 con `frmAnaliticaA`
- `dataway-analysis/04-ecuacion-de-balance/granularidad-y-keys.md` — referencia al bug histórico de JP
- `brain/conventions/comments-firmados-EJC.md` — formaliza la convención `'#EJC<YYYYMMDD>...:` que aparece 3270 veces en el repo
- `brain/scan-comments-tree-map/` — herramienta complementaria que escanea comentarios firmados (Wave 9 followup)
- `tools/case-seed/` — herramienta para extraer evidencia de cada case-pointer desde BD productiva
