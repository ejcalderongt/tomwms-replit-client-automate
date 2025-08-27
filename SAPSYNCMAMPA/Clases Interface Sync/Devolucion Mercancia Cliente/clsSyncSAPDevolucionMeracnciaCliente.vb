Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Public Class clsSyncSAPDevolucionMeracnciaCliente : Inherits clsInterfaceBase
    Implements IDisposable

    Shared VContadorBitacoraTOMWMS As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Public Sub Dispose() Implements IDisposable.Dispose

    End Sub
    Public Function Importar_Devolucion_Cliente_SAP_A_TI_WMS(ByVal lblprg As RichTextBox,
                                                                               ByRef prg As System.Windows.Forms.ProgressBar,
                                                                               ByRef cnnLog As SqlConnection) As Boolean

        Importar_Devolucion_Cliente_SAP_A_TI_WMS = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResult As String = ""

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia.")

            Dim lNotasCreditoCliente As New List(Of clsBeI_nav_ped_compra_enc)

            lNotasCreditoCliente = Get_Notas_Credito_From_SAP()

            BeNavEjecucionRes.Registros_ws = lNotasCreditoCliente.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeProductoBodega As New clsBeProducto_bodega

            clsPublic.Actualizar_Progreso(lblprg, "Devolución de mercancía de cliente en SAP (ORDN): {0} ")

            prg.Maximum = lNotasCreditoCliente.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BePedidoCompraEnc As New clsBeTrans_oc_enc

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            'Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                Dim BeProveedorBodega As New clsBeProveedor

                For Each PC In lNotasCreditoCliente

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Devolución de Cliente: {0} ", PC.No, vbNewLine))

                    If Not clsLnProveedor.Existe_Proveedor(PC.Buy_From_Vendor_No) Then

                        If Inserta_Cliente_Como_Proveedor_Desde_SAP(PC.Buy_From_Vendor_No, cnnLog) Then
                            clsPublic.Actualizar_Progreso(lblprg, "El cliente como proveedor: " & PC.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                        End If

                    End If

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(PC, BePedidoCompraEnc, vResult) Then
                        Marcar_PI_Sincronizado_SAP(PC.No)
                    End If

                    clsPublic.Actualizar_Progreso(lblprg, vResult)

                Next

            End If

            lTransaction.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción en tabla intermedia.")

            Importar_Devolucion_Cliente_SAP_A_TI_WMS = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar devolución de cliente: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function

    Public Shared Function Inserta_Cliente_Como_Proveedor_Desde_SAP(ByVal pCodigo As String,
                                                                ByVal cnnLog As SqlConnection) As Boolean

        Inserta_Cliente_Como_Proveedor_Desde_SAP = False

        Try
            Dim query As String = $"
            SELECT ""CardCode"" AS CODIGO,
                   ""CardName"" AS NOMBRE_COMERCIAL,
                   ""Phone1"",
                   'MI3' AS CONTACTO,
                   ""U_NIT"" AS NIT,
                   ""Address"" AS DIRECCION,
                   ""E_Mail"" 
              FROM OCRD 
             WHERE ""CardType"" = 'C' 
               AND ""CardCode"" = '{pCodigo}' 
             LIMIT 1"

            Dim dt As DataTable = HanaHelper.OpenDT(query)
            If dt.Rows.Count = 0 Then
                Throw New Exception("No se encontró el cliente en SAP con CardCode: " & pCodigo)
            End If

            Dim row = dt.Rows(0)

            Dim BeProveedor As New clsBeProveedor With {
            .IdEmpresa = BeConfigEnc.Idempresa,
            .IdPropietario = BeConfigEnc.IdPropietario,
            .IdProveedor = clsLnProveedor.MaxID() + 1,
            .Codigo = row("CODIGO").ToString(),
            .Nombre = row("NOMBRE_COMERCIAL").ToString(),
            .Telefono = row("Phone1").ToString(),
            .Nit = row("NIT").ToString(),
            .Direccion = row("DIRECCION").ToString(),
            .Contacto = row("CONTACTO").ToString(),
            .Activo = True,
            .User_agr = BeConfigEnc.IdUsuario,
            .Fec_agr = Date.UtcNow,
            .User_mod = BeConfigEnc.IdUsuario,
            .Fec_mod = Date.UtcNow
        }

            Try
                clsLnProveedor.Insertar(BeProveedor)
                VContadorBitacoraTOMWMS += 1

                Dim BeProveedorBodega As New clsBeProveedor_bodega With {
                .IdAsignacion = clsLnProveedor_bodega.MaxID() + 1,
                .IdProveedor = BeProveedor.IdProveedor,
                .IdBodega = BeConfigEnc.Idbodega,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now
            }

                clsLnProveedor_bodega.Insertar(BeProveedorBodega)

                Inserta_Cliente_Como_Proveedor_Desde_SAP = True

            Catch ex As Exception
                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       BeProveedor.Codigo,
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)
                Throw ex
            End Try

        Catch ex As Exception
            Throw New Exception("No se pudo insertar el cliente como proveedor desde SAP: " & ex.Message)
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
                                                        BeConfigDet.Idnavconfigdet, lConnectionLog)

                            lblprg.AppendText(String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                    Else

                        Throw New Exception(
                    String.Format("Error: No existe factor en unidad_medida_conversion 
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

    Public Function Insertar_NC_Cliente_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                                   ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                   Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                   Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_NC_Cliente_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim DifCant As Double = 0

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Devolucion_Cliente") Then
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

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Devolucion_Cliente_SAP_A_TI_WMS(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Devolucion_Cliente_SAP_A_TI_WMS(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lPedidoCompraEnc As New List(Of clsBeI_nav_ped_compra_enc)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando pedidos de compra en tabla intermedia ")

            lPedidoCompraEnc = clsLnI_nav_ped_compra_enc.GetAll(CnnInterface, lTransInterface, lblprg, prg)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedidos de Compra en tabla intermedia: {0}", lPedidoCompraEnc.Count))

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

                clsPublic.Actualizar_Progreso(lblprg, "Trasladando documento a TOMWMS.")

                Dim BeUnidadMedidaPedCompra As New clsBeUnidad_medida

                For Each navPedidoCompraEnc As clsBeI_nav_ped_compra_enc In lPedidoCompraEnc

                    If navPedidoCompraEnc.Status <> 0 Then

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando devolución de cliente: {0} ", navPedidoCompraEnc.No, vbNewLine))

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
                                                                                                   BeConfigEnc.Idbodega,
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

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando# : {0}{1}", navPedidoCompraEnc.No, vbNewLine))

                            VContadorBitacoraTOMWMS += 1

                            If navPedidoCompraEnc.Lineas_Detalle.Count > 0 Then

                                Dim BePedidoCompraDet As New clsBeTrans_oc_det

                                For Each navPedidoCompraDet As clsBeI_nav_ped_compra_det In navPedidoCompraEnc.Lineas_Detalle

                                    vContadorLineasDet += 1

                                    Try

                                        BeProductoBodega = clsLnProducto_bodega.Existe(navPedidoCompraDet.No,
                                                                                       BeConfigEnc.Idbodega,
                                                                                       CnnInterface,
                                                                                       lTransInterface)

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
                                                    Throw New Exception("ERROR_202303031404G: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)
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

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Detalle en: {0}{1}", ex.Message, vbNewLine))

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
                                                                                               BeConfigDet.Idnavconfigdet,
                                                                                               CnnLog)

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))

                                                End Try

                                            End If

                                        End If 'Fin sí: producto existe.

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                   "Pedido Sin Detalle",
                                                                                    BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                    BeConfigDet.Idnavconfigdet,
                                                                                    CnnLog)

                                        String.Format(String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))

                                    End Try

                                Next

                            End If

                        Else

                            '#EJC20180108: Se agregó validación porque el detalle de la O.C. puede tener líneas no válidas a recibir en el WMS.
                            'Si la O.C. tiene detalle en la tabla intermedia
                            If navPedidoCompraEnc.Lineas_Detalle.Count = 0 Then
                                lblprg.AppendText(String.Format("Pedido #:{0} Sin Detalle {1}", navPedidoCompraEnc.No, vbNewLine))
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else

                                gBeOrdenCompra.IdOrdenCompraEnc = clsLnTrans_oc_enc.MaxID(CnnInterface, lTransInterface) + 1
                                gBeOrdenCompra.PropietarioBodega = New clsBePropietario_bodega
                                gBeOrdenCompra.PropietarioBodega.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega,
                                                                                                                                                               BeConfigEnc.IdPropietario,
                                                                                                                                                               CnnInterface,
                                                                                                                                                               lTransInterface)
                                gBeOrdenCompra.IdEstadoOC = 1
                                gBeOrdenCompra.Hora_Creacion = Now
                                gBeOrdenCompra.User_Agr = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Agr = Now
                                gBeOrdenCompra.Fecha_Creacion = Now
                                gBeOrdenCompra.Activo = True

                                BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                                           BeConfigEnc.Idbodega,
                                                                                                           CnnInterface,
                                                                                                           lTransInterface)

                                If BeProveedorBodega Is Nothing Then

                                    BeProveedorBodega = clsSyncSAPProveedor.Insertar_Proveedor_Single(navPedidoCompraEnc.Buy_From_Vendor_No, CnnInterface, lTransInterface, CnnLog, lblprg, prg)

                                    If BeProveedorBodega Is Nothing Then

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("El proveedor: {0} no existe, no se puede importar el pedido de compra: {1}",
                                                                                  navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                  navPedidoCompraEnc.No),
                                                                                  navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                  BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                  BeConfigDet.Idnavconfigdet, CnnLog)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar el pedido de compra: {0} El proveedor: {1} no existe, ¿Ya se actualizó maestro de proveedores?", navPedidoCompraEnc.Buy_From_Vendor_No, navPedidoCompraEnc.No, vbNewLine))

                                        Throw New Exception("No logramos insertar el proveedor asociado a un pedido de compra, lamentamos el inconveniente")

                                    Else

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El proveedor: {1} no existía pero se insertó para el pedido de compra: {0}. Nada de que preocuparse :) ", navPedidoCompraEnc.Buy_From_Vendor_No, navPedidoCompraEnc.No, vbNewLine))

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
                                                    Throw New Exception("ERROR_202303031404G: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)
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
                                                                      BeConfigDet.Idnavconfigdet, CnnLog)

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar desde SAP a intermedia: {0}{1}{2}", BePedidoCompraDet.Nombre_producto, ex.Message, vbNewLine))

                                                End Try

                                            Else

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro ",
                                                                      navPedidoCompraDet.No,
                                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                                      BeConfigDet.Idnavconfigdet, CnnLog)

                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("No existe Producto Bodega: {0}{1}", navPedidoCompraDet.No, vbNewLine))

                                            End If

                                        Next

                                    End If

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               navPedidoCompraEnc.No,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar la devolución de cliente con Referencia: {0}{1}{2}", navPedidoCompraEnc.No, vbNewLine, ex.Message))

                                End Try

                                Application.DoEvents()

                            End If

                        End If

                    Else

                        clsPublic.Actualizar_Progreso(lblprg, vbTab & "Documento inactivo.")

                    End If

                Next

            End If

            lTransInterface.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción en TOMWMS.")
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedidos de compra procesados  correctamente: {0}", VContadorBitacoraTOMWMS))
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))


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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function


    Public Function Get_Notas_Credito_From_SAP(Optional ByVal AplicarFiltros As Boolean = True) As List(Of clsBeI_nav_ped_compra_enc)

        Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
        Dim NoLinea As Integer = 1

        Try
            Dim queryEnc As String = """
            SELECT ""DocEntry"", ""DocNum"", ""DocDate"", ""CardCode"", ""CardName"", ""DocCur"", ""DocTotal"", ""JrnlMemo"", ""Canceled"", ""DocStatus"",
                   CASE WHEN ""DocType"" = 'I' THEN 'ARTICULO' ELSE 'SERVICIO' END AS TIPO_ORDEN_VENTA,
                   (SELECT ""WhsCode"" FROM RIN1 WHERE RIN1.""DocEntry"" = T0.""DocEntry"" LIMIT 1) AS BODEGA,
                   ""Comments"", ""NumAtCard""
              FROM ORDN T0
             WHERE ""U_EnviadoWMS"" = 2
             ORDER BY ""DocEntry"" DESC
        """

            Dim dtEnc As DataTable = HanaHelper.OpenDT(queryEnc)
            For Each rowEnc As DataRow In dtEnc.Rows
                Dim BePedidoCompra As New clsBeI_nav_ped_compra_enc With {
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
                .IsImport = False
            }
                If String.IsNullOrWhiteSpace(BePedidoCompra.Vendor_Invoice_No) Then
                    BePedidoCompra.Vendor_Invoice_No = BePedidoCompra.No.ToString()
                End If

                Dim queryDet As String = $"""
                SELECT T0.""ItemCode"", T0.""Dscription"", T0.""Quantity"", T0.""Price"", T0.""LineTotal"",
                       T0.""VatSum"", T0.""DocEntry"", T0.""WhsCode"", T1.""U_Um_Prod"" AS UNIDAD_MEDIDA
                  FROM RIN1 T0
                  JOIN OITM T1 ON T1.""ItemCode"" = T0.""ItemCode""
                 WHERE T0.""DocEntry"" = '{BePedidoCompra.No}'
            """

                Dim dtDet As DataTable = HanaHelper.OpenDT(queryDet)
                BePedidoCompra.Lineas_Detalle = New List(Of clsBeI_nav_ped_compra_det)

                For Each rowDet As DataRow In dtDet.Rows
                    Dim BePedidoDetWMS As New clsBeI_nav_ped_compra_det With {
                    .NoEnc = rowDet("DocEntry").ToString(),
                    .No = rowDet("ItemCode").ToString(),
                    .Line_No = NoLinea,
                    .Planed_Receipt_Date = Date.Now,
                    .Quantity = Convert.ToDecimal(rowDet("Quantity")),
                    .Quantity_Received = 0,
                    .Description = rowDet("Dscription").ToString(),
                    .Unit_of_Measure_Code = rowDet("UNIDAD_MEDIDA").ToString(),
                    .Type = 2,
                    .Variant_Code = Nothing,
                    .Location_Code = rowDet("WhsCode").ToString()
                }
                    BePedidoCompra.Lineas_Detalle.Add(BePedidoDetWMS)
                    NoLinea += 1
                Next

                lPedidosCompra.Add(BePedidoCompra)
            Next

            Return lPedidosCompra

        Catch ex As Exception
            Throw New Exception($"{MethodBase.GetCurrentMethod.Name()} {ex.Message}")
        End Try

    End Function

    Public Function Marcar_PI_Sincronizado_SAP(ByVal pNoDocumento As String) As Boolean
        Marcar_PI_Sincronizado_SAP = False

        Try
            Dim query As String = $"""
            UPDATE ORIN
               SET ""U_EnviadoWMS"" = '1'
             WHERE ""DocEntry"" = '{pNoDocumento}'
        """

            Dim result As Integer = HanaHelper.Xcute(query)
            Marcar_PI_Sincronizado_SAP = (result > 0)

        Catch ex As Exception
            Throw New Exception($"(M) {MethodBase.GetCurrentMethod.Name()} {ex.Message}")
        End Try

    End Function

End Class
