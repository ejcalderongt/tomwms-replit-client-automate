Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmTrazaPorLote

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public listar As New List(Of clsBeVW_Movimientos)
    Private DT As New DataTable("MovimientosPorLote")

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

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
        DT.Columns.Add("Fecha Vence", GetType(Date))
        DT.Columns.Add("Lic Plate", GetType(String))
        DT.Columns.Add("Fecha Movimiento", GetType(Date))
        DT.Columns.Add("Estado Origen", GetType(String))
        DT.Columns.Add("Estado Destino", GetType(String))
        DT.Columns.Add("Ubicación Origen", GetType(String))
        DT.Columns.Add("Ubicación Destino", GetType(String))
        DT.Columns.Add("Tipo Tarea", GetType(String))
        DT.Columns.Add("IdBodegaOrigen", GetType(Integer))

    End Sub

    Private Sub cargar()

        Try

            Dim cant As Double

            listar = clsLnTrans_movimientos.GetAllMovimientosByLote(txtLote.Text.Trim, cmbBodega.EditValue)

            If listar.Count > 0 Then

                DT.Clear()

                For Each Obj As clsBeVW_Movimientos In listar

                    If Obj.TTarea = "DESP" Then
                        cant = (Obj.Cantidad * -1)
                    Else
                        cant = Obj.Cantidad
                    End If

                    DT.Rows.Add(Obj.Propietario, Obj.Poliza, Obj.Producto, Obj.Codigo, Obj.CodigoBarra, cant,
                                 Obj.UMBas, Obj.Cantidad_Presentacion, Obj.Presentacion,
                                Obj.Peso, Obj.Lote, Obj.Fecha_Vence, Obj.Lic_Plate, Obj.Fecha,
                                Obj.EstadoOrigen, Obj.EstadoDestino, Obj.UbicOrigen, Obj.UbicDestino,
                                Obj.TTarea, Obj.IdBodegaOrigen)

                Next

                grdTrazaLote.DataSource = DT

                If GridView1.RowCount > 0 Then


                    GridView1.OptionsView.ShowFooter = True

                    GridView1.Columns("Código").Group()

                    Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad U.M.Bas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad U.M.Bas")}
                    GridView1.GroupSummary.Add(item)

                    Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Peso",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Peso")}
                    GridView1.GroupSummary.Add(item1)

                    Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad Presentación",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad Presentación")}
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

                    GridView1.Columns("IdBodegaOrigen").Visible = False

                    GridView1.ExpandAllGroups()
                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    GridView1.BestFitColumns(True)

                End If
            Else
                DT.Clear()
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

    Private Sub txtLote_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLote.KeyPress

        Try

            If txtLote.Text.Length = 1 Then
                DT.Clear()
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

    Private Sub txtLote_Validated(sender As Object, e As EventArgs) Handles txtLote.Validated

        Try

            If txtLote.Text <> "" Then

                cargar()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmTrazaPorLote_Load(sender As Object, e As EventArgs) Handles Me.Load

        clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
        SetDatataTable()

        '#CKFK 20190129 Agregué que se inicie en la bodega con la que se entró a la aplicación
        AP.Listar_Bodegas_By_Usuario(cmbBodega)
        cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        cargar()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        ' #EJC20260603_ROWSTYLE_PRINT_GUARD: evitar costo de formato por celda durante impresión.
        If clsUiPrintHelper.IsPrintingPreviewInProgress Then Exit Sub

        If e.Column.FieldName = "Cantidad U.M.Bas" Then

            Dim View As GridView = sender
            Dim TipoT As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Tipo Tarea"))

            If TipoT = "DESP" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                e.Appearance.ForeColor = Color.Black
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            ElseIf TipoT = "RECE" Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.ForeColor = Color.Black
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.White
            End If


        End If

    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdTrazaLote, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True, True, 12)
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
            printLink.Component = grdTrazaLote
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

        Dim reportHeader As String = vbNewLine & "Traza por lote"

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

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        cargar()
    End Sub

    Private Sub gviewEncabezadoPedido_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView1.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", GridView1.DataRowCount.ToString()))
    End Sub

End Class



