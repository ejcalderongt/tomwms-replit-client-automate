Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports DevExpress.XtraEditors
Imports SAPbobsCOM

Public Class clsSyncSAPPedidoCompra : Inherits clsInterfaceBase
    Implements IDisposable

    Private Shared VContadorBitacoraTOMWMS As Integer = 0
    Private VContadorBitacoraIntermedia As Integer = 0

    Public Sub Dispose() Implements IDisposable.Dispose

    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                        ByRef prg As System.Windows.Forms.ProgressBar,
                                                                        ByRef cnnLog As SqlConnection) As Boolean

        Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResult As String = ""

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia TOMWMS.")

            Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
            lPedidosCompra = Get_Pedidos_Compra_From_SAP()

            BeNavEjecucionRes.Registros_ws = lPedidosCompra.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeProductoBodega As New clsBeProducto_bodega

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Pedidos de compra en relación con SAP (OPOR): {0} ", lPedidosCompra.Count))

            prg.Maximum = lPedidosCompra.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            'Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each BeINavPedCompra In lPedidosCompra

                    clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Procesando Pedido Compra: {0} / {1} ", BeINavPedCompra.No, BeINavPedCompra.Vendor_Invoice_No, vbNewLine))

                    If Not clsLnProveedor.Existe_Proveedor(BeINavPedCompra.Buy_From_Vendor_No) Then

                        BeConfigEnc = BeConfigEnc

                        If Inserta_Proveedor_Desde_SAP(BeINavPedCompra.Buy_From_Vendor_No, cnnLog) Then
                            clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & BeINavPedCompra.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                        End If

                    End If

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                            BePedidoCompraEnc,
                                                                            vResult) Then
                        Marcar_PI_Sincronizado_SAP(BeINavPedCompra.No)

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
                                                                       BeConfigDet.Idnavconfigdet, lConnectionLog)

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
            Throw ex
        End Try

    End Function

    Public Function Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                                   ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                   Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                   Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

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

            '#EJC340520: Limpiar el objeto antes de iniciar la transacción.
            lblprg.Text = ""

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

            Application.DoEvents()

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
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

            Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = True

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            prg.Value = 0

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de compra a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))

            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    Public Function Get_Pedidos_Compra_From_SAP(Optional ByVal AplicarFiltros As Boolean = True) As List(Of clsBeI_nav_ped_compra_enc)

        Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
        Dim BePedidoCompra As New clsBeI_nav_ped_compra_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_compra_det
        Dim NoLinea As Integer = 0

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                Dim vSQLOC As String

                vSQLOC = " SELECT T0.DOCENTRY,
                           T0.DOCNUM,T0.DOCDATE,  
                           T0.CARDCODE,
                           T0.CARDNAME,
                           T0.DOCCUR,
                           T0.DOCTOTAL,
                           T0.JRNLMEMO,
                           T0.CANCELED,T0.DOCSTATUS,  
                           CASE WHEN T0.DOCTYPE = 'I'THEN 'ARTICULO'    
                            ELSE 'SERVICIO'    
                            END AS TIPO_ORDEN_VENTA,
                            (SELECT TOP 1 D0.WhsCode 
                                FROM POR1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode WHERE D0.DocEntry = T0.DOCENTRY) AS BODEGA,
                            T0.COMMENTS,
                            CASE WHEN LEN(T0.NumAtCard)>30 THEN SUBSTRING(T0.NumAtCard,1,30) ELSE T0.NumAtCard END NumAtCard,
                            T0.U_Es_ImportacionWMS
                                FROM OPOR T0 WHERE DOCSTATUS = 'O' 
                            AND CreateDate >= '2020-10-09 00:00:00.000' 
                            AND U_EnviadoWMS = 2 ORDER BY t0.DOCENTRY DESC "

                RsEnc.DoQuery(vSQLOC)

                While RsEnc.EoF = False

                    BePedidoCompra = New clsBeI_nav_ped_compra_enc()
                    BePedidoCompra.No = RsEnc.Fields.Item("DOCENTRY").Value
                    BePedidoCompra.Posting_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BePedidoCompra.Order_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BePedidoCompra.Document_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BePedidoCompra.Expected_Receipt_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BePedidoCompra.Status = 1
                    BePedidoCompra.Buy_From_Vendor_No = RsEnc.Fields.Item("CARDCODE").Value.ToString()
                    BePedidoCompra.Buy_From_Vendor_Name = RsEnc.Fields.Item("CARDNAME").Value.ToString()
                    BePedidoCompra.Is_Internal_Transfer = False
                    BePedidoCompra.Location_Code = Convert.ToString(RsEnc.Fields.Item("BODEGA").Value)
                    BePedidoCompra.Vendor_Invoice_No = Convert.ToString(RsEnc.Fields.Item("DOCNUM").Value).ToString()
                    BePedidoCompra.Posting_Description = RsEnc.Fields.Item("COMMENTS").Value.ToString()
                    BePedidoCompra.Product_Owner_Code = BeConfigEnc.IdPropietario
                    BePedidoCompra.Vendor_Invoice_No = RsEnc.Fields.Item("NUMATCARD").Value.ToString()
                    BePedidoCompra.Document_Type = RsEnc.Fields.Item("U_ES_IMPORTACIONWMS").Value.ToString()

                    If BePedidoCompra.Vendor_Invoice_No = "" Then
                        BePedidoCompra.Vendor_Invoice_No = BePedidoCompra.No.ToString()
                    End If

                    Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                    Dim query_det As String = ""

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
                                        T1.U_Um_Prod AS UNIDAD_MEDIDA  
                                        FROM POR1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode 
                                        WHERE T0.DOCENTRY = '" & BePedidoCompra.No & "'" &
                                        "AND T0.LINESTATUS = 'O' 
                                          ORDER BY t0.DOCENTRY DESC "

                    RsDet.DoQuery(query_det)

                    BePedidoCompra.Lineas_Detalle = New List(Of clsBeI_nav_ped_compra_det)

                    Dim vNoLineaBase As Integer = 0
                    Dim vNoLinea As Integer = 0

                    While RsDet.EoF = False

                        BePedidoDetWMS = New clsBeI_nav_ped_compra_det()
                        BePedidoDetWMS.NoEnc = RsDet.Fields.Item("DOCENTRY").Value.ToString()
                        BePedidoDetWMS.No = RsDet.Fields.Item("ITEMCODE").Value.ToString()

                        vNoLineaBase = IIf(IsDBNull(RsDet.Fields.Item("BASELINE").Value.ToString()), 0, RsDet.Fields.Item("BASELINE").Value.ToString())
                        vNoLinea = IIf(IsDBNull(RsDet.Fields.Item("LINENUM").Value.ToString()), 0, RsDet.Fields.Item("LINENUM").Value.ToString())

                        BePedidoDetWMS.Line_No = Val(vNoLinea)
                        BePedidoDetWMS.Planed_Receipt_Date = Date.Now()
                        BePedidoDetWMS.Quantity = Convert.ToDecimal(RsDet.Fields.Item("CANTIDAD_PENDIENTE").Value)
                        BePedidoDetWMS.Quantity_Received = 0
                        BePedidoDetWMS.Description = RsDet.Fields.Item("DSCRIPTION").Value.ToString()
                        BePedidoDetWMS.Unit_of_Measure_Code = (RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString())
                        BePedidoDetWMS.Type = 2
                        BePedidoDetWMS.Variant_Code = Nothing
                        BePedidoDetWMS.Location_Code = RsDet.Fields.Item("WHSCODE").Value.ToString()
                        BePedidoCompra.Lineas_Detalle.Add(BePedidoDetWMS)
                        NoLinea += 1
                        RsDet.MoveNext()

                    End While

                    lPedidosCompra.Add(BePedidoCompra)

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

                        oPedidoSBO.UserFields.Fields.Item("U_EnviadoWMS").Value = "1"
                        oPedidoSBO.Update()

                    Catch e As Exception
                    End Try

                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


    Private Function Get_Objetos_Documento_Ingreso(ByVal IdOrdenCompraEnc As Integer,
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

    Public Function Enviar_Transacciones_De_Ingreso_SAP(ByRef lblprg As RichTextBox,
                                                        ByRef prg As Windows.Forms.ProgressBar) As Boolean

        Dim lTransaccionesIngreso As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesIngresoSingle As New List(Of clsBeI_nav_transacciones_out)
        Dim TipoPedidoCompra As Integer = 0
        Dim Enviado_A_Erp As Boolean = False
        Dim vCodigoBodegaDestino As String = ""
        Dim BeProductoEstado_NC As New clsBeProducto_estado
        Dim BeBodegaUbicacion As New clsBeBodega_ubicacion
        Dim clsTrans As New clsTransaccion

        Enviar_Transacciones_De_Ingreso_SAP = False

        Try


            clsTrans.Begin_Transaction()

            lTransaccionesIngreso = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio(clsTrans.lConnection,
                                                                                                    clsTrans.lTransaction)

            If Not lTransaccionesIngreso Is Nothing AndAlso lTransaccionesIngreso.Count > 0 Then

                Dim ListaPedidosCompra = (From i In lTransaccionesIngreso
                                          Group i By Keys = New With {Key i.No_pedido,
                                                                      Key i.Idordencompra,
                                                                      Key i.Idrecepcionenc,
                                                                      Key i.Idbodega,
                                                                      Key i.IdTipoDocumento} Into Group
                                          Select New With {Key Keys.No_pedido,
                                                           Key Keys.Idordencompra,
                                                           Key Keys.Idrecepcionenc,
                                                           Key Keys.Idbodega,
                                                           Key Keys.IdTipoDocumento})

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesIngreso.Count))

                Dim BeReOC As New clsBeTrans_re_oc
                Dim BeTransOCEnc As New clsBeTrans_oc_enc
                Dim BeEstadoProductoBueno As New clsBeProducto_estado

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, clsTrans.lConnection, clsTrans.lTransaction)

                If Configuracion_Interface_Correcta(BeConfigEnc) Then

                    If ListaPedidosCompra.Count > 0 Then

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

                                    clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Enviando pedido de compra:{0} - BodegaDestino: {1} ", DocumentoIngreso.No_pedido, vCodigoBodegaDestino, vbNewLine))

                                    Select Case TipoPedidoCompra

                                        Case clsDataContractDI.tTipoDocumentoIngreso.Ingreso, clsDataContractDI.tTipoDocumentoIngreso.Devolucion 'Es un pedido de compra de proveedor.

                                            If Not Enviado_A_Erp Then

                                                lTransaccionesIngresoSingle = lTransaccionesIngreso.FindAll(Function(x) x.No_pedido = DocumentoIngreso.No_pedido AndAlso x.IdTipoDocumento = TipoPedidoCompra)

                                                If IsNumeric(DocumentoIngreso.No_pedido) Then

                                                    If Enviar_Entrada_Mercancia_OC_SAP(BeConfigEnc,
                                                                                       DocumentoIngreso.No_pedido,
                                                                                       lTransaccionesIngresoSingle,
                                                                                       BeTransOCEnc,
                                                                                       vCodigoBodegaDestino,
                                                                                       lblprg,
                                                                                       prg) Then

                                                        Try

                                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Documento registrado correctamente: {0}", BeReOC.OC.No_Documento))

                                                            BeReOC.No_docto = "ENV-WMS" & FormatoFechas.tFecha(Now)
                                                            clsLnTrans_re_oc.Actualizar_No_Docto(BeReOC, clsTrans.lConnection, clsTrans.lTransaction)
                                                            clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(DocumentoIngreso.Idordencompra, True)

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
                                                    clsPublic.Actualizar_Progreso(lblprg, "El documento: " & DocumentoIngreso.No_pedido & " no es válido para ser procesado en sap (no númerico).")
                                                End If

                                            Else
                                                '#EJC20201119: aun no se que sucederá aquí....
                                                Try

                                                    clsLnTrans_oc_enc.Actualizar_Estado_OC_By_Interface(DocumentoIngreso.Idordencompra,
                                                                                                        clsDataContractDI.tEstadoOC.BACK_ORDER,
                                                                                                        clsTrans.lConnection,
                                                                                                        clsTrans.lTransaction)

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Se registró el pedido:{0} correctamente en el ERP.", DocumentoIngreso.No_pedido))

                                                    clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(DocumentoIngreso.Idordencompra, True)

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

                                        Case 2 'Es un pedido de transferencia desde una bodega X hacia la bodega de WMS.

                                            If Not Enviado_A_Erp Then


                                            End If

                                    End Select

                                End If


                            Catch ex As Exception
                                Dim vMensaje As String = String.Format(vbTab & "{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
                                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
                            End Try

                        Next

                        Enviar_Transacciones_De_Ingreso_SAP = True

                    End If

                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No hay registros de ingreso para envío a SAP.")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            prg.Value = 0
            prg.Visible = False
        End Try

    End Function

    Private Function Configuracion_Interface_Correcta(ByVal BeINavConfigEnc As clsBeI_nav_config_enc) As Boolean

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

    Private Function Producto_En_SAP_Tiene_Control_Lote(ByVal ProductCode As String) As Boolean

        Producto_En_SAP_Tiene_Control_Lote = False

        Try

            Dim oItems As Items = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)
            If oItems.GetByKey(ProductCode) Then
                Dim isBatchManaged As Boolean = oItems.ManageBatchNumbers
                Producto_En_SAP_Tiene_Control_Lote = isBatchManaged
            Else
                Throw New Exception("ERROR_SAP: No se encontró el material: " & ProductCode)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Enviar_Entrada_Mercancia_OC_SAP(ByVal BeINavConfigEnc As clsBeI_nav_config_enc,
                                                    ByVal _Docentry As Integer,
                                                    ByVal lINav_Transaccioens_Out As List(Of clsBeI_nav_transacciones_out),
                                                    ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                    ByVal vCodigoBodegaDestino As String,
                                                    ByRef lblprg As RichTextBox,
                                                    ByRef prg As Windows.Forms.ProgressBar) As Boolean


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
        Dim EnvioEntradaBuenEstado As Boolean = False
        Dim EnvioEntradaMalEstado As Boolean = False
        Dim EnvioEntradaTeorico As Boolean = False

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

                EnvioEntradaBuenEstado = Enviar_Entrada_Mercancia_Por_Estado_SAP_B1(BeINavConfigEnc,
                                                                                    _Docentry,
                                                                                    lINav_Transaccioens_Out,
                                                                                    BeTransOCEnc,
                                                                                    vCodigoBodegaDestino,
                                                                                    lblprg,
                                                                                    prg)


                BeTipoIngreso = clsLnTrans_oc_ti.GetSingle(BeTransOCEnc.IdTipoIngresoOC)

                If BeTipoIngreso IsNot Nothing Then

                    'If Not BeTipoIngreso.Es_devolucion Then

                    '    BeProductoEstado_NC = clsLnProducto_estado.Get_Single_By_IdEstado(BeConfigEnc.IdProductoEstado_NC)

                    '    EnvioEntradaTeorico = Enviar_Entrada_Mercancia_Teorica_SAP(BeINavConfigEnc,
                    '                                                                   BeProductoEstado_NC,
                    '                                                                   _Docentry,
                    '                                                                   lINav_Transaccioens_Out,
                    '                                                                   BeTransOCEnc,
                    '                                                                   vCodigoBodegaDestino,
                    '                                                                   lblprg,
                    '                                                                   prg)

                    'End If

                End If


                Dim oOrderPurchase As Documents
                oOrderPurchase = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)

                Dim vMensajeError As String = "#ERROR_SAP_20231030: Error al actualizar la Orden de Compra:" & sErrMsg
                Dim vMensajeResultado As String = ""

                If oOrderPurchase.GetByKey(_Docentry) Then

                    Select Case BeTransOCEnc.TipoIngreso.IdTipoIngresoOC

                        Case clsDataContractDI.tTipoDocumentoIngreso.Devolucion

                            vMensajeError = "#ERROR_SAP_20231030: Error al actualizar la Orden de Compra:" & sErrMsg

                    End Select

                    If oOrderPurchase.Update() <> 0 Then
                        sErrMsg = oCompany.GetLastErrorDescription()
                        clsPublic.Actualizar_Progreso(lblprg, vMensajeError)
                        clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(BeTransOCEnc.IdOrdenCompraEnc, False)
                    Else
                        clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(BeTransOCEnc.IdOrdenCompraEnc, True)
                        If BeTransOCEnc.TipoIngreso.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso Then
                            vMensajeResultado = "Pedido de compra actualizado a estado: " & IIf(oOrderPurchase.DocumentStatus = 1, "Cerrado", "Abierto")
                        ElseIf BeTransOCEnc.TipoIngreso.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Devolucion Then
                            vMensajeResultado = "Solicitud de Devolución actualizada a estado: " & IIf(oOrderPurchase.DocumentStatus = 1, "Cerrado", "Abierto")
                        End If
                        clsPublic.Actualizar_Progreso(lblprg, vMensajeResultado)
                    End If

                End If


            End If

            Return True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)
            Throw ex

        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Enviar_Entrada_Mercancia_Teorica_SAP(ByVal BeINavConfigEnc As clsBeI_nav_config_enc,
                                                         ByVal BeProductoEstado_NC As clsBeProducto_estado,
                                                         ByVal _Docentry As Integer,
                                                         ByVal lINav_Transaccioens_Out As List(Of clsBeI_nav_transacciones_out),
                                                         ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                         ByVal vCodigoBodegaDestino As String,
                                                         ByRef lblprg As RichTextBox,
                                                         ByRef prg As Windows.Forms.ProgressBar) As Boolean


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
        Dim vAgregarEntrega As Boolean = False

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

                                    If ListaResumen.Any() Then

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
                                                'BeStockPushAlmacenaje.IdStock = clsLnStock.MaxID() + 1
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
                                                If BeProducto.Control_vencimiento Then BeStockPushAlmacenaje.Fecha_vence = Now.AddYears(1).Date
                                                BeStockPushAlmacenaje.Fecha_Ingreso = Now
                                                BeStockPushAlmacenaje.Cantidad = vDiferenciaPorLineaIngreso
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

                                                vAgregarEntrega = True

                                            End If

                                        Next

                                    Else
                                        clsPublic.Actualizar_Progreso(lblprg, "Nada que enviar a PNC.")
                                    End If

                                End If

                            Else
                                Throw New Exception("ERROR_202310310600: No se encontrò el objeto relacionado con la lìnea.")
                            End If

                        End If

                    Next

                    If vAgregarEntrega Then

                        Dim oResultado As Integer
                        oResultado = oEntrega.Add()

                        If oResultado <> 0 Then
                            sErrMsg = oCompany.GetLastErrorDescription()
                            Throw New Exception("#ERROR_SAP_202309270131: " & sErrMsg)
                        Else

                            Enviar_Entrada_Mercancia_Teorica_SAP = True

                        End If

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
            Desconectar_SAP(oCompany)
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
    Public Function Enviar_Entrada_Mercancia_Por_Estado_SAP_B1(ByVal beINavConfigEnc As clsBeI_nav_config_enc,
                                                               ByVal _Docentry As Integer,
                                                               ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                               ByVal beTransOCEnc As clsBeTrans_oc_enc,
                                                               ByVal codigoBodegaDestino As String,
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

                Case clsDataContractDI.tTipoDocumentoIngreso.Ingreso

                    Try

                        oOrderPurchase = DirectCast(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)
                        If oOrderPurchase IsNot Nothing AndAlso oOrderPurchase.GetByKey(_Docentry) Then
                            Procesar_Detalle_Ingreso(oOrderPurchase,
                                                     beTransOCEnc.TipoIngreso.IdTipoIngresoOC,
                                                     lINavTransaccionesOut,
                                                     lblPrg)
                        Else
                            'clsPublic.Actualizar_Progreso(lblPrg, "ERROR_202311212019: No se pudo obtener el documento de devolución de sap para el _Docentry: " & _Docentry)
                            oOrderPurchase = DirectCast(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseInvoices), Documents)
                            If oOrderPurchase IsNot Nothing AndAlso oOrderPurchase.GetByKey(beTransOCEnc.No_Documento) Then
                                If Procesar_Detalle_Ingreso(oOrderPurchase,
                                                            beTransOCEnc.TipoIngreso.IdTipoIngresoOC,
                                                            lINavTransaccionesOut,
                                                            lblPrg) Then
                                    Enviar_Entrada_Mercancia_Por_Estado_SAP_B1 = True
                                End If
                            Else
                                clsPublic.Actualizar_Progreso(lblPrg, "ERROR_202311212019: No se pudo obtener el documento de devolución de sap para el _Docentry: " & _Docentry)
                            End If
                        End If

                    Catch ex As Exception
                        Throw
                    End Try


                Case clsDataContractDI.tTipoDocumentoIngreso.Devolucion

                    oOrderPurchase = DirectCast(oCompany.GetBusinessObject(BoObjectTypes.oReturnRequest), Documents)

                    If oOrderPurchase IsNot Nothing AndAlso oOrderPurchase.GetByKey(_Docentry) Then
                        Procesar_Detalle_Ingreso_Devolucion(oOrderPurchase, beTransOCEnc.TipoIngreso.IdTipoIngresoOC, lINavTransaccionesOut, lblPrg)
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

    Private Sub Configurar_Entrega_Compra(ByRef oEntrega As Documents, oOrderPurchase As Documents)

        Try

            oEntrega = oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes)

            ' Asignar el código del cliente/proveedor a la entrega.
            oEntrega.CardCode = oOrderPurchase.CardCode

            ' Asignar la fecha actual como la fecha de la entrega.
            oEntrega.DocDate = Date.Today
            oEntrega.DocCurrency = oOrderPurchase.DocCurrency

            ' Si es una reserva de proveedores, asignar detalles adicionales, como moneda o impuestos.
            If oOrderPurchase.DocObjectCode = BoObjectTypes.oPurchaseInvoices Then
                oEntrega.Comments = "Entrega generada para la factura de reserva de proveedores."
            Else
                oEntrega.DocObjectCode = BoObjectTypes.oPurchaseDeliveryNotes
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub Configurar_Devolucion_Cliente(ByRef oReturn As Documents, oReturnRequest As Documents)
        oReturn = oCompany.GetBusinessObject(BoObjectTypes.oReturns)
        oReturn.CardCode = oReturnRequest.CardCode
        oReturn.DocDate = Date.Today
        oReturn.DocObjectCode = BoObjectTypes.oReturns
    End Sub

    Private Function Procesar_Detalle_Ingreso(ByVal oOrderPurchase As Documents,
                                              ByVal pTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
                                              ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                              ByRef lblPrg As RichTextBox) As Boolean

        'Dim clsTransaccion As New clsTransaccion
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
        Dim vCantidadEntregas As Integer = 0

        Dim Enviado_A_Erp As Boolean = False

        Try

            clsTransaccion.Begin_Transaction()

            'Búsqueda de todos los estados de un código y línea sin importar el lote.
            Dim EstadosRecibidos = lINavTransaccionesOut.GroupBy(Function(x) x.Idproductoestado).
                                                            Select(Function(g) New With {
                                                                .Idproductoestado = g.Key,
                                                                .Lotes = g.ToList(),
                                                                .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                            }).ToList()


            If EstadosRecibidos.Count > 0 Then

                For Each Estado In EstadosRecibidos

                    Dim BeProductoEstado = clsLnProducto_estado.Get_Single_By_IdEstado(Estado.Idproductoestado,
                                                                                       clsTransaccion.lConnection,
                                                                                       clsTransaccion.lTransaction)

                    If BeProductoEstado Is Nothing Then
                        Throw New Exception($"No está definido el estado del producto y no se puede inferir la bodega destino para IdProductoEstado: {Estado.Idproductoestado}")
                    ElseIf String.IsNullOrWhiteSpace(BeProductoEstado.Codigo_Bodega_ERP) Then
                        Throw New Exception($"No está definido el código de bodega SAP para el producto, configure: Producto-Estado-Codigo_Bodega_ERP para el IdEstado: {Estado.Idproductoestado}")
                    End If

                    vCodigoBodegaERP = BeProductoEstado.Codigo_Bodega_ERP

                    If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso Then
                        Configurar_Entrega_Compra(oEntrega, oOrderPurchase)
                    End If

                    If oEntrega Is Nothing Then
                        Throw New Exception("No se pudo crear el objeto de entrega.")
                    End If

                    Dim vIdRecepcionEnc As Integer = lINavTransaccionesOut.FirstOrDefault.Idrecepcionenc
                    Dim vCadenaFacturas As String = clsLnTrans_re_fact.Get_Cadena_Factura_By_IdRecepcionEnc(vIdRecepcionEnc)

                    If vCantidadEntregas = 0 Then
                        oEntrega.NumAtCard = vCadenaFacturas
                    Else
                        oEntrega.NumAtCard = vCadenaFacturas & "-" & vCantidadEntregas
                    End If

                    'Reset variables de control
                    NoLineaEntrega = 0 : NoLineaEntregaLote = 0

                    For j As Integer = 0 To oOrderPurchase.Lines.Count - 1

                        oOrderPurchase.Lines.SetCurrentLine(j)

                        Dim vCodigoProductoSAP As String = oOrderPurchase.Lines.ItemCode.ToString()
                        Dim vNoLineaOCSAP As Integer = oOrderPurchase.Lines.LineNum

                        Dim DistinctProductosLineas = lINavTransaccionesOut.Where(Function(x) x.Idproductoestado = Estado.Idproductoestado _
                                                                                  AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                                  AndAlso x.No_linea = vNoLineaOCSAP).
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

                                    oEntrega.Lines.SetCurrentLine(NoLineaEntrega)

                                    If oOrderPurchase.DocObjectCode = BoObjectTypes.oPurchaseInvoices Then
                                        oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_PurchaseInvoice)
                                    Else
                                        oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_PurchaseOrder)
                                    End If

                                    Dim vTipoImpuesto As String = oOrderPurchase.Lines.TaxCode

                                    oEntrega.Lines.BaseEntry = oOrderPurchase.DocEntry
                                    oEntrega.Lines.ItemCode = ProductoIngreso.Codigo_producto
                                    oEntrega.Lines.BaseLine = vNoLineaOCSAP
                                    oEntrega.Lines.TaxCode = vTipoImpuesto
                                    oEntrega.Lines.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                                    oEntrega.Lines.Quantity = ProductoIngreso.Cantidad_Total
                                    oEntrega.Lines.WarehouseCode = BeProductoEstado.Codigo_Bodega_ERP

                                    vCodigoAnterior = oEntrega.Lines.ItemCode
                                    vNoLineaAnterior = oEntrega.Lines.LineNum

                                    Dim vControlPorLote As Boolean = clsLnProducto.Get_Control_Lote_By_Codigo(ProductoIngreso.Codigo_producto,
                                                                                                              clsTransaccion.lConnection,
                                                                                                              clsTransaccion.lTransaction)
                                    If vControlPorLote Then

                                        Dim LotesRecibidosPorEstado = lINavTransaccionesOut.
                                                        Where(Function(x) x.Codigo_producto = ProductoIngreso.Codigo_producto _
                                                              AndAlso x.No_linea = ProductoIngreso.No_linea _
                                                              AndAlso x.Idproductoestado = Estado.Idproductoestado).
                                                        GroupBy(Function(x) x.Idproductoestado).
                                                        Select(Function(g) New With {
                                                            .Idproductoestado = g.Key,
                                                            .Lotes = g.GroupBy(Function(x) New With {Key x.Lote, Key x.Codigo_producto, Key x.No_linea, Key x.Fecha_vence}).
                                                                       Select(Function(lg) New With {
                                                                           .Codigo_producto = lg.Key.Codigo_producto,
                                                                           .No_linea = lg.Key.No_linea,
                                                                           .Lote = lg.Key.Lote,
                                                                           .Fecha_vence = lg.Key.Fecha_vence,
                                                                           .CantidadTotal = lg.Sum(Function(x) x.Cantidad)
                                                                       }).ToList()
                                                        }).ToList()

                                        If LotesRecibidosPorEstado.Count > 0 Then

                                            For Each LoteAgrupadoPorEstado In LotesRecibidosPorEstado

                                                NoLineaEntregaLote = 0

                                                For Each Lote In LoteAgrupadoPorEstado.Lotes

                                                    oEntrega.Lines.BatchNumbers.SetCurrentLine(NoLineaEntregaLote)
                                                    oEntrega.Lines.BatchNumbers.BaseLineNumber = NoLineaEntrega
                                                    oEntrega.Lines.BatchNumbers.ItemCode = Lote.Codigo_producto
                                                    oEntrega.Lines.BatchNumbers.BatchNumber = Lote.Lote
                                                    oEntrega.Lines.BatchNumbers.Quantity = Lote.CantidadTotal
                                                    oEntrega.Lines.BatchNumbers.ExpiryDate = Lote.Fecha_vence
                                                    'oEntrega.Lines.BatchNumbers.InternalSerialNumber = Lote.Licencia
                                                    'oEntrega.Lines.BatchNumbers.Location = BeProductoEstado.Codigo_Bodega_ERP
                                                    oEntrega.Lines.BatchNumbers.AddmisionDate = Now
                                                    oEntrega.Lines.BatchNumbers.Add()
                                                    NoLineaEntregaLote += 1

                                                    clsPublic.Actualizar_Progreso(lblPrg, vbTab & "Lote: " & Lote.Lote & " Cantidad: " & Lote.CantidadTotal & " Producto: " & Lote.Codigo_producto & " Vence: " & Lote.Fecha_vence.Date)

                                                    Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderPurchase.DocEntry _
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

                                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderPurchase.DocEntry _
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

                                    oEntrega.Lines.Add() : NoLineaEntrega += 1

                                End If

                            Next 'DistinctProductosLineas

                            vAgregarEntrega = NoLineaEntrega > 0

                        Else
                            clsPublic.Actualizar_Progreso(lblPrg, "MSG240416A: No se obtuvieron las lìneas del documento: " & oOrderPurchase.DocEntry & " para enviar a SAP.")
                        End If

                    Next

                    If vAgregarEntrega Then

                        If vCantidadEntregas = 0 Then

                            Dim exchangeRate As Double = oOrderPurchase.DocRate 'oOrderPurchase.DocRateObtenerTasaCambioSAPB1(oCompany, oOrderPurchase.DocCurrency, "GTQ", Now.Date)

                            For k As Integer = 0 To oOrderPurchase.Expenses.Count - 1

                                oOrderPurchase.Expenses.SetCurrentLine(k)

                                If oOrderPurchase.Expenses.LineTotal > 0 Then

                                    oEntrega.Expenses.ExpenseCode = oOrderPurchase.Expenses.ExpenseCode
                                    oEntrega.Expenses.TaxCode = oOrderPurchase.Expenses.TaxCode
                                    oEntrega.Expenses.LineTotal = IIf(oOrderPurchase.DocCurrency = "USD", oOrderPurchase.Expenses.LineTotal / exchangeRate, oOrderPurchase.Expenses.LineTotal)
                                    oEntrega.Expenses.BaseDocEntry = oOrderPurchase.Expenses.BaseDocEntry
                                    oEntrega.Expenses.Add()

                                End If

                            Next

                        End If

                        oEntrega.DocRate = oOrderPurchase.DocRate

                        ' Ejecuta oResultado = oEntrega.Add() después de configurar la línea
                        oResultado = oEntrega.Add()

                        vCantidadEntregas += 1

                        If oResultado <> 0 Then
                            Dim errMsg = oCompany.GetLastErrorDescription()
                            Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                        Else

                            vRegistroEntregaPorLote = True

                            If Not Lista_A_Actualizar Is Nothing Then
                                If Lista_A_Actualizar.Count > 0 Then
                                    For Each T In Lista_A_Actualizar
                                        T.Enviado = True
                                        T.Fec_mod = Now
                                        clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(T)
                                    Next
                                End If
                            End If

                            Procesar_Detalle_Ingreso = True

                            clsPublic.Actualizar_Progreso(lblPrg, vbTab & "Se creó la entrega en SAP para la bodega: " & vCodigoBodegaERP)

                        End If

                    End If

                Next 'EstadosRecibidos

            Else
                clsPublic.Actualizar_Progreso(lblPrg, "MSG240416: No se obtuvieron registros para enviar a SAP para el documento: " & oOrderPurchase.DocEntry)
            End If


        Catch ex As Exception
            Throw
        Finally
            If oEntrega IsNot Nothing Then
                Marshal.ReleaseComObject(oEntrega) ' Liberar el objeto de entrega si se creó
            End If
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Function ObtenerTasaCambioSAPB1(ByVal oCompany As Company, ByVal monedaOrigen As String, ByVal monedaDestino As String, ByVal fecha As Date) As Double

        Dim tasaCambio As Double = 0.0

        Try

            ' Crear el objeto SBObob
            Dim oSBObob As SBObob = oCompany.GetBusinessObject(BoObjectTypes.BoBridge)

            ' Obtener la tasa de cambio actual
            Dim oRecordset As Recordset = oSBObob.GetCurrencyRate(monedaOrigen, fecha)

            If oRecordset IsNot Nothing AndAlso Not oRecordset.EoF Then
                tasaCambio = oRecordset.Fields.Item(0).Value
            End If

            ' Liberar el objeto Recordset
            Marshal.ReleaseComObject(oRecordset)

        Catch ex As Exception
            ' Manejo de excepciones
            Console.WriteLine("Error al obtener la tasa de cambio: " & ex.Message)
        End Try

        Return tasaCambio

    End Function

    'Private Function Procesar_Detalle_Ingreso_Devolucion(ByVal oOrderPurchase As Documents,
    '                                                     ByVal pTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
    '                                                     ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
    '                                                     ByRef lblPrg As RichTextBox) As Boolean
    '    Try
    '        ' Establecer conexión con SAP
    '        Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

    '        ' Inicializar objeto Documento para la devolución de mercancías
    '        Dim oReturn As Documents = Nothing

    '        ' Configurar encabezado del documento de devolución
    '        Configurar_Devolucion_Cliente(oReturn, oOrderPurchase)

    '        ' Verificar que el objeto oReturn se haya inicializado correctamente
    '        If oReturn Is Nothing Then
    '            Throw New Exception("El documento de devolución no pudo ser inicializado.")
    '        End If

    '        ' Agrupar datos por Código de Producto y Lote
    '        Dim agrupados = lINavTransaccionesOut.
    '        GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.Lote}).
    '        Select(Function(g) New With {
    '            Key .Codigo_producto = g.Key.Codigo_producto,
    '            Key .Lote = g.Key.Lote,
    '            .Cantidad = g.Sum(Function(x) x.Cantidad),
    '            .Fecha_vence = g.First().Fecha_vence
    '        }).ToList()

    '        Dim NoLineaEntregaLote As Integer = 0

    '        ' Procesar cada grupo para las líneas de detalle (RDN1)
    '        For Each grupo In agrupados

    '            ' Variables para BaseEntry y BaseLine
    '            Dim baseEntry As Integer = 0
    '            Dim baseLine As Integer = 0

    '            ' Obtener BaseEntry y BaseLine para el grupo actual
    '            If Not Obtener_BaseEntry_Y_BaseLine_De_SAP(oOrderPurchase, baseEntry, baseLine) Then
    '                Throw New Exception("No se pudieron obtener BaseEntry y BaseLine de SAP para el grupo actual.")
    '            End If

    '            ' Configurar líneas del documento de devolución
    '            oReturn.Lines.BaseEntry = baseEntry
    '            oReturn.Lines.BaseLine = baseLine
    '            'oReturn.Lines.BaseType = 234000031
    '            oReturn.Lines.BaseType = BoObjectTypes.oOrders.oReturnRequest 'Solicitud de devolución de cliente.
    '            oReturn.Lines.ItemCode = grupo.Codigo_producto
    '            oReturn.Lines.Quantity = grupo.Cantidad

    '            oReturn.Lines.BatchNumbers.SetCurrentLine(NoLineaEntregaLote)
    '            oReturn.Lines.BatchNumbers.BaseLineNumber = baseLine
    '            oReturn.Lines.BatchNumbers.ItemCode = grupo.Codigo_producto
    '            oReturn.Lines.BatchNumbers.BatchNumber = grupo.Lote
    '            oReturn.Lines.BatchNumbers.Quantity = grupo.Cantidad
    '            oReturn.Lines.BatchNumbers.ExpiryDate = grupo.Fecha_vence
    '            'oReturn.Lines.BatchNumbers.InternalSerialNumber = grupo.Licencia
    '            'oReturn.Lines.BatchNumbers.Location = BeProductoEstado.Codigo_Bodega_ERP
    '            oReturn.Lines.BatchNumbers.AddmisionDate = Now
    '            oReturn.Lines.BatchNumbers.Add()
    '            NoLineaEntregaLote += 1

    '            oReturn.Lines.Add()

    '        Next

    '        Dim nombreCampoRegion As String = "U_Region"
    '        oReturn.UserFields.Fields.Item(nombreCampoRegion).Value = "CENTRAL"

    '        Dim nombreCampoUGira As String = "U_Gira"
    '        oReturn.UserFields.Fields.Item(nombreCampoUGira).Value = "CADENAS"

    '        ' Guardar el documento en SAP
    '        If oReturn.Add() <> 0 Then
    '            Throw New Exception("Error al agregar documento de devolución: " & oCompany.GetLastErrorDescription)
    '        End If

    '        ' Confirmación del proceso
    '        Return True

    '    Catch ex As Exception
    '        ' Manejo de excepciones
    '        lblPrg.Text += String.Format("{0} {1}{2}", MethodBase.GetCurrentMethod.Name(), ex.Message, Environment.NewLine)
    '        Throw
    '    End Try

    'End Function
    Private Function Obtener_BaseEntry_Y_BaseLine_De_SAP(ByVal oOrderPurchase As Documents,
                                                         ByRef baseEntry As Integer,
                                                         ByRef baseLine As Integer) As Boolean

        Try

            ' Asumiendo que oOrderPurchase es el documento de solicitud de devolución
            If oOrderPurchase Is Nothing Then
                Throw New Exception("El documento de solicitud de devolución no está inicializado.")
            End If

            ' Recorrer las líneas del documento de solicitud de devolución
            For i As Integer = 0 To oOrderPurchase.Lines.Count - 1

                oOrderPurchase.Lines.SetCurrentLine(i)

                ' Obtener BaseEntry y BaseLine de la línea actual
                ' BaseEntry generalmente se refiere a DocEntry del documento base
                ' BaseLine se refiere al número de línea en el documento base
                baseEntry = oOrderPurchase.Lines.DocEntry
                baseLine = oOrderPurchase.Lines.BaseLine

                ' Salir del bucle una vez que se encuentran los valores (puedes ajustar esta lógica según tus necesidades)
                If baseEntry > 0 AndAlso baseLine >= 0 Then
                    Exit For
                End If

            Next

            Return True ' Si se encontraron los valores correctamente

        Catch ex As Exception
            Throw
        End Try

    End Function

    Private Function Procesar_Detalle_Ingreso_Devolucion(ByVal oReturnRequest As Documents,
                                                         ByVal pTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
                                                         ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                         ByRef lblPrg As RichTextBox) As Boolean

        'Dim clsTransaccion As New clsTransaccion
        Dim oResultado As Integer = 0
        Dim oReturn As Documents = Nothing
        Dim vRegistroEntregaPorLote As Boolean = False
        Dim NoLineaEntrega As Integer = 0 : Dim NoLineaEntregaLote As Integer = 0
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vNoLineaAnterior As Integer = -1
        Dim vCodigoBodegaERP As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion

        Procesar_Detalle_Ingreso_Devolucion = False

        Try

            clsTransaccion.Open_Connection()

            'Búsqueda de todos los estados de un código y línea sin importar el lote.
            Dim EstadosRecibidos = lINavTransaccionesOut.GroupBy(Function(x) x.Idproductoestado).
                                            Select(Function(g) New With {
                                                .Idproductoestado = g.Key,
                                                .Lotes = g.ToList(),
                                                .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                            }).ToList()

            For Each Estado In EstadosRecibidos

                Dim BeProductoEstado = clsLnProducto_estado.Get_Single_By_IdEstado(Estado.Idproductoestado,
                                                                                   clsTransaccion.lConnection,
                                                                                   clsTransaccion.lTransaction)

                If BeProductoEstado Is Nothing Then
                    Throw New Exception($"No está definido el estado del producto y no se puede inferir la bodega destino para IdProductoEstado: {Estado.Idproductoestado}")
                ElseIf String.IsNullOrWhiteSpace(BeProductoEstado.Codigo_Bodega_ERP) Then
                    Throw New Exception($"No está definido el código de bodega SAP para el producto, configure: Producto-Estado-Codigo_Bodega_ERP para el IdEstado: {Estado.Idproductoestado}")
                End If

                vCodigoBodegaERP = BeProductoEstado.Codigo_Bodega_ERP

                If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Devolucion Then
                    Configurar_Devolucion_Cliente(oReturn, oReturnRequest)
                End If

                If oReturn Is Nothing Then
                    Throw New Exception("No se pudo crear el objeto de entrega.")
                End If

                Dim vIdOrdenCompraEnc As Integer = lINavTransaccionesOut.FirstOrDefault.Idordencompra
                Dim vCadenaFacturas As String = clsLnTrans_oc_enc.Get_Documento_By_IdOrdenCompraEnc(vIdOrdenCompraEnc)
                Dim BeProductoPresentacion As New clsBeProducto_Presentacion


                oReturn.NumAtCard = vCadenaFacturas

                'Reset variables de control
                NoLineaEntrega = 0 : NoLineaEntregaLote = 0

                For j As Integer = 0 To oReturnRequest.Lines.Count - 1

                    oReturnRequest.Lines.SetCurrentLine(j)


                    Dim vCodigoProductoSAP As String = oReturnRequest.Lines.ItemCode.ToString()
                    Dim vNoLineaOCSAP As Integer = oReturnRequest.Lines.LineNum

                    Dim DistinctProductosLineas = lINavTransaccionesOut.
                    Where(Function(x) x.Idproductoestado = Estado.Idproductoestado _
                          AndAlso x.Codigo_producto = vCodigoProductoSAP _
                          AndAlso x.No_linea = vNoLineaOCSAP).
                    GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea, Key x.Idpresentacion}).
                    Select(Function(g) New With {
                        g.Key.Codigo_producto,
                        g.Key.No_linea,
                        g.Key.Idpresentacion,
                        .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                    }).ToList()

                    If DistinctProductosLineas.Any() Then

                        Dim nuevaLineaEntrega As Boolean = True
                        Dim vFactor As Double = 1

                        For Each ProductoIngreso In DistinctProductosLineas

                            nuevaLineaEntrega = (vCodigoAnterior <> ProductoIngreso.Codigo_producto OrElse vNoLineaAnterior <> ProductoIngreso.No_linea)

                            If nuevaLineaEntrega Then

                                Dim vTipoImpuesto As String = oReturnRequest.Lines.TaxCode

                                '#EJC240521: Convertir presentación a UMBAS.
                                If ProductoIngreso.Idpresentacion > 0 Then
                                    BeProductoPresentacion = clsLnProducto_presentacion.GetSingle(ProductoIngreso.Idpresentacion)
                                    If BeProductoPresentacion IsNot Nothing Then
                                        ProductoIngreso.Cantidad_Total = ProductoIngreso.Cantidad_Total * BeProductoPresentacion.Factor
                                        vFactor = BeProductoPresentacion.Factor
                                    End If
                                End If

                                oReturn.Lines.SetCurrentLine(NoLineaEntrega)
