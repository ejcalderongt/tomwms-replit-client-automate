Partial Class DsRecepcion
    Partial Public Class DataDataTable
        Private Sub DataDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.columnUnidad_Medida.ColumnName) Then
                'Agregar código de usuario aquí
            End If

        End Sub

    End Class
End Class
