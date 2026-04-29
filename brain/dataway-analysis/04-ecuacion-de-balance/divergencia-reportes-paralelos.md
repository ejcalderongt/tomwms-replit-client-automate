# Capa 04 / Divergencia entre reportes especializados por cliente

> **Erratum (wave 13-7)**: una versión previa de este documento describía a `frmMovimiento_Reporte` como "clon experimental abandonado a medio camino" del reporte canónico. Esa lectura era incorrecta. Los dos reportes son **especializaciones legítimas** para dos formas distintas de llevar el cardex en la base de clientes de TOMWMS: el estándar y el con control de póliza. La deuda técnica del reporte fiscal sigue siendo deuda técnica, pero el marco no es "clon olvidado" sino "reporte especializado mantenido con menos frecuencia que el principal".

> **Erratum (wave 13-8)**: el modelo de "dos reportes paralelos" se queda corto. La búsqueda forense de wave 13-8 reveló un **tercer reporte**: `frmAnaliticaA.vb` (en `Reportes/Analítica/`, 1250 líneas). Este reporte tiene `ModoDepuracion` declarado (línea 16), activado (línea 1242), y muta `trans_movimientos` con el mismo bloque copy-pasted que los otros dos, incluyendo el marker `M.Serie = "#EJCAJUSTEDESFASE"` (línea 624). En total son **tres reportes** que calculan balance teórico y pueden mutar BD. Ver `case-pointers/08-tres-reportes-marker-ejcajustedesfase.md`.

> **Bug `V-DATAWAY-002` (severidad alta)**: existen al menos **tres reportes** que calculan balance teórico — `frmStockEnUnaFecha` (estándar, base mayoritaria de clientes), `frmMovimiento_Reporte` (fiscal con control de póliza, para clientes como Cealsa, Idealsa, Cumbre) y `frmAnaliticaA` (analítica con orientación dashboard, no estaba en el modelo previo). Los tres son legítimos en su nicho, pero divergen en lógica de matching, en guards y en aritmética. Si un mismo cliente corriera los tres contra los mismos datos, los resultados serían distintos. Lo más relevante es que el reporte fiscal arrastra **deuda técnica acumulada** (cascada incompleta, guards comentados, bug de `Salidas`); y los reportes estándar + analítica + fiscal arrastran un **bloque mutador copy-pasted con bug aritmético `Diferencia += 1`** replicado tres veces.

## TL;DR

| Aspecto | `frmStockEnUnaFecha.vb` (estándar) | `frmMovimiento_Reporte.vb` (fiscal/póliza) |
|---|---|---|
| Ubicación | `Reportes/Stock_En_Una_Fecha/` | `Reportes/Fiscales/` |
| Propósito | Inventario teórico al cierre | Reporte fiscal de movimientos con trazabilidad de póliza |
| Audiencia | Mayoría de clientes | Clientes con requisito fiscal de control de póliza (Cealsa, Idealsa, Cumbre — pendiente confirmar lista completa con Erik) |
| Lógica de matching | Exacto 3 keys | Cascada 1-4 niveles |
| Guard `IdMovimiento` (anti-doble-conteo) | Ausente | **Comentado** (existió y se abandonó) |
| ModoDepuracion (muta historia) | Sí (`V-DATAWAY-001`) | No |
| Bug aritmético en suma de Salidas | No | **Sí (`V-DATAWAY-004`)** |
| Cantidad de líneas | ~1111 | ~705 |
| Comments con firma personal | "Wait a second!" + marker `#EJCAJUSTEDESFASE` | "Magia por EJC para corregir cagada" + "(Por error en el cambio de ubicación fecha_vence = now -> JP)" |

## Por qué existen dos reportes (y un tercero analítico)

**No es accidente ni clon olvidado**. Es decisión arquitectónica derivada del modelo de negocio de cada cliente. Para los dos primeros (estándar y fiscal):

### Cliente sin control de póliza (mayoría)

