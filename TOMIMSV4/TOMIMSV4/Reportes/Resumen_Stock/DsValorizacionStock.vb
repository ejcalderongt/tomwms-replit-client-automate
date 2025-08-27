Partial Class DsValorizacionStock
    Partial Public Class DetalleDataTable
        Private Sub DetalleDataTable_DetalleRowChanging(sender As Object, e As DetalleRowChangeEvent) Handles Me.DetalleRowChanging

        End Sub

        Private Sub DetalleDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.IdProductoColumn.ColumnName) Then
                'Agregar código de usuario aquí
            End If

        End Sub

    End Class

    Partial Public Class EncabezadoDataTable
        Private Sub EncabezadoDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.IdProductoColumn.ColumnName) Then
                'Agregar código de usuario aquí
            End If

        End Sub

    End Class
End Class
