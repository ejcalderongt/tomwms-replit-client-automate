Imports DevExpress.XtraEditors

Public Class frmRptMovPorDocu

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub frmRptMovPorDocu_Load(sender As Object, e As EventArgs) Handles Me.Load
        clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
        Cargar()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdMovimiento_Documento, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
            Exit Sub
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
            printLink.Component = grdMovimiento_Documento
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

        Dim reportHeader As String = vbNewLine & "Listado de movimientos"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub


    Private Sub Cargar()

        Try

            Dim DT As DataTable = clsLnTrans_movimientos.GetMovimientosPorDocumento

            grdMovimiento_Documento.DataSource = Nothing

            If DT.Rows.Count > 0 Then

                grdMovimiento_Documento.DataSource = DT

                GridView1.OptionsView.ShowFooter = True

                GridView1.BestFitColumns()

                GridView1.Columns("cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("cantidad").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Propietario").Visible = False
                GridView1.Columns("Estado Origen").Visible = False
                GridView1.Columns("Estado Destino").Visible = False
                GridView1.Columns("peso").Visible = False
                GridView1.Columns("IdBodegaOrigen").Visible = False
                GridView1.Columns("IdProducto").Visible = False
                GridView1.Columns("codigo_barra").Visible = False

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

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Cargar()
    End Sub
End Class



