Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmRptStock

    Public listaStock As New List(Of clsBeVW_stock_res)
    Public pListObjDet As New List(Of clsBeTrans_ubic_hh_det)
    Private DTStock As New DataTable
    Private DTStockDet As New DataTable

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Listar_Stock()

        Dim BeUbicacionActual As New clsBeBodega_ubicacion
        Dim vDisponiblePresentacion As Double = 0.00

        Try

            'grdSeries.BeginUpdate()

            DTStock = clsLnStock.Get_Reporte_Stock_All_DataTable(cmbBodega.EditValue,
                                                                 cmbPropietario.EditValue,
                                                                  DTStockDet)

            Series.Stock.Clear()

            If DTStock IsNot Nothing AndAlso DTStock.Rows.Count > 0 Then

                For Each Obj As DataRow In DTStock.Rows

                    Dim lRow As DataRow = Series.Stock.NewRow

                    lRow.Item("Stock Id") = Obj.Item("IdStock")
                    lRow.Item("Código") = Obj.Item("Codigo")
                    lRow.Item("Propietario") = Obj.Item("Propietario")
                    lRow.Item("Producto") = Obj.Item("Nombre")
                    lRow.Item("Estado") = Obj.Item("NomEstado")
                    lRow.Item("Lote") = Obj.Item("Lote")
                    lRow.Item("Serial") = IIf(IsDBNull(Obj.Item("Serial")), "", Obj.Item("Serial"))
                    lRow.Item("Presentación") = IIf(IsDBNull(Obj.Item("Presentacion")), "", Obj.Item("Presentacion"))
                    lRow.Item("Cant_Presentación") = IIf(IsDBNull(Obj.Item("Cantidad")), "0", Obj.Item("Cantidad"))
                    lRow.Item("Cantidad") = IIf(IsDBNull(Obj.Item("CantidadSF")), "0", Obj.Item("CantidadSF"))
                    lRow.Item("Código_Barra") = IIf(IsDBNull(Obj.Item("Codigo_Barra")), "", Obj.Item("Codigo_Barra"))
                    lRow.Item("UM_Bas") = IIf(IsDBNull(Obj.Item("UnidadMedida")), "", Obj.Item("UnidadMedida"))
                    lRow.Item("Fecha_Ingreso") = IIf(IsDBNull(Obj.Item("Fecha_Ingreso")), "01/01/1900", Obj.Item("Fecha_Ingreso"))
                    lRow.Item("Fecha_Vence") = IIf(IsDBNull(Obj.Item("Fecha_Vence")), "01/01/1900", Obj.Item("Fecha_Vence"))
                    lRow.Item("Recepción") = IIf(IsDBNull(Obj.Item("IdRecepcionEnc")), "0", Obj.Item("IdRecepcionEnc"))
                    lRow.Item("Ubicación") = IIf(IsDBNull(Obj.Item("Nombre_Completo")), "ND?", Obj.Item("Nombre_Completo"))
                    lRow.Item("codigo_poliza") = Obj.Item("codigo_poliza")
                    lRow.Item("Numero_orden") = Obj.Item("Numero_poliza")
                    Series.Stock.AddStockRow(lRow)

                Next

            End If

            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

            GridView1.BestFitColumns()

            'HS20171312_0320PM: Ordena de Menor a Mayor el codigo 
            colCódigo.SortOrder = ColumnSortOrder.Ascending

            Try

                GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Cantidad").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                GridView1.Columns("Cant_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cant_Presentación").DisplayFormat.FormatString = "{0:n6}"
                GridView1.Columns("Cant_Presentación").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView1.Columns("Cant_Presentación").SummaryItem.DisplayFormat = "Sum = {0:n6}"

                GridView1.Columns("Código").VisibleIndex = 2
                GridView1.Columns("Código_Barra").VisibleIndex = 3

                GridView1.Columns("Código_Barra").Caption = "Código Barra"
                GridView1.Columns("UM_Bas").Caption = "UMBas"
                GridView1.Columns("Fecha_Ingreso").Caption = "Fecha Ingreso"
                GridView1.Columns("Fecha_Vence").Caption = "Fecha Vence"
                GridView1.Columns("Cant_Presentación").Caption = "Disponible Presentación"
                GridView1.Columns("Cantidad").Caption = "Disponible UMBas"

                GridView1.BestFitColumns(True)

            Catch ex As Exception

            End Try

            Listar_Stock_Detalle()

            Restore_LayOut()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Stock_Detalle()

        Try

            If DTStockDet IsNot Nothing AndAlso DTStockDet.Rows.Count > 0 Then

                For Each Obj As DataRow In DTStockDet.Rows

                    Dim lRow As DataRow = Series.stock_se.NewRow
                    lRow.Item("Stock Id") = Obj.Item("IdStock")
                    lRow.Item("No_Serie") = Obj.Item("No_Serie")
                    lRow.Item("No_Serie_Inicial") = Obj.Item("No_Serie_Inicial")
                    lRow.Item("No_Serie_Final") = Obj.Item("No_Serie_Final")
                    Series.stock_se.Addstock_seRow(lRow)

                Next

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

    Private Sub CardView1_CustomDrawCardFieldCaption(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs)
        If e.Column.VisibleIndex < 3 Then
            e.Handled = True
        ElseIf e.Column.VisibleIndex = 3 Then
            e.Column.Visible = False
        End If
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
            printLink.Component = grdSeries
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

        Dim reportHeader As String = vbNewLine & "Reporte de Stock"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub


    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs)

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

    Private Sub GridView2_RowStyle(sender As Object, e As RowStyleEventArgs)

        Try

            GridView2.OptionsBehavior.Editable = False
            GridView2.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridView2.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView2.OptionsSelection.EnableAppearanceHideSelection = True
            GridView2.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView2.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridView2.Appearance.FocusedRow.ForeColor = Color.White
            GridView2.Appearance.SelectedRow.ForeColor = Color.White

            GridView2.Appearance.SelectedRow.Options.UseBackColor = True
            GridView2.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub btnImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub btnActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnActualizar.ItemClick
        Listar_Stock()
    End Sub

    Private Sub btnSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSalir.ItemClick
        Close()
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

            Listar_Stock()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = ""
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

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

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            'If File.Exists(vNombreArchivoLayOutGrid) Then
            '    File.Delete(vNombreArchivoLayOutGrid)
            '    mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            'End If

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid)


            XtraMessageBox.Show("Diseño de grid eliminado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub frmRptStock_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            '#EJC20210716:Restaurar LayoutGrid en stockParametro.            
            'vNombreArchivoLayOutGrid = CurDir() & "\" & grdSeries.Name & ".xml"
            vNombreArchivoLayOutGrid = "frmRptSeries.xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

        Catch ex As Exception
            XtraMessageBox.Show("Error: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Restore_LayOut()

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

End Class