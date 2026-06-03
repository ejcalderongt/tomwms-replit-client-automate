Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmPickingUbicacionBuscador

    Public pObjPickingUbicacion As clsBeTrans_picking_ubic
    Public pIdPropietarioBodega As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmPickingUbicacionBuscador_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListaPickingUbicacion()
    End Sub

    Private Sub ListaPickingUbicacion()

        Try

            Dgrid.DataSource = clsLnTrans_picking_ubic.Get_Picking_Ubicacion(chkActivos.Checked, dtpFechaDel.Value, dtpFechaAl.Value, pIdPropietarioBodega)

            If GridView1.Columns.Count > 0 Then
                GridView1.Columns("activo").Visible = False
                GridView1.Columns("IdPickingUbic").Visible = False
                GridView1.Columns("IdPropietarioBodega").Visible = False
                GridView1.Columns("IdProductoBodega").Visible = False
                GridView1.Columns("IdEstado").Visible = False
                GridView1.Columns("IdPresentacion").Visible = False
                GridView1.Columns("IdUnidadMedidaBasica").Visible = False
                GridView1.Columns("IdPedidoDet").Visible = False
            End If
            If GridView1.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
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

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        Try

            If GridView1.RowCount > 0 Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Picking Ubicación")

                Dim dt As DataTable = Dgrid.DataSource

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                pObjPickingUbicacion = New clsBeTrans_picking_ubic

                Dim query = From detalle In dt.AsEnumerable Where detalle("IdPickingUbic").Equals(Dr.Item("IdPickingUbic")) Select detalle

                pObjPickingUbicacion.IdPickingUbic = query(0).Item("IdPickingUbic")
                pObjPickingUbicacion.CodigoProducto = query(0).Item("Código")
                pObjPickingUbicacion.NombreProducto = query(0).Item("Producto")
                pObjPickingUbicacion.ProductoPresentacion = query(0).Item("Presentación")
                pObjPickingUbicacion.ProductoUnidadMedida = query(0).Item("Unidad Medida")
                pObjPickingUbicacion.ProductoEstado = query(0).Item("Estado")
                pObjPickingUbicacion.NombreUbicacion = query(0).Item("Ubicación")
                pObjPickingUbicacion.Cantidad_Solicitada = query(0).Item("Solicitado")
                pObjPickingUbicacion.Cantidad_Recibida = query(0).Item("Picking")
                pObjPickingUbicacion.Cantidad_Verificada = query(0).Item("Verificado")

                pObjPickingUbicacion.IdPedidoEnc = query(0).Item("Pedido")
                pObjPickingUbicacion.IdPedidoDet = query(0).Item("IdPedidoDet")

                pObjPickingUbicacion.IdProductoBodega = query(0).Item("IdProductoBodega")
                pObjPickingUbicacion.IdProductoEstado = query(0).Item("IdProductoEstado")
                pObjPickingUbicacion.IdPresentacion = query(0).Item("IdPresentacion")
                pObjPickingUbicacion.IdPresentacion = query(0).Item("IdPresentacion")
                pObjPickingUbicacion.IdUnidadMedida = query(0).Item("IdUnidadMedida")

                SplashScreenManager.CloseForm(False)

                Hide()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub


    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub


    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        ListaPickingUbicacion()
    End Sub


    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub


    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(Dgrid, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
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
            printLink.Component = Dgrid
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

        Dim reportHeader As String = vbNewLine & "Listado de Picking Ubicación"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub


    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            ListaPickingUbicacion()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            'If Me.dtpFechaDel.Value > Me.dtpFechaAl.Value Or Me.dtpFechaAl.Value < Me.dtpFechaDel.Value Then Throw New Exception("Seleccione un rango de fechas válido.")

            ListaPickingUbicacion()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

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

End Class


