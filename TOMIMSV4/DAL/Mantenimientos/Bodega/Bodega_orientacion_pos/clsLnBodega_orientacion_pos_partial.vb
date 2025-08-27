Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnBodega_orientacion_pos

    Public Shared Function Get_Codigo_By_IdOrientacionPos(ByVal pIdOrientacionPos As Integer, _
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As String

        Get_Codigo_By_IdOrientacionPos = ""

        Try

            Const sp As String = "SELECT Codigo FROM Bodega_orientacion_pos" & _
            " Where(IdOrientacionPos = @IdOrientacionPos)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORIENTACIONPOS", pIdOrientacionPos))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_Codigo_By_IdOrientacionPos = IIf(IsDBNull(dt.Rows(0).Item("codigo")), "", dt.Rows(0).Item("codigo"))
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


End Class
