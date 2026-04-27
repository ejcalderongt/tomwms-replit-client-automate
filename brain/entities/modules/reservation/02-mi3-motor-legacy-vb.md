# 02 · Reserva de Stock MI3 — Motor Legacy (VB.NET monolítico)

> **Fuente primaria**: `TOMWMS_BOF/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb`, anchors **L18192-L26337** (~8 145 líneas dentro de un único método `Reserva_Stock_From_MI3`).  
> **Estado actual**: deprecado en `dev_2028_merge`. Reemplazado por `WMS.DALCore/Reserva_Stock/` (.NET 8). Documentado para entender comportamiento legado, debuggear pedidos antiguos en `log_error_wms` y validar paridad funcional con el motor nuevo.  
> **Cross-refs**: `01-mi3-motor-nuevo-net8.md`, `03-comparison.md`, `decisions/003-mi3-reescrito.md`.

---

## Índice

1. Forma del método (firma + 120 variables locales)
2. Mapa de etiquetas, regiones y `GoTo`
3. Setup inicial (carga de entidades, listas de stock)
4. Bloque RESTAR_STOCK_RESERVADO
5. Bloque OBTENER_FECHA_MINIMA_DE_INVENTARIO
6. Etiqueta `EXPLOSIONAR_PRODUCTO` (decisión UMBas/explosión)
7. Etiqueta `INICIAR_EN_1` (pallets completos Clavaud)
8. Etiqueta `INICIAR_EN_2` (pallets incompletos Clavaud)
9. Etiqueta `INICIAR_EN_3` (zona picking + zona no-picking)
10. Regiones de "Explosión por múltiplo" (dos copias quasi-idénticas)
11. Recursión interna de `Reserva_Stock_From_MI3` (marcador `No_bulto = 1965`)
12. Métodos auxiliares (Procesar_Y_Restar, Get_Fecha_Vence_Minima, Split_Decimal, Tiene_Cantidad_Suficiente)
13. Banderas de control y patología del flujo
14. Errores conocidos y trampas históricas (`ERROR_*` strings)

---

## 1. Forma del método

### 1.1 Firma

```vbnet
Public Shared Function Reserva_Stock_From_MI3(
    ByRef pStockResSolicitud As clsBeStock_res,
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

12 parámetros, 4 de ellos `ByRef` (in-out). El único valor de retorno es `Boolean`. Toda la información de salida real va por:

- `pCantidadDisponibleStock` (cuánto stock había disponible al final)
- `pListStockResOUT` (lista de reservas creadas)
- `pBeTrasladoDet.Process_Result` (mensaje textual de qué pasó, concatenado a lo largo del método)

> No hay tipado de status (Success/Partial/Failed). El caller infiere el resultado del bool + la cantidad reservada vs la solicitada.

### 1.2 Bloque `Variables` (L18-L120)

`#Region "Variables"` declara **~120 `Dim`** locales antes de cualquier ejecución. Categorías:

| Familia                                       | Cantidad | Ejemplos representativos |
|-----------------------------------------------|----------|---------------------------|
| Listas de stock (mutables)                    | 8        | `lBeStockExistente`, `lBeStockExistenteZonasNoPicking`, `lBeStockExistenteZonaPicking`, `lBeStockExistenteTmp`, `lBeStockExistenteTomeDesde`, `lBeStockDisponible`, `lBeStockConPalletsCompletosClavaud`, `lBeStockConPalletsInCompletosClavaud` |
| Listas de reservas a crear                    | 3        | `lBeStockAReservar`, `vlBeStockAReservarUMBas`, `vlBeStockAReservarPresFaltante` |
| Cantidades en presentación                    | ~14      | `vCantidadDispStock`, `vCantidadPendiente`, `vCantidadSolicitadaPedido`, `vCantidadAReservarPorIdStock`, `vCantidadEnteraPres`, `vCantidadEnStockEnPres`, `vCantidadDispStockEnPres`, `vCantidadPendienteEnPres`, … |
| Cantidades en UMBas                           | ~10      | `vCantidadDecimalUMBas`, `vCantidadDecimalStockUMBas`, `vCantidadDecimalUMBasStock`, `CantidadEnUMBasPorPresentacionDelStock`, … |
| Pesos (paralelos a cantidades)                | ~8       | `vPesoReservado`, `vPesoStock`, `vPesoPendiente`, `vPesoSolicitadoPedido`, `vPesoAReservarPorIdStock`, … |
| Fechas FEFO                                   | 6        | `FechaMinimaVenceStock`, `vFechaMinimaVenceZonaPicking`, `vFechaMinimaVenceZonaALM`, `vVenceMinimaPickingCompletoClavaud`, `vVenceMinimaPickingInCompletoClavaud`, `vFechaDefecto = 1900-01-01` |
| Banderas de control                           | ~12      | `vCantidadCompletada`, `vBusquedaEnUmBas`, `vRestoStockReservado`, `vConvirtioCantidadSolicitadaEnUmBas`, `vSolicitudEsEnUMBas`, `vEncontroExistenciaEnPresentacion`, `vRestoInventarioEnUmBas`, `vZonaNoPickingStockEnUmBas`, `vOrdernarListaStockSinPresentacionPrimero`, `vPermitirDecimales`, `pEs_Devolucion`, `ExcepcionFechaVenceEsInferiorEnZonaPicking` |
| Estados de proceso                            | 1        | `ListaEstadosDeProceso As New List(Of Integer)` ← clave del flujo (ver §13) |
| Pivote `Iniciar_En`                           | 1        | `Dim Iniciar_En As Integer = 0` ← determina a qué `INICIAR_EN_*` saltar |
| Entidades de dominio (snapshot)               | ~10      | `BeProducto`, `BeStockRes`, `BeStockDestino`, `BeUbicacionStock`, `BePresentacionDefecto`, `BePedidoDet`, `BeBodega`, `BeUbicacionEnMemoria`, `BeStockOriginal`, `BeCliente` |
| Misceláneo                                    | varios   | `Idx`, `vIdTipoPedido`, `vMensajeReserva`, `vMensajeNoExplosionEnZonasNoPicking`, `vIndicePresentacion`, `vIndiceUbicacion`, `vPresReserva` |

