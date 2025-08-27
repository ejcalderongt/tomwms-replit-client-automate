Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmEjecucion

    Public Property Interface_A_Ejecutar As frmMenu.pInterfaceAEjecutar = -1

    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub Ejecuta_interface_Traslados_SAP(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar traslados?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then

                lblprg.Text = ""

                Using sPT As New clsSyncSAPSSolicitudTraslado()
                    sPT.Ejecutar_Interfaz("Transferencia_Stock")
                    sPT.Importar_Solicitud_Traslado_SAP(lblprg, prg, True, Preguntar)
                End Using

            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Public Sub Ejecuta_interface_Traslado_Mercancias_SAP(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar traslados?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPC As New clsSyncSAPTrasladoStock()
                    sPC.Ejecutar_Interfaz("Traslados")
                    sPC.Importar_Trasladados_SAP(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Public Function Ejecuta_Interface_Bodegas(Optional ByVal Preguntar As Boolean = True) As Boolean

        Ejecuta_Interface_Bodegas = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sBod As New clsSyncSAPBodega()
                    sBod.Ejecutar_Interfaz("Bodega")
                    Ejecuta_Interface_Bodegas = sBod.Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        Finally
            prg.Visible = False
        End Try

    End Function

    Public Function Ejecuta_Interface_Proveedores(Optional ByVal Preguntar As Boolean = True) As Boolean

        Ejecuta_Interface_Proveedores = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sProv As New clsSyncSAPProveedor()
                    sProv.Ejecutar_Interfaz("Proveedor")
                    Ejecuta_Interface_Proveedores = sProv.Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            prg.Visible = False

        End Try

    End Function

    Private Sub mnuActualizarProveedores_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarProveedores.ItemClick

        If Ejecuta_Interface_Proveedores(True) Then
            'Close()
        End If

    End Sub

    Private Function Ejecuta_Interface_Pedidos_Compra_SAP(Optional ByVal Preguntar As Boolean = True) As Boolean

        Ejecuta_Interface_Pedidos_Compra_SAP = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar pedidos de compra?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Using sPc As New clsSyncSAPPedidoCompra()
                    sPc.Ejecutar_Interfaz("Pedido compra")
                    Ejecuta_Interface_Pedidos_Compra_SAP = sPc.Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        End Try

    End Function

    Public Function Ejecuta_Interface_Productos(Optional ByVal Preguntar As Boolean = True) As Boolean

        Ejecuta_Interface_Productos = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar Productos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            '#EJC202404161359: Bool
            If Ejecutar Then
                Using sProd As New clsSyncSAPProducto()
                    sProd.Ejecutar_Interfaz("Producto")
                    Ejecuta_Interface_Productos = sProd.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        Finally
            prg.Visible = False
        End Try

    End Function

    Private tiempoProductos As Double = 0
    Dim frecuencia As Double = 0

    Private Sub timerProducto_Tick(sender As Object, e As EventArgs) Handles timerProducto.Tick
        If (frecuencia * 3) = tiempoProductos AndAlso Not BwProducto.IsBusy Then
            BwProducto.RunWorkerAsync()
            tiempoProductos = 0
            timerProducto.Enabled = False
        Else
            tiempoProductos += 1
        End If
    End Sub

    ' Timer para correr el proceso de producto
    Private Sub BwProducto_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BwProducto.DoWork

        Try
            Using sProd As New clsSyncSAPProducto()
                sProd.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg)
            End Using
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub BwProducto_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BwProducto.RunWorkerCompleted
        timerProducto.Enabled = True
    End Sub

    Private Sub BarButtonItem1_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReporteEjecuciones.ItemClick

        With frmEjecuciones
            .Modo = frmEjecuciones.pModo.Lista
            .WindowState = FormWindowState.Normal
            .Show()
            .Focus()
        End With

    End Sub

    Private Sub mnuProductosI_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuProductos.ItemClick
        If Ejecuta_Interface_Productos(True) Then
            'Close()
        End If
    End Sub

    Private Function Enviar_Pedidos_Compra(Optional ByVal Preguntar As Boolean = True) As Boolean

        Enviar_Pedidos_Compra = False

        Dim MostrarMensajeError As Boolean = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar registros de ingreso?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Using sPc As New clsSyncSAPPedidoCompra()
                    sPc.Ejecutar_Interfaz("Envio ingresos")
                    Enviar_Pedidos_Compra = sPc.Enviar_Transacciones_De_Ingreso_SAP(lblprg, prg)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            If MostrarMensajeError Then
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Else
                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            End If

        End Try

    End Function

    Private Sub mnuEnviarPedidosCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarPedidosCompra.ItemClick
        Enviar_Pedidos_Compra(True)
    End Sub

    Private Sub frmEjecucion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Application.DoEvents()

            Me.Text = "MI3_SYNC ->TOM, WMS (" & gVersionApp & " " & FormatoFechas.tFecha(gFechaVersion) & ")"

            clsPublic.Actualizar_Progreso(lblprg, "Interface TOMWMS - SAP")
            'clsPublic.Actualizar_Progreso(lblprg, "Ejecución remota iniciada con parámetro: " & Interface_A_Ejecutar.ToString)

            If Interface_A_Ejecutar <> -1 Then


                mnuActualizarProveedores.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuReporteEjecuciones.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            End If

            Select Case Interface_A_Ejecutar

                Case pInterfaceAEjecutar.Importar_Productos
                    mnuProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    If Ejecuta_Interface_Productos(False) Then
                        'Close()
                    End If
                Case pInterfaceAEjecutar.Importar_Pedidos_De_Compra
                    mnuProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    If Ejecuta_Interface_Pedidos_Compra_SAP(False) Then
                        'Close()
                    End If
                Case pInterfaceAEjecutar.Enviar_Pedidos_Compra
                    mnuEnviarPedidosCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    If Enviar_Pedidos_Compra(False) Then
                        'Close()
                    End If
                Case pInterfaceAEjecutar.Enviar_Pedidos_Transferencia
                    mnuEnviarPedidosTransferencia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Enviar_Pedidos_Transferencia(False)
                Case pInterfaceAEjecutar.Actualizar_Traslado_No_Enviado
                    Actualizar_Estado_Traslado(NoDocEntrySAP, EstadoEnviadoSAP)
                Case pInterfaceAEjecutar.Actualizar_Pedido_Cliente_No_Enviado
                    If Actualizar_Estado_Pedido_Cliente(NoDocEntrySAP, EstadoEnviadoSAP) Then
                        Close()
                    End If
                Case pInterfaceAEjecutar.Enviar_Pedidos_Cliente_SAP
                    mnuEnviaPedidosTransferencia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Enviar_Documentos_Salida(False)
                Case pInterfaceAEjecutar.Enviar_Devolucion_Proveedor_SAP
                    mnuEnviarDevolucion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Enviar_Devolucion_Solicitud(False)
                Case pInterfaceAEjecutar.Enviar_Traslados_SAP
                    mnuEnviarTrasladosMercancia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Enviar_Traslado_Stock(False)
                Case pInterfaceAEjecutar.Actualizar_Devolucion_Proveedor_No_Enviado_SAP
                    If Actualizar_Estado_Devolucion_Proveedor(NoDocEntrySAP, EstadoEnviadoSAP) Then
                        Close()
                    End If
                Case Else
                    Exit Select
            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub Enviar_Pedidos_Transferencia(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar registros de traslados?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPT As New clsSyncSAPPedidoTraslado()
                    sPT.Ejecutar_Interfaz("Salida_Mercancia")
                    sPT.Enviar_Transacciones_De_Salida(lblprg, prg)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            If MostrarMensaje Then
                XtraMessageBox.Show(vMensaje,
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            Else
                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            End If

        End Try

    End Sub
    Private Sub Enviar_Ajustes(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar ajustes?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Using sPT As New clsSyncAjusteInventario()
                    sPT.Ejecutar_Interfaz("Sync_Ajustes")
                    sPT.Sync_Ajustes(lblprg, prg)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Private Sub mnuEnviarPedidosTransferencia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarPedidosTransferencia.ItemClick
        Enviar_Pedidos_Transferencia(True)
    End Sub

    Private Sub cmdRptTransac_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdRptTransac.ItemClick

        Try

            With frmRegistrosInterface
                .Modo = frmRegistrosInterface.pModo.Lista
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Enviar_Ajustes(True)
    End Sub
    Private Sub Args_Showing(ByVal sender As Object, ByVal e As XtraMessageShowingArgs)
        e.Form.Icon = Me.Icon
    End Sub

    Private Sub mnuEnvios_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPedidoCliente.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCliente As String = ""

        Try

            args.Caption = "Ingrese pedido de cliente"
            args.Prompt = "Pedido No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                pPedidoCliente = tmpResult.ToString

                Ejecuta_interface_Pedido_Cliente(True, pPedidoCliente)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Public Sub Ejecuta_interface_Devolucion_Mercancia(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar devolución de mercancías?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Using sPT As New clsSyncSAPDevolucionMercancia()
                    sPT.Ejecutar_Interfaz("Solicitud_Devolución")
                    sPT.Insertar_Solicitud_Devol_Cli_A_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Private Sub Ejecuta_Interface_NC_Cliente(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar devoluciones de clientes ( Trans -> pedidos de ingrseo)?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPc As New clsSyncSAPDevolucionMeracnciaCliente()
                    sPc.Ejecutar_Interfaz("Pedido compra Devolucion Cliente")
                    sPc.Insertar_NC_Cliente_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            'XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuPedidosCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPedidosCompra.ItemClick

        If Ejecuta_Interface_Pedidos_Compra_SAP(True) Then
            'Close()
        End If

    End Sub

    Private Sub mnuBodegas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBodegas.ItemClick

        If Ejecuta_Interface_Bodegas(True) Then
            'Close()
        End If

    End Sub

    Private Sub mnuPedidosTransferencia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPedidosTransferencia.ItemClick
        'Version de CArolina que lee traslados ejecutados con aprobación 
        'Ejecuta_interface_Traslado_Mercancias_SAP(True)
        Ejecuta_interface_Traslados_SAP(True)
    End Sub

    Private Sub mnuPedidosCompraDevolucionCliente_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNotasCreditoCliente.ItemClick
        Ejecuta_Interface_NC_Cliente(True)
    End Sub

    Private Sub mnuDevolucionMercancia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDevolucionMercancia.ItemClick
        Ejecuta_interface_Devolucion_Mercancia(True)
    End Sub

    Private Sub mnuEntradaMerncacia_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEntradaMerncacia.ItemClick
        Ejecuta_Interface_Entrada_Mercancia()
    End Sub

    Private Sub Ejecuta_Interface_Entrada_Mercancia()

        Dim MostrarMensajeError As Boolean = False
        Dim Preguntar As Boolean = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar registros de ingreso?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPc As New clsSyncSAPEntradaMercancia()
                    sPc.Insertar_Entrada_Mercancia_Desde_SAP(lblprg, prg, True)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        End Try

    End Sub

    Private Sub mnuEnvioPedidosCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnvioPedidosCompra.ItemClick
        If Enviar_Pedidos_Compra(True) Then
            'Close()            
        End If
    End Sub

    Private Sub mnuEnviarAjustes_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarAjustes.ItemClick
        Enviar_Ajustes(True)
    End Sub

    Private Sub BarButtonItem4_ItemClick_2(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviaPedidosTransferencia.ItemClick
        Enviar_Documentos_Salida(True)
    End Sub

    Private Sub mnuTestConexion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTestConexion.ItemClick

        Try

            Dim TestCon As New frmTestCon
            TestCon.ShowDialog()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub Ejecuta_interface_Pedido_Cliente(Optional ByVal Preguntar As Boolean = True,
                                                Optional ByVal pPedidoCliente As String = "")

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar pedido de cliente?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPC As New clsSyncSAPSPedidoCliente()
                    sPC.Ejecutar_Interfaz("Pedido_Cliente")
                    sPC.Importar_Pedido_Cliente_SAP(lblprg, prg, True, Preguntar, pPedidoCliente)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Private Sub Enviar_Documentos_Salida(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar salidas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Using sPT As New clsSyncSAPSPedidoCliente()
                    sPT.Ejecutar_Interfaz("Salida_Mercancia")
                    sPT.Enviar_Transacciones_De_Salida(lblprg, prg, clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            If MostrarMensaje Then
                XtraMessageBox.Show(vMensaje,
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            Else
                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            End If

        End Try

    End Sub

    Private Sub mnuImportarAjustes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImportarAjustes.ItemClick
        Ejecuta_interface_Ajustes()
    End Sub

    Public Sub Ejecuta_interface_Ajustes(Optional ByVal Preguntar As Boolean = True,
                                         Optional ByVal pNoAjuste As String = "")

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar ajustes?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPC As New clsSyncSAPAjustes()
                    sPC.Ejecutar_Interfaz("Pedido_Cliente")
                    sPC.Importar_Ajustes_SAP(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Private Function Actualizar_Estado_Traslado(ByVal pNoDocEntrySAP As Integer, EstadoEnvio As clsDataContractDI.Estado_Enviado_SAP) As Boolean

        Actualizar_Estado_Traslado = False

        Try

            Dim SapTrasladoSync As New clsSyncSAPTrasladoStock
            SapTrasladoSync.Marcar_Trasladado_Sincronizado_SAP(pNoDocEntrySAP, EstadoEnvio)

            Actualizar_Estado_Traslado = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Actualizar_Estado_Pedido_Cliente(ByVal pNoDocEntrySAP As Integer, EstadoEnvio As clsDataContractDI.Estado_Enviado_SAP) As Boolean

        Actualizar_Estado_Pedido_Cliente = False

        Try

            Dim SapTrasladoSync As New clsSyncSAPSPedidoCliente

            If SapTrasladoSync.Marcar_Pedido_Cliente_Sincronizado_SAP(pNoDocEntrySAP, EstadoEnvio, lblprg) Then
                Actualizar_Estado_Pedido_Cliente = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Actualizar_Estado_Devolucion_Proveedor(ByVal pNoDocEntrySAP As Integer, EstadoEnvio As clsDataContractDI.Estado_Enviado_SAP) As Boolean

        Actualizar_Estado_Devolucion_Proveedor = False

        Try

            Dim SapDevolucionProveedorSync As New clsSyncSapDevolucionProveedor

            If SapDevolucionProveedorSync.Marcar_Solicitud_Devolucion_Sincronizado_SAP(pNoDocEntrySAP, EstadoEnvio, lblprg) Then
                Actualizar_Estado_Devolucion_Proveedor = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub mnuImportarInvTeoricoSAP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImportarInvTeoricoSAP.ItemClick

        Try

            If XtraMessageBox.Show("¿Importar inventario teórico?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim InvTeoricoSAPToOC As New clsSyncSAPPedidoCompra

                '#EJC202404030005AM: Nueva versión para crear un documento de ingreso por bodega, para que en consecuencia la recepción se pueda hacer por bodega (ubicación).
                If Not InvTeoricoSAPToOC.Crear_Pedido_Compra_Desde_Inventario_Teorico_By_Bodega(lblprg) Then
                    clsPublic.Actualizar_Progreso(lblprg, "No se concluyó la creación de documentos, revise el log de errores.")
                End If

            End If


        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
        End Try

    End Sub

    Private Sub mnuReporteExistenciasComparativoWMSSAP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReporteExistenciasComparativoWMSSAP.ItemClick

        Try

            Dim frmStockPorLote As New frmStockPorLote
            frmStockPorLote.Modo = frmStockPorLote.pModo.Lista
            frmStockPorLote.ShowDialog()
            frmStockPorLote.Dispose()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuPrefactura_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPrefactura.ItemClick

        Try


            If Ejecuta_Interface_Facturas_Reserva_SAP(True) Then
                'Close()
            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Function Ejecuta_Interface_Facturas_Reserva_SAP(Optional ByVal Preguntar As Boolean = True) As Boolean

        Ejecuta_Interface_Facturas_Reserva_SAP = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar facturas de reserva?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPc As New clsSyncSAPFacturaReservaAcreedores()
                    sPc.Ejecutar_Interfaz("Factura de Reserva")
                    Ejecuta_Interface_Facturas_Reserva_SAP = sPc.Insertar_Facturas_Acreedores_A_Tabla_TOMWMS(lblprg, prg, True, False)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        End Try

    End Function

    Private Sub mnuSolicitudDevolucion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSolicitudDevolucion.ItemClick


        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCliente As String = ""

        Try

            args.Caption = "Ingrese No. de devolución"
            args.Prompt = "Solicitud No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                pPedidoCliente = tmpResult.ToString

                lblprg.Text = ""

                Ejecuta_interface_Devolucion(True, pPedidoCliente)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Sub Ejecuta_interface_Devolucion(Optional ByVal Preguntar As Boolean = True,
                                            Optional ByVal pPedidoCliente As String = "")

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar devolución a proveedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPC As New clsSyncSapDevolucionProveedor()
                    sPC.Ejecutar_Interfaz("Devolucion_Proveedor")
                    sPC.Procesar_Devolucion_Mercancia_SAP(lblprg, prg, pPedidoCliente)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
            End If
        End Try

    End Sub

    Private Sub Enviar_Devolucion_Solicitud(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar devolución sobre solicitud?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Using sPT As New clsSyncSapDevolucionProveedor()
                    sPT.Ejecutar_Interfaz("Devolución_Mercancia")
                    sPT.Enviar_Transacciones_De_Salida(lblprg,
                                                       prg,
                                                       clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            If MostrarMensaje Then
                XtraMessageBox.Show(vMensaje,
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            Else
                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            End If

        End Try

    End Sub

    Private Sub mnuEnviarDevolucion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarDevolucion.ItemClick

        Try
            Enviar_Devolucion_Solicitud(True)
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try

    End Sub

    Private Sub mnuEnviarTrasladosMercancia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarTrasladosMercancia.ItemClick
        Enviar_Traslado_Stock(True)
    End Sub

    Private Sub Enviar_Traslado_Stock(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Generar traslados de stock?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Using sPT As New clsSyncSAPSSolicitudTraslado()
                    sPT.Ejecutar_Interfaz("Devolución_Mercancia")
                    sPT.Enviar_Transacciones_De_Salida(lblprg,
                                                       prg,
                                                       clsDataContractDI.tTipoDocumentoSalida.Traslado_Por_Estados_SAP)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            If MostrarMensaje Then
                XtraMessageBox.Show(vMensaje,
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            Else
                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            End If

        End Try

    End Sub

    Private Sub frmEjecucion_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Dim mdiParent As frmMenu = TryCast(Me.MdiParent, frmMenu)
        If mdiParent IsNot Nothing Then
            mdiParent.CheckAndCloseIfNoChildren()
        End If
    End Sub

    Private Sub mnuReporteComparativo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReporteComparativo.ItemClick


        Try

            Dim frmStockPorLote As New frmStockPorLoteArea
            frmStockPorLote.Modo = frmStockPorLoteArea.pModo.Lista
            frmStockPorLote.ShowDialog()
            frmStockPorLote.Dispose()

        Catch ex As Exception

        End Try

    End Sub

End Class