Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports System.Windows
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json.Linq
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSapFacturaReserva
    Implements IDisposable

    Private disposedValue As Boolean
    Shared vHanaService As SapServiceLayerClient

    Public Shared Async Function Importar_Facturas_Reserva_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                                       prg As System.Windows.Forms.ProgressBar,
                                                                                       cnnLog As SqlConnection,
                                                                                       Optional ByVal pNoDocumentoSAP As String = "") As Task(Of Boolean)

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

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a SAP.")

            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return False
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Conexión correcta.")
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Obteniendo documento(s).")

            Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)
            lPedidosCompra = Await Get_Factura_Reserva_SAP_Hana_SL(Val(pNoDocumentoSAP),
                                                                      BeBodega.Codigo,
                                                                      vHanaService.SessionCookie,
                                                                      BD.Instancia.HANA_SL,
                                                                      lConnection,
                                                                      lTransaction,
                                                                      IdUsuario,
                                                                      lblprg)

            If lPedidosCompra Is Nothing Then
                clsPublic.Actualizar_Progreso(lblprg, "No se obtuvieron facturas de reserva.")
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Pedidos de compra en relación con SAP (OPCH): {0} ", lPedidosCompra.Count))

            prg.Maximum = lPedidosCompra.Count

            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                Dim BeProveedorBodega As New clsBeProveedor_bodega

                For Each BeINavPedCompra In lPedidosCompra

                    clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format("Procesando Pedido Compra: {0} ", BeINavPedCompra.No & " - " & BeINavPedCompra.Vendor_Invoice_No, vbNewLine))

                    If Not clsLnProveedor.Existe_Proveedor(BeINavPedCompra.Buy_From_Vendor_No) Then

                        BeConfigEnc = BeConfigEnc

                        If Await Inserta_Proveedor_Desde_SAP(BeINavPedCompra.Buy_From_Vendor_No, vHanaService.SessionCookie, BD.Instancia.HANA_SL) Then
                            clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & BeINavPedCompra.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                        End If

                    End If

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                            BePedidoCompraEnc,
                                                                            vResult) Then

                        Await Marcar_PI_Sincronizado_SLAsync(BeINavPedCompra.No, vHanaService.SessionCookie, BD.Instancia.HANA_SL)

                    End If

                    clsPublic.Actualizar_Progreso(lblprg, vResult)

                Next

            End If

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            clsLnLog_error_wms.Agregar_Error("Error_20250422_Fact_Res:" & ex.Message)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function

    Public Shared Async Function Inserta_Proveedor_Desde_SAP(ByVal pCodigo As String,
                                                             SessionCookie As String,
                                                             BaseUrl As String) As Task(Of Boolean)


        Dim BeProveedor As New clsBeProveedor
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim BeSAPProveedor As New clsBeI_nav_proveedor
        Dim clstrans As New clsTransaccion
        Dim vResult As Boolean = False

        Try

            clstrans.Begin_Transaction()

            BeSAPProveedor = Await clsSyncSAPProveedor.Get_Proveedor_SAP_SLAsync(pCodigo,
                                                                                 SessionCookie,
                                                                                 BaseUrl)

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

                    Await clsSyncSAPProveedor.Marcar_Proveedor_Sincronizado_SLAsync(BeProveedor.Codigo, SessionCookie, BaseUrl)

                    clstrans.lTransaction.Commit()

                    vResult = True

                Catch ex As Exception

                    clstrans.RollBack_Transaction()

                    clsLnLog_error_wms.Agregar_Error("Error_20250422_Inteface_Proveedor: " & ex.Message & " " & BeProveedor.Codigo)

                    Throw ex

                End Try

            End If

            Return vResult

        Catch ex As Exception
            Throw ex
        Finally
            clstrans.Close_Conection()
        End Try

    End Function

    Public Shared Async Function Insertar_Facturas_Reserva_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg As RichTextBox,
                                                                                                prg As System.Windows.Forms.ProgressBar,
                                                                                                Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                                Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                                                                Optional ByVal pNoDocumentoSAP As String = "") As Task(Of Boolean)

        Dim inicio As Date = Now
        Dim ok As Boolean = False

        Try
            Using CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient),
              CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

                Await CnnLog.OpenAsync().ConfigureAwait(False)
                Await CnnInterface.OpenAsync().ConfigureAwait(False)

                Using lTransInterface As SqlTransaction = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

                    ' --- ¿Llenar tabla intermedia? ---
                    Dim ejecutarImportacion As Boolean = True

                    If Not Pregunta_Si_LLena_Intermedia Then
                        ejecutarImportacion = True
                    Else
                        Dim r = XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?",
                                                "Interface pedidos de compra.",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question)
                        ejecutarImportacion = (r = DialogResult.Yes)
                        If Not ejecutarImportacion Then
                            clsPublic.Actualizar_Progreso(lblprg, "Se omitió el llenado de tabla intermedia por selección del usuario.")
                        End If
                    End If

                    If ejecutarImportacion Then
                        Dim importo As Boolean = Await Importar_Facturas_Reserva_Desde_SAP_A_TablaIntermedia(lblprg,
                                                                                                             prg,
                                                                                                             CnnLog,
                                                                                                             pNoDocumentoSAP).ConfigureAwait(False)

                        If Not importo Then
                            ' Rollback y salida segura con retorno explícito
                            Try : lTransInterface.Rollback() : Catch : End Try
                            prg.Value = 0
                            clsPublic.Actualizar_Progreso(lblprg, "No se importaron facturas de reserva (intermedia).")
                            Return False
                        End If
                    End If

                    ' Si llegamos aquí, lo que se necesitaba se ejecutó sin errores
                    lTransInterface.Commit()
                    ok = True
                End Using
            End Using

            ' Log del tiempo transcurrido (fuera de la transacción)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, inicio, Now)
            clsPublic.Actualizar_Progreso(lblprg, vbTab & $" -> Fin de proceso, tiempo transcurrido: {difSegundos} segundo(s)")

            Return ok

        Catch ex As Exception
            ' Rollback defensivo si la transacción existía (y aún no se liberó)
            ' No tenemos la referencia aquí, por eso hacemos el rollback dentro del Using en el Try anterior.
            prg.Value = 0
            clsLnLog_error_wms.Agregar_Error("Error_20250422_Insert_Fact_Res: " & ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar pedido de compra a tabla de TOMWMS: {ex.Message}{vbNewLine}")
            Return False
        End Try
    End Function

    Private Shared Async Function Enviar_Entrada_Mercancia_OC_SAP(ByVal BeINavConfigEnc As clsBeI_nav_config_enc,
                                                                 ByVal _Docentry As Integer,
                                                                 ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                                 ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                                 ByVal vCodigoBodegaDestino As String,
                                                                 ByVal IdRecepcionEnc As Integer,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction,
                                                                 ByVal lblprg As RichTextBox,
                                                                 ByVal prg As Forms.ProgressBar) As Task(Of Boolean)

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Conectando a Hdb.")

            vHanaService = New SapServiceLayerClient()
            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

            If loginResponse Is Nothing OrElse String.IsNullOrEmpty(loginResponse.SessionId) Then
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener sesión.")
                Return False
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Sesión iniciada correctamente.")

            Dim order As FacturaReservaDto = Await vHanaService.GetPurchaseOrderAsync(_Docentry)
            If order Is Nothing Then
                clsPublic.Actualizar_Progreso(lblprg, $"No se encontró la orden de compra con DocEntry {_Docentry}.")
                Return False
            End If

            Dim ResutlProc = Await vHanaService.Procesar_Detalle_Ingreso_HANA_Entregas_Separadas(order,
                                                                                                BeINavConfigEnc,
                                                                                                BeTransOCEnc,
                                                                                                IdRecepcionEnc,
                                                                                                lINavTransaccionesOut,
                                                                                                lblprg,
                                                                                                lConnection,
                                                                                                lTransaction)

            If Not ResutlProc Then
                clsPublic.Actualizar_Progreso(lblprg, "Error al procesar el documento de ingreso, valide el log.")
                Return False
            End If

            Dim BeTransReEnc = clsLnTrans_re_enc.GetSingle(IdRecepcionEnc)
            If BeTransReEnc IsNot Nothing Then
                Dim vTieneDiferencia As Boolean = Detalle_Tiene_Diferencia_Vrs_OC(BeTransOCEnc,
                                                                                  BeTransOCEnc.DetalleOC,
                                                                                  BeTransReEnc,
                                                                                  BeTransReEnc.Detalle)
            End If

            clsPublic.Actualizar_Progreso(lblprg, "Documento de compra cerrado correctamente.")
            clsLnLog_error_wms.Agregar_Error("LOG20241027: Se cerró el documento de compra en SAP.")
            clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(BeTransOCEnc.IdOrdenCompraEnc, True, lConnection, lTransaction)

            Return True

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error("Error_20231030_Enviar_Entrada_Mercancia_OC_SAP: " & ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
            Return False
        Finally
            prg.Value = 0
            prg.Visible = False
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

    Public Shared Async Sub Enviar_Facturas_Reserva_Prov_Ingreso(ByVal lblprg As RichTextBox,
                                                                 ByVal prg As Forms.ProgressBar)

        Dim lTransaccionesIngreso As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesIngresoImp As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesIngresoSingle As New List(Of clsBeI_nav_transacciones_out)
        Dim TipoPedidoCompra As Integer = 0
        Dim Enviado_A_Erp As Boolean = False
        Dim vCodigoBodegaDestino As String = ""
        Dim BeProductoEstado_NC As New clsBeProducto_estado
        Dim BeBodegaUbicacion As New clsBeBodega_ubicacion
        Dim clsTrans As New clsTransaccion
        Dim BeOrdenCompra As New clsBeTrans_oc_enc

        Try

            clsTrans.Begin_Transaction()

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          clsTrans.lConnection,
                                                          clsTrans.lTransaction)

            lTransaccionesIngreso = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio(tTipoDocumentoIngreso.Factura_Reserva_Proveedor,
                                                                                                    clsTrans.lConnection,
                                                                                                    clsTrans.lTransaction,
                                                                                                    BeConfigEnc.Idbodega)

            lTransaccionesIngresoImp = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio(tTipoDocumentoIngreso.Ingreso_importación,
                                                                                                    clsTrans.lConnection,
                                                                                                    clsTrans.lTransaction,
                                                                                                    BeConfigEnc.Idbodega)

            If Not lTransaccionesIngresoImp Is Nothing AndAlso lTransaccionesIngresoImp.Count > 0 Then
                lTransaccionesIngreso.AddRange(lTransaccionesIngresoImp)
            End If

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

                                    Case tTipoDocumentoIngreso.Ingreso, tTipoDocumentoIngreso.Devolucion, tTipoDocumentoIngreso.Factura_Reserva_Proveedor, tTipoDocumentoIngreso.Ingreso_importación 'Es un pedido de compra de proveedor.

                                        If Not Enviado_A_Erp Then

                                            lTransaccionesIngresoSingle = lTransaccionesIngreso.FindAll(Function(x) x.No_pedido = DocumentoIngreso.No_pedido AndAlso
                                                                                                                    x.Idrecepcionenc = BeReOC.IdRecepcionEnc)

                                            If Await Enviar_Entrada_Mercancia_OC_SAP(BeConfigEnc,
                                                                                     DocumentoIngreso.No_pedido,
                                                                                     lTransaccionesIngresoSingle,
                                                                                     BeTransOCEnc,
                                                                                     vCodigoBodegaDestino,
                                                                                     BeReOC.IdRecepcionEnc,
                                                                                     clsTrans.lConnection,
                                                                                     clsTrans.lTransaction,
                                                                                     lblprg,
                                                                                     prg) Then

                                                Try

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Documento registrado correctamente: {0}", BeTransOCEnc.No_Documento))

                                                    BeReOC.No_docto = "ENV-WMS" & FormatoFechas.tFecha(Now)
                                                    clsLnTrans_re_oc.Actualizar_No_Docto(BeReOC, clsTrans.lConnection, clsTrans.lTransaction)
                                                    clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(DocumentoIngreso.Idordencompra, True, clsTrans.lConnection, clsTrans.lTransaction)
                                                    clsLnLog_error_wms.Agregar_Error(String.Format("#IF_SAP_ENV_PED_COMP: Se registró correctamente EL INGRESO/DEVOLUCIÓN: {0}", BeTransOCEnc.No_Documento))

                                                Catch ex As Exception
                                                    Dim vMensaje As String = String.Format("Error al registrar pedido de Ingreso WMS {0} en SAP: {1}", DocumentoIngreso.No_pedido, ex.Message)
                                                    clsPublic.Actualizar_Progreso(lblprg, vMensaje)
                                                    clsLnLog_error_wms.Agregar_Error(vMensaje)
                                                End Try

                                            End If 'Fin enviar                                        

                                        End If

                                    Case Else
                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Tipo de documento no soportado: {0}", TipoPedidoCompra))
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

            clsPublic.Actualizar_Progreso(lblprg, "Fin de procesamiento.")

            clsTrans.Commit_Transaction()

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
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

    Public Shared Async Function Get_Factura_Reserva_SAP_Hana_SL(docNum As Integer?,
                                                                codBodega As String,
                                                                sessionCookie As String,
                                                                baseUrl As String,
                                                                lConnection As SqlConnection,
                                                                lTransaction As SqlTransaction,
                                                                IdUsuario As String,
                                                                lblprg As RichTextBox) As Task(Of List(Of clsBeI_nav_ped_compra_enc))

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        ServicePointManager.Expect100Continue = False

        Dim filtro As String = "U_ENVIADO_WMS eq 2 and CancelStatus eq 'csNo'"
        If docNum.HasValue AndAlso docNum > 0 Then
            filtro &= $" and DocNum eq {docNum.Value}"
        End If

        Dim requestUrl As String = $"PurchaseInvoices?$filter={Uri.EscapeDataString(filtro)}"
        Dim resultados As New List(Of clsBeI_nav_ped_compra_enc)()

        Try

            Using handler As New HttpClientHandler With {.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate, .ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True, .UseCookies = False}

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(HttpMethod.Get, baseUrl & requestUrl)
                        request.Headers.ConnectionClose = True
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                        Dim response As HttpResponseMessage = Await client.SendAsync(request).ConfigureAwait(False)
                        Dim payload As String = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

                        If Not response.IsSuccessStatusCode Then
                            clsPublic.Actualizar_Progreso(lblprg, $"Error al obtener facturas. Código: {response.StatusCode}, Detalle: {payload}")
                            Return resultados ' lista vacía
                        End If

                        Dim obj As JObject = JObject.Parse(payload)
                        Dim rows As JArray = TryCast(obj("value"), JArray)
                        If rows Is Nothing OrElse rows.Count = 0 Then
                            Return resultados ' lista vacía
                        End If

                        ' Cache para no pedir el mismo vendedor varias veces
                        Dim cacheSalesName As New Dictionary(Of Integer, String)(capacity:=16)

                        For Each row As JObject In rows
                            Dim docEntry As Integer = row.Value(Of Integer?)("DocEntry").GetValueOrDefault(-1)
                            Dim campaniaStr As String = row.Value(Of String)("U_Campania")
                            Dim campaignNo As Integer = 0
                            Integer.TryParse(campaniaStr, campaignNo)

                            ' Enriquecer con nombre de vendedor (si existe)
                            Dim salesCode? As Integer = row.Value(Of Integer?)("SalesPersonCode")
                            If salesCode.HasValue Then
                                Dim nombre As String = Nothing
                                If Not cacheSalesName.TryGetValue(salesCode.Value, nombre) Then
                                    nombre = Await GetSalesEmployeeNameAsync(client, sessionCookie, baseUrl, salesCode.Value).ConfigureAwait(False)
                                    If Not String.IsNullOrWhiteSpace(nombre) Then cacheSalesName(salesCode.Value) = nombre
                                End If
                                If Not String.IsNullOrWhiteSpace(nombre) Then
                                    row("SalesEmployeeName") = nombre
                                End If
                            End If


                            Dim documentLines As JArray = TryCast(row("DocumentLines"), JArray)
                            If documentLines Is Nothing OrElse documentLines.Count = 0 Then Continue For

                            ' Tallas/colores auxiliares
                            Dim dtDetTallaColor As DataTable =
                            FacturaReservaMapper.ConstruirTablaDesdeJsonTallasColores(documentLines, docEntry, campaignNo)

                            ' Encabezado
                            Dim encabezado As clsBeI_nav_ped_compra_enc = FacturaReservaMapper.MapearEncabezado(row, codBodega)

                            ' Detalle
                            encabezado.Lineas_Detalle = FacturaReservaMapper.MapearDetalle(documentLines)

                            ' Tallas/Colores
                            If dtDetTallaColor IsNot Nothing AndAlso dtDetTallaColor.Rows.Count > 0 Then
                                encabezado.Lineas_Detalle_Talla_Color =
                                Await FacturaReservaMapper.MapearDetalleTallaColor(dtDetTallaColor,
                                                                                   lConnection,
                                                                                   lTransaction,
                                                                                   IdUsuario,
                                                                                   sessionCookie,
                                                                                   BD.Instancia.HANA_SL).ConfigureAwait(False)
                            Else
                                encabezado.Lineas_Detalle_Talla_Color = New List(Of clsBeProducto_talla_color)()
                            End If

                            ' Campaña
                            Dim BeCampaña As clsBeCampaña = clsLnCampaña.Get_Single_By_IdCampaña(campaignNo, lConnection, lTransaction)

                            If BeCampaña Is Nothing Then
                                Dim rowCampaña As DataRow = Await Get_Campaña_Sap_Hana_By_IdCampaña(encabezado.Campaign_No, sessionCookie, baseUrl).ConfigureAwait(False)

                                If rowCampaña IsNot Nothing Then
                                    encabezado.Campaña = FacturaReservaMapper.MapearCampaña(rowCampaña, lConnection, lTransaction, IdUsuario)
                                Else
                                    encabezado.Campaña = Nothing
                                End If
                            Else
                                encabezado.Campaña = BeCampaña
                            End If

                            resultados.Add(encabezado)
                        Next

                        clsPublic.Actualizar_Progreso(lblprg, "Facturas obtenidas correctamente.")
                        Return resultados
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("Error al Procesar_Solicitud_Traslado_SAP la factura de reserva desde SAP Hana SL: " & ex.Message, ex)
        End Try
    End Function

    Private Shared Async Function GetSalesEmployeeNameAsync(client As HttpClient,
                                                            sessionCookie As String,
                                                            baseUrl As String,
                                                            employeeCode As Integer) As Task(Of String)

        Dim requestUrl As String = $"SalesPersons?$filter=SalesEmployeeCode eq {Uri.EscapeDataString(employeeCode)}"

        Using req As New HttpRequestMessage(HttpMethod.Get, baseUrl & requestUrl)
            req.Headers.ConnectionClose = True
            req.Headers.Add("Cookie", sessionCookie)
            req.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Dim resp = Await client.SendAsync(req).ConfigureAwait(False)
            If Not resp.IsSuccessStatusCode Then
                Return Nothing
            End If

            Dim json = Await resp.Content.ReadAsStringAsync().ConfigureAwait(False)
            Dim o As JObject = JObject.Parse(json)
            Dim arr As JArray = TryCast(o("value"), JArray)
            If arr Is Nothing OrElse arr.Count = 0 Then Return Nothing
            Dim salesEmployeeName As String = arr(0).Value(Of String)("SalesEmployeeName")
            Return CStr(salesEmployeeName)
        End Using
    End Function

    Public Shared Async Function Marcar_PI_Sincronizado_SLAsync(docEntry As String, sessionCookie As String, baseUrl As String) As Task(Of Boolean)

        Try

            If String.IsNullOrWhiteSpace(docEntry) Then Return False

            Dim requestUrl As String = $"PurchaseInvoices({docEntry})"
            Dim payload As String = "{""U_ENVIADO_WMS"": ""1""}"
            Dim httpPatch As New HttpMethod("PATCH")

            Using handler As New HttpClientHandler()
                handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
                handler.UseCookies = False

                Using client As New HttpClient(handler)
                    client.DefaultRequestHeaders.ConnectionClose = True

                    Using request As New HttpRequestMessage(httpPatch, baseUrl & requestUrl)
                        request.Headers.ConnectionClose = True
                        request.Headers.Add("Cookie", sessionCookie)
                        request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                        request.Content = New StringContent(payload, Encoding.UTF8, "application/json")

                        Dim response = Await client.SendAsync(request).ConfigureAwait(False)

                        If response.IsSuccessStatusCode Then
                            Return True
                        Else
                            Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                            Throw New Exception($"Error al actualizar OPCH. Código: {response.StatusCode}, Detalle: {errContent}")
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("(SL) {0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message), ex)
        End Try

    End Function

    Private Shared Async Function Get_Campaña_Sap_Hana_By_IdCampaña(campañaId As Integer,
                                                                    sessionCookie As String,
                                                                    baseUrl As String) As Task(Of DataRow)

        Dim requestUrl As String = $"Periodo?$filter=Period eq {campañaId} "

        Using handler As New HttpClientHandler()
            handler.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            handler.ServerCertificateCustomValidationCallback = Function(sender, cert, chain, errors) True
            handler.UseCookies = False

            Using client As New HttpClient(handler)
                client.DefaultRequestHeaders.ConnectionClose = True

                Using request As New HttpRequestMessage(HttpMethod.Get, baseUrl & requestUrl)
                    request.Headers.ConnectionClose = True
                    request.Headers.Add("Cookie", sessionCookie)
                    request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

                    Dim response = Await client.SendAsync(request).ConfigureAwait(False)

                    If Not response.IsSuccessStatusCode Then
                        Dim errContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                        Throw New Exception($"Error al consultar campaña SL. Código: {response.StatusCode}, Detalle: {errContent}")
                    End If

                    Dim jsonResponse = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                    Dim obj = JObject.Parse(jsonResponse)
                    Dim rows = obj("value")

                    If rows Is Nothing OrElse Not rows.HasValues Then
                        Return Nothing
                    End If

                    ' Convertir el primer objeto JSON a DataRow
                    Dim dt As New DataTable()
                    dt.Columns.Add("DocEntry", GetType(Integer))
                    dt.Columns.Add("Remark", GetType(String))
                    dt.Columns.Add("U_FechaInicial", GetType(Date))
                    dt.Columns.Add("U_FechaFinal", GetType(Date))

                    Dim dr = dt.NewRow()
                    dr("DocEntry") = IIf(IsDBNull(rows(0)("DocEntry")), 0, rows(0)("DocEntry"))
                    dr("Remark") = IIf(IsDBNull(rows(0)("Remark")), "", rows(0)("Remark"))
                    dr("U_FechaInicial") = IIf(IsDBNull(rows(0)("U_FechaInicial")), Date.MinValue, rows(0)("U_FechaInicial"))
                    dr("U_FechaFinal") = IIf(IsDBNull(rows(0)("U_FechaFinal")), Date.MinValue, rows(0)("U_FechaFinal"))
                    dt.Rows.Add(dr)

                    Return dt.Rows(0)
                End Using
            End Using
        End Using
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

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

End Class