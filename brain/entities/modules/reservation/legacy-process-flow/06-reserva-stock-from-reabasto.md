# Reserva_Stock_From_Reabasto

> **Archivo legacy**: `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
> **Rango**: L9856-L13108 (3.253 líneas)
> **Documentada en**: sub-wave 12B-2 deep mode, 2026-04-29
> **Tamaño en el pipeline**: la función más grande y arquitectónicamente más compleja del módulo de reserva.

---

## Por qué importa esta función

Es la primera función del pipeline que **implementa explícitamente el algoritmo Clavaud** (priorizar pallets completos fuera de zona picking > pallets en zona picking > resto), **respeta granularmente la configuración del propietario** (siete flags distintos), **muta físicamente la tabla `stock`** (insert + update + delete) como parte normal de su flujo, y **se llama a sí misma 3 veces** como fallback de degradación gradual a unidades de medida básica.

Es también la función con **más comentarios de fixes acumulados** (50+ markers `#EJC`, `#CKFK`, `#GT` desde 2017 hasta 2025), lo que sugiere que es donde más bugs de producción se descubrieron y donde más cuidado hay que tener al replicarla en .NET 8.

---

## Firma

```vb
Public Shared Function Reserva_Stock_From_Reabasto(
    ByRef pStockRes As clsBeStock_res,
    ByVal DiasVencimiento As Double,
    ByVal MaquinaQueSolicita As String,
    ByVal pBeConfigEnc As clsBeI_nav_config_enc,                      ' ← config completa
    ByRef pCantidadDisponibleStock As Double,                          ' ← out param
    ByVal pIdPropietarioBodega As Integer,
    ByRef pListStockResOUT As List(Of clsBeStock_res),                 ' ← out list
    ByRef lConnection As SqlConnection,
    ByRef ltransaction As SqlTransaction,
    Optional No_Linea As Integer = 0,                                  ' ← BYB hint (CKFK20221019)
    Optional pTarea_Reabasto As Boolean = False,                       ' ← cambia lStock
    Optional ByVal pBeTrasladoDet As clsBeI_nav_ped_traslado_det = Nothing  ' ← NAV ERP
) As Boolean
```

Es la firma **más rica** del pipeline: 12 parámetros, 3 opcionales, 4 ByRef (uno mutado completo, dos out, uno para mantenimiento de transacción).

---

## Mapa de control flow (15 bloques)

