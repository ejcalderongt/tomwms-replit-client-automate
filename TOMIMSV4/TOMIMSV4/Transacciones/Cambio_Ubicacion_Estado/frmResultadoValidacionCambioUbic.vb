Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Partial Public Class frmResultadoValidacionCambioUbic

    Public Property BeListaErrores As New List(Of clsBeResultadoValidacionCambioUbic)
    Public Property CantidadValidos As Integer = 0

    Private Sub frmResultadoValidacionCambioUbic_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "Resultado validación cambio de ubicación"

        CargarDatos()

    End Sub

    Private Sub CargarDatos()


        dgErrores.DataSource = BeListaErrores

        If gvErrores.Columns.Count > 0 Then

            If gvErrores.Columns("IdStock") IsNot Nothing Then
                gvErrores.Columns("IdStock").Caption = "Id Stock"
                gvErrores.Columns("IdStock").Width = 90
            End If

            If gvErrores.Columns("CodigoProducto") IsNot Nothing Then
                gvErrores.Columns("CodigoProducto").Caption = "Código"
                gvErrores.Columns("CodigoProducto").Width = 110
            End If

            If gvErrores.Columns("NombreProducto") IsNot Nothing Then
                gvErrores.Columns("NombreProducto").Caption = "Producto"
                gvErrores.Columns("NombreProducto").Width = 260
            End If

            If gvErrores.Columns("IdUbicacionOrigen") IsNot Nothing Then
                gvErrores.Columns("IdUbicacionOrigen").Caption = "Ubicación origen"
                gvErrores.Columns("IdUbicacionOrigen").Width = 120
            End If

            If gvErrores.Columns("IdUbicacionDestino") IsNot Nothing Then
                gvErrores.Columns("IdUbicacionDestino").Caption = "Ubicación destino"
                gvErrores.Columns("IdUbicacionDestino").Width = 120
            End If

            If gvErrores.Columns("Motivo") IsNot Nothing Then
                gvErrores.Columns("Motivo").Caption = "Motivo"
                gvErrores.Columns("Motivo").Width = 320
            End If

        End If

        gvErrores.BestFitColumns()

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
            printLink.Component = dgErrores
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)
        Dim reportHeader As String = ""
        reportHeader = vbNewLine & "Listado de errores de cambios de ubicación"


        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub mnuImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Me.Close()
    End Sub
End Class