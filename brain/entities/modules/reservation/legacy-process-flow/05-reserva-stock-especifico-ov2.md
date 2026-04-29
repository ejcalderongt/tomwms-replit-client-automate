# Reserva_Stock_Especifico — overload 2 (con out-list + peso)

> **Archivo legacy**: `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
> **Rango**: L1612-L1797 (186 líneas)
> **Versión documentada en**: sub-wave 12B-1, 2026-04-29
> **Relación con overload 1**: comparte aproximadamente el 85% del cuerpo. Documento solo los DELTAS aquí; para detalle de los bloques compartidos consultar `04-reserva-stock-especifico-ov1.md`.

---

## Resumen

OV2 es la versión "más capaz" de `Reserva_Stock_Especifico`. Hace exactamente lo mismo que OV1 (reservar stock contra un `IdStock` específico ya identificado por el caller), pero **con dos capacidades adicionales**:

1. Devuelve al caller la lista de `stock_res` que insertó (vía parámetro `ByRef pListStockResOUT`).
2. Mantiene contabilidad **paralela de peso** además de cantidad, y la propaga al `stock_res` creado.

OV1 retorna solo `True/False` y no toca peso explícitamente.

**Heredas todos los bugs de OV1**: el hardcode `"PED"`, el typo `"UNCOMMITED"`, la reserva parcial silenciosa, el `clsPublic.CopyObject` que sobrescribe seteos manuales, y la mutación in-place del parámetro `pBeStockEspecifico`. Ver YAML `violaciones_y_bugs:` para el listado completo con cross-refs.

---

## Firma comparada

```vb
' OV1 — 6 parámetros
Public Shared Function Reserva_Stock_Especifico(
    ByRef pStockRes As clsBeStock_res,
    ByVal DiasVencimiento As Double,
    ByVal MaquinaQueSolicita As String,
    ByRef pBeStockEspecifico As clsBeStock,
    ByRef lConnection As SqlConnection,
    ByRef ltransaction As SqlTransaction
) As Boolean

' OV2 — 7 parámetros (agrega pListStockResOUT en posición 4)
Public Shared Function Reserva_Stock_Especifico(
    ByRef pStockRes As clsBeStock_res,
    ByVal DiasVencimiento As Double,
    ByVal MaquinaQueSolicita As String,
    ByRef pListStockResOUT As List(Of clsBeStock_res),     ' ← NUEVO
    ByRef pBeStockEspecifico As clsBeStock,
    ByRef lConnection As SqlConnection,
    ByRef ltransaction As SqlTransaction
) As Boolean
```

VB.NET resuelve overloads por arity y tipo, así que ambas conviven sin colisión.

---

## Los 3 deltas concretos

### DELTA-OV2-001 — Out-list al caller

OV2 escribe la lista de `clsBeStock_res` insertados en `pListStockResOUT` antes de retornar `True`:

```vb
If Inserta_Stock_Reservado(lBeStockAReservar, lConnection, ltransaction) Then
    pListStockResOUT = lBeStockAReservar      ' ← OV1 no hace esto
    Reserva_Stock_Especifico = True
```

**Implicación**: el caller que necesite saber **qué** `IdStockRes` se crearon (para luego actualizarles el `Estado` a `"COMMITTED"`, o para auditoría/log/post-procesamiento) debe usar OV2. OV1 sólo te dice "funcionó/no funcionó" sin handle al objeto creado.

### DELTA-OV2-002 — Llamada distinta para "cantidad ya reservada"

```vb
' OV1 (L1481)
vCantidadReservada = Get_Cantidad_Reservada_By_IdStock(IdStock, lConnection, ltransaction)

