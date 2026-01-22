Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS.WSCantidadPedidoTransferencia
Imports TOMWMS.WSFichaBodegas
Imports TOMWMS.WSLotePedidoTransferencia
Imports TOMWMS.WSNavCantidadRecibirPedidoCompra
Imports TOMWMS.WSNavLotePedidoCompra
Imports TOMWMS.WSPedidoCompra
Imports TOMWMS.WSPedidoTransferencia
Imports TOMWMS.WSRecepcionesAlm
Imports TOMWMS.WSRegistraRecepcionCompra
Imports TOMWMS.WSRegistraTransferRecepcion

Public Class clsSyncNavPedidoCompra : Inherits clsInterfaceBase
    Implements IDisposable

    Property pBodega As String = ""

    Private fichaPedidosCompra() As Pedidos_Compra
    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Dim wsLotePedidoCompra As New Lote_PedidoCompra With
        {
        .UseDefaultCredentials = UsarCredencialesPorDefecto,
        .Credentials = CredencialesConexion
        }

    Dim wsCantidadPedidoCompra As New CantidadRecibir_PedidoCompra With
        {
        .UseDefaultCredentials = UsarCredencialesPorDefecto,
        .Credentials = CredencialesConexion
        }

    Private wsPedidoCompraService As New Pedidos_Compra_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Private wsRegistraRecepcionPedidoCompra As New Registra_Recepcion_Compra() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }
    Private Property WsRegistra_Transfer_Recepcion As New Registra_Transfer_Recepcion() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Dim wsLotePedidoTransferencia As New Lote_PedidoTransferencia With
    {
    .UseDefaultCredentials = UsarCredencialesPorDefecto,
    .Credentials = CredencialesConexion
    }

    Dim wsCantidadPedidoTransferencia As New CantidadEnviar_PedidoTransferencia With
    {
    .UseDefaultCredentials = UsarCredencialesPorDefecto,
    .Credentials = CredencialesConexion
    }

    Private wsPedidoTrasladoService As New Pedidos_Transferencia_Service() With
    {
    .UseDefaultCredentials = UsarCredencialesPorDefecto,
    .Credentials = CredencialesConexion
    }

    Public Sub Dispose() Implements IDisposable.Dispose
        If wsPedidoCompraService IsNot Nothing Then
            wsPedidoCompraService.Dispose()
            wsPedidoCompraService = Nothing
        End If
        If wsLotePedidoCompra IsNot Nothing Then
            wsLotePedidoCompra.Dispose()
            wsLotePedidoCompra = Nothing
        End If
        If wsCantidadPedidoCompra IsNot Nothing Then
            wsCantidadPedidoCompra.Dispose()
            wsCantidadPedidoCompra = Nothing
        End If
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Importar_Pedidos_Compra_Desde_WSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                          ByRef prg As System.Windows.Forms.ProgressBar,
                                                                          ByRef cnnLog As SqlConnection) As Boolean

        Importar_Pedidos_Compra_Desde_WSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim lPedidosCompra As New List(Of Pedidos_Compra)

            lPedidosCompra = Get_Pedidos_Compra_FromWS(lblprg, True)

            BeNavEjecucionRes.Registros_ws = fichaPedidosCompra.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeI_nav_PedidoCompra As clsBeI_nav_ped_compra_enc
            Dim BeI_nav_PedidoCompraDet As clsBeI_nav_ped_compra_det
            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim vErrorAlInsertarClienteComoBodega As Boolean = False
            Dim vMensajeErrorClienteBodega As String = ""

            lblprg.AppendText(String.Format("Pedidos de compra en WS: {0} ", fichaPedidosCompra.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()
            lblprg.Refresh()

            prg.Maximum = lPedidosCompra.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det_lote.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                For Each PC As Pedidos_Compra In lPedidosCompra

                    If PC.No = "PC-098135" Then
                        Debug.Print("Aqui")
                    End If

                    If Not clsLnI_nav_ped_compra_enc.Exist(PC.No, lConnection, lTransaction) Then

                        BeI_nav_PedidoCompra = New clsBeI_nav_ped_compra_enc

                        CopyObject(PC, BeI_nav_PedidoCompra)

                        If Not PC.Document_DateSpecified Then
                            PC.Document_Date = Now.Date 'No tengo fecha en el documento?
                        ElseIf PC.Document_Date.Year <= 1000 Then
                            PC.Document_Date = Now.Date 'No tengo fecha en el documento?
                        End If

                        If Not PC.Order_DateSpecified Then
                            BeI_nav_PedidoCompra.Order_Date = PC.Document_Date
                        ElseIf PC.Order_Date.Year <= 1000 Then
                            BeI_nav_PedidoCompra.Order_Date = PC.Document_Date
                        End If

                        If Not PC.Expected_Receipt_DateSpecified Then
                            BeI_nav_PedidoCompra.Expected_Receipt_Date = BeI_nav_PedidoCompra.Order_Date
                        ElseIf PC.Expected_Receipt_Date.Year <= 1000 Then
                            BeI_nav_PedidoCompra.Expected_Receipt_Date = PC.Document_Date
                        End If

                        If PC.No = "PC-080850" Then
                            Debug.Print(PC.No)
                        End If

                        'Proveedor
                        BeI_nav_PedidoCompra.Buy_From_Vendor_Name = PC.Buy_from_Vendor_Name
                        BeI_nav_PedidoCompra.Buy_From_Vendor_No = PC.Buy_from_Vendor_No
                        '#CKFK20251107 Voy a poner esto en comentario porque deberia ser de tipo ingreso
                        'BeI_nav_PedidoCompra.Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Orden_De_Compra_Interna

                        BeI_nav_PedidoCompra.Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Ingreso

                        lblprg.AppendText(String.Format("Procesando Pedido Compra: {0} ", BeI_nav_PedidoCompra.No, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        vErrorAlInsertarClienteComoBodega = False

                        If Not PC.Location_Code Is Nothing Then

                            vMensajeErrorClienteBodega = String.Format("La bodega:{0} no está registrada como cliente y no es válida para recibir productos. {1} ", PC.Location_Code, vbNewLine)

                            If Not clsLnCliente.Bodega_Es_Valida_Para_Recepcion(PC.Location_Code, lConnection, lTransaction) Then

                                If XtraMessageBox.Show(vMensajeErrorClienteBodega + vbNewLine &
                                                      "¿Insertar como cliente?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                                    If Not clsSyncNavBodega.Insertar_Bodega_Single_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(PC.Location_Code,
                                                                                                                     True,
                                                                                                                     cnnLog,
                                                                                                                     lConnection,
                                                                                                                     lTransaction,
                                                                                                                     lblprg) Then
                                        vErrorAlInsertarClienteComoBodega = True

                                        Exit Function

                                    End If

                                Else
                                    vErrorAlInsertarClienteComoBodega = True
                                End If

                            Else
                                vErrorAlInsertarClienteComoBodega = False
                            End If

                        End If

                        If vErrorAlInsertarClienteComoBodega Then

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensajeErrorClienteBodega,
                                                                   PC.Location_Code,
                                                                   0,
                                                                   0)

                            lblprg.AppendText(vMensajeErrorClienteBodega)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If

                        Try
                            '#EJC20180503: Es un documento de compra de proveedor
                            BeI_nav_PedidoCompra.Is_Internal_Transfer = False

                            'Insertar encabezado
                            clsLnI_nav_ped_compra_enc.Insertar(BeI_nav_PedidoCompra, lConnection, lTransaction)

                            VContadorBitacoraIntermedia += 1

                            prg.Value = vContador

                            vContador += 1

                            Application.DoEvents()

                            'Insertar detalle
                            If Not PC.PurchLines Is Nothing Then

                                For Each L As Purchase_Order_Line In PC.PurchLines

                                    BeI_nav_PedidoCompraDet = New clsBeI_nav_ped_compra_det

                                    Try

                                        Try

                                            CopyObject(L, BeI_nav_PedidoCompraDet)

                                        Catch ex As Exception
                                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                        End Try

                                        BeI_nav_PedidoCompraDet.NoEnc = PC.No

                                        If L.Type = Type.Item Then 'Es Producto

                                            If Not L.Location_Code Is Nothing Then

                                                '#EJC20210114: Se utiliza para determinar si la bodega a donde están intentando hacer la recepción
                                                'es o no una bodega válida, si es una bodega válida, debe haberse insertado previamente como cliente.
                                                If clsLnCliente.Bodega_Es_Valida_Para_Recepcion(L.Location_Code, lConnection, lTransaction) Then

                                                    BeProductoBodega = clsLnProducto_bodega.Existe(L.No, BeConfigEnc.Idbodega, lConnection, lTransaction)

                                                    If BeProductoBodega Is Nothing Then
                                                        If clsSyncNavProducto.Importar_Productos_DesdeWSNav_A_TablaIntermedia(L.No, lblprg, prg, cnnLog) Then
                                                            BeProductoBodega = clsLnProducto_bodega.Existe(L.No, BeConfigEnc.Idbodega, lConnection, lTransaction)
                                                        End If
                                                    End If

                                                    'Existe el producto en el maestro?
                                                    If Not BeProductoBodega Is Nothing Then

                                                        If L.Quantity <> L.Quantity_Received Then
                                                            If clsLnI_nav_ped_compra_det.Exist(BeI_nav_PedidoCompraDet, lConnection, lTransaction) Then
                                                                clsLnI_nav_ped_compra_det.Actualizar(BeI_nav_PedidoCompraDet, lConnection, lTransaction)
                                                                VContadorBitacoraIntermedia += 1
                                                            Else
                                                                clsLnI_nav_ped_compra_det.Insertar(BeI_nav_PedidoCompraDet, lConnection, lTransaction)
                                                                VContadorBitacoraIntermedia += 1
                                                            End If
                                                        Else
                                                            '#EJC20180503: No importar las líneas que ya fueron completadas.
                                                            lblprg.AppendText(String.Format("Producto: {0} no tiene dif. <Esperado = Recibido>", L.No, vbNewLine))
                                                            lblprg.AppendText(vbNewLine)
                                                            lblprg.Refresh()
                                                            lblprg.SelectionStart = lblprg.TextLength
                                                            lblprg.ScrollToCaret()
                                                        End If

                                                    Else

                                                        Try

                                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro",
                                                          L.No,
                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                           BeConfigDet.Idnavconfigdet, cnnLog)

                                                            lblprg.AppendText(String.Format("Producto no existe en maestro: {0}{1}", L.No, vbNewLine))
                                                            lblprg.AppendText(vbNewLine)
                                                            lblprg.Refresh()
                                                            lblprg.SelectionStart = lblprg.TextLength
                                                            lblprg.ScrollToCaret()

                                                        Catch ex As Exception
                                                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                                        End Try

                                                    End If 'Fin 'Existe el producto en el maestro?

                                                Else

                                                    Try

                                                        '#EJC20180614: Información no útil en log
                                                        'clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no pertenece a lista de bodegas válidas para recepción", L.No,
                                                        '   BeNavEjecucionEnc.Idejecucionenc,
                                                        '   BeConfigDet.Idnavconfigdet, cnnLog)

                                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("La bodega no está registrada como cliente y no es válida para recibir el producto. Prod: {0} Bod: {1}{2}", L.No, L.Location_Code, vbNewLine),
                                                                                L.No,
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet,
                                                                                cnnLog)

                                                        lblprg.AppendText(String.Format("La bodega no está registrada como cliente y no es válida para recibir el producto. Prod: {0} Bod: {1}{2}", L.No, L.Location_Code, vbNewLine))
                                                        lblprg.AppendText(vbNewLine)
                                                        lblprg.Refresh()
                                                        lblprg.SelectionStart = lblprg.TextLength
                                                        lblprg.ScrollToCaret()

                                                    Catch ex As Exception
                                                        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                                    End Try

                                                End If 'Fin Bodega_Es_Valida_Para_Recepcion

                                            Else

                                                If Not L.No Is Nothing Then

                                                    Try

                                                        clsLnI_nav_ejecucion_det_error.Inserta_Log("No está definida bodega para producto, no se importará", L.No,
                                                    BeNavEjecucionEnc.IdEjecucionEnc,
                                                    BeConfigDet.Idnavconfigdet, cnnLog)

                                                        lblprg.AppendText(String.Format("No está definida bodega para producto, no se importará: {0}{1}", L.No, vbNewLine))
                                                        lblprg.AppendText(vbNewLine)
                                                        lblprg.Refresh()
                                                        lblprg.SelectionStart = lblprg.TextLength
                                                        lblprg.ScrollToCaret()

                                                    Catch ex As Exception
                                                        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                                    End Try

                                                End If

                                            End If 'Fin location code is nothing                                        

                                        End If

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                "Sin informacion",
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet, cnnLog)

                                        lblprg.AppendText(String.Format("Error al insertar Linea desde el ws a intermedia en pedido de compra: {0}{1}{2}", BeI_nav_PedidoCompraDet.No, vbNewLine, ex.Message))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                    End Try

                                Next

                            Else
                                Console.WriteLine("Pedido de compra sin lineas de detalle?")
                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                      BeI_nav_PedidoCompra.No,
                                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                                      BeConfigDet.Idnavconfigdet, cnnLog)

                            lblprg.AppendText(String.Format("Error al insertar Encabezado OC desde ws a intermedia: {0}{1}{2}", BeI_nav_PedidoCompra.No, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                    End If

                Next


            End If

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.AppendText(vbNewLine)
            lblprg.ScrollToCaret()

            Importar_Pedidos_Compra_Desde_WSNav_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            lblprg.AppendText(String.Format("Error al insertar Ordenes de Compra desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
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

                            lblprg.AppendText(String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

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

    Public Function Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByRef lblprg As RichTextBox,
                                                                                   ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                   Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                   Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim DifCant As Double = 0

        Try

            Dim vMensajeREsultadoCUWMS As String = ""

            '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
            Dim wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                       .Credentials = CredencialesConexion
                                                      }

            wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Pedido compra") Then

                    lblprg.AppendText("La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

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

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText("Consultando WebService de bodega en: " & My.MySettings.Default.DynamicsNavInterface_WSPedidoCompra_Pedidos_Compra_Service)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Pedidos_Compra_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde WS?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Pedidos_Compra_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lPedidoCompraEnc As New List(Of clsBeI_nav_ped_compra_enc)
            lPedidoCompraEnc = clsLnI_nav_ped_compra_enc.GetAll(CnnInterface, lTransInterface, lblprg, prg)

            lblprg.AppendText(String.Format("Pedidos de Compra en tabla intermedia: {0}", lPedidoCompraEnc.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If lPedidoCompraEnc.Count > 0 Then

                Dim gBeOrdenCompra As clsBeTrans_oc_enc = Nothing
                Dim PedidoCompraExistente As clsBeTrans_oc_enc = Nothing
                Dim vContador As Integer = 0
                Dim vContadorLineasDet As Integer = 0
                Dim BeProveedorBodega As New clsBeProveedor_bodega
                Dim BeProductoBodega As New clsBeProducto_bodega
                Dim BePresentacion As New clsBeProducto_Presentacion

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                        CnnInterface, lTransInterface)

                If BeConfigEnc Is Nothing Then
                    If BD.Instancia.IdConfiguracionInterface = 0 Then
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                    Else
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                    End If
                End If

                prg.Maximum = lPedidoCompraEnc.Count

                prg.Value = 0

                VContadorBitacoraTomims = 0

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("********** TRASLADANDO DOCUMENTO A TOMWMS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Dim BeUnidadMedidaPedCompra As New clsBeUnidad_medida

                For Each navPedidoCompraEnc As clsBeI_nav_ped_compra_enc In lPedidoCompraEnc

                    If navPedidoCompraEnc.Status <> 0 Then

                        If Not navPedidoCompraEnc.No = "OP-00047234" Then
                            Debug.Print("ESPERA")
                            'Continue For
                        End If

                        lblprg.AppendText(String.Format("Procesando D.I.: {0} ", navPedidoCompraEnc.No, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        If navPedidoCompraEnc.No = "PC-096587" Then
                            Debug.Print("Hola")
                        End If

                        If navPedidoCompraEnc.No = "PC-098135" Then
                            Debug.Print("Hola")
                        End If

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
                            gBeOrdenCompra.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso 'P.C. REC NAV
                            gBeOrdenCompra.No_Documento = navPedidoCompraEnc.Vendor_Invoice_No
                            gBeOrdenCompra.User_Mod = BeConfigEnc.IdUsuario
                            gBeOrdenCompra.Fec_Mod = Now
                            gBeOrdenCompra.Procedencia = ""
                            gBeOrdenCompra.No_Marchamo = ""
                            gBeOrdenCompra.Referencia = navPedidoCompraEnc.No
                            gBeOrdenCompra.Observacion = navPedidoCompraEnc.Posting_Description
                            gBeOrdenCompra.Control_Poliza = False
                            gBeOrdenCompra.Push_To_NAV = True

                            If gBeOrdenCompra.IsNew Then
                                gBeOrdenCompra.ObjPoliza = Nothing
                            End If

                            clsLnTrans_oc_enc.Actualizar(gBeOrdenCompra, CnnInterface, lTransInterface)

                            lblprg.AppendText(String.Format("Procesando# : {0}{1}", navPedidoCompraEnc.No, vbNewLine))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            VContadorBitacoraTomims += 1

                            If navPedidoCompraEnc.Lineas_Detalle.Count > 0 Then

                                Dim BePedidoCompraDet As New clsBeTrans_oc_det

                                For Each navPedidoCompraDet As clsBeI_nav_ped_compra_det In navPedidoCompraEnc.Lineas_Detalle

                                    vContadorLineasDet += 1

                                    Try

                                        BeProductoBodega = clsLnProducto_bodega.Existe(navPedidoCompraDet.No,
                                                                                       BeConfigEnc.Idbodega,
                                                                                       CnnInterface, lTransInterface)

                                        'Existe el producto en el maestro?
                                        If BeProductoBodega IsNot Nothing Then

                                            'Existe el producto en el detalle de la orden de compra en la tabla DE TOMWMS?
                                            BePedidoCompraDet = clsLnTrans_oc_det.Exist(PedidoCompraExistente.IdOrdenCompraEnc,
                                                                                        navPedidoCompraDet.Line_No,
                                                                                        CnnInterface,
                                                                                        lTransInterface)

                                            '#CKFK 20180725 17:45 coloqué esto en comentario, porque la instancia BeUnidadMedidaPedCompra era nothing y
                                            'no se le podía asignar valor a la property Nombre
                                            'BeUnidadMedidaPedCompra.Nombre = navPedidoCompraDet.Unit_of_Measure_Code
                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Nombre(navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                                          CnnInterface,
                                                                                                          lTransInterface)

                                            If BeUnidadMedidaPedCompra Is Nothing Then
                                                BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                                                                BeConfigEnc.IdPropietario,
                                                                                                                                CnnInterface,
                                                                                                                                lTransInterface)

                                            End If

                                            'La unidad de medida existe?

                                            If BeUnidadMedidaPedCompra Is Nothing Then
                                                'unidad de medida no existe en tabla UNIDAD_MEDIDA
                                                Throw New Exception(
                                                String.Format("Producto: {0} UnidMedBas {1} No existe ",
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
                                                    Throw New Exception("ERROR_202303031404E: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)
                                                End If 'Fin sí: BePresentacion IsNothing (Presentación no existe y se insertó)

                                            Else
                                                BePresentacion = Nothing
                                            End If 'Fin sí: Cod_Variante <> ""

#End Region

                                            'Producto No existe en la tabla de detalle DE TOMWMS. trans_oc_det
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

                                                    BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description)
                                                    BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
                                                    BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity

                                                    '#EJC20220420: Hotfix, actualizar solo si lo recibido en el ERP es mayor que lo que tiene WMS.
                                                    If (navPedidoCompraDet.Quantity_Received > BePedidoCompraDet.Cantidad_recibida) Then
                                                        BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
                                                    End If

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

                                                        clsLnTrans_oc_det.Insertar(BePedidoCompraDet,
                                                                                   CnnInterface,
                                                                                   lTransInterface)

                                                        VContadorBitacoraTomims += 1

                                                    End If

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               BePedidoCompraDet.Nombre_producto,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                                                    lblprg.AppendText(String.Format("Error al insertar Detalle en : {0}{1}", ex.Message, vbNewLine))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.Refresh()
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()

                                                End Try

                                            Else 'Producto sí existe en tabla trans_oc_det

                                                Try

                                                    BePedidoCompraDet.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                    BePedidoCompraDet.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                    BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                    BePedidoCompraDet.Codigo_Producto = navPedidoCompraDet.No
                                                    BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description)
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

                                                    clsLnTrans_oc_det.Actualizar_Desde_Interface(BePedidoCompraDet,
                                                                                                 CnnInterface,
                                                                                                 lTransInterface)

                                                    VContadorBitacoraTomims += 1

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               BePedidoCompraDet.Nombre_producto,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                                                    lblprg.AppendText(String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.Refresh()
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()

                                                End Try

                                            End If

                                        End If 'Fin sí: producto existe.

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                        "Pedido Sin Detalle",
                                                                         BeNavEjecucionEnc.IdEjecucionEnc,
                                                                         BeConfigDet.Idnavconfigdet, CnnLog)

                                        lblprg.AppendText(String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

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
                                '#CKFK20220114 Se va a validar si el navPedidoCompraEnc.Product_Owner_Code es vacío para mandar 1 en caso contrario enviaremos el navPedidoCompraEnc.Product_Owner_Code
                                gBeOrdenCompra.PropietarioBodega.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega,
                                                                                                                                IIf(navPedidoCompraEnc.Product_Owner_Code = "", 1, navPedidoCompraEnc.Product_Owner_Code),
                                                                                                                                CnnInterface, lTransInterface)
                                gBeOrdenCompra.IdEstadoOC = 1
                                gBeOrdenCompra.Hora_Creacion = Now
                                gBeOrdenCompra.User_Agr = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Agr = Now
                                gBeOrdenCompra.Fecha_Creacion = Now
                                gBeOrdenCompra.Activo = True

                                BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                                           BeConfigEnc.Idbodega,
                                                                                                           CnnInterface, lTransInterface)

                                If BeProveedorBodega Is Nothing Then

                                    BeProveedorBodega = clsSyncNavProveedor.Insertar_Proveedor_Single(navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                                      CnnInterface,
                                                                                                      lTransInterface,
                                                                                                      CnnLog,
                                                                                                      lblprg,
                                                                                                      prg)

                                    If BeProveedorBodega Is Nothing Then

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(
                                        String.Format("El proveedor: {0} no existe, no se puede importar el pedido de compra: {1}",
                                                  navPedidoCompraEnc.Buy_From_Vendor_No,
                                                  navPedidoCompraEnc.No),
                                                  navPedidoCompraEnc.Buy_From_Vendor_No,
                                                  BeNavEjecucionEnc.IdEjecucionEnc,
                                                  BeConfigDet.Idnavconfigdet, CnnLog)

                                        lblprg.AppendText(String.Format("Error al insertar el pedido de compra: {0} El proveedor: {1} no existe, ¿Ya se actualizó maestro de proveedores?", navPedidoCompraEnc.Buy_From_Vendor_No, navPedidoCompraEnc.No, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        Throw New Exception("No logramos insertar el proveedor asociado a un pedido de compra, lamentamos el inconveniente")

                                    Else

                                        lblprg.AppendText(String.Format("El proveedor: {1} no existía pero se insertó para el pedido de compra: {0}. Nada de que preocuparse :) ", navPedidoCompraEnc.No, navPedidoCompraEnc.Buy_From_Vendor_No, vbNewLine))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                    End If

                                End If

                                If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                                    gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega
                                End If

                                gBeOrdenCompra.IdProveedorBodega = BeProveedorBodega.IdAsignacion
                                gBeOrdenCompra.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso 'P.C. REC NAV
                                gBeOrdenCompra.No_Documento = navPedidoCompraEnc.Vendor_Invoice_No
                                gBeOrdenCompra.User_Mod = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Mod = Now
                                gBeOrdenCompra.Procedencia = ""
                                gBeOrdenCompra.No_Marchamo = ""
                                gBeOrdenCompra.Referencia = navPedidoCompraEnc.No
                                gBeOrdenCompra.Observacion = navPedidoCompraEnc.Posting_Description
                                gBeOrdenCompra.Control_Poliza = False
                                gBeOrdenCompra.IdBodega = BeConfigEnc.Idbodega
                                gBeOrdenCompra.Push_To_NAV = True

                                If gBeOrdenCompra.IsNew Then
                                    gBeOrdenCompra.ObjPoliza = Nothing
                                End If

                                gBeOrdenCompra.Enviado_A_ERP = False

                                Try

                                    '#EJC20210428: Crear primero la recepción de almacen en NAV, antes de insertar en DOC
                                    'en WMS.
                                    '#EJC20210426: En esta variable se asigna el número de documento de rececpción de NAV.

                                    If BeConfigEnc.Crear_Recepcion_De_Compra_NAV Then

                                        vMensajeREsultadoCUWMS = ""

                                        wsCUWMS.Url = My.Settings.NavSync_CUWMS_CUWMS
                                        wsCUWMS.CreatePurchaseReceipt(navPedidoCompraEnc.No, vMensajeREsultadoCUWMS)

                                        Dim vUrlRecepcionAlmacen As String = My.MySettings.Default.NavSync_WSRecepcionesAlm_Recep_Almacen_Service

                                        Dim ws3 As New Recep_Almacen_Service() With
                                        {
                                            .UseDefaultCredentials = False,
                                            .Credentials = CredencialesConexion,
                                            .Url = vUrlRecepcionAlmacen
                                        }

                                        Dim RecepcionAlmacen As New Recep_Almacen()
                                        RecepcionAlmacen = ws3.Read(vMensajeREsultadoCUWMS)

                                        '#EJC20210324: Modificar cantidad a tomar/colocar 0, para que se pueda recibir parcial en HH.
                                        For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines
                                            Lu.Qty_to_Receive = 0
                                        Next

                                        '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                                        ws3.Update(RecepcionAlmacen)

                                        If gBeOrdenCompra.No_Documento.Trim = "" AndAlso vMensajeREsultadoCUWMS <> "" Then
                                            gBeOrdenCompra.No_Documento = vMensajeREsultadoCUWMS
                                        End If

                                        If vMensajeREsultadoCUWMS <> "" Then
                                            gBeOrdenCompra.No_Documento_Recepcion_ERP = vMensajeREsultadoCUWMS
                                        End If

                                        If vMensajeREsultadoCUWMS <> "" Then

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Se creó la recepción del documento de ingreso: " & vMensajeREsultadoCUWMS,
                                                                                navPedidoCompraEnc.No,
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet, CnnLog)

                                            lblprg.AppendText(String.Format("Se creó la recepción del documento de ingreso: {0} para el documento: {1} {2}", vMensajeREsultadoCUWMS, navPedidoCompraEnc.No, vbNewLine))
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        Else

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("NO se creó la recepción del documento de ingreso: " & vMensajeREsultadoCUWMS & ". La interface no reportó núermo de documento",
                                                                                navPedidoCompraEnc.No,
                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                BeConfigDet.Idnavconfigdet, CnnLog)

                                            'La interface de NAV no notificó número de documento para la recepción de almacén
                                            lblprg.AppendText(String.Format("No se creó la recepción: {0} para el documento: {1} {2}.", vMensajeREsultadoCUWMS, navPedidoCompraEnc.No, vbNewLine))
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        End If


                                    End If

                                    clsLnTrans_oc_enc.Insertar(gBeOrdenCompra, CnnInterface, lTransInterface)

                                    VContadorBitacoraTomims += 1

                                    If navPedidoCompraEnc.Lineas_Detalle.Count > 0 Then

                                        For Each navPedidoCompraDet As clsBeI_nav_ped_compra_det In navPedidoCompraEnc.Lineas_Detalle

                                            vContadorLineasDet += 1

                                            Dim BePedidoCompraDet As New clsBeTrans_oc_det() With {.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc,
                                                                                                   .IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompra.IdOrdenCompraEnc,
                                                                                                                                               CnnInterface, lTransInterface) + 1}

                                            '#20180101_1203:Línea agregada para actulización en envío.
                                            'BePedidoCompraDet.No_Linea = navPedidoCompraDet.No

                                            BeProductoBodega = clsLnProducto_bodega.Existe(navPedidoCompraDet.No,
                                                                                           BeConfigEnc.Idbodega,
                                                                                           CnnInterface,
                                                                                           lTransInterface)

                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                                                            BeConfigEnc.IdPropietario,
                                                                                                                            CnnInterface,
                                                                                                                            lTransInterface)

                                            If BeUnidadMedidaPedCompra Is Nothing Then

                                                BeUnidadMedidaPedCompra = clsLnUnidad_medida.GetSingle(clsLnProducto.Get_Id_Unidad_Medida_By_Codigo(navPedidoCompraDet.No,
                                                                                                                                                    CnnInterface,
                                                                                                                                                    lTransInterface))

                                                '#CKFK20220204 Preguntarle a Erik si aquí debo asignarle al VariantCode la Unit_of_Measure_Code porque es la presentación 
                                            End If



#Region "COD_VARIANTE_A_PRESENTACION"

                                            BePresentacion = Nothing

                                            If navPedidoCompraDet.Variant_Code <> "" Then

                                                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                                                                                                        navPedidoCompraDet.Variant_Code,
                                                                                                                                        CnnInterface,
                                                                                                                                        lTransInterface)

                                                If BePresentacion Is Nothing Then
                                                    Throw New Exception("ERROR_202303031404F: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)
                                                End If

                                            End If

#End Region

                                            If BeProductoBodega IsNot Nothing Then

                                                Try

                                                    BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
                                                    BePedidoCompraDet.Codigo_Producto = navPedidoCompraDet.No
                                                    BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description)
                                                    BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
                                                    BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity

                                                    '#EJC20220420: Hotfix, actualizar solo si lo recibido en el ERP es mayor que lo que tiene WMS.
                                                    If (navPedidoCompraDet.Quantity_Received > BePedidoCompraDet.Cantidad_recibida) Then
                                                        BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
                                                    End If

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

                                                        clsLnTrans_oc_det.Insertar(BePedidoCompraDet,
                                                                                   CnnInterface,
                                                                                   lTransInterface)

                                                        VContadorBitacoraTomims += 1

                                                    End If

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               BePedidoCompraDet.Nombre_producto,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet,
                                                                                               CnnLog)

                                                    lblprg.AppendText(String.Format("Error al insertar desde ws a intermedia: {0}{1}{2}", BePedidoCompraDet.Nombre_producto, ex.Message, vbNewLine))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.Refresh()
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()

                                                End Try

                                            Else

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro ",
                                                                                            navPedidoCompraDet.No,
                                                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                            BeConfigDet.Idnavconfigdet,
                                                                                            CnnLog)

                                                lblprg.AppendText(String.Format("No existe Producto Bodega: {0}{1}", navPedidoCompraDet.No, vbNewLine))
                                                lblprg.AppendText(vbNewLine)
                                                lblprg.Refresh()
                                                lblprg.SelectionStart = lblprg.TextLength
                                                lblprg.ScrollToCaret()

                                            End If

                                        Next

                                    End If


                                    '#EJC20230124: Actualizar a importado (creo que 2 es otra cosa)
                                    navPedidoCompraEnc.Status = 3 'Importado

                                    clsLnI_nav_ped_compra_enc.Actualizar_Estado(navPedidoCompraEnc, CnnInterface, lTransInterface)

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               navPedidoCompraEnc.No,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                    lblprg.AppendText(String.Format("Error al insertar la OC con Referencia: {0}{1}{2}", navPedidoCompraEnc.No, vbNewLine, ex.Message))
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                End Try

                                Application.DoEvents()

                            End If

                        End If

                    Else

                        lblprg.AppendText(String.Format("OC Inactiva {0} ", navPedidoCompraEnc.No, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                Next

            End If

            lTransInterface.Commit()

            lblprg.AppendText(vbNewLine)
            '#EJC20171107_REF04_0250AM: Desplegar cantidad de registros de pedidos de compra procesados
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TOMWMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Pedidos de compra procesados  correctamente: {0}", VContadorBitacoraTomims))
            lblprg.AppendText(vbNewLine)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTomims

            If VContadorBitacoraIntermedia = VContadorBitacoraTomims Then
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

            lblprg.AppendText(String.Format("Error al insertar pedido de compra a tabla DE TOMWMS: {0} {1}", ex.Message, vbNewLine))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Public Function Get_Pedidos_Compra_FromWS(ByVal lblprg As RichTextBox, Optional ByVal AplicarFiltros As Boolean = True) As List(Of Pedidos_Compra)

        Try

            Dim lPedidosCompra As New List(Of Pedidos_Compra)
            Dim StartDate As String = "01092021D"

            If AplicarFiltros Then

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("********** APLICANDO FILTROS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

                Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
                lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Pedido_Compra)

                Dim vCriteria As String = ""
                Dim vContador As Integer = 0

                For Each FiltroCategoria In lFiltros


                    If FiltroCategoria.Tipo_Filtro = "" OrElse FiltroCategoria.Tipo_Filtro = "BODEGA" Then

                        If vContador = 0 Then
                            vCriteria = FiltroCategoria.Valor
                        Else
                            vCriteria += "|" & FiltroCategoria.Valor
                        End If

                    ElseIf FiltroCategoria.Tipo_Filtro = "FECHA_INICIO" Then
                        StartDate = FiltroCategoria.Valor
                    End If

                    vContador += 1

                Next

                If vCriteria <> "" AndAlso pBodega <> "" Then
                    If pBodega <> vCriteria AndAlso Not vCriteria.Contains(pBodega) Then
                        Throw New Exception(String.Format("La Bodega del filtro: {0} no se corresponde con la Bodega de la interface: {1}", vCriteria, pBodega))
                    End If
                End If

                Dim vFiltro1 As New Pedidos_Compra_Filter() With {.Field = Pedidos_Compra_Fields.Location_Code, .Criteria = vCriteria}

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("-Location_Code-")
                lblprg.AppendText("Criteria: " & vCriteria)
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

                Dim vFiltro2 As New Pedidos_Compra_Filter() With {.Field = Pedidos_Compra_Fields.Status, .Criteria = "1"}

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("-Status-")
                lblprg.AppendText("Criteria: 1")
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()


                Dim vFiltro3 As New Pedidos_Compra_Filter() With {.Field = Pedidos_Compra_Fields.Posting_Date, .Criteria = StartDate} '"01/03/2021.."

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("-Posting_Date-")
                lblprg.AppendText("Criteria: " & StartDate)
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

                Dim vFiltros As Pedidos_Compra_Filter() = New Pedidos_Compra_Filter() {vFiltro1, vFiltro2, vFiltro3}

                wsPedidoCompraService.Url = My.Settings.DynamicsNavInterface_WSPedidoCompra_Pedidos_Compra_Service

                fichaPedidosCompra = wsPedidoCompraService.ReadMultiple(vFiltros, Nothing, 500)

                For Each PC As Pedidos_Compra In fichaPedidosCompra
                    lPedidosCompra.Add(PC)
                Next

            Else

                fichaPedidosCompra = wsPedidoCompraService.ReadMultiple(Nothing, Nothing, 1000)

                For Each PC As Pedidos_Compra In fichaPedidosCompra
                    lPedidosCompra.Add(PC)
                Next

            End If

            Return lPedidosCompra

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Sub Enviar_Transacciones_De_Ingreso(ByRef lblprg As RichTextBox,
                                               ByRef prg As System.Windows.Forms.ProgressBar,
                                               ByVal pIdBodega As Integer)

        Dim lTransaccionesIngreso As New List(Of clsBeI_nav_transacciones_out)
        Dim TipoPedidoCompra As Integer = 0
        Dim Enviado_A_Erp As Boolean = False
        Dim WhseReceiptNo As String = ""
        Dim PutAwayHeaderNo As String = ""
        Dim vResultado As String = ""
        Dim vCodigoBodega As String = ""

        '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
        Dim wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                       .Credentials = CredencialesConexion
                                                      }

        wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

        Dim wsBodegaService As New Ficha_Bodegas_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

        wsBodegaService.Url = My.Settings.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service
        wsRegistraRecepcionPedidoCompra.Url = My.Settings.DynamicsNavInterface_WSRegistraRecepcionCompra_Registra_Recepcion_Compra

        Try

            lTransaccionesIngreso = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio_By_IdBodega(pIdBodega)

            If Not lTransaccionesIngreso Is Nothing AndAlso lTransaccionesIngreso.Count > 0 Then

                Dim ListaPedidosCompra = (From i In lTransaccionesIngreso
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idordencompra, Key i.Idrecepcionenc, Key i.Idbodega} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idordencompra, Key Keys.Idrecepcionenc, Key Keys.Idbodega})

                clsPublic.Actualizar_Progreso(lblprg, "Transacciones a enviar: {0}", lTransaccionesIngreso.Count)

                Dim BeReOC As New clsBeTrans_re_oc
                Dim BeTipoDocumento As New clsBeTrans_oc_ti()

                For Each PC In ListaPedidosCompra

                    TipoPedidoCompra = clsLnTrans_oc_enc.Get_Tipo_Documento(PC.No_pedido)

                    clsPublic.Actualizar_Progreso(lblprg, "Procesando documento: {0}", PC.No_pedido)

                    BeReOC = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(PC.Idordencompra, PC.Idrecepcionenc)

                    If BeReOC IsNot Nothing Then
                        Enviado_A_Erp = (BeReOC.No_docto <> "")
                    Else
                        Enviado_A_Erp = False
                    End If

                    vCodigoBodega = clsLnBodega.Get_Codigo_By_IdBodega(PC.Idbodega)

                    Dim beBodega As Ficha_Bodegas
                    beBodega = wsBodegaService.Read(vCodigoBodega)

                    Select Case TipoPedidoCompra

                        Case 1 'Es un pedido de compra de proveedor.

                            If Not Enviado_A_Erp Then

                                Try

                                    If beBodega.Require_Receive Then

                                        wsCUWMS.CreatePurchaseReceipt(PC.No_pedido, WhseReceiptNo)

                                    End If

                                Catch ex As Exception

                                    lblprg.AppendText(String.Format("Error al ejecutar CreatePurchaseReceipt para documento: {0} en el CodeUnit. El error es: {1}", PC.No_pedido, ex.Message))
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al ejecutar CreatePurchaseReceipt para documento: {0} en el CodeUnit. El error es: {1}", PC.No_pedido, ex.Message),
                                      PC.No_pedido,
                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                      BeConfigDet.Idnavconfigdet)

                                    Throw ex

                                End Try


                                If Enviar_Lotes_Ingreso(PC.No_pedido,
                                                        lTransaccionesIngreso,
                                                        lblprg, prg) Then

                                    If Enviar_Cantidades_Ingreso(PC.No_pedido,
                                                                 lTransaccionesIngreso,
                                                                 lblprg,
                                                                 prg) Then

                                        If Enviar_Cantidades_No_Ingreso(PC.No_pedido,
                                                                        PC.Idordencompra,
                                                                        PC.Idrecepcionenc,
                                                                        lTransaccionesIngreso,
                                                                        lblprg,
                                                                        prg) Then

                                            Try

                                                If Not beBodega.Require_Receive Then '#EJC20210927: Si no es almacén avanzado, se registra el P.C. (BA01/BA24)
                                                    wsRegistraRecepcionPedidoCompra.RegistrarRecCompra(PC.No_pedido)
                                                Else
                                                    wsCUWMS.PostWhseReceipt(WhseReceiptNo, PutAwayHeaderNo, PC.No_pedido)
                                                    wsCUWMS.RegisterPutAway(PutAwayHeaderNo, vResultado)
                                                End If

                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Documento registrado correctamente: {0}", BeReOC.OC.No_Documento))

                                                BeReOC.No_docto = "ENV-" & FormatoFechas.tFecha(Now) & " - " & PutAwayHeaderNo & " - " & WhseReceiptNo

                                                '#EJC20200328: Se creó función para actualizar unicamente el campo No_Docto 
                                                'Anteriormente se actualizaba todo el objeto y modificaba fechas que no debía.
                                                clsLnTrans_re_oc.Actualizar_No_Docto(BeReOC)

                                                clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(PC.Idordencompra, True)

                                            Catch ex As Exception

                                                If ex.Message = "There is nothing to post." Then 'Pedido sin nada que registrar
                                                    lblprg.AppendText(String.Format("Nada que registrar para pedido: {0} en NAV.", PC.No_pedido))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.Refresh()
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()
                                                Else
                                                    lblprg.AppendText(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.No_pedido, ex.Message))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.Refresh()
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.No_pedido, ex.Message),
                                                      PC.No_pedido,
                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                      BeConfigDet.Idnavconfigdet)

                                                End If

                                            End Try

                                        End If ' Fin enviar cantidades 0 para productos no recibidos

                                    End If

                                End If 'Fin enviar lotes

                            Else

                                Try

                                    If Enviar_Cantidades_No_Ingreso(PC.No_pedido,
                                                                    PC.Idordencompra,
                                                                    PC.Idrecepcionenc,
                                                                    lTransaccionesIngreso,
                                                                    lblprg,
                                                                    prg) Then

                                        Try

                                            If Not beBodega.Require_Receive Then '#EJC20210927: Si no es almacén avanzado, se registra el P.C. (BA01/BA24)
                                                wsRegistraRecepcionPedidoCompra.RegistrarRecCompra(PC.No_pedido)
                                            Else
                                                wsCUWMS.PostWhseReceipt(WhseReceiptNo, PutAwayHeaderNo, PC.No_pedido)
                                                wsCUWMS.RegisterPutAway(PutAwayHeaderNo, vResultado)
                                            End If

                                            clsLnTrans_oc_enc.Actualizar_Estado_OC_By_Interface(PC.Idordencompra, 6)

                                            lblprg.AppendText(String.Format("Se registró el pedido:{0} correctamente en el ERP.", PC.No_pedido))
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                            clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(PC.Idordencompra, True)

                                        Catch ex As Exception

                                            If ex.Message = "There is nothing to post." Then 'Pedido sin nada que registrar

                                                lblprg.AppendText(String.Format("Nada que registrar para pedido: {0} en NAV.", PC.No_pedido))
                                                lblprg.AppendText(vbNewLine)
                                                lblprg.Refresh()
                                                lblprg.SelectionStart = lblprg.TextLength
                                                lblprg.ScrollToCaret()

                                            Else

                                                lblprg.AppendText(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.No_pedido, ex.Message))
                                                lblprg.AppendText(vbNewLine)
                                                lblprg.Refresh()
                                                lblprg.SelectionStart = lblprg.TextLength
                                                lblprg.ScrollToCaret()

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.No_pedido, ex.Message),
                                                                                          PC.No_pedido,
                                                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                          BeConfigDet.Idnavconfigdet)

                                            End If

                                        End Try

                                    End If

                                Catch ex As Exception

                                    If Not ex.Message = "There is nothing to post." Then 'Pedido sin nada que registrar
                                        lblprg.AppendText(String.Format("Nada que registrar para pedido: {0} en NAV.", PC.No_pedido))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    Else
                                        lblprg.AppendText(String.Format("Error al enviar pedido a NAV: {0}", ex.Message))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    End If

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar pedido a NAV: {0}", ex.Message),
                                      PC.No_pedido,
                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                      BeConfigDet.Idnavconfigdet)

                                End Try

                            End If

                        Case 3, 8 'Es un pedido de transferencia desde una bodega X hacia la bodega de WMS.
                            '#CKFK20230903 Cambié el documento 2 por el 3, la version anterior de WMS usaba el dos

                            lblprg.AppendText(String.Format("Es un documento de Ingreso de tipo {0}", TipoPedidoCompra))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            If Not Enviado_A_Erp Then

                                Try

                                    If beBodega.Require_Receive Then
                                        wsCUWMS.CreateTransferReceipt(PC.No_pedido, WhseReceiptNo)
                                    End If

                                Catch ex As Exception

                                    lblprg.AppendText(String.Format("Error al ejecutar CreatePurchaseReceipt para documento: {0} en el CodeUnit. El error es: {1}", PC.No_pedido, ex.Message))
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al ejecutar CreatePurchaseReceipt para documento: {0} en el CodeUnit. El error es: {1}", PC.No_pedido, ex.Message),
                                      PC.No_pedido,
                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                      BeConfigDet.Idnavconfigdet)

                                    Throw ex

                                End Try

                                If Enviar_Cantidades_Ingreso(PC.No_pedido, lTransaccionesIngreso, True, lblprg, prg) Then

                                    Try

                                        lblprg.AppendText(String.Format("Enviando cantidades para documento: {0}", PC.No_pedido))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        If Not beBodega.Require_Receive Then '#EJC20210927: Si no es almacén avanzado, se registra el P.C. (BA01/BA24)
                                            WsRegistra_Transfer_Recepcion.RegistrarRecepTransfer(PC.No_pedido)
                                        Else
                                            wsCUWMS.PostWhseReceipt(WhseReceiptNo, PutAwayHeaderNo, PC.No_pedido)
                                            wsCUWMS.RegisterPutAway(PutAwayHeaderNo, vResultado)
                                        End If

                                        clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(PC.Idordencompra, True)

                                        For Each T In lTransaccionesIngreso.Where(Function(x) x.No_pedido = PC.No_pedido)
                                            T.Enviado = True
                                        Next

                                        Dim lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)

                                        lista_A_Actualizar = lTransaccionesIngreso.Where(Function(x) x.No_pedido = PC.No_pedido _
                                                                                         AndAlso x.Enviado = True).ToList()

                                        If Not lista_A_Actualizar Is Nothing Then

                                            For Each T In lista_A_Actualizar
                                                clsLnI_nav_transacciones_out.Actualizar(T)
                                            Next
                                        End If

                                        lblprg.AppendText(String.Format("Transacciones de ingreso enviadas correctamente: {0}", lTransaccionesIngreso.Count))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                    Catch ex As Exception
                                        lblprg.AppendText(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.No_pedido, ex.Message))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.No_pedido, ex.Message),
                                        PC.No_pedido,
                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                        BeConfigDet.Idnavconfigdet)

                                    End Try

                                End If

                            End If


                    End Select

                Next

            Else

                '#EJC20180521: Tratar de registrar pedidos de ingreso que no se registraron en NAV.
                Dim lTransOcPendienteRegistroEnNav As New List(Of clsBeTrans_oc_enc)
                lTransOcPendienteRegistroEnNav = clsLnTrans_oc_enc.Get_All_Pendiente_Registro_MI3()

                If lTransOcPendienteRegistroEnNav.Count > 0 Then

                    For Each PC In lTransOcPendienteRegistroEnNav

                        'Cerrada ó en backorder?
                        If PC.IdEstadoOC = 4 OrElse PC.IdEstadoOC = 6 Then

                            'Transf de recepción o pedido de compra?
                            TipoPedidoCompra = clsLnTrans_oc_enc.Get_Tipo_Documento(PC.Referencia)

                            lblprg.AppendText(String.Format("Procesando documento: {0}", PC.Referencia))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            If PC.Referencia = "PC-043933" Then
                                Debug.Print("Espera")
                            Else
                                Debug.Print("Sigue")
                            End If

                            Select Case TipoPedidoCompra

                                Case 1 'Es un pedido de compra de proveedor.

                                    'lTransaccionesIngreso = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio()

                                    Dim lRecs As New List(Of clsBeTrans_re_enc)
                                    lRecs = clsLnTrans_re_enc.Get_All_By_IdOrdenRecEnc(PC.IdOrdenCompraEnc)

                                    prg.Maximum = lRecs.Count

                                    lblprg.Text = ""

                                    If Enviar_Cantidades_No_Ingreso(PC.Referencia, PC.IdOrdenCompraEnc, lRecs(0).IdRecepcionEnc, lTransaccionesIngreso, lblprg, prg) Then

                                    End If

                                    Try

                                        wsRegistraRecepcionPedidoCompra.RegistrarRecCompra(PC.Referencia)

                                        lblprg.AppendText(String.Format("Se registró el pedido:{0} correctamente en el ERP.", PC.Referencia))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(PC.IdOrdenCompraEnc, True)

                                    Catch ex As Exception
                                        lblprg.AppendText(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.Referencia, ex.Message))
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar pedido {0} en NAV: {1}", PC.Referencia, ex.Message),
                                        PC.Referencia,
                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                        BeConfigDet.Idnavconfigdet)
                                    End Try

                                Case 2 'Es un pedido de transferencia desde una bodega X hacia la bodega de WMS.

                                    WsRegistra_Transfer_Recepcion.RegistrarRecepTransfer(PC.Referencia)

                                    clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(PC.IdOrdenCompraEnc, True)

                            End Select

                        Else

                            lblprg.AppendText(String.Format("No se pudo procesar el documento: {0}", PC.Referencia))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If

                    Next

                End If

                lblprg.AppendText("No hay transacciones para enviar.")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
        End Try

    End Sub

    '#EJC20180111: Enviar los lotes primero, luego las cantidades agrupadas y sumadas por producto.
    Private Function Enviar_Lotes_Ingreso(ByVal NoPedidoCompra As String,
                                          ByRef lTransaccionesIngreso As List(Of clsBeI_nav_transacciones_out),
                                          ByRef lblprg As RichTextBox,
                                          ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Enviar_Lotes_Ingreso = False

        Try

            prg.Maximum = lTransaccionesIngreso.Count
            prg.Visible = True

            Dim vContador As Integer = 0
            Dim BePresentacion As New clsBeProducto_Presentacion
            Dim vCantidad As Double = 0
            Dim vUnidMed As String = ""

            Dim listaLotes = lTransaccionesIngreso.Where(Function(x) x.No_pedido = NoPedidoCompra).ToList()

            wsLotePedidoCompra = New Lote_PedidoCompra With
                {
                    .UseDefaultCredentials = UsarCredencialesPorDefecto,
                    .Credentials = CredencialesConexion
                }

            wsLotePedidoCompra.Url = My.Settings.DynamicsNavInterface_WSNavLotePedidoCompra_Lote_PedidoCompra

            If Not listaLotes Is Nothing Then

                For Each I In listaLotes

                    lblprg.AppendText(String.Format("Procesando Lote de Pedido: {0} Línea:{1} ", I.No_pedido, I.No_linea))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Try

                        If Not I.Idpresentacion = 0 Then

                            '#EJC20180418: Enviar la cantidad en UMBAS.
                            BePresentacion.IdPresentacion = I.Idpresentacion
                            clsLnProducto_presentacion.GetSingle(BePresentacion)
                            vCantidad = I.Cantidad * BePresentacion.Factor

                            'Enviar a Nav el nombre/codigo de la unidad de medida básica.
                            vUnidMed = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(I.Idunidadmedida)

                        Else
                            vCantidad = I.Cantidad
                            vUnidMed = I.Unidad_medida
                        End If

                        '#EJC20180503: Enviar siempre UMBAS en Enviar_Lotes_Ingreso.
                        wsLotePedidoCompra.LoteLineaPedidoCompra(I.No_pedido,
                                                                 I.No_linea,
                                                                 I.Codigo_producto,
                                                                 I.Codigo_variante,
                                                                 vUnidMed,
                                                                 Math.Round(vCantidad, 5),
                                                                 I.Lote,
                                                                 I.Fecha_vence)

                        vContador += 1

                        prg.Value = vContador

                        I.Enviado = 1

                    Catch ex As Exception

                        '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al ejecutar WebService LoteLineaPedidoCompra: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)

                        lblprg.AppendText(String.Format("Error al ejecutar WebService LoteLineaPedidoCompra: {0} {1}", ex.Message, vbNewLine))
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End Try

                Next

                Enviar_Lotes_Ingreso = True

            Else
                lblprg.AppendText(String.Format("No se encontraron lotes para el pedido de compra: {0} ", NoPedidoCompra))
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#EJC20180111: Enviar las cantidades sumadas y agrupadas por producto (sin considerar el lote)
    Private Function Enviar_Cantidades_Ingreso(ByVal NoPedidoCompra As String,
                                               ByRef lTransaccionesIngreso As List(Of clsBeI_nav_transacciones_out),
                                               ByRef lblprg As RichTextBox,
                                               ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Enviar_Cantidades_Ingreso = False

        Try

            prg.Maximum = lTransaccionesIngreso.Count
            prg.Visible = True

            Dim vContador As Integer = 0

            Dim ListaResumen = (From i In lTransaccionesIngreso.Where(Function(x) x.No_pedido = NoPedidoCompra)
                                Group i By Keys = New With {Key i.No_pedido, Key i.No_linea,
                                Key i.Codigo_producto, Key i.Codigo_variante, Key i.Enviado} Into Group
                                Select New With {Keys.No_pedido, Keys.No_linea,
                                                 Keys.Codigo_producto,
                                                 Keys.Codigo_variante,
                                                 Keys.Enviado,
                                                 .Cantidad = Group.Sum(Function(x) x.Cantidad)})

            Dim lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)

            If Not ListaResumen Is Nothing Then

                If ListaResumen.Count > 0 Then

                    wsCantidadPedidoCompra.Url = My.Settings.DynamicsNavInterface_WSNavCantidadRecibirPedidoCompra_CantidadRecibir_PedidoCompra

                    For Each I In ListaResumen

                        lblprg.AppendText(String.Format("Procesando Cantidad de Pedido: {0} Línea:{1} ", I.No_pedido, I.No_linea))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        Try

                            wsCantidadPedidoCompra.LineaPedidoCompraCantRec(I.No_pedido,
                                                                           I.No_linea,
                                                                           I.Codigo_producto,
                                                                           I.Codigo_variante,
                                                                           Math.Round(I.Cantidad, 5))

                            lista_A_Actualizar = lTransaccionesIngreso.Where(Function(x) x.No_pedido = I.No_pedido AndAlso
                                x.No_linea = I.No_linea AndAlso x.Codigo_producto = I.Codigo_producto AndAlso
                                x.Codigo_variante = I.Codigo_variante AndAlso x.Enviado = True).ToList()

                            If Not lista_A_Actualizar Is Nothing Then
                                For Each T In lista_A_Actualizar
                                    clsLnI_nav_transacciones_out.Actualizar(T)
                                Next
                            End If

                            vContador += 1

                            prg.Value = vContador

                        Catch ex As Exception

                            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                              "",
                                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                                              BeConfigDet.Idnavconfigdet)

                            lblprg.AppendText(String.Format("Error al enviar lote de pedido de compra a Nav desde WS: {0} {1}", ex.Message, vbNewLine))
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                    Next

                    'Actualizar Enviado = True
                    lTransaccionesIngreso.Where(Function(x) x.No_pedido = NoPedidoCompra).FirstOrDefault.Enviado = True

                End If 'ListaResumen.Count > 0

            End If  'Lista not is nothing          

            Enviar_Cantidades_Ingreso = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Private Function Enviar_Cantidades_No_Ingreso(ByVal NoPedidoCompra As String,
                                                  ByVal IdPedidoCompraEnc As Integer,
                                                  ByVal IdRecepcionEnc As Integer,
                                                  ByRef lTransaccionesIngreso As List(Of clsBeI_nav_transacciones_out),
                                                  ByRef lblprg As RichTextBox,
                                                  ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Enviar_Cantidades_No_Ingreso = False

        Try

            prg.Visible = True

            Dim vContador As Integer = 0

            Dim lRecDet As New List(Of clsBeTrans_re_det)
            lRecDet = clsLnTrans_re_det.Get_All_By_IdRecEnc(IdRecepcionEnc)

            Dim lPCDet As New List(Of clsBeTrans_oc_det)
            lPCDet = clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(IdPedidoCompraEnc)

            Dim BeReDet As New clsBeTrans_re_det

            prg.Maximum = lPCDet.Count

            If Not lPCDet Is Nothing Then

                If lPCDet.Count > 0 Then

                    For Each I In lPCDet

                        '#EJC20180607: Enviar cantidad  0 en las líneas no recibidas.
                        BeReDet = lRecDet.Find(Function(x) x.IdProductoBodega = I.IdProductoBodega AndAlso x.No_Linea = I.No_Linea)

                        If BeReDet Is Nothing Then

                            '#EJC20180810_0346AM: Envía cantidad 0 para los productos no recibidos en una OC
                            wsCantidadPedidoCompra.LineaPedidoCompraCantRec(NoPedidoCompra,
                            I.No_Linea,
                            I.Codigo_Producto,
                            "",
                            0)

                            lblprg.AppendText(String.Format("Enviando Cantidad 0 para Pedido de compra: {0} Línea:{1} ", NoPedidoCompra, I.No_Linea))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If

                        Try

                            vContador += 1

                            prg.Value = vContador

                        Catch ex As Exception

                            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                              "",
                                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                                              BeConfigDet.Idnavconfigdet)

                            lblprg.AppendText(String.Format("Error al enviar cantidad 0 (para prod. no recibido) de pedido de compra a Nav desde WS: {0} {1}", ex.Message, vbNewLine))
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                    Next

                    Try

                        If lTransaccionesIngreso.Count > 0 Then
                            'Actualizar Enviado = True
                            lTransaccionesIngreso.Where(Function(x) x.No_pedido = NoPedidoCompra).FirstOrDefault.Enviado = True
                        End If

                    Catch ex As Exception
                        '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                                  "",
                                                                  BeNavEjecucionEnc.IdEjecucionEnc,
                                                                  BeConfigDet.Idnavconfigdet)

                        lblprg.AppendText(String.Format("Error al enviar cantidad 0 (para prod. no recibido) de pedido de compra a Nav desde WS: {0} {1}", ex.Message, vbNewLine))
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                    End Try

                End If 'ListaResumen.Count > 0

            End If  'Lista not is nothing          

            Enviar_Cantidades_No_Ingreso = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Private Function Enviar_Cantidades_Ingreso(ByVal NoPedidoCompra As String,
                                               ByRef lTransaccionesIngreso As List(Of clsBeI_nav_transacciones_out),
                                               ByVal EsTransferenciaRecepcion As Boolean,
                                               ByRef lblprg As RichTextBox,
                                               ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Dim fichaPedidosTraslado As New Pedidos_Transferencia()
        Dim vContador As Integer = 0
        Dim lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)

        Enviar_Cantidades_Ingreso = False

        Try

            prg.Maximum = lTransaccionesIngreso.Count
            prg.Visible = True

            Dim ListaResumen = (From i In lTransaccionesIngreso.Where(Function(x) x.No_pedido = NoPedidoCompra)
                                Group i By Keys = New With {Key i.No_pedido, Key i.No_linea,
                                Key i.Codigo_producto, Key i.Codigo_variante, Key i.Enviado} Into Group
                                Select New With {Keys.No_pedido, Keys.No_linea,
                                                 Keys.Codigo_producto,
                                                 Keys.Codigo_variante,
                                                 Keys.Enviado,
                                                 .Cantidad = Group.Sum(Function(x) x.Cantidad)})

            If Not ListaResumen Is Nothing Then

                If ListaResumen.Count > 0 Then

                    For Each I In ListaResumen

                        lblprg.AppendText(String.Format("Procesando Cantidad de Pedido: {0} Línea:{1} ", I.No_pedido, I.No_linea))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        Try

                            If EsTransferenciaRecepcion Then

                                fichaPedidosTraslado = wsPedidoTrasladoService.Read(I.No_pedido)

                                If Not fichaPedidosTraslado Is Nothing Then
                                    fichaPedidosTraslado.TransferLines.Where(Function(x) x.Line_No = Integer.Parse(I.No_linea) _
                                        AndAlso x.Item_No = I.Codigo_producto).FirstOrDefault.Qty_to_Receive = I.Cantidad
                                    wsPedidoTrasladoService.Update(fichaPedidosTraslado)
                                End If

                            Else
                                wsCantidadPedidoCompra.LineaPedidoCompraCantRec(I.No_pedido,
                                                                                I.No_linea,
                                                                                I.Codigo_producto,
                                                                                I.Codigo_variante,
                                                                                Math.Round(I.Cantidad, 5))
                            End If

                            lista_A_Actualizar = lTransaccionesIngreso.Where(Function(x) x.No_pedido = I.No_pedido _
                                                                             AndAlso x.No_linea = I.No_linea _
                                                                             AndAlso x.Codigo_producto = I.Codigo_producto _
                                                                             AndAlso x.Codigo_variante = I.Codigo_variante _
                                                                             AndAlso x.Enviado = True).ToList()

                            If Not lista_A_Actualizar Is Nothing Then
                                For Each T In lista_A_Actualizar
                                    clsLnI_nav_transacciones_out.Actualizar(T)
                                Next
                            End If

                            vContador += 1

                            prg.Value = vContador

                        Catch ex As Exception

                            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                              "",
                                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                                              BeConfigDet.Idnavconfigdet)

                            lblprg.AppendText(String.Format("Error al enviar lote de pedido de compra a Nav desde WS: {0} {1}", ex.Message, vbNewLine))
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                    Next

                    'Actualizar Enviado = True
                    lTransaccionesIngreso.Where(Function(x) x.No_pedido = NoPedidoCompra).FirstOrDefault.Enviado = True

                End If 'ListaResumen.Count > 0

            End If  'Lista not is nothing          

            Enviar_Cantidades_Ingreso = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#EJC20180111: Enviar los lotes primero, luego las cantidades agrupadas y sumadas por producto.
    Private Function Enviar_Lotes_Transf(ByVal NoPedidoTransf As String,
                                         ByRef lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out),
                                         ByRef lblprg As RichTextBox,
                                         ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Enviar_Lotes_Transf = False

        Try

            prg.Maximum = lTransaccionesSalida.Count
            prg.Visible = True

            Dim vContador As Integer = 0
            Dim BePresentacion As New clsBeProducto_Presentacion
            Dim vCantidad As Double = 0
            Dim vUnidMed As String = ""

            Dim listaLotes = lTransaccionesSalida.Where(Function(x) x.No_pedido = NoPedidoTransf).ToList()

            If Not listaLotes Is Nothing Then

                For Each I In listaLotes

                    lblprg.AppendText(String.Format("Procesando Lote de Pedido: {0} Línea:{1} ", I.No_pedido, I.No_linea))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Try

                        If Not I.Idpresentacion = 0 Then

                            '#EJC20180418: Enviar la cantidad en UMBAS.
                            BePresentacion.IdPresentacion = I.Idpresentacion
                            clsLnProducto_presentacion.GetSingle(BePresentacion)
                            vCantidad = I.Cantidad * BePresentacion.Factor

                            'Enviar a Nav el nombre/codigo de la unidad de medida básica.
                            vUnidMed = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(I.Idunidadmedida)

                        Else
                            vCantidad = I.Cantidad
                            vUnidMed = I.Unidad_medida
                        End If

                        '#EJC20180503: Enviar siempre UMBAS en Enviar_Lotes_Ingreso.                        
                        wsLotePedidoTransferencia.LoteLinPedidoTransfer(I.No_pedido,
                                                                        I.No_linea,
                                                                        I.Codigo_producto,
                                                                        I.Codigo_variante,
                                                                        vUnidMed,
                                                                        Math.Round(vCantidad, 5),
                                                                        I.Lote)

                        vContador += 1

                        prg.Value = vContador

                        I.Enviado = 1

                    Catch ex As Exception

                        '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al ejecutar WebService LoteLineaPedidoTransf: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)

                        lblprg.AppendText(String.Format("Error al ejecutar WebService LoteLineaPedidoTransf: {0} {1}", ex.Message, vbNewLine))
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End Try

                Next

            End If

            Enviar_Lotes_Transf = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#EJC20180111: Enviar las cantidades sumadas y agrupadas por producto (sin considerar el lote)
    Private Function Enviar_Cantidades_Transf(ByVal NoPedidoTransf As String,
                                              ByRef lTransaccionesSalida As List(Of clsBeI_nav_transacciones_out),
                                              ByRef lblprg As RichTextBox,
                                              ByRef prg As System.Windows.Forms.ProgressBar) As Boolean

        Enviar_Cantidades_Transf = False

        Try

            prg.Maximum = lTransaccionesSalida.Count
            prg.Visible = True

            Dim vContador As Integer = 0

            For Each T In lTransaccionesSalida.Where(Function(x) x.No_pedido = NoPedidoTransf)
                T.Enviado = True
            Next

            Dim ListaResumen = (From i In lTransaccionesSalida.Where(Function(x) x.No_pedido = NoPedidoTransf)
                                Group i By Keys = New With {Key i.No_pedido, Key i.No_linea,
                                Key i.Codigo_producto, Key i.Codigo_variante, Key i.Enviado} Into Group
                                Select New With {Keys.No_pedido, Keys.No_linea,
                                                 Keys.Codigo_producto,
                                                 Keys.Codigo_variante,
                                                 Keys.Enviado,
                                                 .Cantidad = Group.Sum(Function(x) x.Cantidad)})

            Dim lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)

            For Each I In ListaResumen

                lblprg.AppendText(String.Format("Procesando Cantidad de Pedido: {0} Línea:{1} ", I.No_pedido, I.No_linea))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Try

                    wsCantidadPedidoTransferencia.LineaPedidoTransferCantEnviar(I.No_pedido,
                                                                                I.No_linea,
                                                                                I.Codigo_producto,
                                                                                I.Codigo_variante,
                                                                                I.Cantidad)

                    lista_A_Actualizar = lTransaccionesSalida.Where(Function(x) x.No_pedido = I.No_pedido AndAlso
                        x.No_linea = I.No_linea AndAlso x.Codigo_producto = I.Codigo_producto AndAlso
                        x.Codigo_variante = I.Codigo_variante AndAlso x.Enviado = True).ToList()

                    If Not lista_A_Actualizar Is Nothing Then

                        For Each T In lista_A_Actualizar
                            clsLnI_nav_transacciones_out.Actualizar(T)
                        Next
                    End If

                    vContador += 1

                    prg.Value = vContador

                Catch ex As Exception

                    '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                      "",
                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                      BeConfigDet.Idnavconfigdet)

                    lblprg.AppendText(String.Format("Error al enviar lote de pedido de transf. a Nav desde WS: {0} {1}", ex.Message, vbNewLine))
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#CKFK20220125 Agregué el Push en la clase correspondiente
    Public Shared Function Push_Recepcion_Pedido_Compra_To_NAV_For_BYB(ByVal DocumentoIngreso As String,
                                                                       ByVal DocumentoRecepcion As String,
                                                                       ByVal NoLinea As Integer,
                                                                       ByVal CodigoProducto As String,
                                                                       ByVal Cantidad As Double,
                                                                       ByVal NoLote As String,
                                                                       ByVal FechaVence As Date,
                                                                       ByVal NomUnidadMedida As String,
                                                                       ByVal IdRecepcionEnc As Integer,
                                                                       ByVal IdRecepcionDet As Integer,
                                                                       ByVal pIdUsuario As Integer,
                                                                       ByRef pRespuesta As String) As Boolean

        '#EJC20210428: Debería devolver el número de ubicación.
        Dim vResultadoPostWhseReceipt As String = ""
        Dim vResultadoPutAwayCreate As String = ""

        Dim vDocumentoUbicacion As String = ""
        Dim vRecepcionAlmacen As String = ""
        Dim vTipoPush As String = "Push_Recepcion_Pedido_Compra_To_NAV_For_BYB"

        Push_Recepcion_Pedido_Compra_To_NAV_For_BYB = False


        Dim vUrlCodeUnit As String = My.MySettings.Default.NavSync_CUWMS_CUWMS

        Dim ws2 As New CUWMS.CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlCodeUnit
        }

        Dim vUrlRecepcionAlmacen As String = My.MySettings.Default.NavSync_WSRecepcionesAlm_Recep_Almacen_Service

        Dim ws3 As New Recep_Almacen_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlRecepcionAlmacen
        }

        Dim vUrlPaginaLotes As String = My.MySettings.Default.NavSync_WSPaginaLotes_Pagina_lotes_Service

        Dim ws4 As New WSPaginaLotes.Pagina_lotes_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlPaginaLotes
        }

        Dim vUrlLotePedidoCompra As String = My.MySettings.Default.DynamicsNavInterface_WSNavLotePedidoCompra_Lote_PedidoCompra

        Dim wsLotePedidoCompra As New Lote_PedidoCompra With
        {
        .UseDefaultCredentials = False,
        .Credentials = CredencialesConexion,
        .Url = vUrlLotePedidoCompra
        }

        Dim vUrlPedidoCompra As String = My.MySettings.Default.DynamicsNavInterface_WSPedidoCompra_Pedidos_Compra_Service

        Dim wsPedidoCompraService As New Pedidos_Compra_Service() With
            {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlPedidoCompra
            }

        Dim NavPedCompra As Pedidos_Compra = wsPedidoCompraService.Read(DocumentoIngreso)
        Dim vCantidadBasePedidoCompra As Double = 0
        Dim Actualizo_Cantidad_En_Documento = False

        If Not NavPedCompra Is Nothing Then
            If Not NavPedCompra.PurchLines Is Nothing Then
                vCantidadBasePedidoCompra = NavPedCompra.PurchLines.Where(Function(x) x.Line_No = NoLinea).FirstOrDefault.Quantity
            End If
        End If

        Try

            Dim RecepcionAlmacen As New Recep_Almacen()
            RecepcionAlmacen = ws3.Read(DocumentoRecepcion)

            If Not RecepcionAlmacen Is Nothing Then

                '#EJC20210324: Modificar cantidad a tomar/colocar si es diferente.
                For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines.Where(Function(x) x.Item_No = CodigoProducto _
                                                                                            AndAlso x.Unit_of_Measure_Code = NomUnidadMedida _
                                                                                            AndAlso x.Line_No = NoLinea)
                    If Lu.Qty_to_Receive = 0 Then
                        Lu.Qty_to_Receive = Cantidad
                        Actualizo_Cantidad_En_Documento = True
                    Else
                        If Not (Lu.Qty_to_Receive = Cantidad) Then
                            If vCantidadBasePedidoCompra = Lu.Qty_Base Then
                                If Cantidad <= Lu.Qty_Base Then
                                    Lu.Qty_to_Receive += Cantidad
                                    Actualizo_Cantidad_En_Documento = True
                                Else
                                    Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadBasePedidoCompra, Cantidad)
                                    Throw New Exception(vMensaje)
                                End If
                            ElseIf Lu.Qty_Received = 0 Then
                                Lu.Qty_to_Receive += Cantidad
                                Actualizo_Cantidad_En_Documento = True
                            End If
                        Else
                            '#EJC20220228:Ya tiene recibido.
                            If Cantidad <= Lu.Qty_Base Then
                                Lu.Qty_to_Receive += Cantidad
                                Actualizo_Cantidad_En_Documento = True
                            Else
                                Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadBasePedidoCompra, Cantidad)
                                Throw New Exception(vMensaje)
                            End If
                        End If
                    End If

                Next

            Else
                Throw New Exception("No se pudo leer la recepción desde el servicio de NAV #EJC202111111353.")
            End If


            If Actualizo_Cantidad_En_Documento Then

                '#EJC20180503: Enviar siempre UMBAS en Enviar_Lotes_Ingreso.
                wsLotePedidoCompra.LoteLineaPedidoCompra(DocumentoIngreso,
                                                         NoLinea,
                                                         CodigoProducto,
                                                         "",
                                                         NomUnidadMedida,
                                                         Cantidad,
                                                         NoLote,
                                                         FechaVence)

                '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                ws3.Update(RecepcionAlmacen)

            End If

            Push_Recepcion_Pedido_Compra_To_NAV_For_BYB = True

        Catch ex As Exception

            pRespuesta = ex.Message
            Return False

        End Try

    End Function

    '#CKFK20220125 Agregué el Push en la clase correspondiente
    Public Shared Function Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB(ByVal DocumentoIngreso As String,
                                                                                ByVal DocumentoRecepcion As String,
                                                                                ByVal NoLinea As Integer,
                                                                                ByVal CodigoProducto As String,
                                                                                ByVal Cantidad As Double,
                                                                                ByVal NoLote As String,
                                                                                ByVal FechaVence As Date,
                                                                                ByVal NomUnidadMedida As String,
                                                                                ByVal IdRecepcionEnc As Integer,
                                                                                ByVal IdRecepcionDet As Integer,
                                                                                ByVal pIdUsuario As Integer,
                                                                                ByRef pRespuesta As String) As Boolean

        '#EJC20210428: Debería devolver el número de ubicación.
        Dim vResultadoPostWhseReceipt As String = ""
        Dim vResultadoPutAwayCreate As String = ""

        Dim vDocumentoUbicacion As String = ""
        Dim vRecepcionAlmacen As String = ""
        Dim vTipoPush As String = "Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB"

        Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB = False


        Dim vUrlCodeUnit As String = My.MySettings.Default.NavSync_CUWMS_CUWMS

        Dim ws2 As New CUWMS.CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlCodeUnit
        }

        Dim vUrlRecepcionAlmacen As String = My.MySettings.Default.NavSync_WSRecepcionesAlm_Recep_Almacen_Service

        Dim ws3 As New Recep_Almacen_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlRecepcionAlmacen
        }

        Dim vUrlPaginaLotes As String = My.MySettings.Default.NavSync_WSPaginaLotes_Pagina_lotes_Service

        Dim ws4 As New WSPaginaLotes.Pagina_lotes_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlPaginaLotes
        }

        Dim vUrlLotePedidoCompra As String = My.MySettings.Default.DynamicsNavInterface_WSPedidoCompra_Pedidos_Compra_Service

        Dim wsLotePedidoCompra As New Lote_PedidoCompra With
        {
        .UseDefaultCredentials = False,
        .Credentials = CredencialesConexion,
        .Url = vUrlLotePedidoCompra
        }

        Dim vUrlTransferenciaIngreso As String = My.MySettings.Default.DynamicsNavInterface_WsPedidoTransferencia_Pedidos_Transferencia_Service

        Dim wsTransferenciaIngresoService As New Pedidos_Transferencia_Service() With
            {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlTransferenciaIngreso
            }

        Dim NavDevolucionVenta As New Pedidos_Transferencia
        Dim NavPedTransferencia = wsTransferenciaIngresoService.Read(DocumentoIngreso)
        Dim vCantidadTransferenciaIngreso As Double = 0
        Dim Actualizo_Cantidad_En_Documento As Boolean = False

        If Not NavPedTransferencia Is Nothing Then
            If Not NavPedTransferencia.TransferLines Is Nothing Then
                vCantidadTransferenciaIngreso = NavPedTransferencia.TransferLines.Where(Function(x) x.Line_No = NoLinea).FirstOrDefault.Quantity
            End If
        End If

        Try

            Dim RecepcionAlmacen As New Recep_Almacen
            RecepcionAlmacen = ws3.Read(DocumentoRecepcion)

            If Not RecepcionAlmacen Is Nothing Then

                '#CKFK 20211116 Actualizar la fecha del día para el registro
                RecepcionAlmacen.Posting_Date = Now.Date

                '#EJC20210324: Modificar cantidad a tomar/colocar si es diferente.
                For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines.Where(Function(x) x.Item_No = CodigoProducto AndAlso x.Unit_of_Measure_Code = NomUnidadMedida)

                    If Lu.Qty_to_Receive = 0 Then
                        Lu.Qty_to_Receive = Cantidad
                        Actualizo_Cantidad_En_Documento = True
                    Else
                        If Not (Lu.Qty_to_Receive = Cantidad) Then
                            If vCantidadTransferenciaIngreso = Lu.Qty_Base Then
                                If Cantidad <= Lu.Qty_Base Then
                                    Lu.Qty_to_Receive += Cantidad
                                    Actualizo_Cantidad_En_Documento = True
                                Else
                                    Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadTransferenciaIngreso, Cantidad)
                                    Throw New Exception(vMensaje)
                                End If
                            ElseIf Lu.Qty_Received = 0 Then
                                Lu.Qty_to_Receive += Cantidad
                                Actualizo_Cantidad_En_Documento = True
                            End If
                        Else
                            '#EJC20220228:Ya tiene recibido.
                            If Cantidad <= Lu.Qty_Base Then
                                Lu.Qty_to_Receive += Cantidad
                                Actualizo_Cantidad_En_Documento = True
                            Else
                                Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadTransferenciaIngreso, Cantidad)
                                Throw New Exception(vMensaje)
                            End If
                        End If
                    End If

                Next

            Else
                Throw New Exception("No se pudo leer la recepción desde el servicio de NAV #EJC202111111353.")
            End If

            '#EJC20220307: Analizar, si aquí no se deben registrar, lotes con en la compra.
            If Actualizo_Cantidad_En_Documento Then

                '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                ws3.Update(RecepcionAlmacen)

                Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB = True

            End If

        Catch ex As Exception

            pRespuesta = ex.Message
            Return False

        End Try

    End Function

    '#CKFK20220125 Agregué el Push en la clase correspondiente
    Public Shared Function Push_Recepcion_To_NAV_For_BYB(ByVal Location_Code As String,
                                                         ByVal Zone_Code As String,
                                                         ByVal Bin_Code As String,
                                                         ByVal Assigne_User_Id As String,
                                                         ByVal Item_No As String,
                                                         ByVal No_Orden_Prod As String,
                                                         ByVal IdRecepcionEnc As Integer,
                                                         ByVal IdRecepcionDet As Integer,
                                                         ByVal pIdUsuario As Integer,
                                                         ByRef pRespuesta As String) As WSUbicarAlmacen.Ubicar_Almacen

        Dim ActivityNo As String = ""
        Dim vResultadoPutAwayCreate As String = ""

        Dim vDocumentoUbicacion As String = ""
        Dim vRecepcionAlmacen As String = ""
        Dim vTipoPush As String = "Push_Recepcion_To_NAV_For_BYB"

        Push_Recepcion_To_NAV_For_BYB = Nothing

        Dim ws1 As New WSUInternas.U_Internas_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = My.MySettings.Default.NavSync_WSUInternas_U_Internas_Service
        }

        Dim ws2 As New CUWMS.CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = My.MySettings.Default.NavSync_CUWMS_CUWMS
        }

        Dim ws3 As New WSUbicarAlmacen.Ubicar_Almacen_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = My.MySettings.Default.NavSync_WSUbicarAlmacen_Ubicar_Almacen_Service
        }

        '#EJC20210311: No de pedido.
        'Tipo de orden
        Dim ws4 As New WSMovProductos.Mov_Productos_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = My.MySettings.Default.NavSync_WSMovProductos_Mov_Productos_Service
        }

        Try

            '#EJC20210309: Crear un documento de ubicación (cabecera)
            Dim BeUInterna As New WSUInternas.U_Internas()
            BeUInterna.Location_Code = Location_Code 'BA0002
            BeUInterna.From_Zone_Code = Zone_Code 'PROD
            BeUInterna.From_Bin_Code = Bin_Code 'PROD
            BeUInterna.Assigned_User_ID = Assigne_User_Id '"byb\wmsbodega1"
            BeUInterna.Sorting_Method = WSUbicarAlmacen.Sorting_Method.Item
            ws1.Create(BeUInterna)

            Dim vDocumentoUbicacionAsignado As String = BeUInterna.No
            Dim vResultadoGetBinContent As String = ""

            '#EJC20210309: Crear línea para documento.
            ws2.GetBinContent(vDocumentoUbicacionAsignado,
                              Location_Code,
                              Zone_Code,
                              Bin_Code,
                              Item_No,
                              vResultadoGetBinContent,
                              "")

            If vResultadoGetBinContent = "Successfully Created" Then

                '#EJC20210309:Obtener el registro de produccion de un producto en particular (item_no)
                Dim BeUInternaRegistradaEnProd As New WSUInternas.U_Internas()
                BeUInternaRegistradaEnProd = ws1.Read(vDocumentoUbicacionAsignado)

                '#EJC20210309:Crea la ubicación en el almacén.
                'ws2.PutAwayCreate(vDocumentoUbicacionAsignado, ActivityNo, vResultadoPutAwayCreate)
                Dim UbicarAlmacen As New WSUbicarAlmacen.Ubicar_Almacen()
                UbicarAlmacen = ws3.Read(ActivityNo)
                Push_Recepcion_To_NAV_For_BYB = UbicarAlmacen

            End If

        Catch ex As Exception

            pRespuesta = ex.Message
            Return Nothing

        End Try

    End Function

End Class