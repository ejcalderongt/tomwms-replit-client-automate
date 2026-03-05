Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports TOMWMS.clsDataContractDI

Public Class frmEjecucion

    Public Property Interface_A_Ejecutar As frmMenu.pInterfaceAEjecutar = -1
    Public Property pWSActivo As WsActivo
    Private procesoEnEjecucion As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum WsActivo
        Bodega = 0
        Proveedor = 1
        PedidoTransferencia = 2
        PedidoCompra = 3
        Productos = 4
    End Enum

    Public Sub Ejecuta_interface_Transferencia_Stock(Optional ByVal Preguntar As Boolean = True, Optional ByVal pNoDocumentoCompraSAP As String = "")

        Dim MostrarMensaje As Boolean = False
        procesoEnEjecucion = True

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar transferencia de stock?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPT As New clsSyncSAPTrasladoStock()
                    sPT.Ejecutar_Interfaz("Transferencia_Stock")
                    sPT.Importar_Solicitudes_Traslados_SAP(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Public Sub Ejecuta_interface_Solicitud_Transferencia(Optional ByVal Preguntar As Boolean = True, Optional ByVal pNoDocumentoCompraSAP As String = "")

        Dim MostrarMensaje As Boolean = False
        procesoEnEjecucion = True

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar Solicitudes de Transferencia?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPC As New clsSyncSAPTrasladoStock()
                    sPC.Ejecutar_Interfaz("Traslados")
                    sPC.Importar_Solicitudes_Traslados_SAP(lblprg, prg, True, Preguntar, pNoDocumentoCompraSAP)
                End Using
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Public Sub Ejecuta_Interface_Bodegas(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False
            procesoEnEjecucion = True

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
            procesoEnEjecucion = False
        End Try

    End Sub

    Public Sub Ejecuta_Interface_Proveedores(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False
            procesoEnEjecucion = True

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            lblprg.Text = ""

            If Ejecutar Then
                clsSyncSAPProveedor.Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

            prg.Visible = False
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuActualizarProveedores_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarProveedores.ItemClick
        Ejecuta_Interface_Proveedores(True)
    End Sub

    Private Sub Ejecuta_Interface_Pedidos_Compra(Optional ByVal Preguntar As Boolean = True,
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

            lblprg.Text = ""

            If Ejecutar Then
                clsSyncSAPPedidoCompra.Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar, pNoDocumentoCompraSAP)
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

            lblprg.Text = ""

            If Ejecutar Then
                clsSyncSAPProducto.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
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
            clsSyncSAPProducto.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg)
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
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
        Ejecuta_Interface_Productos(True)
    End Sub

    Private Sub Enviar_Pedidos_Compra(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensajeError As Boolean = False
        procesoEnEjecucion = True

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
                clsSyncSAPPedidoCompra.Enviar_Transacciones_De_Ingreso_SAP(lblprg, prg)
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
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuEnviarPedidosCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarPedidosCompra.ItemClick
        Enviar_Pedidos_Compra(True)
    End Sub

    Private Sub cmdEntidad_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)

        Try

            With frmNavEnt_List
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmEjecucion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            CheckForIllegalCrossThreadCalls = False

            Me.Text = "MI3_SYNC ->TOM, WMS (" & gVersionApp & " " & FormatoFechas.tFecha(gFechaVersion) & ")"

            If Interface_A_Ejecutar <> -1 Then


                mnuActualizarProveedores.Visibility = BarItemVisibility.Never
                mnuProductosI.Visibility = BarItemVisibility.Never
                mnuImprimir.Visibility = BarItemVisibility.Never
                mnuReporteEjecuciones.Visibility = BarItemVisibility.Never
                'mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            End If

            '#EJC20240709: Permisos por rol en interface parte 1.
            Try

                '#CKFK20241009 Agregar estas opciones
                'DesactivarMenu()

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

                Dim BeUsuarioBodega As New clsBeUsuario_bodega
                If IdUsuario = -1 Then

                    If BeConfigEnc IsNot Nothing Then
                        IdUsuario = BeConfigEnc.IdUsuario
                    End If

                End If

                BeUsuarioBodega = clsLnUsuario_bodega.Get_Single_By_IdUsuario_And_IdBodega(IdUsuario, BeConfigEnc.Idbodega)

                '#CKFK20241009 Agregar estas opciones
                'If Not BeUsuarioBodega Is Nothing Then
                '    HabilitarMenuRol(BeUsuarioBodega.IdRol, rbMain)
                'End If

            Catch ex As Exception
                Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
                clsPublic.Actualizar_Progreso(lblprg, vMensaje)
                prg.Visible = False
            End Try

            Select Case Interface_A_Ejecutar

                Case pInterfaceAEjecutar.Importar_Productos
                    mnuProductosI.Visibility = BarItemVisibility.Always
                    Ejecuta_Interface_Productos(False)
                Case pInterfaceAEjecutar.Enviar_Pedidos_Compra
                    mnuEnviarPedidosCompra.Visibility = BarItemVisibility.Always
                    Enviar_Pedidos_Compra(False)
                Case pInterfaceAEjecutar.Enviar_Pedidos_Transferencia
                    mnuEnviarPedidosTransferencia.Visibility = BarItemVisibility.Always
                    Enviar_Traslado_Stock(False)
                Case pInterfaceAEjecutar.Enviar_Traslados_SAP
                    mnuEnviarTrasladosProrrateo.Visibility = BarItemVisibility.Always
                Case pInterfaceAEjecutar.Enviar_Devolucion_Proveedor_SAP
                Case pInterfaceAEjecutar.Actualizar_Traslado_No_Enviado
                Case pInterfaceAEjecutar.Actualizar_Pedido_Cliente_No_Enviado
                Case pInterfaceAEjecutar.Enviar_Ajustes_Inventario
                    Enviar_Ajustes(True)
                Case pInterfaceAEjecutar.Cerrar_Documento_Salida_SAP
                Case Else
                    Exit Select

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Enviar_Ajustes(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False
        procesoEnEjecucion = True

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Desea enviar los ajustes?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
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
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub
    Private Sub Ejecuta_Interface_Envio_Ajustes(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False
            procesoEnEjecucion = True

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar ajustes?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPcRec As New clsSyncAjusteInventario()
                    sPcRec.Ejecutar_Interfaz("Envío ajustes")
                    sPcRec.Sync_Ajustes(lblprg, prg)
                End Using
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuEnviarAjustes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)

        Try

            Ejecuta_Interface_Envio_Ajustes(True)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Lista "

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

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
        procesoEnEjecucion = True

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

        Finally

            procesoEnEjecucion = False

        End Try

    End Sub


    Public Sub Ejecuta_interface_Devolucion_Mercancia_Cliente(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False
        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCliente As String = ""
        procesoEnEjecucion = True

        Try

            args.Caption = "Ingrese Solicitud de devolución de cliente"
            args.Prompt = "Solicitud No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                pPedidoCliente = tmpResult.ToString

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
                    clsSyncSAPDevolucionMercancia.Insertar_Solicitud_Devol_Cli_A_TOMWMS(lblprg, prg, True, Preguntar, pPedidoCliente)
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuPedidosCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPedidosCompra.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCompra As String = ""
        procesoEnEjecucion = True

        Try

            args.Caption = "Ingrese pedido de compra"
            args.Prompt = "Pedido No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                pPedidoCompra = tmpResult.ToString

                Ejecuta_Interface_Pedidos_Compra(True, pPedidoCompra)

            End If

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            clsPublic.Actualizar_Progreso(lblprg, vMsgError)

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuBodegas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBodegas.ItemClick
        Ejecuta_Interface_Bodegas(True)
    End Sub

    Private Sub mnuPedidosTransferencia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSolicitudTraslado.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pSolicitudTransferNoDocumento As String = ""
        procesoEnEjecucion = True

        Try

            args.Caption = "Ingrese No. de Solicitud de Transferencia"
            args.Prompt = "Solicitud No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                pSolicitudTransferNoDocumento = tmpResult.ToString

                lblprg.Text = ""

                Ejecuta_interface_Solicitud_Transferencia(True, pSolicitudTransferNoDocumento)

            End If

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            clsPublic.Actualizar_Progreso(lblprg, vMsgError)

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub BarButtonItem4_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTransferenciaStock.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pSolicitudTransferNoDocumento As String = ""
        procesoEnEjecucion = True

        Try

            args.Caption = "Ingrese No. de Solicitud de Transferencia"
            args.Prompt = "Solicitud No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                pSolicitudTransferNoDocumento = tmpResult.ToString

                lblprg.Text = ""

                Ejecuta_interface_Transferencia_Stock(True, pSolicitudTransferNoDocumento)

            End If

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            clsPublic.Actualizar_Progreso(lblprg, vMsgError)

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuDevolucionMercancia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDevolucionMercancia.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim pPedidoCliente As String = ""
        procesoEnEjecucion = True

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

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            clsPublic.Actualizar_Progreso(lblprg, vMsgError)

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Public Sub Ejecuta_interface_Devolucion(Optional ByVal Preguntar As Boolean = True,
                                            Optional ByVal pPedidoCliente As String = "")

        Dim MostrarMensaje As Boolean = False
        procesoEnEjecucion = True

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
                clsSyncSapDevolucionProveedor.Procesar_Devolucion_Mercancia_SAP(lblprg, prg, pPedidoCliente)
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
            End If
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuEnvioPedidosCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnvioPedidosCompra.ItemClick
        Enviar_Pedidos_Compra(True)
    End Sub

    Private Sub mnuEnviarAjustes_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarAjustes.ItemClick
        Enviar_Ajustes(True)
    End Sub

    Private Sub mnuEnviaPedidosTransferencia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviaPedidosTransferencia.ItemClick
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
        procesoEnEjecucion = True

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
                clsSyncSAPSPedidoCliente.Importar_Pedido_Cliente_SAP(lblprg, prg, True, Preguntar, pPedidoCliente)
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub Enviar_Documentos_Salida(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False
        procesoEnEjecucion = True

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
                clsSyncSAPSPedidoCliente.Enviar_Transacciones_De_Salida(lblprg, prg)
            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuProductoPresentacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuProductoPresentacion.ItemClick
        Try


            Dim SAPPresentaciones As New clsSyncSAPPresentaciones
            SAPPresentaciones.Importar_Presentaciones_Productos(lblprg, prg)

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try
    End Sub

    Private Sub mnuDevolucionCliente_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDevolucionCliente.ItemClick
        Ejecuta_interface_Devolucion_Mercancia_Cliente(True)
    End Sub

    Private Sub mnuEnviarTraslados_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarTrasladosProrrateo.ItemClick
        Enviar_Traslado_Stock(True)
    End Sub

    Private Sub Enviar_Traslado_Stock(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensaje As Boolean = False
        procesoEnEjecucion = True

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Generar traslados de stock desde bodegas de prorrateo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                lblprg.Text = ""
                clsSyncSAPSSolicitudTraslado.Enviar_Transacciones_De_Salida(lblprg,
                                                                            prg,
                                                                            tTipoDocumentoSalida.Transferencia_Interna_WMS)
            End If

        Catch ex As Exception

            clsPublic.Actualizar_Progreso(lblprg, ex.Message)

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuTransferenciaIngreso_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuTransferenciaIngreso.ItemClick

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

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            clsPublic.Actualizar_Progreso(lblprg, ex.Message)

        End Try

    End Sub

    Public Sub Ejecuta_interface_Traslados_Entrada_SAP(Optional ByVal Preguntar As Boolean = True,
                                                       Optional ByVal pTraslado As String = "")

        Dim MostrarMensaje As Boolean = False
        procesoEnEjecucion = True

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
                clsSyncSAPSSolicitudTraslado.Importar_Solicitud_Traslado_Entrada_SAP(lblprg, prg, True, Preguntar, pTraslado)
            End If

        Catch ex As Exception
            If MostrarMensaje Then
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuEnviarDevolProveedor_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEnviarDevolProveedor.ItemClick

        Try

            Enviar_Solicitudes_Devol_Prov(True)

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        End Try

    End Sub

    Private Sub Enviar_Solicitudes_Devol_Prov(Optional ByVal Preguntar As Boolean = True)

        Dim MostrarMensajeError As Boolean = False
        procesoEnEjecucion = True

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar Solicitudes de devolución de proveedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                clsSyncSapDevolucionProveedor.Enviar_Transacciones_De_Salida(lblprg, prg, tTipoDocumentoSalida.Devolucion_Proveedor)
            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub FrmEjecucion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If procesoEnEjecucion Then
            MessageBox.Show("El proceso aún se está ejecutando. Por favor, espera a que termine.", "Proceso en ejecución", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True ' Previene que el formulario se cierre
        End If
    End Sub
    Private Sub mnuEnviarTrasladosDesdeSol_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEnviarTrasladosDesdeSol.ItemClick
        mnuEnviarTrasladosDesdeSol.Visibility = BarItemVisibility.Never
        Enviar_Traslado_Desde_Solicitud(True)
        mnuEnviarTrasladosDesdeSol.Visibility = BarItemVisibility.Always
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
                clsSyncSAPSSolicitudTraslado.Enviar_Traslados_Desde_Solicitud(lblprg,
                                                                              prg,
                                                                              tTipoDocumentoSalida.Transferencia_Interna_WMS)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub mnuActualizarCodigosBarra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarCodigosBarra.ItemClick

        Try

            Dim SapProductoCodigosBarra As New clsSyncSapCodigosBarra
            SapProductoCodigosBarra.Importar_Codigos_Barra_Productos(lblprg, prg)

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, "Error: " & ex.Message)
        End Try


    End Sub

    Public Sub Ejecuta_Interface_Clientes(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False
            procesoEnEjecucion = True

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            lblprg.Text = ""

            If Ejecutar Then
                clsSyncSAPClientes.Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
            End If

        Catch ex As Exception

            Dim vMensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

            prg.Visible = False
        Finally
            procesoEnEjecucion = False
        End Try

    End Sub

    Private Sub cmdSyncClientes_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSyncClientes.ItemClick
        Ejecuta_Interface_Clientes(True)
    End Sub

End Class