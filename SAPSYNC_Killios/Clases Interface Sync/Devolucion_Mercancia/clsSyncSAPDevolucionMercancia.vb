Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM

Public Class clsSyncSAPDevolucionMercancia : Inherits clsInterfaceBase

    Public Shared Function Importar_Solicitud_Devolucion_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                                ByRef prg As ProgressBar,
                                                                                ByRef cnnLog As SqlConnection,
                                                                                Optional ByVal pNoDocumentoSAP As String = "") As Boolean

        Dim empresas() As pEmpresa = {pEmpresa.Killios, pEmpresa.Garesa}
        Dim totalRegistros As Integer = 0
        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

            clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction)
            clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction)

            For Each empresa In empresas

                Dim devoluciones As List(Of clsBeI_nav_ped_compra_enc) = Get_Solicitud_Devolucion_From_SAP(True, pNoDocumentoSAP, empresa)

                If devoluciones IsNot Nothing AndAlso devoluciones.Count > 0 Then

                    clsPublic.Actualizar_Progreso(lblprg, $"Documento pertenece a: {empresa}")
                    BeNavEjecucionRes.Registros_ws += devoluciones.Count

                    For Each devolucion In devoluciones

                        Try

                            clsPublic.Actualizar_Progreso(lblprg, $"Procesando Pedido Compra: {devolucion.No}")

                            Dim codigoProveedor As String = empresa.ToString().Substring(0, 1) & devolucion.Buy_From_Vendor_No
                            If Not clsLnProveedor.Existe_By_Codigo_And_Company(codigoProveedor, devolucion.Company_Code) Then
                                If Inserta_Proveedor_Desde_SAP(devolucion.Buy_From_Vendor_No, cnnLog, empresa) Then
                                    clsPublic.Actualizar_Progreso(lblprg, $"Proveedor insertado: {codigoProveedor}")
                                End If
                            End If

                            Dim resultado As String = String.Empty
                            If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(devolucion, BePedidoCompraEnc, resultado) Then
                                Marcar_PI_Sincronizado_SAP(devolucion.No, empresa)
                            End If

                            clsPublic.Actualizar_Progreso(lblprg, resultado)

                        Catch ex As Exception
                            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                        End Try

                    Next

                End If

            Next

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            lTransaction.Commit()

            Return True

        Catch ex As Exception

            If lTransaction IsNot Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar devoluciones: {vbNewLine}{ex.Message}")

            Throw New Exception($"{MethodBase.GetCurrentMethod.Name}: {ex.Message}")

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function

    Private Shared Function Get_Solicitud_Devolucion_From_SAP(Optional ByVal AplicarFiltros As Boolean = True,
                                                              Optional ByVal pNoDocumentoSAP As String = "",
                                                              Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As List(Of clsBeI_nav_ped_compra_enc)

        Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            If oCompany.Connected = False Then
                Throw New Exception("No se pudo establecer conexión con SAP para la empresa: " & pCompany.ToString())
            End If

            Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            Dim vSQLOC As String = "SELECT T0.DOCENTRY, T0.DOCNUM, T0.DOCDATE, T0.CARDCODE, T0.CARDNAME, T0.DOCCUR, T0.DOCTOTAL, " &
                              "T0.JRNLMEMO, T0.CANCELED, T0.DOCSTATUS, " &
                              "CASE WHEN T0.DOCTYPE = 'I' THEN 'ARTICULO' ELSE 'SERVICIO' END AS TIPO_ORDEN_VENTA, " &
                              "(SELECT TOP 1 D0.WhsCode FROM RRR1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode WHERE D0.DocEntry = T0.DOCENTRY) AS BODEGA, " &
                              "T0.COMMENTS, T0.NumAtCard FROM ORRR T0 WHERE T0.DocStatus = 'O' AND T0.U_ENVIADO_WMS = 2 " &
                              IIf(pNoDocumentoSAP <> "", " And T0.DocNum = " & pNoDocumentoSAP, "")

            RsEnc.DoQuery(vSQLOC)

            While RsEnc.EoF = False

                Dim BePedidoCompra As New clsBeI_nav_ped_compra_enc()
                BePedidoCompra.No = RsEnc.Fields.Item("DOCENTRY").Value
                BePedidoCompra.Posting_Date = RsEnc.Fields.Item("DOCDATE").Value
                BePedidoCompra.Order_Date = RsEnc.Fields.Item("DOCDATE").Value
                BePedidoCompra.Document_Date = RsEnc.Fields.Item("DOCDATE").Value
                BePedidoCompra.Expected_Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value
                BePedidoCompra.Status = 1
                BePedidoCompra.Buy_From_Vendor_No = RsEnc.Fields.Item("CARDCODE").Value.ToString()
                BePedidoCompra.Buy_From_Vendor_Name = RsEnc.Fields.Item("CARDNAME").Value.ToString()
                BePedidoCompra.Is_Internal_Transfer = False
                BePedidoCompra.Location_Code = RsEnc.Fields.Item("BODEGA").Value.ToString()
                BePedidoCompra.Vendor_Invoice_No = RsEnc.Fields.Item("NUMATCARD").Value.ToString()
                BePedidoCompra.Posting_Description = RsEnc.Fields.Item("COMMENTS").Value.ToString()
                BePedidoCompra.Product_Owner_Code = BeConfigEnc.IdPropietario
                BePedidoCompra.Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Devolucion
                BePedidoCompra.Company_Code = pCompany.ToString()

                If BePedidoCompra.Vendor_Invoice_No = "" Then
                    BePedidoCompra.Vendor_Invoice_No = BePedidoCompra.No.ToString()
                End If

                BePedidoCompra.Lineas_Detalle = Get_Solicitud_Devolucion_Detalle_SAP(oCompany, BePedidoCompra.No)
                lPedidosCompra.Add(BePedidoCompra)

                RsEnc.MoveNext()

            End While

            Return lPedidosCompra

        Catch ex As Exception
            Throw ex
        Finally
            sapPool.ReleaseConnection(conn)
        End Try

    End Function

    Private Shared Function Get_Solicitud_Devolucion_Detalle_SAP(ByRef oCompany As Company, ByVal docEntry As Integer) As List(Of clsBeI_nav_ped_compra_det)

        Get_Solicitud_Devolucion_Detalle_SAP = Nothing

        Try

            Dim detalles As New List(Of clsBeI_nav_ped_compra_det)
            Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

            Dim query_det As String = "SELECT T1.U_CodWMS AS ItemCode, T0.Dscription, T0.Quantity, T0.Price, T0.LineTotal, " &
                              "T0.VatSum, T0.DocEntry, T0.WhsCode, T0.OpenCreQty AS Cantidad_Pendiente, " &
                              "T0.BaseLine, T0.LineNum, T0.UomCode AS Unidad_Medida " &
                              "FROM RRR1 T0 INNER JOIN OITM T1 ON T1.ItemCode = T0.ItemCode " &
                              "WHERE T0.DOCENTRY = '" & docEntry & "' AND T0.LINESTATUS = 'O'"

            RsDet.DoQuery(query_det)

            While RsDet.EoF = False

                Dim detalle As New clsBeI_nav_ped_compra_det()
                detalle.NoEnc = RsDet.Fields.Item("DOCENTRY").Value.ToString()
                detalle.No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
                detalle.Line_No = IIf(IsDBNull(RsDet.Fields.Item("LINENUM").Value), 0, RsDet.Fields.Item("LINENUM").Value)
                detalle.Planed_Receipt_Date = Date.Now()
                detalle.Quantity = Convert.ToDecimal(RsDet.Fields.Item("CANTIDAD_PENDIENTE").Value)
                detalle.Quantity_Received = 0
                detalle.Description = RsDet.Fields.Item("DSCRIPTION").Value.ToString()
                detalle.Unit_of_Measure_Code = "UN"
                detalle.Type = 2

                If RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString() = "Unidad" Then
                    detalle.Variant_Code = Nothing
                Else
                    detalle.Variant_Code = RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()
                End If

                detalle.Location_Code = RsDet.Fields.Item("WHSCODE").Value.ToString()
                detalles.Add(detalle)

                RsDet.MoveNext()

            End While

            Return detalles

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar_Solicitud_Devol_Cli_A_TOMWMS(ByRef lblprg As RichTextBox,
                                                                 ByRef prg As System.Windows.Forms.ProgressBar,
                                                                 Optional ByVal ForzarEjecucion As Boolean = False,
                                                                 Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                                 Optional ByVal pPedidoCliente As String = "") As Boolean

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

            lblprg.Text = ""

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0'0' 0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = 0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Importar_Solicitud_Devolucion_SAP_A_TablaIntermedia(lblprg, prg, CnnLog, pPedidoCliente) Then
                Exit Function
            End If


            lTransInterface.Commit()

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format(" -> Fin de proceso, tiempo transcurrido: {0} segundo(s)", difSegundos))

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            prg.Value = 0

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                      "",
                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                      BeConfigDet.Idnavconfigdet, CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de compra a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))

        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Private Shared Function Inserta_Proveedor_Desde_SAP(pCodigo As String,
                                                        cnnLog As SqlConnection,
                                                        ByRef pEmpresaOrigen As pEmpresa) As Boolean
        Try

            Dim proveedorNav = clsSyncSAPProveedor.Get_Proveedor_Devolucion_SAP(pCodigo, pEmpresaOrigen)

            If proveedorNav Is Nothing Then Return False

            Dim proveedor = CrearEntidadProveedor(proveedorNav, pEmpresaOrigen)

            CrearProveedorBodega(proveedor)

            clsSyncSAPProveedor.Marcar_Proveedor_Sincronizado_SAP(proveedor.Codigo)

            Return True

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                   pEmpresaOrigen.ToString().Substring(0, 1) & pCodigo,
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet,
                                                   cnnLog)
            Throw
        End Try
    End Function

    Private Shared Function CrearEntidadProveedor(navProv As clsBeI_nav_proveedor, pEmpresaOrigen As pEmpresa) As clsBeProveedor

        CrearEntidadProveedor = Nothing

        Try

            Dim BeProveedor = New clsBeProveedor With {
                .IdEmpresa = BeConfigEnc.Idempresa,
                .IdPropietario = BeConfigEnc.IdPropietario,
                .IdProveedor = clsLnProveedor.MaxID() + 1,
                .Codigo = pEmpresaOrigen.ToString().Substring(0, 1) & navProv.No,
                .Nombre = navProv.Name,
                .Telefono = navProv.Phone_No,
                .Nit = navProv.VAT_Registratrion_No,
                .Direccion = navProv.Adress,
                .Contacto = navProv.Contact,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .Fec_agr = Date.UtcNow,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_mod = Date.UtcNow
            }

            clsLnProveedor.Insertar(BeProveedor)

            CrearEntidadProveedor = BeProveedor

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Shared Sub CrearProveedorBodega(proveedor As clsBeProveedor)

        Try

            Dim lBodegas As New List(Of clsBeBodega)
            lBodegas = clsLnBodega.GetAll()

            For Each Bod In lBodegas

                Dim proveedorBodega = New clsBeProveedor_bodega With {
                .IdAsignacion = clsLnProveedor_bodega.MaxID() + 1,
                .IdProveedor = proveedor.IdProveedor,
                .IdBodega = Bod.IdBodega,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now
            }

                clsLnProveedor_bodega.Insertar(proveedorBodega)

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Marcar_PI_Sincronizado_SAP(ByVal pNoDocumento As String,
                                                      Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As Boolean
        Marcar_PI_Sincronizado_SAP = False

        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            Dim oRecordset As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            Dim sQuery As String = $"UPDATE ORRR SET U_Enviado_WMS = '1' WHERE DocEntry = '{pNoDocumento}'"

            oRecordset.DoQuery(sQuery)
            Marcar_PI_Sincronizado_SAP = True

        Catch ex As Exception
            Throw New Exception("No se pudo marcar el documento como enviado en SAP: " & ex.Message)
        End Try

    End Function

End Class