Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports System.Security.Cryptography
Imports DevExpress.Compatibility
Imports DevExpress.Data.Helpers
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.TextEditController
Imports DevExpress.XtraPrinting.Native
Imports Newtonsoft
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TOMWMS
Public Class clsLnTrans_pe_encDMS

    Private Shared listaIdsEnviados As New List(Of Integer)

    Public Shared Async Sub Exportacion_PedidosAsync(ByVal lblprg As RichTextBox)
        Dim api As New ApiService()
        Dim reloj As New Stopwatch()
        Dim listPE As New List(Of clsBeTrans_pe_enc)
        Dim Contador As Integer = 0
        Dim RegistrosEncontrados As Integer = 0
        Dim pTablaSincronizada As String = ""
        Dim resultado As String = ""
        Dim pRegistrosFallidos As Integer = 0
        Dim pRegistrosExitosos As Integer = 0
        Dim vTotalRegistrosEncontrados As Integer = 0
        Dim listaPedidosPendientes As List(Of Integer) = Nothing
        Try

            reloj.Start()
            pTablaSincronizada = clsHelper.ObtenerNombreTabla("ExportarSalidas")
            clsHelper.LogMensaje(lblprg, "Iniciando carga de pedidos...", clsHelper.TipoMensaje.Info)

            '#GT18062025: obtener los ingresos no sincronizados
            listPE = GetAll_By_CDC(pTablaSincronizada, listPE)

            If listaPedidosPendientes.Count > 0 Then
                listPE = New List(Of clsBeTrans_pe_enc)()
                vTotalRegistrosEncontrados = listaPedidosPendientes.Count
                clsHelper.LogMensaje(lblprg,
                                  String.Format("Se encontraron {0} pedido(s) pendientes de sincronizar.", vTotalRegistrosEncontrados),
                                  clsHelper.TipoMensaje.Info)

                listPE = GetAll_By_CDC_Pendientes(pTablaSincronizada, listPE, listaPedidosPendientes)
                If listPE IsNot Nothing AndAlso listPE.Count > 0 Then
                    RegistrosEncontrados = listPE.Count
                    clsHelper.LogMensaje(lblprg, "Pedidos encontrados " & listPE.Count, clsHelper.TipoMensaje.Exito)
                Else
                    clsHelper.LogMensaje(lblprg, "Pedidos no encontrados ", clsHelper.TipoMensaje.Error_)
                    Exit Sub
                End If

                '#Iteramos por pedido y enviamos a la nube para no hacer un proceso único pesado
                For Each pPeEnc In listPE
                    Contador += 1
                    Dim enviado As Boolean = False
                    Dim intento As Integer = 0
                    Const maxIntentos As Integer = 3

                    clsHelper.LogMensaje(lblprg, "Iterando Registro: " & Contador & "/" & RegistrosEncontrados, clsHelper.TipoMensaje.Info)
                    Dim JsonOC = Crear_Json(lblprg, pPeEnc)

                    If String.IsNullOrEmpty(JsonOC) Then
                        pRegistrosFallidos += 1
                        resultado = "No se generó el archivo json correspondiente."
                        Guadar_Envio_Rechazado(pPeEnc.IdPedidoEnc, resultado)
                        Continue For
                    Else
                        While Not enviado And intento <= maxIntentos
                            resultado = Await api.EnviarJsonPEAsync(JsonPE, lblprg)

                            resultado = Await api.EnviarJsonOCAsync(JsonOC, lblprg)

                            If resultado = "Ok" Then
                                enviado = True
                                pRegistrosExitosos += 1
                                '#GT marcar como enviado MI3 en oc_enc
                            Else
                                intento += 1
                                clsHelper.LogMensaje(lblprg, "Reintento de envio: " & intento, clsHelper.TipoMensaje.Info)
                                Await Task.Delay(2000) ' Esperar 2 segundos entre intentos
                            End If

                        End While

                        ' Si el registro no fue enviado, se guarda el fallo
                        If Not enviado Then
                            pRegistrosFallidos += 1
                            Guadar_Envio_Rechazado(pPeEnc.IdPedidoEnc, resultado)
                        End If

                    End If

                Next

                reloj.Stop()

                Dim pRespuesta As String = ""
                If pRegistrosFallidos > 0 AndAlso pRegistrosExitosos > 0 Then
                    pRespuesta = $"Parcial:  no se sincronizaron {pRegistrosFallidos} registros. Total enviados: {RegistrosEncontrados}"
                ElseIf pRegistrosFallidos > 0 Then
                    pRespuesta = $"Error al sincronizar todos los registros ({pRegistrosFallidos})"
                Else
                    pRespuesta = "Ok"
                End If

                Dim mensajeFinal As String = $"Sincronización finalizada. Tiempo total: {reloj.Elapsed.TotalSeconds} segundos."
                clsHelper.LogMensaje(lblprg, mensajeFinal, clsHelper.TipoMensaje.Exito)

                clsHelper.Registrar_Log(pRespuesta, pTablaSincronizada, CInt(reloj.Elapsed.TotalSeconds))

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsHelper.Registrar_Log("Error " & vMsgError, pTablaSincronizada)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            reloj.Stop()

        End Try
    End Sub

    Public Shared Function GetAll_By_CDC(ByVal pTablaSincronizada As String, ByRef pListPE As List(Of clsBeTrans_pe_enc)) As List(Of clsBeTrans_pe_enc)
        Dim BeLogUltimaSincronizacion As New clsBeLog_sincronizacion_nube()
        Dim clsTransaccion As New clsTransaccion()
        Dim listPropietarios As New List(Of clsBePropietarios)()

        Try

            clsTransaccion.Begin_Transaction()

            BeLogUltimaSincronizacion = New clsBeLog_sincronizacion_nube()
            BeLogUltimaSincronizacion = clsLnLog_sincronizacion_nube.GetLastSync(pTablaSincronizada, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            If BeLogUltimaSincronizacion IsNot Nothing Then
                pListPE = clsLnTrans_pe_enc.GetAll_By_CDC(BeLogUltimaSincronizacion.Fecha_sincronizacion, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            End If

            End If

            End If

            Next

            End If


            'If listaPropietarioBodega IsNot Nothing AndAlso listaPropietarioBodega.Count > 0 Then

            '    For Each pIdPropietarioBodega In listaPropietarioBodega
            '        BeLogUltimaSincronizacion = New clsBeDMS_Log_sincronizacion_nube()
            '        BeLogUltimaSincronizacion = clsLnDMS_Log_sincronizacion_nube.GetLastSync(pTablaSincronizada, pIdPropietarioBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            '        If BeLogUltimaSincronizacion IsNot Nothing Then
            '            Dim lista = clsLnTrans_pe_enc.GetAll_By_CDC(BeLogUltimaSincronizacion.Fecha_sincronizacion, pIdPropietarioBodega, listaPedidosPendientes, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            '            pListPE.AddRange(lista)

            '        End If

            '    Next

            'End If

            clsTransaccion.Commit_Transaction()

            Return pListPE

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Shared Sub Guadar_Envio_Rechazado(ByVal pIdPedidoEnc As Integer, ByVal pMensaje As String)
        Dim BeLogSyncError As New clsBeLog_sincronizacion_fallos()
        Try
            BeLogSyncError = New clsBeLog_sincronizacion_fallos()
            BeLogSyncError.IdLogFallo = clsLnLog_sincronizacion_fallos.MaxID() + 1
            BeLogSyncError.IdOrdenCompraEnc = 0
            BeLogSyncError.IdPedidoEnc = pIdPedidoEnc
            BeLogSyncError.Estado = "Error"
            BeLogSyncError.Mensaje_error = pMensaje
            BeLogSyncError.Fec_agr = Now
            BeLogSyncError.IdPedidoEnc = 0

            clsLnLog_sincronizacion_fallos.Insertar(BeLogSyncError)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
        End If
        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))

        Finally
        ' Cierre solo si es local
        If localConnection AndAlso lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
            lConnection.Close()
        End If
        End Try
    End Sub


    Public Shared Sub Actualizar_Envio_Rechazado(ByVal pPedidoEnc As clsBeTrans_pe_enc)
        Dim BeLogSyncError As New clsBeDMS_Log_sincronizacion_fallos()
        Dim clsTransaccion As New clsTransaccion()
        Try
            clsTransaccion.Begin_Transaction()

            If clsLnDMS_Log_sincronizacion_fallos.Existe_by_Pedido(pPedidoEnc.IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                BeLogSyncError = New clsBeDMS_Log_sincronizacion_fallos()
                BeLogSyncError.IdOrdenCompraEnc = 0
                BeLogSyncError.IdPedidoEnc = pPedidoEnc.IdPedidoEnc
                BeLogSyncError.IdPropietario = pPedidoEnc.PropietarioBodega.IdPropietario
                BeLogSyncError.IdProducto = 0
                BeLogSyncError.Estado = "Ok"
                BeLogSyncError.Fec_mod = Now

                clsLnDMS_Log_sincronizacion_fallos.Actualizar_Registro(BeLogSyncError, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            End If
            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try
    End Sub

    Public Shared Function Crear_Json(ByRef lblprg As RichTextBox, ByVal pBePeEnc As clsBeTrans_pe_enc) As String
        Crear_Json = ""
        Dim listPayload As New List(Of Object)
        Dim clsTransaccion As New clsTransaccion()
        Dim BeLogUltimaSincronizacion As New clsBeLog_sincronizacion_nube()
        Dim listaPayload As New List(Of Object)()
        Dim peEncList As New List(Of Object)()
        Dim pePolizaList As New List(Of Object)()
        Dim pePicking As New Object()
        Dim peMuelle As New Object()
        Dim pPickingOperadoresList As New List(Of Object)
        Dim operadorBodegaList As New List(Of Object)
        Dim operadorList As New List(Of Object)
        Dim pPickingImgList As New List(Of Object)
        Dim ListPickingOp As New List(Of clsBeTrans_picking_op)()
        Dim pPickingPrioridad As New clsBeTrans_picking_prioridad()
        Dim pListPE As New List(Of clsBeTrans_pe_enc)()
        Dim clienteList As New List(Of Object)
        Dim ListClientes As New List(Of clsBeCliente)()
        Dim resultado As String = ""
        Try


            clsTransaccion.Begin_Transaction()

            clsHelper.LogMensaje(lblprg, "Procesando pedido: " & pBePeEnc.IdPedidoEnc, clsHelper.TipoMensaje.Info)

            '#GT23052025: limpiar los objetos para no duplicar registros
            peEncList = New List(Of Object)()
            pPickingOperadoresList = New List(Of Object)
            operadorBodegaList = New List(Of Object)
            operadorList = New List(Of Object)
            pPickingImgList = New List(Of Object)
            peMuelle = New Object()
            clienteList = New List(Of Object)
            ListClientes = New List(Of clsBeCliente)()

            If pBePeEnc.TipoPedido.Control_Poliza Then
                pBePeEnc.ObjPoliza = clsLnTrans_pe_pol.GetSingleId(pBePeEnc.IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            End If

            If pBePeEnc.Detalle Is Nothing OrElse pBePeEnc.Detalle.Count = 0 Then
                'Return peEncList
                clsHelper.LogMensaje(lblprg, "El pedido no tiene detalle.", clsHelper.TipoMensaje.Error_)
                Return ""
            End If

            If pBePeEnc.Picking.ListaPickingUbic Is Nothing Then
                'Throw New Exception("El pedido no tiene detalle del picking_ubic.")
                clsHelper.LogMensaje(lblprg, "El pedido no tiene detalle ascoaido a picking_ubic.", clsHelper.TipoMensaje.Error_)
                Return ""
            End If

            '#GT21052025: cargar lista de pickingUbicStock
            Dim ListPickingUbicStock As New List(Of clsBeTrans_picking_ubic_stock)()
            For Each pickingUbic In pBePeEnc.Picking.ListaPickingUbic
                Dim pPickingUbicSTock As New clsBeTrans_picking_ubic_stock()
                pPickingUbicSTock = clsLnTrans_picking_ubic_stock.Get_Single_By_IdPickingUbic_And_IdStock(pBePeEnc.Picking.IdPickingEnc,
                                                                                                                      pickingUbic.IdPickingUbic,
                                                                                                                      pickingUbic.IdStock,
                                                                                                                      clsTransaccion.lConnection,
                                                                                                                      clsTransaccion.lTransaction)
                If pPickingUbicSTock IsNot Nothing Then
                    ListPickingUbicStock.Add(pPickingUbicSTock)
                End If
            Next

            '#GT21052025: cargar lista operadores picking, sino tiene, cargar objeto por defecto
            ListPickingOp = clsLnTrans_picking_op.Get_All_By_IdPickingEnc(pBePeEnc.IdPickingEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            If ListPickingOp IsNot Nothing AndAlso ListPickingOp.Count > 0 Then

                For Each OperadorPicking In ListPickingOp
                    pPickingOperadoresList.Add(New With {
                                                                    .IdOperadorPicking = OperadorPicking.IdOperadorPicking,
                                                                    .IdPickingEnc = OperadorPicking.IdPickingEnc,
                                                                    .IdOperadorBodega = OperadorPicking.IdOperadorBodega,
                                                                    .user_agr = OperadorPicking.User_agr,
                                                                    .fec_agr = OperadorPicking.Fec_agr,
                                                                    .user_mod = OperadorPicking.User_mod,
                                                                    .fec_mod = OperadorPicking.Fec_mod
                                                           })
                Next
            Else
                pPickingOperadoresList.Add(New With {
                                                                                      .IdOperadorPicking = 0,
                                                                                      .IdPickingEnc = 0,
                                                                                      .IdOperadorBodega = 0,
                                                                                      .user_Agr = "",
                                                                                      .fec_agr = New Date(1900, 1, 1),
                                                                                      .user_mod = "",
                                                                                      .fec_mod = New Date(1900, 1, 1)
                                                  })
            End If

            '#GT23052025: cargar lista imagenes del picking
            Dim ListPicking_img As New List(Of clsBeTrans_picking_img)()
            ListPicking_img = clsLnTrans_Picking_Img.Get_All_Imagen_By_IdPedidoEnc(pBePeEnc.IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            If ListPicking_img IsNot Nothing AndAlso ListPicking_img.Count > 0 Then

                For Each pPickingImg In ListPicking_img
                    pPickingImgList.Add(New With {
                                                            .IdImagen = pPickingImg.IdImagen,
                                                            .IdPickingEnc = pPickingImg.IdPickingEnc,
                                                            .IdPickingDet = pPickingImg.IdPickingDet,
                                                            .IdPedidoEnc = pPickingImg.IdPedidoEnc,
                                                            .IdPedidoDet = pPickingImg.IdPedidoDet,
                                                            .Imagen = pPickingImg.Imagen,
                                                            .user_agr = pPickingImg.User_agr,
                                                            .fec_agr = pPickingImg.Fec_agr,
                                                            .observacion = pPickingImg.Observacion
                                                            })
                Next

            Else
                pPickingImgList.Add(New With {
                                                        .IdImagen = 0,
                                                        .IdPickingEnc = 0,
                                                        .IdPickingDet = 0,
                                                        .IdPedidoEnc = 0,
                                                        .IdPedidoDet = 0,
                                                        .Imagen = Nothing,
                                                        .user_agr = "",
                                                        .fec_agr = New Date(1900, 1, 1),
                                                        .observacion = ""
                                                        })

            End If

            '#GT23052025: cargar prioridad del picking
            pPickingPrioridad = clsLnTrans_picking_prioridad.GetSingle_By_IdPickingPrioridad(pBePeEnc.Picking.IdPrioridadPicking, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If pPickingPrioridad Is Nothing Then
                pPickingPrioridad = New clsBeTrans_picking_prioridad With {
                                                                                .IdPrioridadPicking = 0,
                                                                                .Codigo = 0,
                                                                                .Nombre = "",
                                                                                .User_agr = "",
                                                                                .Fec_agr = New Date(1900, 1, 1),
                                                                                .User_mod = "",
                                                                                .Fec_mod = New Date(1900, 1, 1),
                                                                                .Activo = False
                                                                                }
            End If

            If pBePeEnc.IdMuelle > 0 Then
                Dim pBodegaMuelle As New clsBeBodega_muelles()
                pBodegaMuelle.IdMuelle = pBePeEnc.IdMuelle
                clsLnBodega_muelles.GetSingle(pBodegaMuelle, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                If pBodegaMuelle IsNot Nothing Then
                    peMuelle = New With {
                                                .IdMuelle = pBodegaMuelle.IdMuelle,
                                                .IdBodega = pBodegaMuelle.IdBodega,
                                                .codigo_barra = pBodegaMuelle.Codigo_barra,
                                                .nombre = pBodegaMuelle.Nombre,
                                                .user_agr = pBodegaMuelle.User_agr,
                                                .fec_agr = pBodegaMuelle.Fec_agr,
                                                .user_mod = pBodegaMuelle.User_mod,
                                                .fec_mod = pBodegaMuelle.Fec_mod,
                                                .Color = pBodegaMuelle.Color,
                                                .imagen = pBodegaMuelle.Imagen,
                                                .activo = pBodegaMuelle.Activo,
                                                .Entrada = pBodegaMuelle.Entrada,
                                                .Salida = pBodegaMuelle.Salida,
                                                .IdUbicacionDefecto = pBodegaMuelle.IdUbicacionDefecto
                                                }
                Else
                    'Throw New Exception("El muelle asignado no es valido.")
                    clsHelper.LogMensaje(lblprg, "El muelle asignado no es valido.", clsHelper.TipoMensaje.Error_)
                    Return ""
                End If
            Else
                peMuelle = New With {
                                                .IdMuelle = 0,
                                                .IdBodega = 0,
                                                .codigo_barra = "",
                                                .nombre = "",
                                                .user_agr = "",
                                                .fec_agr = New Date(1900, 1, 1),
                                                .user_mod = "",
                                                .fec_mod = New Date(1900, 1, 1),
                                                .Color = 0,
                                                .imagen = Nothing,
                                                .activo = False,
                                                .Entrada = False,
                                                .Salida = False,
                                                .IdUbicacionDefecto = 0
                                                }
            End If


            '#GT09052025: llenar el Pedido y añadir los objetos de detalle
            peEncList.Add(New With {
                                                 .tipopedido = pBePeEnc.TipoPedido,
                                                 .encabezado = New With {
                                                                         .IdPedidoEnc = pBePeEnc.IdPedidoEnc,
                                                                         .IdBodega = pBePeEnc.IdBodega,
                                                                         .IdCliente = pBePeEnc.IdCliente,
                                                                         .IdMuelle = pBePeEnc.IdMuelle,
                                                                         .IdPropietarioBodega = pBePeEnc.IdPropietarioBodega,
                                                                         .IdTipoPedido = pBePeEnc.IdTipoPedido,
                                                                         .IdPickingEnc = pBePeEnc.IdPickingEnc,
                                                                         .Fecha_Pedido = pBePeEnc.Fecha_Pedido,
                                                                         .hora_ini = pBePeEnc.Hora_ini,
                                                                         .hora_fin = pBePeEnc.Hora_fin,
                                                                         .ubicacion = pBePeEnc.Ubicacion,
                                                                         .estado = pBePeEnc.Estado,
                                                                         .no_despacho = pBePeEnc.No_despacho,
                                                                         .activo = pBePeEnc.Activo,
                                                                         .user_agr = pBePeEnc.User_agr,
                                                                         .fec_agr = pBePeEnc.Fec_agr,
                                                                         .user_mod = pBePeEnc.User_mod,
                                                                         .fec_mod = pBePeEnc.Fec_mod,
                                                                         .no_documento = pBePeEnc.No_documento,
                                                                         .local = pBePeEnc.Local,
                                                                         .pallet_primero = pBePeEnc.Pallet_primero,
                                                                         .dias_cliente = pBePeEnc.Dias_cliente,
                                                                         .anulado = pBePeEnc.Anulado,
                                                                         .RoadKilometraje = pBePeEnc.RoadKilometraje,
                                                                         .RoadFechaEntr = pBePeEnc.RoadFechaEntr,
                                                                         .RoadDirEntrega = pBePeEnc.RoadDirEntrega,
                                                                         .RoadTotal = pBePeEnc.RoadTotal,
                                                                         .RoadDesMonto = pBePeEnc.RoadDesMonto,
                                                                         .RoadImpMonto = pBePeEnc.RoadImpMonto,
                                                                         .RoadPeso = pBePeEnc.RoadPeso,
                                                                         .RoadBandera = pBePeEnc.RoadBandera,
                                                                         .RoadStatCom = pBePeEnc.RoadStatCom,
                                                                         .RoadCalcoBJ = pBePeEnc.RoadCalcoBJ,
                                                                         .RoadImpres = pBePeEnc.RoadImpres,
                                                                         .RoadADD1 = pBePeEnc.RoadADD1,
                                                                         .RoadADD2 = pBePeEnc.RoadADD2,
                                                                         .RoadADD3 = pBePeEnc.RoadADD3,
                                                                         .RoadStatProc = pBePeEnc.RoadStatProc,
                                                                         .RoadRechazado = pBePeEnc.RoadRechazado,
                                                                         .RoadRazon_Rechazado = pBePeEnc.RoadRazon_Rechazado,
                                                                         .RoadInformado = pBePeEnc.RoadInformado,
                                                                         .RoadSucursal = pBePeEnc.RoadSucursal,
                                                                         .RoadIdDespacho = pBePeEnc.RoadIdDespacho,
                                                                         .RoadIdFacturacion = pBePeEnc.RoadIdFacturacion,
                                                                         .RoadIdRuta = pBePeEnc.RoadIdRuta,
                                                                         .RoadIdVendedor = pBePeEnc.RoadIdVendedor,
                                                                         .RoadIdRutaDespacho = pBePeEnc.RoadIdRutaDespacho,
                                                                         .RoadIdVendedorDespacho = pBePeEnc.RoadIdVendedorDespacho,
                                                                         .Observacion = pBePeEnc.Observacion,
                                                                         .PedidoRoad = pBePeEnc.PedidoRoad,
                                                                         .HoraEntregaDesde = pBePeEnc.HoraEntregaDesde,
                                                                         .HoraEntregaHasta = pBePeEnc.HoraEntregaHasta,
                                                                         .referencia = pBePeEnc.Referencia,
                                                                         .IdMotivoAnulacionBodega = pBePeEnc.IdMotivoAnulacionBodega,
                                                                         .Enviado_A_ERP = pBePeEnc.Enviado_A_ERP,
                                                                         .control_ultimo_lote = pBePeEnc.Control_Ultimo_Lote,
                                                                         .serie = pBePeEnc.Serie,
                                                                         .correlativo = pBePeEnc.Correlativo,
                                                                         .Referencia_Documento_Ingreso_Bodega_Destino = pBePeEnc.Referencia_Documento_Ingreso_Bodega_Destino,
                                                                         .sync_mi3 = pBePeEnc.Sync_MI3,
                                                                         .No_Picking_ERP = pBePeEnc.No_Picking_ERP,
                                                                         .no_documento_externo = pBePeEnc.No_Documento_Externo,
                                                                         .requiere_tarimas = pBePeEnc.Requiere_Tarimas,
                                                                         .fecha_preparacion = pBePeEnc.Fecha_Preparacion,
                                                                         .IdTipoManufactura = pBePeEnc.IdTipoManufactura,
                                                                         .bodega_origen = pBePeEnc.Bodega_Origen,
                                                                         .bodega_destino = pBePeEnc.Bodega_Destino,
                                                                         .idacuerdocomercial = pBePeEnc.IdAcuerdoComercial,
                                                                         .IdMotivoDevolucion = pBePeEnc.IdMotivoDevolucion,
                                                                         .Codigo_Empresa_ERP = pBePeEnc.Codigo_Empresa_ERP
                                                                        },
                                                 .detalle = pBePeEnc.Detalle,
                                                 .poliza = If(pBePeEnc.TipoPedido.Control_Poliza,
                                                              New List(Of clsBeTrans_pe_pol) From {pBePeEnc.ObjPoliza},
                                                              New List(Of clsBeTrans_pe_pol) From {New clsBeTrans_pe_pol With {
            .IdOrdenPedidoPol = 0,
                                                                                                                            .IdOrdenPedidoEnc = 0,
                                                                                                                            .Bl_No = "",
                                                                                                                            .NoPoliza = "",
                                                                                                                            .Pto_Descarga = "",
                                                                                                                            .Viaje_no = "",
                                                                                                                            .Buque_no = "",
                                                                                                                            .Remitente = "",
                                                                                                                            .Fecha_abordaje = New Date(1900, 1, 1),
                                                                                                                            .Destino = "",
                                                                                                                            .Dir_destino = "",
                                                                                                                            .Descripcion = "",
                                                                                                                            .Po_number = "",
                                                                                                                            .Cantidad = 0,
                                                                                                                            .Piezas = 0,
                                                                                                                            .Total_kgs = 0D,
                                                                                                                            .Cbm = 0D,
                                                                                                                            .Dua = "",
                                                                                                                            .Fecha_poliza = New Date(1900, 1, 1),
                                                                                                                            .Pais_procede = "",
                                                                                                                            .Tipo_cambio = 0D,
                                                                                                                            .Total_valoraduana = 0D,
                                                                                                                            .Total_lineas = 0,
                                                                                                                            .Total_bultos = 0,
                                                                                                                            .Total_bultos_Peso = 0D,
                                                                                                                            .Total_usd = 0D,
                                                                                                                            .Total_flete = 0D,
                                                                                                                            .Total_seguro = 0D,
                                                                                                                            .User_agr = "",
                                                                                                                            .Fec_agr = New Date(1900, 1, 1),
                                                                                                                            .User_mod = "",
                                                                                                                            .Fec_mod = New Date(1900, 1, 1),
                                                                                                                            .clave_aduana = "",
                                                                                                                            .nit_imp_exp = "",
                                                                                                                            .clase = "",
                                                                                                                            .mod_transporte = "",
                                                                                                                            .total_liquidar = 0D,
                                                                                                                            .total_general = 0D,
                                                                                                                            .codigo_poliza = "",
                                                                                                                            .ticket = "",
                                                                                                                            .numero_orden = "",
                                                                                                                            .fecha_aceptacion = New Date(1900, 1, 1),
                                                                                                                            .fecha_llegada = New Date(1900, 1, 1),
                                                                                                                            .total_otros = 0D,
                                                                                                                            .IdRegimen = 0,
                                                                                                                            .Total_bultos_Peso_Neto = 0D,
                                                                                                                            .activo = False
                                                                                                                            }}),
                                                 .picking = New With {
                                                                        .encabezado = New With {
                                                                                                .idPickingEnc = pBePeEnc.Picking.IdPickingEnc,
                                                                                                .idBodega = pBePeEnc.Picking.IdBodega,
                                                                                                .idPropietarioBodega = pBePeEnc.Picking.IdPropietarioBodega,
                                                                                                .idUbicacionPicking = pBePeEnc.Picking.IdUbicacionPicking,
                                                                                                .fecha_picking = pBePeEnc.Picking.Fecha_picking,
                                                                                                .hora_ini = pBePeEnc.Picking.Hora_ini,
                                                                                                .hora_fin = pBePeEnc.Picking.Hora_fin,
                                                                                                .estado = pBePeEnc.Picking.Estado,
                                                                                                .user_agr = pBePeEnc.Picking.User_agr,
                                                                                                .fec_agr = pBePeEnc.Picking.Fec_agr,
                                                                                                .user_mod = pBePeEnc.Picking.User_mod,
                                                                                                .fec_mod = pBePeEnc.Picking.Fec_mod,
                                                                                                .detalle_operador = pBePeEnc.Picking.Detalle_operador,
                                                                                                .activo = pBePeEnc.Picking.Activo,
                                                                                                .verifica_auto = pBePeEnc.Picking.verifica_auto,
                                                                                                .procesado_bof = pBePeEnc.Picking.procesado_bof,
                                                                                                .requiere_preparacion = pBePeEnc.Picking.Requiere_Preparacion,
                                                                                                .tipo_preparacion = pBePeEnc.Picking.Tipo_Preparacion,
                                                                                                .estado_preparacion = pBePeEnc.Picking.Estado_Preparacion,
                                                                                                .fecha_inicio_preparacion = pBePeEnc.Picking.Fecha_Inicio_Preparacion,
                                                                                                .fecha_fin_preparacion = pBePeEnc.Picking.Fecha_Fin_Preparacion,
                                                                                                .referencia = pBePeEnc.Picking.Referencia,
                                                                                                .fotografia_verificacion = pBePeEnc.Picking.Fotografia_Verificacion,
                                                                                                .idBodegaMuelle = pBePeEnc.Picking.IdBodegaMuelle,
                                                                                                .idPrioridadPicking = pBePeEnc.Picking.IdPrioridadPicking,
                                                                                                .idTipoPicking = pBePeEnc.Picking.IdTipoPicking
                                                                                            },
                                                                        .detalle = pBePeEnc.Picking.ListaPickingDet,
                                                                        .pickingUbic = pBePeEnc.Picking.ListaPickingUbic,
                                                                        .pickingUbicStock = ListPickingUbicStock,
                                                                        .pickingImg = pPickingImgList,
                                                                        .pickingOperadores = pPickingOperadoresList,
                                                                        .prioridad = pPickingPrioridad
                                                                       },
                                                 .bodegaMuelle = peMuelle
                                                })

            listPayload.AddRange(peEncList)

            clsTransaccion.Commit_Transaction()

            Crear_Json = JsonConvert.SerializeObject(listPayload)

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function






    Public Shared Function ImportarSalidas(ByRef lblprg As RichTextBox, ByVal pTablaSincronizada As String) As List(Of Object)

        Dim clsTransaccion As New clsTransaccion()

        Dim BeLogUltimaSincronizacion As New clsBeLog_sincronizacion_nube()
        Dim listaPayload As New List(Of Object)()
        Dim peEncList As New List(Of Object)()
        Dim pePolizaList As New List(Of Object)()
        Dim pePicking As New Object()
        Dim peMuelle As New Object()
        Dim pPickingOperadoresList As New List(Of Object)
        Dim pPickingImgList As New List(Of Object)
        Dim ListPickingOp As New List(Of clsBeTrans_picking_op)()
        Dim pPickingPrioridad As New clsBeTrans_picking_prioridad()
        Dim pListPE As New List(Of clsBeTrans_pe_enc)()

        Try

            clsTransaccion.Begin_Transaction()

            BeLogUltimaSincronizacion = New clsBeLog_sincronizacion_nube()
            BeLogUltimaSincronizacion = clsLnLog_sincronizacion_nube.GetLastSync(pTablaSincronizada, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If BeLogUltimaSincronizacion IsNot Nothing Then
                pListPE = clsLnTrans_pe_enc.GetAll_By_CDC(BeLogUltimaSincronizacion.Fecha_sincronizacion, clsTransaccion.lConnection, clsTransaccion.lTransaction)
            Else
                clsHelper.LogMensaje(lblprg, "No se encontró sincronización previa para '" & pTablaSincronizada & "'. Se realizará sincronización completa desde fecha base.", clsHelper.TipoMensaje.Advertencia)
            End If


            If pListPE IsNot Nothing AndAlso pListPE.Count > 0 Then

                listaPayload = New List(Of Object)

                For Each pBePeEnc In pListPE

                    If pBePeEnc IsNot Nothing Then

                        '#GT23052025: limpiar los objetos para no duplicar registros
                        peEncList = New List(Of Object)()
                        pPickingOperadoresList = New List(Of Object)
                        pPickingImgList = New List(Of Object)
                        peMuelle = New Object()

                        If pBePeEnc.TipoPedido.Control_Poliza Then
                            pBePeEnc.ObjPoliza = clsLnTrans_pe_pol.GetSingleId(pBePeEnc.IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        End If

                        If pBePeEnc.Detalle Is Nothing OrElse pBePeEnc.Detalle.Count = 0 Then
                            Return peEncList
                        End If

                        If pBePeEnc.Picking.ListaPickingUbic Is Nothing Then
                            Throw New Exception("El pedido no tiene detalle del picking_ubic.")
                        End If

                        '#GT21052025: cargar lista de pickingUbicStock
                        Dim ListPickingUbicStock As New List(Of clsBeTrans_picking_ubic_stock)()
                        For Each pickingUbic In pBePeEnc.Picking.ListaPickingUbic
                            Dim pPickingUbicSTock As New clsBeTrans_picking_ubic_stock()
                            pPickingUbicSTock = clsLnTrans_picking_ubic_stock.Get_Single_By_IdPickingUbic_And_IdStock(pBePeEnc.Picking.IdPickingEnc,
                                                                                                                      pickingUbic.IdPickingUbic,
                                                                                                                      pickingUbic.IdStock,
                                                                                                                      clsTransaccion.lConnection,
                                                                                                                      clsTransaccion.lTransaction)
                            If pPickingUbicSTock IsNot Nothing Then
                                ListPickingUbicStock.Add(pPickingUbicSTock)
                            End If
                        Next

                        '#GT21052025: cargar lista operadores picking, sino tiene, cargar objeto por defecto
                        ListPickingOp = clsLnTrans_picking_op.Get_All_By_IdPickingEnc(pBePeEnc.IdPickingEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        If ListPickingOp IsNot Nothing AndAlso ListPickingOp.Count > 0 Then

                            For Each OperadorPicking In ListPickingOp

                                pPickingOperadoresList.Add(New With {
                                                                    .IdOperadorPicking = OperadorPicking.IdOperadorPicking,
                                                                    .IdPickingEnc = OperadorPicking.IdPickingEnc,
                                                                    .IdOperadorBodega = OperadorPicking.IdOperadorBodega,
                                                                    .user_agr = OperadorPicking.User_agr,
                                                                    .fec_agr = OperadorPicking.Fec_agr,
                                                                    .user_mod = OperadorPicking.User_mod,
                                                                    .fec_mod = OperadorPicking.Fec_mod
                                                           })



                                '#GT19052025: obtener operador-bodega
                                Dim pOperadorBodega As New clsBeOperador_bodega()
                                pOperadorBodega = clsLnOperador_bodega.GetSingle_By_IdOperadorBodega(OperadorPicking.IdOperadorBodega,
                                                                                                                    clsTransaccion.lConnection,
                                                                                                                    clsTransaccion.lTransaction)

                                If pOperadorBodega IsNot Nothing Then
                                    operadorBodegaList.Add(New With {
                                                                                .IdOperadorBodega = pOperadorBodega.IdOperadorBodega,
                                                                                .IdOperador = pOperadorBodega.IdOperador,
                                                                                .IdBodega = pOperadorBodega.IdBodega,
                                                                                .activo = pOperadorBodega.Activo,
                                                                                .user_agr = pOperadorBodega.User_agr,
                                                                                .fec_agr = pOperadorBodega.Fec_agr,
                                                                                .user_mod = pOperadorBodega.User_mod,
                                                                                .fec_mod = pOperadorBodega.Fec_mod
                                                                               })

                                End If

                                Dim pBeOperador As New clsBeOperador()
                                pBeOperador = clsLnOperador.Get_Single_By_IdOperador(pOperadorBodega.IdOperador,
                                                                                                 clsTransaccion.lConnection,
                                                                                                 clsTransaccion.lTransaction)
                                If pBeOperador IsNot Nothing Then
                                    operadorList.Add(New With {
                                                                            .IdOperador = pBeOperador.IdOperador,
                                                                            .IdEmpresa = pBeOperador.IdEmpresa,
                                                                            .IdRolOperador = pBeOperador.IdRolOperador,
                                                                            .IdJornada = pBeOperador.IdJornada,
                                                                            .nombres = pBeOperador.Nombres,
                                                                            .apellidos = pBeOperador.Apellidos,
                                                                            .direccion = pBeOperador.Direccion,
                                                                            .telefono = pBeOperador.Telefono,
                                                                            .codigo = pBeOperador.Codigo,
                                                                            .clave = pBeOperador.Clave,
                                                                            .activo = pBeOperador.Activo,
                                                                            .user_agr = pBeOperador.User_agr,
                                                                            .fec_agr = pBeOperador.Fec_agr,
                                                                            .user_mod = pBeOperador.User_mod,
                                                                            .fec_mod = pBeOperador.Fec_mod,
                                                                            .costo_hora = pBeOperador.Costo_hora,
                                                                            .usa_hh = pBeOperador.Usa_hh,
                                                                            .foto = pBeOperador.Foto,
                                                                            .recibe = pBeOperador.Recibe,
                                                                            .ubica = pBeOperador.Ubica,
                                                                            .transporta = pBeOperador.Transporta,
                                                                            .pickea = pBeOperador.Pickea,
                                                                            .verifica = pBeOperador.Verifica,
                                                                            .montacarga = pBeOperador.Montacarga,
                                                                            .sistema = pBeOperador.Sistema
                                                                         })
                                End If


                            Next
                        Else
                            pPickingOperadoresList.Add(New With {
                                                                                      .IdOperadorPicking = 0,
                                                                                      .IdPickingEnc = 0,
                                                                                      .IdOperadorBodega = 0,
                                                                                      .user_Agr = "",
                                                                                      .fec_agr = New Date(1900, 1, 1),
                                                                                      .user_mod = "",
                                                                                      .fec_mod = New Date(1900, 1, 1)
                                                  })
                        End If

                        '#GT23052025: cargar lista imagenes del picking
                        Dim ListPicking_img As New List(Of clsBeTrans_picking_img)()
                        ListPicking_img = clsLnTrans_Picking_Img.Get_All_Imagen_By_IdPedidoEnc(pBePeEnc.IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        If ListPicking_img IsNot Nothing AndAlso ListPicking_img.Count > 0 Then

                            For Each pPickingImg In ListPicking_img
                                pPickingImgList.Add(New With {
                                                            .IdImagen = pPickingImg.IdImagen,
                                                            .IdPickingEnc = pPickingImg.IdPickingEnc,
                                                            .IdPickingDet = pPickingImg.IdPickingDet,
                                                            .IdPedidoEnc = pPickingImg.IdPedidoEnc,
                                                            .IdPedidoDet = pPickingImg.IdPedidoDet,
                                                            .Imagen = pPickingImg.Imagen,
                                                            .user_agr = pPickingImg.User_agr,
                                                            .fec_agr = pPickingImg.Fec_agr,
                                                            .observacion = pPickingImg.Observacion
                                                            })
                            Next

                        Else
                            pPickingImgList.Add(New With {
                                                        .IdImagen = 0,
                                                        .IdPickingEnc = 0,
                                                        .IdPickingDet = 0,
                                                        .IdPedidoEnc = 0,
                                                        .IdPedidoDet = 0,
                                                        .Imagen = Nothing,
                                                        .user_agr = "",
                                                        .fec_agr = New Date(1900, 1, 1),
                                                        .observacion = ""
                                                        })

                        End If

                        '#GT23052025: cargar prioridad del picking
                        pPickingPrioridad = clsLnTrans_picking_prioridad.GetSingle_By_IdPickingPrioridad(pBePeEnc.Picking.IdPrioridadPicking, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                        If pPickingPrioridad Is Nothing Then
                            pPickingPrioridad = New clsBeTrans_picking_prioridad With {
                                                                                .IdPrioridadPicking = 0,
                                                                                .Codigo = 0,
                                                                                .Nombre = "",
                                                                                .User_agr = "",
                                                                                .Fec_agr = New Date(1900, 1, 1),
                                                                                .User_mod = "",
                                                                                .Fec_mod = New Date(1900, 1, 1),
                                                                                .Activo = False
                                                                                }
                        End If

                        If pBePeEnc.IdMuelle > 0 Then
                            Dim pBodegaMuelle As New clsBeBodega_muelles()
                            pBodegaMuelle.IdMuelle = pBePeEnc.IdMuelle
                            clsLnBodega_muelles.GetSingle(pBodegaMuelle, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                            If pBodegaMuelle IsNot Nothing Then
                                peMuelle = New With {
                                                .IdMuelle = pBodegaMuelle.IdMuelle,
                                                .IdBodega = pBodegaMuelle.IdBodega,
                                                .codigo_barra = pBodegaMuelle.Codigo_barra,
                                                .nombre = pBodegaMuelle.Nombre,
                                                .user_agr = pBodegaMuelle.User_agr,
                                                .fec_agr = pBodegaMuelle.Fec_agr,
                                                .user_mod = pBodegaMuelle.User_mod,
                                                .fec_mod = pBodegaMuelle.Fec_mod,
                                                .Color = pBodegaMuelle.Color,
                                                .imagen = pBodegaMuelle.Imagen,
                                                .activo = pBodegaMuelle.Activo,
                                                .Entrada = pBodegaMuelle.Entrada,
                                                .Salida = pBodegaMuelle.Salida,
                                                .IdUbicacionDefecto = pBodegaMuelle.IdUbicacionDefecto
                                                }
                            Else
                                Throw New Exception("El muelle asignado no es valido.")
                            End If
                        Else
                            peMuelle = New With {
                                                .IdMuelle = 0,
                                                .IdBodega = 0,
                                                .codigo_barra = "",
                                                .nombre = "",
                                                .user_agr = "",
                                                .fec_agr = New Date(1900, 1, 1),
                                                .user_mod = "",
                                                .fec_mod = New Date(1900, 1, 1),
                                                .Color = 0,
                                                .imagen = Nothing,
                                                .activo = False,
                                                .Entrada = False,
                                                .Salida = False,
                                                .IdUbicacionDefecto = 0
                                                }
                        End If


                        '#GT09052025: llenar el Pedido y añadir los objetos de detalle
                        peEncList.Add(New With {
                                                 .tipopedido = pBePeEnc.TipoPedido,
                                                 .encabezado = New With {
                                                                         .IdPedidoEnc = pBePeEnc.IdPedidoEnc,
                                                                         .IdBodega = pBePeEnc.IdBodega,
                                                                         .IdCliente = pBePeEnc.IdCliente,
                                                                         .IdMuelle = pBePeEnc.IdMuelle,
                                                                         .IdPropietarioBodega = pBePeEnc.IdPropietarioBodega,
                                                                         .IdTipoPedido = pBePeEnc.IdTipoPedido,
                                                                         .IdPickingEnc = pBePeEnc.IdPickingEnc,
                                                                         .Fecha_Pedido = pBePeEnc.Fecha_Pedido,
                                                                         .hora_ini = pBePeEnc.Hora_ini,
                                                                         .hora_fin = pBePeEnc.Hora_fin,
                                                                         .ubicacion = pBePeEnc.Ubicacion,
                                                                         .estado = pBePeEnc.Estado,
                                                                         .no_despacho = pBePeEnc.No_despacho,
                                                                         .activo = pBePeEnc.Activo,
                                                                         .user_agr = pBePeEnc.User_agr,
                                                                         .fec_agr = pBePeEnc.Fec_agr,
                                                                         .user_mod = pBePeEnc.User_mod,
                                                                         .fec_mod = pBePeEnc.Fec_mod,
                                                                         .no_documento = pBePeEnc.No_documento,
                                                                         .local = pBePeEnc.Local,
                                                                         .pallet_primero = pBePeEnc.Pallet_primero,
                                                                         .dias_cliente = pBePeEnc.Dias_cliente,
                                                                         .anulado = pBePeEnc.Anulado,
                                                                         .RoadKilometraje = pBePeEnc.RoadKilometraje,
                                                                         .RoadFechaEntr = pBePeEnc.RoadFechaEntr,
                                                                         .RoadDirEntrega = pBePeEnc.RoadDirEntrega,
                                                                         .RoadTotal = pBePeEnc.RoadTotal,
                                                                         .RoadDesMonto = pBePeEnc.RoadDesMonto,
                                                                         .RoadImpMonto = pBePeEnc.RoadImpMonto,
                                                                         .RoadPeso = pBePeEnc.RoadPeso,
                                                                         .RoadBandera = pBePeEnc.RoadBandera,
                                                                         .RoadStatCom = pBePeEnc.RoadStatCom,
                                                                         .RoadCalcoBJ = pBePeEnc.RoadCalcoBJ,
                                                                         .RoadImpres = pBePeEnc.RoadImpres,
                                                                         .RoadADD1 = pBePeEnc.RoadADD1,
                                                                         .RoadADD2 = pBePeEnc.RoadADD2,
                                                                         .RoadADD3 = pBePeEnc.RoadADD3,
                                                                         .RoadStatProc = pBePeEnc.RoadStatProc,
                                                                         .RoadRechazado = pBePeEnc.RoadRechazado,
                                                                         .RoadRazon_Rechazado = pBePeEnc.RoadRazon_Rechazado,
                                                                         .RoadInformado = pBePeEnc.RoadInformado,
                                                                         .RoadSucursal = pBePeEnc.RoadSucursal,
                                                                         .RoadIdDespacho = pBePeEnc.RoadIdDespacho,
                                                                         .RoadIdFacturacion = pBePeEnc.RoadIdFacturacion,
                                                                         .RoadIdRuta = pBePeEnc.RoadIdRuta,
                                                                         .RoadIdVendedor = pBePeEnc.RoadIdVendedor,
                                                                         .RoadIdRutaDespacho = pBePeEnc.RoadIdRutaDespacho,
                                                                         .RoadIdVendedorDespacho = pBePeEnc.RoadIdVendedorDespacho,
                                                                         .Observacion = pBePeEnc.Observacion,
                                                                         .PedidoRoad = pBePeEnc.PedidoRoad,
                                                                         .HoraEntregaDesde = pBePeEnc.HoraEntregaDesde,
                                                                         .HoraEntregaHasta = pBePeEnc.HoraEntregaHasta,
                                                                         .referencia = pBePeEnc.Referencia,
                                                                         .IdMotivoAnulacionBodega = pBePeEnc.IdMotivoAnulacionBodega,
                                                                         .Enviado_A_ERP = pBePeEnc.Enviado_A_ERP,
                                                                         .control_ultimo_lote = pBePeEnc.Control_Ultimo_Lote,
                                                                         .serie = pBePeEnc.Serie,
                                                                         .correlativo = pBePeEnc.Correlativo,
                                                                         .Referencia_Documento_Ingreso_Bodega_Destino = pBePeEnc.Referencia_Documento_Ingreso_Bodega_Destino,
                                                                         .sync_mi3 = pBePeEnc.Sync_MI3,
                                                                         .No_Picking_ERP = pBePeEnc.No_Picking_ERP,
                                                                         .no_documento_externo = pBePeEnc.No_Documento_Externo,
                                                                         .requiere_tarimas = pBePeEnc.Requiere_Tarimas,
                                                                         .fecha_preparacion = pBePeEnc.Fecha_Preparacion,
                                                                         .IdTipoManufactura = pBePeEnc.IdTipoManufactura,
                                                                         .bodega_origen = pBePeEnc.Bodega_Origen,
                                                                         .bodega_destino = pBePeEnc.Bodega_Destino,
                                                                         .idacuerdocomercial = pBePeEnc.IdAcuerdoComercial,
                                                                         .IdMotivoDevolucion = pBePeEnc.IdMotivoDevolucion,
                                                                         .Codigo_Empresa_ERP = pBePeEnc.Codigo_Empresa_ERP
                                                                        },
                                                 .detalle = pBePeEnc.Detalle,
                                                 .poliza = If(pBePeEnc.TipoPedido.Control_Poliza,
                                                              New List(Of clsBeTrans_pe_pol) From {pBePeEnc.ObjPoliza},
                                                              New List(Of clsBeTrans_pe_pol) From {New clsBeTrans_pe_pol With {
                                                                                                                            .IdOrdenPedidoPol = 0,
                                                                                                                            .IdOrdenPedidoEnc = 0,
                                                                                                                            .Bl_No = "",
                                                                                                                            .NoPoliza = "",
                                                                                                                            .Pto_Descarga = "",
                                                                                                                            .Viaje_no = "",
                                                                                                                            .Buque_no = "",
                                                                                                                            .Remitente = "",
                                                                                                                            .Fecha_abordaje = New Date(1900, 1, 1),
                                                                                                                            .Destino = "",
                                                                                                                            .Dir_destino = "",
                                                                                                                            .Descripcion = "",
                                                                                                                            .Po_number = "",
                                                                                                                            .Cantidad = 0,
                                                                                                                            .Piezas = 0,
                                                                                                                            .Total_kgs = 0D,
                                                                                                                            .Cbm = 0D,
                                                                                                                            .Dua = "",
                                                                                                                            .Fecha_poliza = New Date(1900, 1, 1),
                                                                                                                            .Pais_procede = "",
                                                                                                                            .Tipo_cambio = 0D,
                                                                                                                            .Total_valoraduana = 0D,
                                                                                                                            .Total_lineas = 0,
                                                                                                                            .Total_bultos = 0,
                                                                                                                            .Total_bultos_Peso = 0D,
                                                                                                                            .Total_usd = 0D,
                                                                                                                            .Total_flete = 0D,
                                                                                                                            .Total_seguro = 0D,
                                                                                                                            .User_agr = "",
                                                                                                                            .Fec_agr = New Date(1900, 1, 1),
                                                                                                                            .User_mod = "",
                                                                                                                            .Fec_mod = New Date(1900, 1, 1),
                                                                                                                            .clave_aduana = "",
                                                                                                                            .nit_imp_exp = "",
                                                                                                                            .clase = "",
                                                                                                                            .mod_transporte = "",
                                                                                                                            .total_liquidar = 0D,
                                                                                                                            .total_general = 0D,
                                                                                                                            .codigo_poliza = "",
                                                                                                                            .ticket = "",
                                                                                                                            .numero_orden = "",
                                                                                                                            .fecha_aceptacion = New Date(1900, 1, 1),
                                                                                                                            .fecha_llegada = New Date(1900, 1, 1),
                                                                                                                            .total_otros = 0D,
                                                                                                                            .IdRegimen = 0,
                                                                                                                            .Total_bultos_Peso_Neto = 0D,
                                                                                                                            .activo = False
                                                                                                                            }}),
                                                 .picking = New With {
                                                                        .encabezado = New With {
                                                                                                .idPickingEnc = pBePeEnc.Picking.IdPickingEnc,
                                                                                                .idBodega = pBePeEnc.Picking.IdBodega,
                                                                                                .idPropietarioBodega = pBePeEnc.Picking.IdPropietarioBodega,
                                                                                                .idUbicacionPicking = pBePeEnc.Picking.IdUbicacionPicking,
                                                                                                .fecha_picking = pBePeEnc.Picking.Fecha_picking,
                                                                                                .hora_ini = pBePeEnc.Picking.Hora_ini,
                                                                                                .hora_fin = pBePeEnc.Picking.Hora_fin,
                                                                                                .estado = pBePeEnc.Picking.Estado,
                                                                                                .user_agr = pBePeEnc.Picking.User_agr,
                                                                                                .fec_agr = pBePeEnc.Picking.Fec_agr,
                                                                                                .user_mod = pBePeEnc.Picking.User_mod,
                                                                                                .fec_mod = pBePeEnc.Picking.Fec_mod,
                                                                                                .detalle_operador = pBePeEnc.Picking.Detalle_operador,
                                                                                                .activo = pBePeEnc.Picking.Activo,
                                                                                                .verifica_auto = pBePeEnc.Picking.verifica_auto,
                                                                                                .procesado_bof = pBePeEnc.Picking.procesado_bof,
                                                                                                .requiere_preparacion = pBePeEnc.Picking.Requiere_Preparacion,
                                                                                                .tipo_preparacion = pBePeEnc.Picking.Tipo_Preparacion,
                                                                                                .estado_preparacion = pBePeEnc.Picking.Estado_Preparacion,
                                                                                                .fecha_inicio_preparacion = pBePeEnc.Picking.Fecha_Inicio_Preparacion,
                                                                                                .fecha_fin_preparacion = pBePeEnc.Picking.Fecha_Fin_Preparacion,
                                                                                                .referencia = pBePeEnc.Picking.Referencia,
                                                                                                .fotografia_verificacion = pBePeEnc.Picking.Fotografia_Verificacion,
                                                                                                .idBodegaMuelle = pBePeEnc.Picking.IdBodegaMuelle,
                                                                                                .idPrioridadPicking = pBePeEnc.Picking.IdPrioridadPicking,
                                                                                                .idTipoPicking = pBePeEnc.Picking.IdTipoPicking
                                                                                            },
                                                                        .detalle = pBePeEnc.Picking.ListaPickingDet,
                                                                        .pickingUbic = pBePeEnc.Picking.ListaPickingUbic,
                                                                        .pickingUbicStock = ListPickingUbicStock,
                                                                        .pickingImg = pPickingImgList,
                                                                        .pickingOperadores = pPickingOperadoresList,
                                                                        .prioridad = pPickingPrioridad
                                                                       },
                                                 .bodegaMuelle = peMuelle
                                                })

                    End If

                    For Each IdPedidoEnc As Integer In listaPedidos
                        Dim pPedido = clsLnTrans_pe_enc.Get_Single_By_IdPedidoEnc(IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        pListPedidos.Add(pPedido)
                    Next

                    End If

            clsTransaccion.Commit_Transaction()

            Return peEncList

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function


End Class
