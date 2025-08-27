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
                                        T0.NumAtCard                                        
                                    FROM ORDR T0 
                                    WHERE T0.DocStatus = 'O' 
                                    AND T0.CreateDate >= '2024-01-01 00:00:00.000' 
                                    AND T0.U_Enviado_WMS = 2 " &
                                    IIf(pPedidoCliente <> "", " AND T0.DocNum  = " & pPedidoCliente, "") & "                                    
                                    ORDER BY T0.DocEntry DESC"

                RsEnc.DoQuery(SAP_OV)

                Dim BePedidoWMS As clsBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc()

                While RsEnc.EoF = False

                    '#CKFK20240528 Cambiando DOCENTRY por DOCNUM 
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
                    BePedidoWMS.Manufacturing_Process = 0

                    Dim n As Integer = 1
                    Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                    Dim query_det As String

                    '#CKFK20240528 Cambiando DOCENTRY por DOCNUM 
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

                            If Inserta_Cliente_SAP(PedidoClienteSAP.Transfer_to_Code) Then

                                clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No existía en WMS y fue insertado.")

                                clsLnLog_error_wms.Agregar_Error("#IF_CLI_NU: El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No existía en WMS y fue insertado.")

                            End If

                        End If

                        Dim BePedidoEncResult As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(PedidoClienteSAP,
                                                                                                                                            lblprg,
                                                                                                                                            Nothing,
                                                                                                                                            Nothing)

                        If Not BePedidoEncResult Is Nothing Then

                            Marcar_Pedido_Cliente_Sincronizado_SAP(PedidoClienteSAP.No, Estado_Enviado_SAP.Enviado, lblprg)

                            If PedidoClienteSAP.Manufacturing_Process Then
                                '#EJC202404011228: Insertar tarea de manufactura.
                                clsLnTrans_manufactura_enc.Insertar_Manufactura_Por_Defecto(PedidoClienteSAP,
                                                                                            BePedidoEncResult,
                                                                                            BeConfigEnc)
                            End If

                            clsLnLog_error_wms.Agregar_Error("#IF_PED_CLI_SAP: Se importó el pedido de cliente: " & PedidoClienteSAP.No)

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
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        If Enviar_Entrega_Mercancia_OV_SAP2(PT.No_pedido, lTransaccionesSalidaSingle, lblprg, prg) Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                                clsLnLog_error_wms.Agregar_Error(String.Format("#IF_SAP_ENV_PC: el pedido:{0} se envió correctamente a SAP.", PT.No_pedido))

                            Catch ex As Exception

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                            End Try

                        End If

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
        Dim clsTransaccion As New clsTransaccion

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

                    clsTransaccion.Commit_Transaction()

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
            clsTransaccion.RollBack_Transaction()
            Throw New Exception("No se pudo insertar el cliente nuevo proviniente de SAP: " & ex.Message)
        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

    End Function

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
        Dim vCodigoBodegaERP As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim NoLineaEntrega As Integer = 0 : Dim NoLineaEntregaLote As Integer = 0
        Dim clsTransaccion As New clsTransaccion

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

                    oEntrega.CardCode = oOrderSales.CardCode
                    oEntrega.DocDate = Date.Today
                    oEntrega.DocObjectCode = BoObjectTypes.oDeliveryNotes

                    NoLineaEntrega = 0 : NoLineaEntregaLote = 0

                    clsTransaccion.Open_Connection()

                    For j As Integer = 0 To oOrderSales.Lines.Count - 1

                        oOrderSales.Lines.SetCurrentLine(j)

                        Dim vCodigoProductoSAP As String = oOrderSales.Lines.ItemCode.ToString()
                        Dim vNoLineaOCSAP As Integer = oOrderSales.Lines.LineNum

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", vCodigoProductoSAP))

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

                                If Not oOrderSales.Lines.LineStatus = BoStatus.bost_Close Then

                                    If ProductoSalida.Cantidad_Total <= oOrderSales.Lines.Quantity Then

                                        Dim nuevaLineaEntrega As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_producto)

                                        If nuevaLineaEntrega Then

                                            oEntrega.Lines.SetCurrentLine(NoLineaEntrega)
                                            oEntrega.Lines.BaseType = BoObjectTypes.oOrders
                                            oEntrega.Lines.ItemCode = oOrderSales.Lines.ItemCode
                                            oEntrega.Lines.BaseEntry = _Docentry
                                            oEntrega.Lines.BaseLine = vNoLineaOCSAP
                                            oEntrega.Lines.Quantity = ProductoSalida.Cantidad_Total
                                            vCodigoAnterior = oEntrega.Lines.ItemCode

                                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderSales.DocEntry _
                                                                                                                  AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                                                  AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                                                                  AndAlso x.Enviado = False)

                                            If Not Sublista_A_Actualizar Is Nothing Then
                                                If Sublista_A_Actualizar.Count > 0 Then
                                                    Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                                End If
                                            End If

                                            oEntrega.Lines.Add() : NoLineaEntrega += 1

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

                            vAgregarEntrega = True

                        End If

                    Next

                    Dim oResultado As Integer
                    oResultado = oEntrega.Add()

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

                        clsLnI_nav_ejecucion_det_error.Inserta_Log("Actualizando el estado enviado = " & EstadoEnvio & " para permitir importación nuevamente en pedido de cliente: " & pNoDocumento,
                                                                   pNoDocumento,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet)

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

    Public Function Cerrar_Lineas_Documento_Salida(ByVal _Docentry As Integer,
                                                   ByRef lblprg As RichTextBox) As Boolean



        Dim vCodigoAnterior As String = ""
        Dim vCodigoBodegaERP As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim NoLineaEntrega As Integer = 0 : Dim NoLineaEntregaLote As Integer = 0
        Dim clsTransaccion As New clsTransaccion

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

                Dim oOrderSales As StockTransfer
                Dim BaseLine As Integer = 0

                oOrderSales = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                If oOrderSales.GetByKey(_Docentry) Then

                    NoLineaEntrega = 0 : NoLineaEntregaLote = 0

                    clsTransaccion.Open_Connection()

                    oOrderSales.Close()

                    Dim oResultado As Integer
                    oResultado = oOrderSales.Update()

                    If oResultado <> 0 Then
                        Dim errMsg = oCompany.GetLastErrorDescription()
                        Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "Documento de traslado cerrado exitosamente.")
                    End If

                    clsTransaccion.Commit_Transaction()

                Else
                    clsPublic.Actualizar_Progreso(lblprg, String.Format("No se obtuvo el documento SAP: {0} ", _Docentry))
                End If

            End If

            Return True

        Catch errMsg As Exception

            clsTransaccion.RollBack_Transaction()

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

End Class