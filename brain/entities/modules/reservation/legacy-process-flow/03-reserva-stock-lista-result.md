# `Reserva_Stock_Lista_Result` — adapter de TRASLADOS con split físico de stock

> **Archivo**: `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
> **Anchor**: L858 → L1458 (601 líneas)
> **Visibilidad**: `Public Shared Function ... As List(Of clsBeStock_res)`
> **Hermano YAML**: `03-reserva-stock-lista-result.yml`

---

## Hallazgos críticos

### V-002 · Hardcode BYB (segunda aparición)

```vb
' L1133 y L1236 (dentro del loop B009):
If pBeConfigEnc.Codigo_proveedor_produccion = "1060315" Then
```

Mismo literal `"1060315"` que en `Reserva_Stock_NAV_BYB`. Acumulamos: 3 apariciones del hardcode entre los dos adapters. Q-LR-04 pendiente: `rg "1060315"` sobre todo el repo.

### V-003 · Violación de single responsibility (más grave que el hardcode)

Esta función **MUTA la tabla `stock` física**, no solo `stock_res`. Cuando se cumple el hotfix BYB y hay fracción decimal en presentación, ejecuta:

```vb
clsLnStock.Insertar(BeStockDestino, lConnection, ltransaction)        ' INSERT en stock con No_bulto=1989
vStockOrigen.Cantidad -= vCantidadDecimalUMBasStock
If vStockOrigen.Cantidad > 0 Then
    clsLnStock.Actualizar_Cantidad(vStockOrigen, ...)                  ' UPDATE stock origen
Else
    clsLnStock.Eliminar_By_IdStock(vStockOrigen.IdStock, ...)          ' DELETE stock origen
End If
```

Es decir: **una función que se llama "Reserva_Stock_..." está splitteando físicamente el stock** (parte cajas en unidades sueltas). Para el caller no es obvio que invocar esta función puede crear nuevos `IdStock` y eliminar el original. Q-LR-03 pendiente: el motor nuevo .NET 8 ¿separó split físico de reserva?

---

## Resumen ejecutivo

Adapter para el **flujo de TRASLADOS internos** (bodega-a-bodega). Lo identifica el parámetro `pBeTrasladoDet As clsBeI_nav_ped_traslado_det` que recibe ByRef y muta. Diferencias estructurales clave:

| Aspecto | Core | Lista_Result |
|---|---|---|
| Return type | `Boolean` | `List(Of clsBeStock_res)` (caller recibe la lista) |
| Maneja política parcial | ❌ (siempre throw) | ✅ vía `Despachar_existencia_parcial` enum |
| Muta `stock` físico | ❌ | ✅ Insert/Update/Delete |
| Muta `i_nav_ped_traslado_det` | ❌ | ✅ `Is_Partially_Processed`, `Qty_to_Receive` |
| Retry de `lStock` | ❌ | ✅ segunda llamada con `Conmutar=True` si la primera fue vacía |
| Marker físico | n/a | `No_bulto = 1989` (vs MI3 que usa 1965) |
| Indicador stock_res | dinámico (`pStockRes.Indicador`) | hardcoded `"PED"` |
| Color/Talla | sí | ❌ |
| Recursión | sí (fracción) | ❌ (no se autollama; pero **muta stock** que es el equivalente físico) |

---

## Tabla de bloques lógicos

| Bloque | Líneas | Tipo | Resumen |
|---|---|---|---|
| B001 | 868 | init | `= Nothing` |
| B002 | 870-end | try_catch | wrapper |
| B003 | 885-896 | branch+db_read | resolución `BeConfigEnc` (mismo dead branch que el core) |
| B004 | 898-920 | db_read+retry | `lStock` con segunda llamada como fallback |
| B005 | 925-961 | loop+db_read | restar reservas UMBas (sin multiplicar por Factor — decisión #CKFK20220321) |
| B006 | 963 | branch | `Count = 0 → ErrorS0004` |
| B007 | 979-1057 | branch+math | normalización cantidad |
| B008 | 1064-1087 | branch+db_write | validación stock + política `Despachar_existencia_parcial` |
| B009 | 1099-end | loop+branch+db_write | iteración con SPLIT FÍSICO BYB |
| B010 | post_loop | db_write | persistencia via `Inserta_Stock_Reservado` |
| B011 | post_catch | catch | log + re-throw |

---

## Particularidades únicas

### B004 · Retry con `Conmutar=True`

```vb
lBeStockExistente = clsLnStock.lStock(pStockRes, BeProducto, ..., Conmutar_Umbas_A_Presentacion)

If (lBeStockExistente.Count = 0 AndAlso Not Conmutar_Umbas_A_Presentacion) AndAlso BeConfigEnc.Explosion_Automatica Then
    lBeStockExistente = clsLnStock.lStock(pStockRes, BeProducto, ..., True)
