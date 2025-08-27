
Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS.wsDevolucionVenta
Imports TOMWMS.WSPaginaLotes
Imports TOMWMS.WSRecepcionesAlm
Imports Type = TOMWMS.wsDevolucionVenta.Type

Public Class clsSyncNavDevolucionVenta : Inherits clsInterfaceBase
    Implements IDisposable

    Property pBodega As String = ""

    Private fichaDevolucionVenta() As Devolucion_venta

    Dim wsDevolucionVenta As New Devolucion_venta_Service With
        {
        .UseDefaultCredentials = UsarCredencialesPorDefecto,
        .Credentials = CredencialesConexion
        }

    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Importar_Devolucion_Venta_Desde_WSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                          ByRef prg As System.Windows.Forms.ProgressBar,
                                                                          ByRef cnnLog As SqlConnection) As Boolean

        Importar_Devolucion_Venta_Desde_WSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim lDevolucionVenta As New List(Of Devolucion_venta)

            lDevolucionVenta = Get_Devolucion_Venta_FromWS(lblprg, True)

            BeNavEjecucionRes.Registros_ws = fichaDevolucionVenta.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, lConnection, lTransaction)

            Application.DoEvents()

            Dim BeI_nav_PedidoCompra As clsBeI_nav_ped_compra_enc
            Dim BeI_nav_PedidoCompraDet As clsBeI_nav_ped_compra_det
            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim BeBodega As New clsBeBodega
            Dim vErrorAlInsertarClienteComoBodega As Boolean = False
            Dim vMensajeErrorClienteBodega As String = ""
            Dim lLotes As New List(Of Pagina_lotes)
            Dim BeNavLote As New clsBeI_nav_ped_compra_det_lote

            lblprg.AppendText(String.Format("Devoluciones de venta en WS: {0} ", fichaDevolucionVenta.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()
            lblprg.Refresh()

            prg.Maximum = lDevolucionVenta.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det_lote.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                For Each PDV As Devolucion_venta In lDevolucionVenta

                    BeI_nav_PedidoCompra = New clsBeI_nav_ped_compra_enc

                    CopyObject(PDV, BeI_nav_PedidoCompra)

                    If (BeI_nav_PedidoCompra.No = "PDV-123979") Then
                        '    Continue For
                    End If

                    If Not PDV.Document_DateSpecified Then
                        PDV.Document_Date = Now.Date 'No tengo fecha en el documento?
                    ElseIf PDV.Document_Date.Year <= 1000 Then
                        PDV.Document_Date = Now.Date 'No tengo fecha en el documento?
                    End If

                    If Not PDV.Order_DateSpecified Then
                        BeI_nav_PedidoCompra.Order_Date = PDV.Document_Date
                    ElseIf PDV.Order_Date.Year <= 1000 Then
                        BeI_nav_PedidoCompra.Order_Date = PDV.Document_Date
                    End If

                    'Proveedor
                    BeI_nav_PedidoCompra.Buy_From_Vendor_Name = PDV.Sell_to_Customer_Name
                    BeI_nav_PedidoCompra.Buy_From_Vendor_No = PDV.Sell_to_Customer_No

                    'lblprg.AppendText(String.Format("Procesando Pedido Compra: {0} ", BeI_nav_PedidoCompra.No, vbNewLine))
                    'lblprg.AppendText(vbNewLine)
                    'lblprg.SelectionStart = lblprg.TextLength
                    'lblprg.ScrollToCaret()

                    vErrorAlInsertarClienteComoBodega = False

                    If Not PDV.Location_Code Is Nothing Then

                        vMensajeErrorClienteBodega = String.Format("La bodega {0} no está registrada como cliente y no es válida para recibir productos. {1} ", PDV.Location_Code, vbNewLine)

                        If Not clsLnCliente.Bodega_Es_Valida_Para_Recepcion(PDV.Location_Code, lConnection, lTransaction) Then

                            If XtraMessageBox.Show(vMensajeErrorClienteBodega + vbNewLine &
                                                   "¿Insertar como cliente?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                                If Not clsSyncNavBodega.Insertar_Bodega_Single_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(PDV.Location_Code,
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
                            vErrorAlInsertarClienteComoBodega = True
                        End If

                    End If

                    If vErrorAlInsertarClienteComoBodega Then

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensajeErrorClienteBodega,
                                                                   PDV.Location_Code,
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
                        If Not PDV.SalesLines Is Nothing Then

                            For Each L As Sales_Return_Order_Line In PDV.SalesLines

                                BeI_nav_PedidoCompraDet = New clsBeI_nav_ped_compra_det

                                Try

                                    Try

                                        CopyObject(L, BeI_nav_PedidoCompraDet)

                                    Catch ex As Exception
                                        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                    End Try

                                    BeI_nav_PedidoCompraDet.NoEnc = PDV.No

                                    If L.Type = Type.Item Then 'Es Producto

                                        If Not L.Location_Code Is Nothing Then

                                            '#EJC20210114: Se utiliza para determinar si la bodega a donde están intentando hacer la recepción
                                            'es o no una bodega válida, si es una bodega válida, debe haberse insertado previamente como cliente.
                                            If clsLnCliente.Bodega_Es_Valida_Para_Recepcion(L.Location_Code, lConnection, lTransaction) Then


                                                BeProductoBodega = clsLnProducto_bodega.Existe(L.No, BeConfigEnc.Idbodega, lConnection, lTransaction)

                                                If BeProductoBodega Is Nothing Then
                                                    If clsSyncNavProducto.Importar_Productos_DesdeWSNav_A_TablaIntermedia(L.No, lblprg, prg, cnnLog) Then
                                                        BeProductoBodega = clsLnProducto_bodega.Existe(L.No, BeBodega.IdBodega, lConnection, lTransaction)
                                                    End If
                                                End If

                                                'Existe el producto en el maestro?
                                                If Not BeProductoBodega Is Nothing Then

                                                    If L.Quantity <> L.Return_Qty_Received Then

                                                        If clsLnI_nav_ped_compra_det.Exist(BeI_nav_PedidoCompraDet, lConnection, lTransaction) Then
                                                            clsLnI_nav_ped_compra_det.Actualizar(BeI_nav_PedidoCompraDet, lConnection, lTransaction)
                                                            VContadorBitacoraIntermedia += 1
                                                        Else
                                                            clsLnI_nav_ped_compra_det.Insertar(BeI_nav_PedidoCompraDet, lConnection, lTransaction)
                                                            VContadorBitacoraIntermedia += 1
                                                        End If

                                                        lLotes = clsSyncLotes.Get_Lista_Lotes(PDV.No, L.No) ' 

                                                        For Each NavLote In lLotes

                                                            BeNavLote = New clsBeI_nav_ped_compra_det_lote
                                                            BeNavLote.NoEnc = PDV.No
                                                            BeNavLote.source_ID = NavLote.Source_ID
                                                            BeNavLote.Source_Prod_Order_Line = NavLote.Source_Ref_No 'NavLote.Source_Prod_Order_Line
                                                            BeNavLote.Item_No = NavLote.Item_No
                                                            BeNavLote.Lot_No = NavLote.Lot_No
                                                            BeNavLote.Expiration_Date = IIf(NavLote.Vencimiento_Calculado = "01/01/0001", "01/01/1900", NavLote.Vencimiento_Calculado)
                                                            BeNavLote.Entry_No = 0 'NavLote.Entry_No -> ya no vino en cambio de ws: #EJC20180516
                                                            BeNavLote.Source_Type = NavLote.Source_Type
                                                            BeNavLote.Quantity_Base = NavLote.Quantity_Base
                                                            BeNavLote.Variant_Code = IIf(NavLote.Variant_Code Is Nothing, "", NavLote.Variant_Code)

                                                            If clsLnI_nav_ped_compra_det_lote.Exist(BeNavLote, lConnection, lTransaction) Then
                                                                clsLnI_nav_ped_compra_det_lote.Actualizar(BeNavLote, lConnection, lTransaction)
                                                            Else
                                                                clsLnI_nav_ped_compra_det_lote.Insertar(BeNavLote, lConnection, lTransaction)
                                                            End If

                                                        Next

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
                                                                  BeConfigDet.Idnavconfigdet,
                                                                  cnnLog)

                        lblprg.AppendText(String.Format("Error al insertar Encabezado de la devolución de venta desde ws a intermedia: {0}{1}{2}", BeI_nav_PedidoCompra.No, vbNewLine, ex.Message))
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End Try

                Next

            End If

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Devolucion_Venta_Desde_WSNav_A_TablaIntermedia = True

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

    Public Function Get_Devolucion_Venta_FromWS(ByVal lblprg As RichTextBox, Optional ByVal AplicarFiltros As Boolean = True) As List(Of Devolucion_venta)

        Try

            Dim lDevolucionVenta As New List(Of Devolucion_venta)
            Dim StartDate As String = "01092021D"

            If AplicarFiltros Then

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("********** APLICANDO FILTROS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

                Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
                lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Devolucion_Venta)

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
                    If pBodega <> vCriteria Then
                        Throw New Exception(String.Format("La Bodega del filtro: {0} no se corresponde con la Bodega de la interface: {1}", vCriteria, pBodega))
                    End If
                End If

                Dim vFiltro1 As New Devolucion_venta_Filter() With {.Field = Devolucion_venta_Fields.Location_Code, .Criteria = vCriteria}

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("-Location_Code-")
                lblprg.AppendText("Criteria: " & vCriteria)
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

                Dim vFiltro2 As New Devolucion_venta_Filter() With {.Field = Devolucion_venta_Fields.Status, .Criteria = "1"}

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("-Status-")
                lblprg.AppendText("Criteria: 1")
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

                '#EJC20211112: No aplicar filtro de fecha, instrucción de Ricardo 11/11/2021, la bandeja de NAV, debería tener solo lo activo.
                'Dim vFiltro3 As New Pedidos_Compra_Filter() With {.Field = Pedidos_Compra_Fields.Posting_Date, .Criteria = StartDate} '"01/03/2021.."

                'lblprg.AppendText(vbNewLine)
                'lblprg.AppendText("-Posting_Date-")
                'lblprg.AppendText("Criteria: " & StartDate)
                'lblprg.AppendText(vbNewLine)
                'lblprg.SelectionStart = lblprg.TextLength
                'lblprg.ScrollToCaret()
                'lblprg.Refresh()

                Dim vFiltro4 As New Devolucion_venta_Filter() With {.Field = Devolucion_venta_Fields.Responsibility_Center, .Criteria = "DEVTOTAL|DEVPARCIAL|ANULACION"}

                'lblprg.AppendText(vbNewLine)
                'lblprg.AppendText("-Posting_Date-")
                'lblprg.AppendText("Criteria: " & StartDate)
                'lblprg.AppendText(vbNewLine)
                'lblprg.SelectionStart = lblprg.TextLength
                'lblprg.ScrollToCaret()
                'lblprg.Refresh()

                Dim vFiltros As Devolucion_venta_Filter() = New Devolucion_venta_Filter() {vFiltro1, vFiltro2, vFiltro4}

                wsDevolucionVenta.Url = My.Settings.NavSync_wsDevolucionVenta_Devolucion_venta_Service

                fichaDevolucionVenta = wsDevolucionVenta.ReadMultiple(vFiltros, Nothing, 500)

                For Each PC As Devolucion_venta In fichaDevolucionVenta
                    lDevolucionVenta.Add(PC)
                Next

            Else

                fichaDevolucionVenta = wsDevolucionVenta.ReadMultiple(Nothing, Nothing, 500)

                For Each PC As Devolucion_venta In fichaDevolucionVenta
                    lDevolucionVenta.Add(PC)
                Next

            End If

            Return lDevolucionVenta

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


    Public Function Insertar_Devolucion_Venta_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByRef lblprg As RichTextBox,
                                                                                   ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                   Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                   Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Devolucion_Venta_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim lConnectionInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim DifCant As Double = 0

        Try

            Dim vMensajeREsultadoCUWMS As String = ""


            '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
            Dim wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                   .Credentials = CredencialesConexion}

            wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Devolucion venta") Then

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

            'lblprg.AppendText(String.Format("Conectando a BD: {0} Sever: {1}", BD.Instancia.NombreBD, BD.Instancia.Server))
            'lblprg.AppendText(vbNewLine)
            'lblprg.Refresh()

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            lConnectionInterface.Open() : lTransInterface = lConnectionInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            lblprg.AppendText("Consultando configuración de interface: " & BD.Instancia.IdConfiguracionInterface)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnectionInterface,
                                                          lTransInterface)

            If BeConfigEnc Is Nothing Then
                If BD.Instancia.IdConfiguracionInterface = 0 Then
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                Else
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                End If

            End If

            'lblprg.AppendText("Iniciando transacción a BD: " & Now)
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

                If Not Importar_Devolucion_Venta_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde WS?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Devolucion_Venta_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lDevolucionVentaEnc As New List(Of clsBeI_nav_ped_compra_enc)

            lblprg.AppendText("Consultando devolucion venta en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lDevolucionVentaEnc = clsLnI_nav_ped_compra_enc.GetAll(lConnectionInterface, lTransInterface, lblprg, prg)

            lblprg.AppendText(String.Format("Devolución de venta en tabla intermedia: {0}", lDevolucionVentaEnc.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If lDevolucionVentaEnc.Count > 0 Then

                Dim gBeOrdenCompra As clsBeTrans_oc_enc = Nothing
                Dim PedidoCompraExistente As clsBeTrans_oc_enc = Nothing
                Dim vContador As Integer = 0
                Dim vContadorLineasDet As Integer = 0
                Dim BeProveedorBodega As New clsBeProveedor_bodega
                Dim BeProductoBodega As New clsBeProducto_bodega
                Dim BePresentacion As New clsBeProducto_Presentacion
                Dim BeOcDetLote As New clsBeTrans_oc_det_lote
                Dim BeOcDetLoteExistente As New clsBeTrans_oc_det_lote

                prg.Maximum = lDevolucionVentaEnc.Count

                prg.Value = 0

                VContadorBitacoraTomims = 0

                lblprg.AppendText("********** TRASLADANDO DOCUMENTO A TOMWMS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Dim BeUnidadMedidaPedCompra As New clsBeUnidad_medida

                For Each navPedidoCompraEnc As clsBeI_nav_ped_compra_enc In lDevolucionVentaEnc '.FindAll(Function(x) x.No = "PDV-124912")

                    If navPedidoCompraEnc.Status <> 0 Then

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(String.Format("Procesando D.I.: {0} ", navPedidoCompraEnc.No, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        gBeOrdenCompra = New clsBeTrans_oc_enc() With {.Referencia = navPedidoCompraEnc.No,
                                                                       .IdTipoIngresoOC = navPedidoCompraEnc.Document_Type}

                        PedidoCompraExistente = clsLnTrans_oc_enc.Get_Single_By_Referencia(gBeOrdenCompra,
                                                                                           lConnectionInterface,
                                                                                           lTransInterface)

                        prg.Value = vContador

                        vContador += 1
                        vContadorLineasDet = 0

                        'La devolucion de venta existe y debe ser actualizado.
                        If Not PedidoCompraExistente Is Nothing Then

                            gBeOrdenCompra.Activo = True

                            BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                                       BeConfigEnc.Idbodega,
                                                                                                       lConnectionInterface,
                                                                                                       lTransInterface)

                            If navPedidoCompraEnc.No = "PDV-114196" Then
                                Debug.Print("PDV-114196")
                            End If

                            If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                                gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega
                            End If

                            gBeOrdenCompra.IdProveedorBodega = BeProveedorBodega.IdAsignacion
                            gBeOrdenCompra.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Devolucion_Venta
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

                            clsLnTrans_oc_enc.Actualizar(gBeOrdenCompra,
                                                         lConnectionInterface,
                                                         lTransInterface)

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
                                                                                       lConnectionInterface,
                                                                                       lTransInterface)

                                        'Existe el producto en el maestro?
                                        If BeProductoBodega IsNot Nothing Then

                                            'Existe el producto en el detalle de la orden de compra en la tabla DE TOMWMS?
                                            BePedidoCompraDet = clsLnTrans_oc_det.Exist(PedidoCompraExistente.IdOrdenCompraEnc,
                                                                                        navPedidoCompraDet.Line_No,
                                                                                        lConnectionInterface,
                                                                                        lTransInterface)

                                            '#CKFK 20180725 17:45 coloqué esto en comentario, porque la instancia BeUnidadMedidaPedCompra era nothing y no se le podía asignar valor a la property Nombre
                                            'BeUnidadMedidaPedCompra.Nombre = navPedidoCompraDet.Unit_of_Measure_Code
                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                                                            BeConfigEnc.IdPropietario,
                                                                                                                            lConnectionInterface,
                                                                                                                            lTransInterface)

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
                                                                                                                                        lConnectionInterface,
                                                                                                                                        lTransInterface)
                                                If BePresentacion Is Nothing Then
                                                    Throw New Exception("ERROR_202303031404A: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)
                                                End If 'Fin sí: BePresentacion IsNothing (Presentación no existe y se insertó)

                                            End If 'Fin sí: Cod_Variante <> ""

#End Region

                                            'Producto No existe en la tabla de detalle DE TOMWMS. trans_oc_det
                                            If BePedidoCompraDet Is Nothing Then

                                                Try

                                                    BePedidoCompraDet = New clsBeTrans_oc_det
                                                    BePedidoCompraDet.IdOrdenCompraEnc = PedidoCompraExistente.IdOrdenCompraEnc
                                                    BePedidoCompraDet.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(BePedidoCompraDet.IdOrdenCompraEnc, lConnectionInterface, lTransInterface) + 1
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
                                                                               lConnectionInterface,
                                                                               lTransInterface,
                                                                               CnnLog) Then

                                                        clsLnTrans_oc_det.Insertar(BePedidoCompraDet,
                                                                                   lConnectionInterface,
                                                                                   lTransInterface)

                                                        '#CKFK 20211108 Agregué la  inserción del detalle de los lotes
                                                        If BeProductoBodega.Producto.Control_lote Then

                                                            If navPedidoCompraEnc.Lineas_Detalle_Lotes.Count > 0 Then

                                                                For Each Lote In navPedidoCompraEnc.Lineas_Detalle_Lotes.Where(Function(x) x.NoEnc = navPedidoCompraDet.NoEnc _
                                                                                                                               AndAlso x.Item_No = navPedidoCompraDet.No _
                                                                                                                               AndAlso x.Source_Prod_Order_Line = navPedidoCompraDet.Line_No)

                                                                    BeOcDetLote = New clsBeTrans_oc_det_lote

                                                                    BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                                    BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                                    BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(lConnectionInterface, lTransInterface) + 1
                                                                    BeOcDetLote.Cantidad = Lote.Quantity_Base
                                                                    BeOcDetLote.No_linea = Lote.Source_Prod_Order_Line
                                                                    BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                                    BeOcDetLote.Lote = Lote.Lot_No
                                                                    BeOcDetLote.Cantidad_recibida = 0
                                                                    BeOcDetLote.Codigo_producto = Lote.Item_No
                                                                    clsLnTrans_oc_det_lote.Insertar(BeOcDetLote,
                                                                                                    lConnectionInterface,
                                                                                                    lTransInterface)

                                                                Next

                                                            Else
                                                                Throw New Exception("ERROR_202302242332: EL documento de devolución no tiene lotes.")
                                                            End If

                                                        End If

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

                                            Else 'La línea de detalle documento sí existe en tabla trans_oc_det

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
                                                                                                 lConnectionInterface,
                                                                                                 lTransInterface)

                                                    '#CKFK 20211108 Agregué la  inserción del detalle de los lotes
                                                    If BeProductoBodega.Producto.Control_lote Then
                                                        If Not PedidoCompraExistente.DetalleLotes Is Nothing Then

                                                            If PedidoCompraExistente.DetalleLotes.Count > 0 Then

                                                                For Each Lote In PedidoCompraExistente.DetalleLotes.Where(Function(x) x.IdOrdenCompraEnc = PedidoCompraExistente.IdOrdenCompraEnc _
                                                                                                                          AndAlso x.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet _
                                                                                                                          AndAlso x.IdProductoBodega = BePedidoCompraDet.IdProductoBodega _
                                                                                                                          AndAlso x.No_linea = BePedidoCompraDet.No_Linea)

                                                                    BeOcDetLote = New clsBeTrans_oc_det_lote
                                                                    BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                                    BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                                    BeOcDetLote.IdOrdenCompraDetLote = Lote.IdOrdenCompraDetLote
                                                                    BeOcDetLote.Cantidad = Lote.Cantidad
                                                                    BeOcDetLote.No_linea = Lote.No_linea
                                                                    BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                                    BeOcDetLote.Lote = Lote.Lote
                                                                    BeOcDetLote.Fecha_vence = Lote.Fecha_vence
                                                                    BeOcDetLote.Cantidad_recibida = Lote.Cantidad_recibida
                                                                    BeOcDetLote.Codigo_producto = Lote.Codigo_producto
                                                                    BeOcDetLote.IdPresentacion = Lote.IdPresentacion
                                                                    BeOcDetLote.IdUnidadMedidaBasica = Lote.IdUnidadMedidaBasica
                                                                    BeOcDetLote.Activo = Lote.Activo
                                                                    BeOcDetLote.No_Documento = Lote.No_Documento

                                                                    BeOcDetLoteExistente = New clsBeTrans_oc_det_lote
                                                                    BeOcDetLoteExistente = clsLnTrans_oc_det_lote.Exist(BeOcDetLote.IdOrdenCompraEnc,
                                                                                                                        BeOcDetLote.No_linea,
                                                                                                                        BeOcDetLote.IdOrdenCompraDetLote,
                                                                                                                        lConnectionInterface,
                                                                                                                        lTransInterface)

                                                                    '#CKFK20220502 Se agregaron validaciones para actualizar o insertar el lote
                                                                    If BeOcDetLoteExistente Is Nothing Then
                                                                        clsLnTrans_oc_det_lote.Insertar(BeOcDetLote,
                                                                                                        lConnectionInterface,
                                                                                                        lTransInterface)
                                                                    Else
                                                                        If Lote.Cantidad_recibida = 0 Then
                                                                            clsLnTrans_oc_det_lote.Actualizar(BeOcDetLote,
                                                                                                              lConnectionInterface,
                                                                                                              lTransInterface)
                                                                        Else
                                                                            BeOcDetLoteExistente.Cantidad_recibida = Lote.Cantidad_recibida
                                                                            clsLnTrans_oc_det_lote.Actualizar(BeOcDetLoteExistente,
                                                                                                              lConnectionInterface,
                                                                                                              lTransInterface)
                                                                        End If
                                                                    End If

                                                                Next

                                                            Else
                                                                Throw New Exception("ERROR_202302242332A: EL documento de devolución no tenía lotes anteriormente.")
                                                            End If

                                                        End If
                                                    End If


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

                                                    If ex.Message.Contains("ERROR_202302242332") Then
                                                        Throw ex
                                                    End If

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
                                lblprg.AppendText(String.Format("Pedido de devolución #:{0} Sin Detalle {1}", navPedidoCompraEnc.No, vbNewLine))
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else

                                gBeOrdenCompra.IdOrdenCompraEnc = clsLnTrans_oc_enc.MaxID(lConnectionInterface, lTransInterface) + 1
                                gBeOrdenCompra.PropietarioBodega = New clsBePropietario_bodega
                                gBeOrdenCompra.PropietarioBodega.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega,
                                                                                                                                                               BeConfigEnc.IdPropietario,
                                                                                                                                                               lConnectionInterface,
                                                                                                                                                               lTransInterface)
                                gBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.NUEVA
                                gBeOrdenCompra.Hora_Creacion = Now
                                gBeOrdenCompra.User_Agr = BeConfigEnc.IdUsuario
                                gBeOrdenCompra.Fec_Agr = Now
                                gBeOrdenCompra.Fecha_Creacion = Now
                                gBeOrdenCompra.Activo = True

                                BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                                           BeConfigEnc.Idbodega,
                                                                                                           lConnectionInterface,
                                                                                                           lTransInterface)

                                If BeProveedorBodega Is Nothing Then

                                    BeProveedorBodega = clsSyncNavProveedor.Insertar_Cliente_As_Proveedor_Single(navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                                                 lConnectionInterface,
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
                                gBeOrdenCompra.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Devolucion_Venta
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
                                        wsCUWMS.CreateSalesReturnReceipt(navPedidoCompraEnc.No,
                                                                         vMensajeREsultadoCUWMS)

                                        Dim vUrlRecepcionAlmacen As String = My.MySettings.Default.NavSync_WSRecepcionesAlm_Recep_Almacen_Service

                                        Dim ws3 As New Recep_Almacen_Service() With
                                        {
                                            .UseDefaultCredentials = False,
                                            .Credentials = CredencialesConexion,
                                            .Url = vUrlRecepcionAlmacen
                                        }

                                        Dim RecepcionAlmacen As New Recep_Almacen()
                                        RecepcionAlmacen = ws3.Read(vMensajeREsultadoCUWMS)

                                        '#CKFK20230220 Agregué esta validación para cuando la Recepción es nula
                                        If Not RecepcionAlmacen Is Nothing Then

                                            If Not RecepcionAlmacen.WhseReceiptLines Is Nothing Then

                                                '#EJC20210324: Modificar cantidad a tomar/colocar 0, para que se pueda recibir parcial en HH.
                                                For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines
                                                    Lu.Qty_to_Receive = 0
                                                Next

                                                '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                                                ws3.Update(RecepcionAlmacen)

                                            End If

                                        End If

                                        If gBeOrdenCompra.No_Documento.Trim = "" AndAlso vMensajeREsultadoCUWMS <> "" Then
                                            gBeOrdenCompra.No_Documento = vMensajeREsultadoCUWMS
                                        End If

                                        If vMensajeREsultadoCUWMS <> "" Then
                                            gBeOrdenCompra.No_Documento_Recepcion_ERP = vMensajeREsultadoCUWMS
                                        End If

                                        If vMensajeREsultadoCUWMS <> "" Then

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Se creó la recepción del documento de devolución: " & vMensajeREsultadoCUWMS,
                                                                                        navPedidoCompraEnc.No,
                                                                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                        BeConfigDet.Idnavconfigdet, CnnLog)

                                            lblprg.AppendText(String.Format("Se creó la recepción: {0} para el documento de devolución: {1} {2}", vMensajeREsultadoCUWMS, navPedidoCompraEnc.No, vbNewLine))
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        Else

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("NO se creó la recepción del documento de devolución: " & vMensajeREsultadoCUWMS & ". La interface no reportó núermo de documento",
                                                                                        navPedidoCompraEnc.No,
                                                                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                        BeConfigDet.Idnavconfigdet, CnnLog)

                                            lblprg.AppendText(String.Format("No se creó la recepción : {0} para el documento de devolución: {1} {2} la interface de NAV no notificó número de documento para la recepción de almacén.", vMensajeREsultadoCUWMS, navPedidoCompraEnc.No, vbNewLine))
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        End If


                                    End If

                                    clsLnTrans_oc_enc.Insertar(gBeOrdenCompra,
                                                               lConnectionInterface,
                                                               lTransInterface)

                                    VContadorBitacoraTomims += 1

                                    If navPedidoCompraEnc.Lineas_Detalle.Count > 0 Then
                                        For Each navPedidoCompraDet As clsBeI_nav_ped_compra_det In navPedidoCompraEnc.Lineas_Detalle

                                            vContadorLineasDet += 1

                                            Dim BePedidoCompraDet As New clsBeTrans_oc_det() With {.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc,
                                                .IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompra.IdOrdenCompraEnc, lConnectionInterface, lTransInterface) + 1}

                                            '#20180101_1203:Línea agregada para actulización en envío.
                                            'BePedidoCompraDet.No_Linea = navPedidoCompraDet.No

                                            BeProductoBodega = clsLnProducto_bodega.Existe(navPedidoCompraDet.No,
                                                                                           BeConfigEnc.Idbodega,
                                                                                           lConnectionInterface,
                                                                                           lTransInterface)

                                            '#CKFK20220216 Puse esto en comentario y lo reemplacé por la función de Existe_By_Nombre
                                            'BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_Unidad_Medida(navPedidoCompraDet.Unit_of_Measure_Code)

                                            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                                                            BeConfigEnc.IdPropietario,
                                                                                                                            lConnectionInterface,
                                                                                                                            lTransInterface)

                                            'La unidad de medida existe?
                                            If BeUnidadMedidaPedCompra Is Nothing Then
                                                'unidad de medida no existe en tabla UNIDAD_MEDIDA
                                                Throw New Exception(
                                                String.Format("Producto: {0} UnidMedBas {1} No existe ",
                                                              navPedidoCompraDet.No,
                                                              BeProductoBodega.Producto.UnidadMedida.Nombre))
                                            End If 'Fin sí: unidad de medida no existe.