Esta carga de variables es intencional para el patrón legacy: todo se preasigna con valores por defecto (`0`, `False`, `Date(1900, 1, 1)`, `New List(Of …)`) para que los GoTo no rompan por NPE en saltos.

---

## 2. Mapa de etiquetas, regiones y GoTo

### 2.1 Etiquetas (`label:`) confirmadas en el extract

| Línea | Etiqueta                                  | Función |
|-------|-------------------------------------------|---------|
| L172  | `INICIAR_CON_NUEVO_LSTOCK:`               | Reentry tras recargar listas (recursivo) |
| L245  | `EXPLOSIONAR_PRODUCTO:`                   | Decisión: explosionar/UMBas o no |
| L868  | `ANALIZAR_FECHAS_DE_VENCIMIENTO:`         | Reentry tras avance del puntero FEFO |
| L1273 | `INICIAR_EN_1:`                           | Procesa pallets COMPLETOS no-picking (Clavaud) |
| L1904 | `INICIAR_EN_2:`                           | Procesa pallets INCOMPLETOS no-picking (Clavaud) |
| L2712 | `INICIAR_EN_3:`                           | Procesa stock en zona picking + no-picking |
| L2751 | `EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING:` | Sub-anchor dentro de INICIAR_EN_3 |
| L1270 | `EJC_202308081248_RESERVAR_DESDE_ULTIMA_LISTA` | Anchor de retorno del switch `Iniciar_En` |

### 2.2 Regiones (`#Region` / `#End Region`)

| Líneas        | Región |
|---------------|--------|
| L18-L120      | `"Variables"` |
| L176-L219     | `"RESTAR_STOCK_RESERVADO"` |
| L221-L242     | `"OBTENER_FECHA_MINIMA_DE_INVENTARIO"` |
| L870-L932     | `"Recalcular stocks y fechas de vencimiento"` |
| L2752-L3201   | `"Reservar stock de zona de picking"` |
| L3133-L3201   | `"Explosión por múltiplo"` (1ª copia, dentro de zona picking) |
| L3920-L4645   | `"Reserverar stock de zona NO Picking"` (1ª copia) |
| L4654-L5683   | `"Reservar stock de zona NO Picking"` (2ª copia) |
| L6204-L6272   | `"Explosión por múltiplo"` (2ª copia) |

> **Nota**: la región "Explosión por múltiplo" y la región "Reservar stock de zona NO Picking" aparecen **dos veces cada una**. El bloque está duplicado con variantes mínimas. Esto es la causa principal del crecimiento del archivo a 8 mil líneas.

### 2.3 GoTo del flujo principal

```
ANALIZAR_FECHAS_DE_VENCIMIENTO  ←─── (desde INICIAR_EN_1 L1289 / INICIAR_EN_2 L1952)
INICIAR_CON_NUEVO_LSTOCK         ←─── (post explosión, recarga de listas)
EXPLOSIONAR_PRODUCTO             ←─── (cuando una lista se vacía)
EJC_202308081248_RESERVAR_DESDE_ULTIMA_LISTA  ←─── (default del switch Iniciar_En)
EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING  ←─── (post INICIAR_EN_3 setup)
```

---

## 3. Setup inicial (L122-L169)

