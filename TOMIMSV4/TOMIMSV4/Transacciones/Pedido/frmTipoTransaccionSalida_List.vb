Imports DevExpress.XtraEditors

Public Class frmTipoTransaccionSalida_List

    Public BePedidoTipo As New clsBeTrans_pe_tipo
    Public pFiltro As Boolean
    Public pConReferencia As Boolean
    Public pTipoTrans As String

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub Listar_TipoTransaccion()

        Try

            Dgrid.DataSource = clsLnTrans_pe_tipo.Get_All_DT(True)

            If GridView1.RowCount > 0 Then
                lblRegistro.Caption = String.Format("Registros: {0}", GridView1.RowCount)
            End If

            Try
                GridView1.OptionsView.ColumnAutoWidth = False
                If GridView1.Columns.Count > 0 Then
                    GridView1.BestFitColumns()
                End If
            Catch ex As Exception
            End Try

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmTipoTransaccionSalida_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Listar_TipoTransaccion()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_TipoTransaccion()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Try
            If (GridView1.RowCount > 0) Then
                Dim Dr As DataRowView = GridView1.GetFocusedRow
                BePedidoTipo.IdTipoPedido = Dr.Item("IdTipoPedido").ToString
                BePedidoTipo = clsLnTrans_pe_tipo.GetSingle(BePedidoTipo.IdTipoPedido)
                If Modo = pModo.Seleccion Then
                    DialogResult = DialogResult.OK
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_TipoTransaccion()
    End Sub

    Private Sub txtFiltro_EditValueChanged(sender As Object, e As EventArgs)
        Listar_TipoTransaccion()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
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
            printLink.Component = Dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Try

            Dim reportHeader As String = vbNewLine & "Listado de tipo de documentos salida"

            e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
            e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

            Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
            e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
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

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dgrid_DoubleClick(Nothing, Nothing)
        End If
    End Sub

    Private Sub Dgrid_Click(sender As Object, e As EventArgs) Handles Dgrid.Click

    End Sub
End Class