| Bloque | Líneas | Qué hace |
|---|---|---|
| **B1** | 9856-9879 | Firma + init de 50+ variables locales |
| **B2** | 9880-9945 | Lookup IdPropietario + **debug code en producción** (V-FROMR-001) |
| **B3** | 9947-9988 | Carga lista inicial vía `clsLnStock.lStock` + filtros tempranos |
| **B4** | 9990-10025 | Calcula tarimas completas Clavaud (cuántas tarimas necesita el pedido) |
| **B5** | 10029-10192 | Branch **Explosión Automática** (re-busca con presentación por defecto) |
| **B6** | 10194-10421 | Cálculo cantidad solicitada según presentación + validación exceso |
| **B7** | 10423-10446 | Setup **Algoritmo Clavaud** (particiona en 3 sub-listas) |
| **B8** | 10448-11156 | **LOOP 1** — Pallets Completos Clavaud (709 L, 3 casos: #1, #2B, #2 ELSE con split físico) |
| **B9** | 11160-11965 | **LOOP 2** — Pallets en Zona Picking (785 L, paralelo a Loop 1) |
| **B10** | 11982-12725 | **LOOP 3** — Stock Existente general (740 L, paralelo) |
| **B11** | 12729-12823 | INSERT principal + **Recursión C** (UMBas si quedó decimal) |
| **B12** | 12825-12985 | Branch alternativo + **Recursión B** (presentación + decimal) |
| **B13** | 12987-13007 | Validación **Despachar_existencia_parcial** |
| **B14** | 13008-13099 | Branch fallback + **Recursión A** (sin presentación, hay que explosionar) |
| **B15** | 13103-13108 | Catch genérico (V-FROMR-004: pierde stack trace) |

---

## El algoritmo Clavaud, explícito en código

`Conservar_Zona_Picking_Clavaud` es una política de almacenamiento que dice: **los pallets completos en zona DE ALMACENAMIENTO no deben fragmentarse mientras haya stock equivalente en zona DE PICKING**. Esto preserva eficiencia operativa (no romper pallets sellados), trazabilidad (lote único = sello del proveedor), y ergonomía (zona picking diseñada para extracción fragmentada).

La implementación está en B7 (L10437-10446):

```vb
If pBeConfigEnc.Conservar_Zona_Picking_Clavaud Then
    lBeStockConPalletsCompletosClavaud = lBeStockExistente.FindAll(Function(x) x.Pallet_Completo = True _
                                                                    AndAlso x.UbicacionPicking = False)
    lBeStockConPalletsInCompletosClavaud = lBeStockExistente.FindAll(Function(x) x.UbicacionPicking = True)
End If
```

Y se procesan en orden estricto en los 3 loops (B8 → B9 → B10).

**Cross-ref**: el algoritmo Clavaud ya está documentado en [`brain/entities/modules/reservation/05-mi3-algoritmo-fefo-clavaud.md`](../05-mi3-algoritmo-fefo-clavaud.md) para el módulo MI3. Reabasto y MI3 son las **dos implementaciones legacy del mismo algoritmo** — vale la pena cruzar referencia para asegurar paridad cuando se replique en .NET 8.

---

## Recursividad triple

```
Reserva_Stock_From_Reabasto
    │
    ├─ B11 → Recursión C (L12765): "después del INSERT principal, si quedó decimal en UMBas"
    │        marcador: ninguno (usa vlBeStockAReservarUMBas como acumulador)
    │        ⚠ V-FROMR-008: SIN guard anti-loop
    │
    ├─ B12 → Recursión B (L12899): "presentación + decimal pendiente"
    │        marcador: No_bulto = 1965
    │        ✓ guard: If BeStockResUMBas.No_bulto = 0 antes de marcar y recursar
    │
    └─ B14 → Recursión A (L13041): "sin presentación, hay que explosionar"
             marcador: No_bulto = 1965
             ✓ guard: If BeStockResUMBas.No_bulto = 0 antes de marcar y recursar
```

**Patrón**: degradación gradual a UMBas. Si la función no puede satisfacer la solicitud en presentación, se llama a sí misma con la solicitud convertida a unidades de medida básica.

**Marker `No_bulto = 1965`** es el mismo que MI3 usa para identificar solicitudes recursivas. Esto significa que cualquier `stock_res` con `No_bulto = 1965` en BD fue creado por una recursión (de Reabasto o de MI3, hay que cruzar contexto para distinguir).

**Bug latente**: la Recursión C no marca con 1965 — usa una lista acumuladora distinta. Si por algún path esa recursión interna disparase ulterior recursión, no hay guard. **No se conoce un path que lo cause hoy**, pero queda registrado como Q-FROMR-05.

---

## Split físico de stock — generalizado (no es exclusivo de BYB)

Dentro de cada uno de los 3 loops, en el caso **CASO_#2 ELSE** (cantidad pendiente convertida a presentación tiene fracción decimal), se ejecuta el siguiente algoritmo:

```vb
' 1. Crear nuevo stock destino con cantidad = 1 caja explosionada
BeStockDestino.Cantidad = (1 * BePresentacionStock.Factor)
BeStockDestino.IdPresentacion = 0
BeStockDestino.No_bulto = 1989                                    ' ← MARCADOR

' 2. Validación paranoica de cantidad entera
If Math.Abs(CantidadStockDestino - Fix(CantidadStockDestino)) Then Throw ...

' 3. INSERT físico del nuevo IdStock
clsLnStock.Insertar(BeStockDestino, lConnection, ltransaction)

' 4. Decrementar stock origen
vStockOrigen.Cantidad = BeStockOriginal.Cantidad - (1 * Factor)

If vStockOrigen.Cantidad > 0 Then
    clsLnStock.Actualizar_Cantidad(vStockOrigen, ...)             ' UPDATE
Else
    clsLnStock.Eliminar_By_IdStock(vStockOrigen.IdStock, ...)     ' DELETE
End If

' 5. Reservar el residuo en cajas con marker de auditoría
BeStockRes.Uds_lic_plate = 20220525  ' "tomado de una caja explosionada"
Insertar(BeStockRes, ...)

' 6. Reservar las cajas restantes en otra fila stock_res
BeStockRes.Uds_lic_plate = 20220526  ' "tomado de cajas de una solicitud en unidades"
```

**Hallazgo clave**: este patrón es **el mismo que GAP-12-001 documenta para `Reserva_Stock_Lista_Result`** cuando se cumple la condición BYB (`Codigo_proveedor_produccion = "1060315"`). En `Reabasto`, el patrón está **generalizado a cualquier cliente** que tenga `Conservar_Zona_Picking_Clavaud` + `Explosion_Automatica` ON.

**Implicación para GAP-12-001 (Q-FROMR-07)**: si BYB usa este flujo (probablemente sí, por nombre y por presencia de markers `EJC20220510`/`EJC20220525` que coinciden con las fechas de los hardcodes BYB), **el patrón ya está parcialmente cubierto en `Reabasto` sin necesidad del hardcode `1060315`**. La pregunta es si el motor nuevo .NET 8 también replicó `Reabasto` con esta semántica (Wave 13 lo confirmará).

---

## Markers de auditoría descubiertos

| Marker | Significado | Origen |
|---|---|---|
| `No_bulto = 1989` | Stock físico recién creado por split de 1 caja explosionada | conocido de Lista_Result |
| `No_bulto = 1965` | Stock_res generado por solicitud recursiva (UMBas fallback) | conocido de MI3 |
| `Uds_lic_plate = 20220525` | Stock_res tomado de una caja explosionada | **NUEVO** |
| `Uds_lic_plate = 20220526` | Stock_res tomado de cajas de una solicitud en unidades | **NUEVO** |

Los dos últimos son **fechas codificadas como enteros** (2022-05-25 y 2022-05-26) usadas como tags de auditoría. Permiten al equipo identificar en cualquier query post-mortem el origen exacto de la reserva. Vale la pena documentar en el módulo `reservation/07-stock-res-ciclo-vida.md`.

---

## Configs respetadas (siete flags distintos)

| Config | Tipo | Uso |
|---|---|---|
| `Explosion_Automatica` | bool | habilita re-búsqueda con presentación por defecto + algoritmo de explosión |
| `Explosion_Automatica_Desde_Ubicacion_Picking` | bool | habilita explosión incluso desde zona picking |
| `Explosion_Automatica_Nivel_Max` | int | nivel máximo de ubicación para permitir explosión |
| `Conservar_Zona_Picking_Clavaud` | bool | habilita el algoritmo Clavaud (3 loops particionados) |
| `Despachar_existencia_parcial` | enum `tDespacharExistenciaParcial` | si NO, throw S0004 cuando no hay stock suficiente |
| `Rechazar_pedido_incompleto` | enum `tRechazarPedidoIncompleto` | si SI, throw cuando solicitado > disponible |
| `Idbodega` | int | bodega target |

Esto contrasta fuertemente con el resto del pipeline:
- `Reserva_Stock` (núcleo): no consulta config
- `Especifico ov1/ov2`: ignora `Despachar_existencia_parcial` (V-OV2-006)
- `Lista_Result`: respeta `Despachar_existencia_parcial` pero ignora los demás
- `From_Reabasto`: respeta los siete

**Reabasto es el flujo más "configurable" del pipeline**. Cualquier cambio de comportamiento en BD a través de la UI `frmConfiguracion.vb` afecta primariamente a Reabasto.

---

## Bugs y violaciones (9 nuevos + 2 heredados)

| ID | Severidad | Descripción | Línea |
|---|---|---|---|
| V-FROMR-001 | BAJA | Debug code hardcoded: `If IdProductoBodega = 183 Then Debug.Print("Hola")` y similar con 82 | 9943, 9968 |
| V-FROMR-002 | MEDIA | Hardcode `Indicador = "PED"` con patrón defensive null check | 10570, 10736 |
| V-FROMR-003 | BAJA | Validación oscura `Math.Abs(... - Fix(...))` como condición de Throw, refactor sugerido | 15 lugares |
| V-FROMR-004 | MEDIA | `Catch ex As Exception` que solo hace `Throw ex` (pierde stack trace) y NO agrega contexto de función | 13105 |
| V-FROMR-005 | TRIVIAL | Typo en marker: `#EJC30220525` (year 3022) | 10594 |
| V-FROMR-006 | **ALTA** | Muta `pStockRes` ByRef reemplazándolo por `BeStockRes` (replace completo del parámetro de entrada) | 12804, 12954, 13075 |
| V-FROMR-007 | **ALTA** | `pListStockResOUT` se asigna con semánticas DISTINTAS (append en algunos paths, replace en otros) | 5 lugares |
| V-FROMR-008 | MEDIA | Recursión C sin guard `No_bulto = 1965` (bug latente) | 12765 |
| V-FROMR-009 | BAJA | Estado `"UNCOMMITED"` (typo persistente, hardcode magic string) | 10680, 10756 |

Los más importantes son **V-FROMR-006 y V-FROMR-007**, porque afectan la **interfaz** que ven los callers. Cualquier código que llame a `Reabasto` necesita estar consciente de:
1. Su `pStockRes` será reemplazado por el objeto reservado (no es la solicitud que pasó).
2. Su `pListStockResOUT` puede recibir append O replace según el path interno.

Esto es un riesgo concreto para Wave 13: cualquier validación legacy-vs-net8 sobre callers de `Reabasto` tiene que considerar estos comportamientos.

---

## Referencias a casos QA

El marker `EJC202210182124_CASO#4` aparece **3 veces** (L10848, L11615, L12405), una en cada loop, en el mismo punto estructural (dentro de CASO_#2 ELSE post-split). Sugiere que el algoritmo Clavaud + split físico fue introducido o validado contra el caso QA `Ejecuta_QA_CASO_4_IDEAL_*` del archivo embebido.

**Cuando se inventaríen las 20 funciones QA** (decisión pendiente Wave 12), el caso 4 tiene prioridad de revisión para confirmar que documenta este flujo y verificar la cobertura.

---

## Open questions

| ID | Severidad | Pregunta |
|---|---|---|
| Q-FROMR-01 | — | ¿Cuáles son los principales callers reales de From_Reabasto? |
| Q-FROMR-02 | — | ¿Qué cambia `pTarea_Reabasto` en el comportamiento de `clsLnStock.lStock`? |
| Q-FROMR-03 | — | ¿La inconsistencia de `pListStockResOUT` (V-FROMR-007) afecta a algún caller real? |
| Q-FROMR-04 | MEDIA | ¿En qué clientes está activo `Conservar_Zona_Picking_Clavaud`? Especialmente: ¿BYB lo tiene? |
| Q-FROMR-05 | MEDIA | Recursión C sin guard (V-FROMR-008): ¿hay path conocido que la dispare anidada? |
| Q-FROMR-06 | — | ¿Qué cubre exactamente `Ejecuta_QA_CASO_4_IDEAL_*`? |
| Q-FROMR-07 | **ALTA** | Si BYB usa `Reabasto` con `Conservar_Zona_Picking_Clavaud=ON` y `Explosion_Automatica=ON`, **¿GAP-12-001 está parcialmente cubierto**? Hay que cruzar para Wave 13. |

---

## Cross-references

- `01-reserva-stock-core` — entry point principal del módulo
- `03-reserva-stock-lista-result` — V-002 split físico (Reabasto generaliza este patrón)
- `04-reserva-stock-especifico-ov1` y `05-...-ov2` — NO comparten código con Reabasto
- `gaps/GAP-12-001-byb-codigo-1060315-no-replicado-en-net8.md` — Q-FROMR-07 conecta con este gap
- `brain/entities/modules/reservation/05-mi3-algoritmo-fefo-clavaud.md` — el algoritmo Clavaud (Reabasto y MI3 lo implementan ambos en legacy)
- `brain/entities/modules/reservation/07-stock-res-ciclo-vida.md` — estado UNCOMMITED (V-FROMR-009)

---

## Acciones sugeridas

1. **Resolver Q-FROMR-04 con un SELECT a la BD** (read-only): identificar qué clientes tienen `Conservar_Zona_Picking_Clavaud = 1`. Si BYB está en la lista, Q-FROMR-07 se vuelve más urgente.
2. **Resolver Q-FROMR-02 leyendo `clsLnStock.lStock`** — esta función es el "core" de la búsqueda de stock disponible y todavía no está documentada.
3. **Cuando se documenten las 20 funciones QA**, priorizar `Ejecuta_QA_CASO_4_IDEAL_*` para resolver Q-FROMR-06.
4. **Cuando Erik confirme casos de uso**, llenar `callers:` con detalles reales (Q-FROMR-01, Q-FROMR-03).
5. Considerar abrir **GAP-12-002** (V-FROMR-007: inconsistencia de `pListStockResOUT`) si después de Q-FROMR-03 se confirma que algún caller depende de la semántica.
