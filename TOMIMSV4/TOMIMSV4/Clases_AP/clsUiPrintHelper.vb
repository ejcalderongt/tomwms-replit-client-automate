Imports System.Drawing
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

'#EJC20260602_PRINT_HELPER:
'Helper de cabecera estandar para impresiones de grid con PrintableComponentLink.
'Permite homologar encabezado visual sin cambiar el flujo de impresion existente.
Public NotInheritable Class clsUiPrintHelper

    Private Sub New()
    End Sub

    ' #EJC20260603_PRINT_GUARD:
    ' Bandera compartida para evitar lógica costosa de estilos durante CreateDocument.
    Public Shared Property IsPrintingPreviewInProgress As Boolean = False

    Private NotInheritable Class PrintColumnState
        Public Property Column As GridColumn
        Public Property Visible As Boolean
        Public Property VisibleIndex As Integer
    End Class

    Private NotInheritable Class PrintColumnScope
        Implements IDisposable

        Private ReadOnly _view As GridView
        Private ReadOnly _states As List(Of PrintColumnState)

        Public Sub New(ByVal view As GridView, ByVal states As List(Of PrintColumnState))
            _view = view
            _states = states
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            If _view Is Nothing OrElse _states Is Nothing Then Return

            _view.BeginUpdate()
            Try
                For Each s In _states
                    If s.Column Is Nothing Then Continue For
                    s.Column.Visible = s.Visible
                    If s.Visible Then
                        s.Column.VisibleIndex = s.VisibleIndex
                    End If
                Next
            Finally
                _view.EndUpdate()
            End Try
        End Sub
    End Class

    Private NotInheritable Class NullScope
        Implements IDisposable
        Public Sub Dispose() Implements IDisposable.Dispose
        End Sub
    End Class

    Private Shared Function NormalizeName(ByVal text As String) As String
        If String.IsNullOrWhiteSpace(text) Then Return ""
        Dim s As String = text.Trim().ToLowerInvariant()
        Return s.Replace("_", "").Replace(" ", "").Replace(".", "")
    End Function

    Private Shared Function ScoreColumn(ByVal col As GridColumn) As Integer
        Dim nField As String = NormalizeName(col.FieldName)
        Dim nCaption As String = NormalizeName(col.Caption)
        Dim n As String = nField & "|" & nCaption

        Dim score As Integer = 0

        If n.Contains("codigo") OrElse n.Contains("code") OrElse n.Contains("sku") Then score += 100
        If n.Contains("nombre") OrElse n.Contains("producto") Then score += 95
        If n.Contains("lote") Then score += 90
        If n.Contains("vence") OrElse n.Contains("fechavence") Then score += 88
        If n.Contains("licencia") OrElse n.Contains("licplate") Then score += 85
        If n.Contains("estado") Then score += 80
        If n.Contains("cantidad") OrElse n.Contains("existencia") OrElse n.Contains("stock") OrElse n.Contains("saldo") Then score += 78
        If n.Contains("umbas") OrElse n.Contains("um") OrElse n.Contains("unidadmedida") Then score += 72
        If n.Contains("presentacion") Then score += 70
        If n.Contains("peso") Then score += 65
        If n.Contains("ubicacion") Then score += 60

        Return score
    End Function

    ' #EJC20260603_PRINT_RELEVANT_COLUMNS:
    ' Scope temporal que selecciona columnas relevantes para impresión y luego restaura el grid.
    Public Shared Function BeginRelevantColumnsScope(ByVal grid As GridControl,
                                                     Optional ByVal maxColumns As Integer = 12) As IDisposable

        If grid Is Nothing Then Return New NullScope()

        Dim view As GridView = TryCast(grid.MainView, GridView)
        If view Is Nothing Then Return New NullScope()

        Dim visibleCols As List(Of GridColumn) =
            view.Columns.Cast(Of GridColumn)().
            Where(Function(c) c IsNot Nothing AndAlso c.Visible AndAlso c.OptionsColumn.ShowInCustomizationForm).
            OrderBy(Function(c) c.VisibleIndex).
            ToList()

        If visibleCols.Count = 0 Then Return New NullScope()
        If visibleCols.Count <= maxColumns Then Return New NullScope()

        Dim states As New List(Of PrintColumnState)
        For Each col In visibleCols
            states.Add(New PrintColumnState With {
                .Column = col,
                .Visible = col.Visible,
                .VisibleIndex = col.VisibleIndex
            })
        Next

        Dim selected As List(Of GridColumn) =
            visibleCols.
            Select(Function(c) New With {.Col = c, .Score = ScoreColumn(c)}).
            OrderByDescending(Function(x) x.Score).
            ThenBy(Function(x) x.Col.VisibleIndex).
            Where(Function(x) x.Score > 0).
            Select(Function(x) x.Col).
            Take(maxColumns).
            ToList()

        If selected.Count = 0 Then
            selected = visibleCols.Take(maxColumns).ToList()
        End If

        view.BeginUpdate()
        Try
            For Each col In visibleCols
                col.Visible = selected.Contains(col)
            Next

            Dim idx As Integer = 0
            For Each col In selected.OrderBy(Function(c) c.VisibleIndex)
                col.VisibleIndex = idx
                idx += 1
            Next

            view.OptionsPrint.AutoWidth = True
            view.BestFitColumns()
        Finally
            view.EndUpdate()
        End Try

        Return New PrintColumnScope(view, states)
    End Function

    Public Shared Sub DrawStandardHeader(ByVal e As CreateAreaEventArgs,
                                         ByVal title As String,
                                         ByVal subTitle As String,
                                         ByVal bodega As String)
        If e Is Nothing Then Return

        Dim safeTitle As String = If(title, "").Trim()
        Dim safeSub As String = If(subTitle, "").Trim()
        Dim safeBodega As String = If(bodega, "").Trim()

        Dim headText As String =
            "TOM WMS" & vbNewLine &
            safeTitle & vbNewLine &
            safeSub & vbNewLine &
            "BODEGA: " & safeBodega

        e.Graph.StringFormat = New BrickStringFormat(StringAlignment.Near)
        e.Graph.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)

        Dim headRect As New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 78)
        e.Graph.DrawString(headText, Color.Black, headRect, BorderSide.None)

        'Compatibilidad: evitar sobrecargas de DrawLine que varian por version de DevExpress.
        Dim sepText As String = New String("_"c, 220)
        Dim sepRect As New RectangleF(0, 78, e.Graph.ClientPageSize.Width, 10)
        e.Graph.Font = New Font("Segoe UI", 8, FontStyle.Regular)
        e.Graph.DrawString(sepText, Color.Gray, sepRect, BorderSide.None)
    End Sub

    '#EJC20260603_PRINT_HELPER_GRID:
    'Pipeline estandar para imprimir vistas de GridControl usando el formato historico
    'de encabezado/footer y permitiendo un handler custom de cabecera por formulario.
    Public Shared Sub PrintGridPreview(ByVal grid As GridControl,
                                       ByVal usuarioNombre As String,
                                       Optional ByVal headerHandler As CreateAreaEventHandler = Nothing,
                                       Optional ByVal landscape As Boolean = True,
                                       Optional ByVal useRelevantColumns As Boolean = False,
                                       Optional ByVal maxColumns As Integer = 12)

        If grid Is Nothing Then Exit Sub

        Try
            IsPrintingPreviewInProgress = True

            Using printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
                Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()
                Dim safeUser As String = If(usuarioNombre, "").Trim()
                Dim gridTitle As String = "Reporte"

                Try
                    If grid.MainView IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(grid.MainView.ViewCaption) Then
                        gridTitle = grid.MainView.ViewCaption.Trim()
                    ElseIf Not String.IsNullOrWhiteSpace(grid.Name) Then
                        gridTitle = grid.Name.Trim()
                    End If
                Catch ex As Exception
                End Try

                If headerHandler IsNot Nothing Then
                    AddHandler printLink.CreateReportHeaderArea, headerHandler
                End If

                ' #EJC20260603_PRINT_STANDARD_HEADER:
                ' Encabezado estandar visible en todas las vistas, sin depender del handler legacy de cada forma.
                AddHandler printLink.CreateMarginalHeaderArea,
                    Sub(sender As Object, e As CreateAreaEventArgs)
                        DrawStandardHeader(e,
                                           "TOM WMS - " & gridTitle,
                                           "Usuario: " & safeUser,
                                           "")
                    End Sub

                Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
                Dim leftColumnHead As String = "Usuario: [User Name] - " & If(usuarioNombre, "")
                Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

                Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
                    TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

                phf.Header.Content.Clear()
                phf.Footer.Content.AddRange(New String() {leftColumnFoot})
                phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near
                phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
                phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

                printLink.Margins = New System.Drawing.Printing.Margins(40, 40, 120, 60)
                printLink.PaperKind = System.Drawing.Printing.PaperKind.Letter
                printingSystem1.PageSettings.Landscape = landscape
                printLink.Component = grid
                printLink.Landscape = landscape

                Dim colScope As IDisposable = Nothing
                Try
                    If useRelevantColumns Then
                        colScope = BeginRelevantColumnsScope(grid, maxColumns)
                    End If

                    printLink.CreateDocument(printingSystem1)
                Finally
                    If colScope IsNot Nothing Then
                        colScope.Dispose()
                    End If
                End Try

                printingSystem1.PreviewFormEx.ShowDialog()
            End Using
        Finally
            IsPrintingPreviewInProgress = False
        End Try
    End Sub

End Class
