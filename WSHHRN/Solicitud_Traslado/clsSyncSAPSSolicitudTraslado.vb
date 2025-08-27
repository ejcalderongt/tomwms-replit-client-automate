Imports System.Reflection
Imports System.Data.SqlClient
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSAPSSolicitudTraslado : Inherits clsInterfaceBase

    Implements IDisposable

    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0
    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    ''' <summary>
    ''' Valida que la línea de detalle de la OV en SAP esté abierta para generar la entrega.
    ''' </summary>
    ''' <param name="itemcode"></param>
    ''' <returns></returns>
    Private Function Estado_Linea(ByVal itemcode As String) As String

        Estado_Linea = "O"

        Try

            Dim oOrderSales As Documents
            oOrderSales = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)

            oOrderSales = oCompany.GetBusinessObject(BoObjectTypes.oOrders)

            If oOrderSales.Lines.GetByKey(itemcode) Then
                Estado_Linea = oOrderSales.Lines.LineStatus
            End If

            MsgBox(Estado_Linea)

        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Public Function Enviar_Traslado_Entre_Almacenes_SAP(ByVal _Docentry As Integer,
                                                    ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out)) As Boolean

        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vCodigoBodegaERP As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim NoLineaEntrega As Integer = 0
        Dim NoLineaEntregaLote As Integer = 0
        Dim clsTransaccion As New clsTransaccion

        Try
            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)



            If lRetCode <> 0 Then
                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If
            Else
                Dim oTransfer As StockTransfer
                Dim oTransferRequest As StockTransfer
                Dim BaseLine As Integer = 0

                oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
                oTransferRequest = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                If oTransferRequest.GetByKey(_Docentry) Then

                    oTransfer.CardCode = oTransferRequest.CardCode
                    oTransfer.DocDate = Date.Today
                    oTransfer.DocObjectCode = BoObjectTypes.oStockTransfer

                    NoLineaEntrega = 0
                    NoLineaEntregaLote = 0

                    clsTransaccion.Open_Connection()

                    For j As Integer = 0 To oTransferRequest.Lines.Count - 1

                        oTransferRequest.Lines.SetCurrentLine(j)

                        Dim vCodigoProductoSAP As String = oTransferRequest.Lines.ItemCode.ToString()
                        Dim vNoLineaOCSAP As Integer = oTransferRequest.Lines.LineNum

                        ''clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", vCodigoProductoSAP))

                        Dim DistinctProductosLineas = lINavTransaccionesOut.Where(Function(x) x.Codigo_producto = vCodigoProductoSAP _
                                                                              AndAlso x.No_linea = vNoLineaOCSAP).
                                                                        GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea}).
                                                                        Select(Function(g) New With {
                                                                            g.Key.Codigo_producto,
                                                                            g.Key.No_linea,
                                                                            .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                                        }).ToList()

                        If DistinctProductosLineas.Any() Then

                            For Each ProductoSalida In DistinctProductosLineas

                                If Not oTransferRequest.Lines.LineStatus = BoStatus.bost_Close Then

                                    If ProductoSalida.Cantidad_Total <= oTransferRequest.Lines.Quantity Then

                                        Dim nuevaLineaEntrega As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_producto)

                                        If nuevaLineaEntrega Then

                                            oTransfer.Lines.ItemCode = oTransferRequest.Lines.ItemCode
                                            oTransfer.Lines.BaseEntry = _Docentry
                                            oTransfer.Lines.BaseLine = vNoLineaOCSAP
                                            oTransfer.Lines.BaseType = BoObjectTypes.oInventoryTransferRequest
                                            oTransfer.Lines.Quantity = ProductoSalida.Cantidad_Total
                                            vCodigoAnterior = oTransfer.Lines.ItemCode

                                            oTransfer.Lines.Add()

                                            NoLineaEntrega += 1

                                        End If

                                    Else
                                        Throw New Exception("WMS está intentando generar una entrega por: " & ProductoSalida.Cantidad_Total &
                                                        " en una línea de SAP para el material: " & oTransferRequest.Lines.ItemCode &
                                                        " que refleja una cantidad de: " & oTransferRequest.Lines.Quantity & " probablemente esto no sea posible.")
                                    End If
                                Else
                                    ''clsPublic.Actualizar_Progreso(lblprg, String.Format("El Producto: {0} ya fue completado. ", oTransferRequest.Lines.ItemCode.ToString()))
                                End If
                            Next 'DistinctProductosLineas

                            vAgregarEntrega = True
                        End If
                    Next

                    Dim oResultado As Integer
                    oResultado = oTransfer.Add()

                    If oResultado <> 0 Then
                        Dim errMsg = oCompany.GetLastErrorDescription()
                        Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                    Else
                        Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar)

                        If IResult = 0 Then
                            Throw New Exception("Se envió la entrada de mercancía a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                        End If
                    End If

                    clsTransaccion.Commit_Transaction()
                End If
            End If

            Return True

        Catch errMsg As Exception
            clsTransaccion.RollBack_Transaction()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            ''clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar entrada de mercancía a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message))
        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try
    End Function


    Private Function Asigna_Unidad_De_Medida(ByRef BePedidoCompraDet As clsBeTrans_oc_det,
                                             ByRef navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                             ByRef BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                             ByRef BeProductoBodega As clsBeProducto_bodega,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction,
                                             ByRef lConnectionLog As SqlConnection) As Boolean

        Asigna_Unidad_De_Medida = False

        Try

            'Existe el producto con U.M.Bas = U.M. de pedido de compra.
            If Not clsLnProducto.Existe(navPedidoCompraDet.No,
                                        BeUnidadMedidaPedCompra.IdUnidadMedida,
                                        lConnection,
                                        lTransaction) Then

                Dim BePresentacion As New clsBeProducto_Presentacion

                BePresentacion = clsLnProducto_presentacion.
                Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                            navPedidoCompraDet.Unit_of_Measure_Code,
                                                            lConnection,
                                                            lTransaction)

                If Not BePresentacion Is Nothing Then
                    'La presentación ya existe
                    BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                    BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                    BePedidoCompraDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BePedidoCompraDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre
                Else

                    Dim vFactorConv As Double = clsLnUnidad_medida_conversion.Get_Factor(BeUnidadMedidaPedCompra.IdUnidadMedida,
                                                                   BeProductoBodega.Producto.UnidadMedida.IdUnidadMedida,
                                                                   lConnection,
                                                                   lTransaction)

                    'Existe factor para crear la presentación con la unidad de medida del pedido de compra.
                    If vFactorConv > 0 Then

                        BePresentacion = New clsBeProducto_Presentacion
                        BePresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1
                        BePresentacion.IdProducto = BeProductoBodega.IdProducto
                        BePresentacion.Codigo_barra = BeProductoBodega.Producto.Codigo_barra + navPedidoCompraDet.Unit_of_Measure_Code
                        BePresentacion.Nombre = navPedidoCompraDet.Unit_of_Measure_Code
                        BePresentacion.Imprime_barra = True
                        BePresentacion.Peso = 0
                        BePresentacion.Alto = 0
                        BePresentacion.Largo = 0
                        BePresentacion.Ancho = 0
                        BePresentacion.Factor = vFactorConv
                        BePresentacion.MinimoExistencia = 0
                        BePresentacion.MaximoExistencia = 0
                        BePresentacion.User_agr = BeConfigEnc.IdUsuario
                        BePresentacion.User_mod = BeConfigEnc.IdUsuario
                        BePresentacion.Fec_agr = Now
                        BePresentacion.Fec_mod = Now
                        BePresentacion.Activo = True
                        BePresentacion.EsPallet = False
                        BePresentacion.Precio = 0
                        BePresentacion.MinimoPeso = 0
                        BePresentacion.MaximoPeso = 0
                        BePresentacion.Costo = 0
                        BePresentacion.CamasPorTarima = 0
                        BePresentacion.CajasPorCama = 0
                        BePresentacion.Genera_lp_auto = False
                        BePresentacion.Permitir_paletizar = False
                        BePresentacion.Sistema = True
                        BePresentacion.Codigo = BeProductoBodega.Producto.Codigo

                        Try

                            clsLnProducto_presentacion.Insertar(BePresentacion, lConnection, lTransaction)

                            BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                            BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                            BePedidoCompraDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica
                            BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica
                            BePedidoCompraDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BePedidoCompraDet.Codigo_Producto,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet,
                                                                       lConnectionLog)

                            ''clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))

                        End Try

                    Else

                        Throw New Exception(String.Format("Error: No existe factor en unidad_medida_conversion 
                                               para Producto: {0} UnidMedBas {1} <> UnidMed Ped. Compra {2} ",
                                              navPedidoCompraDet.No,
                                              BeProductoBodega.Producto.UnidadMedida.Nombre,
                                              navPedidoCompraDet.Unit_of_Measure_Code))

                    End If 'Fin Sí: 'Existe factor para crear la presentación con la unidad de medida del pedido de compra.                   

                End If 'Fin sí: Existe presentación.              

            Else
                'La unidad de medida básica del producto es = a la unidad de medida del pedido de compra.
                'Se utiliza la UM del pedido de compra aunque la básica del maestro sea otra porque existe factor de conversión
                BePedidoCompraDet.IdUnidadMedidaBasica = BeUnidadMedidaPedCompra.IdUnidadMedida
                BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeUnidadMedidaPedCompra.IdUnidadMedida
                BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
            End If

            Asigna_Unidad_De_Medida = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Marcar_Solicitud_Trasladado_Sincronizado_SAP(ByVal pNoDocumento As String, ByVal EstadoEnvio As Estado_Enviado_SAP) As Boolean

        Marcar_Solicitud_Trasladado_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                ''clsPublic.Actualizar_Progreso(lblprg, "Actualizando el estado enviado = " & EstadoEnvio & " para permitir importación nuevamente en pedido de cliente: " & pNoDocumento)

                Dim osalidaMercancia As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                If osalidaMercancia.GetByKey(pNoDocumento) Then

                    Try

                        osalidaMercancia.UserFields.Fields.Item("U_ENVIADO_WMS").Value = EstadoEnvio
                        osalidaMercancia.Update()

                        ''clsPublic.Actualizar_Progreso(lblprg, "Se actualizó el estado del documento.")

                        Marcar_Solicitud_Trasladado_Sincronizado_SAP = True

                    Catch e As Exception
                        Throw e
                    End Try

                Else
                    ''clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener el documento de SAP con DocEntry: " & pNoDocumento)
                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function


    'Public Function Enviar_Traslado_Desde_Solicitud_SAP(ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
    '                                                    ByRef lblprg As RichTextBox,
    '                                                    ByRef prg As Windows.Forms.ProgressBar) As Boolean

    '    prg.Maximum = lINavTransaccionesOut.Count
    '    prg.Visible = True

    '    Dim lINav_Transacciones_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
    '    Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
    '    Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
    '    Dim vCodigoAnterior As String = ""
    '    Dim NoLineaTransfer As Integer = 0
    '    Dim NoLineaTransferLote As Integer = 0
    '    Dim vAgregarEntrega As Boolean = False
    '    Dim clsTransaccion As New clsTransaccion
    '    Dim BodegasByPedido As New Dictionary(Of Integer, clsBeInfoBodegaByIdPedidoEnc)

    '    Try

    '        Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

    '        

    '        If lRetCode <> 0 Then

    '            If sErrMsg = " - The specified resource name cannot be found in the image file." Then
    '                Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
    '            Else
    '                Throw New Exception("Error al conectar a SAP: " & sErrMsg)
    '            End If

    '        Else

    '            Dim oTransfer As StockTransfer
    '            Dim BaseLine As Integer = 0

    '            oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

    '            NoLineaTransfer = 0
    '            NoLineaTransferLote = 0

    '            clsTransaccion.Open_Connection()


    '            Dim DistinctIdPedidoEncByTraslado = lINavTransaccionesOut.Where(Function(x) x.IdTipoDocumento = tTipoDocumentoSalida.Transferencia_Directa).
    '                                                            GroupBy(Function(x) New With {Key x.Idpedidoenc, Key x.Codigo_producto}).
    '                                                            Select(Function(g) New With {
    '                                                                .IdPedidoEnc = g.Key.Idpedidoenc,
    '                                                                .Codigo_Producto = g.Key.Codigo_producto,
    '                                                                .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
    '                                                            }).ToList()

    '            If DistinctIdPedidoEncByTraslado.Any() Then

    '                ''clsPublic.Actualizar_Progreso(lblprg, String.Format("Traslados encontrados: {0} ", DistinctIdPedidoEncByTraslado.Count))

    '                For Each ProductoSalida In DistinctIdPedidoEncByTraslado

    '                    Dim IdPedidoEnc As Integer = ProductoSalida.IdPedidoEnc
    '                    Dim infoBodega As clsBeInfoBodegaByIdPedidoEnc = Nothing
    '                    Dim FromWarehouse As String = ""
    '                    Dim ToWarehouse As String = ""

    '                    If Not BodegasByPedido.TryGetValue(IdPedidoEnc, infoBodega) Then
    '                        ' Registro no encontrado
    '                        Console.WriteLine("El IdPedidoEnc " & IdPedidoEnc & " no se encontró en el diccionario.")
    '                        infoBodega = Get_Codigos_Bodegas(IdPedidoEnc)
    '                    End If

    '                    FromWarehouse = infoBodega.Codigo_Bodega_Origen
    '                    ToWarehouse = infoBodega.Codigo_Bodega_Destino

    '                    oTransfer.FromWarehouse = FromWarehouse
    '                    oTransfer.ToWarehouse = ToWarehouse
    '                    oTransfer.Comments = "Solicitud de traslado generada por TOMWMS"

    '                    Dim nuevaLineaTransfer As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_Producto)

    '                    If nuevaLineaTransfer Then

    '                        oTransfer.Lines.SetCurrentLine(NoLineaTransfer)
    '                        'oTransfer.Lines.BaseType = InvBaseDocTypeEnum.InventoryTransferRequest
    '                        'oTransfer.Lines.BaseEntry = _DocEntry
    '                        'oTransfer.Lines.BaseLine = vNoLineaTRSAP
    '                        oTransfer.Lines.ItemCode = ProductoSalida.Codigo_Producto
    '                        oTransfer.Lines.Quantity = ProductoSalida.Cantidad_Total
    '                        oTransfer.Lines.FromWarehouseCode = FromWarehouse
    '                        oTransfer.Lines.WarehouseCode = ToWarehouse
    '                        vCodigoAnterior = oTransfer.Lines.ItemCode

    '                        oTransfer.Lines.Add() : NoLineaTransfer += 1

    '                    End If

    '                Next 'DistinctProductosLineas

    '                vAgregarEntrega = True
    '            End If

    '            Dim oResultado As Integer
    '            oResultado = oTransfer.Add()

    '            If oResultado <> 0 Then
    '                Dim errMsg = oCompany.GetLastErrorDescription()
    '                Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
    '            Else
    '                Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar)

    '                If IResult = 0 Then
    '                    Throw New Exception("Se envió el traslado entre almacenes a SAP pero no se pudieron marcar los registros como enviados en WMS.")
    '                End If
    '            End If

    '            clsTransaccion.Commit_Transaction()


    '        End If

    '        Return True

    '    Catch errMsg As Exception
    '        clsTransaccion.RollBack_Transaction()
    '        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar traslado entre almacenes a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
    '                                               "",
    '                                               BeNavEjecucionEnc.IdEjecucionEnc,
    '                                               BeConfigDet.Idnavconfigdet)

    '        ''clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar traslado entre almacenes a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message))

    '    Finally
    '        Desconectar_SAP(oCompany)
    '        clsTransaccion.Close_Conection()
    '    End Try
    'End Function

    Public Function Get_Codigos_Bodegas(ByVal IdPedidoEnc As Integer) As clsBeInfoBodegaByIdPedidoEnc

        Get_Codigos_Bodegas = Nothing

        Dim BeInfoBodega As New clsBeInfoBodegaByIdPedidoEnc

        Try

            Dim BePedidoEnc As New clsBeTrans_pe_enc
            BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc)

            If Not BePedidoEnc Is Nothing Then

                BeInfoBodega.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(BePedidoEnc.IdBodega)
                BeInfoBodega.Codigo_Bodega_Destino = BePedidoEnc.Cliente.Codigo
                Get_Codigos_Bodegas = BeInfoBodega

            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Class clsBeInfoBodegaByIdPedidoEnc

        Public Property Codigo_Bodega_Origen As String = ""
        Public Property Codigo_Bodega_Destino As String = ""


    End Class

    Public Function Enviar_Solicitud_Traslado_Proveedor(ByVal pTipo As tTipoDocumentoSalida) As Boolean

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Try

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})

                Dim Enviado_A_Erp As Boolean = False

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        If Enviar_Solicitud_Traslado_Proveedor_SAP(Val(PT.No_pedido), lTransaccionesSalida) Then

                            Try

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet)

                            End Try

                        End If

                    Else
                        ''clsPublic.Actualizar_Progreso(lblprg, "Se están intentando enviar registros de un pedido que ya fue marcado como enviado a ERP, por favor valide la integridad de los datos manualmente.")
                    End If

                Next

            Else

                ''clsPublic.Actualizar_Progreso(lblprg, "MSG_240117: No hay transacciones para enviar.")

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Enviar_Solicitud_Traslado_Proveedor_SAP(ByVal _DocEntry As Integer,
                                                            ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out)) As Boolean

        Dim lINav_Transacciones_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vNoLineaAnterior As Integer = -1
        Dim NoLineaTransfer As Integer = 0
        Dim NoLineaTransferLote As Integer = 0
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion
        Dim IdPedidoEnc As Integer = 0
        Dim IdDespachoEnc As Integer = 0
        Dim BeDespacho As New clsBeTrans_despacho_enc
        Dim oTransfer As StockTransfer
        Dim oTransferRequest As StockTransfer
        Dim BaseLine As Integer = 0

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then

                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If

            Else

                oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
                oTransferRequest = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                Dim vCodigoProductoSAP As String = oTransferRequest.Lines.ItemCode.ToString()
                Dim vNoLineaTRSAP As Integer = oTransferRequest.Lines.LineNum

                IdPedidoEnc = lINavTransaccionesOut.FirstOrDefault().Idpedidoenc
                IdDespachoEnc = lINavTransaccionesOut.FirstOrDefault().Iddespachoenc

                Dim BePedidoEnc As New clsBeTrans_pe_enc
                Dim vBodega_Destino As String = ""

                BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                BeDespacho = clsLnTrans_despacho_enc.GetSingle(IdDespachoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                vBodega_Destino = BePedidoEnc.Bodega_Destino

                Enviar_Solicitud_Traslado_Proveedor_SAP(lINavTransaccionesOut,
                                                        BePedidoEnc)

                clsTransaccion.Commit_Transaction()

            End If


            Return True

        Catch errMsg As Exception
            clsTransaccion.RollBack_Transaction() : oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar traslado entre almacenes a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            'clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar traslado entre almacenes a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message))

        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Function Enviar_Solicitud_Traslado_Proveedor_SAP(ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                            ByVal BePedidoEnc As clsBeTrans_pe_enc) As Boolean


        Dim lINav_Transacciones_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim NoLineaTransfer As Integer = 0
        Dim NoLineaTransferLote As Integer = 0
        Dim vNoLineaAnterior As Integer = -1
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion
        Dim BodegasByPedido As New Dictionary(Of Integer, clsBeInfoBodegaByIdPedidoEnc)

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then

                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If

            Else

                Dim oReturnRequestProvider As Documents
                Dim BaseLine As Integer = 0

                oReturnRequestProvider = CType(oCompany.GetBusinessObject(BoObjectTypes.oGoodsReturnRequest), Documents)

                NoLineaTransfer = 0
                NoLineaTransferLote = 0

                clsTransaccion.Open_Connection()


                Dim DistinctIdPedidoEncByTraslado = lINavTransaccionesOut.Where(Function(x) x.IdTipoDocumento = tTipoDocumentoSalida.Devolucion_Proveedor).
                                                                GroupBy(Function(x) New With {Key x.Idpedidoenc, Key x.Codigo_producto}).
                                                                Select(Function(g) New With {
                                                                    .IdPedidoEnc = g.Key.Idpedidoenc,
                                                                    .Codigo_Producto = g.Key.Codigo_producto,
                                                                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                                }).ToList()

                If DistinctIdPedidoEncByTraslado.Any() Then

                    'clsPublic.Actualizar_Progreso(lblprg, String.Format("Traslados encontrados: {0} ", DistinctIdPedidoEncByTraslado.Count))

                    For Each ProductoSalida In DistinctIdPedidoEncByTraslado

                        oReturnRequestProvider.CardCode = BePedidoEnc.Cliente.Codigo
                        oReturnRequestProvider.Comments = BePedidoEnc.IdPedidoEnc
                        oReturnRequestProvider.UserFields.Fields.Item("U_FIS").Value = "N"
                        oReturnRequestProvider.JournalMemo = BePedidoEnc.IdPedidoEnc

                        Dim nuevaLineaTransfer As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_Producto)

                        If nuevaLineaTransfer Then

                            oReturnRequestProvider.Lines.SetCurrentLine(NoLineaTransfer)
                            oReturnRequestProvider.Lines.ItemCode = ProductoSalida.Codigo_Producto
                            oReturnRequestProvider.Lines.Quantity = ProductoSalida.Cantidad_Total
                            vCodigoAnterior = oReturnRequestProvider.Lines.ItemCode

                            oReturnRequestProvider.Lines.Add() : NoLineaTransfer += 1

                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_linea = NoLineaTransfer _
                                                                                  AndAlso x.Codigo_producto = ProductoSalida.Codigo_Producto _
                                                                                  AndAlso x.Enviado = False)

                            If Not Sublista_A_Actualizar Is Nothing Then
                                If Sublista_A_Actualizar.Count > 0 Then
                                    Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                End If
                            End If

                        End If


                    Next 'DistinctProductosLineas

                    vAgregarEntrega = True

                Else
                    'clsPublic.Actualizar_Progreso(lblprg, "No hay líneas para enviar.")
                End If

                oReturnRequestProvider.UserFields.Fields.Item("U_tiedest").Value = BePedidoEnc.IdBodega.ToString()
                oReturnRequestProvider.UserFields.Fields.Item("U_Causas_dev").Value = BePedidoEnc.IdMotivoDevolucion.ToString()

                Dim oResultado As Integer
                oResultado = oReturnRequestProvider.Add()

                If oResultado <> 0 Then
                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                Else

                    Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar)

                    If IResult = 0 Then
                        Throw New Exception("Se envió el traslado entre almacenes a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                    End If

                    ' Obtener el DocEntry del traslado generado
                    Dim newObjectCode As String = ""
                    oCompany.GetNewObjectCode(newObjectCode)

                    Dim vTrasladoDocEntry As Integer = 0

                    If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                        Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                    End If

                    Dim vMensaje As String = "Se creó la solicitud de traslado con número: " & vTrasladoDocEntry & " en SAP."

                    BePedidoEnc.No_Documento_Externo = vTrasladoDocEntry
                    clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(BePedidoEnc)

                    'clsPublic.Actualizar_Progreso(lblprg, vMensaje)
                    clsLnLog_error_wms.Agregar_Error(BeConfigEnc.Idempresa, BeConfigEnc.Idbodega, vMensaje, BePedidoEnc.IdPedidoEnc, 0, 0, BeConfigEnc.IdUsuario)

                End If

                clsTransaccion.Commit_Transaction()

            End If

            Return True

        Catch errMsg As Exception
            clsTransaccion.RollBack_Transaction()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar traslado entre almacenes a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            'clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar traslado entre almacenes a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message))

        Finally
            'Desconectar_SAP(oCompany) No cerrar porque viene en transacción SAP Desde el traslado.
            clsTransaccion.Close_Conection()
        End Try
    End Function

    Public Function Conectar_A_SAP(ByRef oCompany As Company,
                                   Optional ByVal pThrowException As Boolean = False,
                                   Optional ByRef pCodigoError As Integer = 0,
                                   Optional ByRef pMensajeError As String = "") As Boolean

        Conectar_A_SAP = False

        pCodigoError = 0

        Try

            If oCompany Is Nothing Then
                oCompany = New Company
            End If

            If (Not oCompany.Connected) Then

                'oCompany = New Company
                'oCompany.SLDServer = BD.Instancia.LICENSESERVER_SAP_BO
                'oCompany.Server = BD.Instancia.SERVER_BD_SAP
                'oCompany.CompanyDB = BD.Instancia.SAP_COMPANY_DB
                'oCompany.UserName = BD.Instancia.SAP_USR.Trim()
                'oCompany.Password = BD.Instancia.SAP_USR_PW.Trim()
                'oCompany.DbUserName = BD.Instancia.SAP_DB_USR.Trim()
                'oCompany.DbPassword = BD.Instancia.SAP_DB_PW.Trim()
                'oCompany.language = BoSuppLangs.ln_Spanish_La
                'oCompany.UseTrusted = False

                'If BD.Instancia.SAP_DB_VERSION = 2017 Then
                '    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017
                'ElseIf BD.Instancia.SAP_DB_VERSION = 2019 Then
                '    oCompany.LicenseServer = BD.Instancia.SERVER_BD_SAP
                '    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2019
                'End If

                oCompany.UseTrusted = False

                Dim lRetCode As Integer = oCompany.Connect()

                If lRetCode <> 0 Then
                    oCompany.GetLastError(pCodigoError, pMensajeError)
                    If pThrowException Then
                        Throw New Exception(pMensajeError)
                    End If
                Else
                    Conectar_A_SAP = True
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Desconectar_SAP(ByRef oCompany As Company) As Boolean

        Desconectar_SAP = False

        Try

            If Not IsNothing(oCompany) Then
                If oCompany.Connected Then
                    oCompany.Disconnect()
                End If
            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class