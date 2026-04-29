# Reserva_Stock_From_MI3

> **Archivo legacy**: `TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`
> **Rango**: L18192-L26337 (8.146 líneas — la función más grande del archivo)
> **Documentada en**: sub-wave 12C-1 (panorámico estructurado), 2026-04-29
> **Approach**: panorámico estructurado, no deep. Razón: la función es 2,5x el tamaño de Reabasto y ya está parcialmente refactorizada con 6 helpers extraídos. Documentar cada caso interno línea por línea sería redundante con la documentación pendiente de los helpers. Se prioriza la arquitectura, las novedades vs Reabasto, los bugs descubiertos al pasar, y se dejan abiertas las preguntas que requieren ir más profundo en cada caso.

---

## Por qué importa

MI3 es el **gemelo arquitectónico de Reabasto** (firma idéntica) pero está mucho más avanzada en su evolución:

1. **Es la única función del pipeline refactorizada con helpers extraídos** (Reabasto y las demás son flat).
2. **Implementa una state machine con GoTo + Select Case** que permite reanudación del proceso desde un punto arbitrario (`Iniciar_En = 1, 2, 3`).
3. **Tiene 7 puntos de auto-recursión** (vs 3 en Reabasto) y un **nuevo modo de fallback "presentación faltante"** (`BeStockResPresFalt`).
4. **Respeta 9 configs** (la función más configurable del pipeline) — incluyendo dos nuevas: `considerar_paletizado_en_reabasto` e `Interface_SAP`.
5. **Tiene su propio sistema de auditoría** que persiste cada path elegido a `i_nav_trans_pe_det_log_reserva` con markers numerados (`_LLR_CASO_#28_` a `_#31_`). Ninguna otra función tiene esta capacidad.
6. **Es la función más activamente mantenida del módulo** — 4 fixes identificados en 2024-2025, incluyendo `#CKFK20250924` (un mes antes de hoy) y `#CKFK20250426`.

---

## Firma — idéntica a Reabasto

```vb
Public Shared Function Reserva_Stock_From_MI3(
    ByRef pStockResSolicitud As clsBeStock_res,                        ' ← cambio cosmético (era pStockRes)
    ByVal DiasVencimiento As Double,
    ByVal MaquinaQueSolicita As String,
    ByVal pBeConfigEnc As clsBeI_nav_config_enc,
    ByRef pCantidadDisponibleStock As Double,
    ByVal pIdPropietarioBodega As Integer,
    ByRef pListStockResOUT As List(Of clsBeStock_res),
    ByRef lConnection As SqlConnection,
    ByRef ltransaction As SqlTransaction,
    Optional No_Linea As Integer = 0,
    Optional pTarea_Reabasto As Boolean = False,
    Optional ByVal pBeTrasladoDet As clsBeI_nav_ped_traslado_det = Nothing
) As Boolean
```

El único cambio respecto a Reabasto es el nombre del primer parámetro (`pStockResSolicitud` vs `pStockRes`) — puramente cosmético, sugiere que MI3 fue escrita pensando en el rol semántico ("la solicitud") en lugar de la entidad ("el stock_res").

---

## Helpers extraídos (las 6 funciones que viven entre MI3 y SAP)

| Helper | Línea | Visibilidad | Uso en MI3 | Notas |
|---|---|---|---|---|
| `Procesar_Y_Restar_Stock_Reservado` | L26340 | Public | 3 calls (sobre 3 listas) | Reemplaza inline `Restar_Stock_Reservado_EJC` de Reabasto |
| `Procesar_Logica_Presentacion_Stock` | L26399 | Public | 0 calls desde MI3 | Helper preparatorio, posiblemente consumido fuera del archivo |
| `Get_Objetos_Producto` | L26443 | Private | 1 call inicial | Resuelve producto + presentación por defecto |
| `Tiene_Cantidad_Suficiente` | L26500 | Public | (no detectado) | Validador puro, posiblemente reusable |
| `Get_Fecha_Vence_Minima_Stock_Reserva_MI3` | L26524 | Private | **46 calls (uso intensivo)** | Helper FEFO específico de MI3 |
| `Get_All_By_IdBodega` | L26648 | Public | (no detectado) | Helper genérico, prioridad baja |

