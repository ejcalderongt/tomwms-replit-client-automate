# Glosario WMS — términos canónicos

> Glosario unificado para que humanos y máquinas usen la misma definición. Este es el archivo de referencia: cualquier ambigüedad de término en otros documentos del brain se resuelve consultando aquí.

## Terminología de personas y roles

### operador
**Definición**: bodeguero, usuario de la HH (handheld) o de la BOF (back office). Persona física que mueve mercadería en el almacén.

**No confundir con**: "equipo de operaciones" (que sería un concepto organizacional). En el contexto del WMS, "operador" es siempre la persona física que opera la HH/BOF.

**Ejemplos de uso**:
- "el operador escanea la licencia" → la persona usando la HH
- "el operador confirma el UBIC parcial de 50 cajas" → bodeguero confirma posicionamiento

### operación (del cliente)
**Definición**: la operatoria del negocio del cliente que usa el WMS. Se refiere al flujo de negocio (recepción, despacho, ajustes), no a un equipo organizacional.

**Ejemplos de uso**:
- "esto cambia según la operación del cliente" → varía según cómo opera el cliente su negocio
- "configurar para la operación de farma" → adaptar a flujo del cliente farma

### equipo de desarrollo
**Definición**: vos (Erik), CKFK, MA, GT, AT, MECR. Lectores técnicos del brain.

### equipo PrograX24
**Definición**: ver `brain/agent-context/EQUIPO_PROGRAX24.md`.

## Terminología de identidad de stock

### IdStock
**Definición**: identificador interno (PK) de una fila de la tabla `stock`. Representa una **cantidad de un producto en una ubicación con un estado y una presentación**.

**Característica crítica**: **NO es estable**. Cada vez que ocurre una operación significativa (cambio de ubicación, cambio de estado, implosión, ajuste, reubicación parcial, picking parcial, split físico durante reserva), el flujo destruye el IdStock origen e inserta uno nuevo con cantidad/ubicación/estado modificados.

**Patrón maestro**: `DELETE idstock N` + `INSERT idstock M` (M es copia de N + cambios). Sin linaje linkeado (no hay columna `parent_idstock`).

Ver: `dataway-analysis/00-modelo-identidad-idstock.md`

### licencia (lic_plate)
**Definición**: identificador de la unidad logística física (típicamente una tarima, contenedor, caja maestra). En el código: `lic_plate`.

**Característica**: una licencia puede mapear a **1..N IdStocks**. Cada vez que la licencia se "particiona" (UBIC parcial, picking parcial, implosión), aparecen idstocks nuevos asociados a la misma licencia.

Ver: `dataway-analysis/02-particionamiento-licencia/` (sub-wave siguiente)

### puntero (en `stock_res`)
**Definición**: en analogía con C++, una fila de la tabla `stock_res` actúa como **puntero a un IdStock**. Cuando llega un pedido, el sistema NO resta del stock inmediatamente — inserta un puntero `stock_res(idstock=N, cantidad=X)` que reserva X unidades del idstock N.

**Cálculo de disponible**:
```
disponible(idstock_N) = stock.cantidad(N) − SUM(stock_res.cantidad WHERE idstock = N)
```

**Origen del término**: comment textual en `frmStockEnUnaFecha.vb:172`:
```vb
BeStockEnFecha = RepMovEnUnaFecha(Idx)  'Puntero =>
```
El autor del reporte tenía la analogía de memoria C++ desde el inicio.

### presentación (idpresentacion)
**Definición**: factor de conversión entre la unidad base del producto (UMBas) y una unidad agrupada (caja, master, tarima estándar). Ej: 1 caja = 12 UMBas.

**En `vw_stock_res`**: la vista aplica el factor automáticamente. Si un producto tiene presentación, se expresa la cantidad en términos de presentación. Si no tiene presentación (idpresentacion = 0 o null), solo se expresa en UMBas.

**Implicación para balance**: un mismo idstock puede aparecer dos veces en consultas distintas (con/sin presentación) y los números **no son sumables directamente**.

### UMBas (Unidad de Medida Base)
**Definición**: unidad mínima física del producto. Es la unidad en la que se almacenan las cantidades en `stock.cantidad`.

**Cálculo de volumen** (en `clsBeVW_stock_res_Partial.vb`):
```vb
VolumenUmBas = CantidadUmBas * AltoUMBas * LargoUmBas * AnchoUmBas
```

## Terminología de balance y discrepancia

### balance (ecuación de balance)
**Definición**: la fórmula que reconstruye la existencia teórica desde los movimientos:

```
Existencia_Teorica = Inventario_Inicial
                   + Ingresos
                   + Ajustes_Positivos
                   − Ajustes_Negativos
                   − Salidas
```

