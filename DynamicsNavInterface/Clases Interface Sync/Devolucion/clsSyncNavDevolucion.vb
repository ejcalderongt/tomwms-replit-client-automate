Imports System.Data.SqlClient
Imports System.Reflection
Imports TOMWMS.WSDevolucion

Public Class clsSyncNavDevolucion : Inherits clsInterfaceBase
    Implements IDisposable

    Private fichaDevolucion() As Devolucion

    Dim wsDevolucion As New Devolucion_Service With
        {
        .UseDefaultCredentials = UsarCredencialesPorDefecto,
        .Credentials = CredencialesConexion
        }

    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0


    Public Function Importar_Devolucion_Desde_WSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                          ByRef prg As System.Windows.Forms.ProgressBar,
                                                                          ByRef cnnLog As SqlConnection) As Boolean

        Importar_Devolucion_Desde_WSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim lDevolucion As New List(Of Devolucion)

            lDevolucion = Get_Devolucion_FromWS(lblprg, True)

            BeNavEjecucionRes.Registros_ws = fichaDevolucion.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeI_nav_PedidoCompra As clsBeI_nav_ped_compra_enc
            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim vErrorAlInsertarClienteComoBodega As Boolean = False
            Dim vMensajeErrorClienteBodega As String = ""

            lblprg.AppendText(String.Format("Devoluciones en WS: {0} ", fichaDevolucion.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()
            lblprg.Refresh()

            prg.Maximum = lDevolucion.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Borrar tablas intermedias.
            If clsLnI_nav_ped_compra_det.EliminarTodos(lConnection, lTransaction) _
                AndAlso clsLnI_nav_ped_compra_enc.EliminarTodos(lConnection, lTransaction) Then

                For Each PC As Devolucion In lDevolucion

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

                    'If Not PC.Expected_Receipt_DateSpecified Then
                    '    BeI_nav_PedidoCompra.Expected_Receipt_Date = BeI_nav_PedidoCompra.Order_Date
                    'ElseIf PC.Expected_Receipt_Date.Year <= 1000 Then
                    '    BeI_nav_PedidoCompra.Expected_Receipt_Date = PC.Document_Date
                    'End If

                    ''Proveedor
                    'BeI_nav_PedidoCompra.Buy_From_Vendor_Name = PC.Buy_from_Vendor_Name
                    'BeI_nav_PedidoCompra.Buy_From_Vendor_No = PC.Buy_from_Vendor_No

                    'lblprg.AppendText(String.Format("Procesando Pedido Compra: {0} ", BeI_nav_PedidoCompra.No, vbNewLine))
                    'lblprg.AppendText(vbNewLine)
                    'lblprg.SelectionStart = lblprg.TextLength
                    'lblprg.ScrollToCaret()

                    'vErrorAlInsertarClienteComoBodega = False

                    'If Not PC.Location_Code Is Nothing Then

                    '    vMensajeErrorClienteBodega = String.Format("La bodega {0} no está registrada como cliente y no es válida para recibir productos. {1} ", PC.Location_Code, vbNewLine)

                    '    If Not clsLnCliente.Bodega_Es_Valida_Para_Recepcion(PC.Location_Code, lConnection, lTransaction) Then

                    '        If XtraMessageBox.Show(vMensajeErrorClienteBodega + vbNewLine &
                    '                               "¿Insertar como cliente?", "Interface pedidos de compra.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    '            If Not clsSyncNavBodega.Insertar_Bodega_Single_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(PC.Location_Code,
                    '                                                                                                 True,
                    '                                                                                                 cnnLog,
                    '                                                                                                 lConnection,
                    '                                                                                                 lTransaction,
                    '                                                                                                 lblprg) Then
                    '                vErrorAlInsertarClienteComoBodega = True

                    '                Exit Function

                    '            End If

                    '        Else
                    '            vErrorAlInsertarClienteComoBodega = True
                    '        End If

                    '    Else
                    '        vErrorAlInsertarClienteComoBodega = True
                    '    End If

                    'End If

                    'If vErrorAlInsertarClienteComoBodega Then

                    '    clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensajeErrorClienteBodega,
                    '                                               PC.Location_Code,
                    '                                               0,
                    '                                               0)

                    '    lblprg.AppendText(vMensajeErrorClienteBodega)
                    '    lblprg.AppendText(vbNewLine)
                    '    lblprg.Refresh()
                    '    lblprg.SelectionStart = lblprg.TextLength
                    '    lblprg.ScrollToCaret()

                    'End If

                    'Try
                    '    '#EJC20180503: Es un documento de compra de proveedor
                    '    BeI_nav_PedidoCompra.Is_Internal_Transfer = False

                    '    'Insertar encabezado
                    '    clsLnI_nav_ped_compra_enc.Insertar(BeI_nav_PedidoCompra, lConnection, lTransaction)

                    '    VContadorBitacoraIntermedia += 1

                    '    prg.Value = vContador

                    '    vContador += 1

                    '    Application.DoEvents()

                    '    'Insertar detalle
                    '    If Not PC.PurchLines Is Nothing Then

                    '        For Each L As Purchase_Order_Line In PC.PurchLines

                    '            BeI_nav_PedidoCompraDet = New clsBeI_nav_ped_compra_det

                    '            Try

                    '                Try

                    '                    CopyObject(L, BeI_nav_PedidoCompraDet)

                    '                Catch ex As Exception
                    '                    Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                    '                End Try

                    '                BeI_nav_PedidoCompraDet.NoEnc = PC.No

                    '                If L.Type = Type.Item Then 'Es Producto

                    '                    If Not L.Location_Code Is Nothing Then

                    '                        '#EJC20210114: Se utiliza para determinar si la bodega a donde están intentando hacer la recepción
                    '                        'es o no una bodega válida, si es una bodega válida, debe haberse insertado previamente como cliente.
                    '                        If clsLnCliente.Bodega_Es_Valida_Para_Recepcion(L.Location_Code, lConnection, lTransaction) Then

                    '                            BeProductoBodega = clsLnProducto_bodega.Existe(L.No, lConnection, lTransaction)

                    '                            If BeProductoBodega Is Nothing Then
                    '                                If clsSyncNavProducto.Importar_Productos_DesdeWSNav_A_TablaIntermedia(L.No, lblprg, prg, cnnLog) Then
                    '                                    BeProductoBodega = clsLnProducto_bodega.Existe(L.No, lConnection, lTransaction)
                    '                                End If
                    '                            End If

                    '                            'Existe el producto en el maestro?
                    '                            If Not BeProductoBodega Is Nothing Then

                    '                                If L.Quantity <> L.Quantity_Received Then
                    '                                    If clsLnI_nav_ped_compra_det.Exist(BeI_nav_PedidoCompraDet, lConnection, lTransaction) Then
                    '                                        clsLnI_nav_ped_compra_det.Actualizar(BeI_nav_PedidoCompraDet, lConnection, lTransaction)
                    '                                        VContadorBitacoraIntermedia += 1
                    '                                    Else
                    '                                        clsLnI_nav_ped_compra_det.Insertar(BeI_nav_PedidoCompraDet, lConnection, lTransaction)
                    '                                        VContadorBitacoraIntermedia += 1
                    '                                    End If
                    '                                Else
                    '                                    '#EJC20180503: No importar las líneas que ya fueron completadas.
                    '                                    lblprg.AppendText(String.Format("Producto: {0} no tiene dif. <Esperado = Recibido>", L.No, vbNewLine))
                    '                                    lblprg.AppendText(vbNewLine)
                    '                                    lblprg.Refresh()
                    '                                    lblprg.SelectionStart = lblprg.TextLength
                    '                                    lblprg.ScrollToCaret()
                    '                                End If

                    '                            Else

                    '                                Try

                    '                                    clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro",
                    '                                      L.No,
                    '                                       BeNavEjecucionEnc.IdEjecucionEnc,
                    '                                       BeConfigDet.Idnavconfigdet, cnnLog)

                    '                                    lblprg.AppendText(String.Format("Producto no existe en maestro: {0}{1}", L.No, vbNewLine))
                    '                                    lblprg.AppendText(vbNewLine)
                    '                                    lblprg.Refresh()
                    '                                    lblprg.SelectionStart = lblprg.TextLength
                    '                                    lblprg.ScrollToCaret()

                    '                                Catch ex As Exception
                    '                                    Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                    '                                End Try

                    '                            End If 'Fin 'Existe el producto en el maestro?

                    '                        Else

                    '                            Try

                    '                                '#EJC20180614: Información no útil en log
                    '                                'clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no pertenece a lista de bodegas válidas para recepción", L.No,
                    '                                '   BeNavEjecucionEnc.Idejecucionenc,
                    '                                '   BeConfigDet.Idnavconfigdet, cnnLog)

                    '                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("La bodega no está registrada como cliente y no es válida para recibir el producto. Prod: {0} Bod: {1}{2}", L.No, L.Location_Code, vbNewLine),
                    '                                                            L.No,
                    '                                                            BeNavEjecucionEnc.IdEjecucionEnc,
                    '                                                            BeConfigDet.Idnavconfigdet,
                    '                                                            cnnLog)

                    '                                lblprg.AppendText(String.Format("La bodega no está registrada como cliente y no es válida para recibir el producto. Prod: {0} Bod: {1}{2}", L.No, L.Location_Code, vbNewLine))
                    '                                lblprg.AppendText(vbNewLine)
                    '                                lblprg.Refresh()
                    '                                lblprg.SelectionStart = lblprg.TextLength
                    '                                lblprg.ScrollToCaret()

                    '                            Catch ex As Exception
                    '                                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                    '                            End Try

                    '                        End If 'Fin Bodega_Es_Valida_Para_Recepcion

                    '                    Else

                    '                        If Not L.No Is Nothing Then

                    '                            Try

                    '                                clsLnI_nav_ejecucion_det_error.Inserta_Log("No está definida bodega para producto, no se importará", L.No,
                    '                                BeNavEjecucionEnc.IdEjecucionEnc,
                    '                                BeConfigDet.Idnavconfigdet, cnnLog)

                    '                                lblprg.AppendText(String.Format("No está definida bodega para producto, no se importará: {0}{1}", L.No, vbNewLine))
                    '                                lblprg.AppendText(vbNewLine)
                    '                                lblprg.Refresh()
                    '                                lblprg.SelectionStart = lblprg.TextLength
                    '                                lblprg.ScrollToCaret()

                    '                            Catch ex As Exception
                    '                                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                    '                            End Try

                    '                        End If

                    '                    End If 'Fin location code is nothing                                        

                    '                End If

                    '            Catch ex As Exception

                    '                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                    '                                                    "Sin informacion",
                    '                                                    BeNavEjecucionEnc.IdEjecucionEnc,
                    '                                                    BeConfigDet.Idnavconfigdet, cnnLog)

                    '                lblprg.AppendText(String.Format("Error al insertar Linea desde el ws a intermedia en pedido de compra: {0}{1}{2}", BeI_nav_PedidoCompraDet.No, vbNewLine, ex.Message))
                    '                lblprg.AppendText(vbNewLine)
                    '                lblprg.Refresh()
                    '                lblprg.SelectionStart = lblprg.TextLength
                    '                lblprg.ScrollToCaret()

                    '            End Try

                    '        Next

                    '    Else
                    '        Console.WriteLine("Pedido de compra sin lineas de detalle?")
                    '    End If

                    'Catch ex As Exception

                    '    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                    '                                      BeI_nav_PedidoCompra.No,
                    '                                      BeNavEjecucionEnc.IdEjecucionEnc,
                    '                                      BeConfigDet.Idnavconfigdet, cnnLog)

                    '    lblprg.AppendText(String.Format("Error al insertar Encabezado OC desde ws a intermedia: {0}{1}{2}", BeI_nav_PedidoCompra.No, vbNewLine, ex.Message))
                    '    lblprg.AppendText(vbNewLine)
                    '    lblprg.Refresh()
                    '    lblprg.SelectionStart = lblprg.TextLength
                    '    lblprg.ScrollToCaret()

                    'End Try

                Next

            End If

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Devolucion_Desde_WSNav_A_TablaIntermedia = True

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

    Public Function Get_Devolucion_FromWS(ByVal lblprg As RichTextBox, Optional ByVal AplicarFiltros As Boolean = True) As List(Of Devolucion)

        Try

            Dim lDevolucion As New List(Of Devolucion)
            Dim StartDate As String = "01092021D"

            If AplicarFiltros Then

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("********** APLICANDO FILTROS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

                Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
                lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Devolucion)

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

                Dim vFiltro1 As New Devolucion_Filter() With {.Field = Devolucion_Fields.Location_Code, .Criteria = vCriteria}

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("-Location_Code-")
                lblprg.AppendText("Criteria: " & vCriteria)
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

                Dim vFiltro2 As New Devolucion_Filter() With {.Field = Devolucion_Fields.Status, .Criteria = "1"}

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("-Status-")
                lblprg.AppendText("Criteria: 1")
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                lblprg.Refresh()

                'Dim vFiltro3 As New Pedidos_Compra_Filter() With {.Field = Pedidos_Compra_Fields.Posting_Date, .Criteria = StartDate} '"01/03/2021.."

                'lblprg.AppendText(vbNewLine)
                'lblprg.AppendText("-Posting_Date-")
                'lblprg.AppendText("Criteria: " & StartDate)
                'lblprg.AppendText(vbNewLine)
                'lblprg.SelectionStart = lblprg.TextLength
                'lblprg.ScrollToCaret()
                'lblprg.Refresh()

                Dim vFiltros As Devolucion_Filter() = New Devolucion_Filter() {vFiltro1, vFiltro2}

                wsDevolucion.Url = My.Settings.NavSync_WSDevolucion_Devolucion_Service

                fichaDevolucion = wsDevolucion.ReadMultiple(vFiltros, Nothing, 500)

                For Each PC As Devolucion In fichaDevolucion
                    lDevolucion.Add(PC)
                Next

            Else

                fichaDevolucion = wsDevolucion.ReadMultiple(Nothing, Nothing, 500)

                For Each PC As Devolucion In fichaDevolucion
                    lDevolucion.Add(PC)
                Next

            End If

            Return lDevolucion

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        Throw New NotImplementedException()
    End Sub

End Class
