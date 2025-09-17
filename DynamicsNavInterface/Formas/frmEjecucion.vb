Imports System.ComponentModel
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.clsInterfaceBase
Imports TOMWMS.WSFichaBodegas

Public Class frmEjecucion

    Public Property Interface_A_Ejecutar As frmMenu.pInterfaceAEjecutar = -1
    Public Property pWSActivo As WsActivo
    Public Property pBodega As String
    Public Property pIdUsuario As Integer = 0

    'Private mgr As New SplashScreenManager(Me, GetType(WaitForm), True, True, False)
    Private WithEvents bgWorker As BackgroundWorker

    Public Sub New()

        InitializeComponent()

        Try

            pBodega = clsLnBodega.Get_Codigo_By_IdBodega(BeConfigEnc.Idbodega)

            ' Crear e inicializar el BackgroundWorker
            bgWorker = New System.ComponentModel.BackgroundWorker()
            bgWorker.WorkerReportsProgress = True
            bgWorker.WorkerSupportsCancellation = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Enum WsActivo
        Bodega = 0
        Proveedor = 1
        PedidoTransferencia = 2
        PedidoCompra = 3
        Productos = 4
    End Enum

    Public Sub Ejecuta_interface_Pedidos_Transferencia(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Importar pedidos de transferencia (Salidas) de la Bodega  " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPT As New clsSyncNavPedidoTraslado()
                    sPT.Ejecutar_Interfaz("Pedido_transferencia_envío")
                    sPT.pBodega = pBodega
                    sPT.Importar_Pedidos_Transferencia_Envio_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuPedidosTransferencia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPedidosTransferencia.ItemClick

        Try

            Ejecuta_interface_Pedidos_Transferencia(True)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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
                Using sBod As New clsSyncNavBodega()
                    sBod.Ejecutar_Interfaz("Bodega")
                    sBod.Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            prg.Visible = False
        End Try

    End Sub

    Private Sub mnuBodegas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBodegas.ItemClick
        Ejecuta_Interface_Bodegas(True)
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
                Using sProv As New clsSyncNavProveedor()
                    sProv.Ejecutar_Interfaz("Proveedor")
                    sProv.Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            prg.Visible = False
        End Try

    End Sub

    Private Sub mnuActualizarProveedores_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarProveedores.ItemClick
        Ejecuta_Interface_Proveedores(True)
    End Sub

    'Public threadListar_Stock As New Thread(AddressOf Listar_Stock_With_Obj) With {.IsBackground = True}

    Private Sub Ejecuta_Interface_Pedidos_Compra(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar pedidos de compra para la Bodega  " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPc As New clsSyncNavPedidoCompra()
                    sPc.Ejecutar_Interfaz("Pedido compra")
                    sPc.pBodega = pBodega
                    sPc.Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Ejecuta_Interface_Devolucion_Venta(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar devoluciones de venta de la Bodega " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPc As New clsSyncNavDevolucionVenta()
                    sPc.Ejecutar_Interfaz("Devolucion Venta")
                    sPc.pBodega = pBodega
                    sPc.Insertar_Devolucion_Venta_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuPedidoCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPedidoCompra.ItemClick

        Try

            Ejecuta_Interface_Pedidos_Compra()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

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
                Using sProd As New clsSyncNavProducto()
                    sProd.Ejecutar_Interfaz("Producto")
                    sProd.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Using sProd As New clsSyncNavProducto()
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

    Private Sub mnuConversiones_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuConversiones.ItemClick

        Try

            prg.Visible = True

            If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Using sConv As New clsSyncNavTablaConversion()
                    sConv.Importar_Conversiones_DesdeWSNav_A_TOMIMS(lblprg, prg)
                End Using


            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finalize()
            prg.Visible = False
        End Try

    End Sub

    Private Sub mnuProductosI_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuProductosI.ItemClick
        Ejecuta_Interface_Productos(True)
    End Sub

    Private Sub Enviar_Pedidos_Compra(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar registros de ingreso de la bodega " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPc As New clsSyncNavPedidoCompra()
                    sPc.Ejecutar_Interfaz("Envio ingresos")
                    sPc.pBodega = pBodega
                    sPc.Enviar_Transacciones_De_Ingreso(lblprg, prg, BeConfigEnc.Idbodega)
                End Using
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

    Private Sub mnuEnviarPedidosCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarPedidosCompra.ItemClick
        Enviar_Pedidos_Compra(True)
    End Sub

    Private Sub cmdEntidad_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEntidad.ItemClick

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

    Private Sub Activa_Grupos(ByVal Activar As Boolean)

        rpg1.Enabled = Activar
        rpg2.Enabled = Activar
        rpg3.Enabled = Activar
        rpg4.Enabled = Activar
        rpg5.Enabled = Activar

    End Sub
    Private Sub frmEjecucion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Activa_Grupos(False)

            mnuPedidoCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuEnvios.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuDevolucionVenta.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuOrdenesProduccion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuPedidosTransferencia.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuRecibirPedidosTransfINgreso.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuPedidosDeVenta.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuDatosPendPush.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuEnviarPedidosVenta.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuEnviarDatosPendientesPush.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            If Interface_A_Ejecutar <> -1 Then

                mnuBodegas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuActualizarProveedores.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuProductosI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuCentrosCosto.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuPedidoCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuPedidosTransferencia.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuConversiones.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuReporteEjecuciones.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                cmdEntidad.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuRecibirPedidosTransfINgreso.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuSyncLotes.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            End If

            Select Case Interface_A_Ejecutar

                Case pInterfaceAEjecutar.Importar_Bodegas
                    mnuBodegas.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Ejecuta_Interface_Bodegas(False)
                Case pInterfaceAEjecutar.Importar_Pedidos_De_Compra
                    mnuPedidoCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Ejecuta_Interface_Pedidos_Compra(False)
                Case pInterfaceAEjecutar.Importar_Pedidos_De_Transferencia
                    mnuPedidosTransferencia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Ejecuta_interface_Pedidos_Transferencia(False)
                Case pInterfaceAEjecutar.Importar_Productos
                    mnuProductosI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Ejecuta_Interface_Productos(False)
                Case pInterfaceAEjecutar.Enviar_Pedidos_Compra
                    mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    mnuEnviarPedidosCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Enviar_Pedidos_Compra(False)
                Case pInterfaceAEjecutar.Enviar_Pedidos_Transferencia
                    mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    mnuEnviarPedidosTransferencia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Enviar_Pedidos_Transferencia(False)
                Case pInterfaceAEjecutar.Interface_ROAD
                    mnuPedidoCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Ejecuta_Interface_Pedidos_Compra(True)
                Case Else
                    Exit Select

            End Select

            bgWorker.RunWorkerAsync()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try
    End Sub

    Private Sub Enviar_Pedidos_Transferencia(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar registros de transferencia de salida de la bodega " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPT As New clsSyncNavPedidoTraslado()
                    sPT.Ejecutar_Interfaz("Pedido_transferencia_envío")
                    sPT.pBodega = pBodega
                    sPT.Enviar_Transacciones_De_Salida(lblprg, prg, BeConfigEnc.Idbodega)
                End Using
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

    Private Sub mnuEnviarPedidosTransferencia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarPedidosTransferencia.ItemClick
        Enviar_Pedidos_Transferencia(True)
    End Sub

    Private Sub mnuSyncLotes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSyncLotes.ItemClick

        Try

            clsSyncLotes.Get_Lista_Lotes(Nothing, Nothing, Nothing)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Ejecuta_Interface_Pedidos_Transferencia_Recepcion(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar pedidos de transferencia (Recepción) de la Bodega " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPcRec As New clsSyncNavPedidoTraslado()
                    sPcRec.Ejecutar_Interfaz("Pedidos transferencia ingreso")
                    sPcRec.pBodega = pBodega
                    sPcRec.Importar_Pedidos_Transferencia_Recepcion_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, True)
                End Using
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

    Private Sub Ejecuta_Interface_Envio_Ajustes(Optional ByVal Preguntar As Boolean = True)

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
                Using sPcRec As New clsSyncAjusteInventario()
                    sPcRec.Ejecutar_Interfaz("Envío ajustes")
                    sPcRec.pBodega = pBodega
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

        End Try

    End Sub

    Private Sub mnuRecibirPedidosTransfINgreso_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuRecibirPedidosTransfINgreso.ItemClick

        Try

            Ejecuta_Interface_Pedidos_Transferencia_Recepcion(True)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuEnviarAjustes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarAjustes.ItemClick

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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Lista "

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

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
                Using sCli As New clsSyncNavCliente()
                    sCli.Ejecutar_Interfaz("Clientes")
                    sCli.Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            prg.Visible = False
        End Try

    End Sub

    Private Sub mnuPedidosDeVenta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPedidosDeVenta.ItemClick
        Ejecuta_Interface_Pedidos_De_Venta(True)
    End Sub

    Private Sub mnuOrdenesProduccion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuOrdenesProduccion.ItemClick

        Try

            ' If Es_Bodega_Avanzada(pBodega) Then
            Ejecuta_Interface_Ordenes_Produccion(True)
            'End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Public Sub Ejecuta_Interface_Ordenes_Produccion(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar órdenes de compra para la Bodega " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sOrdenesProd As New clsSyncNavOrdenProduccion
                    sOrdenesProd.Ejecutar_Interfaz("Clientes")
                    sOrdenesProd.pBodega = pBodega
                    sOrdenesProd.Insertar_Ordenes_Produccion_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            prg.Visible = False
        End Try

    End Sub

    Public Sub Ejecuta_Interface_Pedidos_De_Venta(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar los pedidos de venta de la Bodega " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPedidosVenta As New clsSyncNavPedidoVenta
                    sPedidosVenta.Ejecutar_Interfaz("PedidosVenta")
                    sPedidosVenta.pBodega = pBodega
                    sPedidosVenta.Importar_Pedidos_Venta_Desde_Nav_A_WMS(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            prg.Visible = False
        End Try

    End Sub

    Private Sub Args_Showing(ByVal sender As Object, ByVal e As XtraMessageShowingArgs)
        e.Form.Icon = Me.Icon
    End Sub

    Private Sub mnuEnvios_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnvios.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim vEnvio As String = ""

        Try

            args.Caption = "Ingrese Envío de Almacén"
            args.Prompt = "Envío No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                vEnvio = tmpResult.ToString

                If Not vEnvio.StartsWith("EA") Then
                    If vEnvio.Length >= 6 Then
                        vEnvio = "EA-" & vEnvio
                    Else
                        Throw New Exception("La cantidad de dígitos no es válida al parecer para un EA")
                    End If
                End If

                Ejecuta_Interface_Envios_Almacen(True, vEnvio)

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

    Public Sub Ejecuta_Interface_Envios_Almacen(Optional ByVal Preguntar As Boolean = True, Optional pEnvio As String = "")

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar envíos de almacén de la bodega " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sEnviosAlm As New clsSyncNavEnvioAlm
                    sEnviosAlm.Ejecutar_Interfaz("EnviosAlm")
                    sEnviosAlm.pBodega = pBodega
                    sEnviosAlm.pIdUsuario = pIdUsuario
                    sEnviosAlm.Importar_Envio_Almacen_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, Preguntar, pEnvio)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            prg.Visible = False
        End Try

    End Sub

    Private Sub mnuDevolucionVenta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDevolucionVenta.ItemClick

        Try

            Ejecuta_Interface_Devolucion_Venta()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub mnuDatosMI3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDatosMI3.ItemClick

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

    Private Sub mnuDatosPendPush_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDatosPendPush.ItemClick

        Try

            With frmRegistrosPendPush
                .Modo = frmRegistrosInterface.pModo.Lista
                .WindowState = FormWindowState.Maximized
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub mnuEnviarDatosPendientesPush_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarDatosPendientesPush.ItemClick
        Enviar_Pendientes_Push(True)
    End Sub

    Private Sub Enviar_Pendientes_Push(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar registros pendientes de push de la bodega " & pBodega & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then

                Dim vRespuestaPush As String = ""

                Dim BeTransaccionesPushList As New List(Of clsBeI_nav_transacciones_push)
                Dim procesado As Boolean = False
                Dim Ubicar As New WSUbicarAlmacen.Ubicar_Almacen
                Dim entro As Boolean = False
                Dim BeProducto As New clsBeProducto

                BeTransaccionesPushList = clsLnI_nav_transacciones_push.Get_All_No_Enviadas(BeConfigEnc.Idbodega)

                For Each tp In BeTransaccionesPushList

                    vRespuestaPush = ""
                    procesado = False
                    entro = False

                    BeProducto = New clsBeProducto With {.IdProducto = tp.Idproducto}

                    clsLnProducto.GetSingle(BeProducto)

                    Actualizar_Progreso(lblprg, "Se va a ejecutar el " & tp.Tipo_push & " sobre la transaccion " &
                                              tp.IdTransaccionPush & " para el producto " & BeProducto.Codigo)

                    Select Case tp.Tipo_push

                        Case "Push_Recepcion_To_NAV_For_BYB"

                            Ubicar = clsSyncNavPedidoCompra.Push_Recepcion_To_NAV_For_BYB(tp.Location_code,
                                                                                          tp.Zone_code,
                                                                                          tp.Bin_code,
                                                                                          tp.Assigne_user_id,
                                                                                          tp.Item_no,
                                                                                          tp.No_orden_prod,
                                                                                          tp.IdRecepcionEnc,
                                                                                          tp.IdRecepcionDet,
                                                                                          tp.User_agr,
                                                                                          vRespuestaPush)
                            entro = True

                        Case "Push_Recepcion_Produccion_To_NAV_For_BYB"

                            procesado = clsSyncNavOrdenProduccion.Push_Recepcion_Produccion_To_NAV_For_BYB(tp.Documento_ubicacion,
                                                                                                           BeProducto.Codigo,
                                                                                                           tp.Cantidad,
                                                                                                           tp.IdRecepcionEnc,
                                                                                                           tp.IdRecepcionDet,
                                                                                                           tp.User_agr,
                                                                                                           vRespuestaPush)
                            entro = True

                        Case "Push_Recepcion_Pedido_Compra_To_NAV_For_BYB"

                            procesado = clsSyncNavPedidoCompra.Push_Recepcion_Pedido_Compra_To_NAV_For_BYB(tp.Documento_ingreso,
                                                                                                           tp.Documento_recepcion,
                                                                                                           tp.No_linea,
                                                                                                           BeProducto.Codigo,
                                                                                                           tp.Cantidad,
                                                                                                           tp.Lote,
                                                                                                           tp.Fecha_vence,
                                                                                                           tp.Nom_unidad_medida,
                                                                                                           tp.IdRecepcionEnc,
                                                                                                           tp.IdRecepcionDet,
                                                                                                           tp.User_agr,
                                                                                                           vRespuestaPush)
                            entro = True

                        Case "Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB"

                            procesado = clsSyncNavDevolucionVenta.Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB(tp.Documento_ingreso,
                                                                                                                 tp.Documento_recepcion,
                                                                                                                 tp.No_linea,
                                                                                                                 BeProducto.Codigo,
                                                                                                                 tp.Cantidad,
                                                                                                                 tp.Lote,
                                                                                                                 tp.Fecha_vence,
                                                                                                                 tp.Nom_unidad_medida,
                                                                                                                 tp.IdRecepcionEnc,
                                                                                                                 tp.IdRecepcionDet,
                                                                                                                 tp.User_agr,
                                                                                                                 vRespuestaPush)
                            entro = True

                        Case "Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB"

                            procesado = clsSyncNavPedidoCompra.Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB(tp.Documento_ingreso,
                                                                                                                    tp.Documento_recepcion,
                                                                                                                    tp.No_linea,
                                                                                                                    BeProducto.Codigo,
                                                                                                                    tp.Cantidad,
                                                                                                                    tp.Lote,
                                                                                                                    tp.Fecha_vence,
                                                                                                                    tp.Nom_unidad_medida,
                                                                                                                    tp.IdRecepcionEnc,
                                                                                                                    tp.IdRecepcionDet,
                                                                                                                    tp.User_agr,
                                                                                                                    vRespuestaPush)
                            entro = True

                    End Select

                    If entro Then
                        If procesado Then
                            tp.Enviado_A_ERP = True
                            tp.Fec_agr = Now
                            clsLnI_nav_transacciones_push.Actualizar_Bandera_Enviado(tp)
                            Actualizar_Progreso(lblprg, "Registro procesado correctamente para el producto " & BeProducto.Codigo)
                        Else
                            Actualizar_Progreso(lblprg, "No se pudo procesar el registro Push para el producto " & BeProducto.Codigo &
                                              " Error: " & vRespuestaPush)
                        End If
                    End If

                Next

                Actualizar_Progreso(lblprg, "Finalizó el proceso")

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

    Public Sub Ejecuta_Interface_Centros_Costo(Optional ByVal Preguntar As Boolean = True)

        Try

            prg.Visible = True

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar centros de costo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                clsSyncCentrosCosto.Importar_Centros_Costo_DesdeWSNav(lblprg, prg)
                clsSyncCentrosCosto.Importar_Centros_Costo_Mercadeo_DesdeWSNav(lblprg, prg)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            prg.Visible = False
        End Try

    End Sub

    Private Sub mnuCentrosCosto_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCentrosCosto.ItemClick
        Ejecuta_Interface_Centros_Costo(True)
    End Sub

    Private Sub mnuEnviarPedidosVenta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarPedidosVenta.ItemClick
        Enviar_Pedidos_Venta(True)
    End Sub

    Private Sub Enviar_Pedidos_Venta(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Enviar registros de pedidos de venta?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPT As New clsSyncNavPedidoVenta()
                    sPT.Ejecutar_Interfaz("Pedido_transferencia_envío")
                    sPT.Enviar_Transacciones_De_Salida(lblprg, prg)
                End Using
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

    Private Function Es_Bodega_Avanzada(pBodega As String) As Boolean

        Dim BeBodegaNAV As Ficha_Bodegas

        Es_Bodega_Avanzada = False

        Try

            Dim wsBodegaService As New Ficha_Bodegas_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

            wsBodegaService.Url = My.Settings.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service

            'Si es una bodega avanzada 
            BeBodegaNAV = wsBodegaService.Read(pBodega)

            Return BeBodegaNAV.Require_Pick AndAlso BeBodegaNAV.Require_Receive

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String)
        lblPrg.AppendText(mensaje & vbNewLine)
        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
    End Sub

    Private _bgWorkerException As Exception = Nothing
    Private Sub bgWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgWorker.DoWork

        Try

            ActualizarUI("Conectado a NAV, espere por favor...")

            Dim resultado As Boolean = Es_Bodega_Avanzada(pBodega)
            e.Result = resultado

        Catch ex As Exception
            _bgWorkerException = ex
        End Try

    End Sub

    Private Sub bgWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted

        Dim esAvanzada As Boolean = False

        Try

            lblprg.Text = ""

            Actualizar_Progreso_Centrado(lblprg, "Bienvenido a la interface de TOMWMS y NAV.", Color.Blue, New Font("Consolas", 10))

            If _bgWorkerException IsNot Nothing Then
                ActualizarUI("Error: " & _bgWorkerException.Message)
                _bgWorkerException = Nothing ' Restablecer para futuras operaciones                
            Else
                esAvanzada = CType(e.Result, Boolean)
                Actualizar_Progreso(lblprg, "Estado = Conectado a NAV.", Color.DarkGreen)
                Actualizar_Progreso(lblprg, "Bodega_avanzada = " & IIf(esAvanzada, "Si", "No"), Color.Blue)
                Actualizar_Progreso(lblprg, "Inicio = " & Now)

            End If

            If esAvanzada Then
                mnuPedidoCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuEnvios.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuDevolucionVenta.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuOrdenesProduccion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuPedidosTransferencia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuRecibirPedidosTransfINgreso.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuPedidosDeVenta.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuEnviarPedidosCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuDatosPendPush.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuEnviarPedidosVenta.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuEnviarDatosPendientesPush.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuPedidoCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuEnvios.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuDevolucionVenta.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuOrdenesProduccion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuPedidosTransferencia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuRecibirPedidosTransfINgreso.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuDatosPendPush.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuEnviarPedidosVenta.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuPedidosDeVenta.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuEnviarDatosPendientesPush.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception
            ActualizarUI("Error_202312112149: " & ex.Message)
        Finally
            Activa_Grupos(True)
        End Try

    End Sub

    Private Sub ActualizarUI(mensaje As String)
        If lblprg.InvokeRequired Then
            lblprg.Invoke(New Action(Sub() Actualizar_Progreso(lblprg, mensaje)))
        Else
            Actualizar_Progreso(lblprg, mensaje)
        End If
    End Sub

    Private Sub bgWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgWorker.ProgressChanged
        ActualizarUI(e.UserState.ToString())
    End Sub


    Private Sub mnuEliminarEnvio_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarEnvio.ItemClick

        Dim args As New XtraInputBoxArgs()
        Dim tmpResult As Object
        Dim vEnvio As String = ""

        Try

            args.Caption = "Ingrese Envío de Almacén"
            args.Prompt = "Envío No."

            Dim editor As New TextEdit
            args.Editor = editor

            args.DefaultButtonIndex = 0
            args.DefaultResponse = ""
            AddHandler args.Showing, AddressOf Args_Showing

            tmpResult = XtraInputBox.Show(args)

            If Not tmpResult Is Nothing Then

                Dim pBePedidoEnc As New clsBeTrans_pe_enc
                pBePedidoEnc = clsLnTrans_pe_enc.Get_Single_By_Referencia(tmpResult)
                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

                If Not pBePedidoEnc Is Nothing AndAlso Not BeConfigEnc Is Nothing Then

                    If XtraMessageBox.Show("¿Eliminar documento de salida:" & pBePedidoEnc.Referencia & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        If pBePedidoEnc.Sync_MI3 Then

                            Actualizar_Progreso(lblprg, "El pedido es válido y la configuración de interface.")

                            If XtraMessageBox.Show("¿Eliminar documento de ERP?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then


                                '#EJC202211221049: Validar que la instancia no sea nothing para eliminar desde WS
                                If Not wsTOMHHInstance Is Nothing Then

                                    Dim ArchHeader As New WebReference.clsArchHeader()
                                    ArchHeader.Tipo = "WM"

                                    'Si no existe picking no debo borrar
                                    Dim vResultBorraPicking As Boolean = wsTOMHHInstance.Borrar_Picking(pBePedidoEnc.Referencia)

                                    If Not vResultBorraPicking Then
                                        Actualizar_Progreso(lblprg, "No se pudo eliminar el envío de almacén, ni el pedido.")
                                    Else
                                        If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                           True,
                                                                                                           BeConfigEnc.IdUsuario) Then

                                            Actualizar_Progreso(lblprg, "Se ha eliminado el pedido, el envío de almacén y se ha liberado el stock reservado")

                                        Else
                                            Actualizar_Progreso(lblprg, "No se pudo eliminar el pedido.")
                                        End If

                                    End If

                                Else

                                    If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                       True,
                                                                                                       BeConfigEnc.IdUsuario) Then

                                        Actualizar_Progreso(lblprg, "Se ha eliminado el pedido, el envío de almacén y se ha liberado el stock reservado")
                                    Else
                                        Actualizar_Progreso(lblprg, "No se pudo eliminar el pedido.")
                                    End If

                                End If

                            Else

                                If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                   True,
                                                                                                   BeConfigEnc.IdUsuario) Then
                                    Actualizar_Progreso(lblprg, "Se ha eliminado el pedido y se ha liberado el stock reservado")
                                Else
                                    Actualizar_Progreso(lblprg, "No se pudo eliminar el pedido.")
                                End If

                            End If

                        Else

                            If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                               True,
                                                                                               BeConfigEnc.IdUsuario) Then
                                Actualizar_Progreso(lblprg, "Se ha eliminado el pedido y se ha liberado el stock reservado")
                            Else
                                Actualizar_Progreso(lblprg, "No se pudo eliminar el pedido.")
                            End If

                        End If

                    End If

                Else
                    If pBePedidoEnc Is Nothing Then
                        Actualizar_Progreso(lblprg, "MSG_202312181936: Número de envío no válido para eliminar.")
                    End If
                    If BeConfigEnc Is Nothing Then
                        Actualizar_Progreso(lblprg, "MSG_202312181937: No se encontró la configuración de bodega.")
                    End If
                End If

            Else
                Actualizar_Progreso(lblprg, "Número de envío no válido para eliminar.")
            End If

        Catch ex As Exception

            Actualizar_Progreso(lblprg, String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            Actualizar_Progreso(lblprg, "Fin de proceso.")
        End Try

    End Sub

    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String, colorTexto As Color)

        Dim inicioTexto As Integer = lblPrg.TextLength
        lblPrg.AppendText(mensaje & vbNewLine)
        Dim finTexto As Integer = lblPrg.TextLength

        ' Aplica el color al texto después del signo igual
        AplicarColorTexto(lblPrg, inicioTexto, finTexto, mensaje, colorTexto)

        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()

    End Sub

    Private Shared Sub AplicarColorTexto(ByRef lblPrg As RichTextBox, inicioTexto As Integer, finTexto As Integer, mensaje As String, colorTexto As Color)
        Dim indiceIgual As Integer = mensaje.IndexOf("=")
        If indiceIgual >= 0 AndAlso indiceIgual < mensaje.Length - 1 Then
            Dim inicioValor As Integer = inicioTexto + indiceIgual + 1
            Dim longitudValor As Integer = finTexto - inicioValor

            ' Aplica el color
            lblPrg.Select(inicioValor, longitudValor)
            lblPrg.SelectionColor = colorTexto
        End If

        ' Restablece la selección
        lblPrg.Select(finTexto, 0)
    End Sub

    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String, colorTexto As Color, fuenteTexto As Font)
        Dim inicioTexto As Integer = lblPrg.TextLength
        lblPrg.AppendText(mensaje & vbNewLine)
        Dim finTexto As Integer = lblPrg.TextLength

        ' Aplica el color y la fuente al texto después del signo igual
        AplicarFormatoTexto(lblPrg, inicioTexto, finTexto, mensaje, colorTexto, fuenteTexto)

        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
    End Sub

    Private Shared Sub AplicarFormatoTexto(ByRef lblPrg As RichTextBox, inicioTexto As Integer, finTexto As Integer, mensaje As String, colorTexto As Color, fuenteTexto As Font)
        lblPrg.Select(inicioTexto, finTexto - inicioTexto)

        ' Aplica el color y la fuente
        lblPrg.SelectionColor = colorTexto
        lblPrg.SelectionFont = fuenteTexto

        ' Restablece la selección
        lblPrg.Select(finTexto, 0)
    End Sub

    Public Shared Sub Actualizar_Progreso_Centrado(ByRef lblPrg As RichTextBox, mensaje As String, Optional colorTexto As Color = Nothing, Optional fuenteTexto As Font = Nothing)
        Dim inicioTexto As Integer = lblPrg.TextLength
        lblPrg.AppendText(mensaje & vbNewLine)
        Dim finTexto As Integer = lblPrg.TextLength

        ' Centra el texto
        lblPrg.Select(inicioTexto, finTexto - inicioTexto)
        lblPrg.SelectionAlignment = HorizontalAlignment.Center

        ' Aplica el color y la fuente si están especificados
        If Not IsDBNull(colorTexto) Then
            lblPrg.SelectionColor = colorTexto
        End If
        If fuenteTexto IsNot Nothing Then
            lblPrg.SelectionFont = fuenteTexto
        End If

        ' Restablece la selección y desplaza la vista si es necesario
        lblPrg.Select(finTexto, 0)
        lblPrg.ScrollToCaret()
    End Sub

End Class