El cardex se lleva agrupando por `Codigo + EstadoOrigen + Fecha_Vence`. La identidad de un "punto del balance" es esa terna. Movimientos de RECE/DESP/AJ\* se acumulan ahí. El reporte canónico (`frmStockEnUnaFecha`) cubre este caso con matching exacto y suma lineal.

### Cliente con control de póliza (Cealsa, Idealsa, Cumbre, etc.)

El cardex tiene **un eje adicional de trazabilidad: la póliza** (probable referencia a póliza aduanera, póliza de importación, o póliza fiscal regulatoria — pendiente caracterizar formalmente en sub-wave dedicada). Este eje:

- Cambia la granularidad real del balance (un mismo Código + Lote puede tener stocks distintos según póliza de ingreso).
- Introduce eventos donde la fecha de vencimiento se setea o se reasigna (de ahí el bug histórico de JP — ver `CP-002`).
- Requiere agregaciones específicas para presentación fiscal.

Por eso existe un segundo reporte. La cascada de matching de 4 niveles del reporte fiscal es **necesaria para reconciliar variaciones de granularidad introducidas por la póliza**, no es deuda — es funcionalidad.

### El tercer reporte: `frmAnaliticaA` (descubierto wave 13-8)

`frmAnaliticaA.vb` vive en `Reportes/Analítica/` junto a `frmAnalitica1`, `frmDistribucionPorTramo`, `frmIndicadores`, `Dashboard1` y `frmDashView1`. Tamaño 1250 líneas. Tiene `ModoDepuracion` declarado en línea 16, activado en línea 1242, y dos bloques `If ModoDepuracion Then` mutadores (líneas 600 y 648), uno de los cuales escribe `M.Serie = "#EJCAJUSTEDESFASE"` en línea 624.

A diferencia del estándar y el fiscal, **no está claro para qué cliente o caso de negocio fue creado** — su carpeta sugiere uso analítico/dashboard. Acción pendiente: verificar si `frmAnaliticaA` aparece en el menú principal de algún cliente real, o si quedó como herramienta de back-office no expuesta. Si está oculto, el impacto en BD puede ser pequeño; si es visible, el impacto se suma al de los otros dos.

Ver `case-pointers/08-tres-reportes-marker-ejcajustedesfase.md` y bitácora `CP-008.md`.

### Lo que sí es deuda técnica del reporte fiscal

- El guard `IdMovimiento` comentado (probable intento de fix abandonado sin documentar).
- El bug aritmético `BeStockEnFecha.Salidas += ObjM.Salidas` con campo posiblemente en cero según fuente de `Get_Lista_Movimientos`.
- Los hardcodes `TheGoalDate` + `Wait a second!` + `Magia por EJC` que quedaron del último debug.

Son tres cosas distintas. Las trato como tales.

### Lo que es deuda técnica compartida por los tres mutadores

- El bloque `M.Cantidad += Diferencia / Diferencia += 1 / M.Serie = "#EJCAJUSTEDESFASE" / Eliminar/Actualizar` está copy-pasted **idéntico** en los tres reportes (`frmStockEnUnaFecha:425`, `frmMovimiento_Reporte:487`, `frmAnaliticaA:624`).
- El bug aritmético `Diferencia += 1` (cuando debería ser `Diferencia += M.Cantidad` o similar) está triplicado.
- Cualquier intento de corrección debe aplicarse a los tres sitios. Refactor candidato: extraer helper `clsAjusteDesfase.AplicarA(lMovimientos, Diferencia)`.

## Las 4 divergencias concretas

### Divergencia 1: lógica de matching

`frmStockEnUnaFecha.vb:113-117` (matching exacto):
```vb
Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                  AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen _
                                  AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)
If Idx = -1 Then
    RepMovEnUnaFecha.Add(BeStockEnFecha)
Else
    BeStockEnFecha = RepMovEnUnaFecha(Idx) 'Puntero =>
End If
```

