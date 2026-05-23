Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports SAPbobsCOM

Public Class clsSyncSAPEntradaMercancia : Inherits clsInterfaceBase
    Implements IDisposable

    Private VContadorBitacoraTOMWMS As Integer = 0
    Private VContadorBitacoraIntermedia As Integer = 0

    Public Sub Dispose() Implements IDisposable.Dispose

    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Importar_Entrada_Mercancia_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                        ByRef prg As System.Windows.Forms.ProgressBar,
                                                                        ByRef cnnLog As SqlConnection) As Boolean

        Importar_Entrada_Mercancia_Desde_SAP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResult As String = ""

        Try

            Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia.")

            Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)

            lPedidosCompra = Get_Entrada_Mercancia_From_SAP()

            BeNavEjecucionRes.Registros_ws = lPedidosCompra.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeProductoBodega As New clsBeProducto_bodega

            Actualizar_Progreso(lblprg, String.Format("Entradas en SAP (OIGN): {0} ", lPedidosCompra.Count))

            prg.Maximum = lPedidosCompra.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            'Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each PC In lPedidosCompra

                    Actualizar_Progreso(lblprg, String.Format("Procesando Entrada de Mercancía: {0} ", PC.No, vbNewLine))

                    If Not clsLnProveedor.Existe_Proveedor(PC.Buy_From_Vendor_No) Then

                        If Inserta_Proveedor_Desde_SAP(PC.Buy_From_Vendor_No, cnnLog) Then
                            Actualizar_Progreso(lblprg, "El proveedor: " & PC.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                        End If

                    End If

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(PC, BePedidoCompraEnc, vResult) Then
                        Marcar_PI_Sincronizado_SAP(PC.No)
                    End If

                    Actualizar_Progreso(lblprg, vResult)

                Next

            End If

            lTransaction.Commit()

            Actualizar_Progreso(lblprg, "Fin de insercción en tabla intermedia.")

            Importar_Entrada_Mercancia_Desde_SAP_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Actualizar_Progreso(lblprg, String.Format("Error al insertar Ordenes de Compra desde SAP a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function

    Private Function Inserta_Proveedor_Desde_SAP(ByVal pCodigo As String,
                                                 ByVal cnnLog As SqlConnection) As Boolean

        Inserta_Proveedor_Desde_SAP = False


        Dim BeProveedor As New clsBeProveedor
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim BeINavProveedor As New clsBeI_nav_proveedor

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            BeINavProveedor = clsSyncSAPProveedor.Get_Proveedor_SAP(pCodigo)

            If BeINavProveedor Is Nothing AndAlso pCodigo = "0000" Then
                BeINavProveedor = New clsBeI_nav_proveedor()
                BeINavProveedor.No = "0000"
                BeINavProveedor.VAT_Registratrion_No = "0000"
                BeINavProveedor.Search_Name = "PROVEEDOR GENERICO MI3-WMS"
                BeINavProveedor.Phone_No = "0000"
                BeINavProveedor.Name = "PROVEEDOR GENERICO MI3-WMS"
                BeINavProveedor.Location_Code = "01"
                BeINavProveedor.Adress = "0000"
                BeINavProveedor.City = "GT"
                BeINavProveedor.Contact = "Support"
            End If

            If Not BeINavProveedor Is Nothing Then

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
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

                    clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    VContadorBitacoraTOMWMS += 1

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.Insertar(BeProveedorBodega, lConnection, lTransaction)

                    'Marcar_Enviado_SAP
                    clsSyncSAPProveedor.Marcar_Proveedor_Sincronizado_SAP(BeProveedor.Codigo)

                    Inserta_Proveedor_Desde_SAP = True

                    lTransaction.Commit()

                Catch ex As Exception

                    If Not lTransaction Is Nothing Then lTransaction.Rollback()

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, cnnLog)

                    Throw ex

                Finally
                    If lConnection.State = ConnectionState.Open Then lConnection.Close()
                End Try

            End If

        Catch ex As Exception
            Throw ex
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

                            Actualizar_Progreso(lblprg, String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))

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

    Public Function Insertar_Entrada_Mercancia_Desde_SAP(ByRef lblprg As RichTextBox,
                                                        ByRef prg As System.Windows.Forms.ProgressBar,
                                                        Optional ByVal ForzarEjecucion As Boolean = False,
                                                        Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Entrada_Mercancia_Desde_SAP = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim DifCant As Double = 0

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Entrada Mercancía") Then
                    Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
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

            BeNavEjecRes = BeNavEjecucionRes

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            lblprg.Text = ""

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Entrada_Mercancia_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface entradas de mercancía.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Entrada_Mercancia_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lPedidoCompraEnc As New List(Of clsBeI_nav_ped_compra_enc)

            Actualizar_Progreso(lblprg, "Consultando entradas de mercancía en tabla intermedia ")

            lPedidoCompraEnc = clsLnI_nav_ped_compra_enc.GetAll(CnnInterface, lTransInterface, lblprg, prg)

            Actualizar_Progreso(lblprg, String.Format("Entradas de mercancía en tabla intermedia: {0}", lPedidoCompraEnc.Count))

            If lPedidoCompraEnc.Count > 0 Then

                Dim gBeOrdenCompra As clsBeTrans_oc_enc = Nothing
                Dim PedidoCompraExistente As clsBeTrans_oc_enc = Nothing
                Dim vContador As Integer = 0
                Dim vContadorLineasDet As Integer = 0
                Dim BeProveedorBodega As New clsBeProveedor_bodega
                Dim BeProductoBodega As New clsBeProducto_bodega
                Dim BePresentacion As New clsBeProducto_Presentacion

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                              CnnInterface,
                                                              lTransInterface)

                prg.Maximum = lPedidoCompraEnc.Count

                prg.Value = 0

                VContadorBitacoraTOMWMS = 0

                Actualizar_Progreso(lblprg, "Trasladando documento a TOMWMS.")

                Dim BeUnidadMedidaPedCompra As New clsBeUnidad_medida

                For Each navPedidoCompraEnc As clsBeI_nav_ped_compra_enc In lPedidoCompraEnc

                    If navPedidoCompraEnc.Status <> 0 Then

                        Actualizar_Progreso(lblprg, String.Format("Procesando E.M.: {0} ", navPedidoCompraEnc.No, vbNewLine))

                        gBeOrdenCompra = New clsBeTrans_oc_enc() With {.Referencia = navPedidoCompraEnc.No,
                                                                       .IdTipoIngresoOC = navPedidoCompraEnc.Document_Type}

                        PedidoCompraExistente = clsLnTrans_oc_enc.Get_Single_By_Referencia(gBeOrdenCompra, CnnInterface, lTransInterface)

                        prg.Value = vContador

                        vContador += 1
                        vContadorLineasDet = 0

                        'El pedido de compra existe y debe ser actualizado.
                        If Not PedidoCompraExistente Is Nothing Then

                            gBeOrdenCompra.Activo = True

                            BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                                   BeConfigEnc.IdBodega,
                                                                                                   CnnInterface,
                                                                                                   lTransInterface)
                            If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                                gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega
                            End If

                            gBeOrdenCompra.IdProveedorBodega = BeProveedorBodega.IdAsignacion
                            gBeOrdenCompra.IdTipoIngresoOC = 1 'P.C. REC NAV
                            gBeOrdenCompra.No_Documento = navPedidoCompraEnc.Vendor_Invoice_No
                            gBeOrdenCompra.User_Mod = BeConfigEnc.IdUsuario
                            gBeOrdenCompra.Fec_Mod = Now
                            gBeOrdenCompra.Procedencia = ""
                            gBeOrdenCompra.No_Marchamo = ""
                            gBeOrdenCompra.Referencia = navPedidoCompraEnc.No
                            gBeOrdenCompra.Observacion = navPedidoCompraEnc.Posting_Description
                            gBeOrdenCompra.Control_Poliza = False

                            If gBeOrdenCompra.IsNew Then
                                gBeOrdenCompra.ObjPoliza = Nothing
                            End If

                            clsLnTrans_oc_enc.Actualizar(gBeOrdenCompra, CnnInterface, lTransInterface)

                            Actualizar_Progreso(lblprg, String.Format("Procesando# : {0}{1}", navPedidoCompraEnc.No, vbNewLine))

                            VContadorBitacoraTOMWMS += 1

                            If navPedidoCompraEnc.Lineas_Detalle.Count > 0 Then

                                Dim BePedidoCompraDet As New clsBeTrans_oc_det

                                For Each navPedidoCompraDet As clsBeI_nav_ped_compra_det In navPedidoCompraEnc.Lineas_Detalle

                                    vContadorLineasDet += 1

                                    Try

                                        BeProductoBodega = clsLnProducto_bodega.Existe(navPedidoCompraDet.No, BeConfigEnc.Idbodega, CnnInterface, lTransInterface)

                                        'Existe el producto en el maestro?
                                        If BeProductoBodega IsNot Nothing Then

                                            'Existe el producto en el detalle de la orden de compra en la tabla de TOMWMS?
                                            BePedidoCompraDet = clsLnTrans_oc_det.Exist(PedidoCompraExistente.IdOrdenCompraEnc,
                                                                                        navPedidoCompraDet.Line_No,
                                                                                        CnnInterface,
                                                                                        lTransInterface)

                                            '#CKFK 20180725 17:45 coloqué esto en comentario, porque la instancia BeUnidadMedidaPedCompra era nothing y no se le podía asignar valor a la property Nombre
                                            'BeUnidadMedidaPedCompra.Nombre = navPedidoCompraDet.Unit_of_Measure_Code
                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Nombre(navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                                CnnInterface,
                                                                                                lTransInterface)

                                            'La unidad de medida existe?
                                            If BeUnidadMedidaPedCompra Is Nothing Then
                                                'unidad de medida no existe en tabla UNIDAD_MEDIDA
                                                Throw New Exception(String.Format("Producto: {0} UnidMedBas {1} No existe ",
                                                                      navPedidoCompraDet.No,
                                                                      BeProductoBodega.Producto.UnidadMedida.Nombre))
                                            End If 'Fin sí: unidad de medida no existe.

