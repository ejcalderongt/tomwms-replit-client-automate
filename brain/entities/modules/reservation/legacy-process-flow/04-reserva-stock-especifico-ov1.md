# `Reserva_Stock_Especifico` (overload 1) — reserva forzada sobre un IdStock puntual

> **Archivo**: `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
> **Anchor**: L1459 → L1611 (153 líneas — la más chica del archivo)
> **Visibilidad**: `Public Shared Function ... As Boolean`
> **Hermano YAML**: `04-reserva-stock-especifico-ov1.yml`

---

## Bug crítico (B-ESP1-01)

```vb
' L1554-1557:
ElseIf vCantidadPendiente > vCantidadStock Then
    vCantidadAReservarPorIdStock = vCantidadStock
    vCantidadPendiente -= vCantidadStock
End If
' ... después continúa derecho a Inserta_Stock_Reservado y retorna True ...
```

Si lo solicitado supera el stock disponible del `IdStock` específico, la función **reserva lo que hay y deja un residuo de `vCantidadPendiente` sin nadie que lo reserve**. Y como **NO hay loop** (es una sola pasada sobre un solo stock), no hay continuación posible. El caller recibe `Reserva_Stock_Especifico = True` y asume que todo se reservó. Es **reserva parcial silenciosa**.

Comparación:
- Core (`Reserva_Stock`): tira `ErrorS0002` si solicitado > disponible total.
- NAV_BYB: tira `ErrorS0002` o `ErrorS0002A` según modo.
- Lista_Result: usa política `Despachar_existencia_parcial` y actualiza `Is_Partially_Processed`.
- **Especifico ov1: silencioso.** El caller nunca se entera.

Q-ESP1-03 con severidad **crítica**: confirmar si esto es bug o feature. Si el caso de uso real es "handheld escanea un LP, el operador pide reservar X unidades y el LP solo tiene Y < X", el comportamiento esperado debería ser ERROR, no reserva silenciosa.

---

## Resumen ejecutivo

La función **más chica** del archivo (153 líneas) y la **más simple**: reserva sobre un único stock provisto por el caller. No consulta `lStock`, no itera, no tiene recursión. Caso de uso típico: el handheld escanea un LP o ubicación específica y el operador decide reservar de ese stock puntualmente.

| Aspecto | Core | Especifico ov1 |
|---|---|---|
| Consulta `lStock` | ✅ | ❌ (recibe el stock del caller) |
| Itera lista | ✅ For Each | ❌ (un solo stock) |
| Recursión por fracción | ✅ | ❌ |
| Cache de presentación | ✅ `lPresentaciones` | ❌ lectura directa |
| Política partial | throw | **silenciosa** |
| Color/Talla/TallaColor | ✅ | ❌ |
| Indicador stock_res | dinámico | hardcoded `"PED"` |
| Estado | `"UNCOMMITED"` | `"UNCOMMITED"` (idem) |
| `DiasVencimiento` | usado | **declarado, nunca usado** |
| Líneas | 290 | 153 |

---

## Tabla de bloques lógicos

| Bloque | Líneas | Tipo | Resumen |
|---|---|---|---|
| B001 | 1466 | init | `= False` |
| B002 | 1468-end | try_catch | wrapper |
| B003 | 1477-1483 | db_read | `Get_Cantidad_Reservada_By_IdStock` (sin sufijo UMBas — Q-ESP1-01) |
| B005 | 1494-1534 | branch+math | normalización (sin Explosion_Automatica) |
| B006 | 1536-1538 | assign | inicializar vCantidadPendiente y vCantidadStock |
| B007 | 1540-1591 | branch+assign | construcción de **UN solo** `BeStockRes` |
| B008 | 1593-1603 | branch+db_write | persistencia |
| B010 | 1605-1608 | catch | log + re-throw |

(No hay B004 ni B009 explícitos — la función no necesita query inicial ni loop.)

---

## Particularidades únicas

### Vestigios de copy-paste

```vb
Dim BeProducto As New clsBeProducto              ' L1471 — declarada, nunca usada
Dim StockOrdenado As Object = Nothing            ' L1485 — declarada Object=Nothing, nunca tocada
Dim vCantidadCompletada As Boolean = False       ' L1487 — asignada al final, nunca consultada (no hay loop)
```

Estas tres declaraciones son **rastros de la plantilla** que usó quien implementó esta función a partir del core. Fueron copiadas pero la lógica que las consumía no se replicó. Q-ESP1-04 sugiere que la asimetría con el overload 2 (8.242 líneas) merece una pregunta arquitectónica.

### `DiasVencimiento` parámetro inerte

La firma incluye `DiasVencimiento As Double`. Lo lógico sería que se pase a `lStock` para filtrar por fecha de vencimiento. Pero esta función **no llama a `lStock`** (recibe el stock del caller), así que `DiasVencimiento` queda inerte. **Riesgo de confusión**: cualquier caller leyendo solo la firma asume que el filtro existe. Q-ESP1-02 pide investigar si esto es restos del template.

### Branch defensive inalcanzable

```vb
If lBeStockAReservar.Count > 0 Then
    If Inserta_Stock_Reservado(...) Then
        Reserva_Stock_Especifico = True
    Else
        Throw New Exception(... ErrorS0006 ...)
    End If
Else
    Throw New Exception(... ErrorS0005 ...)
End If
```

El `Else` con `ErrorS0005` es **inalcanzable**: B007 siempre hace `lBeStockAReservar.Add(BeStockRes)` antes de llegar a este punto, por lo que la lista siempre tiene al menos 1 elemento. Mismo patrón que el core (defensive imposible).

---

## Reglas extraídas (R001-R011)

Ver `04-reserva-stock-especifico-ov1.yml`. Las **más relevantes**:

- **R002** (`Get_Cantidad_Reservada_By_IdStock` sin UMBas) — Q-ESP1-01: ¿qué retorna distinto que la versión UMBas que usa el core?
- **R007** (reserva parcial silenciosa) — el bug B-ESP1-01.
- **R010** (`DiasVencimiento` inerte) — gap de contrato.

---

## Comparación contra .NET 8 (placeholder Wave 13)

Hipótesis del equivalente .NET 8: `StockReservationFacade.ReserveSpecific(stockEspecifico, quantity)` o un step `ForceReserveSpecificStockStep`. Pendiente verificar:

1. ¿El motor nuevo expone una API explícita para reservar contra un `IdStock` puntual?
2. ¿El comportamiento "reserva parcial silenciosa" se corrigió a throw o se mantiene?
3. ¿`DiasVencimiento` se eliminó de la firma o sigue inerte?
4. ¿Color/Talla/IdProductoTallaColor se agregaron al flujo específico?

Si el motor nuevo "absorbió" este caso dentro del flujo principal asumiendo que toda reserva pasa por `lStock`, **se pierde el caso de uso de handheld escaneando un LP específico**. Es gap a verificar en Wave 13.

---

## Nota sobre asimetría con overload 2

Existe un segundo `Reserva_Stock_Especifico` (L1612, **8.242 líneas** — el monstruo no documentado del archivo). La asimetría brutal entre los dos overloads (153 vs 8.242) sugiere que:

1. El overload 1 es la versión "puntual / handheld / 1-stock"
2. El overload 2 es la versión "lista / batch / multi-stock"

Pero esto es especulación hasta que se inventaríe el overload 2 en sub-wave 12B. Q-ESP1-04 abierta.
