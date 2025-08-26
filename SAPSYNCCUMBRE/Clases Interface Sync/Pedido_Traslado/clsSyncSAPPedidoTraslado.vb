Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports SAPbobsCOM

Public Class clsSyncSAPPedidoTraslado : Inherits clsInterfaceBase

    Implements IDisposable

    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

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

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))

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

    Public Function Procesar_Salidas_Desde_SAP(ByVal lblprg As RichTextBox,
                                               ByRef prg As Windows.Forms.ProgressBar,
                                               ByRef cnnLog As SqlConnection) As Boolean
        Procesar_Salidas_Desde_SAP = False

        Dim Resultado As String = ""

        Try

            Dim lPedidosTraslado As New List(Of clsBeI_nav_ped_traslado_enc)

            lPedidosTraslado = Get_Pedidos_Cliente_SAP()

            Dim BeClienteWMS As New clsBeCliente


            For Each PC In lPedidosTraslado

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido de Cliente: {0}/{1}{2}", PC.No, PC.Receipt_Document_Reference, vbNewLine))

                BeClienteWMS = clsLnCliente.Existe(PC.Transfer_to_Code)

                If BeClienteWMS Is Nothing Then
                    Inserta_Cliente_SAP(PC.Transfer_to_Code)
                    clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PC.Transfer_to_Code & " No existía en WMS y fue insertado.")
                    clsLnLog_error_wms.Agregar_Error("#IF_SAP_CLI_TRAS: El cliente: " & PC.Transfer_to_Code & " No existía en WMS y fue insertado.")
                End If

                clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(PC, lblprg)

                clsPublic.Actualizar_Progreso(lblprg, Resultado)

            Next

            Procesar_Salidas_Desde_SAP = True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Throw ex

        End Try

    End Function

    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    Private Function Get_Pedidos_Cliente_SAP() As List(Of clsBeI_nav_ped_traslado_enc)

        Get_Pedidos_Cliente_SAP = Nothing

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

                If BeConfigEnc Is Nothing Then
                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
                End If

                BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

                Dim oRecSet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                Dim SAP_OV As String = " SELECT  
                                        T0.DOCENTRY,
                                        T0.DOCNUM,
                                        T0.DOCDATE,  
                                        T0.CARDCODE,
                                        T0.CARDNAME,
                                        T0.DOCCUR,
                                        T0.DOCTOTAL,
                                        T0.JRNLMEMO,
                                        T0.CANCELED,T0.DOCSTATUS,  
                                        CASE WHEN T0.DOCTYPE = 'I'THEN 'ARTICULO'    
                                            ELSE 'SERVICIO'    
                                        END AS TIPO_ORDEN_VENTA,
                                        (SELECT TOP 1 D0.WhsCode FROM RDR1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode 
                                        WHERE D0.DocEntry = T0.DOCENTRY) AS BODEGA   
                                        FROM ORDR  T0 
                                        WHERE DOCSTATUS = 'O' 
                                        AND CreateDate >= '2020-10-09 00:00:00.000' 
                                        AND U_EnviadoWMS = 2 ORDER BY t0.DOCENTRY DESC"

                RsEnc.DoQuery(SAP_OV)

                Dim BePedidoWMS As clsBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc()

                While RsEnc.EoF = False

                    BePedidoWMS = New clsBeI_nav_ped_traslado_enc()
                    BePedidoWMS.No = RsEnc.Fields.Item("DOCENTRY").Value
                    BePedidoWMS.Posting_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BePedidoWMS.Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BePedidoWMS.Shipment_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BePedidoWMS.Status = 1
                    BePedidoWMS.Transfer_from_Code = RsEnc.Fields.Item("BODEGA").Value
                    BePedidoWMS.Transfer_from_Contact = "MI3_NAME"
                    BePedidoWMS.Transfer_from_Name = "MI3_NAME"
                    BePedidoWMS.Transfer_to_Code = RsEnc.Fields.Item("CARDCODE").Value
                    BePedidoWMS.Transfer_to_Contact = RsEnc.Fields.Item("CARDNAME").Value
                    BePedidoWMS.Transfer_to_Name = RsEnc.Fields.Item("CARDNAME").Value
                    BePedidoWMS.Product_Owner_Code = BePropietario.Codigo
                    BePedidoWMS.Receipt_Document_Reference = RsEnc.Fields.Item("DOCNUM").Value

                    Dim n As Integer = 1
                    Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                    Dim query_det As String

                    query_det = "SELECT 
                                 T0.ITEMCODE,
                                 T0.DSCRIPTION,
                                 T0.QUANTITY,
                                 T0.PRICE,
                                 T0.LINETOTAL,
                                 T0.VATSUM,
                                 T0.DOCENTRY,
                                 T0.WHSCODE,
                                 T1.U_Um_Prod AS UNIDAD_MEDIDA  
                                 FROM RDR1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode   
                                 WHERE T0.DOCENTRY = '" & BePedidoWMS.No & "'"

                    RsDet.DoQuery(query_det)

                    BePedidoWMS.Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)

                    While RsDet.EoF = False

                        BePedidoDetWMS = New clsBeI_nav_ped_traslado_det()
                        BePedidoDetWMS.NoEnc = BePedidoWMS.No
                        BePedidoDetWMS.No = clsLnTrans_pe_det.MaxID() + 1
                        BePedidoDetWMS.Item_No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
                        BePedidoDetWMS.Line_No = n
                        BePedidoDetWMS.Shipment_Date = Date.Now
                        BePedidoDetWMS.Quantity = Convert.ToDecimal(RsDet.Fields.Item("QUANTITY").Value)
                        BePedidoDetWMS.Description = RsDet.Fields.Item("dscription").Value.ToString()
                        BePedidoDetWMS.Unit_of_Measure_Code = RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()
                        BePedidoDetWMS.Status = 1
                        BePedidoDetWMS.Variant_Code = Nothing
                        BePedidoDetWMS.Transfer_to_CodeField = RsDet.Fields.Item("WHSCODE").Value.ToString()
                        BePedidoDetWMS.Price = Convert.ToDouble(RsDet.Fields.Item("PRICE").Value)
                        BePedidoWMS.Lineas_Detalle.Add(BePedidoDetWMS)

                        n += 1

                        RsDet.MoveNext()

                    End While

                    lPedidosCliente.Add(BePedidoWMS)

                    RsEnc.MoveNext()

                End While

            End If

            Return lPedidosCliente

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Importar_Pedido_Cliente_SAP(ByRef lblprg As RichTextBox,
                                                ByRef prg As Windows.Forms.ProgressBar,
                                                Optional ByVal ForzarEjecucion As Boolean = False,
                                                Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean
        Importar_Pedido_Cliente_SAP = False

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Pedido traslado") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Procesar_Salidas_Desde_SAP(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Procesar_Salidas_Desde_SAP(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                        "",
                                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                                        BeConfigDet.Idnavconfigdet,
                                                        CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de traslado a tabla de TOMWMS: {1} {0} {1}", ex.Message, vbNewLine))
            Throw ex
        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
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

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})
                Dim Enviado_A_Erp As Boolean = False

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        If Enviar_Entrega_Mercancia_OV_SAP(PT.No_pedido, lTransaccionesSalidaSingle, lblprg, prg) Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc,
                                                                                  True,
                                                                                  BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                            PT.No_pedido,
                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                            BeConfigDet.Idnavconfigdet,
                                                                            CnnLog)

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

            Conectar_A_SAP(oCompany, False, lRetCode, sErrMsg)

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

                    oEntrega.CardCode = oOrderSales.CardCode
                    oEntrega.DocDate = Date.Today
                    oEntrega.DocObjectCode = BoObjectTypes.oDeliveryNotes

                    For j As Integer = 0 To oOrderSales.Lines.Count - 1

                        oOrderSales.Lines.SetCurrentLine(j)

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", oOrderSales.Lines.ItemCode.ToString()))

                        Dim BeInavTransaccioensOut As clsBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out()
                        BeInavTransaccioensOut = lINav_Transaccioens_Out.Find(Function(x) x.Codigo_producto = oOrderSales.Lines.ItemCode.ToString())

                        If Not BeInavTransaccioensOut Is Nothing Then

                            If Not oOrderSales.Lines.LineStatus = BoStatus.bost_Close Then

                                If BeInavTransaccioensOut.Cantidad <= oOrderSales.Lines.Quantity Then

                                    oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_Order)
                                    oEntrega.Lines.ItemCode = oOrderSales.Lines.ItemCode
                                    oEntrega.Lines.BaseEntry = _Docentry
                                    oEntrega.Lines.BaseLine = BaseLine
                                    oEntrega.Lines.Quantity = BeInavTransaccioensOut.Cantidad
                                    oEntrega.Lines.Add()

                                Else
                                    Throw New Exception("WMS está intentando generar una entrega por: " & BeInavTransaccioensOut.Cantidad & " en una línea de SAP para el material: " & oOrderSales.Lines.ItemCode & " que refleja una cantidad de: " & BeInavTransaccioensOut.Cantidad & " probablemente esto no sea posible.")
                                End If

                            Else
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("El Producto: {0} ya fue completado. ", oOrderSales.Lines.ItemCode.ToString()))
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

                        clsLnLog_error_wms.Agregar_Error("#IF_SAP_TRAS_ENV: Se envió el traslado de mercancía: " & _Docentry)

                    End If

                End If

            End If

            Return True

        Catch errMsg As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar entrada de mercancía a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw errMsg

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

            Dim oOrderSales As Documents
            oOrderSales = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)

            oOrderSales = oCompany.GetBusinessObject(BoObjectTypes.oOrders)

            If oOrderSales.Lines.GetByKey(itemcode) Then
                Estado_Linea = oOrderSales.Lines.LineStatus
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Inserta_Cliente_SAP(ByVal pCodigo As String) As Boolean

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

                query_sap = "SELECT top 1 T0.CARDCODE AS CODIGO,
                             T0.CARDNAME AS NOMBRE_COMERCIAL,
                             T0.Phone1, 'TEST' AS CONTACTO ,
                             T0.u_nit AS NIT, 
                             T0.Address AS DIRECCION, 
                             T0.E_Mail FROM OCRD T0 
                             WHERE T0.CARDTYPE = 'C'  
                             AND (t0.CARDCODE)= '" & pCodigo & "'"


                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeCliente As New clsBeCliente

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
            If oCompany.Connected() Then
                oCompany.Disconnect()
            End If
        End Try

    End Function

End Class
