

Partial Public Class DsExistenciasConReserva
    Partial Public Class DetalleDataTable
    End Class

    Partial Public Class EncabezadoDataTable
        Private Sub EncabezadoDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.Disponible_PresentaciónColumn.ColumnName) Then
                'Agregar código de usuario aquí
            End If

        End Sub

    End Class

End Class
