Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports DevExpress.XtraEditors
Imports SAPbobsCOM

Public Class clsSyncSAPDevolucionMercancia : Inherits clsInterfaceBase

    Implements IDisposable

    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0
    Private Shared oCompany As Company
    Private Shared lRetCode
    Private Shared lErrCode As Long
    Private Shared sErrMsg As String = ""

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Private Function Asigna_Unidad_De_Medida(ByRef BePedidoCompraDet As clsBeTrans_oc_det,
                                             ByRef navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                             ByRef BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                             ByRef BeProductoBodega As clsBeProducto_bodega,
                                             ByRef lblprg As RichTextBox,
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

                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
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

                            Actualizar_Progreso(lblprg, String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))

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
    Public Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                              ByRef prg As Windows.Forms.ProgressBar)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Sl As New clsSyncLotes()
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(0)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        If Enviar_Entrega_Mercancia_OV_SAP(PT.No_pedido, lTransaccionesSalidaSingle, lblprg, prg) Then

                            Try

                                Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                            PT.No_pedido,
                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                            BeConfigDet.Idnavconfigdet, CnnLog)

                            End Try

                        End If

                    End If

                Next

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

    Public Function Enviar_Entrega_Mercancia_OV_SAP(ByVal _Docentry As Integer,
                                                    ByVal lINav_Transaccioens_Out As List(Of clsBeI_nav_transacciones_out),
                                                    ByRef lblprg As RichTextBox,
                                                    ByRef prg As Windows.Forms.ProgressBar) As Boolean


        prg.Maximum = lINav_Transaccioens_Out.Count
        prg.Visible = True

        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)

        Try

            Conectar_A_SAP(oCompany, lRetCode, sErrMsg)

            Application.DoEvents()

            If lRetCode <> 0 Then
                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If
            Else

                Dim oEntrega As Documents
                Dim oOrderSales As Documents
                Dim BaseLine As Integer = 0

                oEntrega = CType(oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes), Documents)
                oOrderSales = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)

                If oOrderSales.GetByKey(_Docentry) Then

                    Console.WriteLine(oOrderSales.DocumentStatus)

                    oEntrega.CardCode = oOrderSales.CardCode
                    oEntrega.DocDate = Date.Today
                    oEntrega.DocObjectCode = BoObjectTypes.oDeliveryNotes

                    For j As Integer = 0 To oOrderSales.Lines.Count - 1

                        oOrderSales.Lines.SetCurrentLine(j)

                        Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", oOrderSales.Lines.ItemCode.ToString()))

                        Dim BeInavTransaccioensOut As clsBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out()
                        BeInavTransaccioensOut = lINav_Transaccioens_Out.Find(Function(x) x.Codigo_producto = oOrderSales.Lines.ItemCode.ToString())

                        If Not BeInavTransaccioensOut Is Nothing Then

                            If Not oOrderSales.Lines.LineStatus = BoStatus.bost_Close Then

                                If BeInavTransaccioensOut.Cantidad <= oOrderSales.Lines.Quantity Then

                                    Dim vTipoImpuesto As String = oOrderSales.Lines.TaxCode

                                    oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_Order)
                                    oEntrega.Lines.ItemCode = oOrderSales.Lines.ItemCode
                                    oEntrega.Lines.BaseEntry = _Docentry
                                    oEntrega.Lines.BaseLine = BaseLine
                                    oEntrega.Lines.TaxCode = vTipoImpuesto
                                    oEntrega.Lines.UserFioEntregaelds.Fields.Item("U_Tipo").Value = "B"
                                    oEntrega.Lines.Quantity = BeInavTransaccioensOut.Cantidad
                                    oEntrega.Lines.Add()

                                Else
                                    Throw New Exception("WMS está intentando generar una entrega por: " & BeInavTransaccioensOut.Cantidad & " en una línea de SAP para el material: " & oOrderSales.Lines.ItemCode & " que refleja una cantidad de: " & BeInavTransaccioensOut.Cantidad & " probablemente esto no sea posible.")
                                End If

                            Else

                                Actualizar_Progreso(lblprg, String.Format("El Producto: {0} ya fue completado. ", oOrderSales.Lines.ItemCode.ToString()))

                            End If

                            lINav_Transaccioens_Out_Enviadas.Add(BeInavTransaccioensOut)

                            BaseLine += 1

                        End If

                    Next

                    Dim oResultado As Integer
                    oResultado = oEntrega.Add()

                    If oResultado <> 0 Then
                        Throw New Exception(oCompany.GetLastErrorDescription())
                    Else

                        Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(lINav_Transaccioens_Out_Enviadas)

                        If IResult = 0 Then
                            Throw New Exception("Se envió la entrada de mercancía a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                        End If

                    End If

                End If

            End If

            Return True

        Catch errMsg As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)

            Actualizar_Progreso(lblprg, String.Format("Error al enviar entrada de mercancía a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message))

        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    ''' <summary>
    ''' Valida que la línea de detalle de la OV en SAP esté abierta para generar la entrega.
    ''' </summary>
    ''' <param name="itemcode"></param>
    ''' <returns></returns>
    Private Function Estado_Linea(ByVal itemcode As String) As String

        Estado_Linea = "O"

        Try

            Dim oOrderSales As SAPbobsCOM.Documents
            oOrderSales = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)

            oOrderSales = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)

            If oOrderSales.Lines.GetByKey(itemcode) Then
                Estado_Linea = oOrderSales.Lines.LineStatus
            End If

            MsgBox(Estado_Linea)

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Inserta_Cliente_SAP(ByVal pCodigo As String,
                                               Optional ByVal pEsProveedor As Boolean = False) As Boolean

        Inserta_Cliente_SAP = False

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim query_sap As String

                If Not pEsProveedor Then

                    query_sap = "SELECT top 1 T0.CARDCODE AS CODIGO,
                                          T0.CARDNAME AS NOMBRE_COMERCIAL,
                                          T0.Phone1, 'TEST' AS CONTACTO ,
                                          T0.u_nit AS NIT, 
                                          T0.Address AS DIRECCION, 
                                          T0.E_Mail FROM OCRD T0 
                                          WHERE T0.CARDTYPE = 'C'  
                                          AND (t0.CARDCODE)= '" & pCodigo & "'"

                Else

                    query_sap = "SELECT top 1 T0.CARDCODE AS CODIGO,
                                          T0.CARDNAME AS NOMBRE_COMERCIAL,
                                          T0.Phone1, 'TEST' AS CONTACTO ,
                                          T0.u_nit AS NIT, 
                                          T0.Address AS DIRECCION, 
                                          T0.E_Mail FROM OCRD T0 
                                          WHERE T0.CARDTYPE = 'S'  
                                          AND (t0.CARDCODE)= '" & pCodigo & "'"

                End If


                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeCliente As New clsBeCliente
                Dim BeClienteBodega As New clsBeCliente_bodega

                While rs.EoF = False

                    BeCliente.IdCliente = clsLnCliente.MaxID() + 1
                    BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                    BeCliente.Codigo = rs.Fields.Item("CODIGO").Value.ToString()
                    BeCliente.Nombre_comercial = rs.Fields.Item("NOMBRE_COMERCIAL").Value.ToString()
                    BeCliente.Sistema = True
                    BeCliente.Activo = True
                    BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                    BeCliente.Nit = rs.Fields.Item("NIT").Value.ToString()
                    BeCliente.IdTipoCliente = 1
                    BeCliente.Es_bodega_recepcion = False
                    BeCliente.Es_Bodega_Traslado = False

                    clsLnCliente.Insertar(BeCliente)

                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID() + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                    BeClienteBodega.User_agr = BeConfigEnc.User_agr
                    BeClienteBodega.User_mod = BeConfigEnc.User_agr
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now
                    clsLnCliente_bodega.Insertar(BeClienteBodega)

                    Dim oBusinessPartnerSBO As BusinessPartners = CType(oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners), SAPbobsCOM.BusinessPartners)

                    If oBusinessPartnerSBO.GetByKey(pCodigo) Then
                        oBusinessPartnerSBO.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                        oBusinessPartnerSBO.Update()
                    End If

                    Inserta_Cliente_SAP = True

                    rs.MoveNext()

                End While

            End If

        Catch ex As Exception
            Throw New Exception("No se pudo insertar el cliente nuevo proviniente de SAP: " & ex.Message)
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function
    Public Function Marcar_PI_Sincronizado_SAP(ByVal pNoDocumento As String) As Boolean

        Marcar_PI_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim osalidaMercancia As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseReturns), Documents)

                If osalidaMercancia.GetByKey(pNoDocumento) Then

                    Try

                        osalidaMercancia.UserFields.Fields.Item("U_EnviadoWMS").Value = "1"
                        osalidaMercancia.Update()

                    Catch e As Exception
                        Throw e
                    End Try

                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String)
        lblPrg.AppendText(mensaje & vbNewLine)
        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
    End Sub

    Public Class DevolucionCliente
        Public Property DocEntry As Integer
        Public Property CardCode As String
        Public Property CardName As String
        Public Property DocDate As Date
        Public Property DocTotal As Double
        Public Property WhsCode As String ' Código de Bodega
        Public Property WhsName As String ' Nombre de Bodega
        Public Property Detalles As List(Of DetalleDevolucionCliente)

    End Class

    Public Class DetalleDevolucionCliente
        Public Property LineNum As Integer
        Public Property ItemCode As String
        Public Property Dscription As String
        Public Property Quantity As Double
        Public Property Price As Double
        Public Property UomCode As String

    End Class
    Private Function Get_Detalle_Devolucion(ByVal oCompany As Company,
                                            ByVal docEntry As Integer) As List(Of DetalleDevolucionCliente)

        Dim detalles As New List(Of DetalleDevolucionCliente)

        Dim oRecordset As Recordset
        oRecordset = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset)

        Dim sqlQueryDetalle As String = "SELECT LineNum, ItemCode, Dscription, Quantity, Price, UomCode " &
                                        "FROM RDR1 " &
                                        "WHERE DocEntry = " & docEntry

        Try

            oRecordset.DoQuery(sqlQueryDetalle)

            While Not oRecordset.EoF

                Dim detalle As New DetalleDevolucionCliente()
                detalle.LineNum = oRecordset.Fields.Item("LineNum").Value
                detalle.ItemCode = oRecordset.Fields.Item("ItemCode").Value
                detalle.Dscription = oRecordset.Fields.Item("Dscription").Value
                detalle.Quantity = oRecordset.Fields.Item("Quantity").Value
                detalle.Price = oRecordset.Fields.Item("Price").Value
                detalle.UomCode = oRecordset.Fields.Item("UomCode").Value

                detalles.Add(detalle)
                oRecordset.MoveNext()

            End While

        Catch ex As Exception
            Throw
        End Try

        Return detalles

    End Function

    Public Function Importar_Solicitud_Devolucion_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                        ByRef prg As System.Windows.Forms.ProgressBar,
                                                                        ByRef cnnLog As SqlConnection) As Boolean

        Importar_Solicitud_Devolucion_SAP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResult As String = ""

        Try

            Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia.")

            Dim lDevolucionesCliente As New List(Of clsBeI_nav_ped_compra_enc)
            lDevolucionesCliente = Get_Solicitud_Devolucion_From_SAP()

            BeNavEjecucionRes.Registros_ws = lDevolucionesCliente.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeProductoBodega As New clsBeProducto_bodega

            Actualizar_Progreso(lblprg, vbTab & String.Format("Pedidos de compra en relación con SAP (OPOR): {0} ", lDevolucionesCliente.Count))

            prg.Maximum = lDevolucionesCliente.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            'Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each BeINavDevolCliente In lDevolucionesCliente

                    Actualizar_Progreso(lblprg, vbTab & String.Format("Procesando Pedido Compra: {0} ", BeINavDevolCliente.No, vbNewLine))

                    If Not clsLnProveedor.Existe_Proveedor(BeINavDevolCliente.Buy_From_Vendor_No) Then

                        BeConfigEnc = BeConfigEnc

                        If Inserta_Proveedor_Desde_SAP(BeINavDevolCliente.Buy_From_Vendor_No, cnnLog) Then
                            Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & BeINavDevolCliente.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                        End If

                    End If

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavDevolCliente,
                                                                            BePedidoCompraEnc,
                                                                            vResult) Then
                        Marcar_PI_Sincronizado_SAP(BeINavDevolCliente.No)

                    End If

                    Actualizar_Progreso(lblprg, vResult)

                Next

            End If

            lTransaction.Commit()

            Importar_Solicitud_Devolucion_SAP_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Actualizar_Progreso(lblprg, String.Format("Error al insertar Ordenes de Compra desde SAP a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function

    Public Function Get_Solicitud_Devolucion_From_SAP(Optional ByVal AplicarFiltros As Boolean = True) As List(Of clsBeI_nav_ped_compra_enc)

        Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
        Dim BePedidoCompra As New clsBeI_nav_ped_compra_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_compra_det
        Dim NoLinea As Integer = 0

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                Dim vSQLOC As String = ""

                vSQLOC = "SELECT T0.DOCENTRY, T0.DOCNUM,T0.DOCDATE, T0.CARDCODE, T0.CARDNAME, T0.DOCCUR, T0.DOCTOTAL,
                            T0.JRNLMEMO,
                            T0.CANCELED,T0.DOCSTATUS,  
                            CASE WHEN T0.DOCTYPE = 'I'THEN 'ARTICULO'    
                            ELSE 'SERVICIO'    
                            END AS TIPO_ORDEN_VENTA,
                            (SELECT TOP 1 D0.WhsCode 
                            FROM POR1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode WHERE D0.DocEntry = T0.DOCENTRY) AS BODEGA,
                            T0.COMMENTS,
                            T0.NumAtCard,
                            ISNULL(T0.U_Es_ImportacionWMS,0) AS U_Es_ImportacionWMS
                            FROM ORRR T0
                            WHERE T0.DocStatus = 'O' AND T0.U_ENVIADOWMS = 2"

                RsEnc.DoQuery(vSQLOC)

                While RsEnc.EoF = False

                    BePedidoCompra = New clsBeI_nav_ped_compra_enc()
                    BePedidoCompra.No = RsEnc.Fields.Item("DOCENTRY").Value
                    BePedidoCompra.Posting_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BePedidoCompra.Order_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BePedidoCompra.Document_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BePedidoCompra.Expected_Receipt_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BePedidoCompra.Status = 1
                    BePedidoCompra.Buy_From_Vendor_No = RsEnc.Fields.Item("CARDCODE").Value.ToString()
                    BePedidoCompra.Buy_From_Vendor_Name = RsEnc.Fields.Item("CARDNAME").Value.ToString()
                    BePedidoCompra.Is_Internal_Transfer = False
                    BePedidoCompra.Location_Code = Convert.ToString(RsEnc.Fields.Item("BODEGA").Value)
                    BePedidoCompra.Vendor_Invoice_No = Convert.ToString(RsEnc.Fields.Item("DOCNUM").Value).ToString()
                    BePedidoCompra.Posting_Description = RsEnc.Fields.Item("COMMENTS").Value.ToString()
                    BePedidoCompra.Product_Owner_Code = BeConfigEnc.IdPropietario
                    BePedidoCompra.Vendor_Invoice_No = RsEnc.Fields.Item("NUMATCARD").Value.ToString()
                    BePedidoCompra.Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Devolucion

                    If BePedidoCompra.Vendor_Invoice_No = "" Then
                        BePedidoCompra.Vendor_Invoice_No = BePedidoCompra.No.ToString()
                    End If

                    Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                    Dim query_det As String

                    query_det = "SELECT T0.ItemCode, 
                                T0.Dscription,
                                T0.Quantity, 
                                T0.Price, 
                                T0.LineTotal,
                                T0.VatSum, 
                                T0.DocEntry, 
                                T0.WhsCode, 
                                T0.OpenCreQty AS Cantidad_Pendiente, 
                                T0.BaseLine,
                                T0.LineNum, 
                                T1.U_Um_Prod AS Unidad_Medida 
                                FROM RRR1 T0 INNER JOIN OITM T1 ON T1.ItemCode = T0.ItemCode 
                                WHERE T0.DOCENTRY = '" & BePedidoCompra.No & "' " &
                                "AND T0.LINESTATUS = 'O' "

                    RsDet.DoQuery(query_det)

                    BePedidoCompra.Lineas_Detalle = New List(Of clsBeI_nav_ped_compra_det)

                    Dim vNoLineaBase As Integer = 0
                    Dim vNoLinea As Integer = 0

                    While RsDet.EoF = False

                        BePedidoDetWMS = New clsBeI_nav_ped_compra_det()
                        BePedidoDetWMS.NoEnc = RsDet.Fields.Item("DOCENTRY").Value.ToString()
                        BePedidoDetWMS.No = RsDet.Fields.Item("ITEMCODE").Value.ToString()

                        vNoLineaBase = IIf(IsDBNull(RsDet.Fields.Item("BASELINE").Value.ToString()), 0, RsDet.Fields.Item("BASELINE").Value.ToString())
                        vNoLinea = IIf(IsDBNull(RsDet.Fields.Item("LINENUM").Value.ToString()), 0, RsDet.Fields.Item("LINENUM").Value.ToString())

                        BePedidoDetWMS.Line_No = vNoLinea
                        BePedidoDetWMS.Planed_Receipt_Date = Date.Now()
                        BePedidoDetWMS.Quantity = Convert.ToDecimal(RsDet.Fields.Item("CANTIDAD_PENDIENTE").Value)
                        BePedidoDetWMS.Quantity_Received = 0
                        BePedidoDetWMS.Description = RsDet.Fields.Item("DSCRIPTION").Value.ToString()
                        BePedidoDetWMS.Unit_of_Measure_Code = (RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString())
                        BePedidoDetWMS.Type = 2
                        BePedidoDetWMS.Variant_Code = Nothing
                        BePedidoDetWMS.Location_Code = RsDet.Fields.Item("WHSCODE").Value.ToString()
                        BePedidoCompra.Lineas_Detalle.Add(BePedidoDetWMS)
                        NoLinea += 1
                        RsDet.MoveNext()

                    End While

                    lPedidosCompra.Add(BePedidoCompra)

                    RsEnc.MoveNext()

                End While

            End If

            Return lPedidosCompra

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Insertar_Solicitud_Devol_Cli_A_TOMWMS(ByRef lblprg As RichTextBox,
                                                          ByRef prg As System.Windows.Forms.ProgressBar,
                                                          Optional ByVal ForzarEjecucion As Boolean = False,
                                                          Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Solicitud_Devol_Cli_A_TOMWMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim DifCant As Double = 0
        Dim gBeOrdenCompra As clsBeTrans_oc_enc = Nothing
        Dim PedidoCompraExistente As clsBeTrans_oc_enc = Nothing
        Dim vContador As Integer = 0
        Dim vContadorLineasDet As Integer = 0
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim BeProductoBodega As New clsBeProducto_bodega
        Dim BePresentacion As New clsBeProducto_Presentacion
        Dim vDocumentosIngresoPendientesImportacion As Boolean = False

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Pedido compra") Then
                    Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc =0' 0'0' 0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = 0'0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Solicitud_Devolucion_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Solicitud_Devolucion_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            lTransInterface.Commit()

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)

            Actualizar_Progreso(lblprg, vbTab & String.Format(" -> Fin de proceso, tiempo transcurrido: {0} segundo(s)", difSegundos))

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS

            If VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            prg.Value = 0

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                              "",
                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                              BeConfigDet.Idnavconfigdet, CnnLog)

            lblprg.AppendText(String.Format("Error al insertar pedido de compra a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Private Function Inserta_Proveedor_Desde_SAP(ByVal pCodigo As String,
                                                 ByVal cnnLog As SqlConnection) As Boolean

        Inserta_Proveedor_Desde_SAP = False


        Dim BeProveedor As New clsBeProveedor
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim BeINavProveedor As New clsBeI_nav_proveedor

        Try

            BeINavProveedor = clsSyncSAPProveedor.Get_Proveedor_Devolucion_SAP(pCodigo)

            If Not BeINavProveedor Is Nothing Then

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID() + 1
                BeProveedor.Codigo = BeINavProveedor.No
                BeProveedor.Nombre = BeINavProveedor.Name
                BeProveedor.Telefono = BeINavProveedor.Phone_No
                BeProveedor.Nit = BeINavProveedor.VAT_Registratrion_No
                BeProveedor.Direccion = BeINavProveedor.Adress
                BeProveedor.Contacto = BeINavProveedor.Contact
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor)

                    VContadorBitacoraTOMWMS += 1

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID() + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.Insertar(BeProveedorBodega)

                    'Marcar_Enviado_SAP
                    clsSyncSAPProveedor.Marcar_Proveedor_Sincronizado_SAP(BeProveedor.Codigo)

                    Inserta_Proveedor_Desde_SAP = True

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, cnnLog)

                    Throw ex

                End Try

            End If


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Enviar_Entrada_Mercancia_Por_Estado_SAP_B1(ByVal beINavConfigEnc As clsBeI_nav_config_enc,
                                                               ByVal _Docentry As Integer,
                                                               ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                               ByVal beTransOCEnc As clsBeTrans_oc_enc,
                                                               ByVal codigoBodegaDestino As String,
                                                               ByRef lblPrg As RichTextBox,
                                                               ByRef prg As Windows.Forms.ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Value = 0
        prg.Visible = True

        Dim oOrderPurchase As Documents = Nothing
        Dim result As Boolean = False
        Dim NoLineaSAPEntrega As Integer = 0

        Try

            oOrderPurchase = DirectCast(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)

            If oOrderPurchase IsNot Nothing AndAlso oOrderPurchase.GetByKey(_Docentry) Then
                Procesar_Detalle_Ingreso(oOrderPurchase, lINavTransaccionesOut, lblPrg)
            End If

        Catch ex As Exception
            Throw
        Finally

            If oOrderPurchase IsNot Nothing Then Marshal.ReleaseComObject(oOrderPurchase)
            prg.Value = 0
            prg.Visible = False

        End Try

        Return result

    End Function

    Private Function Procesar_Detalle_Ingreso(ByVal oOrderPurchase As Documents,
                                              ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                              ByRef lblPrg As RichTextBox) As Boolean

        Dim cTrans As New clsTransaccion
        Dim oResultado As Integer = 0
        Dim oEntrega As Documents = Nothing
        Dim vRegistroEntregaPorLote As Boolean = False
        Dim NoLineaEntrega As Integer = 0 : Dim NoLineaEntregaLote As Integer = 0
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vCodigoBodegaERP As String = ""
        Dim clsTransaccion As New clsTransaccion

        Procesar_Detalle_Ingreso = False

        Try

            cTrans.Open_Connection()

            'Búsqueda de todos los estados de un código y línea sin importar el lote.
            Dim EstadosRecibidos = lINavTransaccionesOut.GroupBy(Function(x) x.Idproductoestado).
                                            Select(Function(g) New With {
                                                .Idproductoestado = g.Key,
                                                .Lotes = g.ToList(),
                                                .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                            }).ToList()


            For Each Estado In EstadosRecibidos

                Dim BeProductoEstado = clsLnProducto_estado.Get_Single_By_IdEstado(Estado.Idproductoestado,
                                                                                       cTrans.lConnection,
                                                                                       cTrans.lTransaction)

                If BeProductoEstado Is Nothing Then
                    Throw New Exception($"No está definido el estado del producto y no se puede inferir la bodega destino para IdProductoEstado: {Estado.Idproductoestado}")
                ElseIf String.IsNullOrWhiteSpace(BeProductoEstado.Codigo_Bodega_ERP) Then
                    Throw New Exception($"No está definido el código de bodega SAP para el producto, configure: Producto-Estado-Codigo_Bodega_ERP para el IdEstado: {Estado.Idproductoestado}")
                End If

                vCodigoBodegaERP = BeProductoEstado.Codigo_Bodega_ERP

                ' Crear la entrega para cada estado de la línea.
                oEntrega = oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes)
                If oEntrega Is Nothing Then
                    Throw New Exception("No se pudo crear el objeto de entrega.")
                End If

                Configurar_Cabecera_Entrega(oEntrega, oOrderPurchase)

                'Reset variables de control
                NoLineaEntrega = 0 : NoLineaEntregaLote = 0

                For j As Integer = 0 To oOrderPurchase.Lines.Count - 1

                    oOrderPurchase.Lines.SetCurrentLine(j)

                    Dim vCodigoProductoSAP As String = oOrderPurchase.Lines.ItemCode.ToString()
                    Dim vNoLineaOCSAP As Integer = oOrderPurchase.Lines.LineNum

                    Dim DistinctProductosLineas = lINavTransaccionesOut.
                    Where(Function(x) x.Idproductoestado = Estado.Idproductoestado _
                          AndAlso x.Codigo_producto = vCodigoProductoSAP _
                          AndAlso x.No_linea = vNoLineaOCSAP).
                    GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea}).
                    Select(Function(g) New With {
                        g.Key.Codigo_producto,
                        g.Key.No_linea,
                        .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                    }).ToList()

                    For Each ProductoIngreso In DistinctProductosLineas

                        Dim nuevaLineaEntrega As Boolean = (vCodigoAnterior <> ProductoIngreso.Codigo_producto)

                        If nuevaLineaEntrega Then

                            Dim vTipoImpuesto As String = oOrderPurchase.Lines.TaxCode

                            oEntrega.Lines.SetCurrentLine(NoLineaEntrega)
                            oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_PurchaseOrder)
                            oEntrega.Lines.BaseEntry = oOrderPurchase.DocEntry
                            oEntrega.Lines.ItemCode = ProductoIngreso.Codigo_producto
                            oEntrega.Lines.BaseLine = vNoLineaOCSAP
                            oEntrega.Lines.TaxCode = vTipoImpuesto
                            oEntrega.Lines.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                            oEntrega.Lines.Quantity = ProductoIngreso.Cantidad_Total
                            oEntrega.Lines.WarehouseCode = BeProductoEstado.Codigo_Bodega_ERP

                            ' Copiar gastos (expenses) desde el documento base
                            For k As Integer = 0 To oOrderPurchase.Expenses.Count - 1
                                oOrderPurchase.Expenses.SetCurrentLine(k)
                                oEntrega.Expenses.ExpenseCode = oOrderPurchase.Expenses.ExpenseCode
                                oEntrega.Expenses.LineTotal = oOrderPurchase.Expenses.LineTotal
                                oEntrega.Expenses.Add()
                            Next

                            vCodigoAnterior = oEntrega.Lines.ItemCode

                            Dim vControlPorLote As Boolean = clsLnProducto.Get_Control_Lote_By_Codigo(ProductoIngreso.Codigo_producto,
                                                                                                          cTrans.lConnection,
                                                                                                          cTrans.lTransaction)
                            If vControlPorLote Then

                                Dim LotesRecibidosPorEstado = lINavTransaccionesOut.
                                                Where(Function(x) x.Codigo_producto = ProductoIngreso.Codigo_producto _
                                                      AndAlso x.No_linea = ProductoIngreso.No_linea _
                                                      AndAlso x.Idproductoestado = Estado.Idproductoestado).
                                                GroupBy(Function(x) x.Idproductoestado).
                                                Select(Function(g) New With {
                                                    .Idproductoestado = g.Key,
                                                    .Lotes = g.GroupBy(Function(x) New With {Key x.Lote, Key x.Codigo_producto, Key x.No_linea, Key x.Fecha_vence, Key x.Lic_Plate}).
                                                               Select(Function(lg) New With {
                                                                   .Codigo_producto = lg.Key.Codigo_producto,
                                                                   .No_linea = lg.Key.No_linea,
                                                                   .Lote = lg.Key.Lote,
                                                                   .Fecha_vence = lg.Key.Fecha_vence,
                                                                   .Licencia = lg.Key.Lic_Plate,
                                                                   .CantidadTotal = lg.Sum(Function(x) x.Cantidad)
                                                               }).ToList()
                                                }).ToList()

                                If LotesRecibidosPorEstado.Count > 0 Then

                                    For Each LoteAgrupadoPorEstado In LotesRecibidosPorEstado

                                        NoLineaEntregaLote = 0

                                        For Each Lote In LoteAgrupadoPorEstado.Lotes

                                            oEntrega.Lines.BatchNumbers.SetCurrentLine(NoLineaEntregaLote)
                                            oEntrega.Lines.BatchNumbers.BaseLineNumber = NoLineaEntrega
                                            oEntrega.Lines.BatchNumbers.ItemCode = Lote.Codigo_producto
                                            oEntrega.Lines.BatchNumbers.BatchNumber = Lote.Lote
                                            oEntrega.Lines.BatchNumbers.Quantity = Lote.CantidadTotal
                                            oEntrega.Lines.BatchNumbers.ExpiryDate = Lote.Fecha_vence
                                            oEntrega.Lines.BatchNumbers.InternalSerialNumber = Lote.Licencia
                                            oEntrega.Lines.BatchNumbers.Location = BeProductoEstado.Codigo_Bodega_ERP
                                            oEntrega.Lines.BatchNumbers.AddmisionDate = Now
                                            oEntrega.Lines.BatchNumbers.Add()
                                            NoLineaEntregaLote += 1

                                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderPurchase.DocEntry _
                                                                                                  AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                                  AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                                                  AndAlso x.Lote = Lote.Lote _
                                                                                                  AndAlso x.Enviado = False)

                                            If Not Sublista_A_Actualizar Is Nothing Then
                                                If Sublista_A_Actualizar.Count > 0 Then
                                                    Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                                End If
                                            End If

                                        Next

                                    Next

                                Else

                                    Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderPurchase.DocEntry _
                                                                                          AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                          AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                                          AndAlso x.Enviado = False)

                                    If Not Sublista_A_Actualizar Is Nothing Then
                                        If Sublista_A_Actualizar.Count > 0 Then
                                            Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                        End If
                                    End If


                                End If

                            End If

                            oEntrega.Lines.Add() : NoLineaEntrega += 1

                        End If

                    Next 'DistinctProductosLineas                    

                Next

                ' Ejecuta oResultado = oEntrega.Add() después de configurar la línea
                oResultado = oEntrega.Add()
                If oResultado <> 0 Then
                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                Else

                    vRegistroEntregaPorLote = True

                    If Not Lista_A_Actualizar Is Nothing Then
                        If Lista_A_Actualizar.Count > 0 Then
                            For Each T In Lista_A_Actualizar
                                T.Enviado = True
                                T.Fec_mod = Now
                                clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(T)
                            Next
                        End If
                    End If

                    Procesar_Detalle_Ingreso = True

                    Actualizar_Progreso(lblPrg, vbTab & "Se creó la entrega en SAP para la bodega: " & vCodigoBodegaERP)

                End If

            Next 'EstadosRecibidos


        Catch ex As Exception
            Throw
        Finally
            If oEntrega IsNot Nothing Then
                Marshal.ReleaseComObject(oEntrega) ' Liberar el objeto de entrega si se creó
            End If
            cTrans.Close_Conection()
        End Try

    End Function

    Private Sub Configurar_Cabecera_Entrega(oEntrega As Documents, oOrderPurchase As Documents)
        oEntrega.CardCode = oOrderPurchase.CardCode
        oEntrega.DocDate = Date.Today
        oEntrega.DocObjectCode = BoObjectTypes.oPurchaseDeliveryNotes
    End Sub

End Class