#Region "Cod_Variante_A_Presentacion"
                                            If navPedidoCompraDet.Variant_Code <> "" Then

                                                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                                                                                                        navPedidoCompraDet.Variant_Code,
                                                                                                                                        CnnInterface,
                                                                                                                                        lTransInterface)
                                                If BePresentacion Is Nothing Then
                                                    Throw New Exception("ERROR_202303031404H: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)
                                                End If 'Fin sí: BePresentacion IsNothing (Presentación no existe y se insertó)

                                            End If 'Fin sí: Cod_Variante <> ""

#End Region

                                            'Producto No existe en la tabla de detalle de TOMWMS. trans_oc_det
                                            If BePedidoCompraDet Is Nothing Then

                                                Try

                                                    BePedidoCompraDet = New clsBeTrans_oc_det
                                                    BePedidoCompraDet.IdOrdenCompraEnc = PedidoCompraExistente.IdOrdenCompraEnc
                                                    BePedidoCompraDet.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(BePedidoCompraDet.IdOrdenCompraEnc, CnnInterface, lTransInterface) + 1
                                                    BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega

                                                    If Not BePresentacion Is Nothing Then
                                                        BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                                                        BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                                                    Else
                                                        BePedidoCompraDet.IdPresentacion = 0
                                                    End If

                                                    BePedidoCompraDet.Nombre_producto = navPedidoCompraDet.Description
                                                    BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
                                                    BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
                                                    BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
                                                    BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
                                                    BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
                                                    BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
                                                    BePedidoCompraDet.Activo = True
                                                    BePedidoCompraDet.Porcentaje_arancel = 0
                                                    BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

                                                    If Asigna_Unidad_De_Medida(BePedidoCompraDet,
                                                                            navPedidoCompraDet,
                                                                            BeUnidadMedidaPedCompra,
                                                                            BeProductoBodega,
                                                                            lblprg,
                                                                            CnnInterface,
                                                                            lTransInterface,
                                                                            CnnLog) Then

                                                        clsLnTrans_oc_det.Insertar(BePedidoCompraDet, CnnInterface, lTransInterface)

                                                        VContadorBitacoraTOMWMS += 1

                                                    End If

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               BePedidoCompraDet.Nombre_producto,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet,
                                                                                               CnnLog)

                                                    Actualizar_Progreso(lblprg, String.Format("Error al insertar Detalle en : {0}{1}", ex.Message, vbNewLine))

                                                End Try

                                            Else 'Producto sí existe en tabla trans_oc_det

                                                Try

                                                    BePedidoCompraDet.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                    BePedidoCompraDet.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                    BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                    BePedidoCompraDet.Codigo_Producto = navPedidoCompraDet.No
                                                    BePedidoCompraDet.Nombre_producto = navPedidoCompraDet.Description
                                                    BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code

                                                    If BePedidoCompraDet.Cantidad = 0 Then
                                                        BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
                                                    Else

                                                        DifCant = navPedidoCompraDet.Quantity - BePedidoCompraDet.Cantidad

                                                        lblprg.AppendText(vbNewLine)

                                                        Select Case DifCant

                                                            Case 0
                                                                lblprg.AppendText(String.Format("La cantidad no se modificó para pedido {0} producto {1} ", navPedidoCompraEnc.No, navPedidoCompraDet.No))
                                                            Case Is > 0
                                                                lblprg.AppendText(String.Format("La cantidad incrementó respecto a TOM para pedido {0} producto {1} ", navPedidoCompraEnc.No, navPedidoCompraDet.No))
                                                            Case Is < 0
                                                                lblprg.AppendText(String.Format("La cantidad disminuyó respecto al original en WMS  para pedido {0} producto {1} ", navPedidoCompraEnc.No, navPedidoCompraDet.No))
                                                            Case Else
                                                                Exit Select
                                                        End Select

                                                        BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity

                                                    End If

                                                    BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
                                                    BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
                                                    BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
                                                    BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
                                                    BePedidoCompraDet.Activo = True
                                                    BePedidoCompraDet.Porcentaje_arancel = 0
                                                    BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

                                                    clsLnTrans_oc_det.Actualizar_Desde_Interface(BePedidoCompraDet, CnnInterface, lTransInterface)

                                                    VContadorBitacoraTOMWMS += 1

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                 BePedidoCompraDet.Nombre_producto,
                                                                                 BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                 BeConfigDet.Idnavconfigdet, CnnLog)

                                                    Actualizar_Progreso(lblprg, String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))

                                                End Try

                                            End If

                                        End If 'Fin sí: producto existe.

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                            "Pedido Sin Detalle",
                                                                             BeNavEjecucionEnc.IdEjecucionEnc,
                                                                             BeConfigDet.Idnavconfigdet,
                                                                             CnnLog)

                                        Actualizar_Progreso(lblprg, String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))

                                    End Try

                                Next

                            End If

                        Else

                            '#EJC20180108: Se agregó validación porque el detalle de la O.C. puede tener líneas no válidas a recibir en el WMS.
                            'Si la O.C. tiene detalle en la tabla intermedia
                            If navPedidoCompraEnc.Lineas_Detalle.Count = 0 Then
                                Actualizar_Progreso(lblprg, String.Format("Pedido #:{0} Sin Detalle {1}", navPedidoCompraEnc.No, vbNewLine))
                            Else

                                gBeOrdenCompra.IdOrdenCompraEnc = clsLnTrans_oc_enc.MaxID(CnnInterface, lTransInterface) + 1
                                gBeOrdenCompra.PropietarioBodega = New clsBePropietario_bodega
                                gBeOrdenCompra.PropietarioBodega.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.IdBodega,
                                                                                                                                BeConfigEnc.IdPropietario,
                                                                                                                                CnnInterface, lTransInterface)
                                gBeOrdenCompra.IdEstadoOC = 1
                                gBeOrdenCompra.Hora_Creacion = Now
                                gBeOrdenCompra.User_Agr = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Agr = Now
                                gBeOrdenCompra.Fecha_Creacion = Now
                                gBeOrdenCompra.Activo = True

                                BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                                       BeConfigEnc.IdBodega,
                                                                                                       CnnInterface, lTransInterface)

                                If BeProveedorBodega Is Nothing Then

                                    clsSyncSAPProveedor.BeConfigEnc = BeConfigEnc

                                    BeProveedorBodega = clsSyncSAPProveedor.Insertar_Proveedor_Single(navPedidoCompraEnc.Buy_From_Vendor_No, CnnInterface, lTransInterface, CnnLog, lblprg, prg)

                                    If BeProveedorBodega Is Nothing Then

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("El proveedor: {0} no existe, no se puede importar el pedido de compra: {1}",
                                                                                  navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                  navPedidoCompraEnc.No),
                                                                                  navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                  BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                  BeConfigDet.Idnavconfigdet, CnnLog)

                                        Actualizar_Progreso(lblprg, String.Format("Error al insertar el pedido de compra: {0} El proveedor: {1} no existe, ¿Ya se actualizó maestro de proveedores?", navPedidoCompraEnc.Buy_From_Vendor_No, navPedidoCompraEnc.No, vbNewLine))

                                        Throw New Exception("No logramos insertar el proveedor asociado a un pedido de compra, lamentamos el inconveniente")

                                    Else
                                        Actualizar_Progreso(lblprg, String.Format("El proveedor: {1} no existía pero se insertó para el pedido de compra: {0}. Nada de que preocuparse :) ", navPedidoCompraEnc.Buy_From_Vendor_No, navPedidoCompraEnc.No, vbNewLine))
                                    End If

                                End If

                                If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                                    gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega
                                End If

                                gBeOrdenCompra.IdProveedorBodega = BeProveedorBodega.IdAsignacion
                                gBeOrdenCompra.IdTipoIngresoOC = 1 'P.C. REC NAV
                                gBeOrdenCompra.No_Documento = navPedidoCompraEnc.Vendor_Invoice_No
                                gBeOrdenCompra.User_Mod = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Mod = Now
                                gBeOrdenCompra.Procedencia = ""
                                gBeOrdenCompra.No_Marchamo = ""
                                gBeOrdenCompra.Referencia = navPedidoCompraEnc.No
                                gBeOrdenCompra.Observacion = navPedidoCompraEnc.Posting_Description
                                gBeOrdenCompra.Control_Poliza = False

                                If gBeOrdenCompra.IsNew Then
                                    gBeOrdenCompra.ObjPoliza = Nothing
                                End If

                                gBeOrdenCompra.Enviado_A_ERP = False

                                Try

                                    clsLnTrans_oc_enc.Insertar(gBeOrdenCompra, CnnInterface, lTransInterface)

                                    VContadorBitacoraTOMWMS += 1

                                    If navPedidoCompraEnc.Lineas_Detalle.Count > 0 Then

                                        For Each navPedidoCompraDet As clsBeI_nav_ped_compra_det In navPedidoCompraEnc.Lineas_Detalle

                                            vContadorLineasDet += 1

                                            Dim BePedidoCompraDet As New clsBeTrans_oc_det() With {.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc,
                                                .IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompra.IdOrdenCompraEnc, CnnInterface, lTransInterface) + 1}

                                            '#20180101_1203:Línea agregada para actulización en envío.
                                            'BePedidoCompraDet.No_Linea = navPedidoCompraDet.No

                                            BeProductoBodega = clsLnProducto_bodega.Existe(navPedidoCompraDet.No, BeConfigEnc.Idbodega, CnnInterface, lTransInterface)

                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_Unidad_Medida(navPedidoCompraDet.Unit_of_Measure_Code)

