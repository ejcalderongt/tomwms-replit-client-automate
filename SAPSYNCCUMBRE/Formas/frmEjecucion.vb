Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors

Public Class frmEjecucion
    Public Property Interface_A_Ejecutar As Integer = -1
    Private procesoEnEjecucion As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub Ejecuta_interface_Traslados_SAP(Optional ByVal Preguntar As Boolean = True,
                                               Optional ByVal pTraslado As String = "")

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
                Using sPT As New clsSyncSAPSSolicitudTraslado()
                    sPT.Ejecutar_Interfaz("Transferencia_Stock")
                    sPT.Importar_Solicitud_Traslado_SAP(lblprg, prg, False, True, Preguntar, pTraslado)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Public Sub Ejecuta_interface_Traslado_Mercancias_SAP(Optional ByVal Preguntar As Boolean = True,
                                                         Optional ByVal pTraslado As String = "")

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
                    sPC.Importar_Trasladados_SAP(lblprg, prg, True, Preguntar, pTraslado)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Public Sub Ejecuta_Interface_Bodegas(Optional ByVal Preguntar As Boolean = True)

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
                    sBod.Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        Finally
            prg.Visible = False
        End Try

    End Sub

    Public Sub Ejecuta_Interface_Proveedores(Optional ByVal Preguntar As Boolean = True)

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
                    sProv.Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            prg.Visible = False

        End Try

    End Sub

    Private Sub mnuActualizarProveedores_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarProveedores.ItemClick
        Ejecuta_Interface_Proveedores(True)
    End Sub

    Private Sub Ejecuta_Interface_Pedidos_Compra_SAP(Optional ByVal Preguntar As Boolean = True,
                                                     Optional ByVal pNoDocumentoCompraSAP As String = "")

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
                Using sPc As New clsSyncSAPPedidoCompra()
                    sPc.Ejecutar_Interfaz("Pedido compra")
                    sPc.Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar, pNoDocumentoCompraSAP)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        End Try

    End Sub

    Public Sub Ejecuta_Interface_Productos(Optional ByVal Preguntar As Boolean = True)

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

            If Ejecutar Then
                Using sProd As New clsSyncSAPProducto()
                    sProd.Ejecutar_Interfaz("Producto")
                    sProd.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        Finally
            prg.Visible = False
        End Try

    End Sub

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

    Private Sub mnuProductosI_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuProductosI.ItemClick
        mnuProductosI.Enabled = False
        Ejecuta_Interface_Productos(True)
        mnuProductosI.Enabled = True
    End Sub

    Private Sub Enviar_Pedidos_Compra(Optional ByVal Preguntar As Boolean = True)

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

            lblprg.Text = ""

            If Ejecutar Then
                Using sPc As New clsSyncSAPPedidoCompra()
                    sPc.Ejecutar_Interfaz("Envio ingresos")
                    clsSyncSAPPedidoCompra.Enviar_Transacciones_De_Ingreso_SAP(lblprg, prg)
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

    End Sub
    Private Sub frmEjecucion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Application.DoEvents()

            Me.Text = "MI3_SYNC ->TOM, WMS (" & gVersionApp & " " & FormatoFechas.tFecha(gFechaVersion) & ")"

            'clsPublic.Actualizar_Progreso(lblprg, "Interface TOMWMS - SAP")
            'clsPublic.Actualizar_Progreso(lblprg, "Ejecución remota iniciada con parámetro: " & Interface_A_Ejecutar.ToString)

            If Interface_A_Ejecutar <> -1 Then

                mnuActualizarProveedores.Visibility = BarItemVisibility.Never
                mnuProductosI.Visibility = BarItemVisibility.Never
                mnuImprimir.Visibility = BarItemVisibility.Never
                mnuReporteEjecuciones.Visibility = BarItemVisibility.Never

            End If

            '#EJC20240709: Permisos por rol en interface parte 1.
            Try

                DesactivarMenu()

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

                Dim BeUsuarioBodega As New clsBeUsuario_bodega
                If IdUsuario = -1 Then

                    If BeConfigEnc IsNot Nothing Then
                        IdUsuario = BeConfigEnc.IdUsuario
                    End If

                End If

                BeUsuarioBodega = clsLnUsuario_bodega.Get_Single_By_IdUsuario_And_IdBodega(IdUsuario, BeConfigEnc.Idbodega)

                If Not BeUsuarioBodega Is Nothing Then
                    HabilitarMenuRol(BeUsuarioBodega.IdRol, rbMain)
                End If

            Catch ex As Exception
                Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
                prg.Visible = False
            End Try

            Select Case Interface_A_Ejecutar

                Case pInterfaceAEjecutar.Importar_Productos
                    mnuProductosI.Visibility = BarItemVisibility.Always
                    Ejecuta_Interface_Productos(False)

                Case pInterfaceAEjecutar.Importar_Pedidos_De_Compra
                    mnuProductosI.Visibility = BarItemVisibility.Always
                    Ejecuta_Interface_Pedidos_Compra_SAP(False)

                Case pInterfaceAEjecutar.Importar_Pedidos_De_Transferencia
                    Ejecuta_interface_Traslados_Entrada_SAP(False)
                Case pInterfaceAEjecutar.Enviar_Pedidos_Compra
                    mnuEnviarPedidosCompra.Visibility = BarItemVisibility.Always
                    Enviar_Pedidos_Compra(False)
                Case pInterfaceAEjecutar.Enviar_Pedidos_Transferencia
                    mnuEnviarPedidosTransferencia.Visibility = BarItemVisibility.Always
                    Enviar_Traslado_Stock(False)
                Case pInterfaceAEjecutar.Enviar_Traslados_SAP
                    mnuEnviarTrasladosMercancia.Visibility = BarItemVisibility.Always
                    Enviar_Traslado_Desde_Solicitud(False)
                Case pInterfaceAEjecutar.Enviar_Devolucion_Proveedor_SAP
                    mnuEnviarSolicitudDevolucion.Visibility = BarItemVisibility.Always
                    Enviar_Solicitud_Devolucion_Proveedor(False)
                Case pInterfaceAEjecutar.Actualizar_Traslado_No_Enviado
                    Actualizar_Estado_Traslado(NoDocEntrySAP, EstadoEnviadoSAP)
                Case pInterfaceAEjecutar.Actualizar_Pedido_Cliente_No_Enviado
                    If Actualizar_Estado_Pedido_Cliente(NoDocEntrySAP, EstadoEnviadoSAP) Then
                        Close()
                    End If
                Case pInterfaceAEjecutar.Enviar_Ajustes_Inventario
                    mnuEnviarAjustes.Visibility = BarItemVisibility.Never
                    Enviar_Ajustes(True)
                Case pInterfaceAEjecutar.Cerrar_Documento_Salida_SAP
                    mnuCerrarDocumentoSAP.Visibility = BarItemVisibility.Never
                    If Cerrar_Solicitud_Traslado(NoDocEntrySAP) Then
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
                Using sPT As New clsSyncAjusteInventario()
                    sPT.Ejecutar_Interfaz("Sync_Ajustes")
                    Try
                        sPT.Sync_Ajustes(lblprg, prg)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
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

    Public Sub Ejecuta_interface_Devolucion_Mercancia(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar devolucion de mercancías?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPT As New clsSyncSAPDevolucionMercancia()
                    sPT.Ejecutar_Interfaz("Pedido_transferencia_envío")
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


        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCompra As String = ""

        Try

            mnuPedidosCompra.Enabled = False

            args.Caption = "Ingrese pedido de compra"
            args.Prompt = "Pedido No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            lblprg.Text = ""

            If Not tmpResult Is Nothing Then

                pPedidoCompra = tmpResult.ToString

                Ejecuta_Interface_Pedidos_Compra_SAP(True, pPedidoCompra)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            mnuPedidosCompra.Enabled = True
        End Try

    End Sub

    Private Sub mnuBodegas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBodegas.ItemClick
        Ejecuta_Interface_Bodegas(True)
    End Sub

    Private Sub mnuPedidosTransferencia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSolicitudTraslado.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pTraslado As String = ""

        Try

            args.Caption = "Ingrese número de traslado"
            args.Prompt = "Traslado No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                pTraslado = tmpResult.ToString
                'Ejecuta_interface_Traslado_Mercancias_SAP(True, pTraslado)
                Ejecuta_interface_Traslados_SAP(True, pTraslado)
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

    Private Sub BarButtonItem4_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pTraslado As String = ""

        Try

            args.Caption = "Ingrese número de traslado"
            args.Prompt = "Traslado No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                pTraslado = tmpResult.ToString

                Ejecuta_interface_Traslados_SAP(True, pTraslado)

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

    Private Sub mnuPedidosCompraDevolucionCliente_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Ejecuta_Interface_NC_Cliente(True)
    End Sub

    Private Sub mnuDevolucionMercancia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Ejecuta_interface_Devolucion_Mercancia(True)
    End Sub

    Private Sub mnuEntradaMerncacia_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
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

    Private Sub mnuEnviarPedidosCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarPedidosCompra.ItemClick
        Enviar_Pedidos_Compra(True)
    End Sub

    Private Sub mnuEnviarAjustes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarAjustes.ItemClick
        mnuEnviarAjustes.Visibility = BarItemVisibility.Never
        Enviar_Ajustes(True)
        mnuEnviarAjustes.Visibility = BarItemVisibility.Always
    End Sub

    Private Sub BarButtonItem4_ItemClick_2(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Enviar_Documentos_Salida(True)
    End Sub

    Private Sub mnuTestConexion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTestConexion.ItemClick

        Try

            Dim TestCon As New frmTestCon
            TestCon.ShowDialog()

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
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
                Using sPT As New clsSyncSAPSPedidoCliente()
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

    Private Function Actualizar_Estado_Traslado(ByVal pNoDocEntrySAP As Integer, EstadoEnvio As clsDataContractDI.Estado_Enviado_SAP) As Boolean

        Actualizar_Estado_Traslado = False

        Try

            Dim SapTrasladoSync As New clsSyncSAPTrasladoStock
            SapTrasladoSync.Marcar_Trasladado_Sincronizado_SAP(pNoDocEntrySAP, EstadoEnvio)

            Actualizar_Estado_Traslado = True

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
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
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try

    End Function

    Private Sub mnuActualizarCodigosBarra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarCodigosBarra.ItemClick

        Try

            Dim SapProductoCodigosBarra As New clsSyncSapCodigosBarra
            SapProductoCodigosBarra.Importar_Codigos_Barra_Productos(lblprg, prg)

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try


    End Sub

    Private Sub mnuCentrosCosto_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCentrosCosto.ItemClick

        Try


            Dim SAPCentrosCosto As New clsSyncSapCentrosCosto
            SAPCentrosCosto.Importar_Centros_Costos_From_SAP(lblprg, prg)

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try

    End Sub

    Private Sub mnuPresentaciones_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPresentaciones.ItemClick

        Try

            Dim SAPPresentaciones As New clsSyncSAPPresentaciones
            SAPPresentaciones.Importar_Presentaciones_Productos(lblprg, prg)

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try

    End Sub

    Private Sub mnuEnviarTrasladosMercancia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarTrasladosMercancia.ItemClick

        Try
            mnuEnviarTrasladosMercancia.Visibility = BarItemVisibility.Never
            Enviar_Traslado_Stock(True)
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
        Finally
            mnuEnviarTrasladosMercancia.Visibility = BarItemVisibility.Always
        End Try

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
                                                       clsDataContractDI.tTipoDocumentoSalida.Transferencia_Directa)
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

    Private Sub mnuTrasladoDesdeSolicitud_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTrasladoDesdeSolicitud.ItemClick
        mnuTrasladoDesdeSolicitud.Visibility = BarItemVisibility.Never
        Enviar_Traslado_Desde_Solicitud(True)
        mnuTrasladoDesdeSolicitud.Visibility = BarItemVisibility.Always
    End Sub

    Private Sub Enviar_Traslado_Desde_Solicitud(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False
        procesoEnEjecucion = True

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Generar traslados de stock desde solicitud?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Using sPT As New clsSyncSAPSSolicitudTraslado()
                    sPT.Ejecutar_Interfaz("Traslado_Mercancia")
                    sPT.Enviar_Traslados_Desde_Solicitud(lblprg,
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

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuSolicitudTrasladoEntrada_ItemClick(sender As Object,
                                                      e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSolicitudTrasladoEntrada.ItemClick


        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pTraslado As String = ""

        Try

            args.Caption = "Ingrese número de traslado de entrada"
            args.Prompt = "Traslado No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then
                pTraslado = tmpResult.ToString
                Ejecuta_interface_Traslados_Entrada_SAP(True, pTraslado)
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

    Public Sub Ejecuta_interface_Traslados_Entrada_SAP(Optional ByVal Preguntar As Boolean = True,
                                                       Optional ByVal pTraslado As String = "")

        Dim MostrarMensaje As Boolean = False

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar traslados de entrada?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPT As New clsSyncSAPSSolicitudTraslado()
                    sPT.Ejecutar_Interfaz("Transferencia_Stock")
                    sPT.Importar_Solicitud_Traslado_Entrada_SAP(lblprg, prg, True, Preguntar, pTraslado)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub

    Private Sub mnuDevolucionProveedor_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDevolucionProveedor.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCliente As String = ""

        Try

            mnuDevolucionProveedor.Enabled = False

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

        Finally
            mnuDevolucionProveedor.Enabled = True
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

    Private Sub mnuEnviarSolicitudDevolucion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarSolicitudDevolucion.ItemClick
        mnuEnviarSolicitudDevolucion.Enabled = False
        Enviar_Solicitud_Devolucion_Proveedor(True)
        mnuEnviarSolicitudDevolucion.Enabled = True
    End Sub

    Private Sub Enviar_Solicitud_Devolucion_Proveedor(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Generar solicitud de devolución a proveedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                Using sPT As New clsSyncSAPSSolicitudTraslado()
                    sPT.Ejecutar_Interfaz("Traslado_Mercancia")
                    sPT.Enviar_Solicitud_Devolucion_Proveedor(lblprg,
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

    Private Sub mnuClientes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuClientes.ItemClick
        Ejecuta_Interface_Clientes(True)
    End Sub

    Public Sub Ejecuta_Interface_Clientes(Optional ByVal Preguntar As Boolean = True)

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
                    sProv.Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)
            prg.Visible = False

        End Try

    End Sub

    Private Sub frmEjecucion_Load(sender As Object, e As EventArgs) Handles MyBase.Load



    End Sub

    Private Sub DesactivarMenu()

        Try

            Dim mCurrentItem As BarItem
            Dim mBarSubItem As BarSubItem
            Dim mSubLink As BarItemLink

            For Each currentPage As RibbonPage In rbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    currentGroup.Visible = False
                    currentGroup.Enabled = False

                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

                        mCurrentItem = currenLink.Item

                        currenLink.Item.Visibility = BarItemVisibility.Never
                        currenLink.Item.Enabled = False

                        If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarSubItem" Then

                            mBarSubItem = mCurrentItem

                            For Each mSubLink In mBarSubItem.ItemLinks

                                mSubLink.Item.Visibility = BarItemVisibility.Never
                                mSubLink.Item.Enabled = False

                                Debug.Print(String.Format("{0} - {1} - {2} - {3} - {4}", currentPage.Text, currentGroup.Text, mSubLink.Item.Caption, mSubLink.Item.Name, mSubLink.Item.GetType.FullName))

                            Next

                        End If

                    Next currenLink

                Next currentGroup

            Next currentPage

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Shared Sub HabilitarMenuRol(ByVal pIdRol As Integer, ByVal prbMain As RibbonControl)

        Dim mCurrentItem As BarItem
        Dim mBarSubItem As BarSubItem
        Dim mSubLink As BarItemLink

        ' Este procedimiento verifica que cada item del menu 
        ' esté disponible para el usuario que se ha logeado        
        Dim lMenuRol As New List(Of clsBeMenu_sistema)

        Try

            prbMain.Enabled = True

            lMenuRol = clsLnMenu_sistema.Get_All_By_IdRol(pIdRol)

            Dim BeMenuSistema As New clsBeMenu_sistema

            For Each currentPage As RibbonPage In prbMain.Pages

                BeMenuSistema = Nothing

                BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = currentPage.Text OrElse x.Nombre_lgco = currentPage.Name) AndAlso x.Nivel = 1)

                If Not BeMenuSistema Is Nothing Then
                    currentPage.Visible = BeMenuSistema.Visible
                Else
                    currentPage.Visible = False
                    '#EJC20180419:No deshabilitar el ribon principal.
                    'currentPage.Ribbon.Enabled = False
                End If

                Debug.Print(String.Format("Habilita Page: {0} - {1}", currentPage.Text, currentPage.Name))

            Next

            For Each currentPage As RibbonPage In prbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = currentGroup.Text OrElse x.Nombre_lgco = currentGroup.Name) AndAlso x.Nivel = 2)

                    BeMenuSistema = lMenuRol.Find(Function(x) (x.Nombre_lgco = currentGroup.Name))

                    If Not BeMenuSistema Is Nothing Then
                        currentGroup.Visible = BeMenuSistema.Visible
                        currentGroup.Enabled = BeMenuSistema.Visible
                    Else
                        currentGroup.Visible = False
                        currentGroup.Enabled = False
                    End If

                    If currentPage.Name = "rpReportes" Then
                        Debug.Print(String.Format("Habilita Group: {0}/{1} - {2}/{3} ", currentPage.Text, currentPage.Name, currentGroup.Text, currentGroup.Name))
                    End If

                    Debug.Print(String.Format("Habilita Group: {0}/{1} - {2}/{3} ", currentPage.Text, currentPage.Name, currentGroup.Text, currentGroup.Name))


                Next currentGroup

            Next currentPage

            For Each currentPage As RibbonPage In prbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

                        BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = currenLink.Item.Caption OrElse x.Nombre_lgco = currenLink.Item.Name) AndAlso x.Nivel = 3)

                        If Not BeMenuSistema Is Nothing Then
                            currenLink.KeyTip = BeMenuSistema.IdMenu

                            currenLink.Visible = BeMenuSistema.Visible
                            currenLink.Item.Visibility = BeMenuSistema.Visible
                            currenLink.Item.Enabled = BeMenuSistema.Visible
                        Else
                            currenLink.Visible = False
                            currenLink.Item.Visibility = False
                            currenLink.Item.Enabled = False
                        End If

                        Debug.Print(String.Format("Habilita Link: {0} - {1} - {2}", currentPage.Text, currentGroup.Text, currenLink.Item.Caption))

                    Next

                Next

            Next

            For Each currentPage As RibbonPage In prbMain.Pages

                For Each currentGroup As RibbonPageGroup In currentPage.Groups

                    For Each currenLink As BarItemLink In currentGroup.ItemLinks

                        mCurrentItem = currenLink.Item

                        If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarSubItem" Then

                            mBarSubItem = mCurrentItem

                            Debug.Print("Ha: " & mBarSubItem.Caption)

                            For Each mSubLink In mBarSubItem.ItemLinks

                                If mSubLink.Item.Name = "mnuTareasPreIngreso" OrElse mSubLink.Item.Caption = "mnuTareasPreIngreso" Then
                                    Debug.Print("Espera")
                                Else
                                    Debug.Print(String.Format("Habilita: {0} - {1} - {2} - {3} - {4}", currentPage.Text, currentGroup.Text, mSubLink.Item.Caption, mSubLink.Item.Name, mSubLink.Item.GetType.FullName))
                                End If

                                BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = mSubLink.Item.Name OrElse x.Nombre_lgco = mSubLink.Item.Name) AndAlso x.Nivel = 4)

                                If Not BeMenuSistema Is Nothing Then
                                    mSubLink.KeyTip = BeMenuSistema.IdMenu
                                    mSubLink.Visible = BeMenuSistema.Visible
                                    mSubLink.Item.Visibility = BeMenuSistema.Visible
                                    mSubLink.Item.Enabled = BeMenuSistema.Visible
                                Else
                                    mSubLink.Visible = False
                                    mSubLink.Item.Visibility = False
                                    mSubLink.Item.Enabled = False
                                End If

                            Next

                        Else

                            If mCurrentItem.GetType.FullName = "DevExpress.XtraBars.BarButtonItem" Then

                                BeMenuSistema = lMenuRol.Find(Function(x) (x.Titulo = mCurrentItem.Name OrElse x.Nombre_lgco = mCurrentItem.Name))

                                If Not BeMenuSistema Is Nothing Then
                                    Debug.Print("Opción de menú está configurada en nivel: " & BeMenuSistema.Nivel &
                                                " Se encontró en iteración de nivel 4 - Opción: " & mCurrentItem.Name)
                                Else
                                    Debug.Print("Ha_Na: " & mCurrentItem.GetType.FullName & " " & mCurrentItem.Name)
                                End If

                            Else
                                Debug.Print("Ha_Na: " & mCurrentItem.GetType.FullName & " " & mCurrentItem.Name)
                            End If

                        End If

                    Next

                Next

            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub mnuEnvioPedidosCompra_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEnvioPedidosCompra.ItemClick
        Enviar_Pedidos_Compra(True)
    End Sub

    Private Sub mnuReporteComparativoWMSvrsERP_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuReporteComparativoWMSvrsERP.ItemClick

        Try

            Dim frmStockPorLote As New frmStockPorLoteArea
            frmStockPorLote.Modo = frmStockPorLoteArea.pModo.Lista
            frmStockPorLote.ShowDialog()
            frmStockPorLote.Dispose()

        Catch ex As Exception

        End Try

    End Sub

    Private Function Cerrar_Solicitud_Traslado(ByVal pNoDocEntrySAP As Integer) As Boolean

        Cerrar_Solicitud_Traslado = False

        Try

            Dim BePedidoCliente As New clsBeTrans_pe_enc

            BePedidoCliente = clsLnTrans_pe_enc.Get_Single_By_Referencia_Documento(pNoDocEntrySAP)

            If Not BePedidoCliente Is Nothing Then

                If Not clsLnStock_res.Tiene_StockRes_By_IdPedidoEnc(BePedidoCliente.IdPedidoEnc, BePedidoCliente.IdBodega) Then

                    Dim SapTrasladoSync As New clsSyncSAPSPedidoCliente

                    If SapTrasladoSync.Cerrar_Lineas_Documento_Salida(pNoDocEntrySAP, lblprg) Then
                        Cerrar_Solicitud_Traslado = True
                    End If

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "El traslado tiene stock reservado en WMS, no se puede marcar como despachado.")
                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener el documento de WMS.")
            End If


        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try

    End Function

    Private Function Cerrar_Solicitud_Traslado_DocNum(ByVal pDocNumSAP As Integer) As Boolean

        Cerrar_Solicitud_Traslado_DocNum = False

        Try

            Dim BePedidoCliente As New clsBeTrans_pe_enc

            BePedidoCliente = clsLnTrans_pe_enc.Get_Single_By_No_Documento(pDocNumSAP)

            If Not BePedidoCliente Is Nothing Then

                If Not clsLnStock_res.Tiene_StockRes_By_IdPedidoEnc(BePedidoCliente.IdPedidoEnc, BePedidoCliente.IdBodega) Then

                    Dim SapTrasladoSync As New clsSyncSAPSPedidoCliente

                    If SapTrasladoSync.Cerrar_Lineas_Documento_Salida(pDocNumSAP, lblprg) Then
                        Cerrar_Solicitud_Traslado_DocNum = True
                    End If

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "El traslado tiene stock reservado en WMS, no se puede marcar como despachado.")
                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener el documento de WMS.")
            End If


        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try

    End Function

    Private Sub mnuCerrarDocumentoSAP_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCerrarDocumentoSAP.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pTraslado As String = ""

        Try

            args.Caption = "Ingrese número de pedido (DocNum)"
            args.Prompt = "Pedido No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then
                pTraslado = tmpResult.ToString
                mnuCerrarDocumentoSAP.Enabled = False
                Cerrar_Solicitud_Traslado_DocNum(pTraslado)
                mnuCerrarDocumentoSAP.Enabled = True
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

    Private Sub FrmEjecucion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If procesoEnEjecucion Then
            MessageBox.Show("El proceso aún se está ejecutando. Por favor, espera a que termine.", "Proceso en ejecución", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True ' Previene que el formulario se cierre
        End If
    End Sub

End Class