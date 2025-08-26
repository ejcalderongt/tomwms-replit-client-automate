Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmStockPorLote_Posicion

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private DT As New DataTable("StockPorLotePosicion")

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar_Datos()
    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)
        Cargar_Datos()
    End Sub

    Private IsLoading As Boolean = False
    Private Sub Cargar_Datos()

        IsLoading = True

        Try

            Dim DT As New DataTable

            grdStockPorLote.DataSource = Nothing

            Dim vIdBodega = Integer.Parse(cmbBodega.EditValue)
            Dim vIdPropietarioBodega = Integer.Parse(cmbPropietarioBodega.EditValue)

            DT = clsLnStock.Get_Reporte_Stock_y_Posiciones(vIdBodega, vIdPropietarioBodega)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    grdStockPorLote.DataSource = DT

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                    GridView1.OptionsView.ShowFooter = True

                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("CantidadUMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("CantidadPresentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Disponible_Presentación").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

                    GridView1.Columns("Posiciones").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Posiciones").SummaryItem.DisplayFormat = "{0:n2}"
                    GridView1.Columns("Posiciones").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Posiciones").DisplayFormat.FormatString = "{0:n2}"

                    Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Reservada_UMBas", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_Reservada")}
                    GridView1.GroupSummary.Add(item)

                    Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_UMBas", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_UMBas")}
                    GridView1.GroupSummary.Add(item1)

                    Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Presentacion", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_Presentacion")}
                    GridView1.GroupSummary.Add(item2)

                    Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_UMBas", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Disponible_UMBas")}
                    GridView1.GroupSummary.Add(item3)

                    Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_Presentación", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Disponible_Presentación")}
                    GridView1.GroupSummary.Add(item4)

                    Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Posiciones", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n2}", .ShowInGroupColumnFooter = GridView1.Columns("Posiciones")}
                    GridView1.GroupSummary.Add(item5)

                    GridView1.Columns("IdPresentacion").Visible = False
                    GridView1.Columns("IdStock").Visible = False

                    GridView1.ExpandAllGroups()

                    If GridView1.Columns.Count > 0 Then GridView1.BestFitColumns()

                End If

                IsLoading = False

                'GT14032022: aqui se restaura el layout.
                Restore_LayOut()

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
            printLink.Component = grdStockPorLote
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

        Dim reportHeader As String = vbNewLine & "Detalle de existencias por lote y posición"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub cmdExToExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExToExcel.ItemClick
        Exportar_Grid_A_Excel(grdStockPorLote, "WMS_ExistenciasPorLotePosicion.xlsx")
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

    Private Sub cmdPrintLP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdPrintLP.ItemClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lp As String = ""
                Dim Cod As String = ""
                Dim Nombre As String = ""
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                Dim pd As PrintDialog = New PrintDialog()
                pd.PrinterSettings = New PrinterSettings()

                lp = IIf(IsDBNull(Dr.Item("lic_plate")), "", Dr.Item("lic_plate"))
                Cod = Dr.Item("Codigo")
                Nombre = Dr.Item("Producto")

                If lp <> "" Then
                    SplashScreenManager.Default.SetWaitFormCaption("Imprimiendo licencia de pallet")
                    Imprimir_Etiqueta(lp, Cod, Nombre, pd.PrinterSettings.PrinterName)
                    SplashScreenManager.CloseForm(False)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Imprimir_Etiqueta(ByVal Lp As String, ByVal CodigoProd As String, ByVal NombreProd As String, ByVal PrinterName As String)

        Try

            Dim ZPLString As String = String.Format(
                                                          "^XA " &
                                                          "^MMT " &
                                                          "^PW700 " &
                                                          "^LL0406 " &
                                                          "^LS0 " &
                                                          "^FT171,61^A0I,25,14^FH\^FD{0}^FS " &
                                                          "^FT550,61^A0I,25,14^FH\^FD{1}^FS " &
                                                          "^FT670,306^A0I,25,14^FH\^FD{2}^FS " &
                                                          "^FT292,61^A0I,25,24^FH\^FDBodega:^FS " &
                                                          "^FT670,61^A0I,25,24^FH\^FDEmpresa:^FS " &
                                                          "^FT670,367^A0I,25,24^FH\^FDTOM, WMS.  Product Barcode^FS " &
                                                          "^FO2,340^GB670,0,14^FS " &
                                                          "^BY3,3,160^FT670,131^BCI,,Y,N " &
                                                          "^FD{3}^FS " &
                                                          "^PQ1,0,1,Y " &
                                                          "^XZ", AP.NomBodega,
                                                          AP.NomEmpresa,
                                                          CodigoProd + " - " + NombreProd,
                                                         "$" + Lp)

            RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

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


    Private Sub grdStockPorLote_DoubleClick(sender As Object, e As EventArgs) Handles grdStockPorLote.DoubleClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim Obj As New clsBeVW_stock_res
                Dim objRecepcionOC As New clsBeTrans_re_oc
                Obj.IdStock = Dr.Item("IdStock")
                clsLnVW_stock_res.GetSingle(Obj)
                objRecepcionOC = clsLnTrans_re_oc.GetSingle(Obj.IdRecepcionEnc)

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    With frmRegistroLote_Posicion
                        .Modo = frmRegistroLote_Posicion.TipoTrans.Editar
                        '.MdiParent = MdiParent
                        .pBeStockRes = Obj
                        .pBeTrans_re_oc = objRecepcionOC
                        .ShowDialog()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    Hide()
                End If

                Cargar_Datos()

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGrid As String = ""

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


            XtraMessageBox.Show("Diseño de grid guardado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub frmStockPorLote_Posicion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            '#EJC20210716:Restaurar LayoutGrid en frmstock_especifico_list.vb
            vNombreArchivoLayOutGrid = "frmStockPorLote_Posicion.xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

            Cargar_Datos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Restore_LayOut()

        Try

            If IsLoading Then Exit Sub

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

    Private Sub GridView_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        Try

            If IsLoading Then Exit Sub

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