**Comparación**: `Diferencia = Existencia_Teorica − Existencia_Actual`. Si `Diferencia ≠ 0`, hay **gap**.

**Implementación canónica**: `frmStockEnUnaFecha.vb:329` (`Llena_Grid` Sub).

### gap (desfase)
**Definición**: diferencia entre `Existencia_Teorica` y `Existencia_Actual` (la `stock.cantidad` actual). Si `gap ≠ 0`, hay **desfase** que requiere investigación.

**Posibles causas**:
1. Bug en una operación que mutó stock sin generar movimiento.
2. Movimiento generado con TipoTarea no reconocido (silenciosamente ignorado por `Llena_Grid`).
3. Doble conteo (sin guard `IdMovimiento` — ver `V-DATAWAY-002`).
4. Mutación directa de `trans_movimientos` por `ModoDepuracion` (ver `V-DATAWAY-001`).
5. Problema de interpretación de datos (no es bug, es lectura incorrecta).

### TipoTarea
**Definición**: enum en `clsDataContractDI.tTipoTarea` que clasifica cada movimiento de `trans_movimientos`. Los relevantes para el balance:

| TipoTarea | Significado | Suma en balance |
|---|---|---|
| `INVE` | Inventario inicial | `Inventario_Inicial` |
| `RECE` | Recepción / ingreso | `Ingresos` |
| `AJCANTP` | Ajuste cantidad positiva | `Ajustes_Positivos` |
| `AJCANTPI` | Ajuste cantidad positiva (inverso) | `Ajustes_Positivos` |
| `AJCANTN` | Ajuste cantidad negativa | `Ajustes_Negativos` |
| `AJCANTNI` | Ajuste cantidad negativa (inverso) | `Ajustes_Negativos` |
| `DESP` | Despacho / salida | `Salidas` |
| `UBIC` | Cambio de ubicación | **NEUTRO** (excluido del balance) |
| `CEST` | Cambio de estado (típicamente entre estados de calidad, ej: `1` BUEN ESTADO ↔ `16` MAL ESTADO) | **NEUTRO** (excluido del balance) — pero ver `V-DATAWAY-004`: el path CEST es el sospechoso del anti-patrón insert-stock-sin-merge |
| `VERI` | Verificación pre-despacho | NEUTRO |
| `AJUS` | Ajuste genérico | depende del subtipo |
| (otros) | — | **silenciosamente ignorado** (`V-DATAWAY-003`) |

**IDs numéricos en Killios** (`sis_tipo_tarea`): `1=RECE`, `2=UBIC`, `3=CEST`, `5=DESP`, `6=INVE`, `7=AJUS`, `11=VERI`. Esquema lowercase, NO existe `tipo_tarea` (es `sis_tipo_tarea`).

Ver: `dataway-analysis/04-ecuacion-de-balance/tipos-tarea-relevantes.md`

## Terminología forense

### case-pointer
**Definición**: hardcode encontrado en código fuente (Debug.Print, If con producto/lote/fecha/IdStock específico) que apunta a un caso real histórico que rompió en producción y dejó marca en el código como bypass de debug.

**Ejemplo canónico**: `frmStockEnUnaFecha.vb:144`:
```vb
If ObjM.Codigo = "030772033524" Then
    Debug.Print("Wait a second!")
End If
```
Este hardcode existe para que el desarrollador (probablemente Erik) pueda saltar al breakpoint solo en el caso problemático, sin hacer step-through de los 9 casos sanos previos.

**Convención de naming**: `CP-NNN-<contexto>-<hint>`, ej: `CP-001-stockfecha-codigo-030772033524`.

Ver: `dataway-analysis/07-correlacion-codigo-data/case-pointers/00-INDEX.md`

### case-seed
**Definición**: ZIP de evidencia exportado desde BD productiva por la herramienta `tools/case-seed/export_case_seed.ps1`. Contiene snapshot de stock + ventana de movimientos + `vw_stock_res` + ajustes, para un `CaseId`, `IdProducto`, `IdBodega` y rango de tiempo dados.

Ver: `dataway-analysis/00-relacion-con-case-seed.md`

### CaseId
**Definición**: identificador único del caso reportado. Convención sugerida en `tools/case-seed/README.md`: `INC-YYYY-MM-DD-<HINT>`, ej: `INC-2026-04-16-GUINDA`.

### CaseType
**Definición**: clasificación del caso. Valores actuales en `tools/case-seed/templates/CASE_INTAKE_TEMPLATE.md`:
- `data-discrepancy` (gap en balance)
- `hh-bug` (bug funcional en HH)
- `vb-exception` (excepción VB.NET en BOF)
- `sql-perf` (problema de performance SQL)

