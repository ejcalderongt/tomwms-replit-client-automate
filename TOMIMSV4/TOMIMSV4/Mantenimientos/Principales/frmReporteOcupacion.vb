Imports System.Drawing.Drawing2D
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks

Public Class frmReporteOcupacion
    Inherits XtraForm

    Private ReadOnly _idBodega As Integer
    Private ReadOnly _dtOcupacion As DataTable
    Private ReadOnly _titulo As String

    ' KPI superior izquierdo
    Private pnlIndicador As PanelControl
    Private lblKpiTitulo As LabelControl
    Private lblKpiPorcentaje As LabelControl
    Private lblKpiDetalle As LabelControl
    Private pbOcupacion As ProgressBarControl

    ' Charts
    Private chartArea As ChartControl
    Private chartResumen As ChartControl

    ' Grid
    Private grid As GridControl
    Private view As GridView

    ' Botones
    Private btnPreview As SimpleButton
    Private btnPdf As SimpleButton
    Private btnCerrar As SimpleButton

    Public Sub New(idBodega As Integer, dtOcupacion As DataTable, titulo As String)
        _idBodega = idBodega
        _dtOcupacion = dtOcupacion
        _titulo = titulo

        Me.Text = "Reporte de ocupación"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.WindowState = FormWindowState.Maximized

        BuildUI()
        BindData()
    End Sub

    Private Sub BuildUI()
        ' =========================
        ' Layout principal
        ' =========================
        Dim root As New TableLayoutPanel()
        root.Dock = DockStyle.Fill
        root.ColumnCount = 1
        root.RowCount = 3
        root.RowStyles.Add(New RowStyle(SizeType.Absolute, 52))
        root.RowStyles.Add(New RowStyle(SizeType.Percent, 45))
        root.RowStyles.Add(New RowStyle(SizeType.Percent, 55))
        Me.Controls.Add(root)

        ' =========================
        ' Barra superior
        ' =========================
        Dim topPanel As New PanelControl()
        topPanel.Dock = DockStyle.Fill
        root.Controls.Add(topPanel, 0, 0)

        btnPreview = New SimpleButton() With {.Text = "Vista previa / Imprimir", .Height = 34, .Width = 170}
        btnPdf = New SimpleButton() With {.Text = "Exportar PDF", .Height = 34, .Width = 120}
        btnCerrar = New SimpleButton() With {.Text = "Cerrar", .Height = 34, .Width = 90}

        btnPreview.Location = New Point(12, 9)
        btnPdf.Location = New Point(190, 9)
        btnCerrar.Location = New Point(318, 9)

        AddHandler btnPreview.Click, AddressOf BtnPreview_Click
        AddHandler btnPdf.Click, AddressOf BtnPdf_Click
        AddHandler btnCerrar.Click, Sub() Me.Close()

        topPanel.Controls.Add(btnPreview)
        topPanel.Controls.Add(btnPdf)
        topPanel.Controls.Add(btnCerrar)

        ' =========================
        ' Panel superior (3 bloques)
        ' =========================
        Dim chartsPanel As New TableLayoutPanel()
        chartsPanel.Dock = DockStyle.Fill
        chartsPanel.ColumnCount = 3
        chartsPanel.RowCount = 1
        chartsPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 28))
        chartsPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 42))
        chartsPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30))
        root.Controls.Add(chartsPanel, 0, 1)

        ' =========================
        ' KPI ocupación (izquierda)
        ' =========================
        pnlIndicador = New PanelControl()
        pnlIndicador.Dock = DockStyle.Fill
        pnlIndicador.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        pnlIndicador.Padding = New Padding(16)

        lblKpiTitulo = New LabelControl()
        lblKpiTitulo.Dock = DockStyle.Top
        lblKpiTitulo.Height = 28
        lblKpiTitulo.Appearance.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        lblKpiTitulo.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        lblKpiTitulo.Text = "Estado de ocupación"

        lblKpiPorcentaje = New LabelControl()
        lblKpiPorcentaje.Dock = DockStyle.Top
        lblKpiPorcentaje.Height = 58
        lblKpiPorcentaje.Appearance.Font = New Font("Segoe UI", 28, FontStyle.Bold)
        lblKpiPorcentaje.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        lblKpiPorcentaje.Text = "0.00%"

        pbOcupacion = New ProgressBarControl()
        pbOcupacion.Dock = DockStyle.Top
        pbOcupacion.Height = 36
        pbOcupacion.Properties.Minimum = 0
        pbOcupacion.Properties.Maximum = 100
        pbOcupacion.Properties.ShowTitle = True
        pbOcupacion.Properties.PercentView = True

        lblKpiDetalle = New LabelControl()
        lblKpiDetalle.Dock = DockStyle.Top
        lblKpiDetalle.Height = 55
        lblKpiDetalle.Appearance.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        lblKpiDetalle.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        lblKpiDetalle.Appearance.TextOptions.WordWrap = WordWrap.Wrap
        lblKpiDetalle.AutoSizeMode = LabelAutoSizeMode.None
        lblKpiDetalle.Text = "Ocupadas: 0 | Vacías: 0 | Total: 0"

        pnlIndicador.Controls.Add(lblKpiDetalle)
        pnlIndicador.Controls.Add(pbOcupacion)
        pnlIndicador.Controls.Add(lblKpiPorcentaje)
        pnlIndicador.Controls.Add(lblKpiTitulo)

        chartsPanel.Controls.Add(WrapGroup("Porcentaje de ocupación", pnlIndicador), 0, 0)

        ' =========================
        ' Chart por área
        ' =========================
        chartArea = New ChartControl() With {.Dock = DockStyle.Fill}
        AddHandler chartArea.ObjectSelected, AddressOf chartArea_ObjectSelected
        chartsPanel.Controls.Add(WrapGroup("Ocupación por área", chartArea), 1, 0)

        ' =========================
        ' Chart resumen
        ' =========================
        chartResumen = New ChartControl() With {.Dock = DockStyle.Fill}
        chartsPanel.Controls.Add(WrapGroup("Resumen", chartResumen), 2, 0)

        ' =========================
        ' Grid
        ' =========================
        grid = New GridControl() With {.Dock = DockStyle.Fill}
        view = New GridView(grid)
        grid.MainView = view
        grid.ViewCollection.Add(view)
        root.Controls.Add(WrapGroup("Detalle por área", grid), 0, 2)

        view.OptionsView.ShowGroupPanel = False
        view.OptionsView.ColumnAutoWidth = False
        view.OptionsBehavior.Editable = False
        view.OptionsPrint.PrintHeader = True
        view.OptionsPrint.AutoWidth = False
        view.OptionsView.ShowFooter = True
    End Sub

    Private Sub chartArea_ObjectSelected(sender As Object, e As HotTrackEventArgs)
        Try
            If e Is Nothing OrElse e.HitInfo Is Nothing Then Exit Sub
            If e.HitInfo.SeriesPoint Is Nothing Then Exit Sub

            Dim area As String = Convert.ToString(e.HitInfo.SeriesPoint.Argument)
            If String.IsNullOrWhiteSpace(area) Then Exit Sub

            Dim nombreSerie As String = ""

            If e.HitInfo.Series IsNot Nothing Then
                nombreSerie = e.HitInfo.Series.ToString()
            End If

            ' Como ToString() puede no servir, mejor buscamos la serie real por referencia
            For Each s As DevExpress.XtraCharts.Series In chartArea.Series
                If Object.ReferenceEquals(s, TryCast(e.HitInfo.Series, DevExpress.XtraCharts.Series)) Then
                    nombreSerie = s.Name
                    Exit For
                End If
            Next

            Dim esVacia As Boolean =
            nombreSerie.IndexOf("Vacías", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
            nombreSerie.IndexOf("Vacias", StringComparison.OrdinalIgnoreCase) >= 0

            If Not esVacia Then Exit Sub

            MostrarUbicacionesVaciasPorArea(area)

        Catch ex As Exception
            XtraMessageBox.Show("Error al consultar ubicaciones vacías: " & ex.Message,
                            Me.Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function WrapGroup(titulo As String, ctrl As Windows.Forms.Control) As Windows.Forms.Control
        Dim g As New GroupControl()
        g.Text = titulo
        g.Dock = DockStyle.Fill
        ctrl.Dock = DockStyle.Fill
        g.Controls.Add(ctrl)
        Return g
    End Function

    Private Sub BindData()

        If _dtOcupacion Is Nothing OrElse _dtOcupacion.Rows.Count = 0 Then
            grid.DataSource = Nothing
            chartArea.Series.Clear()
            chartResumen.Series.Clear()
            lblKpiPorcentaje.Text = "0.00%"
            pbOcupacion.EditValue = 0
            lblKpiDetalle.Text = "Ocupadas: 0 | Vacías: 0 | Total: 0"
            Exit Sub
        End If

        ' =========================
        ' 1) Grid detalle
        ' =========================
        Dim dtDetalle As DataTable = BuildDT_OcupacionDetalle(_dtOcupacion)
        grid.DataSource = dtDetalle

        If view.Columns("Ocupadas") IsNot Nothing Then
            view.Columns("Ocupadas").DisplayFormat.FormatType = FormatType.Numeric
            view.Columns("Ocupadas").DisplayFormat.FormatString = "n0"
            view.Columns("Ocupadas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            view.Columns("Ocupadas").SummaryItem.DisplayFormat = "{0:n0}"
        End If

        If view.Columns("Vacías") IsNot Nothing Then
            view.Columns("Vacías").DisplayFormat.FormatType = FormatType.Numeric
            view.Columns("Vacías").DisplayFormat.FormatString = "n0"
            view.Columns("Vacías").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            view.Columns("Vacías").SummaryItem.DisplayFormat = "{0:n0}"
        End If

        If view.Columns("Total") IsNot Nothing Then
            view.Columns("Total").DisplayFormat.FormatType = FormatType.Numeric
            view.Columns("Total").DisplayFormat.FormatString = "n0"
            view.Columns("Total").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            view.Columns("Total").SummaryItem.DisplayFormat = "{0:n0}"
        End If

        If view.Columns("Porcentaje") IsNot Nothing Then
            view.Columns("Porcentaje").AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center
        End If

        view.BestFitColumns()

        RemoveHandler view.RowStyle, AddressOf Grid_RowStyle_Total
        AddHandler view.RowStyle, AddressOf Grid_RowStyle_Total

        ' =========================
        ' 2) KPI ocupación
        ' =========================
        Dim ocupadasTotalObj As Object = _dtOcupacion.Compute("SUM(Cantidad)", "Estado='Ocupadas'")
        Dim ocupadasTotal As Decimal = If(IsDBNull(ocupadasTotalObj), 0D, Convert.ToDecimal(ocupadasTotalObj))

        Dim totalObj As Object = _dtOcupacion.Compute("SUM(Cantidad)", Nothing)
        Dim total As Decimal = If(IsDBNull(totalObj), 0D, Convert.ToDecimal(totalObj))

        Dim vaciasKpi As Decimal = total - ocupadasTotal
        Dim perc As Decimal = If(total <= 0D, 0D, (ocupadasTotal * 100D) / total)

        lblKpiPorcentaje.Text = perc.ToString("0.00") & "%"
        pbOcupacion.EditValue = CInt(Math.Round(perc, 0))
        lblKpiDetalle.Text = String.Format("Ocupadas: {0:n0}   |   Vacías: {1:n0}   |   Total: {2:n0}",
                                           ocupadasTotal, vaciasKpi, total)

        If perc < 50D Then
            lblKpiPorcentaje.Appearance.ForeColor = Color.LimeGreen
        ElseIf perc < 80D Then
            lblKpiPorcentaje.Appearance.ForeColor = Color.Goldenrod
        Else
            lblKpiPorcentaje.Appearance.ForeColor = Color.Firebrick
        End If

        ' =========================
        ' 3) Chart por área
        ' =========================
        Dim dtChart As DataTable = _dtOcupacion.Copy()

        If Not dtChart.Columns.Contains("Serie") Then dtChart.Columns.Add("Serie", GetType(String))
        For Each r As DataRow In dtChart.Rows
            Dim estado As String = If(r.IsNull("Estado"), "", r("Estado").ToString().Trim())
            r("Serie") = estado
        Next

        chartArea.Series.Clear()
        chartArea.DataSource = dtChart
        chartArea.SeriesDataMember = "Serie"

        With chartArea.SeriesTemplate
            .ArgumentDataMember = "Area"
            .ValueDataMembers.Clear()
            .ValueDataMembers.AddRange(New String() {"Cantidad"})
            .View = New SideBySideStackedBarSeriesView()
            .LabelsVisibility = DefaultBoolean.False
            .ToolTipPointPattern = "Área: {A}" & vbCrLf & "Estado: {S}" & vbCrLf & "Cantidad: {V:n0}"
        End With

        chartArea.Legend.Visibility = DefaultBoolean.True
        chartArea.ToolTipEnabled = True
        chartArea.RefreshData()

        For Each s As DevExpress.XtraCharts.Series In chartArea.Series
            Dim v = TryCast(s.View, SideBySideStackedBarSeriesView)
            If v Is Nothing Then Continue For

            Dim esOcupada As Boolean = s.Name.IndexOf("Ocupadas", StringComparison.OrdinalIgnoreCase) >= 0
            v.Color = If(esOcupada, Color.Firebrick, Color.LimeGreen)
            v.BarWidth = 0.8
        Next

        Dim diagram = TryCast(chartArea.Diagram, XYDiagram)
        If diagram IsNot Nothing Then
            diagram.AxisY.Label.TextPattern = "{V:n0}"
            diagram.AxisY.Title.Text = "Ubicaciones"
            diagram.AxisY.Title.Visibility = DefaultBoolean.True

            diagram.AxisX.Title.Text = "Área"
            diagram.AxisX.Title.Visibility = DefaultBoolean.True
            diagram.AxisX.Label.Angle = 0
            diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = False
            diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = True
            diagram.AxisX.QualitativeScaleOptions.AutoGrid = False
            diagram.AxisX.WholeRange.SideMarginsValue = 0.5
        End If

        ' =========================
        ' 4) Pie resumen
        ' =========================
        Dim vaciasTotalObj As Object = _dtOcupacion.Compute("SUM(Cantidad)", "(Estado='Vacías' OR Estado='Vacias')")
        Dim vaciasPie As Decimal = If(IsDBNull(vaciasTotalObj), 0D, Convert.ToDecimal(vaciasTotalObj))

        Dim dtResumen As New DataTable()
        dtResumen.Columns.Add("Estado", GetType(String))
        dtResumen.Columns.Add("Cantidad", GetType(Decimal))
        dtResumen.Rows.Add("Ocupadas", ocupadasTotal)
        dtResumen.Rows.Add("Vacías", vaciasPie)

        chartResumen.Series.Clear()
        chartResumen.DataSource = dtResumen

        Dim sr As New DevExpress.XtraCharts.Series("Resumen", ViewType.Pie)
        sr.ArgumentDataMember = "Estado"
        sr.ValueDataMembers.AddRange(New String() {"Cantidad"})

        Dim lbl As PieSeriesLabel = TryCast(sr.Label, PieSeriesLabel)
        If lbl IsNot Nothing Then
            lbl.TextPattern = "{A} {VP:p2}"
            lbl.Position = PieSeriesLabelPosition.TwoColumns
            lbl.ResolveOverlappingMode = ResolveOverlappingMode.Default
        End If

        sr.LabelsVisibility = DefaultBoolean.True

        chartResumen.Series.Add(sr)
        chartResumen.Legend.Visibility = DefaultBoolean.True
        chartResumen.RefreshData()

        For Each p As SeriesPoint In sr.Points
            If p.Argument.Equals("Ocupadas", StringComparison.OrdinalIgnoreCase) Then
                p.Color = Color.Firebrick
            Else
                p.Color = Color.LimeGreen
            End If
        Next
    End Sub

    Private Sub Grid_RowStyle_Total(sender As Object, e As RowStyleEventArgs)
        If e.RowHandle < 0 Then Return
        Dim area As String = Convert.ToString(view.GetRowCellValue(e.RowHandle, "Area"))
        If String.Equals(area, "TOTAL", StringComparison.OrdinalIgnoreCase) Then
            e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            e.Appearance.BackColor = Color.FromArgb(245, 245, 245)
        End If
    End Sub

    Private Sub chartArea_MouseClick(sender As Object, e As MouseEventArgs)
        Try
            Dim hit As ChartHitInfo = chartArea.CalcHitInfo(e.Location)
            If hit Is Nothing OrElse hit.SeriesPoint Is Nothing Then Exit Sub

            Dim serieBase As SeriesBase = hit.Series
            If serieBase Is Nothing Then Exit Sub

            Dim nombreSerie As String = Convert.ToString(hit.SeriesPoint.Values)
            Dim esVacia As Boolean =
                nombreSerie.IndexOf("Vacías", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                nombreSerie.IndexOf("Vacias", StringComparison.OrdinalIgnoreCase) >= 0

            If Not esVacia Then Exit Sub

            Dim area As String = Convert.ToString(hit.SeriesPoint.Argument)
            If String.IsNullOrWhiteSpace(area) Then Exit Sub

            MostrarUbicacionesVaciasPorArea(area)

        Catch ex As Exception
            XtraMessageBox.Show("Error al consultar ubicaciones vacías: " & ex.Message,
                                Me.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub MostrarUbicacionesVaciasPorArea(area As String)
        Dim dtUbicaciones As DataTable = clsLnBodega.GetUbicacionesVaciasPorArea(_idBodega, area)

        If dtUbicaciones Is Nothing OrElse dtUbicaciones.Rows.Count = 0 Then
            XtraMessageBox.Show("No se encontraron ubicaciones vacías para el área: " & area,
                            Me.Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim f As New XtraForm()
        f.Text = "Ubicaciones vacías - " & area
        f.StartPosition = FormStartPosition.CenterParent
        f.Size = New Size(800, 500)

        Dim root As New TableLayoutPanel()
        root.Dock = DockStyle.Fill
        root.ColumnCount = 1
        root.RowCount = 2
        root.RowStyles.Add(New RowStyle(SizeType.Absolute, 45))
        root.RowStyles.Add(New RowStyle(SizeType.Percent, 100))
        f.Controls.Add(root)

        ' Panel de botones
        Dim pnlTop As New PanelControl()
        pnlTop.Dock = DockStyle.Fill
        root.Controls.Add(pnlTop, 0, 0)

        Dim btnExportar As New SimpleButton()
        btnExportar.Text = "Exportar Excel"
        btnExportar.Size = New Size(120, 30)
        btnExportar.Location = New Point(10, 7)

        Dim btnCerrar As New SimpleButton()
        btnCerrar.Text = "Cerrar"
        btnCerrar.Size = New Size(90, 30)
        btnCerrar.Location = New Point(140, 7)

        pnlTop.Controls.Add(btnExportar)
        pnlTop.Controls.Add(btnCerrar)

        ' Grid
        Dim gc As New GridControl() With {
        .Dock = DockStyle.Fill,
        .DataSource = dtUbicaciones
    }

        Dim gv As New GridView(gc)
        gc.MainView = gv
        gc.ViewCollection.Add(gv)

        gv.OptionsView.ShowGroupPanel = False
        gv.OptionsBehavior.Editable = False
        gv.OptionsView.ColumnAutoWidth = True
        gv.BestFitColumns()

        root.Controls.Add(gc, 0, 1)

        ' Evento cerrar
        AddHandler btnCerrar.Click,
        Sub()
            f.Close()
        End Sub

        ' Evento exportar
        AddHandler btnExportar.Click,
        Sub()
            Using sfd As New SaveFileDialog()
                sfd.Filter = "Excel (*.xlsx)|*.xlsx"
                sfd.FileName = "UbicacionesVacias_" & area.Replace(" ", "_").Replace("/", "_") & ".xlsx"

                If sfd.ShowDialog() = DialogResult.OK Then
                    Try
                        gc.ExportToXlsx(sfd.FileName)
                        XtraMessageBox.Show("Archivo exportado correctamente:" & vbCrLf & sfd.FileName,
                                            "Exportar Excel",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information)
                    Catch ex As Exception
                        XtraMessageBox.Show("Error al exportar a Excel: " & ex.Message,
                                            "Exportar Excel",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End Sub

        f.ShowDialog(Me)
    End Sub

    Private Sub BtnPreview_Click(sender As Object, e As EventArgs)
        PrintOrPreview(Nothing)
    End Sub

    Private Sub BtnPdf_Click(sender As Object, e As EventArgs)
        Using sfd As New SaveFileDialog()
            sfd.Filter = "PDF (*.pdf)|*.pdf"
            sfd.FileName = "Reporte_Ocupacion.pdf"
            If sfd.ShowDialog() = DialogResult.OK Then
                PrintOrPreview(sfd.FileName)
            End If
        End Using
    End Sub

    Private Sub PrintOrPreview(exportPdfPath As String)

        Dim ps As New PrintingSystem()

        Dim linkChart1 As New PrintableComponentLink(ps)
        linkChart1.Component = CType(chartArea, IBasePrintable)

        Dim linkChart2 As New PrintableComponentLink(ps)
        linkChart2.Component = CType(chartResumen, IBasePrintable)

        Dim linkGrid As New PrintableComponentLink(ps)
        linkGrid.Component = CType(grid, IBasePrintable)

        Dim composite As New CompositeLink(ps)

        AddHandler composite.CreateReportHeaderArea,
            Sub(sender As Object, e As CreateAreaEventArgs)
                Dim r As New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 40)
                Dim tb As TextBrick = e.Graph.DrawString(_titulo, r)
                tb.Font = New Font("Segoe UI", 14, FontStyle.Bold)
                tb.ForeColor = Color.Black
                tb.BackColor = Color.Transparent
                tb.StringFormat = New BrickStringFormat(StringAlignment.Center, StringAlignment.Center)
            End Sub

        AddHandler composite.CreateDetailArea,
            Sub(sender As Object, e As CreateAreaEventArgs)
                Using img As Image = ControlToImage(pnlIndicador)
                    Dim w As Single = e.Graph.ClientPageSize.Width
                    Dim h As Single = 220
                    Dim rect As New RectangleF(0, 0, w, h)
                    e.Graph.DrawImage(img, rect)
                    e.Graph.DrawEmptyBrick(New RectangleF(0, h, w, 10))
                End Using
            End Sub

        composite.Links.AddRange(New Object() {linkChart1, linkChart2, linkGrid})

        ps.PageSettings.Landscape = True
        ps.PageSettings.LeftMargin = 30
        ps.PageSettings.RightMargin = 30
        ps.PageSettings.TopMargin = 40
        ps.PageSettings.BottomMargin = 40

        Dim phf As PageHeaderFooter = TryCast(composite.PageHeaderFooter, PageHeaderFooter)
        If phf IsNot Nothing Then
            phf.Header.Content.Clear()
            phf.Footer.Content.Clear()

            phf.Header.Content.AddRange(New String() {$"Usuario: {AP.UsuarioAp.Nombres}", "", "Fecha: [Date Printed] [Time Printed]"})
            phf.Header.LineAlignment = BrickAlignment.Far

            phf.Footer.Content.AddRange(New String() {"Páginas: [Page # of Pages #]"})
            phf.Footer.LineAlignment = BrickAlignment.Near
        End If

        composite.CreateDocument()

        If String.IsNullOrEmpty(exportPdfPath) Then
            ps.PreviewFormEx.ShowDialog()
        Else
            ps.ExportToPdf(exportPdfPath)
            XtraMessageBox.Show("PDF generado correctamente:" & vbCrLf & exportPdfPath,
                                "Reporte",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
        End If

        ps.Dispose()
    End Sub

    Private Function BuildDT_OcupacionDetalle(dtSource As DataTable) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Area", GetType(String))
        dt.Columns.Add("Código", GetType(String))
        dt.Columns.Add("Ocupadas", GetType(Integer))
        dt.Columns.Add("Vacías", GetType(Integer))
        dt.Columns.Add("Total", GetType(Integer))
        dt.Columns.Add("Porcentaje", GetType(String))

        If dtSource Is Nothing OrElse dtSource.Rows.Count = 0 Then Return dt

        Dim areas As DataTable = dtSource.DefaultView.ToTable(True, "Area")

        Dim totalO As Integer = 0
        Dim totalV As Integer = 0

        For Each ra As DataRow In areas.Rows
            Dim area As String = ra("Area").ToString()

            Dim oObj As Object = dtSource.Compute("SUM(Cantidad)", $"Area='{area.Replace("'", "''")}' AND Estado='Ocupadas'")
            Dim vObj As Object = dtSource.Compute("SUM(Cantidad)", $"Area='{area.Replace("'", "''")}' AND (Estado='Vacías' OR Estado='Vacias')")

            Dim ocupadas As Integer = If(IsDBNull(oObj), 0, CInt(oObj))
            Dim vacias As Integer = If(IsDBNull(vObj), 0, CInt(vObj))
            Dim total As Integer = ocupadas + vacias
            Dim pct As Integer = If(total = 0, 0, CInt(Math.Round((ocupadas * 100.0) / total, 0)))

            totalO += ocupadas
            totalV += vacias

            Dim codigo As String = ""
            If dtSource.Columns.Contains("Codigo") Then
                Dim rows = dtSource.Select($"Area='{area.Replace("'", "''")}'")
                If rows.Length > 0 AndAlso Not rows(0).IsNull("Codigo") Then
                    codigo = rows(0)("Codigo").ToString()
                End If
            End If

            dt.Rows.Add(area, codigo, ocupadas, vacias, total, pct.ToString() & "%")
        Next

        Dim totalG As Integer = totalO + totalV
        Dim pctG As Integer = If(totalG = 0, 0, CInt(Math.Round((totalO * 100.0) / totalG, 0)))

        dt.Rows.Add("TOTAL", "", totalO, totalV, totalG, pctG.ToString() & "%")
        Return dt
    End Function

    Private Function ControlToImage(ctrl As Windows.Forms.Control) As Image
        Dim w As Integer = Math.Max(1, ctrl.Width)
        Dim h As Integer = Math.Max(1, ctrl.Height)

        Dim bmp As New Bitmap(w, h)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.Clear(Color.White)
        End Using

        ctrl.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))
        Return bmp
    End Function

End Class