`frmMovimiento_Reporte.vb:113-148` (cascada 4 niveles, **legítima por póliza**):
```vb
Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                  AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen _
                                  AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)

If Idx <> -1 Then 'Lo encontró por lote.
    Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
      AndAlso x.Lote = BeStockEnFecha.Lote _
      AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date)

    If Idx1 = -1 Then 'No coincide la fecha de vencimiento para el mismo lote en el mismo movimiento
        '(Por error en el cambio de ubicación fecha_vence = now -> JP.)
        Debug.Print("Espera")
        'Magia por EJC para corregir cagada.
        If RepMovEnUnaFecha(Idx).Fecha_Vence.Date > BeStockEnFecha.Fecha_Vence.Date Then
            'BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date
            Debug.Print(BeStockEnFecha.Codigo)
        End If
    End If

    'Si no tiene control por lote...
    If BeStockEnFecha.Lote = "" Then
        ' [otra cascada para producto sin lote]
    End If

Else
    Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                     AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence)
    ' [matching débil con comment Magia por EJC]
End If
```

**Lectura corregida**: la cascada **no es bug, es feature** — necesaria para casos donde el control de póliza introduce variaciones de Lote y Fecha_Vence. Lo que sí es bug:

- El re-matching que comenta `'BeStockEnFecha.Fecha_Vence = RepMovEnUnaFecha(Idx).Fecha_Vence.Date` está **inactivo** (comentado). Es decir, detecta el caso pero no corrige nada — solo escribe `Debug.Print(BeStockEnFecha.Codigo)`. Caso típico de "lo dejé a medias".

### Divergencia 2: guard `IdMovimiento` abandonado

`frmMovimiento_Reporte.vb:178` (comentado):
```vb
'ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE And BeStockEnFecha.IdMovimiento <> ObjM.IdMovimiento Then
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE Then
    BeStockEnFecha.Ingresos += ObjM.Cantidad
```

`frmMovimiento_Reporte.vb:200` (comentado):
```vb
'ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP And BeStockEnFecha.IdMovimiento <> ObjM.IdMovimiento Then
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
    BeStockEnFecha.Salidas += ObjM.Salidas
```

**Lectura**: alguien intentó evitar contar dos veces el mismo `IdMovimiento` para el mismo punto. Se comentó. `frmStockEnUnaFecha` **nunca tuvo este guard**.

**Hipótesis del por qué se abandonó (sin confirmar)**:
- (a) `IdMovimiento` cambió de semántica (de PK del movimiento a PK del Lote o del Despacho).
- (b) `VW_Movimientos` empezó a generar varias filas con el mismo `IdMovimiento` legítimamente (presentación, ubicación, póliza) y el guard rompía conteo correcto.
- (c) Se descubrió que era duplicado falso (el `Get_Lista_Movimientos` ya hacía DISTINCT).

**Acción**: necesita arqueología. Si Erik recuerda quién lo introdujo y por qué, agregar a `learnings/`.

### Divergencia 3: `V-DATAWAY-004` — suma `ObjM.Salidas`

`frmMovimiento_Reporte.vb:201`:
```vb
ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
    BeStockEnFecha.Salidas += ObjM.Salidas
```

**Diferencia con el canónico**: a primera vista son idénticos, pero la diferencia está en **de dónde viene `ObjM`**. Si en el reporte fiscal `Get_Lista_Movimientos` no consume `VW_Movimientos` (que pre-calcula `Salidas` para DESP), sino una variante distinta (por ej, una vista específica para fiscal/póliza, o tabla raw `trans_movimientos` donde `Salidas` viene null/0), el campo viene en cero y las salidas nunca se acumulan.

**Severidad alta** porque el efecto silencioso sería: balance teórico fiscal **artificialmente alto**, los analistas atribuyen a "robo" o "merma" lo que es **un campo no leído**.

**Pendiente confirmar**: localizar `Get_Lista_Movimientos` en cada archivo y comparar fuente.

### Divergencia 4: ModoDepuracion ausente

`frmMovimiento_Reporte` **no tiene** `ModoDepuracion`. No muta `trans_movimientos`. No borra `Diferencias_movimientos`. No tiene Ctrl+D. Es un reporte **puro de lectura**.

