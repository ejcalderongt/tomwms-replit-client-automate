Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmDetalleSalidas_DiasPiso

    Public Property IsLoading As Boolean = True
    Public Property Skip_Saving_Layout As Boolean = False

    Private vNombreArchivoLayOutGrid As String = ""

    Public Sub New()
        InitializeComponent()
        IsLoading = False
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public listarDespacho As New List(Of clsBeVW_Despacho_Rep)

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum


    Private Sub frmDetalleSalidas_DiasPiso_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            'mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never


            vNombreArchivoLayOutGrid = "frmDetalleSalidas_DiasPiso_Load.xml"

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


    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub


    Private Sub Cargar()

        Skip_Saving_Layout = True

        Dim dt As DataTable

        Try

            dt = clsLnTrans_movimientos.Get_Movimientos_Dias_Piso(cmbBodega.EditValue, dtpFechaDel.Value, dtpFechaAl.Value)

            If cmbBodega.Text <> "" Then

                Grd.DataSource = Nothing

                If dt.Rows.Count > 0 Then

                    Grd.DataSource = dt
                    GridView.OptionsView.ShowFooter = True

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
            clsUiPrintHelper.PrintGridPreview(Grd, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
            Exit Sub
            GridView.OptionsPrint.ExpandAllDetails = True
            GridView.OptionsPrint.PrintDetails = True

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

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub
    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Salidas"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
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

                'mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

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
                'mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
            Else
                'mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView_Layout(sender As Object, e As EventArgs)
        Guardar_Layout()
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

    Private Sub cmbBodega_SelectedValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        Cargar()
        GridView.Focus()
    End Sub
End Class



