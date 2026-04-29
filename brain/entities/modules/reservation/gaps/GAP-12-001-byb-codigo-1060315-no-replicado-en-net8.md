# GAP-12-001 — La condición BYB (`Codigo_proveedor_produccion = "1060315"`) no se replicó al motor nuevo .NET 8

> **Severidad**: alta (gap funcional silencioso, afecta cliente real BYB)
> **Detectado**: Wave 12 sub-wave 12A, 2026-04-29
> **Origen**: inventario punto-a-punto del archivo `clsLnStock_res_Partial.vb`
> **Estado**: abierto, candidate a ADR-12 (propuesta al final)

---

## Resumen ejecutivo

El archivo legacy `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` evalúa el literal `"1060315"` contra el campo `pBeConfigEnc.Codigo_proveedor_produccion` en **3 puntos** para activar comportamiento especial del cliente BYB. El reescritor del motor nuevo (`WMS.DALCore` + `WMS.EntityCore`) **no replicó esta condición**: el campo se sigue cargando y persistiendo como plomería CRUD, pero **nunca se evalúa funcionalmente**.

Si BYB se conecta al motor nuevo .NET 8 sin que se restablezca la lógica especial, **pierde silenciosamente** dos comportamientos de negocio que el legacy le da:

1. **Política anti-reserva-parcial-fraccional** en `Reserva_Stock_NAV_BYB`: cuando un stock candidato tiene fracción decimal en presentación y la cantidad pendiente excede la disponible, el legacy salta el stock entero (`Continue For`) en vez de tomar parcial. El motor nuevo, sin esta condición, va a tomar parcial como cualquier otro cliente.

2. **Split físico de stock** en `Reserva_Stock_Lista_Result`: cuando el caso BYB se cumple, el legacy **muta la tabla `stock`** insertando un nuevo `IdStock` con marker `No_bulto = 1989` que contiene la fracción en UMBas y decrementa o elimina el stock origen. El motor nuevo no hace este split y deja el stock como vino.

El primer gap genera reservas que BYB no esperaba (parciales fraccionales). El segundo deja la presentación intacta y obliga al operador o al sync NAV a re-acomodar manualmente. **Ninguno tira excepción** — es deriva silenciosa.

---

## Evidencia (rg sobre `/tmp/repos/TOMWMS_BOF/`)

### `rg "1060315"` — 3 hits, todos en el mismo archivo

| Archivo | Línea | Función legacy |
|---|---|---|
| `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` | 708 | `Reserva_Stock_NAV_BYB` (B009, rama `vCantidadPendiente > vCantidadStock`) |
| `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` | 1133 | `Reserva_Stock_Lista_Result` (B009, rama `vCantidadPendiente < vCantidadDispStock`) |
| `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb` | 1236 | `Reserva_Stock_Lista_Result` (B009, rama `vCantidadPendiente > vCantidadDispStock`) |

### `rg "1060315" WMS.DALCore WMS.EntityCore` — 0 hits

Ningún archivo del motor nuevo .NET 8 contiene este literal. Confirmación directa: la condición no existe en el reescrito.

### `rg "Codigo_proveedor_produccion" WMS.*` — 7 hits, todos plomería CRUD

| Archivo | Línea | Naturaleza |
|---|---|---|
| `WMS.EntityCore/Interface/clsBeI_nav_config_enc.cs` | 22 | property declaration |
| `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs` | 50 | `GetString` al cargar |
| `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs` | 124 | `SqlParameter` al persistir |
| `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs` | 203, 328, 441 | columnas en Insert/Update |
| `WMS.DALCore/Interface/clsLnI_nav_config_enc.cs` | 668 | `SqlParameter` en select adapter |

Ninguna ocurrencia evalúa, compara ni decide en base al valor del campo. El campo es **funcionalmente muerto** en el motor nuevo.

---

## Subhallazgo: el campo está mal nombrado y mal usado

`rg "Transfer_from_Code"` revela el uso semántico real del campo:

```vb
' DynamicsNavInterface/Clases Interface Sync/Envios_Almacen/clsSyncNavEnvioAlm.vb:210
BeI_Nav_PedidoTraslado.Transfer_from_Code = BeConfigEnc.Codigo_proveedor_produccion
```

```csharp
// TestMI3/Form1.cs:246, 302, 342, 382, 422
BePedidoEnc.Transfer_from_Code = "ET"; // o "04", "BV01"
```

El campo es, en intención original, **el código de la bodega origen para envíos NAV ERP**. No es un identificador de cliente. El valor de BYB ahí (`"1060315"`) coincidía con su código de proveedor de producción NAV, y Erik en mayo 2022 (markers `#EJC20220523`, `#EJC20220510`) lo aprovechó como side-channel para identificar al cliente y triggerar lógica especial.

Es **misuse del campo** — pragmático en su momento, arquitectónicamente sucio, y exactamente la clase de práctica que la convención **L-035** ("no codificar por cliente; parametrizar via Bodega/Producto/i_nav_config_enc") prohíbe.

---

## Impacto funcional