```vbnet
vIdTipoPedido = clsLnTrans_pe_enc.Get_IdTipoPedido_By_IdPedidoEnc(
    pStockResSolicitud.IdPedido, lConnection, ltransaction)

pEs_Devolucion = (vIdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor)

vPresReserva = pStockResSolicitud.IdPresentacion

Cargar_Bodega_Y_Linea_Pedido(pBeConfigEnc.Idbodega, pIdPropietarioBodega,
                             pStockResSolicitud.IdPedido, pStockResSolicitud.IdPedidoDet,
                             lConnection, ltransaction, BeBodega, BePedidoDet)

Get_Objetos_Producto(pStockResSolicitud, BePresentacionDefecto, IdProducto, BeProducto,
                     lConnection, ltransaction)

Dim ListasStock = Obtener_Listas_De_Stock(pStockResSolicitud, BeProducto,
                                          DiasVencimiento, pBeConfigEnc,
                                          lConnection, ltransaction,
                                          pTarea_Reabasto, pEs_Devolucion)

lBeStockExistente              = ListasStock.lBeStockExistente
lBeStockExistenteZonasNoPicking = ListasStock.lBeStockExistenteZonasNoPicking
lBeStockExistenteZonaPicking   = ListasStock.lBeStockExistenteZonaPicking
```

Tres pasos:

1. **Identificar tipo de pedido**: si es devolución a proveedor, `pEs_Devolucion = True` (afecta luego varios filtros).
2. **Cargar bodega y línea de pedido**: por refs `BeBodega` y `BePedidoDet` salen pobladas.
3. **Cargar producto y presentación default**: por refs `BeProducto`, `BePresentacionDefecto`, `IdProducto`.
4. **Obtener las 3 listas base**: existente total, no-picking, picking.

`Obtener_Listas_De_Stock` (L1-L63 del aux) es la versión legacy del `StockQueryStep` del motor nuevo, pero con tres llamadas independientes a `clsLnStock.lStock` con flags distintos por lista.

---

## 4. Bloque `RESTAR_STOCK_RESERVADO` (L176-L219)

Dentro del label `INICIAR_CON_NUEVO_LSTOCK`:

```vbnet
INICIAR_CON_NUEVO_LSTOCK:

    If Not lBeStockExistente Is Nothing Then

#Region "RESTAR_STOCK_RESERVADO"

        Procesar_Y_Restar_Stock_Reservado(lBeStockExistente, ...)
        Procesar_Y_Restar_Stock_Reservado(lBeStockExistenteZonasNoPicking, ...)
        Procesar_Y_Restar_Stock_Reservado(lBeStockExistenteZonaPicking, ...)

        vRestoStockReservado = True

#End Region
```

Llama **tres veces** al mismo procedimiento `Procesar_Y_Restar_Stock_Reservado` para cada una de las tres listas. La firma de ese aux es:

```vbnet
Public Shared Sub Procesar_Y_Restar_Stock_Reservado(
    ByRef lBeStockExistente As List(Of clsBeStock),
    ByRef lPresentaciones As List(Of clsBeProducto_Presentacion),
    ByRef vEncontroExistenciaEnPresentacion As Boolean,
    ByRef vCantidadProductoPorTarima As Decimal,
    ByRef vCantidadTarimasCompletasAPickearClavaud As Decimal,
    ByRef vCantidadEnteraTarimasCompletasClavaud As Integer,
    ByRef vCantidadDecimalTarimasCompletasClavaud As Decimal,
    ByVal pStockResSolicitud As clsBeStock_res,
    ByVal vOrdernarListaStockSinPresentacionPrimero As Boolean,
    ByVal pBeConfigEnc As clsBeI_nav_config_enc,
    ByVal lConnection As SqlConnection,
    ByVal ltransaction As SqlTransaction)
```

Internamente:

1. Llama a `Restar_Stock_Reservado(lista, ...)` que descuenta de cada `clsBeStock.Cantidad` la suma de las reservas pendientes (`stock_res` con `Estado IN ('UNCOMMITED', 'COMMITED')`).
2. Filtra `Where(x => x.Cantidad > 0)`.
3. Resuelve la presentación del solicitud y carga su factor.
4. Llama a `Procesar_Logica_Presentacion_Stock` que computa `vCantidadProductoPorTarima = CajasPorCama * CamasPorTarima` y separa entero/decimal.

---

## 5. Bloque `OBTENER_FECHA_MINIMA_DE_INVENTARIO` (L221-L242)

```vbnet
#Region "OBTENER_FECHA_MINIMA_DE_INVENTARIO"

    FechaMinimaVenceStock = Get_Fecha_Vence_Minima_Stock_Reserva_MI3(
        pStockResSolicitud, DiasVencimiento, pBeConfigEnc, lConnection, ltransaction,
        BeProducto, pTarea_Reabasto,
        vFechaMinimaVenceZonaPicking,    ' ByRef
        vFechaMinimaVenceZonaALM,        ' ByRef
        lBeStockExistente, BePresentacionDefecto)

    If lBeStockExistenteZonasNoPicking IsNot Nothing AndAlso lBeStockExistenteZonasNoPicking.Count > 0 _
       AndAlso vFechaMinimaVenceZonaALM < FechaMinimaVenceStock Then
        lBeStockExistente = lBeStockExistenteZonasNoPicking
    End If

    If FechaMinimaVenceStock.Date = vFechaDefecto _
       AndAlso lBeStockExistente IsNot Nothing AndAlso lBeStockExistente.Count > 0 Then
        FechaMinimaVenceStock = lBeStockExistente.Min(Function(x) x.Fecha_vence)
    End If
#End Region
```

