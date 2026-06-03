Imports System.Drawing.Printing
Imports System.IO
Imports System.Diagnostics
Imports System.Reflection
Imports System.Linq
Imports System.Collections.Generic
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
    Private _layoutApplied As Boolean = False
    Private _chartsLoadedForKey As String = String.Empty
    Private _pendingChartsRefresh As Boolean = False
    Private _dtChartRiesgoVence As DataTable = Nothing
    Private _dtChartTopSku As DataTable = Nothing
    Private _dtChartUtilizable As DataTable = Nothing

    Private Sub Cargar_Datos()

        IsLoading = True

        Dim swTotal As Stopwatch = Stopwatch.StartNew()

        Try

            Debug.WriteLine(String.Format("[StockPorLote] Cargar_Datos inicio {0:yyyy-MM-dd HH:mm:ss.fff}", Now))

            Dim DT As New DataTable
            DT.Clear()

            If cmbBodega.EditValue Is Nothing Then Return

            Dim vIdBodega = Integer.Parse(cmbBodega.EditValue)
            Dim vIdPropietarioBodega = Integer.Parse(cmbPropietarioBodega.EditValue)

            Dim swQuery As Stopwatch = Stopwatch.StartNew()
            Dim dsReporte As DataSet = Nothing

            Try
                ' #EJC20260602_STOCK_POR_LOTE_DATASET: Carga todo el reporte en un único roundtrip SQL.
                dsReporte = clsLnStock.Get_Reporte_Stock_Dataset(vIdBodega, vIdPropietarioBodega, mnuSinExistencia.Checked)
            Catch exDs As Exception
                Debug.WriteLine(String.Format("[StockPorLote] Fallback DataSet->Legacy por error: {0}", exDs.Message))
            End Try

            If dsReporte IsNot Nothing AndAlso dsReporte.Tables.Count > 0 Then
                DT = dsReporte.Tables(0)
                _dtChartRiesgoVence = If(dsReporte.Tables.Count > 1, dsReporte.Tables(1), Nothing)
                _dtChartTopSku = If(dsReporte.Tables.Count > 2, dsReporte.Tables(2), Nothing)
                _dtChartUtilizable = If(dsReporte.Tables.Count > 3, dsReporte.Tables(3), Nothing)
            Else
                DT = clsLnStock.Get_Reporte_Stock(vIdBodega, vIdPropietarioBodega, mnuSinExistencia.Checked)
                _dtChartRiesgoVence = Nothing
                _dtChartTopSku = Nothing
                _dtChartUtilizable = Nothing
            End If

            swQuery.Stop()
            Debug.WriteLine(String.Format("[StockPorLote] Query principal ms={0}, rows={1}, dataset={2}", swQuery.ElapsedMilliseconds, If(DT Is Nothing, 0, DT.Rows.Count), If(dsReporte Is Nothing, "No", "Si")))

            grdStockPorLote.DataSource = Nothing

            If DT IsNot Nothing Then

                If Comunicacion_NAV AndAlso Not DT.Columns.Contains("Existencia_NAV") Then
                    DT.Columns.Add("Existencia_NAV", GetType(Decimal))
                End If

                If DT.Rows.Count > 0 Then
                    Dim swRender As Stopwatch = Stopwatch.StartNew()
                    GridView1.BeginDataUpdate()
                    GridView1.BeginUpdate()
                    Try
                        grdStockPorLote.DataSource = DT
                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                        ApplyGridFormattingAndSummaries()

                        If GridView1.GroupCount > 0 AndAlso GridView1.RowCount <= 15000 Then
                            GridView1.ExpandAllGroups()
                        End If

                        If GridView1.RowCount <= 5000 Then
                            GridView1.BestFitColumns()
                        End If
                    Finally
                        GridView1.EndUpdate()
                        GridView1.EndDataUpdate()
                    End Try
                    swRender.Stop()
                    Debug.WriteLine(String.Format("[StockPorLote] Render grid ms={0}", swRender.ElapsedMilliseconds))
                End If

            End If

            If Not _layoutApplied Then
                Set_LayOut_Grid()
            End If

            If chkObtenerExistenciaNAV.Checked Then
                Dim swNav As Stopwatch = Stopwatch.StartNew()
                Get_Existencias_NAV(DT)
                swNav.Stop()
                Debug.WriteLine(String.Format("[StockPorLote] NAV reconcile ms={0}", swNav.ElapsedMilliseconds))
            End If

            _pendingChartsRefresh = True
            LoadChartsIfNeeded(vIdBodega, vIdPropietarioBodega, mnuSinExistencia.Checked)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            swTotal.Stop()
            Debug.WriteLine(String.Format("[StockPorLote] Cargar_Datos fin ms={0}", swTotal.ElapsedMilliseconds))
            IsLoading = False
        End Try

    End Sub

    Private Sub ApplyGridFormattingAndSummaries()

        GridView1.OptionsView.ShowFooter = True
        GridView1.GroupSummary.Clear()

        ApplyNumericSummary("Cantidad_Reservada_UMBas")
        ApplyNumericSummary("Cantidad_Reservada_Pres")
        ApplyNumericSummary("Peso")
        ApplyNumericSummary("CantidadUMBas")
        ApplyNumericSummary("CantidadPresentacion")
        ApplyNumericSummary("Disponible_UMBas")
        ApplyNumericSummary("Disponible_Presentación")
        ApplyNumericSummary("Cant_Pickeada_Presentacion")
        ApplyNumericSummary("Cant_No_Pickeada_UMBas")

        If Comunicacion_NAV Then
            ApplyNumericSummary("Existencia_NAV")
        End If

        Dim colProducto = GridView1.Columns.ColumnByFieldName("Producto")
        If colProducto IsNot Nothing Then
            colProducto.SummaryItem.SummaryType = SummaryItemType.Count
            colProducto.SummaryItem.DisplayFormat = "{0:n0}"
            colProducto.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            colProducto.DisplayFormat.FormatString = "{0:n0}"
        End If

        Dim colFechaIngreso = GridView1.Columns.ColumnByFieldName("Fecha_Ingreso")
        If colFechaIngreso IsNot Nothing Then
            colFechaIngreso.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            colFechaIngreso.DisplayFormat.FormatString = "G"
        End If

        AddGroupSummary("Cantidad_Reservada_UMBas", "Cantidad_Reservada_UMBas")
        AddGroupSummary("CantidadUMBas", "CantidadUMBas")
        AddGroupSummary("CantidadPresentacion", "CantidadPresentacion")
        AddGroupSummary("Disponible_UMBas", "Disponible_UMBas")
        AddGroupSummary("Disponible_Presentación", "Disponible_Presentación")

        If Comunicacion_NAV Then
            AddGroupSummary("Existencia_NAV", "Existencia_NAV")
        End If

        AddGroupSummary("Producto", "Producto", SummaryItemType.Count, "{0:n0}")

        Dim colIdPresentacion = GridView1.Columns.ColumnByFieldName("IdPresentacion")
        If colIdPresentacion IsNot Nothing Then
            colIdPresentacion.Visible = False
        End If

    End Sub

    Private Sub ApplyNumericSummary(ByVal fieldName As String)
        Dim col = GridView1.Columns.ColumnByFieldName(fieldName)
        If col Is Nothing Then Exit Sub

        col.SummaryItem.SummaryType = SummaryItemType.Sum
        col.SummaryItem.DisplayFormat = "{0:n6}"
        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        col.DisplayFormat.FormatString = "{0:n6}"
    End Sub

    Private Sub AddGroupSummary(ByVal fieldName As String,
                                ByVal showInFieldName As String,
                                Optional ByVal summaryType As SummaryItemType = SummaryItemType.Sum,
                                Optional ByVal displayFormat As String = "{0:n6}")
        Dim showCol = GridView1.Columns.ColumnByFieldName(showInFieldName)
        If showCol Is Nothing Then Exit Sub

        Dim item As New GridGroupSummaryItem() With {
            .FieldName = fieldName,
            .SummaryType = summaryType,
            .DisplayFormat = displayFormat,
            .ShowInGroupColumnFooter = showCol
        }

        GridView1.GroupSummary.Add(item)
    End Sub

    Private Function BuildChartsKey(ByVal pIdBodega As Integer,
                                    ByVal pIdPropietarioBodega As Integer,
                                    ByVal pExcluirSinExistencia As Boolean) As String
        Return String.Format("{0}|{1}|{2}", pIdBodega, pIdPropietarioBodega, pExcluirSinExistencia)
    End Function

    Private Sub LoadChartsIfNeeded(ByVal pIdBodega As Integer,
                                   ByVal pIdPropietarioBodega As Integer,
                                   ByVal pExcluirSinExistencia As Boolean)

        Dim selected = XtraTabControl1.SelectedTabPage
        If selected Is tabDatos AndAlso Not _pendingChartsRefresh Then Exit Sub

        Dim key = BuildChartsKey(pIdBodega, pIdPropietarioBodega, pExcluirSinExistencia)
        If Not _pendingChartsRefresh AndAlso _chartsLoadedForKey = key Then Exit Sub

        Dim swCharts As Stopwatch = Stopwatch.StartNew()

        Dim dtRiesgo As DataTable = _dtChartRiesgoVence
        Dim dtTopSku As DataTable = _dtChartTopSku
        Dim dtUtilizable As DataTable = _dtChartUtilizable

        If dtRiesgo Is Nothing OrElse dtTopSku Is Nothing Then
            Dim DT1 As DataTable = clsLnStock.Get_Reporte_Stock_Grafico(pIdBodega, pIdPropietarioBodega, pExcluirSinExistencia)
            dtRiesgo = DT1
            dtTopSku = DT1
        End If

        If dtUtilizable Is Nothing Then
            Dim DTStock As DataTable = TryCast(grdStockPorLote.DataSource, DataTable)
            ConfigureEstadoUtilizableChart(chartFamilia, DTStock)
        Else
            ConfigureEstadoUtilizableChartDirect(chartFamilia, dtUtilizable)
        End If

        ConfigureVencimientoBucketsChart(chartDispersionVence, dtRiesgo)
        ConfigureTopProductosChart(chartProducto, dtTopSku)

        swCharts.Stop()
        Debug.WriteLine(String.Format("[StockPorLote] Charts ms={0}", swCharts.ElapsedMilliseconds))

        _chartsLoadedForKey = key
        _pendingChartsRefresh = False
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

            AddHandler printLink.CreateMarginalHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

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

            printLink.Margins = New System.Drawing.Printing.Margins(40, 40, 130, 60)
            printLink.PaperKind = System.Drawing.Printing.PaperKind.Letter
            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdStockPorLote
            printLink.Landscape = True
            Dim colScope As IDisposable = clsUiPrintHelper.BeginRelevantColumnsScope(grdStockPorLote, 12)
            Try
                printLink.CreateDocument(printingSystem1)
            Finally
                colScope.Dispose()
            End Try
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

        Dim vTitulo As String = "EXISTENCIAS POR LOTE - RESUMEN"
        Dim vBodega As String = If(cmbBodega.Text, AP.NomBodega)
        Dim vPropietario As String = If(cmbPropietarioBodega.Text, "")
        Dim vUsuario As String = If(AP.UsuarioAp.Nombres, "")
        Dim vFiltroStock As String = If(mnuSinExistencia.Checked, "Solo con existencia", "Incluye sin existencia")

        Dim vWidth As Single = e.Graph.ClientPageSize.Width
        Dim vBlue As Color = Color.FromArgb(24, 69, 117)

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Near)
        e.Graph.Font = New Font("Segoe UI Semibold", 16, FontStyle.Bold)
        e.Graph.DrawString(vTitulo,
                           vBlue,
                           New RectangleF(0, 0, vWidth, 34),
                           DevExpress.XtraPrinting.BorderSide.None)

        e.Graph.DrawLine(New PointF(0, 34), New PointF(vWidth, 34), vBlue, 1)

        e.Graph.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        e.Graph.DrawString("Bodega:", Color.Black, New RectangleF(0, 40, 90, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString("Propietario:", Color.Black, New RectangleF(0, 58, 90, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString("Filtro:", Color.Black, New RectangleF(0, 76, 90, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString("Usuario:", Color.Black, New RectangleF(vWidth - 250, 40, 70, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString("Fecha impresión:", Color.Black, New RectangleF(vWidth - 250, 58, 110, 18), DevExpress.XtraPrinting.BorderSide.None)

        e.Graph.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        e.Graph.DrawString(vBodega, Color.Black, New RectangleF(95, 40, vWidth - 360, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString(vPropietario, Color.Black, New RectangleF(95, 58, vWidth - 360, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString(vFiltroStock, Color.Black, New RectangleF(95, 76, vWidth - 360, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString(vUsuario, Color.Black, New RectangleF(vWidth - 175, 40, 170, 18), DevExpress.XtraPrinting.BorderSide.None)
        e.Graph.DrawString(Format(Now, "dd/MM/yyyy HH:mm"), Color.Black, New RectangleF(vWidth - 135, 58, 130, 18), DevExpress.XtraPrinting.BorderSide.None)

        e.Graph.DrawLine(New PointF(0, 102), New PointF(vWidth, 102), Color.Gainsboro, 1)

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
                Dim NomPres As String = ""
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
                NomPres = IIf(IsDBNull(Dr.Item("Presentacion")), 0, Dr.Item("Presentacion"))

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
                ObjTransReDet.Nombre_presentacion = NomPres

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

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
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

            ' #EJC20260602: Tabs con nombre operativo para lectura rápida.
            tabGraficoDispersion.Text = "Riesgo Vencimiento"
            tabGraficoProducto.Text = "Top SKU"
            tabGraficoPorFamilia.Text = "Utilizable / No utilizable"

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

            _layoutApplied = True

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

    Private Sub XtraTabControl1_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        Try
            If cmbBodega.EditValue Is Nothing OrElse cmbPropietarioBodega.EditValue Is Nothing Then Exit Sub

            Dim vIdBodega = Integer.Parse(cmbBodega.EditValue)
            Dim vIdPropietarioBodega = Integer.Parse(cmbPropietarioBodega.EditValue)
            LoadChartsIfNeeded(vIdBodega, vIdPropietarioBodega, mnuSinExistencia.Checked)
        Catch ex As Exception
            Debug.WriteLine(String.Format("[StockPorLote] Error al cargar gráficos por tab: {0}", ex.Message))
        End Try
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

    Private Sub ConfigureVencimientoBucketsChart(chartControl As ChartControl, dataTable As DataTable)
        Try
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            Dim dtBuckets As DataTable = BuildVencimientoBucketsData(dataTable)

            Dim series As New DevExpress.XtraCharts.Series("Stock por riesgo de vencimiento", ViewType.Bar)
            series.ArgumentDataMember = "Rango"
            series.ValueDataMembers.AddRange("Stock")
            series.DataSource = dtBuckets
            chartControl.Series.Add(series)

            Dim diagram As XYDiagram = CType(chartControl.Diagram, XYDiagram)
            diagram.AxisX.Title.Text = "Rango de vencimiento"
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Title.Text = "Stock"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

            Dim title As New ChartTitle()
            title.Text = "Riesgo de vencimiento"
            chartControl.Titles.Add(title)
        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function BuildVencimientoBucketsData(ByVal dataTable As DataTable) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Rango", GetType(String))
        dt.Columns.Add("Orden", GetType(Integer))
        dt.Columns.Add("Stock", GetType(Decimal))

        dt.Rows.Add("0-15 días", 1, 0D)
        dt.Rows.Add("16-30 días", 2, 0D)
        dt.Rows.Add("31-60 días", 3, 0D)
        dt.Rows.Add("61-90 días", 4, 0D)
        dt.Rows.Add(">90 días", 5, 0D)
        dt.Rows.Add("Sin vencimiento", 6, 0D)

        If dataTable Is Nothing Then Return dt

        ' #EJC20260602_CHART_SCHEMA_GUARD:
        ' El origen puede venir detallado (Fecha_Vence + Stock) o ya agregado (Rango + Stock).
        ' Se soportan ambos para evitar error por columna faltante al cambiar el SP/dataset.
        Dim hasFechaVence As Boolean = dataTable.Columns.Contains("Fecha_Vence")
        Dim hasStock As Boolean = dataTable.Columns.Contains("Stock")
        Dim hasRango As Boolean = dataTable.Columns.Contains("Rango")

        If hasRango AndAlso hasStock AndAlso Not hasFechaVence Then
            For Each row As DataRow In dataTable.Rows
                Dim rango As String = If(IsDBNull(row("Rango")), "", Convert.ToString(row("Rango")).Trim())
                If rango = "" Then Continue For

                Dim stock As Decimal = 0D
                If Not IsDBNull(row("Stock")) Then stock = Convert.ToDecimal(row("Stock"))

                For i As Integer = 0 To dt.Rows.Count - 1
                    If String.Equals(Convert.ToString(dt.Rows(i)("Rango")), rango, StringComparison.OrdinalIgnoreCase) Then
                        dt.Rows(i)("Stock") = Convert.ToDecimal(dt.Rows(i)("Stock")) + stock
                        Exit For
                    End If
                Next
            Next

            Dim viewFromRango As DataView = dt.DefaultView
            viewFromRango.Sort = "Orden ASC"
            Return viewFromRango.ToTable()
        End If

        If Not hasFechaVence OrElse Not hasStock Then
            Debug.WriteLine("[StockPorLote] BuildVencimientoBucketsData: esquema no compatible para riesgo de vencimiento.")
            Return dt
        End If

        For Each row As DataRow In dataTable.Rows
            Dim stock As Decimal = 0D
            If Not IsDBNull(row("Stock")) Then stock = Convert.ToDecimal(row("Stock"))

            If IsDBNull(row("Fecha_Vence")) Then
                dt.Rows(5)("Stock") = Convert.ToDecimal(dt.Rows(5)("Stock")) + stock
            Else
                Dim vence As Date = Convert.ToDateTime(row("Fecha_Vence"))
                If vence = New Date(1900, 1, 1) Then
                    dt.Rows(5)("Stock") = Convert.ToDecimal(dt.Rows(5)("Stock")) + stock
                Else
                    Dim dias As Integer = CInt((vence.Date - Today.Date).TotalDays)
                    If dias <= 15 Then
                        dt.Rows(0)("Stock") = Convert.ToDecimal(dt.Rows(0)("Stock")) + stock
                    ElseIf dias <= 30 Then
                        dt.Rows(1)("Stock") = Convert.ToDecimal(dt.Rows(1)("Stock")) + stock
                    ElseIf dias <= 60 Then
                        dt.Rows(2)("Stock") = Convert.ToDecimal(dt.Rows(2)("Stock")) + stock
                    ElseIf dias <= 90 Then
                        dt.Rows(3)("Stock") = Convert.ToDecimal(dt.Rows(3)("Stock")) + stock
                    Else
                        dt.Rows(4)("Stock") = Convert.ToDecimal(dt.Rows(4)("Stock")) + stock
                    End If
                End If
            End If
        Next

        Dim view As DataView = dt.DefaultView
        view.Sort = "Orden ASC"
        Return view.ToTable()
    End Function

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

    Private Sub ConfigureTopProductosChart(chartControl As ChartControl, dataTable As DataTable)
        Try
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            Dim dtTop As DataTable = BuildTopProductosData(dataTable, 20)

            Dim series As New DevExpress.XtraCharts.Series("Top SKU por stock", ViewType.Bar)
            series.ArgumentDataMember = "Codigo"
            series.ValueDataMembers.AddRange("Stock")
            series.DataSource = dtTop
            chartControl.Series.Add(series)

            Dim diagram As XYDiagram = CType(chartControl.Diagram, XYDiagram)
            diagram.AxisX.Title.Text = "SKU"
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
            diagram.AxisY.Title.Text = "Stock"
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True

            Dim title As New ChartTitle()
            title.Text = "Top productos por stock actual"
            chartControl.Titles.Add(title)
        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function BuildTopProductosData(ByVal dataTable As DataTable, ByVal topN As Integer) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Codigo", GetType(String))
        dt.Columns.Add("Stock", GetType(Decimal))

        If dataTable Is Nothing Then Return dt

        Dim stockByCodigo As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)

        For Each row As DataRow In dataTable.Rows
            Dim codigo As String = If(IsDBNull(row("Codigo")), "", Convert.ToString(row("Codigo")).Trim())
            If codigo = "" Then Continue For

            Dim stock As Decimal = 0D
            If Not IsDBNull(row("Stock")) Then stock = Convert.ToDecimal(row("Stock"))

            If stockByCodigo.ContainsKey(codigo) Then
                stockByCodigo(codigo) += stock
            Else
                stockByCodigo.Add(codigo, stock)
            End If
        Next

        For Each item In stockByCodigo.OrderByDescending(Function(x) x.Value).Take(topN)
            dt.Rows.Add(item.Key, item.Value)
        Next

        Return dt
    End Function

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

    Private Sub ConfigureEstadoUtilizableChart(chartControl As ChartControl, dataTable As DataTable)
        Try
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            Dim dtEstado As DataTable = BuildEstadoUtilizableData(dataTable)

            Dim series As New DevExpress.XtraCharts.Series("Estado", ViewType.Pie)
            series.ArgumentDataMember = "Categoria"
            series.ValueDataMembers.AddRange("Stock")
            series.DataSource = dtEstado
            chartControl.Series.Add(series)

            Dim title As New ChartTitle()
            title.Text = "Stock utilizable vs no utilizable"
            chartControl.Titles.Add(title)
        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureEstadoUtilizableChartDirect(chartControl As ChartControl, dataTable As DataTable)
        Try
            chartControl.Series.Clear()
            chartControl.Titles.Clear()

            Dim series As New DevExpress.XtraCharts.Series("Estado", ViewType.Pie)
            series.ArgumentDataMember = "Categoria"
            series.ValueDataMembers.AddRange("Stock")
            series.DataSource = dataTable
            chartControl.Series.Add(series)

            Dim title As New ChartTitle()
            title.Text = "Stock utilizable vs no utilizable"
            chartControl.Titles.Add(title)
        Catch ex As Exception
            MessageBox.Show("Se ha producido un error al configurar el gráfico: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function BuildEstadoUtilizableData(ByVal dataTable As DataTable) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Categoria", GetType(String))
        dt.Columns.Add("Stock", GetType(Decimal))

        Dim utilizable As Decimal = 0D
        Dim noUtilizable As Decimal = 0D

        If dataTable IsNot Nothing Then
            For Each row As DataRow In dataTable.Rows
                Dim estado As String = ""
                If dataTable.Columns.Contains("Estado") AndAlso Not IsDBNull(row("Estado")) Then
                    estado = Convert.ToString(row("Estado")).Trim().ToUpperInvariant()
                End If

                Dim stock As Decimal = 0D
                If dataTable.Columns.Contains("Disponible_UMBas") AndAlso Not IsDBNull(row("Disponible_UMBas")) Then
                    stock = Convert.ToDecimal(row("Disponible_UMBas"))
                End If

                If estado.Contains("BUEN") OrElse estado.Contains("UTILIZABLE") Then
                    utilizable += stock
                Else
                    noUtilizable += stock
                End If
            Next
        End If

        dt.Rows.Add("Utilizable", utilizable)
        dt.Rows.Add("No utilizable", noUtilizable)
        Return dt
    End Function


End Class