#Region "COD_VARIANTE_A_PRESENTACION"

                                            If navPedidoCompraDet.Variant_Code <> "" Then

                                                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                                                                                                        navPedidoCompraDet.Variant_Code,
                                                                                                                                        lConnectionInterface,
                                                                                                                                        lTransInterface)

                                                If BePresentacion Is Nothing Then

                                                    Throw New Exception("ERROR_202303031404B: La presentación: " & navPedidoCompraDet.Variant_Code & " no existe para el código de producto " & navPedidoCompraDet.No)

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
                                                        If BePedidoCompraDet.IdPresentacion <> 0 Then
                                                            BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                                                            BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                                                        Else
                                                            BePedidoCompraDet.IdPresentacion = 0
                                                        End If
                                                    Else
                                                        BePedidoCompraDet.IdPresentacion = 0
                                                    End If

                                                    If Asigna_Unidad_De_Medida(BePedidoCompraDet,
                                                                               navPedidoCompraDet,
                                                                               BeUnidadMedidaPedCompra,
                                                                               BeProductoBodega,
                                                                               lblprg,
                                                                               lConnectionInterface,
                                                                               lTransInterface,
                                                                               CnnLog) Then

                                                        clsLnTrans_oc_det.Insertar(BePedidoCompraDet,
                                                                                   lConnectionInterface,
                                                                                   lTransInterface)

                                                        If BeProductoBodega.Producto.Control_lote Then

                                                            If Not navPedidoCompraEnc.Lineas_Detalle_Lotes Is Nothing Then

                                                                If navPedidoCompraEnc.Lineas_Detalle_Lotes.Count > 0 Then

                                                                    For Each Lote In navPedidoCompraEnc.Lineas_Detalle_Lotes.Where(Function(x) x.NoEnc = navPedidoCompraDet.NoEnc _
                                                                                                                                   AndAlso x.Item_No = navPedidoCompraDet.No _
                                                                                                                                   AndAlso x.Source_Prod_Order_Line = navPedidoCompraDet.Line_No)

                                                                        BeOcDetLote = New clsBeTrans_oc_det_lote
                                                                        BeOcDetLote.IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc
                                                                        BeOcDetLote.IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet
                                                                        BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(lConnectionInterface, lTransInterface) + 1
                                                                        BeOcDetLote.Cantidad = Lote.Quantity_Base
                                                                        BeOcDetLote.No_linea = Lote.Source_Prod_Order_Line
                                                                        BeOcDetLote.IdProductoBodega = BePedidoCompraDet.IdProductoBodega
                                                                        BeOcDetLote.Lote = Lote.Lot_No
                                                                        BeOcDetLote.Fecha_vence = Lote.Expiration_Date
                                                                        BeOcDetLote.Cantidad_recibida = 0
                                                                        BeOcDetLote.Codigo_producto = Lote.Item_No
                                                                        BeOcDetLote.IdUnidadMedidaBasica = BePedidoCompraDet.IdUnidadMedidaBasica
                                                                        BeOcDetLote.IdPresentacion = BePedidoCompraDet.IdPresentacion
                                                                        BeOcDetLote.Presentacion = BePedidoCompraDet.Presentacion
                                                                        BeOcDetLote.UnidadMedida = BePedidoCompraDet.UnidadMedida
                                                                        BeOcDetLote.User_agr = BePedidoCompraDet.User_agr
                                                                        BeOcDetLote.User_mod = BePedidoCompraDet.User_mod

                                                                        clsLnTrans_oc_det_lote.Insertar(BeOcDetLote,
                                                                                                        lConnectionInterface,
                                                                                                        lTransInterface)

                                                                    Next
                                                                Else
                                                                    Throw New Exception("ERROR_202302242332F: EL documento de devolución no tiene lotes (el count es 0).")
                                                                End If
                                                            Else
                                                                Throw New Exception("ERROR_202302242332G: EL documento de devolución no tiene lotes (La lista es nothing).")
                                                            End If

                                                        End If

                                                        VContadorBitacoraTomims += 1

                                                    End If

                                                Catch ex As Exception

                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               BePedidoCompraDet.Nombre_producto,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                                                    lblprg.AppendText(String.Format("Error al insertar desde ws a intermedia: {0}{1}{2}", BePedidoCompraDet.Nombre_producto, ex.Message, vbNewLine))
                                                    lblprg.AppendText(vbNewLine)
                                                    lblprg.Refresh()
                                                    lblprg.SelectionStart = lblprg.TextLength
                                                    lblprg.ScrollToCaret()

                                                    If ex.Message.Contains("ERROR_202302242332") Then
                                                        Throw ex
                                                    End If

                                                End Try

                                            Else

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro ",
                                                                                          navPedidoCompraDet.No,
                                                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                          BeConfigDet.Idnavconfigdet, CnnLog)

                                                lblprg.AppendText(String.Format("No existe Producto Bodega: {0}{1}", navPedidoCompraDet.No, vbNewLine))
                                                lblprg.AppendText(vbNewLine)
                                                lblprg.Refresh()
                                                lblprg.SelectionStart = lblprg.TextLength
                                                lblprg.ScrollToCaret()

                                            End If

                                        Next
                                    Else
                                        Throw New Exception("ERROR_202302242332B: EL documento de devolución no tiene líneas de detalle.")
                                    End If

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               navPedidoCompraEnc.No,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                    lblprg.AppendText(String.Format("Error al insertar la devolución de venta con Referencia: {0}{1}{2}", navPedidoCompraEnc.No, vbNewLine, ex.Message))
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                End Try

                                Application.DoEvents()

                            End If

                        End If

                    Else

                        lblprg.AppendText(String.Format("Documento de ingreso Inactivo {0} ", navPedidoCompraEnc.No, vbNewLine))
                        lblprg.AppendText(vbNewLine)
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                Next

            End If

            lTransInterface.Commit()

            '#EJC20171107_REF04_0250AM: Desplegar cantidad de registros de pedidos de compra procesados
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TOMWMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Devoluciones de venta procesados  correctamente: {0}", VContadorBitacoraTomims))
            lblprg.AppendText(vbNewLine)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
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
            If lConnectionInterface.State = ConnectionState.Open Then lConnectionInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
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
                Existe_By_IdProducto_And_NombrePresentacion(
                BeProductoBodega.IdProducto,
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

    '#CKFK20220125 Agregué el Push en la clase correspondiente
    Public Shared Function Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB(ByVal DocumentoIngreso As String,
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
        Dim vTipoPush As String = "Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB"

        Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB = False


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

        Dim vUrlDevolucionVenta As String = My.MySettings.Default.NavSync_wsDevolucionVenta_Devolucion_venta_Service

        Dim wsDevolucionVentaService As New Devolucion_venta_Service() With
            {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlDevolucionVenta
            }

        Try

            Dim NavDevolucionVenta As New Devolucion_venta
            NavDevolucionVenta = wsDevolucionVentaService.Read(DocumentoIngreso)
            Dim vCantidadBasePedidoDevolucionVenta As Double = 0

            If Not NavDevolucionVenta Is Nothing Then

                If Not NavDevolucionVenta.SalesLines Is Nothing Then

                    vCantidadBasePedidoDevolucionVenta = NavDevolucionVenta.SalesLines.Where(Function(x) x.Line_No = NoLinea).FirstOrDefault.Quantity


                    Dim RecepcionAlmacen As New Recep_Almacen()
                    RecepcionAlmacen = ws3.Read(DocumentoRecepcion)

                    If Not RecepcionAlmacen Is Nothing Then
                        '#EJC20210324: Modificar cantidad a tomar/colocar si es diferente.
                        For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines.Where(Function(x) x.Item_No = CodigoProducto)

                            If Not (Lu.Qty_Received = Cantidad) Then
                                If vCantidadBasePedidoDevolucionVenta = Lu.Quantity Then
                                    If Cantidad <= Lu.Quantity Then
                                        Lu.Qty_to_Receive = Cantidad
                                        Exit For
                                    Else
                                        Dim vMensaje As String = String.Format("Error NAV - No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadBasePedidoDevolucionVenta, Cantidad)
                                        Throw New Exception(vMensaje)
                                    End If
                                ElseIf Lu.Qty_Received = 0 Then
                                    Lu.Qty_to_Receive = Cantidad
                                    Exit For
                                End If
                            End If

                        Next

                        '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                        ws3.Update(RecepcionAlmacen)

                        Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB = True

                        '#CKFK20220307 Ya no se va a hacer el registro de la transaccion
                        'Dim vResultPutAway As Boolean = Registrar_Pedido_Compra_To_NAV_For_BYB(DocumentoIngreso,
                        '                                                                       DocumentoRecepcion,
                        '                                                                       RecepcionAlmacen)

                        'Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB = vResultPutAway

                        'If Not vResultPutAway Then

                        '    Throw New Exception("Error NAV - No se pudo realizar el registro")

                        'End If

                    Else
                        Throw New Exception("Error NAV - No se pudo leer la recepción desde el servicio de NAV #EJC202111111351.")
                    End If

                Else
                    Throw New Exception("Error NAV - No se pudo realizar el registro no hay líneas para registrar")
                End If

            Else
                Throw New Exception("Error NAV - No se pudo realizar el registro no hay devolución para registrar")
            End If

            'If vResultPutAway Then
            '    '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
            '    'ws3.Update(RecepcionAlmacen)
            '    Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB = True
            'End If

        Catch ex As Exception

            pRespuesta = ex.Message
            Return False

        End Try

    End Function

    Public Shared Function Registrar_Pedido_Compra_To_NAV_For_BYB(ByVal DocumentoIngreso As String,
                                                                  ByVal DocumentoRecepcion As String,
                                                                  ByVal RecepcionAlmacen As Recep_Almacen) As Boolean

        Registrar_Pedido_Compra_To_NAV_For_BYB = False

        Try


            Dim vResultadoPostWhseReceipt As String = ""
            Dim vResultadoPutAwayCreate As String = ""
            Dim vUrlRecepcionAlmacen As String = My.MySettings.Default.NavSync_WSRecepcionesAlm_Recep_Almacen_Service
            Dim vUrlCodeUnit As String = My.MySettings.Default.NavSync_CUWMS_CUWMS

            Dim ws2 As New CUWMS.CUWMS() With
            {
                .UseDefaultCredentials = False,
                .Credentials = CredencialesConexion,
                .Url = vUrlCodeUnit
            }

            '#EJC20210527: Para la recepción de compra, devuelve el número de documento de ubicación.
            ws2.PostWhseReceipt(RecepcionAlmacen.No,
                                vResultadoPostWhseReceipt,
                                DocumentoIngreso)

            If vResultadoPostWhseReceipt.StartsWith("UA") Then

                ws2.RegisterPutAway(vResultadoPostWhseReceipt,
                                    vResultadoPutAwayCreate)

                If vResultadoPutAwayCreate = "Successfully Created" Then
                    Registrar_Pedido_Compra_To_NAV_For_BYB = True
                Else
                    Throw New Exception("Error - NAV " & vResultadoPutAwayCreate)
                End If

            Else
                Throw New Exception("Error - NAV " & vResultadoPostWhseReceipt)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        fichaDevolucionVenta = Nothing
        wsDevolucionVenta = Nothing
    End Sub

End Class
