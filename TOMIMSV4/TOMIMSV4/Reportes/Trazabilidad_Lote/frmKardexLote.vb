Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmKardexLote

    Private pListMovs As New List(Of clsBeVW_Movimientos)
    Private DT As New DataTable("MovimientosKardexLote")

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetDatataTable()

        DT.Columns.Add("Propietario", GetType(String))
        DT.Columns.Add("Poliza", GetType(String))
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
        DT.Columns.Add("No_Doc_Ingreso", GetType(String))
        DT.Columns.Add("No_Ref_Ingreso", GetType(String))
        DT.Columns.Add("No_Doc_Salida", GetType(String))
        DT.Columns.Add("No_Ref_Salida", GetType(String))

    End Sub

    Private Sub Cargar()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Consultando datos...")

            Dim Cantidad As Double

            pListMovs = clsLnTrans_movimientos.Get_Movimientos_Kardex_Con_Docs(cmbBodega.EditValue,
                                                                               dtpFechaDel.Value,
                                                                               dtpFechaAl.Value,
                                                                               txtLote.Text)

            If pListMovs.Count > 0 Then

                grdKardex.DataSource = Nothing

                DT.Clear()

                For Each Obj As clsBeVW_Movimientos In pListMovs

                    If Obj.IdTipoTarea = 5 OrElse Obj.IdTipoTarea = 17 OrElse Obj.IdTipoTarea = 18 Then
                        Cantidad = (Obj.Cantidad * -1)
                    Else
                        Cantidad = Obj.Cantidad
                    End If

                    If Obj.IdPresentacion <> 0 Then
                        Obj.Cantidad_Presentacion = (Cantidad / Obj.Factor)
                    Else
                        Obj.Cantidad_Presentacion = 0
                    End If

                    DT.Rows.Add(Obj.Propietario,
                                Obj.Poliza,
                                Obj.Producto,
                                Obj.Codigo,
                                Obj.CodigoBarra,
                                Cantidad,
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
                                Obj.No_Doc_Ingreso,
                                Obj.No_Ref_Ingreso,
                                Obj.No_Doc_Salida,
                                Obj.No_Ref_Salida)
                Next

            End If

            grdKardex.DataSource = DT

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ShowFooter = True

                GridView1.Columns("Código").Group()
                GridView1.Columns("Lote").Group()

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad U.M.Bas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "DISP: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad U.M.Bas")}
                GridView1.GroupSummary.Add(item)

                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad Presentación",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "DISP: {0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad Presentación")}
                GridView1.GroupSummary.Add(item3)

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

                GridView1.Columns("IdBodegaOrigen").Visible = False
                GridView1.Columns("IdProducto").Visible = False

                GridView1.ExpandAllGroups()

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                Restore_LayOut_Grid()

                GridView1.BestFitColumns(True)

            End If

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

    Private Sub cmbBodega_EditValueChanging(sender As Object, e As ChangingEventArgs)
        Cargar()
        GridView1.Focus()
    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            Cargar()

            GridView1.Focus()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            Cargar()

            GridView1.Focus()

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

        Dim reportHeader As String = vbNewLine & "Listado de Movimientos Kardex Por Lote"

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

        Try

            If e.Column.FieldName = "Cantidad U.M.Bas" Then

                Dim View As GridView = sender
                Dim TipoT As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Tipo Tarea"))

                If TipoT = "DESP" Then
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

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub frmKardexLote_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Inicializando reporte...")

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            vNombreArchivoLayOutGrid = "GrdMovimientosKardexLote.xml"

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            SetDatataTable()

            Cargar()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                  Text,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
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

    Private vNombreArchivoLayOutGrid As String = ""
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub Restore_LayOut_Grid()

        Try

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            vNombreArchivoLayOutGrid = "GrdMovimientosKardexLote.xml"

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

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            If File.Exists(vNombreArchivoLayOutGrid) Then
                File.Delete(vNombreArchivoLayOutGrid)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

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

End Class