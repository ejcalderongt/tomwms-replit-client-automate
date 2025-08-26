Imports System.IO
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmAnalitica1

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Private DT As New DataTable("StockPorLote")

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Cargar_Datos()

        Try

#Region "Tipo_Producto por Tramo"

            Dim DT As New DataTable
            DT = clsAnalitica.Get_Distribucion_Por_Tramo_By_IdBodega(cmbBodega.EditValue)
            ChartControl1.Series.Clear()
            ChartControl1.DataSource = DT
            ChartControl1.SeriesDataMember = "Tipo"
            ChartControl1.SeriesTemplate.ArgumentDataMember = "Ubicacion_Tramo"
            ChartControl1.SeriesTemplate.ValueDataMembers.AddRange(New String() {"Cantidad"})
            ChartControl1.SeriesTemplate.View = New StackedBarSeriesView()

#End Region

#Region "Existencia por Codigos de  producto "

            ChartControl2.Series.Clear()
            Dim DT1 As New DataTable
            DT1 = clsAnalitica.Get_Resumen_Exitencia_Codigos_By_IdBodega(cmbBodega.EditValue)
            Dim s As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series("Test", ViewType.Pie)
            s.DataSource = DT1
            s.ArgumentDataMember = "Arg"
            s.ValueDataMembers.AddRange(New String() {"Val"})
            s.ValueScaleType = ScaleType.Numerical
            s.ArgumentScaleType = ScaleType.Qualitative
            ChartControl2.Series.Add(s)
            s.Label.TextPattern = "{A}: {V}"
            CType(s.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
            CType(s.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default
            ChartControl2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.Default

            'Dim myView As PieSeriesView = CType(s.View, PieSeriesView)
            '' Specify a data filter to explode points.
            'myView.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Value_1, DataFilterCondition.GreaterThanOrEqual, 9))
            'myView.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.NotEqual, "Others"))
            'myView.ExplodeMode = PieExplodeMode.UseFilters
            'myView.ExplodedDistancePercentage = 30
            'myView.RuntimeExploding = True

#End Region

#Region "Existencia Estado de producto "

            ChartControl3.Series.Clear()
            Dim DT3 As New DataTable
            DT3 = clsAnalitica.Get_Resumen_Exitencia_Estado_Producto_By_IdBodega(cmbBodega.EditValue)
            Dim series3 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series("Existencias_Por_Estado", ViewType.Pie)
            series3.DataSource = DT3
            series3.ArgumentDataMember = "Arg"
            series3.ValueDataMembers.AddRange(New String() {"Val"})
            series3.ValueScaleType = ScaleType.Numerical
            series3.ArgumentScaleType = ScaleType.Qualitative
            ChartControl3.Series.Add(series3)
            series3.Label.TextPattern = "{A}: {V}"
            CType(series3.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
            CType(series3.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default
            ChartControl3.Legend.Visibility = DevExpress.Utils.DefaultBoolean.Default
#End Region

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub frmAnalita1_Load(sender As Object, e As EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'DsetAnalitica.VW_Stock_Rep_20200112' table. You can move, or remove it, as needed.
        ' Me.VW_Stock_Rep_20200112TableAdapter.Fill(Me.DsetAnalitica.VW_Stock_Rep_20200112)

        Try

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

            Cargar_Datos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            'GridView1.OptionsPrint.ExpandAllDetails = True
            'GridView1.OptionsPrint.PrintDetails = True

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
            'printLink.Component = grdStockPorLote
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Detalle de existencias por estado de producto"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)
        Cargar_Datos()
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
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdExToExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExToExcel.ItemClick
        'Exportar_Grid_A_Excel(grdStockPorLote, "WMS_ExistenciasPorLote.xlsx")
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs)

        'Try

        '    GridView1.OptionsBehavior.Editable = False
        '    GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        '    GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        '    GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
        '    GridView1.OptionsSelection.EnableAppearanceHideSelection = True
        '    GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
        '    GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
        '    GridView1.Appearance.FocusedRow.ForeColor = Color.White
        '    GridView1.Appearance.SelectedRow.ForeColor = Color.White
        '    GridView1.Appearance.SelectedRow.Options.UseBackColor = True
        '    GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        'Catch ex As Exception
        '    XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End Try

    End Sub

    Private Sub RibbonControl_Click(sender As Object, e As EventArgs) Handles RibbonControl.Click

    End Sub

End Class