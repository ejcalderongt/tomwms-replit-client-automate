Imports DevExpress.XtraEditors

Public Class frmEstacionalidadProducto

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private cantidad As Double = 0

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Call_Listar_Reporte_Safe As New MethodInvoker(AddressOf Listar_Reporte)

    Private Sub Listar_Reporte()

        Try

            lblEstatus.Text = "Buscando registros..."
            lblEstatus.Refresh()

            Dgrid.DataSource = Nothing

            Dim IdBodega As Integer = Integer.Parse(IIf(cmbBodega.EditValue IsNot Nothing, cmbBodega.EditValue, 0))
            Dim IdPropietarioBodega As Integer = Integer.Parse(IIf(cmbPropietarioBodega.EditValue IsNot Nothing, cmbPropietarioBodega.EditValue, 0))
            Dim DT As New DataTable
            Dim rango As Integer = IIf(TrackBarControl1.Value > 0, TrackBarControl1.Value, 0)

            DT = clsLnStock.Get_Rpt_Estacionalidad_Producto_By_IdBodega_And_IdPropietarioBodega(0,
                                                                                             IdBodega,
                                                                                             IdPropietarioBodega,
                                                                                             rango)


            If DT.Rows.Count = 0 Then
                lblEstatus.Text = "0 registros encontrados"
                lblEstatus.Refresh()
            Else
                lblEstatus.Text = DT.Rows.Count - 1 & " registros encontrados " & vbCrLf & " Rango días: " & TrackBarControl1.Value
                lblEstatus.Refresh()
            End If

            Dgrid.DataSource = DT

            If GridView1.RowCount > 0 Then

                GridView1.Columns(0).GroupIndex = 0

                GridView1.OptionsBehavior.AutoExpandAllGroups = True

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("CantPres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantPres").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("CantPres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantPres").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("CantUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantUMBas").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("CantUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantUMBas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Tolerancia_dias").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Tolerancia_dias").DisplayFormat.FormatString = "{0:n2}"

                GridView1.Columns("Tolerancia_dias").Caption = "Estacionalidad"

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

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

    Private Sub TrackBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles TrackBarControl1.EditValueChanged
        TrackBarControl1.ToolTip = TrackBarControl1.Value
        If IsHandleCreated Then BeginInvoke(Call_Listar_Reporte_Safe)
    End Sub


    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)
        If IsHandleCreated Then BeginInvoke(Call_Listar_Reporte_Safe)

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        If IsHandleCreated Then BeginInvoke(Call_Listar_Reporte_Safe)
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Close()
    End Sub

    Private Sub frmProximos_A_Vencer_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            '#EJC20191205: Un año debería ser el máximo
            'Pensar si hace sentido conocer vencimientos a mas de un año.
            'Att Erik del pasado.
            'DiasTol = clsLnProducto.MaxTolerancia()
            Dim DiasTol As Integer = 365

            TrackBarControl1.Value = 0

            If DiasTol > 0 Then
                TrackBarControl1.Properties.Maximum = DiasTol
                TrackBarControl1.Value = 1
            End If

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(Dgrid, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
            Exit Sub
            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

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

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Productos Sin Movimiento"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

End Class




