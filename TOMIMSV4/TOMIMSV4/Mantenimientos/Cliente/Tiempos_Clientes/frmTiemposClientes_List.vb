Imports System.IO
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmTiemposClientes_List

    Public Property IsLoading As Boolean = False
    Public Property Modo As pModo

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Dim reportHeader As String = vbNewLine & "Listado de Clientes"

    Private Sub Listar_Tiempos_Clientes_Clas_And_Fam()

        Try

            IsLoading = True

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Listando detalle de tiempos...")

            Dim lista As New DataTable

            lista = clsLnCliente_tiempos.Get_All_Tiempos_Clas_And_Fam(chkConTiempos.Checked)

            If lista IsNot Nothing AndAlso lista.Rows.Count > 0 Then

                Dgrid.DataSource = lista

            Else
                Dgrid.DataSource = Nothing
            End If

            If (GridView1.Columns.Count <> 0) Then

                Try

                    GridView1.Columns("Codigo").SummaryItem.SummaryType = SummaryItemType.Count
                    GridView1.Columns("Codigo").SummaryItem.DisplayFormat = "{0:n2}"

                    GridView1.BestFitColumns()

                    IsLoading = False

                    Set_LayOut_Grid1()

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try

            End If

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkConTiempos.CheckedChanged
        Listar_Tiempos_Clientes_Clas_And_Fam()

        If GridView1.RowCount > 0 Then

            Dim rowHandle As Integer = GridView1.GetVisibleRowHandle(0)

            GridView1.FocusedRowHandle = rowHandle
            GridView1.SelectRow(rowHandle)

            Dim vCodCliente As String = GridView1.GetRowCellValue(rowHandle, "Codigo")

            Listar_Tiempos_Clientes_Det(vCodCliente)



        End If
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")

        If Dgrid.Focused Then
            reportHeader = vbNewLine & "Listado de Clientes"
            Imprimir_Vista(Dgrid)
        ElseIf dgridDetalle.Focused Then
            reportHeader = vbNewLine & "Listado de Tiempos"
            Imprimir_Vista(dgridDetalle)
        End If

        SplashScreenManager.CloseForm(False)

    End Sub

    Private Sub Imprimir_Vista(ByVal pGrid As DevExpress.XtraGrid.GridControl)

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True

            printLink.Component = pGrid

            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Listar_Tiempos_Clientes_Clas_And_Fam()

        If GridView1.RowCount > 0 Then

            Dim rowHandle As Integer = GridView1.GetVisibleRowHandle(0)

            GridView1.FocusedRowHandle = rowHandle
            GridView1.SelectRow(rowHandle)

            Dim vCodCliente As String = GridView1.GetRowCellValue(rowHandle, "Codigo")

            Listar_Tiempos_Clientes_Det(vCodCliente)

        End If

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.Options.UseBackColor = True
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmTiemposClientes_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Listar_Tiempos_Clientes_Det(ByVal pCodCliente As String)

        IsLoading = True

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Listando detalle de tiempos...")

            Dim lista As New DataTable

            lista = clsLnCliente_tiempos.Get_All_Tiempos_Det_By_IdCliente(pCodCliente)

            If lista IsNot Nothing AndAlso lista.Rows.Count > 0 Then

                dgridDetalle.DataSource = lista

            Else
                dgridDetalle.DataSource = Nothing
            End If

            If (GridView2.Columns.Count <> 0) Then

                Try

                    GridView2.Columns("Familia").SummaryItem.SummaryType = SummaryItemType.Count
                    GridView2.Columns("Familia").SummaryItem.DisplayFormat = "{0:n2}"

                    GridView2.BestFitColumns()
                    GridView2.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False
                    GridView2.OptionsBehavior.AutoExpandAllGroups = True

                    IsLoading = False

                    Set_LayOut_Grid2()

                Catch ex As Exception
                    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try

            End If

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub frmTiemposClientes_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Listar_Tiempos_Clientes_Clas_And_Fam()

            If GridView1.RowCount > 0 Then

                Dim rowHandle As Integer = GridView1.GetVisibleRowHandle(0)

                GridView1.FocusedRowHandle = rowHandle
                GridView1.SelectRow(rowHandle)

                Dim vCodCliente As String = GridView1.GetRowCellValue(rowHandle, "Codigo")

                Listar_Tiempos_Clientes_Det(vCodCliente)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Dim vNombreArchivoLayOutGridvGridTiemposFamAndClas As String = "vGridTiemposFamAndClas"
    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        If IsLoading Then Return

        Try

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGridvGridTiemposFamAndClas,
                                                          LayoutToText)

            'mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_LayOut_Grid1()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridvGridTiemposFamAndClas)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                'mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                'mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Dim vNombreArchivoLayOutGridvGridTiemposDet As String = "vGridTiemposDet"
    Private Sub GridView2_Layout(sender As Object, e As EventArgs) Handles GridView2.Layout

        If IsLoading Then Return

        Try

            Dim Ms As New MemoryStream
            GridView2.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGridvGridTiemposDet,
                                                          LayoutToText)

            'mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_LayOut_Grid2()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridvGridTiemposDet)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView2.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                'mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                'mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick

        If GridView1.RowCount > 0 Then

            Dim rowHandle As Integer = GridView1.FocusedRowHandle

            If rowHandle >= 0 Then

                Dim vCodCliente As String = GridView1.GetRowCellValue(rowHandle, "Codigo")

                Listar_Tiempos_Clientes_Det(vCodCliente)

            End If

        End If

    End Sub

End Class