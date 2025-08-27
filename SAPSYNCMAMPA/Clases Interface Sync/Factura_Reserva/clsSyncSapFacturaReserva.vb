Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports Sap.Data.Hana

Public Class clsSyncSapFacturaReserva
    Implements IDisposable

    Private disposedValue As Boolean

    Private Function ConstruirQueryEncabezado(codBodega As String, pNoDocumentoSAP As String) As String
        Return "SELECT 
                    T0.""DocEntry"",
                    T0.""DocNum"",
                    T0.""DocDueDate"" AS ""DocDate"",  
                    T0.""CardCode"", 
                    T0.""CardName"", 
                    T0.""DocCur"", 
                    T0.""DocTotal"", 
                    T0.""JrnlMemo"", 
                    T0.""CANCELED"",
                    T0.""DocStatus"", 
                    CASE 
                        WHEN T0.""DocType"" = 'I' THEN 'ARTICULO' 
                        ELSE 'SERVICIO' 
                    END AS ""Tipo_Orden_Venta"",
                    (
                        SELECT MIN(D0.""WhsCode"")
                        FROM ""PCH1"" D0 
                        INNER JOIN ""OWHS"" D1 ON D1.""WhsCode"" = D0.""WhsCode"" 
                        WHERE D0.""DocEntry"" = T0.""DocEntry""
                    ) AS ""Bodega"", 
                    T0.""Comments"", 
                    T0.""NumAtCard"", 
                    T0.""Series"" ,
                    T0.""U_Campania""
                FROM ""OPCH"" T0
                WHERE 
                    T0.""CANCELED"" = 'N'
                    AND T0.""CreateDate"" >= '2020-10-09'
                    AND (
                        SELECT MIN(D0.""WhsCode"")
                        FROM ""PCH1"" D0 
                        INNER JOIN ""OWHS"" D1 ON D1.""WhsCode"" = D0.""WhsCode"" 
                        WHERE D0.""DocEntry"" = T0.""DocEntry""
                    ) = '" & codBodega & "'" &
                    If(pNoDocumentoSAP <> "", " AND T0.""DocNum"" = " & pNoDocumentoSAP, "") & "
                ORDER BY T0.""DocEntry"" DESC"
    End Function

    Private Function ConstruirQueryDetalle(docEntry As Integer) As String
        Return "SELECT 
                T0.""ItemCode"",
                T0.""Dscription"",
                T0.""Quantity"",
                T0.""Price"",
                T0.""LineTotal"",
                T0.""VatSum"",
                T0.""DocEntry"",
                T0.""WhsCode"",
                T0.""OpenCreQty"" AS ""Cantidad_Pendiente"",
                T0.""BaseLine"",
                T0.""LineNum"",
                T1.""BuyUnitMsr"" AS ""Unidad_Medida""
            FROM ""PCH1"" T0
            INNER JOIN ""OITM"" T1 ON T1.""ItemCode"" = T0.""ItemCode""
            WHERE 
                T0.""DocEntry"" = '" & docEntry & "'
                AND T0.""LineStatus"" = 'O'"
    End Function

    Private Function ConstruirQueryTallasColores(docEntry As Integer, Campaña As Integer) As String
        Return "WITH factura AS (
                    SELECT ""DocEntry"", ""U_Campania""
                    FROM ""OPCH""
                    WHERE ""DocEntry"" = " & docEntry & "
                )
                SELECT DISTINCT p.*, o.""U_Campania""
                FROM factura o
                INNER JOIN ""@PERIODO_ADL"" a ON o.""U_Campania"" = a.""U_Periodo""
                INNER JOIN ""@PERIODO_ADL2"" p ON p.""DocEntry"" = a.""DocEntry""
                INNER JOIN ""PCH1"" d ON p.""U_Modelo"" = d.""ItemCode""
                WHERE o.""U_Campania""= " & Campaña
    End Function

    Private Function ConstruirQueryCampaña(Campaña As Integer) As String

        Return "SELECT  TOP 1 ""DocEntry"", ""Remark"", ""U_FechaInicial"", ""U_FechaFinal"" " &
               "FROM ""@APERIODOTIEMPO"" " &
               "WHERE ""DocEntry"" = " & Campaña & " " &
               " ORDER BY ""LogInst"" DESC"

    End Function

    Public Function GetFactura_Reserva_SAP_HANA(CodBodega As String,
                                                lConnection As SqlConnection,
                                                lTransaction As SqlTransaction,
                                                Optional AplicarFiltros As Boolean = True,
                                                Optional pNoDocumentoSAP As String = "") As List(Of clsBeI_nav_ped_compra_enc)

        Dim lReturnList As New List(Of clsBeI_nav_ped_compra_enc)
        Dim vNoLineaBase As Integer = 0
        Dim vNoLinea As Integer = 0
        Dim BeINAVPedidoDetWMS As New clsBeI_nav_ped_compra_det()

        GetFactura_Reserva_SAP_HANA = Nothing

        Try

            Using conn As HanaConnection = HanaHelper.OpenDB()

                Dim queryEnc As String = ConstruirQueryEncabezado(CodBodega, pNoDocumentoSAP)
                Dim dtEnc As DataTable = HanaHelper.OpenDT(queryEnc, conn)

                For Each enc As DataRow In dtEnc.Rows

                    Dim encabezado As clsBeI_nav_ped_compra_enc = MapearEncabezado(enc)

                    Dim queryDet As String = ConstruirQueryDetalle(encabezado.No)
                    Dim dtDet As DataTable = HanaHelper.OpenDT(queryDet, conn)
                    encabezado.Lineas_Detalle = MapearDetalle(dtDet)

                    Dim queryTallaColor As String = ConstruirQueryTallasColores(encabezado.No,
                                                                                encabezado.Campaign_No)
                    Dim dtDetTallaColor As DataTable = HanaHelper.OpenDT(queryTallaColor, conn)

                    encabezado.Lineas_Detalle_Talla_Color = MapearDetalleTallaColor(dtDetTallaColor,
                                                                                    lConnection,
                                                                                    lTransaction,
                                                                                    BeConfigEnc.IdUsuario)

                    Dim queryCampaña As String = ConstruirQueryCampaña(encabezado.Campaign_No)
                    Dim dtCampaña As DataTable = HanaHelper.OpenDT(queryCampaña, conn)
                    encabezado.Campaña = MapearCampaña(dtCampaña.Rows(0),
                                                       lConnection,
                                                       lTransaction,
                                                       BeConfigEnc.IdUsuario)

                    lReturnList.Add(encabezado)

                Next

            End Using

            GetFactura_Reserva_SAP_HANA = lReturnList

        Catch ex As HanaException
            Throw New Exception("Error al obtener productos: " & ex.Message)
        End Try

    End Function

    Private Function MapearEncabezado(enc As DataRow) As clsBeI_nav_ped_compra_enc
        Dim be As New clsBeI_nav_ped_compra_enc()
        be.No = Convert.ToInt32(enc("DocEntry"))
        be.Posting_Date = Convert.ToDateTime(enc("DocDate"))
        be.Order_Date = be.Posting_Date
        be.Document_Date = be.Posting_Date
        be.Expected_Receipt_Date = be.Posting_Date
        be.Status = 1
        be.Buy_From_Vendor_No = enc("CardCode").ToString()
        be.Buy_From_Vendor_Name = enc("CardName").ToString()
        be.Is_Internal_Transfer = False
        be.Location_Code = enc("Bodega").ToString()
        be.Vendor_Invoice_No = enc("DocNum").ToString()
        be.Posting_Description = enc("Comments").ToString()
        be.Product_Owner_Code = BeConfigEnc.IdPropietario
        If String.IsNullOrEmpty(be.Vendor_Invoice_No) Then
            be.Vendor_Invoice_No = be.No.ToString()
        End If
        be.Campaign_No = enc("U_Campania").ToString()
        Return be
    End Function
    Private Function MapearDetalle(dtDet As DataTable) As List(Of clsBeI_nav_ped_compra_det)
        Dim lista As New List(Of clsBeI_nav_ped_compra_det)()
        For Each det As DataRow In dtDet.Rows
            Dim d As New clsBeI_nav_ped_compra_det()
            d.NoEnc = det("DocEntry").ToString()
            d.No = det("ItemCode").ToString()
            d.Line_No = If(IsDBNull(det("LineNum")), 0, Convert.ToInt32(det("LineNum")))
            d.Planed_Receipt_Date = Date.Now()
            d.Quantity = Convert.ToDouble(det("Cantidad_Pendiente"))
            d.Quantity_Received = 0
            d.Description = clsPublic.Quitar_Caracteres_No_Permitidos(det("Dscription").ToString())
            d.Unit_of_Measure_Code = det("Unidad_Medida").ToString()
            d.Type = 2
            d.Variant_Code = Nothing
            d.Location_Code = det("WhsCode").ToString()
            lista.Add(d)
        Next
        Return lista
    End Function
    Private Function MapearDetalleTallaColor(dtDetTallaColor As DataTable,
                                             lConnection As SqlConnection,
                                             lTransaction As SqlTransaction,
                                             Usuario As String) As List(Of clsBeProducto_talla_color)

        Dim lista As New List(Of clsBeProducto_talla_color)()

        For Each det As DataRow In dtDetTallaColor.Rows

            Dim d As New clsBeProducto_talla_color()

            Dim BeProducto As New clsBeProducto
            BeProducto = clsLnProducto.Get_Single_By_Codigo(det("U_Modelo").ToString(),
                                                            lConnection,
                                                            lTransaction)

            If Not BeProducto Is Nothing Then
                d.IdProducto = BeProducto.IdProducto
            Else

                d.IdProducto = clsSyncSAPProducto.Insertar_Producto_From_Sap_Hana(det("U_Modelo").ToString(),
                                                                                   lConnection,
                                                                                   lTransaction)

            End If

            Dim BeTalla As New clsBeTalla
            BeTalla = clsLnTalla.Get_Single_By_Codigo(det("U_Talla").ToString(), lConnection, lTransaction)

            If Not BeTalla Is Nothing Then
                d.IdTalla = BeTalla.IdTalla
            Else
                'd.IdTalla = clsSyncSapTalla.Insertar_Talla_From_Sap_Hana(det("U_Talla").ToString(),
                '                                                         lConnection,
                '                                                         lTransaction)

            End If

            Dim BeColor As New clsBeColor
            BeColor = clsLnColor.Get_Single_By_Codigo(det("U_Color").ToString(), lConnection, lTransaction)

            If Not BeColor Is Nothing Then
                d.IdColor = BeColor.IdColor
            Else
                d.IdTalla = clsSyncSapColor.Insertar_Color_From_Sap_Hana(det("U_Color").ToString(),
                                                                         lConnection,
                                                                         lTransaction)
            End If

            Dim vCodigoBarra As String = $"{det("U_Modelo")}{det("U_Color")}{det("U_Talla")}"
            d.CodigoSKU = vCodigoBarra

            Dim BeCampaña As New clsBeCampaña
            BeCampaña = clsLnCampaña.Get_Single_By_Codigo(det("U_Campania").ToString(), lConnection, lTransaction)

            If Not BeCampaña Is Nothing Then
                d.IdCampaña = det("U_Campania").ToString()
            Else
                'd.IdCampaña = clsSyncSAPCampaña.Insertar_Campaña_From_Sap_Hana(det("U_Campania"), lConnection, lTransaction)
            End If

            d.Fec_agr = Date.Now()
            d.User_agr = Usuario
            d.Fec_mod = Date.Now()
            d.User_mod = Usuario

            clsLnProducto_talla_color.InsertOrUpdate(d, lConnection, lTransaction)

            lista.Add(d)

        Next

        Return lista

    End Function
    Private Function MapearCampaña(enc As DataRow, lConnection As SqlConnection, lTransaction As SqlTransaction, Usuario As String) As clsBeCampaña

        Dim be As New clsBeCampaña()
        be = clsLnCampaña.Get_Single_By_Codigo(Convert.ToInt32(enc("DocEntry")), lConnection, lTransaction)

        If be Is Nothing Then
            be = New clsBeCampaña
            be.IdCampaña = Convert.ToInt32(enc("DocEntry"))
            be.Codigo = Convert.ToInt32(enc("DocEntry"))
            be.Nombre = enc("Remark")
            be.FechaInicio = Convert.ToDateTime(enc("U_FechaInicial"))
            be.FechaFin = Convert.ToDateTime(enc("U_FechaFinal"))
            be.Activo = True
            be.Fec_agr = Now
            be.Fec_mod = Now
            be.User_agr = Usuario
            be.User_mod = Usuario
        End If

        Return be
    End Function
    Public Function Importar_Facturas_Reserva_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                         ByRef prg As System.Windows.Forms.ProgressBar,
                                                                         ByRef cnnLog As SqlConnection,
                                                                         Optional ByVal pNoDocumentoSAP As String = "") As Boolean

        Importar_Facturas_Reserva_Desde_SAP_A_TablaIntermedia = False

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

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeConfigEnc.Idbodega,
                                                         lConnection,
                                                         lTransaction)

            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia TOMWMS.")

            Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
            lPedidosCompra = GetFactura_Reserva_SAP_HANA(BeBodega.Codigo,
                                                         lConnection,
                                                         lTransaction,
                                                         True,
                                                         pNoDocumentoSAP)

            If lPedidosCompra Is Nothing Then
                clsPublic.Actualizar_Progreso(lblprg, "No se obtuvieron facturas de reserva.")
                Exit Function
            End If

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Pedidos de compra en relación con SAP (OPOR): {0} ", lPedidosCompra.Count))

            prg.Maximum = lPedidosCompra.Count

            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each BeINavPedCompra In lPedidosCompra

                    clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Procesando Pedido Compra: {0} ", BeINavPedCompra.No & " - " & BeINavPedCompra.Vendor_Invoice_No, vbNewLine))

                    If Not clsLnProveedor.Existe_Proveedor(BeINavPedCompra.Buy_From_Vendor_No) Then

                        BeConfigEnc = BeConfigEnc

                        If Inserta_Proveedor_Desde_SAP(BeINavPedCompra.Buy_From_Vendor_No, cnnLog) Then
                            clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & BeINavPedCompra.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                        End If

                    End If

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                            BePedidoCompraEnc,
                                                                            vResult) Then
                        Marcar_PI_Sincronizado_HANA(BeINavPedCompra.No)

                    End If

                    clsPublic.Actualizar_Progreso(lblprg, vResult)

                Next

            End If

            lTransaction.Commit()

            Importar_Facturas_Reserva_Desde_SAP_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnLog_error_wms.Agregar_Error("Error_20250422_Fact_Res:" & ex.Message)

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

            BeSAPProveedor = clsSyncSAPProveedor.Get_Proveedor_SAP_Hana(pCodigo)

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

                    clsLnLog_error_wms.Agregar_Error("Error_20250422_Inteface_Proveedor: " & ex.Message & " " & BeProveedor.Codigo)

                    Throw ex

                End Try

            End If


        Catch ex As Exception
            Throw ex
        Finally
            clstrans.Close_Conection()
        End Try

    End Function

    Public Shared Function Marcar_PI_Sincronizado_HANA(ByVal pNoDocumento As String) As Boolean

        Marcar_PI_Sincronizado_HANA = False

        Try
            Dim query As String = "UPDATE ""OPOR""
                                    SET ""U_ENVIADO_WMS"" = '1'
                                    WHERE ""DocNum"" = '" & pNoDocumento & "'"

            Dim filasAfectadas As Integer = HanaHelper.Xcute(query)

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Insertar_Facturas_Reserva_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                                   ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                   Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                   Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                                                   Optional ByVal pNoDocumentoSAP As String = "") As Boolean

        Insertar_Facturas_Reserva_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

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
        Dim vInicio As Date = Now

        Try


            CnnLog.Open()

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Facturas_Reserva_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog, pNoDocumentoSAP) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Facturas_Reserva_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog, pNoDocumentoSAP) Then
                        Exit Function
                    End If
                End If

            End If

            lTransInterface.Commit()

            Dim difSegundos As Double = DateDiff(DateInterval.Second, vInicio, Now)

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format(" -> Fin de proceso, tiempo transcurrido: {0} segundo(s)", difSegundos))

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            prg.Value = 0

            clsLnLog_error_wms.Agregar_Error("Error_20250422_Insert_Fact_Res: " & ex.Message)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de compra a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))

            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