**Lectura** (corregida): no es virtud, es **diferencia de origen funcional**. El reporte fiscal nunca debió mutar (un reporte fiscal escribiendo a `trans_movimientos` violaría el requisito regulatorio). El canónico sí lo permitió porque históricamente sirvió también como herramienta operativa.

## Recomendaciones (corregidas)

### R1 — caracterizar formalmente "control de póliza" como capa del modelo

Antes de cualquier limpieza de los reportes, entender qué es póliza en el modelo de datos:
- ¿Es un campo en `trans_movimientos`?
- ¿Es una tabla aparte que se joinea por `IdPoliza`?
- ¿Es un eje de particionamiento implícito por cliente (algunos lo usan, otros no)?
- ¿Quién lo escribe, quién lo lee?

Sub-wave dedicada (`02-particionamiento-licencia/` y/o nuevo `08-control-poliza/`).

### R2 — extraer la lógica de cálculo a una clase única, **parametrizada por dimensión póliza**

Hoy el cálculo del balance vive **clonado** en dos archivos. La parte que es genuinamente común (acumulación por TipoTarea, fórmula de balance) debería vivir en una clase única. La parte que difiere (matching cascada para clientes con póliza) debería ser una estrategia inyectada.

Propuesta:
```vb
Public Interface IEstrategiaMatching
    Function Encontrar(Repos As List(Of clsBeStockEnUnaFecha), Obj As clsBeMovimiento) As Integer
End Interface

Public Class clsCalculadorBalance
    Public Function Calcular(ListaMovimientos As List(Of clsBeMovimiento), _
                             EstrategiaMatching As IEstrategiaMatching) As List(Of clsBeStockEnUnaFecha)
End Class
```

Implementaciones: `clsMatchingExacto` (estándar), `clsMatchingCascadaPoliza` (fiscal). Ambos reportes consumen `clsCalculadorBalance` y se diferencian solo en qué estrategia inyectan.

### R3 — confirmar `V-DATAWAY-004` con datos reales

Antes de marcar como bug confirmado, ejecutar el reporte fiscal sobre un cliente con póliza y despachos conocidos. Verificar si las salidas suman.

Query de verificación:
```sql
SELECT Fecha, TipoTarea, Cantidad, Salidas, IdMovimiento
FROM VW_Movimientos          -- o la vista que use el reporte fiscal
WHERE IdProducto = @IdProducto
  AND TipoTarea = 'DESP'
  AND Fecha BETWEEN @From AND @To
ORDER BY Fecha
```

Si `Salidas` viene poblado para DESP en la vista que consume el fiscal, el bug es teórico. Si viene en cero, es real.

### R4 — case-pointers `CP-002`, `CP-003` (reporte fiscal)

Documentados en sub-wave 13-7, pendientes de confirmación con casos reales del cliente afectado.

## Cross-refs

- `modelo-conceptual.md` — fórmula canónica del balance
- `granularidad-y-keys.md` — detalle de la cascada de matching del reporte fiscal
- `tipos-tarea-relevantes.md` — switch de TipoTarea con guards comentados
- `anti-patron-modo-depuracion.md` — `V-DATAWAY-001` (solo en estándar)
- `07-correlacion-codigo-data/case-pointers/02-frmmovreporte-fecha-vence-now-jp.md` — CP-002
- `07-correlacion-codigo-data/case-pointers/03-frmmovreporte-magia-ejc.md` — CP-003
- `07-correlacion-codigo-data/case-pointers/04-frmmovreporte-thegoaldate-declaracion.md` — CP-004
- `07-correlacion-codigo-data/case-pointers/05-frmmovreporte-breakpoint-fecha.md` — CP-005
- `07-correlacion-codigo-data/case-pointers/06-frmmovreporte-breakpoint-triple.md` — CP-006
- `07-correlacion-codigo-data/case-pointers/08-tres-reportes-marker-ejcajustedesfase.md` — CP-008 (familia expandida)
