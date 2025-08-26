Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraBars
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmStockPorLote

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property IsLoading As Boolean = True
    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Comunicacion_NAV As Boolean = False

    Private Sub Cargar_Datos()

        IsLoading = True

        Try

            Dim DT As New DataTable

            DT.Clear()

            grdStockPorLote.DataSource = Nothing

            If cmbBodega.EditValue Is Nothing Then Return

            Dim vIdBodega = Integer.Parse(cmbBodega.EditValue)
            Dim vIdPropietarioBodega = Integer.Parse(cmbPropietarioBodega.EditValue)

            DT = clsLnStock.Get_Reporte_Stock(vIdBodega, vIdPropietarioBodega, mnuSinExistencia.Checked)

            If Not DT Is Nothing Then

                If Comunicacion_NAV Then
                    DT.Columns.Add("Existencia_NAV", GetType(Decimal))
                End If

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

                    GridView1.Columns("Cant_Pickeada_Presentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Cant_Pickeada_Presentacion").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Cant_Pickeada_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cant_Pickeada_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Cant_No_Pickeada_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Cant_No_Pickeada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("Cant_No_Pickeada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Cant_No_Pickeada_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    If Comunicacion_NAV Then
                        GridView1.Columns("Existencia_NAV").SummaryItem.SummaryType = SummaryItemType.Sum
                        GridView1.Columns("Existencia_NAV").SummaryItem.DisplayFormat = "{0:n6}"
                        GridView1.Columns("Existencia_NAV").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Existencia_NAV").DisplayFormat.FormatString = "{0:n6}"
                    End If

                    GridView1.Columns("Producto").SummaryItem.SummaryType = SummaryItemType.Count
                    GridView1.Columns("Producto").SummaryItem.DisplayFormat = "{0:n0}"
                    GridView1.Columns("Producto").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Producto").DisplayFormat.FormatString = "{0:n0}"

                    GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

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

                    If Comunicacion_NAV Then

                        Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Existencia_NAV", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView1.Columns("Existencia_NAV")}
                        GridView1.GroupSummary.Add(item5)

                    End If

                    Dim item6 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Producto", .SummaryType = SummaryItemType.Count,
                        .DisplayFormat = "{0:n0}", .ShowInGroupColumnFooter = GridView1.Columns("Producto")}
                    GridView1.GroupSummary.Add(item6)

                    GridView1.Columns("IdPresentacion").Visible = False

                    GridView1.ExpandAllGroups()

                    GridView1.BestFitColumns()

                End If

            End If

            Set_LayOut_Grid()

            If chkObtenerExistenciaNAV.Checked Then
                Get_Existencias_NAV(DT)
            End If

            Dim DT1 As New DataTable
            DT1 = clsLnStock.Get_Reporte_Stock_Grafico(vIdBodega, vIdPropietarioBodega, mnuSinExistencia.Checked)
            ConfigureVencimientoScatterChart(chartDispersionVence, DT1)
            ConfigureExistenciasChart(chartProducto, DT1)

            Dim DT2 As New DataTable
            DT2 = clsLnStock.Get_Reporte_Stock_Grafico_Familia_And_Clasificacion(vIdBodega, vIdPropietarioBodega, mnuSinExistencia.Checked)
            ConfigureExistenciasChartFam(chartFamilia, DT2)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            IsLoading = False
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

        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

            Cargar_Datos()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

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
        Exportar_Grid_A_Excel(grdStockPorLote, "WMS_ExistenciasPorLote.xlsx")
    End Sub

    Private Sub Imprimir_Etiqueta(ByVal Lp As String, ByVal CodigoProd As String, ByVal NombreProd As String, ByVal PrinterName As String)

        Try


            Dim ZPLString As String = String.Format("^XA " &
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

    Private Sub cmdPrintLP_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdPrintLP.ItemClick

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim Licencia As String = ""
                Dim Codigo_Producto As String = ""
                Dim Nombre_producto As String = ""
                Dim Lote As String = ""
                Dim CantidadUMBas As Double = 0
                Dim Factor As Double = 0
                Dim IdProducto As Integer = 0
                Dim Vence As Date = New Date(1900, 1, 1)

                'SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                Dim pd As PrintDialog = New PrintDialog()
                pd.PrinterSettings = New PrinterSettings()

                '#EJC20240528:Imprimir licencia stock por lote
                Licencia = IIf(IsDBNull(Dr.Item("licencia")), "", Dr.Item("licencia"))
                Codigo_Producto = Dr.Item("Codigo")
                Nombre_producto = Dr.Item("Producto")
                Lote = IIf(IsDBNull(Dr.Item("Lote")), "", Dr.Item("Lote"))
                CantidadUMBas = Dr.Item("Disponible_UMBas")
                Factor = IIf(IsDBNull(Dr.Item("Factor")), 0, Dr.Item("Factor"))
                IdProducto = IIf(IsDBNull(Dr.Item("IdProducto")), 0, Dr.Item("IdProducto"))
                Vence = IIf(IsDBNull(Dr.Item("Fecha_Vence")), New Date(1900, 1, 1), Dr.Item("Fecha_Vence"))

                Dim ObjTransReDet As New clsBeTrans_re_det
                ObjTransReDet.Codigo_Producto = Codigo_Producto
                ObjTransReDet.Nombre_producto = Nombre_producto
                ObjTransReDet.Lote = Lote
                ObjTransReDet.Lic_plate = Licencia
                ObjTransReDet.cantidad_recibida = CantidadUMBas
                ObjTransReDet.IdPresentacion = IIf(IsDBNull(Dr("IdPresentacion")), 0, Dr("IdPresentacion"))
                ObjTransReDet.Presentacion.Factor = Factor
                ObjTransReDet.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(IdProducto, cmbBodega.EditValue)
                ObjTransReDet.Fecha_vence = Vence

                If ObjTransReDet IsNot Nothing Then
                    Dim Impresion As New frmImpresionRecepcion
                    Impresion.pTransReDet = ObjTransReDet
                    Impresion.ShowDialog()
                    Impresion.Dispose()
                Else
                    XtraMessageBox.Show("No se cargo el producto para reimpresión.", "Impresión BOF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If


            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub frmStockPorLote_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Inicializando estructuras...")

            vNombreArchivoLayOutGrid = "grdStockPorLote.xml"

            Comunicacion_NAV = (clsBD.Instancia.WSTOMHH.Trim <> "")

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietarioBodega, cmbBodega.EditValue)

            '#EJC20220412: Mostrar check para obtener existencias NAV.
            If Comunicacion_NAV Then
                chkObtenerExistenciaNAV.Visibility = BarItemVisibility.Always
            Else
                chkObtenerExistenciaNAV.Visibility = BarItemVisibility.Never
            End If

            '#EJC20220412: Se llama al listar bodegas.
            'Cargar_Datos()

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

            Set_LayOut_Grid()

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

    Private Sub Get_Existencias_NAV(ByVal DTExistenciasWMS As DataTable)

        Try

            Dim vCodigoProducto As String = ""
            Dim vLote As String = ""
            Dim vExistenciaNAV As Decimal = 0
            Dim vIdPresentacion As Integer = 0
            Dim vResultadoConversion As Double = 0
            Dim vFactor As Double = 0

            Debug.WriteLine("Inicio " & Now)

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            If Not DTExistenciasWMS Is Nothing Then

                For Each D In DTExistenciasWMS.Rows

                    vCodigoProducto = D("Codigo")
                    vLote = D("Lote")
                    vIdPresentacion = IIf(IsDBNull(D("IdPresentacion")), 0, D("IdPresentacion"))
                    vFactor = IIf(IsDBNull(D("Factor")), 0, D("Factor"))

                    SplashScreenManager.Default.SetWaitFormDescription("Cod: " & vCodigoProducto & " Lot. " & vLote)

                    vExistenciaNAV = Get_Single_Existencia_NAV(vCodigoProducto, vLote)

                    If vExistenciaNAV <> 0 Then

                        If vIdPresentacion <> 0 Then
                            If vFactor > 0 Then
                                vExistenciaNAV = vExistenciaNAV / vFactor
                            End If
                        End If

                        D("Existencia_NAV") = vExistenciaNAV

                        Debug.WriteLine("Una singularidad. Código: " & vCodigoProducto & " Lote: " & vLote & " Existencia_NAV: " & vExistenciaNAV)

                    Else
                        D("Existencia_NAV") = 0
                        Debug.WriteLine("Código: " & vCodigoProducto & " Lote: " & vLote & " Existencia_NAV: 0")
                    End If

                    Application.DoEvents()

                Next

            End If

            Debug.WriteLine("Fin " & Now)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        Finally
            SplashScreenManager.CloseForm()
        End Try

    End Sub

    Private Function Get_Single_Existencia_NAV(ByVal pCodigoProducto As String, ByVal pLote As String) As Decimal

        Get_Single_Existencia_NAV = 0

        Try

            Dim ArchHeader As New wsTOMHH.clsArchHeader
            ArchHeader.Tipo = "WM"

            Get_Single_Existencia_NAV = wsTOMHHInstance.Get_Existencia_NAV(ArchHeader, pCodigoProducto, pLote)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Function

    Private Sub chkObtenerExistenciaNAV_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkObtenerExistenciaNAV.CheckedChanged
        Cargar_Datos()
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

    Private Sub mnuSinExistencia_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles mnuSinExistencia.CheckedChanged
        Cargar_Datos()
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            If e.RowHandle >= 0 Then

                Dim View As GridView = sender

                '#EJC20210223: Formateo condicional de columnas por reabasto.
                If GridView1.RowCount > 0 Then

                    If View.Columns.ColumnByFieldName("Disponible_UMBas") IsNot Nothing Then

                        Dim dispubic As Double = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Disponible_UMBas"))
                        Dim reserva As Double = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cantidad_Reservada_UMBas"))

                        If (dispubic = 0) Then
                            e.Appearance.BackColor = Color.LightSalmon
                            e.Appearance.BackColor2 = Color.LightSalmon
                        ElseIf reserva > 0 Then
                            e.Appearance.BackColor = Color.Yellow
                            e.Appearance.BackColor2 = Color.LightYellow
                        ElseIf reserva = 0 Then
                            e.Appearance.BackColor = Color.White
                            e.Appearance.BackColor2 = Color.White
                        End If

                    End If

                End If

            End If

        Catch ex As Exception

            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
    Private Sub gviewEncabezadoPedido_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView1.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView1.DataRowCount.ToString()))
    End Sub

    Private Sub ConfigureVencimientoScatterChart(chartControl As ChartControl, dataTable As DataTable)

        Try
            ' Limpiar cualquier serie o título existente en el control de gráfico
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            ' Crear la serie de dispersión (Scatter) para Fecha de Vencimiento vs. Cantidad en Stock
            Dim series1 As New DevExpress.XtraCharts.Series("Fecha de Vencimiento vs. Cantidad", ViewType.ScatterLine)
            series1.ArgumentDataMember = "Fecha_Vence"  ' La fecha de vencimiento será el argumento (eje X)
            series1.ValueDataMembers.AddRange("Stock") ' La cantidad en stock será el valor (eje Y)
            series1.DataSource = dataTable

            ' Agregar la serie al control de gráfico
            chartControl.Series.Add(series1)

            ' Configuración del diagrama (ejes)
            Dim diagram As XYDiagram = CType(chartControl.Diagram, XYDiagram)
            diagram.AxisX.Title.Text = "Vencimiento"
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Title.Text = "Cantidad en Stock"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

            ' Configuración del título del gráfico
            Dim title As New ChartTitle()
            title.Text = "Dispersión de Fecha de Vencimiento vs. Cantidad en Stock"
            chartControl.Titles.Add(title)

        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ConfigureExistenciasChart(chartControl As ChartControl, dataTable As DataTable)
        Try
            ' Limpiar cualquier serie o título existente en el control de gráfico
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            ' Crear la serie de barras para mostrar existencias por producto
            Dim series1 As New DevExpress.XtraCharts.Series("Existencias", ViewType.Bar)
            series1.ArgumentDataMember = "Codigo"  ' El producto será mostrado en el eje X
            series1.ValueDataMembers.AddRange("Stock") ' La cantidad existente será mostrada en el eje Y
            series1.DataSource = dataTable

            ' Agregar la serie al control de gráfico
            chartControl.Series.Add(series1)

            ' Configuración del diagrama (ejes)
            Dim diagram As XYDiagram = CType(chartControl.Diagram, XYDiagram)
            diagram.AxisX.Title.Text = "Producto"
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Title.Text = "Cantidad Existente"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

            ' Configuración del título del gráfico
            Dim title As New ChartTitle()
            title.Text = "Existencias de Productos en la Bodega"
            chartControl.Titles.Add(title)

        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureExistenciasChartFamClas(chartControl As ChartControl, dataTable As DataTable)
        Try
            ' Limpiar cualquier serie o título existente en el control de gráfico
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            ' Obtener los valores únicos de Clasificación
            Dim clasificaciones = dataTable.AsEnumerable().Select(Function(row) row.Field(Of String)("Clasificacion")).Distinct()

            ' Crear una serie para cada clasificación
            For Each clasificacion In clasificaciones
                Dim series As New DevExpress.XtraCharts.Series(clasificacion, ViewType.Bar)
                series.ArgumentDataMember = "Familia"  ' La familia será el argumento principal (eje X)
                series.ValueDataMembers.AddRange("Stock") ' La cantidad en stock será el valor (eje Y)

                ' Filtrar los datos para la clasificación actual
                series.DataSource = dataTable.Select("Clasificacion = '" & clasificacion & "'").CopyToDataTable()

                ' Agregar la serie al control de gráfico
                chartControl.Series.Add(series)
            Next

            ' Configuración del diagrama (ejes)
            Dim diagram As XYDiagram = CType(chartControl.Diagram, XYDiagram)
            diagram.AxisX.Title.Text = "Familia"
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Title.Text = "Stock"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

            ' Configuración del título del gráfico
            Dim title As New ChartTitle()
            title.Text = "Existencias por Familia y Clasificación"
            chartControl.Titles.Add(title)

        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureExistenciasChartFam(chartControl As ChartControl, dataTable As DataTable)
        Try
            ' Limpiar cualquier serie o título existente en el control de gráfico
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            ' Crear la serie de barras para mostrar existencias por Familia
            Dim series As New DevExpress.XtraCharts.Series("Existencias por Familia", ViewType.Bar)
            series.ArgumentDataMember = "Familia"  ' La familia será el argumento principal (eje X)
            series.ValueDataMembers.AddRange("Stock") ' La cantidad en stock será el valor (eje Y)

            ' Asignar la fuente de datos
            series.DataSource = dataTable

            ' Agregar la serie al control de gráfico
            chartControl.Series.Add(series)

            ' Configuración del diagrama (ejes)
            Dim diagram As XYDiagram = CType(chartControl.Diagram, XYDiagram)
            diagram.AxisX.Title.Text = "Familia"
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Title.Text = "Stock"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

            ' Configuración del título del gráfico
            Dim title As New ChartTitle()
            title.Text = "Existencias por Familia"
            chartControl.Titles.Add(title)

        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class