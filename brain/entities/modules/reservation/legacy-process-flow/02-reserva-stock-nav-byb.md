# `Reserva_Stock_NAV_BYB` — adapter BYB con peso proporcional

> **Archivo**: `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
> **Anchor**: L442 → L855 (414 líneas)
> **Visibilidad**: `Public Shared Function ... As Boolean`
> **Introducida**: 2022-05-23 por Erik (`#EJC20220523`)
> **Hermano YAML**: `02-reserva-stock-nav-byb.yml`

---

## Hallazgo crítico (V-001)

```vb
' Línea 708 (dentro de B009, rama vCantidadPendiente > vCantidadStock):
If pBeConfigEnc.Codigo_proveedor_produccion = "1060315" Then
    ' ...lógica especial: evitar reservas parciales fraccionales...
```

**Esto viola directamente L-035** ("no codificar por cliente; parametrizar via Bodega/Producto/i_nav_config_enc"). El string `"1060315"` es un literal hardcoded que identifica al cliente BYB. Cualquier nuevo cliente que necesite la misma política tendría que recompilar el WMS.

**Repercusiones**:
- El mismo literal `"1060315"` aparece también en `Reserva_Stock_Lista_Result` (L1133, L1236) — ver archivo 03.
- Pendiente Q-NAVBYB-02: hacer `rg "1060315"` sobre todo el repo TOMWMS_BOF para enumerar todas las apariciones.
- Solución propuesta: exponer un flag boolean en `i_nav_config_enc` (ej. `Reserva_Solo_Cantidad_Entera_En_Presentacion`) y migrar la condición. Esto es candidate para una ADR nueva (ADR-12 propuesta).

---

## Resumen ejecutivo

Adapter para el cliente **BYB** que opera bajo NAV/Killios. Diferencias estructurales contra el core:

| Aspecto | Core (`Reserva_Stock`) | NAV_BYB |
|---|---|---|
| Config encabezado | `(IdBodega, IdPropietario)` | `(IdBodega, IdEmpresa)` |
| Maneja Peso | ❌ | ✅ proporcional |
| Resta inicial usa | `Get_Cantidad_ReservadaUMBas_By_IdStock` | `Get_Cantidad_Y_Peso_ReservadaUMBas_By_IdStock` |
| `pExcluirUbicacionPicking` | parámetro opcional | ❌ no se pasa (siempre incluye picking) |
| Validación stock suficiente | en UMBas | en UMBas o en PRESENTACION según `Explosion_Automatica` |
| Recursión por fracción | siempre que haya decimal | gated por `Explosion_Automatica = True` |
| Indicador en stock_res | respeta `pStockRes.Indicador` | hardcoded `"PED"` |
| Color/Talla/TallaColor | sí (post #GT09012025) | ❌ no propaga |
| `pListStockResOUT` (lista creada) | ❌ no devuelve | ✅ ByRef |
| Cliente hardcoded | ❌ | ✅ `Codigo_proveedor_produccion = "1060315"` |
| Error 0002A | ❌ | ✅ variante para insuficiencia en presentación |

---

## Tabla de bloques lógicos

| Bloque | Líneas | Tipo | Resumen |
|---|---|---|---|
| B001 | 452-456 | init | `=False; pListStockResOUT=Nothing; vIdxConfig=-1 (dead)` |
| B002 | 458-853 | try_catch | wrapper |
| B003 | 468 | init | `BeConfigEnc` placeholder con IdPropietario |
| B004 | 470-475 | db_read | `lStock` SIN `pExcluirUbicacionPicking` |
| B005 | 481-497 | loop+db_read | restar cantidad **y peso** UMBas pendientes |
| B006 | 499 | branch | `Count = 0 → ErrorS0004` |
| B007 | 532-618 | branch+math | normalización cantidad/peso por presentación + lookup config por IdEmpresa |
| B008 | 636-676 | branch | validación stock suficiente (UMBas o presentación según `Explosion_Automatica`) |
| B009 | 681-808 | loop+branch | iteración + **HOTFIX BYB hardcoded** |
| B010 | 812-843 | db_write+recursion | persistencia + recursión gated por `Explosion_Automatica` |
| B011 | 849-852 | catch | log sin prefijo timestamp + re-throw |

---

## Recorrida narrativa (puntos clave únicos)

### B007 · Lookup de config por (IdBodega, IdEmpresa)

```vb
pBeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(
    pIdBodega, pIdEmpresa, lConnection, ltransaction)
```

**Divergencia clave** vs el core: el core usa `Get_Single_By_IdBodega_And_IdPropietario`. Acá usa **IdEmpresa**. Esto sugiere que en el modelo BYB, una empresa puede tener varios propietarios contables pero la config viene amarrada al nivel empresa, no propietario. Q-NAVBYB-06 abierta para confirmar.

### B007 · Flag `Explosion_Automatica`

Si `pBeConfigEnc.Explosion_Automatica = True` el adapter divide la cantidad solicitada en parte entera (en presentación) y fracción decimal (en UMBas, vía recursión). Si es `False`, multiplica directo por Factor. Esto es **una rama de comportamiento entera distinta**, candidate a ser un Step propio en el motor nuevo.

### B008 · Validación de stock en presentación (no UMBas)

```vb
If pBeConfigEnc.Explosion_Automatica AndAlso pStockRes.IdPresentacion <> 0 Then
    vCantidadStockEnPres = Math.Round(vCantidadStock / BePres.Factor, 6)
    vCantidadSolicitadaPedidoEnPres = Math.Round(vCantidadSolicitadaPedido / BePres.Factor, 6)
    If vCantidadSolicitadaPedidoEnPres > vCantidadStockEnPres Then
        Throw New Exception(... ErrorS0002A ...)
    End If
```

Cuando se reserva por presentación (caja, pallet) y el cliente tiene `Explosion_Automatica`, la insuficiencia se evalúa en **unidades de presentación**, no en UMBas. Tira **`ErrorS0002A`** (variante de 0002 con sufijo A) — Q-NAVBYB-07 pendiente: enumerar la familia de variantes alfabéticas de errores en `clsDalEx`.

### B009 · El hotfix BYB hardcoded

```vb
ElseIf vCantidadPendiente > vCantidadStock Then
    If pBeConfigEnc.Codigo_proveedor_produccion = "1060315" Then
        ' ... lógica BYB ...
        If vCantidadDecimalStockUMBas > 0 AndAlso vCantidadEnteraStockPres > 0 Then
            vCantidadAReservarPorIdStock = Math.Round(vCantidadEnteraStockPres * BePres.Factor, 6)
            ' ...
        Else
            If vCantidadEnteraStockPres > 0 Then
                vCantidadAReservarPorIdStock = ...
            Else
                ' #EJC20220510: Para byb hotfix, evitar que reserve parciales
                Continue For
            End If
        End If
    Else
        ' Comportamiento NORMAL: tomar todo el stock disponible
        vCantidadAReservarPorIdStock = vCantidadStock
    End If
End If
```

La lógica BYB es: cuando el stock disponible es insuficiente para satisfacer la cantidad pendiente y existe **fracción decimal en presentación**, en vez de reservar parcial el adapter **salta el stock entero** (`Continue For`). Comportamiento opuesto al core, que sí reserva parcial. Esto es la regla R007 y la que justifica el hardcode.

### B010 · Recursión gated

```vb
If Inserta_Stock_Reservado(lBeStockAReservar, lConnection, ltransaction) Then
    If Not pBeConfigEnc Is Nothing Then
        If pBeConfigEnc.Explosion_Automatica Then
            If vCantidadDecimalUMBas > 0 Then
                BeStockRes.Cantidad = vCantidadDecimalUMBas
                BeStockRes.IdPresentacion = 0
                Reserva_Stock_NAV_BYB(BeStockRes, ..., pListStockResOUT, ...)
            End If
        End If
    End If
    Reserva_Stock_NAV_BYB = True
    pListStockResOUT = lBeStockAReservar
End If
```

**Bug latente Q-NAVBYB-09**: la recursión recibe `pListStockResOUT` `ByRef` y al final del adapter padre se hace `pListStockResOUT = lBeStockAReservar` (la lista del padre). Pero la **recursión sobreescribe** `pListStockResOUT` en su propio scope. Hay que verificar si el padre conserva su lista o si la fracción la sobrescribe. Lectura literal del VB: `ByRef` significa que la recursión modifica la referencia, así que sí, la sobreescribe. **El padre pierde la lista parcial** y solo entrega lo que reservó la fracción decimal. Esto es un bug latente serio para Wave 13.

---

## Hallazgos críticos

### Bugs latentes

- **B-NAVBYB-01**: recursión sobreescribe `pListStockResOUT` (Q-NAVBYB-09).
- **B-NAVBYB-02**: recursión sin captura de return value (heredado del core).
- **B-NAVBYB-03**: hotfix BYB hardcoded por string literal (V-001, R007).
- **B-NAVBYB-04**: Indicador hardcoded a `"PED"` ignora `pStockRes.Indicador` (R008, Q-NAVBYB-04).
- **B-NAVBYB-05**: NO propaga Color/Talla/IdProductoTallaColor (R010, Q-NAVBYB-05).
- **B-NAVBYB-06**: lStock siempre incluye picking (no propaga `pExcluirUbicacionPicking`) (R002, Q-NAVBYB-03).

### Dead code

- `vIdxConfig` declarada y nunca usada (B001).
- Comentario L621-627 muestra una rama deshabilitada (filter por presentación).
- `BeStockRes.IdRecepcion` asignado dos veces (L783, L794).

### Comentario fuera de lugar

- L792 dice "Cealsa, reservar peso proporcional" pero estamos en el adapter BYB (Q-NAVBYB-08). Posible copy-paste accidental → verificar si Cealsa también pasa por este adapter.

---

## Reglas extraídas (R001-R012)

Ver `02-reserva-stock-nav-byb.yml`. Las **rules más relevantes para gap analysis**:

- **R001** (config por IdEmpresa, no IdPropietario) — divergencia mayor con el core, si el motor nuevo solo usa IdPropietario, gap completo.
- **R007** (hotfix BYB hardcoded) — más violación que regla, ADR-12 candidate.
- **R008** (Indicador hardcoded) — divergencia con el core post-#CKFK20210221.
- **R010** (no propaga Color/Talla) — divergencia con el core post-#GT09012025.
- **R011** (peso proporcional con comentario "Cealsa") — riesgo de cliente cruzado.

---

## Comparación contra .NET 8 (placeholder Wave 13)

Mapeo inferido (especulativo, validar en Wave 13):

| Bloque legacy | Step .NET 8 (inferido) | Status |
|---|---|---|
| B003-B007 | `ConfigResolverStep` con resolver dual (IdPropietario / IdEmpresa)? | sin verificar |
| B005 | `SubtractCantidadYPesoUMBasStep`? | sin verificar |
| B007 | `QuantityNormalizationStep` con flag Explosion_Automatica | sin verificar |
| B008 | `StockSufficiencyValidatorStep` con dos modos (UMBas vs Presentación) | sin verificar |
| B009 hotfix | ¿`PartialReservationPolicyStep`? con flag `AvoidPartialOnFraction` | sin verificar — **posible que el motor nuevo NO conozca esta política** |
| B010 | `PersistStep` + `FractionalUMBasFallbackStep` con gating por Explosion_Automatica | sin verificar |

**Gaps probables en .NET 8** (a confirmar en Wave 13):
- ¿Existe el adapter BYB como handler separado o se asumió que el comportamiento del core cubre todo?
- ¿Existe la familia de errores `S0002A`?
- ¿El reescrito tomó la decisión de NO migrar el hardcode `"1060315"` y entonces eliminó la política, o la migró a flag de config?
- ¿Peso proporcional es first-class en el modelo nuevo o se infiere?

Si el reescrito ignoró este adapter (hipótesis razonable dado que el brain previo no lo tenía mapeado), **BYB perdería la política anti-parcial fraccional cuando el WMS migre al motor nuevo**. Esto es exactamente el tipo de gap funcional que esta wave existe para detectar.