End If
```

Si la primera lectura del stock devuelve vacío y el cliente tiene `Explosion_Automatica`, se reintenta forzando `Conmutar_Umbas_A_Presentacion = True`. Esta lógica de retry **no existe en el core** ni en NAV_BYB. Significa que `lStock` con `Conmutar=True` puede materializar stock que con `Conmutar=False` no aparecería. Q-LR-10 pendiente.

### B008 · Política de reserva parcial vía `Despachar_existencia_parcial`

```vb
If vCantidadSolicitadaPedido > vCantidadDispStock Then
    If pBeConfigEnc.Despachar_existencia_parcial = tDespacharExistenciaParcial.No Then
        Throw New Exception(... ErrorS0002 ...)
    Else
        If vCantidadDispStock > 0 Then
            pBeTrasladoDet.Qty_to_Receive = vCantidadDispStock
            pBeTrasladoDet.Is_Partially_Processed = True
            clsLnI_nav_ped_traslado_det.Actualizar_Partially_Processed(pBeTrasladoDet, ..)
        End If
    End If
Else
    pBeTrasladoDet.Is_Partially_Processed = False
End If
```

Acá está la única función del archivo que **honora** la política `Despachar_existencia_parcial`. Si el cliente la tiene en algo distinto de `No`, el adapter:
1. Pone `Is_Partially_Processed = True` en el detalle del traslado
2. Pone `Qty_to_Receive = disponible` (no `solicitado`)
3. Persiste el cambio
4. Sigue con el flujo de reserva tomando lo que hay

Esto significa que **el caller de un traslado puede recibir una reserva parcial** sin enterarse explícitamente — solo notará que el `pBeTrasladoDet` quedó con flag de parcialmente procesado. Q-LR-07 pendiente: enumerar valores del enum.

### B009 · Split físico (el monstruo)

Cuando entra el hotfix BYB y hay fracción decimal en stock con presentación:

```vb
' Insertar el stock en umbas y asociar a la reserva
BeStockDestino.Cantidad = vCantidadDecimalUMBasStock
BeStockDestino.Fec_agr = Now
BeStockDestino.IdPresentacion = 0          ' UMBas
BeStockDestino.IdStock = 0                  ' will be assigned by Insertar
BeStockDestino.No_bulto = 1989              ' marker BYB-split
clsLnStock.Insertar(BeStockDestino, ..)

' Quitar al stock en cajas, las unidades
vStockOrigen.Cantidad -= vCantidadDecimalUMBasStock
If vStockOrigen.Cantidad > 0 Then
    clsLnStock.Actualizar_Cantidad(vStockOrigen, ..)
Else
    clsLnStock.Eliminar_By_IdStock(vStockOrigen.IdStock, ..)
End If
```

El marker **`No_bulto = 1989`** distingue stocks creados por este split físico (vs `No_bulto = 1965` del MI3). Q-LR-02: confirmar la semántica de los números — ¿año de algo? ¿fecha personal? Igual que el `1965` de MI3, candidate a entrada `naked-erik-anatomy/`.

### Trampas de debug abandonadas

```vb
' L1108-1110:
If vStockOrigen.IdStock = 3612 Then
    Debug.Write("Espera que el factor es 0.")
End If

' L1197:
Debug.Write("#EJC20220510: something is missing but still I don't know what it is.")
```

El primero es un trap puntual sobre `IdStock=3612` — Q-LR-05 pendiente, ¿quedó algún caso real que rompía con ese stock?
El segundo es un **TODO admitido sin resolver desde 2022** — Q-LR-06 con severidad alta. Es muy probable que el motor nuevo no haya migrado este TODO porque el reescritor no sabía qué resolver.

### `vRecursionSolicitud` — vestigio sin uso

```vb
Private Shared vRecursionSolicitud As Integer = 0
```

Declarada justo antes de la firma de la función. **No se incrementa visiblemente** en el bloque que leí. Q-LR-01: ¿se usa en otro overload del archivo (los 7 restantes) o quedó como vestigio?

---

## Reglas extraídas (R001-R014)

Ver `03-reserva-stock-lista-result.yml`. Las **más críticas para gap analysis**:

- **R003** (retry de lStock con `Conmutar=True`) — gap probable en el motor nuevo si no replicaron el fallback.
- **R006** (política `Despachar_existencia_parcial`) — única función del archivo que la honra; si el motor nuevo no expone este flag, los TRASLADOS pierden la capacidad de operar parciales.
- **R008** (split físico de stock) — efecto colateral grave; gap arquitectónico si el nuevo no separó esto.
- **R009** (marker `No_bulto = 1989`) — convención no documentada; si el nuevo usa otro marker, queries de auditoría rompen.

---

## Comparación contra .NET 8 (placeholder Wave 13)

Esta es la función **más rica funcionalmente** de las inventariadas hasta ahora. Hipótesis a verificar en Wave 13:

1. ¿El motor nuevo separó `StockSplitterStep` de `StockReservationStep`?
2. ¿Existe un step para gestionar `Despachar_existencia_parcial` o el motor nuevo siempre tira `Throw` y delega al caller la decisión?
3. ¿La actualización de `i_nav_ped_traslado_det` se hace dentro del step o el caller la hace explícitamente?
4. ¿El TODO de `L1197 "something is missing"` se resolvió en el reescrito o se trasladó como `// TODO`?
5. ¿El marker `1989` quedó en algún enum/constante o se eliminó al migrar?

Si el reescrito ignoró este adapter (probable, dado que el brain previo no lo tenía mapeado), **los flujos de TRASLADO BYB perderían**:
- la política `Despachar_existencia_parcial`
- el split físico automático
- el flag `Is_Partially_Processed` actualizado

Esto es **gap funcional grave** que justifica esta wave.
