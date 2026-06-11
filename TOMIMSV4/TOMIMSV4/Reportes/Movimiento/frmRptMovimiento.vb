Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmRptMovimiento

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim iX As DevExpress.XtraBars.Docking.DockPanel
        iX = DockManager1.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Left)
        iX.Text = "Filtros"
        iX.Width = 275
        GroupControl6.Dock = DockStyle.Fill
        iX.ControlContainer.Controls.Add(GroupControl6)

        'Dim p1 As DockPanel = DockManager1.AddPanel(DockingStyle.Left)
        'p1.Text = "Filtros"
        'p1.ParentPanel.Tabbed = True

        '#CKFK 20190129 Agregué que se inicie en la bodega con la que se entró a la aplicación
        AP.Listar_Bodegas_By_Usuario(cmbBodega)
        cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

        Cargar()

    End Sub

    Private Sub Cargar()

        Try

            If cmbBodega.Text <> "" Then

                Dim dt As DataTable = clsLnTrans_movimientos.Get_Movimientos(cmbBodega.EditValue, dtpFechaDel.Value, dtpFechaAl.Value)

                Grd.DataSource = Nothing

                If dt.Rows.Count > 0 Then

                    Grd.DataSource = dt

                    GridView.OptionsView.ShowFooter = True

                    GridView.Columns("cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView.Columns("cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView.Columns("cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"

                    GridView.Columns("peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView.Columns("peso").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView.Columns("peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView.Columns("peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView.Columns("IdBodegaOrigen").Visible = False

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(Grd, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = Grd
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Movimientos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        Imprimir_Vista()

    End Sub

    Private Sub cmbBodega_SelectedValueChanged(sender As Object, e As EventArgs)

        Cargar()
        GridView.Focus()

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar()
            '    GridView.Focus()
            'End If

            Cargar()
            GridView.Focus()

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

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then
            '    Throw New Exception("Seleccione un rango de fechas válido.")
            'Else
            '    Cargar()
            '    GridView.Focus()
            'End If

            Cargar()
            GridView.Focus()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView.RowStyle

        Try

            GridView.OptionsBehavior.Editable = False
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridView.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView.OptionsSelection.EnableAppearanceHideSelection = True
            GridView.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridView.Appearance.FocusedRow.ForeColor = Color.White
            GridView.Appearance.SelectedRow.ForeColor = Color.White

            GridView.Appearance.SelectedRow.Options.UseBackColor = True
            GridView.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


End Class


