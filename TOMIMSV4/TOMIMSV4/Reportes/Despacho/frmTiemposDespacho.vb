Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmTiemposDespacho

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Cargar_Datos()

        LoadingData = True

        Try

            Dim DT As New DataTable

            DT.Clear()

            dgridTiemposPedido.DataSource = Nothing

            DT = clsLnTrans_despacho_det.Get_Reporte_Tiempos_Despacho_By_Fechas(dtpFechaDesde.Value, dtpfechaHasta.Value)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    dgridTiemposPedido.DataSource = DT

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.OptionsView.ShowFooter = True

                    'GridView1.Columns("cantidad_solicitada").SummaryItem.SummaryType = SummaryItemType.Sum
                    'GridView1.Columns("cantidad_solicitada").SummaryItem.DisplayFormat = "{0:n6}"
                    'GridView1.Columns("cantidad_solicitada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'GridView1.Columns("cantidad_solicitada").DisplayFormat.FormatString = "{0:n6}"

                    'GridView1.Columns("cantidad_recibida").SummaryItem.SummaryType = SummaryItemType.Sum
                    'GridView1.Columns("cantidad_recibida").SummaryItem.DisplayFormat = "{0:n6}"
                    'GridView1.Columns("cantidad_recibida").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'GridView1.Columns("cantidad_recibida").DisplayFormat.FormatString = "{0:n6}"

                    'GridView1.Columns("cantidad_verificada").SummaryItem.SummaryType = SummaryItemType.Sum
                    'GridView1.Columns("cantidad_verificada").SummaryItem.DisplayFormat = "{0:n6}"
                    'GridView1.Columns("cantidad_verificada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'GridView1.Columns("cantidad_verificada").DisplayFormat.FormatString = "{0:n6}"

                    'GridView1.Columns("cantidad_despachada").SummaryItem.SummaryType = SummaryItemType.Sum
                    'GridView1.Columns("cantidad_despachada").SummaryItem.DisplayFormat = "{0:n6}"
                    'GridView1.Columns("cantidad_despachada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    'GridView1.Columns("cantidad_despachada").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Fecha_Pedido").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView1.Columns("Fecha_Pedido").DisplayFormat.FormatString = "G"

                    GridView1.Columns("Fecha_Despacho").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView1.Columns("Fecha_Despacho").DisplayFormat.FormatString = "G"

                    'GridView1.Columns("Fecha_Verificacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    'GridView1.Columns("Fecha_Verificacion").DisplayFormat.FormatString = "G"

                    'Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    '    With {.FieldName = "cantidad_solicitada", .SummaryType = SummaryItemType.Sum,
                    '    .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("cantidad_solicitada")}
                    'GridView1.GroupSummary.Add(item)

                    'Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    '    With {.FieldName = "cantidad_recibida", .SummaryType = SummaryItemType.Sum,
                    '    .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("cantidad_recibida")}
                    'GridView1.GroupSummary.Add(item1)

                    'Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    '    With {.FieldName = "cantidad_verificada", .SummaryType = SummaryItemType.Sum,
                    '    .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("cantidad_verificada")}
                    'GridView1.GroupSummary.Add(item2)

                    'Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                    '    With {.FieldName = "cantidad_despachada", .SummaryType = SummaryItemType.Sum,
                    '    .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("cantidad_despachada")}
                    'GridView1.GroupSummary.Add(item3)

                    'GridView1.ExpandAllGroups()

                    Set_LayOut_Grid()

                    If Not ExisteLayout Then
                        GridView1.BestFitColumns()
                    End If

                End If

            End If

            ConfigureDespachoChart(ChartControl1, DT)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            LoadingData = False
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
            printLink.Component = GridView1
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

        Dim reportHeader As String = vbNewLine & "Detalle de existencias por estado de producto"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

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

    Private Sub cmdExToExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExToExcel.ItemClick
        Exportar_Grid_A_Excel(dgridTiemposPedido, "ProgresoPicking.xlsx")
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs)

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus
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

    '#EJC20210716:Guardar LayoutGrid.
    Private vNombreArchivoLayOutGrid As String = "frmTiemposDespacho"

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid)


            XtraMessageBox.Show("Diseño de grid eliminado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmProgresoPicking_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Inicializando estructuras...")

            vNombreArchivoLayOutGrid = dgridTiemposPedido.Name & ".xml"

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private ExisteLayout As Boolean = False
    Private Sub Set_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

        Try


            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

            XtraMessageBox.Show("Diseño de grid guardado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private LoadingData As Boolean = False
    Private Sub GridView1_Layout(sender As Object, e As EventArgs)

        Try

            If LoadingData Then Exit Sub

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try


    End Sub

    Private Sub ConfigureDespachoChart(chartControl As ChartControl, dataTable As DataTable)

        Try
            ' Limpiar cualquier serie o título existente en el control de gráfico
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            ' Crear la serie de barras para el Tiempo Total en Horas
            Dim series1 As New DevExpress.XtraCharts.Series("Tiempo Total (Horas)", ViewType.Bar)
            series1.ArgumentDataMember = "IdPedidoEnc" ' Utilizar el Id del Pedido como argumento (eje X)
            series1.ValueDataMembers.AddRange("Horas") ' Utilizar la columna "Horas" para los valores (eje Y)
            series1.DataSource = dataTable

            ' Agregar la serie al control de gráfico
            chartControl.Series.Add(series1)

            ' Configuración del diagrama (ejes)
            Dim diagram As XYDiagram = CType(chartControl.Diagram, XYDiagram)
            diagram.AxisX.Title.Text = "Id Pedido"
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Title.Text = "Tiempo Total (Horas)"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

            ' Configuración del título del gráfico
            Dim title As New ChartTitle()
            title.Text = "Análisis de Tiempos de Despacho por Pedido"
            chartControl.Titles.Add(title)

        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

End Class