Los **46 calls a `Get_Fecha_Vence_Minima_Stock_Reserva_MI3`** son una bandera grande: ese helper define gran parte del comportamiento FEFO de MI3 y necesita su propia documentación. Queda como **prioridad ALTA** para sub-wave posterior.

Además MI3 llama a 2 helpers que **viven fuera del archivo**:
- `Cargar_Bodega_Y_Linea_Pedido` (1 call inicial)
- `Obtener_Listas_De_Stock` (1 call que devuelve un tuple/anonymous con 3 listas)

---

## State machine con GoTo + Select Case

MI3 implementa un control de flujo no-estructurado **deliberado**: usa labels GoTo + un `Select Case Iniciar_En` para permitir **reanudación del proceso** desde un punto arbitrario. Esto es un anti-patrón de programación moderna pero funcional para el caso de uso.

### Variables de estado de la máquina

- `Iniciar_En As Integer = 0` — qué bloque debe ejecutarse primero
- `ListaEstadosDeProceso As List(Of Integer)` — códigos opacos `100, 101, 102, 103` (V-MI3-003: magic numbers)
- `vNombreCasoReservaInternoWMS As String` — acumula markers de auditoría (`_LLR_CASO_#28_` etc)

### Labels descubiertas

```text
INICIAR_CON_NUEVO_LSTOCK           ← reinicia desde carga de listas
INICIAR_EN_1                        ← Pallets completos lBeStockConPalletsCompletosClavaud
INICIAR_EN_2                        ← Pallets incompletos lBeStockConPalletsInCompletosClavaud
INICIAR_EN_3                        ← Producto en zona picking
EXPLOSIONAR_PRODUCTO                ← Salto a explosión por múltiplo
ANALIZAR_FECHAS_DE_VENCIMIENTO      ← Target compartido por muchos GoTos
EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING
EJC_202308081248_RESERVAR_DESDE_ZONA_NO_PICKING1   ← pase 1 (con sufijo)
EJC_202308081248_RESERVAR_DESDE_ZONA_NO_PICKING    ← pase 2 (SIN sufijo, V-MI3-004)
EJC_202308081248_RESERVAR_DESDE_ULTIMA_LISTA       ← fallback final del Select Case
```

El `Select Case Iniciar_En` está en L19453 con casos 1, 2, 3, Else. Cada caso dispara el GoTo correspondiente.

**Riesgo crítico para .NET 8**: cualquier replicación a C# tendrá que **reemplazar este patrón con métodos separados o un state pattern explícito**. Mantener la state machine GoTo en código moderno es inviable para mantenimiento.

---

## Mapa de bloques (14 bloques estructurales)

| Bloque | Líneas | Qué hace |
|---|---|---|
| **B1** | 18192-18311 | Firma + #Region Variables (50+ Dim) |
| **B2** | 18312-18365 | Setup: tipo pedido, devolución, BeBodega, Get_Objetos_Producto, Obtener_Listas_De_Stock, label `INICIAR_CON_NUEVO_LSTOCK` |
| **B3** | 18367-18410 | #Region RESTAR_STOCK_RESERVADO (3 calls) |
| **B4** | 18412-18433 | #Region OBTENER_FECHA_MINIMA_DE_INVENTARIO |
| **B5** | 18434-19060 | Validaciones + `Select Case Iniciar_En` + GoTos |
| **B6** | 19061-19123 | #Region Recalcular stocks y fechas de vencimiento |
| **B7** | 19124-20942 | Loop Clavaud Pallets Completos + Pallets Incompletos (~1.800 L sin region) |
| **B8** | 20943-21323 | #Region Reservar stock de zona de picking (`INICIAR_EN_3`) |
| **B9** | 21324-21392 | #Region Explosión por múltiplo (anidada en zona picking) |
| **B10** | 22111-22836 | #Region "Reserverar stock de zona NO Picking" (PASE 1 — typo "Reserverar") |
| **B11** | 22845-23874 | #Region "Reservar stock de zona NO Picking" (PASE 2 — typo corregido) |
| **B12** | 24395-24463 | #Region Explosión por múltiplo (en zona NO picking) |
| **B13** | 24464-25599 | Bloque sin region: último pase + `EJC_202308081248_RESERVAR_DESDE_ULTIMA_LISTA` |
| **B14** | 25600-26337 | Cierre: 7 recursiones + INSERT principal + Catch |

