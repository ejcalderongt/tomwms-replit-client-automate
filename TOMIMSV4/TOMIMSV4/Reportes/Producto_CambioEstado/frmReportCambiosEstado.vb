Imports System.IO
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class frmReportCambiosEstado

    Public Sub New()
        InitializeComponent()
    End Sub


    Public Property Modo As pModo
    Private DT As New DataTable("ExistenciasPorEstado")
    Public Property ProductoEspecifico As New clsBeProducto
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub SetDatataTable()

        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Tarea", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Origen", GetType(String))
        DT.Columns.Add("Destino", GetType(String))
        DT.Columns.Add("Cantidad", GetType(Double))
        DT.Columns.Add("Peso", GetType(Double))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("Vence", GetType(Date))
        DT.Columns.Add("Estado", GetType(String))
        DT.Columns.Add("Motivo", GetType(String))
        DT.Columns.Add("Fecha", GetType(DateTime))
        DT.Columns.Add("Poliza", GetType(String))

    End Sub




    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try

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
            printLink.Component = grdCambiosEstado
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

        Dim reportHeader As String = vbNewLine & "Movimientos de Estado"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdExToExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExToExcel.ItemClick
        Exportar_Grid_A_Excel(grdCambiosEstado, "Cambios_Estado.xlsx")
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
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub frmReportCambiosEstado_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            SetDatataTable()

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

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

    Private Sub Cargar()

        Try

            If DT Is Nothing Then
                DT = New DataTable("ExistenciasPorEstado")
                SetDatataTable()
            Else
                DT.Clear()
            End If

            Dim IdProductoBodega As Integer = 0

            If txtIdProducto.Text.Trim <> "" AndAlso Not ProductoEspecifico Is Nothing Then
                IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(ProductoEspecifico.IdProducto, cmbBodega.EditValue)
            End If

            DT = clsLnTrans_movimientos.Get_All_Movimientos_Cambio_Estado_DT(dtpFechaDel.Value,
                                                                             dtpFechaAl.Value,
                                                                             IdProductoBodega,
                                                                             cmbBodega.EditValue,
                                                                             cmbPropietario.EditValue)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    grdCambiosEstado.DataSource = DT

                    If GridView1.Columns.Count > 0 Then

                        GridView1.OptionsView.ShowFooter = True

                        GridView1.Columns("Propietario").Group()

                        GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Fecha").DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"

                        GridView1.Columns("IdPresentacion").Visible = False
                        GridView1.Columns("Presentacion").Visible = False
                        GridView1.Columns("Factor").Visible = False
                        GridView1.Columns("IdProductoBodega").Visible = False
                        GridView1.Columns("IdPropietarioBodega").Visible = False
                        GridView1.Columns("IdBodegaOrigen").Visible = False

                        GridView1.ExpandAllGroups()
                        GridView1.BestFitColumns(True)

                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    End If

                End If

            End If


            Dim DT1 As New DataTable

            DT1 = clsLnTrans_movimientos.Get_Reporte_Cant_Movimientos_Estado_By_Operador_And_Producto(dtpFechaDel.Value,
                                                                                                      dtpFechaAl.Value,
                                                                                                      IdProductoBodega,
                                                                                                      cmbBodega.EditValue,
                                                                                                      cmbPropietario.EditValue)

            If Not DT1 Is Nothing Then

                If DT1.Rows.Count > 0 Then

                    dgridResumen.DataSource = DT1

                    If GridView2.Columns.Count > 0 Then

                        GridView2.OptionsView.ShowFooter = True

                        'GridView2.Columns("Tipo").Group()

                        'GridView2.Columns("CantidadCambiosUBIC").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        'GridView2.Columns("CantidadCambiosUBIC").DisplayFormat.FormatString = "{0:n2}"
                        'GridView2.Columns("CantidadCambiosUBIC").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        'GridView2.Columns("CantidadCambiosUBIC").SummaryItem.DisplayFormat = "{0:n2}"

                        GridView2.Columns("CantidadCambiosCEST").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView2.Columns("CantidadCambiosCEST").DisplayFormat.FormatString = "{0:n2}"
                        GridView2.Columns("CantidadCambiosCEST").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView2.Columns("CantidadCambiosCEST").SummaryItem.DisplayFormat = "{0:n2}"

                        GridView2.Columns("FechaOperador").Visible = False

                        GridView2.ExpandAllGroups()

                        GridView2.BestFitColumns(True)

                        lblRegs.Caption = String.Format("Registros: {0}", GridView2.RowCount)

                    End If

                    ConfigureChangeChart(ChartControl1, DT1)

                End If

            Else
                dgridResumen.DataSource = Nothing
                ChartControl1.DataSource = Nothing
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

    Private Sub ConfigureChangeChart(ChartControl1 As ChartControl, dataTable As DataTable)

        Try

            ChartControl1.Series.Clear()
            ChartControl1.Titles.Clear()

            ' Crear una serie para cada tipo de tarea ('UBIC' y 'CEST')
            'Dim seriesUBIC As New DevExpress.XtraCharts.Series("Cambios de Ubicación", ViewType.Bar)
            'seriesUBIC.ArgumentDataMember = "FechaOperador"
            'seriesUBIC.ValueDataMembers.AddRange("CantidadCambiosUBIC")
            'seriesUBIC.DataSource = dataTable
            'seriesUBIC.View.Color = System.Drawing.Color.Blue

            Dim seriesCEST As New DevExpress.XtraCharts.Series("Cambios de Estado", ViewType.Bar)
            seriesCEST.ArgumentDataMember = "FechaOperador"
            seriesCEST.ValueDataMembers.AddRange("CantidadCambiosCEST")
            seriesCEST.DataSource = dataTable
            seriesCEST.View.Color = System.Drawing.Color.Green

            ' Agregar las series al control de gráfico
            'ChartControl1.Series.Add(seriesUBIC)
            ChartControl1.Series.Add(seriesCEST)

            '' Configuración del diagrama (ejes)
            Dim diagram As XYDiagram = CType(ChartControl1.Diagram, XYDiagram)
            diagram.AxisX.Title.Text = "Fecha y Operador"
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Title.Text = "Cantidad de Cambios"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

            ' Configuración del título del gráfico
            Dim title As New ChartTitle()
            title.Text = "Análisis de Cambios por Fecha y Operador"
            ChartControl1.Titles.Add(title)

            '' Formatear las etiquetas del eje X para que muestren la fecha y el nombre del operador
            CType(diagram, XYDiagram).AxisX.Label.Angle = 45
            CType(diagram, XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowRotate = True
            CType(diagram, XYDiagram).AxisX.Label.ResolveOverlappingOptions.AllowStagger = True

        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try
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
End Class