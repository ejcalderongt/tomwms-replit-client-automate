Public Class frmTramo

    Property tipo As Integer

    'Private Sub frmTramo_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    Dim DT As New DataTable
    '    Dim vSQL As String = ""
    '    Try

    '        If tipo = 1 Then

    '            vSQL = "Select Codigo, descripcion, alto, largo, ancho,pos_x, pos_y, horizontal
    '                From bodega_sector Where IdBodega = 14"
    '        ElseIf tipo = 2 Then

    '            vSQL = "select Codigo, descripcion, alto, largo, ancho, horizontal,Orientacion, margen_izquierdo, margen_superior, margen_inferior
    '                    from bodega_tramo where IdBodega = 14"
    '        End If
    '        BD.OpenDT(DT, vSQL)
    '        dgvTramos.DataSource = DT

    '    Catch ex As Exception

    '    End Try

    'End Sub
End Class