---

## Las 7 recursiones — degradación gradual con 2 modos

| # | Línea | Caso | Acumulador | Notas |
|---|---|---|---|---|
| 1 | L25661 | `_LLR_CASO_#28_` | `vlBeStockAReservarUMBas` | Fix `#CKFK20240116`: cambio ExcluirUbicacionesPicking → pTareaReabasto |
| 2 | L25749 | (inferido `#29_`) | `vlBeStockAReservarUMBas` | Fix `#EJC20231031`: mantener presentación en recursión (antes la perdía) |
| 3 | L25814 | (inferido `#30_`) | `vlBeStockAReservarUMBas` | Branch alternativo presentación + decimal |
| 4 | **L25900** | (inferido `#31_`) | `vlBeStockAReservarPresFaltante` | **NOVEDAD: caso PresFaltante** |
| 5 | L26132 | (sin marker) | `vlBeStockAReservarUMBas` | Fix `#CKFK20250924`: agregar al log (muy reciente) |
| 6 | L26272 | (sin marker) | `vlBeStockAReservarUMBas` | Fallback final post-INSERT |
| 7 | (firma) | — | — | La función misma como entry point recursivo |

### Patrones clave

- **Todas las recursiones tienen guard anti-loop** (`If BeStockResUMBas.No_bulto = 0`). MI3 NO tiene el bug latente V-FROMR-008 de Reabasto.
- **Cada recursión está precedida de un log estructurado** vía `clsLnTrans_pe_det_log_reserva.Agregar_Log_Reserva` con contexto rico (FechaMinima, DiasVencimiento, FechaMinimaVenceZonaPicking, FechaMinimaVenceZonaALM, Lote, Ubicación). Esto da observabilidad que Reabasto NO tiene.
- **Recursión 4 (L25900) es la NOVEDAD**: usa `BeStockResPresFalt` y acumulador separado `vlBeStockAReservarPresFaltante`. Es para el caso donde hay stock disponible **pero en una presentación distinta a la solicitada**. No existe en Reabasto. Pendiente Q-MI3-05 para entender el filtro exacto.

---

## Sistema de auditoría con casos numerados

MI3 implementa una capa de observabilidad que ninguna otra función tiene. Cada vez que el flujo elige un path:

1. Append a `vNombreCasoReservaInternoWMS` con un marker (`_LLR_CASO_#28_` etc)
2. Construcción de `vMensajeReserva` con todo el contexto temporal
3. Persistencia a tabla `i_nav_trans_pe_det_log_reserva` vía `Agregar_Log_Reserva`

### Casos descubiertos en código

```text
_LLR_CASO_#28_      _LLR_CASO_#29_      _LLR_CASO_#30_      _LLR_CASO_#31_
                                         _CASO_#30_           _CASO_#31_
```

Hay **6 markers** (los `_LLR_` y los sin prefijo aparecen como variantes). El acrónimo "LLR" es opaco — pendiente Q-MI3-02.

**Implicación operativa**: el equipo puede reconstruir post-mortem QUE camino tomó MI3 para una reserva específica. Esto es valioso. Vale la pena documentar la tabla `i_nav_trans_pe_det_log_reserva` en el módulo de audit-trail (Q-MI3-07).

---

## Configs respetadas (9 — la función más configurable del pipeline)

| Config | Tipo | Novedad vs Reabasto |
|---|---|---|
| `Conservar_Zona_Picking_Clavaud` | bool | (heredado) |
| `considerar_paletizado_en_reabasto` | bool | **NUEVO** |
| `Despachar_existencia_parcial` | enum | (heredado) |
| `Explosion_Automatica` | bool | (heredado) |
| `Explosion_Automatica_Desde_Ubicacion_Picking` | bool | (heredado) |
| `Explosion_Automatica_Nivel_Max` | int | (heredado) |
| `Idbodega` | int | (heredado) |
| `Interface_SAP` | bool | **NUEVO — sugiere discriminación de behavior según ERP** |
| `Rechazar_pedido_incompleto` | enum | (heredado) |

