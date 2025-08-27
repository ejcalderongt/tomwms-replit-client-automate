Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmEjecucion

    Public Property Interface_A_Ejecutar As frmMenu.pInterfaceAEjecutar = -1
    Public Property pWSActivo As WsActivo

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
    Public Sub Ejecuta_Interface_Clientes(Optional ByVal Preguntar As Boolean = True)

        Dim lConnection As New SqlClient.SqlConnection(BD.Instancia.CadenaConexionSQLClient)

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

                Using sProv As New clsSyncERPCliente()
                    sProv.Ejecutar_Interfaz("Cliente")
                    sProv.Insertar_Clientes_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, False)
                End Using

                '#GT27052024: agrego importar acuerdos en la misma funcionalidad de importar propietarios.
                'Using sPc As New clsSyncERPAcuerdosComerciales()
                '    sPc.Ejecutar_Interfaz("Acuerdo_Comerciales")
                '    sPc.Insertar_Acuerdos_Comerciales_Desde_ERP(lblprg, prg, True, Preguntar)
                'End Using

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            prg.Visible = False
        End Try

    End Sub

    Private Sub mnuActualizarProveedores_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizarProveedores.ItemClick

        Try

            mnuActualizarProveedores.Enabled = False
            Application.DoEvents()

            Ejecuta_Interface_Clientes(False)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            mnuActualizarProveedores.Enabled = True
        End Try
    End Sub

    'Public threadListar_Stock As New Thread(AddressOf Listar_Stock_With_Obj) With {.IsBackground = True}

    Private Sub Ejecuta_Interface_Acuerdos_Comerciales(Optional ByVal Preguntar As Boolean = True)

        Try

            Dim Ejecutar As Boolean = False

            If Preguntar Then
                If XtraMessageBox.Show("¿Actualizar acuerdos comerciales?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Ejecutar = True
                End If
            Else
                Ejecutar = True
            End If

            If Ejecutar Then
                Using sPc As New clsSyncERPAcuerdosComerciales()
                    sPc.Ejecutar_Interfaz("Acuerdo_Comerciales")
                    sPc.Insertar_Acuerdos_Comerciales_Desde_ERP(lblprg, prg, True, Preguntar)
                End Using
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuAcuerdos_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAcuerdos.ItemClick
        Ejecuta_Interface_Acuerdos_Comerciales()
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
                    sProd.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg, True, False)
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
                sProd.Insertar_Productos_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(lblprg, prg)
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

    Private Sub mnuConversiones_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)

        Try

            prg.Visible = True

            If XtraMessageBox.Show("¿Actualizar registros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Using sConv As New clsSyncNavTablaConversion()
                    'sConv.Get_Single_Conversiones_FromWS()
                End Using

                Using sConv As New clsSyncNavTablaConversion()
                    'sConv.Get_Tabla_Conversiones_FromWS()
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



    Private Sub mnuEnviarPedidosCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEnviarPedidosCompra.ItemClick
        'Enviar_Pedidos_Compra(True)
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

            If Interface_A_Ejecutar <> -1 Then

                mnuBodegas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuActualizarProveedores.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuProductosI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuAcuerdos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuReporteEjecuciones.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            End If

            Select Case Interface_A_Ejecutar

                Case pInterfaceAEjecutar.Importar_Bodegas
                    mnuBodegas.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                   ' Ejecuta_Interface_Bodegas(False)
                Case pInterfaceAEjecutar.Importar_Pedidos_De_Compra
                    mnuAcuerdos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Ejecuta_Interface_Acuerdos_Comerciales(False)
                Case pInterfaceAEjecutar.Importar_Productos
                    mnuProductosI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Ejecuta_Interface_Productos(False)
                Case pInterfaceAEjecutar.Enviar_Pedidos_Compra
                    mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    mnuEnviarPedidosCompra.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    'Enviar_Pedidos_Compra(False)
                Case pInterfaceAEjecutar.Enviar_Pedidos_Transferencia
                    mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    mnuEnviarPedidosTransferencia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    'Enviar_Pedidos_Transferencia(False)
                Case Else
                    Exit Select

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub mnuSyncLotes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)

        Try

            Dim L As New clsSyncLotes()

            'L.Get_Lista_Lotes(Nothing, Nothing, Nothing)

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

End Class