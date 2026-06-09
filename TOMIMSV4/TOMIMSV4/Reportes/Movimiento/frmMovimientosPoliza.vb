Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmMovimientosPoliza

    'Private pListMovs As New List(Of clsBeVW_Movimientos)
    Private pListMovs As New List(Of clsBeVW_Movimientos_Poliza)
    Private DT As New DataTable("MovsCardex")
    Public pBeListaProductos As New List(Of Integer)
    Public Property ProductoEspecifico As New clsBeProducto

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetDatataTable()

        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Póliza", GetType(String))
        DT.Columns.Add("NombreArea", GetType(String))
        DT.Columns.Add("Producto", GetType(String))
        DT.Columns.Add("Código", GetType(String))
        DT.Columns.Add("Código Barra", GetType(String))
        DT.Columns.Add("Cantidad U.M.Bas", GetType(Double))
        DT.Columns.Add("UMBas", GetType(String))
        DT.Columns.Add("Cantidad Presentación", GetType(Double))
        DT.Columns.Add("Presentación", GetType(String))
        DT.Columns.Add("Peso", GetType(Double))
        DT.Columns.Add("Lote", GetType(String))
        DT.Columns.Add("FechaVence", GetType(Date))
        DT.Columns.Add("Licencia", GetType(String))
        DT.Columns.Add("FechaMovimiento", GetType(Date))
        DT.Columns.Add("Estado Origen", GetType(String))
        DT.Columns.Add("Estado Destino", GetType(String))
        DT.Columns.Add("Ubicación Origen", GetType(String))
        DT.Columns.Add("Ubicación Destino", GetType(String))
        DT.Columns.Add("Tipo Tarea", GetType(String))
        DT.Columns.Add("IdBodegaOrigen", GetType(Integer))
        DT.Columns.Add("IdProducto", GetType(Integer))
        DT.Columns.Add("Clasificacion", GetType(String))
        DT.Columns.Add("Area_Origen", GetType(String))
        '#GT24032022: campos para reporte cealsa
        DT.Columns.Add("regimen_ingreso", GetType(String))
        DT.Columns.Add("no_ticket_tms", GetType(Integer))
        DT.Columns.Add("fecha_ingreso", GetType(String))
        DT.Columns.Add("placa_ingreso", GetType(String))
        DT.Columns.Add("marca_ingreso", GetType(String))
        DT.Columns.Add("tipo_ingreso", GetType(String))
        DT.Columns.Add("contenedor_ingreso", GetType(String))
        DT.Columns.Add("Regimen_Salida", GetType(String))
        DT.Columns.Add("Fecha_Salida", GetType(String))
        DT.Columns.Add("placa_salida", GetType(String))
        DT.Columns.Add("marca_salida", GetType(String))
        DT.Columns.Add("tipo_salida", GetType(String))
        DT.Columns.Add("contenedor_salida", GetType(String))
        DT.Columns.Add("Poliza_Salida", GetType(String))
        DT.Columns.Add("numero_referencia", GetType(String))

    End Sub

    Public Property Skip_Saving_Layout As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub Cargar()

        Skip_Saving_Layout = True

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando Movimientos...")

            Dim cant As Double
            '#GT02052022_1106: reporte sin filtros, se usaran los de cada columna
            'pListMovs = clsLnTrans_movimientos.Get_Movimientos_Kardex_by_Bodega(cmbBodega.EditValue, ProductoEspecifico.IdProductoBodega)
            '#GT26072024: se agrega el filtro por propietario, pero se dejara fijada la bodega fiscal.
            pListMovs = clsLnTrans_movimientos.Get_Movimientos_Kardex_by_Poliza(cmbPropietario.EditValue, dtpFechaDel.Value, dtpFechaAl.Value)

            If pListMovs.Count > 0 Then

                grdKardex.DataSource = Nothing

                DT.Clear()

                For Each Obj As clsBeVW_Movimientos_Poliza In pListMovs

                    If Obj.IdTipoTarea = 5 OrElse Obj.IdTipoTarea = 17 OrElse Obj.IdTipoTarea = 18 OrElse Obj.TipoTarea = 4 Then
                        cant = (Obj.Cantidad * -1)
                    Else
                        cant = Obj.Cantidad
                    End If

                    If Obj.IdPresentacion <> 0 Then
                        Obj.Cantidad_Presentacion = (cant / Obj.Factor)
                    Else
                        Obj.Cantidad_Presentacion = 0
                    End If

                    DT.Rows.Add(Obj.Propietario,
                                Obj.Poliza,
                                Obj.NombreArea,
                                Obj.Producto,
                                Obj.Codigo,
                                Obj.CodigoBarra,
                                cant,
                                Obj.UMBas,
                                Obj.Cantidad_Presentacion,
                                Obj.Presentacion,
                                Obj.Peso,
                                Obj.Lote,
                                Obj.Fecha_Vence,
                                Obj.Lic_Plate,
                                Obj.Fecha,
                                Obj.EstadoOrigen,
                                Obj.EstadoDestino,
                                Obj.UbicOrigen,
                                Obj.UbicDestino,
                                Obj.TipoTarea,
                                Obj.IdBodegaOrigen,
                                Obj.IdProducto,
                                Obj.Clasificacion,
                                Obj.Area_Origen,
                                Obj.regimen_ingreso,
                                Obj.no_ticket_tms,
                                Obj.fecha_ingreso,
                                Obj.placa_ingreso,
                                Obj.marca_ingreso,
                                Obj.tipo_ingreso,
                                Obj.contenedor_ingreso,
                                Obj.regimen_salida,
                                Obj.Fecha_Salida,
                                Obj.placa_salida,
                                Obj.marca_salida,
                                Obj.tipo_salida,
                                 Obj.contenedor_salida,
                                Obj.Poliza_Salida,
                                Obj.numero_orden
                                )
                Next

            End If

            grdKardex.DataSource = DT

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("Código").Group()

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad U.M.Bas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "DISP: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad U.M.Bas")}
                GridView1.GroupSummary.Add(item)

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Peso",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Peso")}
                GridView1.GroupSummary.Add(item1)

                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Propietario",
                .SummaryType = DevExpress.Data.SummaryItemType.Count,
                .DisplayFormat = "Movimientos: {0:n}",
                .ShowInGroupColumnFooter = GridView1.Columns("Propietario")}
                GridView1.GroupSummary.Add(item2)

                GridView1.Columns("Cantidad U.M.Bas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad U.M.Bas").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad U.M.Bas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad U.M.Bas").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Cantidad Presentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad Presentación").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad Presentación").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("FechaMovimiento").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView1.Columns("FechaMovimiento").DisplayFormat.FormatString = "G"

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad Presentación",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "DISP: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad Presentación")}
                GridView1.GroupSummary.Add(item3)

                GridView1.Columns("IdBodegaOrigen").Visible = False
                GridView1.Columns("IdProducto").Visible = False

                GridView1.ExpandAllGroups()

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                Restore_LayOut()

                GridView1.BestFitColumns(True)

            End If


            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            Skip_Saving_Layout = False
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    'Private Sub cmbBodega_EditValueChanging(sender As Object, e As ChangingEventArgs)
    '    'Cargar()
    '    'GridView1.Focus()
    'End Sub

    Private Sub frmMovimientosPoliza_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
            mnuGuardarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            vNombreArchivoLayOutGrid = "gridMovimientosPoliza.xml"

            If AP.Bodega.Es_Bodega_Fiscal Then

                cmdActualizar.Enabled = True
                AP.Listar_Bodegas_By_Usuario(cmbRegimen)
                cmbRegimen.EditValue = Integer.Parse(AP.IdBodega)
                IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbRegimen.EditValue)
                SetDatataTable()

            Else
                cmdActualizar.Enabled = False
                XtraMessageBox.Show("Reporte disponible para bodega fiscal.")
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

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        cmdActualizar.Enabled = False
        Cargar()
        cmdActualizar.Enabled = True
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdKardex, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True, True, 12)
            Exit Sub
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
            printLink.Component = grdKardex
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

        Dim reportHeader As String = vbNewLine & "Listado de Movimientos Cardex"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        ' #EJC20260603_ROWSTYLE_PRINT_GUARD: evitar costo de formato por celda durante impresión.
        If clsUiPrintHelper.IsPrintingPreviewInProgress Then Exit Sub

        If e.Column.FieldName = "Cantidad U.M.Bas" Then

            Dim View As GridView = sender
            Dim TipoT As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Tipo Tarea"))

            If TipoT = "DESP" OrElse TipoT = "TRAS" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf TipoT = "AJCANTN" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf TipoT = "AJCANTP" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            ElseIf TipoT = "RECE" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            End If

        End If

    End Sub

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

    Private Sub GridView1_Layout(sender As Object, e As EventArgs)
        Guardar_Layout()
    End Sub

    Private Sub Guardar_Layout()

        Try

            If Not Skip_Saving_Layout Then

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

    Private Sub cmbRegimen_EditValueChanged(sender As Object, e As EventArgs) Handles cmbRegimen.EditValueChanged
        Try

            'If AP.Bodega.Es_Bodega_Fiscal Then
            Dim bodega = clsLnBodega.GetSingle_By_Idbodega(cmbRegimen.EditValue)

            If bodega.Es_Bodega_Fiscal Then
                cmdActualizar.Enabled = True
                IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbRegimen.EditValue)
            Else
                cmdActualizar.Enabled = False
                XtraMessageBox.Show("Reporte disponible para bodega fiscal.")
            End If



        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
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

End Class