`Get_Fecha_Vence_Minima_Stock_Reserva_MI3` retorna la fecha mínima FEFO **considerando**:

- Días de vencimiento del producto (`DiasVencimiento`)
- Configuración del propietario (`pBeConfigEnc`)
- Si es tarea de reabasto (`pTarea_Reabasto`)
- Y por refs **devuelve también** las fechas mínimas de zona picking (`vFechaMinimaVenceZonaPicking`) y zona ALM (`vFechaMinimaVenceZonaALM`).

> En el motor nuevo esta lógica se separa en `DateCalculationStep` (4 fechas mínimas, sin acoplamiento al config enc).

Después del bloque, si la zona ALM tiene una fecha más temprana que la global, **reemplaza** `lBeStockExistente` por la lista no-picking. Esa mutación es uno de los puntos donde el flujo legacy **pierde trazabilidad** (no se sabe a partir de qué momento `lBeStockExistente` representa "el stock global" o "el stock no-picking").

---

## 6. Etiqueta `EXPLOSIONAR_PRODUCTO` (L245)

```vbnet
EXPLOSIONAR_PRODUCTO:

    If Stock_Requiere_Explosion(pBeConfigEnc, lBeStockExistente, pStockResSolicitud) Then

        If pStockResSolicitud.IdPresentacion = 0 Then
            vOrdernarListaStockSinPresentacionPrimero = True
            vBusquedaEnUmBas = True
        End If

        pStockResBusquedaParaExplosion = Nothing

        If lBeStockExistente.Count = 0 _
           AndAlso lBeStockExistenteZonaPicking.Count = 0 _
           AndAlso lBeStockExistenteZonasNoPicking.Count > 0 Then
            lBeStockExistente = lBeStockExistenteZonasNoPicking
        End If

        If lBeStockExistente.Count = 0 Then
            ' #EJC20250714: Funcionalidad para Killios por funcionalidad del estado reempaque.
            Dim vEstadoProducto As New clsBeProducto_estado
            Dim vReservaUMBas As Boolean = False

            If pStockResSolicitud.IdProductoEstado <> 0 Then
                vEstadoProducto = clsLnProducto_estado.GetSingle(
                    pStockResSolicitud.IdProductoEstado, lConnection, ltransaction)
                vReservaUMBas = vEstadoProducto.Reservar_En_UmBas
            End If

            If vReservaUMBas AndAlso pBeConfigEnc.Interface_SAP Then
                vBusquedaEnUmBas = True
                pStockResSolicitud.IdPresentacion = 0
            ElseIf pStockResSolicitud.IdPresentacion = 0 Then
                If BePresentacionDefecto IsNot Nothing Then
                    pStockResBusquedaParaExplosion = pStockResSolicitud.Clone()
                    pStockResBusquedaParaExplosion.IdPresentacion = BePresentacionDefecto.IdPresentacion
                    vBusquedaEnUmBas = False
                Else
                    ' #CKFK20240320 no aplica lanzar excepcion por esto
                    ' Throw New Exception("ERROR_202302021127: ...")
                End If
            ElseIf Not vEncontroExistenciaEnPresentacion Then
                If pBeConfigEnc.Rechazar_pedido_incompleto = tRechazarPedidoIncompleto.Si Then
                    Throw New Exception(String.Format(
                        "Error_202212140140D: {0} Código: {1} Sol: {2} Disp: {3}. ",
                        clsDalEx.ErrorS0002, BeProducto.Codigo, pStockResSolicitud.Cantidad, 0))
                ElseIf Not vCantidadCompletada Then
                    If BePedidoDet.IdPresentacion = 0 Then
                        vBusquedaEnUmBas = True
                    End If
                End If
            Else
                vBusquedaEnUmBas = True
            End If
        ElseIf Not vCantidadCompletada AndAlso (pStockResSolicitud.IdPresentacion = 0 _
                                              OrElse (BePedidoDet IsNot Nothing _
                                                  AndAlso BePedidoDet.IdPresentacion = 0)) Then
            vBusquedaEnUmBas = True
        End If

        If vBusquedaEnUmBas Then
            Split_Decimal(pStockResSolicitud.Cantidad, vCantidadEnteraPres, vCantidadDecimalUMBas)
            ...
        End If
```

Esta es la "decisión madre" del legacy: define en una cascada de if-elseif si:

- Se busca en UMBas (`vBusquedaEnUmBas = True`)
- Se construye una solicitud paralela `pStockResBusquedaParaExplosion` para explosión
- Se rechaza el pedido (`Rechazar_pedido_incompleto = Si`)

> En el motor nuevo, esta decisión está distribuida en `ReservationLoopStep.TryEnableExplosionFallback` y `TryEnableUMBasFallback`, llamados solo después de agotar CASO_1..CASO_4. En el legacy ocurre **antes** de cualquier reserva, lo que cambia la semántica: el legacy puede activar UMBas sin haber probado primero los pallets.

