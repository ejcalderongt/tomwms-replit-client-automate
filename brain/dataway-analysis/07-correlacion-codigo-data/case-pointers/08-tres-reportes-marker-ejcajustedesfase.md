# CP-008 — Tres reportes con marker `Serie = "#EJCAJUSTEDESFASE"`

> El bloque mutador del `ModoDepuracion` con el marker `#EJCAJUSTEDESFASE` está copy-pasted **idéntico** en **tres reportes**, no uno: `frmStockEnUnaFecha` (estándar), `frmMovimiento_Reporte` (fiscal/póliza) y `frmAnaliticaA` (analítica). Esto **expande el alcance del `V-DATAWAY-001`** de un único reporte a una familia de tres mutadores, todos con el mismo bug aritmético `Diferencia += 1` y la misma huella en BD.

## Resumen

| Campo | Valor |
|---|---|
| ID | CP-008-tres-reportes-marker-ejcajustedesfase |
| Tipo | hardcode persistente en BD (3 ocurrencias idénticas) |
| Estado | documentado — efecto persistente en producción confirmado por estructura del código |
| Severidad estimada | **alta** (expande `V-DATAWAY-001` a familia de 3 reportes) |
| Bug enlazado | `V-DATAWAY-001` (anti-patrón ModoDepuracion) — **alcance triplicado** |
| Persistencia | **sí** — escribe a `trans_movimientos.Serie` desde tres puntos distintos |
| Cross-link | espejo expandido de CP-007 (que solo cubría `frmStockEnUnaFecha`) |
| Detectado en | wave-13-8 vía búsqueda `rg 'Serie\s*=\s*"#[A-Za-z]'` |

## Las tres ocurrencias

| Reporte | Archivo | Línea marker | Línea bug `Diferencia += 1` |
|---|---|---|---|
| Estándar | `Reportes/Stock_En_Una_Fecha/frmStockEnUnaFecha.vb` | 425 | 419 |
| Fiscal/póliza | `Reportes/Fiscales/frmMovimiento_Reporte.vb` | 487 | 481 |
| Analítica | `Reportes/Analítica/frmAnaliticaA.vb` | 624 | 618 |

## Cita textual del bloque copy-pasted

El siguiente bloque aparece **idéntico** (mismo indent, mismas variables, mismo bug) en los tres reportes:

```vb
If M.Cantidad >= Math.Abs(Diferencia) Then
    M.Cantidad += Diferencia
    Diferencia += 1                            ' bug aritmetico replicado x3
Else
    M.Cantidad = 0
    Diferencia += M.Cantidad
End If

M.Serie = "#EJCAJUSTEDESFASE"                  ' marker persistente x3

If M.Cantidad = 0 Then
    clsLnTrans_movimientos.Eliminar(M)
Else
    clsLnTrans_movimientos.Actualizar(M)
End If
```

Las únicas diferencias entre los tres reportes son: el contexto envolvente (granularidad de matching, guards previos), el `lblPrg.Text` posterior, y el número de línea.

## Caracterización de `frmAnaliticaA`

| Aspecto | Valor |
|---|---|
| Carpeta | `TOMIMSV4/TOMIMSV4/Reportes/Analítica/` |
| Tamaño | 1250 líneas |
| Tipo | Reporte analítico — `Public Class frmAnaliticaA Inherits` (form WinForms con DevExpress) |
| Vecinos | `frmAnalitica1`, `frmDistribucionPorTramo`, `frmIndicadores`, `Dashboard1`, `frmDashView1` |
| Propósito aparente | Reporte de análisis de stock en una fecha (similar a `frmStockEnUnaFecha` pero con orientación analítica/dashboard) |
| Tiene `ModoDepuracion`? | sí, declarado en L16, activado en L1242 |
| Tiene `If ModoDepuracion Then ...` mutador? | sí, en L514, L600, L648 |

`frmAnaliticaA` es un **tercer hermano** de la familia de reportes que mutan datos cuando se les activa `ModoDepuracion`. No estaba mapeado en capa 04 antes de wave 13-8.

## Mapa expandido del `ModoDepuracion`

Búsqueda `rg 'ModoDepuracion' /tmp/repos/TOMWMS_BOF/TOMIMSV4/TOMIMSV4/Reportes/` revela:

### 15 reportes con la propiedad declarada

