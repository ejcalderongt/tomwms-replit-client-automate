# `Reserva_Stock` (core) — proceso punto a punto

> **Archivo**: `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
> **Anchor**: L138 → L427 (290 líneas)
> **Visibilidad**: `Public Shared Function ... As Boolean`
> **Hermano YAML**: `01-reserva-stock-core.yml`
> **Scan**: 2026-04-29

---

## Resumen ejecutivo

`Reserva_Stock` es el núcleo genérico de reserva. Recibe una solicitud (`clsBeStock_res`) y dos coordenadas obligatorias (`IdPropietario`, `MaquinaQueSolicita`) más la conexión/transacción ambiente. Su trabajo es:

1. Resolver la **config encabezado** del propietario en la bodega (`i_nav_config_enc`).
2. Pedir la lista de **stock candidato** a `clsLnStock.lStock` (FEFO viene heredado).
3. **Restar** reservas pendientes UMBas del mismo IdStock.
4. **Normalizar** la cantidad solicitada según presentación (UMBas / Pallet / control-por-peso).
5. **Recorrer** el stock candidato armando los `clsBeStock_res` a persistir.
6. **Persistir** vía `Inserta_Stock_Reservado`.
7. Si quedó **fracción decimal**, auto-llamarse en UMBas (recursión).

Es el método al que los 7 adapters (NAV_BYB, Lista_Result, Especifico×2, From_Reabasto, From_MI3, From_SAP) **deberían** delegar — y la sospecha (a confirmar en archivos 02-08) es que la mayoría replican la lógica en vez de invocar este core, razón principal por la que el archivo creció a 34K líneas.

---

## Tabla de bloques lógicos

| Bloque | Líneas | Tipo | Resumen |
|---|---|---|---|
| B001 | 147 | init | `Reserva_Stock = False` (default return) |
| B002 | 150-425 | try_catch | wrapper |
| B003 | 170-199 | branch + db_read | resolución `BeConfigEnc` por (IdBodega, IdPropietario) |
| B004 | 201-207 | db_read | query stock disponible vía `clsLnStock.lStock` |
| B005 | 210-224 | loop + db_read | restar reservas UMBas pendientes |
| B006 | 226 | filter | `Where(Cantidad > 0)` |
| B007 | 228, 417-419 | branch | si lista vacía → `ErrorS0004` |
| B008 | 230-288 | branch + math | normalización cantidad por presentación |
| B009 | 290-389 | loop + branch | iteración armando `clsBeStock_res` |
| B010 | 391-415 | db_write + recursion | persistencia + recursión por fracción |
| B011 | 421-425 | catch | log + re-throw |

---

## Recorrida narrativa

### B001 · Default false (L147)

`Reserva_Stock = False` antes del Try. Cualquier salida que no llegue al `Reserva_Stock = True` final retorna False. Esto incluye el caso edge `lBeStockAReservar.Count = 0` (ver Q-CORE-05).

### B003 · Resolución de configuración encabezado (L170-199)

Aquí hay **una rama muerta**: `vIdxConfig` se inicializa en `-1` y **nunca se asigna otro valor**, por lo que el `Else` en L196-199 (que leería del cache `lBeConfigInMemory(vIdxConfig)`) jamás se ejecuta. El código siempre cae en la rama `If vIdxConfig = -1`.

Dentro de esa rama, si `BeConfigEnc Is Nothing` (caso normal, el caller no la pasa), se hace lookup vía:

```vb
BeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(
    pStockRes.IdBodega, pIdPropietario, lConnection, ltransaction)
```

Si después del lookup sigue siendo `Nothing`, se tira `ERROR_202210182024` con el detalle de IdBodega + IdPropietario. **No se valida usuario, ni canal, ni timestamp** — la clave de config es estrictamente `(IdBodega, IdPropietario)`.

Después agrega `BeConfigEnc.Clone()` a `lBeConfigInMemory` (cache module-level acumulativa, **sin invalidación visible** — si la config cambia en BD durante la vida del proceso, el cache no la recoge). Posible regla R-stale-config a investigar.

### B004 · Query stock disponible (L201-207)

Una sola llamada gorda:

```vb
lBeStockExistente = clsLnStock.lStock(pStockRes, BeProducto, DiasVencimiento,
                                       BeConfigEnc, lConnection, ltransaction,
                                       pExcluirUbicacionPicking)