`Stock_Requiere_Explosion(...)` evalúa si la presentación de la solicitud no está disponible directamente en el stock y hay que ir a otra presentación o a UMBas.

---

## 7. Etiqueta `INICIAR_EN_1` (L1273) — Pallets COMPLETOS Clavaud

```vbnet
INICIAR_EN_1:
    If Not ListaEstadosDeProceso.Contains(100) Then

        If pBeConfigEnc.Conservar_Zona_Picking_Clavaud Then

            Restar_Stock_Reservado(lBeStockConPalletsCompletosClavaud,
                                   pBeConfigEnc, lConnection, ltransaction)

            For Each vStockOrigen As clsBeStock In lBeStockConPalletsCompletosClavaud

                BeStockDestino = New clsBeStock()
                clsPublic.CopyObject(vStockOrigen, BeStockDestino)
                vCantidadDispStock = Math.Round(vStockOrigen.Cantidad, 6)

                If (vStockOrigen.Fecha_vence > FechaMinimaVenceStock) Then
                    ListaEstadosDeProceso.Add(100)
                    GoTo ANALIZAR_FECHAS_DE_VENCIMIENTO
                ElseIf (vStockOrigen.Fecha_vence > FechaMinimaVenceStock) Then
                    ListaEstadosDeProceso.Add(100)
                    Exit For
                Else
                    ListaEstadosDeProceso.Add(100)
                End If
                ...
                ' Lookup ubicación, lookup presentación,
                ' verificar Explosion_Automatica + Nivel_Max + ubicación picking,
                ' construir BeStockRes con los 35 campos de stock_res,
                ' añadir a lBeStockAReservar.
                ...
            Next
        End If
    End If
```

Características:

- **Guard de doble entrada**: `If Not ListaEstadosDeProceso.Contains(100)` impide ejecutar dos veces el mismo CASO al volver tras un GoTo.
- **GoTo recursivo a `ANALIZAR_FECHAS_DE_VENCIMIENTO`**: si encuentra stock cuya fecha es **mayor** que la mínima vigente (porque venció el lote actual), recalcula fechas y retorna.
- **Filtros internos** (L1320-L1333): si `Explosion_Automatica`, evalúa `Explosion_Automatica_Nivel_Max` vs nivel de la ubicación, y `Explosion_Automatica_Desde_Ubicacion_Picking`.
- **Construcción manual** del `BeStockRes` con los 35 campos (sin helper común; cada CASO los copia uno a uno).

---

## 8. Etiqueta `INICIAR_EN_2` (L1904) — Pallets INCOMPLETOS Clavaud

```vbnet
INICIAR_EN_2:
    If Not vCantidadCompletada Then

        FechaMinimaVenceStock = Get_Fecha_Vence_Minima_Stock_Reserva_MI3(...)

        ' #EJC202303031732: Condición y parametrización solicitada por Carolina.
        If pTarea_Reabasto Then
            If pBeConfigEnc.considerar_paletizado_en_reabasto Then
                Dim vMensajeNoRellenado As String = "Error_202303031731: La tarea de reabasto para: " _
                    & pStockResSolicitud.Codigo_Producto _
                    & " no se generará porque no hay tarimas completas y la configuración está activa."
                clsLnLog_error_wms.Agregar_Error(...)
                XtraMessageBox.Show(vMensajeNoRellenado, "Reabasto", ...)
                Exit Function
            End If
        End If

        Restar_Stock_Reservado(lBeStockConPalletsInCompletosClavaud, ...)

        For Each vStockOrigen As clsBeStock In lBeStockConPalletsInCompletosClavaud
            ...
            If (vStockOrigen.Fecha_vence >= FechaMinimaVenceStock) _
               AndAlso Not ListaEstadosDeProceso.Contains(101) _
               AndAlso ListaEstadosDeProceso.Contains(100) Then
                ListaEstadosDeProceso.Add(101)
                GoTo ANALIZAR_FECHAS_DE_VENCIMIENTO
            ElseIf (vStockOrigen.Fecha_vence > FechaMinimaVenceStock) Then
                ListaEstadosDeProceso.Add(101)
                Exit For
            Else
                ListaEstadosDeProceso.Add(101)
            End If
            ...
        Next
```

**Tres efectos colaterales no obvios**:

1. Si `pTarea_Reabasto AndAlso considerar_paletizado_en_reabasto` y **no hay tarimas completas**, abre un `XtraMessageBox` de WinForms (sí, en una capa DAL invocada desde un endpoint REST). Este es uno de los bugs históricos: si el backend corre sin sesión gráfica, esta línea **bloquea**. Por eso se construyó el motor nuevo como C# puro sin dependencia UI.
2. Re-llama `Get_Fecha_Vence_Minima_Stock_Reserva_MI3` (la segunda vez en este flujo). Cada llamada hace lecturas a `stock`, `stock_res` y `productos` en Killios.
3. Marca `ListaEstadosDeProceso.Add(101)` para evitar re-entry.