`Interface_SAP` es especialmente interesante: el solo hecho de que la función llamada `From_MI3` consulte un flag `Interface_SAP` sugiere que **MI3 y SAP tienen branches compartidos pero comportamientos divergentes según el ERP origen**. Esto es Q-MI3-03 (ALTA).

---

## Bugs y violaciones (9 detectadas)

| ID | Severidad | Descripción | Línea |
|---|---|---|---|
| V-MI3-001 | BAJA | Debug code: `If IdProductoBodega = 616 Then Debug.Print("Aqui ...")` | 18313 |
| V-MI3-002 | **ALTA** | Uso intensivo de GoTo + 10+ labels (anti-patrón estructural). Riesgo crítico para refactor a .NET 8 | múltiples |
| V-MI3-003 | MEDIA | Magic numbers `100, 101, 102, 103` en `ListaEstadosDeProceso` sin enum | múltiples |
| V-MI3-004 | MEDIA | Labels casi-idénticas (`..._NO_PICKING1` vs `..._NO_PICKING`) + typo "Reserverar" en region | 22111, 22845 |
| V-MI3-005 | **ALTA** | **Bug funcional confirmado**: `vMensaje = vMensaje = String.Format(...)` — doble `=` (asignación de bool en vez de concatenación) | 26283 |
| V-MI3-006 | MEDIA | Catch genérico `Throw ex` (pierde stack trace) sin contexto. Heredado de Reabasto | 26336 |
| V-MI3-007 | MEDIA | Hardcode `Indicador="PED"` (23 ocurrencias estimadas, requiere validación puntual) | múltiples |
| V-MI3-008 | BAJA | `Estado="UNCOMMITED"` typo persistente (29 ocurrencias) | múltiples |
| V-MI3-009 | BAJA | Acrónimo opaco "LLR" en markers de auditoría | múltiples |

### El más importante: V-MI3-005

```vb
vMensajeNoExplosionEnZonasNoPicking = vMensajeNoExplosionEnZonasNoPicking = String.Format(...)
```

El segundo `=` es comparación (devuelve `Boolean`), el primero asigna ese boolean a la variable. La intención era concatenar (`&=` o `+=`). Resultado: el mensaje de error **nunca se concatena bien en este path** — la variable termina con la cadena `"True"` o `"False"` según la comparación.

**Pendiente Q-MI3-09**: validar si este mensaje termina en algún sistema crítico visible al usuario o solo en un log interno. Si afecta UX, hay que abrir GAP.

---

## Mutaciones de estado

| Tabla | Operación | Notas |
|---|---|---|
| `stock` | INSERT/UPDATE/DELETE estimado | 49 STOCK-CALL detectados, no auditadas individualmente. Q-MI3-11 confirmará si comparte el split físico de Reabasto |
| `stock_res` | INSERT batch (3 puntos) | Vía `Inserta_Stock_Reservado` |
| **`i_nav_trans_pe_det_log_reserva`** | **INSERT (audit log)** | **Vía `Agregar_Log_Reserva` — NOVEDAD: solo MI3 escribe a esta tabla** |
| `i_nav_ped_traslado_det` | UPDATE `Quantity_Reserved_WMS` | Igual que Reabasto, condicional a `pBeTrasladoDet` |

---

## Códigos de error nuevos

54 throws totales. Novedades respecto al pipeline anterior:

- **`S0005`** (2 ocurrencias) — pendiente Q-MI3-04
- **`S0006`** (1 ocurrencia) — pendiente Q-MI3-04
- `ERROR_202309120159` (6 ocurrencias)
- `ERROR_202310312158` (7 ocurrencias)
- `Error20230306` (6 ocurrencias)
- `ERROR_202401291007` (2 ocurrencias)
- **`ERROR_20250615` (1 ocurrencia, fix muy reciente)**

Los códigos S0001-S0004 ya están documentados en el módulo `reservation/06-codigos-error.md` (pendiente). S0005 y S0006 hay que agregarlos cuando se confirmen sus mensajes.

---

## Open questions (11 — varias de severidad alta)