' OV2 (L1633)
Get_Cantidad_Y_Peso_ReservadaUMBas_By_IdStock(
    pBeStockEspecifico.IdStock,
    False,                                         ' ← flag booleano de semántica desconocida (Q-OV2-02)
    vCantidadReservada,
    vPesoReservado,
    lConnection,
    ltransaction
)
```

OV2 usa una función helper diferente que devuelve **cantidad Y peso** reservado, en UMBas, con un flag booleano cuya semántica es desconocida (Q-OV2-02 — probablemente "incluir reservas en estado X" pero hay que verificar la firma).

### DELTA-OV2-003 — Stream paralelo de peso

OV2 declara cuatro variables adicionales:

```vb
Dim vPesoStock As Double = 0
Dim vPesoPendiente As Double = 0
Dim vPesoSolicitadoPedido As Double = 0
Dim vPesoAReservarPorIdStock As Double = 0
```

Y las propaga en **cada bifurcación** donde OV1 sólo maneja cantidad: en el cálculo de `vCantidadSolicitadaPedido` según presentación, en el bloque de matching cantidad-vs-stock (3 ramas), y al final asigna:

```vb
BeStockRes.Cantidad = Math.Round(vCantidadAReservarPorIdStock, 6)
BeStockRes.Peso = Math.Round(vPesoAReservarPorIdStock, 6)   ' ← OV1 no asigna esto
```

**Implicación crítica**: OV1 hereda el peso del stock origen completo vía `clsPublic.CopyObject(pBeStockEspecifico, BeStockRes)`, lo que deja un peso INCONSISTENTE con la cantidad parcial reservada (si reservó 5 de 10, el peso reservado quedaría como el peso de los 10). OV2 corrige esto asignando peso explícito proporcional.

**Si OV1 todavía está siendo llamada por algún flujo y ese flujo le importa el peso**, hay un bug latente en OV1 que OV2 esquivó.

---

## Bloques compartidos con OV1 (no se re-documentan acá)

Para los siguientes bloques el código es prácticamente idéntico (excepto por la propagación de peso descrita en DELTA-OV2-003). Consultar `04-reserva-stock-especifico-ov1.md`:

- **Bloque "cálculo cantidad solicitada según presentación"** (L1654-1693): pallet vs no-pallet, factor, control por peso, redondeo a entero o a 6 decimales.
- **Bloque "matching cantidad pendiente vs stock disponible"** (L1709-1724): tres ramas (==, <, >). La rama `>` es la que tiene el bug crítico **V-OV2-003 / V-OV1-003** (reserva parcial silenciosa).
- **Bloque "armado del clsBeStock_res"** (L1701-1774): seteos manuales + `clsPublic.CopyObject` + propagación talla/color (post-#GT09012025) + Fec_agr override (post-#GT08102025).
- **Bloque "insert + return"** (L1778-1789): Inserta_Stock_Reservado + return True / throw S0006 / throw S0005.

---

## Bugs y violaciones (todas heredadas de OV1, ninguna nueva)

| ID | Severidad | Descripción | Línea |
|---|---|---|---|
| V-OV2-001 | MEDIA | Hardcode `Indicador = "PED"` (viola L-035) | 1703 |
| V-OV2-002 | BAJA | Typo `Estado = "UNCOMMITED"` (debería ser `UNCOMMITTED`) | 1744 |
| V-OV2-003 | **ALTA** | Reserva parcial silenciosa cuando pendiente > stock | 1719 |
| V-OV2-004 | **ALTA** | `CopyObject` sobrescribe seteos manuales previos | 1726 |
| V-OV2-005 | MEDIA | Muta `pBeStockEspecifico.Cantidad` in-place (side-effect ByRef) | 1638 |
| V-OV2-006 | MEDIA | Ignora `Despachar_existencia_parcial` (inconsistencia con Lista_Result) | 1719 |

Cross-refs detalladas en el YAML.

---

## Mutaciones de estado

| Destino | Operación | Detalle |
|---|---|---|
| Tabla `stock_res` | INSERT 1 fila | siempre exactamente una; vía `Inserta_Stock_Reservado` (L1780) |
| Tabla `stock` | **ninguna** | a diferencia de `Reserva_Stock_Lista_Result` que sí splittea físicamente (V-002, V-003 de 03-lista-result) |
| `pBeStockEspecifico.Cantidad` | MUTA in-place | resta `vCantidadReservada` al inicio (L1638) — el caller recibe el objeto modificado |
| `pListStockResOUT` | ASIGNA lista | comportamiento intencional, R-OV2-007 |

---

## Open questions específicas de OV2

- **Q-OV2-01**: ¿Quién llama OV2 vs OV1? Hipótesis: picking/despacho (necesitan IdStockRes para confirmar) usan OV2; flujos batch o legacy usan OV1.
- **Q-OV2-02**: Semántica del flag `False` en `Get_Cantidad_Y_Peso_ReservadaUMBas_By_IdStock`. Necesita leer la firma del helper.
- **Q-OV2-03**: ¿OV2 deprecó silenciosamente a OV1? Si hay un commit que agregó OV2 explícitamente para reemplazar OV1, OV1 debería marcarse como deprecada y migrarse callers.
- **Q-OV2-04**: ¿Existe documentación interna de la diferencia OV1/OV2? Si no, el riesgo es que un dev nuevo elija la "incorrecta" para su use case.
- **Q-OV2-05** (severidad ALTA): ¿qué copia exactamente `clsPublic.CopyObject`? Si copia por reflexión todos los campos comunes, sobrescribe los seteos manuales hechos en L1701-1707 con valores del stock origen — comportamiento frágil y dependiente de implementación.

---

## Referencias cruzadas

- `04-reserva-stock-especifico-ov1.{yml,md}` — versión base, fuente de verdad para los bloques compartidos
- `01-reserva-stock-core.{yml,md}` — punto de entrada principal del módulo (NO llama a Especifico; Especifico es entry point alternativo)
- `gaps/GAP-12-001-byb-codigo-1060315-no-replicado-en-net8.md` — el hardcode BYB no aparece en Especifico (no hay rama BYB acá)
- `gaps/Q-OV2-05` (a crear si se confirma como gap) — si CopyObject sobrescribe seteos manuales, hay un patrón frágil sistemático en el módulo

---

## Acciones sugeridas

1. **Resolver Q-OV2-02** leyendo la firma de `Get_Cantidad_Y_Peso_ReservadaUMBas_By_IdStock`.
2. **Resolver Q-OV2-05** leyendo la implementación de `clsPublic.CopyObject` — si el bug se confirma, abrir `GAP-12-002` para documentarlo a nivel modular.
3. **Cuando Erik confirme casos de uso de OV1 vs OV2**, llenar la sección `callers` y posiblemente abrir ADR-13 para deprecar OV1 si OV2 cubre todos los casos.