#Region "COD_VARIANTE_A_PRESENTACION"

                                            If navPedidoCompraDet.Variant_Code <> "" Then

                                                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                                                                                          navPedidoCompraDet.Variant_Code,
                                                                                                                          CnnInterface,
                                                                                                                          lTransInterface)

                                                If BePresentacion Is Nothing Then
                                                    Throw New Exception("ERROR_202303031404I: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)
                                                End If

                                            End If

#End Region

                                            If BeProductoBodega IsNot Nothing Then

                                                Try

                                                    BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                    BePedidoCompraDet.Codigo_Producto = navPedidoCompraDet.No
                                                    BePedidoCompraDet.Nombre_producto = navPedidoCompraDet.Description
                                                    BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
                                                    BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
                                                    BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
                                                    BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
                                                    BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
                                                    BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
                                                    BePedidoCompraDet.Activo = True
                                                    BePedidoCompraDet.Porcentaje_arancel = 0
                                                    BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
                                                    BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

                                                    If Not BePresentacion Is Nothing Then
                                                        BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                                                        BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                                                    Else
                                                        BePedidoCompraDet.IdPresentacion = 0
                                                    End If

                                                    If Asigna_Unidad_De_Medida(BePedidoCompraDet,
                                                                            navPedidoCompraDet,
                                                                            BeUnidadMedidaPedCompra,
                                                                            BeProductoBodega,
                                                                            lblprg,
                                                                            CnnInterface,
                                                                            lTransInterface,
                                                                            CnnLog) Then

                                                        clsLnTrans_oc_det.Insertar(BePedidoCompraDet, CnnInterface, lTransInterface)

                                                        VContadorBitacoraTOMWMS += 1

                                                    End If

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                              BePedidoCompraDet.Nombre_producto,
                                                                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                              BeConfigDet.Idnavconfigdet,
                                                                                              CnnLog)


                                                    Actualizar_Progreso(lblprg, String.Format("Error al insertar desde SAP a intermedia: {0}{1}{2}", BePedidoCompraDet.Nombre_producto, ex.Message, vbNewLine))

                                                End Try

                                            Else

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro ",
                                                                                            navPedidoCompraDet.No,
                                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                            BeConfigDet.Idnavconfigdet,
                                                                                            CnnLog)

                                                Actualizar_Progreso(lblprg, String.Format("No existe Producto Bodega: {0}{1}", navPedidoCompraDet.No, vbNewLine))

                                            End If

                                        Next

                                    End If

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                            navPedidoCompraEnc.No,
                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                            BeConfigDet.Idnavconfigdet, CnnLog)

                                    Actualizar_Progreso(lblprg, String.Format("Error al insertar la OC con Referencia: {0}{1}{2}", navPedidoCompraEnc.No, vbNewLine, ex.Message))

                                End Try

                                Application.DoEvents()

                            End If

                        End If

                    Else
                        Actualizar_Progreso(lblprg, String.Format("Documento Inactivo {0} ", navPedidoCompraEnc.No, vbNewLine))
                    End If

                Next

            End If

            lTransInterface.Commit()

            Actualizar_Progreso(lblprg, "Fin de inserción en TOMWMS.")
            Actualizar_Progreso(lblprg, String.Format("Pedidos de compra procesados  correctamente: {0}", VContadorBitacoraTOMWMS))
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

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

            Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de compra a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    Public Function Get_Entrada_Mercancia_From_SAP(Optional ByVal AplicarFiltros As Boolean = True) As List(Of clsBeI_nav_ped_compra_enc)

        Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
        Dim BePedidoCompra As New clsBeI_nav_ped_compra_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_compra_det
        Dim NoLinea As Integer = 1

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                Dim vSQLOC As String

                vSQLOC = " SELECT T0.DOCENTRY,
                           T0.DOCNUM,T0.DOCDATE,  
                           '0000' AS  CARDCODE,
                           'PROVEEDOR GENERICO MI3' as  CARDNAME,
                           T0.DOCCUR,
                           T0.DOCTOTAL,
                           T0.JRNLMEMO,
                           T0.CANCELED,T0.DOCSTATUS,  
                           CASE WHEN T0.DOCTYPE = 'I'THEN 'ARTICULO'    
                            ELSE 'SERVICIO'    
                            END AS TIPO_ORDEN_VENTA,
                            (SELECT TOP 1 D0.WhsCode 
                                FROM IGN1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode WHERE D0.DocEntry = T0.DOCENTRY) AS BODEGA,
                            T0.COMMENTS,
                            T0.NumAtCard
                                FROM OIGN  T0 WHERE DOCSTATUS = 'O' 
                          --  AND CreateDate >= '2020-10-09 00:00:00.000' 
                            AND U_EnviadoWMS = 2 ORDER BY t0.DOCENTRY DESC"
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

                    If BePedidoCompra.Vendor_Invoice_No = "" Then
                        BePedidoCompra.Vendor_Invoice_No = BePedidoCompra.No.ToString()
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
                                        T1.U_UM_PROD AS UNIDAD_MEDIDA  
                                        FROM IGN1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode   
                                        WHERE T0.DOCENTRY = '" & BePedidoCompra.No & "'"
                    RsDet.DoQuery(query_det)

                    BePedidoCompra.Lineas_Detalle = New List(Of clsBeI_nav_ped_compra_det)

                    While RsDet.EoF = False

                        BePedidoDetWMS = New clsBeI_nav_ped_compra_det()
                        BePedidoDetWMS.NoEnc = RsDet.Fields.Item("DOCENTRY").Value.ToString()
                        BePedidoDetWMS.No = RsDet.Fields.Item("DOCENTRY").Value.ToString()
                        BePedidoDetWMS.No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
                        BePedidoDetWMS.Line_No = NoLinea
                        BePedidoDetWMS.Planed_Receipt_Date = Date.Now
                        BePedidoDetWMS.Quantity = Convert.ToDecimal(RsDet.Fields.Item("QUANTITY").Value)
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Marcar_PI_Sincronizado_SAP(ByVal pNoDocumento As String) As Boolean

        Marcar_PI_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim oEntradaMercancia As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryGenEntry), Documents)

                If oEntradaMercancia.GetByKey(pNoDocumento) Then

                    Try

                        oEntradaMercancia.UserFields.Fields.Item("U_EnviadoWMS").Value = "1"
                        oEntradaMercancia.Update()

                    Catch e As Exception
                        Throw e
                    End Try

                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Sub Enviar_Transacciones_De_Ingreso_SAP(ByRef lblprg As RichTextBox,
                                                   ByRef prg As Windows.Forms.ProgressBar)

        Dim lTransaccionesIngreso As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesIngresoSingle As New List(Of clsBeI_nav_transacciones_out)
        Dim TipoPedidoCompra As Integer = 0
        Dim Enviado_A_Erp As Boolean = False

        Try

            lTransaccionesIngreso = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio()

            If Not lTransaccionesIngreso Is Nothing AndAlso lTransaccionesIngreso.Count > 0 Then

                Dim ListaPedidosCompra = (From i In lTransaccionesIngreso
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idordencompra, Key i.Idrecepcionenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idordencompra, Key Keys.Idrecepcionenc})

                Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesIngreso.Count))

                Dim BeReOC As New clsBeTrans_re_oc

                For Each PC In ListaPedidosCompra

                    TipoPedidoCompra = clsLnTrans_oc_enc.Get_Tipo_Documento(PC.No_pedido)

                    BeReOC = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(PC.Idordencompra, PC.Idrecepcionenc)

                    If Not BeReOC Is Nothing Then

                        Enviado_A_Erp = ((BeReOC.No_docto <> "") AndAlso BeReOC.OC.Enviado_A_ERP)

                        Select Case TipoPedidoCompra

                            Case 1 'Es un pedido de compra de proveedor.

                                If Not Enviado_A_Erp Then

                                    lTransaccionesIngresoSingle = lTransaccionesIngreso.FindAll(Function(x) x.No_pedido = PC.No_pedido)

                                    If Enviar_Entrada_Mercancia_Proveedor_SAP(PC.No_pedido, lTransaccionesIngresoSingle, lblprg, prg) Then

                                        Try

                                            Actualizar_Progreso(lblprg, String.Format("Documento registrado correctamente: {0}", BeReOC.OC.No_Documento))

                                            BeReOC.No_docto = "ENV-" & FormatoFechas.tFecha(Now)

                                            clsLnTrans_re_oc.Actualizar_No_Docto(BeReOC)

                                            clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(PC.Idordencompra, True)

                                        Catch ex As Exception

                                            If ex.Message = "There is nothing to post." Then 'Pedido sin nada que registrar
                                                Actualizar_Progreso(lblprg, String.Format("Nada que registrar para pedido: {0} en NAV.", PC.No_pedido))
                                            Else
                                                Actualizar_Progreso(lblprg, String.Format("Error al registrar pedido de Ingreso WMS {0} en SAP: {1}", PC.No_pedido, ex.Message))
                                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar pedido {0} en SAP: {1}", PC.No_pedido, ex.Message),
                                                          PC.No_pedido,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)

                                            End If

                                        End Try

                                    End If 'Fin enviar

                                Else
                                    '#EJC20201119: aun no se que sucederá aquí....
                                    Try

                                        clsLnTrans_oc_enc.Actualizar_Estado_OC_By_Interface(PC.Idordencompra, 6)

                                        Actualizar_Progreso(lblprg, String.Format("Se registró el documento:{0} correctamente en SAP.", PC.No_pedido))

                                        clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(PC.Idordencompra, True)

                                    Catch ex As Exception

                                        If ex.Message = "There is nothing to post." Then 'Pedido sin nada que registrar
                                            Actualizar_Progreso(lblprg, String.Format("Nada que registrar para pedido: {0} en NAV.", PC.No_pedido))
                                        Else

                                            Actualizar_Progreso(lblprg, String.Format("Error al registrar entrada de mercancía {0} en SAP: {1}", PC.No_pedido, ex.Message))

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar pedido {0} en SAP: {1}", PC.No_pedido, ex.Message),
                                                                                      PC.No_pedido,
                                                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                      BeConfigDet.Idnavconfigdet)

                                        End If

                                    End Try

                                End If

                            Case 2 'Es un pedido de transferencia desde una bodega X hacia la bodega de WMS.

                                If Not Enviado_A_Erp Then


                                End If

                        End Select

                    End If 'OC Is Nothing

                Next

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
        End Try

    End Sub

    Public Function Enviar_Entrada_Mercancia_Proveedor_SAP(ByVal _Docentry As Integer,
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
                Dim oOrderPurchase As Documents

                oEntrega = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes), Documents)
                oOrderPurchase = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)

                If oOrderPurchase.GetByKey(_Docentry) Then

                    oEntrega.CardCode = oOrderPurchase.CardCode
                    oEntrega.DocDate = Date.Today
                    oEntrega.DocObjectCode = BoObjectTypes.oPurchaseDeliveryNotes

                    Dim n As Integer = 0

                    For j As Integer = 0 To oOrderPurchase.Lines.Count - 1

                        oOrderPurchase.Lines.SetCurrentLine(j)

                        Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", oOrderPurchase.Lines.ItemCode.ToString()))

                        Dim BeInavTransaccioensOut As clsBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out()
                        BeInavTransaccioensOut = lINav_Transaccioens_Out.Find(Function(x) x.Codigo_producto = oOrderPurchase.Lines.ItemCode.ToString())

                        If Not BeInavTransaccioensOut Is Nothing Then

                            Dim vTipoImpuesto As String = oOrderPurchase.Lines.TaxCode

                            oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_PurchaseOrder)
                            oEntrega.Lines.ItemCode = oOrderPurchase.Lines.ItemCode
                            oEntrega.Lines.BaseLine = n 'oOrderPurchase.Lines.BaseLine
                            oEntrega.Lines.UserFields.Fields.Item("U_Tipo").Value = "B"
                            oEntrega.Lines.TaxCode = vTipoImpuesto
                            oEntrega.Lines.BaseEntry = _Docentry
                            oEntrega.Lines.Quantity = BeInavTransaccioensOut.Cantidad
                            oEntrega.Lines.Add()

                            'oPurchaseDeliveryNote.lines.BaseEntry = 2027
                            'oPurchaseDeliveryNote.lines.BaseLine = 0
                            'oPurchaseDeliveryNote.lines.BaseType = 22

                            lINav_Transaccioens_Out_Enviadas.Add(BeInavTransaccioensOut)

                            n += 1

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

            Actualizar_Progreso(lblprg, String.Format("Error al enviar entrada de mercancía a SAP: {1} ", errMsg.Message))
            Throw errMsg

        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String)
        lblPrg.AppendText(mensaje & vbNewLine)
        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
    End Sub

End Class