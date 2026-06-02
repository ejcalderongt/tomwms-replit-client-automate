Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports DevExpress.XtraEditors
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI
Imports TOMWMS.clsSyncSAPSSolicitudTraslado

Public Class clsSyncSAPPedidoCompra : Inherits clsInterfaceBase
    Implements IDisposable

    Private Shared VContadorBitacoraTOMWMS As Integer = 0
    Private VContadorBitacoraIntermedia As Integer = 0

    Public Function Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                        ByRef prg As System.Windows.Forms.ProgressBar,
                                                                        ByRef cnnLog As SqlConnection,
                                                                        Optional ByVal pNoDocumentoSAP As String = "") As Boolean

        Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResult As String = ""
        Dim vContador As Integer = 0
        Dim BeBodega As New clsBeBodega
        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega, lConnection, lTransaction)

            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia TOMWMS.")

            Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
            lPedidosCompra = Get_Pedidos_Compra_From_SAP(BeBodega.Codigo, True, pNoDocumentoSAP)

            BeNavEjecucionRes.Registros_ws = lPedidosCompra.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeProductoBodega As New clsBeProducto_bodega

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Pedidos de compra en relación con SAP (OPOR): {0} ", lPedidosCompra.Count))

            prg.Maximum = lPedidosCompra.Count

            'Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each BeINavPedCompra In lPedidosCompra

                    clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Procesando Pedido Compra: {0} ", BeINavPedCompra.No & " - " & BeINavPedCompra.Vendor_Invoice_No, vbNewLine))

                    If Not clsLnProveedor.Existe_Proveedor(BeINavPedCompra.Buy_From_Vendor_No) Then

                        BeConfigEnc = BeConfigEnc

                        If Inserta_Proveedor_Desde_SAP(BeINavPedCompra.Buy_From_Vendor_No, cnnLog) Then

                            clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & BeINavPedCompra.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")

                            clsLnLog_error_wms.Agregar_Error("#IF_SAP_PROV: El proveedor: " & BeINavPedCompra.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")

                        End If

                    End If

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                            BePedidoCompraEnc,
                                                                            vResult) Then
                        Marcar_PI_Sincronizado_SAP(BeINavPedCompra.No)

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("#IF_SAP_PED_COM: Se importó el Pedido de Compra: {0} ", BeINavPedCompra.No & " - " & BeINavPedCompra.Vendor_Invoice_No),
                                                                   BeINavPedCompra.No,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet,
                                                                   cnnLog)
                    End If

                    clsPublic.Actualizar_Progreso(lblprg, vResult)

                Next

            End If

            lTransaction.Commit()

            Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Ordenes de Compra desde SAP a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw ex

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function

    Public Shared Function Inserta_Proveedor_Desde_SAP(ByVal pCodigo As String,
                                                       ByVal cnnLog As SqlConnection) As Boolean

        Inserta_Proveedor_Desde_SAP = False


        Dim BeProveedor As New clsBeProveedor
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim BeSAPProveedor As New clsBeI_nav_proveedor
        Dim clstrans As New clsTransaccion

        Try

            clstrans.Begin_Transaction()

            BeSAPProveedor = clsSyncSAPProveedor.Get_Proveedor_SAP(pCodigo)

            If Not BeSAPProveedor Is Nothing Then

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(clstrans.lConnection, clstrans.lTransaction) + 1
                BeProveedor.Codigo = BeSAPProveedor.No
                BeProveedor.Nombre = BeSAPProveedor.Name
                BeProveedor.Telefono = BeSAPProveedor.Phone_No
                BeProveedor.Nit = BeSAPProveedor.VAT_Registratrion_No
                BeProveedor.Direccion = BeSAPProveedor.Adress
                BeProveedor.Contacto = BeSAPProveedor.Contact
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, clstrans.lConnection, clstrans.lTransaction)

                    VContadorBitacoraTOMWMS += 1

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(clstrans.lConnection, clstrans.lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.Insertar(BeProveedorBodega, clstrans.lConnection, clstrans.lTransaction)

                    clsSyncSAPProveedor.Marcar_Proveedor_Sincronizado_SAP(BeProveedor.Codigo)

                    Inserta_Proveedor_Desde_SAP = True

                    clstrans.lTransaction.Commit()

                Catch ex As Exception

                    clstrans.RollBack_Transaction()

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, cnnLog)

                    Throw ex

                End Try

            End If


        Catch ex As Exception
            Throw ex
        Finally
            clstrans.Close_Conection()
        End Try

    End Function

    Public Function Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                                   ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                   Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                   Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                                                   Optional ByVal pNoDocumentoSAP As String = "") As Boolean

        Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

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
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
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

            Application.DoEvents()

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog, pNoDocumentoSAP) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog, pNoDocumentoSAP) Then
                        Exit Function
                    End If
                End If

            End If

            lTransInterface.Commit()

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format(" -> Fin de proceso, tiempo transcurrido: {0} segundo(s)", difSegundos))

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

    Private Shared oCompany As Company
    Shared lRetCode, lErrCode As Long
    Shared sErrMsg As String = ""

    Public Function Get_Pedidos_Compra_From_SAP(ByVal CodBodega As String,
                                                Optional ByVal AplicarFiltros As Boolean = True,
                                                Optional ByVal pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_compra_enc)

        Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
        Dim BeINAVPedidoCompra As New clsBeI_nav_ped_compra_enc
        Dim BeINAVPedidoDetWMS As New clsBeI_nav_ped_compra_det
        Dim NoLinea As Integer = 0

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                Dim vSQLOC As String

                '#CKFK20240528 Cambiando DOCENTRY por DOCNUM 
                vSQLOC = " SELECT 
                             T0.DOCENTRY,
                             T0.DOCNUM,
                             T0.DOCDUEDATE AS DOCDATE,  
                             T0.CARDCODE, 
                             T0.CARDNAME, 
                             T0.DOCCUR, 
                             T0.DOCTOTAL, 
                             T0.JRNLMEMO, 
                             T0.CANCELED,
                             T0.DOCSTATUS, 
                             CASE WHEN T0.DOCTYPE = 'I'THEN 'ARTICULO' 
                                ELSE 'SERVICIO' 
                             END AS TIPO_ORDEN_VENTA, 
                              (SELECT TOP 1 D0.WhsCode  
                                  FROM POR1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode WHERE D0.DocEntry = T0.DOCENTRY) AS BODEGA, 
                              T0.COMMENTS, 
                              T0.NumAtCard, 
                              T0.Series,
                              T0.U_NContenedor                        
                                 FROM OPOR T0 
                                    WHERE DOCSTATUS = 'O' 
                                    AND CANCELED = 'N'                                     
                                    AND CreateDate >= '2020-10-09 00:00:00.000' 
                                    AND U_Enviado_WMS = 2 " &
                                    IIf(pNoDocumentoSAP <> "", " And T0.DocNum = " & pNoDocumentoSAP, "") &
                                    " AND (SELECT TOP 1 D0.WhsCode  
                                  FROM POR1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode WHERE D0.DocEntry = T0.DOCENTRY) = '" & CodBodega &
                                  "'  ORDER BY T0.DocEntry DESC"

                RsEnc.DoQuery(vSQLOC)

                While RsEnc.EoF = False

                    BeINAVPedidoCompra = New clsBeI_nav_ped_compra_enc()
                    BeINAVPedidoCompra.No = RsEnc.Fields.Item("DOCENTRY").Value
                    BeINAVPedidoCompra.Posting_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BeINAVPedidoCompra.Order_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BeINAVPedidoCompra.Document_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BeINAVPedidoCompra.Expected_Receipt_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BeINAVPedidoCompra.Status = 1
                    BeINAVPedidoCompra.Buy_From_Vendor_No = RsEnc.Fields.Item("CARDCODE").Value.ToString()
                    BeINAVPedidoCompra.Buy_From_Vendor_Name = RsEnc.Fields.Item("CARDNAME").Value.ToString()
                    BeINAVPedidoCompra.Is_Internal_Transfer = False
                    BeINAVPedidoCompra.Location_Code = Convert.ToString(RsEnc.Fields.Item("BODEGA").Value)
                    BeINAVPedidoCompra.Vendor_Invoice_No = Convert.ToString(RsEnc.Fields.Item("DOCNUM").Value).ToString()
                    BeINAVPedidoCompra.Posting_Description = RsEnc.Fields.Item("COMMENTS").Value.ToString()
                    BeINAVPedidoCompra.Product_Owner_Code = BeConfigEnc.IdPropietario
                    BeINAVPedidoCompra.Ship_To_Contact = RsEnc.Fields.Item("U_NContenedor").Value.ToString()

                    Dim vSerie As String = RsEnc.Fields.Item("SERIES").Value.ToString()

                    If vSerie = "79" Then
                        BeINAVPedidoCompra.IsImport = True
                    Else
                        BeINAVPedidoCompra.IsImport = False
                    End If

                    If BeINAVPedidoCompra.Vendor_Invoice_No = "" Then
                        BeINAVPedidoCompra.Vendor_Invoice_No = BeINAVPedidoCompra.No.ToString()
                    End If

                    Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                    Dim query_det As String

                    query_det = "SELECT T0.ITEMCODE,
                                        T0.DSCRIPTION,
                                        T0.QUANTITY,
                                        T0.PRICE,
                                        T0.LINETOTAL,
                                        T0.VATSUM,
                                        T0.DOCENTRY,
                                        T0.WHSCODE,
                                        T0.WHSCODE,
                                        T0.OPENCREQTY AS CANTIDAD_PENDIENTE,
                                        T0.BASELINE,
                                        T0.LINENUM,
                                        T1.BuyUnitMsr AS UNIDAD_MEDIDA  
                                        FROM POR1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode 
                                        WHERE T0.DOCENTRY = '" & BeINAVPedidoCompra.No & "' " &
                                        "AND T0.LINESTATUS = 'O' "
                    RsDet.DoQuery(query_det)

                    BeINAVPedidoCompra.Lineas_Detalle = New List(Of clsBeI_nav_ped_compra_det)

                    Dim vNoLineaBase As Integer = 0
                    Dim vNoLinea As Integer = 0

                    While RsDet.EoF = False

                        BeINAVPedidoDetWMS = New clsBeI_nav_ped_compra_det()
                        BeINAVPedidoDetWMS.NoEnc = RsDet.Fields.Item("DOCENTRY").Value.ToString()
                        BeINAVPedidoDetWMS.No = RsDet.Fields.Item("ITEMCODE").Value.ToString()

                        vNoLineaBase = IIf(IsDBNull(RsDet.Fields.Item("BASELINE").Value.ToString()), 0, RsDet.Fields.Item("BASELINE").Value.ToString())
                        vNoLinea = IIf(IsDBNull(RsDet.Fields.Item("LINENUM").Value.ToString()), 0, RsDet.Fields.Item("LINENUM").Value.ToString())

                        BeINAVPedidoDetWMS.Line_No = vNoLinea
                        BeINAVPedidoDetWMS.Planed_Receipt_Date = Date.Now()
                        BeINAVPedidoDetWMS.Quantity = Convert.ToDouble(RsDet.Fields.Item("CANTIDAD_PENDIENTE").Value)
                        BeINAVPedidoDetWMS.Quantity_Received = 0
                        BeINAVPedidoDetWMS.Description = clsPublic.Quitar_Caracteres_No_Permitidos(RsDet.Fields.Item("DSCRIPTION").Value.ToString())
                        BeINAVPedidoDetWMS.Unit_of_Measure_Code = (RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString())
                        BeINAVPedidoDetWMS.Type = 2
                        BeINAVPedidoDetWMS.Variant_Code = Nothing
                        BeINAVPedidoDetWMS.Location_Code = RsDet.Fields.Item("WHSCODE").Value.ToString()
                        BeINAVPedidoCompra.Lineas_Detalle.Add(BeINAVPedidoDetWMS)
                        NoLinea += 1
                        RsDet.MoveNext()

                    End While

                    lPedidosCompra.Add(BeINAVPedidoCompra)

                    RsEnc.MoveNext()

                End While

            End If

            Return lPedidosCompra

        Catch ex As Exception
            Throw ex
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function
    Public Function Marcar_PI_Sincronizado_SAP(ByVal pNoDocumento As String) As Boolean

        Marcar_PI_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim oPedidoSBO As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)

                If oPedidoSBO.GetByKey(pNoDocumento) Then

                    Try

                        oPedidoSBO.UserFields.Fields.Item("U_ENVIADO_WMS").Value = "1"
                        oPedidoSBO.Update()

                    Catch e As Exception
                    End Try

                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Private Shared Function Get_Objetos_Documento_Ingreso(ByVal IdOrdenCompraEnc As Integer,
                                                          ByVal IdRecepcionEnc As Integer,
                                                          ByVal IdBodega As Integer,
                                                          ByRef BeTransOCEnc As clsBeTrans_oc_enc,
                                                          ByRef BeReOC As clsBeTrans_re_oc,
                                                          ByRef vCodigoBodegaDestino As String,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As Boolean

        Get_Objetos_Documento_Ingreso = False

        Try

            BeTransOCEnc = clsLnTrans_oc_enc.Get_BeTransOcEnc_By_IdOrdenCompraEnc(IdOrdenCompraEnc,
                                                                                  lConnection,
                                                                                  lTransaction)

            If BeTransOCEnc Is Nothing Then
                Throw New Exception("ERROR_202310310531: No se obtuvo el objeto de la orden de compra para el IdOrdenCompraEnc: " & IdOrdenCompraEnc)
            End If

            BeReOC = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(IdOrdenCompraEnc,
                                                                                        IdRecepcionEnc,
                                                                                        lConnection,
                                                                                        lTransaction)

            If BeReOC Is Nothing Then
                Throw New Exception("ERROR_202310310532: No se obtuvo el objeto de la recepción de compra para el IdRecepcionEnc: " & IdRecepcionEnc)
            End If

            vCodigoBodegaDestino = clsLnBodega.Get_Codigo_By_IdBodega(IdBodega,
                                                                      lConnection,
                                                                      lTransaction)


            If vCodigoBodegaDestino = "" Then
                Throw New Exception("ERROR_202310310533: No se obtuvo el código de bodega destino sap para el IdBodega: " & IdBodega & " Bodega.Codigo=(vacio/no válido)")
            End If

            Get_Objetos_Documento_Ingreso = True

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Sub Enviar_Transacciones_De_Ingreso_SAP(ByRef lblprg As RichTextBox,
                                                          ByRef prg As Windows.Forms.ProgressBar)

        Dim lTransaccionesIngreso As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesIngresoSingle As New List(Of clsBeI_nav_transacciones_out)
        Dim TipoPedidoCompra As Integer = 0
        Dim Enviado_A_Erp As Boolean = False
        Dim vCodigoBodegaDestino As String = ""
        Dim BeProductoEstado_NC As New clsBeProducto_estado
        Dim BeBodegaUbicacion As New clsBeBodega_ubicacion
        Dim clsTrans As New clsTransaccion
        Dim BeOrdenCompra As New clsBeTrans_oc_enc
        Dim vTransaccionSQLAbierta As Boolean = False

        Try

            clsTrans.Begin_Transaction()
            vTransaccionSQLAbierta = True

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          clsTrans.lConnection,
                                                          clsTrans.lTransaction)

            lTransaccionesIngreso = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio(clsTrans.lConnection,
                                                                                                    clsTrans.lTransaction,
                                                                                                    BeConfigEnc.Idbodega)

            If Not lTransaccionesIngreso Is Nothing AndAlso lTransaccionesIngreso.Count > 0 Then

                Dim ListaPedidosCompra = (From i In lTransaccionesIngreso
                                          Group i By Keys = New With {Key i.No_pedido,
                                                                      Key i.Idordencompra,
                                                                      Key i.Idrecepcionenc,
                                                                      Key i.Idbodega} Into Group
                                          Select New With {Key Keys.No_pedido,
                                                           Key Keys.Idordencompra,
                                                           Key Keys.Idrecepcionenc,
                                                           Key Keys.Idbodega})

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesIngreso.Count))

                Dim BeReOC As New clsBeTrans_re_oc
                Dim BeTransOCEnc As New clsBeTrans_oc_enc
                Dim BeEstadoProductoBueno As New clsBeProducto_estado

                If Configuracion_Interface_Correcta(BeConfigEnc) Then

                    For Each DocumentoIngreso In ListaPedidosCompra

                        Try

                            If Get_Objetos_Documento_Ingreso(DocumentoIngreso.Idordencompra,
                                                             DocumentoIngreso.Idrecepcionenc,
                                                             DocumentoIngreso.Idbodega,
                                                             BeTransOCEnc,
                                                             BeReOC,
                                                             vCodigoBodegaDestino,
                                                             clsTrans.lConnection,
                                                             clsTrans.lTransaction) Then

                                TipoPedidoCompra = BeTransOCEnc.IdTipoIngresoOC

                                Enviado_A_Erp = ((BeReOC.No_docto <> "") AndAlso BeReOC.OC.Enviado_A_ERP)

                                Select Case TipoPedidoCompra

                                    Case tTipoDocumentoIngreso.Ingreso, tTipoDocumentoIngreso.Devolucion 'Es un pedido de compra de proveedor.

                                        If Not Enviado_A_Erp Then

                                            lTransaccionesIngresoSingle = lTransaccionesIngreso.FindAll(Function(x) x.No_pedido = DocumentoIngreso.No_pedido)

                                            If Enviar_Entrada_Mercancia_OC_SAP(BeConfigEnc,
                                                                               DocumentoIngreso.No_pedido,
                                                                               lTransaccionesIngresoSingle,
                                                                               BeTransOCEnc,
                                                                               vCodigoBodegaDestino,
                                                                               BeReOC.IdRecepcionEnc,
                                                                               BeReOC,
                                                                               lblprg,
                                                                               prg) Then

                                                Try

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Documento registrado correctamente: {0}", BeTransOCEnc.No_Documento))

                                                    BeReOC.No_docto = "ENV-WMS" & FormatoFechas.tFecha(Now)
                                                    clsLnTrans_re_oc.Actualizar_No_Docto(BeReOC, clsTrans.lConnection, clsTrans.lTransaction)
                                                    clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(DocumentoIngreso.Idordencompra, True)
                                                    clsLnLog_error_wms.Agregar_Error(String.Format("#IF_SAP_ENV_PED_COMP: Se registró correctamente EL INGRESO/DEVOLUCIÓN: {0}", BeTransOCEnc.No_Documento))

                                                Catch ex As Exception

                                                    If ex.Message = "There is nothing to post." Then 'Pedido sin nada que registrar
                                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Nada que registrar para pedido: {0} en NAV.", DocumentoIngreso.No_pedido))
                                                    Else
                                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar pedido de Ingreso WMS {0} en SAP: {1}", DocumentoIngreso.No_pedido, ex.Message))
                                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar pedido {0} en SAP: {1}", DocumentoIngreso.No_pedido, ex.Message),
                                                                                                  DocumentoIngreso.No_pedido,
                                                                                                  BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                  BeConfigDet.Idnavconfigdet)
                                                    End If

                                                End Try

                                            End If 'Fin enviar

                                        Else
                                            '#EJC20201119: aun no se que sucederá aquí....
                                            Try

                                                clsLnTrans_oc_enc.Actualizar_Estado_OC_By_Interface(DocumentoIngreso.Idordencompra,
                                                                                                    tEstadoOC.BACK_ORDER,
                                                                                                    clsTrans.lConnection,
                                                                                                    clsTrans.lTransaction)

                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Se registró el pedido:{0} correctamente en el ERP.", DocumentoIngreso.No_pedido))
                                                clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(DocumentoIngreso.Idordencompra, True)
                                                clsLnLog_error_wms.Agregar_Error(String.Format("#IF_SAP_ENV_PED_COMP: Se registró correctamente el documento: {0}", BeTransOCEnc.No_Documento))

                                            Catch ex As Exception

                                                If ex.Message = "There is nothing to post." Then 'Pedido sin nada que registrar
                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Nada que registrar para pedido: {0} en NAV.", DocumentoIngreso.No_pedido))
                                                Else

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar pedido de Ingreso WMS {0} en SAP: {1}", DocumentoIngreso.No_pedido, ex.Message))

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar pedido {0} en SAP: {1}", DocumentoIngreso.No_pedido, ex.Message),
                                                                                              DocumentoIngreso.No_pedido,
                                                                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                              BeConfigDet.Idnavconfigdet)

                                                End If

                                            End Try

                                        End If

                                    Case tTipoDocumentoIngreso.Transferencia_de_Ingreso 'Es un pedido de transferencia desde una bodega X hacia la bodega de WMS.

                                        If Not Enviado_A_Erp Then

                                            lTransaccionesIngresoSingle = lTransaccionesIngreso.FindAll(Function(x) x.No_pedido = DocumentoIngreso.No_pedido)

                                            Debug.WriteLine(BeTransOCEnc.ProveedorBodega.Proveedor.Codigo)

                                            Enviar_Solicitud_Traslado_SAP(lTransaccionesIngresoSingle,
                                                                          BeTransOCEnc.ProveedorBodega.Proveedor.Codigo,
                                                                          vCodigoBodegaDestino,
                                                                          DocumentoIngreso.No_pedido,
                                                                          DocumentoIngreso.Idordencompra,
                                                                          lblprg,
                                                                          prg)
                                        End If

                                    Case Else

                                        clsPublic.Actualizar_Progreso(lblprg, "#MSG_240609EJC: El registro de ingreso no está considerado dentro de la tipología de envío: " & TipoPedidoCompra.ToString())

                                End Select

                            End If


                        Catch ex As Exception
                            Dim vMensaje As String = String.Format(vbTab & "{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
                            clsPublic.Actualizar_Progreso(lblprg, vMensaje)
                        End Try

                    Next

                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay registros de ingreso para envío a SAP.")
            End If

            '#EJC20260602_SYNC_INGRESO_SAP: Cerramos la transaccion SQL de lectura/marcas del barrido.
            ' Carol, aqui cerre explicitamente la transaccion porque la interface la abria para revisar ingresos,
            ' pero el cierre no quedaba claro en este flujo. Asi evitamos locks largos y es mas facil auditar.
            clsTrans.Commit_Transaction()
            vTransaccionSQLAbierta = False

            clsPublic.Actualizar_Progreso(lblprg, "Fin de procesamiento.")

        Catch ex As Exception
            If vTransaccionSQLAbierta Then
                clsTrans.RollBack_Transaction()
                vTransaccionSQLAbierta = False
            End If
            Throw ex
        Finally
            prg.Value = 0
            prg.Visible = False
        End Try

    End Sub

    Private Shared Function Configuracion_Interface_Correcta(ByVal BeINavConfigEnc As clsBeI_nav_config_enc) As Boolean

        Configuracion_Interface_Correcta = False

        Dim BeEstadoProductoBueno As New clsBeProducto_estado
        Dim BeBodegaUbicacion As New clsBeBodega_ubicacion

        Try

            If BeConfigEnc Is Nothing Then
                Throw New Exception("ERROR_20231031: No está definida la configuración de interface")
            Else

                If BeConfigEnc.IdProductoEstado = 0 Then
                    Throw New Exception("ERROR_20231031A: El IdProductoEstado (que define el producto en buen estado) no está configurado en la interface")
                Else

                    BeEstadoProductoBueno = clsLnProducto_estado.Get_Single_By_IdEstado(BeConfigEnc.IdProductoEstado)

                    If BeEstadoProductoBueno Is Nothing Then
                        Throw New Exception("ERROR_20231031B: No se obtuvo el objeto de estado para el BeConfigEnc.IdProductoEstado: " & BeConfigEnc.IdProductoEstado)
                    End If

                End If

            End If

            If BeConfigEnc.IdProductoEstado_NC <> 0 Then

                BeEstadoProductoBueno = clsLnProducto_estado.Get_Single_By_IdEstado(BeINavConfigEnc.IdProductoEstado_NC)

                If BeEstadoProductoBueno Is Nothing Then
                    Throw New Exception("ERROR_CONFIGURACION_20231025: No se encontró el estado de producto por defecto para la gestión de inventario teórico en N.C.")
                End If

            Else
                Throw New Exception("ERROR_CONFIGURACION_20231025A: No se encontró el estado de producto por defecto para la gestión de inventario teórico en N.C.")
            End If

            Configuracion_Interface_Correcta = True

        Catch ex As Exception
            Throw
        End Try

    End Function

    Dim vControlVencimientoProductoWMS As Boolean = False
    Private disposedValue As Boolean

    Public Shared Function Enviar_Entrada_Mercancia_OC_SAP(ByVal BeINavConfigEnc As clsBeI_nav_config_enc,
                                                           ByVal _Docentry As Integer,
                                                           ByVal lINav_Transaccioens_Out As List(Of clsBeI_nav_transacciones_out),
                                                           ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                           ByVal vCodigoBodegaDestino As String,
                                                           ByVal IdRecepcionEnc As Integer,
                                                           ByVal BeTransReOC As clsBeTrans_re_oc,
                                                           ByRef lblprg As RichTextBox,
                                                           ByRef prg As Windows.Forms.ProgressBar) As Boolean


        Enviar_Entrada_Mercancia_OC_SAP = False

        prg.Maximum = lINav_Transaccioens_Out.Count
        prg.Visible = True

        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim lLotesPorProductoYLinea As New List(Of clsBeI_nav_transacciones_out)
        Dim BeProducto As New clsBeProducto()
        Dim BeProductoEstado As New clsBeProducto_estado
        Dim BeBodegaUbicacion As New clsBeBodega_ubicacion
        Dim BeProductoEstado_NC As New clsBeProducto_estado
        Dim BeLineaDetalleOC As New clsBeTrans_oc_det
        Dim lDetalleOC As New List(Of clsBeTrans_oc_det)
        Dim vControlVencimiento As Boolean = False
        Dim vProductoRecibido As Boolean = False
        Dim vContadorLote As Integer = 0
        Dim vCorrelativoInternalSerialNumber As String = 0
        Dim vItemCode As String = ""
        Dim vCodigoProductoOCSAP As String = ""
        Dim vNoLineaOCSAP As Integer = 0
        Dim BeTipoIngreso As New clsBeTrans_oc_ti
        Dim vRecibioProductoNoApto As Boolean = False
        Dim vLoteBuenEstado As Boolean = True
        Dim lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim BeTransReEnc As New clsBeTrans_re_enc
        Dim EnvioEntradaBuenEstado As Boolean = False
        Dim EnvioEntradaMalEstado As Boolean = False
        Dim EnvioEntradaTeorico As Boolean = False
        Dim clsTransSAP As clsSapTransaction = Nothing

        Try

            Conectar_A_SAP(oCompany, False, lRetCode, sErrMsg)

            Application.DoEvents()

            If lRetCode <> 0 Then

                If sErrMsg.Contains(" - The specified resource name cannot be found in the image file.") Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If

            Else

                If Documento_Ingreso_Ya_Existe_En_SAP(BeTransReOC, BeTransOCEnc.TipoIngreso.IdTipoIngresoOC, lblprg) Then
                    '#EJC20260602_SYNC_INGRESO_SAP: Carol, agregue esta salida porque a veces el problema no es crear,
                    ' sino reconocer que SAP ya tiene el documento. Si la referencia real existe en SAP, no lo volvemos
                    ' a crear; dejamos que WMS marque/reconcilie el envio con esa evidencia.
                    Return True
                End If

                '#EJC20260602_SYNC_INGRESO_SAP: Killios ya protege este paquete con StartTransaction/EndTransaction.
                ' Carol, agregue esto porque SAP podia crear la entrada y luego fallar una marca
                ' local en TOMWMS; al siguiente sync, WMS la veia pendiente y la enviaba otra vez. Desde aqui
                ' SAP queda "en espera" hasta que la entrada, el update/cierre de la OC y las marcas locales
                ' pasen completas. Si falla algo, SAP hace rollback.
                clsTransSAP = New clsSapTransaction(oCompany)
                clsTransSAP.BeginTransaction()

                EnvioEntradaBuenEstado = Enviar_Entrada_Mercancia_Sin_Estado_SAP_B1(BeINavConfigEnc,
                                                                                    _Docentry,
                                                                                    lINav_Transaccioens_Out,
                                                                                    BeTransOCEnc,
                                                                                    vCodigoBodegaDestino,
                                                                                    BeTransReOC,
                                                                                    lblprg,
                                                                                    prg)

                If BeTipoIngreso.Es_Importacion Then

                    EnvioEntradaTeorico = Enviar_Entrada_Mercancia_Teorica_SAP(BeINavConfigEnc,
                                                                               BeProductoEstado_NC,
                                                                               _Docentry,
                                                                               lINav_Transaccioens_Out,
                                                                               BeTransOCEnc,
                                                                               vCodigoBodegaDestino,
                                                                               lblprg,
                                                                               prg,
                                                                               True)

                End If

                Dim oOrderPurchase As Documents
                oOrderPurchase = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)

                Dim vMensajeError As String = "#ERROR_SAP_20231030: Error al actualizar la Orden de Compra:" & sErrMsg
                Dim vMensajeResultado As String = ""

                If oOrderPurchase.GetByKey(_Docentry) Then

                    Select Case BeTransOCEnc.TipoIngreso.IdTipoIngresoOC

                        Case tTipoDocumentoIngreso.Devolucion

                            vMensajeError = "#ERROR_SAP_20231030: Error al actualizar la Orden de Compra:" & sErrMsg

                    End Select

                    BeTransReEnc = clsLnTrans_re_enc.GetSingle(IdRecepcionEnc)
                    Dim vTieneDiferencia As Boolean = False

                    If Not BeTransReEnc Is Nothing Then

                        vTieneDiferencia = Detalle_Tiene_Diferencia_Vrs_OC(BeTransOCEnc,
                                                                           BeTransOCEnc.DetalleOC,
                                                                           BeTransReEnc,
                                                                           BeTransReEnc.Detalle)

                    End If

                    If vTieneDiferencia AndAlso Not BeTransOCEnc.IdEstadoOC = tEstadoOC.BACK_ORDER Then

                        Dim errCode As Integer
                        Dim errMsg As String = ""

                        ' Actualiza el documento en la base de datos
                        errCode = oOrderPurchase.Close()

                        If errCode <> 0 Then
                            oCompany.GetLastError(errCode, errMsg)
                            clsPublic.Actualizar_Progreso(lblprg, "El documento en SAP no se pudo marcar como cerrado aparentemente.")
                            clsPublic.Actualizar_Progreso(lblprg, "Mensaje de SAP vía DIAPI: " & errMsg)
                            clsLnLog_error_wms.Agregar_Error("El documento en SAP no se pudo marcar como cerrado aparentemente." &
                                                             " Mensaje de SAP vía DIAPI: " & errMsg)
                        Else
                            Console.WriteLine("Se cerró el documento de compra en SAP.")
                            clsPublic.Actualizar_Progreso(lblprg, "Se cerró el documento de compra")
                            clsLnLog_error_wms.Agregar_Error("LOG20241027: Se cerró el documento de compra en SAP.")
                        End If

                    Else

                        If oOrderPurchase.Update() <> 0 Then
                            sErrMsg = oCompany.GetLastErrorDescription()
                            clsPublic.Actualizar_Progreso(lblprg, vMensajeError)
                            clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(BeTransOCEnc.IdOrdenCompraEnc, False)
                            clsLnLog_error_wms.Agregar_Error("LOG20241027A: Error al actualizar documento en sap: " & vMensajeError)
                        Else
                            clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(BeTransOCEnc.IdOrdenCompraEnc, True)
                            If BeTransOCEnc.TipoIngreso.IdTipoIngresoOC = tTipoDocumentoIngreso.Ingreso Then
                                vMensajeResultado = "Pedido de compra actualizado a estado: " & IIf(oOrderPurchase.DocumentStatus = BoStatus.bost_Close, "Cerrado", "Abierto")
                            ElseIf BeTransOCEnc.TipoIngreso.IdTipoIngresoOC = tTipoDocumentoIngreso.Devolucion Then
                                vMensajeResultado = "Solicitud de Devolución actualizada a estado: " & IIf(oOrderPurchase.DocumentStatus = BoStatus.bost_Close, "Cerrado", "Abierto")
                            End If
                            clsPublic.Actualizar_Progreso(lblprg, vMensajeResultado)
                            clsLnLog_error_wms.Agregar_Error("#IF_SAP_ENV_ENTRADA_OC: Se actualizó el documento en sap: " & vMensajeResultado)
                        End If

                    End If

                Else
                    Throw New Exception(String.Format("#ERROR_SAP_20260602: No se encontro en SAP la orden base DocEntry {0}.", _Docentry))
                End If

            End If

            clsTransSAP.CommitTransaction()

            Return True

        Catch ex As Exception

            If clsTransSAP IsNot Nothing Then clsTransSAP.RollbackTransaction()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)

        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Shared Function Enviar_Entrada_Mercancia_Teorica_SAP(ByVal BeINavConfigEnc As clsBeI_nav_config_enc,
                                                                ByVal BeProductoEstado_NC As clsBeProducto_estado,
                                                                ByVal _Docentry As Integer,
                                                                ByVal lINav_Transaccioens_Out As List(Of clsBeI_nav_transacciones_out),
                                                                ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                                ByVal vCodigoBodegaDestino As String,
                                                                ByRef lblprg As RichTextBox,
                                                                ByRef prg As Windows.Forms.ProgressBar,
                                                                Optional ByVal pUsarConexionSAPActual As Boolean = False) As Boolean


        prg.Maximum = lINav_Transaccioens_Out.Count
        prg.Visible = True

        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim BeProducto As New clsBeProducto()
        Dim vControlVencimiento As Boolean = False
        Dim clsTransaccion As New clsTransaccion
        Dim BeLineaDetalleOC As New clsBeTrans_oc_det
        Dim lDetalleOC As New List(Of clsBeTrans_oc_det)
        Dim vContadorLote As Integer = 0
        Dim vCorrelativoInternalSerialNumber As String = 0

        Try

            If Not pUsarConexionSAPActual Then
                Conectar_A_SAP(oCompany, False, lRetCode, sErrMsg)
            Else
                lRetCode = 0
                sErrMsg = ""
            End If

            Application.DoEvents()

            If lRetCode <> 0 Then
                If sErrMsg.Contains(" - The specified resource name cannot be found in the image file.") Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If
            Else

                Dim oEntrega As Documents
                Dim oOrderPurchase As Documents

                oEntrega = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes), Documents)
                oOrderPurchase = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)

                If oOrderPurchase.GetByKey(_Docentry) Then

                    oEntrega.CardCode = oOrderPurchase.CardCode
                    oEntrega.DocDate = Date.Today
                    oEntrega.DocObjectCode = BoObjectTypes.oPurchaseDeliveryNotes
                    oEntrega.AgentCode = "WMS"

                    Dim n As Integer = 0

                    lblprg.AppendText(vbNewLine)

                    clsTransaccion.Begin_Transaction()

                    lDetalleOC = BeTransOCEnc.DetalleOC

                    Dim ListaResumen = (From i In lINav_Transaccioens_Out.Where(Function(x) x.No_pedido = _Docentry)
                                        Group i By Keys = New With {Key i.No_pedido, Key i.No_linea,
                                                                    Key i.Codigo_producto,
                                                                    Key i.Enviado} Into Group
                                        Select New With {Keys.No_pedido,
                                                         Keys.No_linea,
                                                         Keys.Codigo_producto,
                                                         Keys.Enviado,
                                                        .Cantidad = Group.Sum(Function(x) x.Cantidad)})

                    For j As Integer = 0 To oOrderPurchase.Lines.Count - 1

                        oOrderPurchase.Lines.SetCurrentLine(j)

                        If oOrderPurchase.Lines.LineStatus = BoStatus.bost_Open Then

                            Dim lLotesPorProductoYLinea As New List(Of clsBeI_nav_transacciones_out)
                            lLotesPorProductoYLinea = lINav_Transaccioens_Out.FindAll(Function(x) x.Codigo_producto = oOrderPurchase.Lines.ItemCode.ToString() AndAlso x.No_linea = oOrderPurchase.Lines.LineNum)

                            BeLineaDetalleOC = lDetalleOC.Find(Function(x) x.Codigo_Producto = oOrderPurchase.Lines.ItemCode.ToString() _
                                                               AndAlso x.No_Linea = oOrderPurchase.Lines.LineNum)

                            If Not BeLineaDetalleOC Is Nothing Then

                                If Not lLotesPorProductoYLinea Is Nothing Then

                                    Dim vItemCode As String = ""
                                    Dim vBaseLineOC As Integer = oOrderPurchase.Lines.LineNum

                                    For Each LineaIngresoResumida In ListaResumen

                                        vItemCode = LineaIngresoResumida.Codigo_producto

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format(vbTab & "Procesando Producto: {0} - Cantidad: {1}.",
                                                                        vItemCode,
                                                                        LineaIngresoResumida.Cantidad))

                                        Dim vDiferenciaPorLineaIngreso As Double = Math.Round(BeLineaDetalleOC.Cantidad - BeLineaDetalleOC.Cantidad_recibida, 6)

                                        If vDiferenciaPorLineaIngreso > 0 Then

                                            Dim vTipoImpuesto As String = oOrderPurchase.Lines.TaxCode

                                            oEntrega.Lines.SetCurrentLine(n)
                                            oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_PurchaseOrder)
                                            oEntrega.Lines.BaseEntry = _Docentry
                                            oEntrega.Lines.ItemCode = vItemCode
                                            oEntrega.Lines.BaseLine = vBaseLineOC
                                            oEntrega.Lines.TaxCode = vTipoImpuesto
                                            oEntrega.Lines.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                                            oEntrega.Lines.Quantity = vDiferenciaPorLineaIngreso
                                            oEntrega.Lines.WarehouseCode = BeINavConfigEnc.Codigo_Bodega_ERP_NC

                                            ' Copiar gastos (expenses) desde el documento base
                                            For k As Integer = 0 To oOrderPurchase.Expenses.Count - 1
                                                oOrderPurchase.Expenses.SetCurrentLine(k)
                                                oEntrega.Expenses.ExpenseCode = oOrderPurchase.Expenses.ExpenseCode
                                                oEntrega.Expenses.LineTotal = oOrderPurchase.Expenses.LineTotal
                                                oEntrega.Expenses.Add()
                                            Next

                                            vControlVencimiento = clsLnProducto.Get_Control_Lote_By_Codigo(vItemCode,
                                                                                                           clsTransaccion.lConnection,
                                                                                                           clsTransaccion.lTransaction)

                                            If vControlVencimiento Then

                                                vContadorLote = 0

                                                If vDiferenciaPorLineaIngreso > 0 Then

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Generando entrada de mercancía teórica para documento: {0}", _Docentry))

                                                    If vControlVencimiento Then

                                                        oEntrega.Lines.BatchNumbers.SetCurrentLine(vContadorLote)
                                                        oEntrega.Lines.BatchNumbers.BaseLineNumber = oEntrega.Lines.LineNum
                                                        oEntrega.Lines.BatchNumbers.ItemCode = LineaIngresoResumida.Codigo_producto
                                                        oEntrega.Lines.BatchNumbers.BatchNumber = BeINavConfigEnc.Lote_Defecto_Entrada_NC
                                                        oEntrega.Lines.BatchNumbers.Quantity = vDiferenciaPorLineaIngreso
                                                        oEntrega.Lines.BatchNumbers.ExpiryDate = New Date(2999, 1, 1)
                                                        oEntrega.Lines.BatchNumbers.InternalSerialNumber = "Docentry_" & _Docentry
                                                        oEntrega.Lines.BatchNumbers.Location = BeINavConfigEnc.Codigo_Bodega_ERP_NC
                                                        oEntrega.Lines.BatchNumbers.AddmisionDate = Now
                                                        oEntrega.Lines.BatchNumbers.Add()

                                                        clsPublic.Actualizar_Progreso(lblprg, String.Format(vbTab & "Procesando Lote (teórico): {0} - Cantidad: {1} Vence: {2} ",
                                                                                    BeINavConfigEnc.Lote_Defecto_Entrada_NC,
                                                                                    vDiferenciaPorLineaIngreso,
                                                                                    oEntrega.Lines.BatchNumbers.ExpiryDate))
                                                    End If

                                                End If

                                            End If

                                            oEntrega.Lines.Add()
                                            n += 1

                                            Dim BeStockPushAlmacenaje As New clsBeStock
                                            BeStockPushAlmacenaje = New clsBeStock()
                                            BeStockPushAlmacenaje.IdBodega = BeINavConfigEnc.Idbodega
                                            BeStockPushAlmacenaje.IdStock = 0 'clsLnStock.MaxID() + 1
                                            BeProducto = clsLnProducto.Get_Single_By_Codigo(vItemCode)
                                            BeStockPushAlmacenaje.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BeProducto.IdProducto, BeINavConfigEnc.Idbodega)
                                            BeStockPushAlmacenaje.IdPropietarioBodega = BeINavConfigEnc.IdPropietario
                                            BeStockPushAlmacenaje.IdProductoEstado = BeINavConfigEnc.IdProductoEstado_NC
                                            BeStockPushAlmacenaje.ProductoEstado.IdEstado = BeINavConfigEnc.IdProductoEstado_NC
                                            BeStockPushAlmacenaje.Presentacion.IdPresentacion = 0
                                            BeStockPushAlmacenaje.IdUnidadMedida = BeProducto.IdUnidadMedidaBasica
                                            BeStockPushAlmacenaje.IdUbicacion = BeProductoEstado_NC.IdUbicacionDefecto
                                            BeStockPushAlmacenaje.IdUbicacion_anterior = BeStockPushAlmacenaje.IdUbicacion
                                            If BeProducto.Control_lote Then BeStockPushAlmacenaje.Lote = BeINavConfigEnc.Lote_Defecto_Entrada_NC
                                            If BeProducto.Control_vencimiento Then BeStockPushAlmacenaje.Fecha_vence = New Date(2026, 7, 1)
                                            BeStockPushAlmacenaje.Fecha_Ingreso = Now
                                            BeStockPushAlmacenaje.Cantidad = 1
                                            BeStockPushAlmacenaje.Activo = True
                                            BeStockPushAlmacenaje.Peso = 0
                                            BeStockPushAlmacenaje.Temperatura = 0
                                            BeStockPushAlmacenaje.Fec_agr = Now
                                            BeStockPushAlmacenaje.Fec_mod = Now
                                            BeStockPushAlmacenaje.User_agr = BeINavConfigEnc.IdUsuario
                                            BeStockPushAlmacenaje.User_mod = BeINavConfigEnc.IdUsuario
                                            BeStockPushAlmacenaje.Pallet_No_Estandar = False
                                            BeStockPushAlmacenaje.Lic_plate = FormatoFechas.tFechaHora(Now)
                                            clsLnStock.Insertar(BeStockPushAlmacenaje)

                                        End If

                                    Next

                                End If

                            Else
                                Throw New Exception("ERROR_202310310600: No se encontrò el objeto relacionado con la lìnea.")
                            End If

                        End If

                    Next

                    If oEntrega.Lines.Count > 0 Then

                        Dim oResultado As Integer
                        oResultado = oEntrega.Add()

                        If oResultado <> 0 Then
                            sErrMsg = oCompany.GetLastErrorDescription()
                            Throw New Exception("#ERROR_SAP_202309270131: " & sErrMsg)
                        Else

                            Enviar_Entrada_Mercancia_Teorica_SAP = True

                        End If

                        clsLnLog_error_wms.Agregar_Error(String.Format("#IF_SAP_EM_TEO: Se envió correctamente el documento de entrada de mercancía teórica: {0}", _Docentry))

                    Else

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay líneas pendientes para generar entrega de documento: {0}", _Docentry))

                    End If

                End If

            End If

            clsTransaccion.Commit_Transaction()

            Return True

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet)
            Throw ex

        Finally
            If Not pUsarConexionSAPActual Then Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

    End Function

    ''' <summary>
    ''' #EJC20231121_DECIMO_OCTAVA
    ''' </summary>
    ''' <param name="beINavConfigEnc"></param>
    ''' <param name="_Docentry"></param>
    ''' <param name="lINavTransaccionesOut"></param>
    ''' <param name="beTransOCEnc"></param>
    ''' <param name="codigoBodegaDestino"></param>
    ''' <param name="lblPrg"></param>
    ''' <param name="prg"></param>
    ''' <returns></returns>
    Public Shared Function Enviar_Entrada_Mercancia_Sin_Estado_SAP_B1(ByVal beINavConfigEnc As clsBeI_nav_config_enc,
                                                                      ByVal _Docentry As Integer,
                                                                      ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                                      ByVal beTransOCEnc As clsBeTrans_oc_enc,
                                                                      ByVal codigoBodegaDestino As String,
                                                                      ByVal beTransReOC As clsBeTrans_re_oc,
                                                                      ByRef lblPrg As RichTextBox,
                                                                      ByRef prg As Windows.Forms.ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Value = 0
        prg.Visible = True

        Dim oOrderPurchase As Documents = Nothing
        Dim result As Boolean = False
        Dim NoLineaSAPEntrega As Integer = 0

        Try

            Select Case beTransOCEnc.TipoIngreso.IdTipoIngresoOC

                Case tTipoDocumentoIngreso.Ingreso

                    oOrderPurchase = DirectCast(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)
                    If oOrderPurchase IsNot Nothing AndAlso oOrderPurchase.GetByKey(_Docentry) Then
                        result = Procesar_Detalle_Ingreso(oOrderPurchase,
                                                          beTransOCEnc.TipoIngreso.IdTipoIngresoOC,
                                                          lINavTransaccionesOut,
                                                          beTransReOC,
                                                          lblPrg)
                    Else
                        clsPublic.Actualizar_Progreso(lblPrg, "ERROR_202311212019: No se pudo obtener el documento de devolución de sap para el _Docentry: " & _Docentry)
                    End If

                Case tTipoDocumentoIngreso.Devolucion

                    oOrderPurchase = DirectCast(oCompany.GetBusinessObject(BoObjectTypes.oReturnRequest), Documents)

                    If oOrderPurchase IsNot Nothing AndAlso oOrderPurchase.GetByKey(_Docentry) Then
                        Procesar_Detalle_Ingreso_Devolucion(oOrderPurchase,
                                                            beTransOCEnc.TipoIngreso.IdTipoIngresoOC,
                                                            lINavTransaccionesOut,
                                                            _Docentry,
                                                            lblPrg)
                    Else
                        clsPublic.Actualizar_Progreso(lblPrg, "ERROR_202311212019: No se pudo obtener el documento de devolución de sap para el _Docentry: " & _Docentry)
                    End If

            End Select

        Catch ex As Exception
            Throw
        Finally

            If oOrderPurchase IsNot Nothing Then Marshal.ReleaseComObject(oOrderPurchase)
            prg.Value = 0
            prg.Visible = False

        End Try

        Return result

    End Function

    Private Shared Sub Configurar_Entrega_Compra(ByRef oEntrega As Documents, oOrderPurchase As Documents)
        oEntrega = oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes)
        oEntrega.CardCode = oOrderPurchase.CardCode
        oEntrega.DocDate = Date.Today
        oEntrega.DocCurrency = oOrderPurchase.DocCurrency
        oEntrega.DocObjectCode = BoObjectTypes.oPurchaseDeliveryNotes
    End Sub

    Private Shared Sub Configurar_Devolucion_Cliente(ByRef oReturn As Documents, oReturnRequest As Documents, _Docentry As Integer)
        oReturn = oCompany.GetBusinessObject(BoObjectTypes.oReturns)
        oReturn.CardCode = oReturnRequest.CardCode
        oReturn.DocDate = Date.Today
        oReturn.DocCurrency = oReturnRequest.DocCurrency
        oReturn.DocObjectCode = BoObjectTypes.oReturns
        oReturn.BaseEntry = _Docentry
    End Sub

    Public Shared Function Enviar_Solicitud_Traslado_SAP(ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                         ByVal FromWhs As String,
                                                         ByVal ToWhs As String,
                                                         ByVal vTrasladoDocEntry As String,
                                                         ByVal IdPedidoEnc As Integer,
                                                         ByRef lblprg As RichTextBox,
                                                         ByRef prg As Windows.Forms.ProgressBar) As Boolean

        Enviar_Solicitud_Traslado_SAP = False

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

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
        Dim clsTransSAP As clsSapTransaction = Nothing

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            Application.DoEvents()

            If lErrCode <> 0 Then

                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If

            Else

                Dim oTransfer As StockTransfer
                Dim BaseLine As Integer = 0

                oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)

                NoLineaTransfer = 0 : NoLineaTransferLote = 0

                clsTransaccion.Open_Connection()
                clsTransSAP = New clsSapTransaction(oCompany)
                clsTransSAP.BeginTransaction()

                Dim DistinctIdPedidoEncByTraslado = lINavTransaccionesOut.Where(Function(x) x.IdTipoDocumento = tTipoDocumentoIngreso.Transferencia_de_Ingreso AndAlso x.Enviado = False).
                                                                GroupBy(Function(x) New With {Key x.Idpedidoenc, Key x.Codigo_producto, Key x.No_linea}).
                                                                Select(Function(g) New With {
                                                                    .IdPedidoEnc = g.Key.Idpedidoenc,
                                                                    .Codigo_Producto = g.Key.Codigo_producto,
                                                                    .No_Linea = g.Key.No_linea,
                                                                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                                }).ToList()

                If DistinctIdPedidoEncByTraslado.Any() Then

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Traslados encontrados: {0} ", DistinctIdPedidoEncByTraslado.Count))

                    Dim oStockTransferRequest As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                    If oStockTransferRequest.GetByKey(vTrasladoDocEntry) Then

                        For j As Integer = 0 To oStockTransferRequest.Lines.Count - 1

                            oStockTransferRequest.Lines.SetCurrentLine(j)

                            Dim vCodigoProductoSAP As String = oStockTransferRequest.Lines.ItemCode.ToString()
                            Dim vNoLineaSTSAP As Integer = oStockTransferRequest.Lines.LineNum

                            For Each ProductoSalida In DistinctIdPedidoEncByTraslado.Where(Function(x) x.Codigo_Producto = vCodigoProductoSAP AndAlso x.No_Linea = vNoLineaSTSAP)

                                oTransfer.FromWarehouse = FromWhs
                                oTransfer.ToWarehouse = ToWhs
                                oTransfer.Comments = "Traslado generado por TOMWMS Ref: " & vTrasladoDocEntry
                                oTransfer.UserFields.Fields.Item("U_FIS").Value = "N"
                                oTransfer.JournalMemo = IdPedidoEnc

                                Dim nuevaLineaTransfer As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_Producto OrElse vNoLineaAnterior <> ProductoSalida.No_Linea)

                                If nuevaLineaTransfer Then

                                    oTransfer.Lines.SetCurrentLine(NoLineaTransfer)
                                    oTransfer.Lines.BaseEntry = Integer.Parse(vTrasladoDocEntry)
                                    oTransfer.Lines.BaseType = InvBaseDocTypeEnum.InventoryTransferRequest
                                    oTransfer.Lines.BaseLine = vNoLineaSTSAP
                                    oTransfer.Lines.ItemCode = ProductoSalida.Codigo_Producto
                                    oTransfer.Lines.Quantity = ProductoSalida.Cantidad_Total
                                    oTransfer.Lines.FromWarehouseCode = FromWhs
                                    oTransfer.Lines.WarehouseCode = ToWhs
                                    vCodigoAnterior = oTransfer.Lines.ItemCode
                                    vNoLineaAnterior = ProductoSalida.No_Linea
                                    oTransfer.Lines.Add() : NoLineaTransfer += 1

                                    Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_linea = ProductoSalida.No_Linea _
                                                                                              AndAlso x.Codigo_producto = ProductoSalida.Codigo_Producto _
                                                                                              AndAlso x.Enviado = False)
                                    If Not Sublista_A_Actualizar Is Nothing Then
                                        If Sublista_A_Actualizar.Count > 0 Then
                                            Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                        End If
                                    End If

                                End If

                            Next

                        Next

                    End If

                    If Lista_A_Actualizar Is Nothing OrElse Lista_A_Actualizar.Count = 0 Then
                        Throw New Exception(String.Format("#ERROR_SAP_20260602_SOL_TRAS: La solicitud {0} no genero lineas para actualizar en WMS; se revierte SAP para no dejar traslado huerfano.", vTrasladoDocEntry))
                    End If

                    'DistinctProductosLineas

                    vAgregarEntrega = True

                Else
                    Throw New Exception(String.Format("#ERROR_SAP_20260602_SOL_TRAS: No hay lineas pendientes de transferencia para la solicitud {0}.", vTrasladoDocEntry))
                End If

                Dim oResultado As Integer
                oResultado = oTransfer.Add()

                If oResultado <> 0 Then
                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                Else

                    ' Obtener el DocEntry del traslado generado
                    Dim newObjectCode As String = ""
                    oCompany.GetNewObjectCode(newObjectCode)

                    If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                        Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                    End If

                    '#EJC20260602_SYNC_INGRESO_SAP: Validacion de existencia real en SAP antes de marcar WMS.
                    ' Carol, aqui valido el DocEntry porque no basta con que Add() diga ok; WMS solo debe marcar
                    ' enviado si SAP puede volver a encontrar el traslado que acaba de crear.
                    Dim oTransferCreado As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
                    If Not oTransferCreado.GetByKey(vTrasladoDocEntry) Then
                        Throw New Exception(String.Format("#ERROR_SAP_20260602_SOL_TRAS: SAP devolvio DocEntry {0}, pero no se pudo validar el traslado creado.", vTrasladoDocEntry))
                    End If

                    Dim vDocNumTraslado As String = Obtener_Doc_Num(oCompany, vTrasladoDocEntry)
                    Dim vMensaje As String = "Se creó el traslado con DocNum: " & vDocNumTraslado & " en SAP."

                    clsPublic.Actualizar_Progreso(lblprg, vMensaje)

                    If Not Lista_A_Actualizar Is Nothing Then
                        If Lista_A_Actualizar.Count > 0 Then
                            For Each T In Lista_A_Actualizar
                                T.Enviado = True
                                T.no_documento_salida_ref_devol = vTrasladoDocEntry
                                T.Fec_mod = Now
                                clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(T)
                            Next
                        End If
                    End If

                End If

                clsTransaccion.Commit_Transaction()
                clsTransSAP.CommitTransaction()

                clsLnLog_error_wms.Agregar_Error(String.Format("#IF_SAP_SOL_TRAS_ENV: Se envió correctamente el documento de traslado: {0}", vTrasladoDocEntry))

            End If

            Return True

        Catch errMsg As Exception

            clsTransaccion.RollBack_Transaction()
            If clsTransSAP IsNot Nothing Then clsTransSAP.RollbackTransaction()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar traslado entre almacenes a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar traslado entre almacenes a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            clsPublic.Actualizar_Progreso(lblprg, errMsg.Message)
        Finally
            'Desconectar_SAP(oCompany) No cerrar porque viene en transacción SAP Desde el traslado.
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Private Shared Function Detalle_Tiene_Diferencia_Vrs_OC(ByVal pObjOC As clsBeTrans_oc_enc,
                                                            ByVal pListObjOrdeCompraDet As List(Of clsBeTrans_oc_det),
                                                            ByVal gBeRecepcion As clsBeTrans_re_enc,
                                                            ByVal pListRecepcionDetalle As List(Of clsBeTrans_re_det)) As Boolean

        Detalle_Tiene_Diferencia_Vrs_OC = False

        Try

            Dim vPresRec As New clsBeProducto_Presentacion
            Dim vPresOC As New clsBeProducto_Presentacion
            Dim vCantidadOCUMBas As Double = 0
            Dim vCantidadRecUMBas As Double = 0
            Dim vCantidadRecAntUMBas As Double = 0
            Dim lDetRec As New List(Of clsBeTrans_re_det)
            Dim lDetRecAnteriores As New List(Of clsBeTrans_re_det)
            Dim lDetRecAnterioresFiltro As New List(Of clsBeTrans_re_det)
            Dim lDetRecAnterioresFiltroFinal As New List(Of clsBeTrans_re_det)
            Dim lDetRecAnteriorResult As New List(Of clsBeTrans_re_det)

            If Not pObjOC Is Nothing Then

                lDetRecAnteriores = clsLnTrans_re_det.Get_All_By_Orden_Compra_Filtro(pObjOC.IdOrdenCompraEnc,
                                                                                     gBeRecepcion.IdRecepcionEnc)

                For Each BeTransOcDet As clsBeTrans_oc_det In pListObjOrdeCompraDet

                    vPresOC = BeTransOcDet.Presentacion
                    vCantidadOCUMBas = BeTransOcDet.Cantidad

                    If Not lDetRecAnteriores Is Nothing Then

                        If lDetRecAnteriores.Count > 0 Then

                            If BeTransOcDet.IdPresentacion = 0 Then
                                lDetRecAnterioresFiltro = lDetRecAnteriores.FindAll(Function(b) b.IdProductoBodega = BeTransOcDet.IdProductoBodega)
                            Else
                                lDetRecAnterioresFiltro = lDetRecAnteriores.FindAll(Function(b) b.IdProductoBodega = BeTransOcDet.IdProductoBodega AndAlso b.IdPresentacion = BeTransOcDet.IdPresentacion)
                            End If

                            lDetRecAnterioresFiltroFinal.Clear()

                            For Each ProdInDetRec As clsBeTrans_re_det In lDetRecAnterioresFiltro

                                vPresRec = ProdInDetRec.Presentacion

                                If Not vPresRec Is Nothing AndAlso vPresRec.IdPresentacion <> 0 Then
                                    vPresRec = clsLnProducto_presentacion.GetSingle(vPresOC.IdPresentacion)
                                    vCantidadRecUMBas += BeTransOcDet.Cantidad * vPresOC.Factor
                                Else
                                    vCantidadRecUMBas += ProdInDetRec.cantidad_recibida
                                End If

                                ProdInDetRec.cantidad_recibida = vCantidadRecUMBas

                                lDetRecAnterioresFiltroFinal.Add(ProdInDetRec)

                            Next ProdInDetRec

                        End If

                    End If

                    vCantidadRecUMBas = 0

                    If BeTransOcDet.IdPresentacion = 0 Then
                        lDetRec = pListRecepcionDetalle.FindAll(Function(b) b.IdProductoBodega = BeTransOcDet.IdProductoBodega)
                    Else
                        lDetRec = pListRecepcionDetalle.FindAll(Function(b) b.IdProductoBodega = BeTransOcDet.IdProductoBodega AndAlso b.IdPresentacion = BeTransOcDet.IdPresentacion)
                    End If

                    If Not lDetRec Is Nothing Then

                        For Each ProdInDetRec As clsBeTrans_re_det In lDetRec

                            vPresRec = ProdInDetRec.Presentacion

                            If Not vPresRec Is Nothing AndAlso vPresRec.IdPresentacion <> 0 Then

                                vPresRec = clsLnProducto_presentacion.GetSingle(vPresOC.IdPresentacion)

                                If vPresRec.EsPallet Then
                                    vCantidadRecUMBas += ProdInDetRec.cantidad_recibida / (vPresRec.Factor * vPresRec.CajasPorCama * vPresRec.CamasPorTarima)
                                Else
                                    vCantidadRecUMBas += ProdInDetRec.cantidad_recibida / vPresRec.Factor
                                End If

                            Else
                                vCantidadRecUMBas += ProdInDetRec.cantidad_recibida
                            End If

                        Next ProdInDetRec

                        If Not lDetRecAnterioresFiltroFinal Is Nothing Then
                            vCantidadRecAntUMBas += lDetRecAnterioresFiltroFinal.FindAll(Function(y) y.IdProductoBodega = BeTransOcDet.IdProductoBodega).Sum(Function(z) z.cantidad_recibida)
                        End If

                        Dim vDiferencia As Double = (vCantidadRecUMBas + vCantidadRecAntUMBas) - vCantidadOCUMBas

                        If vDiferencia < 0 Then
                            'Se recibió menos producto de lo esperado.
                            Detalle_Tiene_Diferencia_Vrs_OC = True
                            Exit Function
                        ElseIf vDiferencia > 0 Then
                            'Se recibió más producto de lo esperado.
                            Detalle_Tiene_Diferencia_Vrs_OC = True
                            Exit Function
                        Else
                            vCantidadRecUMBas = 0
                        End If

                    Else
                        'No se recepcionó nada de ese código de producto
                        Detalle_Tiene_Diferencia_Vrs_OC = True
                        Exit Function
                    End If

                Next BeTransOcDet

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Shared Function Obtener_Doc_Num(oCompany As Company, docEntry As Integer) As Integer

        Obtener_Doc_Num = 0

        Dim docNum As Integer = 0

        Try

            Dim oRecordset As Recordset = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
            Dim query As String = $"SELECT DocNum FROM OWTR WHERE DocEntry = {docEntry}"

            oRecordset.DoQuery(query)

            If oRecordset.RecordCount > 0 Then
                oRecordset.MoveFirst()
                docNum = oRecordset.Fields.Item("DocNum").Value
            Else
                Throw New Exception("No se pudo obtener el DocNum del traslado generado.")
            End If

        Catch ex As Exception
            Throw
        End Try

        Return docNum

    End Function

    Private Shared Function Obtener_Doc_Num_Traslado(oCompany As Company, docEntry As Integer) As Integer
        ' Inicializar el valor de retorno en 0
        Dim docNum As Integer = 0

        Try
            ' Crear un objeto Recordset
            Dim oRecordset As Recordset = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
            ' Definir la consulta SQL para obtener el DocNum basándose en el DocEntry
            Dim query As String = $"SELECT DocNum FROM OWTR WHERE DocEntry = {docEntry}"

            ' Ejecutar la consulta
            oRecordset.DoQuery(query)

            ' Verificar si se encontraron resultados
            If oRecordset.RecordCount > 0 Then
                ' Moverse al primer registro y obtener el valor del campo DocNum
                oRecordset.MoveFirst()
                docNum = oRecordset.Fields.Item("DocNum").Value
            Else
                ' Lanzar una excepción si no se encuentra el DocNum
                Throw New Exception("No se pudo obtener el DocNum del traslado generado.")
            End If

        Catch ex As Exception
            ' Re-lanzar la excepción capturada
            Throw
        End Try

        ' Retornar el valor de DocNum
        Return docNum
    End Function

    Private Shared Function Obtener_Doc_Num_Entrega(oCompany As Company, docEntry As Integer) As Integer
        ' Inicializar el valor de retorno en 0
        Dim docNum As Integer = 0

        Try
            ' Crear un objeto Recordset
            Dim oRecordset As Recordset = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
            ' Definir la consulta SQL para obtener el DocNum basándose en el DocEntry
            Dim query As String = $"SELECT DocNum FROM OPDN WHERE DocEntry = {docEntry}"

            ' Ejecutar la consulta
            oRecordset.DoQuery(query)

            ' Verificar si se encontraron resultados
            If oRecordset.RecordCount > 0 Then
                ' Moverse al primer registro y obtener el valor del campo DocNum
                oRecordset.MoveFirst()
                docNum = oRecordset.Fields.Item("DocNum").Value
            Else
                ' Lanzar una excepción si no se encuentra el DocNum
                Throw New Exception("No se pudo obtener el DocNum de la entrega de mercancía generada.")
            End If

        Catch ex As Exception
            ' Re-lanzar la excepción capturada
            Throw
        End Try

        ' Retornar el valor de DocNum
        Return docNum
    End Function

    Private Shared Function Documento_Ingreso_Ya_Existe_En_SAP(ByVal beTransReOC As clsBeTrans_re_oc,
                                                               ByVal pTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
                                                               ByRef lblPrg As RichTextBox) As Boolean

        Documento_Ingreso_Ya_Existe_En_SAP = False

        Try

            If beTransReOC Is Nothing Then Return False
            If pTipoDocumento <> clsDataContractDI.tTipoDocumentoIngreso.Ingreso Then Return False
            If String.IsNullOrWhiteSpace(beTransReOC.No_Erp_Docentry_Entrega) Then Return False

            Dim vDocEntry As Integer = 0
            If Not Integer.TryParse(beTransReOC.No_Erp_Docentry_Entrega, vDocEntry) Then
                Throw New Exception(String.Format("#ERROR_SAP_20260602_RECON_ING: La referencia SAP guardada en WMS no es numerica: {0}", beTransReOC.No_Erp_Docentry_Entrega))
            End If

            Dim oEntrega As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes), Documents)

            Try

                If Not oEntrega.GetByKey(vDocEntry) Then
                    Throw New Exception(String.Format("#ERROR_SAP_20260602_RECON_ING: WMS tiene DocEntry SAP {0}, pero SAP no lo encontro. No se reenvia automatico para evitar duplicados.", vDocEntry))
                End If

                If Not String.IsNullOrWhiteSpace(beTransReOC.No_Erp_Docnum_Entrega) AndAlso
                   beTransReOC.No_Erp_Docnum_Entrega <> oEntrega.DocNum.ToString() Then
                    Throw New Exception(String.Format("#ERROR_SAP_20260602_RECON_ING: WMS tiene DocEntry {0} con DocNum {1}, pero SAP responde DocNum {2}. Revisar antes de reenviar.",
                                                      vDocEntry,
                                                      beTransReOC.No_Erp_Docnum_Entrega,
                                                      oEntrega.DocNum))
                End If

                clsPublic.Actualizar_Progreso(lblPrg, String.Format("Carol, SAP ya tiene la entrada DocEntry {0} DocNum {1}; no la vuelvo a crear, solo seguimos con la marca local.",
                                                                     vDocEntry,
                                                                     oEntrega.DocNum))

                Return True

            Finally
                If oEntrega IsNot Nothing Then Marshal.ReleaseComObject(oEntrega)
            End Try

        Catch ex As Exception
            Throw
        End Try

    End Function

    Private Shared Function Procesar_Detalle_Ingreso(ByVal oOrderPurchase As Documents,
                                                     ByVal pTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
                                                     ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                     ByVal beTransReOC As clsBeTrans_re_oc,
                                                     ByRef lblPrg As RichTextBox) As Boolean

        Dim oResultado As Integer = 0
        Dim oEntrega As Documents = Nothing
        Dim vRegistroEntregaPorLote As Boolean = False
        Dim NoLineaEntrega As Integer = 0 : Dim NoLineaEntregaLote As Integer = 0
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vNoLineaAnterior As Integer = -1
        Dim vCodigoBodegaERP As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion
        Procesar_Detalle_Ingreso = False

        Try

            clsTransaccion.Begin_Transaction()

            If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso Then
                Configurar_Entrega_Compra(oEntrega, oOrderPurchase)
            End If

            If oEntrega Is Nothing Then
                Throw New Exception("No se pudo crear el objeto de entrega.")
            End If

            NoLineaEntrega = 0 : NoLineaEntregaLote = 0

            For j As Integer = 0 To oOrderPurchase.Lines.Count - 1

                oOrderPurchase.Lines.SetCurrentLine(j)

                Dim vCodigoProductoSAP As String = oOrderPurchase.Lines.ItemCode.ToString()
                Dim vNoLineaOCSAP As Integer = oOrderPurchase.Lines.LineNum

                Dim DistinctProductosLineas = lINavTransaccionesOut.
                Where(Function(x) x.Codigo_producto = vCodigoProductoSAP AndAlso x.No_linea = vNoLineaOCSAP).
                GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea}).
                Select(Function(g) New With {
                    g.Key.Codigo_producto,
                    g.Key.No_linea,
                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                }).ToList()

                If DistinctProductosLineas.Any() Then

                    Dim nuevaLineaEntrega As Boolean = True

                    For Each ProductoIngreso In DistinctProductosLineas

                        nuevaLineaEntrega = (vCodigoAnterior <> ProductoIngreso.Codigo_producto OrElse vNoLineaAnterior <> ProductoIngreso.No_linea)

                        If nuevaLineaEntrega Then

                            Dim vTipoImpuesto As String = oOrderPurchase.Lines.TaxCode

                            oEntrega.Lines.SetCurrentLine(NoLineaEntrega)
                            oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_PurchaseOrder)
                            oEntrega.Lines.BaseEntry = oOrderPurchase.DocEntry
                            oEntrega.Lines.ItemCode = ProductoIngreso.Codigo_producto
                            oEntrega.Lines.BaseLine = vNoLineaOCSAP
                            oEntrega.Lines.TaxCode = vTipoImpuesto
                            oEntrega.Lines.UserFields.Fields.Item("U_ENVIADO_WMS").Value = "1"
                            oEntrega.Lines.Quantity = ProductoIngreso.Cantidad_Total

                            vCodigoAnterior = oEntrega.Lines.ItemCode
                            vNoLineaAnterior = oEntrega.Lines.LineNum

                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderPurchase.DocEntry _
                                                                                  AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                  AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                                  AndAlso x.Enviado = False)

                            If Sublista_A_Actualizar IsNot Nothing AndAlso Sublista_A_Actualizar.Count > 0 Then
                                Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                            End If

                            oEntrega.Lines.Add() : NoLineaEntrega += 1
                        End If

                    Next 'DistinctProductosLineas

                    vAgregarEntrega = True

                End If

            Next

            If vAgregarEntrega Then

                If Lista_A_Actualizar Is Nothing OrElse Lista_A_Actualizar.Count = 0 Then
                    Throw New Exception(String.Format("#ERROR_SAP_20260602_RECON_ING: La entrada SAP para OC {0} no tiene lineas WMS para marcar; se revierte para evitar una entrada huerfana.", oOrderPurchase.DocEntry))
                End If

                oResultado = oEntrega.Add()

                If oResultado <> 0 Then
                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}: Documento: {oOrderPurchase.DocNum}")
                Else

                    Dim vTrasladoDocEntry As Integer = 0
                    Dim newObjectCode As String = ""
                    oCompany.GetNewObjectCode(newObjectCode)

                    If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                        Throw New Exception("No se pudo obtener el DocEntry de la entrada generada.")
                    End If

                    Dim oEntregaCreada As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes), Documents)
                    Try
                        '#EJC20260602_SYNC_INGRESO_SAP: Carol, guardamos y validamos el DocEntry real porque ese
                        ' es el comprobante que nos permite reconocer la entrada si el proceso se corta despues.
                        If Not oEntregaCreada.GetByKey(vTrasladoDocEntry) Then
                            Throw New Exception(String.Format("#ERROR_SAP_20260602_RECON_ING: SAP devolvio DocEntry {0}, pero no se pudo validar la entrada creada.", vTrasladoDocEntry))
                        End If
                    Finally
                        If oEntregaCreada IsNot Nothing Then Marshal.ReleaseComObject(oEntregaCreada)
                    End Try

                    Dim vDocNum As String = ""
                    Try
                        If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Transferencia_de_Ingreso Then
                            vDocNum = Obtener_Doc_Num(oCompany, vTrasladoDocEntry)
                        ElseIf pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Devolucion Then
                            vDocNum = Obtener_Doc_Num_Traslado(oCompany, vTrasladoDocEntry)
                        ElseIf pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso Then
                            vDocNum = Obtener_Doc_Num_Entrega(oCompany, vTrasladoDocEntry)
                        End If
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, ex.Message)
                    End Try

                    vRegistroEntregaPorLote = True

                    If beTransReOC IsNot Nothing Then
                        beTransReOC.No_Erp_Docentry_Entrega = vTrasladoDocEntry.ToString()
                        beTransReOC.No_Erp_Docnum_Entrega = vDocNum
                        clsLnTrans_re_oc.Actualizar(beTransReOC,
                                                    clsTransaccion.lConnection,
                                                    clsTransaccion.lTransaction)
                    End If

                    If Lista_A_Actualizar IsNot Nothing AndAlso Lista_A_Actualizar.Count > 0 Then
                        For Each T In Lista_A_Actualizar
                            T.Enviado = True
                            T.no_documento_salida_ref_devol = vTrasladoDocEntry
                            T.Fec_mod = Now
                            clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(T)
                        Next
                    End If

                    Procesar_Detalle_Ingreso = True

                    clsLnTrans_oc_enc.Actualizar_No_Documento_Recepcion_ERP(vDocNum,
                                                                            Lista_A_Actualizar.FirstOrDefault.Idordencompra,
                                                                            clsTransaccion.lConnection,
                                                                            clsTransaccion.lTransaction)

                    clsPublic.Actualizar_Progreso(lblPrg, "Se generó la entrega: " & vDocNum & " en SAP.")

                    clsLnLog_error_wms.Agregar_Error("IF_SAP_PROC_DET_ING: Se generó la entrega: " & vDocNum & " en SAP.")

                End If

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw
        Finally
            If oEntrega IsNot Nothing Then
                Marshal.ReleaseComObject(oEntrega)
            End If
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Private Shared Function Procesar_Detalle_Ingreso_Devolucion(ByVal oReturnRequest As Documents,
                                                                ByVal pTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
                                                                ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                                ByVal _Docentry As Integer,
                                                                ByRef lblPrg As RichTextBox) As Boolean

        Dim oResultado As Integer = 0
        Dim oReturn As Documents = Nothing
        Dim vRegistroEntregaPorLote As Boolean = False
        Dim NoLineaEntrega As Integer = 0 : Dim NoLineaEntregaLote As Integer = 0
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion

        Procesar_Detalle_Ingreso_Devolucion = False

        Try

            clsTransaccion.Open_Connection()

            Dim vDocEntrySolicitud As Integer = lINavTransaccionesOut.FirstOrDefault.No_pedido

            If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Devolucion Then
                Configurar_Devolucion_Cliente(oReturn, oReturnRequest, vDocEntrySolicitud)
            End If

            If oReturn Is Nothing Then
                Throw New Exception("No se pudo crear el objeto de entrega.")
            End If

            NoLineaEntrega = 0 : NoLineaEntregaLote = 0

            For j As Integer = 0 To oReturnRequest.Lines.Count - 1

                oReturnRequest.Lines.SetCurrentLine(j)

                Dim vCodigoProductoSAP As String = oReturnRequest.Lines.ItemCode.ToString()
                Dim vNoLineaOCSAP As Integer = oReturnRequest.Lines.LineNum

                Dim DistinctProductosLineas = lINavTransaccionesOut.
                Where(Function(x) x.Codigo_producto = vCodigoProductoSAP AndAlso x.No_linea = vNoLineaOCSAP).
                GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea}).
                Select(Function(g) New With {
                    g.Key.Codigo_producto,
                    g.Key.No_linea,
                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                }).ToList()

                If DistinctProductosLineas.Any() Then

                    For Each ProductoIngreso In DistinctProductosLineas

                        Dim nuevaLineaEntrega As Boolean = (vCodigoAnterior <> ProductoIngreso.Codigo_producto)

                        If nuevaLineaEntrega Then

                            Dim vTipoImpuesto As String = oReturnRequest.Lines.TaxCode
                            oReturn.Lines.SetCurrentLine(NoLineaEntrega)
                            oReturn.Lines.BaseType = BoObjectTypes.oReturnRequest
                            oReturn.Lines.BaseEntry = oReturnRequest.DocEntry
                            oReturn.Lines.BaseLine = vNoLineaOCSAP
                            oReturn.Lines.ItemCode = ProductoIngreso.Codigo_producto
                            oReturn.Lines.TaxCode = vTipoImpuesto
                            oReturn.Lines.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                            oReturn.Lines.Quantity = ProductoIngreso.Cantidad_Total

                            vCodigoAnterior = oReturn.Lines.ItemCode

                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oReturnRequest.DocEntry _
                                                                              AndAlso x.No_linea = vNoLineaOCSAP _
                                                                              AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                              AndAlso x.Enviado = False)

                            If Sublista_A_Actualizar IsNot Nothing AndAlso Sublista_A_Actualizar.Count > 0 Then
                                Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                            End If

                            oReturn.Lines.Add() : NoLineaEntrega += 1

                        End If

                    Next 'DistinctProductosLineas

                    vAgregarEntrega = True

                End If

            Next

            If vAgregarEntrega Then

                Dim nombreCampoRegion As String = "U_Region"
                oReturn.UserFields.Fields.Item(nombreCampoRegion).Value = "CENTRAL"
                Dim nombreCampoUGira As String = "U_Gira"
                oReturn.UserFields.Fields.Item(nombreCampoUGira).Value = "CADENAS"
                Dim nombreCampoUDepto As String = "U_Depto"
                oReturn.UserFields.Fields.Item(nombreCampoUDepto).Value = "CADENAS"

                oResultado = oReturn.Add()

                If oResultado <> 0 Then
                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                Else
                    vRegistroEntregaPorLote = True

                    If Lista_A_Actualizar IsNot Nothing AndAlso Lista_A_Actualizar.Count > 0 Then
                        For Each T In Lista_A_Actualizar
                            T.Enviado = True
                            T.Fec_mod = Now
                            clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(T)
                        Next
                    End If

                    Procesar_Detalle_Ingreso_Devolucion = True

                    Dim newObjectCode As String = ""
                    oCompany.GetNewObjectCode(newObjectCode)
                    Dim vTrasladoDocEntry As Integer = 0

                    If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                        Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                    End If

                    clsPublic.Actualizar_Progreso(lblPrg, "#MSG2407152241: Se creó el traslado con DocEntry: " & vTrasladoDocEntry)

                    clsLnLog_error_wms.Agregar_Error("#IF_SAP_PROC_DET_ING_DEVOL: Se creó el traslado con DocEntry: " & vTrasladoDocEntry)

                End If

            End If

        Catch ex As Exception
            Throw
        Finally
            If oReturn IsNot Nothing Then
                Marshal.ReleaseComObject(oReturn)
            End If
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
