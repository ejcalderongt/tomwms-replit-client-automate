Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSAPSPedidoCliente : Inherits clsInterfaceBase

    Implements IDisposable

    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0
    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Private Function Get_Pedidos_Cliente_SAP(Optional ByVal pPedidoCliente As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

        Get_Pedidos_Cliente_SAP = Nothing

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                If BeConfigEnc Is Nothing Then
                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
                End If

                BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

                Dim oRecSet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                Dim SAP_OV As String = "SELECT 
                                        T0.DocEntry,
                                        T0.DocNum,
                                        T0.DocDate,
                                        T0.CardCode AS CARDCODE,
                                        T0.CardName AS CARDNAME,
                                        T0.DocCur,
                                        T0.DocTotal,
                                        T0.JrnlMemo,
                                        T0.Canceled,
                                        T0.DocStatus,
                                        CASE 
                                            WHEN T0.DocType = 'I' THEN 'ARTICULO'    
                                            ELSE 'SERVICIO'    
                                        END AS TIPO_ORDEN_VENTA,
                                        (SELECT TOP 1 D0.WhsCode 
                                            FROM RDR1 D0 
                                            WHERE D0.DocEntry = T0.DocEntry) AS BODEGA,
                                        T0.Comments,
                                        T0.NumAtCard,
                                        ISNULL(T0.U_WMS_manufactura,0) U_WMS_manufactura,
                                        T0.Address2, T0.Comments
                                    FROM ORDR T0 
                                    WHERE T0.DocStatus = 'O' 
                                    AND T0.CreateDate >= '2024-01-01 00:00:00.000' 
                                    AND T0.U_EnviadoWMS = 2 " &
                                    IIf(pPedidoCliente <> "", " AND T0.DocEntry  = " & pPedidoCliente, "") & "                                    
                                    ORDER BY T0.DocEntry DESC"

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
                    BePedidoWMS.Manufacturing_Process = RsEnc.Fields.Item("U_WMS_manufactura").Value
                    BePedidoWMS.Document_Type = tTipoDocumentoSalida.Pedido_De_Cliente
                    BePedidoWMS.Address = RsEnc.Fields.Item("Address2").Value
                    BePedidoWMS.Comments = RsEnc.Fields.Item("Comments").Value

                    Dim n As Integer = 1
                    Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                    Dim query_det As String

                    query_det = "SELECT 
                                 T0.LineNum, 
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
                        BePedidoDetWMS.Line_No = RsDet.Fields.Item("LINENUM").Value.ToString()
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

    Public Function Procesar_Pedidos_Cliente_SAP(ByVal lblprg As RichTextBox,
                                                 ByRef prg As Windows.Forms.ProgressBar,
                                                 ByRef cnnLog As SqlConnection,
                                                 Optional pPedidoCliente As String = "") As Boolean
        Procesar_Pedidos_Cliente_SAP = False

        Dim Resultado As String = ""

        Try

            Dim lPedidosTrasladoSAP As New List(Of clsBeI_nav_ped_traslado_enc)

            lPedidosTrasladoSAP = Get_Pedidos_Cliente_SAP(pPedidoCliente)

            Dim BeClienteWMS As New clsBeCliente

            If lPedidosTrasladoSAP IsNot Nothing Then

                If lPedidosTrasladoSAP.Count = 0 Then

                    If pPedidoCliente <> "" Then
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay pedidos de cliente pendientes de importar con el No.: {0} {1}", pPedidoCliente, vbNewLine))
                    Else
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay pedidos de cliente pendientes de importar {0}", vbNewLine))
                    End If

                Else

                    For Each PedidoClienteSAP In lPedidosTrasladoSAP

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido de Cliente (ORDR) : {0}/{1}{2}", PedidoClienteSAP.No, PedidoClienteSAP.Receipt_Document_Reference, vbNewLine))

                        BeClienteWMS = clsLnCliente.Existe(PedidoClienteSAP.Transfer_to_Code)

                        If BeClienteWMS Is Nothing Then

                            If Inserta_Cliente_SAP(PedidoClienteSAP.Transfer_to_Code, lblprg) Then
                                clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No existía en WMS y fue insertado.")
                            End If

                        End If

                        Dim BePedidoEncResult As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(PedidoClienteSAP,
                                                                                                                                               lblprg)

                        If Not BePedidoEncResult Is Nothing Then

                            BePedidoEncResult.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(BePedidoEncResult.IdPedidoEnc)

                            Marcar_Pedido_Cliente_Sincronizado_SAP(PedidoClienteSAP.No, Estado_Enviado_SAP.Enviado, lblprg)

                            If PedidoClienteSAP.Manufacturing_Process <> 0 Then
                                '#EJC202404011228: Insertar tarea de manufactura.
                                clsLnTrans_manufactura_enc.Insertar_Manufactura_Por_Defecto(PedidoClienteSAP,
                                                                                            BePedidoEncResult,
                                                                                            BeConfigEnc)
                            End If

                        End If

                        clsPublic.Actualizar_Progreso(lblprg, Resultado)

                    Next

                End If

            End If

            Procesar_Pedidos_Cliente_SAP = True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Throw ex

        End Try

    End Function

    Public Function Importar_Pedido_Cliente_SAP(ByRef lblprg As RichTextBox,
                                                ByRef prg As Windows.Forms.ProgressBar,
                                                Optional ByVal ForzarEjecucion As Boolean = False,
                                                Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                Optional ByVal pPedidoCliente As String = "") As Boolean
        Importar_Pedido_Cliente_SAP = False

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Resultado As String = ""

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

                If Not Procesar_Pedidos_Cliente_SAP(lblprg, prg, CnnLog, pPedidoCliente) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Procesar_Pedidos_Cliente_SAP(lblprg, prg, CnnLog, pPedidoCliente) Then
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

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de cliente a tabla de TOMWMS: {1} {0} {1}", ex.Message, vbNewLine))

            Throw ex

        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
        End Try

    End Function

    Public Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                              ByRef prg As Windows.Forms.ProgressBar,
                                              ByVal pTipo As tTipoDocumentoSalida)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Sl As New clsSyncLotes()
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_Referencia_TipoDoc(PT.No_pedido, pTipo)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        Try

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando documento : {0}", PT.No_pedido))

                            If Enviar_Entrega_Mercancia_OV_SAP3(PT.No_pedido, lTransaccionesSalidaSingle, lblprg, prg) Then

                                Try

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Registros de salida enviados documento: {0}", PT.No_pedido))

                                    clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                                Catch ex As Exception

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                               PT.No_pedido,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                                End Try

                            End If

                        Catch ex As Exception

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                       PT.No_pedido,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                        End Try


                    End If

                Next

            Else

                clsPublic.Actualizar_Progreso(lblprg, "MSG_240117: No hay transacciones para enviar.")

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

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

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

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", oOrderSales.Lines.ItemCode.ToString()))

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

                    End If

                End If

            End If

            Return True

        Catch errMsg As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, errMsg.Message)

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

    Private Function Inserta_Cliente_SAP(ByVal pCodigo As String, ByVal lblprg As RichTextBox) As Boolean

        Inserta_Cliente_SAP = False

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios
        Dim clsTransaccion As New clsTransaccion
        Dim lFamilias As New List(Of clsBeProducto_familia)
        Dim lClasificacion As New List(Of clsBeProducto_clasificacion)

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

                clsTransaccion.Open_Connection()

                Dim BeCliente As New clsBeCliente

                While Not rs.EoF

                    BeCliente.IdCliente = clsLnCliente.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
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
                    BeCliente.User_agr = BeConfigEnc.IdUsuario
                    BeCliente.Fec_agr = Now
                    BeCliente.User_mod = "MI3"
                    BeCliente.Fec_mod = Now

                    clsLnCliente.Insertar(BeCliente, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    Dim BeClienteBodega As New clsBeCliente_bodega
                    BeClienteBodega = New clsBeCliente_bodega()
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now
                    BeClienteBodega.Cliente = BeCliente

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                clsTransaccion.lConnection,
                                                                clsTransaccion.lTransaction)


                    clsPublic.Actualizar_Progreso(lblprg, "Días vida por defecto clientes perecederos: " & BeConfigEnc.Dias_Vida_Defecto_Perecederos)

                    '#EJC202303031646: Insertar días por defecto para clientes.
                    If BeConfigEnc.Dias_Vida_Defecto_Perecederos > 0 Then

                        lFamilias = clsLnProducto_familia.Get_All_Filtro(True,
                                                                         BeConfigEnc.IdPropietario,
                                                                         clsTransaccion.lConnection,
                                                                         clsTransaccion.lTransaction)

                        clsPublic.Actualizar_Progreso(lblprg, "Lista de familias: " & lFamilias.Count)


                        lClasificacion = clsLnProducto_clasificacion.Get_All_Filtro(True,
                                                                                    BeConfigEnc.IdPropietario,
                                                                                    clsTransaccion.lConnection,
                                                                                    clsTransaccion.lTransaction)

                        clsPublic.Actualizar_Progreso(lblprg, "Lista de clasificaciones: " & lClasificacion.Count)

                        If Not lFamilias Is Nothing AndAlso Not lClasificacion Is Nothing Then

                            Dim BeTiempoCliente As New clsBeCliente_tiempos

                            For Each F In lFamilias

                                For Each C In lClasificacion

                                    BeTiempoCliente = New clsBeCliente_tiempos()
                                    BeTiempoCliente.IdTiempoCliente = clsLnCliente_tiempos.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                                    BeTiempoCliente.IdCliente = BeCliente.IdCliente
                                    BeTiempoCliente.IdFamilia = F.IdFamilia
                                    BeTiempoCliente.IdClasificacion = C.IdClasificacion
                                    BeTiempoCliente.Dias_Local = BeConfigEnc.Dias_Vida_Defecto_Perecederos
                                    BeTiempoCliente.Dias_Exterior = BeConfigEnc.Dias_Vida_Defecto_Perecederos
                                    BeTiempoCliente.User_agr = BeConfigEnc.IdUsuario
                                    BeTiempoCliente.User_mod = BeConfigEnc.IdUsuario
                                    BeTiempoCliente.Fec_agr = Now
                                    BeTiempoCliente.Fec_mod = Now
                                    BeTiempoCliente.Activo = True
                                    clsLnCliente_tiempos.Insertar(BeTiempoCliente, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                                Next

                            Next

                        End If

                    End If


                    clsTransaccion.Commit_Transaction()

                    Dim oBusinessPartnerSBO As BusinessPartners = CType(oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners), BusinessPartners)

                    If oBusinessPartnerSBO.GetByKey(pCodigo) Then
                        oBusinessPartnerSBO.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                        oBusinessPartnerSBO.Update()
                    End If

                    Inserta_Cliente_SAP = True

                    rs.MoveNext()

                End While

            End If

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception("No se pudo insertar el cliente nuevo proviniente de SAP: " & ex.Message)
        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Class clsBeTmpInvSAP
        Public Property CodigoProducto As String = ""
        Public Property Cantidad As Double = 0
        Public Property Lote As String = ""

    End Class

    Public Function Enviar_Entrega_Mercancia_OV_SAP2(ByVal _Docentry As Integer,
                                                     ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                     ByRef lblprg As RichTextBox,
                                                     ByRef prg As Windows.Forms.ProgressBar) As Boolean


        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vLineaAnterior As Integer = -1
        Dim vCodigoBodegaERP As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim NoLineaEntrega As Integer = 0 : Dim NoLineaEntregaLote As Integer = 0
        Dim clsTransaccion As New clsTransaccion
        Dim BePedidoWMS As New clsBeTrans_pe_enc
        Dim lBeInvSAP As New List(Of clsBeTmpInvSAP)
        Dim lBeInvSAPLotes As New List(Of clsBeTmpInvSAP)

        Enviar_Entrega_Mercancia_OV_SAP2 = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

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
                Dim LotesSAP As New clsSyncLotes
                Dim vExistenciaLoteSAP As Double = 0
                Dim vTotalDisponibleCodigoSAP As Double = 0
                Dim vTotalItemWMS As Double = 0
                Dim vTotalItemWMSP As Double = 0
                Dim vTotalReservadoSAPPorLinea As Double = 0

                oEntrega = CType(oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes), Documents)
                oOrderSales = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)

                '#EJC202406210: Forzar conexión SAP.
                Try
                    oOrderSales.GetByKey(_Docentry)
                Catch ex As Exception
                    If ex.Message.Contains("You are not connected to a company") Then
                        clsPublic.Actualizar_Progreso(lblprg, "Reintentando conexión a SAP.")
                        Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg, True)
                    End If
                End Try

                If oOrderSales.GetByKey(_Docentry) Then

                    oEntrega.CardCode = oOrderSales.CardCode
                    oEntrega.DocDate = Date.Today
                    oEntrega.DocObjectCode = BoObjectTypes.oDeliveryNotes

                    NoLineaEntrega = 0 : NoLineaEntregaLote = 0

                    clsTransaccion.Open_Connection()

                    BePedidoWMS = clsLnTrans_pe_enc.GetSingle(lINavTransaccionesOut.FirstOrDefault.Idpedidoenc,
                                                              clsTransaccion.lConnection,
                                                              clsTransaccion.lTransaction)

                    For j As Integer = 0 To oOrderSales.Lines.Count - 1

                        oOrderSales.Lines.SetCurrentLine(j)

                        Dim vCodigoProductoSAP As String = oOrderSales.Lines.ItemCode.ToString()
                        Dim vNoLineaOCSAP As Integer = oOrderSales.Lines.LineNum

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Documento:{1} - Procesando Producto: {0} ", vCodigoProductoSAP, _Docentry))

                        Dim DistinctProductosLineas = lINavTransaccionesOut.Where(Function(x) x.Codigo_producto = vCodigoProductoSAP _
                                                                          AndAlso x.No_linea = vNoLineaOCSAP).
                                                                    GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea}).
                                                                    Select(Function(g) New With {
                                                                        g.Key.Codigo_producto,
                                                                        g.Key.No_linea,
                                                                        .Cantidad_Total = g.Sum(Function(x) x.Cantidad),
                                                                        .Cantidad_Pendiente = g.Sum(Function(x) x.Cantidad_Pendiente)
                                                                    }).ToList()

                        If DistinctProductosLineas.Any() Then

                            For Each ProductoSalida In DistinctProductosLineas

                                If Not oOrderSales.Lines.LineStatus = BoStatus.bost_Close Then

                                    If ProductoSalida.Cantidad_Total <= oOrderSales.Lines.Quantity Then

                                        Dim nuevaLineaEntrega As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_producto OrElse vLineaAnterior <> ProductoSalida.No_linea)

                                        If nuevaLineaEntrega Then

                                            vCodigoAnterior = oEntrega.Lines.ItemCode
                                            vLineaAnterior = oEntrega.Lines.LineNum

                                            Dim ProductoSalidaSinEstado = lINavTransaccionesOut.
                                                                            Where(Function(x) x.Codigo_producto = ProductoSalida.Codigo_producto).
                                                                            GroupBy(Function(x) x.Idproductoestado).
                                                                            Select(Function(g) New With {
                                                                                .Idproductoestado = g.Key,
                                                                                .Lotes = g.GroupBy(Function(x) New With {Key x.Codigo_producto}).
                                                                                           Select(Function(lg) New With {
                                                                                               .Codigo_producto = lg.Key.Codigo_producto,
                                                                                               .CantidadTotal = lg.Sum(Function(x) x.Cantidad),
                                                                                               .Cantidad_Pendiente = lg.Sum(Function(x) x.Cantidad_Pendiente)
                                                                                           }).ToList()
                                                                            }).ToList()

                                            vTotalDisponibleCodigoSAP = LotesSAP.Get_Total_Lote_SAP(oCompany,
                                                                                                    oOrderSales.Lines.ItemCode,
                                                                                                    oOrderSales.Lines.WarehouseCode)

                                            If Not lBeInvSAP Is Nothing Then
                                                If lBeInvSAP.Count > 0 Then
                                                    If lBeInvSAP.FindIndex(Function(x) x.CodigoProducto = oOrderSales.Lines.ItemCode) = -1 Then
                                                        Dim beInvTmpSap As New clsBeTmpInvSAP
                                                        beInvTmpSap.CodigoProducto = oOrderSales.Lines.ItemCode
                                                        beInvTmpSap.Cantidad = vTotalDisponibleCodigoSAP
                                                        lBeInvSAP.Add(beInvTmpSap)
                                                    End If
                                                Else
                                                    Dim beInvTmpSap As New clsBeTmpInvSAP
                                                    beInvTmpSap.CodigoProducto = oOrderSales.Lines.ItemCode
                                                    beInvTmpSap.Cantidad = vTotalDisponibleCodigoSAP
                                                    lBeInvSAP.Add(beInvTmpSap)
                                                End If
                                            End If


                                            Dim vControlPorLote As Boolean = clsLnProducto.Get_Control_Lote_By_Codigo(ProductoSalida.Codigo_producto,
                                                                                                                      clsTransaccion.lConnection,
                                                                                                                      clsTransaccion.lTransaction)
                                            If vControlPorLote Then

                                                Dim LotesRecibidosPorEstado = lINavTransaccionesOut.
                                                        Where(Function(x) x.Codigo_producto = ProductoSalida.Codigo_producto _
                                                              AndAlso x.No_linea = ProductoSalida.No_linea).
                                                        GroupBy(Function(x) x.Idproductoestado).
                                                        Select(Function(g) New With {
                                                            .Idproductoestado = g.Key,
                                                            .Lotes = g.GroupBy(Function(x) New With {Key x.Lote, Key x.Codigo_producto, Key x.No_linea, Key x.Fecha_vence}).
                                                                       Select(Function(lg) New With {
                                                                           .Codigo_producto = lg.Key.Codigo_producto,
                                                                           .No_linea = lg.Key.No_linea,
                                                                           .Lote = lg.Key.Lote,
                                                                           .Fecha_vence = lg.Key.Fecha_vence,
                                                                           .CantidadTotal = lg.Sum(Function(x) x.Cantidad),
                                                                           .Cantidad_Pendiente = lg.Sum(Function(x) x.Cantidad_Pendiente)
                                                                       }).ToList()
                                                        }).ToList()

                                                If LotesRecibidosPorEstado.Count > 0 Then

                                                    ' Aquí se debe obtener la cantidad disponible a enviar a sap en base a la cantidad requeridad por lote.
                                                    'es decir la cantidad de la línea de la entrega debe ser igual a la cantidad a enviar por lotes.
                                                    Dim vTotalAEnviarPorLote As Double = 19

                                                    For Each LoteAgrupadoPorEstado In LotesRecibidosPorEstado

                                                        NoLineaEntregaLote = 0

                                                        vTotalItemWMS = (From estado In ProductoSalidaSinEstado
                                                                         From lote In estado.Lotes
                                                                         Where lote.Codigo_producto = LoteAgrupadoPorEstado.Lotes.FirstOrDefault.Codigo_producto
                                                                         Select lote.CantidadTotal).Sum()

                                                        vTotalItemWMSP = (From estado In ProductoSalidaSinEstado
                                                                          From lote In estado.Lotes
                                                                          Where lote.Codigo_producto = LoteAgrupadoPorEstado.Lotes.FirstOrDefault.Codigo_producto
                                                                          Select lote.Cantidad_Pendiente).Sum()

                                                        Dim BeInvExistenteSAP = lBeInvSAP.Find(Function(x) x.CodigoProducto = LoteAgrupadoPorEstado.Lotes.FirstOrDefault.Codigo_producto)
                                                        vTotalDisponibleCodigoSAP = BeInvExistenteSAP.Cantidad

                                                        vTotalReservadoSAPPorLinea = 0

                                                        '#EJC: Basado en la premisa que WMS siempre tiene más que SAP.
                                                        While vTotalDisponibleCodigoSAP > 0

                                                            For Each Lote In LoteAgrupadoPorEstado.Lotes

                                                                vExistenciaLoteSAP = LotesSAP.Existe_Lote_SAP(oCompany,
                                                                                                              oOrderSales.Lines.ItemCode,
                                                                                                              Lote.Lote,
                                                                                                              oOrderSales.Lines.WarehouseCode)

                                                                '#EJC202409041552: Si SAP no tiene existencia de ese lote, no enviar la línea.
                                                                If Not vExistenciaLoteSAP = 0 AndAlso vExistenciaLoteSAP >= Lote.CantidadTotal Then

                                                                    If NoLineaEntregaLote = 0 Then
                                                                        oEntrega.Lines.SetCurrentLine(NoLineaEntrega)
                                                                        oEntrega.Lines.BaseType = BoObjectTypes.oOrders
                                                                        oEntrega.Lines.ItemCode = oOrderSales.Lines.ItemCode
                                                                        oEntrega.Lines.BaseEntry = _Docentry
                                                                        oEntrega.Lines.BaseLine = vNoLineaOCSAP
                                                                        oEntrega.Lines.Quantity = vTotalAEnviarPorLote
                                                                    End If

                                                                    clsPublic.Actualizar_Progreso(lblprg, " ItemCode: " & oOrderSales.Lines.ItemCode & " Lote: " & Lote.Lote & " Existencia SAP: " & vExistenciaLoteSAP & " Solicitado WMS: " & Lote.CantidadTotal)

                                                                    'oEntrega.Lines.BatchNumbers.SetCurrentLine(NoLineaEntregaLote)
                                                                    oEntrega.Lines.BatchNumbers.BatchNumber = Lote.Lote
                                                                    oEntrega.Lines.BatchNumbers.Quantity = Lote.CantidadTotal
                                                                    oEntrega.Lines.BatchNumbers.Add()
                                                                    NoLineaEntregaLote += 1

                                                                    vTotalDisponibleCodigoSAP -= Lote.CantidadTotal
                                                                    BeInvExistenteSAP.Cantidad -= Lote.CantidadTotal
                                                                    vTotalReservadoSAPPorLinea += Lote.CantidadTotal

                                                                    Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderSales.DocEntry _
                                                                                                                          AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                                                          AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                                                                          AndAlso x.Lote = Lote.Lote _
                                                                                                                          AndAlso x.Enviado = False)

                                                                    If Not Sublista_A_Actualizar Is Nothing Then
                                                                        If Sublista_A_Actualizar.Count > 0 Then

                                                                            For Each O In Sublista_A_Actualizar
                                                                                O.Cantidad_Enviada = O.Cantidad
                                                                                O.Cantidad_Pendiente = 0
                                                                                O.Enviado = True
                                                                                O.Auditar = False
                                                                            Next

                                                                            Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)

                                                                        End If
                                                                    End If


                                                                    If vTotalReservadoSAPPorLinea > 0 Then
                                                                        oEntrega.Lines.Add() : NoLineaEntrega += 1
                                                                    End If

                                                                    '#EJC20240911: Reserva parcial en SAP.
                                                                ElseIf Not vExistenciaLoteSAP = 0 AndAlso vExistenciaLoteSAP < Lote.CantidadTotal Then

                                                                    If NoLineaEntregaLote = 0 Then
                                                                        oEntrega.Lines.SetCurrentLine(NoLineaEntrega)
                                                                        oEntrega.Lines.BaseType = BoObjectTypes.oOrders
                                                                        oEntrega.Lines.ItemCode = oOrderSales.Lines.ItemCode
                                                                        oEntrega.Lines.BaseEntry = _Docentry
                                                                        oEntrega.Lines.BaseLine = vNoLineaOCSAP
                                                                        oEntrega.Lines.Quantity = vTotalAEnviarPorLote
                                                                    End If

                                                                    clsPublic.Actualizar_Progreso(lblprg, " ItemCode: " & oOrderSales.Lines.ItemCode & " Lote: " & Lote.Lote & " Existencia SAP: " & vExistenciaLoteSAP & " Solicitado WMS: " & Lote.CantidadTotal)

                                                                    oEntrega.Lines.BatchNumbers.SetCurrentLine(NoLineaEntregaLote)
                                                                    oEntrega.Lines.BatchNumbers.BatchNumber = Lote.Lote
                                                                    oEntrega.Lines.BatchNumbers.Quantity = vExistenciaLoteSAP
                                                                    oEntrega.Lines.BatchNumbers.Add()
                                                                    NoLineaEntregaLote += 1

                                                                    vTotalDisponibleCodigoSAP -= vExistenciaLoteSAP
                                                                    BeInvExistenteSAP.Cantidad -= vExistenciaLoteSAP
                                                                    vTotalReservadoSAPPorLinea += vExistenciaLoteSAP

                                                                    Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderSales.DocEntry _
                                                                                                                          AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                                                          AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                                                                          AndAlso x.Lote = Lote.Lote _
                                                                                                                          AndAlso x.Enviado = False)

                                                                    If Not Sublista_A_Actualizar Is Nothing Then
                                                                        If Sublista_A_Actualizar.Count > 0 Then

                                                                            For Each O In Sublista_A_Actualizar
                                                                                O.Cantidad_Enviada = vExistenciaLoteSAP
                                                                                O.Cantidad_Pendiente = Lote.CantidadTotal - vExistenciaLoteSAP
                                                                                O.Enviado = False
                                                                                O.Auditar = True
                                                                            Next

                                                                            Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                                                        End If
                                                                    End If

                                                                    If vTotalReservadoSAPPorLinea > 0 Then
                                                                        oEntrega.Lines.Add() : NoLineaEntrega += 1
                                                                    End If

                                                                Else

                                                                    Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderSales.DocEntry _
                                                                                                                          AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                                                          AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                                                                          AndAlso x.Lote = Lote.Lote _
                                                                                                                          AndAlso x.Enviado = False)

                                                                    For Each O In Sublista_A_Actualizar
                                                                        O.Cantidad_Enviada = 0
                                                                        O.Cantidad_Pendiente = O.Cantidad
                                                                        O.Enviado = False
                                                                        O.Auditar = True
                                                                    Next

                                                                    Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)

                                                                    Dim vMensaje As String = $"#MSG_240916_NREQ: WMS ha detectado que posiblemente el lote:'{Lote.Lote}' para el producto: '{oOrderSales.Lines.ItemCode}' no tenga existencia SAP y esto genere un error al crear el documento."

                                                                    clsLnLog_error_wms.Agregar_Error(1,
                                                                                                     BePedidoWMS.IdBodega,
                                                                                                     vMensaje,
                                                                                                     BePedidoWMS.IdPedidoEnc,
                                                                                                     BePedidoWMS.IdPickingEnc,
                                                                                                     0,
                                                                                                     BePedidoWMS.User_agr)

                                                                    clsPublic.Actualizar_Progreso(lblprg, vMensaje)

                                                                    Continue For

                                                                End If

                                                            Next

                                                            Exit While

                                                        End While

                                                    Next

                                                End If

                                            End If

                                        End If

                                    Else
                                        Throw New Exception("WMS está intentando generar una entrega por: " & ProductoSalida.Cantidad_Total &
                                                            " en una línea de SAP para el material: " & oOrderSales.Lines.ItemCode &
                                                            " que refleja una cantidad de: " & oOrderSales.Lines.Quantity & " probablemente esto no sea posible.")
                                    End If

                                Else

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("El Producto: {0} ya fue completado. ", oOrderSales.Lines.ItemCode.ToString()))

                                End If

                            Next 'DistinctProductosLineas

                            vAgregarEntrega = NoLineaEntrega > 0

                        End If

                    Next

                    If vAgregarEntrega Then

                        Dim U_Distribuidor As String = "U_Distribuidor"
                        oEntrega.UserFields.Fields.Item(U_Distribuidor).Value = oOrderSales.UserFields.Fields.Item(U_Distribuidor).Value

                        '#EJC20240709: Marcar la entrega en SAP para que se sepa que fue generada por WMS.
                        Dim U_SEGUNDAREFERENCIA As String = "U_SEGUNDAREFERENCIA"
                        oEntrega.UserFields.Fields.Item(U_SEGUNDAREFERENCIA).Value = "WMS_NO: " & lINavTransaccionesOut.FirstOrDefault.Idpedidoenc

                        Dim oResultado As Integer
                        oResultado = oEntrega.Add()

                        If oResultado <> 0 Then
                            Dim errMsg = oCompany.GetLastErrorDescription()
                            Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                        Else

                            Dim vTrasladoDocEntry As Integer = 0

                            ' Obtener el DocEntry del traslado generado
                            Dim newObjectCode As String = ""
                            oCompany.GetNewObjectCode(newObjectCode)

                            If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                                Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                            End If

                            BePedidoWMS.No_Documento_Externo = vTrasladoDocEntry

                            clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(BePedidoWMS, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                            Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Estado_Enviado(Lista_A_Actualizar)

                            If IResult = 0 Then
                                Throw New Exception("Se envió la entrada de mercancía a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                            End If

                            clsPublic.Actualizar_Progreso(lblprg, "Se creó el documento en SAP: " & vTrasladoDocEntry)
                            clsPublic.Actualizar_Progreso(lblprg, "Fin: " & Now.ToString("dd/MMM/yyyy HH:mm:ss"))

                            Enviar_Entrega_Mercancia_OV_SAP2 = True

                        End If

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "No hay productos para enviar a SAP.")
                    End If

                    clsTransaccion.Commit_Transaction()

                End If

            End If

        Catch errMsg As Exception

            clsTransaccion.RollBack_Transaction()

            If errMsg.Message.Contains("You are not connected to a company") Then
                clsPublic.Actualizar_Progreso(lblprg, "No hay conexión a SAP.")
            Else

                Try
                    'oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
                Catch ex As Exception
                    clsPublic.Actualizar_Progreso(lblprg, "Error al hacer rollback en SAP: " & ex.Message)
                End Try

            End If

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar entrada de mercancía a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

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

    Public Function Marcar_Pedido_Cliente_Sincronizado_SAP(ByVal pNoDocumento As String, ByVal EstadoEnvio As Estado_Enviado_SAP, ByVal lblprg As RichTextBox) As Boolean

        Marcar_Pedido_Cliente_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                clsPublic.Actualizar_Progreso(lblprg, "Actualizando el estado enviado = " & EstadoEnvio & " para permitir importación nuevamente en pedido de cliente: " & pNoDocumento)

                Dim osalidaMercancia As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)

                If osalidaMercancia.GetByKey(pNoDocumento) Then

                    Try

                        osalidaMercancia.UserFields.Fields.Item("U_EnviadoWMS").Value = EstadoEnvio
                        osalidaMercancia.Update()

                        clsPublic.Actualizar_Progreso(lblprg, "Se actualizó el estado del documento.")

                        Marcar_Pedido_Cliente_Sincronizado_SAP = True

                    Catch e As Exception
                        Throw e
                    End Try

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener el documento de SAP con DocEntry: " & pNoDocumento)
                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Enviar_Entrega_Mercancia_OV_SAP3(ByVal _Docentry As Integer,
                                                     ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                     ByRef lblprg As RichTextBox,
                                                     ByRef prg As Windows.Forms.ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        ' Inicializar variables para WMS
        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim NoLineaEntrega As Integer = 0
        Dim clsTransaccion As New clsTransaccion
        Dim BePedidoWMS As New clsBeTrans_pe_enc
        Dim lBeInvSAP As New List(Of clsBeTmpInvSAP)

        ' Inicializamos el resultado como False
        Enviar_Entrega_Mercancia_OV_SAP3 = False

        Try
            ' Conectar a SAP
            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            ' Verificar si la conexión a SAP fue exitosa
            If lRetCode <> 0 Then
                Throw New Exception("Error al conectar a SAP: " & sErrMsg)
            End If

            ' Declarar las variables de objetos de SAP solo después de la conexión exitosa
            Dim oEntrega As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes), Documents)
            Dim oOrderSales As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)
            Dim LotesSAP As New clsSyncLotes ' Se inicializa después de la conexión

            ' Intentar obtener la orden de venta por DocEntry
            If Not oOrderSales.GetByKey(_Docentry) Then
                Throw New Exception("No se pudo obtener la orden de venta.")
            End If

            ' Configurar cabecera del documento de entrega
            oEntrega.CardCode = oOrderSales.CardCode
            oEntrega.DocDate = Date.Today

            ' Obtener información del pedido en WMS
            clsTransaccion.Open_Connection()

            BePedidoWMS = clsLnTrans_pe_enc.GetSingle(lINavTransaccionesOut.FirstOrDefault.Idpedidoenc,
                                                      clsTransaccion.lConnection,
                                                      clsTransaccion.lTransaction)

            ' Iterar sobre las líneas de la orden de venta
            For j As Integer = 0 To oOrderSales.Lines.Count - 1

                oOrderSales.Lines.SetCurrentLine(j)

                Dim vCodigoProductoSAP As String = oOrderSales.Lines.ItemCode.ToString()
                Dim vNoLineaOCSAP As Integer = oOrderSales.Lines.LineNum

                ' Actualizar progreso
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Documento:{1} - Procesando Producto: {0}", vCodigoProductoSAP, _Docentry))

                ' Agrupar los productos por línea
                Dim DistinctProductosLineas = lINavTransaccionesOut.Where(Function(x) x.Codigo_producto = vCodigoProductoSAP _
                                                          AndAlso x.No_linea = vNoLineaOCSAP).
                                                    GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea}).
                                                    Select(Function(g) New With {
                                                        g.Key.Codigo_producto,
                                                        g.Key.No_linea,
                                                        .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                    }).ToList()

                ' Procesar cada producto encontrado
                If DistinctProductosLineas.Any() Then
                    For Each ProductoSalida In DistinctProductosLineas
                        ' Verificar si la línea no está cerrada
                        If Not oOrderSales.Lines.LineStatus = BoStatus.bost_Close Then
                            ' Verificar si la cantidad a enviar es válida
                            If ProductoSalida.Cantidad_Total <= oOrderSales.Lines.Quantity Then

                                ' Control de lotes
                                Dim vControlPorLote As Boolean = clsLnProducto.Get_Control_Lote_By_Codigo(ProductoSalida.Codigo_producto,
                                                                                                          clsTransaccion.lConnection,
                                                                                                          clsTransaccion.lTransaction)

                                ' Si el producto tiene control por lote
                                If vControlPorLote Then
                                    ' Agrupar los lotes recibidos por estado
                                    Dim LotesRecibidosPorEstado = lINavTransaccionesOut.
                                        Where(Function(x) x.Codigo_producto = ProductoSalida.Codigo_producto _
                                              AndAlso x.No_linea = ProductoSalida.No_linea).
                                        GroupBy(Function(x) x.Idproductoestado).
                                        Select(Function(g) New With {
                                            .Idproductoestado = g.Key,
                                            .Lotes = g.GroupBy(Function(x) New With {Key x.Lote, Key x.Codigo_producto, Key x.No_linea, Key x.Fecha_vence}).
                                                       Select(Function(lg) New With {
                                                           .Codigo_producto = lg.Key.Codigo_producto,
                                                           .No_linea = lg.Key.No_linea,
                                                           .Lote = lg.Key.Lote,
                                                           .Fecha_vence = lg.Key.Fecha_vence,
                                                           .CantidadTotal = lg.Sum(Function(x) x.Cantidad),
                                                           .CantidadEnviada = lg.Sum(Function(x) x.Cantidad_Enviada),
                                                           .CantidadPendiente = lg.Sum(Function(x) x.Cantidad_Pendiente)
                                                       }).ToList()
                                        }).ToList()

                                    ' Si hay lotes recibidos
                                    If LotesRecibidosPorEstado.Count > 0 Then
                                        ' Llamar a AgregarLineaEntrega con todos los lotes asociados
                                        Agregar_Linea_Entrega(oEntrega,
                                                              oOrderSales,
                                                              _Docentry,
                                                              vNoLineaOCSAP,
                                                              LotesRecibidosPorEstado.FirstOrDefault,
                                                              LotesSAP,
                                                              lblprg,
                                                              lINavTransaccionesOut,
                                                              Lista_A_Actualizar)
                                        If Lista_A_Actualizar.Count > 0 AndAlso Lista_A_Actualizar.Sum(Function(x) x.Cantidad_Enviada) > 0 Then
                                            NoLineaEntrega += 1
                                        End If
                                    End If
                                End If
                            Else
                                Throw New Exception("WMS está intentando generar una entrega por una cantidad mayor a la que permite SAP.")
                            End If
                        End If
                    Next
                End If
            Next

            ' Si se agregaron líneas, se guarda la entrega en SAP
            If NoLineaEntrega > 0 Then
                ' Enviar campos de usuario antes de agregar el documento
                oEntrega.UserFields.Fields.Item("U_Distribuidor").Value = oOrderSales.UserFields.Fields.Item("U_Distribuidor").Value
                oEntrega.UserFields.Fields.Item("U_SEGUNDAREFERENCIA").Value = "WMS_NO: " & lINavTransaccionesOut.FirstOrDefault.Idpedidoenc

                ' Guardar el documento de entrega en SAP
                Dim oResultado As Integer = oEntrega.Add()
                If oResultado <> 0 Then
                    '#CKFK20241012 Quité el throw 
                    'Throw New Exception($"Error al crear la entrega en SAP: {oCompany.GetLastErrorDescription()}")
                    clsPublic.Actualizar_Progreso(lblprg, $"Error al crear la entrega en SAP: {oCompany.GetLastErrorDescription()}")
                    clsLnI_nav_transacciones_out.Actualizar_Estado_Enviado(Lista_A_Actualizar, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                Else

                    ' Actualizar en WMS
                    Dim vTrasladoDocEntry As Integer = ObtenerDocEntryDeEntregaSAP(oCompany)
                    BePedidoWMS.No_Documento_Externo = vTrasladoDocEntry
                    clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(BePedidoWMS, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    clsLnI_nav_transacciones_out.Actualizar_Estado_Enviado(Lista_A_Actualizar, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    Enviar_Entrega_Mercancia_OV_SAP3 = True

                    clsPublic.Actualizar_Progreso(lblprg, "Se creó el documento en SAP: " & vTrasladoDocEntry)

                End If

            Else
                clsLnI_nav_transacciones_out.Actualizar_Estado_Enviado(Lista_A_Actualizar, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                clsPublic.Actualizar_Progreso(lblprg, "No hay productos para enviar a SAP.")
            End If

            ' Confirmar transacción
            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            ' Rollback de transacción y manejo de errores
            clsTransaccion.RollBack_Transaction()
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
            Throw New Exception("Error al enviar la entrega a SAP: " & ex.Message)

        Finally
            ' Desconectar de SAP y cerrar conexiones
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

        Return Enviar_Entrega_Mercancia_OV_SAP3
    End Function


    ' Función para agregar una línea de entrega
    Private Sub Agregar_Linea_Entrega(ByRef oEntrega As Documents,
                                      ByRef oOrderSales As Documents,
                                      ByVal _Docentry As Integer,
                                      ByVal vNoLineaOCSAP As Integer,
                                      ByVal LotesRecibidosPorEstado As Object,
                                      ByVal LotesSAP As clsSyncLotes,
                                      ByRef lblprg As RichTextBox,
                                      ByRef lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                      ByRef Lista_A_Actualizar As List(Of clsBeI_nav_transacciones_out))
        Try

            Dim CantidadTotalAEnviar As Double = 0
            Dim CantidadDisponibleLoteSAP As Double = 0
            Dim TotalDisponibleSAP As Double = 0
            Dim vExistenciaLoteSAP As Double = 0  ' Declaración de la variable

            ' Fase 1: Calcular la cantidad total disponible en SAP para la entrega
            For Each lote In LotesRecibidosPorEstado.Lotes
                Try
                    ' Obtener la cantidad pendiente de enviar desde WMS
                    Dim CantidadPendienteWMS As Double = lote.CantidadTotal - lote.CantidadEnviada

                    ' Obtener la cantidad disponible de SAP para este lote
                    CantidadDisponibleLoteSAP = LotesSAP.Existe_Lote_SAP(oCompany,
                                                                         oOrderSales.Lines.ItemCode,
                                                                         lote.Lote,
                                                                         oOrderSales.Lines.WarehouseCode)

                    vExistenciaLoteSAP = CantidadDisponibleLoteSAP ' Asignación del valor disponible en SAP

                    ' Determinar cuánta cantidad del lote se puede enviar (mínimo entre lo que hay pendiente en WMS y lo que hay en SAP)
                    Dim CantidadAEnviar As Double = Math.Min(CantidadPendienteWMS, CantidadDisponibleLoteSAP)

                    ' Mostrar advertencia si la cantidad en SAP es inferior a la cantidad requerida
                    If CantidadAEnviar < CantidadPendienteWMS Then
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Advertencia: El lote {0} tiene menos cantidad en SAP ({1}) que la solicitada ({2}). Se enviarán {1} unidades.", lote.Lote, CantidadAEnviar, CantidadPendienteWMS))
                    End If

                    ' Acumular la cantidad total disponible en SAP
                    TotalDisponibleSAP += CantidadAEnviar
                Catch ex As Exception
                    ' Si ocurre un error con un lote, continuar con los demás
                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al procesar el lote {0} para el producto {1}: {2}", lote.Lote, oOrderSales.Lines.ItemCode, ex.Message))
                    Continue For ' Continuar con el siguiente lote
                End Try
            Next

            ' Verificar si hay alguna cantidad disponible para enviar
            If TotalDisponibleSAP > 0 Then
                ' Fase 2: Crear la entrega en SAP con los lotes y la cantidad determinada
                oEntrega.Lines.SetCurrentLine(oEntrega.Lines.Count - 1)
                oEntrega.Lines.BaseType = BoObjectTypes.oOrders
                oEntrega.Lines.BaseEntry = _Docentry
                oEntrega.Lines.BaseLine = vNoLineaOCSAP
                oEntrega.Lines.ItemCode = oOrderSales.Lines.ItemCode
                oEntrega.Lines.Quantity = TotalDisponibleSAP

                ' Ciclo para agregar los lotes a la línea de entrega en SAP
                For Each lote In LotesRecibidosPorEstado.Lotes

                    Try

                        ' Obtener la cantidad pendiente de enviar desde WMS
                        Dim CantidadPendienteWMS As Double = lote.CantidadTotal - lote.CantidadEnviada

                        ' Obtener la cantidad disponible de SAP para este lote
                        CantidadDisponibleLoteSAP = LotesSAP.Existe_Lote_SAP(oCompany,
                                                                             oOrderSales.Lines.ItemCode,
                                                                             lote.Lote,
                                                                             oOrderSales.Lines.WarehouseCode)

                        vExistenciaLoteSAP = CantidadDisponibleLoteSAP ' Asignación del valor disponible en SAP

                        ' Determinar cuánta cantidad del lote se puede enviar
                        Dim CantidadAEnviar As Double = Math.Min(CantidadPendienteWMS, CantidadDisponibleLoteSAP)

                        ' Si hay cantidad a enviar, agregar el lote a la línea
                        If CantidadAEnviar > 0 Then

                            oEntrega.Lines.BatchNumbers.SetCurrentLine(oEntrega.Lines.BatchNumbers.Count - 1)
                            oEntrega.Lines.BatchNumbers.BatchNumber = lote.Lote
                            oEntrega.Lines.BatchNumbers.Quantity = CantidadAEnviar
                            oEntrega.Lines.BatchNumbers.Add()

                            ' Actualizar la cantidad disponible en SAP para el siguiente ciclo
                            CantidadDisponibleLoteSAP -= CantidadAEnviar

                            ' Actualizar WMS después de procesar el lote en SAP
                            If CantidadAEnviar = CantidadPendienteWMS Then
                                ' Si se envió la cantidad total, actualizar completamente en WMS
                                ActualizarCantidadEnWMS(lINavTransaccionesOut,
                                                        Lista_A_Actualizar,
                                                        oOrderSales.DocEntry,
                                                        vNoLineaOCSAP,
                                                        oOrderSales.Lines.ItemCode,
                                                        lote.Lote,
                                                        CantidadAEnviar)
                            Else
                                ' Si solo se envió una cantidad parcial, actualizar parcialmente en WMS
                                ActualizarCantidadParcialEnWMS(lINavTransaccionesOut,
                                                               Lista_A_Actualizar,
                                                               oOrderSales.DocEntry,
                                                               vNoLineaOCSAP,
                                                               oOrderSales.Lines.ItemCode,
                                                               lote.Lote,
                                                               vExistenciaLoteSAP) ' Usar vExistenciaLoteSAP correctamente aquí
                            End If
                        End If
                    Catch ex As Exception
                        ' Si ocurre un error con un lote al agregarlo, continuar con los demás
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al agregar el lote {0} para el producto {1}: {2}", lote.Lote, oOrderSales.Lines.ItemCode, ex.Message))
                        Continue For ' Continuar con el siguiente lote
                    End Try
                Next

                ' Agregar una nueva línea si es necesario
                If oEntrega.Lines.Count > 0 Then
                    oEntrega.Lines.Add()
                End If

            Else
                ' Si no hay cantidades disponibles en SAP, emitir una advertencia y continuar
                clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay cantidades disponibles en SAP para el producto: {0}.", oOrderSales.Lines.ItemCode))
                For Each lote In LotesRecibidosPorEstado.Lotes

                    Try

                        ' Si solo se envió una cantidad parcial, actualizar parcialmente en WMS
                        ActualizarCantidadParcialEnWMS(lINavTransaccionesOut,
                                                       Lista_A_Actualizar,
                                                       oOrderSales.DocEntry,
                                                       vNoLineaOCSAP,
                                                       oOrderSales.Lines.ItemCode,
                                                       lote.Lote,
                                                       vExistenciaLoteSAP) ' Usar vExistenciaLoteSAP correctamente aquí
                    Catch ex As Exception
                        ' Si ocurre un error con un lote al agregarlo, continuar con los demás
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al agregar el lote {0} para el producto {1}: {2}", lote.Lote, oOrderSales.Lines.ItemCode, ex.Message))
                        Continue For ' Continuar con el siguiente lote
                    End Try
                Next
            End If

        Catch ex As Exception
            ' Manejar cualquier error que ocurra fuera del proceso de los lotes
            Throw New Exception("Error al procesar la línea de entrega para el producto: " & oOrderSales.Lines.ItemCode & ". Detalles del error: " & ex.Message)
        End Try
    End Sub

    ' Funciones auxiliares para actualizar cantidades en WMS
    Private Sub ActualizarCantidadEnWMS(ByRef lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                        ByRef Lista_A_Actualizar As List(Of clsBeI_nav_transacciones_out),
                                        ByVal DocEntry As Integer,
                                        ByVal NoLinea As Integer,
                                        ByVal CodigoProducto As String,
                                        ByVal Lote As String,
                                        ByVal Cantidad As Double)

        Dim Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = DocEntry _
                                                                   AndAlso x.No_linea = NoLinea _
                                                                   AndAlso x.Codigo_producto = CodigoProducto _
                                                                   AndAlso x.Lote = Lote _
                                                                   AndAlso x.Enviado = False)
        If Sublista_A_Actualizar IsNot Nothing Then
            For Each item In Sublista_A_Actualizar
                item.Cantidad_Enviada = Cantidad
                item.Cantidad_Pendiente = 0
                item.Enviado = True
                item.Auditar = False ' Actualizamos el valor de Auditar
                Lista_A_Actualizar.Add(item) ' Añadir a la lista que será actualizada posteriormente
            Next
        End If

    End Sub

    Private Sub ActualizarCantidadParcialEnWMS(ByRef lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                               ByRef Lista_A_Actualizar As List(Of clsBeI_nav_transacciones_out),
                                               ByVal DocEntry As Integer,
                                               ByVal NoLinea As Integer,
                                               ByVal CodigoProducto As String,
                                               ByVal Lote As String,
                                               ByVal CantidadEnviada As Double)

        ' Buscar todos los registros que coinciden con la búsqueda y no han sido enviados
        Dim Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = DocEntry _
                                                                  AndAlso x.No_linea = NoLinea _
                                                                  AndAlso x.Codigo_producto = CodigoProducto _
                                                                  AndAlso x.Lote = Lote _
                                                                  AndAlso x.Enviado = False)

        If Sublista_A_Actualizar IsNot Nothing Then
            For Each item In Sublista_A_Actualizar
                ' Determinar cuánta cantidad está pendiente de enviar en WMS
                Dim CantidadPendienteEnWMS As Double = item.Cantidad - item.Cantidad_Enviada

                ' Si la cantidad enviada es mayor o igual a la cantidad pendiente
                If CantidadEnviada >= CantidadPendienteEnWMS Then
                    ' Se envía todo lo que queda pendiente de este lote
                    item.Cantidad_Enviada = item.Cantidad ' Marcamos el lote como completamente enviado
                    item.Cantidad_Pendiente = 0 ' No queda nada pendiente
                    item.Auditar = False ' No es necesario auditar porque se ha completado
                    CantidadEnviada -= CantidadPendienteEnWMS ' Reducir la cantidad enviada en lo que se envió de este lote
                Else
                    ' Si la cantidad enviada es menor que la cantidad pendiente
                    item.Cantidad_Enviada += CantidadEnviada ' Agregar lo que se ha enviado a lo que ya estaba enviado
                    item.Cantidad_Pendiente = item.Cantidad - item.Cantidad_Enviada ' Actualizar la cantidad pendiente
                    item.Auditar = True ' Marcar el ítem para auditoría
                    CantidadEnviada = 0 ' Ya no queda nada más por enviar
                End If

                ' Marcar el ítem para auditoría y agregar a la lista de actualización
                Lista_A_Actualizar.Add(item)

                ' Si ya se ha enviado toda la cantidad, no es necesario seguir
                If CantidadEnviada <= 0 Then
                    Exit For
                End If
            Next
        End If
    End Sub


    ' Función auxiliar para obtener el DocEntry de la entrega creada en SAP
    Private Function ObtenerDocEntryDeEntregaSAP(oCompany As Object) As Integer
        Dim newObjectCode As String = ""
        oCompany.GetNewObjectCode(newObjectCode)
        Dim vTrasladoDocEntry As Integer
        If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
            Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
        End If
        Return vTrasladoDocEntry
    End Function

End Class