```

`BeProducto` se llena ByRef adentro de `lStock`. `pExcluirUbicacionPicking` viaja hasta acá (la única vez que se usa en toda la función). El **orden FEFO** y el **filtro por días de vencimiento** ocurren dentro de `lStock`, no acá. Esa función es el verdadero cerebro del query.

### B005 · Restar reservas UMBas pendientes (L210-224)

Para cada candidato del listado se llama `Get_Cantidad_ReservadaUMBas_By_IdStock(S.IdStock, False, ...)` (interna del archivo). Si el resultado es `<> 0`, se descuenta de `S.Cantidad`. El segundo argumento `False` quizá indica "no incluir reservas confirmadas" (a verificar leyendo esa función).

**Riesgo identificado (Q-CORE-06)**: solo resta UMBas. Si hay reservas activas del mismo IdStock en otra presentación (caja, pallet), no se restan acá → posible doble-reserva.

### B007 · Branch principal (L228 vs L417)

Si después del filtro `Where(Cantidad > 0)` no queda nada, salta al `Else` de L417 que tira `ErrorS0004` ("no hay stock disponible para el producto"). **No acepta cero stock como reserva válida**.

### B008 · Normalización de cantidad solicitada (L230-288)

Acá vive la mayor lógica de presentación. Tres ramas anidadas:

1. **`IdPresentacion = 0`** → `vCantidadSolicitadaPedido = pStockRes.Cantidad` (asume UMBas directa). El comentario L232-233 admite explícitamente que este escenario "se debe probar".

2. **`IdPresentacion <> 0` y `BePres.EsPallet`** → multiplica por `(Factor * CajasPorCama * CamasPorTarima)`. Si el factor es ≤ 0 tira excepción ad-hoc.

3. **`IdPresentacion <> 0` y NO Pallet** → `Split_Decimal` separa entero y decimal:
   - decimal × Factor con `Math.Ceiling`
   - si entero > 0 usa entero, sino usa decimal
   - sub-rama por `Tiene_Control_Por_Peso`: si NO → `Integer.TryParse` o `Math.Truncate`; si SÍ → `Math.Round(.., 6)`

`vCantidadDecimalUMBas` queda guardado para más tarde — es el **trigger de la recursión** del B010.

### B009 · Iteración del stock candidato (L290-389)

For Each sobre `lBeStockExistente`. Por cada vStock con `Cantidad > 0`:

- Se construye un nuevo `clsBeStock_res` copiando campos de `pStockRes` (IdTransaccion, Indicador, IdPedido, IdPedidoDet) y de `vStock` (IdBodega, IdStock, IdUbicacion, IdProductoEstado, IdUnidadMedida, Lote, Lic_plate, Serial, Peso, Fecha_ingreso, Fecha_vence, Uds_lic_plate, Ubicacion_ant, No_bulto, IdRecepcion, añada, Fecha_manufactura).
- IdPicking = 0, IdDespacho = 0 (la reserva nace "no comprometida").
- **Estado = `"UNCOMMITED"`** — literal con typo (debería ser UNCOMMITTED, doble T). Q-CORE-04: confirmar si BD también está mal escrita. Si sí, es contrato.
- Branch de cantidades (L322-334): tres ramas explícitas (`=`, `<`, `>`) — la rama `>` y la rama `=` hacen exactamente lo mismo, posible refactor.
- **Color, Talla, IdProductoTallaColor** (L375-377, marca `#GT09012025` — agregado en enero de 2025, reciente). Se propagan desde pStockRes sin validación. Importante para Wave 13: ¿el motor nuevo .NET 8 conoce estos campos o el reescrito los olvidó?

`vCantidadCompletada = (vCantidadPendiente = 0)` actúa como flag de salida del loop.

### B010 · Persistencia y recursión por fracción (L391-415)

```vb
If lBeStockAReservar.Count > 0 Then
    If Inserta_Stock_Reservado(lBeStockAReservar, lConnection, ltransaction) Then
        If vCantidadDecimalUMBas > 0 Then
            BeStockRes.Cantidad = vCantidadDecimalUMBas
            BeStockRes.IdPresentacion = 0
            Reserva_Stock(BeStockRes, pIdPropietario, DiasVencimiento,
                          MaquinaQueSolicita, lConnection, ltransaction)
        End If
        Reserva_Stock = True
    Else
        Throw New Exception(... ErrorS0006 ...)
    End If
Else
    Throw New Exception(... ErrorS0005 ...)
End If
```

**Tres hallazgos críticos en estas 15 líneas**:

1. **La recursión NO captura el return value** de `Reserva_Stock(BeStockRes, ...)`. Si la fracción decimal falla, se pierde silenciosamente y el caller cree que todo OK porque `Reserva_Stock = True` se ejecuta justo después. Q-CORE-02 (severidad **alta**).