## Terminología de tablas y vistas centrales

### `stock`
**Definición**: tabla maestra del inventario actual. PK = `IdStock`. Granularidad: producto + ubicación + estado + presentación + lote + fecha de vencimiento.

### `stock_res`
**Definición**: tabla de reservas. Cada fila es un "puntero" desde un pedido/picking hacia un `idstock`. Una reserva consume virtualmente la cantidad del idstock sin alterar `stock.cantidad`.

### `trans_movimientos`
**Definición**: tabla append-only de todos los movimientos físicos del inventario. Es la **fuente de verdad** del balance. Cada fila tiene `TipoTarea` que clasifica si es INVE/RECE/AJ*/DESP/UBIC/etc.

### `Diferencias_movimientos`
**Definición**: tabla donde el reporte `frmStockEnUnaFecha` (en `ModoDepuracion=True`) inserta los gaps detectados. **Atención**: el reporte borra TODA esta tabla al inicio de cada corrida (`clsLnDiferencias_movimientos.EliminarTodos()`), no es histórica.

### `VW_Movimientos`
**Definición**: vista sobre `trans_movimientos` que pre-junta campos derivados (producto, presentación, ubicación) para evitar `Get` adicionales por LN-class durante la generación del cardex.

**Razón de existir** (palabras de Erik): "al inicio la tabla raw estaba bien, pero luego teníamos que buscar mucha información vía querys adicionales, como gets con las clases ln, esto convertía al proceso de generación de kardex en un proceso que duraba o podía durar hasta horas."

Variantes encontradas:
- `VW_Movimientos` (general)
- `VW_Movimientos_Poliza` (poliza fiscal)
- `VW_Movimientos_Propietario` (multi-propietario)
- `VW_MovimientosRetroactivo` (retroactivo)

### `VW_Stock_Res` (vw_stock_res)
**Definición**: vista derivada que aplica el cálculo del puntero. Para cada producto-bodega-ubicación-estado-presentación-lote-fecha_vence, calcula:

```
disponible_pres = (stock.cantidad − SUM(stock_res.cantidad por idstock)) / factor_presentacion
disponible_umbas = stock.cantidad − SUM(stock_res.cantidad por idstock)   -- si no tiene presentacion
```

Es la vista que el código consume para "saber qué hay disponible para reservar".

## Terminología de operaciones HH

### UBIC parcial
**Definición**: el operador con HH confirma posicionamiento de **parte** de la licencia (ej: 50 de 100 cajas en la posición A). Esto genera un IdStock nuevo por la cantidad confirmada y deja la licencia "abierta" con el resto pendiente.

### implosión
**Definición**: descomposición de una unidad agrupada en sus componentes (ej: descomponer una caja de 12 unidades en 12 unidades sueltas). Genera múltiples idstocks hijos.

### reubicación parcial
**Definición**: mover **parte** del contenido de un idstock a otra ubicación. Genera split: el idstock origen queda con la cantidad residual + idstock nuevo en la ubicación destino con la cantidad movida.

### picking parcial
**Definición**: el operador picka menos cantidad que la solicitada. La diferencia queda como "picking pendiente" o se cierra como faltante.

### split físico (en reserva)
**Definición**: durante el proceso de reserva (Reabasto, MI3, SAP), el sistema puede "partir" un idstock en filas hijas para poder reservar parcialmente. En Reabasto, el marker es `No_bulto = 1989`. Ver: `brain/entities/modules/reservation/legacy-process-flow/06-reserva-stock-from-reabasto.md` (Wave 12B-2).

## Terminología de pipeline de reserva

### From_Reabasto, From_MI3, From_SAP
**Definición**: las 3 funciones del pipeline `clsLnStock_res_Partial.vb` que reservan stock según el origen del pedido. Ver inventario en Wave 12 (entities/modules/reservation/legacy-process-flow/).

### NAV_BYB
**Definición**: variante de reserva con BYB (Bandeja Y Bulto / Bagging Y Boxing). Ver Wave 12.

### lista_resultado
**Definición**: variante de reserva por lista de resultado. Ver Wave 12.

### Especifico OV1, OV2
**Definición**: reserva específica con pedido marcado (OV1) y con orden de venta marcada (OV2). Ver Wave 12.

## Terminología de configs

### Interface_SAP
**Definición**: config booleano que activa lógica SAP-específica en el pipeline de reserva. Descubierto en `From_MI3` (Wave 12C-1). **Q-MI3-03 abierta**: ¿qué cambia exactamente con `Interface_SAP=True`?

### considerar_paletizado_en_reabasto
**Definición**: config booleano que activa consideración de paletizado en el reabasto. Descubierto en `From_MI3` (Wave 12C-1).

