Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmReportDespacho
    Public Property Modo As pModo
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Cargar()

        Dim dt As DataTable

        Try

            dt = clsLnTrans_movimientos.GetMovimientosSalidas(fchDel.Value, fchAl.Value)

            grdSalidas.DataSource = Nothing

            If dt.Rows.Count > 0 Then

                grdSalidas.DataSource = dt


                GridView1.Columns("IdBodegaOrigen").Visible = False
                GridView1.Columns("Estado Origen").Visible = False
                GridView1.Columns("Estado Destino").Visible = False
                GridView1.Columns("peso").Visible = False
                GridView1.Columns("lote").Visible = False
                GridView1.Columns("Origen").Visible = False
                GridView1.Columns("Destino").Visible = False
                GridView1.Columns("Tipo Tarea").Visible = False
                GridView1.Columns("IdProducto").Visible = False

                GridView1.Columns("codigo").Caption = "Código"
                GridView1.Columns("codigo_barra").Caption = "Código Barra"
                GridView1.Columns("cantidad").Caption = "Cantidad"
                GridView1.Columns("fecha").Caption = "Fecha"

                'Cambia la posición de las columnas.
                GridView1.Columns("codigo").VisibleIndex = 1
                GridView1.Columns("codigo_barra").VisibleIndex = 2

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"

            End If

            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
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

    Private Sub frmReportDespacho_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
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

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

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

    Private Sub fchAl_ValueChanged(sender As Object, e As EventArgs) Handles fchAl.ValueChanged

        Try

            If Me.fchDel.Value > Me.fchAl.Value Or Me.fchAl.Value < Me.fchDel.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Cargar()
                GridView1.Focus()
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

    Private Sub fchDel_ValueChanged(sender As Object, e As EventArgs) Handles fchDel.ValueChanged

        Try

            If Me.fchDel.Value > Me.fchAl.Value Or Me.fchAl.Value < Me.fchDel.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Cargar()
                GridView1.Focus()
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

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdSalidas, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = grdSalidas
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

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Salidas"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

End Class



