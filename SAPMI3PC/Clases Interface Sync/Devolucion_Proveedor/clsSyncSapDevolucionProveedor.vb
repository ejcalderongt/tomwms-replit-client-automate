Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSapDevolucionProveedor : Inherits clsInterfaceBase : Implements IDisposable

    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0
    Private Shared oCompany As Company
    Private Shared lRetCode
    Private Shared lErrCode As Long
    Private Shared sErrMsg As String = ""
    Private disposedValue As Boolean

    Public Function Get_Devolucion_Proveedor_From_SAP(Optional ByVal AplicarFiltros As Boolean = True,
                                                      Optional ByVal pDocEntrySolicitudDevolucion As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lSolicitudesDevolucion As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BeSolicitudDevolEnc As New clsBeI_nav_ped_traslado_enc
        Dim BeSolicitudDevolDet As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 0
        Dim BePropietario As New clsBePropietarios

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

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
                                            FROM PRR1 D0 
                                            WHERE D0.DocEntry = T0.DocEntry) AS BODEGA,
                                        ISNULL(T0.Comments,'')  + ISNULL(T0.Address,'') as Comments,
                                        T0.NumAtCard                                        
                                    FROM OPRR T0 
                                    WHERE T0.DocStatus = 'O' 
                                    AND T0.CreateDate >= '2024-01-01 00:00:00.000' 
                                    AND T0.U_EnviadoWMS = 2 AND T0.Canceled = 'N'" &
                                    IIf(pDocEntrySolicitudDevolucion <> "", " AND T0.DocEntry  = " & pDocEntrySolicitudDevolucion, "") & "                                    
                                    ORDER BY T0.DocEntry DESC"

                RsEnc.DoQuery(SAP_OV)

                While RsEnc.EoF = False

                    BeSolicitudDevolEnc = New clsBeI_nav_ped_traslado_enc()
                    BeSolicitudDevolEnc.No = RsEnc.Fields.Item("DOCENTRY").Value
                    BeSolicitudDevolEnc.Posting_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BeSolicitudDevolEnc.Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BeSolicitudDevolEnc.Shipment_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BeSolicitudDevolEnc.Status = 1
                    BeSolicitudDevolEnc.Transfer_from_Code = RsEnc.Fields.Item("BODEGA").Value
                    BeSolicitudDevolEnc.Transfer_from_Contact = "MI3_NAME"
                    BeSolicitudDevolEnc.Transfer_from_Name = "MI3_NAME"
                    BeSolicitudDevolEnc.Transfer_to_Code = RsEnc.Fields.Item("CARDCODE").Value
                    BeSolicitudDevolEnc.Transfer_to_Contact = RsEnc.Fields.Item("CARDNAME").Value
                    BeSolicitudDevolEnc.Transfer_to_Name = RsEnc.Fields.Item("CARDNAME").Value
                    BeSolicitudDevolEnc.Product_Owner_Code = BePropietario.Codigo
                    BeSolicitudDevolEnc.Receipt_Document_Reference = RsEnc.Fields.Item("DOCNUM").Value
                    BeSolicitudDevolEnc.Document_Type = tTipoDocumentoSalida.Devolucion_Proveedor

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
                                 FROM PRR1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode    
                                 WHERE T0.DOCENTRY = '" & BeSolicitudDevolEnc.No & "'"

                    RsDet.DoQuery(query_det)

                    BeSolicitudDevolEnc.Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)

                    While RsDet.EoF = False

                        BeSolicitudDevolDet = New clsBeI_nav_ped_traslado_det()
                        BeSolicitudDevolDet.NoEnc = BeSolicitudDevolEnc.No
                        BeSolicitudDevolDet.No = clsLnTrans_pe_det.MaxID() + 1
                        BeSolicitudDevolDet.Item_No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
                        BeSolicitudDevolDet.Line_No = RsDet.Fields.Item("LINENUM").Value.ToString()
                        BeSolicitudDevolDet.Shipment_Date = Date.Now
                        BeSolicitudDevolDet.Quantity = Convert.ToInt32(RsDet.Fields.Item("QUANTITY").Value)
                        BeSolicitudDevolDet.Description = RsDet.Fields.Item("dscription").Value.ToString()
                        BeSolicitudDevolDet.Unit_of_Measure_Code = RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()
                        BeSolicitudDevolDet.Status = 1
                        BeSolicitudDevolDet.Variant_Code = Nothing
                        BeSolicitudDevolDet.Transfer_to_CodeField = RsDet.Fields.Item("WHSCODE").Value.ToString()
                        BeSolicitudDevolDet.Price = Convert.ToDouble(RsDet.Fields.Item("PRICE").Value)
                        BeSolicitudDevolEnc.Lineas_Detalle.Add(BeSolicitudDevolDet)

                        n += 1

                        RsDet.MoveNext()

                    End While

                    lSolicitudesDevolucion.Add(BeSolicitudDevolEnc)

                    RsEnc.MoveNext()

                End While

            End If

            Return lSolicitudesDevolucion

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Procesar_Devolucion_Mercancia_SAP(ByVal lblprg As RichTextBox,
                                                        ByRef prg As Windows.Forms.ProgressBar,
                                                        Optional pPedidoCliente As String = "") As Boolean
        Procesar_Devolucion_Mercancia_SAP = False

        Dim Resultado As String = ""
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        Try

            Dim lPedidosTrasladoSAP As New List(Of clsBeI_nav_ped_traslado_enc)

            lPedidosTrasladoSAP = Get_Devolucion_Proveedor_From_SAP(True, pPedidoCliente)

            Dim BeClienteWMS As New clsBeCliente

            If lPedidosTrasladoSAP IsNot Nothing Then

                If lPedidosTrasladoSAP.Count = 0 Then

                    If pPedidoCliente <> "" Then
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay devoluciones a proveedor pendientes de importar con el No.: {0} {1}", pPedidoCliente, vbNewLine))
                    Else
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay hay devoluciones a proveedor pendientes de importar {0}", vbNewLine))
                    End If

                Else

                    CnnLog.Open()

                    For Each PedidoClienteSAP In lPedidosTrasladoSAP

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando devolución a proveedor (OPRR) : {0}/{1}{2}", PedidoClienteSAP.No, PedidoClienteSAP.Receipt_Document_Reference, vbNewLine))

                        BeClienteWMS = clsLnCliente.Existe(PedidoClienteSAP.Transfer_to_Code)

                        If BeClienteWMS Is Nothing Then

                            If Inserta_Cliente_SAP(PedidoClienteSAP.Transfer_to_Code) Then
                                clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No existía en WMS y fue insertado.")
                            Else
                                clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No se pudo insertar en WMS.")
                                Exit Function
                            End If

                        End If

                        Dim BePedidoEncResult As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(PedidoClienteSAP,
                                                                                                                                            lblprg,
                                                                                                                                            Nothing,
                                                                                                                                            Nothing)

                        If Not BePedidoEncResult Is Nothing Then

                            BePedidoEncResult.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(BePedidoEncResult.IdPedidoEnc)

                            Marcar_Solicitud_Devolucion_Sincronizado_SAP(PedidoClienteSAP.No, Estado_Enviado_SAP.Enviado, lblprg)

                        End If

                        clsPublic.Actualizar_Progreso(lblprg, Resultado)

                    Next

                End If

            End If

            Procesar_Devolucion_Mercancia_SAP = True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, CnnLog)

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
                             WHERE T0.CARDTYPE = 'S'  
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

                    BeCliente.IdUbicacionAbastecerCon = clsLnBodega.Get_IdUbicDespacho_By_IdBodega(BeConfigEnc.Idbodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

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


                    '#EJC202303031646: Insertar días por defecto para clientes.
                    If BeConfigEnc.Dias_Vida_Defecto_Perecederos > 0 Then

                        lFamilias = clsLnProducto_familia.Get_All_Filtro(True,
                                                                         BeConfigEnc.IdPropietario,
                                                                         clsTransaccion.lConnection,
                                                                         clsTransaccion.lTransaction)

                        lClasificacion = clsLnProducto_clasificacion.Get_All_Filtro(True,
                                                                                    BeConfigEnc.IdPropietario,
                                                                                    clsTransaccion.lConnection,
                                                                                    clsTransaccion.lTransaction)

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

    Public Function Marcar_Solicitud_Devolucion_Sincronizado_SAP(ByVal pNoDocumento As String, ByVal EstadoEnvio As Estado_Enviado_SAP, ByVal lblprg As RichTextBox) As Boolean

        Marcar_Solicitud_Devolucion_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                clsPublic.Actualizar_Progreso(lblprg, "Actualizando el estado enviado = " & EstadoEnvio & " para permitir importación nuevamente en pedido de cliente: " & pNoDocumento)

                Marcar_Solicitud_Devolucion_Sincronizado_SAP = True

                Dim osalidaMercancia As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oGoodsReturnRequest), Documents)

                If osalidaMercancia.GetByKey(pNoDocumento) Then

                    Try

                        osalidaMercancia.UserFields.Fields.Item("U_EnviadoWMS").Value = EstadoEnvio
                        osalidaMercancia.Update()

                        clsPublic.Actualizar_Progreso(lblprg, "Se actualizó el estado del documento.")

                        Marcar_Solicitud_Devolucion_Sincronizado_SAP = True

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

    Public Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                              ByRef prg As Windows.Forms.ProgressBar,
                                              ByVal pTipo As clsDataContractDI.tTipoDocumentoSalida)

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

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Solicitud de Devolución: {0}-{1}", PT.Idpedidoenc, PT.No_pedido))

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_Referencia_TipoDoc(PT.No_pedido, pTipo)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        If Crear_Devolucion_Desde_Solicitud_Aprobada(PT.No_pedido,
                                                                     lTransaccionesSalidaSingle,
                                                                     lblprg,
                                                                     prg) Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

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
            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

    Public Function Crear_Devolucion_Desde_Solicitud_Aprobada(ByVal _Docentry As Integer,
                                                             ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                             ByRef lblprg As RichTextBox,
                                                             ByRef prg As ProgressBar) As Boolean


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

        Crear_Devolucion_Desde_Solicitud_Aprobada = False

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

                Dim oSolicitudDevolucion As Documents
                Dim oDevolucion As Documents

                oSolicitudDevolucion = CType(oCompany.GetBusinessObject(234000032), Documents)
                oDevolucion = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseReturns), Documents)

                If oSolicitudDevolucion.GetByKey(_Docentry) Then

                    oDevolucion.CardCode = oSolicitudDevolucion.CardCode
                    oDevolucion.DocDate = Date.Today
                    oDevolucion.DocObjectCode = BoObjectTypes.oPurchaseDeliveryNotes
                    oDevolucion.NumAtCard = oSolicitudDevolucion.NumAtCard

                    Dim nombreCampoSegundaReferencia As String = "U_SEGUNDAREFERENCIA"
                    oDevolucion.UserFields.Fields.Item(nombreCampoSegundaReferencia).Value = oSolicitudDevolucion.UserFields.Fields.Item(nombreCampoSegundaReferencia).Value

                    NoLineaEntrega = 0 : NoLineaEntregaLote = 0

                    ' Verificar el estado de aprobación del documento
                    If oSolicitudDevolucion.AuthorizationStatus = DocumentAuthorizationStatusEnum.dasPending Then
                        Throw New Exception("El documento aún está pendiente de aprobación en SAP y no puede ser procesado.")
                    ElseIf oSolicitudDevolucion.AuthorizationStatus = DocumentAuthorizationStatusEnum.dasRejected Then
                        Throw New Exception("El documento fue rechazado en el proceso de aprobación y no puede ser procesado.")
                    End If


                    clsTransaccion.Begin_Transaction()

                    For j As Integer = 0 To oSolicitudDevolucion.Lines.Count - 1

                        oSolicitudDevolucion.Lines.SetCurrentLine(j)

                        Dim vCodigoProductoSAP As String = oSolicitudDevolucion.Lines.ItemCode.ToString()
                        Dim vNoLineaOCSAP As Integer = oSolicitudDevolucion.Lines.LineNum

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

                                If Not oSolicitudDevolucion.Lines.LineStatus = BoStatus.bost_Close Then

                                    If ProductoSalida.Cantidad_Total <= oSolicitudDevolucion.Lines.Quantity Then

                                        Dim nuevaLineaEntrega As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_producto)

                                        If nuevaLineaEntrega Then

                                            oDevolucion.Lines.SetCurrentLine(NoLineaEntrega)
                                            oDevolucion.Lines.BaseType = 234000032
                                            oDevolucion.Lines.ItemCode = oSolicitudDevolucion.Lines.ItemCode
                                            oDevolucion.Lines.BaseEntry = _Docentry
                                            oDevolucion.Lines.BaseLine = vNoLineaOCSAP
                                            oDevolucion.Lines.Quantity = ProductoSalida.Cantidad_Total
                                            oDevolucion.Lines.WarehouseCode = oSolicitudDevolucion.Lines.WarehouseCode

                                            vCodigoAnterior = oDevolucion.Lines.ItemCode

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

                                                            oDevolucion.Lines.BatchNumbers.SetCurrentLine(NoLineaEntregaLote)
                                                            oDevolucion.Lines.BatchNumbers.BatchNumber = Lote.Lote
                                                            oDevolucion.Lines.BatchNumbers.Quantity = Lote.CantidadTotal
                                                            oDevolucion.Lines.BatchNumbers.Add()

                                                            NoLineaEntregaLote += 1

                                                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oSolicitudDevolucion.DocEntry _
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

                                                    Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oSolicitudDevolucion.DocEntry _
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

                                            oDevolucion.Lines.Add() : NoLineaEntrega += 1

                                        End If

                                    Else
                                        Throw New Exception("WMS está intentando generar una entrega por: " & ProductoSalida.Cantidad_Total &
                                                            " en una línea de SAP para el material: " & oSolicitudDevolucion.Lines.ItemCode &
                                                            " que refleja una cantidad de: " & oSolicitudDevolucion.Lines.Quantity & " probablemente esto no sea posible.")
                                    End If

                                Else

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("El Producto: {0} ya fue completado. ", oSolicitudDevolucion.Lines.ItemCode.ToString()))

                                End If

                            Next 'DistinctProductosLineas

                            vAgregarEntrega = True

                        End If

                    Next

                    ' Verificar que los CardCode coincidan
                    If oDevolucion.CardCode <> oSolicitudDevolucion.CardCode Then
                        Throw New Exception("Tarjeta documento base y tarjeta documento destino no coinciden.")
                    End If

                    Dim oResultado As Integer
                    oResultado = oDevolucion.Add()

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

                    Crear_Devolucion_Desde_Solicitud_Aprobada = True

                End If

            End If

        Catch errMsg As Exception

            clsTransaccion.RollBack_Transaction()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar entrada de mercancía a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            clsPublic.Actualizar_Progreso(lblprg, errMsg.Message)

        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

    End Function


    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: eliminar el estado administrado (objetos administrados)
            End If

            ' TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            ' TODO: establecer los campos grandes como NULL
            disposedValue = True
        End If
    End Sub

    ' ' TODO: reemplazar el finalizador solo si "Dispose(disposing As Boolean)" tiene código para liberar los recursos no administrados
    ' Protected Overrides Sub Finalize()
    '     ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