#Disable Warning BC42025 ' Acceso del miembro compartido, el miembro de constante, el miembro de enumeración o el tipo anidado a través de una instancia
                                oReturn.Lines.BaseType = BoObjectTypes.oOrders.oReturnRequest
#Enable Warning BC42025 ' Acceso del miembro compartido, el miembro de constante, el miembro de enumeración o el tipo anidado a través de una instancia
                                oReturn.Lines.BaseEntry = oReturnRequest.DocEntry
                                oReturn.Lines.BaseLine = vNoLineaOCSAP
                                oReturn.Lines.ItemCode = ProductoIngreso.Codigo_producto
                                oReturn.Lines.TaxCode = vTipoImpuesto
                                oReturn.Lines.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                                oReturn.Lines.Quantity = ProductoIngreso.Cantidad_Total
                                oReturn.Lines.WarehouseCode = BeProductoEstado.Codigo_Bodega_ERP

                                vCodigoAnterior = oReturn.Lines.ItemCode
                                vNoLineaAnterior = oReturn.Lines.LineNum

                                Dim vControlPorLote As Boolean = clsLnProducto.Get_Control_Lote_By_Codigo(ProductoIngreso.Codigo_producto,
                                                                                                          clsTransaccion.lConnection,
                                                                                                          clsTransaccion.lTransaction)
                                If vControlPorLote Then

                                    Dim LotesRecibidosPorEstado = lINavTransaccionesOut.
                                                    Where(Function(x) x.Codigo_producto = ProductoIngreso.Codigo_producto _
                                                          AndAlso x.No_linea = ProductoIngreso.No_linea _
                                                          AndAlso x.Idproductoestado = Estado.Idproductoestado).
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

                                                oReturn.Lines.BatchNumbers.SetCurrentLine(NoLineaEntregaLote)
                                                oReturn.Lines.BatchNumbers.BaseLineNumber = NoLineaEntrega
                                                oReturn.Lines.BatchNumbers.ItemCode = Lote.Codigo_producto
                                                oReturn.Lines.BatchNumbers.BatchNumber = Lote.Lote
                                                oReturn.Lines.BatchNumbers.Quantity = Lote.CantidadTotal * IIf(vFactor = 0, 1, vFactor)
                                                oReturn.Lines.BatchNumbers.ExpiryDate = Lote.Fecha_vence
                                                oReturn.Lines.BatchNumbers.InternalSerialNumber = Lote.Licencia
                                                oReturn.Lines.BatchNumbers.Location = BeProductoEstado.Codigo_Bodega_ERP
                                                oReturn.Lines.BatchNumbers.AddmisionDate = Now
                                                oReturn.Lines.BatchNumbers.Add()
                                                NoLineaEntregaLote += 1

                                                Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oReturnRequest.DocEntry _
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

                                        Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oReturnRequest.DocEntry _
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

                                oReturn.Lines.Add() : NoLineaEntrega += 1

                            End If

                        Next 'DistinctProductosLineas

                        vAgregarEntrega = True

                    End If

                Next

                If vAgregarEntrega Then

                    Dim nombreCampoRegion As String = "U_Region"
                    oReturn.UserFields.Fields.Item(nombreCampoRegion).Value = oReturnRequest.UserFields.Fields.Item(nombreCampoRegion).Value

                    Dim nombreCampoUGira As String = "U_Gira"
                    oReturn.UserFields.Fields.Item(nombreCampoUGira).Value = oReturnRequest.UserFields.Fields.Item(nombreCampoUGira).Value '"CADENAS"

                    Dim nombreCampoUDepto As String = "U_Depto"
                    Try
                        oReturn.UserFields.Fields.Item(nombreCampoUDepto).Value = oReturnRequest.UserFields.Fields.Item(nombreCampoUDepto).Value
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, vbTab & nombreCampoUDepto)
                    End Try

                    Dim U_TipoNotaCredito As String = "U_TiipoNotaCredito"
                    Try
                        oReturn.UserFields.Fields.Item(U_TipoNotaCredito).Value = oReturnRequest.UserFields.Fields.Item(U_TipoNotaCredito).Value
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, vbTab & U_TipoNotaCredito)
                    End Try

                    Dim U_Distribuidor As String = "U_Distribuidor"
                    Try
                        oReturn.UserFields.Fields.Item(U_Distribuidor).Value = oReturnRequest.UserFields.Fields.Item(U_Distribuidor).Value
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, vbTab & U_Distribuidor)
                    End Try

                    Dim U_FactNit As String = "U_FacNit"
                    Try
                        oReturn.UserFields.Fields.Item(U_FactNit).Value = oReturnRequest.UserFields.Fields.Item(U_FactNit).Value
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, vbTab & U_FactNit)
                    End Try

                    Dim U_NIT As String = "U_NIT"
                    Try
                        oReturn.UserFields.Fields.Item(U_NIT).Value = oReturnRequest.UserFields.Fields.Item(U_NIT).Value
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, vbTab & U_NIT)
                    End Try

                    Dim U_SegundaReferencia As String = "U_SEGUNDAREFERENCIA"
                    Try
                        oReturn.UserFields.Fields.Item(U_SegundaReferencia).Value = vIdOrdenCompraEnc.ToString()
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, vbTab & U_SegundaReferencia)
                    End Try

                    Dim U_Tipo_Documento As String = "U_TIPO_DOCUMENTO"
                    Try
                        oReturn.UserFields.Fields.Item(U_Tipo_Documento).Value = oReturnRequest.UserFields.Fields.Item(U_Tipo_Documento).Value
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, vbTab & U_Tipo_Documento)
                    End Try

                    oReturn.ManualNumber = 1989

                    oResultado = oReturn.Add()
                    If oResultado <> 0 Then
                        Dim errMsg = oCompany.GetLastErrorDescription()
                        Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                    Else

                        vRegistroEntregaPorLote = True

                        If Not Lista_A_Actualizar Is Nothing Then
                            If Lista_A_Actualizar.Count > 0 Then
                                For Each T In Lista_A_Actualizar
                                    T.Enviado = True
                                    T.Fec_mod = Now
                                    clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(T)
                                Next
                            End If
                        End If

                        Procesar_Detalle_Ingreso_Devolucion = True

                        clsPublic.Actualizar_Progreso(lblPrg, vbTab & "Se creó la entrega en SAP para la bodega: " & vCodigoBodegaERP)

                    End If

                End If

            Next 'EstadosRecibidos

        Catch ex As Exception
            Throw
        Finally
            If oReturn IsNot Nothing Then
                Marshal.ReleaseComObject(oReturn) ' Liberar el objeto de entrega si se creó
            End If
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Function Get_Inventario_Teorico_From_SAP() As List(Of clsBeVWInventarioTeoricoSAP)

        Dim BeProductoInvTeoricoSAP As New clsBeVWInventarioTeoricoSAP
        Dim lProductosInventarioTeorico As New List(Of clsBeVWInventarioTeoricoSAP)

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                Dim vSQLOC As String

                vSQLOC = " SELECT * FROM VW_STOCK_POR_LOTE  WHERE Cantidad_Lote >0 "

                RsEnc.DoQuery(vSQLOC)

                While RsEnc.EoF = False

                    BeProductoInvTeoricoSAP = New clsBeVWInventarioTeoricoSAP

                    BeProductoInvTeoricoSAP.Codigo = RsEnc.Fields.Item("Codigo").Value
                    BeProductoInvTeoricoSAP.Nombre = RsEnc.Fields.Item("Nombre").Value
                    BeProductoInvTeoricoSAP.Codigo_Bodega = RsEnc.Fields.Item("Codigo_Bodega").Value
                    BeProductoInvTeoricoSAP.Nombre_Bodega = RsEnc.Fields.Item("Nombre_Bodega").Value
                    BeProductoInvTeoricoSAP.Total_Almacen = RsEnc.Fields.Item("Total_Almacen").Value
                    BeProductoInvTeoricoSAP.Lote = RsEnc.Fields.Item("Lote").Value
                    BeProductoInvTeoricoSAP.Cantidad_Lote = RsEnc.Fields.Item("Cantidad_Lote").Value
                    BeProductoInvTeoricoSAP.Fecha_Vence = RsEnc.Fields.Item("Fecha_Vence").Value
                    lProductosInventarioTeorico.Add(BeProductoInvTeoricoSAP)

                    RsEnc.MoveNext()

                End While

            End If

            Return lProductosInventarioTeorico

        Catch ex As Exception
            Throw ex
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Private Function Inserta_Enc_OC_Inv_Ini(ByRef CodigoBodega As String,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction,
                                            ByVal lblPrg As RichTextBox) As clsBeTrans_oc_enc

        Dim BeOrdenCompraEnc As New clsBeTrans_oc_enc
        Dim BeProveedorBodega As New clsBeProveedor_bodega

        Inserta_Enc_OC_Inv_Ini = Nothing

        Try

            BeOrdenCompraEnc.IdOrdenCompraEnc = clsLnTrans_oc_enc.MaxID(lConnection, lTransaction) + 1
            BeOrdenCompraEnc.PropietarioBodega = New clsBePropietario_bodega
            BeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega,
                                                                                                                                             BeConfigEnc.IdPropietario,
                                                                                                                                             lConnection,
                                                                                                                                             lTransaction)
            If BeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega = 0 Then
                Throw New Exception("No se pudo obtener el IdPropietarioBodega")
            Else
                BeOrdenCompraEnc.IdPropietarioBodega = BeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega
            End If

            BeOrdenCompraEnc.IdEstadoOC = clsDataContractDI.tEstadoOC.NUEVA
            BeOrdenCompraEnc.Hora_Creacion = Now
            BeOrdenCompraEnc.User_Agr = BeConfigEnc.IdUsuario
            BeOrdenCompraEnc.Fec_Agr = Now
            BeOrdenCompraEnc.Fecha_Creacion = Now
            BeOrdenCompraEnc.Activo = True
            BeOrdenCompraEnc.IdBodega = BeConfigEnc.Idbodega

            BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor_Defecto_Inv_Inicial(lConnection, lTransaction, BeConfigEnc)

            If BeProveedorBodega Is Nothing Then
                Throw New Exception("No logramos insertar el proveedor para el inventario inicial.")
            End If

            If BeOrdenCompraEnc.ProveedorBodega Is Nothing Then
                BeOrdenCompraEnc.ProveedorBodega = New clsBeProveedor_bodega
            End If

            BeOrdenCompraEnc.IdProveedorBodega = BeProveedorBodega.IdAsignacion
            BeOrdenCompraEnc.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Inventario_Inicial
            BeOrdenCompraEnc.No_Documento = FormatoFechas.tFechaHora(Now)
            BeOrdenCompraEnc.User_Agr = BeConfigEnc.IdUsuario
            BeOrdenCompraEnc.User_Mod = BeConfigEnc.IdUsuario
            BeOrdenCompraEnc.Fec_Mod = Now
            BeOrdenCompraEnc.Fec_Agr = Now
            BeOrdenCompraEnc.Procedencia = "Interface TOMWMS SAP Bodega: " & CodigoBodega
            BeOrdenCompraEnc.No_Marchamo = ""
            BeOrdenCompraEnc.Referencia = "INVINI_BOD" & CodigoBodega
            BeOrdenCompraEnc.Observacion = "Documento de ingreso Inv. Teórico SAP, Bodega: " & CodigoBodega
            BeOrdenCompraEnc.Control_Poliza = False

            If BeOrdenCompraEnc.IsNew Then
                BeOrdenCompraEnc.ObjPoliza = Nothing
            End If

            BeOrdenCompraEnc.Enviado_A_ERP = False

            Try

                clsLnTrans_oc_enc.Insertar(BeOrdenCompraEnc, lConnection, lTransaction)

                Inserta_Enc_OC_Inv_Ini = BeOrdenCompraEnc

            Catch ex As Exception

                Dim vMsgEx4 As String = String.Format(vbNewLine & "Error al insertar el documento de ingreso: {0}{1}", vbNewLine, ex.Message)
                clsLnLog_error_wms.Agregar_Error(ex.Message)
                clsPublic.Actualizar_Progreso(lblPrg, vMsgEx4)
                Throw New Exception(vMsgEx4)

            End Try

            Application.DoEvents()

        Catch ex As Exception
            Throw
        End Try

    End Function

    ''' <summary>
    ''' #EJC202404030025AM: Creación de documentos de ingreso en base a inventario teórico de SAP por bodega configurada en bodega_area de WMS.
    ''' </summary>
    ''' <param name="lblprg"></param>
    ''' <returns></returns>
    Public Function Crear_Pedido_Compra_Desde_Inventario_Teorico_By_Bodega(ByVal lblprg As RichTextBox) As Boolean

        Crear_Pedido_Compra_Desde_Inventario_Teorico_By_Bodega = False

        Dim lProductosInventarioTeorico As New List(Of clsBeVWInventarioTeoricoSAP)
        Dim BeProductoWMS As New clsBeProducto
        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vContadorLIneasDetInsertadas As Integer = 0
        Dim vContadorLotesInsertados As Integer = 0
        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
        Dim vContadorLineasDet As Integer = 0
        Dim BeProductoBodega As New clsBeProducto_bodega
        Dim BeUnidadMedidaPedCompra As New clsBeUnidad_medida
        Dim IdBodegaDestino As Integer = BeConfigEnc.Idbodega
        Dim vNoLinea As Integer = 0
        Dim BeOcDetLote As New clsBeTrans_oc_det_lote
        Dim lAreasComoBodegasWMS As New List(Of clsBeBodega_area)
        Dim vContadorDocumentosIngreso As Integer = 0

        Try

            lProductosInventarioTeorico = Get_Inventario_Teorico_From_SAP()

            If Not lProductosInventarioTeorico Is Nothing Then

                If lProductosInventarioTeorico.Count > 0 Then

                    lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                      lConnection,
                                                                      lTransaction)


                    If Not BeConfigEnc Is Nothing Then

                        lAreasComoBodegasWMS = clsLnBodega_area.Get_All_By_IdBodega_For_SAP(BeConfigEnc.Idbodega,
                                                                                            lConnection,
                                                                                            lTransaction)


                        If lAreasComoBodegasWMS IsNot Nothing AndAlso lAreasComoBodegasWMS.Any() Then

                            Dim codigosBodegaWMS = lAreasComoBodegasWMS.Select(Function(a) a.Codigo).ToList()

                            Dim productosAgrupados = (From producto In lProductosInventarioTeorico
                                                      Where codigosBodegaWMS.Contains(producto.Codigo_Bodega)
                                                      Group By producto.Codigo, producto.Nombre, producto.Codigo_Bodega Into Grupo = Group
                                                      Select New With {
                                                          .Codigo = Codigo,
                                                          .Nombre = Nombre,
                                                          .Codigo_Bodega = Codigo_Bodega,
                                                          .CantidadTotal = Grupo.Sum(Function(p) p.Cantidad_Lote)
                                                      }).ToList()

                            If productosAgrupados IsNot Nothing AndAlso productosAgrupados.Any() Then

                                Dim codigosBodegaUnicos = productosAgrupados.Select(Function(p) p.Codigo_Bodega).Distinct()

                                If codigosBodegaUnicos IsNot Nothing AndAlso codigosBodegaUnicos.Any() Then

                                    For Each CodigoBodega In codigosBodegaUnicos

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Creando documento de ingreso para bodega: {0}", CodigoBodega))

                                        BePedidoCompraEnc = New clsBeTrans_oc_enc()
                                        BePedidoCompraEnc = Inserta_Enc_OC_Inv_Ini(CodigoBodega, lConnection, lTransaction, lblprg)

                                        Dim productosEnBodega = productosAgrupados.Where(Function(p) p.Codigo_Bodega = CodigoBodega).ToList()

                                        If productosEnBodega IsNot Nothing AndAlso productosEnBodega.Any() Then

                                            '#ECJ202404 Ciclo para crear línea en el documento de ingreso en unidades
                                            For Each ProductoInvTeoricoSAPAgrupado In productosEnBodega

                                                vContadorLineasDet += 1

                                                Dim BePedidoCompraDet As New clsBeTrans_oc_det()
                                                BePedidoCompraDet.IdOrdenCompraEnc = BePedidoCompraEnc.IdOrdenCompraEnc
                                                BePedidoCompraDet.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(BePedidoCompraEnc.IdOrdenCompraEnc,
                                                                                                             lConnection,
                                                                                                             lTransaction) + 1

                                                BeProductoBodega = New clsBeProducto_bodega
                                                BeProductoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(ProductoInvTeoricoSAPAgrupado.Codigo,
                                                                                                                  BeConfigEnc.Idbodega,
                                                                                                                  lConnection,
                                                                                                                  lTransaction)


                                                '#EJC20190327l: Se cambio a busqueda por codigo
                                                BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario("UN",
                                                                                                                                BeConfigEnc.IdPropietario,
                                                                                                                                lConnection,
                                                                                                                                lTransaction)

                                                If BeUnidadMedidaPedCompra Is Nothing Then

                                                    '#EJC20190327l: Se cambio a busqueda por codigo
                                                    BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario("UND",
                                                                                                                                    BeConfigEnc.IdPropietario,
                                                                                                                                    lConnection,
                                                                                                                                    lTransaction)

                                                    If BeUnidadMedidaPedCompra Is Nothing Then
                                                        Dim vMsgEx1 As String = String.Format("La unidad de medida: {1} no está definida para el código de producto:{0} en la tabla unidad_medida. {2}", ProductoInvTeoricoSAPAgrupado.Codigo, "UN", vbNewLine)
                                                        clsPublic.Actualizar_Progreso(lblprg, vMsgEx1)
                                                        Throw New Exception(vMsgEx1)
                                                    End If

                                                End If

                                                If BeProductoBodega Is Nothing Then

                                                    Dim vIdProducto As Integer = clsLnProducto.Get_IdProductoBodega_By_Codigo(ProductoInvTeoricoSAPAgrupado.Codigo, lConnection, lTransaction)

                                                    BeProductoBodega = New clsBeProducto_bodega
                                                    BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction)
                                                    BeProductoBodega.IdProducto = vIdProducto
                                                    BeProductoBodega.IdBodega = BeConfigEnc.Idbodega
                                                    BeProductoBodega.User_agr = "MI3"
                                                    BeProductoBodega.Fec_agr = Now
                                                    BeProductoBodega.User_agr = "MI3"
                                                    BeProductoBodega.Fec_mod = Now

                                                    clsLnProducto_bodega.Actualizar(BeProductoBodega, lConnection, lTransaction)

                                                    clsPublic.Actualizar_Progreso(lblprg, "El producto fue asociado a la bodega: " & BeConfigEnc.Idbodega & " de forma automática")

                                                End If

                                                If BeProductoBodega IsNot Nothing Then

                                                    Try

                                                        BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                        BePedidoCompraDet.Codigo_Producto = ProductoInvTeoricoSAPAgrupado.Codigo
                                                        BePedidoCompraDet.Nombre_producto = ProductoInvTeoricoSAPAgrupado.Nombre
                                                        BePedidoCompraDet.Nombre_unidad_medida_basica = BeUnidadMedidaPedCompra.IdUnidadMedida
                                                        BePedidoCompraDet.Cantidad = ProductoInvTeoricoSAPAgrupado.CantidadTotal
                                                        BePedidoCompraDet.Cantidad_recibida = 0
                                                        BePedidoCompraDet.Costo = 0.1
                                                        BePedidoCompraDet.Total_linea = ProductoInvTeoricoSAPAgrupado.CantidadTotal * 0.1
                                                        BePedidoCompraDet.No_Linea = vContadorLineasDet
                                                        BePedidoCompraDet.Activo = True
                                                        BePedidoCompraDet.Porcentaje_arancel = 0
                                                        BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
                                                        BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
                                                        BePedidoCompraDet.Atributo_variante_1 = Nothing
                                                        BePedidoCompraDet.IdPresentacion = 0
                                                        BePedidoCompraDet.UnidadMedida = BeUnidadMedidaPedCompra

                                                        clsLnTrans_oc_det.Insertar(BePedidoCompraDet, lConnection, lTransaction)

                                                        Dim detallesLoteVencimiento = (From producto In lProductosInventarioTeorico
                                                                                       Group By producto.Codigo, producto.Lote, producto.Fecha_Vence, producto.Codigo_Bodega Into Grupo = Group
                                                                                       Select New With {
                                                                                                      .Codigo = Codigo,
                                                                                                      .Lote = Lote,
                                                                                                      .Fecha_Vence = Fecha_Vence,
                                                                                                      .Codigo_Bodega = Codigo_Bodega,
                                                                                                      .Cantidad_Lote = Grupo.Sum(Function(p) p.Cantidad_Lote)
                                                                                                  }).Where(Function(x) x.Codigo = ProductoInvTeoricoSAPAgrupado.Codigo _
                                                                                                  AndAlso x.Codigo_Bodega = CodigoBodega).ToList()

                                                        If detallesLoteVencimiento IsNot Nothing AndAlso detallesLoteVencimiento.Any() Then

                                                            For Each DetalleLote In detallesLoteVencimiento

                                                                BeOcDetLote = New clsBeTrans_oc_det_lote
                                                                BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                                BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                                BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(lConnection, lTransaction) + 1
                                                                BeOcDetLote.Cantidad = DetalleLote.Cantidad_Lote
                                                                BeOcDetLote.No_linea = vContadorLineasDet
                                                                BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                                BeOcDetLote.Lote = DetalleLote.Lote
                                                                BeOcDetLote.Cantidad_recibida = 0
                                                                BeOcDetLote.Codigo_producto = ProductoInvTeoricoSAPAgrupado.Codigo
                                                                BeOcDetLote.Fecha_vence = DetalleLote.Fecha_Vence
                                                                BeOcDetLote.UnidadMedida = BePedidoCompraDet.UnidadMedida
                                                                BeOcDetLote.IdUnidadMedidaBasica = BePedidoCompraDet.UnidadMedida.IdUnidadMedida
                                                                clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, lConnection, lTransaction)

                                                                vContadorLotesInsertados += 1

                                                            Next

                                                        Else
                                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("No se encontraron detalles de lote y vencimiento para el producto con código: {0}", ProductoInvTeoricoSAPAgrupado.Codigo))
                                                        End If

                                                        vContadorLIneasDetInsertadas += 1

                                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Insertando producto: {0} - {1}, Bodega: {2}", ProductoInvTeoricoSAPAgrupado.Codigo, ProductoInvTeoricoSAPAgrupado.Nombre, ProductoInvTeoricoSAPAgrupado.Codigo_Bodega))

                                                    Catch ex As Exception

                                                        Dim vMsgEx3 As String = String.Format("Error al insertar desde ws a intermedia: {0}{1}{2}", BePedidoCompraDet.Nombre_producto, ex.Message, vbNewLine)
                                                        clsLnLog_error_wms.Agregar_Error(vMsgEx3)
                                                        clsPublic.Actualizar_Progreso(lblprg, vMsgEx3)
                                                        Throw New Exception(vMsgEx3)

                                                    End Try

                                                Else

                                                    Dim vMensajeError20240402 As String = String.Format("Error_20240402: El código de producto:{0} no existe o no está asociado a la bodega:{1}{2}", ProductoInvTeoricoSAPAgrupado.Codigo, IdBodegaDestino, vbNewLine)
                                                    clsLnLog_error_wms.Agregar_Error(vMensajeError20240402)
                                                    clsPublic.Actualizar_Progreso(lblprg, vMensajeError20240402)

                                                End If

                                            Next

                                            Dim BePresentacionDefault As New clsBeProducto_Presentacion

                                            '#CKFK20240414 Ciclo para crear línea en el documento de ingreso por presentacion
                                            For Each ProductoInvTeoricoSAPAgrupado In productosEnBodega

                                                Dim BePedidoCompraDet As New clsBeTrans_oc_det()
                                                BePedidoCompraDet.IdOrdenCompraEnc = BePedidoCompraEnc.IdOrdenCompraEnc
                                                BePedidoCompraDet.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(BePedidoCompraEnc.IdOrdenCompraEnc,
                                                                                                             lConnection,
                                                                                                             lTransaction) + 1

                                                BeProductoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(ProductoInvTeoricoSAPAgrupado.Codigo,
                                                                                                                  BeConfigEnc.Idbodega,
                                                                                                                  lConnection,
                                                                                                                  lTransaction)


                                                '#EJC20190327l: Se cambio a busqueda por codigo
                                                BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario("UN",
                                                                                                                                BeConfigEnc.IdPropietario,
                                                                                                                                lConnection,
                                                                                                                                lTransaction)

                                                If BeUnidadMedidaPedCompra Is Nothing Then

                                                    '#EJC20190327l: Se cambio a busqueda por codigo
                                                    BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario("UND",
                                                                                                                                    BeConfigEnc.IdPropietario,
                                                                                                                                    lConnection,
                                                                                                                                    lTransaction)


                                                    If BeUnidadMedidaPedCompra Is Nothing Then
                                                        Dim vMsgEx1 As String = String.Format("La unidad de medida: {1} no está definida para el código de producto:{0} en la tabla unidad_medida. {2}", ProductoInvTeoricoSAPAgrupado.Codigo, "UN", vbNewLine)
                                                        clsPublic.Actualizar_Progreso(lblprg, vMsgEx1)
                                                        Throw New Exception(vMsgEx1)
                                                    End If

                                                End If

                                                If BeProductoBodega IsNot Nothing Then

                                                    Try

                                                        BePresentacionDefault = New clsBeProducto_Presentacion
                                                        BePresentacionDefault = clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(BeProductoBodega.IdProducto)

                                                        If BePresentacionDefault IsNot Nothing Then

                                                            If BePresentacionDefault.Factor > 0 AndAlso ProductoInvTeoricoSAPAgrupado.CantidadTotal >= BePresentacionDefault.Factor Then

                                                                vContadorLineasDet += 1

                                                                BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                                BePedidoCompraDet.Codigo_Producto = ProductoInvTeoricoSAPAgrupado.Codigo
                                                                BePedidoCompraDet.Nombre_producto = ProductoInvTeoricoSAPAgrupado.Nombre
                                                                BePedidoCompraDet.Nombre_unidad_medida_basica = BeUnidadMedidaPedCompra.IdUnidadMedida

                                                                Dim cantPres As Integer = Math.Truncate(ProductoInvTeoricoSAPAgrupado.CantidadTotal / BePresentacionDefault.Factor)

                                                                BePedidoCompraDet.Cantidad = cantPres
                                                                BePedidoCompraDet.Cantidad_recibida = 0
                                                                BePedidoCompraDet.Costo = 0.1
                                                                BePedidoCompraDet.Total_linea = cantPres * BePresentacionDefault.Factor * 0.1
                                                                BePedidoCompraDet.No_Linea = vContadorLineasDet
                                                                BePedidoCompraDet.Activo = True
                                                                BePedidoCompraDet.Porcentaje_arancel = 0
                                                                BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
                                                                BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
                                                                BePedidoCompraDet.Atributo_variante_1 = Nothing
                                                                BePedidoCompraDet.IdPresentacion = BePresentacionDefault.IdPresentacion
                                                                BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacionDefault.IdPresentacion
                                                                BePedidoCompraDet.UnidadMedida = BeUnidadMedidaPedCompra

                                                                clsLnTrans_oc_det.Insertar(BePedidoCompraDet, lConnection, lTransaction)

                                                                Dim detallesLoteVencimiento = (From producto In lProductosInventarioTeorico
                                                                                               Group By producto.Codigo, producto.Lote, producto.Fecha_Vence, producto.Codigo_Bodega Into Grupo = Group
                                                                                               Select New With {
                                                                                                          .Codigo = Codigo,
                                                                                                          .Lote = Lote,
                                                                                                          .Fecha_Vence = Fecha_Vence,
                                                                                                          .Codigo_Bodega = Codigo_Bodega,
                                                                                                          .Cantidad_Lote = Grupo.Sum(Function(p) p.Cantidad_Lote)
                                                                                                      }).Where(Function(x) x.Codigo = ProductoInvTeoricoSAPAgrupado.Codigo _
                                                                                                      AndAlso x.Codigo_Bodega = CodigoBodega).ToList()

                                                                If detallesLoteVencimiento IsNot Nothing AndAlso detallesLoteVencimiento.Any() Then

                                                                    For Each DetalleLote In detallesLoteVencimiento.FindAll(Function(x) x.Cantidad_Lote >= BePresentacionDefault.Factor)

                                                                        BeOcDetLote = New clsBeTrans_oc_det_lote
                                                                        BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                                        BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                                        BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(lConnection, lTransaction) + 1

                                                                        Dim cantPresLote As Integer = Math.Truncate(DetalleLote.Cantidad_Lote / BePresentacionDefault.Factor)

                                                                        BeOcDetLote.Cantidad = cantPresLote
                                                                        BeOcDetLote.No_linea = vContadorLineasDet
                                                                        BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                                        BeOcDetLote.Lote = DetalleLote.Lote
                                                                        BeOcDetLote.Cantidad_recibida = 0
                                                                        BeOcDetLote.Codigo_producto = ProductoInvTeoricoSAPAgrupado.Codigo
                                                                        BeOcDetLote.Fecha_vence = DetalleLote.Fecha_Vence
                                                                        BeOcDetLote.UnidadMedida = BePedidoCompraDet.UnidadMedida
                                                                        BeOcDetLote.IdUnidadMedidaBasica = BePedidoCompraDet.UnidadMedida.IdUnidadMedida
                                                                        BeOcDetLote.IdPresentacion = BePresentacionDefault.IdPresentacion
                                                                        BeOcDetLote.Presentacion.IdPresentacion = BePresentacionDefault.IdPresentacion

                                                                        clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, lConnection, lTransaction)

                                                                        vContadorLotesInsertados += 1

                                                                    Next

                                                                Else
                                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("No se encontraron detalles de lote y vencimiento para el producto con código: {0}", ProductoInvTeoricoSAPAgrupado.Codigo))
                                                                End If

                                                                vContadorLIneasDetInsertadas += 1

                                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Insertando producto: {0} - {1}, Bodega: {2}", ProductoInvTeoricoSAPAgrupado.Codigo, ProductoInvTeoricoSAPAgrupado.Nombre, ProductoInvTeoricoSAPAgrupado.Codigo_Bodega))

                                                            End If

                                                        End If

                                                    Catch ex As Exception

                                                        Dim vMsgEx3 As String = String.Format("Error al insertar desde ws a intermedia: {0}{1}{2}", BePedidoCompraDet.Nombre_producto, ex.Message, vbNewLine)
                                                        clsLnLog_error_wms.Agregar_Error(vMsgEx3)
                                                        clsPublic.Actualizar_Progreso(lblprg, vMsgEx3)
                                                        Throw New Exception(vMsgEx3)

                                                    End Try

                                                Else

                                                    Dim vMensajeError20240402 As String = String.Format("Error_20240402: El código de producto:{0} no existe o no está asociado a la bodega:{1}{2}", ProductoInvTeoricoSAPAgrupado.Codigo, IdBodegaDestino, vbNewLine)
                                                    clsLnLog_error_wms.Agregar_Error(vMensajeError20240402)
                                                    clsPublic.Actualizar_Progreso(lblprg, vMensajeError20240402)

                                                End If

                                            Next

                                        Else
                                            Console.WriteLine(String.Format("No se encontraron productos para la bodega con código: {0}", CodigoBodega))
                                        End If

                                        '#EJC202404030044: Crear tarea de recepción auotmática por PC con IdUbicacion de recepción por defecto definida por área de WMS.
                                        Dim OutBeRecepcionEnc As New clsBeTrans_re_enc
                                        clsLnTrans_re_enc.Generar_Tarea_Recepcion_By_OrdenCompraEnc_For_SAP(BePedidoCompraEnc,
                                                                                                            BeConfigEnc,
                                                                                                            OutBeRecepcionEnc,
                                                                                                            CodigoBodega,
                                                                                                            lConnection,
                                                                                                            lTransaction)

                                        vContadorDocumentosIngreso += 1

                                    Next

                                End If

                            Else
                                clsPublic.Actualizar_Progreso(lblprg, "La lista de productos agrupados está vacía o es Nothing.")
                            End If

                        Else
                            clsPublic.Actualizar_Progreso(lblprg, "No están definidas las áreas como bodegas de SAP en WMS.")
                        End If

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "Error_20240402: No está definida la configuración de interface.")
                    End If

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "Error 20240402A: La lista de inventario teórico no tiene objetos.")
                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, "Error 20240402: La lista de inventario está vacía.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Se insertaron {0} documentos, {1} productos y {2} lotes.", vContadorDocumentosIngreso, vContadorLIneasDetInsertadas, vContadorLotesInsertados))

            lTransaction.Commit()

            Crear_Pedido_Compra_Desde_Inventario_Teorico_By_Bodega = True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class