---

## 9. Etiqueta `INICIAR_EN_3` (L2712) — Zona picking + zona NO picking

```vbnet
INICIAR_EN_3:
    If Not vCantidadCompletada Then

        FechaMinimaVenceStock = Get_Fecha_Vence_Minima_Stock_Reserva_MI3(...)

        ' #EJC202308081023: Tomar producto de la zona de picking.
        If lBeStockZonaPicking.Count = 0 Then
            If lBeStockExistente.Count > 0 Then
                lBeStockZonaPicking = lBeStockExistente.Where(
                    Function(x) x.UbicacionPicking = True AndAlso x.Cantidad > 0).ToList()
                Restar_Stock_Reservado(lBeStockZonaPicking, ...)
                If lBeStockZonaPicking.Count > 0 Then
                    lBeStockZonaPicking = lBeStockZonaPicking
                        .Where(Function(x) x.Cantidad > 0)
                        .OrderBy(Function(x) x.Fecha_vence)
                        .ToList()
                End If
            End If
        End If

        ' #EJC202308081023: Tomar producto de las zonas de NO picking.
        lBeStockZonasNoPicking = lBeStockExistente.Where(
            Function(x) x.UbicacionPicking = False AndAlso x.Cantidad > 0).ToList()
        If lBeStockZonasNoPicking.Count > 0 Then
            FechaMinimaVenceStock = lBeStockZonasNoPicking.Min(Function(x) x.Fecha_vence)
        End If
    End If

EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING:
#Region "Reservar stock de zona de picking"
    If Not vCantidadCompletada Then
        ...
        If Not ExcepcionFechaVenceEsInferiorEnZonaPicking AndAlso lBeStockZonaPicking.Count > 0 Then
            ' Loop reservando de zona picking
            ...
        End If
    End If
#End Region
```

Caracteristicas:

- **Re-particiona** las listas de stock por la bandera `UbicacionPicking`. Esta es la tercera vez en el método que se hace este filtro (la primera fue en `Obtener_Listas_De_Stock`).
- **Recalcula** `FechaMinimaVenceStock` por tercera vez tomando el mínimo de la zona NO picking.
- Ejecuta el bloque "Reservar stock de zona de picking" entre L2752-L3201 con un `For Each` similar a INICIAR_EN_1 pero:
  - Aplica explosión por múltiplo si la presentación es 0.
  - Permite descuento decimal (parte entera + parte UMBas).
  - Maneja la excepción `ExcepcionFechaVenceEsInferiorEnZonaPicking`.

Después de este bloque hay otro region "Reserverar stock de zona NO Picking" (L3920-L4645, **con typo en el nombre**), que es la versión "no picking" del mismo loop. Y luego se repite por tercera vez en L4654-L5683 con variantes mínimas. Los tres son **quasi-duplicados**.

---

## 10. Regiones "Explosión por múltiplo" (L3133 y L6204)

Ambas implementan la conversión presentación↔UMBas con `Split_Decimal` y `Math.Ceiling`. Una está dentro del scope de zona picking, la otra dentro de zona no-picking. Las diferencias son **menores**: cambian las variables locales referenciadas (`vCantidadEnStockEnPres` vs `vDisponibleStockEnPres`) y los logs (`vMensajeNoExplosionEnZonasNoPicking`).

Esta duplicación es el síntoma más visible del problema arquitectónico del legacy: el copy-paste de bloques cuando se necesitaba una variante zona-picking vs zona-no-picking.

---

## 11. Recursión interna (L8059-L8132)

Al final del método, antes del `Catch`, hay una recursión **a sí mismo** para forzar el modo UMBas:

```vbnet
If (pBeConfigEnc.Explosion_Automatica) AndAlso (vCantidadSolicitadaPedido > 0) _
   AndAlso lBeStockExistente.Count > 0 Then

    Dim BeStockResUMBas As New clsBeStock_res
    BeStockResUMBas = pStockResSolicitud.Clone()

    If BeStockResUMBas.No_bulto = 0 Then

        BeStockResUMBas.Cantidad = vCantidadSolicitadaPedido
        BeStockResUMBas.IdPresentacion = 0
        BeStockResUMBas.Serial = No_Linea
        BeStockResUMBas.No_bulto = 1965 ' Identificación para solicitud recursiva.
        ...

        Dim vCantDisRef As Double = 0

        If Inserta_Stock_Reservado(lBeStockAReservar, lConnection, ltransaction) Then

            If Not Reserva_Stock_From_MI3(BeStockResUMBas, DiasVencimiento, MaquinaQueSolicita,
                                           pBeConfigEnc, vCantDisRef, pIdPropietarioBodega,
                                           vlBeStockAReservarUMBas, lConnection, ltransaction,
                                           No_Linea, False, pBeTrasladoDet) Then
                ' Si la recursión también falla...
                pCantidadDisponibleStock = vCantidadDispStock
                vMensajeNoExplosionEnZonasNoPicking = String.Format(
                    "{0} Código:{1} UMBas={2} Pres='Sin pres' Cant={3} ...",
                    clsDalEx.ErrorS0004, BeProducto.Codigo, ...)
                pBeTrasladoDet.Process_Result += vMensajeNoExplosionEnZonasNoPicking
                pBeTrasladoDet.Qty_to_Receive = vCantidadPendiente
                clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet, ...)

                If pBeConfigEnc.Rechazar_pedido_incompleto = tRechazarPedidoIncompleto.Si Then
                    Throw New Exception(vMensajeNoExplosionEnZonasNoPicking)
                Else
                    Exit Function
                End If
            End If
        End If
        ...
    End If
End If
```

