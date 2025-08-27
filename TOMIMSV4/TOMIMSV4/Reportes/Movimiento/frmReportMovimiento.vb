Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmReportMovimiento

    Public pBeProducto As New clsBeProducto

    '#EJC20210716:Guardar LayoutGrid en rptMovimientos.
    Private vNombreArchivoLayOutGrid As String = ""

    Public Property Modo As pModo
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public pMovimiento As Boolean
    Public pIdProducto As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Public Property Skip_Saving_Layout As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub Cargar()

        Skip_Saving_Layout = True

        Dim dt As DataTable

        Try

            If Modo = pModo.Seleccion Then
                dt = clsLnTrans_movimientos.Get_All_Movimientos_By_Producto(cmbBodega.EditValue, dtpFechaDel.Value, dtpFechaAl.Value, pIdProducto)
            Else
                dt = clsLnTrans_movimientos.Get_Movimientos(cmbBodega.EditValue, dtpFechaDel.Value, dtpFechaAl.Value)
            End If

            If cmbBodega.Text <> "" Then

                Grd.DataSource = Nothing

                If dt.Rows.Count > 0 Then

                    Grd.DataSource = dt

                    'GridView.Columns(0).GroupIndex = 0
                    GridView.OptionsView.ShowFooter = True
                    'GridView.OptionsBehavior.AutoExpandAllGroups = True

                    GridView.Columns("cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView.Columns("cantidad").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView.Columns("cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"

                    GridView.Columns("Cantidad_Presentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView.Columns("Cantidad_Presentacion").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView.Columns("Cantidad_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView.Columns("Cantidad_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                    GridView.Columns("peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView.Columns("peso").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView.Columns("peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView.Columns("peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView.Columns("fecha").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView.Columns("fecha").DisplayFormat.FormatString = "G"

                    GridView.Columns("IdBodegaOrigen").Visible = False

                    GridView.Columns("codigo_barra").Caption = "Código Barra"
                    GridView.Columns("codigo").Caption = "Código"
                    GridView.Columns("cantidad").Caption = "Cantidad U.M.Bas"
                    GridView.Columns("peso").Caption = "Peso"
                    GridView.Columns("lote").Caption = "Lote"
                    GridView.Columns("fecha").Caption = "Fecha_Movimiento"
                    GridView.Columns("fecha_vence").Caption = "Fecha Vence"
                    GridView.Columns("barra_pallet").Caption = "Licencia"
                    GridView.Columns("Cantidad_Presentacion").Caption = "Cantidad Presentación"

                    GridView.Columns("Propietario").VisibleIndex = 0
                    GridView.Columns("Poliza").VisibleIndex = 1
                    GridView.Columns("Producto").VisibleIndex = 2
                    GridView.Columns("codigo").VisibleIndex = 3
                    GridView.Columns("codigo_barra").VisibleIndex = 4
                    GridView.Columns("cantidad").VisibleIndex = 5
                    GridView.Columns("Unidad de Medida").VisibleIndex = 6
                    GridView.Columns("Cantidad_Presentacion").VisibleIndex = 8
                    GridView.Columns("Presentación").VisibleIndex = 9
                    GridView.Columns("peso").VisibleIndex = 10
                    GridView.Columns("lote").VisibleIndex = 11
                    GridView.Columns("fecha_vence").VisibleIndex = 12
                    GridView.Columns("barra_pallet").VisibleIndex = 13
                    GridView.Columns("fecha").VisibleIndex = 14
                    GridView.Columns("Estado Origen").VisibleIndex = 15
                    GridView.Columns("Estado Destino").VisibleIndex = 16
                    GridView.Columns("IdProducto").Visible = False

                End If

            End If

            Restore_LayOut_Grid()

            If GridView.Columns.Count > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView.RowCount)
                GridView.BestFitColumns(True)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            Skip_Saving_Layout = False
        End Try

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

        Dim reportHeader As String = vbNewLine & "Listado de Movimientos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmbBodega_SelectedValueChanged(sender As Object, e As EventArgs)

        Cargar()
        GridView.Focus()

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        'Try

        '    'Cargar()

        '    'GridView.Focus()

        'Catch ex As Exception

        '    XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        '    Text,
        '    MessageBoxButtons.OK,
        '    MessageBoxIcon.Error)

        '    Dim vMsgError As String = ex.Message
        '    clsLnLog_error_wms.Agregar_Error(vMsgError)

        'End Try

    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        'Try

        '    Cargar()

        '    GridView.Focus()

        'Catch ex As Exception

        '    XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        '    Text,
        '    MessageBoxButtons.OK,
        '    MessageBoxIcon.Error)

        '    Dim vMsgError As String = ex.Message
        '    clsLnLog_error_wms.Agregar_Error(vMsgError)

        'End Try

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

    Private Sub frmReportMovimiento_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never


            vNombreArchivoLayOutGrid = "rptgridMovimientos.xml"

            '#CKFK 20190129 Agregué que se inicie en la bodega con la que se entró a la aplicación
            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

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

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        Cargar()
    End Sub

    Private Sub Guardar_Layout()

        Try

            If Not Skip_Saving_Layout Then

                Dim Ms As New MemoryStream
                GridView.SaveLayoutToStream(Ms)
                Ms.Seek(0, SeekOrigin.Begin)
                Dim MsReader As New StreamReader(Ms)
                Dim LayoutToText As String = MsReader.ReadToEnd()

                clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                              AP.UsuarioAp.IdUsuario,
                                                              AP.HostName,
                                                              vNombreArchivoLayOutGrid,
                                                              LayoutToText)

                GridView.SaveLayoutToXml(vNombreArchivoLayOutGrid)

                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

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

    Private Sub Restore_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView_Layout(sender As Object, e As EventArgs) Handles GridView.Layout
        Guardar_Layout()
    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

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

    Private Sub cmdExToExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExToExcel.ItemClick
        Exportar_Grid_A_Excel(Grd, "WMS_ReporteMovimientos.xlsx")
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

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

        Try

            Dim Ms As New MemoryStream
            GridView.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView.DataRowCount.ToString()))
    End Sub

End Class