## Terminología de catálogo forense (case-pointers, patterns, conventions)

### case-pointer (auto-confirmable)
**Definición**: case-pointer cuya verificación no requiere entrevistar a un actor humano ni reproducir el bug — basta con correr una query SQL contra producción para confirmar o refutar el impacto.

**Ejemplo**: `CP-007` y `CP-008` (marker `Serie = "#EJCAJUSTEDESFASE"`). La query 06 cuenta movimientos con esa `Serie` y responde sola: "esto pasó / no pasó / pasó N veces".

**No confundir con**: case-pointer que requiere entrevista (mayoría) o reproducción manual.

### bitácora viva
**Definición**: archivo en `brain/debuged-cases/CP-NNN.md` con frontmatter YAML + bitácora append-only. Rastrea status (`open` → `reproducing` → `confirmed` → `solved`/`wont-fix`/`obsolete`), avances datados, queries corridas, decisiones condicionales.

**Característica clave**: **append-only**. Nunca se borra historia. Cualquier cambio se documenta como entrada nueva.

### espejo (entre case-pointers)
**Definición**: dos o más case-pointers que apuntan al **mismo caso histórico** vivido en archivos distintos (típicamente reportes paralelos especializados por tipo de cliente).

**Ejemplo**: `CP-001` (en `frmStockEnUnaFecha`) ↔ `CP-006` (en `frmMovimiento_Reporte`) — ambos investigan el caso del producto `030772033524` con `Fecha 2019-08-30`, pero en los dos reportes paralelos.

### trinity (en case-pointers)
**Definición**: grupo de tres case-pointers que se sostienen entre sí (uno declara, otro consume amplio, otro consume preciso) — la limpieza de uno requiere limpiar los tres.

**Ejemplo**: trinity TheGoalDate = `CP-004` (declara `Dim TheGoalDate As Date = New Date(2019, 8, 30)`) + `CP-005` (consume amplio: `If Fecha_Vence = TheGoalDate Then ...`) + `CP-006` (consume preciso: triple condición).

### marker persistente (en BD)
**Definición**: string hardcodeado en código fuente que se asigna a un campo persistente de BD (típicamente `Serie`, `Comentario`, `Observaciones`) para dejar **huella en producción** de que un registro pasó por una herramienta o un flujo específico.

**Ejemplo**: `M.Serie = "#EJCAJUSTEDESFASE"` en los 3 reportes con `ModoDepuracion`. Cualquier auditoría de BD puede contar registros con esa `Serie` y medir el impacto histórico de la herramienta.

**Por qué importa**: convierte un case-pointer común (sin efecto en datos) en un case-pointer **auto-confirmable** (la BD responde sola).

### breakpoint arqueológico (P-001)
**Definición**: bloque `If <Producto>.Codigo = "<SKU>" Then Debug.Print/Write("<TEXTO_CORTO>") End If` dejado en código fuente como bypass de debug para parar el debugger solo en el caso problemático sin tener que hacer step-through de los casos sanos previos. Cuando el bug se "resuelve" (típicamente ad-hoc en producción), el `If` queda como fósil.

**Pattern formal**: `P-001` en `dataway-analysis/07-correlacion-codigo-data/case-pointers/patterns/breakpoint-arqueologico-codigo-hardcoded.md`.

**Instancias documentadas**: `CP-001`, `CP-009`, `CP-010`, `CP-011`, `CP-012`.

### convention de comments firmados (C-001)
**Definición**: convención personal de Erik para firmar cambios en código con comments del estilo `'#EJC<YYYYMMDD>[<sufijo>]: <body>`. Aparece **3270 veces** en `TOMWMS_BOF`. Documentado formalmente en `brain/conventions/comments-firmados-EJC.md`.

**Diferencia con marker `#EJCAJUSTEDESFASE`**: comparten iniciales `EJC` pero son cosas distintas (uno es comment de bitácora inline, el otro es string asignado a campo de BD).

### pattern (catálogo)
**Definición**: forma estructural repetida en el código (no contenido). Cuando dos o más case-pointers comparten forma, se promueven a pattern. Vive en `dataway-analysis/07-correlacion-codigo-data/case-pointers/patterns/`. Naming: `P-NNN`.

### convention (catálogo)
**Definición**: acuerdo (explícito o implícito) que sigue el equipo. Distinto de pattern (que es forma observada en código sin importar si fue intencional). Vive en `brain/conventions/`. Naming: `C-NNN`.

## Notas

- Cuando aparezca un término nuevo en cualquier documento del brain, agregarlo aquí con definición + ejemplo.
- Si un término ya existente cambia de significado por hallazgo nuevo, actualizar aquí y dejar entrada en `learnings/` referenciando el cambio.