Detalles críticos:

- **Marcador anti-recursión**: `BeStockResUMBas.No_bulto = 1965`. La condición `If BeStockResUMBas.No_bulto = 0` impide que la recursión se llame a sí misma indefinidamente; solo entra al ciclo si la solicitud "fresca" no viene marcada.
- **`Inserta_Stock_Reservado(lBeStockAReservar)` se ejecuta antes** de la recursión: las reservas parciales hechas en presentación normal **se persisten primero** en `stock_res`, y la recursión solo busca el remanente en UMBas. Si la recursión falla, esas reservas parciales **ya quedaron** en BD.
- **Comparable al motor nuevo**: el equivalente conceptual es `TryEnableExplosionFallback` + `TryEnableUMBasFallback`, pero allá no se llama recursivamente a `Reserva_Stock_From_MI3`. En el motor nuevo el loop externo simplemente cambia el modo y reconstruye la cadena de handlers.

---

## 12. Métodos auxiliares (verificados en extracts)

### 12.1 `Procesar_Y_Restar_Stock_Reservado` (59 L)
Ya documentado en §4. Hace `Restar_Stock_Reservado` + filter `Cantidad > 0` + carga presentación + llamada a `Procesar_Logica_Presentacion_Stock`.

### 12.2 `Procesar_Logica_Presentacion_Stock` (43 L)
Computa por refs:

```vbnet
vCantidadProductoPorTarima = Math.Round(BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima, 2)
vCantidadTarimasCompletasAPickearClavaud = Math.Round(pStockResSolicitud.Cantidad / vCantidadProductoPorTarima, 2)
Split_Decimal(vCantidadTarimasCompletasAPickearClavaud, wholePart, fractionalPart)
vCantidadEnteraTarimasCompletasClavaud = Convert.ToInt32(wholePart)
vCantidadDecimalTarimasCompletasClavaud = fractionalPart
```

Es la única lógica que define qué constituye un "pallet completo" en términos de cantidad: `CajasPorCama * CamasPorTarima`. El motor nuevo conserva esta semántica vía `clsBeStock.Pallet_Completo` (bool persistido en BD), no recalculado en runtime.

### 12.3 `Tiene_Cantidad_Suficiente` (24 L)
Helper trivial: `True` si **al menos un** stock de la lista tiene `Cantidad >= Solicitado`. No suma ni considera fechas. Se usa como guard en algunos caminos.

### 12.4 `Get_Fecha_Vence_Minima_Stock_Reserva_MI3`
No documentado íntegro en extract pero invocado 3+ veces. Devuelve la `Date` mínima de vencimiento aplicable según `DiasVencimiento` del producto y consideración de zona picking vs ALM (vía refs `vFechaMinimaVenceZonaPicking`, `vFechaMinimaVenceZonaALM`).

### 12.5 `Stock_Requiere_Explosion`
Decide si la solicitud necesita explosión: típicamente cuando la presentación pedida no aparece en ningún `clsBeStock.IdPresentacion` de las listas y existe stock en otra presentación o en UMBas.

### 12.6 `Split_Decimal`
Separa un `Double` en parte entera y parte decimal. Usado decenas de veces en el método para distinguir cantidades enteras (presentaciones) vs fraccionarias (que terminan como UMBas).

### 12.7 `Restar_Stock_Reservado`
Llama a `clsLnStock_res.Get_Reservas_Pendientes_Por_IdStock(...)` y descuenta. No persiste; solo muta la lista in-memory.

---

## 13. Banderas de control y patología del flujo

### 13.1 `ListaEstadosDeProceso As List(Of Integer)`

Es el **estado finito** del método legacy, representado como un set de enteros. Valores observados:

| Valor | Significado |
|-------|-------------|
| `100` | INICIAR_EN_1 ya ejecutó (CompletePackages) |
| `101` | INICIAR_EN_2 ya ejecutó (IncompletePackages) |
| `103-107` | Diversos sub-estados de zona picking/no-picking (no exhaustivo en extract) |

El patrón **`If Not ListaEstadosDeProceso.Contains(N)`** se repite ~15 veces como guard de re-entry. Su rol es **emular un control flow estructurado** sobre un cuerpo de método con GoTo.

### 13.2 `vCantidadCompletada As Boolean`

Equivale conceptualmente a `IsQuantityFullyReserved()` del motor nuevo, pero con dos diferencias críticas:

