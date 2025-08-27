Imports System.Data.SqlClient

Partial Public Class clsLnTurno

    Public Shared Function Listar(ByVal pActivo As Boolean, ByVal pFiltro As String) As DataTable

        Try

            Dim sp As String = "SELECT IdBodega,IdTurno AS Código, Nombre FROM turno WHERE 1 > 0 "

            If pActivo Then
                sp += " AND Activo=1"
            Else
                sp += " AND Activo=0"
            End If

            If Not String.IsNullOrEmpty(pFiltro) Then

                '#HS20171023_1640pm: Quité String.Format.
                sp += " AND (IdTurno LIKE '%@IdTurno%'"
                sp += " OR Nombre LIKE '%@Nombre%')"

            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            If Not String.IsNullOrEmpty(pFiltro) Then
                dad.SelectCommand.Parameters.AddWithValue("@IdTurno", pFiltro)
                dad.SelectCommand.Parameters.AddWithValue("@Nombre", pFiltro)
            End If
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Turnos_por_Bodega(ByVal pIdBodega As Integer) As DataTable

        Try

            Dim sp As String = " select IdTurno,nombre from turno where activo=1 and IdBodega = @pIdBodega "


            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
