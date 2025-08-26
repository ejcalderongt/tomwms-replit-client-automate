Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmRptUbicacionPicking


    Private Sub frmRptUbicacionPicking_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Cargar()

    End Sub


    Private Sub Cargar()

        Try

            Grd.DataSource = clsLnTrans_picking_ubic.Get_Ubicacion_Picking_By_IdPicking_And_IdPedidoEnc(txtIdPicking.Value, 0)

            GridView.Columns(0).GroupIndex = 0
            GridView.OptionsView.ShowFooter = True
            GridView.OptionsBehavior.AutoExpandAllGroups = True

            GridView.Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            GridView.Columns(7).SummaryItem.DisplayFormat = "{0:n2}"

            GridView.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridView.Columns(7).DisplayFormat.FormatString = "{0:n2}"

            GridView.Columns(8).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            GridView.Columns(8).SummaryItem.DisplayFormat = "{0:n2}"

            GridView.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridView.Columns(8).DisplayFormat.FormatString = "{0:n2}"

            GridView.Columns(9).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            GridView.Columns(9).SummaryItem.DisplayFormat = "{0:n2}"

            GridView.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            GridView.Columns(9).DisplayFormat.FormatString = "{0:n2}"

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

        Dim reportHeader As String = vbNewLine & "Listado de Ubicación Picking"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub


    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick

        Imprimir_Vista()

    End Sub


    Private Sub txtIdPicking_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdPicking.KeyPress

        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If

        If e.KeyChar = "." Then
            e.Handled = True
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If

        If e.KeyChar = Convert.ToChar(8) AndAlso txtIdPicking.Text.Length = 1 Then
            txtIdPicking.Value = 0
        End If

    End Sub


    Private Sub cmdBuscar_Click(sender As Object, e As EventArgs) Handles cmdBuscar.Click

        Try

            If String.IsNullOrEmpty(txtIdPicking.Value) Then
                Throw New Exception("Ingrese Número de Picking")
            End If

            Cargar()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub txtIdPicking_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdPicking.KeyDown

        If e.KeyCode = Keys.Enter Then
            cmdBuscar_Click(sender, e)
        End If

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