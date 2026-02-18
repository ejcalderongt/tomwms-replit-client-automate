Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmRptStockParametro

    Public listaStock As New List(Of clsBeVW_stock_res)

    Public Sub New()
        InitializeComponent()
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

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter = _
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdParametros
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

    Private Sub Cargar_Datos

        Try

            grdParametros.BeginUpdate()

            Restore_LayOut_Grid()

            ListarStock()

            ListarStockDetalle()

            grdParametros.EndUpdate()

            grdParametros.ForceInitialize()

            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

            GridView1.BestFitColumns()

            'HS20171312_0320PM: Ordena de Menor a Mayor el codigo 
            colCódigo.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

            Try

                GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Disponible_Presentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Código").VisibleIndex = 2
                GridView1.Columns("Código_Barra").VisibleIndex = 3

                GridView1.Columns("Disponible_Presentación").Caption = "Disponible Presentación"
                GridView1.Columns("Cantidad").Caption = "Disponible Cantidad UMBas"
                GridView1.Columns("Código_Barra").Caption = "Código Barra"
                GridView1.Columns("UM_Bas").Caption = "UMBas"
                GridView1.Columns("Fecha_Ingreso").Caption = "Fecha Ingreso"
                GridView1.Columns("Fecha_Vence").Caption = "Fecha Vence"
                GridView1.Columns("Licencia").Caption = "Licencia"

                GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

                GridView1.BestFitColumns(True)

            Catch ex As Exception

            End Try

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ListarStock()

        Dim BeUbicacionActual As New clsBeBodega_ubicacion

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Buscando registros...")

            listaStock = clsLnStock.Get_All_Stock_By_IdBodega_And_IdPropietarioBodega(cmbBodega.EditValue, cmbPropietario.EditValue).ToList()

            Parametros.Stock.Clear()

            For Each Obj As clsBeVW_stock_res In listaStock

                Dim lRow As DataRow = Parametros.Stock.NewRow

                If listaStock IsNot Nothing AndAlso listaStock.Count > 0 Then

                    BeUbicacionActual.IdUbicacion = Obj.IdUbicacion

                    BeUbicacionActual = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(BeUbicacionActual.IdUbicacion, cmbBodega.EditValue)

                    '#MECR27082025: Se agrega columna de talla y color
                    lRow.Item("Stock Id") = Obj.IdStock
                    lRow.Item("Código") = Obj.Codigo_Producto
                    lRow.Item("Propietario") = Obj.Propietario
                    lRow.Item("Producto") = Obj.Nombre_Producto
                    lRow.Item("Estado") = Obj.NomEstado
                    lRow.Item("Licencia") = Obj.Lic_plate
                    lRow.Item("Lote") = Obj.Lote
                    lRow.Item("Serial") = Obj.Serial
                    lRow.Item("Presentación") = Obj.Nombre_Presentacion
                    lRow.Item("Disponible_Presentación") = Obj.CantidadPresentacion
                    lRow.Item("Cantidad") = Obj.CantidadUmBas
                    lRow.Item("Código_Barra") = Obj.Codigo_Barra
                    lRow.Item("UM_Bas") = Obj.UMBas
                    lRow.Item("Fecha_Ingreso") = Obj.Fecha_ingreso
                    lRow.Item("Fecha_Vence") = Obj.Fecha_Vence
                    lRow.Item("Recepción") = Obj.IdRecepcionEnc
                    lRow.Item("Ubicación") = BeUbicacionActual.NombreCompleto
                    lRow.Item("Talla") = Obj.Codigo_Talla
                    lRow.Item("Color") = Obj.Codigo_Color

                End If

                Parametros.Stock.AddStockRow(lRow)

            Next

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub ListarStockDetalle()

        Dim BeUbicacionActual As New clsBeBodega_ubicacion

        Try


            listaStock = clsLnStock.Get_All_Stock_Detalle_Parametros()

            Parametros.Stock_parametro.Clear()

            For Each Obj As clsBeVW_stock_res In listaStock

                Dim lRow As DataRow = Parametros.Stock_parametro.NewRow

                If listaStock IsNot Nothing AndAlso listaStock.Count > 0 Then

                    'BeUbicacionActual.IdUbicacion = Obj.IdUbicacion

                    'BeUbicacionActual = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(BeUbicacionActual.IdUbicacion)

                    lRow.Item("StockId") = Obj.IdStock
                    lRow.Item("Valor_Texto") = Obj.ValorTexto
                    lRow.Item("Valor_Numerico") = Obj.ValorNumerico
                    lRow.Item("Valor_Fecha") = Obj.ValorFecha
                    lRow.Item("Valor_Logico") = Obj.ValorLogico
                End If

                Parametros.Stock_parametro.AddStock_parametroRow(lRow)

            Next

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

        Dim reportHeader As String = vbNewLine & "Reporte de Stock"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Imprimir_Vista()
    End Sub


    Private Sub GridView2_RowStyle(sender As Object, e As RowStyleEventArgs)

        Try

            GridView2.OptionsBehavior.Editable = False
            GridView2.OptionsSelection.EnableAppearanceFocusedCell = False

            GridView2.FocusRectStyle = DrawFocusRectStyle.RowFocus

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

    Private Sub btnImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub btnActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub btnSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSalir.ItemClick
        Close()
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
        Cargar_Datos()
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

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

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

    Private Sub frmRptStockParametro_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            '#EJC20210716:Restaurar LayoutGrid en stockParametro.
            'vNombreArchivoLayOutGrid = grdParametros.Name & ".xml"
            vNombreArchivoLayOutGrid = "frmRptStockParametro.xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

            Cargar_Datos()

        Catch ex As Exception
            XtraMessageBox.Show("Error: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Guardar_Layout()

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

    Private Sub Restore_LayOut_Grid()

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

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout
        Guardar_Layout()
    End Sub


End Class