Imports System.Windows.Forms
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

'#EJC20260602_GRID_COPY_HELPER:
'Helper reusable para grids de solo visualizacion.
'Agrega menu contextual (click derecho) con submenu "Copiar"
'y permite copiar por columna o fila completa sin volver editable el grid.
Public NotInheritable Class clsUiGridCopyHelper

    Private Shared ReadOnly _AttachedViews As New HashSet(Of Integer)
    Private Shared ReadOnly _LockObj As New Object()

    Private Sub New()
    End Sub

    Public Shared Sub Attach(ByVal view As GridView,
                             Optional ByVal menuTitle As String = "Copiar")
        If view Is Nothing Then Return

        SyncLock _LockObj
            Dim key As Integer = Runtime.CompilerServices.RuntimeHelpers.GetHashCode(view)
            If _AttachedViews.Contains(key) Then Return

            AddHandler view.PopupMenuShowing,
                Sub(sender As Object, e As PopupMenuShowingEventArgs)
                    OnPopupMenuShowing(DirectCast(sender, GridView), e, menuTitle)
                End Sub

            _AttachedViews.Add(key)
        End SyncLock
    End Sub

    '#EJC20260603_GRID_COPY_HELPER:
    'Atacha todos los GridView encontrados dentro de un formulario (GridControl.MainView y ViewCollection).
    Public Shared Sub AttachToForm(ByVal frm As Form,
                                   Optional ByVal menuTitle As String = "Copiar")
        If frm Is Nothing Then Return
        AttachToControlRecursive(frm, menuTitle)
    End Sub

    Private Shared Sub AttachToControlRecursive(ByVal parent As Control,
                                                ByVal menuTitle As String)
        If parent Is Nothing Then Return

        Dim grid As GridControl = TryCast(parent, GridControl)
        If grid IsNot Nothing Then
            Dim gvMain As GridView = TryCast(grid.MainView, GridView)
            If gvMain IsNot Nothing Then
                Attach(gvMain, menuTitle)
            End If

            For i As Integer = 0 To grid.ViewCollection.Count - 1
                Dim gv As GridView = TryCast(grid.ViewCollection(i), GridView)
                If gv IsNot Nothing Then
                    Attach(gv, menuTitle)
                End If
            Next
        End If

        For Each child As Control In parent.Controls
            AttachToControlRecursive(child, menuTitle)
        Next
    End Sub

    Private Shared Sub OnPopupMenuShowing(ByVal view As GridView,
                                          ByVal e As PopupMenuShowingEventArgs,
                                          ByVal menuTitle As String)

        If view Is Nothing OrElse e Is Nothing OrElse e.Menu Is Nothing Then Return
        If e.HitInfo Is Nothing OrElse e.HitInfo.InRow = False Then Return

        Dim rowHandle As Integer = e.HitInfo.RowHandle
        If rowHandle < 0 Then Return

        Dim copySubMenu As New DXSubMenuItem(menuTitle)

        For Each col As GridColumn In view.VisibleColumns.OrderBy(Function(c) c.VisibleIndex)
            If col Is Nothing OrElse col.Visible = False Then Continue For

            Dim txtToCopy As String = GetCellText(view, rowHandle, col)
            Dim colName As String = GetColumnDisplayName(col)
            Dim valuePreview As String = txtToCopy
            If valuePreview.Length > 48 Then
                valuePreview = valuePreview.Substring(0, 48) & "..."
            End If

            Dim itemCaption As String
            If String.IsNullOrWhiteSpace(valuePreview) Then
                itemCaption = String.Format("{0}", colName)
            Else
                itemCaption = String.Format("{0}: {1}", colName, valuePreview)
            End If

            Dim menuItem As New DXMenuItem(itemCaption,
                Sub()
                    SafeCopyToClipboard(txtToCopy)
                End Sub)

            copySubMenu.Items.Add(menuItem)
        Next

        copySubMenu.Items.Add(New DXMenuItem("Fila (columnas visibles)",
            Sub()
                SafeCopyToClipboard(BuildRowVisibleText(view, rowHandle))
            End Sub))

        If copySubMenu.Items.Count > 0 Then
            e.Menu.Items.Add(copySubMenu)
        End If
    End Sub

    Private Shared Function BuildRowVisibleText(ByVal view As GridView,
                                                ByVal rowHandle As Integer) As String
        Dim parts As New List(Of String)

        For Each col As GridColumn In view.VisibleColumns.OrderBy(Function(c) c.VisibleIndex)
            If col Is Nothing OrElse col.Visible = False Then Continue For
            Dim valueText As String = GetCellText(view, rowHandle, col)
            parts.Add(String.Format("{0}: {1}", GetColumnDisplayName(col), valueText))
        Next

        Return String.Join(" | ", parts)
    End Function

    Private Shared Function GetColumnDisplayName(ByVal col As GridColumn) As String
        If col Is Nothing Then Return "(Columna)"

        Dim n As String = If(col.Caption, "").Trim()
        If n = "" Then n = If(col.FieldName, "").Trim()
        If n = "" Then n = If(col.Name, "").Trim()
        If n = "" Then n = String.Format("Columna {0}", col.VisibleIndex)
        Return n
    End Function

    Private Shared Function GetCellText(ByVal view As GridView,
                                        ByVal rowHandle As Integer,
                                        ByVal col As GridColumn) As String
        Try
            Dim value = view.GetRowCellDisplayText(rowHandle, col)
            Return If(value, String.Empty).Trim()
        Catch
            Return String.Empty
        End Try
    End Function

    Private Shared Sub SafeCopyToClipboard(ByVal textValue As String)
        Try
            Dim val As String = If(textValue, String.Empty)
            Clipboard.SetText(val)
        Catch
        End Try
    End Sub

End Class
