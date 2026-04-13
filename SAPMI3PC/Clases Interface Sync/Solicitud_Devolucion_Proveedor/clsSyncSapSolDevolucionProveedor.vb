Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSapSolDevolucionProveedor : Inherits clsInterfaceBase : Implements IDisposable

    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0
    Private Shared oCompany As Company
    Private Shared lRetCode
    Private Shared lErrCode As Long
    Private Shared sErrMsg As String = ""
    Private disposedValue As Boolean

    Public Function Get_Solicitud_Devolucion_From_SAP(Optional ByVal AplicarFiltros As Boolean = True,
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

                Dim BeSolicitudDevolWMS As clsBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc()

                While RsEnc.EoF = False

                    BeSolicitudDevolWMS = New clsBeI_nav_ped_traslado_enc()
                    BeSolicitudDevolWMS.No = RsEnc.Fields.Item("DOCENTRY").Value
                    BeSolicitudDevolWMS.Posting_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BeSolicitudDevolWMS.Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BeSolicitudDevolWMS.Shipment_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BeSolicitudDevolWMS.Status = 1
                    BeSolicitudDevolWMS.Transfer_from_Code = RsEnc.Fields.Item("BODEGA").Value
                    BeSolicitudDevolWMS.Transfer_from_Contact = "MI3_NAME"
                    BeSolicitudDevolWMS.Transfer_from_Name = "MI3_NAME"
                    BeSolicitudDevolWMS.Transfer_to_Code = RsEnc.Fields.Item("CARDCODE").Value
                    BeSolicitudDevolWMS.Transfer_to_Contact = RsEnc.Fields.Item("CARDNAME").Value
                    BeSolicitudDevolWMS.Transfer_to_Name = RsEnc.Fields.Item("CARDNAME").Value
                    BeSolicitudDevolWMS.Product_Owner_Code = BePropietario.Codigo
                    BeSolicitudDevolWMS.Receipt_Document_Reference = RsEnc.Fields.Item("DOCNUM").Value

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
                                 WHERE T0.DOCENTRY = '" & BeSolicitudDevolWMS.No & "'"

                    RsDet.DoQuery(query_det)

                    BeSolicitudDevolWMS.Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)

                    While RsDet.EoF = False

                        BeSolicitudDevolDet = New clsBeI_nav_ped_traslado_det()
                        BeSolicitudDevolDet.NoEnc = BeSolicitudDevolWMS.No
                        BeSolicitudDevolDet.No = clsLnI_nav_ped_traslado_det.MaxID() + 1
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
                        BeSolicitudDevolWMS.Lineas_Detalle.Add(BeSolicitudDevolDet)

                        n += 1

                        RsDet.MoveNext()

                    End While

                    lSolicitudesDevolucion.Add(BeSolicitudDevolWMS)

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

    Public Function Procesar_Solicitudes_Devolucion_SAP(ByVal lblprg As RichTextBox,
                                                        ByRef prg As Windows.Forms.ProgressBar,
                                                        Optional pPedidoCliente As String = "") As Boolean
        Procesar_Solicitudes_Devolucion_SAP = False

        Dim Resultado As String = ""
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        Try

            Dim lPedidosTrasladoSAP As New List(Of clsBeI_nav_ped_traslado_enc)

            lPedidosTrasladoSAP = Get_Solicitud_Devolucion_From_SAP(True, pPedidoCliente)

            Dim BeClienteWMS As New clsBeCliente

            If lPedidosTrasladoSAP IsNot Nothing Then

                If lPedidosTrasladoSAP.Count = 0 Then

                    If pPedidoCliente <> "" Then
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay pedidos de cliente pendientes de importar con el No.: {0} {1}", pPedidoCliente, vbNewLine))
                    Else
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay pedidos de cliente pendientes de importar {0}", vbNewLine))
                    End If

                Else

                    CnnLog.Open()

                    For Each PedidoClienteSAP In lPedidosTrasladoSAP

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido de Cliente (ORDR) : {0}/{1}{2}", PedidoClienteSAP.No, PedidoClienteSAP.Receipt_Document_Reference, vbNewLine))

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
                                                                                                                                               lblprg, Nothing, Nothing)

                        If Not BePedidoEncResult Is Nothing Then

                            BePedidoEncResult.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(BePedidoEncResult.IdPedidoEnc)

                            Marcar_Solicitud_Devolucion_Sincronizado_SAP(PedidoClienteSAP.No, Estado_Enviado_SAP.Enviado, lblprg)

                        End If

                        clsPublic.Actualizar_Progreso(lblprg, Resultado)

                    Next

                End If

            End If

            Procesar_Solicitudes_Devolucion_SAP = True

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

                Dim osalidaMercancia As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)

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

    Public Function Crear_Draft_Devolucion_Proveedor(ByVal _DocEntry As Integer,
                                                     ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                     ByRef lblprg As RichTextBox,
                                                     ByRef prg As Windows.Forms.ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim lINav_Transacciones_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vAgregarDraft As Boolean = False
        Dim NoLineaDraft As Integer = 0
        Dim NoLineaDraftLote As Integer = 0
        Dim clsTransaccion As New clsTransaccion

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            Application.DoEvents()

            If lRetCode <> 0 Then
                Throw New Exception("Error al conectar a SAP: " & sErrMsg)
            Else
                Dim oDraftDevolucion As Documents
                Dim oSolicitudDevolucion As Documents

                oDraftDevolucion = CType(oCompany.GetBusinessObject(BoObjectTypes.oDrafts), Documents)
                oSolicitudDevolucion = CType(oCompany.GetBusinessObject(BoObjectTypes.oDrafts), Documents)

                If oDraftDevolucion.GetByKey(_DocEntry) Then

                    oSolicitudDevolucion.DocObjectCode = BoObjectTypes.oPurchaseReturns
                    oSolicitudDevolucion.CardCode = oSolicitudDevolucion.CardCode
                    oSolicitudDevolucion.DocDate = Date.Today

                    NoLineaDraft = 0

                    For j As Integer = 0 To oSolicitudDevolucion.Lines.Count - 1

                        oSolicitudDevolucion.Lines.SetCurrentLine(j)

                        Dim vCodigoProductoSAP As String = oSolicitudDevolucion.Lines.ItemCode.ToString()
                        Dim vNoLineaPO As Integer = oSolicitudDevolucion.Lines.LineNum

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0}", vCodigoProductoSAP))

                        Dim DistinctProductosLineas = lINavTransaccionesOut.Where(Function(x) x.Codigo_producto = vCodigoProductoSAP AndAlso x.No_linea = vNoLineaPO).ToList()

                        For Each ProductoSalida In DistinctProductosLineas

                            If vCodigoAnterior <> ProductoSalida.Codigo_producto Then

                                oSolicitudDevolucion.Lines.SetCurrentLine(NoLineaDraft)
                                oSolicitudDevolucion.Lines.BaseType = BoObjectTypes.oPurchaseOrders
                                oSolicitudDevolucion.Lines.ItemCode = vCodigoProductoSAP
                                oSolicitudDevolucion.Lines.BaseEntry = _DocEntry
                                oSolicitudDevolucion.Lines.BaseLine = vNoLineaPO
                                oSolicitudDevolucion.Lines.Quantity = ProductoSalida.Cantidad
                                vCodigoAnterior = vCodigoProductoSAP

                                ' Manejo de detalles de lote, si es necesario
                                oSolicitudDevolucion.Lines.BatchNumbers.BatchNumber = ProductoSalida.Lote
                                oSolicitudDevolucion.Lines.BatchNumbers.Quantity = ProductoSalida.Cantidad
                                oSolicitudDevolucion.Lines.BatchNumbers.Add()

                                oDraftDevolucion.Lines.Add()
                                NoLineaDraft += 1

                            End If

                        Next

                    Next

                    If oDraftDevolucion.Add() <> 0 Then
                        Throw New Exception("Error al agregar draft: " & oCompany.GetLastErrorDescription())
                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "Draft de devolución creado correctamente.")
                    End If

                End If

            End If

            Return True

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
            Throw New Exception("Error al crear draft de devolución: " & ex.Message)
        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
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

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio()

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

                        If Crear_Draft_Devolucion_Proveedor(PT.No_pedido, lTransaccionesSalidaSingle, lblprg, prg) Then

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

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
