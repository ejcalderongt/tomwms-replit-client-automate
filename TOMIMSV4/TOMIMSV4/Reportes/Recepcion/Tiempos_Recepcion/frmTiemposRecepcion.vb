Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Public Class frmTiemposRecepcion

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private rand As New Random()

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

            DT = clsLnTrans_re_enc.Generar_Reporte_Tiempos_Recepcion(cmbBodega.EditValue,
                                                                     cmbPropietarioBodega.EditValue,
                                                                     dtpFechaDel.Value,
                                                                     dtpFechaAl.Value)

            If DT.Rows.Count = 0 Then
                lblEstatus.Text = "0 registros encontrados"
                lblEstatus.Refresh()
            Else
                lblEstatus.Text = DT.Rows.Count & " registros encontrados "
                lblEstatus.Refresh()
            End If

            Dgrid.DataSource = DT

            If GridView1.RowCount > 0 Then

                GridView1.Columns(0).GroupIndex = 0

                GridView1.OptionsBehavior.AutoExpandAllGroups = True

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("NumeroRecepciones").DisplayFormat.FormatType = FormatType.Numeric
                GridView1.Columns("NumeroRecepciones").DisplayFormat.FormatString = "{0:n2}"
                GridView1.Columns("NumeroRecepciones").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("NumeroRecepciones").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("TiempoPromedioMinutos").DisplayFormat.FormatType = FormatType.Numeric
                GridView1.Columns("TiempoPromedioMinutos").DisplayFormat.FormatString = "{0:n2}"
                GridView1.Columns("TiempoPromedioMinutos").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
                GridView1.Columns("TiempoPromedioMinutos").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("TiempoMinimoMinutos").DisplayFormat.FormatType = FormatType.Numeric
                GridView1.Columns("TiempoMinimoMinutos").DisplayFormat.FormatString = "{0:n2}"
                GridView1.Columns("TiempoMinimoMinutos").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Min
                GridView1.Columns("TiempoMinimoMinutos").SummaryItem.DisplayFormat = "{0:n2}"

                GridView1.Columns("TiempoMaximoMinutos").DisplayFormat.FormatType = FormatType.Numeric
                GridView1.Columns("TiempoMaximoMinutos").DisplayFormat.FormatString = "{0:n2}"
                GridView1.Columns("TiempoMaximoMinutos").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max
                GridView1.Columns("TiempoMaximoMinutos").SummaryItem.DisplayFormat = "{0:n2}"

                'GridView1.Columns("Tolerancia_dias").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'GridView1.Columns("Tolerancia_dias").DisplayFormat.FormatString = "{0:n2}"

            End If

            ' Configurar el gráfico
            ConfigureChart(ChartControl1, DT)

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

    Private Sub frmTiemposRecepcion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
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

    Private Sub ConfigureChart(chartControl As ChartControl, dataTable As DataTable)
        Try
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            ' Crear una serie para cada tipo de tiempo (Promedio, Mínimo, Máximo)
            Dim seriesPromedio As New DevExpress.XtraCharts.Series("Tiempo Promedio", ViewType.Line)
            Dim seriesMinimo As New DevExpress.XtraCharts.Series("Tiempo Mínimo", ViewType.Line)
            Dim seriesMaximo As New DevExpress.XtraCharts.Series("Tiempo Máximo", ViewType.Line)

            ' Configurar las series con los datos del DataTable
            seriesPromedio.DataSource = dataTable
            seriesPromedio.ArgumentScaleType = ScaleType.DateTime
            seriesPromedio.ValueScaleType = ScaleType.Numerical
            seriesPromedio.ArgumentDataMember = "FechaRecepcion"
            seriesPromedio.ValueDataMembers.AddRange(New String() {"TiempoPromedioMinutos"})
            CType(seriesPromedio.View, LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True

            seriesMinimo.DataSource = dataTable
            seriesMinimo.ArgumentScaleType = ScaleType.DateTime
            seriesMinimo.ValueScaleType = ScaleType.Numerical
            seriesMinimo.ArgumentDataMember = "FechaRecepcion"
            seriesMinimo.ValueDataMembers.AddRange(New String() {"TiempoMinimoMinutos"})
            CType(seriesMinimo.View, LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True

            seriesMaximo.DataSource = dataTable
            seriesMaximo.ArgumentScaleType = ScaleType.DateTime
            seriesMaximo.ValueScaleType = ScaleType.Numerical
            seriesMaximo.ArgumentDataMember = "FechaRecepcion"
            seriesMaximo.ValueDataMembers.AddRange(New String() {"TiempoMaximoMinutos"})
            CType(seriesMaximo.View, LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True

            ' Agregar las series al gráfico
            chartControl.Series.Add(seriesPromedio)
            chartControl.Series.Add(seriesMinimo)
            chartControl.Series.Add(seriesMaximo)

            ' Configurar los ejes
            Dim diagram As XYDiagram = CType(chartControl.Diagram, XYDiagram)

            ' Eje X (Fechas)
            diagram.AxisX.Title.Text = "Fecha de Recepción"
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisX.Label.TextPattern = "{A:dd/MM/yyyy}"
            diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.None
            diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day
            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day

            ' Eje Y (Tiempo en Minutos)
            diagram.AxisY.Title.Text = "Tiempo de Recepción (Minutos)"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Label.TextPattern = "{V} minutos"

            ' Configurar el título del gráfico
            chartControl.Titles.Add(New ChartTitle())
            chartControl.Titles(0).Text = "Tiempos de Recepción por Fecha"

            ' Opcional: Configurar la leyenda
            chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
            chartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center
            chartControl.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside
            chartControl.Legend.Direction = LegendDirection.LeftToRight

        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Public Function GetRandomPaleColor() As Color
        ' Generar componentes RGB altos para colores pálidos
        Dim red As Integer = rand.Next(128, 256)
        Dim green As Integer = rand.Next(128, 256)
        Dim blue As Integer = rand.Next(128, 256)

        Return Color.FromArgb(red, green, blue)
    End Function

End Class




