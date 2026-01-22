Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSapDevolucionProveedor : Inherits clsInterfaceBase : Implements IDisposable

    Private Shared oCompany As Company
    Private Shared lRetCode
    Private Shared lErrCode As Long
    Private Shared sErrMsg As String = ""
    Private disposedValue As Boolean

    Public Function Get_Devolucion_Proveedor_From_SAP(ByVal CodBodega As String,
                                                      Optional ByVal AplicarFiltros As Boolean = True,
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
                                        T0.DocDueDate DocDate,
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
                                    AND T0.U_Enviado_WMS = 2 AND T0.Canceled = 'N'" &
                                    IIf(pDocEntrySolicitudDevolucion <> "", " AND T0.DocEntry  = " & pDocEntrySolicitudDevolucion, "") &
                                    " AND (SELECT TOP 1 D0.WhsCode 
                                            FROM PRR1 D0 
                                            WHERE D0.DocEntry = T0.DocEntry) = " & CodBodega & "                                  
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
                                 T1.SalUnitMsr AS UNIDAD_MEDIDA     
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
        Dim BeBodega As New clsBeBodega
        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega,
                                                         lConnection,
                                                         lTransaction)

            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia TOMWMS.")

            Dim lPedidosTrasladoSAP As New List(Of clsBeI_nav_ped_traslado_enc)

            lPedidosTrasladoSAP = Get_Devolucion_Proveedor_From_SAP(BeBodega.Codigo, True, pPedidoCliente)

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

                        BeClienteWMS = clsLnCliente.Existe(PedidoClienteSAP.Transfer_to_Code, lConnection, lTransaction)

                        If BeClienteWMS Is Nothing Then

                            If Inserta_Cliente_SAP(PedidoClienteSAP.Transfer_to_Code, lConnection, lTransaction) Then
                                clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No existía en WMS y fue insertado.")
                                clsLnLog_error_wms.Agregar_Error("#IF_SAP_DEVOL_CLI: El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No existía en WMS y fue insertado.")
                            Else
                                clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No se pudo insertar en WMS.")
                                Exit Function
                            End If

                        End If

                        Dim BePedidoEncResult As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(PedidoClienteSAP,
                        lblprg,
                        lConnection,
                        lTransaction)

                        If Not BePedidoEncResult Is Nothing Then

                            BePedidoEncResult.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(BePedidoEncResult.IdPedidoEnc,
                                                                                                 lConnection,
                                                                                                 lTransaction)

                            Marcar_Solicitud_Devolucion_Sincronizado_SAP(PedidoClienteSAP.No,
                                                                         Estado_Enviado_SAP.Enviado,
                                                                         lblprg)

                            clsLnLog_error_wms.Agregar_Error("#IF_SAP_DEVOL_SAP_PROV: Se importó el documento: " & PedidoClienteSAP.No)

                        End If

                        clsPublic.Actualizar_Progreso(lblprg, Resultado)

                    Next

                End If

            End If

            lTransaction.Commit()

            Procesar_Devolucion_Mercancia_SAP = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, CnnLog)

            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    Public Function Marcar_Solicitud_Devolucion_Sincronizado_SAP(ByVal pNoDocumento As String,
                                                                 ByVal EstadoEnvio As Estado_Enviado_SAP,
                                                                 ByVal lblprg As RichTextBox) As Boolean

        Marcar_Solicitud_Devolucion_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                clsPublic.Actualizar_Progreso(lblprg, "Actualizando el estado enviado = " & EstadoEnvio & " para permitir importación nuevamente en pedido de cliente: " & pNoDocumento)

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

    Private Function Inserta_Cliente_SAP(ByVal pCodigo As String,
                                         ByVal pConnection As SqlConnection,
                                         ByVal pTransaction As SqlTransaction) As Boolean

        Inserta_Cliente_SAP = False

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios
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
                             T0.LicTradNum AS NIT, 
                             T0.Address AS DIRECCION, 
                             T0.E_Mail FROM OCRD T0 
                             WHERE T0.CARDTYPE = 'S'  
                             AND (t0.CARDCODE)= '" & pCodigo & "'"


                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                Dim BeCliente As New clsBeCliente

                While Not rs.EoF

                    BeCliente.IdCliente = clsLnCliente.MaxID(pConnection, pTransaction) + 1
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

                    BeCliente.IdUbicacionAbastecerCon = clsLnBodega.Get_IdUbicDespacho_By_IdBodega(BeConfigEnc.Idbodega, pConnection, pTransaction)

                    clsLnCliente.Insertar(BeCliente, pConnection, pTransaction)

                    Dim BeClienteBodega As New clsBeCliente_bodega
                    BeClienteBodega = New clsBeCliente_bodega()
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(pConnection, pTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now
                    BeClienteBodega.Cliente = BeCliente

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                pConnection,
                                                                pTransaction)


                    '#EJC202303031646: Insertar días por defecto para clientes.
                    If BeConfigEnc.Dias_Vida_Defecto_Perecederos > 0 Then

                        lFamilias = clsLnProducto_familia.Get_All_Filtro(True,
                                                                         BeConfigEnc.IdPropietario,
                                                                         pConnection,
                                                                         pTransaction)

                        lClasificacion = clsLnProducto_clasificacion.Get_All_Filtro(True,
                                                                                    BeConfigEnc.IdPropietario,
                                                                                    pConnection,
                                                                                    pTransaction)

                        If Not lFamilias Is Nothing AndAlso Not lClasificacion Is Nothing Then

                            Dim BeTiempoCliente As New clsBeCliente_tiempos

                            For Each F In lFamilias

                                For Each C In lClasificacion

                                    BeTiempoCliente = New clsBeCliente_tiempos()
                                    BeTiempoCliente.IdTiempoCliente = clsLnCliente_tiempos.MaxID(pConnection, pTransaction) + 1
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
                                    clsLnCliente_tiempos.Insertar(BeTiempoCliente, pConnection, pTransaction)

                                Next

                            Next

                        End If

                    End If

                    Dim oBusinessPartnerSBO As BusinessPartners = CType(oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners), SAPbobsCOM.BusinessPartners)

                    If oBusinessPartnerSBO.GetByKey(pCodigo) Then
                        oBusinessPartnerSBO.UserFields.Fields.Item("U_ENVIADO_WMS").Value = "1"
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
