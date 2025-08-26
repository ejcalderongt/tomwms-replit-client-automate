Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports DevExpress.XtraEditors

Public Class clsSyncSAPDevolucionMercancia : Inherits clsInterfaceBase

    Implements IDisposable

    Shared VContadorBitacoraTOMWMS As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Private Shared lRetCode
    Private Shared lErrCode As Long
    Private Shared sErrMsg As String = ""

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Shared BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

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

    Public Shared Function Marcar_PI_Sincronizado_SAP(ByVal pNoDocumento As String) As Boolean
        Marcar_PI_Sincronizado_SAP = False
        Try
            Dim sql As String = $"""
            UPDATE OPDN
               SET ""U_EnviadoWMS"" = '1'
             WHERE ""DocEntry"" = '{pNoDocumento}'
        """
            Dim result As Integer = HanaHelper.Xcute(sql)
            Marcar_PI_Sincronizado_SAP = (result > 0)
        Catch ex As Exception
            Throw New Exception($"(M) {MethodBase.GetCurrentMethod.Name()} {ex.Message}")
        End Try
    End Function

    Public Class DevolucionCliente
        Public Property DocEntry As Integer
        Public Property CardCode As String
        Public Property CardName As String
        Public Property DocDate As Date
        Public Property DocTotal As Double
        Public Property WhsCode As String
        Public Property WhsName As String
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
    Public Function Get_Detalle_Devolucion(ByVal docEntry As Integer) As List(Of DetalleDevolucionCliente)
        Dim detalles As New List(Of DetalleDevolucionCliente)
        Try
            Dim sql As String = $"""
            SELECT ""LineNum"", ""ItemCode"", ""Dscription"", ""Quantity"", ""Price"", ""UomCode""
              FROM RDR1
             WHERE ""DocEntry"" = {docEntry}
        """

            Dim dt As DataTable = HanaHelper.OpenDT(sql)
            For Each row As DataRow In dt.Rows
                Dim detalle As New DetalleDevolucionCliente With {
                .LineNum = Convert.ToInt32(row("LineNum")),
                .ItemCode = row("ItemCode").ToString(),
                .Dscription = row("Dscription").ToString(),
                .Quantity = Convert.ToDecimal(row("Quantity")),
                .Price = Convert.ToDecimal(row("Price")),
                .UomCode = row("UomCode").ToString()
            }
                detalles.Add(detalle)
            Next
        Catch ex As Exception
            Throw
        End Try
        Return detalles
    End Function

    Public Shared Function Importar_Solicitud_Devolucion_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                        ByRef prg As System.Windows.Forms.ProgressBar,
                                                                        ByRef cnnLog As SqlConnection) As Boolean

        Importar_Solicitud_Devolucion_SAP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResult As String = ""

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia.")

            Dim lDevolucionesCliente As New List(Of clsBeI_nav_ped_compra_enc)
            lDevolucionesCliente = Get_Solicitud_Devolucion_From_SAP()

            BeNavEjecucionRes.Registros_ws = lDevolucionesCliente.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeProductoBodega As New clsBeProducto_bodega

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Pedidos de compra en relación con SAP (OPOR): {0} ", lDevolucionesCliente.Count))

            prg.Maximum = lDevolucionesCliente.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each BeINavDevolCliente In lDevolucionesCliente

                    clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Procesando Pedido Compra: {0} ", BeINavDevolCliente.No, vbNewLine))

                    If Not clsLnProveedor.Existe_Proveedor(BeINavDevolCliente.Buy_From_Vendor_No) Then

                        BeConfigEnc = BeConfigEnc

                        If Inserta_Proveedor_Desde_SAP(BeINavDevolCliente.Buy_From_Vendor_No, cnnLog) Then
                            clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & BeINavDevolCliente.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                        End If

                    End If

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavDevolCliente,
                                                                            BePedidoCompraEnc,
                                                                            vResult) Then
                        Marcar_PI_Sincronizado_SAP(BeINavDevolCliente.No)

                    End If

                    clsPublic.Actualizar_Progreso(lblprg, vResult)

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

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Ordenes de Compra desde SAP a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function
    Public Shared Function Get_Solicitud_Devolucion_From_SAP(Optional ByVal AplicarFiltros As Boolean = True) As List(Of clsBeI_nav_ped_compra_enc)
        Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
        Dim NoLinea As Integer = 0

        Try
            Dim queryEnc As String = $"""
            SELECT T0.""DocEntry"", T0.""DocNum"", T0.""DocDate"", T0.""CardCode"", T0.""CardName"", T0.""DocCur"", T0.""DocTotal"",
                   T0.""JrnlMemo"", T0.""Canceled"", T0.""DocStatus"",
                   CASE WHEN T0.""DocType"" = 'I' THEN 'ARTICULO' ELSE 'SERVICIO' END AS ""TIPO_ORDEN_VENTA"",
                   (SELECT ""WhsCode"" FROM POR1 WHERE POR1.""DocEntry"" = T0.""DocEntry"" LIMIT 1) AS ""BODEGA"",
                   T0.""Comments"", T0.""NumAtCard"",
                   IFNULL(T0.""U_Es_ImportacionWMS"", 0) AS ""U_Es_ImportacionWMS""
              FROM ORRR T0
             WHERE T0.""DocStatus"" = 'O' AND T0.""U_EnviadoWMS"" = 2
        """

            Dim dtEnc As DataTable = HanaHelper.OpenDT(queryEnc)
            For Each rowEnc As DataRow In dtEnc.Rows
                Dim enc As New clsBeI_nav_ped_compra_enc With {
                .No = rowEnc("DocEntry").ToString(),
                .Posting_Date = Convert.ToDateTime(rowEnc("DocDate")),
                .Order_Date = Convert.ToDateTime(rowEnc("DocDate")),
                .Document_Date = Convert.ToDateTime(rowEnc("DocDate")),
                .Expected_Receipt_Date = Convert.ToDateTime(rowEnc("DocDate")),
                .Status = 1,
                .Buy_From_Vendor_No = rowEnc("CardCode").ToString(),
                .Buy_From_Vendor_Name = rowEnc("CardName").ToString(),
                .Is_Internal_Transfer = False,
                .Location_Code = rowEnc("BODEGA").ToString(),
                .Vendor_Invoice_No = rowEnc("NumAtCard").ToString(),
                .Posting_Description = rowEnc("Comments").ToString(),
                .Product_Owner_Code = BeConfigEnc.IdPropietario,
                .Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Devolucion
            }
                If String.IsNullOrWhiteSpace(enc.Vendor_Invoice_No) Then enc.Vendor_Invoice_No = enc.No.ToString()

                Dim queryDet As String = $"""
                SELECT T0.""ItemCode"", T0.""Dscription"", T0.""Quantity"", T0.""Price"", T0.""LineTotal"",
                       T0.""VatSum"", T0.""DocEntry"", T0.""WhsCode"", T0.""OpenCreQty"" AS ""Cantidad_Pendiente"",
                       T0.""BaseLine"", T0.""LineNum"", T1.""U_Um_Prod"" AS ""Unidad_Medida""
                  FROM RRR1 T0
                  JOIN OITM T1 ON T1.""ItemCode"" = T0.""ItemCode""
                 WHERE T0.""DocEntry"" = '{enc.No}' AND T0.""LineStatus"" = 'O'
            """

                Dim dtDet As DataTable = HanaHelper.OpenDT(queryDet)
                enc.Lineas_Detalle = New List(Of clsBeI_nav_ped_compra_det)

                For Each rowDet As DataRow In dtDet.Rows
                    Dim det As New clsBeI_nav_ped_compra_det With {
                    .NoEnc = rowDet("DocEntry").ToString(),
                    .No = rowDet("ItemCode").ToString(),
                    .Line_No = Convert.ToInt32(rowDet("LineNum")),
                    .Planed_Receipt_Date = Date.Now(),
                    .Quantity = Convert.ToDecimal(rowDet("Cantidad_Pendiente")),
                    .Quantity_Received = 0,
                    .Description = rowDet("Dscription").ToString(),
                    .Unit_of_Measure_Code = rowDet("Unidad_Medida").ToString(),
                    .Type = 2,
                    .Variant_Code = Nothing,
                    .Location_Code = rowDet("WhsCode").ToString()
                }
                    enc.Lineas_Detalle.Add(det)
                    NoLinea += 1
                Next

                lPedidosCompra.Add(enc)
            Next

            Return lPedidosCompra

        Catch ex As Exception
            Throw New Exception($"{MethodBase.GetCurrentMethod.Name()} {ex.Message}")
        End Try
    End Function
    Public Shared Function Insertar_Solicitud_Devol_Cli_A_TOMWMS(ByRef lblprg As RichTextBox,
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

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

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

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                      "",
                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                      BeConfigDet.Idnavconfigdet,
                                                      CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de compra a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))

            Throw ex
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Private Shared Function Inserta_Proveedor_Desde_SAP(ByVal pCodigo As String,
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

        Enviar_Entrada_Mercancia_Por_Estado_SAP_B1 = False
        prg.Maximum = lINavTransaccionesOut.Count
        prg.Value = 0
        prg.Visible = True

        Try
            Dim queryEnc As String = $"""
            SELECT ""CardCode"", ""DocDate"", ""DocDueDate"", ""Comments""
              FROM OPOR
             WHERE ""DocEntry"" = {_Docentry}
        """

            Dim dtEnc As DataTable = HanaHelper.OpenDT(queryEnc)
            If dtEnc.Rows.Count = 0 Then Throw New Exception("No se encontró la orden de compra en SAP con DocEntry: " & _Docentry)

            Dim rowEnc = dtEnc.Rows(0)
            Dim lineas = lINavTransaccionesOut.Select(Function(x, idx) New With {
            .ItemCode = x.Codigo_producto,
            .Quantity = x.Cantidad,
            .WarehouseCode = codigoBodegaDestino,
            .BaseType = 22,
            .BaseEntry = _Docentry,
            .BaseLine = x.No_linea
            }).ToList()

            Dim payload As New With {
            .CardCode = rowEnc("CardCode").ToString(),
            .DocDate = DateTime.Today.ToString("yyyy-MM-dd"),
            .DocDueDate = DateTime.Today.ToString("yyyy-MM-dd"),
            .Comments = rowEnc("Comments").ToString(),
            .DocumentLines = lineas
            }

            Dim client As New SapServiceLayerClient()
            'Dim result = client.PostDeliveryAsync(payload)

            clsPublic.Actualizar_Progreso(lblPrg, "Entrada de mercancía por estado enviada correctamente a SAP.")

            Dim actualizado As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(
            lINavTransaccionesOut.Where(Function(x) Not x.Enviado).ToList())

            If actualizado = 0 Then
                Throw New Exception("La entrada se envió a SAP pero no se marcaron como enviados en WMS.")
            End If

            Return True

        Catch ex As Exception
            Throw New Exception($"{MethodBase.GetCurrentMethod.Name()} {ex.Message}")

        Finally
            prg.Value = 0
            prg.Visible = False
        End Try

    End Function

End Class