- Solo lo settean los caminos que **explícitamente** completaron la cantidad (no es derivado de `vCantidadPendiente == 0`).
- Una vez `True`, todos los `If Not vCantidadCompletada` posteriores cortan ejecución.

### 13.3 `vBusquedaEnUmBas As Boolean`

Activa el modo de búsqueda en UMBas. Una vez `True`, modifica el comportamiento de `Stock_Requiere_Explosion`, de `lStock` (carga listas distintas) y del loop de reserva. **No se puede revertir** dentro del mismo invocación.

### 13.4 `Iniciar_En As Integer`

Pivote del switch:

```vbnet
Select Case Iniciar_En
    Case 1: GoTo INICIAR_EN_1
    Case 2: GoTo INICIAR_EN_2
    Case 3: GoTo INICIAR_EN_3
    Case 4: GoTo INICIAR_EN_4   ' (no presente como label en este extract; en otra rama)
    Case Else: GoTo EJC_202308081248_RESERVAR_DESDE_ULTIMA_LISTA
End Select
```

> Conceptualmente equivale al parámetro `startingPoint` que `ServiceFactory.BuildHandlerChain` recibe en el motor nuevo. Las dos arquitecturas comparten el mismo **vocabulario de decisión**, solo cambia el mecanismo: GoTo vs factory de cadena.

---

## 14. Errores conocidos y trampas históricas

### 14.1 Strings de error con códigos

Aparecen en el método como `Throw New Exception("ERROR_YYYYMMDDhhmm[A-Z]: ...")` o `"Error_YYYYMMDDhhmm[A-Z]: ..."`. Los hallados en extract:

| Código                  | Línea | Descripción |
|-------------------------|-------|-------------|
| `ERROR_202302061300E`   | L1315 | Cantidad disponible negativa en INICIAR_EN_1 |
| `ERROR_202302061300F`   | L1979 | Cantidad disponible negativa en INICIAR_EN_2 |
| `Error_202303031731`    | L1924 | Reabasto sin tarimas completas + config restrictiva |
| `Error_202212140140D`   | L284  | Pedido incompleto + Rechazar_pedido_incompleto = Si |
| `ERROR_202302021127`    | L280  | Comentado: explosión sin presentación default (no se lanza) |

Estos códigos sobreviven en `log_error_wms` y son la única manera de rastrear **históricamente** problemas que ocurrieron antes de la reescritura. Documentados acá para que el wms-brain pueda mapear un código → rama del flujo.

### 14.2 `XtraMessageBox.Show` desde DAL (L1929)

Como se mencionó en §8, este método invoca un MessageBox de WinForms. Si BOF corre como servicio, el thread que lo llama **se bloquea esperando una sesión interactiva que no existe**. Workaround histórico: mantener BOF abierto como aplicación de escritorio. Bug fundamental resuelto en motor nuevo (sin UI calls).

### 14.3 Mutación de `pStockResSolicitud`

En varios puntos (L272, L8119), el método **muta el parámetro `ByRef`** original:

```vbnet
pStockResSolicitud.IdPresentacion = 0
pStockResSolicitud = BeStockRes
```

Esto significa que el `clsBeStock_res` que el caller pasó **se altera** después de la invocación. Cualquier caller que lo reutilice asume comportamiento sutil. Documentar esta mutación es importante porque el motor nuevo **garantiza inmutabilidad del request** (vía `ReservationContext.Request` que es read-only para handlers).

### 14.4 Uso de `Debug.Print` con Códigos hardcoded

L122-L123 y L2756-L2757 contienen:

```vbnet
If pStockResSolicitud.IdProductoBodega = 616 Then Debug.Print("Aqui " & DiasVencimiento)
If BeProducto.Codigo = "00190454" Then Debug.Print("Aqui")
```

Son breakpoints históricos de debugging hardcoded en el código de producción. No tienen efecto funcional pero indican qué SKUs/bodegas dieron problemas durante el desarrollo.

### 14.5 `lBeStockExistente` como variable polimórfica

A lo largo del método, `lBeStockExistente` cambia su semántica:

- **Tras setup**: representa "todo el stock disponible".
- **Tras §5 (line L236)**: puede ser reemplazado por `lBeStockExistenteZonasNoPicking` si la zona ALM tiene fecha más temprana.
- **Tras §6 (line L256)**: puede ser reemplazado nuevamente si la lista global está vacía y hay no-picking.
- **Dentro de §9**: se usa como pool del que se filtran zonas picking y no-picking.

Esta **variable polimórfica** es la mayor fuente de bugs históricos del legacy y la razón principal por la que el motor nuevo introduce listas explícitamente nombradas (`StockListPickingZone`, `StockListNonPickingZones`, `WorkingStockList`) sin reemplazos.

---

> Próximo: `03-comparison.md` documenta la matriz de paridad funcional motor nuevo vs motor legacy, qué patrones del legacy mantenidos, qué patrones eliminados y por qué.