| Escenario BYB | Legacy VB.NET | Motor nuevo .NET 8 (actual) | Riesgo |
|---|---|---|---|
| Pedido con cantidad > stock disponible y stock con fracción decimal en presentación | Salta el stock entero (no parcial) | Toma parcial como cualquier cliente | reservas BYB con fracción donde antes no las había |
| Pedido con cantidad < stock disponible y fracción decimal | Splittea físicamente el stock (insert UMBas, update/delete origen, marker 1989) | Reserva sin tocar stock físico | presentaciones BYB quedan rotas en BD |
| Cualquier otro cliente | sin cambios | sin cambios | sin gap |

**Marcador para auditoría**: si BYB migra al motor nuevo y aparecen reservas con cantidades fraccionales o `stock_res` sobre stocks que el legacy hubiera splitteado, este gap es el responsable.

---

## Propuesta de ADR-12

**Título tentativo**: "Migrar el side-channel `Codigo_proveedor_produccion = '1060315'` a un flag dedicado en `i_nav_config_enc`"

**Pasos**:

1. Agregar dos columnas booleanas a `i_nav_config_enc` (nombre tentativo, validar con Erik):
   - `Reserva_Evita_Parcial_Por_Fraccion_Decimal BIT NOT NULL DEFAULT 0` (cubre R007 NAV_BYB)
   - `Reserva_Splittea_Stock_En_Fraccion BIT NOT NULL DEFAULT 0` (cubre R008 Lista_Result + V-003)

2. Actualizar la entidad y DAL en **ambos** modelos:
   - Legacy VB: `clsBeI_nav_config_enc.vb` + `clsLnI_nav_config_enc.vb`
   - Motor nuevo: `clsBeI_nav_config_enc.cs` + `clsLnI_nav_config_enc.cs`

3. Reemplazar las 3 condiciones en `clsLnStock_res_Partial.vb`:
   - L708: `If pBeConfigEnc.Codigo_proveedor_produccion = "1060315"` → `If pBeConfigEnc.Reserva_Evita_Parcial_Por_Fraccion_Decimal`
   - L1133, L1236: `If pBeConfigEnc.Codigo_proveedor_produccion = "1060315"` → `If pBeConfigEnc.Reserva_Splittea_Stock_En_Fraccion`

4. Replicar las dos políticas en el motor nuevo .NET 8 (Wave 13 las ubicará en el step correspondiente).

5. UPDATE puntual en BD para BYB:
   ```sql
   UPDATE i_nav_config_enc
      SET Reserva_Evita_Parcial_Por_Fraccion_Decimal = 1,
          Reserva_Splittea_Stock_En_Fraccion = 1
    WHERE Codigo_proveedor_produccion = '1060315';
   ```

6. Agregar checkbox en `frmConfiguracion.vb` (`txtCodigoProvProd` ya existe; agregar dos `chkBox` adyacentes).

7. Registrar la decisión en `brain/_index/ADR/ADR-12.md` (a crear).

**Beneficio**: cualquier cliente futuro que necesite las mismas políticas se configura desde la UI sin recompilar el WMS. Convención **L-035** restablecida.

---

## Referencias cruzadas

- Archivos del piloto + sub-wave 12A donde se documentó la violación:
  - `brain/entities/modules/reservation/legacy-process-flow/02-reserva-stock-nav-byb.{yml,md}` — V-001, R007, Q-NAVBYB-02
  - `brain/entities/modules/reservation/legacy-process-flow/03-reserva-stock-lista-result.{yml,md}` — V-002, V-003, R008, R009, Q-LR-04
- Convenciones aplicables:
  - **L-035** — no codificar por cliente; parametrizar via Bodega/Producto/i_nav_config_enc
  - **L-025** — NAV no es NAV (los flujos llamados "NAV" en el código no necesariamente sincronizan con NAV ERP)
- Markers físicos relacionados:
  - `No_bulto = 1989` — split BYB de cajas a unidades (Lista_Result)
  - `No_bulto = 1965` — recursión MI3 (ya documentado en módulo MI3)

---

## Open questions vinculadas

- **Q-NAVBYB-08**: el comentario `#EJC20220303 "Cealsa, reservar peso proporcional"` aparece dentro del adapter BYB. Si Cealsa también pasa por este flujo, el gap aquí descrito también la afecta.
- **Q-LR-03**: el motor nuevo .NET 8 ¿separó SPLIT FÍSICO de RESERVA en steps distintos? Si sí, el step de split podría no existir o estar deshabilitado por defecto, lo que confirmaría este gap como una omisión consciente del reescritor (que entonces debería haber dejado un comentario o issue).

---

## Acciones sugeridas

1. **Confirmar con Erik** si BYB ya está migrado al motor nuevo o sigue en el legacy. Si está en legacy, el gap es teórico hasta que migre. Si está en .NET 8, hay que verificar producción **ya** (auditoría sobre `stock_res` y `stock` para BYB en los últimos N meses).
2. Aprobar o rechazar **ADR-12**.
3. Si se aprueba ADR-12, programar el cambio coordinado entre legacy y motor nuevo.