| Carpeta | Reporte | Activación (`= True`) | Mutador (`If ... Then`) | Marker `#EJCAJUSTEDESFASE` |
|---|---|---|---|---|
| Stock_En_Una_Fecha | `frmStockEnUnaFecha` | L984 | L401, L449 | **sí (L425)** |
| Analítica | `frmAnaliticaA` | L1242 | L600, L648 | **sí (L624)** |
| Fiscales | `frmMovimiento_Reporte` | (no encontrada activación explícita en grep) | L463, L511 | **sí (L487)** |
| Stock/Stock_Fiscal | `FrmStock_Fiscal` | L137 | (sin mutador) | no |
| Log_Error_WMS | `frmLogErrorWMS` | L473 | (sin mutador) | no |
| Doc_Diferencias | `frmDocPeConDiferencias` | L515 | (sin mutador) | no |
| Doc_Diferencias | `frmDocConDiferencias` | L399 | (sin mutador) | no |
| Trazabilidad_Lote | `frmMovimientosporLote` | (sin activación) | (sin mutador) | no |
| Fiscales | `frmStockResJornadaMerca` | (sin activación) | (sin mutador) | no |
| Fiscales | `frmStockResJornada` | (sin activación) | (sin mutador) | no |
| Fiscales | `frmResumenPorCliente` | (sin activación) | (sin mutador) | no |
| Fiscales | `frmResumenHistorico` | (sin activación) | (sin mutador) | no |
| Fiscales | `frmCtasOrden` | (sin activación) | (sin mutador) | no |
| Fiscales | `frmCtaOrdenPoliza` | (sin activación) | (sin mutador) | no |
| Fiscales | `frmMovimientos_Retroactivo` | (sin activación) | (sin mutador) | no |
| Fiscales | `frmMercaVencida` | (sin activación) | (sin mutador) | no |

### Clasificación

- **Mutadores activos con marker** (3): los que mutan BD y dejan huella → `frmStockEnUnaFecha`, `frmAnaliticaA`, `frmMovimiento_Reporte`. **Son los responsables del `V-DATAWAY-001`**.
- **Activadores sin mutador localizable** (4): tienen `ModoDepuracion = True` pero no se encontró bloque `If ModoDepuracion Then` que mute BD. Probablemente lo usan como flag de logging o para mostrar columnas extra. Requiere investigación de detalle.
- **Declarativos puros** (8): solo declaran la propiedad heredada, pero no la activan ni la usan para mutar. Es **scaffolding inerte** copiado entre reportes hermanos por convención.

## Por qué CP-008 promueve a alta severidad el `V-DATAWAY-001`

Hasta wave 13-7 el `V-DATAWAY-001` se modelaba como "anti-patrón en `frmStockEnUnaFecha`". Wave 13-8 demuestra que:

1. **Tres reportes mutan BD** con el mismo bloque, no uno.
2. Cada cliente con control de póliza (que usa `frmMovimiento_Reporte`) puede tener movimientos marcados con `#EJCAJUSTEDESFASE` provenientes de **dos fuentes distintas** (estándar + fiscal). Si además se corrió `frmAnaliticaA`, son **tres**.
3. El bug aritmético `Diferencia += 1` está triplicado por copy-paste — corregirlo en uno no corrige los otros dos.
4. La query 06 (que cuenta movimientos con `Serie = "#EJCAJUSTEDESFASE"`) **mide el efecto agregado de los tres mutadores**, no de uno. El número puede ser hasta 3× lo asumido.

## Recomendaciones (NO ejecutar esta wave)

1. **Promover `V-DATAWAY-001` a `severidad: critica`** una vez que la query 06 confirme el conteo en producción.
2. **Refactor candidato**: extraer el bloque mutador a un helper `clsAjusteDesfase.AplicarA(lMovimientos, Diferencia)` para que el bug se corrija en un solo lugar. Pero esto **no es trivial** porque cada reporte llega al bloque con guards y matchings distintos.
3. **Revisión de los 4 "activadores sin mutador localizable"**: confirmar que efectivamente no muten BD. Si alguno también tiene marker, agregar a la familia.

## Cross-refs

- `dataway-analysis/04-ecuacion-de-balance/anti-patron-modo-depuracion.md` — origen del `V-DATAWAY-001`, requiere update con familia expandida
- `dataway-analysis/04-ecuacion-de-balance/divergencia-reportes-paralelos.md` — modelo de "reportes paralelos" se queda corto (son 3, no 2)
- `case-pointers/07-stockfecha-marker-ejcajustedesfase.md` — predecesor (cubría solo 1 reporte)
- `brain/debuged-cases/CP-008.md` — bitácora viva
- `tools/case-seed/queries/data-discrepancy/06_movimientos_ejcajustedesfase.sql` — query a implementar (debe contar por reporte de origen si es posible)

## Acción forense complementaria (futura)

Verificar si `frmAnaliticaA` aparece en el menú de algún cliente real. Si está oculto en una rama del menú que casi nadie usa, el impacto en BD puede ser pequeño. Si está en el panel principal de algún cliente, el impacto es alto.

```bash
# Buscar referencias a frmAnaliticaA desde otros forms o desde el menú
rg -n 'frmAnaliticaA' /tmp/repos/TOMWMS_BOF/ --type vb
```