2. **La recursión NO pasa `pExcluirUbicacionPicking` ni `BeConfigEnc`**. Por defaults: `pExcluirUbicacionPicking` vuelve a `False` (incluso si el caller original lo tenía en `True` para excluir picking) y `BeConfigEnc` vuelve a `Nothing` (relectura redundante de BD). Q-CORE-07 (severidad **alta**). Si el caller original quería excluir picking, la recursión por fracción puede reservar en picking igual.

3. **Caso `lBeStockAReservar.Count = 0`** post-loop. No tira excepción, no entra a `Inserta_Stock_Reservado`, `Reserva_Stock = True` nunca se setea, retorna `False`. Q-CORE-05.

### B011 · Catch (L421-425)

Catch genérico que loguea con `clsLnLog_error_wms.Agregar_Error("ERROR_20220825_1417: ...")` y re-tira la excepción. No se hace cleanup ni rollback explícito (asume que el caller maneja la transacción).

---

## Hallazgos críticos

### Bugs latentes

- **B-CORE-01**: recursión sin captura de return value (Q-CORE-02).
- **B-CORE-02**: recursión sin propagación de `pExcluirUbicacionPicking` ni `BeConfigEnc` (Q-CORE-07).
- **B-CORE-03**: `Get_Cantidad_ReservadaUMBas_By_IdStock` solo resta UMBas → posible doble-reserva si hay reservas activas en otras presentaciones (Q-CORE-06).
- **B-CORE-04**: `lBeStockAReservar.Count = 0` retorna False silencioso (Q-CORE-05).

### Dead code

- Else del B003 (L196-199) — `vIdxConfig` nunca cambia de `-1`.
- Else defensive de B009 → `ErrorS0005` (L413-414) — inalcanzable porque B007 ya validó `Count > 0`.
- `vCantidadPresentacionAUbicar`, `BePresentacion`, `vCantidadTarima` declaradas y nunca usadas.
- `BeStockRes.IdRecepcion` asignado dos veces (L358 y L367) con el mismo valor.

### Typos / contrato

- `Estado = "UNCOMMITED"` (debería ser UNCOMMITTED, doble T). Si BD lo tiene así también → es **contrato establecido**, no se toca.

---

## Reglas extraídas (R001-R010)

Ver `01-reserva-stock-core.yml`, sección `extracted_rules`. Resumen:

- **R001**: `BeConfigEnc` = clave `(IdBodega, IdPropietario)`.
- **R002**: stock disponible = stock − reservas **UMBas** del mismo IdStock (asimetría).
- **R003**: si `IdPresentacion = 0` → cantidad ya está en UMBas.
- **R004**: si Pallet → multiplica por `Factor × CajasPorCama × CamasPorTarima`.
- **R005**: NO control-por-peso → trunca a entero; SÍ control-por-peso → redondea a 6 decimales.
- **R006**: orden de reserva = orden de retorno de `lStock` (FEFO heredado).
- **R007**: fracción decimal → recursión separada en UMBas.
- **R008**: estado inicial = `'UNCOMMITED'` (literal con typo).
- **R009**: stock insuficiente NO permite reserva parcial.
- **R010**: Color/Talla/IdProductoTallaColor se propagan sin validación (#GT09012025).

---

## Open questions abiertas (Q-CORE-01..08)

Ver `01-reserva-stock-core.yml` `open_questions`. Las **alta severidad** son Q-CORE-02 (recursión sin return), Q-CORE-06 (resta solo UMBas), Q-CORE-07 (recursión pierde flags) y Q-CORE-08 (Color/Talla en .NET 8).

---

## Comparación contra .NET 8 (placeholder Wave 13)

Mapeo inferido del facade nuevo:

| Bloque legacy | Step .NET 8 (inferido) | Status |
|---|---|---|
| B003 | `ConfigResolverStep` | sin verificar |
| B004 | `StockQueryStep` | sin verificar |
| B005 | `SubtractExistingReservationsStep` | sin verificar (**verificar UMBas-only**) |
| B008 | `QuantityNormalizationStep` | sin verificar |
| B009 | `StockSplitStep` | sin verificar (**verificar Color/Talla/TallaColor**) |
| B010 (persist) | `PersistStep` | sin verificar |
| B010 (recursión) | `FractionalUMBasFallbackStep` | sin verificar (**verificar propagación de flags**) |

Wave 13 cierra esta tabla con anchors al código .NET 8.
