' ============================================================================
'  DGridLegacyAccess.vb
'  ----------------------------------------------------------------------------
'  Adapter chico que preserva la sintaxis "dgrid.Rows(i).Cells("X").Value"
'  sobre un DevExpress GridView. Sirve para minimizar el delta del code-behind
'  durante la migración de DataGridView -> XtraGrid.GridControl.
'
'  Uso típico (en frmAjusteStockNV):
'     dgridView.Cell(i, "ColCantidad").Value = 12.5
'     Dim v = dgridView.Cell(i, "ColCantidad").Value
'     dgridView.AppendRow(codigo, nombre, ...)         ' atajo de Rows.Add
'     dgridView.RemoveRow(i)                           ' atajo de Rows.RemoveAt
'  ----------------------------------------------------------------------------
'  Agregar este archivo al proyecto WMS (build action: Compile).
' ============================================================================

Imports System.Runtime.CompilerServices
Imports DevExpress.XtraGrid.Views.Grid

''' <summary>
''' Wrapper liviano de una celda. Default property `Value` permite usar
''' get y set con la misma sintaxis: `cell.Value = x` y `Dim y = cell.Value`.
''' </summary>
Public Class GridCellAccessor

    Private ReadOnly _view As GridView
    Private ReadOnly _row As Integer
    Private ReadOnly _col As String

    Public Sub New(view As GridView, row As Integer, col As String)
        _view = view
        _row = row
        _col = col
    End Sub

    ''' <summary>Valor crudo de la celda. Equivalente a Cells(c).Value.</summary>
    Default Public Property Value As Object
        Get
            If _view Is Nothing Then Return Nothing
            Return _view.GetRowCellValue(_row, _col)
        End Get
        Set(v As Object)
            If _view Is Nothing Then Return
            _view.SetRowCellValue(_row, _col, v)
        End Set
    End Property

    ''' <summary>Texto formateado tal como lo ve el usuario.</summary>
    Public ReadOnly Property EditedFormattedValue As Object
        Get
            If _view Is Nothing Then Return Nothing
            Return _view.GetRowCellDisplayText(_row, _col)
        End Get
    End Property

    ''' <summary>Equivalente a Cells(c).FormattedValue (DataGridView).</summary>
    Public ReadOnly Property FormattedValue As Object
        Get
            Return EditedFormattedValue
        End Get
    End Property

    ''' <summary>Permite leer la columna asociada (FieldName/Caption/etc).</summary>
    Public ReadOnly Property OwningColumn As DevExpress.XtraGrid.Columns.GridColumn
        Get
            If _view Is Nothing Then Return Nothing
            Return _view.Columns(_col)
        End Get
    End Property

    Public ReadOnly Property RowIndex As Integer
        Get : Return _row : End Get
    End Property

End Class

''' <summary>
''' Extension methods sobre GridView que preservan API histórica del
''' DataGridView. Sólo agregamos lo que el code-behind realmente usa.
''' </summary>
Public Module GridViewLegacyExtensions

    ''' <summary>
    ''' Acceso a celda por (rowHandle, fieldName). Devuelve un wrapper con
    ''' Default Property Value (read/write).
    ''' </summary>
    <Extension>
    Public Function Cell(view As GridView, row As Integer, col As String) As GridCellAccessor
        Return New GridCellAccessor(view, row, col)
    End Function

    ''' <summary>
    ''' Equivalente a `dgrid.Rows.Add(a, b, c, ...)`. Inserta una fila nueva
    ''' y la rellena por orden visual de columnas (VisibleIndex). Devuelve el
    ''' rowHandle de la fila creada (para encadenar con Cell()).
    ''' </summary>
    <Extension>
    Public Function AppendRow(view As GridView, ParamArray values As Object()) As Integer
        If view Is Nothing Then Return -1
        view.AddNewRow()
        Dim handle As Integer = view.GetRowHandle(view.DataRowCount)
        ' Recorrer columnas en orden visual
        Dim ordered = view.Columns.OrderBy(Function(c) c.VisibleIndex).
            Where(Function(c) c.VisibleIndex >= 0).
            ToList()
        For i As Integer = 0 To Math.Min(values.Length, ordered.Count) - 1
            Try
                view.SetRowCellValue(handle, ordered(i), values(i))
            Catch
                ' Silencioso: si una conversión de tipo falla, dejamos la
                ' celda en su valor por defecto. Igual que DataGridView.
            End Try
        Next
        view.UpdateCurrentRow()
        Return handle
    End Function

    ''' <summary>Equivalente a `dgrid.Rows.RemoveAt(i)`.</summary>
    <Extension>
    Public Sub RemoveRow(view As GridView, row As Integer)
        If view IsNot Nothing AndAlso row >= 0 Then view.DeleteRow(row)
    End Sub

    ''' <summary>Equivalente a `dgrid.Rows.Clear()` cuando NO hay DataSource bindeado.</summary>
    <Extension>
    Public Sub ClearAllRows(view As GridView)
        If view Is Nothing Then Return
        ' En modo unbound, borramos todas las filas. En modo bindeado, hay
        ' que limpiar el DataSource externamente; este helper no aplica.
        For h As Integer = view.DataRowCount - 1 To 0 Step -1
            view.DeleteRow(h)
        Next
    End Sub

    ''' <summary>Equivalente a `dgrid.IsCurrentCellInEditMode`.</summary>
    <Extension>
    Public Function IsCurrentCellInEditMode(view As GridView) As Boolean
        If view Is Nothing Then Return False
        Return view.IsEditing
    End Function

    ''' <summary>Equivalente a `dgrid.EndEdit()`.</summary>
    <Extension>
    Public Sub EndEdit(view As GridView)
        If view Is Nothing Then Return
        view.CloseEditor()
        view.UpdateCurrentRow()
    End Sub

End Module