| ID | Severidad | Pregunta |
|---|---|---|
| Q-MI3-01 | ALTA | ¿Cuáles son los principales callers de From_MI3? |
| Q-MI3-02 | MEDIA | ¿Qué significa el acrónimo "LLR" en `_LLR_CASO_#XX_`? |
| Q-MI3-03 | ALTA | ¿Qué cambia exactamente `Interface_SAP=True` en MI3? |
| Q-MI3-04 | MEDIA | ¿Qué significan los códigos S0005 y S0006 nuevos? |
| Q-MI3-05 | ALTA | Recursión PresFaltante (L25900): ¿qué campo distingue "presentación faltante" vs "correcta"? |
| Q-MI3-06 | ALTA | ¿Semántica exacta de `100, 101, 102, 103` en `ListaEstadosDeProceso`? |
| Q-MI3-07 | ALTA | ¿La tabla `i_nav_trans_pe_det_log_reserva` está consumida por algún reporte? Retention? Tamaño? |
| Q-MI3-08 | MEDIA | ¿Las 6 helpers extraídas se usan solo desde MI3 o también desde otras partes? |
| Q-MI3-09 | ALTA | V-MI3-005 (bug doble `=`): ¿afecta a algún mensaje crítico visible al usuario? |
| Q-MI3-10 | MEDIA | ¿pStockResSolicitud y pListStockResOUT también se mutan como en Reabasto (V-FROMR-006/007)? |
| Q-MI3-11 | ALTA | ¿MI3 hace split físico de stock con `No_bulto=1989` igual que Reabasto? |

---

## Cross-references

- `01-reserva-stock-core` — entry point principal del módulo
- `06-reserva-stock-from-reabasto` — gemelo arquitectónico, menos refactorizado
- `brain/entities/modules/reservation/05-mi3-algoritmo-fefo-clavaud.md` — el algoritmo Clavaud (MI3 lo implementa con zonas particionadas)
- `brain/entities/audit-trail/i_nav_trans_pe_det_log_reserva.md` — pendiente, único consumidor MI3
- `gaps/GAP-12-001-byb-codigo-1060315-no-replicado-en-net8.md` — MI3 tampoco tiene hardcode 1060315

---

## Acciones sugeridas

1. **Sub-wave 12C-1-helpers**: documentar las 6 helpers extraídas (especialmente `Get_Fecha_Vence_Minima_Stock_Reserva_MI3` con sus 46 calls).
2. **Resolver Q-MI3-03 con `rg`**: identificar las ramas afectadas por `Interface_SAP` en MI3 antes de empezar 12C-2 (SAP).
3. **Resolver Q-MI3-07 con un read-only a BD** (cuando se autorice): `SELECT count(*), MIN(fecha), MAX(fecha) FROM i_nav_trans_pe_det_log_reserva` para entender si la tabla está activa o muerta.
4. **Resolver Q-MI3-09**: leer L26270-L26310 con detalle para confirmar si el bug `vMensaje = vMensaje =` afecta un canal crítico.
5. **Cuando se documente la sub-wave 12C-2 (From_SAP)**, comparar firma + 9 configs para mapear deltas finos. Probable que SAP comparta arquitectura con MI3 + variaciones específicas SAP.

---

## Estado del pipeline

| # | Función | Líneas | Estado |
|---|---|---|---|
| 1 | `Reserva_Stock` (núcleo) | 553 | piloto Wave 12 |
| 2 | `Reserva_Stock_NAV_BYB` | 290 | Wave 12 |
| 3 | `Reserva_Stock_Lista_Result` | 1.397 | Wave 12 + GAP-12-001 |
| 4 | `Reserva_Stock_Especifico` ov1 | 245 | Wave 12 |
| 5 | `Reserva_Stock_Especifico` ov2 | 186 | sub-wave 12B-1 (delta) |
| 6 | `Reserva_Stock_From_Reabasto` | 3.253 | sub-wave 12B-2 (deep) |
| 7 | **`Reserva_Stock_From_MI3`** | **8.146** | **sub-wave 12C-1 (panorámico)** |
| 8 | `Reserva_Stock_From_SAP` | 7.067 | pendiente (12C-2) |

Total cubierto: **7/8 funciones, 14.210 / 20.125 L = 70,6%** del archivo.
