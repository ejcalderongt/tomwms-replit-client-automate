Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmDespacho_List

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmPicking_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListarDespacho()
    End Sub

    Private Sub ListarDespacho()

        Try

            Dgrid.DataSource = clsLnTrans_despacho_enc.GetAllByIdBodega(chkActivos.Checked, dtpFechaDel.Value, dtpFechaAl.Value, AP.IdBodega)

            If GridView1.Columns.Count > 0 Then
                GridView1.Columns("activo").Visible = False
                GridView1.Columns("IdBodega").Visible = False
                GridView1.Columns("fecha").Caption = "Fecha"
            End If

            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

            Try

                GridView1.OptionsView.ColumnAutoWidth = False

                If GridView1.Columns.Count > 0 Then
                    GridView1.BestFitColumns()
                End If

            Catch ex As Exception
            End Try

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Nuevo_Despacho()

        Try

            Cierra_Instancia_Previa(frmDespacho)

            With frmDespacho
                .Modo = frmDespacho.TipoTrans.Nuevo
                .InvokeListarDespacho = AddressOf ListarDespacho
                .WindowState = FormWindowState.Normal

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                    .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                    .mnuAnular.Enabled = .OpcionesMenu.Eliminar
                End If

                .Activate()
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

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

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        mnuNuevo.Enabled = False
        Nuevo_Despacho()
        mnuNuevo.Enabled = True
    End Sub

    Private Sub Procesar_Registro()

        Try

            If GridView1.RowCount > 0 Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmDespacho)

                    With frmDespacho
                        .Modo = frmDespacho.TipoTrans.Editar
                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Despacho")
                        .BeDespachoEnc = clsLnTrans_despacho_enc.GetSingle(Dr.Item("Código"))
                        SplashScreenManager.CloseForm(False)
                        .InvokeListarDespacho = AddressOf ListarDespacho
                        .WindowState = FormWindowState.Maximized

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                            .mnuActualizar.Enabled = OpcionesMenu.Modificar
                            .mnuGuardar.Enabled = OpcionesMenu.Modificar
                            .mnuAnular.Enabled = OpcionesMenu.Eliminar
                        End If

                        .Show()
                        .Focus()
                    End With

                    'Dim Despacho As New frmDespacho(frmDespacho.TipoTrans.Editar)
                    'SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    'SplashScreenManager.Default.SetWaitFormCaption("Despacho")

                    'Despacho.BeDespachoEnc = clsLnTrans_despacho_enc.GetSingle(Dr.Item("Código"))
                    'SplashScreenManager.CloseForm(False)
                    'Despacho.ShowDialog()
                    'Despacho.Dispose()
                    'ListarDespacho()

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        ListarDespacho()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        ListarDespacho()
    End Sub


    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(Dgrid, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True, True, 12)
            Exit Sub
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
            printLink.Component = Dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Despachos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            ListarDespacho()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            ListarDespacho()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

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

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmDespacho_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub mnuVacio_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuVacio.ItemClick
        mnuVacio.Enabled = False
        Nuevo_Despacho_Vacio()
        mnuVacio.Enabled = True
    End Sub

    Private Sub Nuevo_Despacho_Vacio()

        Try

            Cierra_Instancia_Previa(frmDespacho)

            With frmDespacho
                .Modo = frmDespacho.TipoTrans.Nuevo
                .InvokeListarDespacho = AddressOf ListarDespacho
                .WindowState = FormWindowState.Normal

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                    .mnuActualizar.Enabled = .OpcionesMenu.Modificar
                    .mnuAnular.Enabled = .OpcionesMenu.Eliminar
                End If

                .Activate()
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportarExcel.ItemClick

        Try

            Exportar_Grid_A_Excel(Dgrid, "WMS_ListaPickings_" & DateTime.Now.ToString("yyyy_MM_dd") & ".xlsx")

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

        Try

            Try

                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()

                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True
                saveFileDialog1.FileName = NomArchivo

                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        ' Code to write the stream goes here.
                        dGrid.ExportToXlsx(myStream)
                        myStream.Close()
                    End If
                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView1.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView1.DataRowCount.ToString()))
    End Sub

End Class


