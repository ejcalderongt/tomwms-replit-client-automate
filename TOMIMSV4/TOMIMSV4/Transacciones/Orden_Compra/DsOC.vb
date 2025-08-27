Partial Class DsOC
    Partial Public Class DetalleDataTable
        Private Sub DetalleDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.costo_estadisticoColumn.ColumnName) Then

                'Add user code here
            End If

        End Sub

    End Class

    Partial Public Class EncabezadoDataTable
        Private Sub EncabezadoDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.CódigoColumn.ColumnName) Then
                'Agregar código de usuario aquí
            End If

        End Sub

    End Class
End Class
